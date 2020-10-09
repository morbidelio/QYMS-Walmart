using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class App_Tipo_Solicitud : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    #region GridView

    protected void GoPage(object sender, System.EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        if (int.TryParse((oIraPag.Text), out iNumPag) && iNumPag > 0 && iNumPag <= gv_listaTipoSolicitud.PageCount)
        {
            if (int.TryParse(oIraPag.Text, out iNumPag) && iNumPag > 0 && iNumPag <= gv_listaTipoSolicitud.PageCount)
            {
                gv_listaTipoSolicitud.PageIndex = iNumPag - 1;
            }
            else
            {
                gv_listaTipoSolicitud.PageIndex = 0;
            }
        }
        ObtenerTipoSolicitud(true);
    }

    public void gv_listaTipoSolicitud_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (gv_listaTipoSolicitud.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = gv_listaTipoSolicitud.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(gv_listaTipoSolicitud.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = Convert.ToString(gv_listaTipoSolicitud.PageIndex + 1);
        }
    }

    public void gv_listaTipoSolicitud_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        DataView view = new DataView();
        if (ViewState["lista"] == null)
        {
            SolicitudTipoBC tipo_solicitud = new SolicitudTipoBC();
            view.Table = tipo_solicitud.obtenerTodoSolicitudTipo();
        }
        else
        {
            view.Table = (DataTable)ViewState["lista"];
        }
        view.Sort = e.SortExpression + " " + direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ViewState["lista"] = view.Table;
        this.gv_listaTipoSolicitud.DataSource = view;
        this.gv_listaTipoSolicitud.DataBind();
    }

    public void gv_listaTipoSolicitud_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SolicitudTipoBC tipo_solicitud = new SolicitudTipoBC();
        if (e.CommandName == "ELIMINAR")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            this.gv_listaTipoSolicitud.SelectedIndex = index;
            hf_idTipoSolicitud.Value = e.CommandArgument.ToString();
            tipo_solicitud = tipo_solicitud.obtenerXID(int.Parse(hf_idTipoSolicitud.Value));
            lblRazonEliminacion.Text = "Eliminar TipoSolicitud";
            msjEliminacion.Text = "Se eliminará el tipo_solicitud seleccionado, ¿desea continuar?";
            btnModalEliminar.Attributes.Remove("onclick");
            btnModalEliminar.Attributes.Add("onclick", "eliminarTipoSolicitud();");
            btnModalEliminar.Text = "Eliminar";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
        }
    }

    public void gv_listaTipoSolicitud_RowEditing(object sender, GridViewEditEventArgs e)
    {
        SolicitudTipoBC tipo_solicitud = new SolicitudTipoBC();
        gv_listaTipoSolicitud.SelectedIndex = e.NewEditIndex;
        hf_idTipoSolicitud.Value = this.gv_listaTipoSolicitud.SelectedDataKey.Value.ToString();

        tipo_solicitud = tipo_solicitud.obtenerXID(int.Parse(this.gv_listaTipoSolicitud.SelectedDataKey.Value.ToString()));
        txt_editDesc.Text = tipo_solicitud.DESCRIPCION;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTipoSolicitud();", true);
    }

    public void gv_listaTipoSolicitud_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listaTipoSolicitud.PageIndex = e.NewPageIndex;
        }
        ObtenerTipoSolicitud(false);
    }

    protected void ObtenerTipoSolicitud(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            SolicitudTipoBC tipo_solicitud = new SolicitudTipoBC();
            DataTable dt = tipo_solicitud.obtenerTodoSolicitudTipo();
            ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listaTipoSolicitud.DataSource = dw;
        this.gv_listaTipoSolicitud.DataBind();
    }

    #endregion

    #region Buttons

    public void btn_nuevoTipoSolicitud_Click(object sender, EventArgs e)
    {
        hf_idTipoSolicitud.Value = "";
        txt_editDesc.Text = "";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTipoSolicitud();", true);
    }

    public void btn_buscarTipoSolicitud_Click(object sender, EventArgs e)
    {
        SolicitudTipoBC tipo_solicitud = new SolicitudTipoBC();
        DataTable dt = tipo_solicitud.obtenerXParametro(txt_buscarNombre.Text);
        ViewState["lista"] = dt;
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null)
        {
            String sortExp = (String)ViewState["sortExpresion"];
            if (sortExp != "")
                dw.Sort = (String)ViewState["sortExpresion"];
        }
        this.gv_listaTipoSolicitud.DataSource = dw;
        this.gv_listaTipoSolicitud.DataBind();
        this.txt_buscarNombre.Focus();
    }

    public void btn_editGrabar_Click(object sender, EventArgs e)
    {
        SolicitudTipoBC tipo_solicitud = new SolicitudTipoBC();
        tipo_solicitud.DESCRIPCION = txt_editDesc.Text;
        if (hf_idTipoSolicitud.Value == "")
        {
            if (tipo_solicitud.Crear(tipo_solicitud))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Tipo solicitud creado exitosamente');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar tipo solicitud. Intente nuevamente.');", true);
            hf_idTipoSolicitud.Value = "";
            txt_editDesc.Text = "";
        }
        else
        {
            tipo_solicitud.ID = int.Parse(hf_idTipoSolicitud.Value);
            if (tipo_solicitud.Modificar(tipo_solicitud))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Tipo solicitud modificado exitosamente');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar tipo solicitud. Intente nuevamente.');", true);
        }
        ObtenerTipoSolicitud(true);
    }

    public void btn_EliminarTipoSolicitud_Click(object sender, EventArgs e)
    {
        SolicitudTipoBC tipo_solicitud = new SolicitudTipoBC();
        if (tipo_solicitud.Eliminar(int.Parse(hf_idTipoSolicitud.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Tipo solicitud eliminado exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar tipo solicitud. Revise si tiene otros datos asociados');", true);
        ObtenerTipoSolicitud(true);
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            ObtenerTipoSolicitud(true);
    }
}