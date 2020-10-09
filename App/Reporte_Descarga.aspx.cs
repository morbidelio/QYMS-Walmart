using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Reporte_Descarga : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    UsuarioBC user = new UsuarioBC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("~/InicioQYMS2.aspx");
        user = (UsuarioBC)Session["usuario"];
        if(!IsPostBack)
        {
            YMS_ZONA_BC y = new YMS_ZONA_BC();
            txt_desde.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            txt_hasta.Text = DateTime.Now.ToShortDateString();
            utils.CargaDropNormal(ddl_site, "ID", "DESCRIPCION", y.ObteneSites(user.ID));
            ddl_site_SelectedIndexChanged(null, null);
       //     ObtenerReporte(true);
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

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerReporte(false);
    }

    private void ObtenerReporte(bool forzarBD)
    {
        if (ViewState["listar"] == null || forzarBD)
        {
            ReportBC r = new ReportBC();
            DateTime desde = Convert.ToDateTime(txt_desde.Text);
            DateTime hasta = Convert.ToDateTime(txt_hasta.Text);
            string placa = txt_placa.Text;
            int site_id = Convert.ToInt32(ddl_site.SelectedValue);
            int play_id = Convert.ToInt32(ddl_playa.SelectedValue);
            ViewState["listar"] = r.CargarSolicitudesDescarga(desde, hasta, placa, site_id, play_id);
        }
        DataView dw = new DataView((DataTable)this.ViewState["listar"]);
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
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

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerReporte(true);
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)this.ViewState["listar"];

        if (view.Count > 0)
        {
            GridView gv = new GridView();
            gv.DataSource = view;
            gv.DataBind();

            string fileName = "Reporte_Descarga.xls";
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
            utils.ShowMessage(this, "Debe cargar datos antes de exportar!", "warn", true);
        }
    }

    protected void ddl_site_SelectedIndexChanged(object sender, EventArgs e)
    {
        PlayaBC p = new PlayaBC();
        int site_id = Convert.ToInt32(ddl_site.SelectedValue);
        utils.CargaDrop(ddl_playa, "ID", "DESCRIPCION", p.ObtenerDrop(site_id));
    }
}