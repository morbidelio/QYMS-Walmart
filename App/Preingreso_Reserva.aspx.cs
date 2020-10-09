using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Preingreso_Reserva : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProveedorBC p = new ProveedorBC();
            SiteBC s = new SiteBC();
            CargaTipoBC ct = new CargaTipoBC();
            DataTable dt;
            txt_buscarDesde.Text = DateTime.Now.AddDays(-utils.Intervalo_preingreso).ToShortDateString();
            txt_buscarHasta.Text = DateTime.Now.AddDays(utils.Intervalo_preingreso).ToShortDateString();
            dt = s.ObtenerTodos();
            utils.CargaDropNormal(ddl_buscarSite, "ID", "NOMBRE", dt);
            utils.CargaDrop(ddl_editSite, "ID", "NOMBRE", dt);
            dt = p.obtenerTodo();
            utils.CargaDropTodos(ddl_buscarProveedor, "ID", "DESCRIPCION", dt);
            utils.CargaDrop(ddl_editProveedor, "ID", "DESCRIPCION", dt);
            dt = ct.obtenerTodo();
            DataView dw = dt.AsDataView();
            dw.RowFilter = "ID IN (3,15)";
            utils.CargaDropTodos(ddl_buscarTipoCarga, "ID", "DESCRIPCION", dw.ToTable());
            utils.CargaDrop(ddl_editTipoCarga, "ID", "DESCRIPCION", dw.ToTable());
            ObtenerReservas(true);
        }
    }
    #region Buttons
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerReservas(true);
    }
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        Limpiar();
        utils.AbrirModal(this, "modalEdit");
    }
    protected void btn_editGrabar_Click(object sender, EventArgs e)
    {
        ProveedorBC p = new ProveedorBC();
        int prov_id = Convert.ToInt32(ddl_editProveedor.SelectedValue);
        int site_id = Convert.ToInt32(ddl_editSite.SelectedValue);
        int prve_id = Convert.ToInt32(ddl_editVendor.SelectedValue);
        int nro_cita = Convert.ToInt32(txt_editNro.Text);
        DateTime fecha = Convert.ToDateTime(txt_editFecha.Text + " " + txt_editHora.Text);
        int tiic_id = Convert.ToInt32(ddl_editTipoCarga.SelectedValue);
        try
        {
            if(p.EditarPreIngresoReserva(prov_id, site_id, prve_id, nro_cita, fecha, tiic_id))
            {
                utils.ShowMessage2(this, "edit", "success");
            }
            else
            {
                utils.ShowMessage2(this, "edit", "error");
            }
        }
        catch(Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
    }
    #endregion
    #region DropDownList
    protected void ddl_editProveedor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_editProveedor.SelectedIndex > 0)
        {
            ProveedorBC p = new ProveedorBC();
            int prov_id = Convert.ToInt32(ddl_editProveedor.SelectedValue);
            DataTable dt = p.obtenerVendorXParametros(prov_id);
            utils.CargaDrop(ddl_editVendor, "PRVE_ID", "PRVE_NUMERO", dt);
            ddl_editVendor.Enabled = true;

        }
        else
        {
            ddl_editVendor.ClearSelection();
            ddl_editVendor.Enabled = false;
        }
    }
    #endregion
    #region GridView
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "MODIFICAR")
        {
            ProveedorBC p = new ProveedorBC();
            int num_cita = Convert.ToInt32(e.CommandArgument);
            DataRow dr = p.ObtenerReservaPreingresoXNumero(num_cita);
            ddl_editProveedor.SelectedValue = dr["PROV_ID"].ToString();
            ddl_editProveedor_SelectedIndexChanged(null, null);
            ddl_editVendor.SelectedValue = dr["PRVE_ID"].ToString();
            txt_editFecha.Text = Convert.ToDateTime(dr["FECHA_CITA"].ToString()).ToShortDateString();
            txt_editHora.Text = Convert.ToDateTime(dr["FECHA_CITA"].ToString()).ToShortTimeString();
            txt_editNro.Text = dr["NUM_CITA"].ToString();
            ddl_editSite.SelectedValue = dr["SITE_ID"].ToString();
            ddl_editTipoCarga.SelectedValue = dr["TIIC_ID"].ToString();
            txt_editNro.Enabled = false;
            utils.AbrirModal(this, "modalEdit");
        }
        if(e.CommandName == "ELIMINAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            utils.AbrirModal(this, "modalConf");
        }
        if(e.CommandName == "PDF")
        {
            hf_id.Value = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "pdf", "btn_pdf();", true);
        }
    }
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            if (DataBinder.Eval(e.Row.DataItem, "PRING_ID") == DBNull.Value)
                ((LinkButton)e.Row.FindControl("btn_pdf")).Style.Add("visibility", "hidden");
        }
    }
    protected void gv_listar_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if(e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    #endregion
    #region UtilsPagina
    private void ObtenerReservas(bool forzarBD)
    {
        if (ViewState["listar"] == null || forzarBD)
        {
            ProveedorBC p = new ProveedorBC();
            int site_id = Convert.ToInt32(ddl_buscarSite.SelectedValue);
            DateTime desde = Convert.ToDateTime(txt_buscarDesde.Text);
            DateTime hasta = Convert.ToDateTime(txt_buscarHasta.Text);
            int proveedor_id = Convert.ToInt32(ddl_buscarProveedor.SelectedValue);
            string numcita = txt_buscarNumCita.Text;
            int tipocarga_id = Convert.ToInt32(ddl_buscarTipoCarga.SelectedValue);
            bool preingreso = chk_buscarPreIngreso.Checked;
            ViewState["listar"] = p.ObtenerReservaPreingresoXParametros(site_id, desde, hasta, proveedor_id, numcita, tipocarga_id, preingreso);
        }
        DataView dw = new DataView((DataTable)ViewState["listar"]);
        gv_listar.DataSource = dw.ToTable();
        gv_listar.DataBind();
    }
    private void Limpiar()
    {
        ddl_editProveedor.ClearSelection();
        ddl_editProveedor_SelectedIndexChanged(null, null);
        ddl_editSite.ClearSelection();
        txt_editNro.Text = "";
        txt_editFecha.Text = DateTime.Now.ToShortDateString();
        txt_editHora.Text = DateTime.Now.ToShortTimeString();
        ddl_editTipoCarga.ClearSelection();
    }
    #endregion

    protected void btnModalEliminar_Click(object sender, EventArgs e)
    {

    }

    protected void btn_pdf_Click(object sender, EventArgs e)
    {
        ReportBC r = new ReportBC();
        DataTable dt = r.CargarPreEntradaDT(Convert.ToInt32(hf_id.Value));
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

        //   barcode.Symbology = KeepAutomation.Barcode.Symbology.Code11;
        //  barcode.CodeToEncode = row.ProductID.ToString();
        //    barcode.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;

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
        //byte[] renderedBytes = this.ReportViewer1.LocalReport.Render("PDF");
        Response.Clear();

        Response.ContentType = mimeType;

        Response.AddHeader("content-disposition", "attachment; filename=Doc_PreIngreso_" + dt.Rows[0]["PRING_DOC_INGRESO"] + '.' + extension);

        Response.BinaryWrite(bytes);

        Response.End();
    }
}