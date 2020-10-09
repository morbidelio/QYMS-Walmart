using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;

public partial class App_Proceso_PreEntradav3_ex : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    UsuarioBC usuario;
    DateTime menorFecha = DateTime.Now.Date.AddDays(-utils.Intervalo_preingreso);        //Menor fecha cita permitida
    DateTime mayorFecha = DateTime.Now.Date.AddDays(utils.Intervalo_preingreso);        //Mayor fecha cita permitida
    private TransportistaBC llenarTransportista()
    {
        TransportistaBC transportista = new TransportistaBC();
        transportista.RUT = txt_editRutTran.Text.Replace(".", "");
        transportista.NOMBRE = txt_editNombre.Text;
        transportista.ROL = Convert.ToInt32(txt_editRol.Text);
        return transportista;
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["Usuario"] != null)
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)Session["Usuario"];

            if (usuario.TIPO != "Proveedor")
            {
                Response.Redirect("~/inicioQYMS2.aspx");
            }
        }
        else
        {
            Response.Redirect("~/inicioQYMS2.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("~/InicioQYMS2.aspx");
        usuario = (UsuarioBC)Session["usuario"];
        DataTable dt;
        DataView dw = new DataView((DataTable)ViewState["sel"]);
        if (!this.IsPostBack)
        {
            TransportistaBC tran = new TransportistaBC();
            ProveedorBC proveedor = new ProveedorBC();
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            dt = yms.ObteneSites(usuario.ID);
            utils.CargaDrop(this.dropsite, "ID", "NOMBRE", dt);
            this.drop_SelectedIndexChanged(null, null);
            dt = proveedor.obtenerTodo();
            utils.CargaDrop(this.ddl_proveedor, "ID", "DESCRIPCION", dt);
            dt = tran.ObtenerTodos();
            utils.CargaDrop(this.ddl_transportista, "ID", "NOMBRE", dt);
            utils.CargaDrop(this.ddl_editTran, "ID", "NOMBRE", dt);
            ddl_proveedor.SelectedValue = usuario.ID_PROVEEDOR.ToString();
            txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
            txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
       //     ObtenerCitas(true);
        }
        if (dw.Count > 0)
        {
            menorFecha = Convert.ToDateTime(dw.ToTable().Compute("MAX(FECHA_HORA)", "")).Date.AddDays(-utils.Intervalo_preingreso);
            mayorFecha = Convert.ToDateTime(dw.ToTable().Compute("MIN(FECHA_HORA)", "")).Date.AddDays(utils.Intervalo_preingreso);
        }
        else
        {
            menorFecha = DateTime.Now.Date.AddDays(-utils.Intervalo_preingreso);
            mayorFecha = DateTime.Now.Date.AddDays(utils.Intervalo_preingreso);
        }
    }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        DataView dw = new DataView((DataTable)ViewState["sel"]);
        dw.RowFilter = string.Format("FECHA_HORA > '{0}'", DateTime.Now);
        if (dw.Count > 0)
        {
            txt_ingresoFecha.Enabled = false;
            txt_ingresoHora.Enabled = false;
            txt_ingresoHora.Text = Convert.ToDateTime(dw.ToTable().Compute("MIN(FECHA_HORA)", "")).ToShortTimeString();
        }
        else
        {
            txt_ingresoFecha.Enabled = true;
            txt_ingresoHora.Enabled = true;
        }
    }
    protected void gv_noSeleccionados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SEL")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable no_sel = (DataTable)ViewState["no_sel"];
            DataTable sel = (DataTable)ViewState["sel"];
            DataRow dr = sel.NewRow();
            dr["NUM_CITA"] = no_sel.Rows[index]["NUM_CITA"];
            dr["TIPO_CARGA_ID"] = no_sel.Rows[index]["TIIC_ID"];
            dr["TIPO_CARGA"] = no_sel.Rows[index]["TIPO_CARGA"];
            if (ddl_motivo.SelectedIndex < 1)
            {
                dr["MOTIVO_CARGA_ID"] = DBNull.Value;
                dr["MOTIVO_CARGA"] = DBNull.Value;
            }
            else
            {
                dr["MOTIVO_CARGA_ID"] = ddl_motivo.SelectedValue;
                dr["MOTIVO_CARGA"] = ddl_motivo.SelectedItem.Text;
            }
            dr["FECHA_HORA"] = no_sel.Rows[index]["FECHA_HORA"];
            sel.Rows.Add(dr);
            ViewState["sel"] = sel;
            ObtenerCitas(false);
        }
    }
    protected void gv_seleccionados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DESEL")
        {
            DataView dw= new DataView((DataTable)ViewState["sel"]);
            dw.RowFilter = string.Format("Num_Cita <> {0}", e.CommandArgument);
            ViewState["sel"] = dw.ToTable();
            ObtenerCitas(false);
        }
    }
    protected void gv_noSeleccionados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            bool ingresado = (bool)DataBinder.Eval(e.Row.DataItem, "INGRESADO");
            DateTime horacita = (DateTime)DataBinder.Eval(e.Row.DataItem, "FECHA_HORA");
            if (!ingresado)
            {
                ((LinkButton)e.Row.FindControl("btn_sel")).Style.Add("visibility", "visible");
            }
            else
            {
                ((LinkButton)e.Row.FindControl("btn_sel")).Style.Add("visibility", "hidden");
            }
            if (horacita.Date > mayorFecha ||
                horacita.Date < menorFecha)
            {
                ((LinkButton)e.Row.FindControl("btn_sel")).Enabled = false;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("btn_sel")).Enabled = true;
            }
        }
    }
    #region TextBox
    protected void txt_editRutTran_TextChanged(object sender, EventArgs e)
    {
        if (!utils.validarRut(txt_editRutTran.Text)) 
        { 
            utils.ShowMessage2(this, "crearTransportista", "warn_rutInvalido"); 
            txt_editRutTran.Text = "";
            txt_editRutTran.Focus(); 
        }
        else
        {
            txt_editNombre.Focus();
        }
    }
    protected void txt_editPlaca_TextChanged(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        t = t.obtenerXPlaca(txt_editPlaca.Text);
        if (t.ID != 0)
        {
            utils.ShowMessage2(this, "agregarTrailer", "warn_placaExiste");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer ya existe en la base de datos!');", true);
            txt_editPlaca.Text = "";
            txt_editPlaca.Focus();
        }
    }
    protected void txt_buscarDoc_TextChanged(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        trailer = trailer.obtenerXDoc(txt_buscarDoc.Text, dropsite.SelectedValue);
        if (trailer.ID != 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Documento ya existente.');", true);
            txt_buscarDoc.Text = "";
            txt_buscarDoc.Focus();
        }
    }
    protected void txt_conductorRut_TextChanged(object sender, EventArgs e)
    {
        if (txt_conductorRut.Text == "")
        {
            txt_conductorNombre.Enabled = true;
            return;
        }

        if (!chk_conductorExtranjero.Checked && !utils.validarRut(this.txt_conductorRut.Text))
        {
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
            txt_conductorNombre.Enabled = true;
            utils.ShowMessage2(this, "conductor", "success_conductorNoexiste");
            return;
        }
        if (c.BLOQUEADO)
        {
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            txt_conductorNombre.Enabled = false;
            utils.ShowMessage2(this, "conductor", "warn_conductorBloqueado");
            return;
        }
        txt_conductorNombre.Text = c.NOMBRE;
        txt_conductorNombre.Enabled = false;
        chk_conductorExtranjero.Checked = c.COND_EXTRANJERO;
        utils.ShowMessage2(this, "conductor", "success");
    }
    protected void txt_ingresoFecha_TextChanged(object sender, EventArgs e)
    {
        ObtenerCitas(false);
    }
    #endregion
    #region DropDownList
    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        SiteBC s = new SiteBC();
        s = s.ObtenerXId(Convert.ToInt32(dropsite.SelectedValue));
        lbl_site.Text = "CD " + s.DESCRIPCION;

        if (!string.IsNullOrEmpty(txt_placaTrailer.Text))
            this.btnBuscarTrailer_Click(null, null);

              ObtenerCitas(true);
    }
    protected void ddl_tipo_carga_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerCitas(false);
    }
    #endregion
    #region Buttons
    protected void btn_tranGrabar_Click(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(txt_editRutTran.Text)){ utils.ShowMessage2(this, "crearTransportista", "warn_RutVacio"); return; }
        if (string.IsNullOrEmpty(txt_editNombre.Text)) { utils.ShowMessage2(this, "crearTransportista", "warn_NombreVacio"); return; }
        if(string.IsNullOrEmpty(txt_editRol.Text)){ utils.ShowMessage2(this, "crearTransportista", "warn_RolVacio"); return; }

        TransportistaBC transportista = llenarTransportista();
        int id;
        if (transportista.Crear(out id))
        {
            utils.ShowMessage2(this, "crearTransportista", "success");
        }
        else
        {
            utils.ShowMessage2(this, "crearTransportista", "error");
        }
        TransportistaBC tran = new TransportistaBC();
        utils.CargaDrop(this.ddl_transportista, "ID", "NOMBRE", tran.ObtenerTodos());
        ddl_transportista.SelectedValue = id.ToString();
        utils.CerrarModal(this, "modalTransportista");
    }
    protected void btn_limpiar_Click(object sender, EventArgs e)
    {
        limpiarTodo();
    }
    protected void generaPDF(object sender, EventArgs e)
    {
        ReportBC r = new ReportBC();
        DataTable dt = r.CargarPreEntradaDT(Convert.ToInt32(hf_id.Value));
        DataColumn codigobarras = new DataColumn("DOC_IMPRESION");
        codigobarras.DataType = typeof(byte[]);
        dt.Columns.Add(codigobarras);
        Barcode_BC barcode = new Barcode_BC("code128", 005, dt.Rows[0]["DOC_INGRESO_BARRAS"].ToString());
        dt.Columns["DOC_IMPRESION"].ReadOnly = false;
        dt.Rows[0]["DOC_IMPRESION"] = barcode.Byte;

        DataColumn codigobarras2 = new DataColumn("DOC_PATENTE");
        codigobarras2.DataType = typeof(byte[]);
        dt.Columns.Add(codigobarras2);
        Barcode_BC barcode2 = new Barcode_BC("CODE_39", 003, dt.Rows[0]["TRAI_PLACA"].ToString().ToUpper());
        dt.Columns["DOC_PATENTE"].ReadOnly = false;
        dt.Rows[0]["DOC_PATENTE"] = barcode2.Byte;

        ReportDataSource dataSource = new ReportDataSource("DataSet1", dt);
        limpiarTodo();
       
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(dataSource);

        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        //Word
        byte[] bytes = ReportViewer1.LocalReport.Render(
            "PDF", null, out mimeType, out encoding,
            out extension,
            out streamids, out warnings);
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=Doc_PreIngreso_" + txt_buscarDoc.Text + '.' + extension);

        Response.BinaryWrite(bytes);

        Response.End();
    }
    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_editPlaca.Text)) { utils.ShowMessage2(this, "crearTrailer", "warn_placaVacia"); return; }
     //   if (ddl_editTran.SelectedIndex < 1) { utils.ShowMessage2(this, "crearTrailer", "warn_tranVacio"); return; }
        TrailerBC t = new TrailerBC();
        t.PLACA = txt_editPlaca.Text;
      //  t.TRAN_ID = Convert.ToInt32(ddl_editTran.SelectedValue);
        if (t.CrearGenerico(t, chk_vehiculoImportado.Checked))
        {
            this.hf_idTrailer.Value = t.ID.ToString();
            this.txt_placaTrailer.Text = t.PLACA;
            this.ddl_transportista.SelectedValue = t.TRAN_ID.ToString();
            btnBuscarTrailer_Click(null, null);
            utils.ShowMessage2(this, "crearTrailer", "success");
            utils.CerrarModal(this, "modalTrailer");
        }
        else
        {
            utils.ShowMessage2(this, "crearTrailer", "error");
        }
    }
    protected void btn_Conf_Click(object sender, EventArgs e)
    {
        txt_editPlaca.Text = txt_placaTrailer.Text;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalTrailer();", true);
    }
    protected void btnBuscarTrailer_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        trailer = trailer.obtenerXPlaca(txt_placaTrailer.Text);
        if (trailer.ID == 0) //Trailer nuevo, no existe
        {
            TractoBC tracto = new TractoBC();
            if (txt_placaTrailer.Text != "") 
                tracto = tracto.ObtenerXPatente(txt_placaTrailer.Text);
            if (tracto.ID > 0)
                utils.ShowMessage2(this, "trailer", "warn_placaTracto");
            else
                utils.AbrirModal(this, "modalConfirmar");
            chk_vehiculoImportado.Enabled = false;
        }
        else //Trailer existente, trae datos
        {
            hf_idTrailer.Value = trailer.ID.ToString();
            txt_placaTrailer.Text = trailer.PLACA;
            ddl_transportista.SelectedValue = trailer.TRAN_ID.ToString();
            rb_propio.Checked = !trailer.EXTERNO;
            rb_externo.Checked = trailer.EXTERNO;
            txt_placaTracto.Enabled = true;
            txt_idSello.Enabled = true;
            txt_conductorRut.Enabled = true;
            chk_conductorExtranjero.Enabled = true;
            txt_conductorNombre.Enabled = true;
            txt_acomRut.Enabled = true;
            chk_vehiculoImportado.Enabled = false;
            txt_selloReferencia.Enabled = chk_vehiculoImportado.Checked;
            txt_conductorRut_TextChanged(null, null);
            utils.ShowMessage2(this, "trailer", "success");
        }
    }
    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        if (((DataTable)ViewState["sel"]).Rows.Count < 1) 
        { 
            utils.ShowMessage2(this, "confirmar", "warn_docVacio"); 
            return; 
        }
        if (!string.IsNullOrEmpty(txt_placaTracto.Text) && txt_placaTracto.Text.ToUpper() != txt_placaTrailer.Text.ToUpper())
        {
            TrailerBC t = new TrailerBC().obtenerXPlaca(txt_placaTracto.Text);
            if (t.ID != 0) { utils.ShowMessage2(this, "tracto", "warn_placaTrailer"); return; }
        }

        try
        {
            PreEntradaBC p = new PreEntradaBC();
            p.CONDUCTOR = new ConductorBC().ObtenerXRut(txt_conductorRut.Text);
            if (p.CONDUCTOR.ID == 0)
            {
                p.CONDUCTOR.RUT = txt_conductorRut.Text;
                p.CONDUCTOR.NOMBRE = txt_conductorNombre.Text;
                p.CONDUCTOR.TRAN_ID = Convert.ToInt32(ddl_transportista.SelectedValue);
                p.CONDUCTOR.COND_EXTRANJERO = chk_conductorExtranjero.Checked;
                p.CONDUCTOR.ID = p.CONDUCTOR.AgregarIdentity();
            }
            p.COND_ID = p.CONDUCTOR.ID;
            p.RUT_CHOFER = p.CONDUCTOR.RUT;
            p.NOMBRE_CHOFER = p.CONDUCTOR.NOMBRE;
            p.SITE_ID = Convert.ToInt32(dropsite.SelectedValue);
            p.TRAI_ID = Convert.ToInt32(hf_idTrailer.Value);
            p.FECHA = DateTime.Parse(txt_ingresoFecha.Text + " " + txt_ingresoHora.Text);
            p.ESTADO = 1;
            p.DOC_INGRESO = txt_buscarDoc.Text;
            p.Observacion = txt_obs.Text;
            p.RUT_ACOMP = txt_acomRut.Text;
            p.CARGADO = rb_ingresoCargado.Checked;
            p.SELLO_INGRESO = txt_selloReferencia.Text;
            p.extranjero = chk_vehiculoImportado.Checked;
            p.PRING_FONO = txt_ingresoFono.Text;
            if (rb_ingresoCargado.Checked)
            {
                p.SELLO_CARGA = txt_idSello.Text;
                p.PROV_ID = Convert.ToInt32(ddl_proveedor.SelectedValue);
                p.PATENTE_TRACTO = txt_placaTracto.Text;
            }
            p.citas = (DataTable)ViewState["sel"];
            string error = "";

            int id;
     
            if (p.CrearV2(p, out id, out error) && error == "")
            {
                hf_id.Value = id.ToString();
                utils.ShowMessage2(this, "confirmar", "success");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "generarPDF", "generaPDF();", true);
                limpiarTodo();
                ObtenerCitas(true);
            }
            else
            {
                utils.ShowMessage(this, error, "error", false);
            }
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
    }
    #endregion
    #region CheckBox
    protected void chk_ingresoCargado_CheckedChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(hf_idTrailer.Value))
        {
            txt_placaTracto.Enabled = rb_ingresoCargado.Checked;
            txt_idSello.Enabled = rb_ingresoCargado.Checked;
        }
    }
    #endregion
    #region UtilsPagina
    private void limpiarTodo()
    {
        hf_idTrailer.Value = "";
        txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
        txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
        txt_placaTrailer.Text = "";
        rb_propio.Checked = false;
        rb_externo.Checked = false;
        txt_conductorNombre.Text = "";
        txt_conductorRut.Text = "";
        txt_acomRut.Text = "";
        txt_placaTracto.Text = "";
        txt_idSello.Text = "";
        txt_selloReferencia.Text = "";
        txt_obs.Text = "";
        txt_buscarDoc.Text = "";
        txt_selloReferencia.Text = "";
        txt_conductorNombre.Enabled = false;
        chk_conductorExtranjero.Enabled = false;
        txt_conductorRut.Enabled = false;
        txt_acomRut.Enabled = false;
        txt_placaTracto.Enabled = false;
        txt_idSello.Enabled = false;
        txt_selloReferencia.Enabled = false;
        ddl_transportista.ClearSelection();
        ddl_transportista.Enabled = false;
        chk_vehiculoImportado.Enabled = true;
        ObtenerCitas(true);
    }
    private void ObtenerCitas(bool forzarBD)
    {
        if (ViewState["no_sel"] == null || forzarBD)
        {
            PreEntradaBC p = new PreEntradaBC();
            ViewState["no_sel"] = p.CargarCitas("", int.Parse(dropsite.SelectedValue), usuario.ID_PROVEEDOR);
            DataTable dt = new DataTable();
            dt.Columns.Add("NUM_CITA");
            dt.Columns.Add("TIPO_CARGA_ID");
            dt.Columns.Add("TIPO_CARGA");
            dt.Columns.Add("MOTIVO_CARGA_ID");
            dt.Columns.Add("MOTIVO_CARGA");
            dt.Columns.Add("FECHA_HORA");
            ViewState["sel"] = dt;
        }
        DataView no_sel = new DataView((DataTable)ViewState["no_sel"]);
        DataTable sel = (DataTable)ViewState["sel"];
        if (sel.Rows.Count > 0)
        {
            string nros = "";
            foreach (DataRow dr in sel.Rows)
            {
                if (!string.IsNullOrEmpty(nros))
                    nros += ",";
                nros += string.Format("'{0}'", dr["Num_Cita"]);
            }
            menorFecha = Convert.ToDateTime(sel.Compute("MAX(FECHA_HORA)", "")).Date.AddDays(-utils.Intervalo_preingreso);
            mayorFecha = Convert.ToDateTime(sel.Compute("MIN(FECHA_HORA)", "")).Date.AddDays(utils.Intervalo_preingreso);
            no_sel.RowFilter = string.Format("Num_Cita NOT IN ({0})", nros);
        }

        if (dropsite.SelectedValue != "0")
        {
            gv_noSeleccionados.DataSource = no_sel.ToTable();
            gv_noSeleccionados.DataBind();
            gv_Seleccionados.DataSource = ViewState["sel"];
            gv_Seleccionados.DataBind();
        }
        else
        {
            gv_noSeleccionados.DataSource = null;
            gv_noSeleccionados.DataBind();
            gv_Seleccionados.DataSource = null;
            gv_Seleccionados.DataBind();
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