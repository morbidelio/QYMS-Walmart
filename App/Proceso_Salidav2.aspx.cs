using System;
using System.Linq;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

partial class App_Proceso_Salida : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    UsuarioBC usuario;

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
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("~/InicioQYMS2.aspx");
        usuario = (UsuarioBC)Session["USUARIO"];
        if (!IsPostBack)
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
            DestinoTipoBC desti = new DestinoTipoBC();
            utils.CargaDropNormal(this.dropsite, "ID", "NOMBRE", ds);
            utils.CargaDrop(ddl_tipoDestino, "CODIGO", "NOMBRE", desti.obtenerTodo());
        }
    }
    #region DropDownList
    protected void ddl_tipoDestino_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_tipoDestino.SelectedIndex > 0)
        {
            //DestinoTipoBC desti = new DestinoTipoBC();
            DataTable dt;
            //desti = desti.ObtenerSeleccionado(Convert.ToInt32(ddl_tipoDestino.SelectedValue), null);
            if (ddl_tipoDestino.SelectedValue == "DLPR")
            {
                LocalBC l = new LocalBC();
                dt = l.obtenerTodoLocal();
                utils.CargaDrop(ddl_destino, "ID", "DESCRIPCION", dt);
            }
            else
            {
                DestinoBC d = new DestinoBC();
                dt = d.ObtenerXTipo(ddl_tipoDestino.SelectedValue);
                utils.CargaDrop(ddl_destino, "ID", "NOMBRE", dt);
            }
            if (dt.Rows.Count > 0)
            {
                ddl_destino.Enabled = true;
            }
            else
                ddl_destino.Enabled = false;
        }
    }
    #endregion
    #region Buttons
    protected void btn_limpiar_Click(object sender, EventArgs e)
    {
        hf_idTrailer.Value = "";
        hf_idCond.Value = "";
        dv_contenido.Style.Add("display", "none");
        dv_locales.Style.Add("display", "none");
        dv_destino.Style.Add("display", "none");
        lblPlacaTrailer.Text = "";
        lblFlotaTrailer.Text = "";
        lbl_trailerFechaDatos.Text = "";
        lbl_tractoFecha.Text = "";
        lbl_trailerEstado.Text = "";
        lbl_trailerTransportista.Text = "";
        lbl_trailerTipo.Text = "";
        lblauditoria.Text = "";
        lblTemperatura.Text = "";
        lbl_trailerUbicacion.Text = "";
        txt_nroViaje.Text = "";
        txt_patenteTracto.Text = "";
        txt_conductorRut.Text = "";
        txt_conductorNombre.Text = "";
        gilllocal.DataSource = null;
        gilllocal.DataBind();
        datosViaje(null);
        txt_tractoGps.Text = "";
        dv_contenido.Style.Add("display", "none");
        Gridviajes.DataSource = null;
        Gridviajes.DataBind();
    }
    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        TrailerUltSalidaBC tu = new TrailerUltSalidaBC();
        tu.ID = Convert.ToInt32(hf_idTrailer.Value);
        tu.PATENTE_TRACTO = txt_patenteTracto.Text;
        tu.CHOFER_RUT = utils.formatearRut(txt_conductorRut.Text);
        tu.CHOFER_NOMBRE = txt_conductorNombre.Text;
        if (ddl_tipoDestino.SelectedValue == "DLPR")
            tu.LOCA_ID = Convert.ToInt32(ddl_destino.SelectedValue);
        else
            tu.DEST_ID = Convert.ToInt32(ddl_destino.SelectedValue);
        string resultado;

        if (string.IsNullOrEmpty(hf_idCond.Value))
        {
            ConductorBC c = new ConductorBC();
            c.RUT = txt_conductorRut.Text;
            c.NOMBRE = txt_conductorNombre.Text;
            c.TRAN_ID = Convert.ToInt32(hf_idTran.Value);
            c.COND_EXTRANJERO = chk_conductorExtranjero.Checked;
            hf_idCond.Value = c.AgregarIdentity().ToString();
        }
        tu.COND_ID = Convert.ToInt32(hf_idCond.Value);
        tu.CHOFER_RUT = txt_conductorRut.Text;
        tu.ESTADO_YMS = estado_yms.Value;
        tu.OBSERVACION = locales_YMS.Value.ToString();

        bool ejecucion = tu.ProcesoSalida(tu, LlenarTableLocales(), usuario.ID, txt_nroViaje.Text, lbl_trailerGPS.Text, out resultado);

        if (ejecucion && resultado == "")
        {
            btn_limpiar_Click(null, null);
            utils.ShowMessage2(this, "confirmar", "success");
        }
        else
            utils.ShowMessage(this, resultado, "error", false);
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        DataTable dt= new DataTable();
        try
        {
             dt = new TrailerBC().ObtenerSalidaXViaje(txt_nroViaje.Text);
        }
        catch (Exception EX)
        {
            utils.ShowMessage(this, EX.Message, "error", false);
            btn_limpiar_Click(null, null);
            return; 
        }

        if (dt.Rows.Count == 0) 
        {
            btn_limpiar_Click(null, null);
            utils.ShowMessage2(this, "buscarViaje", "warn_noEncontrado"); 
            return; 
        }
        DataRow dr = dt.Rows[0];
        TrailerBC trailer = new TrailerBC().obtenerXPlaca(dr["TRAILER"].ToString());
        if (trailer.ID == 0)
        {
            utils.ShowMessage2(this, "buscarViaje", "warn_trailerNoExiste");
            btn_limpiar_Click(null, null);
            return;
        }
        if (!trailer.SITE_IN)
        {
            utils.ShowMessage2(this, "buscarViaje", "warn_trailerFuera");
            btn_limpiar_Click(null, null);
            return;
        }
        if (trailer.SITE_ID != Convert.ToInt32(dropsite.SelectedValue))
        {
            utils.ShowMessage2(this, "buscarViaje", "warn_trailerOtroSite");
            btn_limpiar_Click(null, null);
            return;
        }
        ConductorBC conductor = new ConductorBC().ObtenerXRutSAP(dr["RUT_CONDUCTOR"].ToString());
        if (conductor.ID != 0)
        {
            hf_idCond.Value = conductor.ID.ToString();
            txt_conductorRut.Text = utils.rutANumero(conductor.RUT);
            txt_conductorNombre.Text = conductor.NOMBRE;
            chk_conductorExtranjero.Checked = conductor.COND_EXTRANJERO;
        }
        else
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = dr["RUT_CONDUCTOR"].ToString();
            txt_conductorNombre.Text = dr["NOMBRE_CONDUCTOR"].ToString();
            chk_conductorExtranjero.Checked = !new FuncionesGenerales().ValidaRut(txt_conductorRut.Text);
            txt_rutChofer_TextChanged(null, null);
        }
        TrailerEstadoBC estado = new TrailerEstadoBC().ObtenerXId(trailer.TRES_ID);
        LugarBC lugar = new LugarBC().obtenerXID(trailer.LUGAR_ID);
        hf_idTrailer.Value = trailer.ID.ToString();
        lblPlacaTrailer.Text = trailer.PLACA;
        lblFlotaTrailer.Text = trailer.NUMERO;
        lbl_trailerTransportista.Text = trailer.TRANSPORTISTA;
        lbl_trailerTipo.Text = trailer.TIPO;
        lbl_trailerEstado.Text = estado.DESCRIPCION;
        dv_contenido.Attributes.Add("style", "display:block");
        lbl_trailerUbicacion.Text = lugar.DESCRIPCION;
        txt_patenteTracto.Text = dr["TRACTO"].ToString();
        estado_yms.Value = "";

        if (trailer.TRES_ID == 400)
        {
            ObtenerLocales();
        }
        else
        {
            gilllocal.DataSource = null;
            gilllocal.DataBind();
            dv_locales.Style.Add("display", "none");
            dv_destino.Style.Add("display", "block");
        }

        try
        {

            lblTemperatura.Text = dr["TEM"].ToString();
            lbl_trailerGPS.Text = dr["ESTADO_TRAILER"].ToString();
            lblauditoria.Text = dr["AU"].ToString();
            lbl_trailerFechaDatos.Text = dr["FH_TRAILER"].ToString();
            lbl_tractoFecha.Text = dr["FH_TRACTO"].ToString();
            txt_tractoGps.Text = dr["ESTADO_TRACTO"].ToString();
            lbl_tractoTransportista.Text = dr["TRANSPORTE"].ToString();
        }
        catch
        {
            lblTemperatura.Text = "";
            lbl_trailerGPS.Text = "";
            lblauditoria.Text = "";
            lbl_trailerFechaDatos.Text = "";
            lbl_tractoFecha.Text = "";
            txt_tractoGps.Text = "";
            lbl_tractoTransportista.Text = "";
        }
    }
    #endregion
    #region TextBox
    protected void txt_rutChofer_TextChanged(object sender, EventArgs e)
    {
        if (!chk_conductorExtranjero.Checked && !utils.validarRut(txt_conductorRut.Text))
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            txt_conductorNombre.Enabled = false;
            utils.ShowMessage2(this, "conductor", "warn_rutNovalido");
            return;
        }
        ConductorBC c = new ConductorBC();
        c = c.ObtenerXRut(utils.formatearRut(txt_conductorRut.Text));

        if (c.ID == 0)
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            utils.ShowMessage2(this, "conductor", "success_conductorNoexiste");
            return;
        }
        if (c.BLOQUEADO)
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            utils.ShowMessage2(this, "conductor", "warn_conductorBloqueado");
            return;
        }
        txt_conductorNombre.Text = c.NOMBRE;
        hf_idCond.Value = c.ID.ToString();
        chk_conductorExtranjero.Checked = c.COND_EXTRANJERO;
        utils.ShowMessage2(this, "conductor", "success");
    }
    #endregion
    #region UtilsPAgina
    private void datosViaje(DataTable dt)
    {
        Gridviajes.DataSource = dt;
        Gridviajes.DataBind();
    }
    private void ObtenerLocales()
    {
        try
        {
            SolicitudLocalesBC s = new SolicitudLocalesBC();
            DataTable dtlocales = s.CargaLocalesXSolicitudTrailer(Convert.ToInt32(hf_idTrailer.Value), this.txt_nroViaje.Text, Convert.ToInt32(this.dropsite.SelectedValue));
            string output = "";
            for (int i = 0; i < dtlocales.Rows.Count; i++)
            {
                output = output + dtlocales.Rows[i]["LOCA_COD"].ToString();
                output += (i < dtlocales.Rows.Count) ? "," : string.Empty;
            }
            locales_YMS.Value = output;
            DataColumn[] key = new DataColumn[4];
            key[0] = dtlocales.Columns["SOLI_ID"];
            key[1] = dtlocales.Columns["LUGA_ID"];
            key[2] = dtlocales.Columns["SOAN_ORDEN"];
            key[3] = dtlocales.Columns["LOCA_ID"];
            dtlocales.PrimaryKey = key;
            dtlocales.Columns.Add("SOLD_SELLO", Type.GetType("System.String"));
            ViewState["locales"] = dtlocales;
            gilllocal.DataSource = dtlocales;
            gilllocal.DataBind();
        }
        catch (Exception ex)
        {
        }

        dv_locales.Style.Add("display", "block");
        dv_destino.Style.Add("display", "none");
    }


    protected void gilllocal_rowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox sello = (TextBox)e.Row.FindControl("txt_sello");

            if (sello != null)
            {
                string text_sello = DataBinder.Eval(e.Row.DataItem, "SELLO").ToString();

                sello.Text = text_sello;
            }
        }
    }


    private DataTable LlenarTableLocales()
    {
        DataTable dt = (DataTable)ViewState["locales"];
        DataTable dtout = new DataTable();
        dtout.Columns.Add("SOLI_ID", typeof(Int32));
        dtout.Columns.Add("LUGA_ID", typeof(Int32));
        dtout.Columns.Add("SOAN_ORDEN", typeof(Int32));
        dtout.Columns.Add("LOCA_ID", typeof(Int32));
        dtout.Columns.Add("SOLD_SELLO", typeof(String));
        foreach (GridViewRow r in gilllocal.Rows)
        {
            int index = r.RowIndex;
            dtout.Rows.Add(dt.Rows[index]["SOLI_ID"]
                , dt.Rows[index]["LUGA_ID"]
                , dt.Rows[index]["SOAN_ORDEN"]
                , dt.Rows[index]["LOCA_ID"]
                , ((TextBox)r.Cells[3].FindControl("txt_sello")).Text);
        }
        return dtout;
    }
    #endregion

    protected void chk_conductorExtranjero_CheckedChanged(object sender, EventArgs e)
    {
        if (!chk_conductorExtranjero.Checked && !utils.validarRut(txt_conductorRut.Text) && !string.IsNullOrEmpty(txt_conductorRut.Text))
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            txt_conductorNombre.Enabled = false;
            utils.ShowMessage2(this, "conductor", "warn_rutNovalido");
            return;
        }
    }
}