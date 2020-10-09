using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class App_Reporte_historia_bloqueos : System.Web.UI.Page
{


    UtilsWeb utils = new UtilsWeb();
    UsuarioBC u = new UsuarioBC();


    public void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerReporte();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["usuario"] == null)
        {
            this.Response.Redirect("~/InicioQYMS2.aspx");
        }

        this.u = (UsuarioBC)this.Session["usuario"];

        if (!IsPostBack)
        {

            this.txt_desde.Text = DateTime.Now.AddDays(-7).ToShortDateString();
            this.txt_hasta.Text = DateTime.Now.ToShortDateString();
            ObtenerReporte();
        }
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

            string fileName = "Reporte_Ottawa.xls";
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
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", texto), true);
        }
    }
    #region UtilsPagina
    private void ObtenerReporte()
    {
        ReportBC r = new ReportBC();
        DateTime desde = Convert.ToDateTime(this.txt_desde.Text);
        DateTime hasta = Convert.ToDateTime(this.txt_hasta.Text);
        DataTable dt = r.Historia_bloqueos(desde, hasta);
        this.ViewState["listar"] = dt;
        this.gv_listar.DataSource = dt;
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
}