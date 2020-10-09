// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Control_Descarga : System.Web.UI.Page
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
            this.drops.Site_Normal(this.ddl_buscarSite, this.u.ID);
            //SiteBC s = new SiteBC();
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
       
    protected void btn_cargafiltros_Click(object sender, EventArgs e)
    {
        SolicitudBC sol = new SolicitudBC();
        TransportistaBC t = new TransportistaBC();
        CargaTipoBC ct = new CargaTipoBC();

        //utils.CargaDropNormal(ddl_buscarSite, "ID", "NOMBRE", s.ObtenerTodos());
        //utils.CargaDrop(ddl_buscarAnden, "ID", "DESCRIPCION", l.obtenerTodoLugar(int.Parse(ddl_buscarSite.SelectedValue)));
        this.utils.CargaDropTodos(this.ddl_buscarTrans, "ID", "NOMBRE", t.ObtenerTodos());
        this.utils.CargaDropTodos(this.ddl_buscarTipoCarga, "ID", "DESCRIPCION", ct.obtenerTodo());
        this.ddl_buscarSite_SelectedIndexChanged(null, null);
    }
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

    protected void btn_bloquearAgregar_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC s = new SolicitudAndenesBC();
        string resultado;
        s.LUGA_ID = int.Parse(this.ddl_bloquearPos.SelectedValue);
        s.SOLI_ID = int.Parse(this.hf_idSolicitud.Value);

        if (s.Bloquear(this.u.ID, out resultado))
        {
            this.gv_bloqueo.DataSource = s.ObtenerBloqueados();
            this.gv_bloqueo.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", string.Format("showAlert('{0}');", resultado), true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", string.Format("alert('{0}');", resultado), true);
        }
    }

    protected void btn_finalizarExterno_Click(object sender, EventArgs e)
    {
        SolicitudBC solicitud = new SolicitudBC();
        solicitud.SOLI_ID = int.Parse(this.hf_idSolicitud.Value);
        string resultado = "";
        if (solicitud.DescargaCompleta(solicitud.SOLI_ID, out resultado, int.Parse(this.ddl_editPos.SelectedValue)))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "showAlert('Descarga completada');", true);
            this.ObtenerSolicitudes(true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", string.Format("alert('{0}');", resultado), true);
        }
    }

    protected void btn_posModificar_Click(object sender, EventArgs e)
    {
        SolicitudBC s = new SolicitudBC();
        s.SOLI_ID = int.Parse(this.hf_idSolicitud.Value);
        s.OBSERVACION = "";
        s.ID_DESTINO = int.Parse(this.ddl_editPos.SelectedValue);
        s.TIMESTAMP = DateTime.Parse(this.hf_timestamp.Value);
        if (s.validarTimeStamp())
        {
            if (s.ModificarDescarga(s))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "showAlert('Solicitud Modificada');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrarModal", "$('#modalPosicion').modal('hide');", true);
                this.ObtenerSolicitudes(true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "alert('Ocurrió un error!');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "alert('No se pudo validar timestamp.');", true);
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

    protected void btn_mover_Click(object sender, EventArgs e)
    {
        MovimientoBC m = new MovimientoBC();
        SolicitudBC s = new SolicitudBC();
        s = s.ObtenerXId(int.Parse(this.hf_idSolicitud.Value));
        string resultado;
        m.ID_DESTINO = int.Parse(this.ddl_editPos.SelectedValue);
        m.ID_TRAILER = s.ID_TRAILER;
        m.ID_SOLICITUD = s.SOLI_ID;
        m.OBSERVACION = "";
        bool ejecucion = s.DescargaMovimiento(m, s.ID_SITE, this.u.ID, out resultado);
        if (resultado == "" && ejecucion)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Se generó movimiento correctamente.');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrarModal", "$('#modalPosicion').modal('hide');", true);
            this.ObtenerSolicitudes(true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", resultado), true);
        }
    }

    #endregion

    #region DropDownList

    protected void ddl_buscarSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.drops.Playa_Todos(this.ddl_buscarPlaya, 0, int.Parse(this.ddl_buscarSite.SelectedValue));
        //PlayaBC p = new PlayaBC();
        //utils.CargaDropTodos(ddl_buscarPlaya, "ID", "DESCRIPCION", p.ObtenerDrop(int.Parse(ddl_buscarSite.SelectedValue)));
        //   ObtenerSolicitudes(true);
    }

    protected void ddl_editPlaya_IndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_editPlaya.SelectedIndex > 0)
        {
            this.drops.Lugar1(this.ddl_editPos, 0, int.Parse(this.ddl_editPlaya.SelectedValue), 1);
            if (this.ddl_editPos.Items.Count > 1)
            {
                this.ddl_editPos.Enabled = true;
            }
            else
            {
                this.ddl_editPos.Enabled = false;
            }
        }
        else
        {
            this.ddl_editPos.ClearSelection();
            this.ddl_editPos.Enabled = false;
        }
    }

    protected void ddl_editZona_IndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_editZona.SelectedIndex > 0)
        {
            this.drops.Playa(this.ddl_editPlaya, int.Parse(this.ddl_editZona.SelectedValue));
            this.ddl_editPlaya_IndexChanged(null, null);
        }
        else
        {
            this.ddl_editPlaya.ClearSelection();
            this.ddl_editPlaya.Enabled = false;
            this.ddl_editPos.ClearSelection();
            this.ddl_editPos.Enabled = false;
        }
    }

    #endregion

    #region GridView

    protected void gv_bloqueo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "QUITAR")
        {
            SolicitudAndenesBC s = new SolicitudAndenesBC();
            string resultado;
            s.SOLI_ID = int.Parse(this.hf_idSolicitud.Value);
            s.LUGA_ID = int.Parse(e.CommandArgument.ToString());
            if (s.Liberar(this.u.ID, out resultado))
            {
                this.gv_bloqueo.DataSource = s.ObtenerBloqueados();
                this.gv_bloqueo.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", string.Format("showAlert('{0}');", resultado), true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", string.Format("alert('{0}');", resultado), true);
            }
        }
    }

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
            LinkButton btnCompletar = (LinkButton)e.Row.FindControl("btn_descargaCompleta");
            LinkButton btnEditar = (LinkButton)e.Row.FindControl("btn_editar");
            LinkButton btn_mover = (LinkButton)e.Row.FindControl("btn_mover");
            LinkButton btn_editarPos = (LinkButton)e.Row.FindControl("btn_editarPos");
                       
            switch (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID_ESTADOSOLICITUD")))
            {
                case 200 : //Solicitud Creada
                    btnEditar.Visible = true;
                    btnCompletar.Visible = false;
                    btn_mover.Visible = true;
                    btn_editarPos.Visible = true;
                    break;
                case 205: //casi descarga
                    btnEditar.Visible = false;
                    btnCompletar.Visible = false;
                    btn_mover.Visible = false;
                    btn_editarPos.Visible = true;
                    break;
                case 210: //decargando
                    btnCompletar.Visible = true;
                    btnEditar.Visible = false;
                    btn_mover.Visible = false;
                    btn_editarPos.Visible = true;
                    break;
             
                case 230: //Carga Completa
                    btnCompletar.Visible = false;
                    btnEditar.Visible = false;
                    btn_mover.Visible = true;
                    btn_editarPos.Visible = true;
                    break;

                default:
                    btnCompletar.Visible = false;
                    btnEditar.Visible = false;
                    btn_mover.Visible = false;
                    btn_editarPos.Visible = false;
                    break;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.CssClass = "header-color2";
        }
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "COMPLETAR")
        {
            this.hf_idSolicitud.Value = e.CommandArgument.ToString();
            if (this.comprobarTrailerEx())
            {
                this.btn_mover.Visible = false;
                this.btn_posModificar.Visible = false;
                this.btn_cargaFin.Visible = true;
                this.btn_finalizarExterno.Visible = true;
                this.llenarForm();
            }
            else
            {
                //btn_cargaFin.Visible = true;
                this.btn_mover.Visible = false;
                this.btn_finalizarExterno.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
            }
        }
        if (e.CommandName == "Edit")
        {
            this.hf_idSolicitud.Value = e.CommandArgument.ToString();
            string id = this.hf_idSolicitud.Value;
            string url = string.Format("Solicitud_Descarga.aspx?soli_id={0}&type=edit", id);
            this.Response.Redirect(url);
        }
        if (e.CommandName == "EDITPOS")
        {
            this.hf_idSolicitud.Value = e.CommandArgument.ToString();
            this.btn_mover.Visible = false;
            this.btn_posModificar.Visible = true;
            this.btn_cargaFin.Visible = false;
            this.btn_finalizarExterno.Visible = false;
            this.llenarForm();
        }
        if (e.CommandName == "POSICION")
        {
            this.hf_idSolicitud.Value = e.CommandArgument.ToString();
            this.btn_mover.Visible = true;
            this.btn_posModificar.Visible = false;
            this.btn_cargaFin.Visible = false;
            this.btn_finalizarExterno.Visible = false;
            this.llenarForm();
        }
        if (e.CommandName == "BLOQUEAR")
        {
            this.hf_idSolicitud.Value = e.CommandArgument.ToString();
            SolicitudAndenesBC s = new SolicitudAndenesBC();
            s.SOLI_ID = int.Parse(this.hf_idSolicitud.Value);
            //LugarBC l = new LugarBC();
            int play_id = s.ObtenerPlayaId();
            this.drops.Lugar1(this.ddl_bloquearPos, 0, play_id, 1);
            //utils.CargaDrop(ddl_bloquearPos, "ID", "DESCRIPCION", l.ObtenerXPlaya(play_id));
            this.gv_bloqueo.DataSource = s.ObtenerBloqueados();
            this.gv_bloqueo.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalBloqueoAnden();", true);
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

    private void llenarForm()
    {
        SolicitudBC s = new SolicitudBC();
        TrailerBC t = new TrailerBC();
        LugarBC l = new LugarBC();
        ZonaBC z = new ZonaBC();
        s = s.ObtenerXId(int.Parse(this.hf_idSolicitud.Value));
        this.hf_timestamp.Value = s.TIMESTAMP.ToString();
        t = t.obtenerXID(s.ID_TRAILER);
        l = l.obtenerXID(t.LUGAR_ID);
        this.utils.CargaDropTodos(this.ddl_editZona, "ID", "DESCRIPCION", z.ObtenerXSite(s.ID_SITE, false));
        this.ddl_editZona.SelectedValue = l.ID_ZONA.ToString();
        this.ddl_editZona_IndexChanged(null, null);
        this.ddl_editPlaya.SelectedValue = l.ID_PLAYA.ToString();
        this.ddl_editPlaya_IndexChanged(null, null);
        this.ddl_editPos.SelectedValue = l.ID.ToString();
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalPosicion();", true);
    }

    protected void ObtenerSolicitudes(bool forzarBD)
    {
        SolicitudBC solicitud = new SolicitudBC();
        if (this.ViewState["lista"] == null || forzarBD)
        {
            DataTable dt = solicitud.ObtenerSolicitudesDescarga(int.Parse(this.ddl_buscarSite.SelectedValue));
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