using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class App_Caract_Tipo_Carga : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ObtenerCaractTipoCarga(true);
    }
    #region GridView
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerCaractTipoCarga(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            hf_idCaractTipoCarga.Value = e.CommandArgument.ToString();
            utils.AbrirModal(this, "modalConf");
        }
        if(e.CommandName == "MODIFICAR")
        {
            CaractTipoCargaBC cct = new CaractTipoCargaBC();
            hf_idCaractTipoCarga.Value = e.CommandArgument.ToString();
            cct.ID = Convert.ToInt32(hf_idCaractTipoCarga.Value);
            cct = cct.obtenerXID();
            txt_editId.Text = cct.ID.ToString();
            txt_editDesc.Text = cct.DESCRIPCION;
            if (cct.EXCLUYENTE)
                chk_editExcluyente.Checked = true;
            else
                chk_editExcluyente.Checked = false;
            utils.AbrirModal(this, "modalEdit");
        }
    }
    protected void ObtenerCaractTipoCarga(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            CaractTipoCargaBC caract_tipo_carga = new CaractTipoCargaBC();
            DataTable dt = caract_tipo_carga.obtenerTodoCaractTipoCarga();
            ViewState["lista"] = dt;
            ViewState.Remove("filtrados");
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["filtrados"] == null)
            dw = new DataView((DataTable)ViewState["lista"]);
        else
            dw = new DataView((DataTable)ViewState["filtrados"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }
    #endregion
    #region Buttons
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        hf_idCaractTipoCarga.Value = "";
        txt_editDesc.Text = "";
        chk_editExcluyente.Checked = false;
            utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_buscarNombre.Text))
            
            ObtenerCaractTipoCarga(true);
        else
        {
            DataView dw = new DataView((DataTable)ViewState["lista"]);
            dw.RowFilter = "DESCRIPCION LIKE '%" + txt_buscarNombre.Text + "%'";
            ViewState["filtrados"] = dw.ToTable();
            ObtenerCaractTipoCarga(false);
        }
    }
    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        CaractTipoCargaBC cct = new CaractTipoCargaBC();
        cct.DESCRIPCION = txt_editDesc.Text;
        if (chk_editExcluyente.Checked)
            cct.EXCLUYENTE = true;
        else
            cct.EXCLUYENTE = false;
        cct.ID = Convert.ToInt32(txt_editId.Text);
        if (hf_idCaractTipoCarga.Value == "")
        {
            if (cct.Crear())
            {
                utils.ShowMessage2(this, "crear", "success");
                utils.CerrarModal(this, "modalEdit");
            }
            else
            {
                utils.ShowMessage2(this, "crear", "error");
            }
        }
        else
        {
            cct.ID = Convert.ToInt32(hf_idCaractTipoCarga.Value);
            if (cct.Modificar())
            {
                utils.ShowMessage2(this, "modificar", "success");
                utils.CerrarModal(this, "modalEdit");
            }
            else
            {
                utils.ShowMessage2(this, "modificar", "error");
            }
        }
        ObtenerCaractTipoCarga(true);
    }
    protected void btn_EliminarCaractTipoCarga_Click(object sender, EventArgs e)
    {
        CaractTipoCargaBC cct = new CaractTipoCargaBC();
        cct.ID = Convert.ToInt32(hf_idCaractTipoCarga.Value);
        if (cct.Eliminar())
        {
            utils.ShowMessage2(this, "eliminar", "success");
            utils.CerrarModal(this, "modalConf");
        }
        else
        {
            utils.ShowMessage2(this, "eliminar", "error");
        }
        ObtenerCaractTipoCarga(true);
    }
    #endregion

    protected void gv_listar_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
}