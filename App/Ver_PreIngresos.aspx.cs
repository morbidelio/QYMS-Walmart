using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Microsoft.Reporting.WebForms;

public partial class App_Ver_PreIngreso : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    UsuarioBC usuario;
    static FuncionesGenerales funcion = new FuncionesGenerales();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("~/InicioQYMS2.aspx");
        usuario = (UsuarioBC)Session["Usuario"];

        if (!IsPostBack)
        {
            CaractCargaBC catt = new CaractCargaBC();
            TransportistaBC tran = new TransportistaBC();
            TrailerTipoBC tipo = new TrailerTipoBC();
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable dt = catt.obtenerTodo();
            CargaDrops c = new CargaDrops();
            c.Proveedor_Todos(ddl_buscarProveedor);

            if (usuario.PROVEEDOR != "")
            {
                this.ddl_buscarProveedor.SelectedValue = usuario.ID_PROVEEDOR.ToString() ;
                this.ddl_buscarProveedor.Enabled = false;
            }

            DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
            utils.CargaDropNormal(this.ddl_buscarSite, "ID", "NOMBRE", ds);
            txt_buscarDesde.Text = DateTime.Now.ToShortDateString();
            txt_buscarHasta.Text = DateTime.Now.ToShortDateString();
            ObtenerPreIngresos(true);
        }
    }
    #region GridView
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
             LinkButton btnbloquear = (LinkButton)e.Row.FindControl("btn_eliminar");
             LinkButton lb = e.Row.FindControl("btn_pdf") as LinkButton;
             ScriptManager.GetCurrent(this).RegisterPostBackControl(lb);

            if (DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString() == "1")
            {
                btnbloquear.Style.Add("visibility", "visible");
            }
            else
            {
                btnbloquear.Style.Add("visibility", "hidden");
            }
        }
    }
    protected void gv_listar_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerPreIngresos(false);
    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        TransportistaBC trans = new TransportistaBC();
        hf_preingreso.Value = e.CommandArgument.ToString();
        trans = trans.DatosPreIngreso(Convert.ToInt32(hf_preingreso.Value));


        if (e.CommandName == "ELIMINAR")
        {
            if (trans.ESTADO == "1")
            {
                hf_preingreso.Value = e.CommandArgument.ToString();
                utils.AbrirModal(this, "modalConfirmar");
            }
            else
            {
                utils.ShowMessage(this, "No se puede eliminar preingreso", "warn", true);
            }
        }
        else if (e.CommandName == "pdf")
        {
            hf_preingreso.Value = e.CommandArgument.ToString();
            generaPDF(null, null);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Ocurrio un Problema');", true);
        }
    }
    protected void generaPDF(object sender, EventArgs e)
    {
        ReportBC r = new ReportBC();
        DataTable dt = r.CargarPreEntradaDT(Convert.ToInt32(hf_preingreso.Value));
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


        Response.AddHeader("content-disposition", "attachment; filename=Doc_PreIngreso_" + dt.Rows[0]["DOC_INGRESO"].ToString() + '.' + extension);

        Response.BinaryWrite(bytes);

        Response.End();
    }
    #endregion
    #region Buttons
    protected void btn_Eliminar_Click(object sender, EventArgs e)
    {
        ProveedorBC prov = new ProveedorBC();
        if (prov.EliminarPreIngreso(Convert.ToInt32(hf_preingreso.Value)))
        {
            utils.ShowMessage2(this, "eliminar", "success");
            utils.CerrarModal(this, "modalConfirmar");
        }
        else
        {
            utils.ShowMessage2(this, "eliminar", "error");
        }
        ObtenerPreIngresos(true);

    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)ViewState["lista"];

        if (view.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Exportar", "Exportar();", true);
        }
        else
        {
            string texto = "Debe cargar datos antes de exportar!";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + texto + "');", true);
        }
    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)ViewState["lista"];
        GridView gv = new GridView();
        gv.DataSource = view;
        gv.DataBind();

        string fileName = "reporte_Trailer.xls";
        string Extension = ".xls";
        if (Extension == ".xls")
        {
            PrepareControlForExport(gv);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        System.Web.UI.WebControls.Table table = new System.Web.UI.WebControls.Table();
                        table.GridLines = gv.GridLines;

                        if (gv.HeaderRow != null)
                        {
                            PrepareControlForExport(gv.HeaderRow);
                            table.Rows.Add(gv.HeaderRow);
                        }

                        foreach (GridViewRow row in gv.Rows)
                        {
                            PrepareControlForExport(row);
                            table.Rows.Add(row);
                        }

                        if (gv.FooterRow != null)
                        {
                            PrepareControlForExport(gv.FooterRow);
                            table.Rows.Add(gv.FooterRow);
                        }

                        gv.GridLines = GridLines.Both;
                        table.RenderControl(htw);
                        HttpContext.Current.Response.Write(sw.ToString());
                        HttpContext.Current.Response.End();
                    }
                }
            }
            catch (HttpException ex)
            {
                throw ex;
            }
        }
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerPreIngresos(true);
    }
    #endregion
    #region UtilsPagina
    private void ObtenerPreIngresos(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            TrailerBC trailer = new TrailerBC();
            DataTable dt = trailer.obtener_pre_ingresos(Convert.ToInt32(ddl_buscarSite.SelectedValue), Convert.ToInt32(ddl_buscarProveedor.SelectedValue), txt_buscarDesde.Text, txt_buscarHasta.Text);
            ViewState["lista"] = dt;

        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        this.gv_listar.DataSource = dw.ToTable();
        this.gv_listar.DataBind();
    }
    private static void PrepareControlForExport(Control control)
    {
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
            }
            else if (current is HiddenField)
            {
                control.Controls.Remove(current);
            }
            if (current.HasControls())
            {
                PrepareControlForExport(current);
            }
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
        file = string.Format("{0}\\{1}", this.utils.pathviewstate(), file);

        return file;
    }
}