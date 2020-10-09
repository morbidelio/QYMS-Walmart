// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Control_yardtag : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    UsuarioBC u = new UsuarioBC();
    CargaDrops drops = new CargaDrops();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["usuario"] != null)
        {
            this.u = (UsuarioBC)this.Session["usuario"];
        }
        else
        {
            this.Response.Redirect("../InicioQYMS2.aspx");
        }
        if (!this.IsPostBack)
        {
            RemolcadorBC r = new RemolcadorBC();
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds = yms.ObteneSites(((UsuarioBC)this.Session["Usuario"]).ID);
            this.utils.CargaDropNormal(this.ddl_buscarSite, "ID", "NOMBRE", ds);
              this.ddl_buscarSite_SelectedIndexChanged(null, null);
        }
    }

 

  



    #region Button

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)this.ViewState["lista"];
        GridView gv = new GridView();
        gv.DataSource = view;
        gv.DataBind();

        string fileName = "movimientos_yardtag_pendientes.xls";
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

    protected void btn_anular_Click(object sender, EventArgs e)
    {
        MovimientoBC m = new MovimientoBC();

        string resultado = "";
        UsuarioBC usuario = new UsuarioBC();
        usuario = (UsuarioBC)this.Session["Usuario"];

        if (this.ddl_confMotivo.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Seleccione Motivo!');", true);
        }
        else
        {
            // m.Confirmar(int.Parse(hf_id.Value), u.ID, txt_compObs.Text, int.Parse(ddl_compMotivo.SelectedValue), int.Parse(ddl_compPos.SelectedValue), out resultado);
            m.Anular(int.Parse(this.hf_id.Value), int.Parse(this.ddl_confMotivo.SelectedValue), this.txt_compObs.Text, usuario.ID, out resultado);
            if (resultado == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Anulado!');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "$('#modalConfirmar').modal('hide');", true);
                this.ObtenerMovimientos(true);
                //   ObtenerMovimientos(true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", resultado), true);
            }
        }
    }
  

    protected void btn_cambiarDestino_Click(object sender, EventArgs e)
    {
        MovimientoBC m = new MovimientoBC();
        string resultado;
        if (m.ModificarDestino(int.Parse(this.hf_id.Value), int.Parse(this.ddl_posDestino.SelectedValue), this.u.ID, out resultado) && resultado == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Modificado correctamente!');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "$('#modalDestino').modal('hide');", true);
            this.ObtenerMovimientos(true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", resultado), true);
        }
    }

    protected void btn_buscarMovimientos_Click(object sender, EventArgs e)
    {
        this.ObtenerMovimientos(true);
        DataTable dt = (DataTable)this.ViewState["lista"];
        string filtros = "";
        bool segundo = false;
        //if (this.ddl_buscarRemolcador.SelectedIndex > 0)
        //{
        //    segundo = true;
        //    filtros += string.Format("REMO_ID = {0} ", this.ddl_buscarRemolcador.SelectedValue);
        //}
    //if (this.ddl_buscarPlaya.SelectedIndex > 0)
    //{
    //    if (segundo)
    //    {
    //        filtros += "AND ";
    //    }
    //    filtros += string.Format("PLAY_DEST_ID = {0}", this.ddl_buscarPlaya.SelectedValue);
    //}
        //DataView dw = dt.AsDataView();
        //if (!string.IsNullOrWhiteSpace(filtros))
        //{
        //    dw.RowFilter = filtros;
        //    this.ViewState["filtrados"] = dw.ToTable();
        //    this.ObtenerMovimientos(false);
        //}
        // else
        //   ObtenerMovimientos(true);
    }

    #endregion

    #region DropDownList

    protected void ddl_playaDestino_IndexChanged(object sender, EventArgs e)
    {
        this.drops.Lugar1(this.ddl_posDestino, 0, int.Parse(this.ddl_playaDestino.SelectedValue), -1, 1);
        //LugarBC l = new LugarBC();
        //utils.CargaDrop(ddl_posDestino, "ID", "DESCRIPCION", l.obtenerLugaresXPlayaDrop(int.Parse(ddl_playaDestino.SelectedValue)));
        if (this.ddl_posDestino.Items.Count > 0 && this.ddl_playaDestino.SelectedIndex > 0)
        {
            this.ddl_posDestino.Enabled = true;
        }
        else
        {
            this.ddl_posDestino.Enabled = false;
        }
    }

    protected void ddl_compPlaya_IndexChanged(object sender, EventArgs e)
    {
        this.drops.Lugar1(this.ddl_posDestino, 0, int.Parse(this.ddl_compPlaya.SelectedValue), -1, 1);
        //LugarBC l = new LugarBC();
        //utils.CargaDrop(ddl_compPos, "ID", "DESCRIPCION", l.obtenerLugaresXPlayaDrop(int.Parse(ddl_compPlaya.SelectedValue)));
        if (this.ddl_compPos.Items.Count > 0 && this.ddl_compPos.SelectedIndex > 0)
        {
            this.ddl_compPos.Enabled = true;
        }
        else
        {
            this.ddl_compPos.Enabled = false;
        }
    }

    protected void ddl_buscarSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.drops.Playa_Todos(this.ddl_buscarPlaya, 0, int.Parse(this.ddl_buscarSite.SelectedValue));
        this.drops.Playa(this.ddl_compPlaya, 0, int.Parse(this.ddl_buscarSite.SelectedValue));
        this.drops.Playa(this.ddl_playaDestino, 0, int.Parse(this.ddl_buscarSite.SelectedValue));
        this.btn_buscarMovimientos_Click(null, null);
    }

    #endregion

    #region GridView

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = this.utils.ConvertSortDirectionToSql((String)this.ViewState["sortOrder"]);
        this.ViewState["sortOrder"] = direccion;
        this.ViewState["sortExpresion"] = string.Format("{0} {1}", e.SortExpression, direccion);
        this.ObtenerMovimientos(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         if (e.CommandName == "DESTINO")
        {
                YMS_ZONA_BC m = new YMS_ZONA_BC();

                gv_listar.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                string YATA_COD = gv_listar.SelectedDataKey.Values[0].ToString();
                int YAHI_ID = int.Parse(gv_listar.SelectedDataKey.Values[1].ToString());
                int YAPE_ID = int.Parse(gv_listar.SelectedDataKey.Values[2].ToString());
                int LUGA_ID = int.Parse(gv_listar.SelectedDataKey.Values[3].ToString());
                int TRAI_ID = int.Parse(gv_listar.SelectedDataKey.Values[4].ToString()); 

                string resultado = "";
                UsuarioBC usuario = new UsuarioBC();
                usuario = (UsuarioBC)this.Session["Usuario"];
              // this.hf_id.Value = e.CommandArgument.ToString();
                   // m.Confirmar(int.Parse(hf_id.Value), u.ID, txt_compObs.Text, int.Parse(ddl_compMotivo.SelectedValue), int.Parse(ddl_compPos.SelectedValue), out resultado);
                    //m.Anular(int.Parse(this.hf_id.Value), int.Parse(this.ddl_confMotivo.SelectedValue), this.txt_compObs.Text, usuario.ID, out resultado);
                m.cuadra(YATA_COD, YAHI_ID, YAPE_ID, LUGA_ID, TRAI_ID, usuario.ID, out resultado);
             
                    if (resultado == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Accion Correcta');", true);
                        this.ObtenerMovimientos(true);
                        //   ObtenerMovimientos(true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", resultado), true);
                    }
  

        }

    //{
    //    if (e.CommandName == "DESTINO")
    //    {
    //        this.hf_id.Value = e.CommandArgument.ToString();
    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalDestino();", true);
    //    }
    //    if (e.CommandName == "ANULAR")
    //    {
    //        this.hf_id.Value = e.CommandArgument.ToString();
    //        MovimientoBC m = new MovimientoBC();
    //        this.utils.CargaDrop(this.ddl_confMotivo, "MOET_ID", "DESCRIPCION", m.obtenerXEstadoMovEstSubTipo(60, true));
    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalConfirmar();", true);
    //    }
    //    if (e.CommandName == "CONFIRMAR")
    //    {
    //        this.hf_id.Value = e.CommandArgument.ToString();
    //        MovimientoBC m = new MovimientoBC();
    //        this.utils.CargaDrop(this.ddl_compMotivo, "MOET_ID", "DESCRIPCION", m.obtenerXEstadoMovEstSubTipo(40, true));
    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalFinalizar();", true);
    //    }
    //    if (e.CommandName == "SUBIR")
    //    {
    //        this.hf_id.Value = e.CommandArgument.ToString();
    //        MovimientoBC m = new MovimientoBC();
    //        m.ID = Convert.ToInt32(this.hf_id.Value);
    //        m.CambiarOrden(true);
    //        this.ObtenerMovimientos(true);
    //    }
    //    if (e.CommandName == "BAJAR")
    //    {
    //        this.hf_id.Value = e.CommandArgument.ToString();
    //        MovimientoBC m = new MovimientoBC();
    //        m.ID = Convert.ToInt32(this.hf_id.Value);
    //        m.CambiarOrden(false);
    //        this.ObtenerMovimientos(true);
    //    }
    }

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        //    MovimientoBC m = new MovimientoBC();
            LinkButton btnconf = (LinkButton)e.Row.FindControl("btn_editar");
        //    LinkButton btnanular = (LinkButton)e.Row.FindControl("btn_anular");
        //    LinkButton btndestino = (LinkButton)e.Row.FindControl("btn_editar");
        //    LinkButton btn_subir = (LinkButton)e.Row.FindControl("btn_subir");
        //    LinkButton btn_bajar = (LinkButton)e.Row.FindControl("btn_bajar");
        //    int site_id = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SITE_ID"));
        //    int orden = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ORDEN"));
            btnconf.Visible = false;

            if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "YAPE_ACCION_MANUAL")) > 0)
            {
                btnconf.Visible = true;
                
            }


        //    if (orden == m.MinOrden(site_id))
        //    {
        //        btn_subir.Visible = false;
        //    }
        //    if (orden == m.MaxOrden(site_id))
        //    {
        //        btn_bajar.Visible = false;
        //    }

        //    //temporal
        //    btn_subir.Visible = false;
        //    btn_bajar.Visible = false;

        //    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MOES_ID")) >= 40)
        //    {
        //        btnconf.Visible = false;
        //        btnanular.Visible = false;
        //        btndestino.Visible = false;
        //    }
        //}
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    e.Row.CssClass = "header-color2";
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

    private void ObtenerMovimientos(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            this.ViewState.Remove("filtrados");
            YMS_ZONA_BC m = new YMS_ZONA_BC();
            DataTable dt = m.yardtag_cuadra_ObtenerTodos(txt_placa.Text,int.Parse(ddl_buscarPlaya.SelectedValue),0,int.Parse(ddl_buscarSite.SelectedValue));
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