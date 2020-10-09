// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Tipo_Carga2 : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    static FuncionesGenerales funcion = new FuncionesGenerales();
    UsuarioBC user = new UsuarioBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("~/InicioQYMS2.aspx");
        }
        user = (UsuarioBC)Session["Usuario"];

        if (!this.IsPostBack)
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds = yms.ObteneSites(user.ID);
            utils.CargaDrop(ddl_destinoSite, "ID", "NOMBRE", ds);
            this.ObtenerTipoCarga(true);
        }
    }

    #region DropDownList

    protected void ddl_destinoSite_IndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_destinoSite.SelectedIndex > 0)
        {
            ZonaBC z = new ZonaBC();
            this.utils.CargaDrop(this.ddl_destinoZona, "ID", "DESCRIPCION", z.ObtenerXSite(Convert.ToInt32(this.ddl_destinoSite.SelectedValue),true));
            this.ddl_destinoZona.Enabled = true;
        }
        else
        {
            this.ddl_destinoZona.ClearSelection();
            this.ddl_destinoZona.Enabled = false;
        }
        this.ddl_destinoZona_IndexChanged(null, null);
    }

    protected void ddl_destinoZona_IndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_destinoZona.SelectedIndex > 0)
        {
            this.ObtenerDestinos(false);
        }
        else
        {
            this.gv_noSeleccionados.DataSource = null;
            this.gv_noSeleccionados.DataBind();
        }
    }

    #endregion

    #region GridView

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
        this.ObtenerTipoCarga(false);
    }

    protected void gv_seleccionados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex;
            LinkButton btnsubir = (LinkButton)e.Row.FindControl("btn_subir");
            LinkButton btnbajar = (LinkButton)e.Row.FindControl("btn_bajar");
            DataTable dt = (DataTable)this.ViewState["seleccionados"];
            if (index == 0)
            {
                btnsubir.Visible = false;
            }
            if (dt.Rows.Count == (index + 1))
            {
                btnbajar.Visible = false;
            }
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
            DataTable sel = (DataTable)this.ViewState["seleccionados"];
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
            this.ViewState["seleccionados"] = sel;
            this.ObtenerSeleccionados(false);
            this.ObtenerDestinos(false);
        }
        if (e.CommandName == "ARRIBA")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            DataTable sel = (DataTable)this.ViewState["seleccionados"];
            int ordenO = int.Parse(sel.Rows[index]["ORDEN"].ToString());
            sel.Rows[index]["ORDEN"] = ordenO - 1;
            sel.Rows[index - 1]["ORDEN"] = ordenO;
            DataView dw = sel.AsDataView();
            dw.Sort = "ORDEN ASC";
            sel = dw.ToTable();
            this.ViewState["seleccionados"] = sel;
            this.ObtenerSeleccionados(false);
            this.ObtenerDestinos(false);
        }
        if (e.CommandName == "ABAJO")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            DataTable sel = (DataTable)this.ViewState["seleccionados"];
            int ordenO = int.Parse(sel.Rows[index]["ORDEN"].ToString());
            sel.Rows[index]["ORDEN"] = ordenO + 1;
            sel.Rows[index + 1]["ORDEN"] = ordenO;
            DataView dw = sel.AsDataView();
            dw.Sort = "ORDEN ASC";
            sel = dw.ToTable();
            this.ViewState["seleccionados"] = sel;
            this.ObtenerSeleccionados(false);
            this.ObtenerDestinos(false);
        }
    }

    protected void gv_noSeleccionados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SEL")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            DataTable sel = new DataTable();
            DataTable nosel = (DataTable)this.ViewState["nosel"];
            if (this.ViewState["seleccionados"] == null)
            {
                sel.Columns.Add("SITE_ID");
                sel.Columns.Add("PLAY_ID_DES");
                sel.Columns.Add("DESTINO");
                sel.Columns.Add("ZONA_ID");
                sel.Columns.Add("ZONA_DESTINO");
                sel.Columns.Add("ORDEN");
                sel.Columns.Add("TIIC_ID");
                this.ViewState["seleccionados"] = sel;
            }
            sel = (DataTable)this.ViewState["seleccionados"];
            int site_id = Convert.ToInt32(this.ddl_destinoSite.SelectedValue);
            int zona_id = Convert.ToInt32(nosel.Rows[index]["ZONA_ID"]);
            int play_id = Convert.ToInt32(nosel.Rows[index]["PLAY_ID"]);
            string zona = nosel.Rows[index]["ZONA"].ToString();
            string playa = nosel.Rows[index]["DESCRIPCION"].ToString();
            int orden = sel.Rows.Count + 1;
            if (string.IsNullOrWhiteSpace(this.hf_seleccionados.Value))
            {
                this.hf_seleccionados.Value = play_id.ToString();
            }
            else
            {
                this.hf_seleccionados.Value += string.Format(",{0}", play_id);
            }
            int tiic_id = Convert.ToInt32(this.hf_idTipoCarga.Value);
            sel.Rows.Add(site_id, play_id, playa, zona_id, zona, orden, tiic_id);
            DataView dw = sel.AsDataView();
            dw.Sort = "ORDEN ASC";
            sel = dw.ToTable();
            this.ViewState["seleccionados"] = sel;
            this.ObtenerSeleccionados(false);
            this.ObtenerDestinos(false);
        }
    }

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (this.gv_listar.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = this.gv_listar.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(this.gv_listar.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = Convert.ToString(this.gv_listar.PageIndex + 1);
        }
    }

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerTipoCarga(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            this.hf_idTipoCarga.Value = e.CommandArgument.ToString();
            this.lblRazonEliminacion.Text = "Eliminar tipo de carga";
            this.msjEliminacion.Text = "Se eliminará el tipo de carga seleccionado, ¿desea continuar?";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
        }
        if (e.CommandName == "DESTINO")
        {
            hf_idTipoCarga.Value = e.CommandArgument.ToString();
            ObtenerSeleccionados(true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalDestino();", true);
        }
    }

    protected void gv_listar_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CargaTipoBC tipo_carga = new CargaTipoBC();
        this.gv_listar.SelectedIndex = e.NewEditIndex;
        this.hf_idTipoCarga.Value = this.gv_listar.SelectedDataKey.Value.ToString();

        tipo_carga = tipo_carga.obtenerXID(int.Parse(this.gv_listar.SelectedDataKey.Value.ToString()));
        this.txt_editDesc.Text = tipo_carga.DESCRIPCION;
        this.chk_editPreingreso.Checked = tipo_carga.PREINGRESO;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalTipoCarga();", true);
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            this.gv_listar.PageIndex = e.NewPageIndex;
        }
        this.ObtenerTipoCarga(false);
    }

    #endregion

    #region Buttons

    protected void btn_asignarGuardar_Click(object sender, EventArgs e)
    {
        CargaTipoBC tc = new CargaTipoBC();
        int tiic_id = Convert.ToInt32(hf_idTipoCarga.Value);
        DataTable dt = (DataTable)ViewState["seleccionados"];
        if (tc.AsignarDestinos(dt,tiic_id))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Todo OK!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error!');", true);
        }
    }

    protected void btn_nuevoTipoCarga_Click(object sender, EventArgs e)
    {
        this.hf_idTipoCarga.Value = "";
        this.txt_editDesc.Text = "";
        this.chk_editPreingreso.Checked = false;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalTipoCarga();", true);
    }

    protected void btn_buscarTipoCarga_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.txt_buscarNombre.Text))
        {
            this.ObtenerTipoCarga(true);
        }
        else
        {
            DataView dw = new DataView((DataTable)this.ViewState["lista"]);
            dw.RowFilter = string.Format("DESCRIPCION LIKE '%{0}%'", this.txt_buscarNombre.Text);
            this.ViewState["filtrados"] = dw.ToTable();
            this.ObtenerTipoCarga(false);
        }
    }

    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        CargaTipoBC tipo_carga = new CargaTipoBC();
        tipo_carga.DESCRIPCION = this.txt_editDesc.Text;
        tipo_carga.PREINGRESO = this.chk_editPreingreso.Checked;
        if (string.IsNullOrEmpty(this.hf_idTipoCarga.Value))
        {
            if (tipo_carga.Crear(tipo_carga))
            {
                this.ObtenerTipoCarga(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Tipo de carga creado exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalTipoCarga');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al agregar tipo de carga. Intente nuevamente.');", true);
            }
        }
        else
        {
            tipo_carga.ID = int.Parse(this.hf_idTipoCarga.Value);
            if (tipo_carga.Modificar(tipo_carga))
            {
                this.ObtenerTipoCarga(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('Tipo de carga modificado exitosamente');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalTipoCarga');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al modificar tipo de carga. Intente nuevamente.');", true);
            }
        }
    }

    protected void btn_EliminarTipoCarga_Click(object sender, EventArgs e)
    {
        CargaTipoBC tipo_carga = new CargaTipoBC();
        if (tipo_carga.Eliminar(int.Parse(this.hf_idTipoCarga.Value)))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "ShowAlert('Tipo de carga eliminado exitosamente');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrió un error al eliminar tipo de carga. Revise si tiene otros datos asociados');", true);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmacion');", true);
        this.ObtenerTipoCarga(true);
    }

    #endregion

    #region UtilsPagina

    private void ObtenerTipoCarga(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            CargaTipoBC tipo_carga = new CargaTipoBC();
            DataTable dt = tipo_carga.obtenerTodo();
            this.ViewState["lista"] = dt;
            this.ViewState.Remove("filtrados");
        }
        DataView dw;
        if (this.ViewState["filtrados"] == null)
        {
            dw = new DataView((DataTable)this.ViewState["lista"]);
        }
        else
        {
            dw = new DataView((DataTable)this.ViewState["filtrados"]);
        }
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }

    private void ObtenerDestinos(bool forzarBD)
    {
        PlayaBC p = new PlayaBC();
        DataTable playas;

        if (this.ViewState["playas"] == null || forzarBD)
        {
            playas = p.ObtenerXSite(Convert.ToInt32(this.ddl_destinoSite.SelectedValue));
            this.ViewState["playas"] = playas;
        }
        playas = (DataTable)this.ViewState["playas"];

        //DataView playasS = playas.AsDataView();
        //playasS.RowFilter = "ID IN (" + hf_seleccionados.Value + ")";
        //gv_seleccionados.DataSource = (DataTable)ViewState["seleccionados"];
        //gv_seleccionados.DataBind();

        DataView playasNS = playas.AsDataView();
        string filtros = "";
        filtros += string.Format("ZONA_ID = {0}", this.ddl_destinoZona.SelectedValue);
        if (!string.IsNullOrEmpty(this.hf_seleccionados.Value))
        {
            filtros += string.Format(" AND PLAY_ID NOT IN ({0})", this.hf_seleccionados.Value);
        }
        playasNS.RowFilter = filtros;
        this.ViewState["nosel"] = playasNS.ToTable();
        this.gv_noSeleccionados.DataSource = playasNS.ToTable();
        this.gv_noSeleccionados.DataBind();
    }

    private void ObtenerSeleccionados(bool forzarBD)
    {
        CargaTipoBC tc = new CargaTipoBC();
        DataTable dt;
        if (this.ViewState["seleccionados"] == null || forzarBD)
        {
            dt = tc.CargaDestinos(Convert.ToInt32(this.hf_idTipoCarga.Value));
            DataView dw = dt.AsDataView();
            dw.Sort = "ORDEN ASC";
            this.ViewState["seleccionados"] = dw.ToTable();
        }
        dt = (DataTable)this.ViewState["seleccionados"];

        string cadena = "";
        bool primero = true;
        foreach (DataRow dr in dt.Rows)
        {
            if (primero)
            {
                primero = false;
            }
            else
            {
                cadena += ",";
            }
            cadena += dr["PLAY_ID_DES"].ToString();
        }
        this.hf_seleccionados.Value = cadena;
        this.gv_seleccionados.DataSource = dt;
        this.gv_seleccionados.DataBind();
    }
    #endregion
}