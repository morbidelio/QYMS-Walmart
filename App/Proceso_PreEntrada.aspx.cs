using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;

public partial class App_Proceso_PreEntrada : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();


    protected void Page_Load(object sender, EventArgs e)
    {
        this.nuevo_trailer.ButtonClickDemo += new EventHandler(trailercreado);
        if (!this.IsPostBack)
        {
            //     Page.LoadComplete += new EventHandler(Page_LoadComplete);
            this.txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
            this.txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
            TransportistaBC tran = new TransportistaBC();
            ProveedorBC proveedor = new ProveedorBC();

            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
            utils.CargaDropNormal(this.dropsite, "ID", "NOMBRE", ds);
            this.drop_SelectedIndexChanged(null, null);

            LugarBC lugar = new LugarBC();
            utils.CargaDrop(this.ddl_proveedor, "ID", "DESCRIPCION", proveedor.obtenerTodo());
            utils.CargaDrop(this.ddl_transportista, "ID", "NOMBRE", tran.ObtenerTodos());
            utils.CargaDrop(this.ddl_tipo_carga, "ID", "DESCRIPCION", yms.obtenerTipoCarga(null, true, false));
        }
    }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        // if (txt_buscarPatente.Text!="" && ) btnBuscarTrailer_Click(null,null);
    }

    #region TextBox
    protected void txt_conductorRut_TextChanged(object sender, EventArgs e)
    {
        if (txt_conductorRut.Text == "")
        {
            txt_conductorNombre.Enabled = true;
            return;
        }

        if (utils.validarRut(txt_conductorRut.Text))
        {
            ConductorBC c = new ConductorBC();
            c = c.ObtenerXRut(utils.formatearRut(txt_conductorRut.Text));
            if (c.ID != 0)
            {
                if (c.BLOQUEADO)
                {
                    txt_conductorNombre.Enabled = false;
                    hf_idCond.Value = "";
                    txt_conductorRut.Text = "";
                    txt_conductorNombre.Text = "";
                    txt_conductorRut.Focus();
                    utils.ShowMessage2(this, "buscarConductor", "warn_conductorBloqueado");
                    return;
                }
                txt_conductorNombre.Text = c.NOMBRE;
                txt_conductorNombre.Enabled = false;
                hf_idCond.Value = c.ID.ToString();
                txt_acomRut.Focus();
                utils.ShowMessage2(this, "buscarConductor", "success");
            }
            else
            {
                txt_conductorNombre.Enabled = true;
                hf_idCond.Value = "";
                txt_conductorNombre.Focus();
                utils.ShowMessage2(this, "buscarConductor", "warn_conductorNoexiste");
            }
        }
        else
        {
            txt_conductorNombre.Enabled = false;
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            txt_conductorRut.Focus();
            utils.ShowMessage2(this, "buscarConductor", "warn_rutNovalido");
        }
    }
    #endregion
    #region DropDownList
    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        SiteBC s = new SiteBC();
        s = s.ObtenerXId(Convert.ToInt32(dropsite.SelectedValue));
        lbl_site.Text = "CD " + s.DESCRIPCION;
        //YMS_ZONA_BC yms = new YMS_ZONA_BC();
        string tipo_zona;
        if (this.rb_ingresoCargado.Checked == true)
        {
            tipo_zona = "200";
        }
        else
        {
            tipo_zona = "100";
        }

        if (txt_placaTrailer.Text != "" && txt_placaTrailer.Text != null)
            this.btnBuscarTrailer_Click(null, null);
    }
    #endregion
    #region Buttons
    protected void btn_nuevoTrailer_Click(object sender, EventArgs e)
    {
        nuevo_trailer.limpiarForm();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalEditarTrailer();", true);
    }
    protected void btnBuscarTrailer_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        if (this.txt_buscarNro.Text != "" || this.txt_placaTrailer.Text != "")
        {
            if (this.txt_buscarNro.Text != "")
                trailer = trailer.obtenerXNro(txt_buscarNro.Text);
            else if (utils.patentevalida(txt_placaTrailer.Text) == true)
                trailer = trailer.obtenerXPlaca(txt_placaTrailer.Text);
            else
            {
                this.hf_idTrailer.Value = "0";
                this.ddl_transportista.ClearSelection();
                this.txt_conductorRut.Text = "";
                this.txt_conductorNombre.Text = "";
                this.txt_acomRut.Text = "";
                this.txt_buscarNro.Text = "";
                this.ddl_proveedor.ClearSelection();
                this.rb_externo.Checked = false;
                this.rb_propio.Checked = false;
                this.ddl_transportista.Enabled = true;
                this.txt_conductorRut.Enabled = true;
                this.txt_conductorNombre.Enabled = true;
                this.txt_acomRut.Enabled = true;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Patente Invalida.');", true);
            }

            if (trailer.ID == 0 || trailer.ID == null) //Trailer nuevo, no existe
            {
                this.hf_idTrailer.Value = "0";
                this.ddl_transportista.ClearSelection();
                this.txt_conductorRut.Text = "";
                this.txt_conductorNombre.Text = "";
                this.txt_acomRut.Text = "";
                this.txt_buscarNro.Text = "";
                this.ddl_proveedor.ClearSelection();
                this.rb_externo.Checked = false;
                this.rb_propio.Checked = false;
                this.ddl_transportista.Enabled = true;
                this.txt_conductorRut.Enabled = true;
                this.txt_conductorNombre.Enabled = true;
                this.txt_acomRut.Enabled = true;
                TractoBC tracto = new TractoBC();
                if (txt_placaTrailer.Text!="") tracto=tracto.ObtenerXPatente(txt_placaTrailer.Text);
                if (tracto.ID>0)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Placa Corresponde a tracto');", true);
                else
                     ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer no existe en la base de datos. Debe Crearlo.');", true);



            }
            else //Trailer existente, trae datos
            {
                if (trailer.SITE_IN)
                {
                    if (trailer.SITE_ID == Convert.ToInt32(this.dropsite.SelectedValue))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer ya registrado en el CD. No es posible reingresar');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer registrado en otro CD. No es posible reingresar');", true);
                    }
                    this.txt_placaTracto.Enabled = false;
                    this.ddl_transportista.Enabled = false;
                    this.txt_conductorRut.Enabled = false;
                    this.txt_conductorNombre.Enabled = false;
                    this.txt_acomRut.Enabled = false;
                }
                else
                {
                    this.hf_idTrailer.Value = trailer.ID.ToString();
                    this.txt_placaTrailer.Text = trailer.PLACA;
                    this.txt_buscarNro.Text = trailer.NUMERO;
                    this.ddl_transportista.SelectedValue = trailer.TRAN_ID.ToString();
                    if (trailer.EXTERNO)
                    {
                        this.rb_propio.Checked = false;
                        this.rb_externo.Checked = true;
                    }
                    else
                    {
                        this.rb_propio.Checked = true;
                        this.rb_externo.Checked = false;
                    }
                    this.txt_placaTracto.Enabled = false;
                    this.ddl_transportista.Enabled = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Se cargaron los datos del trailer seleccionado.');", true);

                    if (this.rb_ingresoCargado.Checked)
                    {
                        this.txt_placaTracto.Enabled = true;
                        this.ddl_proveedor.Enabled = true;
                        this.txt_doc.Enabled = true;
                        this.txt_idSello.Enabled = true;
                    }
                    else
                    {
                        this.txt_placaTracto.Enabled = false;
                        this.ddl_proveedor.Enabled = false;
                        this.txt_doc.Enabled = false;
                        this.txt_idSello.Enabled = false;
                    }
                    this.txt_conductorRut.Enabled = true;
                    this.txt_conductorNombre.Enabled = true;
                    this.txt_acomRut.Enabled = true;
                }
                txt_conductorRut_TextChanged(null, null);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe ingresar un numero de flota o patente');", true);
            this.hf_idTrailer.Value = "";
        }
    }
    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.hf_idTrailer.Value == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje2", "alert('Debe ingresar Trailer');", true);
                limpiarTodo();
            }
            else
            {
                ConductorBC c = new ConductorBC();
                if (string.IsNullOrEmpty(hf_idCond.Value))
                {
                    c.RUT = utils.formatearRut(txt_conductorRut.Text);
                    c.NOMBRE = txt_conductorNombre.Text;
                    c.TRAN_ID = Convert.ToInt32(ddl_transportista.SelectedValue);
                    c.COND_EXTRANJERO = chk_conductorExtranjero.Checked;
                    hf_idCond.Value = c.AgregarIdentity().ToString();
                }
                PreEntradaBC p = new PreEntradaBC();
                p.SITE_ID = Convert.ToInt32(dropsite.SelectedValue);
                p.TRAI_ID = Convert.ToInt32(hf_idTrailer.Value);
                p.FECHA = DateTime.Parse(txt_ingresoFecha.Text + " " + txt_ingresoHora.Text);
                p.ESTADO = 1;
                p.DOC_INGRESO = "";
                p.RUT_CHOFER = utils.formatearRut(txt_conductorRut.Text);
                p.NOMBRE_CHOFER = txt_conductorNombre.Text;
                p.RUT_ACOMP = txt_acomRut.Text;
                p.COND_ID = Convert.ToInt32(hf_idCond.Value);
                if (rb_ingresoVacio.Checked)
                {
                    p.SELLO_INGRESO = txt_idSello.Text;
                    p.CARGADO = false;
                }
                if (utils.patentevalida(this.txt_placaTracto.Text) == true)
                {
                    TrailerBC trailer_tracto= new TrailerBC();

                    trailer_tracto = trailer_tracto.obtenerXPlaca(this.txt_placaTracto.Text);

                    if (trailer_tracto.ID > 0 && trailer_tracto.ID!=p.TRAI_ID )
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Tracto corresponde a trailer');", true);

                    }

                    else
                    {

                        if (rb_ingresoCargado.Checked)
                        {
                            p.PROV_ID = Convert.ToInt32(ddl_proveedor.SelectedValue);
                            p.TIIC_ID = Convert.ToInt32(ddl_tipo_carga.SelectedValue);
                            p.MOIC_ID = Convert.ToInt32(ddl_motivo.SelectedValue);
                            p.SELLO_CARGA = txt_idSello.Text;
                            p.PATENTE_TRACTO = txt_placaTracto.Text;
                            p.CARGADO = true;
                        }
                        int id;
                        string error;
                        if (p.Crear(p,out id,out error))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Ingreso correcto.');", true);
                            limpiarTodo();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');",error), true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + "patente de Tracto Invalida" + "');", true);

                }


            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error! No se pudo ingresar los datos.');", true);
        }
    }
    protected void btn_limpiar_Click(object sender, EventArgs e)
    {
        hf_idTrailer.Value = "";
        hf_idCond.Value = "";
        this.txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
        this.txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
        txt_placaTracto.Text = "";
        txt_placaTrailer.Text = "";
        txt_conductorRut.Text = "";
        txt_conductorNombre.Text = "";
        txt_acomRut.Text = "";
        txt_buscarNro.Text = "";
        txt_idSello.Text = "";
        txt_obs.Text = "";
        txt_doc.Text = "";
        ddl_transportista.ClearSelection();
        ddl_tipo_carga.ClearSelection();
        ddl_proveedor.ClearSelection();

        txt_placaTracto.Enabled = false;
        txt_conductorRut.Enabled = false;
        txt_conductorNombre.Enabled = false;
        txt_acomRut.Enabled = false;
        txt_buscarNro.Enabled = false;
        txt_idSello.Enabled = false;
        txt_doc.Enabled = false;
        ddl_transportista.Enabled = false;
        ddl_proveedor.Enabled = false;
    }
    #endregion
    #region CheckBox
    protected void chk_ingresoCargado_CheckedChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(hf_idTrailer.Value))
        {
            //    this.drop_SelectedIndexChanged(null, null);
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            string tipo_zona;
            if (this.rb_ingresoCargado.Checked == true)
            {
                tipo_zona = "200";
                ddl_tipo_carga.Enabled = true;
            }
            else
            {
                tipo_zona = "100";
                ddl_tipo_carga.Enabled = false;
            }
            tipo_carga_SelectedIndexChanged(null, null);

            if (this.rb_ingresoCargado.Checked)
            {
                this.txt_placaTracto.Enabled = true;
                this.ddl_proveedor.Enabled = true;
                this.txt_doc.Enabled = true;
                this.txt_idSello.Enabled = true;
            }
            else
            {
                this.txt_placaTracto.Enabled = false;
                this.ddl_proveedor.Enabled = false;
                this.txt_doc.Enabled = false;
                this.txt_idSello.Enabled = false;
            }
        }
    }
    protected void tipo_carga_SelectedIndexChanged(object sender, EventArgs e)
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();

        if (ddl_tipo_carga.SelectedValue == "0")
        {
            ddl_motivo.Items.Clear();
            ddl_motivo.Enabled = false;
        }
        else
        {
            utils.CargaDrop(this.ddl_motivo, "ID", "DESCRIPCION", yms.obtenerMotivoTipoCarga(ddl_tipo_carga.SelectedValue, null));

            ddl_motivo.Enabled = true;
        }
    }
    #endregion
    #region UtilsPagina
    private void limpiarTodo()
    {
        hf_idTrailer.Value = "";
        txt_ingresoFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txt_ingresoHora.Text = DateTime.Now.ToString("hh:mm");
        txt_placaTrailer.Text = "";
        rb_propio.Checked = false;
        rb_externo.Checked = false;
        txt_buscarNro.Text = "";
        txt_conductorNombre.Text = "";
        txt_conductorRut.Text = "";
        txt_acomRut.Text = "";
        txt_placaTracto.Text = "";
        txt_doc.Text = "";
        txt_idSello.Text = "";
        txt_buscarNro.Enabled = false;
        txt_conductorNombre.Enabled = false;
        txt_conductorRut.Enabled = false;
        txt_acomRut.Enabled = false;
        txt_placaTracto.Enabled = false;
        txt_doc.Enabled = false;
        txt_idSello.Enabled = false;
        txt_obs.Text = "";
        ddl_transportista.Enabled = false;
      //  dropsite.ClearSelection();
        ddl_proveedor.ClearSelection();
        rb_ingresoCargado.Checked = false;
        chk_ingresoCargado_CheckedChanged(null, null);
    }
    protected void trailercreado(object sender, EventArgs e)
    {
        this.txt_placaTrailer.Text = ((TrailerBC)(sender)).PLACA;
        this.txt_buscarNro.Text = ((TrailerBC)(sender)).NUMERO;
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