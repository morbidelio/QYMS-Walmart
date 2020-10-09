using System;
using System.Linq;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;

partial class App_Proceso_Salida_lo_Aguirre : System.Web.UI.Page
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

            this.SITE.Visible = false;
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
            DataTable ds = yms.ObteneSites(usuario.ID);

            DestinoTipoBC desti = new DestinoTipoBC();
            utils.CargaDropNormal(this.dropsite, "ID", "NOMBRE", ds);
            utils.CargaDrop(ddl_tipoDestino, "CODIGO", "NOMBRE", desti.obtenerTodo());
            this.dropsite.SelectedValue = "10";
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
        dv_contenido.Attributes.Add("style", "display:none");
        //txt_nroViaje.Text = "";
        txt_Patente.Text = "";
        txt_NroFlota.Text = "";

        hf_idTrailer.Value = "";
        lblPlacaTrailer.Text = "";
        lblFlotaTrailer.Text = "";
        lblFechaDatos.Text = "";
        lblFechaDatos2.Text = "";
        lblEstado.Text = "";
        lblTransportista.Text = "";
        lblTipo.Text = "";
        lblauditoria.Text = "";
        lblEstadoSol.Text = "";
        lblTemperatura.Text = "";
        lblUbicacion.Text = "";
        txt_patenteTracto.Text = "";
        txt_conductorRut.Text = "";
        txt_conductorNombre.Text = "";
        hf_idCond.Value = "";
        dv_locales.Visible = false;
        dv_destino.Visible = false;
        gilllocal.DataSource = null;
        gilllocal.DataBind();
        Gridviajes.DataSource = null;
        Gridviajes.DataBind();
        txt_gpsActivoTracto.Text = "";
    }
    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        TrailerUltSalidaLABC tu = new TrailerUltSalidaLABC();
        string resultado;
        TrailerBC t = new TrailerBC();
        tu.ID = Convert.ToInt32(hf_idTrailer.Value);
        tu.PATENTE_TRACTO = txt_patenteTracto.Text;
        tu.CHOFER_RUT = utils.formatearRut(txt_conductorRut.Text);
        tu.CHOFER_NOMBRE = txt_conductorNombre.Text;
        tu.SITE_ID = Convert.ToInt32(ddl_destino.SelectedValue);
        if (ddl_tipoDestino.SelectedValue == "DLPR")
            tu.LOCA_ID = Convert.ToInt32(ddl_destino.SelectedValue);
        else
            tu.DEST_ID = Convert.ToInt32(ddl_destino.SelectedValue);

        tu.COND_ID = Convert.ToInt32(hf_idCond.Value);
        tu.CHOFER_RUT = txt_conductorRut.Text;
        tu.CHOFER_NOMBRE = txt_conductorNombre.Text;
        tu.ESTADO_YMS = estado_yms.Value;
        tu.OBSERVACION = locales_YMS.Value.ToString();
        tu.SELLO_CARGA = txt_sello.Text;
        tu.MMPP = txt_mmpp.Text;
        tu.GUIA = txt_gdNro.Text;
        tu.TRUE_COD_INTERNO_IN = Convert.ToInt64(hf_trueCodInterno.Value);
        if (!string.IsNullOrEmpty(txt_cajas.Text))
            tu.CAJAS = Convert.ToInt32(txt_cajas.Text);
        if (!string.IsNullOrEmpty(txt_liAzules.Text))
            tu.PALLET_AZUL = Convert.ToInt32(txt_liAzules.Text);
        if (!string.IsNullOrEmpty(txt_liRojos.Text))
            tu.PALLET_ROJO = Convert.ToInt32(txt_liRojos.Text);
        if (!string.IsNullOrEmpty(txt_liBlancos.Text))
            tu.PALLET_BLANCO = Convert.ToInt32(txt_liBlancos.Text);
        if (!string.IsNullOrEmpty(txt_liLeña.Text))
            tu.LEÑA = Convert.ToInt32(txt_liLeña.Text);

        tu.VIAJE = txt_nroViaje.Text;

        bool ejecucion = tu.ProcesoSalida(tu, LlenarTableLocales(), locales_YMS.Value, usuario.ID, out resultado);

        if (ejecucion && resultado == "")
        {
            btn_limpiar_Click(null, null);
            utils.ShowMessage2(this, "confirmar", "success");
        }
        else
        {
            utils.ShowMessage(this, resultado, "error", false);
        }
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        if (txt_nroViaje.Text != "")
            trailer = trailer.obtenerXviaje(txt_nroViaje.Text);
        else if (txt_NroFlota.Text != "")
            trailer = trailer.obtenerXNro(txt_NroFlota.Text);
        else if (utils.patentevalida(txt_Patente.Text) == true)
            trailer = trailer.obtenerXPlaca(txt_Patente.Text);
        else
        {
            btn_limpiar_Click(null, null);
            utils.ShowMessage2(this, "trailer", "warn_placaInvalida");
        }
        if (trailer.ID == 0)
        {
            btn_limpiar_Click(null, null);
            utils.ShowMessage2(this, "trailer", "warn_noExiste");
            return;
        }
        DataSet datos = trailer.obtenerDatosSalida(trailer.PLACA, trailer.NUMERO);

        if (datos.Tables.Count == 0)
        {
            btn_limpiar_Click(null, null);
            utils.ShowMessage2(this, "trailer", "warn_noExiste");
            return;
        }
        DataTable dt = datos.Tables[0];
        string site = dt.Rows[0]["SITE_ID"].ToString();
        if (!Convert.ToBoolean(dt.Rows[0]["TRUE_SITE_IN"]))
        {
            utils.ShowMessage2(this, "trailer", "warn_fueraSite");
            btn_limpiar_Click(null, null);
            return;
        }
        if (site != dropsite.SelectedValue || string.IsNullOrEmpty(site))
        {
            utils.ShowMessage2(this, "trailer", "warn_otroSite");
            btn_limpiar_Click(null, null);
            return;
        }
        hf_idTrailer.Value = dt.Rows[0]["TRAI_ID"].ToString();
        hf_idTran.Value = dt.Rows[0]["TRAN_ID"].ToString();
        hf_trueCodInterno.Value = dt.Rows[0]["TRUE_COD_INTERNO_IN"].ToString();
        lblPlacaTrailer.Text = dt.Rows[0]["PLACA"].ToString();
        lblFlotaTrailer.Text = dt.Rows[0]["FLOTA"].ToString();
        lblFechaDatos.Text = dt.Rows[0]["FECHA_MODIFICACION"].ToString();
        lblFechaDatos2.Text = dt.Rows[0]["FECHA_MODIFICACION"].ToString();
        lblEstado.Text = dt.Rows[0]["TRAILER_ESTADO"].ToString();
        lblTransportista.Text = dt.Rows[0]["TRANSPORTISTA"].ToString();
        lblTipo.Text = dt.Rows[0]["TIPO_TRAILER"].ToString();
        lblauditoria.Text = "En Construcción";
        lblEstadoSol.Text = dt.Rows[0]["ESTADO_SOL"].ToString();
        dv_contenido.Attributes.Add("style", "display:block");
        lblUbicacion.Text = dt.Rows[0]["UBICACION"].ToString();
        txt_obs.Text = dt.Rows[0]["TRUE_OBS"].ToString();
        txt_patenteTracto.Text = dt.Rows[0]["PATENTE_TRACTO"].ToString();
        if (!string.IsNullOrEmpty(dt.Rows[0]["COND_ID"].ToString()))
        {
            int cond_id = Convert.ToInt32(dt.Rows[0]["COND_ID"].ToString());
            ConductorBC c = new ConductorBC(cond_id);
            txt_conductorRut.Text = utils.rutANumero(c.RUT);
            txt_conductorNombre.Text = c.NOMBRE;
            chk_conductorExtranjero.Checked = c.COND_EXTRANJERO;
            hf_idCond.Value = cond_id.ToString();
        }
        string estado = dt.Rows[0]["TRES_ID"].ToString().ToLower();
        estado_yms.Value = "";
        if (site == "10")
        {
            try
            {
                DataTable dt3 = datos.Tables[4];
                if (dt3.Rows[0]["Cargado"].ToString() == "NO" && txt_nroViaje.Text == "")
                {
                    lblEstado.Text = "VACIO";
                    estado = "100";
                    estado_yms.Value = "1";
                }
                else
                {
                    lblEstado.Text = "CARGADO";
                    estado = "400";
                    estado_yms.Value = "0";
                }

            }

            catch (Exception ex)
            { estado = dt.Rows[0]["TRES_ID"].ToString().ToLower(); }

        }
        locales_YMS.Value = "";
        if (estado == "400")
        {
            ObtenerLocales();
        }
        else
        {
            gilllocal.DataSource = null;
            gilllocal.DataBind();
            dv_locales.Visible = false;
            dv_destino.Visible = true;
        }

        try
        {
            DataTable dtviaje = datos.Tables[2];
            Gridviajes.DataSource = dtviaje;
            Gridviajes.DataBind();
        }
        catch (Exception)
        {
        }

        try
        {
            DataTable dttracto = datos.Tables[3];
            txt_gpsActivoTracto.Text = dttracto.Rows[0]["STATUS"].ToString();
            lbl_tran.Text = dttracto.Rows[0]["TRANSPORTE"].ToString();
        }
        catch (Exception ex)
        {
        }

        try
        {
            DataTable dttrailerGPS = datos.Tables[1];
            lblTemperatura.Text = dttrailerGPS.Rows[0]["TEMPERATURA"].ToString();
            lblGPS.Text = dttrailerGPS.Rows[0]["STATUS"].ToString();
            lblauditoria.Text = dttrailerGPS.Rows[0]["AU"].ToString();
            lblFechaDatos2.Text = dttrailerGPS.Rows[0]["fh_dato"].ToString();
        }
        catch (Exception)
        {
        }
    }
    #endregion
    #region TextBox
    protected void txt_rutChofer_TextChanged(object sender, EventArgs e)
    {
        if (!chk_conductorExtranjero.Checked && !utils.validarRut(this.txt_conductorRut.Text))
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            txt_conductorNombre.Enabled = false;
            utils.ShowMessage2(this, "conductor", "warn_rutNovalido");
            return;
        }
        ConductorBC c = new ConductorBC();
        c = c.ObtenerXRut(utils.formatearRut(this.txt_conductorRut.Text));

        if (c.ID == 0)
        {
            hf_idCond.Value = "";
            txt_conductorNombre.Enabled = true;
            utils.ShowMessage2(this, "conductor", "warn_conductorNoexiste");
            return;
        }
        if (c.BLOQUEADO)
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            txt_conductorNombre.Enabled = false;
            utils.ShowMessage2(this, "conductor", "warn_conductorBloqueado");
            return;
        }
        txt_conductorNombre.Text = c.NOMBRE;
        txt_conductorNombre.Enabled = false;
        hf_idCond.Value = c.ID.ToString();
        chk_conductorExtranjero.Checked = c.COND_EXTRANJERO;
        utils.ShowMessage2(this, "conductor", "success");
    }
    #endregion
    #region UtilsPagina
    private void ObtenerLocales()
    {
        try
        {
            SolicitudLocalesBC s = new SolicitudLocalesBC();
            DataTable dtlocales = s.CargaLocalesXSolicitudTrailer(Convert.ToInt32(hf_idTrailer.Value), txt_nroViaje.Text, Convert.ToInt32(this.dropsite.SelectedValue));
            string output = "";
            for (int i = 0; i < dtlocales.Rows.Count; i++)
            {
                output = output + dtlocales.Rows[i]["LOCA_COD"].ToString();
                output += (i < dtlocales.Rows.Count) ? "," : string.Empty;
            }
            locales_YMS.Value = output;
            DataColumn[] key = new DataColumn[1];
            key[0] = dtlocales.Columns["LOCA_COD"];
            dtlocales.PrimaryKey = key;
            dtlocales.Columns.Add("SELLO", Type.GetType("System.String"));
            dtlocales.Columns.Add("SOBRE", Type.GetType("System.String"));
            dtlocales.Columns.Add("CARRO", Type.GetType("System.String"));
            dtlocales.Columns.Add("EMBARQUE", Type.GetType("System.String"));
            ViewState["locales"] = dtlocales;
            gilllocal.DataSource = dtlocales;
            gilllocal.DataBind();
        }
        catch (Exception ex)
        {
        }
        dv_locales.Visible = true;
        dv_destino.Visible = false;
    }
    private DataTable LlenarTableLocales()
    {
        DataTable dt = (DataTable)ViewState["locales"];
        foreach (GridViewRow r in gilllocal.Rows)
        {
            int index = r.RowIndex;
            int local = Convert.ToInt32(gilllocal.DataKeys[index].Value);
            string sello = ((TextBox)r.Cells[3].FindControl("txt_sello")).Text;
            string sobre = ((TextBox)r.Cells[4].FindControl("txt_sobre")).Text;
            string carro = ((TextBox)r.Cells[5].FindControl("txt_carro")).Text;
            string embarque = ((TextBox)r.Cells[6].FindControl("txt_embarque")).Text;
            DataRow dr = dt.Rows.Find(local);
            dr["SELLO"] = sello;
            dr["SOBRE"] = sobre;
            dr["CARRO"] = carro;
            dr["EMBARQUE"] = embarque;
        }
        return dt;
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