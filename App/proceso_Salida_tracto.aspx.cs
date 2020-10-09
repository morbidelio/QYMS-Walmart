using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Salida_tracto : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    UsuarioBC usuario = new UsuarioBC();
    CargaDrops drop = new CargaDrops();

    protected void txt_rutConductor_TextChanged(object sender, EventArgs e)
    {
        if (!chk_conductorExtranjero.Checked && !utils.validarRut(txt_conductorRut.Text))
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            lbl_nombreConductor.Text = "";
            utils.ShowMessage2(this, "conductor", "warn_rutNovalido");
            return;
        }
        ConductorBC c = new ConductorBC();
        c = c.ObtenerXRut(txt_conductorRut.Text);

        if (c.ID == 0)
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            lbl_nombreConductor.Text = "";
            utils.ShowMessage2(this, "conductor", "success_conductorNoexiste");
            return;
        }
        if (c.BLOQUEADO)
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            lbl_nombreConductor.Text = "";
            utils.ShowMessage2(this, "conductor", "warn_conductorBloqueado");
            return;
        }
        lbl_nombreConductor.Text = c.NOMBRE;
        hf_idCond.Value = c.ID.ToString();
        utils.ShowMessage2(this, "conductor", "success");
    }
    protected void btn_guardar_Click(object sender, EventArgs e)
    {
        TractoBC t = new TractoBC();
        t = t.ObtenerXPatente(txt_placa.Text);
        t.ID = Convert.ToInt32(hf_id.Value);
        t.FH_SALIDA = Convert.ToDateTime(txt_fecha.Text + " " + txt_hora.Text);
        t.COND_ID_SALIDA = Convert.ToInt32(hf_idCond.Value);
        t.OBSERVACION = "";
        t.USUA_ID = usuario.ID;
        t.SITE_ID = Convert.ToInt32(ddl_site.SelectedValue);
        string error;
        if (t.Salida(out error))
        {
            utils.ShowMessage2(this, "guardar", "success");
            Limpiar();
        }
        else
        {
            utils.ShowMessage(this, error, "error", false);
        }
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        TractoBC trai = new TractoBC();
        trai = trai.ObtenerXPatente(txt_placa.Text);
        if (trai.ID == 0)
        {
            utils.ShowMessage2(this, "tracto", "warn_noExiste");
            Limpiar();
            return;
        }
        if (trai.SITE_ID != Convert.ToInt32(ddl_site.SelectedValue))
        {
            utils.ShowMessage2(this, "tracto", "warn_otroSite");
            Limpiar();
            return;
        }
        if (!trai.SITE_IN)
        {
            utils.ShowMessage2(this, "tracto", "warn_fueraSite");
            Limpiar();
            return;
        }
        if (trai.COND_ID_INGRESO != 0)
        {
            ConductorBC c = new ConductorBC().ObtenerXId(trai.COND_ID_INGRESO);
            hf_idCond.Value = c.ID.ToString();
            lbl_nombreConductor.Text = c.NOMBRE;
            txt_conductorRut.Text = c.RUT;
            chk_conductorExtranjero.Checked = c.COND_EXTRANJERO;
        }
        else
        {
            hf_idCond.Value = "";
            lbl_nombreConductor.Text = "";
            txt_conductorRut.Text = "";
            chk_conductorExtranjero.Checked = false;
        }
        TransportistaBC tran = new TransportistaBC().ObtenerXId(trai.TRAN_ID);
        hf_id.Value = trai.ID.ToString();
        lbl_transportista.Text = tran.NOMBRE;
        lbl_placa.Text = trai.PATENTE;
        trai.USUA_ID_CREACION = usuario.ID;
        dv_contenido.Attributes.Add("style", "display:block");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("~/InicioQYMS.aspx");
        usuario = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            drop.Site_Normal(ddl_site, usuario.ID);
            Limpiar();
        }
    }
    private void Limpiar()
    {
        hf_id.Value = "";
        txt_fecha.Text = DateTime.Now.ToShortDateString();
        txt_hora.Text = DateTime.Now.ToShortTimeString();
        lbl_placa.Text = "";
        txt_placa.Text = "";
        dv_contenido.Attributes.Add("style", "display:none");
    }

    protected void chk_conductorExtranjero_CheckedChanged(object sender, EventArgs e)
    {
        if (!chk_conductorExtranjero.Checked && !utils.validarRut(txt_conductorRut.Text) && !string.IsNullOrEmpty(txt_conductorRut.Text))
        {
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            lbl_nombreConductor.Text = "";
            utils.ShowMessage2(this, "conductor", "warn_rutNovalido");
            return;
        }
    }
}