using System;
using System.Linq;
using System.Data;
using System.Web.UI;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Text;

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
    #region TextBox
    protected void txt_rutChofer_TextChanged(object sender, EventArgs e)
    {
        if (!chk_conductorExtranjero.Checked && !utils.validarRut(txt_conductorRut.Text))
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            utils.ShowMessage2(this, "conductor", "warn_rutNovalido");
            return;
        }
        ConductorBC c = new ConductorBC(txt_conductorRut.Text);

        if (c.ID == 0)
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            utils.ShowMessage2(this, "conductor", "warn_conductorNoexiste");
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
    #region Buttons
    protected void btn_limpiar_Click(object sender, EventArgs e)
    {
        hf_idTrailer.Value = "";
        hf_idCond.Value = "";
        dv_contenido.Attributes.Add("style", "display:none");
        txt_Patente.Text = "";
        txt_NroFlota.Text = "";
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
        dv_locales.Visible = false;
        dv_destino.Visible = false;
        gilllocal.DataSource = null;
        gilllocal.DataBind();
        Gridviajes.DataSource = null;
        Gridviajes.DataBind();
        txt_gpsActivoTracto.Text = "";
        dv_contenido.Attributes.Add("style", "display:none");
    }
    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        TrailerUltSalidaBC tu = new TrailerUltSalidaBC();
        tu.COND_ID = Convert.ToInt32(hf_idCond.Value);
        tu.ID = Convert.ToInt32(hf_idTrailer.Value);
        tu.PATENTE_TRACTO = txt_patenteTracto.Text;
        tu.CHOFER_RUT = txt_conductorRut.Text;
        tu.CHOFER_NOMBRE = txt_conductorNombre.Text;

        if (ddl_tipoDestino.SelectedValue == "DLPR")
            tu.LOCA_ID = Convert.ToInt32(ddl_destino.SelectedValue);
        else
            tu.DEST_ID = Convert.ToInt32(ddl_destino.SelectedValue);
        tu.ESTADO_YMS = estado_yms.Value;
        tu.OBSERVACION = locales_YMS.Value.ToString();
        string resultado;
        bool ejecucion = tu.ProcesoSalida(tu, usuario.ID, "", lblGPS.Text, out resultado);

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
        trailer = (!string.IsNullOrEmpty(txt_NroFlota.Text)) ? trailer.obtenerXNro(txt_NroFlota.Text) : trailer.obtenerXPlaca(txt_Patente.Text);

        if (trailer.ID == 0)
        {
            utils.ShowMessage2(this, "trailer", "warn_noExiste");
            btn_limpiar_Click(null, null);
            return;
        }
        DataSet datos = trailer.obtenerDatosSalida(trailer.PLACA, trailer.NUMERO);
        DataTable dt;
        if (datos.Tables.Count > 0)
        {
            dt = datos.Tables[0];
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
            txt_patenteTracto.Text = dt.Rows[0]["PATENTE_TRACTO"].ToString();
            if (dt.Rows[0]["COND_ID"] != DBNull.Value)
            {
                int cond_id = Convert.ToInt32(dt.Rows[0]["COND_ID"]);
                ConductorBC c = new ConductorBC(cond_id);
                txt_conductorRut.Text = utils.rutANumero(c.RUT);
                txt_conductorNombre.Text = c.NOMBRE;
                chk_conductorExtranjero.Checked = c.COND_EXTRANJERO;
                hf_idCond.Value = cond_id.ToString();
            }
            string estado = dt.Rows[0]["TRES_ID"].ToString();
            estado_yms.Value = "";
            if (site == "10")
            {
                try
                {
                    if (datos.Tables[4].Rows[0]["Cargado"].ToString() == "NO")
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

                catch
                { estado = dt.Rows[0]["TRES_ID"].ToString(); }

            }
            locales_YMS.Value = "";
            if (estado != "400")
            {
                gilllocal.DataSource = null;
                gilllocal.DataBind();
                dv_locales.Visible = false;
                dv_destino.Visible = true;
            }
            else
            {
                try
                {
                    DataTable dtlocales = new SolicitudLocalesBC().CargaLocalesXSolicitudTrailer(Convert.ToInt32(hf_idTrailer.Value), "", Convert.ToInt32(this.dropsite.SelectedValue));
                    string output = "";
                    foreach (DataRow dr in dtlocales.Rows)
                    {
                        if (!string.IsNullOrEmpty(output))
                            output += ",";
                        output += dr["LOCA_COD"].ToString();
                    }

                    locales_YMS.Value = output;
                    gilllocal.DataSource = dtlocales;
                    gilllocal.DataBind();

                }
                catch
                {
                    gilllocal.DataSource = null;
                    gilllocal.DataBind();
                }
                dv_locales.Visible = true;
                dv_destino.Visible = false;
            }

            try
            {
                Gridviajes.DataSource = datos.Tables[2];
                Gridviajes.DataBind();

            }
            catch
            {
                Gridviajes.DataSource = null;
                Gridviajes.DataBind();
            }

            try
            {
                DataRow dr = datos.Tables[3].Rows[0];
                txt_gpsActivoTracto.Text = dr["STATUS"].ToString();
                lbl_tran.Text = dr["TRANSPORTE"].ToString();
            }
            catch
            {
                txt_gpsActivoTracto.Text = "";
                lbl_tran.Text = "";
            }

            try
            {
                DataRow dr = datos.Tables[1].Rows[0];
                lblTemperatura.Text = dr["TEMPERATURA"].ToString();
                lblGPS.Text = dr["STATUS"].ToString();
                lblauditoria.Text = dr["AU"].ToString();
                lblFechaDatos2.Text = dr["fh_dato"].ToString();
            }
            catch (Exception)
            {
                lblTemperatura.Text = "";
                lblGPS.Text = "";
                lblauditoria.Text = "";
                lblFechaDatos2.Text = "";
            }
        }
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