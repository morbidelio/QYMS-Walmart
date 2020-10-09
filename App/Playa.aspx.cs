using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class App_Playa : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UsuarioBC usuario = new UsuarioBC();
    CargaDrops drops = new CargaDrops();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("../InicioQYMS.aspx");
        usuario = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            YMS_ZONA_BC y = new YMS_ZONA_BC();
            SiteBC site = new SiteBC();
            DataTable dt = y.ObteneSites(usuario.ID);
            DataTable dt2 = site.ObtenerTodos();

            utils.CargaDropNormal(ddl_site, "ID", "NOMBRE", dt);
            utils.CargaDropNormal(ddl_editSite, "ID", "NOMBRE", dt2);
            ddl_site_SelectedIndexChanged(null, null);
            ddl_editSite_SelectedIndexChanged(null, null);
            //ZonaBC zona = new ZonaBC();
            //utils.CargaDrop(ddl_buscarZona, "ID", "DESCRIPCION", zona.ObtenerTodas());
            //utils.CargaDrop(ddl_editZona, "ID", "DESCRIPCION", zona.ObtenerTodas());
            //ObtenerPlaya(true);
        }
    }

    #region DropDownList

    protected void ddl_editSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        drops.Zona(ddl_editZona, int.Parse(ddl_editSite.SelectedValue));
        //ZonaBC z = new ZonaBC();
        //utils.CargaDrop(ddl_editZona, "ID", "DESCRIPCION", z.ObtenerXSite(int.Parse(ddl_editSite.SelectedValue)));
    }

    protected void ddl_site_SelectedIndexChanged(object sender, EventArgs e)
    {
        drops.Zona_Todos(ddl_buscarZona, int.Parse(ddl_editSite.SelectedValue));
        //ZonaBC z = new ZonaBC();
        //utils.CargaDrop(ddl_buscarZona, "ID", "DESCRIPCION", z.ObtenerXSite(int.Parse(ddl_site.SelectedValue)));
        ObtenerPlaya(true);
    }

    #endregion

    #region Gridview

    public void gv_listaPlaya_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (gv_listaPlaya.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = gv_listaPlaya.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(gv_listaPlaya.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = Convert.ToString(gv_listaPlaya.PageIndex + 1);
        }
    }

    public void gv_listaPlaya_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerPlaya(false);
    }

    public void gv_listaPlaya_RowEditing(object sender, GridViewEditEventArgs e)
    {
        PlayaBC playa = new PlayaBC();
        gv_listaPlaya.SelectedIndex = e.NewEditIndex;
        hf_idPlaya.Value = this.gv_listaPlaya.SelectedDataKey.Value.ToString();
        playa = playa.ObtenerPlayaXId(int.Parse(this.gv_listaPlaya.SelectedDataKey.Value.ToString()));
        txt_editCodigo.Text = playa.CODIGO;
        txt_editDesc.Text = playa.DESCRIPCION;
        ddl_editSite.SelectedValue = playa.SITE_ID.ToString();
        ddl_editSite_SelectedIndexChanged(null, null);
        ddl_editZona.SelectedValue = playa.ZONA_ID.ToString();
        chk_editVirtual.Checked = playa.VIRTUAL;
        ddl_editZona.Enabled = false;
        ddl_editSite.Enabled = false;
        ddl_editTipo.SelectedValue = playa.ID_TIPOZONA.ToString();
        //txt_editPosX.Text = playa.PLAYA_X.ToString();
        //txt_editPosY.Text = playa.PLAYA_Y.ToString();
        //txt_editRotacion.Text = playa.ROTACION.ToString();
        //txt_editAnchura.Text = playa.ANCHO.ToString();
        //txt_editAltura.Text = playa.ALTO.ToString();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarPlaya();", true);
    }

    public void gv_listaPlaya_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listaPlaya.PageIndex = e.NewPageIndex;
        }
        ObtenerPlaya(false);
    }

    public void gv_listaPlaya_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            hf_idPlaya.Value = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
        }
    }

    protected void GoPage(object sender, System.EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        if (int.TryParse((oIraPag.Text), out iNumPag) && iNumPag > 0 && iNumPag <= gv_listaPlaya.PageCount)
        {
            if (int.TryParse(oIraPag.Text, out iNumPag) && iNumPag > 0 && iNumPag <= gv_listaPlaya.PageCount)
            {
                gv_listaPlaya.PageIndex = iNumPag - 1;
            }
            else
            {
                gv_listaPlaya.PageIndex = 0;
            }
        }
        ObtenerPlaya(false);
    }

    #endregion

    #region Buttons

    public void btn_nuevaPlaya_Click(object sender, EventArgs e)
    {
        LimpiarPagina();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarPlaya();", true);
    }

    public void btn_buscarPlaya_Click(object sender, EventArgs e)
    {
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        string filtros = "";
        bool anterior = false;
        if (!string.IsNullOrEmpty(txt_buscarCodigo.Text))
        {
            filtros = "CODIGO LIKE '%" + txt_buscarCodigo.Text + "%' ";
            anterior = true;
        }
        if (!string.IsNullOrEmpty(txt_buscarNombre.Text))
        {
            if (anterior)
                filtros += "AND ";
            else
                anterior = true;
            filtros = "DESCRIPCION LIKE '%" + txt_buscarNombre.Text + "%' ";
            anterior = true;
        }
        if (ddl_buscarZona.SelectedIndex != 0)
        {
            if (anterior)
                filtros += "AND ";
            else
                anterior = true;
            filtros = "ZONA_ID = " + ddl_buscarZona.SelectedValue + " ";
            anterior = true;
        }
        if (chk_buscarVirtual.Checked)
        {
            if (anterior)
                filtros += "AND ";
            else
                anterior = true;
            filtros = "VIRTUAL = 'False'";
            anterior = true;
        }

        if (string.IsNullOrEmpty(filtros))
            ObtenerPlaya(true);
        else
        {

            dw.RowFilter = filtros;
            ViewState["filtrados"] = dw.ToTable();
            ObtenerPlaya(false);
            this.txt_buscarNombre.Focus();
        }
    }

    public void btn_EliminarPlaya_Click(object sender, EventArgs e)
    {
        PlayaBC playa = new PlayaBC();
        if (playa.Eliminar(int.Parse(hf_idPlaya.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Playa eliminada exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar zona. Revise si tiene otros datos asociados');", true);
        ObtenerPlaya(true);
    }

    public void btn_editGrabar_Click(object sender, EventArgs e)
    {
        PlayaBC playa = new PlayaBC();
        playa.CODIGO = txt_editCodigo.Text;
        playa.DESCRIPCION = txt_editDesc.Text;
        playa.ZONA_ID = int.Parse(ddl_editZona.SelectedValue);
        playa.VIRTUAL = chk_editVirtual.Checked;
        playa.ID_TIPOPLAYA = int.Parse(ddl_editTipo.SelectedValue);
        //playa.PLAYA_X = double.Parse(txt_editPosX.Text);
        //playa.PLAYA_Y = double.Parse(txt_editPosY.Text);
        //playa.ROTACION = int.Parse(txt_editRotacion.Text);
        //playa.ALTO = double.Parse(txt_editAltura.Text);
        //playa.ANCHO = double.Parse(txt_editAnchura.Text);
        if (string.IsNullOrEmpty(hf_idPlaya.Value))
        {
            if (playa.Crear(playa))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Playa creada exitosamente');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar playa. Intente nuevamente.');", true);
            //txt_editPosX.Text = "";
            //txt_editPosY.Text = "";
            //txt_editRotacion.Text = "";
            //txt_editAnchura.Text = "";
            //txt_editAltura.Text = "";
        }
        else
        {
            playa.ID = int.Parse(hf_idPlaya.Value);
            if (playa.Modificar(playa))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Playa modificada exitosamente');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar playa. Intente nuevamente.');", true);
        }
        ObtenerPlaya(true);
    }

    #endregion

    #region UtilsPagina

    private void LimpiarPagina()
    {
        hf_idPlaya.Value = "";
        txt_editCodigo.Text = "";
        txt_editDesc.Text = "";
        chk_editVirtual.Checked = false;
        ddl_editZona.ClearSelection();
        ddl_editSite.ClearSelection();
        ddl_editTipo.ClearSelection();
        ddl_editZona.Enabled = true;
        ddl_editSite.Enabled = true;
    }

    protected void ObtenerPlaya(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            PlayaBC p = new PlayaBC();
            DataTable dt = p.ObtenerXSite(int.Parse(ddl_site.SelectedValue));
            ViewState["lista"] = dt;
            ViewState.Remove("filtrados");
        }
        DataView dw;
        if (ViewState["filtrados"] == null)
            dw = new DataView((DataTable)ViewState["lista"]);
        else
            dw = new DataView((DataTable)ViewState["filtrados"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listaPlaya.DataSource = dw;
        this.gv_listaPlaya.DataBind();
    }

    #endregion
}