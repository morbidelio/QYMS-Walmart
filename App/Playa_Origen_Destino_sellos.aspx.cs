using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Playa_Origen_Destino_sellos : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UsuarioBC user = new UsuarioBC();
    CargaDrops drops = new CargaDrops();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("../InicioQYMS.aspx");
        user = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            YMS_ZONA_BC y = new YMS_ZONA_BC();
            //ZonaBC zona = new ZonaBC();
            drops.Zona_Todos(ddl_editZona);
            drops.Zona_Todos(ddl_buscarZona);
            //utils.CargaDropTodos(ddl_buscarZona, "ID", "DESCRIPCION", zona.ObtenerTodas());
            //utils.CargaDrop(ddl_editZona, "ID", "DESCRIPCION", zona.ObtenerTodas());
            utils.CargaDropNormal(ddl_site, "ID", "NOMBRE", y.ObteneSites(user.ID));
            ObtenerPlaya(true);
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
                this.SITE.Visible = false;
            }
        }
    }

    #region DropDownList

    protected void ddl_site_IndexChanged(object sender, EventArgs e)
    {
        ObtenerPlaya(true);
    }

    protected void ddl_destinoZona_IndexChanged(object sender, EventArgs e)
    {
        if (ddl_destinoZona.SelectedValue != "0")
        {
            ObtenerDestinos(false);
        }
        else
        {
            gv_noSeleccionados.DataSource = null;
            gv_noSeleccionados.DataBind();
        }
    }

    #endregion

    #region Gridview

    protected void gv_seleccionados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex;
            LinkButton btnsubir = (LinkButton)e.Row.FindControl("btn_subir");
            LinkButton btnbajar = (LinkButton)e.Row.FindControl("btn_bajar");
            DataTable dt = (DataTable)ViewState["seleccionados"];
            if (index == 0)
                btnsubir.Visible = false;
            if (dt.Rows.Count == (index + 1))
                btnbajar.Visible = false;
            if (dt.Rows.Count <= 1)
            {
                btnsubir.Visible = false;
                btnbajar.Visible = false;
            }
        }
    }

    protected void gv_seleccionados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SEL")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            DataTable sel = (DataTable)ViewState["seleccionados"];
            for (int i = index; i < sel.Rows.Count; i++)
            {
                int orden = int.Parse(sel.Rows[i]["ORDEN"].ToString());
                orden--;
                sel.Rows[i]["ORDEN"] = orden;
            }
            sel.Rows.RemoveAt(index);
            DataView dw = sel.AsDataView();
            dw.Sort = "ORDEN ASC";
            sel = dw.ToTable();
            ViewState["seleccionados"] = sel;
            ObtenerSeleccionados(false);
            ObtenerDestinos(false);
        }
        if (e.CommandName == "ARRIBA")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            DataTable sel = (DataTable)ViewState["seleccionados"];
            int ordenO = int.Parse(sel.Rows[index]["ORDEN"].ToString());
            sel.Rows[index]["ORDEN"] = ordenO - 1;
            sel.Rows[index - 1]["ORDEN"] = ordenO;
            DataView dw = sel.AsDataView();
            dw.Sort = "ORDEN ASC";
            sel = dw.ToTable();
            ViewState["seleccionados"] = sel;
            ObtenerSeleccionados(false);
            ObtenerDestinos(false);
        }
        if (e.CommandName == "ABAJO")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            DataTable sel = (DataTable)ViewState["seleccionados"];
            int ordenO = int.Parse(sel.Rows[index]["ORDEN"].ToString());
            sel.Rows[index]["ORDEN"] = ordenO + 1;
            sel.Rows[index + 1]["ORDEN"] = ordenO;
            DataView dw = sel.AsDataView();
            dw.Sort = "ORDEN ASC";
            sel = dw.ToTable();
            ViewState["seleccionados"] = sel;
            ObtenerSeleccionados(false);
            ObtenerDestinos(false);
        }
    }

    protected void gv_noSeleccionados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SEL")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            DataTable sel = new DataTable();
            DataTable nosel = (DataTable)ViewState["nosel"];
            PlayaBC p = new PlayaBC();
            p = p.ObtenerPlayaXId(int.Parse(hf_idPlaya.Value));
            if (ViewState["seleccionados"] == null)
            {
                sel.Columns.Add("SITE_ID");
                sel.Columns.Add("PLAY_ID_ORI");
                sel.Columns.Add("ORIGEN");
                sel.Columns.Add("PLAY_ID_DES");
                sel.Columns.Add("DESTINO");
                sel.Columns.Add("ZONA_ID");
                sel.Columns.Add("ZONA_DESTINO");
                sel.Columns.Add("ORDEN");
                sel.Columns.Add("TIIC_ID");
                ViewState["seleccionados"] = sel;
            }
            sel = (DataTable)ViewState["seleccionados"];
            string id_dest = nosel.Rows[index]["PLAY_ID"].ToString();
            string destino = nosel.Rows[index]["DESCRIPCION"].ToString();
            string zona_id_des = nosel.Rows[index]["ZONA_ID"].ToString();
            string zona_des = nosel.Rows[index]["ZONA"].ToString();
            //string tiic_id = nosel.Rows[index]["TIIC_ID"].ToString();
            int orden = sel.Rows.Count + 1;
            if (string.IsNullOrWhiteSpace(hf_seleccionados.Value))
                hf_seleccionados.Value = id_dest;
            else
                hf_seleccionados.Value += "," + id_dest;
            sel.Rows.Add(p.SITE_ID, p.ID, p.DESCRIPCION, id_dest, destino, zona_id_des, zona_des, orden, 0);
            DataView dw = sel.AsDataView();
            dw.Sort = "ORDEN ASC";
            sel = dw.ToTable();
            ViewState["seleccionados"] = sel;
            ObtenerSeleccionados(false);
            ObtenerDestinos(false);
        }
    }

    protected void gv_listaPlaya_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gv_listaPlaya_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerPlaya(false);
    }

    protected void gv_listaPlaya_RowEditing(object sender, GridViewEditEventArgs e)
    {
        PlayaBC playa = new PlayaBC();
        gv_listar.SelectedIndex = e.NewEditIndex;
        hf_idPlaya.Value = this.gv_listar.SelectedDataKey.Value.ToString();
        playa = playa.ObtenerPlayaXId(int.Parse(this.gv_listar.SelectedDataKey.Value.ToString()));
        txt_editCodigo.Text = playa.CODIGO;
        txt_editDesc.Text = playa.DESCRIPCION;
        ddl_editZona.SelectedValue = playa.ZONA_ID.ToString();
        chk_editVirtual.Checked = playa.VIRTUAL;
        ddl_editZona.Enabled = false;
        ddl_editTipo.SelectedValue = playa.ID_TIPOZONA.ToString();
        //txt_editPosX.Text = playa.PLAYA_X.ToString();
        //txt_editPosY.Text = playa.PLAYA_Y.ToString();
        //txt_editRotacion.Text = playa.ROTACION.ToString();
        //txt_editAnchura.Text = playa.ANCHO.ToString();
        //txt_editAltura.Text = playa.ALTO.ToString();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalPlaya();", true);
    }

    protected void gv_listaPlaya_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerPlaya(false);
    }

    protected void gv_listaPlaya_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            hf_idPlaya.Value = e.CommandArgument.ToString();
            lblRazonEliminacion.Text = "Eliminar Tipo Playa";
            msjEliminacion.Text = "Se eliminará el tipo zona seleccionado, ¿desea continuar?";
            btnModalEliminar.Attributes.Remove("onclick");
            btnModalEliminar.Attributes.Add("onclick", "eliminarPlaya();");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
        }
        if (e.CommandName == "DESTINO")
        {
            hf_idPlaya.Value = e.CommandArgument.ToString();
            PlayaBC p = new PlayaBC();
            p = p.ObtenerPlayaXId(int.Parse(hf_idPlaya.Value));
            hf_idSite.Value = p.SITE_ID.ToString();
            ZonaBC z = new ZonaBC();
            utils.CargaDrop(ddl_destinoZona, "ID", "DESCRIPCION", z.ObtenerXSite(p.SITE_ID,true));
            ObtenerSeleccionados(true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalDestino();", true);
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

    protected void btn_asignarGuardar_Click(object sender, EventArgs e)
    {
        PlayaOrigenDestinoSelloBC plod = new PlayaOrigenDestinoSelloBC();
        DataTable dt = (DataTable)ViewState["seleccionados"];
        if (plod.Crear(dt, int.Parse(hf_idPlaya.Value)))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Todo OK!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error!');", true);
        }
    }

    protected void btn_nuevaPlaya_Click(object sender, EventArgs e)
    {
        //hf_idPlaya.Value = "";
        //txt_editCodigo.Text = "";
        //txt_editDesc.Text = "";
        //chk_editVirtual.Checked = false;
        //ddl_editZona.ClearSelection();
        //ddl_editZona.Enabled = true;
        //ddl_editTipo.ClearSelection();
        //txt_editPosX.Text = "";
        //txt_editPosY.Text = "";
        //txt_editRotacion.Text = "";
        //txt_editAnchura.Text = "";
        //txt_editAltura.Text = "";
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalPlaya(true);", true);
    }

    protected void btn_buscarPlaya_Click(object sender, EventArgs e)
    {
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        string filtros = "";
        bool anterior = false;
        if (!string.IsNullOrEmpty(txt_buscarCodigo.Text))
        {
            filtros = "CODIGO LIKE '%" + txt_buscarCodigo.Text + "%' ";
            anterior = true;
        }
        if (!string.IsNullOrEmpty(txt_buscarDescripcion.Text))
        {
            if (anterior)
                filtros += "AND ";
            else
                anterior = true;
            filtros = "DESCRIPCION LIKE '%" + txt_buscarDescripcion.Text + "%' ";
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
        }
        this.txt_buscarDescripcion.Focus();
    }

    protected void btn_EliminarPlaya_Click(object sender, EventArgs e)
    {
        PlayaBC playa = new PlayaBC();
        if (playa.Eliminar(int.Parse(hf_idPlaya.Value)))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Playa eliminada exitosamente');", true);
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Ocurrió un error al eliminar zona. Revise si tiene otros datos asociados');", true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmacion');", true);
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
            {
                ObtenerPlaya(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Playa creada exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalPlaya');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Ocurrió un error al agregar playa. Intente nuevamente.');", true);
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

    #region UtilsPagina

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
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }

    private void ObtenerDestinos(bool forzarBD)
    {
        PlayaBC p = new PlayaBC();
        DataTable playas;

        if (ViewState["playas"] == null || forzarBD)
        {
            playas = p.ObtenerXSite(int.Parse(hf_idSite.Value));
            ViewState["playas"] = playas;
        }
        playas = (DataTable)ViewState["playas"];

        //DataView playasS = playas.AsDataView();
        //playasS.RowFilter = "ID IN (" + hf_seleccionados.Value + ")";
        //gv_seleccionados.DataSource = (DataTable)ViewState["seleccionados"];
        //gv_seleccionados.DataBind();

        DataView playasNS = playas.AsDataView();
        string filtros = "";
        filtros += "ZONA_ID = " + ddl_destinoZona.SelectedValue;
        if (!string.IsNullOrEmpty(hf_seleccionados.Value))
            filtros += " AND PLAY_ID NOT IN (" + hf_seleccionados.Value + ")";
        playasNS.RowFilter = filtros;
        ViewState["nosel"] = playasNS.ToTable();
        gv_noSeleccionados.DataSource = playasNS.ToTable();
        gv_noSeleccionados.DataBind();
    }

    private void ObtenerSeleccionados(bool forzarBD)
    {
        PlayaOrigenDestinoSelloBC plod = new PlayaOrigenDestinoSelloBC();
        DataTable dt;
        if (ViewState["seleccionados"] == null || forzarBD)
        {
            dt = plod.ObtenerXPlayId(int.Parse(hf_idPlaya.Value));
            DataView dw = dt.AsDataView();
            dw.Sort = "ORDEN ASC";
            ViewState["seleccionados"] = dw.ToTable();
        }
        dt = (DataTable)ViewState["seleccionados"];

        string cadena = "";
        bool primero = true;
        foreach (DataRow dr in dt.Rows)
        {
            if (primero)
                primero = false;
            else
                cadena += ",";
            cadena += dr["PLAY_ID_DES"].ToString();
        }
        hf_seleccionados.Value = cadena;
        gv_seleccionados.DataSource = dt;
        gv_seleccionados.DataBind();
    }
    private void LimpiarTodo()
    {
        hf_idPlaya.Value = "";
        hf_seleccionados.Value = "";
        hf_idSite.Value = "";
        txt_editCodigo.Text = "";
        txt_editDesc.Text = "";
        chk_editVirtual.Checked = false;
        ddl_editZona.ClearSelection();
        ddl_editZona.Enabled = true;
        ddl_editTipo.ClearSelection();
        gv_noSeleccionados.DataSource = null;
        gv_seleccionados.DataSource = null;
        gv_seleccionados.DataBind();
        gv_noSeleccionados.DataBind();
        txt_editDesc.Text = "";
        ViewState.Remove("seleccionados");
        ViewState.Remove("playas");
    }

    #endregion

}