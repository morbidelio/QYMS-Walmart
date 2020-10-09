// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;

public partial class App_Solicitud_Pallets : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    CargaDrops drop = new CargaDrops();
    UsuarioBC usuario = new UsuarioBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["Usuario"] == null)
        {
            this.Response.Redirect("~/InicioQYMS2.aspx");
        }
        this.usuario = (UsuarioBC)this.Session["Usuario"];
        if (!this.IsPostBack)
        {
            this.drop.Site_Normal(this.dropsite, this.usuario.ID);
            this.drop_SelectedIndexChanged(null, null);
            txt_buscarFecha.Text = DateTime.Now.ToShortDateString();
            txt_buscarHora.Text = DateTime.Now.ToShortTimeString();
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (this.usuario.numero_sites < 2)
        {
            this.SITE.Visible = false;
        }
    }

    #region DropDownList

    protected void ddl_buscarPatente_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_buscarPatente.SelectedIndex > 0)
        {
            TrailerBC trailer = new TrailerBC();
            trailer = trailer.obtenerXID(Convert.ToInt32(this.ddl_buscarPatente.SelectedValue));
            LugarBC lugar = new LugarBC();
            bool error = true;
            lugar = lugar.obtenerXID(trailer.LUGAR_ID);
            DataTable dt_lugares = lugar.obtenerLugarEstado(lugar.ID_PLAYA, lugar.ID, lugar.ID_SITE );
            DataRow row_lugar = dt_lugares.Rows[0];
            this.hf_trailerId.Value = trailer.ID.ToString();
            this.pnl_detalleLugar.Style.Add("background-color", row_lugar["COLOR_LUGAR"].ToString());
            this.pnl_detalleLugar.Style.Add("color", "black");
            this.lbl_lugar.Text = row_lugar["LUGA_COD"].ToString();
            this.pnl_detalleTrailer.Attributes.Add("style", string.Format("background-color:{0};", row_lugar["TRTI_COLOR"].ToString()));
            int estado_sol = int.Parse(row_lugar["SOES_ID"].ToString());
            this.pnl_imgAlerta.BackColor = System.Drawing.ColorTranslator.FromHtml(row_lugar["COLOR"].ToString());
            if (row_lugar["tres_icono"].ToString() != "")
            {
                this.img_trailer.ImageUrl = row_lugar["tres_icono"].ToString();
            }
            this.lbl_origenZona.Text = lugar.ZONA;
            this.lbl_origenPlaya.Text = lugar.PLAYA;
            this.ddl_destinoZona1.Enabled = true;
            this.ddl_destinoZona2.Enabled = true;
            this.txt_nroFlota.Text = trailer.NUMERO;
            this.txt_transporte.Text = trailer.TRANSPORTISTA;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Se cargaron los datos del trailer seleccionado.');", true);
            error = false;

            if (error)
            {
                this.limpia(null, null);
            }
        }
        else
        {
            limpia(null, null);
        }
    }

    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        ZonaBC z = new ZonaBC();
        z.DESCRIPCION = "";
        z.SITE_ID = Convert.ToInt32(this.dropsite.SelectedValue);
        z.ZOTI_ID = 200;
        utils.CargaDrop(this.ddl_destinoZona1, "ID", "DESCRIPCION", z.ObtenerXParametros());
        utils.CargaDrop(this.ddl_destinoZona2, "ID", "DESCRIPCION", z.ObtenerXParametros());
        utils.CargaDrop(this.ddl_buscarPatente, "ID", "PLACA", t.obtenerDisponiblesDrop(Convert.ToInt32(this.dropsite.SelectedValue), false, false));
    }

    protected void ddl_destinoZona1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(this.ddl_destinoZona1.SelectedIndex == 0))
        {
            int id_zona = int.Parse(this.ddl_destinoZona1.SelectedValue);
            PlayaBC playa = new PlayaBC();
            utils.CargaDrop(this.ddl_destinoPlaya1, "ID", "DESCRIPCION", playa.ObtenerPlayasXCriterio(null, null, id_zona, false, "100"));
            if (this.ddl_destinoPlaya1.Items.Count > 1)
            {
                this.ddl_destinoPlaya1.Enabled = true;
                this.ddl_destinoPlaya1_SelectedIndexChanged(null, null);
                //if (ddl_destinoPlaya1.SelectedIndex != 0)
                //{
                //    int id_playa = int.Parse(ddl_destinoPlaya1.SelectedValue);
                //    YMS_ZONA_BC yms = new YMS_ZONA_BC();
                //    DataTable ds1 = yms.Obtenerlugares_playa(id_playa, null, "0");
                //    utils.CargaDrop(ddl_destinoPos1, "ID", "DESCRIPCION", ds1);
                //    if (ddl_destinoPos1.Items.Count > 1)
                //        ddl_destinoPos1.Enabled = true;
                //    else
                //        ddl_destinoPos1.Enabled = false;
                //}
            }
            else
            {
                this.ddl_destinoPlaya1.Enabled = false;
                this.ddl_destinoPos1.Enabled = false;
            }
        }
        else
        {
            this.ddl_destinoPos1.Enabled = false;
            this.ddl_destinoPlaya1.Enabled = false;
            this.ddl_destinoPos1.SelectedIndex = 0;
            this.ddl_destinoPlaya1.SelectedIndex = 0;
        }
    }

    protected void ddl_destinoZona2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_destinoZona2.SelectedIndex > 0)
        {
            int id_zona = int.Parse(this.ddl_destinoZona2.SelectedValue);
            PlayaBC playa = new PlayaBC();
            utils.CargaDrop(this.ddl_destinoPlaya2, "ID", "DESCRIPCION", playa.ObtenerPlayasXCriterio(null, null, id_zona, false, "100"));
            if (this.ddl_destinoPlaya1.Items.Count > 1)
            {
                this.ddl_destinoPlaya2.Enabled = true;
                this.ddl_destinoPlaya2_SelectedIndexChanged(null, null);
            }
            else
            {
                this.ddl_destinoPlaya2.Enabled = false;
                this.ddl_destinoPos2.Enabled = false;
            }
        }
        else
        {
            this.ddl_destinoPos2.Enabled = false;
            this.ddl_destinoPlaya2.Enabled = false;
            this.ddl_destinoPos2.SelectedIndex = 0;
            this.ddl_destinoPlaya2.SelectedIndex = 0;
        }
    }

    protected void ddl_destinoPlaya1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(this.ddl_destinoPlaya1.SelectedIndex == 0) || (this.ddl_destinoZona1.SelectedIndex == 0))
        {
            int id_playa = int.Parse(this.ddl_destinoPlaya1.SelectedValue);
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds1 = yms.Obtenerlugares_playa(id_playa, null, "0");
            utils.CargaDrop(this.ddl_destinoPos1, "ID", "DESCRIPCION", ds1);
            if (this.ddl_destinoPos1.Items.Count > 1)
            {
                this.ddl_destinoPos1.Enabled = true;
            }
            else
            {
                this.ddl_destinoPos1.Enabled = false;
            }
        }
        else
        {
            this.ddl_destinoPos1.ClearSelection();
            this.ddl_destinoPos1.Enabled = false;
        }
    }

    protected void ddl_destinoPlaya2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(this.ddl_destinoPlaya2.SelectedIndex == 0) || (this.ddl_destinoZona2.SelectedIndex == 0))
        {
            int id_playa = int.Parse(this.ddl_destinoPlaya2.SelectedValue);
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds1 = yms.Obtenerlugares_playa(id_playa, null, "0");
            utils.CargaDrop(this.ddl_destinoPos2, "ID", "DESCRIPCION", ds1);
            if (this.ddl_destinoPos2.Items.Count > 1)
            {
                this.ddl_destinoPos2.Enabled = true;
            }
            else
            {
                this.ddl_destinoPos2.Enabled = false;
            }
        }
        else
        {
            this.ddl_destinoPos2.ClearSelection();
            this.ddl_destinoPos2.Enabled = false;
        }
    }

    #endregion

    #region Buttons

    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        if (this.ddl_destinoPos1.SelectedIndex > 0 && this.ddl_destinoPos2.SelectedIndex > 0)
        {
            string resultado;
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)this.Session["Usuario"];
            SolicitudBC solicitud = new SolicitudBC();
            TrailerUltEstadoBC traiue = new TrailerUltEstadoBC();
            int id = int.Parse(this.hf_trailerId.Value);
            traiue = traiue.CargaTrue(id);
            string fh = string.Format("{0} {1}", this.txt_buscarFecha.Text, this.txt_buscarHora.Text);
            solicitud.ID_TIPO = 3;
            solicitud.ID_USUARIO = usuario.ID;   // Variable de sesión
            solicitud.FECHA_CREACION = DateTime.Now;
            solicitud.FECHA_PLAN_ANDEN = DateTime.Parse(fh);
            solicitud.DOCUMENTO = traiue.DOC_INGRESO;
            solicitud.OBSERVACION = "";
            solicitud.ID_TRAILER = id;
            solicitud.ID_DESTINO = int.Parse(this.ddl_destinoPos1.SelectedValue);
            solicitud.ID_DESTINO_PALLET = int.Parse(this.ddl_destinoPos2.SelectedValue);
            solicitud.ID_SITE = int.Parse(this.dropsite.SelectedValue);

            if (solicitud.pallet(solicitud, out resultado) && resultado == "")
            {
                limpia(null, null);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("showAlert('{0}');", "Solicitud Creada Correctamente"), true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('" + resultado + "');", resultado), true);
        }
    }

    protected void limpia(object sender, EventArgs e)
    {
        this.drop_SelectedIndexChanged(null, null);
        this.hf_trailerId.Value = "";
        this.txt_nroFlota.Text = "";
        //this.txt_buscarPatente.Text = "";
        this.ddl_destinoZona1.ClearSelection();
        this.ddl_destinoZona2.ClearSelection();
        this.ddl_buscarPatente.ClearSelection();
        this.ddl_destinoZona1.Enabled = false;
        this.ddl_destinoZona2.Enabled = false;
        this.ddl_destinoZona1_SelectedIndexChanged(null, null);
        this.ddl_destinoZona2_SelectedIndexChanged(null, null);

        this.pnl_detalleLugar.Style.Add("background-color", "#ffffff");
        this.pnl_detalleLugar.Style.Add("color", "black");
        this.pnl_detalleTrailer.Attributes.Add("style", string.Format("background-color:{0};", "#ffffff"));
        this.pnl_imgAlerta.BackColor = System.Drawing.Color.White;
        this.img_trailer.ImageUrl = "";
        this.lbl_origenZona.Text = "";
        this.lbl_origenPlaya.Text = "";
        this.txt_transporte.Text = "";
        this.lbl_lugar.Text = "";
        TrailerBC t = new TrailerBC();
        utils.CargaDrop(this.ddl_buscarPatente, "ID", "PLACA", t.obtenerDisponiblesDrop(Convert.ToInt32(this.dropsite.SelectedValue), false, false));
 
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
    }

    #endregion

    #region UtilsPagina

    private void llenarForm()
    {
        int soli_id = int.Parse(this.Request.Params["soli_id"].ToString());
        this.hf_soliId.Value = soli_id.ToString();
        SolicitudBC solicitud = new SolicitudBC();
        SolicitudAndenesBC sa = new SolicitudAndenesBC();
        LugarBC l = new LugarBC();
        TrailerBC t = new TrailerBC();
        solicitud = solicitud.ObtenerXId(soli_id);
        sa = sa.ObtenerXId(soli_id,1);
        this.txt_buscarFecha.Text = solicitud.FECHA_CREACION.ToString("dd/MM/yyyy");
        this.txt_buscarHora.Text = solicitud.FECHA_CREACION.ToString("hh:mm");
        this.hf_trailerId.Value = solicitud.ID_TRAILER.ToString();
        t = t.obtenerXID(solicitud.ID_TRAILER);
        this.btnBuscar_Click(null, null);
        l = l.obtenerXID(sa.LUGA_ID);
        this.dropsite.SelectedValue = l.ID_SITE.ToString();
        this.drop_SelectedIndexChanged(null, null);
        this.ddl_destinoZona1.SelectedValue = l.ID_ZONA.ToString();
        this.ddl_destinoZona1_SelectedIndexChanged(null, null);
        this.ddl_destinoPlaya1.SelectedValue = l.ID_PLAYA.ToString();
        this.ddl_destinoPlaya1_SelectedIndexChanged(null, null);
        this.ddl_destinoPos1.SelectedValue = l.ID.ToString();
    }
    #endregion
}