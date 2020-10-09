using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Caract_Carga : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    protected void Page_Load(object sender, EventArgs e)
    {
        CaractTipoCargaBC catt = new CaractTipoCargaBC();
        if (!IsPostBack)
        {
            utils.CargaDrop(ddl_buscarTipo, "ID", "DESCRIPCION", catt.obtenerTodoCaractTipoCarga());
            utils.CargaDrop(ddl_editTipo, "ID", "DESCRIPCION", catt.obtenerTodoCaractTipoCarga());
            ddl_editTipo.Items[0].Value = "-1";
            ObtenerCaractCarga(true);
        }
    }
    #region GridView
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerCaractCarga(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            Limpiar();
            hf_idCaractCarga.Value = e.CommandArgument.ToString();
            utils.AbrirModal(this, "modalConf");
        }
        if(e.CommandName == "MODIFICAR")
        {
            CaractCargaBC cc = new CaractCargaBC();
            Limpiar();
            hf_idCaractCarga.Value = e.CommandArgument.ToString();
            cc.ID = Convert.ToInt32(e.CommandArgument);
            cc = cc.obtenerSeleccionado();
            txt_editCodigo.Text = cc.CODIGO.ToString();
            txt_editDesc.Text = cc.DESCRIPCION;
            txt_editValor.Text = cc.VALOR.ToString();
            ddl_editTipo.SelectedValue = cc.CACT_ID.ToString();
            utils.AbrirModal(this, "modalEdit");
        }
    }
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
    #endregion
    #region Buttons
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
            ObtenerCaractCarga(true);
    }
    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        CaractCargaBC caca = new CaractCargaBC();
        caca.DESCRIPCION = txt_editDesc.Text;
        caca.VALOR = int.Parse(txt_editValor.Text);
        caca.CACT_ID = int.Parse(ddl_editTipo.SelectedValue);
        caca.CODIGO = txt_editCodigo.Text;
        if (hf_idCaractCarga.Value == "")
        {
            if (caca.Crear(caca))
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
            caca.ID = int.Parse(hf_idCaractCarga.Value);
            if (caca.Modificar(caca))
            {
                utils.ShowMessage2(this, "modificar", "success");
                utils.CerrarModal(this, "modalEdit");
            }
            else
            {
                utils.ShowMessage2(this, "modificar", "error");
            }
        }
        ObtenerCaractCarga(true);
    }
    protected void btn_EliminarCaractCarga_Click(object sender, EventArgs e)
    {
        CaractCargaBC caca = new CaractCargaBC();
        if (caca.Eliminar(int.Parse(hf_idCaractCarga.Value)))
        {
            utils.ShowMessage2(this, "eliminar", "success");
            utils.CerrarModal(this, "modalConf");
        }
        else
        {
            utils.ShowMessage2(this, "eliminar", "error");
        }
        ObtenerCaractCarga(true);
    }
    #endregion
    #region UtilsPagina
    private void Limpiar()
    {
        hf_idCaractCarga.Value = "";
        txt_editDesc.Text = "";
        txt_editValor.Text = "";
        ddl_editTipo.ClearSelection();
        txt_editCodigo.Text = "";
    }
    protected void ObtenerCaractCarga(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            CaractCargaBC caca = new CaractCargaBC();
            int cact_id = Convert.ToInt32(ddl_buscarTipo.SelectedValue);
            DataTable dt = caca.obtenerXParametro(txt_buscarDescripcion.Text,txt_buscarCodigo.Text,cact_id);
            ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }
    #endregion
}