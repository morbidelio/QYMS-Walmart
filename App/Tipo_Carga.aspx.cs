using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Tipo_Cargav1 : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ObtenerTipoCarga(true);
    }

    #region GridView

    protected void GoPage(object sender, System.EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        if (int.TryParse((oIraPag.Text), out iNumPag) && iNumPag > 0 && iNumPag <= gv_listar.PageCount)
        {
            if (int.TryParse(oIraPag.Text, out iNumPag) && iNumPag > 0 && iNumPag <= gv_listar.PageCount)
            {
                gv_listar.PageIndex = iNumPag - 1;
            }
            else
            {
                gv_listar.PageIndex = 0;
            }
        }
        ObtenerTipoCarga(false);
    }

    protected void gv_listaTipoCarga_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (gv_listar.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = gv_listar.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(gv_listar.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = Convert.ToString(gv_listar.PageIndex + 1);
        }
    }

    protected void gv_listaTipoCarga_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerTipoCarga(false);
    }

    protected void gv_listaTipoCarga_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            hf_idTipoCarga.Value = e.CommandArgument.ToString();
            lblRazonEliminacion.Text = "Eliminar tipo de carga";
            msjEliminacion.Text = "Se eliminará el tipo de carga seleccionado, ¿desea continuar?";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
        }
    }

    public void gv_listaTipoCarga_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CargaTipoBC tipo_carga = new CargaTipoBC();
        gv_listar.SelectedIndex = e.NewEditIndex;
        hf_idTipoCarga.Value = this.gv_listar.SelectedDataKey.Value.ToString();

        tipo_carga = tipo_carga.obtenerXID(int.Parse(this.gv_listar.SelectedDataKey.Value.ToString()));
        txt_editDesc.Text = tipo_carga.DESCRIPCION;
        chk_editPreingreso.Checked = tipo_carga.PREINGRESO;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTipoCarga();", true);
    }

    public void gv_listaTipoCarga_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerTipoCarga(false);
    }

    protected void ObtenerTipoCarga(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            CargaTipoBC tipo_carga = new CargaTipoBC();
            DataTable dt = tipo_carga.obtenerTodo();
            ViewState["lista"] = dt;
            ViewState.Remove("filtrados");
        }
        DataView dw;
        if(ViewState["filtrados"] == null)
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

    public void btn_nuevoTipoCarga_Click(object sender, EventArgs e)
    {
        hf_idTipoCarga.Value = "";
        txt_editDesc.Text = "";
        chk_editPreingreso.Checked = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTipoCarga();", true);
    }

    public void btn_buscarTipoCarga_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_buscarNombre.Text))
            ObtenerTipoCarga(true);
        else
        {
            DataView dw = new DataView((DataTable)ViewState["lista"]);
            dw.RowFilter = "DESCRIPCION LIKE '%" + txt_buscarNombre.Text + "%'";
            ViewState["filtrados"] = dw.ToTable();
            ObtenerTipoCarga(false);
        }
    }

    public void btn_editGrabar_Click(object sender, EventArgs e)
    {
        CargaTipoBC tipo_carga = new CargaTipoBC();
        tipo_carga.DESCRIPCION = txt_editDesc.Text;
        tipo_carga.PREINGRESO = chk_editPreingreso.Checked;
        if (string.IsNullOrEmpty(hf_idTipoCarga.Value))
        {
            if (tipo_carga.Crear(tipo_carga))
            {
                ObtenerTipoCarga(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Tipo de carga creado exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalTipoCarga');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar tipo de carga. Intente nuevamente.');", true);
        }
        else
        {
            tipo_carga.ID = int.Parse(hf_idTipoCarga.Value);
            if (tipo_carga.Modificar(tipo_carga))
            {
                ObtenerTipoCarga(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Tipo de carga modificado exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalTipoCarga');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar tipo de carga. Intente nuevamente.');", true);
        }
    }

    public void btn_EliminarTipoCarga_Click(object sender, EventArgs e)
    {
        CargaTipoBC tipo_carga = new CargaTipoBC();
        if (tipo_carga.Eliminar(int.Parse(hf_idTipoCarga.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "ShowAlert('Tipo de carga eliminado exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar tipo de carga. Revise si tiene otros datos asociados');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmacion');", true);
        ObtenerTipoCarga(true);
    }

    #endregion

}