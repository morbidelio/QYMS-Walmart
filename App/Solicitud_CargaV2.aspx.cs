// Example header text. Can be configured in the options.
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Solicitud_Carga2 : System.Web.UI.Page
{
    UsuarioBC usuario = new UsuarioBC();
    static UtilsWeb utils = new UtilsWeb();

    public void ddl_idShortek_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.carga_trailers();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["usuario"] == null)
        {
            this.Response.Redirect("~/InicioQYMS2.aspx");
        }
        this.usuario = (UsuarioBC)this.Session["usuario"];
        SolicitudBC sol = new SolicitudBC();
        CaractCargaBC car = new CaractCargaBC();
        ShorteckBC sh = new ShorteckBC();
        
        if (!this.IsPostBack)
        {
            this.volver.Visible = false;
            sol.ID_TIPO = 1;
            sol.FECHA_CREACION = DateTime.Now;
            this.txt_solHora.Text = DateTime.Now.AddMinutes(63).ToShortTimeString();
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            utils.CargaDropNormal(this.dropsite, "ID", "NOMBRE", yms.ObteneSites(this.usuario.ID));
            utils.CargaDrop(this.ddl_solTemp, "ID", "DESCRIPCION", car.obtenerXTipo(30));
            utils.CargaDrop(this.ddl_largoMax, "ID", "DESCRIPCION", car.obtenerXTipo(0));
          //  utils.CargaDropTodos(this.ddl_idShortek, "SHOR_ID", "SHOR_DESC", sh.ObtenerTodos());
            ddl_idShortek.DataSource = sh.ObtenerTodos();
            ddl_idShortek.DataTextField = "SHOR_DESC";

            ddl_idShortek.DataValueField = "SHOR_ID";
            ddl_idShortek.DataBind();
            ddl_idShortek.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Seleccione", "0"));
            ddl_idShortek.SelectedValue = "0";

            ddl_id_shortrec2.DataSource = sh.ObtenerTodos();
            ddl_id_shortrec2.DataTextField = "SHOR_DESC";

            ddl_id_shortrec2.DataValueField = "SHOR_ID";
            ddl_id_shortrec2.DataBind();
            ddl_id_shortrec2.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("Seleccione", "0"));
            ddl_id_shortrec2.SelectedValue = "0";

           

            this.txt_solFecha.Text = DateTime.Now.AddMinutes(63).ToShortDateString();
            this.ddl_solTemp.ClearSelection();
            this.ddl_largoMax.ClearSelection();
            this.LimpiarLocales();
            if (this.Request.Params["type"] != null)
            {
                TrailerBC trailer = new TrailerBC();
                CaractCargaBC cc = new CaractCargaBC();
                this.hf_soliId.Value = this.Request.Params["id"].ToString();
                this.btn_limpiarDatos.Visible = false;
                this.txt_solNumero.Text = this.hf_soliId.Value;
                sol = sol.ObtenerXId(int.Parse(this.hf_soliId.Value));
                this.hf_localesSeleccionados.Value = sol.LOCALES;
                this.txt_totalPallets.Text = sol.Pallets.ToString();
                this.txt_ruta.Text = sol.RUTA;
                this.ddl_idShortek.SelectedValue = sol.ID_SHORTECK;
                this.hf_timeStamp.Value = sol.TIMESTAMP.ToString();
                this.volver.Visible = true;
                string[] caract = sol.CARACTERISTICAS.Split(",".ToCharArray());
                foreach (string c in caract)
                {
                    if (c != "")
                    {
                        cc = cc.obtenerSeleccionado(int.Parse(c));
                        switch (cc.CODIGO)
                        {
                            case "CCF":
                                this.chk_solFrio.Checked = true;
                                break;
                            case "CCS":
                                this.chk_solSeco.Checked = true;
                                break;
                            case "CCC":
                                this.chk_solCongelado.Checked = true;
                                break;
                            case "CCMF":
                                this.chk_solMultifrio.Checked = true;
                                break;
                            case "CCCP":
                                this.chk_plancha.Checked = true;
                                break;
                            case "CCWAY":
                                this.chk_solWays.Checked = true;
                                break;
                        }
                    }
                }
                this.chk_solMultifrio.Enabled = false;
                this.chk_solCongelado.Enabled = false;
                this.chk_solSeco.Enabled = false;
                this.chk_solFrio.Enabled = false;
                this.dropsite.Enabled = false;
                this.txt_solFecha.Enabled = false;
                this.txt_solHora.Enabled = false;
                SolicitudAndenesBC sa = new SolicitudAndenesBC();
                sa.ObtenerTodo(sol.SOLI_ID);
                this.ObtenerLocalesSolicitud(true);
                this.stringCaractSolicitud();
                this.carga_playas();
                if (sol.TETR_ID != 0)
                {
                    this.DDL_TEMP.SelectedValue = sol.TETR_ID.ToString();
                }
                trailer = trailer.obtenerXID(sol.ID_TRAILER);
                this.txt_trailerPatente.Text = trailer.PLACA;
                this.calcula_solicitud(null, null);
                this.btn_buscarTrailer_Click(null, null);
                
                if (this.hf_traiId.Value != "0" && sol.SOES_ID > 101)
                {
                    this.visiblasignamanual.Enabled = false;
                }
            }
        }
        this.visiblasignamanual.Visible = false;
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (usuario.numero_sites < 2)
        {
            this.SITE.Visible = false;
        }
    }

    #region Buttons
    protected void volver_click(object sender, EventArgs e)
    {
        this.Response.Redirect("./control_carga_new.aspx");
    }
    protected void btn_limpiarDatos_Click(object sender, EventArgs e)
    {
        this.hf_soliId.Value = "0";
        this.txt_solNumero.Text = "";
        this.dropsite.ClearSelection();
        this.chk_solFrio.Checked = false;
        this.chk_solSeco.Checked = false;
        this.chk_solCongelado.Checked = false;
        this.chk_solMultifrio.Checked = false;
        this.DDL_TEMP.ClearSelection();
        this.ddl_solTemp.ClearSelection();
        this.ddl_solPlaya.ClearSelection();
        this.ddl_solPlaya.Enabled = false;
        this.txt_buscaLocal.Text = "";
        this.txt_descLocal.Text = "";
        this.txt_destinoPallets.Text = "0";
        this.ddl_origenAnden.ClearSelection();
        this.ddl_trailers.ClearSelection();
        this.ddl_idShortek.ClearSelection();
        this.ddl_id_shortrec2.ClearSelection();
        this.txt_ruta.Text = "";
        this.txt_trailerShortek.Text = "";
        this.txt_trailerPatente.Text = "";
        this.txt_trailerNro.Text = "";
        this.txt_trailerTransporte.Text = "";
        this.hf_traiId.Value = "0";
        this.hf_trailerAuto.Value = "false";
        this.hf_localesSeleccionados.Value = "";
        this.ddl_largoMax.ClearSelection();
        LimpiarLocales();
        this.txt_destinoPallets.Text = "0";
        this.txt_totalPallets.Text = "0";
        this.txt_solFecha.Text = DateTime.Now.AddMinutes(63).ToShortDateString();
        this.txt_solHora.Text = DateTime.Now.AddMinutes(63).ToShortTimeString();
        this.ViewState["trailers"] = null;
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
            int luga_id = Convert.ToInt32(this.ddl_origenAnden.SelectedValue);
            anden = this.ddl_origenAnden.SelectedItem.Text;
            if (this.ProxSoanOrden(luga_id, out soan_orden))
            {
                DataTable dt2 = (DataTable)this.ViewState["andenes"];
                dt2.Rows.Add(soli_id, soan_orden, luga_id, anden, soes_id, null, this.ddl_origenAnden.SelectedValue, this.ddl_origenAnden.SelectedItem.Text);
                this.ViewState["andenes"] = dt2;
            }
            LocalBC l = new LocalBC();
            l = l.obtenerXCodigo(local);

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
            this.txt_descLocal.Text = "";
            this.txt_buscaLocal.Text = "";
            this.txt_destinoPallets.Text = "0";

            this.ViewState["locales"] = dt;
            this.ObtenerLocalesSolicitud(false);
            this.calcula_solicitud(null, null);
        }
    }
    protected void btn_buscarTrailer_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        this.hf_traiId.Value = "0";

        DataTable dt2 = (DataTable)this.ViewState["locales"];
        if (dt2.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe seleccionar al menos un anden y local de la solicitud');", true);
        }
        else if (string.IsNullOrEmpty(this.txt_trailerPatente.Text) && string.IsNullOrEmpty(this.txt_trailerNro.Text))
        {
            if (this.IsPostBack == true)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe ingresar al menos un identificador de trailer');", true);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(this.txt_trailerPatente.Text))
            {
                trailer = trailer.obtenerXPlaca(this.txt_trailerPatente.Text);
            }
            else if (!string.IsNullOrEmpty(this.txt_trailerNro.Text))
            {
                trailer = trailer.obtenerXNro(this.txt_trailerNro.Text);
            }

            this.txt_trailerTransporte.Text = "";

            if (trailer.ID == 0 || trailer.ID == null) //Trailer nuevo, no existe
            {
                this.hf_traiId.Value = "0";
                this.txt_trailerNro.Text = "";
                this.txt_trailerPatente.Text = "";
                this.txt_trailerTransporte.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer no existe en la base de datos.');", true);
            }
            else //Trailer existente, trae datos
            {
                if (trailer.SITE_ID == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer no registrado en el CD. No es posible Cargar');", true);
                }
                else
                {
                    if (trailer.SITE_ID != int.Parse(this.dropsite.SelectedValue))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer registrado en otro CD. No es posible Cargar');", true);
                    }
                    else if (trailer.TRES_ID != 100)
                    {
                        switch (trailer.TRES_ID)
                        {
                            case 150:
                            case 200:
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer está cargando.');", true);
                                break;
                            case 300:
                            case 310:
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer está descargando.');", true);
                                break;
                            case 400:
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer está cargado.');", true);
                                break;
                            case 500:
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer está en ruta.');", true);
                                break;
                            case 600:
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer está bloqueado.');", true);
                                break;
                        }
                        txt_trailerTransporte.Text = "";
                    }
                    else if (string.IsNullOrEmpty(this.hf_soliId.Value) && (trailer.SOLI_ID != null && trailer.SOLI_ID != 0))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer tiene solicitud pendiente.');", true);
                    }
                    else if (!string.IsNullOrEmpty(this.hf_soliId.Value) &&
                             ((trailer.SOLI_ID.ToString() != this.hf_soliId.Value) && (trailer.SOLI_ID != 0)))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer asignado a otra solicitud.');", true);
                    }
                    else if (trailer.PYTI_ID == 300)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer en Mantenimiento.');", true);
                    }
                    else if (string.IsNullOrEmpty(this.hf_soliId.Value) && (trailer.MOVI_ID != null && trailer.MOVI_ID != 0))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer tiene movimiento pendiente.');", true);
                    }
                    else
                    {
                        this.hf_traiId.Value = trailer.ID.ToString();
                        this.txt_trailerPatente.Text = trailer.PLACA;
                        this.txt_trailerNro.Text = trailer.NUMERO;
                        this.txt_trailerShortek.Text = trailer.ID_SHORTEK;
                        this.txt_trailerTransporte.Text = trailer.TRANSPORTISTA.ToString();

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Se cargaron los datos del trailer seleccionado.');", true);
                    }
                }
            }
        }

        if (this.hf_traiId.Value != "0")
        {
            DataTable dt = (DataTable)(this.ViewState["trailers"]);
            if (dt != null && dt.Select(string.Format("TRAI_ID={0}", this.hf_traiId.Value .ToString())).Count() < 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje2", "showAlert2('Trailer Incompatible con solicitud o bloqueado.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje2", "showAlert('Se cargaron los datos del trailer seleccionado.');", true);
            }
        }
    }
    protected void btn_confirmarMov_Click(object sender, EventArgs e)
    {
        if (this.hf_ordenAndenesOk.Value.ToLower() == "true")
        {
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('No hay andenes de carga seleccionados para la solicitud');", true);
            }
            else
            {
                SolicitudBC s = new SolicitudBC();
                s.ID_TIPO = 1;
                s.ID_SITE = Convert.ToInt32(this.dropsite.SelectedValue);
                s.ID_USUARIO = usuario.ID; //Variable sesión
                s.FECHA_CREACION = DateTime.Now;
                s.FECHA_PLAN_ANDEN = Convert.ToDateTime(string.Format("{0} {1}", this.txt_solFecha.Text, this.txt_solHora.Text));
                s.DOCUMENTO = "";
                s.OBSERVACION = "";
                s.RUTA = this.txt_ruta.Text;
                s.ID_SHORTECK = this.ddl_idShortek.SelectedValue;
                if (!String.IsNullOrEmpty(this.hf_traiId.Value))
                {
                    s.ID_TRAILER_RESERVADO = Convert.ToInt32(this.hf_traiId.Value);
                }
                s.TETR_ID = Convert.ToInt32(this.DDL_TEMP.SelectedValue);
                s.Pallets = Convert.ToInt32(this.txt_totalPallets.Text);
                s.CARACTERISTICAS = this.hf_caractSolicitud.Value;
                DataTable dt3 = (DataTable)(this.ViewState["trailers"]);
                if (DateTime.Now.AddMinutes(57) > s.FECHA_PLAN_ANDEN)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Se Necesita al menos una hora para colocar el Trailer en Anden.');", true);
                    return;
                }
                if (string.IsNullOrEmpty(this.hf_traiId.Value))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('debe seleccionar trailer');", true);
                    return;
                }
                if ((dt3 != null && dt3.Select(string.Format("TRAI_ID = {0}", this.hf_traiId.Value)).Count() < 1) && (this.hf_traiId.Value != "0" || this.hf_soliId.Value != "0"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer Incompatible con solicitud o bloqueado');", true);
                    return;
                }
                TrailerBC tr = new TrailerBC();
                if (s.ID_TRAILER_RESERVADO != 0)
                {
                    tr = tr.obtenerXID(int.Parse(this.hf_traiId.Value));
                    if (s.ID_TRAILER_RESERVADO != 0 && (tr.SOLI_ID != 0 || tr.MOVI_ID != 0))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer asignado a Solicitud o Movimiento');", true);
                        return;
                    }
                }
                string mensaje = "";
                DataSet ds = new DataSet();
                dtAndenes.TableName = "ANDENES";
                dtLocales.TableName = "LOCALES";
                ds.Tables.Add(dtAndenes);
                ds.Tables.Add(dtLocales);
                if (this.hf_soliId.Value == "0")
                {
                    if (s.Carga(ds, out mensaje) && mensaje == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Solicitud Creada Correctamente.');", true);
                        btn_limpiarDatos_Click(null, null);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');",mensaje), true);
                    }
                }
                else
                {
                    s.TIMESTAMP = DateTime.Parse(this.hf_timeStamp.Value);
                    if (!s.validarTimeStamp())
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Timestamp incorrecto.');", true);
                        return;
                    }
                    else
                    {
                        if (s.ModificarCarga(ds, out mensaje) && mensaje == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Todo Ok');", true);
                            Response.Redirect("control_carga_new.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", mensaje), true);
                        }
                    }
                }
            }
        }
    }
    protected void btn_anularSol_Click(object sender, EventArgs e)
    {
        SolicitudBC solicitud = new SolicitudBC();
        string error;
        if (solicitud.Eliminar(int.Parse(this.hf_traiId.Value), out error) && error == "")
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
        if (this.ddl_solPlaya.SelectedIndex != 0)
        {
            CargaDrops drops = new CargaDrops();
            drops.Lugar1(this.ddl_origenAnden, 0, int.Parse(this.ddl_solPlaya.SelectedValue), -1, 1);
            if (this.ddl_origenAnden.Items.Count > 1)
            {
                this.ddl_origenAnden.Enabled = true;
            }
            else
            {
                this.ddl_origenAnden.Enabled = false;
            }
        }
        else
        {
            this.ddl_origenAnden.Enabled = false;
        }
    }
    protected void ddl_trailers_SelectChanged(object sender, EventArgs e)
    {
        this.txt_trailerPatente.Text = this.ddl_trailers.SelectedItem.Text;
        this.btn_buscarTrailer_Click(null, null);
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
        this.calcula_solicitud(null, null);
        this.carga_playas();
        if (this.chk_solFrio.Checked || this.chk_solSeco.Checked || this.chk_solMultifrio.Checked || this.chk_solCongelado.Checked || this.chk_solWays.Checked)
        {
            this.ddl_solPlaya.Enabled = true;
        }
        else
        {
            this.ddl_solPlaya.Enabled = false;
        }
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
        }
        else
        {
            this.txt_buscaLocal.Text = "";
            this.txt_descLocal.Text = "Local no encontrado";
        }
    }
    #endregion
    #region GridView
    protected void gv_solLocales_rowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtLocales = (DataTable)this.ViewState["locales"];
            LinkButton btn_eliminarLocal = (LinkButton)e.Row.FindControl("btn_eliminarLocal");
            LinkButton btnSubir = (LinkButton)e.Row.FindControl("btnSubir");
            LinkButton btnBajar = (LinkButton)e.Row.FindControl("btnBajar");
            if (dtLocales.Rows.Count == 1)
            {
                btn_eliminarLocal.Visible = false;
            }
            int orden_a = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SOAN_ORDEN"));

            DataTable dtAndenes = (DataTable)this.ViewState["andenes"];

            int soes_id = Convert.ToInt32(dtAndenes.Select(string.Format("SOAN_ORDEN = {0}", orden_a))[0]["SOES_ID"]);
            if (soes_id >= 105)
            {
                btn_eliminarLocal.Visible = false;
                btnSubir.Visible = false;
                btnBajar.Visible = false;
            }
            else
            {
                DataRow dr;
                try
                {
                    dr = dtLocales.Rows[e.Row.DataItemIndex + 1];
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
                    dr = dtLocales.Rows[e.Row.DataItemIndex - 1];
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
            this.ViewState["locales"] = dtLocales;
            this.ObtenerLocalesSolicitud(false);
            this.stringLocalesSeleccionados(dtLocales);
            this.marca_seleccion();
            this.calcula_solicitud(null, null);
            if (this.txt_trailerPatente.Text != "" || this.txt_trailerNro.Text != "")
            {
                this.btn_buscarTrailer_Click(null, null);
            }
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
        dw.RowFilter = string.Format("SOLD_ORDEN_OLD IS NULL AND NUMERO = {0}", cod_local);
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
        if (this.ViewState["locales"] == null || forzarBD)
        {
            SolicitudAndenesBC sa = new SolicitudAndenesBC();
            sa.SOLI_ID = Convert.ToInt32(this.hf_soliId.Value);
            DataSet ds = sa.ObtenerTodo(sa.SOLI_ID);
            this.ViewState["locales"] = ds.Tables["LOCALES"];
            this.ViewState["andenes"] = ds.Tables["ANDENES"];
            this.ReordenarLocales();
        }
        DataView dw = ((DataTable)this.ViewState["locales"]).AsDataView();
        dw.Sort = "SOAN_ORDEN,SOLD_ORDEN ASC";
        this.ViewState["locales"] = dw.ToTable();
        this.gv_solLocales.DataSource = this.ViewState["locales"];
        this.gv_solLocales.DataBind();
        this.calcula_solicitud(null, null);
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
        string caracteristicas = "";
        bool segundo = false;
        if (this.chk_solFrio.Checked)
        {
            caracteristicas += this.obtenerId("CCF");
            segundo = true;
        }
        if (this.chk_solSeco.Checked)
        {
            if (segundo)
            {
                caracteristicas += ",";
            }
            caracteristicas += this.obtenerId("CCS");
            segundo = true;
        }
        if (this.chk_solCongelado.Checked)
        {
            if (segundo)
            {
                caracteristicas += ",";
            }
            caracteristicas += this.obtenerId("CCC");
            segundo = true;
        }
        //if (this.chk_plancha.Checked)
        //{
        //    if (segundo)
        //    {
        //        caracteristicas += ",";
        //    }
        //    caracteristicas += this.obtenerId("CCCP");
        //    segundo = true;
        //}
        if (this.chk_solMultifrio.Checked)
        {
            if (segundo)
            {
                caracteristicas += ",";
            }
            caracteristicas += this.obtenerId("CCMF");
            segundo = true;
        }
        if (this.chk_solWays.Checked)
        {
            if (segundo)
            {
                caracteristicas += ",";
            }
            caracteristicas += this.obtenerId("CCWAY");
            segundo = true;
        }
        //else
        //{
        //    if (segundo)
        //        caracteristicas += ",";
        //    caracteristicas += ",34";
        //    segundo = true;
        //}
        //if (chk_plancha.Checked)
        //    caracteristicas += ",7";
        //else
        //    caracteristicas += ",6";
        //    caracteristicas += string.Format(",{0}", this.ddl_largoMax.SelectedValue);
        //caracteristicas += string.Format(",{0}", this.DDL_TEMP.SelectedValue);
        this.hf_caractSolicitud.Value = caracteristicas;
    }
    private void marca_seleccion()
    {
        CaractCargaBC cc = new CaractCargaBC();
        DataTable dt = cc.caracteristicasdesdelocales(this.hf_localesSeleccionados.Value, this.hf_caractSolicitud.Value);

        //   this.chk_solFrio.Checked= Boolean.Parse( dt.Select("min(caca_orden) where caract_ID='20'")[0][0].ToString());
        
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
            this.Session["MaxPallet"] = maxpallet;
        }

        //this.chk_plancha.Checked = !maxplancha.ToString().Equals("6");// 6 caca plancha falso
        this.chk_plancha.Checked = maxplancha.ToString().Equals("7");// 6 caca plancha falso
        this.ddl_largoMax.SelectedValue = mincantidad.ToString();
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
        this.stringCaractSolicitud();
        this.stringLocalesSeleccionados((DataTable)this.ViewState["locales"]);
        this.locales_Compatibles();
        this.marca_seleccion();

        try
        {
            DataTable dt = (DataTable)this.ViewState["locales"] ;
            int contador = 0;
            foreach (DataRow row in dt.Rows)
            {
                contador += int.Parse(row["PALLETS"].ToString());
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
        utils.CargaDrop(this.ddl_solPlaya, "PLAY_ID", "PLAY_DESC", dt);
        this.ddl_solPlaya_SelectedIndexChanged(null, null);
    }
    private void carga_trailers()
    {
        try
        {
            ddl_id_shortrec2.Enabled = false;
            ddl_id_shortrec2.ClearSelection();
            CaractCargaBC cc = new CaractCargaBC();
            DataTable dt = cc.obtenertrailersCompatibles(this.hf_localesSeleccionados.Value, Convert.ToInt32(this.txt_totalPallets.Text), this.hf_caractSolicitud.Value, Convert.ToInt32(this.dropsite.SelectedValue));

            
            this.ViewState["trailers"] = dt;
            string query = "SOLI_ID = 0";
                                   
            
            if (this.ddl_idShortek.SelectedIndex > 0)
            {
                query += string.Format(" AND ID_SHORTEK = '{0}'  ", this.ddl_idShortek.SelectedValue);
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
                utils.CargaDrop_patentes(this.ddl_trailers, "TRAI_ID", "TRAI_PATENTE", this.TopDataRow(dt, 20), null, "id_shortek", this.ddl_idShortek.SelectedValue);
            }
            catch (Exception)
            {
                ddl_id_shortrec2.Enabled = true;
            
                this.ddl_trailers.Items.Clear();
                this.ddl_trailers.Items.Add(new ListItem("No Disponibles", "0"));
            }

            this.ddl_trailers.ClearSelection();
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

    public void visibleasignamovilmanual()
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        UsuarioBC usuario = (UsuarioBC)this.Session["USUARIO"];
        if (usuario != null)
        {
            Boolean ds1 = yms.visibleasignartrailer(int.Parse(this.dropsite.SelectedValue), usuario.ID);

            this.visiblasignamanual.Visible = ds1;
            this.chk_trailer.Checked = ds1;
            this.pnl_trailer.Enabled = ds1;
        }
    }
}