// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;

public partial class App_Proceso_Entrada : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    UsuarioBC usuario = new UsuarioBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/InicioQYMS2.aspx");
        }
        usuario = (UsuarioBC)Session["usuario"];
        nuevo_trailer.ButtonClickDemo += new EventHandler(trailercreado);
        if (!IsPostBack)
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
            utils.ShowMessage2(this, "conductor", "success_conductorNoexiste");
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

        //utils.CargaDrop(this.ddl_zona, "ID", "DESCRIPCION", yms.ObtenerZonas(Convert.ToInt32(this.dropsite.SelectedValue), "", tipo_zona));
        this.ddl_zona_SelectedIndexChanged(null, null);
        if (this.txt_buscarPatente.Text != "" && this.txt_buscarPatente.Text != null)
        {
            this.btnBuscarTrailer_Click(null, null);
        }
    }
    protected void ddl_zona_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddl_posicion.ClearSelection();
        if (this.ddl_zona.SelectedIndex != 0)
        {
            int id_zona = Convert.ToInt32(this.ddl_zona.SelectedValue);
            PlayaBC playa = new PlayaBC();
            if (rb_mantenimiento.Checked)
            {
                utils.CargaDrop(this.ddl_playa, "ID", "DESCRIPCION", playa.ObtenerPlayasXCriterio(null, null, id_zona, false, null));
            }
            else
            {
                utils.CargaDrop(this.ddl_playa, "ID", "DESCRIPCION", playa.ObtenerPlayasXCriterioTipoCarga(null, null, id_zona, false, Convert.ToInt32(this.ddl_tipo_carga.SelectedValue), "200"));
            }
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
        if (ddl_playa.SelectedIndex > 0)
        {
            int id_playa = Convert.ToInt32(this.ddl_playa.SelectedValue);
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
        this.rb_posAuto.Checked = false;
        this.rb_posManual.Checked = true;
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

        if (ddl_tipo_carga.SelectedValue == "0")
        {
            ddl_motivo.Items.Clear();
            ddl_motivo.Enabled = false;
        }
        else
        {
            utils.CargaDrop(this.ddl_motivo, "ID", "DESCRIPCION", yms.obtenerMotivoTipoCarga(this.ddl_tipo_carga.SelectedValue, null));
            ddl_motivo.Enabled = (ddl_motivo.Items.Count > 1);
        }

        utils.CargaDrop(ddl_zona, "ID", "DESCRIPCION", yms.ObtenerZonasTipoCarga(Convert.ToInt32(this.dropsite.SelectedValue), "", tipo_zona, Convert.ToInt32(this.ddl_tipo_carga.SelectedValue), 200));
        ddl_zona_SelectedIndexChanged(null, null);
        Pnl_guia.Visible = true;
        PNL_CITA.Visible = true;
    }
    #endregion
    #region Buttons
    protected void btnBuscarTrailer_Click(object sender, EventArgs e)
    {
        if (txt_buscarPatente.Text != "" || txt_buscarNro.Text != "")
        {
            TrailerBC trailer = (!string.IsNullOrEmpty(this.txt_buscarNro.Text)) ? new TrailerBC().obtenerXNro(this.txt_buscarNro.Text): new TrailerBC().obtenerXPlaca(this.txt_buscarPatente.Text);

            if (trailer.ID == 0) //Trailer nuevo, no existe
            {
                hf_idTrailer.Value = "";
                ddl_transportista.ClearSelection();
                txt_conductorRut.Text = "";
                txt_conductorNombre.Text = "";
                txt_acomRut.Text = "";
                txt_buscarNro.Text = "";
                ddl_proveedor.ClearSelection();
                rb_externo.Checked = false;
                rb_propio.Checked = false;
                guia.Enabled = true;
                txt_idSello.Text = "";
                txt_tracto.Text = "";
                chk_ingresoCargado_CheckedChanged(null, null);
                if (new TractoBC().ObtenerXPatente(txt_buscarPatente.Text).ID > 0) { utils.ShowMessage2(this, "trailer", "warn_tracto"); return; }
                utils.ShowMessage2(this, "trailer", "warn_noExiste");
            }
            else //Trailer existente, trae datos
            {
                hf_idTrailer.Value = trailer.ID.ToString();
                txt_buscarPatente.Text = trailer.PLACA;
                txt_buscarNro.Text = trailer.NUMERO;
                ddl_transportista.SelectedValue = trailer.TRAN_ID.ToString();
                rb_propio.Checked = !trailer.EXTERNO;
                rb_externo.Checked = trailer.EXTERNO;
                txt_tracto.Enabled = rb_ingresoCargado.Checked;
                ddl_proveedor.Enabled = rb_ingresoCargado.Checked;
                txt_doc.Enabled = rb_ingresoCargado.Checked;
                txt_idSello.Enabled = rb_ingresoCargado.Checked;
                ddl_transportista.Enabled = false;
                txt_conductorRut.Enabled = true;
                chk_conductorExtranjero.Enabled = true;
                txt_conductorNombre.Enabled = true;
                txt_acomRut.Enabled = true;
                guia.Enabled = true;

                PreEntradaBC p = new PreEntradaBC();
                TrailerUltSalidaBC tusa = new TrailerUltSalidaBC();
                p = p.CargarPreEntrada(trailer.ID, Convert.ToInt32(this.dropsite.SelectedValue));
                if (p.ID != 0)
                {
                    if (p.COND_ID != 0)
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
                    rb_ingresoCargado.Checked = p.CARGADO;
                    rb_ingresoVacio.Checked = !p.CARGADO;
                    chk_ingresoCargado_CheckedChanged(null, null);
                    try
                    {
                        ddl_tipo_carga.SelectedValue = p.TIIC_ID.ToString();

                    }
                    catch (Exception ex)
                    {
                        ddl_tipo_carga.SelectedValue = "0";
                    }
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
                            chk_conductorExtranjero.Checked = c.COND_EXTRANJERO;
                            txt_conductorRut.Text = (!c.COND_EXTRANJERO) ? utils.formatearRut(c.RUT) : c.RUT;
                            txt_conductorNombre.Text = c.NOMBRE;
                            txt_conductorNombre.Enabled = false;
                        }
                        txt_tracto.Text = tusa.PATENTE_TRACTO;
                        txt_acomRut.Text = tusa.ACOMP_RUT;
                        ddl_proveedor.SelectedValue = tusa.PROV_ID.ToString();
                        if (!string.IsNullOrEmpty(tusa.SELLO_INGRESO))
                            txt_idSello.Text = tusa.SELLO_INGRESO;
                        if (!string.IsNullOrEmpty(tusa.SELLO_CARGA))
                            txt_idSello.Text = tusa.SELLO_CARGA;
                        rb_ingresoCargado.Checked = false;
                        rb_ingresoVacio.Checked = true;
                        chk_ingresoCargado_CheckedChanged(null, null);
                        ddl_tipo_carga.SelectedValue = tusa.TIIC_ID.ToString();
                        tipo_carga_SelectedIndexChanged(null, null);
                        ddl_motivo.SelectedValue = tusa.MOIC_ID.ToString();
                    }
                }
                rb_posAuto.Checked = false;
                rb_posManual.Checked = false;
                ddl_zona.Enabled = true;
                guia.Enabled = true;
                utils.ShowMessage2(this, "trailer", "success");
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
            MovimientoBC mov = new MovimientoBC();
            TrailerUltEstadoBC trailerUE = new TrailerUltEstadoBC();
            TrailerBC trailer = new TrailerBC();
            if (string.IsNullOrEmpty(hf_idCond.Value))
            {
                ConductorBC c = new ConductorBC();
                c.RUT = txt_conductorRut.Text;
                c.NOMBRE = txt_conductorNombre.Text;
                c.TRAN_ID = Convert.ToInt32(ddl_transportista.SelectedValue);
                c.COND_EXTRANJERO = chk_conductorExtranjero.Checked;
                hf_idCond.Value = c.AgregarIdentity().ToString();
            }
            mov.FECHA_CREACION = DateTime.Now;
            mov.ID_ESTADO = 10;
            mov.OBSERVACION = this.txt_obs.Text;
            mov.FECHA_ORIGEN = DateTime.Parse(string.Format("{0} {1}", txt_ingresoFecha.Text, txt_ingresoHora.Text));
            mov.ID_DESTINO = Convert.ToInt32(ddl_posicion.SelectedValue);
            mov.FECHA_DESTINO = mov.FECHA_ORIGEN.AddMinutes(30);
            mov.PATENTE_TRACTO = txt_tracto.Text;

            if (rb_mantExterno.Checked)
            {
                mov.MANT_EXTERNO = true;
            }
            else
            {
                PreEntradaBC p = new PreEntradaBC();
                mov.MANT_EXTERNO = false;
                mov.ID_TRAILER = Convert.ToInt32(hf_idTrailer.Value);
                trailer.ID = Convert.ToInt32(hf_idTrailer.Value);
                trailer.PLACA = txt_buscarPatente.Text;
                trailer.CODIGO = string.Format("{0}_{1}", ddl_transportista.SelectedItem.Text, txt_buscarPatente.Text);
                trailer.EXTERNO = (rb_externo.Checked || rb_proveedor.Checked || rb_mantExterno.Checked);
                trailer.NUMERO = txt_buscarNro.Text;
                trailer.TRAN_ID = Convert.ToInt32(ddl_transportista.SelectedValue);
                trailerUE.SITE_ID = Convert.ToInt32(dropsite.SelectedValue);
                trailerUE.CHOFER_RUT = utils.formatearRut(txt_conductorRut.Text);
                trailerUE.CHOFER_NOMBRE = txt_conductorNombre.Text;
                trailerUE.GUIA = guia.Text;
                trailerUE.ACOMP_RUT = txt_acomRut.Text;
                trailerUE.PROV_ID = Convert.ToInt32(ddl_proveedor.SelectedValue);
                trailerUE.DOC_INGRESO = txt_doc.Text;
                trailerUE.SELLO_INGRESO = txt_idSello.Text;
                trailerUE.TIPO_INGRESO_CARGA = ddl_tipo_carga.SelectedValue;
                trailerUE.motivo_TIPO_INGRESO_CARGA = ddl_motivo.SelectedValue;
                trailerUE.COND_ID = Convert.ToInt32(hf_idCond.Value);
                trailerUE.CARGADO = rb_ingresoCargado.Checked;
                trailerUE.pring_id = p.CargarPreEntrada(mov.ID_TRAILER, Convert.ToInt32(dropsite.SelectedValue), txt_doc.Text).ID.ToString();
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
        this.nuevo_trailer.setplaca(this.txt_buscarPatente.Text, false);
        utils.AbrirModal(this, "modalEditarTrailer");
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
                if (ct.CargaDestinos(Convert.ToInt32(ddl_tipo_carga.SelectedValue)).Rows.Count == 0)
                {
                    LugarBC l = new LugarBC();
                    ddl_zona.Enabled = false;
                    ddl_playa.Enabled = false;
                    ddl_posicion.Enabled = false;
                    l = l.obtenerLugarAuto(Convert.ToInt32(dropsite.SelectedValue), usuario.ID, null);
                    if (l.ID == 0)
                    {
                        utils.ShowMessage2(this, "posicion", "warn_posAuto");
                        return;
                    }
                    try
                    {
                        try
                        {
                            ddl_zona.SelectedValue = l.ID_ZONA.ToString();
                        }
                        catch (Exception)
                        {
                            ZonaBC zona = new ZonaBC();
                            zona = zona.ObtenerXId(l.ID_ZONA);
                            ddl_zona.Items.Add(new System.Web.UI.WebControls.ListItem(zona.DESCRIPCION, zona.ID.ToString()));
                            ddl_zona.SelectedValue = zona.ID.ToString();
                        }
                        ddl_zona_SelectedIndexChanged(null, null);
                        ddl_playa.SelectedValue = l.ID_PLAYA.ToString();
                        ddl_playa_SelectedIndexChanged(null, null);
                        ddl_posicion.SelectedValue = l.ID.ToString();
                    }
                    catch (Exception)
                    {
                        ddl_zona.ClearSelection();
                        ddl_zona_SelectedIndexChanged(null, null);
                    }
                }
            }
            else if (rb_posManual.Checked == true)
            {

                tipo_carga_SelectedIndexChanged(null, null);
                ddl_zona.Enabled = true;
                ddl_playa.Enabled = (ddl_playa.SelectedIndex > 0);
                ddl_posicion.Enabled = (ddl_posicion.SelectedIndex > 0);

            }
            else if (this.rb_mantenimiento.Checked)
            {
                ZonaBC zona = new ZonaBC();
                utils.CargaDrop(this.ddl_zona, "ID", "DESCRIPCION", zona.ObtenerXCriterio("", Convert.ToInt32(this.dropsite.SelectedValue), 204));
                ddl_zona_SelectedIndexChanged(null, null);
                ddl_zona.Enabled = true;
                ddl_playa.Enabled = (ddl_playa.SelectedIndex > 0);
                ddl_posicion.Enabled = (ddl_posicion.SelectedIndex > 0);
            }
            if (sender != null) ddl_zona_SelectedIndexChanged(null, null);
        }
    }
    protected void chk_ingresoCargado_CheckedChanged(object sender, EventArgs e)
    {
        rb_posAuto.Checked = false;
        rb_posManual.Checked = true;
        rb_mantenimiento.Checked = false;
        ddl_tipo_carga.Enabled = false;
        ddl_motivo.Enabled = false;
        if (!String.IsNullOrEmpty(this.hf_idTrailer.Value) && this.hf_idTrailer.Value != "0")
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            if (rb_ingresoCargado.Checked == true)
            {
                ddl_tipo_carga.Enabled = true;
            }
            else
            {
                ddl_tipo_carga.Enabled = false;
                ddl_tipo_carga.ClearSelection();
                ddl_motivo.ClearSelection();
                ddl_proveedor.ClearSelection();
            }
            if (rb_ingresoCargado.Checked)
            {
                txt_tracto.Enabled = true;
                ddl_proveedor.Enabled = true;
                txt_doc.Enabled = true;
                txt_idSello.Enabled = true;
                txt_conductorRut.Enabled = true;
                txt_conductorNombre.Enabled = true;
                chk_conductorExtranjero.Enabled = true;
                txt_acomRut.Enabled = true;
                rb_posAuto.Enabled = true;
            }
            if (rb_ingresoVacio.Checked)
            {
                ddl_proveedor.SelectedValue = "0";
                txt_idSello.Text = "";
                txt_doc.Text = "";
                txt_tracto.Enabled = true;
                ddl_proveedor.Enabled = false;
                txt_doc.Enabled = false;
                txt_idSello.Enabled = false;
                txt_conductorRut.Enabled = true;
                txt_conductorNombre.Enabled = true;
                chk_conductorExtranjero.Enabled = true;
                txt_acomRut.Enabled = true;
                rb_posAuto.Enabled = false;
            }
        }
        rb_pos_CheckedChanged(sender, e);
    }
    #endregion
    #region UtilsPagina
    private void cargaDrops()
    {
        TransportistaBC tran = new TransportistaBC();
        ProveedorBC proveedor = new ProveedorBC();
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        DataTable dt = yms.ObteneSites(usuario.ID);
        utils.CargaDropNormal(dropsite, "ID", "NOMBRE", dt);
        drop_SelectedIndexChanged(null, null);
        utils.CargaDrop(ddl_proveedor, "ID", "DESCRIPCION", proveedor.obtenerTodo());
        utils.CargaDrop(ddl_transportista, "ID", "NOMBRE", tran.ObtenerTodos());
        utils.CargaDrop(ddl_motivo, "ID", "DESCRIPCION", yms.obtenerMotivoTipoCarga(ddl_tipo_carga.SelectedValue, null));
        utils.CargaDrop(ddl_tipo_carga, "ID", "DESCRIPCION", yms.obtenerTipoCarga(null, false, true));
    }
    private void limpiarTodo()
    {
        hf_idTrailer.Value = "";
        hf_idCond.Value = "";
        txt_buscarNro.Text = "";
        ddl_transportista.ClearSelection();
        ddl_tipo_carga.ClearSelection();
        txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
        txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
        txt_buscarPatente.Text = "";
        rb_propio.Checked = false;
        rb_externo.Checked = false;
        txt_buscarNro.Text = "";
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
        ddl_zona.ClearSelection();
        ddl_zona_SelectedIndexChanged(null, null);
        ddl_zona.Enabled = false;
        ddl_transportista.Enabled = false;
        //  dropsite.ClearSelection();
        ddl_proveedor.ClearSelection();
        ddl_proveedor.Enabled = false;
        rb_ingresoCargado.Checked = false;
        rb_ingresoVacio.Checked = false;
        rb_posManual.Checked = true;
        rb_posAuto.Checked = false;
        guia.Text = "";
        PNL_CITA.Visible = true;
        Pnl_guia.Visible = true;
        guia.Enabled = false;
        chk_ingresoCargado_CheckedChanged(null, null);
    }
    protected void trailercreado(object sender, EventArgs e)
    {
        this.txt_buscarPatente.Text = ((TrailerBC)(sender)).PLACA;
        this.txt_buscarNro.Text = ((TrailerBC)(sender)).NUMERO;
        this.btnBuscarTrailer_Click(null, null);
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

        //     utils.buscarArchivo

        //    file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);
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