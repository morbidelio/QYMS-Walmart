// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;

public partial class App_Proceso_EntradaV3 : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    UsuarioBC usuario = new UsuarioBC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["usuario"] == null)
        {
            this.Response.Redirect("../InicioQYMS.aspx");
        }
        this.usuario = (UsuarioBC)this.Session["usuario"];
        this.nuevo_trailer.ButtonClickDemo += new EventHandler(trailercreado);
        if (!this.IsPostBack)
        {
            this.txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
            this.txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
            CargaDrops();
        }
    }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (this.Session["Usuario"] != null)
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)this.Session["Usuario"];

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
            utils.ShowMessage2(this, "conductor", "warn_rutNovalido");
            return;
        }
        ConductorBC c = new ConductorBC().ObtenerXRut(this.txt_conductorRut.Text);
        if (c.ID == 0 || !c.ACTIVO)
        {
            txt_conductorNombre.Enabled = true;
            hf_idCond.Value = "";
            utils.ShowMessage2(this, "conductor", "success_conductorNoexiste");
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
        t = t.obtenerXPlaca(this.txt_editPlaca.Text);
        if (t.ID != 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer ya existe en la base de datos!');", true);
            this.txt_editPlaca.Text = "";
            this.txt_editPlaca.Focus();
        }
        utils.ShowMessage2(this, "buscarConductor", "warn_rutNovalido");
    }
    protected void txt_buscarDoc_TextChanged(object sender, EventArgs e)
    {
        this.btnBuscarTrailer_Click(sender, e);
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

        utils.CargaDrop(this.ddl_zona, "ID", "DESCRIPCION", yms.ObtenerZonas(Convert.ToInt32(this.dropsite.SelectedValue), "", tipo_zona));
        this.ddl_zona_SelectedIndexChanged(null, null);
        if (!string.IsNullOrEmpty(this.hf_idTrailer.Value))
        {
            if (this.txt_buscarPatente.Text != "" && this.txt_buscarPatente.Text != null)
            {
                this.btnBuscarTrailer_Click(null, null);
            }
        }
    }
    protected void ddl_zona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_zona.SelectedIndex != 0)
        {
            int id_zona = Convert.ToInt32(this.ddl_zona.SelectedValue);
            PlayaBC playa = new PlayaBC();
            utils.CargaDrop(this.ddl_playa, "ID", "DESCRIPCION", playa.ObtenerPlayasXCriterio(null, null, id_zona, false, "200"));
            if (this.ddl_playa.Items.Count > 1)
            {
                this.ddl_playa.Enabled = true;
                if (this.ddl_playa.SelectedIndex != 0)
                {
                    int id_playa = Convert.ToInt32(this.ddl_playa.SelectedValue);
                    YMS_ZONA_BC yms = new YMS_ZONA_BC();
                    DataTable ds1 = yms.Obtenerlugares_playa(id_playa, null, "0");

                    utils.CargaDrop(this.ddl_posicion, "ID", "DESCRIPCION", ds1);
                    if (this.ddl_posicion.Items.Count > 1)
                    {
                        this.ddl_posicion.Enabled = true;
                    }
                    else
                    {
                        this.ddl_posicion.Enabled = false;
                    }
                }
                else
                {
                    this.ddl_posicion.Enabled = false;
                }
            }
            else
            {
                this.ddl_playa.Enabled = false;
                this.ddl_posicion.Enabled = false;
            }
        }
        else
        {
            this.ddl_posicion.Enabled = false;
            this.ddl_playa.Enabled = false;
            this.ddl_posicion.SelectedIndex = 0;
            this.ddl_playa.SelectedIndex = 0;
        }
    }
    protected void ddl_playa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((this.ddl_playa.SelectedIndex != 0) && (this.ddl_zona.SelectedIndex != 0))
        {
            int id_playa = Convert.ToInt32(this.ddl_playa.SelectedValue);
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds1 = yms.Obtenerlugares_playa(id_playa, null, "0");
            utils.CargaDrop(this.ddl_posicion, "ID", "DESCRIPCION", ds1);
            if (this.ddl_posicion.Items.Count > 1)
            {
                this.ddl_posicion.Enabled = true;
            }
            else
            {
                this.ddl_posicion.Enabled = false;
            }
        }
        else
        {
            this.ddl_posicion.SelectedIndex = 0;
            this.ddl_posicion.Enabled = false;
        }
    }
    protected void tipo_carga_SelectedIndexChanged(object sender, EventArgs e)
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        this.rb_posAuto.Checked = false;
        this.rb_posManual.Checked = true;

        if (this.ddl_tipo_carga.SelectedValue == "0")
        {
            this.ddl_motivo.Items.Clear();
            this.ddl_motivo.Enabled = false;
        }
        else
        {
            utils.CargaDrop(this.ddl_motivo, "ID", "DESCRIPCION", yms.obtenerMotivoTipoCarga(this.ddl_tipo_carga.SelectedValue, null));

            if (this.ddl_motivo.Items.Count > 1)
            {
                this.ddl_motivo.Enabled = true;
            }
            else
            {
                this.ddl_motivo.Enabled = false;
            }
        }
    }
    #endregion
    #region Buttons
    protected void btn_Conf_Click(object sender, EventArgs e)
    {
        //UpdatePanel4.Update();
        this.txt_editPlaca.Text = this.txt_buscarPatente.Text;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalTrailer();", true);
    }
    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        t.PLACA = this.txt_editPlaca.Text;
        t.TRAN_ID = Convert.ToInt32(this.ddl_editTran.SelectedValue);
        if (t.CrearGenerico(t, false))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Trailer agregado correctamente.');", true);
            hf_idTrailer.Value = t.ID.ToString();
            txt_buscarPatente.Text = t.PLACA;
            ddl_transportista.SelectedValue = t.TRAN_ID.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "$('#modalTrailer').modal('hide');", true);
        }
    }
    protected void btnBuscarTrailer_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        ddl_playa.Enabled = false;
        ddl_zona.Enabled = false;
        ddl_posicion.Enabled = false;

        if (!string.IsNullOrEmpty(this.txt_buscarDoc.Text))
        {
            trailer = trailer.obtenerXDoc(this.txt_buscarDoc.Text, this.dropsite.SelectedValue);
            if (trailer.ID == 0) //Trailer nuevo, no existe
            {
                limpiarTodo();
                utils.ShowMessage2(this, "cita", "warn_noExiste");
            }
            else //Trailer existente, trae datos
            {
                ddl_zona.Enabled = true;
                hf_idTrailer.Value = trailer.ID.ToString();
                hf_pring_id.Value = trailer.PRING_ID.ToString();
                txt_buscarPatente.Text = trailer.PLACA;
                ddl_transportista.SelectedValue = trailer.TRAN_ID.ToString();
                rb_propio.Checked = !trailer.EXTERNO;
                rb_externo.Checked = trailer.EXTERNO;
                txt_traExtPat.Enabled = false;
                txt_traExtPat.Enabled = rb_ingresoCargado.Checked;
                txt_idSello.Enabled = rb_ingresoCargado.Checked;
                ddl_transportista.Enabled = false;
                txt_conductorRut.Enabled = true;
                txt_conductorNombre.Enabled = true;
                chk_conductorExtranjero.Enabled = true;
                txt_acomRut.Enabled = true;
                utils.ShowMessage2(this, "cita", "success");
                //      }
                PreEntradaBC p = new PreEntradaBC();
                p = p.CargarPreEntrada(trailer.ID, Convert.ToInt32(dropsite.SelectedValue), txt_buscarDoc.Text);
                if (p.ID != 0)
                {
                    if (p.COND_ID != 0)
                    {
                        ConductorBC c = new ConductorBC();
                        c = c.ObtenerXId(p.COND_ID);
                        hf_idCond.Value = c.ID.ToString();
                        txt_conductorRut.Text = utils.formatearRut(c.RUT);
                        txt_conductorNombre.Text = c.NOMBRE;
                    }
                    hf_pring_id.Value = p.ID.ToString();
                    txt_traExtPat.Text = p.PATENTE_TRACTO;
                    txt_acomRut.Text = p.RUT_ACOMP;
                    ddl_proveedor.SelectedValue = p.PROV_ID.ToString();
                    hf_idTrailer.Value = p.TRAI_ID.ToString();
                    DateTime fechaHora = DateTime.Parse(string.Format("{0} {1}", this.txt_ingresoFecha.Text, this.txt_ingresoHora.Text));
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
                    //  }
                }
                else
                {
                    utils.ShowMessage2(this, "cita", "warn_expirado");
                    limpiarTodo();
                }
            }
            rb_posAuto.Checked = false;
            rb_posManual.Checked = false;
            ddl_zona.Enabled = false;
            if (txt_conductorRut.Text != "")
                txt_conductorRut_TextChanged(null, null);
        }
        else
        {
            utils.ShowMessage2(this, "cita", "warn_nroVacio");
            ddl_zona.Enabled = false;
            hf_idTrailer.Value = "";
            hf_pring_id.Value = "";
        }
    }
    protected void btn_limpiar_Click(object sender, EventArgs e)
    {
        limpiarTodo();
    }
    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        try
        {
            ConductorBC c = new ConductorBC();
            MovimientoBC mov = new MovimientoBC();
            TrailerUltEstadoBC trailerUE = new TrailerUltEstadoBC();
            TrailerBC trailer = new TrailerBC();
            if (string.IsNullOrEmpty(hf_idCond.Value))
            {
                c.RUT = utils.formatearRut(txt_conductorRut.Text);
                c.NOMBRE = txt_conductorNombre.Text;
                c.TRAN_ID = Convert.ToInt32(ddl_transportista.SelectedValue);
                c.COND_EXTRANJERO = chk_conductorExtranjero.Checked;
                hf_idCond.Value = c.AgregarIdentity().ToString();
            }

            mov.FECHA_CREACION = DateTime.Now;
            mov.ID_ESTADO = 10;
            mov.OBSERVACION = txt_obs.Text;
            mov.FECHA_ORIGEN = Convert.ToDateTime(string.Format("{0} {1}", txt_ingresoFecha.Text, txt_ingresoHora.Text));
            mov.ID_DESTINO = Convert.ToInt32(ddl_posicion.SelectedValue);
            mov.FECHA_DESTINO = mov.FECHA_ORIGEN.AddMinutes(30);
            if (utils.patentevalida(txt_traExtPat.Text) == true)
            {
                mov.PATENTE_TRACTO = txt_traExtPat.Text;

                if (rb_mantExterno.Checked)
                {
                    mov.MANT_EXTERNO = true;
                }
                else
                {
                    trailerUE.COND_ID = Convert.ToInt32(hf_idCond.Value);
                    mov.MANT_EXTERNO = false;
                    mov.ID_TRAILER = Convert.ToInt32(hf_idTrailer.Value);
                    trailer.ID = Convert.ToInt32(hf_idTrailer.Value);
                    trailer.PLACA = txt_buscarPatente.Text;
                    trailer.CODIGO = string.Format("{0}_{1}", ddl_transportista.SelectedItem.Text, txt_buscarPatente.Text);
                    trailer.EXTERNO = (rb_externo.Checked || rb_proveedor.Checked || rb_mantExterno.Checked);
                    trailer.TRAN_ID = Convert.ToInt32(ddl_transportista.SelectedValue);

                    trailerUE.SITE_ID = Convert.ToInt32(dropsite.SelectedValue); // 1; // Cambiar después de introducir variables de sesión
                    trailerUE.CHOFER_RUT = utils.formatearRut(txt_conductorRut.Text);
                    trailerUE.CHOFER_NOMBRE = txt_conductorNombre.Text;
                    trailerUE.ACOMP_RUT = txt_acomRut.Text;
                    trailerUE.PROV_ID = Convert.ToInt32(ddl_proveedor.SelectedValue);
                    trailerUE.DOC_INGRESO = txt_buscarDoc.Text;
                    trailerUE.SELLO_INGRESO = txt_idSello.Text;
                    trailerUE.TIPO_INGRESO_CARGA = ddl_tipo_carga.SelectedValue;
                    trailerUE.motivo_TIPO_INGRESO_CARGA = ddl_motivo.SelectedValue;
                    trailerUE.pring_id = hf_pring_id.Value.ToString(); // p.CargarPreEntrada(  //p.CargarPreEntrada(mov.ID_TRAILER, Convert.ToInt32(dropsite.SelectedValue)).ID.ToString();
                    trailerUE.CARGADO = rb_ingresoCargado.Checked;
                }
                UsuarioBC usuario = (UsuarioBC)this.Session["USUARIO"];

                if (trailer.ID == 0)
                {
                    utils.ShowMessage(this, "Debe ingresar Trailer", "warn", true);
                    limpiarTodo();
                }
                else
                {
                    string resultado;
                    bool ejecucion = mov.ProcesoEntrada(mov, trailerUE, trailer, usuario.ID, out resultado);
                    if (ejecucion && resultado == "")
                    {
                        utils.ShowMessage(this, "Ingreso correcto", "success", true);
                        limpiarTodo();
                    }
                    else
                    {
                        utils.ShowMessage(this, resultado, "error", false);
                    }
                }
            }
            else
            {
                utils.ShowMessage(this, "Patente de Tracto Invalida", "warn", false);
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
    }
    protected void btn_nuevoTrailer_Click(object sender, EventArgs e)
    {
        this.nuevo_trailer.limpiarForm();
        this.nuevo_trailer.setplaca(this.txt_buscarPatente.Text, false);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTrailer();", true);
    }
    #endregion
    #region CheckBox
    protected void rb_pos_CheckedChanged(object sender, EventArgs e)
    {
        if (this.hf_idTrailer.Value != "")
        {
            //  chk_ingresoCargado_CheckedChanged(null,null);
            if (this.rb_posAuto.Checked)
            {
                CargaTipoBC ct = new CargaTipoBC();
                // ct = ct.obtenerXID(Convert.ToInt32(this.ddl_tipo_carga.SelectedValue));
                if (ct.CargaDestinos(Convert.ToInt32(this.ddl_tipo_carga.SelectedValue)).Rows.Count == 0)
                {
                    LugarBC l = new LugarBC();
                    this.ddl_zona.Enabled = false;
                    this.ddl_playa.Enabled = false;
                    this.ddl_posicion.Enabled = false;
                    l = l.obtenerLugarAuto(Convert.ToInt32(this.dropsite.SelectedValue), this.usuario.ID, null);
                    if (l.ID == 0 || l.ID == null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Error. Intente designar un lugar manualmente.');", true);
                    }
                    else
                    {
                        PlayaBC p = new PlayaBC();
                        ZonaBC zona = new ZonaBC();
                        try
                        {
                            this.ddl_zona.SelectedValue = l.ID_ZONA.ToString();
                        }
                        catch (Exception)
                        {
                            zona = zona.ObtenerXId(l.ID_ZONA);
                            this.ddl_zona.Items.Add(new System.Web.UI.WebControls.ListItem(zona.DESCRIPCION, zona.ID.ToString()));
                            this.ddl_zona.SelectedValue = zona.ID.ToString();
                        }
                        utils.CargaDrop(this.ddl_playa, "ID", "DESCRIPCION", p.ObtenerXZona(l.ID_ZONA));
                        this.ddl_playa.SelectedValue = l.ID_PLAYA.ToString();
                        CargaDrops drops = new CargaDrops();
                        drops.Lugar1(this.ddl_posicion, 0, l.ID_PLAYA, 0, 1);
                        //utils.CargaDrop(this.ddl_posicion, "ID", "DESCRIPCION", lugar.ObtenerXPlaya(l.ID_PLAYA));
                        this.ddl_posicion.SelectedValue = l.ID.ToString();
                    }
                }
            }
            else
            {
                this.ddl_zona.Enabled = true;
                if (this.ddl_playa.SelectedIndex > 0)
                {
                    this.ddl_playa.Enabled = true;
                }
                if (this.ddl_posicion.SelectedIndex > 0)
                {
                    this.ddl_posicion.Enabled = true;
                }
            }
        }
    }
    protected void chk_ingresoCargado_CheckedChanged(object sender, EventArgs e)
    {
        this.rb_posAuto.Checked = false;
        this.rb_posManual.Checked = true;
        this.ddl_motivo.Enabled = false;
        if (!String.IsNullOrEmpty(this.hf_idTrailer.Value) && this.hf_idTrailer.Value != "0")
        {
            //    this.drop_SelectedIndexChanged(null, null);
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
            this.tipo_carga_SelectedIndexChanged(null, null);

            utils.CargaDrop(this.ddl_zona, "ID", "DESCRIPCION", yms.ObtenerZonas(Convert.ToInt32(this.dropsite.SelectedValue), "", tipo_zona));

            this.ddl_zona_SelectedIndexChanged(null, null);
            if (this.rb_ingresoCargado.Checked)
            {
                this.txt_traExtPat.Enabled = true;
                //  this.txt_doc.Enabled = true;
                this.txt_idSello.Enabled = true;
                this.rb_posAuto.Enabled = true;
            }
            else
            {
                this.txt_traExtPat.Enabled = false;
                //        this.txt_doc.Enabled = false;
                this.txt_idSello.Enabled = false;
                this.rb_posAuto.Enabled = false;
            }
        }
    }
    protected void refrescarpos(object sender, EventArgs e)
    {
        if (this.rb_posAuto.Checked == true)
        {
            this.rb_pos_CheckedChanged(sender, e);
        }
        else 
        {
            this.ddl_playa_SelectedIndexChanged(sender, e);
        }
    }
    #endregion
    #region UtilsPagina
    private void CargaDrops()
    {
        TransportistaBC tran = new TransportistaBC();
        ProveedorBC proveedor = new ProveedorBC();
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        utils.CargaDrop(this.ddl_tipo_carga, "ID", "DESCRIPCION", yms.obtenerTipoCarga(null, false, false));
        DataTable ds = yms.ObteneSites(usuario.ID);
        utils.CargaDropNormal(this.dropsite, "ID", "NOMBRE", ds);
        this.drop_SelectedIndexChanged(null, null);
        utils.CargaDrop(this.ddl_proveedor, "ID", "DESCRIPCION", proveedor.obtenerTodo());
        utils.CargaDrop(this.ddl_transportista, "ID", "NOMBRE", tran.ObtenerTodos());
    }
    private void limpiarTodo()
    {
        hf_idTrailer.Value = "";
        hf_pring_id.Value = "";
        ddl_transportista.ClearSelection();
        ddl_tipo_carga.ClearSelection();
        txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
        txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
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
        ddl_zona.ClearSelection();
        ddl_playa.ClearSelection();
        ddl_posicion.ClearSelection();
        ddl_proveedor.ClearSelection();
        rb_ingresoCargado.Checked = false;
        chk_conductorExtranjero.Enabled = false;
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