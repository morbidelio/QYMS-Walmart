// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;

public partial class App_Entrada_Tracto : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    UsuarioBC usuario = new UsuarioBC();
    CargaDrops d = new CargaDrops();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("../InicioQYMS.aspx");
        }
        usuario = (UsuarioBC)this.Session["usuario"];
        if (!IsPostBack)
        {
            txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
            txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
            TransportistaBC tran = new TransportistaBC();

            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
            utils.CargaDropNormal(dropsite, "ID", "NOMBRE", ds);
            utils.CargaDrop(ddl_transportista, "ID", "NOMBRE", tran.ObtenerTodos());
        }
    }
    #region Buttons
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        // TODO: Implement this method
        if (txt_buscarPatente.Text != "")
        {
            TractoBC tracto = new TractoBC();
            tracto = tracto.ObtenerXPatente(this.txt_buscarPatente.Text);
            if (tracto.ID == 0)
            {
                if (new TrailerBC().obtenerXPlaca(this.txt_buscarPatente.Text).ID > 0)
                {
                    btn_confirmar.Enabled = false;
                    ddl_transportista.Enabled = false;
                    ddl_transportista.ClearSelection();
                    txt_conductorRut.Enabled = false;
                    txt_conductorRut.Text = "";
                    txt_acomRut.Enabled = false;
                    txt_acomRut.Text = "";
                    chk_conductorExtranjero.Checked = false;
                    chk_conductorExtranjero.Enabled = false;
                    utils.ShowMessage2(this, "tracto", "warn_trailer");
                    return;
                }
                ddl_transportista.ClearSelection();
                ddl_transportista.Enabled = true;
                utils.ShowMessage2(this, "tracto", "success_noExiste");
            }
            else
            {
                ddl_transportista.SelectedValue = tracto.TRAN_ID.ToString();
                ddl_transportista.Enabled = false;
                utils.ShowMessage2(this, "tracto", "success");
            }
            txt_conductorRut.Enabled = true;
            txt_conductorRut.Text = "";
            txt_acomRut.Enabled = true;
            txt_acomRut.Text = "";
            btn_confirmar.Enabled = true;
            chk_conductorExtranjero.Checked = false;
            chk_conductorExtranjero.Enabled = true;
            rb_pos_CheckedChanged(null, null);
        }
    }
    protected void btn_limpiar_click(object sender, EventArgs e)
    {
        this.limpiarTodo();
    }
    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(hf_idCond.Value))
            {
                ConductorBC c = new ConductorBC();
                c.RUT = txt_conductorRut.Text;
                c.NOMBRE = txt_conductorNombre.Text;
                c.TRAN_ID = Convert.ToInt32(ddl_transportista.SelectedValue);
                c.COND_EXTRANJERO = chk_conductorExtranjero.Checked;
                hf_idCond.Value = c.AgregarIdentity().ToString();
            }
            MovimientoBC mov = new MovimientoBC();
            TrailerUltEstadoBC trailerUE = new TrailerUltEstadoBC();
            TrailerBC trailer = new TrailerBC();

            mov.FECHA_CREACION = DateTime.Now;
            mov.ID_ESTADO = 10;
            mov.OBSERVACION = txt_obs.Text;
            DateTime fh = DateTime.Parse(string.Format("{0} {1}", txt_ingresoFecha.Text, txt_ingresoHora.Text));
            mov.FECHA_ORIGEN = fh;
            mov.ID_DESTINO = Convert.ToInt32(this.ddl_posicion.SelectedValue);
            mov.FECHA_DESTINO = fh.AddMinutes(30);

            mov.PATENTE_TRACTO = txt_buscarPatente.Text;

            mov.MANT_EXTERNO = false;
            mov.ID_TRAILER = 0;
            trailer.ID = 0;
            trailer.PLACA = "";

            trailer.TRAN_ID = Convert.ToInt32(ddl_transportista.SelectedValue);

            trailerUE.SITE_ID = Convert.ToInt32(dropsite.SelectedValue); // 1; // Cambiar después de introducir variables de sesión
            trailerUE.CHOFER_RUT = utils.formatearRut(txt_conductorRut.Text);
            trailerUE.CHOFER_NOMBRE = txt_conductorNombre.Text;
            trailerUE.ACOMP_RUT = txt_acomRut.Text;

            trailerUE.COND_ID = Convert.ToInt32(hf_idCond.Value);

            UsuarioBC usuario = (UsuarioBC)Session["USUARIO"];
            string resultado;
            bool ejecucion = mov.ProcesoEntrada(mov, trailerUE, trailer, usuario.ID, out resultado);
            if (ejecucion && resultado == "")
            {
                utils.ShowMessage2(this, "confirmar", "success");
                limpiarTodo();
            }
            else
            {
                utils.ShowMessage(this, resultado, "error", false);
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
    }
    #endregion
    #region DropDownList
    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        d.Zona(this.ddl_zona, Convert.ToInt32(this.dropsite.SelectedValue));
        ddl_zona_SelectedIndexChanged(null, null);
    }
    protected void ddl_zona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_zona.SelectedIndex != 0)
        {
            int id_zona = Convert.ToInt32(this.ddl_zona.SelectedValue);
            PlayaBC playa = new PlayaBC();
            this.d.Playa(this.ddl_playa, id_zona);
            ddl_playa.Enabled = true;
            ddl_playa_SelectedIndexChanged(null, null);
        }
        else
        {
            ddl_posicion.Enabled = false;
            ddl_playa.Enabled = false;
            ddl_posicion.ClearSelection();
            ddl_playa.ClearSelection();
        }
    }
    protected void ddl_playa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((this.ddl_playa.SelectedIndex != 0) && (this.ddl_zona.SelectedIndex != 0))
        {
            int id_playa = Convert.ToInt32(this.ddl_playa.SelectedValue);
            d.Lugar1(this.ddl_posicion, 0, id_playa, 0, 1);
            ddl_posicion.Enabled = true;
        }
        else
        {
            ddl_posicion.ClearSelection();
            ddl_posicion.Enabled = false;
        }
    }
    #endregion
    #region TextBox
    protected void txt_conductorRut_TextChanged(object sender, EventArgs e)
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
        txt_acomRut.Focus();
        utils.ShowMessage2(this, "conductor", "success");
    }
    #endregion
    #region RadioButton
    protected void rb_pos_CheckedChanged(object sender, EventArgs e)
    {
        this.ddl_zona.Enabled = true;
        if (ddl_playa.SelectedIndex > 0)
            ddl_playa.Enabled = true;
        if (ddl_posicion.SelectedIndex > 0)
            ddl_posicion.Enabled = true;
    }
    #endregion
    #region UtilsPagina
    private void limpiarTodo()
    {
        hf_idCond.Value = "";
        ddl_transportista.ClearSelection();
        txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
        txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
        txt_buscarPatente.Text = "";
        txt_conductorNombre.Text = "";
        txt_conductorRut.Text = "";
        txt_acomRut.Text = "";
        txt_buscarPatente.Text = "";
        txt_conductorNombre.Enabled = false;
        txt_conductorRut.Enabled = false;
        txt_acomRut.Enabled = false;
        txt_buscarPatente.Enabled = true;
        txt_obs.Text = "";
        ddl_transportista.Enabled = false;
        chk_conductorExtranjero.Enabled = false;
        chk_conductorExtranjero.Checked = false;
    }
    #endregion
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
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            txt_conductorNombre.Enabled = false;
            utils.ShowMessage2(this, "conductor", "warn_rutNovalido");
            return;
        }
    }
}