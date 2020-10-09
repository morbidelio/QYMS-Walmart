// Example header text. Can be configured in the options.
using System;
using System.IO;
using System.Linq;
using System.Web.UI;

public partial class App_Entrada_Servicios_Externos : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    UsuarioBC usuario = new UsuarioBC();
    CargaDrops drop = new CargaDrops();

    protected void txt_rutCond_TextChanged(object sender, EventArgs e)
    {
        if (!chk_conductorExtranjero.Checked && !utils.validarRut(txt_conductorRut.Text))
        {
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            utils.ShowMessage2(this, "conductor", "warn_rutNovalido");
            return;
        }
        ServiciosExternosConductorBC c = new ServiciosExternosConductorBC();
        c = c.ObtenerXRut(utils.formatearRut(txt_conductorRut.Text));

        if (c.ID == 0)
        {
            utils.ShowMessage2(this, "conductor", "warn_conductorNoexiste");
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
        utils.ShowMessage2(this, "conductor", "success");
    }
    protected void btn_guardar_Click(object sender, EventArgs e)
    {
        ServiciosExternosVehiculosBC se = new ServiciosExternosVehiculosBC();
        se.PLACA = txt_placa.Text;
        se.SITE_ID = Convert.ToInt32(ddl_site.SelectedValue);
        se.FH_INGRESO = Convert.ToDateTime(string.Format("{0} {1}", txt_fecha.Text, txt_hora.Text));
        se.CONDUCTOR.NOMBRE = txt_conductorNombre.Text;
        se.CONDUCTOR.RUT = txt_conductorRut.Text;
        se.CONDUCTOR.COSE_EXTRANJERO = chk_conductorExtranjero.Checked;
        se.SEEX_ID = Convert.ToInt32(ddl_servExt.SelectedValue);
        se.PROV_ID = Convert.ToInt32(ddl_prov.SelectedValue);
        se.OBSERVACION = txt_obs.Text;
        se.USUA_ID = usuario.ID;
        if (se.Entrada())
        {
            utils.ShowMessage2(this, "confirmar", "success");
            Limpiar();
        }
        else
        {
            utils.ShowMessage2(this, "confirmar", "error");
        }
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ServiciosExternosVehiculosBC se = new ServiciosExternosVehiculosBC();
        bool existe;
        se = se.ObtenerXPlaca(this.txt_placa.Text, out existe);
        if (existe)
        {
            if (se.SITE_IN)
            {
                txt_placa.Text = "";
                utils.ShowMessage2(this, "buscar", "warn_vehiculoIngresado");
                return;
            }
            se.CONDUCTOR = se.CONDUCTOR.ObtenerXId(se.SEEX_ID);
            txt_conductorRut.Text = se.CONDUCTOR.RUT;
            txt_conductorNombre.Text = se.CONDUCTOR.NOMBRE;
            chk_conductorExtranjero.Checked = se.CONDUCTOR.COSE_EXTRANJERO;
            ddl_prov.SelectedValue = se.PROV_ID.ToString();
            ddl_servExt.SelectedValue = se.SEEX_ID.ToString();
            ddl_prov.Enabled = false;
            ddl_servExt.Enabled = true;
        }
        else
        {
            if (new TrailerBC().obtenerXPlaca(txt_placa.Text).ID > 0)
            {
                txt_placa.Text = "";
                utils.ShowMessage2(this, "buscar", "warn_trailer");
                return;
            }
            ddl_prov.ClearSelection();
            ddl_prov.Enabled = false;
            ddl_prov.SelectedValue = "809";
            ddl_servExt.Enabled = true;
            utils.ShowMessage2(this, "buscar", "warn_noExiste");
        }
        btn_guardar.Enabled = true;
        ddl_servExt.Enabled = true;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["usuario"] == null)
        {
            this.Response.Redirect("~/InicioQYMS.aspx");
        }

        this.usuario = (UsuarioBC)this.Session["usuario"];
        if (!this.IsPostBack)
        {
            this.drop.Site_Normal(this.ddl_site, this.usuario.ID);
            this.drop.Proveedor(this.ddl_prov);
            ServiciosExternosBC se = new ServiciosExternosBC();
            utils.CargaDrop(this.ddl_servExt, "SEEX_ID", "CODIGO", se.ObtenerTodos());
            this.Limpiar();
        }
    }
    private void Limpiar()
    {
        txt_fecha.Text = DateTime.Now.ToShortDateString();
        txt_hora.Text = DateTime.Now.ToShortTimeString();
        txt_placa.Text = "";
        txt_conductorNombre.Text = "";
        txt_conductorRut.Text = "";
        ddl_servExt.ClearSelection();
        ddl_servExt.Enabled = false;
        ddl_prov.ClearSelection();
        ddl_prov.SelectedValue = "809";
        ddl_prov.Visible = false;
        txt_obs.Text = "";
        ddl_servExt.ClearSelection();
    }
    // viewstate
    protected override void SavePageStateToPersistenceMedium(object state)
    {
        string file = this.GenerateFileName();

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
            StreamReader reader = new StreamReader(this.GenerateFileName());

            LosFormatter formator = new LosFormatter();

            state = formator.Deserialize(reader);

            reader.Close();
        }
        catch (Exception)
        {
            this.Response.Redirect(string.Format("{0}.aspx", Path.GetFileNameWithoutExtension(this.Page.AppRelativeVirtualPath)));
        }
        return state;
    }

    private string GenerateFileName()
    {
        string pageName = Path.GetFileNameWithoutExtension(this.Page.AppRelativeVirtualPath);

        string file = string.Format("{0}{1}.txt", pageName, this.Session.SessionID.ToString());

        //       file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);  
        file = string.Format("{0}\\{1}", utils.pathviewstate(), file);

        return file;
    }

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