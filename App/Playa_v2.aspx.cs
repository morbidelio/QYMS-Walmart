using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Text;

public partial class App_Playa_v2 : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UsuarioBC usuario = new UsuarioBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("../InicioQYMS.aspx");
        usuario = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            YMS_ZONA_BC y = new YMS_ZONA_BC();
            PlayaBC p = new PlayaBC();
            Tipo_ZonaBC tz = new Tipo_ZonaBC();
            DataTable dt = y.ObteneSites(usuario.ID);
            utils.CargaDropNormal(ddl_site, "ID", "NOMBRE", dt);
            utils.CargaDropNormal(ddl_editSite, "ID", "NOMBRE", dt);
            utils.CargaDrop(ddl_editTipo, "PYTI_ID", "TIPO_PLAYA", p.ObtenerTipos());
            utils.CargaDrop(ddl_editTipoZona, "ID", "DESCRIPCION", tz.ObtenerTodos());
            ddl_site_SelectedIndexChanged(null, null);
            ddl_editSite_SelectedIndexChanged(null, null);
            //ltl_contenidoCaract.Text = crearContenido();

        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (Session["Usuario"] != null)
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)Session["Usuario"];

            if (usuario.numero_sites < 2)
            {
                this.ddl_site.Visible = false;
            }
        }
    }

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
        ObtenerPlaya(false);
    }

    protected void gv_listar_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_listar.SelectedIndex = e.NewEditIndex;
        hf_idPlaya.Value = this.gv_listar.SelectedDataKey.Value.ToString();
        LlenarForm();
        //txt_editPosX.Text = playa.PLAYA_X.ToString();
        //txt_editPosY.Text = playa.PLAYA_Y.ToString();
        //txt_editRotacion.Text = playa.ROTACION.ToString();
        //txt_editAnchura.Text = playa.ANCHO.ToString();
        //txt_editAltura.Text = playa.ALTO.ToString();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarPlaya();", true);
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerPlaya(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        PlayaBC zona = new PlayaBC();
        if (e.CommandName == "ELIMINAR")
        {
            hf_idPlaya.Value = e.CommandArgument.ToString();
            zona = zona.ObtenerPlayaXId(int.Parse(hf_idPlaya.Value));
            lblRazonEliminacion.Text = "Eliminar Tipo Playa";
            msjEliminacion.Text = "Se eliminará el tipo zona seleccionado, ¿desea continuar?";
            btnModalEliminar.Attributes.Remove("onclick");
            btnModalEliminar.Attributes.Add("onclick", "eliminarPlaya();");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
        }
        else if (e.CommandName == "CARACT")
        {
            chk_seco.Checked = false;
            chk_frio.Checked = false;
            chk_mfrio.Checked = false;
            chk_cong.Checked = false;
            chk_ways.Checked = false;
            PlayaBC playa = new PlayaBC();
            hf_idPlaya.Value = e.CommandArgument.ToString();
            try
            {
                string caract = playa.ObtenerCaracteristicasPlaya(int.Parse(hf_idPlaya.Value));
                string[] car = caract.Split(",".ToCharArray());
                foreach (string c in car)
                {
                    switch (c)
                    {
                        case "CCS":
                            chk_seco.Checked = true;
                            break;
                        case "CCF":
                            chk_frio.Checked = true;
                            break;
                        case "CCMF":
                            chk_mfrio.Checked = true;
                            break;
                        case "CCC":
                            chk_cong.Checked = true;
                            break;
                        case "CCWAY":
                            chk_ways.Checked = true;
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
            //Label1.Text = "Caracteristicas Playa ";
            //btnModalCaract.Attributes.Add("onclick", "CaracteristicasPlaya();");
            //btnModalCaract.Text = "Grabar";

            //playa = playa.ObtenerCaracteristicasPlaya(int.Parse(hf_idPlaya.Value));
            //hf_excluyentes.Value = playa.EXCLUYENTES;
            //hf_noexcluyentes.Value = playa.NO_EXCLUYENTES;

            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "llenarForm", "llenarForm();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asddf", "ModalCaracteristicas();", true);
        }
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
        ObtenerPlaya(false);
    }

    #endregion

    #region Buttons

    protected void btn_guardarTTrai_Click(object sender, EventArgs e)
    {
        PlayaBC playa = new PlayaBC();

        string caract = GenerarCadena();

        if (!string.IsNullOrEmpty(caract))
        {
            if (playa.AgregarCaracteristica(int.Parse(hf_idPlaya.Value), caract))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Caracteristicas Agregadas Correctamente');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar caracteristicas. Intente nuevamente.');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Debe seleccionar al menos una característica.');", true);
    }

    protected void btn_nuevaPlaya_Click(object sender, EventArgs e)
    {
        LimpiarPagina();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarPlaya();", true);
    }

    protected void btn_buscarPlaya_Click(object sender, EventArgs e)
    {
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
            DataView dw = new DataView((DataTable)ViewState["lista"]);
            dw.RowFilter = filtros;
            ViewState["filtrados"] = dw.ToTable();
            ObtenerPlaya(false);
            this.txt_buscarNombre.Focus();
        }
    }

    protected void btn_EliminarPlaya_Click(object sender, EventArgs e)
    {
        PlayaBC playa = new PlayaBC();
        if (playa.Eliminar(int.Parse(hf_idPlaya.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Playa eliminada exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar playa. Revise si tiene otros datos asociados');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmacion');", true);
        ObtenerPlaya(true);
    }

    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        PlayaBC playa = new PlayaBC();
        playa.CODIGO = txt_editCodigo.Text;
        playa.DESCRIPCION = txt_editDesc.Text;
        playa.ZONA_ID = int.Parse(ddl_editZona.SelectedValue);
        playa.VIRTUAL = chk_editVirtual.Checked;
        playa.ID_TIPOPLAYA = int.Parse(ddl_editTipo.SelectedValue);
        playa.ID_TIPOZONA = int.Parse(ddl_editTipoZona.SelectedValue);
        playa.SITE_ID = int.Parse(ddl_editSite.SelectedValue);
        if (string.IsNullOrEmpty(hf_idPlaya.Value))
        {
            if (playa.Crear(playa))
            {
                ObtenerPlaya(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Playa creada exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalPlaya');", true);
            }
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
            {
                ObtenerPlaya(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Playa modificada exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalPlaya');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar playa. Intente nuevamente.');", true);
        }
    }

    #endregion

    #region DropDownList

    protected void ddl_editSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        ZonaBC z = new ZonaBC();
        utils.CargaDrop(ddl_editZona, "ID", "DESCRIPCION", z.ObtenerXSite(int.Parse(ddl_editSite.SelectedValue),true));
    }

    protected void ddl_site_SelectedIndexChanged(object sender, EventArgs e)
    {
        ZonaBC z = new ZonaBC();
        utils.CargaDrop(ddl_buscarZona, "ID", "DESCRIPCION", z.ObtenerXSite(int.Parse(ddl_site.SelectedValue),true));
        ObtenerPlaya(true);
    }

    #endregion

    #region UtilsPagina

    private void LlenarForm()
    {
        PlayaBC p = new PlayaBC();
        p = p.ObtenerPlayaXId(int.Parse(hf_idPlaya.Value));
        txt_editCodigo.Text = p.CODIGO;
        txt_editDesc.Text = p.DESCRIPCION;
        ddl_editSite.SelectedValue = p.SITE_ID.ToString();
        ddl_editSite_SelectedIndexChanged(null, null);
        try
        { 
        ddl_editZona.SelectedValue = p.ZONA_ID.ToString();
        }
        catch (Exception ex)
        { 
        
        }

        
        chk_editVirtual.Checked = p.VIRTUAL;
        ddl_editSite.Enabled = false;
        ddl_editZona.Enabled = true;
        ddl_editTipo.SelectedValue = p.ID_TIPOPLAYA.ToString();
        ddl_editTipoZona.SelectedValue = p.ID_TIPOZONA.ToString();
    }

    private void LimpiarPagina()
    {
        hf_concaract.Value = "";
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

    private void ObtenerPlaya(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            PlayaBC zona = new PlayaBC();
            DataTable dt = zona.ObtenerXSite(int.Parse(ddl_site.SelectedValue));
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
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }

    private string GenerarCadena()
    {
        string c = "";
        bool primero = true;
        if (chk_seco.Checked)
        {
            if (primero)
                primero = false;
            else
                c += ",";
            c += "CCS";
        }
        if (chk_frio.Checked)
        {
            if (primero)
                primero = false;
            else
                c += ",";
            c += "CCF";
        }
        if (chk_cong.Checked)
        {
            if (primero)
                primero = false;
            else
                c += ",";
            c += "CCC";
        }
        if (chk_mfrio.Checked)
        {
            if (primero)
                primero = false;
            else
                c += ",";
            c += "CCMF";
        }
        if (chk_ways.Checked)
        {
            if (primero)
                primero = false;
            else
                c += ",";
            c += "CCWAY";
        }
        return c;
    }

    #endregion
}