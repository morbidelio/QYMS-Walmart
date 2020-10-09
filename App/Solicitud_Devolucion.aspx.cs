﻿// Example header text. Can be configured in the options.
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Solicitud_Carga : System.Web.UI.Page
{
    UsuarioBC usuario = new UsuarioBC();
    static UtilsWeb utils = new UtilsWeb();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/InicioQYMS2.aspx");
        }
        usuario = (UsuarioBC)Session["usuario"];

        if (!IsPostBack)
        {
            SolicitudBC sol = new SolicitudBC();
            CargaDrops();
            volver.Visible = false;
            sol.ID_TIPO = 1;
            sol.FECHA_CREACION = DateTime.Now;
            txt_solHora.Text = DateTime.Now.AddMinutes(63).ToShortTimeString();

            txt_solFecha.Text = DateTime.Now.AddMinutes(63).ToShortDateString();
            ddl_solTemp.ClearSelection();
            ddl_largoMax.ClearSelection();
            LimpiarLocales();
            ddl_idShortek.Enabled = true;
            string request = Request.Params["type"];
            if (request == "devolucion")
            {
                hf_devoId.Value = Request.Params["id"].ToString();
                DevolucionBC dev = new DevolucionBC().ObtenerXId(Convert.ToInt32(hf_devoId.Value));
                hf_soliId.Value = dev.SOLI_ID_DEVOLUCION.ToString();
                hf_traiIdOld.Value = dev.TRAI_ID.ToString();
                CaractCargaBC cc = new CaractCargaBC();
                ddl_idShortek.Enabled = false;
                btn_limpiarDatos.Visible = false;

                sol = sol.ObtenerFinalizadaXId(Convert.ToInt32(hf_soliId.Value));
                dropsite.SelectedValue = sol.ID_SITE.ToString();
                drop_SelectedIndexChanged(null, null);
                hf_localesSeleccionados.Value = sol.LOCALES;
                txt_totalPallets.Text = sol.Pallets.ToString();
                txt_ruta.Text = sol.RUTA;
                ddl_idShortek.SelectedValue = sol.ID_SHORTECK;
                hf_timeStamp.Value = sol.TIMESTAMP.ToString();
                volver.Visible = true;
                volver2.Visible = false;
                string[] caract = sol.CARACTERISTICAS.Split(",".ToCharArray());
                foreach (string c in caract)
                {
                    if (c != "")
                    {
                        cc = cc.obtenerSeleccionado(Convert.ToInt32(c));
                        switch (cc.CODIGO)
                        {
                            case "CCF":
                                chk_solFrio.Checked = true;
                                break;
                            case "CCS":
                                chk_solSeco.Checked = true;
                                break;
                            case "CCC":
                                chk_solCongelado.Checked = true;
                                break;
                            case "CCMF":
                                chk_solMultifrio.Checked = true;
                                break;
                            case "CCCP":
                                chk_plancha.Checked = true;
                                break;
                            case "CCWAY":
                                chk_solWays.Checked = true;
                                break;
                        }
                    }
                }
                chk_frio_CheckedChanged(null, null);
                chk_solMultifrio.Enabled = false;
                chk_solCongelado.Enabled = false;
                chk_solSeco.Enabled = false;
                chk_solFrio.Enabled = false;
                chk_solWays.Enabled = false;
                dropsite.Enabled = false;
                ObtenerLocalesSolicitud(true);
                DataTable dsol = (DataTable)ViewState["locales"];
                carga_playas();
                ddl_solPlaya.SelectedValue = new LugarBC().obtenerXID(Convert.ToInt32(dsol.Rows[0]["LUGA_ID"])).ID_PLAYA.ToString();  // dsol.Rows[0]["id_playa"].ToString() ;
                ddl_solPlaya_SelectedIndexChanged(null, null);
                if (sol.TETR_ID != 0)
                {
                    DDL_TEMP.SelectedValue = sol.TETR_ID.ToString();
                }
                if (sol.ID_TRAILER != 0)
                {
                    TrailerBC t = new TrailerBC().obtenerXID(sol.ID_TRAILER);
                    hf_traiId.Value = t.ID.ToString();
                    txt_trailerPatente.Text = t.PLACA;
                    txt_trailerNro.Text = t.NUMERO;
                    txt_trailerShortek.Text = t.ID_SHORTEK;
                    // txt_trailerTransporte.Text = t.TRANSPORTISTA;
                    txt_trailerTransporte_1.Text = t.PLACA;
                }
                calcula_solicitud(null, null);
            }
        }
        visibleasignamovilmanual(null, null);
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
    #region Buttons
    protected void volver_click(object sender, EventArgs e)
    {
        this.Response.Redirect("./control_devolucion.aspx");
    }
    protected void volver2_click(object sender, EventArgs e)
    {
        this.Response.Redirect("./solicitud_logi.aspx");
    }
    protected void btn_limpiarDatos_Click(object sender, EventArgs e)
    {
        hf_soliId.Value = "0";
        txt_solNumero.Text = "";
        dropsite.ClearSelection();
        chk_solFrio.Checked = false;
        chk_solSeco.Checked = false;
        chk_solCongelado.Checked = false;
        chk_solMultifrio.Checked = false;
        DDL_TEMP.ClearSelection();
        ddl_solTemp.ClearSelection();
        ddl_solPlaya.ClearSelection();
        txt_totalPallets.Text = "";
        txt_buscaLocal.Text = "";
        txt_descLocal.Text = "";
        txt_destinoPallets.Text = "0";
        ddl_origenAnden.ClearSelection();
        ddl_origenAnden.Items.Clear();
        ddl_trailers.ClearSelection();
        ddl_idShortek.ClearSelection();
        txt_ruta.Text = "";
        txt_trailerShortek.Text = "";
        txt_trailerPatente.Text = "";
        txt_trailerNro.Text = "";
        txt_trailerTransporte_1.Text = "";
        hf_traiId.Value = "";
        hf_trailerAuto.Value = "false";
        hf_localesSeleccionados.Value = "";
        ddl_largoMax.ClearSelection();
        LimpiarLocales();
        txt_destinoPallets.Text = "0";
        txt_totalPallets.Text = "0";
        txt_solFecha.Text = DateTime.Now.AddMinutes(63).ToShortDateString();
        txt_solHora.Text = DateTime.Now.AddMinutes(63).ToShortTimeString();
        ViewState["trailers"] = null;
        txt_trailerShortek.Text = "";
    }
    protected void btn_agregarCarga_Click(object sender, EventArgs e)
    {
        string local = this.txt_buscaLocal.Text;
        if (this.ComprobarLocalExistente(local))
        {
            DataTable dt = (DataTable)this.ViewState["locales"];
            int soli_id = Convert.ToInt32(this.hf_soliId.Value);
            int soan_orden, loca_id, sold_orden;
            string anden;
            int soes_id = 100;
            int luga_id = Convert.ToInt32(ddl_origenAnden.SelectedValue);
            anden = ddl_origenAnden.SelectedItem.Text;
            if (ProxSoanOrden(luga_id, out soan_orden))
            {
                DataTable dt2 = (DataTable)this.ViewState["andenes"];
                dt2.Rows.Add(soli_id, soan_orden, luga_id, anden, soes_id, null, ddl_origenAnden.SelectedValue, ddl_origenAnden.SelectedItem.Text);
                ViewState["andenes"] = dt2;
            }
            LocalBC l = new LocalBC();
            l = l.obtenerXCodigo(local);

            if (txt_destinoPallets.Text == "")
            {
                txt_destinoPallets.Text = "0";
            }

            int maximo = l.VALOR_CARACT_MAXIMO;
            try
            {
                maximo = Math.Min(Convert.ToInt32(this.Session["MaxPallet"]), l.VALOR_CARACT_MAXIMO);
            }
            catch (Exception)
            {
                maximo = l.VALOR_CARACT_MAXIMO;
            }
            if (maximo == 0)
            {
                maximo = l.VALOR_CARACT_MAXIMO;
            }

            loca_id = l.ID;
            sold_orden = this.ProxSoldOrden(soan_orden);
            anden = anden.Trim();
            dt.Rows.Add(soli_id, luga_id, anden, soan_orden, sold_orden, loca_id, l.CODIGO, l.DESCRIPCION, l.VALOR_CARACT_MAXIMO, null);
            txt_descLocal.Text = "";
            txt_buscaLocal.Text = "";
            txt_destinoPallets.Text = "0";

            ViewState["locales"] = dt;
            ObtenerLocalesSolicitud(false);
            calcula_solicitud(null, null);
            ddl_playaDescarga_SelectedIndexChanged(null, null);
        }
        else
        {

            utils.ShowMessage2(this, "buscarTrailer", "warn_andenLocalVacio");
        }

    }
    protected void btn_buscarTrailer_Click(object sender, EventArgs e)
    {
        hf_traiId.Value = "";
        carga_trailers();
        DataTable dt2 = (DataTable)ViewState["locales"];
        if (dt2.Rows.Count == 0)
        {
            hf_traiId.Value = "";
            txt_trailerPatente.Text = "";
            txt_trailerNro.Text = "";
            //txt_trailerTransporte.Text = "";
            utils.ShowMessage2(this, "buscarTrailer", "warn_andenLocalVacio");
            return;
        }
        else
        {
            TrailerBC trailer = (!string.IsNullOrEmpty(txt_trailerNro.Text)) ? new TrailerBC().obtenerXNro(txt_trailerNro.Text) : new TrailerBC().obtenerXPlaca(txt_trailerPatente.Text);
            int old_id = Convert.ToInt32(hf_traiIdOld.Value);
            if (old_id == trailer.ID)
            {
                ddl_playaDescarga.Enabled = false;
                ddl_playaDescarga.ClearSelection();
                ddl_playaDescarga_SelectedIndexChanged(null, null);
                hf_traiId.Value = trailer.ID.ToString();
                txt_trailerPatente.Text = trailer.PLACA;
                txt_trailerNro.Text = trailer.NUMERO;
                txt_trailerShortek.Text = trailer.ID_SHORTEK;
               // txt_trailerTransporte.Text = trailer.TRANSPORTISTA.ToString();
                utils.ShowMessage2(this, "buscarTrailer", "success");
            }
            else if (ComprobarTrailer(trailer))
            {
                ddl_playaDescarga.Enabled = true;
                ddl_playaDescarga_SelectedIndexChanged(null, null);
                hf_traiId.Value = trailer.ID.ToString();
                txt_trailerPatente.Text = trailer.PLACA;
                txt_trailerNro.Text = trailer.NUMERO;
                txt_trailerShortek.Text = trailer.ID_SHORTEK;
              //  txt_trailerTransporte.Text = trailer.TRANSPORTISTA.ToString();
                utils.ShowMessage2(this, "buscarTrailer", "success");
            }
            else
            {
                ddl_playaDescarga.Enabled = false;
                ddl_playaDescarga.ClearSelection();
                ddl_playaDescarga_SelectedIndexChanged(null, null);
                hf_traiId.Value = "";
                txt_trailerPatente.Text = "";
                txt_trailerNro.Text = "";
              //  txt_trailerTransporte.Text = "";
                utils.ShowMessage2(this, "buscarTrailer", "warn_andenLocalVacio");
                return;
            }
        }
    }
    protected void btn_confirmarMov_Click(object sender, EventArgs e)
    {
        DateTime fh = Convert.ToDateTime(string.Format("{0} {1}", txt_solFecha.Text, txt_solHora.Text));
        if (DateTime.Now.AddMinutes(57) > fh) { utils.ShowMessage2(this, "editar", "warn_horaInvalida"); return; }
        DataTable dtAndenes = (DataTable)ViewState["andenes"];
        DataTable dtLocales = (DataTable)ViewState["locales"];
        int registros;
        try
        {
            registros = dtLocales.Rows.Count;
        }
        catch (NullReferenceException)
        {
            registros = 0;
        }
        if (registros == 0)
        {
            utils.ShowMessage2(this, "nuevo", "warn_andenVacio");
            return;
        }
        DevolucionBC d = new DevolucionBC();
        SolicitudBC s = new SolicitudBC();
        d.DEVO_ID = Convert.ToInt32(hf_devoId.Value);
        d.TRAI_ID = Convert.ToInt32(hf_traiIdOld.Value);
        s.ID_SITE = Convert.ToInt32(dropsite.SelectedValue);
        s.ID_USUARIO = usuario.ID; //Variable sesión
        s.FECHA_PLAN_ANDEN = Convert.ToDateTime(string.Format("{0} {1}", txt_solFecha.Text, txt_solHora.Text));
        s.RUTA = txt_ruta.Text;
        s.ID_SHORTECK = ddl_idShortek.SelectedValue;
        s.ID_TRAILER_RESERVADO = Convert.ToInt32(hf_traiId.Value);
        s.TETR_ID = Convert.ToInt32(DDL_TEMP.SelectedValue);
        s.Pallets = Convert.ToInt32(txt_totalPallets.Text);
        s.CARACTERISTICAS = hf_caractSolicitud.Value;
        DataView dw = new DataView((DataTable)ViewState["trailers"]);
        string mensaje;
        DataSet ds = new DataSet();
        dtAndenes.TableName = "ANDENES";
        dtLocales.TableName = "LOCALES";
        ds.Tables.Add(dtAndenes);
        ds.Tables.Add(dtLocales);
        if (hf_traiId.Value == hf_traiIdOld.Value)
        {

          if (d.Despachar(s, ds, d.DEVO_ID, out mensaje) && mensaje == "")
            {
                utils.ShowMessage2(this, "editar", "success_despacho");
                btn_limpiarDatos_Click(null, null);
                Response.Redirect("./control_devolucion.aspx");
            }
            else
            {
                utils.ShowMessage(this, mensaje, "error", false);
            }
        }
        else
        {

            DataView dw2 = new DataView((DataTable)ViewState["locales"]);
            dw2.RowFilter = "SOAN_ORDEN > 1";

            int cantidad_soan_2 = dw2.ToTable().Rows.Count;

            if (cantidad_soan_2 > 0)
            {
                int luga_id_descarga = Convert.ToInt32(ddl_andenDescarga.SelectedValue);
                if (d.Trasvasije(s, d, luga_id_descarga, ds, out mensaje) && mensaje == "")
                {
                    utils.ShowMessage2(this, "editar", "success_trasvasije");
                    btn_limpiarDatos_Click(null, null);
                }
                else
                {
                    utils.ShowMessage(this, mensaje, "error", false);
                }
            }
            else
            {
                utils.ShowMessage(this, "Agregue al menos un local para Trasvasije", "error", false);

            }



        }
    }
    protected void btn_anularSol_Click(object sender, EventArgs e)
    {
        SolicitudBC solicitud = new SolicitudBC();
        string error;
        if (solicitud.Eliminar(Convert.ToInt32(this.hf_traiId.Value), out error) && error == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Solicitud anulada!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", error), true);
        }
    }
    #endregion
    #region DropDownList
    protected void DDL_TEMP_IndexChanged(object sender, EventArgs e)
    {
        this.stringCaractSolicitud();
    }
    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.calcula_solicitud(null, null);
    }
    protected void ddl_solPlaya_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_solPlaya.SelectedIndex > 0)
        {
            int play_id = Convert.ToInt32(ddl_solPlaya.SelectedValue);
            int trai_id = Convert.ToInt32(hf_traiIdOld.Value);
            DataView dw = new DataView(new LugarBC().ObtenerXPlaya(play_id));
            dw.RowFilter = string.Format("OCUPADO = 0 OR TRAI_ID = {0}", trai_id);
            utils.CargaDrop(ddl_origenAnden, "ID", "DESCRIPCION", dw.ToTable());
            ddl_origenAnden.Enabled = true;
        }
        else
        {
            ddl_origenAnden.ClearSelection();
            ddl_origenAnden.Enabled = false;
        }
    }
    protected void ddl_playaDescarga_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_playaDescarga.SelectedIndex > 0)
        {
            int play_id = Convert.ToInt32(ddl_playaDescarga.SelectedValue);
            int trai_id = Convert.ToInt32(hf_traiIdOld.Value);
            int soli_id = Convert.ToInt32(hf_soliId.Value);
            string filtro = string.Format("(OCUPADO = 0 OR TRAI_ID = {0})", trai_id);
            DataView dw;
            dw = new DataView((DataTable)ViewState["andenes"]);
            dw.RowFilter = "SOAN_ORDEN = 2";
            if (dw.Count > 0)
            {
                filtro += " AND ID <> " + dw.ToTable().Rows[0]["LUGA_ID"].ToString();
            }
            dw = new DataView(new LugarBC().ObtenerXPlaya(play_id));
            dw.RowFilter = filtro;
            utils.CargaDrop(ddl_andenDescarga, "ID", "DESCRIPCION", dw.ToTable());
            ddl_andenDescarga.Enabled = true;
        }
        else
        {
            ddl_andenDescarga.ClearSelection();
            ddl_andenDescarga.Enabled = false;
        }
    }
    protected void ddl_trailers_SelectChanged(object sender, EventArgs e)
    {
        txt_trailerPatente.Text = this.ddl_trailers.SelectedItem.Text;
        txt_trailerNro.Text = "";

        btn_buscarTrailer_Click(null, null);
    }
    protected void ddl_idShortek_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.carga_trailers();
    }
    #endregion
    #region CheckBox
    protected void chk_trailer_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk_trailer.Checked)
        {
            this.pnl_trailer.Enabled = true;
            this.hf_trailerAuto.Value = "false";
        }
        else
        {
            this.pnl_trailer.Enabled = false;
            this.hf_trailerAuto.Value = "true";
        }
    }
    protected void chk_frio_CheckedChanged(object sender, EventArgs e)
    {
        calcula_solicitud(null, null);
        carga_playas();
        ddl_solPlaya.Enabled = (chk_solFrio.Checked || chk_solSeco.Checked || chk_solMultifrio.Checked || chk_solCongelado.Checked || chk_solWays.Checked);
    }
    #endregion
    #region TextBox
    protected void txt_buscaLocal_TextChanged(object sender, EventArgs e)
    {
        LocalBC l = new LocalBC();
        l = l.obtenerXCodigo(this.txt_buscaLocal.Text);
        if (l.ID != 0)
        {
            this.txt_descLocal.Text = string.Format("{0}({1})", l.CODIGO2, l.VALOR_CARACT_MAXIMO);
            this.ddl_origenAnden.Enabled = true;

        }
        else
        {
            this.txt_buscaLocal.Text = "";
            this.txt_descLocal.Text = "Local no encontrado";
            this.ddl_origenAnden.Enabled = false;
        }
    }
    #endregion
    #region GridView
    protected void gv_solLocales_rowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataView dw = new DataView((DataTable)ViewState["locales"] );
            dw.RowFilter = "SOAN_ORDEN = 1";
            int cantidad_soan_1=dw.ToTable().Rows.Count;

            int index = e.Row.RowIndex;
            DataTable dtLocales = (DataTable)this.ViewState["locales"];
            LinkButton btn_eliminarLocal = (LinkButton)e.Row.FindControl("btn_eliminarLocal");
            LinkButton btnSubir = (LinkButton)e.Row.FindControl("btnSubir");
            LinkButton btnBajar = (LinkButton)e.Row.FindControl("btnBajar");
            if (dtLocales.Rows.Count == 1 )
            {
                btn_eliminarLocal.Style.Add("visibility", "hidden");
            }
            int soan_orden = Convert.ToInt32(gv_solLocales.DataKeys[index].Values[0]);
            DataRow dr = ((DataTable)ViewState["andenes"]).Select(string.Format("SOAN_ORDEN = {0}", soan_orden))[0];
            int soes_id = Convert.ToInt32(dr["SOES_ID"]);
            if (soan_orden == 1 )
            {
                if ( (cantidad_soan_1 == 1)) btn_eliminarLocal.Style.Add("visibility", "hidden");
                btnSubir.Style.Add("visibility", "hidden");
                btnBajar.Style.Add("visibility", "hidden");
            }
            else
            {
                try
                {
                    int soan_orden_new = Convert.ToInt32(gv_solLocales.DataKeys[index + 1].Values[0]);
                    if (soan_orden != soan_orden_new)
                        btnBajar.Style.Add("visibility", "hidden");
                }
                catch (Exception)
                {
                    btnBajar.Style.Add("visibility", "hidden");
                }
                try
                {
                    int soan_orden_new = Convert.ToInt32(gv_solLocales.DataKeys[index - 1].Values[0]);
                    if (soan_orden != soan_orden_new)
                        btnSubir.Style.Add("visibility", "hidden");
                }
                catch (Exception)
                {
                    btnSubir.Style.Add("visibility", "hidden");
                }
            }
        }
    }
    protected void gv_solLocales_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    try
                    {

                        dtLocales.Rows[i]["SOLD_ORDEN"] = Convert.ToInt32(dtLocales.Rows[i]["SOLD_ORDEN"]) - 1;
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        dtLocales.Rows[i]["SOLD_ORDEN"] = 1;
                    }
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
            ViewState["locales"] = dtLocales;
            ObtenerLocalesSolicitud(false);
            stringLocalesSeleccionados(dtLocales);
            marca_seleccion();
            calcula_solicitud(null, null);
            if (txt_trailerPatente.Text != "" || this.txt_trailerNro.Text != "")
            {
                btn_buscarTrailer_Click(null, null);
            }
            ddl_playaDescarga_SelectedIndexChanged(null, null);
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
            this.calcula_solicitud(null, null);
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
            this.calcula_solicitud(null, null);
        }
    }
    #endregion
    #region Utils
    private void CargaDrops()
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        CaractCargaBC car = new CaractCargaBC();
        ShorteckBC sh = new ShorteckBC();
        DataTable dt;
        dt = yms.ObteneSites(usuario.ID);
        utils.CargaDropNormal(dropsite, "ID", "NOMBRE", dt);
        dt = car.obtenerXTipo(30);
        utils.CargaDrop(ddl_solTemp, "ID", "DESCRIPCION", dt);
        dt = car.obtenerXTipo(0);
        utils.CargaDrop(ddl_largoMax, "ID", "DESCRIPCION", dt);
        dt = sh.ObtenerTodos();
        utils.CargaDrop(ddl_idShortek, "SHOR_ID", "SHOR_DESC", dt);
        utils.CargaDrop(ddl_id_shortrec2, "SHOR_ID", "SHOR_DESC", dt);

    }
    private bool ComprobarTrailer(TrailerBC trailer)
    {
        if (trailer.ID == 0) //Trailer nuevo, no existe
        {
            hf_traiId.Value = "";
            txt_trailerNro.Text = "";
            txt_trailerPatente.Text = "";
          //  txt_trailerTransporte.Text = "";
            utils.ShowMessage2(this, "buscarTrailer", "warn_trailerNoExiste");
            return false;
        }
        if (trailer.SITE_IN == false)
        {
            hf_traiId.Value = "";
            txt_trailerNro.Text = "";
            txt_trailerPatente.Text = "";
          //  txt_trailerTransporte.Text = "";
            utils.ShowMessage2(this, "buscarTrailer", "warn_trailerFueraCD");
            return false;
        }
        if (trailer.SITE_ID != Convert.ToInt32(this.dropsite.SelectedValue))
        {
            hf_traiId.Value = "";
            txt_trailerNro.Text = "";
            txt_trailerPatente.Text = "";
          //  txt_trailerTransporte.Text = "";
            utils.ShowMessage2(this, "buscarTrailer", "warn_trailerOtroCD");
            return false;
        }
        else if (trailer.TRES_ID != 100)
        {
            switch (trailer.TRES_ID)
            {
                case 150:
                case 200:
                    hf_traiId.Value = "";
                    txt_trailerNro.Text = "";
                    txt_trailerPatente.Text = "";
            //        txt_trailerTransporte.Text = "";
                    utils.ShowMessage2(this, "buscarTrailer", "warn_trailerCargando");
                    return false;
                case 300:
                case 310:
                    hf_traiId.Value = "";
                    txt_trailerNro.Text = "";
                    txt_trailerPatente.Text = "";
                //    txt_trailerTransporte.Text = "";
                    utils.ShowMessage2(this, "buscarTrailer", "warn_trailerDescargando");
                    return false;
                case 400:
                    hf_traiId.Value = "";
                    txt_trailerNro.Text = "";
                    txt_trailerPatente.Text = "";
                //    txt_trailerTransporte.Text = "";
                    utils.ShowMessage2(this, "buscarTrailer", "warn_trailerCargado");
                    return false;
                case 500:
                    hf_traiId.Value = "";
                    txt_trailerNro.Text = "";
                    txt_trailerPatente.Text = "";
               //     txt_trailerTransporte.Text = "";
                    utils.ShowMessage2(this, "buscarTrailer", "warn_trailerRuta");
                    return false;
                case 600:
                    hf_traiId.Value = "";
                    txt_trailerNro.Text = "";
                    txt_trailerPatente.Text = "";
               //     txt_trailerTransporte.Text = "";
                    utils.ShowMessage2(this, "buscarTrailer", "warn_trailerBloqueado");
                    return false;
            }
        }

        if (trailer.MOVI_ID != 0)
        {

            hf_traiId.Value = "";
            txt_trailerNro.Text = "";
            txt_trailerPatente.Text = "";
        //    txt_trailerTransporte.Text = "";
            utils.ShowMessage2(this, "buscarTrailer", "warn_trailerMovPendiente");
            return false;

        }

        if (trailer.SOLI_ID != 0)
        {
            if (string.IsNullOrEmpty(hf_soliId.Value))
            {
                hf_traiId.Value = "";
                txt_trailerNro.Text = "";
                txt_trailerPatente.Text = "";
          //      txt_trailerTransporte.Text = "";
                utils.ShowMessage2(this, "buscarTrailer", "warn_trailerSolPendiente");
                return false;
            }
            else if (trailer.SOLI_ID.ToString() != hf_soliId.Value)
            {
                hf_traiId.Value = "";
                txt_trailerNro.Text = "";
                txt_trailerPatente.Text = "";
            //    txt_trailerTransporte.Text = "";
                utils.ShowMessage2(this, "buscarTrailer", "warn_trailerSolDistinta");
                return false;
            }
        }
        if (trailer.PYTI_ID == 300)
        {
            hf_traiId.Value = "";
            txt_trailerNro.Text = "";
            txt_trailerPatente.Text = "";
         //   txt_trailerTransporte.Text = "";
            utils.ShowMessage2(this, "buscarTrailer", "warn_trailerMantenimiento");
            return false;
        }
        if (string.IsNullOrEmpty(hf_soliId.Value) && trailer.MOVI_ID != 0)
        {
            hf_traiId.Value = "";
            txt_trailerNro.Text = "";
            txt_trailerPatente.Text = "";
       //     txt_trailerTransporte.Text = "";
            utils.ShowMessage2(this, "buscarTrailer", "warn_trailerMovPendiente");
            return false;
        }
        if (!string.IsNullOrEmpty(hf_soliId.Value) && hf_soliId.Value != "0")
        {
            SolicitudBC soli = new SolicitudBC().ObtenerXId(Convert.ToInt32(hf_soliId.Value));
            DataView dw = new DataView((DataTable)ViewState["trailers"]);
            dw.RowFilter = string.Format("TRAI_ID={0}", trailer.ID);
            if (dw.Count == 0 && soli.ID_TRAILER != trailer.ID)
            {
                hf_traiId.Value = "";
                txt_trailerPatente.Text = "";
                txt_trailerNro.Text = "";
         //       txt_trailerTransporte.Text = "";
                utils.ShowMessage2(this, "buscarTrailer", "warn_trailerIncompatible");
                return false;
            }
        }
        if (string.IsNullOrEmpty(hf_soliId.Value) || hf_soliId.Value == "0")
        {

            DataView dw = new DataView((DataTable)ViewState["trailers"]);
            dw.RowFilter = string.Format("TRAI_ID={0}", trailer.ID);
            if (dw.Count == 0)
            {
                hf_traiId.Value = "";
                txt_trailerPatente.Text = "";
                txt_trailerNro.Text = "";
           //     txt_trailerTransporte.Text = "";
                utils.ShowMessage2(this, "buscarTrailer", "warn_trailerIncompatible");
                return false;
            }
        }
        return true;
    }
    private bool ProxSoanOrden(int luga_id, out int soan_orden)
    {
        DataTable dt = (DataTable)this.ViewState["andenes"];
        DataView andenes = ((DataTable)this.ViewState["andenes"]).AsDataView();
        andenes.RowFilter = string.Format("LUGA_ID = {0} AND SOES_ID < 105", luga_id);
        if (andenes.ToTable().Rows.Count > 0)
        {
            soan_orden = Convert.ToInt32(andenes.ToTable().Rows[0]["SOAN_ORDEN"]);
            return false;
        }
        else
        {
            try
            {
                DataRow ultimoAnden = dt.Rows[dt.Rows.Count - 1];
                soan_orden = Convert.ToInt32(ultimoAnden["SOAN_ORDEN"]) + 1;
                return true;
            }
            catch (Exception)
            {
                soan_orden = 1;
                return true;
            }
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
    private bool ComprobarLocalExistente(string cod_local)
    {
        DataView dw = ((DataTable)this.ViewState["locales"]).AsDataView();
        dw.RowFilter = string.Format("SOLD_ORDEN_OLD IS NULL AND NUMERO = {0} AND SOAN_ORDEN>1", cod_local);
        return (dw.ToTable().Rows.Count == 0);
    }
    private void LimpiarLocales()
    {
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

        this.ViewState["andenes"] = dtAndenes;
        this.ViewState["locales"] = dtLocales;
        this.gv_solLocales.DataSource = this.ViewState["locales"];
        this.gv_solLocales.DataBind();
    }
    private void ObtenerLocalesSolicitud(bool forzarBD)
    {
        DataView dw;
        if (this.ViewState["locales"] == null || forzarBD)
        {
            int soli_id = Convert.ToInt32(this.hf_soliId.Value);
            DataSet ds;
            if (Request.Params["type"] == "devolucion")
            {
                ds = new SolicitudAndenesBC().ObtenerFinalizados(soli_id);
               

                int contador = 0;
                while (contador < ds.Tables["ANDENES"].Rows.Count)
                {

                 //   ds.Tables["ANDENES"].Rows[contador]["SOAN_ORDEN"] = "1";
                 //   ds.Tables["ANDENES"].Rows[contador]["luga_id"] = ds.Tables["ANDENES"].Rows[0]["luga_id"];
                 //   ds.Tables["ANDENES"].Rows[contador]["solicitud_anden"] = ds.Tables["ANDENES"].Rows[0]["solicitud_anden"];
                 //   ds.Tables["ANDENES"].Rows[contador]["anden"] = ds.Tables["ANDENES"].Rows[0]["anden"];
                 //   ds.Tables["ANDENES"].Rows[contador]["soes_id"] = 150;
                 //   ds.Tables["ANDENES"].Rows[contador]["soan_cod"] = ds.Tables["ANDENES"].Rows[0]["soan_cod"];

                    contador = contador + 1;
                }


                dw = new DataView(ds.Tables["ANDENES"]);
                dw.RowFilter = "SOAN_ORDEN = 1";
                ViewState["andenes"] = dw.ToTable();
                DataTable dt = ds.Tables["LOCALES"];
                foreach (DataRow dr in dt.Rows)
                {
                    dr["LUGA_ID"] = ds.Tables["ANDENES"].Rows[0]["LUGA_ID"];
                    dr["SOAN_ORDEN"] = ds.Tables["ANDENES"].Rows[0]["SOAN_ORDEN"];
                    dr["ANDEN"] = "--";//ds.Tables["ANDENES"].Rows[0]["ANDEN"];
                }
                ViewState["locales"] = dt;
            }
            else
            {
                ds = new SolicitudAndenesBC().ObtenerTodo(soli_id);
                ViewState["locales"] = ds.Tables["LOCALES"];
                ViewState["andenes"] = ds.Tables["ANDENES"];
            }
            ReordenarLocales();
        }
        dw = ((DataTable)ViewState["locales"]).AsDataView();
        dw.Sort = "SOAN_ORDEN,SOLD_ORDEN ASC";
        ViewState["locales"] = dw.ToTable();
        gv_solLocales.DataSource = ViewState["locales"];
        gv_solLocales.DataBind();
        calcula_solicitud(null, null);
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
    private int obtenerId(string codigo)
    {
        CaractCargaBC cc = new CaractCargaBC();
        cc = cc.obtenerSeleccionado(0, codigo);
        return cc.ID;
    }
    private void stringCaractSolicitud()
    {
        hf_caractSolicitud.Value = "";
        if (chk_solFrio.Checked)
            hf_caractSolicitud.Value += obtenerId("CCF");
        else if (chk_solSeco.Checked)
            hf_caractSolicitud.Value += obtenerId("CCS");
        else if (chk_solCongelado.Checked)
            hf_caractSolicitud.Value += obtenerId("CCC");
        else if (chk_solMultifrio.Checked)
            hf_caractSolicitud.Value += obtenerId("CCMF");
        else if (chk_solWays.Checked)
            hf_caractSolicitud.Value += obtenerId("CCWAY");
    }
    private void marca_seleccion()
    {
        CaractCargaBC cc = new CaractCargaBC();
        DataTable dt = cc.caracteristicasdesdelocales(this.hf_localesSeleccionados.Value, this.hf_caractSolicitud.Value);

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
            Session["MaxPallet"] = maxpallet;
        }

        chk_plancha.Checked = maxplancha.ToString().Equals("7");// 6 caca plancha falso
        ddl_largoMax.SelectedValue = mincantidad.ToString();
    }
    private void stringLocalesSeleccionados(DataTable dt)
    {
        try
        {
            this.hf_localesSeleccionados.Value = "";
            bool primero = true;
            string locales = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (primero)
                {
                    locales += dr["LOCA_ID"].ToString();
                    primero = false;
                }
                else
                {
                    locales += string.Format(",{0}", dr["LOCA_ID"].ToString());
                }
            }
            this.hf_localesSeleccionados.Value = locales;
        }
        catch (Exception)
        {
        }
    }
    public void calcula_solicitud(object sender, EventArgs e)
    {
        stringCaractSolicitud();
        stringLocalesSeleccionados((DataTable)this.ViewState["locales"]);
        locales_Compatibles();
        marca_seleccion();

        try
        {
            DataTable dt = (DataTable)this.ViewState["locales"];
            int contador = 0;
            foreach (DataRow row in dt.Rows)
            {
                contador += Convert.ToInt32(row["PALLETS"].ToString());
            }
            this.txt_totalPallets.Text = contador.ToString();
        }
        catch (Exception)
        {
            this.txt_totalPallets.Text = "0";
        }
        this.carga_trailers();
    }
    private void carga_temperaturas()
    {
        if (this.chk_solFrio.Checked || this.chk_solCongelado.Checked || this.chk_solSeco.Checked || this.chk_solMultifrio.Checked || this.chk_solMultifrio.Checked || this.chk_solWays.Checked)
        {
            this.DDL_TEMP.Enabled = true;
            SolicitudBC solicitud = new SolicitudBC();
            DataTable dt = solicitud.Obtenertemperaturas(this.chk_solFrio.Checked, this.chk_solCongelado.Checked, this.chk_solSeco.Checked, this.chk_solMultifrio.Checked, this.chk_solWays.Checked);
            utils.CargaDrop(this.DDL_TEMP, "ID", "VALOR", dt);
        }
        else
        {
            this.DDL_TEMP.ClearSelection();
            this.DDL_TEMP.Enabled = false;
        }
    }
    private void carga_playas()
    {
        this.carga_temperaturas();
        CaractCargaBC cc = new CaractCargaBC();
        DataTable dt = cc.obtenerplayasCompatibles(this.hf_localesSeleccionados.Value, this.hf_caractSolicitud.Value, Int32.Parse(this.dropsite.SelectedValue));
        utils.CargaDrop(ddl_solPlaya, "PLAY_ID", "PLAY_DESC", dt);
        utils.CargaDrop(ddl_playaDescarga, "PLAY_ID", "PLAY_DESC", dt);
        this.ddl_solPlaya_SelectedIndexChanged(null, null);
    }
    private void carga_trailers()
    {
        try
        {
            ddl_id_shortrec2.Enabled = false;
            ddl_id_shortrec2.ClearSelection();
            CaractCargaBC cc = new CaractCargaBC();
            DataTable dt = cc.obtenertrailersCompatibles(hf_localesSeleccionados.Value, Convert.ToInt32(txt_totalPallets.Text), hf_caractSolicitud.Value, Convert.ToInt32(dropsite.SelectedValue));
            ViewState["trailers"] = dt;
            string query = "SOLI_ID = 0";
            if (ddl_idShortek.SelectedIndex > 0)
            {
                query += string.Format(" AND ID_SHORTEK = '{0}'  ", ddl_idShortek.SelectedValue);
                DataTable dt2;
                try
                {
                    dt2 = dt.Select(query).CopyToDataTable();
                }
                catch (Exception ex3)
                {
                    ddl_id_shortrec2.Enabled = true;
                    dt2 = new DataTable();
                }
                dt = dt2;
            }
            else
            {
                DataTable dt2 = dt.Select(query).CopyToDataTable();
                dt = dt2;
            }


            try
            {
                utils.CargaDrop_patentes(ddl_trailers, "TRAI_ID", "TRAI_PATENTE", TopDataRow(dt, 20), null, "id_shortek", ddl_idShortek.SelectedValue);
            }
            catch (Exception)
            {
                ddl_id_shortrec2.Enabled = true;

                ddl_trailers.Items.Clear();
                ddl_trailers.Items.Add(new ListItem("No Disponibles", "0"));
            }

            ddl_trailers.ClearSelection();
        }
        catch (Exception ex)
        {
        }


    }
    protected void carga_trailers_sh2(object sender, EventArgs e)
    {
        try
        {

            CaractCargaBC cc = new CaractCargaBC();
            DataTable dt = cc.obtenertrailersCompatibles(this.hf_localesSeleccionados.Value, Convert.ToInt32(this.txt_totalPallets.Text), this.hf_caractSolicitud.Value, Convert.ToInt32(this.dropsite.SelectedValue));


            this.ViewState["trailers"] = dt;
            string query = "SOLI_ID = 0";


            if (this.ddl_id_shortrec2.SelectedIndex > 0)
            {
                query += string.Format(" AND ID_SHORTEK = '{0}'  ", this.ddl_id_shortrec2.SelectedValue);
                DataTable dt2;
                try
                {
                    dt2 = dt.Select(query).CopyToDataTable();
                }
                catch (Exception ex3)
                {
                    dt2 = new DataTable();
                    this.ddl_trailers.Items.Clear();
                    this.ddl_trailers.Items.Add(new ListItem("No Disponibles", "0"));
                    return;

                }

                dt = dt2;
            }
            else
            {

                carga_trailers();
                return;

            }


            try
            {
                utils.CargaDrop_patentes(this.ddl_trailers, "TRAI_ID", "TRAI_PATENTE", this.TopDataRow(dt, 20), null, "id_shortek", this.ddl_idShortek.SelectedValue);
            }
            catch (Exception)
            {
                this.ddl_trailers.Items.Clear();
                this.ddl_trailers.Items.Add(new ListItem("No Disponibles", "0"));
            }

            this.ddl_trailers.ClearSelection();
        }
        catch (Exception ex)
        {
        }

    }
    public DataTable TopDataRow(DataTable dt, int count)
    {
        DataTable dtn = dt.Clone();
        int i = 0;
        foreach (DataRow row in dt.Rows)
        {
            if (i < count)
            {
                dtn.ImportRow(row);
                i++;
            }
            if (i > count)
            {
                break;
            }
        }
        return dtn;
    }
    private void locales_Compatibles()
    {
        CaractCargaBC cc = new CaractCargaBC();
        DataTable dt = cc.obtenerCompatibles(this.hf_localesSeleccionados.Value, this.hf_caractSolicitud.Value);
        bool primero = true;
        string compatibles = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (primero)
            {
                compatibles += dr[0].ToString();
                primero = false;
            }
            else
            {
                compatibles += string.Format(",{0}", dr[0].ToString());
            }
        }
        this.hf_localesCompatibles.Value = compatibles;
    }
    #endregion
    public DataTable GroupBy(string i_sGroupByColumn, string i_sAggregateColumn, DataTable i_dSourceTable)
    {
        DataView dv = new DataView(i_dSourceTable);

        //getting distinct values for group column
        DataTable dtGroup = dv.ToTable(true, new string[] { i_sGroupByColumn });

        //adding column for the row count
        dtGroup.Columns.Add("Count", typeof(int));

        //looping thru distinct values for the group, counting
        foreach (DataRow dr in dtGroup.Rows)
        {
            dr["Count"] = i_dSourceTable.Compute(string.Format("Count({0})", i_sAggregateColumn), string.Format("{0} = '{1}'", i_sGroupByColumn, dr[i_sGroupByColumn]));
        }

        //returning grouped/counted result
        return dtGroup;
    }

    protected void visibleasignamovilmanual(Object sender,  EventArgs e)
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        UsuarioBC usuario = (UsuarioBC)this.Session["USUARIO"];
        if (usuario != null)
        {
            Boolean ds1 = yms.visibleasignartrailer(Convert.ToInt32(this.dropsite.SelectedValue), usuario.ID);

            visiblasignamanual.Visible = ds1;
            chk_trailer.Checked = ds1;
            pnl_trailer.Enabled = ds1;
        }


        visiblasignamanual.Enabled = chk_TRASVASIJE.Checked;
        chk_trailer.Checked = chk_TRASVASIJE.Checked;
        pnl_trailer.Enabled = chk_TRASVASIJE.Checked; ;
    }

}