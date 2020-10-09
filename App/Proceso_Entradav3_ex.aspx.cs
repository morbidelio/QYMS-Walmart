// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.IO;

public partial class App_Proceso_EntradaV3ex : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    UsuarioBC usuario = new UsuarioBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("../InicioQYMS.aspx");
        usuario = (UsuarioBC)Session["usuario"];
        this.nuevo_trailer.ButtonClickDemo += new EventHandler(trailercreado);
        if (!this.IsPostBack)
        {
            this.txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
            this.txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
            TransportistaBC tran = new TransportistaBC();
            ProveedorBC proveedor = new ProveedorBC();

            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
            utils.CargaDropNormal(this.dropsite, "ID", "NOMBRE", ds);
            this.drop_SelectedIndexChanged(null, null);

            utils.CargaDrop(ddl_proveedor, "ID", "DESCRIPCION", proveedor.obtenerTodo());
            utils.CargaDrop(ddl_transportista, "ID", "NOMBRE", tran.ObtenerTodos());
            utils.CargaDrop(ddl_tipo_carga, "ID", "DESCRIPCION", yms.obtenerTipoCarga(null, false, false));
        }
    }
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

    #region TextBox
    protected void txt_conductorRut_TextChanged(object sender, EventArgs e)
    {
        if (!utils.validarRut(txt_conductorRut.Text) && !chk_conductorExtranjero.Checked)
        {
            txt_conductorNombre.Enabled = false;
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            utils.ShowMessage2(this, "buscarConductor", "warn_rutNovalido");
            return;
        }
        ConductorBC c = new ConductorBC().ObtenerXRut(this.txt_conductorRut.Text);
        if (c.ID == 0 || !c.ACTIVO)
        {
            txt_conductorNombre.Enabled = true;
            hf_idCond.Value = "";
            utils.ShowMessage2(this, "conductor", "warn_conductorNoexiste");
            return;
        }
        if (c.BLOQUEADO)
        {
            txt_conductorNombre.Enabled = false;
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            utils.ShowMessage2(this, "conductor", "warn_conductorBloqueado");
            return;
        }
        hf_idCond.Value = c.ID.ToString();
        txt_conductorNombre.Text = c.NOMBRE;
        txt_conductorNombre.Enabled = false;
        utils.ShowMessage2(this, "conductor", "success");
    }
    protected void txt_editPlaca_TextChanged(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        t = t.obtenerXPlaca(txt_editPlaca.Text);
        if (t.ID != 0 && t.ID != null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer ya existe en la base de datos!');", true);
            txt_editPlaca.Text = "";
            txt_editPlaca.Focus();
        }
    }
    protected void txt_buscarDoc_TextChanged(object sender, EventArgs e)
    {
        btnBuscarTrailer_Click(sender, e);
    }
    #endregion
    #region DropDownList
    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        string tipo_zona;
        if (this.rb_ingresoCargado.Checked == true)
        {
            tipo_zona = "200";
        }
        else
        {
            tipo_zona = "100";
        }

        utils.CargaDrop(this.ddl_zona, "ID", "DESCRIPCION", yms.ObtenerZonas(int.Parse(this.dropsite.SelectedValue), "", tipo_zona));
        this.ddl_zona_SelectedIndexChanged(null, null);
        if (!string.IsNullOrEmpty(this.hf_idTrailer.Value))

            if (txt_buscarPatente.Text != "" && txt_buscarPatente.Text != null)
                this.btnBuscarTrailer_Click(null, null);
    }
    protected void ddl_zona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_zona.SelectedIndex != 0)
        {
            int id_zona = int.Parse(this.ddl_zona.SelectedValue);
            PlayaBC playa = new PlayaBC();
            utils.CargaDrop(this.ddl_playa, "ID", "DESCRIPCION", playa.ObtenerPlayasXCriterio(null, null, id_zona, false, "200"));
            ddl_playa_SelectedIndexChanged(null, null);
            ddl_playa.Enabled = true;
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
            int id_playa = int.Parse(this.ddl_playa.SelectedValue);
            LugarBC lugar = new LugarBC();
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds1 = yms.Obtenerlugares_playa(id_playa, null, "0");
            utils.CargaDrop(this.ddl_posicion, "ID", "DESCRIPCION", ds1);
            this.ddl_posicion.Enabled = true;
        }
        else
        {
            this.ddl_posicion.ClearSelection();
            this.ddl_posicion.Enabled = false;
        }
    }
    protected void tipo_carga_SelectedIndexChanged(object sender, EventArgs e)
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        rb_posAuto.Checked = false;
        rb_posManual.Checked = true;

        if (ddl_tipo_carga.SelectedValue == "0")
        {
            ddl_motivo.Items.Clear();
            ddl_motivo.Enabled = false;
        }
        else
        {
            utils.CargaDrop(this.ddl_motivo, "ID", "DESCRIPCION", yms.obtenerMotivoTipoCarga(ddl_tipo_carga.SelectedValue, null));

            if (ddl_motivo.Items.Count > 1)
                ddl_motivo.Enabled = true;
            else
                ddl_motivo.Enabled = false;
        }
    }
    #endregion
    #region Buttons
    protected void btn_Conf_Click(object sender, EventArgs e)
    {
        //UpdatePanel4.Update();
        txt_editPlaca.Text = txt_buscarPatente.Text;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalTrailer();", true);
    }
    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        t.PLACA = txt_editPlaca.Text;
        t.TRAN_ID = int.Parse(ddl_editTran.SelectedValue);
        if (t.CrearGenerico(t, extranjero.Checked))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Trailer agregado correctamente.');", true);
            this.hf_idTrailer.Value = t.ID.ToString();
            this.txt_buscarPatente.Text = t.PLACA;
            this.ddl_transportista.SelectedValue = t.TRAN_ID.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "$('#modalTrailer').modal('hide');", true);
        }
    }
    protected void btnBuscarTrailer_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        ddl_playa.Enabled = false;
        ddl_zona.Enabled = false;
        ddl_posicion.Enabled = false;

        trailer = trailer.obtenerXDoc(this.txt_buscarDoc.Text, dropsite.SelectedValue);
        if (trailer.ID == 0) //Trailer nuevo, no existe
        {
            limpiarTodo();
            utils.ShowMessage2(this, "cita", "warn_noExiste");
            return;
        }
        ddl_zona.Enabled = true;
        hf_idTrailer.Value = trailer.ID.ToString();
        hf_pring_id.Value = trailer.PRING_ID.ToString();
        txt_buscarPatente.Text = trailer.PLACA;
        ddl_transportista.SelectedValue = trailer.TRAN_ID.ToString();
        rb_propio.Checked = !trailer.EXTERNO;
        rb_externo.Checked = trailer.EXTERNO;
        txt_traExtPat.Enabled = false;
        ddl_transportista.Enabled = false;
        utils.ShowMessage2(this, "cita", "success");
        txt_traExtPat.Enabled = rb_ingresoCargado.Checked;
        txt_idSello.Enabled = rb_ingresoCargado.Checked;
        txt_conductorRut.Enabled = true;
        chk_conductorExtranjero.Enabled = true;
        txt_conductorNombre.Enabled = true;
        txt_acomRut.Enabled = true;
        PreEntradaBC p = new PreEntradaBC();
        p = p.CargarPreEntrada(trailer.ID, int.Parse(dropsite.SelectedValue), txt_buscarDoc.Text);

        if (p.ID != 0)
        {
            int hours = (p.FECHA_HORA - DateTime.Now).Hours;
            if (p.COND_ID != 0)
            {
                ConductorBC c = new ConductorBC();
                c = c.ObtenerXId(p.COND_ID);
                hf_idCond.Value = c.ID.ToString();
                if (p.extranjero == false) txt_conductorRut.Text = utils.formatearRut(c.RUT);
                else txt_conductorRut.Text = c.RUT;

                txt_conductorNombre.Text = c.NOMBRE;
            }
            hf_pring_id.Value = p.ID.ToString();
            txt_traExtPat.Text = p.PATENTE_TRACTO;
            txt_acomRut.Text = p.RUT_ACOMP;

            ddl_proveedor.SelectedValue = p.PROV_ID.ToString();
            hf_idTrailer.Value = p.TRAI_ID.ToString();
            DateTime fechaHora = DateTime.Parse(txt_ingresoFecha.Text + " " + txt_ingresoHora.Text);
            if (fechaHora > p.FECHA_HORA.AddHours(2) || fechaHora < p.FECHA_HORA.AddHours(-2))
                utils.ShowMessage2(this, "cita", "warn_fhDiferente");

            if (!string.IsNullOrEmpty(p.SELLO_INGRESO))
                txt_idSello.Text = p.SELLO_INGRESO;
            if (!string.IsNullOrEmpty(p.SELLO_CARGA))
                txt_idSello.Text = p.SELLO_CARGA;
            rb_ingresoCargado.Checked = p.CARGADO;
            rb_ingresoVacio.Checked = !p.CARGADO;
            txt_obs.Text = p.Observacion;
            chk_ingresoCargado_CheckedChanged(null, null);
            ddl_tipo_carga.SelectedValue = p.TIIC_ID.ToString();
            tipo_carga_SelectedIndexChanged(null, null);
            ddl_motivo.SelectedValue = p.MOIC_ID.ToString();
        }
        else
        {
            utils.ShowMessage2(this, "cita", "warn_expirado");
            limpiarTodo();
        }
        rb_posAuto.Checked = false;
        rb_posManual.Checked = false;
        ddl_zona.Enabled = false;
        if (txt_conductorRut.Text != "") txt_conductorRut_TextChanged(null, null);
    }
    protected void btn_limpiar_Click(object sender, EventArgs e)
    {
        limpiarTodo();
    }
    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ddl_posicion.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje2", "alert('Debe seleccionar posicion Destino');", true);
            }
            else if (this.hf_idTrailer.Value == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje2", "alert('Debe ingresar Trailer');", true);
                limpiarTodo();
            }
            else if (this.txt_conductorRut.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje2", "alert('Debe ingresar Conductor');", true);
            }

            else
            {
                ConductorBC c = new ConductorBC();
                if (string.IsNullOrEmpty(hf_idCond.Value))
                {
                    if (extranjero.Checked == false) c.RUT = utils.formatearRut(txt_conductorRut.Text);
                    else c.RUT = txt_conductorRut.Text;

                    c.NOMBRE = txt_conductorNombre.Text;
                    c.TRAN_ID = int.Parse(ddl_transportista.SelectedValue);
                    hf_idCond.Value = c.AgregarIdentity().ToString();
                }
                MovimientoBC mov = new MovimientoBC();
                TrailerUltEstadoBC trailerUE = new TrailerUltEstadoBC();
                TrailerBC trailer = new TrailerBC();

                mov.FECHA_CREACION = DateTime.Now;

                mov.ID_ESTADO = 10;

                mov.OBSERVACION = this.txt_obs.Text;

                DateTime fh = DateTime.Parse(string.Format("{0} {1}", this.txt_ingresoFecha.Text, this.txt_ingresoHora.Text));

                mov.FECHA_ORIGEN = fh;

                mov.ID_DESTINO = int.Parse(this.ddl_posicion.SelectedValue);
                mov.FECHA_DESTINO = fh.AddMinutes(30);

                mov.PATENTE_TRACTO = this.txt_traExtPat.Text;

                trailerUE.COND_ID = int.Parse(hf_idCond.Value);
                mov.MANT_EXTERNO = false;
                mov.ID_TRAILER = int.Parse(this.hf_idTrailer.Value);
                trailer.ID = int.Parse(this.hf_idTrailer.Value);
                trailer.PLACA = this.txt_buscarPatente.Text;
                trailer.CODIGO = string.Format("{0}_{1}", this.ddl_transportista.SelectedItem.Text, this.txt_buscarPatente.Text);
                if (rb_externo.Checked)
                {
                    trailer.EXTERNO = true;
                }
                else
                {
                    trailer.EXTERNO = false;
                }
                trailer.TRAN_ID = int.Parse(this.ddl_transportista.SelectedValue);

                trailerUE.SITE_ID = Convert.ToInt32(this.dropsite.SelectedValue); // 1; // Cambiar después de introducir variables de sesión

                if (extranjero.Checked == false) trailerUE.CHOFER_RUT = utils.formatearRut(this.txt_conductorRut.Text);
                else trailerUE.CHOFER_RUT = this.txt_conductorRut.Text;
                trailerUE.CHOFER_NOMBRE = this.txt_conductorNombre.Text;
                trailerUE.ACOMP_RUT = this.txt_acomRut.Text;
                trailerUE.PROV_ID = int.Parse(this.ddl_proveedor.SelectedValue);
                trailerUE.DOC_INGRESO = this.txt_buscarDoc.Text;
                trailerUE.SELLO_INGRESO = this.txt_idSello.Text;
                trailerUE.TIPO_INGRESO_CARGA = this.ddl_tipo_carga.SelectedValue;
                trailerUE.motivo_TIPO_INGRESO_CARGA = this.ddl_motivo.SelectedValue;
                PreEntradaBC p = new PreEntradaBC();
                trailerUE.pring_id = hf_pring_id.Value.ToString(); // p.CargarPreEntrada(  //p.CargarPreEntrada(mov.ID_TRAILER, int.Parse(dropsite.SelectedValue)).ID.ToString();
                if (this.rb_ingresoCargado.Checked) //Trailer cargado: Entrada a destino
                {
                    trailerUE.CARGADO = true;
                }
                else //Trailer sin carga: Entrada a origen
                {
                    trailerUE.CARGADO = false;
                }
                UsuarioBC usuario = (UsuarioBC)Session["USUARIO"];

                if (trailer.ID == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe ingresar Trailer);", true);
                    limpiarTodo();
                }
                else
                {
                    string resultado;
                    bool ejecucion = mov.ProcesoEntrada(mov, trailerUE, trailer, usuario.ID, out resultado);
                    if (ejecucion && resultado == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje8", "showAlert('Ingreso correcto.');", true);
                        limpiarTodo();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + resultado + "');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error! No se pudo ingresar los datos.');", true);
        }
    }
    protected void btn_nuevoTrailer_Click(object sender, EventArgs e)
    {
        nuevo_trailer.limpiarForm();
        nuevo_trailer.setplaca(txt_buscarPatente.Text, extranjero.Checked);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTrailer();", true);
    }
    #endregion
    #region CheckBox
    protected void rb_pos_CheckedChanged(object sender, EventArgs e)
    {
        if (hf_idTrailer.Value != "")
        {
            if (rb_posAuto.Checked)
            {
                CargaTipoBC ct = new CargaTipoBC();
                if (ct.CargaDestinos(int.Parse(this.ddl_tipo_carga.SelectedValue)).Rows.Count == 0)
                {
                    LugarBC l = new LugarBC();
                    ddl_zona.Enabled = false;
                    ddl_playa.Enabled = false;
                    ddl_posicion.Enabled = false;
                    l = l.obtenerLugarAuto(int.Parse(dropsite.SelectedValue), usuario.ID, null);
                    if (l.ID == 0 || l.ID == null)
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Error. Intente designar un lugar manualmente.');", true);
                    else
                    {

                        PlayaBC p = new PlayaBC();
                        ZonaBC zona = new ZonaBC();
                        try
                        {
                            ddl_zona.SelectedValue = l.ID_ZONA.ToString();

                        }
                        catch (Exception ex)
                        {
                            zona = zona.ObtenerXId(l.ID_ZONA);
                            ddl_zona.Items.Add(new System.Web.UI.WebControls.ListItem(zona.DESCRIPCION, zona.ID.ToString()));
                            ddl_zona.SelectedValue = zona.ID.ToString();
                        }


                        utils.CargaDrop(this.ddl_playa, "ID", "DESCRIPCION", p.ObtenerXZona(l.ID_ZONA));
                        ddl_playa.SelectedValue = l.ID_PLAYA.ToString();
                        LugarBC lugar = new LugarBC();
                        CargaDrops drops = new CargaDrops();
                        drops.Lugar1(ddl_posicion, 0, l.ID_PLAYA, 0, 1);
                        //utils.CargaDrop(this.ddl_posicion, "ID", "DESCRIPCION", lugar.ObtenerXPlaya(l.ID_PLAYA));
                        ddl_posicion.SelectedValue = l.ID.ToString();
                    }
                }
            }
            else
            {
                ddl_zona.Enabled = true;
                if (ddl_playa.SelectedIndex > 0)
                    ddl_playa.Enabled = true;
                if (ddl_posicion.SelectedIndex > 0)
                    ddl_posicion.Enabled = true;
            }
        }
    }
    protected void chk_ingresoCargado_CheckedChanged(object sender, EventArgs e)
    {
        rb_posAuto.Checked = false;
        rb_posManual.Checked = true;
        ddl_motivo.Enabled = false;
        if (!String.IsNullOrEmpty(hf_idTrailer.Value) && hf_idTrailer.Value != "0")
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            string tipo_zona;
            if (this.rb_ingresoCargado.Checked == true)
            {
                tipo_zona = "200";
            }
            else
            {
                tipo_zona = "100";
            }
            tipo_carga_SelectedIndexChanged(null, null);

            utils.CargaDrop(this.ddl_zona, "ID", "DESCRIPCION", yms.ObtenerZonas(int.Parse(this.dropsite.SelectedValue), "", tipo_zona));

            ddl_zona_SelectedIndexChanged(null, null);
            if (rb_ingresoCargado.Checked)
            {
                txt_traExtPat.Enabled = true;
                txt_idSello.Enabled = true;
                rb_posAuto.Enabled = true;
            }
            else
            {
                txt_traExtPat.Enabled = false;
                txt_idSello.Enabled = false;
                rb_posAuto.Enabled = false;
            }
        }
    }
    protected void refrescarpos(object sender, EventArgs e)
    {
        if (rb_posAuto.Checked == true)
        {
            rb_pos_CheckedChanged(sender, e);
        }
        else
        {
            ddl_playa_SelectedIndexChanged(sender, e);
        }
    }
    #endregion
    #region UtilsPagina
    private void limpiarTodo()
    {
        hf_idTrailer.Value = "";
        hf_pring_id.Value = "";
        this.ddl_transportista.ClearSelection();
        this.ddl_tipo_carga.ClearSelection();
        txt_ingresoFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txt_ingresoHora.Text = DateTime.Now.ToString("hh:mm");
        rb_propio.Checked = false;
        rb_externo.Checked = false;
        txt_buscarPatente.Text = "";
        txt_conductorNombre.Text = "";
        txt_conductorRut.Text = "";
        txt_acomRut.Text = "";
        txt_traExtPat.Text = "";
        txt_idSello.Text = "";
        txt_conductorNombre.Enabled = false;
        txt_conductorRut.Enabled = false;
        txt_acomRut.Enabled = false;
        txt_traExtPat.Enabled = false;
        txt_idSello.Enabled = false;
        txt_obs.Text = "";
        ddl_zona.Enabled = false;
        ddl_transportista.Enabled = false;
        ddl_zona.ClearSelection();
        ddl_playa.ClearSelection();
        ddl_posicion.ClearSelection();
        ddl_proveedor.ClearSelection();
        rb_ingresoCargado.Checked = false;
        chk_ingresoCargado_CheckedChanged(null, null);
        txt_buscarDoc.Text = "";
    }
    protected void trailercreado(object sender, EventArgs e)
    {
        this.txt_buscarPatente.Text = ((TrailerBC)(sender)).PLACA;
        this.btnBuscarTrailer_Click(null, null);
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