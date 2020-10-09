using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class App_Site : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UsuarioBC u = new UsuarioBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("../InicioTMS.aspx");
        u = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            EmpresaBC empresa = new EmpresaBC();
            utils.CargaDrop(this.ddl_editEmpresa, "ID", "NOMBRE_FANTASIA", empresa.ObtenerTodas());
            utils.CargaDrop(this.ddl_buscarEmpresa, "ID", "NOMBRE_FANTASIA", empresa.ObtenerTodas());
            ObtenerSite(true);
        }
    }

    #region DropDownList

    protected void ddl_virtualZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_virtualZona.SelectedIndex > 0)
        {
            PlayaBC p = new PlayaBC();
            utils.CargaDrop(ddl_virtualPlaya, "ID", "DESCRIPCION", p.ObtenerXZona(Convert.ToInt32(ddl_virtualZona.SelectedValue)));
            ddl_virtualPlaya.Enabled = true;
        }
        else
        {
            ddl_virtualPlaya.Enabled = false;
            ddl_virtualPlaya.ClearSelection();
        }
    }

    #endregion

    #region Gridview

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerSite(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName=="EDITAR")
        {
            SiteBC site = new SiteBC();
            hf_idSite.Value = e.CommandArgument.ToString();
            site = site.ObtenerXId(Convert.ToInt32(hf_idSite.Value));
            txt_editNombre.Text = site.NOMBRE;
            txt_editDesc.Text = site.DESCRIPCION;
            ddl_editEmpresa.SelectedValue = site.EMPRESA_ID.ToString();
            txt_editCodSap.Text = site.COD_SAP.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalSite();", true);
        }
        if (e.CommandName == "ELIMINAR")
        {
            hf_idSite.Value = e.CommandArgument.ToString();
            lblRazonEliminacion.Text = "Eliminar Site";
            msjEliminacion.Text = "Se eliminará el Site seleccionado, ¿desea continuar?";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalConfirmacion();", true);
        }
        if (e.CommandName == "ACTIVAR")
        {
            SiteBC site = new SiteBC();
            if (site.TrailerAuto(u.ID, Convert.ToInt32(e.CommandArgument.ToString())))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Todo OK');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Error');", true);
            }
            ObtenerSite(true);
        }
        if (e.CommandName == "VIRTUAL")
        {
            hf_idSite.Value = e.CommandArgument.ToString();
            SiteBC site = new SiteBC();
            site.ObtenerXId(Convert.ToInt32(hf_idSite.Value));
            ZonaBC z = new ZonaBC();
            utils.CargaDrop(ddl_virtualZona, "ID", "DESCRIPCION", z.ObtenerXSite(Convert.ToInt32(hf_idSite.Value),true));
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalVirtual();", true);
            YMS_ZONA_BC p = new YMS_ZONA_BC();
            DataTable dt=   p.ObtenerPlayas_Site(Convert.ToInt32(hf_idSite.Value), "", "1");
            if (dt.Rows.Count == 1)
            {
                ddl_virtualZona.SelectedValue = dt.Rows[0]["zona_id"].ToString();
                ddl_virtualZona_SelectedIndexChanged(null, null);
                ddl_virtualPlaya.SelectedValue = dt.Rows[0]["id"].ToString();
            }
        }
    }

    protected void gv_listar_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerSite(false);
    }

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
        ObtenerSite(false);
    }

    #endregion

    #region Buttons

    protected void btn_virtualGuardar_Click(object sender, EventArgs e)
    {
        SiteBC s = new SiteBC();
        if (s.Virtual(Convert.ToInt32(hf_idSite.Value), Convert.ToInt32(ddl_virtualPlaya.SelectedValue)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Todo OK');cerrarModal('modalVirtual');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error');cerrarModal('modalVirtual');", true);
        ObtenerSite(true);
    }

    protected void btn_nuevoSite_Click(object sender, EventArgs e)
    {
        Limpiar();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalSite();", true);
    }

    protected void btn_buscarSite_Click(object sender, EventArgs e)
    {
        ObtenerSite(true);
        this.txt_buscarNombre.Focus();
    }

    protected void btn_EliminarSite_Click(object sender, EventArgs e)
    {
        SiteBC site = new SiteBC();
        if (site.Eliminar(Convert.ToInt32(hf_idSite.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Site eliminado exitosamente');cerrarModal('modalSite');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar site. Revise si tiene otros datos asociados');cerrarModal('modalSite');", true);
        ObtenerSite(true);
    }

    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        SiteBC site = new SiteBC();
        site.NOMBRE = txt_editNombre.Text;
        site.DESCRIPCION = txt_editDesc.Text;
        site.EMPRESA_ID = Convert.ToInt32(ddl_editEmpresa.SelectedValue);
        site.COD_SAP = Convert.ToInt32(txt_editCodSap.Text);
        if (string.IsNullOrEmpty(hf_idSite.Value))
        {
            if (site.Crear(site))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Site creado exitosamente');cerrarModal('modalSite');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar site. Intente nuevamente.');", true);
            ObtenerSite(true);
        }
        else
        {
            site.ID = Convert.ToInt32(hf_idSite.Value);
            if (site.Modificar(site))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Site modificado exitosamente');cerrarModal('modalSite');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar site. Intente nuevamente.');", true);
                ObtenerSite(true);
        }
    }

    #endregion

    #region FuncionesPagina

    private void Limpiar()
    {
        hf_idSite.Value = "";
        txt_editNombre.Text = "";
        txt_editDesc.Text = "";
        ddl_editEmpresa.ClearSelection();
    }

    protected void ObtenerSite(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            SiteBC site = new SiteBC();
            string nombre = txt_buscarNombre.Text;
            int empr_id = Convert.ToInt32(ddl_buscarEmpresa.SelectedValue);
            DataTable dt = site.ObtenerXCriterio(nombre, empr_id);
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