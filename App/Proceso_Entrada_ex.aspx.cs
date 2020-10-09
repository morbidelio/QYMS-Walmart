// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Web.UI;

public partial class App_Proceso_Entrada_ex : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    
    UsuarioBC usuario = new UsuarioBC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("../InicioQYMS.aspx");
        usuario = (UsuarioBC)this.Session["usuario"];
        nuevo_trailer.ButtonClickDemo += new EventHandler(trailercreado);
        if (!this.IsPostBack)
        {
            txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
            txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
            cargaDrops();
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
        if (this.txt_buscarPatente.Text != "")
        {
            this.btnBuscarTrailer_Click(null, null);
        }
    }
    protected void ddl_zona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_zona.SelectedIndex != 0)
        {
            int id_zona = Convert.ToInt32(this.ddl_zona.SelectedValue);
            PlayaBC playa = new PlayaBC();

            utils.CargaDrop(this.ddl_playa, "ID", "DESCRIPCION", playa.ObtenerPlayasXCriterio(null, null, id_zona, false, "200"));
            ddl_playa_SelectedIndexChanged(null, null);
        }
        else
        {
            this.ddl_posicion.Enabled = false;
            this.ddl_playa.Enabled = false;
            this.ddl_posicion.ClearSelection();
            this.ddl_playa.ClearSelection();
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
    protected void btnBuscarTrailer_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_buscarPatente.Text))
        {
            TrailerBC trailer = new TrailerBC().obtenerXPlaca(txt_buscarPatente.Text);

            if (trailer.ID == 0) //Trailer nuevo, no existe
            {
                
                if (new TractoBC().ObtenerXPatente(txt_buscarPatente.Text).ID > 0) { utils.ShowMessage2(this, "trailer", "warn_tracto"); return; }
                utils.ShowMessage2(this, "trailer", "warn_noExiste");
                limpiarTodo();
            }
            else //Trailer existente, trae datos
            {
                PreEntradaBC p = new PreEntradaBC();
                TrailerUltSalidaBC tusa = new TrailerUltSalidaBC();
                YMS_ZONA_BC yms = new YMS_ZONA_BC();
                utils.CargaDrop(this.ddl_tipo_carga, "ID", "DESCRIPCION", yms.obtenerTipoCarga(null, false,true));
                hf_idTrailer.Value = trailer.ID.ToString();
                txt_buscarPatente.Text = trailer.PLACA;
                ddl_transportista.SelectedValue = trailer.TRAN_ID.ToString();
                rb_propio.Checked = !trailer.EXTERNO;
                rb_externo.Checked = trailer.EXTERNO;
                txt_tracto.Enabled = false;
                ddl_transportista.Enabled = false;
                txt_tracto.Enabled = rb_ingresoCargado.Checked;
                ddl_proveedor.Enabled = rb_ingresoCargado.Checked;
                txt_doc.Enabled = rb_ingresoCargado.Checked;
                txt_idSello.Enabled = rb_ingresoCargado.Checked;
                txt_conductorRut.Enabled = true;
                txt_conductorNombre.Enabled = true;
                chk_conductorExtranjero.Enabled = true;
                txt_acomRut.Enabled = true;
                utils.ShowMessage2(this, "trailer", "success");
                p = p.CargarPreEntrada(trailer.ID, Convert.ToInt32(this.dropsite.SelectedValue));
                if (p.ID != 0 )
                {
                    if (p.COND_ID != 0 )
                    {
                        ConductorBC c = new ConductorBC(p.COND_ID);
                        hf_idCond.Value = c.ID.ToString();
                        chk_conductorExtranjero.Checked = c.COND_EXTRANJERO;
                        txt_conductorRut.Text = (!c.COND_EXTRANJERO) ? utils.formatearRut(c.RUT) : c.RUT;
                        txt_conductorNombre.Text = c.NOMBRE;
                        txt_conductorNombre.Enabled = false;
                    }
                    txt_doc.Text = p.DOC_INGRESO;
                    txt_tracto.Text = p.PATENTE_TRACTO;
                    txt_acomRut.Text = p.RUT_ACOMP;
                    ddl_proveedor.SelectedValue = p.PROV_ID.ToString();
                    txt_obs.Text = p.Observacion;
                    if (!string.IsNullOrEmpty(p.SELLO_INGRESO))
                        txt_idSello.Text = p.SELLO_INGRESO;
                    if (!string.IsNullOrEmpty(p.SELLO_CARGA))
                        txt_idSello.Text = p.SELLO_CARGA;
                    rb_ingresoCargado.Checked = true;
                    rb_ingresoVacio.Checked = false;
                    chk_ingresoCargado_CheckedChanged(null, null);
                    ddl_tipo_carga.SelectedValue = p.TIIC_ID.ToString();
                    tipo_carga_SelectedIndexChanged(null, null);
                    ddl_motivo.SelectedValue = p.MOIC_ID.ToString();
                    txt_doc.Enabled = false;
                }
                else
                {
                    tusa = tusa.ObtenerXId(trailer.ID);
                    if (tusa.TRAI_ID != 0)
                    {
                        if (tusa.COND_ID != 0)
                        {
                            ConductorBC c = new ConductorBC(tusa.COND_ID);
                            hf_idCond.Value = c.ID.ToString();
                            txt_conductorRut.Text = c.RUT;
                            txt_conductorNombre.Text = c.NOMBRE;
                        }
                        txt_tracto.Text = tusa.PATENTE_TRACTO;
                        txt_acomRut.Text = tusa.ACOMP_RUT;
                        ddl_proveedor.SelectedValue = tusa.PROV_ID.ToString();
                        if (!string.IsNullOrEmpty(tusa.SELLO_INGRESO))
                        {
                            txt_idSello.Text = tusa.SELLO_INGRESO;
                        }
                        if (!string.IsNullOrEmpty(tusa.SELLO_CARGA))
                        {
                            txt_idSello.Text = tusa.SELLO_CARGA;
                        }

                        rb_ingresoCargado.Checked = true;
                        rb_ingresoVacio.Checked = false;
                        
                        chk_ingresoCargado_CheckedChanged(null, null);
                        ddl_tipo_carga.SelectedValue = tusa.TIIC_ID.ToString();
                        tipo_carga_SelectedIndexChanged(null, null);
                        ddl_motivo.SelectedValue = tusa.MOIC_ID.ToString();
                    }
                }
            }
            rb_posAuto.Checked = false;
            rb_posManual.Checked = false;
            ddl_zona.Enabled = true;
            if (txt_conductorRut.Text != "")
            {
                txt_conductorRut_TextChanged(null, null);
            }
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
            txt_conductorNombre.Enabled = false;
            if (string.IsNullOrEmpty(hf_idCond.Value))
            {
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
            mov.OBSERVACION = this.txt_obs.Text;
            mov.FECHA_ORIGEN = DateTime.Parse(string.Format("{0} {1}", this.txt_ingresoFecha.Text, this.txt_ingresoHora.Text));
            mov.ID_DESTINO = Convert.ToInt32(this.ddl_posicion.SelectedValue);
            mov.FECHA_DESTINO = mov.FECHA_ORIGEN.AddMinutes(30);
            mov.PATENTE_TRACTO = txt_tracto.Text;
            if (this.rb_mantExterno.Checked)
            {
                mov.MANT_EXTERNO = true;
            }
            else
            {
                PreEntradaBC p = new PreEntradaBC();
                mov.MANT_EXTERNO = false;
                mov.ID_TRAILER = Convert.ToInt32(hf_idTrailer.Value);
                trailer.ID = Convert.ToInt32(hf_idTrailer.Value);
                trailer.PLACA = this.txt_buscarPatente.Text;
                trailer.CODIGO = string.Format("{0}_{1}", ddl_transportista.SelectedItem.Text, txt_buscarPatente.Text);
                trailer.EXTERNO = (rb_externo.Checked || rb_proveedor.Checked || rb_mantExterno.Checked);
                trailer.TRAN_ID = Convert.ToInt32(ddl_transportista.SelectedValue);
                trailerUE.SITE_ID = Convert.ToInt32(dropsite.SelectedValue); // 1; // Cambiar después de introducir variables de sesión
                trailerUE.CHOFER_RUT = txt_conductorRut.Text;
                trailerUE.CHOFER_NOMBRE = txt_conductorNombre.Text;
                trailerUE.ACOMP_RUT = txt_acomRut.Text;
                trailerUE.PROV_ID = Convert.ToInt32(ddl_proveedor.SelectedValue);
                trailerUE.DOC_INGRESO = txt_doc.Text;
                trailerUE.SELLO_INGRESO = txt_idSello.Text;
                trailerUE.TIPO_INGRESO_CARGA = ddl_tipo_carga.SelectedValue;
                trailerUE.motivo_TIPO_INGRESO_CARGA = ddl_motivo.SelectedValue;
                trailerUE.COND_ID = Convert.ToInt32(hf_idCond.Value);
                trailerUE.pring_id = p.CargarPreEntrada(mov.ID_TRAILER, Convert.ToInt32(dropsite.SelectedValue), txt_doc.Text).ID.ToString();
                trailerUE.CARGADO = rb_ingresoCargado.Checked;
            }
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
    protected void btn_nuevoTrailer_Click(object sender, EventArgs e)
    {
        this.nuevo_trailer.limpiarForm();
        this.nuevo_trailer.setplaca(this.txt_buscarPatente.Text, this.chk_vehiculoImportado.Checked);

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTrailer();", true);
    }
    #endregion
    #region CheckBox
    protected void rb_pos_CheckedChanged(object sender, EventArgs e)
    {
        if (this.hf_idTrailer.Value != "")
        {
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
                    if (l.ID == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Error. Intente designar un lugar manualmente.');", true);
                    }
                    else
                    {
                        try
                        {
                            PlayaBC p = new PlayaBC();
                            CargaDrops drops = new CargaDrops();

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

                            drops.Playa(this.ddl_playa, l.ID_ZONA);
                            this.ddl_playa.SelectedValue = l.ID_PLAYA.ToString();
                            drops.Lugar1(this.ddl_posicion, 0, l.ID_PLAYA, 0, 1);
                            this.ddl_posicion.SelectedValue = l.ID.ToString();
                        }
                        catch (Exception)
                        {
                            this.ddl_zona.ClearSelection();
                            this.ddl_playa.ClearSelection();
                            this.ddl_posicion.ClearSelection();
                            this.ddl_zona.Enabled = false;
                            this.ddl_playa.Enabled = false;
                            this.ddl_posicion.Enabled = false;
                        }
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
        this.ddl_tipo_carga.Enabled = false;
        this.ddl_motivo.Enabled = false;
        if (!String.IsNullOrEmpty(this.hf_idTrailer.Value) && this.hf_idTrailer.Value != "0")
        {
            //    this.drop_SelectedIndexChanged(null, null);
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            string tipo_zona;
            if (this.rb_ingresoCargado.Checked == true)
            {
                tipo_zona = "200";
                this.ddl_tipo_carga.Enabled = true;
            }
            else
            {
                tipo_zona = "100";
                ddl_tipo_carga.Enabled = false;
            }
            tipo_carga_SelectedIndexChanged(null, null);
            ddl_zona.Enabled = true;

            utils.CargaDrop(this.ddl_zona, "ID", "DESCRIPCION", yms.ObtenerZonasTipoCarga(Convert.ToInt32(this.dropsite.SelectedValue), "", tipo_zona, Convert.ToInt32(this.ddl_tipo_carga.SelectedValue),200));

            ddl_zona_SelectedIndexChanged(null, null);
            if (rb_ingresoCargado.Checked)
            {
                txt_tracto.Enabled = true;
                ddl_proveedor.Enabled = true;
                txt_doc.Enabled = true;
                txt_idSello.Enabled = true;
                txt_conductorRut.Enabled = true;
                txt_conductorNombre.Enabled = true;
                txt_acomRut.Enabled = true;
            }
            if (rb_ingresoVacio.Checked)
            {
                txt_tracto.Enabled = true;
                ddl_proveedor.Enabled = false;
                txt_doc.Enabled = false;
                txt_idSello.Enabled = false;
                txt_conductorRut.Enabled = true;
                txt_conductorNombre.Enabled = true;
                txt_acomRut.Enabled = true;
            }
        }

        this.rb_pos_CheckedChanged(null, null);
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
    private void cargaDrops()
    {
        DataTable dt;
        TransportistaBC tran = new TransportistaBC();
        ProveedorBC proveedor = new ProveedorBC();
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        dt = yms.ObteneSites(usuario.ID);
        utils.CargaDropNormal(this.dropsite, "ID", "NOMBRE", dt);
        drop_SelectedIndexChanged(null, null);
        dt = proveedor.obtenerTodo();
        utils.CargaDrop(this.ddl_proveedor, "ID", "DESCRIPCION", dt);
        dt = tran.ObtenerTodos();
        utils.CargaDrop(this.ddl_transportista, "ID", "NOMBRE", dt);
    }
    private void limpiarTodo()
    {
        hf_idTrailer.Value = "";
        hf_idCond.Value = "";
        ddl_transportista.ClearSelection();
        ddl_tipo_carga.ClearSelection();
        txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
        txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
        txt_buscarPatente.Text = "";
        rb_propio.Checked = false;
        rb_externo.Checked = false;
        txt_conductorNombre.Text = "";
        txt_conductorRut.Text = "";
        txt_acomRut.Text = "";
        txt_tracto.Text = "";
        txt_doc.Text = "";
        txt_idSello.Text = "";
        //    txt_buscarNro.Enabled = false;
        txt_conductorNombre.Enabled = false;
        txt_conductorRut.Enabled = false;
        chk_conductorExtranjero.Checked = false;
        chk_conductorExtranjero.Enabled = false;
        txt_acomRut.Enabled = false;
        txt_tracto.Enabled = false;
        txt_doc.Enabled = false;
        txt_idSello.Enabled = false;
        txt_obs.Text = "";
        ddl_zona.Enabled = false;
        ddl_zona.ClearSelection();
        ddl_zona_SelectedIndexChanged(null, null);
        ddl_transportista.Enabled = false;
        ddl_proveedor.ClearSelection();
        ddl_proveedor.Enabled = false;
        rb_ingresoCargado.Checked = true;
        rb_ingresoVacio.Checked = false;
        rb_posManual.Checked = true;
        rb_posAuto.Checked = false;
        chk_ingresoCargado_CheckedChanged(null, null);
        chk_vehiculoImportado.Enabled = false;
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