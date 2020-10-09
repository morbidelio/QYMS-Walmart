using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Salida_Servicios_Externos : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    UsuarioBC usuario;
    CargaDrops drop = new CargaDrops();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("~/InicioQYMS.aspx");
        usuario = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            drop.Site_Normal(ddl_site,usuario.ID);
            Limpiar();
        }
    }

    #region Buttons
    protected void btn_guardar_Click(object sender, EventArgs e)
    {
        if (utils.validarRut(txt_conductorRut.Text) == false)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Rut del Conductor invalido');", true);
            txt_conductorRut.Text = "";
        }
        else
        {
            ServiciosExternosVehiculosBC se = new ServiciosExternosVehiculosBC();
            se.SEVE_ID = Convert.ToInt32(hf_id.Value);
            se.FH_SALIDA = Convert.ToDateTime(txt_fecha.Text + " " + txt_hora.Text);
            se.COSE_RUT = txt_conductorRut.Text;
            se.COSE_NOMBRE = txt_conductorNombre.Text;
            se.OBSERVACION = "";
            se.USUA_ID = usuario.ID;
            se.SEEX_ID = 0;
            if (se.Salida())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Salida correcta');", true);
                Limpiar();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Error');", true);
            }
        }
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ServiciosExternosVehiculosBC sev = new ServiciosExternosVehiculosBC();
        ServiciosExternosBC se = new ServiciosExternosBC();
        bool existe;
        sev = sev.ObtenerXPlaca(txt_placa.Text, out existe);
        sev.CONDUCTOR = sev.CONDUCTOR.ObtenerXId(sev.SEEX_ID);
        if (!existe)
        {
            if (new TrailerBC().obtenerXPlaca(txt_placa.Text).ID > 0)
            {
                txt_placa.Text = "";
                utils.ShowMessage2(this, "buscar", "warn_trailer");
                return;
            }
            utils.ShowMessage2(this, "buscar", "warn_vehiculoNoExiste");
            Limpiar();
            return;
        }
        if (sev.SITE_ID != Convert.ToInt32(ddl_site.SelectedValue))
        {
            utils.ShowMessage2(this, "buscar", "warn_vehiculoOtroSite");
            Limpiar();
            return;
        }
        if (!sev.SITE_IN)
        {
            utils.ShowMessage2(this, "buscar", "warn_vehiculoFueraSite");
            Limpiar();
            return;
        }
        ProveedorBC p = new ProveedorBC();
        p = p.ObtenerXId(sev.PROV_ID);
        hf_id.Value = sev.SEVE_ID.ToString();
        lbl_proveedor.Text = p.DESCRIPCION;
        lbl_placa.Text = sev.PLACA;
        txt_conductorRut.Text = sev.COSE_RUT;
        txt_conductorNombre.Text = sev.COSE_NOMBRE;
        lbl_servExt.Text = se.CODIGO;
        dv_contenido.Attributes.Add("style", "display:block");
        utils.ShowMessage2(this, "buscar", "success");
    }
    #endregion
    #region TextBox
    protected void txt_rutCond_TextChanged(object sender, EventArgs e)
    {
        if (!chk_conductorExtranjero.Checked && !utils.validarRut(txt_conductorRut.Text))
        {
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            utils.ShowMessage2(this, "conductor", "warn_rutNovalido");
            return;
        }
        ConductorBC c = new ConductorBC(txt_conductorRut.Text);

        if (c.ID == 0)
        {
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            utils.ShowMessage2(this, "conductor", "success_conductorNoexiste");
            return;
        }
        if (c.BLOQUEADO)
        {
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            utils.ShowMessage2(this, "conductor", "warn_conductorBloqueado");
            return;
        }
        txt_conductorNombre.Text = c.NOMBRE;
        chk_conductorExtranjero.Checked = c.COND_EXTRANJERO;
        utils.ShowMessage2(this, "conductor", "success");
    }
    #endregion
    #region UtilsPagina
    private void Limpiar()
    {
        hf_id.Value = "";
        txt_fecha.Text = DateTime.Now.ToShortDateString();
        txt_hora.Text = DateTime.Now.ToShortTimeString();
        lbl_proveedor.Text = "";
        lbl_placa.Text = "";
        txt_conductorRut.Text = "";
        txt_conductorNombre.Text = "";
        dv_contenido.Attributes.Add("style", "display:none");
        txt_placa.Text = "";
    }
    #endregion

    protected void chk_conductorExtranjero_CheckedChanged(object sender, EventArgs e)
    {
        if (!chk_conductorExtranjero.Checked && !utils.validarRut(txt_conductorRut.Text) && !string.IsNullOrEmpty(txt_conductorRut.Text))
        {
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            txt_conductorNombre.Enabled = false;
            utils.ShowMessage2(this, "conductor", "warn_rutNovalido");
            return;
        }
    }
}