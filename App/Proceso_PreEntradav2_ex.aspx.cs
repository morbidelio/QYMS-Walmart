using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;

public partial class App_Proceso_PreEntradav2_ex : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    UsuarioBC usuario;
    DateTime menorFecha;
    DateTime mayorFecha;
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
        if (!this.IsPostBack)
        {
            TransportistaBC tran = new TransportistaBC();
            ProveedorBC proveedor = new ProveedorBC();
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
            CreaDataTable();
            this.txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
            this.txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
            utils.CargaDrop(this.dropsite, "ID", "NOMBRE", ds);
            this.drop_SelectedIndexChanged(null, null);
            utils.CargaDrop(this.ddl_proveedor, "ID", "DESCRIPCION", proveedor.obtenerTodo());
            ddl_proveedor.SelectedValue = ((UsuarioBC)Session["Usuario"]).ID_PROVEEDOR.ToString();
            utils.CargaDrop(this.ddl_transportista, "ID", "NOMBRE", tran.ObtenerTodos());
            utils.CargaDrop(this.ddl_editTran, "ID", "NOMBRE", tran.ObtenerTodos());
            utils.CargaDrop(this.ddl_tipo_carga, "ID", "DESCRIPCION", yms.obtenerTipoCarga(null, true, false));
        }
        DataView dw = new DataView((DataTable)ViewState["listar"]);
        dw.RowFilter = "FECHA_HORA IS NOT NULL";
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
        DataView dw = new DataView((DataTable)ViewState["listar"]);
        dw.RowFilter = "FECHA_HORA IS NOT NULL";
        if (dw.Count > 0)
        {
            dw.RowFilter = string.Format("FECHA_HORA > '{0}'", DateTime.Now);
            if (dw.Count > 0)
            {
                txt_ingresoFecha.Enabled = false;
                txt_ingresoHora.Enabled = false;
                txt_ingresoHora.Text = Convert.ToDateTime(dw.ToTable().Compute("MIN(FECHA_HORA)", "")).ToShortTimeString();
            }
            else
            {
                txt_ingresoHora.Enabled = true;
                txt_ingresoFecha.Enabled = true;
            }
        }
        else
        {
            txt_ingresoHora.Enabled = true;
            txt_ingresoFecha.Enabled = true;
        }
    }
    protected void gv_seleccionados_rowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "NOSEL")
        {
            DataView dw = new DataView((DataTable)(ViewState["listar"]));
            dw.RowFilter = string.Format("NUM_CITA <> '{0}'", e.CommandArgument);
            ViewState["listar"] = dw.ToTable();
            gv_Seleccionados.DataSource = ViewState["listar"];
            gv_Seleccionados.DataBind();
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
        TrailerBC t = new TrailerBC().obtenerXPlaca(txt_editPlaca.Text);
        if (t.ID != 0)
        {
            utils.ShowMessage2(this, "agregarTrailer", "warn_placaExiste");
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
            utils.ShowMessage2(this, "buscarConductor", "warn_rutNovalido");
            return;
        }
        ConductorBC c = new ConductorBC();
        c = c.ObtenerXRut(txt_conductorRut.Text);
        if (c.ID == 0)
        {
            txt_conductorNombre.Enabled = true;
            utils.ShowMessage2(this, "buscarConductor", "success_conductorNoexiste");
            return;
        }
        if (c.BLOQUEADO)
        {
            txt_conductorNombre.Enabled = false;
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            txt_conductorRut.Focus();
            utils.ShowMessage2(this, "buscarConductor", "warn_conductorBloqueado");
            return;
        }
        txt_conductorNombre.Text = c.NOMBRE;
        txt_conductorNombre.Enabled = false;
        utils.ShowMessage2(this, "buscarConductor", "success");
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
    }
    protected void tipo_carga_SelectedIndexChanged(object sender, EventArgs e)
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();

        if (ddl_tipo_carga.SelectedValue == "0")
        {
            ddl_motivo.ClearSelection();
            ddl_motivo.Enabled = false;
        }
        else
        {
            utils.CargaDrop(this.ddl_motivo, "ID", "DESCRIPCION", yms.obtenerMotivoTipoCarga(ddl_tipo_carga.SelectedValue, null));
            ddl_motivo.Enabled = true;
        }
    }
    #endregion
    #region Buttons
    protected void btn_limpiar_Click(object sender, EventArgs e)
    {
        CreaDataTable();
        txt_buscarDoc.Text = "";
        this.txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
        this.txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
        hf_idTrailer.Value = "";
        txt_placaTrailer.Text = "";
        txt_placaTracto.Text = "";
        txt_conductorRut.Text = "";
        txt_conductorNombre.Text = "";
        txt_acomRut.Text = "";
        txt_idSello.Text = "";
        txt_obs.Text = "";
        ddl_editTran.ClearSelection();
        ddl_tipo_carga.ClearSelection();
        ddl_transportista.ClearSelection();

        txt_placaTracto.Enabled = false;
        txt_conductorRut.Enabled = false;
        chk_conductorExtranjero.Enabled = false;
        txt_conductorNombre.Enabled = false;
        txt_acomRut.Enabled = false;
        txt_idSello.Enabled = false;
        ddl_tipo_carga.Enabled = false;
        ddl_proveedor.Enabled = false;
        txt_doc2.Enabled = false;
        this.chk_vehiculoImportado.Enabled = true;
    }
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
    protected void btn_AgregarListado_Click(object sender, EventArgs e)
    {
        if (ddl_tipo_carga.SelectedIndex < 1) { utils.ShowMessage2(this, "agregarCita", "warn_tipoCargaVacio"); return; }
        if (string.IsNullOrEmpty(txt_doc2.Text.Replace(" ", ""))) { utils.ShowMessage2(this, "agregarCita", "warn_citaVacia"); return; }
        DataTable dt = (DataTable)ViewState["listar"];
        DataView dw = new DataView(dt);
        dw.RowFilter = "NUM_CITA = '" + txt_doc2.Text + "'";
        if (dw.Count > 0) { utils.ShowMessage2(this, "agregarCita", "warn_citaSeleccionada"); return; }

        try
        {
            DateTime fecha = ComprobarCita(txt_doc2.Text);
            DataRow dr = dt.NewRow();
            dr["NUM_CITA"] = txt_doc2.Text;
            dr["TIPO_CARGA_ID"] = ddl_tipo_carga.SelectedValue;
            dr["TIPO_CARGA"] = ddl_tipo_carga.SelectedItem.Text;
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
            if (fecha == DateTime.MinValue)
                dr["FECHA_HORA"] = DBNull.Value;
            else
                dr["FECHA_HORA"] = fecha;
            dt.Rows.Add(dr);
            ViewState["listar"] = dt;
            gv_Seleccionados.DataSource = ViewState["listar"];
            gv_Seleccionados.DataBind();
            ddl_tipo_carga.ClearSelection();
            ddl_motivo.ClearSelection();
            txt_doc2.Text = "";
        }
        catch (Exception EX)
        {
            utils.ShowMessage(this, EX.Message, "error", true);
        }
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
        if (string.IsNullOrEmpty(txt_placaTrailer.Text)) { utils.ShowMessage2(this, "buscarTrailer", "warn_placaVacia"); return; }
        if (!chk_vehiculoImportado.Checked && !utils.patentevalida(txt_placaTrailer.Text)) { utils.ShowMessage2(this, "buscarTrailer", "warn_placaInvalida"); return; }
        TrailerBC trailer = new TrailerBC();
        trailer = trailer.obtenerXPlaca(txt_placaTrailer.Text);
        if (trailer.ID == 0) //Trailer nuevo, no existe
        {
            TractoBC tracto = new TractoBC();
            if (txt_placaTrailer.Text != "") 
                tracto = tracto.ObtenerXPatente(txt_placaTrailer.Text);
            if (tracto.ID > 0)
                utils.ShowMessage2(this, "buscarTrailer", "warn_placaTracto");
            else
                utils.AbrirModal(this, "modalConfirmar");
            this.chk_vehiculoImportado.Enabled = false;
        }
        else //Trailer existente, trae datos
        {
            hf_idTrailer.Value = trailer.ID.ToString();
            txt_placaTrailer.Text = trailer.PLACA;
            ddl_transportista.SelectedValue = trailer.TRAN_ID.ToString();
            rb_propio.Checked = !trailer.EXTERNO;
            rb_externo.Checked = trailer.EXTERNO;
            ddl_tipo_carga.Enabled = true;

            txt_placaTracto.Enabled = true;
            txt_idSello.Enabled = true;
            txt_conductorRut.Enabled = true;
            chk_conductorExtranjero.Enabled = true;
            txt_conductorNombre.Enabled = true;
            txt_acomRut.Enabled = true;
            txt_doc2.Enabled = true;
            chk_vehiculoImportado.Enabled = false;
            txt_selloReferencia.Enabled = chk_vehiculoImportado.Checked;
            txt_conductorRut_TextChanged(null, null);
            utils.ShowMessage2(this, "buscarTrailer", "success");
        }
    }
    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_placaTracto.Text) && txt_placaTracto.Text.ToUpper() != txt_placaTrailer.Text.ToUpper())
        {
            TrailerBC t = new TrailerBC();
            t = t.obtenerXPlaca(this.txt_placaTracto.Text);
            if (t.ID > 0) { utils.ShowMessage2(this, "tracto", "warn_placaTrailer"); return; }
        }

        try
        {
            PreEntradaBC p = new PreEntradaBC();
            ConductorBC c = new ConductorBC().ObtenerXRut(txt_conductorRut.Text);
            if (c.ID == 0)
            {
                c.RUT = txt_conductorRut.Text;
                c.NOMBRE = txt_conductorNombre.Text;
                c.TRAN_ID = Convert.ToInt32(ddl_transportista.SelectedValue);
                c.COND_EXTRANJERO = chk_conductorExtranjero.Checked;
                c.ID = c.AgregarIdentity();
            }
            p.COND_ID = c.ID;
            p.RUT_CHOFER = c.RUT;
            p.NOMBRE_CHOFER = c.NOMBRE;
            p.SITE_ID = Convert.ToInt32(dropsite.SelectedValue);
            p.TRAI_ID = Convert.ToInt32(hf_idTrailer.Value);
            p.FECHA = DateTime.Parse(txt_ingresoFecha.Text + " " + txt_ingresoHora.Text);
            p.ESTADO = 1;
            p.DOC_INGRESO = txt_buscarDoc.Text;
            p.Observacion = txt_obs.Text;
            p.RUT_ACOMP = txt_acomRut.Text;
            p.extranjero = chk_vehiculoImportado.Checked;
            p.SELLO_INGRESO = txt_selloReferencia.Text;
            p.PRING_FONO = txt_ingresoFono.Text;
            if (rb_ingresoVacio.Checked)
            {
                p.CARGADO = false;
            }
            if (rb_ingresoCargado.Checked)
            {
                p.PROV_ID = Convert.ToInt32(ddl_proveedor.SelectedValue);
                p.TIIC_ID = Convert.ToInt32(ddl_tipo_carga.SelectedValue);
                p.MOIC_ID = Convert.ToInt32(ddl_motivo.SelectedValue);
                p.SELLO_CARGA = txt_idSello.Text;
                p.PATENTE_TRACTO = txt_placaTracto.Text;
                p.CARGADO = true;
            }
            DataTable table = (DataTable)(ViewState["listar"]);

            DataTable citas = table.Clone();
            foreach (DataRow drtableOld in table.Rows)
            {
                citas.ImportRow(drtableOld);
            }

            p.citas = citas;

            string error = "";

            int id;
            if (p.Crear(p, out id, out error) && error == "")
            {
                hf_id.Value = id.ToString();
                utils.ShowMessage2(this, "confirmar", "success");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "generarPDF", "generaPDF();", true);
                CreaDataTable();
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
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            if (this.rb_ingresoCargado.Checked == true)
            {
                ddl_tipo_carga.Enabled = true;
            }
            else
            {
                ddl_tipo_carga.Enabled = false;
            }
            tipo_carga_SelectedIndexChanged(null, null);

            if (this.rb_ingresoCargado.Checked)
            {
                this.txt_placaTracto.Enabled = true;
                this.txt_idSello.Enabled = true;
            }
            else
            {
                this.txt_placaTracto.Enabled = false;
                this.txt_idSello.Enabled = false;
            }
        }
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
    #endregion
    #region UtilsPagina
    private void limpiarTodo()
    {
        CreaDataTable();
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
        txt_conductorNombre.Enabled = false;
        chk_conductorExtranjero.Enabled = false;
        txt_conductorRut.Enabled = false;
        txt_acomRut.Enabled = false;
        txt_placaTracto.Enabled = false;
        txt_idSello.Enabled = false;
        txt_obs.Text = "";
        txt_buscarDoc.Text = "";
        ddl_transportista.ClearSelection();
        ddl_tipo_carga.ClearSelection();
        ddl_tipo_carga.Enabled = false;
        ddl_transportista.ClearSelection();
        ddl_transportista.Enabled = false;
    }
    protected void trailercreado(object sender, EventArgs e)
    {
        this.txt_placaTrailer.Text = ((TrailerBC)(sender)).PLACA;
        this.btnBuscarTrailer_Click(null, null);
    }
    private void CreaDataTable()
    {
        DataTable table = new DataTable();
        this.gv_Seleccionados.DataSource = null;
        this.gv_Seleccionados.DataBind();
        table.Columns.Clear();
        table.Columns.Add("NUM_CITA", typeof(String));
        table.Columns.Add("TIPO_CARGA_ID", typeof(int));
        table.Columns.Add("TIPO_CARGA", typeof(String));
        table.Columns.Add("MOTIVO_CARGA_ID", typeof(int));
        table.Columns.Add("MOTIVO_CARGA", typeof(String));
        table.Columns.Add("FECHA_HORA", typeof(DateTime));
        ViewState["listar"] = table;
    }
    private DateTime ComprobarCita(string num_cita)
    {
        DateTime fecha = DateTime.MinValue;
        DataTable dt = new PreEntradaBC().CargarCitas(num_cita);
        if (dt.Rows.Count > 0)
        {
            DataView dw = new DataView(dt);
            dw.RowFilter = string.Format("SITE_ID = {0}", dropsite.SelectedValue);
            if (dw.Count == 0) { throw new Exception("Site no válido"); }
            dw.RowFilter += string.Format(" AND PROV_ID = {0}", usuario.ID_PROVEEDOR);
            if (dw.Count == 0) { throw new Exception("Proveedor no válido"); }
            dw.RowFilter += string.Format(" AND TIIC_ID = {0}", ddl_tipo_carga.SelectedValue);
            if (dw.Count == 0) { throw new Exception("Tipo carga no válido"); }
            dw.RowFilter += " AND PRING_ID IS NULL AND (INGRESADO = 0 OR INGRESADO IS NULL)";
            if (dw.Count == 0) { throw new Exception("N° Cita ya ingresado"); }
            else 
            {
                fecha = (DateTime)dw.ToTable().Rows[0]["FECHA_HORA"];
                if (fecha < DateTime.Now.AddDays(-utils.Intervalo_preingreso)) { throw new Exception(string.Format("Cita expirada. Fecha cita: {0}", fecha)); }
                if (fecha.Date > mayorFecha || fecha.Date < menorFecha) { throw new Exception(string.Format("El intervalo entre fechas no puede superar los {0} días. Fecha cita: {1}.", utils.Intervalo_preingreso, fecha)); }
            }
        }
        return fecha;
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

}