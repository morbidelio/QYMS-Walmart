// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Servicios_Externos : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ObtenerServicios(false);
    }

    #region Buttons

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerServicios(true);
    }

    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        this.hf_id.Value = "";
        this.txt_editCodigo.Text = "";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditar();", true);
    }

    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {
        ServiciosExternosBC s = new ServiciosExternosBC();
        if (s.Eliminar(int.Parse(this.hf_id.Value)))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Servicio externo eliminado exitosamente');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Error');", true);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmar');", true);
        this.ObtenerServicios(true);
    }

    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        ServiciosExternosBC s = new ServiciosExternosBC();
        s.CODIGO = this.txt_editCodigo.Text;
        if (string.IsNullOrEmpty(this.hf_id.Value))
        {
            if (s.Crear())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert('Servicio externo agregado correctamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalEditar');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Error');", true);
            }
        }
        else
        {
            s.SEEX_ID = Convert.ToInt32(this.hf_id.Value);
            if (s.Modificar())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert('Servicio externo modificado correctamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalEditar');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Error');", true);
            }
        }
        this.ObtenerServicios(true);
    }

    #endregion

    #region DropDownList

    protected void GoPage(object sender, System.EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        if (int.TryParse((oIraPag.Text), out iNumPag) && iNumPag > 0 && iNumPag <= this.gv_listar.PageCount)
        {
            if (int.TryParse(oIraPag.Text, out iNumPag) && iNumPag > 0 && iNumPag <= this.gv_listar.PageCount)
            {
                this.gv_listar.PageIndex = iNumPag - 1;
            }
            else
            {
                this.gv_listar.PageIndex = 0;
            }
        }
        this.ObtenerServicios(false);
    }

    #endregion

    #region GridView

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerServicios(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            ServiciosExternosBC s = new ServiciosExternosBC();
            this.hf_id.Value = e.CommandArgument.ToString();
            s.SEEX_ID = Convert.ToInt32(this.hf_id.Value);
            s = s.ObtenerXId();
            this.txt_editCodigo.Text = s.CODIGO;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditar();", true);
        }
        else if (e.CommandName == "ELIMINAR")
        {
            this.hf_id.Value = e.CommandArgument.ToString();
            this.lbl_tituloConfirmar.Text = "Eliminar servicio externo";
            this.lbl_msjConfirmar.Text = "Se eliminará el servicio externo seleccionado, ¿desea continuar?";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalConfirmar();", true);
        }
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            this.gv_listar.PageIndex = e.NewPageIndex;
        }
        this.ObtenerServicios(false);
    }

    #endregion

    #region UtilsPagina

    private void ObtenerServicios(bool forzarBD)
    {
        if (this.ViewState["listar"] == null || forzarBD)
        {
            ServiciosExternosBC s = new ServiciosExternosBC();
            string codigo = txt_buscarCodigo.Text;
            DataTable dt = s.ObtenerXParametros(codigo);
            this.ViewState["listar"] = dt;
        }
        DataView dw = new DataView((DataTable)this.ViewState["listar"]);
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw.ToTable();
        this.gv_listar.DataBind();
    }
    #endregion
}