using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Control_Devolucion : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    UsuarioBC user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("~/InicioQYMS2.aspx");
        user = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt = new YMS_ZONA_BC().ObteneSites(user.ID);
            utils.CargaDropNormal(ddl_buscarSite, "ID", "NOMBRE", dt);
            ObtenerDevoluciones(true);
        }
    }
    protected void ddl_site_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerDevoluciones(true);
    }
    protected void btn_bloquearAgregar_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC s = new SolicitudAndenesBC();
        string resultado;
        s.LUGA_ID = Convert.ToInt32(ddl_bloquearPos.SelectedValue);
        s.SOLI_ID = Convert.ToInt32(hf_soliId.Value);

        if (s.Bloquear(user.ID, out resultado))
        {
            gv_bloqueo.DataSource = s.ObtenerBloqueados();
            gv_bloqueo.DataBind();
            utils.ShowMessage2(this, "descargaBloquear", "success_bloquear");
        }

        else
        {
            utils.ShowMessage(this, resultado, "error", false);
        }
    }
    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        DataTable table = (DataTable)ViewState["bloqueados"];
        DataView dw = table.AsDataView();
        dw.RowFilter = "POSICION1 = " + ddl_descargaPos.SelectedValue;
        if (dw.Count != 0)
        {
            utils.ShowMessage2(this, "guardar", "warn_destinoBloqueado");
            return;
        }
        try
        {
            string resultado;
            DevolucionBC d = new DevolucionBC();
            d.DEVO_ID = Convert.ToInt32(hf_devoId.Value);
            d = d.ObtenerXId();
            d.USUA_ID_ACTUALIZA = user.ID;   // Variable de sesión
            d.SOLICITUD_DESCARGA = new SolicitudBC();
            d.SOLICITUD_DESCARGA.ID_DESTINO = Convert.ToInt32(ddl_descargaPos.SelectedValue);
            d.SOLICITUD_DESCARGA.BLOQUEADOS = "";
            foreach(DataRow dr in table.Rows)
            {
                if (!string.IsNullOrEmpty(d.SOLICITUD_DESCARGA.BLOQUEADOS))
                    d.SOLICITUD_DESCARGA.BLOQUEADOS += ",";
                d.SOLICITUD_DESCARGA.BLOQUEADOS+= dr["POSICION1"].ToString();
            }

            bool ejecucion = d.Descargar(out resultado);
            if (ejecucion && resultado == "")
            {
                utils.ShowMessage2(this, "descarga", "success_creada");
                utils.CerrarModal(this, "modalDescarga");
            }
            else
                utils.ShowMessage(this, resultado, "error", false);
        }
        catch (Exception EX)
        {
            utils.ShowMessage(this, EX.Message, "error", false);
        }
        finally
        {
            ObtenerDevoluciones(true);
        }
    }
    #region GridView
    private void LimpiarBloqueados()
    {
        txt_descargaFecha.Text = "";
        txt_descargaHora.Text = "";
        txt_descargaNro.Text = "";
        txt_descargaPlaca.Text = "";
        txt_descargaTransportista.Text = "";
        chk_descargaExtranjero.Checked = false;
        lbl_lugar.Text = "";
        lbl_descargaZonaOri.Text = "";
        lbl_descargaPlayaOri.Text = "";
        ddl_descargaZona.ClearSelection();
        ddl_destinoZona_SelectedIndexChanged(null, null);
        DataTable dt = new DataTable();
        dt.Columns.Add("ZONA1");
        dt.Columns.Add("ZONA");
        dt.Columns.Add("PLAYA1");
        dt.Columns.Add("PLAYA");
        dt.Columns.Add("POSICION1");
        dt.Columns.Add("POSICION");
        ViewState["bloqueados"] = dt;
        gv_descargaBloqueados.DataSource = ViewState["bloqueados"];
        gv_descargaBloqueados.DataBind();

    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "REINTENTAR")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            new DevolucionBC().Reintentar(id);
        }
        if (e.CommandName == "EDITAR")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Response.Redirect(string.Format("Solicitud_devolucion.aspx?id={0}&type=devolucion", id));
        }
        if (e.CommandName == "DESCARGAR")
        {
            LimpiarBloqueados();
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            hf_devoId.Value = gv_listar.SelectedDataKey.Values[6].ToString();
            int id = Convert.ToInt32(hf_devoId.Value);
            DevolucionBC d = new DevolucionBC().ObtenerXId(id);
            d.SOLICITUD_DEVOLUCION = new SolicitudBC().ObtenerFinalizadaXId(d.SOLI_ID_DEVOLUCION);
            TrailerBC t = new TrailerBC().obtenerXID(d.SOLICITUD_DEVOLUCION.ID_TRAILER);
            LugarBC l = new LugarBC().obtenerXID(t.LUGAR_ID);
            DataTable dt = new ZonaBC().ObtenerXSite(d.SOLICITUD_DEVOLUCION.ID_SITE, false);
            utils.CargaDrop(ddl_descargaZona, "ID", "DESCRIPCION", dt);
            txt_descargaFecha.Text = DateTime.Now.ToShortDateString();
            txt_descargaHora.Text = DateTime.Now.ToShortTimeString();
            txt_descargaNro.Text = t.NUMERO;
            txt_descargaPlaca.Text = t.PLACA;
            txt_descargaTransportista.Text = t.TRANSPORTISTA;
            chk_descargaExtranjero.Checked = t.EXTERNO; ;
            DataRow row_lugar = l.obtenerLugarEstado(id_lugar: l.ID).Rows[0];
            pnl_detalleLugar.Style.Add("background-color", row_lugar["COLOR_LUGAR"].ToString());
            pnl_detalleLugar.Style.Add("color", "black");
            lbl_lugar.Text = row_lugar["LUGA_COD"].ToString();
            pnl_detalleTrailer.Attributes.Add("style", string.Format("background-color:{0};", row_lugar["TRTI_COLOR"].ToString()));
            int estado_sol = Convert.ToInt32(row_lugar["SOES_ID"].ToString());
            pnl_imgAlerta.BackColor = System.Drawing.ColorTranslator.FromHtml(row_lugar["COLOR"].ToString());
            if (row_lugar["tres_icono"].ToString() != "")
                img_trailer.ImageUrl = row_lugar["tres_icono"].ToString();
            lbl_descargaZonaOri.Text = l.ZONA;
            lbl_descargaPlayaOri.Text = l.PLAYA;

            utils.AbrirModal(this, "modalDescarga");
        }
        if (e.CommandName == "CARGA_COMPLETA")
        {
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            dv_pallets.Visible = false;
            btn_cargaParcial.Visible = false;
            btn_cargaTerminar.Visible = true;
            txt_fechaCarga.Text = DateTime.Now.ToShortDateString();
            txt_horaCarga.Text = DateTime.Now.ToShortTimeString();
            hf_soliId.Value = gv_listar.SelectedDataKey.Values[0].ToString();
            hf_lugaId.Value = gv_listar.SelectedDataKey.Values[1].ToString();
            hf_soanOrden.Value = gv_listar.SelectedDataKey.Values[2].ToString();
            utils.AbrirModal(this, "modalCarga");
        }
        if (e.CommandName == "CARGA_COMIENZA")
        {
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            hf_soliId.Value = gv_listar.SelectedDataKey.Values[0].ToString();
            hf_lugaId.Value = gv_listar.SelectedDataKey.Values[1].ToString();
            hf_soanOrden.Value = gv_listar.SelectedDataKey.Values[2].ToString();
            lbl_confirmarTitulo.Text = "Andén preparado";
            lbl_confirmarMensaje.Text = "¿Comenzar Carga?";
            btn_confEliminarCarga.Visible = false;
            btn_confComenzarCarga.Visible = true;
            btn_confFinalizarDevolucion.Visible = false;
            utils.AbrirModal(this, "modalConfirmar");
        }
        if (e.CommandName == "CARGA_SELLO")
        {
            SolicitudAndenesBC anden = new SolicitudAndenesBC();
            anden.SOLI_ID = Convert.ToInt32(e.CommandArgument);
            string resultado;
            bool ejecucion = anden.SelloValidar(user.ID, out resultado);
            if (ejecucion && resultado == "")
            {
                utils.ShowMessage2(this, "validarSello", "success");
            }
            else
            {
                utils.ShowMessage(this, resultado, "error", false);
            }
            ObtenerDevoluciones(true);
        }
        if (e.CommandName == "CARGA_FINALIZAR")
        {
            SolicitudAndenesBC anden = new SolicitudAndenesBC();
            anden.SOLI_ID = Convert.ToInt32(e.CommandArgument);
            string resultado;
            bool ejecucion = anden.SelloValidado(user.ID, out resultado);
            if (ejecucion && resultado == "")
            {
                utils.ShowMessage2(this, "validadoSello", "success");
            }
            else
            {
                utils.ShowMessage(this, resultado, "error", false);
            }
            ObtenerDevoluciones(true);
        }
        if (e.CommandName == "CARGA_PARCIAL")
        {
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            hf_soliId.Value = gv_listar.SelectedDataKey.Values[0].ToString();
            hf_lugaId.Value = gv_listar.SelectedDataKey.Values[1].ToString();
            hf_soanOrden.Value = gv_listar.SelectedDataKey.Values[2].ToString();
            txt_fechaCarga.Text = DateTime.Now.ToShortDateString();
            txt_horaCarga.Text = DateTime.Now.ToShortTimeString();
            dv_pallets.Visible = true;
            btn_cargaParcial.Visible = true;
            btn_cargaTerminar.Visible = false;
            utils.AbrirModal(this, "modalCarga");
        }
        if (e.CommandName == "CARGA_CONTINUAR")
        {
            LimpiarLocales();
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            SolicitudAndenesBC sa = new SolicitudAndenesBC();
            sa.SOLI_ID = Convert.ToInt32(gv_listar.SelectedDataKey.Values[0]);
            sa.SOAN_ORDEN = Convert.ToInt32(gv_listar.SelectedDataKey.Values[2]);
            sa = sa.ObtenerXId();
            hf_soliId.Value = sa.SOLI_ID.ToString();
            hf_lugaId.Value = sa.LUGA_ID.ToString();
            hf_soanOrden.Value = sa.SOAN_ORDEN.ToString();
            hf_timeStamp.Value = sa.TIMESTAMP.ToString();
            ObtenerLocalesSolicitud(true);
            utils.CargaDrop(ddl_reanudarAnden, "ID", "DESCRIPCION", new LugarBC().ObtenerXPlaya(sa.PLAY_ID));
            lbl_tituloModal.Text = "Reanudar Carga";
            gv_reanudarLocales.Columns[0].Visible = true;
            gv_reanudarLocales.Columns[1].Visible = true;
            txt_reanudarLocal.Enabled = true;
            txt_reanudarCodLocal.Enabled = true;
            btn_agregarCarga.Enabled = true;
            ddl_reanudarAnden.Enabled = true;
            btn_reanudar.Visible = true;
            btn_locales.Visible = false;
            btn_emergencia.Visible = false;
            utils.AbrirModal(this, "modalReanudar");
        }
        if (e.CommandName == "DESCARGA_COMPLETA")
        {
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            hf_soliId.Value = gv_listar.SelectedDataKey.Values[3].ToString();
            hf_lugaId.Value = gv_listar.SelectedDataKey.Values[4].ToString();
            hf_soanOrden.Value = gv_listar.SelectedDataKey.Values[5].ToString();
            string mensaje;
            SolicitudBC s = new SolicitudBC();
            bool error = s.DescargaCompleta(Convert.ToInt32(hf_soliId.Value), out mensaje, user.ID);
            if (error)
                utils.ShowMessage2(this, "descarga", "success_completa");
            else
                utils.ShowMessage(this, mensaje, "error", false);
            ObtenerDevoluciones(true);
        }
        if (e.CommandName == "DESCARGA_BLOQUEAR")
        {
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            hf_soliId.Value = gv_listar.SelectedDataKey.Values[3].ToString();
            hf_lugaId.Value = gv_listar.SelectedDataKey.Values[4].ToString();
            hf_soanOrden.Value = gv_listar.SelectedDataKey.Values[5].ToString();
            SolicitudAndenesBC sa = new SolicitudAndenesBC();
            sa.SOLI_ID = Convert.ToInt32(hf_soliId.Value);
            int play_id = sa.ObtenerPlayaId();
            DataTable dt = new LugarBC().ObtenerTodos1(ocupados: 0, lues_id: 1, play_id: play_id);
            utils.CargaDrop(ddl_bloquearPos, "ID", "DESCRIPCION", dt);
            gv_bloqueo.DataSource = sa.ObtenerBloqueados();
            gv_bloqueo.DataBind();
            utils.AbrirModal(this, "modalBloqueoAnden");
        }
        if (e.CommandName == "DEVOLUCION_COMPLETA")
        {
            gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            hf_soliId.Value = gv_listar.SelectedDataKey.Values[3].ToString();
            hf_lugaId.Value = gv_listar.SelectedDataKey.Values[4].ToString();
            hf_soanOrden.Value = gv_listar.SelectedDataKey.Values[5].ToString();
            hf_devoId.Value = gv_listar.SelectedDataKey.Values[6].ToString();
            lbl_confirmarTitulo.Text = "Finalizar Devolucion";
            lbl_confirmarMensaje.Text = "Se finalizará la Devolución ¿Desea continuar?";
            btn_confEliminarCarga.Visible = false;
            btn_confComenzarCarga.Visible = false;
            btn_confFinalizarDevolucion.Visible = true;
            utils.AbrirModal(this, "modalConfirmar");
        }
    }
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {

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
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int soli_id_carga = 0;
            int soli_id_descarga = 0;

            try
            {
                soli_id_carga = Convert.ToInt32(gv_listar.DataKeys[e.Row.RowIndex].Values[0].ToString());

            }
            catch (Exception ex)
            {
            }

            try
            {
                soli_id_descarga = Convert.ToInt32(gv_listar.DataKeys[e.Row.RowIndex].Values[3].ToString());

            }
            catch (Exception ex)
            {
            }


            LinkButton btn_editar = (LinkButton)e.Row.FindControl("btn_editar");
            LinkButton btn_descargar = (LinkButton)e.Row.FindControl("btn_descargar");
            LinkButton btn_cargaCompleta = (LinkButton)e.Row.FindControl("btn_cargaCompleta");
            LinkButton btn_cargaAnden = (LinkButton)e.Row.FindControl("btn_cargaAnden");
            LinkButton btn_cargaParcial = (LinkButton)e.Row.FindControl("btn_cargaParcial");
            LinkButton btn_cargaContinuar = (LinkButton)e.Row.FindControl("btn_cargaContinuar");
            LinkButton btn_cargaSello = (LinkButton)e.Row.FindControl("btn_cargaSello");
            LinkButton btn_cargaEstacionamiento = (LinkButton)e.Row.FindControl("btn_cargaEstacionamiento");
            LinkButton btn_devolucionCompleta = (LinkButton)e.Row.FindControl("btn_devolucionCompleta");

            LinkButton btn_descargaCompleta = (LinkButton)e.Row.FindControl("btn_descargaCompleta");
            LinkButton btn_descargaBloquear = (LinkButton)e.Row.FindControl("btn_descargaBloquear");

            btn_cargaAnden.Style.Add("visibility", "hidden");
            btn_cargaCompleta.Style.Add("visibility", "hidden");
            btn_cargaParcial.Style.Add("visibility", "hidden");
            btn_cargaContinuar.Style.Add("visibility", "hidden");
            btn_editar.Style.Add("visibility", "hidden");
            btn_descargar.Style.Add("visibility", "hidden");
            btn_cargaSello.Style.Add("visibility", "hidden");
            btn_cargaEstacionamiento.Style.Add("visibility", "hidden");
            btn_devolucionCompleta.Style.Add("visibility", "hidden");
            btn_descargaCompleta.Style.Add("visibility", "hidden");
            btn_descargaBloquear.Style.Add("visibility", "hidden");


            switch (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DEES_ID")))
            {
                case 100:
                    btn_editar.Style.Add("visibility", "visible");
                    btn_descargar.Style.Add("visibility", "visible");
                    break;
          //      case 110:

                    //if (soli_id_descarga != 0)
                    //{
                    //    string soes_id_descarga = DataBinder.Eval(e.Row.DataItem, "SOES_ID_DESCARGA").ToString();
                    //    if (soes_id_descarga =="") btn_devolucionCompleta.Style.Add("visibility", "visible");
                    //}
                    //    break;

            }

            if (soli_id_carga != 0)

            {
                int soes_id_carga = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SOES_ID_CARGA"));
                int lues_id_carga = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "LUES_ID_CARGA"));
                switch (soes_id_carga)
                {
                    //  case (int)SolicitudBC.estado_solicitud.SolicitudAndenesCreada: //Solicitud Creada
                    //      btn_editar.Style.Add("visibility", "visible");
                    //      break;
                    //   case 101: //Solicitud Creada
                    //       btn_editar.Style.Add("visibility", "visible");
                    //       break;
                    case 105: //casi carga
                        break;
                    case 120: //reanudar carga
                        if (lues_id_carga == 110)
                        {
                            btn_cargaCompleta.Style.Add("visibility", "visible");
                            btn_cargaParcial.Style.Add("visibility", "visible");
                        }
                        break;
                    case 108: //en anden
                        if (lues_id_carga == 108)
                        {
                            btn_cargaAnden.Style.Add("visibility", "visible");
                            btn_cargaCompleta.Style.Add("visibility", "hidden");
                            btn_cargaParcial.Style.Add("visibility", "hidden");
                        }
                        break;

                    case 110: //cargando
                        if (lues_id_carga == 110)
                        {
                            btn_cargaCompleta.Style.Add("visibility", "visible");
                            btn_cargaParcial.Style.Add("visibility", "visible");
                        }
                        break;
                    case 125: //Suspendida
                        if (lues_id_carga == 120) //Solicitud andén siguiente a la que interrumpió la carga
                        {
                            btn_cargaContinuar.Style.Add("visibility", "visible");
                        }
                        else if (lues_id_carga == 100) //Solicitud andén siguiente a la que interrumpió la carga
                        {
                            btn_cargaContinuar.Style.Add("visibility", "visible");
                        }
                        break;
                    case 132: //Carga Completa
                        btn_cargaSello.Style.Add("visibility", "visible");
                        break;
                    case 142: //sello colocado 
                        btn_cargaEstacionamiento.Style.Add("visibility", "visible");
                        break;
                    case 150: //Solicitud Finalizada
                    case 148: //Solicitud Finalizada
                    default:
                        break;
                }
            }
            if (soli_id_descarga != 0)
            {
                string soes_id_descarga = DataBinder.Eval(e.Row.DataItem, "SOES_ID_DESCARGA").ToString();
              //  int lues_id_descarga = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "LUES_ID_DESCARGA"));
                switch (soes_id_descarga)
                {
                    case "200": //Solicitud Creada
                        btn_descargaBloquear.Style.Add("visibility", "visible");
                        break;
                    case "205": //casi descarga
                        btn_descargaBloquear.Style.Add("visibility", "visible");
                        break;
                    case "210": //decargando
                      
                        btn_descargaBloquear.Style.Add("visibility", "visible");
                        if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DEES_ID"))==110)
                        {
                            btn_devolucionCompleta.Style.Add("visibility", "visible");
                        }
                        else
                        {
                            btn_descargaCompleta.Style.Add("visibility", "visible");
                        }
                            break;

                        break;

                    case "230": //Carga Completa
                        btn_descargaBloquear.Style.Add("visibility", "visible");
                        break;
                }
            }
        }
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
                for (int i = soan_orden; i < dtAndenes.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtAndenes.Rows[i]["SOAN_ORDEN"]) > soan_orden)
                    {
                        dtAndenes.Rows[i]["SOAN_ORDEN"] = Convert.ToInt32(dtAndenes.Rows[i]["SOAN_ORDEN"]) - 1;
                    }
                }
                //foreach (DataRow dr in dtAndenes.Rows)
                //{
                //    if (Convert.ToInt32(dr["SOAN_ORDEN"]) > soan_orden)
                //    {
                //        dr["SOAN_ORDEN"] = Convert.ToInt32(dr["SOAN_ORDEN"]) - 1;
                //    }
                //}
            }
            ViewState["locales"] = dtLocales;
            ObtenerLocalesSolicitud(false);
        }
        if (e.CommandName == "BAJAR")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)ViewState["locales"];
            int orden_a = Convert.ToInt32(dt.Rows[index]["SOLD_ORDEN"]);
            int orden_b = Convert.ToInt32(dt.Rows[index + 1]["SOLD_ORDEN"]);
            dt.Rows[index]["SOLD_ORDEN"] = orden_b;
            dt.Rows[index + 1]["SOLD_ORDEN"] = orden_a;
            ObtenerLocalesSolicitud(false);
        }
        if (e.CommandName == "SUBIR")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)ViewState["locales"];
            int orden_a = Convert.ToInt32(dt.Rows[index]["SOLD_ORDEN"]);
            int orden_b = Convert.ToInt32(dt.Rows[index - 1]["SOLD_ORDEN"]);
            dt.Rows[index]["SOLD_ORDEN"] = orden_b;
            dt.Rows[index - 1]["SOLD_ORDEN"] = orden_a;
            ObtenerLocalesSolicitud(false);
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
                btn_eliminarLocal.Style.Add("visibility", "hidden");
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
                    btnBajar.Style.Add("visibility", "hidden");
                }
            }
            catch (Exception)
            {
                btnBajar.Style.Add("visibility", "hidden");
            }
            try
            {
                dr = dt.Rows[e.Row.DataItemIndex - 1];
                int orden_a = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SOAN_ORDEN"));
                int orden_b = Convert.ToInt32(dr["SOAN_ORDEN"]);
                if (orden_a != orden_b)
                {
                    btnSubir.Style.Add("visibility", "hidden");
                }
            }
            catch (Exception)
            {
                btnSubir.Style.Add("visibility", "hidden");
            }
        }
    }
    protected void gv_bloqueo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "QUITAR")
        {
            SolicitudAndenesBC s = new SolicitudAndenesBC();
            string resultado;
            s.SOLI_ID = Convert.ToInt32(hf_soliId.Value);
            s.LUGA_ID = Convert.ToInt32(e.CommandArgument.ToString());
            if (s.Liberar(user.ID, out resultado))
                utils.ShowMessage2(this, "descargaBloquear", "success_desbloquear");
            else
                utils.ShowMessage(this, resultado, "error", false);
            gv_bloqueo.DataSource = s.ObtenerBloqueados();
            gv_bloqueo.DataBind();
        }
    }
    #endregion
    #region Buttons
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerDevoluciones(true);
    }
    protected void btn_cargaParcial_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC anden = new SolicitudAndenesBC();
        anden.SOLI_ID = Convert.ToInt32(hf_soliId.Value);
        anden.LUGA_ID = Convert.ToInt32(hf_lugaId.Value);
        anden.SOAN_ORDEN = Convert.ToInt32(hf_soanOrden.Value);
        anden.FECHA_CARGA_FIN = Convert.ToDateTime(string.Format("{0} {1}", txt_fechaCarga.Text, txt_horaCarga.Text));
        anden.PALLETS_CARGADOS = Convert.ToInt32(txt_palletsCargados.Text);
        string resultado1;
        bool ejecucion1 = anden.InterrumpirCarga(anden, user.ID, out resultado1);
        if (ejecucion1 && resultado1 == "")
        {
            utils.ShowMessage2(this, "cargaParcial", "success");
            utils.CerrarModal(this, "modalCarga");
        }
        else
        {
            utils.ShowMessage(this, resultado1, "error", false);
        }
        ObtenerDevoluciones(true);
    }
    protected void btn_cargaTerminar_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC anden = new SolicitudAndenesBC();
        anden.SOLI_ID = Convert.ToInt32(hf_soliId.Value);
        anden.LUGA_ID = Convert.ToInt32(hf_lugaId.Value);
        anden.SOAN_ORDEN = Convert.ToInt32(hf_soanOrden.Value);
        anden.FECHA_CARGA_FIN = Convert.ToDateTime(string.Format("{0} {1}", txt_fechaCarga.Text, txt_horaCarga.Text));
        string resultado;
        bool ejecucion = anden.CompletarCarga(anden, user.ID, out resultado);
        if (ejecucion && resultado == "")
        {
            utils.ShowMessage2(this, "cargaCompleta", "success");
            utils.CerrarModal(this, "modalCarga");
        }
        else
        {
            utils.ShowMessage(this, resultado, "error", false);
        }
        ObtenerDevoluciones(true);
    }
    protected void btn_confEliminarCarga_Click(object sender, EventArgs e)
    {
        SolicitudBC s = new SolicitudBC();
        s.SOLI_ID = Convert.ToInt32(hf_soliId.Value);
        string error = "";
        if (s.Eliminar(user.ID, out error) && error == "")
        {
            utils.ShowMessage2(this, "eliminar", "success");
            utils.CerrarModal(this, "modalConfirmar");
        }
        else
        {
            utils.ShowMessage(this, error, "error", false);
        }
        ObtenerDevoluciones(true);
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
    protected void btn_confComenzarCarga_Click(object sender, EventArgs e)
    {
        SolicitudBC s = new SolicitudBC();
        s.SOLI_ID = Convert.ToInt32(hf_soliId.Value);
        int soan_orden;
        int luga_id;
        soan_orden = Convert.ToInt32(hf_soanOrden.Value);
        luga_id = Convert.ToInt32(hf_lugaId.Value);
        string error = "";
        if (s.andenListo(user.ID, soan_orden, luga_id, out error) && error == "")
        {
            utils.ShowMessage2(this, "andenlisto", "success");
            utils.CerrarModal(this, "modalConfirmar");
        }
        else
        {
            utils.ShowMessage(this, error, "error", false);
        }
        ObtenerDevoluciones(true);
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
        bool ejecucion = sa.ReanudarCarga(ds, user.ID, out resultado);
        if (resultado == "" && ejecucion)
        {
            utils.ShowMessage2(this, "reanudar", "success");
            utils.CerrarModal(this, "modalReanudar");
        }
        else
        {
            utils.ShowMessage(this, resultado, "error", false);
        }
        ObtenerDevoluciones(true);
    }
    protected void btn_andenListo_Click(object sender, EventArgs e)
    {
        SolicitudBC s = new SolicitudBC();
        s.SOLI_ID = Convert.ToInt32(hf_soliId.Value);
        int soan_orden;
        int luga_id;
        soan_orden = Convert.ToInt32(hf_soanOrden.Value);
        luga_id = Convert.ToInt32(hf_lugaId.Value);
        string error = "";
        if (s.andenListo(user.ID, soan_orden, luga_id, out error) && error == "")
        {
            utils.ShowMessage2(this, "andenlisto", "success");
            utils.CerrarModal(this, "modalConfirmar");
        }
        else
        {
            utils.ShowMessage(this, error, "error", false);
        }
        ObtenerDevoluciones(true);
    }
    protected void btn_emergencia_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC sa = new SolicitudAndenesBC();
        SolicitudBC s = new SolicitudBC();
        s.SOLI_ID = Convert.ToInt32(hf_soliId.Value);
        s.TIMESTAMP = Convert.ToDateTime(hf_timeStamp.Value);
        if (!s.validarTimeStamp()) { utils.ShowMessage2(this, "locales", "warn_timestamp"); return; }
        DataTable dt = (DataTable)ViewState["locales"];
        DataTable dt2 = (DataTable)ViewState["andenes"];

        string error = "";
        if (sa.Emergencia(dt, dt2, user.ID, out error) && error == "")
        {
            utils.ShowMessage2(this, "andenEmergencia", "success");
            utils.CerrarModal(this, "modalReanudar");
        }
        else
        {
            utils.ShowMessage(this, error, "error", false);
        }
        ObtenerDevoluciones(true);
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
        ObtenerLocalesSolicitud(true);
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
        hf_soliId.Value = "0";
        hf_soanOrden.Value = "0";
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

        ViewState["andenes"] = dtAndenes;
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

        ViewState["locales"] = dtLocales;
        gv_reanudarLocales.DataSource = ViewState["locales"];
        gv_reanudarLocales.DataBind();
    }
    private void ObtenerDevoluciones(bool forzarBD)
    {
        if (ViewState["listar"] == null || forzarBD)
        {
            int site_id = Convert.ToInt32(ddl_buscarSite.SelectedValue);
            ViewState["listar"] = new DevolucionBC().ObtenerTodo(site_id);
        }
        DataView dw = new DataView((DataTable)ViewState["listar"]);
        gv_listar.DataSource = dw.ToTable();
        gv_listar.DataBind();
    }
    private void ObtenerLocalesSolicitud(bool forzarBD)
    {
        if (this.ViewState["locales"] == null || forzarBD)
        {
            SolicitudAndenesBC sa = new SolicitudAndenesBC();
            sa.SOLI_ID = Convert.ToInt32(hf_soliId.Value);
            sa.SOAN_ORDEN = Convert.ToInt32(hf_soanOrden.Value);
            DataSet ds = sa.ObtenerTodo(sa.SOLI_ID, sa.SOAN_ORDEN);
            ViewState["locales"] = ds.Tables["LOCALES"];
            ViewState["andenes"] = ds.Tables["ANDENES"];
            ReordenarLocales();
        }
        DataView dw = ((DataTable)ViewState["locales"]).AsDataView();
        dw.Sort = "SOAN_ORDEN,SOLD_ORDEN ASC";
        ViewState["locales"] = dw.ToTable();
        gv_reanudarLocales.DataSource = ViewState["locales"];
        gv_reanudarLocales.DataBind();
        calcula_solicitud(hf_caractSolicitud.Value, (DataTable)ViewState["locales"]);
    }
    private void calcula_solicitud(string caracteristicas, DataTable dt)
    {
        hf_localesSeleccionados.Value = LocalesSeleccionados(dt);
        hf_localesCompatibles.Value = LocalesCompatibles(hf_localesSeleccionados.Value, caracteristicas);
        hf_maxPallets.Value = CantMaxPallets(hf_localesSeleccionados.Value, caracteristicas).ToString();
    }
    private string LocalesCompatibles(string seleccionados, string caracteristicas)
    {
        string compatibles = "";
        DataTable dt = new CaractCargaBC().obtenerCompatibles(seleccionados, caracteristicas);
        foreach (DataRow dr in dt.Rows)
        {
            if (!string.IsNullOrEmpty(compatibles))
                compatibles += ",";
            compatibles += dr[0].ToString();
        }
        return compatibles;
    }
    private string LocalesSeleccionados(DataTable dt)
    {
        string locales = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (!string.IsNullOrEmpty(locales))
                locales += ",";
            locales += dr["LOCA_ID"].ToString();
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
    private void ReordenarLocales()
    {
        DataTable andenes = (DataTable)ViewState["andenes"];
        DataTable dtLocales = (DataTable)ViewState["locales"];
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
        ViewState["locales"] = dtLocales;
    }
    #endregion

    protected void gv_Seleccionados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            int luga_id = Convert.ToInt32(e.CommandArgument);
            DataView dw = new DataView((DataTable)ViewState["bloqueados"]);
            dw.RowFilter = string.Format("POSICION1 <> {0}",luga_id);
            ViewState["bloqueados"] = dw.ToTable();
            gv_descargaBloqueados.DataSource = ViewState["bloqueados"];
            gv_descargaBloqueados.DataBind();
        }
    }

    protected void gv_Seleccionados_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType== DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        if (e.Row.RowType== DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if (e.Row.RowType== DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }

    protected void ddl_destinoZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_descargaZona.SelectedIndex > 0)
        {
            int id_zona = Convert.ToInt32(ddl_descargaZona.SelectedValue);
            utils.CargaDrop(ddl_descargaPlaya, "ID", "DESCRIPCION", new PlayaBC().ObtenerPlayasXCriterio(null, null, id_zona, false, "100"));
            ddl_descargaPlaya.Enabled = true;
            ddl_destinoPlaya_SelectedIndexChanged(null, null);
        }
        else
        {
            ddl_descargaPos.Enabled = false;
            ddl_descargaLugarBloqueo.Enabled = false;
            ddl_descargaPlaya.Enabled = false;
            ddl_descargaPos.ClearSelection();
            ddl_descargaLugarBloqueo.ClearSelection();
            ddl_descargaPlaya.ClearSelection();
        }
    }

    protected void ddl_destinoPlaya_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_descargaPlaya.SelectedIndex > 0)
        {
            int id_playa = Convert.ToInt32(ddl_descargaPlaya.SelectedValue);
            DataTable ds1 = new YMS_ZONA_BC().Obtenerlugares_playa(id_playa, null, "0");
            utils.CargaDrop(ddl_descargaPos, "ID", "DESCRIPCION", ds1);
            utils.CargaDrop(ddl_descargaLugarBloqueo, "ID", "DESCRIPCION", ds1);
            ddl_descargaPos.Enabled = true;
            ddl_descargaLugarBloqueo.Enabled = true;
        }
        else
        {
            ddl_descargaPos.ClearSelection();
            ddl_descargaLugarBloqueo.ClearSelection();
            ddl_descargaPos.Enabled = false;
            ddl_descargaLugarBloqueo.Enabled = false;
        }
    }

    protected void btn_descargaBloquear_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)ViewState["bloqueados"];
        DataView dw = dt.AsDataView();
        dw.RowFilter = "POSICION1 = " + ddl_descargaLugarBloqueo.SelectedValue;
        if (dw.ToTable().Rows.Count != 0)
        {
            utils.ShowMessage2(this, "descargaBloquear", "warn_bloqueado");
            return;
        }
        try
        {
            DataRow row = dt.NewRow();
            row["ZONA1"] = ddl_descargaZona.SelectedValue;
            row["ZONA"] = ddl_descargaZona.SelectedItem.Text;
            row["PLAYA1"] = ddl_descargaPlaya.SelectedValue;
            row["PLAYA"] = ddl_descargaPlaya.SelectedItem.Text;
            row["POSICION1"] = ddl_descargaLugarBloqueo.SelectedValue;
            row["POSICION"] = ddl_descargaLugarBloqueo.SelectedItem.Text;
            dt.Rows.Add(row);
            ViewState["bloqueados"] = dt;
            gv_descargaBloqueados.DataSource = dt;
            gv_descargaBloqueados.DataBind();
            utils.ShowMessage2(this, "descargaBloquear", "success_bloqueado");
        }
        catch (Exception EX)
        {
            utils.ShowMessage(this, EX.Message, "error", false);
        }
    }

    protected void btn_confFinalizarDevolucion_Click(object sender, EventArgs e)
    {
        try
        {
            DevolucionBC d = new DevolucionBC();
            d.DEVO_ID = Convert.ToInt32(hf_devoId.Value);
            d.SOLI_ID_DESCARGA = Convert.ToInt32(hf_soliId.Value);
            d.USUA_ID_DESCARGA = user.ID;
            string mensaje;
            if (d.FinalizarDevolucion(out mensaje))
            {
                utils.ShowMessage2(this, "descarga", "success_finalizarDevolucion");
                utils.CerrarModal(this, "modalConfirmar");
            }
            else
                utils.ShowMessage(this, mensaje, "error", false);
        }
        catch(Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            ObtenerDevoluciones(true);
        }
    }
}