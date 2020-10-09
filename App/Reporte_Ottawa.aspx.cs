// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;

public partial class App_Reporte_Ottawa : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    UsuarioBC u = new UsuarioBC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["usuario"] == null)
        {
            this.Response.Redirect("~/InicioQYMS2.aspx");
        }
        this.u = (UsuarioBC)this.Session["usuario"];
        if (!this.IsPostBack)
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            MovimientoBC m = new MovimientoBC();
            UsuarioBC usu = new UsuarioBC();
            this.utils.CargaDropNormal(this.ddl_buscarSite, "ID", "NOMBRE", yms.ObteneSites(this.u.ID));
            this.ddl_buscarSite_SelectedIndexChanged(null, null);
            this.utils.CargaDropTodos(this.ddl_moti, "ID", "DESCRIPCION", m.ObtenerTipos());
            this.utils.CargaDropTodos(this.ddl_usuario, "ID", "USERNAME", usu.ObtenerUsuariosRemolcador( int.Parse(ddl_buscarSite.SelectedValue)));
            this.txt_desde.Text = DateTime.Now.AddDays(-7).ToShortDateString();
            this.txt_hasta.Text = DateTime.Now.ToShortDateString();
            this.ObtenerReporte();
        }
    }
    #region GridView

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                TableCell c = e.Row.Cells[i];
                if (c.Text != "0")
                {
                    c.Style.Add("background-color", "#58DCEC");
                }
            }
        }
    }

    #endregion
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

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        DateTime desde = Convert.ToDateTime(this.txt_desde.Text);
        DateTime hasta = Convert.ToDateTime(this.txt_hasta.Text);
        if (hasta < desde)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('La fecha inicial debe ser menor a la fecha final.');", true);
        }
        else if ((hasta - desde).TotalDays > 7)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('El intervalo entre fechas no puede superar los 7 días');", true);
        }
        else
        {
            this.ObtenerReporte();
        }
    }

    #endregion
    #region DropDownList

    protected void ddl_buscarSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        RemolcadorBC r = new RemolcadorBC();
        this.utils.CargaDropTodos(this.ddl_remo, "ID", "DESCRIPCION", r.obtenerTodos(Convert.ToInt32(this.ddl_buscarSite.SelectedValue)));
    }

    #endregion
    #region UtilsPagina
    private void ObtenerReporte()
    {
        ReportBC r = new ReportBC();
        int site_id = Convert.ToInt32(this.ddl_buscarSite.SelectedValue);
        int moti_id = Convert.ToInt32(this.ddl_moti.SelectedValue);
        int usua_id = Convert.ToInt32(this.ddl_usuario.SelectedValue);
        int remo_id = Convert.ToInt32(this.ddl_remo.SelectedValue);
        DateTime desde = Convert.ToDateTime(this.txt_desde.Text);
        DateTime hasta = Convert.ToDateTime(this.txt_hasta.Text);
        DataTable dt = r.Ottawa(site_id, remo_id, moti_id, usua_id, null, desde, hasta);
        DataTable mostrar = new DataTable();
        mostrar.Columns.Add("Hora");
        for (DateTime fecha = desde; fecha <= hasta; fecha = fecha.AddDays(1.0))
        {
            mostrar.Columns.Add(fecha.ToShortDateString());
        }
        for (int hora = 0; hora < 24; hora++)
        {
            string[] fila = new string[mostrar.Columns.Count];
            fila[0] = new DateTime().AddHours(hora).ToShortTimeString();
            for (int cont = 1; cont < mostrar.Columns.Count; cont++)
            {
                DateTime fh = Convert.ToDateTime(mostrar.Columns[cont].ColumnName).AddHours(hora);
                //fila[cont] = "-";
                int cant = 0;
                for (int x = 2; x < dt.Columns.Count - 1; x++)
                {
                    string[] strcol = dt.Columns[x].ColumnName.Split("#".ToCharArray());
                    DateTime fh2 = Convert.ToDateTime(strcol[0]).AddHours(Convert.ToInt32(strcol[1]));
                    if (fh2 == fh)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            cant += Convert.ToInt32(dr[x]);
                        }
                    }
                }
                fila[cont] = cant.ToString();
            }
            mostrar.Rows.Add(fila);
        }

        this.ViewState["listar"] = mostrar;
        this.gv_listar.DataSource = mostrar;
        this.gv_listar.DataBind();
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)this.ViewState["listar"];
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