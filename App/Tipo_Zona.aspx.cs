using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_TipoZona : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ObtenerTipoZona(true);
        }
    }

    #region Gridview

    public void gv_listaTipoZona_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (gv_listaTipoZona.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = gv_listaTipoZona.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(gv_listaTipoZona.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = Convert.ToString(gv_listaTipoZona.PageIndex + 1);
        }
    }

    public void gv_listaTipoZona_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        DataView view = new DataView();
        if (ViewState["lista"] == null)
        {
            Tipo_ZonaBC zoti = new Tipo_ZonaBC();
            view.Table = zoti.ObtenerTodos();
        }
        else
        {
            view.Table = (DataTable)ViewState["lista"];
        }
        view.Sort = e.SortExpression + " " + direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ViewState["lista"] = view.Table;
        this.gv_listaTipoZona.DataSource = view;
        this.gv_listaTipoZona.DataBind();
    }

    public void gv_listaTipoZona_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Tipo_ZonaBC zoti = new Tipo_ZonaBC();
        gv_listaTipoZona.SelectedIndex = e.NewEditIndex;
        hf_idTipoZona.Value = this.gv_listaTipoZona.SelectedDataKey.Value.ToString();
        zoti = zoti.ObtenerTipoZonaXId(int.Parse(this.gv_listaTipoZona.SelectedDataKey.Value.ToString()));
        txt_editDesc.Text = zoti.DESCRIPCION;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTipoZona();", true);
    }

    public void gv_listaTipoZona_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listaTipoZona.PageIndex = e.NewPageIndex;
        }
        ObtenerTipoZona(false);        
    }

    public void gv_listaTipoZona_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Tipo_ZonaBC zoti = new Tipo_ZonaBC();
        if (e.CommandName == "ELIMINAR")
        {
            hf_idTipoZona.Value = e.CommandArgument.ToString();
            zoti = zoti.ObtenerTipoZonaXId(int.Parse(hf_idTipoZona.Value));
            lblRazonEliminacion.Text = "Eliminar Tipo Zona";
            msjEliminacion.Text = "Se eliminará el tipo zona seleccionado, ¿desea continuar?";
            btnModalEliminar.Attributes.Remove("onclick");
            btnModalEliminar.Attributes.Add("onclick", "eliminarTipoZona();");
            btnModalEliminar.Text = "Eliminar";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
        }
    }

    protected void ObtenerTipoZona(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            Tipo_ZonaBC zoti = new Tipo_ZonaBC();
            DataTable dt = zoti.ObtenerTodos();
            ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listaTipoZona.DataSource = dw;
        this.gv_listaTipoZona.DataBind();
    }

    protected void GoPage(object sender, System.EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        if (int.TryParse((oIraPag.Text), out iNumPag) && iNumPag > 0 && iNumPag <= gv_listaTipoZona.PageCount)
        {
            if (int.TryParse(oIraPag.Text, out iNumPag) && iNumPag > 0 && iNumPag <= gv_listaTipoZona.PageCount)
            {
                gv_listaTipoZona.PageIndex = iNumPag - 1;
            }
            else
            {
                gv_listaTipoZona.PageIndex = 0;
            }
        }
        ObtenerTipoZona(true);
    }

    #endregion

    #region Buttons

    public void btn_nuevoTipoZona_Click(object sender, EventArgs e)
    {
        hf_idTipoZona.Value = "";
        txt_editDesc.Text = "";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTipoZona();", true);
    }

    public void btn_buscarTipoZona_Click(object sender, EventArgs e)
    {
        Tipo_ZonaBC zoti = new Tipo_ZonaBC();
        DataTable dt = zoti.ObtenerTiposZonaXCriterio(txt_buscarNombre.Text);
        ViewState["lista"] = dt;
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null)
        {
            String sortExp = (String)ViewState["sortExpresion"];
            if (sortExp != "")
                dw.Sort = (String)ViewState["sortExpresion"];
        }
        this.gv_listaTipoZona.DataSource = dw;
        this.gv_listaTipoZona.DataBind();
        this.txt_buscarNombre.Focus();
    }

    public void btn_EliminarTipoZona_Click(object sender, EventArgs e)
    {
        Tipo_ZonaBC zoti = new Tipo_ZonaBC();
        if (zoti.Eliminar(int.Parse(hf_idTipoZona.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Tipo zona eliminado exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar tipo zona. Revise si tiene otros datos asociados');", true);
        ObtenerTipoZona(true);
    }

    public void btn_editGrabar_Click(object sender, EventArgs e)
    {
        Tipo_ZonaBC zoti = new Tipo_ZonaBC();
        zoti.CODIGO = txt_editDesc.Text.Substring(0,4); // DateTime.Now.ToString("dd/MM/yyyy_h:m_") + txt_editDesc.Text.Substring(0, 5).Trim();
        zoti.DESCRIPCION = txt_editDesc.Text;
        if (hf_idTipoZona.Value == "")
        {
            if (zoti.Crear(zoti))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Tipo zona creado exitosamente');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar tipo zona. Intente nuevamente.');", true);
            hf_idTipoZona.Value = "";
            txt_editDesc.Text = "";
        }
        else
        {
            zoti.ID = int.Parse(hf_idTipoZona.Value);
            if (zoti.Modificar(zoti))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Tipo zona modificado exitosamente');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar tipo zona. Intente nuevamente.');", true);
        }
        ObtenerTipoZona(true);
    }

    #endregion

}