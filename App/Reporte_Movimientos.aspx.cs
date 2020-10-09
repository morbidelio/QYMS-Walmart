// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Reporte_Movimientos : System.Web.UI.Page
{
    UsuarioBC user = new UsuarioBC();
    UtilsWeb utils = new UtilsWeb();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["usuario"] == null)
        {
            this.Response.Redirect("~/InicioQYMS.aspx");
        }
        this.user = (UsuarioBC)this.Session["usuario"];
        if (!this.IsPostBack)
        {
            this.txt_desde.Text = DateTime.Now.AddDays(-1).ToShortDateString();
            this.txt_hasta.Text = DateTime.Now.ToShortDateString();
            CargaDrops c = new CargaDrops();
            c.Site_Normal(this.ddl_site, this.user.ID);
         //   this.ObtenerReporte(true);
        }
    }
    #region Buttons
    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)this.ViewState["listar"];

        if (view.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Exportar", "Exportar();", true);
        }
        else
        {
            string texto = "Debe cargar datos antes de exportar!";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", texto), true);
        }
    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)this.ViewState["listar"];
        GridView gv = new GridView();
        gv.DataSource = view;
        gv.DataBind();

        string fileName = "Reporte_Movimientos.xls";
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
        this.ObtenerReporte(true);
    }
    #endregion
    #region GridView
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
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerReporte(false);
    }
    #endregion
    #region DropDownList
    protected void ddl_site_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ObtenerReporte(true);
    }
    #endregion
    #region UtilsPagina
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
    private void ObtenerReporte(bool forzarBD)
    {
        if (this.ViewState["listar"] == null || forzarBD)
        {
            int site_id = Convert.ToInt32(this.ddl_site.SelectedValue);
            DateTime desde = Convert.ToDateTime(this.txt_desde.Text);
            DateTime hasta = Convert.ToDateTime(this.txt_hasta.Text);
            ReportBC r = new ReportBC();
            this.ViewState["listar"] = r.CargarMovimientos(site_id, desde, hasta,No_ottawa.Checked);
        }
        DataView dw = new DataView((DataTable)this.ViewState["listar"]);
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw.ToTable();
        this.gv_listar.DataBind();
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