using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Reporte_Gps : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SiteBC s = new SiteBC();
            TransportistaBC tran = new TransportistaBC();
            TrailerTipoBC tipo = new TrailerTipoBC();
            utils.CargaDrop(ddl_buscarTransportista, "ID", "NOMBRE", tran.ObtenerTodos());
            utils.CargaDrop(ddl_buscarTipo, "ID", "DESCRIPCION", tipo.obtenerTodo());
            utils.CargaDropNormal(ddl_site, "ID", "DESCRIPCION", s.ObtenerTodos());
            ObtenerReporte(true);
        }
    }

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerReporte(true);
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)this.ViewState["lista"];

        if (view.Count > 0)
        {
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
        else
        {
            string texto = "Debe cargar datos antes de exportar!";
            utils.ShowMessage(this, texto, "warn", true);
        }

    }
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "GPS")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            gv_listar.SelectedIndex = index;
            int trai_id = Convert.ToInt32(gv_listar.SelectedDataKey.Values[0]);
            decimal lat = Convert.ToDecimal(gv_listar.SelectedDataKey.Values[1]);
            decimal lon = Convert.ToDecimal(gv_listar.SelectedDataKey.Values[2]);
            string trai_placa = Convert.ToString(gv_listar.SelectedDataKey.Values[3]);
            DateTime fh = (gv_listar.SelectedDataKey.Values[4] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(gv_listar.SelectedDataKey.Values[4]);
            int sentido = Convert.ToInt32(gv_listar.SelectedDataKey.Values[5]);
            int vel = Convert.ToInt32(gv_listar.SelectedDataKey.Values[6]);
            string tran_nombre = Convert.ToString(gv_listar.SelectedDataKey.Values[7]);
            string script = string.Format("modalGps({0},'{1}',{2},{3},'{4}',{5},{6},'{7}');", trai_id, trai_placa, lat.ToString("G", CultureInfo.InvariantCulture), lon.ToString("G",CultureInfo.InvariantCulture), fh, sentido, vel, tran_nombre) ;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modalGps", script, true);
        }
    }
    protected void gv_listar_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    private void ObtenerReporte(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {

            ReportBC report = new ReportBC();
            string trai_placa = txt_buscarPlaca.Text;
            string trai_nro = txt_buscarNro.Text;
            int site_id = Convert.ToInt32(ddl_site.SelectedValue);
            int trti_id = Convert.ToInt32(ddl_buscarTipo.SelectedValue);
            int tran_id = Convert.ToInt32(ddl_buscarTransportista.SelectedValue);
            ViewState["lista"] = (chk_buscarInterno.Checked) ? report.Reporte_TrailerGPS(false, trai_placa, trai_nro, trti_id, tran_id, site_id) : report.Reporte_TrailerGPS(trai_placa, trai_nro, trti_id, tran_id, site_id);
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)ViewState["sortExpresion"];
        }
        gv_listar.DataSource = dw;
        gv_listar.DataBind();
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
}