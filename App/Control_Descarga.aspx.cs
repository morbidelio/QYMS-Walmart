using System;
using System.Collections.Generic;
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
        if (Session["Usuario"] != null)
        {
            u = (UsuarioBC)Session["Usuario"];
        }
        else
            Response.Redirect("../InicioQYMS.aspx");

        if (!IsPostBack)
        {
            //SiteBC s = new SiteBC();
            TransportistaBC t = new TransportistaBC();
            CargaTipoBC ct = new CargaTipoBC();
            drops.Site_Normal(ddl_buscarSite, u.ID);
            //utils.CargaDropNormal(ddl_buscarSite, "ID", "NOMBRE", s.ObtenerTodos());
            //utils.CargaDrop(ddl_buscarAnden, "ID", "DESCRIPCION", l.obtenerTodoLugar(int.Parse(ddl_buscarSite.SelectedValue)));
            utils.CargaDropTodos(ddl_buscarTrans, "ID", "NOMBRE", t.ObtenerTodos());
            utils.CargaDropTodos(ddl_buscarTipoCarga, "ID", "DESCRIPCION", ct.obtenerTodo());
            ddl_buscarSite_SelectedIndexChanged(null, null);
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (u.numero_sites < 2)
        {
            this.SITE.Visible = false;
        }
    }

    #region Buttons

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)ViewState["lista"];
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

    protected void btn_bloquearAgregar_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC s = new SolicitudAndenesBC();
        string resultado;
        s.LUGA_ID = int.Parse(ddl_bloquearPos.SelectedValue);
        s.SOLI_ID = int.Parse(hf_idSolicitud.Value);

        if (s.Bloquear(u.ID, out resultado))
        {
            gv_bloqueo.DataSource = s.ObtenerBloqueados();
            gv_bloqueo.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert('" + resultado + "');", true);
        }

        else

        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('" + resultado + "');", true);
        }

       
    }

    protected void btn_finalizarExterno_Click(object sender, EventArgs e)
    {
        SolicitudBC solicitud = new SolicitudBC();
        solicitud.SOLI_ID = int.Parse(hf_idSolicitud.Value);
        string resultado = "";
        if (solicitud.DescargaCompleta(solicitud.SOLI_ID,out resultado, int.Parse(ddl_editPos.SelectedValue)))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "showAlert('Descarga completada');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf1", "$('#modalPosicion').modal('hide');", true);
          
            ObtenerSolicitudes(true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "alert('" +resultado +"');", true);
    }

    protected void btn_posModificar_Click(object sender, EventArgs e)
    {
        SolicitudBC s = new SolicitudBC();
        s.SOLI_ID = int.Parse(hf_idSolicitud.Value);
        s.OBSERVACION = "";
        s.ID_DESTINO = int.Parse(ddl_editPos.SelectedValue);
        s.TIMESTAMP = DateTime.Parse(hf_timestamp.Value);
        if (s.validarTimeStamp())
        {
            if (s.ModificarDescarga(s))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "showAlert('Solicitud Modificada');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrarModal", "$('#modalPosicion').modal('hide');", true);
                ObtenerSolicitudes(true);
            }
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "alert('Ocurrió un error!');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "alert('No se pudo validar timestamp.');", true);
    }

    protected void cargaFin_Click(object sender, EventArgs e)
    {
        SolicitudBC solicitud = new SolicitudBC();
        solicitud.SOLI_ID = int.Parse(hf_idSolicitud.Value);
        string resultado = "";
         UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)Session["Usuario"];
        solicitud.DescargaCompleta(solicitud.SOLI_ID, out resultado, usuario.ID );
        if (resultado=="")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "showAlert('Descarga completada');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf1", "$('#modalConfirmacion').modal('hide');", true);
         
            ObtenerSolicitudes(true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "alert('"+resultado + "');", true);
    }

    protected void btn_buscarSolicitud_Click(object sender, EventArgs e)
    {

        ObtenerSolicitudes(true);

        DataTable dt = (DataTable)ViewState["lista"];
        DataView dw = dt.AsDataView();
        string filtro = "";
        bool segundo = false;
        if (ddl_buscarTrans.SelectedIndex > 0)
        {
            filtro += "TRAN_ID = " + ddl_buscarTrans.SelectedValue + " ";
            segundo = true;
        }
        if (ddl_buscarTipoCarga.SelectedIndex > 0)
        {
            if (segundo)
                filtro += "AND ";
            filtro += "TIIC_ID = " + ddl_buscarTipoCarga.SelectedValue + " ";
            segundo = true;
        }
        if (ddl_buscarPlaya.SelectedIndex > 0)
        {
            if (segundo)
                filtro += "AND ";
            filtro += "PLAY_ID = " + ddl_buscarPlaya.SelectedValue + " ";
            segundo = true;
        }
        if (!string.IsNullOrWhiteSpace(filtro))
        {
            dw.RowFilter = filtro;
            ViewState["filtrados"] = dw.ToTable();
            ObtenerSolicitudes(false);
        }
      //  else
       //     ObtenerSolicitudes(true);
    }

    protected void btn_mover_Click(object sender, EventArgs e)
    {
        MovimientoBC m = new MovimientoBC();
        SolicitudBC s = new SolicitudBC();
        s = s.ObtenerXId(int.Parse(hf_idSolicitud.Value));
        string resultado;
        m.ID_DESTINO = int.Parse(ddl_editPos.SelectedValue);
        m.ID_TRAILER = s.ID_TRAILER;
        m.ID_SOLICITUD = s.SOLI_ID;
        m.OBSERVACION = "";
        bool ejecucion = s.DescargaMovimiento(m, s.ID_SITE, u.ID, out resultado);
        if (resultado == "" && ejecucion)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Se generó movimiento correctamente.');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrarModal", "$('#modalPosicion').modal('hide');", true);
            ObtenerSolicitudes(true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + resultado + "');", true);
        }
    }

    #endregion

    #region DropDownList

    protected void ddl_buscarSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        drops.Playa_Todos(ddl_buscarPlaya, 0, int.Parse(ddl_buscarSite.SelectedValue));
        //PlayaBC p = new PlayaBC();
        //utils.CargaDropTodos(ddl_buscarPlaya, "ID", "DESCRIPCION", p.ObtenerDrop(int.Parse(ddl_buscarSite.SelectedValue)));
        ObtenerSolicitudes(true);
    }

    protected void ddl_editPlaya_IndexChanged(object sender, EventArgs e)
    {
        if (ddl_editPlaya.SelectedIndex > 0)
        {
            drops.Lugar1(ddl_editPos, 0, int.Parse(ddl_editPlaya.SelectedValue),0,1);
            if (ddl_editPos.Items.Count > 1)
                ddl_editPos.Enabled = true;
            else
                ddl_editPos.Enabled = false;
        }
        else
        {
            ddl_editPos.ClearSelection();
            ddl_editPos.Enabled = false;
        }
    }

    protected void ddl_editZona_IndexChanged(object sender, EventArgs e)
    {
        if (ddl_editZona.SelectedIndex > 0)
        {
            drops.Playa(ddl_editPlaya, int.Parse(ddl_editZona.SelectedValue));
            ddl_editPlaya_IndexChanged(null, null);
        }
        else
        {
            ddl_editPlaya.ClearSelection();
            ddl_editPlaya.Enabled = false;
            ddl_editPos.ClearSelection();
            ddl_editPos.Enabled = false;
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
            s.SOLI_ID =int.Parse(hf_idSolicitud.Value);
            s.LUGA_ID = int.Parse(e.CommandArgument.ToString());
            if (s.Liberar(u.ID, out resultado))
            {
                gv_bloqueo.DataSource = s.ObtenerBloqueados();
                gv_bloqueo.DataBind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert('" + resultado + "');", true);

            }
            else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('" + resultado + "');", true);
        }
    }

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerSolicitudes(false);
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
                    btn_mover.Visible = false;
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
                    btn_mover.Visible = true;
                    btn_editarPos.Visible = true;
                    break;
             
                case 230: //Carga Completa
                    btnCompletar.Visible = false;
                    btnEditar.Visible = false;
                    btn_mover.Visible = false;
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
            hf_idSolicitud.Value = e.CommandArgument.ToString();
            if (comprobarTrailerEx())
            {
                btn_mover.Visible = false;
                btn_posModificar.Visible = false;
                btn_cargaFin.Visible = false;
                btn_finalizarExterno.Visible = true;
                llenarForm();
            }
            else
            {
                btn_cargaFin.Visible = true;
                btn_mover.Visible = false;
                btn_finalizarExterno.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalConfirmacion();", true);
            }
        }

        if (e.CommandName == "Edit")
        {
            hf_idSolicitud.Value = e.CommandArgument.ToString();
            string id = hf_idSolicitud.Value;
            string url = "Solicitud_Descarga.aspx?soli_id=" + id + "&type=edit";
            Response.Redirect(url);
        }
        if (e.CommandName == "EDITPOS")
        {
            hf_idSolicitud.Value = e.CommandArgument.ToString();
            btn_mover.Visible = false;
            btn_posModificar.Visible = true;
            btn_cargaFin.Visible = false;
            btn_finalizarExterno.Visible = false;
            llenarForm();
        }
        if (e.CommandName == "POSICION")
        {
            hf_idSolicitud.Value = e.CommandArgument.ToString();
            btn_mover.Visible = true;
            btn_posModificar.Visible = false;
            btn_cargaFin.Visible = false;
            btn_finalizarExterno.Visible = false;
            llenarForm();
        }
        if (e.CommandName == "BLOQUEAR")
        {
            hf_idSolicitud.Value = e.CommandArgument.ToString();
            SolicitudAndenesBC s = new SolicitudAndenesBC();
            s.SOLI_ID = int.Parse(hf_idSolicitud.Value);
            //LugarBC l = new LugarBC();
            int play_id = s.ObtenerPlayaId();
            drops.Lugar1(ddl_bloquearPos, 0, play_id,1);
            //utils.CargaDrop(ddl_bloquearPos, "ID", "DESCRIPCION", l.ObtenerXPlaya(play_id));
            gv_bloqueo.DataSource = s.ObtenerBloqueados();
            gv_bloqueo.DataBind();
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
        s = s.ObtenerXId(int.Parse(hf_idSolicitud.Value));
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
        s = s.ObtenerXId(int.Parse(hf_idSolicitud.Value));
        hf_timestamp.Value = s.TIMESTAMP.ToString();
        t = t.obtenerXID(s.ID_TRAILER);
        l = l.obtenerXID(t.LUGAR_ID);
        utils.CargaDropTodos(ddl_editZona, "ID", "DESCRIPCION", z.ObtenerXSite(s.ID_SITE,false));
        ddl_editZona.SelectedValue = l.ID_ZONA.ToString();
        ddl_editZona_IndexChanged(null, null);
        ddl_editPlaya.SelectedValue = l.ID_PLAYA.ToString();
        ddl_editPlaya_IndexChanged(null, null);
        try

        {ddl_editPos.SelectedValue = l.ID.ToString();
        }  
       catch (Exception e)

        {}
      
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "asdf", "modalPosicion();", true);
    }

    protected void ObtenerSolicitudes(bool forzarBD)
    {
        SolicitudBC solicitud = new SolicitudBC();
        if (ViewState["lista"] == null || forzarBD)
        {
            DataTable dt = solicitud.ObtenerSolicitudesDescarga(int.Parse(ddl_buscarSite.SelectedValue));
            ViewState["lista"] = dt;
            ViewState.Remove("filtrados");
        }
        DataView dw;
        if (ViewState["filtrados"] == null)
            dw = new DataView((DataTable)ViewState["lista"]);
        else
            dw = new DataView((DataTable)ViewState["filtrados"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }
    #endregion

    // viewstate
    protected override void SavePageStateToPersistenceMedium(object state)
    {

        string file = GenerateFileName();

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



            StreamReader reader = new StreamReader(GenerateFileName());

            LosFormatter formator = new LosFormatter();

            state = formator.Deserialize(reader);

            reader.Close();


        }
        catch (Exception ex)
        {
            Response.Redirect(Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath) + ".aspx");
        }
        return state;
    }

    private string GenerateFileName()
    {

        string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);

        string file = pageName + Session.SessionID.ToString() + ".txt";

        //       file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);  
        file = utils.pathviewstate() + "\\" + file;

        return file;

    }  
}