// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Control_Carga_New : System.Web.UI.Page
{
    UsuarioBC usuario = new UsuarioBC();
    UtilsWeb utils = new UtilsWeb();
    private bool LocalesEditados()
    {
        DataView dw = ((DataTable)this.ViewState["locales"]).AsDataView();
        dw.RowFilter = "SOLD_ORDEN_OLD IS NULL";
        return dw.ToTable().Rows.Count != 0;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["usuario"] == null)
        {
            this.Response.Redirect("../InicioQYMS2.aspx");
        }
        this.usuario = (UsuarioBC)this.Session["Usuario"];
        if (!this.IsPostBack)
        {
            YMS_ZONA_BC y = new YMS_ZONA_BC();
            SolicitudBC sol = new SolicitudBC();
            this.utils.CargaDropNormal(this.ddl_buscarSite, "ID", "NOMBRE", y.ObteneSites(this.usuario.ID));
            this.ddl_buscarSite_SelectedIndexChanged(null, null);
            this.utils.CargaDropTodos(this.ddl_buscarEstado, "ID", "DESCRIPCION", sol.ObtenerEstadosCarga());
        }
    }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (this.Session["Usuario"] != null)
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)this.Session["Usuario"];

            if (usuario.numero_sites < 2)
            {
                this.SITE.Visible = false;
            }
        }
    }
    #region GridView
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerSolicitudes(false);
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
    }
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id_solicitud = Convert.ToInt32(this.gv_listar.DataKeys[e.Row.RowIndex].Values[0].ToString());
            if (e.Row.RowIndex > 0)
            {
                int anterior = Convert.ToInt32(this.gv_listar.DataKeys[e.Row.RowIndex - 1].Values[0].ToString());
                if (anterior != id_solicitud)
                {
                    if (this.gv_listar.Rows[e.Row.RowIndex - 1].CssClass == "")
                    {
                        e.Row.CssClass = "row-color2";
                    }
                }
                else
                {
                    e.Row.CssClass = this.gv_listar.Rows[e.Row.RowIndex - 1].CssClass;
                }
            }
            bool estado = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "SOLI_REASIGNACION_TRAILER"));
            if (estado)
            {
                foreach (TableCell tc in e.Row.Cells)
                {
                    if (!string.IsNullOrEmpty(tc.CssClass))
                    {
                        tc.CssClass += " ";
                    }
                    tc.CssClass += "row-color3";
                }
            }
            LinkButton btnCompletar = (LinkButton)e.Row.FindControl("btn_cargado");
            LinkButton btnParcial = (LinkButton)e.Row.FindControl("btn_cargaParcial");
            LinkButton btnContinuar = (LinkButton)e.Row.FindControl("btn_cargaContinuar");
            LinkButton btnEditar = (LinkButton)e.Row.FindControl("btn_editar");
            LinkButton btneliminar = (LinkButton)e.Row.FindControl("btn_solicitudCancelar");
            LinkButton btncolocarsello = (LinkButton)e.Row.FindControl("btn_sello");
            LinkButton btnvalidasello = (LinkButton)e.Row.FindControl("btn_valida_sello");
            LinkButton btnemergencia = (LinkButton)e.Row.FindControl("btn_AndenEmergencia");
            LinkButton btn_locales = (LinkButton)e.Row.FindControl("btn_locales");
            LinkButton btn_encendido = (LinkButton)e.Row.FindControl("btn_encendido");
            LinkButton btn_anden = (LinkButton)e.Row.FindControl("btn_anden");
            btnemergencia.Visible = false;
            btn_anden.Visible = false;
            btnCompletar.Visible = false;
            btnParcial.Visible = false;
            btnContinuar.Visible = false;
            btnEditar.Visible = false;
            btncolocarsello.Visible = false;
            btnvalidasello.Visible = false;
            btneliminar.Visible = false;
            btn_locales.Visible = false;
            btn_encendido.Visible = false;
            int lues_id = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID_ESTADOANDEN"));
            switch (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID_ESTADOSOLICITUD").ToString()))
            {
                case (int)SolicitudBC.estado_solicitud.SolicitudAndenesCreada: //Solicitud Creada
                    btneliminar.Visible = true;
                    btnEditar.Visible = true;
                    break;
                case 101: //Solicitud Creada
                    btneliminar.Visible = true;
                    btnEditar.Visible = true;
                    btn_encendido.Visible = true;
                    break;
                case 105: //casi carga
                    btn_encendido.Visible = true;
                    break;
                case 120: //reanudar carga
                    btn_encendido.Visible = true;
                    if (lues_id == 110)
                    {
                        btnCompletar.Visible = true;
                        btnParcial.Visible = true;
                    }
                    break;
                case 108: //en anden
                    btn_encendido.Visible = true;
                    if (lues_id == 108)
                    {
                        btn_anden.Visible = true;
                        btnCompletar.Visible = false;
                        btnParcial.Visible = false;
                        btnemergencia.Visible = true;
                        btneliminar.Visible = true;
                        btn_locales.Visible = true;
                    }
                    break;

                case 110: //cargando
                    btn_encendido.Visible = true;
                    if (lues_id == 110)
                    {
                        btnCompletar.Visible = true;
                        btnParcial.Visible = true;
                        btnemergencia.Visible = true;
                        btneliminar.Visible = true;
                        btn_locales.Visible = true;
                    }
                    break;
                case 125: //Suspendida
                    if (lues_id == 120) //Solicitud andén siguiente a la que interrumpió la carga
                    {
                        btnContinuar.Visible = true;
                        btn_encendido.Visible = true;
                    }
                    else if (lues_id == 100) //Solicitud andén siguiente a la que interrumpió la carga
                    {
                        btnContinuar.Visible = true;
                    }
                    break;
                case 132: //Carga Completa
                    btncolocarsello.Visible = true;
                    break;
                case 142: //sello colocado 
                    btnvalidasello.Visible = true;
                    break;
                case 150: //Solicitud Finalizada
                case 148: //Solicitud Finalizada
                default:
                    break;
            }
            btn_encendido.Visible = false;
        }
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SolicitudAndenesBC sa = new SolicitudAndenesBC();
        string resultado;
        try
        {
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            sa.SOLI_ID = Convert.ToInt32(gv_listar.SelectedDataKey.Values[0]);
            sa.LUGA_ID = Convert.ToInt32(gv_listar.SelectedDataKey.Values[1]);
            sa.SOAN_ORDEN = Convert.ToInt32(gv_listar.SelectedDataKey.Values[2]);
            sa = sa.ObtenerXId();
        }
        catch (Exception)
        {
        }
        hf_accion.Value = e.CommandName;
        LugarBC l = new LugarBC();
        switch (e.CommandName.ToString())
        {
            case "anden":
                hf_soliId.Value = sa.SOLI_ID.ToString();
                hf_soanOrden.Value = sa.SOAN_ORDEN.ToString();
                lbl_confirmarTitulo.Text = "Comenzar Carga";
                lbl_confirmarMensaje.Text = "¿Comenzar Carga?";
                btn_eliminarSolicitud.Visible = false;
                hf_soanOrden.Value = sa.SOAN_ORDEN.ToString();
                hf_lugaId.Value = sa.LUGA_ID.ToString();
                btn_andenListo.Visible = true;
                utils.AbrirModal(this, "modalConfirmar");
                break;
            case "CANCELAR":
                hf_soliId.Value = sa.SOLI_ID.ToString();
                lbl_confirmarTitulo.Text = "Eliminar Solicitud";
                lbl_confirmarMensaje.Text = "Se eliminará la solicitud de la base de datos ¿Desea continuar?";
                btn_eliminarSolicitud.Visible = true;
                btn_andenListo.Visible = false;
                utils.AbrirModal(this, "modalConfirmar");
                break;
            case "encender":
                hf_soliId.Value = sa.SOLI_ID.ToString();
                SolicitudBC solicitud = new SolicitudBC();
                solicitud.SOLI_ID = Convert.ToInt32(this.hf_soliId.Value);
                resultado="";
                bool ejecucion = solicitud.Encender_termo(this.usuario.ID, out resultado);
                if (ejecucion && resultado == "")
                    utils.ShowMessage2(this, "encender", "success");
                else
                    utils.ShowMessage(this, resultado, "error", false);
                ObtenerSolicitudes(true);
                break;
            case "AndenEmergencia":
                LimpiarLocales();
                l.ObtenerXPlaya(sa.PLAY_ID);
                btn_loc.Visible = false;
                hf_soliId.Value = sa.SOLI_ID.ToString();
                hf_soanOrden.Value = sa.SOAN_ORDEN.ToString();
                hf_caractSolicitud.Value = sa.CARACTERISTICAS;
                hf_localesSeleccionados.Value = sa.LOCALES;
                hf_timeStamp.Value = sa.TIMESTAMP.ToString();
                ObtenerLocalesSolicitud(true);
                utils.CargaDropNormal(this.ddl_reanudarAnden, "ID", "CODIGO", l.ObtenerXPlaya(sa.PLAY_ID,0,1));
                ddl_reanudarAnden.Items.Remove(ddl_reanudarAnden.Items.FindByValue(sa.LUGA_ID.ToString()));
                lbl_tituloModal.Text = "Agregar Andén de Emergencia";
                gv_reanudarLocales.Columns[0].Visible = false;
                gv_reanudarLocales.Columns[1].Visible = false;
                btn_reanudar.Visible = false;
                btn_emergencia.Visible = true;
                txt_reanudarLocal.Enabled = true;
                txt_reanudarCodLocal.Enabled = true;
                btn_agregarCarga.Enabled = true;
                ddl_reanudarAnden.Enabled = true;
                utils.AbrirModal(this, "modalReanudar");
                break;
            case "Locales":
                btn_loc.Visible = true;
                LimpiarLocales();
                hf_soliId.Value = sa.SOLI_ID.ToString();
                hf_caractSolicitud.Value = sa.CARACTERISTICAS;
                hf_localesSeleccionados.Value = sa.LOCALES;
                hf_timeStamp.Value = sa.TIMESTAMP.ToString();
                ObtenerLocalesSolicitud(true);
                utils.CargaDropNormal(this.ddl_reanudarAnden, "SOAN_COD", "SOLICITUD_ANDEN", (DataTable)this.ViewState["andenes"]);
                ddl_reanudarAnden.SelectedValue = string.Format("{0},{1},{2}", sa.SOLI_ID, sa.LUGA_ID, sa.SOAN_ORDEN);
                btn_reanudar.Visible = false;
                btn_emergencia.Visible = false;
                lbl_tituloModal.Text = "Modificar Locales";
                gv_reanudarLocales.Columns[0].Visible = true;
                gv_reanudarLocales.Columns[1].Visible = true;
                txt_reanudarLocal.Enabled = true;
                txt_reanudarCodLocal.Enabled = true;
                btn_agregarCarga.Enabled = true;
                ddl_reanudarAnden.Enabled = true;
                utils.AbrirModal(this, "modalReanudar");
                break;
            case "Parcial":
                txt_fechaCarga.Text = DateTime.Now.ToShortDateString();
                txt_horaCarga.Text = DateTime.Now.ToShortTimeString();
                hf_soliId.Value = sa.SOLI_ID.ToString();
                hf_soanOrden.Value = sa.SOAN_ORDEN.ToString();
                hf_lugaId.Value = sa.LUGA_ID.ToString();
                dv_pallets.Visible = true;
                btn_cargaParcial.Visible = true;
                btn_cargaTerminar.Visible = false;
                btn_loc.Visible = false;
                utils.AbrirModal(this, "modalCarga");
                break;
            case "Continuar":
                btn_loc.Visible = false;
                LimpiarLocales();
                hf_soliId.Value = sa.SOLI_ID.ToString();
                hf_caractSolicitud.Value = sa.CARACTERISTICAS;
                hf_localesSeleccionados.Value = sa.LOCALES;
                hf_timeStamp.Value = sa.TIMESTAMP.ToString();
                ObtenerLocalesSolicitud(true);
                utils.CargaDrop(ddl_reanudarAnden, "ID", "DESCRIPCION", l.ObtenerXPlaya(sa.PLAY_ID));
                btn_reanudar.Visible = true;
                btn_emergencia.Visible = false;
                lbl_tituloModal.Text = "Reanudar Carga";
                gv_reanudarLocales.Columns[0].Visible = true;
                gv_reanudarLocales.Columns[1].Visible = true;
                txt_reanudarLocal.Enabled = true;
                txt_reanudarCodLocal.Enabled = true;
                btn_agregarCarga.Enabled = true;
                ddl_reanudarAnden.Enabled = true;
                utils.AbrirModal(this, "modalReanudar");
                break;
            case "Cargado":
                btn_loc.Visible = false;
                dv_pallets.Visible = false;
                btn_cargaParcial.Visible = false;
                btn_cargaTerminar.Visible = true;
                txt_fechaCarga.Text = DateTime.Now.ToShortDateString();
                txt_horaCarga.Text = DateTime.Now.ToShortTimeString();
                hf_soliId.Value = sa.SOLI_ID.ToString();
                hf_soanOrden.Value = sa.SOAN_ORDEN.ToString();
                hf_lugaId.Value = sa.LUGA_ID.ToString();
                utils.AbrirModal(this, "modalCarga");
                break;
            case "Edit":
                string url = string.Format("Solicitud_Carga.aspx?id={0}&type=edit", sa.SOLI_ID);
                this.Response.Redirect(url);
                break;
            case "colocar_sello":
                this.hf_soliId.Value = sa.SOLI_ID.ToString();
                this.validar_sello();
                break;
            case "validar_sello":
                this.hf_soliId.Value = sa.SOLI_ID.ToString();
                this.validado_sello();
                break;
               
        }
    }
    private void validar_sello()
    {
        SolicitudAndenesBC anden = new SolicitudAndenesBC();
        anden.SOLI_ID = Convert.ToInt32(this.hf_soliId.Value);
        string resultado;
        bool ejecucion = anden.SelloValidar(usuario.ID, out resultado);
        if (ejecucion && resultado == "")
        {
            utils.ShowMessage2(this, "validarSello", "success");
        }
        else
        {
            utils.ShowMessage(this, resultado, "error", false);
        }
        this.ObtenerSolicitudes(true);
    }
    private void validado_sello()
    {
        SolicitudAndenesBC anden = new SolicitudAndenesBC();
        anden.SOLI_ID = Convert.ToInt32(this.hf_soliId.Value);
        string resultado;
        bool ejecucion = anden.SelloValidado(usuario.ID, out resultado);
        if (ejecucion && resultado == "")
        {
            utils.ShowMessage2(this, "validadoSello", "success");
        }
        else
        {
            utils.ShowMessage(this, resultado, "error", false);
        }
        this.ObtenerSolicitudes(true);
    }
    protected void gv_reanudarLocales_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dtLocales = (DataTable)this.ViewState["locales"];
            int soan_orden = Convert.ToInt32(dtLocales.Rows[index]["SOAN_ORDEN"]); 
            int sold_orden = Convert.ToInt32(dtLocales.Rows[index]["SOLD_ORDEN"]);
            for (int i = index; i < dtLocales.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtLocales.Rows[i]["SOAN_ORDEN"]) == soan_orden && Convert.ToInt32(dtLocales.Rows[i]["SOLD_ORDEN"]) > sold_orden)
                {
                    dtLocales.Rows[i]["SOLD_ORDEN"] = Convert.ToInt32(dtLocales.Rows[i]["SOLD_ORDEN"]) - 1;
                }
            }
            dtLocales.Rows.RemoveAt(index);
            DataView dwLocales = dtLocales.AsDataView();
            dwLocales.RowFilter = string.Format("SOAN_ORDEN = {0}", soan_orden);
            if (dwLocales.ToTable().Rows.Count == 0)
            {
                foreach (DataRow dr in dtLocales.Rows)
                {
                    if (Convert.ToInt32(dr["SOAN_ORDEN"]) > soan_orden)
                    {
                        dr["SOAN_ORDEN"] = Convert.ToInt32(dr["SOAN_ORDEN"]) - 1;
                    }
                }
                DataTable dtAndenes = (DataTable)this.ViewState["andenes"];
                dtAndenes.Rows.Remove(dtAndenes.Select(string.Format("SOAN_ORDEN = {0}", soan_orden))[0]);
                foreach (DataRow dr in dtAndenes.Rows)
                {
                    if (Convert.ToInt32(dr["SOAN_ORDEN"]) > soan_orden)
                    {
                        dr["SOAN_ORDEN"] = Convert.ToInt32(dr["SOAN_ORDEN"]) - 1;
                    }
                }
            }
            this.ViewState["locales"] = dtLocales;
            this.ObtenerLocalesSolicitud(false);
        }
        if (e.CommandName == "BAJAR")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)this.ViewState["locales"];
            int orden_a = Convert.ToInt32(dt.Rows[index]["SOLD_ORDEN"]);
            int orden_b = Convert.ToInt32(dt.Rows[index + 1]["SOLD_ORDEN"]);
            dt.Rows[index]["SOLD_ORDEN"] = orden_b;
            dt.Rows[index + 1]["SOLD_ORDEN"] = orden_a;
            this.ObtenerLocalesSolicitud(false);
        }
        if (e.CommandName == "SUBIR")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)this.ViewState["locales"];
            int orden_a = Convert.ToInt32(dt.Rows[index]["SOLD_ORDEN"]);
            int orden_b = Convert.ToInt32(dt.Rows[index - 1]["SOLD_ORDEN"]);
            dt.Rows[index]["SOLD_ORDEN"] = orden_b;
            dt.Rows[index - 1]["SOLD_ORDEN"] = orden_a;
            this.ObtenerLocalesSolicitud(false);
        }
    }
    protected void gv_reanudarLocales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (DataTable)this.ViewState["locales"];
            if (dt.Rows.Count == 1)
            {
                LinkButton btn_eliminarLocal = (LinkButton)e.Row.FindControl("btn_eliminarLocal");
                btn_eliminarLocal.Visible = false;
            }
            LinkButton btnSubir = (LinkButton)e.Row.FindControl("btnSubir");
            LinkButton btnBajar = (LinkButton)e.Row.FindControl("btnBajar");
            DataRow dr;
            try
            {
                dr = dt.Rows[e.Row.DataItemIndex + 1];
                int orden_a = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SOAN_ORDEN"));
                int orden_b = Convert.ToInt32(dr["SOAN_ORDEN"]);
                if (orden_a != orden_b)
                {
                    btnBajar.Visible = false;
                }
            }
            catch (Exception)
            {
                btnBajar.Visible = false;
            }
            try
            {
                dr = dt.Rows[e.Row.DataItemIndex - 1];
                int orden_a = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SOAN_ORDEN"));
                int orden_b = Convert.ToInt32(dr["SOAN_ORDEN"]);
                if (orden_a != orden_b)
                {
                    btnSubir.Visible = false;
                }
            }
            catch (Exception)
            {
                btnSubir.Visible = false;
            }
        }
    }
    #endregion
    #region Buttons
    protected void btn_cargaParcial_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC anden = new SolicitudAndenesBC();
        anden.SOLI_ID = Convert.ToInt32(this.hf_soliId.Value);
        anden.LUGA_ID = Convert.ToInt32(this.hf_lugaId.Value);
        anden.SOAN_ORDEN = Convert.ToInt32(this.hf_soanOrden.Value);
        anden.FECHA_CARGA_FIN = DateTime.Parse(string.Format("{0} {1}", this.txt_fechaCarga.Text, this.txt_horaCarga.Text));
        anden.PALLETS_CARGADOS = Convert.ToInt32(this.txt_palletsCargados.Text);
        string resultado1;
        bool ejecucion1 = anden.InterrumpirCarga(anden, this.usuario.ID, out resultado1);
        if (ejecucion1 && resultado1 == "")
        {
            utils.ShowMessage2(this, "cargaParcial", "success");
            utils.CerrarModal(this, "modalCarga");
        }
        else
        {
            utils.ShowMessage(this, resultado1, "error", false);
        }
        this.ObtenerSolicitudes(true);
    }
    protected void btn_cargaTerminar_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC anden = new SolicitudAndenesBC();
        anden.SOLI_ID = Convert.ToInt32(this.hf_soliId.Value);
        anden.LUGA_ID = Convert.ToInt32(this.hf_lugaId.Value);
        anden.SOAN_ORDEN = Convert.ToInt32(this.hf_soanOrden.Value);
        anden.FECHA_CARGA_FIN = DateTime.Parse(string.Format("{0} {1}", this.txt_fechaCarga.Text, this.txt_horaCarga.Text));
        string resultado;
        bool ejecucion = anden.CompletarCarga(anden, this.usuario.ID, out resultado);
        if (ejecucion && resultado == "")
        {
            utils.ShowMessage2(this, "cargaCompleta", "success");
            utils.CerrarModal(this, "modalCarga");
            this.ObtenerSolicitudes(true);
        }
        else
        {
            utils.ShowMessage(this, resultado, "error", false);
        }
        this.ObtenerSolicitudes(true);
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        this.ObtenerSolicitudes(true);
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)this.ViewState["lista"];
        if(view.Count == 0) { utils.ShowMessage2(this, "exportar", "warn_sinFilas"); return; }
        view.Table.Columns.Remove(view.Table.Columns["ID_SOLICITUDANDEN"]);
        view.Table.Columns.Remove(view.Table.Columns["USUA_ID"]);
        view.Table.Columns.Remove(view.Table.Columns["ORDEN"]);
        view.Table.Columns.Remove(view.Table.Columns["ID_ESTADOANDEN"]);
        view.Table.Columns.Remove(view.Table.Columns["ESTADOANDEN"]);
        view.Table.Columns.Remove(view.Table.Columns["ID_ESTADOSOLICITUD"]);
        view.Table.Columns.Remove(view.Table.Columns["SOLI_FH_CREACION_2"]);
        view.Table.Columns.Remove(view.Table.Columns["FECHA_RESERVA_ANDEN_2"]);
        view.Table.Columns.Remove(view.Table.Columns["FECHA_PUESTA_ANDEN_2"]);
        view.Table.Columns.Remove(view.Table.Columns["ID_TRANSPORTISTA"]);
        view.Table.Columns.Remove(view.Table.Columns["PLAY_ID"]);
        view.Table.Columns.Remove(view.Table.Columns["PLAYA"]);
        view.Table.Columns.Remove(view.Table.Columns["ID_LUGAR"]);
        GridView gv = new GridView();
        gv.DataSource = view;
        gv.DataBind();

        string fileName = "reporte_Carga.xls";
        PrepareControlForExport(gv);
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("Content-type", "application / xls");

        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        try
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();
                    table.GridLines = gv.GridLines;

                    if (gv.HeaderRow != null)
                    {
                        PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    foreach (GridViewRow row in gv.Rows)
                    {
                        PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    if (gv.FooterRow != null)
                    {
                        PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    gv.GridLines = GridLines.Both;
                    table.RenderControl(htw);
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }
        catch (HttpException ex)
        {
            throw ex;
        }
    }
    protected void btn_emergencia_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC sa = new SolicitudAndenesBC();
        SolicitudBC s = new SolicitudBC();
        s.SOLI_ID = Convert.ToInt32(this.hf_soliId.Value);
        s.TIMESTAMP = Convert.ToDateTime(this.hf_timeStamp.Value);
        if (!s.validarTimeStamp()) { utils.ShowMessage2(this, "locales", "warn_timestamp"); return; }
        DataTable dt = (DataTable)this.ViewState["locales"];
        DataTable dt2 = (DataTable)ViewState["andenes"];

        string error="";
        if (sa.Emergencia(dt,dt2, this.usuario.ID, out error) && error=="")
        {
            utils.ShowMessage2(this, "andenEmergencia", "success");
            utils.CerrarModal(this, "modalReanudar");
        }
        else
        {
            utils.ShowMessage(this, error, "error", false);
        }
        this.ObtenerSolicitudes(true);
    }
    protected void btn_locales_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC sa = new SolicitudAndenesBC();
        SolicitudBC s = new SolicitudBC();
        s.SOLI_ID = Convert.ToInt32(this.hf_soliId.Value);
        s.TIMESTAMP = Convert.ToDateTime(this.hf_timeStamp.Value);
        if (!s.validarTimeStamp()) { utils.ShowMessage2(this, "locales", "warn_timestamp"); return; }
        DataTable dt = (DataTable)this.ViewState["locales"];
        if (sa.Locales(dt))
        {
            utils.ShowMessage2(this, "locales", "success");
            utils.CerrarModal(this, "modalReanudar");
        }
        else
        {
            utils.ShowMessage2(this, "andenEmergencia", "error");
        }
        this.ObtenerSolicitudes(true);
    }
    protected void btn_agregarCarga_Click(object sender, EventArgs e)
    {
        string local = this.txt_reanudarCodLocal.Text;
        if (!ComprobarLocalExistente(local)) { utils.ShowMessage2(this, "locales", "warn_andenExiste"); return; }
        DataTable dt = (DataTable)this.ViewState["locales"];
            
        int soli_id = Convert.ToInt32(this.hf_soliId.Value);
        int luga_id = 0;
        int soan_orden = 0;
        int loca_id = 0;
        int sold_orden = 0;
        string anden = "";
        int soes_id = 100;
        switch (hf_accion.Value)
        {
            case "Locales":
                luga_id = Convert.ToInt32(this.ddl_reanudarAnden.SelectedValue.Split(",".ToCharArray())[1]);
                anden = this.ddl_reanudarAnden.SelectedItem.Text.Split("-".ToCharArray())[1];
                soan_orden = Convert.ToInt32(this.ddl_reanudarAnden.SelectedValue.Split(",".ToCharArray())[2]);
                break;
            case "AndenEmergencia":
                luga_id = Convert.ToInt32(this.ddl_reanudarAnden.SelectedValue);
                anden = this.ddl_reanudarAnden.SelectedItem.Text;
                if (ProxSoanOrden(luga_id, out soan_orden))
                {
                    DataTable dt2 = (DataTable)ViewState["andenes"];
                    dt2.Rows.Add(soli_id, soan_orden, luga_id, anden, soes_id, null, ddl_reanudarAnden.SelectedValue, ddl_reanudarAnden.SelectedItem.Text);
                    ViewState["andenes"] = dt2;
                }
                break;
            case "Continuar":
                luga_id = Convert.ToInt32(this.ddl_reanudarAnden.SelectedValue);
                anden = this.ddl_reanudarAnden.SelectedItem.Text;
                if (OrdenCorrecto(luga_id))
                {
                    if (ProxSoanOrden(luga_id, out soan_orden))
                    {
                        DataTable dt2 = (DataTable)ViewState["andenes"];
                        dt2.Rows.Add(soli_id, soan_orden, luga_id, anden, soes_id, null, ddl_reanudarAnden.SelectedValue, ddl_reanudarAnden.SelectedItem.Text);
                        ViewState["andenes"] = dt2;
                    }
                }
                else { utils.ShowMessage2(this, "locales", "warn_ordenIncorrecto"); return; }
                break;
        }
        LocalBC l = new LocalBC();
        l = l.obtenerXCodigo(this.txt_reanudarCodLocal.Text);
        loca_id = l.ID;
        sold_orden = this.ProxSoldOrden(soan_orden);
        anden = anden.Trim();
        dt.Rows.Add(soli_id, luga_id, anden, soan_orden, sold_orden, loca_id, l.CODIGO, l.DESCRIPCION, null, null);
            
        this.ViewState["locales"] = dt;
        this.ObtenerLocalesSolicitud(false);
        this.calcula_solicitud(this.hf_caractSolicitud.Value, dt);
        if (hf_accion.Value == "AndenEmergencia")
        {
            txt_reanudarLocal.Enabled = false;
            txt_reanudarCodLocal.Enabled = false;
            btn_agregarCarga.Enabled = false;
            ddl_reanudarAnden.Enabled = false;
        }
        else
        {
            this.txt_reanudarLocal.Text = "";
            this.txt_reanudarCodLocal.Text = "";
        }
    }

    protected void btn_eliminarSolicitud_Click(object sender, EventArgs e)
    {
        SolicitudBC s = new SolicitudBC();
        s.SOLI_ID = Convert.ToInt32(this.hf_soliId.Value);
        string error = "";
        if (s.Eliminar(this.usuario.ID, out error) && error == "")
        {
            utils.ShowMessage2(this, "eliminar", "success");
            utils.CerrarModal(this, "modalConfirmar");
        }
        else
        {
            utils.ShowMessage(this, error, "error", false);
        }
        this.ObtenerSolicitudes(true);
    }


    protected void btn_andenListo_Click(object sender, EventArgs e)
    {
        SolicitudBC s = new SolicitudBC();
        s.SOLI_ID = Convert.ToInt32(this.hf_soliId.Value);
        int soan_orden;
        int luga_id;
         soan_orden= Convert.ToInt32( hf_soanOrden.Value);
        luga_id=Convert.ToInt32(hf_lugaId.Value );
        string error = "";
        if (s.andenListo (this.usuario.ID, soan_orden ,luga_id,out error) && error == "")
        {
            utils.ShowMessage2(this, "andenlisto", "success");
            utils.CerrarModal(this, "modalConfirmar");
        }
        else
        {
            utils.ShowMessage(this, error, "error", false);
        }
        this.ObtenerSolicitudes(true);
    }

    protected void btn_reanudar_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC sa = new SolicitudAndenesBC();
        SolicitudBC s = new SolicitudBC();
        s.SOLI_ID = Convert.ToInt32(hf_soliId.Value);
        s.TIMESTAMP = DateTime.Parse(hf_timeStamp.Value);
        if (!s.validarTimeStamp()) { utils.ShowMessage2(this, "locales", "warn_timestamp"); return; }
        DataSet ds = new DataSet();
        ds.Tables.Add((DataTable)ViewState["andenes"]);
        ds.Tables.Add((DataTable)ViewState["locales"]);
        string resultado;
        bool ejecucion = sa.ReanudarCarga(ds, this.usuario.ID, out resultado);
        if (resultado == "" && ejecucion)
        {
            utils.ShowMessage2(this, "reanudar", "success");
            utils.CerrarModal(this, "modalReanudar");
        }
        else
        {
            utils.ShowMessage(this, resultado, "error", false);
        }
        ObtenerSolicitudes(true);
    }
    #endregion
    #region DropDownList
    protected void ddl_buscarSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        PlayaBC p = new PlayaBC();
        p.SITE_ID = Convert.ToInt32(this.ddl_buscarSite.SelectedValue);
        p.ID_TIPOPLAYA = 100;
        this.utils.CargaDropTodos(this.ddl_buscarPlaya, "ID", "DESCRIPCION", p.ObtenerTodas());
        this.ddl_buscarPlaya_SelectedIndexChanged(null, null);
        this.ObtenerSolicitudes(true);
    }
    protected void ddl_buscarPlaya_SelectedIndexChanged(object sender, EventArgs e)
    {
        LugarBC l = new LugarBC();
        int play_id = Convert.ToInt32(this.ddl_buscarPlaya.SelectedValue);
        int site_id = Convert.ToInt32(ddl_buscarSite.SelectedValue);
        this.utils.CargaDropTodos(this.ddl_buscarAnden, "ID", "DESCRIPCION", l.ObtenerTodos1(-1, 1, -1, play_id,0,site_id,100));
    }
    #endregion
    #region TextBox
    protected void txt_reanudarCodLocal_TextChanged(object sender, EventArgs e)
    {
        LocalBC l = new LocalBC();
        l = l.obtenerXCodigo(this.txt_reanudarCodLocal.Text);
        if (l.ID != 0)
        {
            this.txt_reanudarLocal.Text = string.Format("{0}({1})", l.CODIGO2, l.VALOR_CARACT_MAXIMO);
        }
        else
        {
            this.txt_reanudarCodLocal.Text = "";
            this.txt_reanudarLocal.Text = "Local no encontrado";
        }
    }
    #endregion
    #region UtilsPagina
    private void LimpiarLocales()
    {
        this.hf_soliId.Value = "0";
        this.hf_soanOrden.Value = "0";
        DataTable dtLocales = new DataTable();
        DataTable dtAndenes = new DataTable();
        dtAndenes.TableName = "ANDENES";
        dtAndenes.Columns.Add("SOLI_ID");
        dtAndenes.Columns.Add("SOAN_ORDEN");
        dtAndenes.Columns.Add("LUGA_ID");
        dtAndenes.Columns.Add("ANDEN");
        dtAndenes.Columns.Add("SOES_ID");
        dtAndenes.Columns.Add("SOAN_ORDEN_OLD");
        dtAndenes.Columns.Add("SOAN_COD");
        dtAndenes.Columns.Add("SOLICITUD_ANDEN");

        this.ViewState["andenes"] = dtAndenes;
        dtLocales.TableName = "LOCALES";
        dtLocales.Columns.Add("SOLI_ID");
        dtLocales.Columns.Add("LUGA_ID");
        dtLocales.Columns.Add("ANDEN");
        dtLocales.Columns.Add("SOAN_ORDEN");
        dtLocales.Columns.Add("SOLD_ORDEN");
        dtLocales.Columns.Add("LOCA_ID");
        dtLocales.Columns.Add("NUMERO");
        dtLocales.Columns.Add("LOCAL");
        dtLocales.Columns.Add("PALLETS");
        dtLocales.Columns.Add("SOLD_ORDEN_OLD");

        this.ViewState["locales"] = dtLocales;
        this.gv_reanudarLocales.DataSource = this.ViewState["locales"];
        this.gv_reanudarLocales.DataBind();
    }
    private void calcula_solicitud(string caracteristicas, DataTable dt)
    {
        this.hf_localesSeleccionados.Value = this.LocalesSeleccionados(dt);
        this.hf_localesCompatibles.Value = this.LocalesCompatibles(this.hf_localesSeleccionados.Value, caracteristicas);
        this.hf_maxPallets.Value = this.CantMaxPallets(this.hf_localesSeleccionados.Value, caracteristicas).ToString();
    }
    private string LocalesCompatibles(string seleccionados, string caracteristicas)
    {
        string compatibles = "";
        CaractCargaBC cc = new CaractCargaBC();
        DataTable dt = cc.obtenerCompatibles(seleccionados, caracteristicas);
        bool primero = true;
        foreach (DataRow dr in dt.Rows)
        {
            if (!primero)
            {
                compatibles += ",";
            }
            compatibles += dr[0].ToString();
            primero = false;
        }
        return compatibles;
    }
    private string LocalesSeleccionados(DataTable dt)
    {
        string locales = "";
        bool primero = true;
        foreach (DataRow dr in dt.Rows)
        {
            if (!primero)
            {
                locales += ",";
            }
            locales += dr["LOCA_ID"].ToString();
            primero = false;
        }
        return locales;
    }
    private int CantMaxPallets(string seleccionados, string caracteristicas)
    {
        CaractCargaBC cc = new CaractCargaBC();
        DataTable dt = cc.caracteristicasdesdelocales(seleccionados, caracteristicas);

        int maxplancha = 6;
        int mincantidad = 32;
        int maxpallet = 0;

        foreach (DataRow dr in dt.Rows)
        {
            int orden = dr.Field<int>("ID");
            if (dr.Field<int>("caract_ID") == 10)
            {
                maxplancha = Math.Max(maxplancha, orden);
            }
            if (dr.Field<int>("caract_ID") == 0)
            {
                mincantidad = Math.Min(mincantidad, orden);
                maxpallet = maxpallet + dr.Field<int>("valor");
            }
        }
        return maxpallet;
    }
    private void ObtenerSolicitudes(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            SolicitudBC sol = new SolicitudBC();
            int site_id = Convert.ToInt32(this.ddl_buscarSite.SelectedValue);
            int play_id = Convert.ToInt32(this.ddl_buscarPlaya.SelectedValue);
            int luga_id = Convert.ToInt32(this.ddl_buscarAnden.SelectedValue);
            int nro = 0;
            if (!string.IsNullOrEmpty(this.txt_buscarNumero.Text))
            {
                nro = Convert.ToInt32(this.txt_buscarNumero.Text);
            }
            int estado = Convert.ToInt32(this.ddl_buscarEstado.SelectedValue);
            int tran_id = Convert.ToInt32(this.ddl_buscarTransportista.SelectedValue);
            string ruta_id = this.txt_buscarRuta.Text;
            DataTable dt = sol.ObtenerSolicitudesCarga(site_id, play_id, luga_id, nro, estado, tran_id, ruta_id);
            this.ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)this.ViewState["lista"]);
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw.ToTable();
        this.gv_listar.DataBind();
    }
    private void ObtenerLocalesSolicitud(bool forzarBD)
    {
        if (this.ViewState["locales"] == null || forzarBD)
        {
            SolicitudAndenesBC sa = new SolicitudAndenesBC();
            sa.SOLI_ID = Convert.ToInt32(this.hf_soliId.Value);
            sa.SOAN_ORDEN = Convert.ToInt32(this.hf_soanOrden.Value);
            DataSet ds = sa.ObtenerTodo(sa.SOLI_ID, sa.SOAN_ORDEN);
            this.ViewState["locales"] = ds.Tables["LOCALES"];
            this.ViewState["andenes"] = ds.Tables["ANDENES"];
            this.ReordenarLocales();
        }
        DataView dw = ((DataTable)this.ViewState["locales"]).AsDataView();
        dw.Sort = "SOAN_ORDEN,SOLD_ORDEN ASC";
        this.ViewState["locales"] = dw.ToTable();
        this.gv_reanudarLocales.DataSource = this.ViewState["locales"];
        this.gv_reanudarLocales.DataBind();
        this.calcula_solicitud(this.hf_caractSolicitud.Value, (DataTable)this.ViewState["locales"]);
    }
    private void ReordenarLocales()
    {
        DataTable andenes = (DataTable)this.ViewState["andenes"];
        DataTable dtLocales = (DataTable)this.ViewState["locales"];
        foreach (DataRow a in andenes.Rows)
        {
            int i = 1;
            foreach (DataRow l in dtLocales.Rows)
            {
                if (Convert.ToInt32(l["SOAN_ORDEN"]) == Convert.ToInt32(a["SOAN_ORDEN"]))
                {
                    l["SOLD_ORDEN"] = i;
                    i++;
                }
            }
        }
        this.ViewState["locales"] = dtLocales;
    }
    private bool OrdenCorrecto(int luga_id)
    {
        int luga_id_last;
        try
        {
            DataTable dt = (DataTable)ViewState["locales"];
            luga_id_last = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["LUGA_ID"]);
            if (luga_id_last == luga_id)
            {
                return true;
            }
            else
            {
                DataView dw = ((DataTable)this.ViewState["andenes"]).AsDataView();
                dw.RowFilter = string.Format("SOES_ID = 100 AND LUGA_ID = {0}", luga_id);
                return (dw.ToTable().Rows.Count == 0);
            }
        }
        catch
        {
            return true;
        }
    }
    private bool ComprobarLocalExistente(string cod_local)
    {
        DataView dw = ((DataTable)this.ViewState["locales"]).AsDataView();
        dw.RowFilter = string.Format("SOLD_ORDEN_OLD IS NULL AND NUMERO = {0}", cod_local);
        return (dw.ToTable().Rows.Count == 0);
    }
    private bool ProxSoanOrden(int luga_id, out int soan_orden)
    {
        DataTable dt = (DataTable)this.ViewState["andenes"];
        try
        {
            DataRow ultimoAnden = dt.Rows[dt.Rows.Count - 1];
            if (Convert.ToInt32(ultimoAnden["LUGA_ID"]) == luga_id)
            {
                soan_orden = Convert.ToInt32(ultimoAnden["SOAN_ORDEN"]);
                return false;
            }
            else
            {
                soan_orden = Convert.ToInt32(ultimoAnden["SOAN_ORDEN"]) + 1;
                return true;
            }
        }
        catch (Exception)
        {
            soan_orden = 1;
            return true;
        }
    }
    private int ProxSoldOrden(int soan_orden)
    {
        DataView locales = ((DataTable)this.ViewState["locales"]).AsDataView();
        locales.RowFilter = string.Format("SOAN_ORDEN = {0}", soan_orden);
        locales.Sort = "SOLD_ORDEN ASC";
        DataTable dt = locales.ToTable();
        try
        {
            if (Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["SOAN_ORDEN"]) == soan_orden)
            {
                return Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["SOLD_ORDEN"]) + 1;
            }
            else
            {
                return 1;
            }
        }
        catch (Exception)
        {
            return 1;
        }
    }
    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
            }
            else if (current is HiddenField)
            {
                control.Controls.Remove(current);
            }
            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
        }
    }
    #endregion

    // viewstate

    protected override void SavePageStateToPersistenceMedium(object state)
    {

        string file = GenerateFileName();

        FileStream filestream = new FileStream(file, FileMode.Create);

        LosFormatter formator = new LosFormatter();

        formator.Serialize(filestream, state);

        filestream.Flush();

        filestream.Close();

        filestream = null;

    }

    protected override object LoadPageStateFromPersistenceMedium()
    {
        object state = null;
        try
        {
            

            StreamReader reader = new StreamReader(GenerateFileName());

            LosFormatter formator = new LosFormatter();

            state = formator.Deserialize(reader);

            reader.Close();


        }
        catch (Exception ex)
        {
            Response.Redirect(Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + ".aspx");
        }



        return state;
    }

    private string GenerateFileName()
    {

        string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);

        string file = pageName + Session.SessionID.ToString() + ".txt";

        //       file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);  
        file = utils.pathviewstate() + "\\" + file;

        return file;

    }  
}