// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class App_Proceso_PreEntradav2 : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    //DataTable table = new DataTable();
    
    private TransportistaBC llenarTransportista()
    {
        TransportistaBC transportista = new TransportistaBC();
        transportista.RUT = this.txt_editRut.Text.Replace(".", "");
        transportista.NOMBRE = this.txt_editNombre.Text;
        //transportista.PASS = txt_editPass.Text;
        transportista.ROL = Convert.ToInt32(this.txt_editRol.Text);
        return transportista;
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (this.Session["Usuario"] != null)
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)this.Session["Usuario"];

            if (usuario.TIPO != "Proveedor")
            {
                this.Response.Redirect("~/inicioQYMS2.aspx");
            }
        }
        else
        {
            this.Response.Redirect("~/inicioQYMS2.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.nuevo_trailer.ButtonClickDemo += new EventHandler(trailercreado);
        if (!this.IsPostBack)
        { 
            this.CreaDataTable();

            //     Page.LoadComplete += new EventHandler(Page_LoadComplete);
            this.txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
            this.txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
            TransportistaBC tran = new TransportistaBC();
            ProveedorBC proveedor = new ProveedorBC();
            CargaTipoBC c = new CargaTipoBC();
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds = yms.ObteneSites(((UsuarioBC)this.Session["Usuario"]).ID);
            utils.CargaDropNormal(this.dropsite, "ID", "NOMBRE", ds);
            this.drop_SelectedIndexChanged(null, null);

            LugarBC lugar = new LugarBC();
            utils.CargaDrop(this.ddl_proveedor, "ID", "DESCRIPCION", proveedor.obtenerTodo());
            this.ddl_proveedor.SelectedValue = ((UsuarioBC)this.Session["Usuario"]).ID_PROVEEDOR.ToString();
            //  utils.CargaDrop(ddl_zona, "ID", "DESCRIPCION", zona.ObtenerTodas());
            utils.CargaDrop(this.ddl_transportista, "ID", "NOMBRE", tran.ObtenerTodos());
            utils.CargaDrop(this.ddl_editTran, "ID", "NOMBRE", tran.ObtenerTodos());
            utils.CargaDrop(this.ddl_tipo_carga, "ID", "DESCRIPCION", yms.obtenerTipoCarga(null, true, false));
        }
    }
    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        // if (txt_buscarPatente.Text!="" && ) btnBuscarTrailer_Click(null,null);
    }
    public static Boolean IsNumeric(string valor)
    {
        int result;
        return int.TryParse(valor, out result);
    }
    public void CreaDataTable()
    {
        DataTable table = new DataTable();
        this.gv_Seleccionados.DataSource = null;
        this.gv_Seleccionados.DataBind();
        table.Columns.Clear();
        table.Columns.Add("ZONA1", typeof(String));
        table.Columns.Add("ZONA", typeof(String));
        table.Columns.Add("PLAYA1", typeof(String));
        table.Columns.Add("PLAYA", typeof(String));
        table.Columns.Add("POSICION1", typeof(String));
        table.Columns.Add("POSICION", typeof(String));
        this.ViewState["table"] = table;
    }
    public void gv_seleccionados_rowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable table = (DataTable)this.ViewState["table"];
        int index = Convert.ToInt32(e.CommandArgument.ToString());
        table.Rows[index].Delete();
        this.gv_Seleccionados.DataSource = table;
        this.gv_Seleccionados.DataBind();
    }
    #region TextBox
    protected void txt_editRutTran_TextChanged(object sender, EventArgs e)
    {
        // TODO: Implement this method
        throw new NotImplementedException();
    }
    protected void txt_editPlaca_TextChanged(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        t = t.obtenerXPlaca(this.txt_editPlaca.Text);
        if (t.ID != 0 && t.ID != null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer ya existe en la base de datos!');", true);
            this.txt_editPlaca.Text = "";
            this.txt_editPlaca.Focus();
        }
    }
    protected void txt_buscarDoc_TextChanged(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        trailer = trailer.obtenerXDoc(this.txt_buscarDoc.Text, this.dropsite.SelectedValue);
        if (trailer.ID != 0 && trailer.ID != null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Documento ya existente.');", true);
            this.txt_buscarDoc.Text = "";
            this.txt_buscarDoc.Focus();
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
            hf_idCond.Value = "";
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
            hf_idCond.Value = "";
            utils.ShowMessage2(this, "buscarConductor", "warn_conductorNoexiste");
            return;
        }
        if (c.BLOQUEADO)
        {
            txt_conductorNombre.Enabled = false;
            hf_idCond.Value = "";
            txt_conductorRut.Text = "";
            txt_conductorNombre.Text = "";
            utils.ShowMessage2(this, "buscarConductor", "warn_conductorBloqueado");
            return;
        }
        txt_conductorNombre.Text = c.NOMBRE;
        txt_conductorNombre.Enabled = false;
        hf_idCond.Value = c.ID.ToString();
        txt_acomRut.Focus();
        utils.ShowMessage2(this, "buscarConductor", "success");
    }
    #endregion
    #region DropDownList
    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        SiteBC s = new SiteBC();
        s = s.ObtenerXId(Convert.ToInt32(this.dropsite.SelectedValue));
        this.lbl_site.Text = string.Format("CD {0}", s.DESCRIPCION);
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

        if (this.txt_placaTrailer.Text != "" && this.txt_placaTrailer.Text != null)
        {
            this.btnBuscarTrailer_Click(null, null);
        }
    }
    #endregion
    #region Buttons
    protected void generaPDF(object sender, EventArgs e)
    {
        ReportBC r = new ReportBC();
        DataTable dt = r.CargarPreEntradaDT(Convert.ToInt32(this.hf_id.Value));
        DataColumn codigobarras = new DataColumn("DOC_IMPRESION");
        codigobarras.DataType = typeof(byte[]);
        dt.Columns.Add(codigobarras);
        Barcode_BC barcode = new Barcode_BC("code128", 005, dt.Rows[0]["DOC_INGRESO_BARRAS"].ToString());
        dt.Columns["DOC_IMPRESION"].ReadOnly = false;
        // dt.Columns["DOC_IMPRESION"].DataType = typeof(System.Drawing.Image);
        dt.Rows[0]["DOC_IMPRESION"] = barcode.Byte;

        DataColumn codigobarras2 = new DataColumn("DOC_PATENTE");
        codigobarras2.DataType = typeof(byte[]);
        dt.Columns.Add(codigobarras2);
        Barcode_BC barcode2 = new Barcode_BC("CODE_39", 003, dt.Rows[0]["TRAI_PLACA"].ToString().ToUpper()); //   dt.Rows[0]["TRAI_PLACA"].ToString().ToUpper());
        dt.Columns["DOC_PATENTE"].ReadOnly = false;
        // dt.Columns["DOC_IMPRESION"].DataType = typeof(System.Drawing.Image);
        dt.Rows[0]["DOC_PATENTE"] = barcode2.Byte;

        ReportDataSource dataSource = new ReportDataSource("DataSet1", dt);
        this.limpiarTodo();
       
        //   barcode.Symbology = KeepAutomation.Barcode.Symbology.Code11;
        //  barcode.CodeToEncode = row.ProductID.ToString();
        //    barcode.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
  
        this.ReportViewer1.LocalReport.DataSources.Clear();
        this.ReportViewer1.LocalReport.DataSources.Add(dataSource);

        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        //Word
        byte[] bytes = this.ReportViewer1.LocalReport.Render(
            "PDF", null, out mimeType, out encoding,
            out extension,
            out streamids, out warnings);
        //byte[] renderedBytes = this.ReportViewer1.LocalReport.Render("PDF");
        this.Response.Clear();

        this.Response.ContentType = mimeType;

        this.Response.AddHeader("content-disposition", string.Format("attachment; filename=Doc_PreIngreso_{0}{1}{2}", this.txt_buscarDoc.Text, '.', extension));

        this.Response.BinaryWrite(bytes);

        this.Response.End();
    }
    protected void btn_tranGrabar_Click(object sender, EventArgs e)
    {
        string exito;
        TransportistaBC transportista = this.llenarTransportista();
        int id;
        if (transportista.Crear(out id))
        {
            exito = "Todo OK!";
        }
        else
        {
            exito = "Error!";
        }
        TransportistaBC tran = new TransportistaBC();
        utils.CargaDrop(this.ddl_transportista, "ID", "NOMBRE", tran.ObtenerTodos());
        this.ddl_transportista.SelectedValue = id.ToString();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", exito), true);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalTransportista');", true);
    }
    protected void btn_limpiar_Click(object sender, EventArgs e)
    {
        CreaDataTable();
        txt_buscarDoc.Text = "";
        txt_ingresoFecha.Text = DateTime.Now.ToShortDateString();
        txt_ingresoHora.Text = DateTime.Now.ToShortTimeString();
        hf_idTrailer.Value = "";
        hf_idCond.Value = "";
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
    }
    protected void btn_AgregarListado_Click(object sender, EventArgs e)
    {
        txt_doc2.Text = txt_doc2.Text.Replace(" ", "");

        if (this.txt_doc2.Text != "" && IsNumeric(this.txt_doc2.Text) == true)  //(ddl_bloquearPos.SelectedValue != "0" && ddl_bloquearPos.SelectedIndex != 0)
        {
            DataTable table = (DataTable)this.ViewState["table"];
            DataView dw = table.AsDataView();
            //   DataView dw = ((DataTable)ViewState["lista"]).AsDataView();
            dw.RowFilter = string.Format("POSICION1 = {0}", this.txt_doc2.Text);
            if (dw.ToTable().Rows.Count == 0)
            {
                PreEntradaBC p = new PreEntradaBC();
                if (p.ObtenerXDoc(this.txt_doc2.Text, this.dropsite.SelectedValue) == false)
                {
                    try
                    {
                        DataRow row = table.NewRow();
                        row["ZONA1"] = this.ddl_tipo_carga.SelectedValue;
                        row["ZONA"] = this.ddl_tipo_carga.SelectedItem.Text;

                        if (this.ddl_motivo.SelectedValue != "0")
                        {
                            row["PLAYA1"] = this.ddl_motivo.SelectedValue;
                            row["PLAYA"] = this.ddl_motivo.SelectedItem.Text;
                        }
                        else
                        {
                            row["PLAYA1"] = this.ddl_motivo.SelectedValue;
                            row["PLAYA"] = "";
                        }

                        row["POSICION1"] = this.txt_doc2.Text;
                        row["POSICION"] = this.txt_doc2.Text;
                        table.Rows.Add(row);
                        this.ViewState["lista"] = table;
                        this.gv_Seleccionados.DataSource = table;
                        this.gv_Seleccionados.DataBind();

                        this.ddl_tipo_carga.SelectedValue = "0";
                        this.ddl_motivo.SelectedValue = "0";
                        this.txt_doc2.Text = "";
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Ocurrió un error!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Cita ya existe!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Cita ya seleccionada!');", true);
            }
            //if (table.Rows.Count > 0)
            //{
            //    ddl_destinoZona.Enabled = false;
            //    ddl_destinoPlaya.Enabled = false;
            //}
            //else
            //{
            //    ddl_destinoZona.Enabled = true;
            //    ddl_destinoZona_SelectedIndexChanged(null, null);
            //}
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('seleccione una cita!');", true);
        }
    }
    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        t.PLACA = this.txt_editPlaca.Text;
        t.TRAN_ID = Convert.ToInt32(this.ddl_editTran.SelectedValue);
        if (t.CrearGenerico(t, false))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer agregado correctamente.');", true);
            this.hf_idTrailer.Value = t.ID.ToString();
            this.txt_placaTrailer.Text = t.PLACA;
            this.ddl_transportista.SelectedValue = t.TRAN_ID.ToString();
            this.btnBuscarTrailer_Click(null, null);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "$('#modalTrailer').modal('hide');", true);
        }
    }
    protected void btn_Conf_Click(object sender, EventArgs e)
    {
        //UpdatePanel4.Update();
        this.txt_editPlaca.Text = this.txt_placaTrailer.Text;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalTrailer();", true);
    }
    protected void btnBuscarTrailer_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();

        trailer = trailer.obtenerXPlaca(this.txt_placaTrailer.Text);

        if (utils.patentevalida(this.txt_placaTrailer.Text) == false)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Patente Invalida.');", true);
        }
        else if (trailer.ID == 0 || trailer.ID == null) //Trailer nuevo, no existe
        {
            TractoBC tracto = new TractoBC();
            if (this.txt_placaTrailer.Text != "")
            {
                tracto = tracto.ObtenerXPatente(this.txt_placaTrailer.Text);
            }
            if (tracto.ID > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Placa Corresponde a tracto');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalConfirmar();", true);
            }
            //this.hf_idTrailer.Value = "0";
            //this.ddl_transportista.ClearSelection();
            //this.txt_conductorRut.Text = "";
            //this.txt_conductorNombre.Text = "";
            //this.txt_acomRut.Text = "";
            //this.ddl_proveedor.ClearSelection();
            //this.rb_externo.Checked = false;
            //this.rb_propio.Checked = false;
            //this.ddl_transportista.Enabled = true;
            //this.txt_conductorRut.Enabled = true;
            //this.txt_conductorNombre.Enabled = true;
            //this.txt_acomRut.Enabled = true;
        }
        else //Trailer existente, trae datos
        {
            hf_idTrailer.Value = trailer.ID.ToString();
            txt_placaTrailer.Text = trailer.PLACA;
            ddl_transportista.SelectedValue = trailer.TRAN_ID.ToString();
            if (trailer.EXTERNO)
            {
                rb_propio.Checked = false;
                rb_externo.Checked = true;
            }
            else
            {
                rb_propio.Checked = true;
                rb_externo.Checked = false;
            }
            ddl_tipo_carga.Enabled = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Se cargaron los datos del trailer seleccionado.');", true);

            txt_placaTracto.Enabled = true;
            // this.ddl_proveedor.Enabled = true;
            //    this.txt_doc.Enabled = true;
            txt_idSello.Enabled = true;
            txt_conductorRut.Enabled = true;
            chk_conductorExtranjero.Enabled = true;
            txt_conductorNombre.Enabled = true;
            txt_acomRut.Enabled = true;
            txt_doc2.Enabled = true;
            txt_conductorRut_TextChanged(null, null);
        }
    }
    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        DataTable table = (DataTable)this.ViewState["table"];
        try
        {
            if (this.hf_idTrailer.Value == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje2", "alert('Debe ingresar Trailer');", true);
                this.limpiarTodo();
            }
            else
            {
                PreEntradaBC p = new PreEntradaBC();
                if (this.hf_idCond.Value == "")
                {
                    ConductorBC c = new ConductorBC();
                    c.RUT = txt_conductorRut.Text;
                    c.NOMBRE = this.txt_conductorNombre.Text;
                    c.TRAN_ID = Convert.ToInt32(this.ddl_transportista.SelectedValue);
                    c.COND_EXTRANJERO = chk_conductorExtranjero.Checked;
                    hf_idCond.Value = c.AgregarIdentity().ToString();
                }
                else
                {
                    p.COND_ID = Convert.ToInt32(this.hf_idCond.Value);
                }
                p.SITE_ID = Convert.ToInt32(this.dropsite.SelectedValue);
                p.TRAI_ID = Convert.ToInt32(this.hf_idTrailer.Value);
                p.FECHA = DateTime.Parse(string.Format("{0} {1}", this.txt_ingresoFecha.Text, this.txt_ingresoHora.Text));
                p.ESTADO = 1;
                p.DOC_INGRESO = this.txt_buscarDoc.Text;
                p.RUT_CHOFER = utils.formatearRut(this.txt_conductorRut.Text);
                p.Observacion = this.txt_obs.Text;
                p.NOMBRE_CHOFER = this.txt_conductorNombre.Text;
                p.RUT_ACOMP = this.txt_acomRut.Text;
                if (this.rb_ingresoVacio.Checked)
                {
                    p.SELLO_INGRESO = this.txt_idSello.Text;
                    p.CARGADO = false;
                }
                if (this.rb_ingresoCargado.Checked)
                {
                    p.PROV_ID = Convert.ToInt32(this.ddl_proveedor.SelectedValue);
                    p.TIIC_ID = Convert.ToInt32(this.ddl_tipo_carga.SelectedValue);
                    p.MOIC_ID = Convert.ToInt32(this.ddl_motivo.SelectedValue);
                    p.SELLO_CARGA = this.txt_idSello.Text;
                    p.PATENTE_TRACTO = this.txt_placaTracto.Text;
                    p.CARGADO = true;
                }

                DataTable citas = table.Clone();
                foreach (DataRow drtableOld in table.Rows)
                {
                    citas.ImportRow(drtableOld);
                }

                citas.Columns.RemoveAt(5);
                citas.Columns.RemoveAt(3);
                citas.Columns.RemoveAt(1);

                citas.Columns[0].ColumnName = "tipo_carga";
                citas.Columns[1].ColumnName = "motivo_carga";
                citas.Columns[2].ColumnName = "numero_cita";
                    
                p.citas = citas;

                string error = "";

                int id;
                TrailerBC trailer_tracto = new TrailerBC();

                if (this.txt_placaTracto.Text != "")
                {
                    trailer_tracto = trailer_tracto.obtenerXPlaca(this.txt_placaTracto.Text);
                }

                if (utils.patentevalida(this.txt_placaTracto.Text) == false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('patente de Tracto Invalida');"), true);
                }
                else if (trailer_tracto.ID > 0 && trailer_tracto.ID != p.TRAI_ID)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('Tracto corresponde a trailer');"), true);
                }
                else if (p.Crear(p, out id, out error) && error == "")
                {
                    if (id != 0)
                    {
                        this.hf_id.Value = id.ToString();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Preingreso correcto.');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "generarPDF", "generaPDF();", true);
                        this.CreaDataTable();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", error), true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", error), true);
                }
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error! No se pudo ingresar los datos.');", true);
        }
    }
    #endregion
    #region CheckBox
    protected void chk_ingresoCargado_CheckedChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(this.hf_idTrailer.Value))
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            if (this.rb_ingresoCargado.Checked == true)
            {
                this.ddl_tipo_carga.Enabled = true;
            }
            else
            {
                this.ddl_tipo_carga.Enabled = false;
            }
            this.tipo_carga_SelectedIndexChanged(null, null);

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

            this.ddl_motivo.Enabled = true;
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
        //   txt_doc.Text = "";
        txt_idSello.Text = "";
        txt_conductorNombre.Enabled = false;
        txt_conductorRut.Enabled = false;
        txt_acomRut.Enabled = false;
        txt_placaTracto.Enabled = false;
        //    txt_doc.Enabled = false;
        txt_idSello.Enabled = false;
        txt_obs.Text = "";
        txt_buscarDoc.Text = "";
        ddl_transportista.ClearSelection();
        ddl_tipo_carga.ClearSelection();
        ddl_tipo_carga.Enabled = false;
        ddl_transportista.ClearSelection();
        ddl_transportista.Enabled = false;
        chk_conductorExtranjero.Checked = false;
        //     dropsite.ClearSelection();
        //  ddl_proveedor.ClearSelection();
        // ddl_proveedor.SelectedValue = ((UsuarioBC)Session["Usuario"]).ID_PROVEEDOR.ToString();
        //      rb_ingresoCargado.Checked = false;
        //    chk_ingresoCargado_CheckedChanged(null, null);
    }
    protected void trailercreado(object sender, EventArgs e)
    {
        this.txt_placaTrailer.Text = ((TrailerBC)(sender)).PLACA;
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