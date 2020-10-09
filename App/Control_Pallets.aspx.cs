// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Control_Pallets : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    UsuarioBC u = new UsuarioBC();
    CargaDrops drops = new CargaDrops();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["Usuario"] != null)
        {
            this.u = (UsuarioBC)this.Session["Usuario"];
        }
        else
        {
            this.Response.Redirect("../InicioQYMS.aspx");
        }

        if (!this.IsPostBack)
        {
            //SiteBC s = new SiteBC();
            TransportistaBC t = new TransportistaBC();
            CargaTipoBC ct = new CargaTipoBC();
            this.drops.Site_Normal(this.ddl_buscarSite, this.u.ID);
            //utils.CargaDropNormal(ddl_buscarSite, "ID", "NOMBRE", s.ObtenerTodos());
            //utils.CargaDrop(ddl_buscarAnden, "ID", "DESCRIPCION", l.obtenerTodoLugar(int.Parse(ddl_buscarSite.SelectedValue)));
            this.utils.CargaDropTodos(this.ddl_buscarTrans, "ID", "NOMBRE", t.ObtenerTodos());
            this.utils.CargaDropTodos(this.ddl_buscarTipoCarga, "ID", "DESCRIPCION", ct.obtenerTodo());
            this.ddl_buscarSite_SelectedIndexChanged(null, null);
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (this.u.numero_sites < 2)
        {
            this.SITE.Visible = false;
        }
    }

    #region Buttons

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)this.ViewState["lista"];
        GridView gv = new GridView();
        gv.DataSource = view;
        gv.DataBind();

        string fileName = "reporte_Descarga.xls";
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
    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)this.ViewState["lista"];

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

    protected void cargaFin_Click(object sender, EventArgs e)
    {
        SolicitudBC solicitud = new SolicitudBC();
        solicitud.SOLI_ID = int.Parse(this.hf_idSolicitud.Value);
        string resultado = "";
        UsuarioBC usuario = new UsuarioBC();
        usuario = (UsuarioBC)this.Session["Usuario"];
        solicitud.DescargaCompleta(solicitud.SOLI_ID, out resultado, usuario.ID);
        if (resultado == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "showAlert('Descarga completada');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf1", "$('#modalConfirmacion').modal('hide');", true);
         
            this.ObtenerSolicitudes(true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", string.Format("alert('{0}');", resultado), true);
        }
    }

    protected void btn_buscarSolicitud_Click(object sender, EventArgs e)
    {
        this.ObtenerSolicitudes(true);

        DataTable dt = (DataTable)this.ViewState["lista"];
        DataView dw = dt.AsDataView();
        string filtro = "";
        bool segundo = false;
        if (this.ddl_buscarTrans.SelectedIndex > 0)
        {
            filtro += string.Format("TRAN_ID = {0} ", this.ddl_buscarTrans.SelectedValue);
            segundo = true;
        }
        if (this.ddl_buscarTipoCarga.SelectedIndex > 0)
        {
            if (segundo)
            {
                filtro += "AND ";
            }
            filtro += string.Format("TIIC_ID = {0} ", this.ddl_buscarTipoCarga.SelectedValue);
            segundo = true;
        }
        if (this.ddl_buscarPlaya.SelectedIndex > 0)
        {
            if (segundo)
            {
                filtro += "AND ";
            }
            filtro += string.Format("PLAY_ID = {0} ", this.ddl_buscarPlaya.SelectedValue);
            segundo = true;
        }
        if (!string.IsNullOrWhiteSpace(filtro))
        {
            dw.RowFilter = filtro;
            this.ViewState["filtrados"] = dw.ToTable();
            this.ObtenerSolicitudes(false);
        }
        //  else
        //     ObtenerSolicitudes(true);
    }

    #endregion

    #region DropDownList

    protected void ddl_buscarSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.drops.Playa_Todos(this.ddl_buscarPlaya, 0, int.Parse(this.ddl_buscarSite.SelectedValue));
        //PlayaBC p = new PlayaBC();
        //utils.CargaDropTodos(ddl_buscarPlaya, "ID", "DESCRIPCION", p.ObtenerDrop(int.Parse(ddl_buscarSite.SelectedValue)));
        this.ObtenerSolicitudes(true);
    }

    #endregion 

    #region GridView

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerSolicitudes(false);
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

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnAnden1 = (LinkButton)e.Row.FindControl("btn_llegadaAnden1");
            LinkButton btnAnden2 = (LinkButton)e.Row.FindControl("btn_llegadaAnden2");
            int id_estado = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID_ESTADOSOLICITUD"));
            if (id_estado == 310)
            {
                btnAnden1.Visible = true;
                btnAnden2.Visible = false;
            }
            else if (id_estado == 330)
            {
                btnAnden1.Visible = false;
                btnAnden2.Visible = true;
            }
            else
            {
                btnAnden1.Visible = false;
                btnAnden2.Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.CssClass = "header-color2";
        }
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SolicitudBC s = new SolicitudBC();
        DataTable dt;
        if (this.ViewState["filtrados"] == null)
        {
            dt = (DataTable)this.ViewState["lista"];
        }
        else
        {
            dt = (DataTable)this.ViewState["filtrados"];
        }
        if (e.CommandName == "ANDEN1")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int soli_id = Convert.ToInt32(dt.Rows[index]["ID"]);
            int luga_id = Convert.ToInt32(dt.Rows[index]["LUGA_ID1"]);
            string resultado;
            if (s.CargaPallets(soli_id, luga_id, out resultado, this.u.ID) && resultado == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Accion realizada correctamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", resultado), true);
            }
            this.ObtenerSolicitudes(true);
        }
        if (e.CommandName == "ANDEN2")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int soli_id = Convert.ToInt32(dt.Rows[index]["ID"]);
            int luga_id = Convert.ToInt32(dt.Rows[index]["LUGA_ID2"]);
            string resultado;
            if (s.DescargaPallets(soli_id, luga_id, out resultado, this.u.ID) && resultado == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Accion realizada correctamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", resultado), true);
            }
            this.ObtenerSolicitudes(true);
        }
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

    private bool comprobarTrailerEx()
    {
        SolicitudBC s = new SolicitudBC();
        s = s.ObtenerXId(int.Parse(this.hf_idSolicitud.Value));
        TrailerBC t = new TrailerBC();
        t = t.obtenerXID(s.ID_TRAILER);
        return t.EXTERNO;
    }

    private void ObtenerSolicitudes(bool forzarBD)
    {
        SolicitudBC solicitud = new SolicitudBC();
        if (this.ViewState["lista"] == null || forzarBD)
        {
            DataTable dt = solicitud.ObtenerSolicitudesPallets(int.Parse(this.ddl_buscarSite.SelectedValue));
            this.ViewState["lista"] = dt;
            this.ViewState.Remove("filtrados");
        }
        DataView dw;
        if (this.ViewState["filtrados"] == null)
        {
            dw = new DataView((DataTable)this.ViewState["lista"]);
        }
        else
        {
            dw = new DataView((DataTable)this.ViewState["filtrados"]);
        }
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw;
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