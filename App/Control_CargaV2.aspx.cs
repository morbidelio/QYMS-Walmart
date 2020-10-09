using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Control_Cargav2 : System.Web.UI.Page
{
    UsuarioBC usuario = new UsuarioBC();
    UtilsWeb utils = new UtilsWeb();
    CargaDrops drops = new CargaDrops();

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (Session["Usuario"] != null)
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)Session["Usuario"];

            if (usuario.numero_sites < 2)
            {
                this.SITE.Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("../InicioQYMS.aspx");
        usuario = (UsuarioBC)Session["Usuario"];
        if (!IsPostBack)
        {
            YMS_ZONA_BC y = new YMS_ZONA_BC();
            utils.CargaDropNormal(ddl_buscarSite, "ID", "NOMBRE", y.ObteneSites(usuario.ID));
    
        }
    }

    #region TextBox

    protected void txt_buscaLocal_TextChanged(object sender, EventArgs e)
    {
        LocalBC l = new LocalBC();
        l = l.obtenerXCodigo(this.txt_buscaLocal.Text);

        if (l.ID != 0)
        {
            this.ViewState["id_local"] = l.ID;
            this.txt_descLocal.Text = string.Format("{0}({1})", l.CODIGO2, l.VALOR_CARACT_MAXIMO);
        }
        else
        {
            this.ViewState["id_local"] = null;
            this.txt_descLocal.Text = "Local no encontrado";
        }
    }

    #endregion

    #region GridView

    protected void gv_listar_Sorting(object sender, GridViewSortEventArgs e)
    {
        string direccion = utils.ConvertSortDirectionToSql((String)ViewState["sortOrder"]);
        ViewState["sortOrder"] = direccion;
        ViewState["sortExpresion"] = e.SortExpression + " " + direccion;
        ObtenerSolicitudes(false);
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Sort")
        {
            //      int index = int.Parse(e.CommandArgument.ToString());
            string[] arg = new string[3];
            arg = e.CommandArgument.ToString().Split(';');
            hf_idSolicitud.Value = arg[0];//  index.ToString();// gv_Carga.DataKeys[index].Values[0].ToString();
            hf_idLugar.Value = arg[1];// gv_Carga.DataKeys[index].Values[1].ToString();
            hf_orden.Value = arg[2];// gv_Carga.DataKeys[index].Values[2].ToString();
            txt_fechaCarga.Text = DateTime.Now.ToShortDateString();
            txt_horaCarga.Text =  DateTime.Now.ToShortTimeString();
            hf_idEstado.Value = e.CommandName;
            switch (e.CommandName)
            {
                case "Cargado":
                    dv_pallets.Visible = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalCarga();", true);
                    break;
                case "Parcial":
                    dv_pallets.Visible = true;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalCarga();", true);
                    break;
                case "Continuar":
                    SolicitudBC s = new SolicitudBC();
                    s = s.ObtenerXId(int.Parse(hf_idSolicitud.Value));
                    drops.Lugar1(ddl_origenAnden, 0, s.PLAY_ID, 0,1);
                    hf_caractSolicitud.Value = s.CARACTERISTICAS;
                    hf_localesSeleccionados.Value = s.LOCALES;
                    hf_timeStamp.Value = s.TIMESTAMP.ToString();
                    DataTable dsol = s.ObtenerAndenesXSolicitudId(s.SOLI_ID);
                    Ordenar(dsol, true);
                    gv_solLocales.DataSource = ViewState["datosA"];
                    gv_solLocales.DataBind();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalReanudar();", true);
                    break;
                case "Edit":
                    string id = hf_idSolicitud.Value;
                    string url = "Solicitud_Carga.aspx?id=" + id + "&type=edit";
                    Response.Redirect(url);
                    break;
                case "colocar_sello":
                    validar_sello();
                    break;
                case "validar_sello":
                    validado_sello();
                    break;
            }
        }
    }

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.CssClass = "header-color2";
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id_solicitud = int.Parse(gv_listar.DataKeys[e.Row.RowIndex].Values[0].ToString());
            if (e.Row.RowIndex > 0)
            {
                int anterior = int.Parse(gv_listar.DataKeys[e.Row.RowIndex - 1].Values[0].ToString());
                if (anterior != id_solicitud)
                {
                    if (gv_listar.Rows[e.Row.RowIndex - 1].CssClass == "")
                        e.Row.CssClass = "row-color2";
                }
                else
                {
                    e.Row.CssClass = gv_listar.Rows[e.Row.RowIndex - 1].CssClass;
                }
            }
            LinkButton btnCompletar = (LinkButton)e.Row.FindControl("btn_cargado");
            btnCompletar.Visible = true;
            LinkButton btnParcial = (LinkButton)e.Row.FindControl("btn_cargaParcial");
            btnParcial.Visible = true;
            LinkButton btnContinuar = (LinkButton)e.Row.FindControl("btn_cargaContinuar");
            btnContinuar.Visible = false;
            LinkButton btnEditar = (LinkButton)e.Row.FindControl("btn_editar");

            LinkButton btncolocarsello = (LinkButton)e.Row.FindControl("btn_sello");
            //    btncolocarsello.Visible = false;
            LinkButton btnvalidasello = (LinkButton)e.Row.FindControl("btn_valida_sello");
            //   btnvalidasello.Visible = false;
            //DataBinder.Eval(e.Row.DataItem, "ID_ESTADOSOLICITUD").ToString();
            switch (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID_ESTADOSOLICITUD").ToString()))
            {
                case (int)SolicitudBC.estado_solicitud.SolicitudAndenesCreada: //Solicitud Creada
                    btncolocarsello.Visible = false;
                    btnvalidasello.Visible = false;
                    btnParcial.Visible = false;
                    btnCompletar.Visible = false;
                    break;

                case 101: //Solicitud Creada
                    btncolocarsello.Visible = false;
                    btnvalidasello.Visible = false;
                    btnParcial.Visible = false;
                    btnCompletar.Visible = false;
                    break;

                case 105: //casi carga
                    btncolocarsello.Visible = false;
                    btnvalidasello.Visible = false;
                    btnParcial.Visible = false;
                    btnCompletar.Visible = false;
                    btnEditar.Visible = false;
                    break;

                case 120: //reanudar carga
                    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID_ESTADOANDEN")) == 110)
                    {
                        btnCompletar.Visible = true;
                        btnParcial.Visible = true;
                        btnContinuar.Visible = false;
                        btnEditar.Visible = false;
                        btncolocarsello.Visible = false;
                        btnvalidasello.Visible = false;
                    }
                    else
                    {
                        btnCompletar.Visible = false;
                        btnParcial.Visible = false;
                        btnContinuar.Visible = false;
                        btnEditar.Visible = false;
                        btncolocarsello.Visible = false;
                        btnvalidasello.Visible = false;
                    }
                    break;


                case 110: //cargando
                    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID_ESTADOANDEN")) == 110)
                    {
                        btnCompletar.Visible = true;
                        btnParcial.Visible = true;
                        btnContinuar.Visible = false;
                        btnEditar.Visible = false;
                        btncolocarsello.Visible = false;
                        btnvalidasello.Visible = false;
                    }
                    else
                    {
                        btnCompletar.Visible = false;
                        btnParcial.Visible = false;
                        btnContinuar.Visible = false;
                        btnEditar.Visible = false;
                        btncolocarsello.Visible = false;
                        btnvalidasello.Visible = false;
                    }
                    break;
                case 125: //Suspendida

                    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID_ESTADOANDEN")) == 120) //Solicitud andén siguiente a la que interrumpió la carga
                    {
                        btnCompletar.Visible = false;
                        btnParcial.Visible = false;
                        btnContinuar.Visible = true;
                        btnEditar.Visible = false;
                        btncolocarsello.Visible = false;
                        btnvalidasello.Visible = false;

                    }
                    else if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID_ESTADOANDEN")) == 100) //Solicitud andén siguiente a la que interrumpió la carga
                    {
                        btnCompletar.Visible = false;
                        btnParcial.Visible = false;
                        btnContinuar.Visible = true;
                        btnEditar.Visible = false;
                        btncolocarsello.Visible = false;
                        btnvalidasello.Visible = false;

                    }

                    break;
                case 132: //Carga Completa
                    btnCompletar.Visible = false;
                    btnParcial.Visible = false;
                    btnContinuar.Visible = false;
                    btnEditar.Visible = false;
                    btncolocarsello.Visible = true;
                    btnvalidasello.Visible = false;

                    break;

                case 142: //sello colocado 
                    btnCompletar.Visible = false;
                    btnParcial.Visible = false;
                    btnContinuar.Visible = false;
                    btnEditar.Visible = false;
                    btncolocarsello.Visible = false;
                    btnvalidasello.Visible = true;

                    break;

                case 150: //Solicitud Finalizada
                    btnCompletar.Visible = false;
                    btnParcial.Visible = false;
                    btnContinuar.Visible = false;
                    btnEditar.Visible = false;
                    btncolocarsello.Visible = false;
                    btnvalidasello.Visible = false;
                    break;

                case 148: //Solicitud Finalizada
                    btnCompletar.Visible = false;
                    btnParcial.Visible = false;
                    btnContinuar.Visible = false;
                    btnEditar.Visible = false;
                    btncolocarsello.Visible = false;
                    btnvalidasello.Visible = false;
                    break;

                default:
                    btnCompletar.Visible = false;
                    btnParcial.Visible = false;
                    btnContinuar.Visible = false;
                    btnEditar.Visible = false;
                    btncolocarsello.Visible = false;
                    btnvalidasello.Visible = false;

                    break;
            }
        }
    }

    protected void gv_listar_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
            e.Row.CssClass = "header-color2";
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
    }

    protected void gv_solLocales_rowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnsubir = (LinkButton)e.Row.FindControl("btnSubir");
            LinkButton btnbajar = (LinkButton)e.Row.FindControl("btnBajar");
            DataTable dt = (DataTable)ViewState["datosA"];
            int index = e.Row.RowIndex;
            int registros = 0;
            try
            {
                registros = dt.Rows.Count;
            }
            catch (NullReferenceException)
            {
                registros = 0;
            }

            int cell = int.Parse(DataBinder.Eval(e.Row.DataItem, "SOES_ID").ToString());
            if (cell != -1)
            {
                LinkButton btn_eliminar = (LinkButton)e.Row.FindControl("btn_eliminarLocal");
                DataView dw = dt.AsDataView();
                dw.RowFilter = "SOES_ID = -1";
                if (dw.ToTable().Rows.Count == 0 && cell == 100)
                    btn_eliminar.Visible = true;
                else 
                    btn_eliminar.Visible = false;
                btnsubir.Visible = false;
                btnbajar.Visible = false;
            }
            else
            {
                if (index == 0)
                {
                    btnsubir.Visible = false;
                    if (registros == 0)
                        btnbajar.Visible = false;
                }
                if (index == registros - 1)
                {
                    btnbajar.Visible = false;
                }
                if (registros > 1 && index > 0)
                {
                    try
                    {
                        cell = int.Parse(dt.Rows[index - 1]["SOES_ID"].ToString());
                        if (cell != -1)
                            btnsubir.Visible = false;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        btnsubir.Visible = false;
                    }
                    try
                    {
                        cell = int.Parse(dt.Rows[index + 1]["SOES_ID"].ToString());
                        if (cell != -1)
                            btnbajar.Visible = false;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        btnbajar.Visible = false;
                    }
                }
            }
        }
    }

    protected void gv_solLocales_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            Int32 index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)ViewState["datosA"];
            if (dt.Rows[index]["SOES_ID"].ToString() == "-1")
            {
                dt.Rows.RemoveAt(index);
                Ordenar(dt, true);
                this.gv_solLocales.DataSource = ViewState["datosA"];
                this.gv_solLocales.DataBind();
                this.stringLocalesSeleccionados((DataTable)ViewState["datosA"]);
                marca_seleccion();
                this.calcula_solicitud(null, null);
            }
            else
            {
                hf_idSolicitud.Value = dt.Rows[index]["SOLI_ID"].ToString();
                hf_idLocal.Value = dt.Rows[index]["ID_LOCAL"].ToString();
                hf_orden.Value = dt.Rows[index]["SOAN_ORDEN"].ToString();
                hf_idLugar.Value = dt.Rows[index]["LUGA_ID"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalConfirmar();", true);
            }
        }
        if (e.CommandName == "BAJAR")
        {
            Int32 index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)ViewState["datosA"];

            int sold_a = int.Parse(dt.Rows[index]["ORDEN"].ToString());
            int sold_b = int.Parse(dt.Rows[index + 1]["ORDEN"].ToString());
            dt.Rows[index]["ORDEN"] = sold_b;
            dt.Rows[index + 1]["ORDEN"] = sold_a;
            Ordenar(dt, false);
            this.gv_solLocales.DataSource = ViewState["datosA"];
            this.gv_solLocales.DataBind();
        }
        if (e.CommandName == "SUBIR")
        {
            Int32 index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)ViewState["datosA"];

            int sold_a = int.Parse(dt.Rows[index]["ORDEN"].ToString());
            int sold_b = int.Parse(dt.Rows[index - 1]["ORDEN"].ToString());
            dt.Rows[index]["ORDEN"] = sold_b;
            dt.Rows[index - 1]["ORDEN"] = sold_a;
            Ordenar(dt, false);
            this.gv_solLocales.DataSource = ViewState["datosA"];
            this.gv_solLocales.DataBind();
        }
    }

    #endregion

    #region Buttons

    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        SolicitudLocalesBC s = new SolicitudLocalesBC();
        s.SOLI_ID = int.Parse(hf_idSolicitud.Value);
        s.LOCA_ID = int.Parse(hf_idLocal.Value);
        s.SOAN_ORDEN = int.Parse(hf_orden.Value);
        s.LUGA_ID = int.Parse(hf_idLugar.Value);
        if (s.Eliminar())
        {
            SolicitudBC sol = new SolicitudBC();
            Ordenar(sol.ObtenerAndenesXSolicitudId(s.SOLI_ID), true);
            gv_solLocales.DataSource = ViewState["datosA"];
            gv_solLocales.DataBind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Registro eliminado!');", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalConfirmar');", true);
        }
    }

    protected void btn_cargafiltros_Click(object sender, EventArgs e)
    {
        SolicitudBC sol = new SolicitudBC();
        TransportistaBC t = new TransportistaBC();
        drops.Playa_Todos(ddl_buscarPlaya, 0, int.Parse(ddl_buscarSite.SelectedValue));
        drops.Lugar_Todos(ddl_buscarAnden, int.Parse(ddl_buscarPlaya.SelectedValue));
        utils.CargaDrop(ddl_buscarEstado, "ID", "DESCRIPCION", sol.ObtenerEstadosCarga());
        utils.CargaDrop(ddl_buscarTransportista, "ID", "NOMBRE", t.ObtenerTodos());
    }

    protected void btn_agregarCarga_Click(object sender, EventArgs e)
    {
        if (this.ViewState["id_local"] == null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe seleccionar local.');", true);
        }
        else if (ddl_origenAnden.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe ingresar un andén de origen.');", true);
        }
        else
        {
            DataTable dt;
            if (ViewState["datosA"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("ID_ANDEN");
                dt.Columns.Add("SOLI_ID");
                dt.Columns.Add("ID_LOCAL");
                dt.Columns.Add("ORDEN");
                dt.Columns.Add("NUMERO_LOCAL");
                dt.Columns.Add("NOMBRE_LOCAL");
                dt.Columns.Add("LUGA_ID");
                dt.Columns.Add("ANDEN");
                dt.Columns.Add("PALLETS");
                dt.Columns.Add("SOES_ID");
                dt.Columns.Add("SOAN_ORDEN");
                ViewState["datosA"] = dt;
            }
            dt = ViewState["datosA"] as DataTable;
            LocalBC l = new LocalBC(); 
            l.ID = (int)ViewState["id_local"];
            l = l.obtenerXID();
            //DataRow local = (DataRow)this.Session["id_local"];
            int maximo = 0;

            try
            {
                maximo = Math.Min(Convert.ToInt32(this.Session["MaxPallet"]), l.VALOR_CARACT_MAXIMO);
            }
            catch (Exception)
            {
                maximo = l.VALOR_CARACT_MAXIMO;
            }

            if (maximo == 0)
            {
                maximo = l.VALOR_CARACT_MAXIMO;
            }

            dt.Rows.Add(this.ddl_origenAnden.SelectedValue, hf_idSolicitud.Value, l.ID, null, l.CODIGO, l.DESCRIPCION, this.ddl_origenAnden.SelectedValue, this.ddl_origenAnden.SelectedItem.Text, 0, -1, null);
            Ordenar(dt,true);
            this.gv_solLocales.DataSource = ViewState["datosA"];
            this.gv_solLocales.DataBind();

            this.calcula_solicitud(null, null);
            this.ViewState["id_local"] = null;
            this.txt_descLocal.Text = "";
            this.txt_buscaLocal.Text = "";

        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)ViewState["lista"];
        GridView gv = new GridView();
        gv.DataSource = view;
        gv.DataBind();

        string fileName = "reporte_Carga.xls";
        string Extension = ".xls";
        if (Extension == ".xls")
        {
            PrepareControlForExport(gv);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-type", "application / xls");

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

    protected void btn_buscarSolicitud_Click(object sender, EventArgs e)
    {
        string filtro = "";
        bool primero = true;
        if (ddl_buscarPlaya.SelectedIndex > 0)
        {
            primero = false;
            filtro += "PLAY_ID = " + ddl_buscarPlaya.SelectedValue + " ";
        }
        if (ddl_buscarAnden.SelectedIndex > 0)
        {
            if (!primero)
                filtro += "and ";
            primero = false;
            filtro += "ID_LUGAR = " + ddl_buscarAnden.SelectedValue + " ";
        }
        if (!string.IsNullOrEmpty(txt_buscarNumero.Text))
        {
            if (!primero)
                filtro += ", ";
            primero = false;
            filtro += "ID_SOLICITUD = " + txt_buscarNumero.Text + " ";
        }
        if (ddl_buscarEstado.SelectedIndex > 0)
        {
            if (!primero)
                filtro += "and ";
            primero = false;
            filtro += "ID_ESTADOSOLICITUD = " + ddl_buscarEstado.SelectedValue + " ";
        }
        if (ddl_buscarTransportista.SelectedIndex > 0)
        {
            if (!primero)
                filtro += "and ";
            primero = false;
            filtro += "ID_TRANSPORTISTA = " + ddl_buscarTransportista.SelectedValue + " ";
        }
        if (string.IsNullOrEmpty(filtro))
            ObtenerSolicitudes(true);
        else
        {
            DataTable dt = (DataTable)ViewState["lista"];
            DataView dw = dt.AsDataView();
            dw.RowFilter = filtro;
            dt = dw.ToTable();
            //string query = "[dbo].[CARGA_TODO_SOLICITUDES_ANDENES] @SITE_ID=" + site_id;
            //if (playa_id != 0)
            //    query += ",@PLAYA_ID=" + playa_id;
            //if (anden_id != 0)
            //    query += ",@ANDEN_ID=" + anden_id;
            //if (!String.IsNullOrEmpty(id_soli))
            //    query += ",@SOLI_ID=" + int.Parse(id_soli);

            //if (!String.IsNullOrEmpty(estado_soli))
            //    query += ",@SOLI_estado=" + int.Parse(estado_soli);

            //if (!String.IsNullOrEmpty(transportista))
            //    query += ",@transportista=" + transportista + "";

            //DataTable dt = sol.ObtenerSolicitudesCarga(int.Parse(ddl_buscarSite.SelectedValue), int.Parse(ddl_buscarPlaya.SelectedValue), int.Parse(ddl_buscarAnden.SelectedValue), txt_buscarNumero.Text, ddl_buscarEstado.SelectedValue, ddl_buscarTransportista.SelectedValue);
            ViewState["filtrados"] = dt;
            ObtenerSolicitudes(false);
        }
    }

    protected void btn_reanudar_Click(object sender, EventArgs e)
    {
        if (hf_ordenAndenesOk.Value.ToLower() == "true")
        {
            SolicitudAndenesBC sa = new SolicitudAndenesBC();
            SolicitudBC s = new SolicitudBC();
            bool exito = false;
            s.SOLI_ID = int.Parse(hf_idSolicitud.Value);
            s.TIMESTAMP = DateTime.Parse(hf_timeStamp.Value);
            if (!s.validarTimeStamp())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Timestamp incorrecto.');", true);
                exito = false;
            }
            else
            {
                sa.SOLI_ID = s.SOLI_ID;
                bool registros = false;
                DataView dw = new DataView((DataTable)ViewState["datosA"]);
                dw.RowFilter = "SOES_ID = -1";
                DataTable dt = dw.ToTable(true, "SOAN_ORDEN", "ID_ANDEN");
                DataTable dt2 = dw.ToTable();
                exito = true;

                DataView dw2 = new DataView((DataTable)ViewState["datosA"]);
                dw2.RowFilter = "SOES_ID = -1 or SOES_ID = 100";
                DataTable dt4 = dw2.ToTable();
                registros = (dt4.Rows.Count > 0);
                //    exito = registros;

                foreach (DataRow row in dt.Rows)
                {
                    exito = true;
                    registros = true;
                    sa.MINS_CARGA_EST = 60; //Variable calculada automáticamente
                    sa.LUGA_ID = int.Parse(row["ID_ANDEN"].ToString());
                    sa.SOAN_ORDEN = int.Parse(row["SOAN_ORDEN"].ToString());
                    if (sa.AgregarAnden())
                    {
                        exito = true;
                    }
                    else
                    {
                        exito = false;
                        break;
                    }
                }
                if (exito)
                {
                    foreach (DataRow row in dt2.Rows)
                    {
                        SolicitudLocalesBC sl = new SolicitudLocalesBC();
                        sl.SOLI_ID = s.SOLI_ID;
                        sl.LUGA_ID = int.Parse(row["ID_ANDEN"].ToString());
                        sl.LOCA_ID = int.Parse(row["ID_LOCAL"].ToString());
                        sl.PALLETS = int.Parse(row["PALLETS"].ToString());
                        sl.SOAN_ORDEN = int.Parse(row["SOAN_ORDEN"].ToString());
                        sl.SOLD_ORDEN = int.Parse(row["ORDEN"].ToString());
                        if (sl.AgregarLocal(sl))
                            exito = true;
                        else
                        {
                            exito = false;
                            break;
                        }
                    }

                    if (!registros)
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Debe agregar al menos un andén de carga para continuar la carga.');", true);
                    else if (exito)
                    {
                        //  int nuevolugar;
                        //if (rb_nuevoSi.Checked)
                        //    nuevolugar = 0;
                        //else
                        //    nuevolugar = int.Parse(ddl_nuevoAnden.SelectedValue);
                        string resultado;
                        bool ejecucion = sa.ReanudarCarga(sa.SOLI_ID, usuario.ID, out resultado);
                        if (resultado == "" && ejecucion)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Se reanudó la carga correctamente.');", true);
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje2", "cerrarModal('modalReanudar');", true);
                        }
                        else
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + resultado + "');", true);
                        btn_buscarSolicitud_Click(null, null);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Error');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Error');", true);
            }
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error: Compruebe el órden de los andenes seleccionados.');", true);
    }

    protected void btn_terminarCarga_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC anden = new SolicitudAndenesBC();
        anden.SOLI_ID = int.Parse(hf_idSolicitud.Value);
        anden.LUGA_ID = int.Parse(hf_idLugar.Value);
        anden.SOAN_ORDEN = int.Parse(hf_orden.Value);
        switch (hf_idEstado.Value)
        {
            case "Cargado":
                anden.FECHA_CARGA_FIN = DateTime.Parse(txt_fechaCarga.Text + " " + txt_horaCarga.Text);
                string resultado;
                bool ejecucion = anden.CompletarCarga(anden, usuario.ID, out resultado);
                if (ejecucion && resultado == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('CARGA COMPLETA, DISPONIBLE PARA PONER SELLO');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalCarga');", true);
                    ObtenerSolicitudes(true);
                }
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('" + resultado + "');", true);
                break;
            case "Parcial":
                anden.FECHA_CARGA_FIN = DateTime.Parse(txt_fechaCarga.Text + " " + txt_horaCarga.Text);
                anden.PALLETS_CARGADOS = int.Parse(txt_palletsCargados.Text);
                string resultado1;
                bool ejecucion1 = anden.InterrumpirCarga(anden, usuario.ID, out resultado1);
                if (ejecucion1 && resultado1 == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('CARGA PARCIAL');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalCarga');", true);
                    ObtenerSolicitudes(true);
                }
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('" + resultado1 + "');", true);
                break;
            default:
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Se produjo un error');", true);
                break;
        }
        btn_buscarSolicitud_Click(null, null);
    }

    protected void validar_sello()
    {
        SolicitudAndenesBC anden = new SolicitudAndenesBC();
        anden.SOLI_ID = int.Parse(this.hf_idSolicitud.Value);
        string resultado;
        bool ejecucion = anden.SelloValidar(this.usuario.ID, out resultado);
        if (ejecucion && resultado == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('VALIDANDO SELLO');", true);
            this.ObtenerSolicitudes(true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", string.Format("alert('{0}');", resultado), true);
        }
    }

    protected void validado_sello()
    {
        SolicitudAndenesBC anden = new SolicitudAndenesBC();
        anden.SOLI_ID = int.Parse(this.hf_idSolicitud.Value);
        string resultado;
        bool ejecucion = anden.SelloValidado(this.usuario.ID, out resultado);
        if (ejecucion && resultado == "")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('CARRO CARGADO A ESTACIONAMIENTO');", true);
            this.ObtenerSolicitudes(true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", string.Format("alert('{0}');", resultado), true);
        }
    }

    #endregion

    #region DropDownList

    protected void ddl_buscarPlaya_SelectedIndexChanged(object sender, EventArgs e)
    {
        drops.Lugar_Todos(ddl_buscarAnden, 0, int.Parse(ddl_buscarPlaya.SelectedValue));
        if (ddl_buscarAnden.Items.Count > 1)
            ddl_buscarAnden.Enabled = true;
        else
            ddl_buscarAnden.Enabled = false;
    }

    protected void ddl_buscarSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        drops.Playa_Todos(ddl_buscarPlaya, 0, int.Parse(ddl_buscarSite.SelectedValue));
        drops.Lugar_Todos(ddl_buscarAnden, int.Parse(ddl_buscarSite.SelectedValue), int.Parse(ddl_buscarPlaya.SelectedValue));
        //PlayaBC p = new PlayaBC();
        //LugarBC l = new LugarBC();
        //utils.CargaDrop(ddl_buscarPlaya, "ID", "DESCRIPCION", p.ObtenerDrop(int.Parse(ddl_buscarSite.SelectedValue)));
        //utils.CargaDrop(ddl_buscarAnden, "ID", "DESCRIPCION", l.obtenerTodoLugar(int.Parse(ddl_buscarSite.SelectedValue), 0, int.Parse(ddl_buscarPlaya.SelectedValue)));
        ObtenerSolicitudes(true);
    }

    protected void GoPage(object sender, System.EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        if (int.TryParse((oIraPag.Text), out iNumPag) && iNumPag > 0 && iNumPag <= gv_listar.PageCount)
        {
            if (int.TryParse(oIraPag.Text, out iNumPag) && iNumPag > 0 && iNumPag <= gv_listar.PageCount)
            {
                gv_listar.PageIndex = iNumPag - 1;
            }
            else
            {
                gv_listar.PageIndex = 0;
            }
        }
        ObtenerSolicitudes(false);
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

    private void Ordenar(DataTable dt,bool reordenar)
    {
        if (reordenar)
        {
            int orden = 1;
            foreach (DataRow dr in dt.Rows)
            {
                dr["ORDEN"] = orden;
                orden++;
            }
        }
        DataView dw = dt.AsDataView();
        dw.Sort = "ORDEN ASC";
        dt = dw.ToTable();
        if (!OrdenarAnden(dt))
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alerta", "showAlert2('ADVERTENCIA: Órden de los andenes incorrecto.');", true);
        this.ViewState["datosA"] = dt;
    }

    private bool OrdenarAnden(DataTable dt)
    {
        int orden = 1;
        int index = 0;
        List<String> andenes = new List<String>();
        bool ordenOk = true;
        foreach (DataRow dr in dt.Rows)
        {
            if (int.Parse(dr["SOES_ID"].ToString()) == -1)
            {
                if (index > 0)
                {
                    orden = int.Parse(dt.Rows[index - 1]["SOAN_ORDEN"].ToString());
                    int estadob = int.Parse(dt.Rows[index - 1]["SOES_ID"].ToString());
                    if (dt.Rows[index - 1]["ID_ANDEN"].ToString() != dr["ID_ANDEN"].ToString() || estadob != -1)
                    {
                        if (andenes.Contains(dr["ID_ANDEN"].ToString()))
                            ordenOk = false;
                        andenes.Add(dr["ID_ANDEN"].ToString());
                        orden++;
                    }
                }
                else
                    andenes.Add(dr["ID_ANDEN"].ToString());
                dr["SOAN_ORDEN"] = orden;
            }
            index++;
        }
        hf_ordenAndenesOk.Value = ordenOk.ToString();
        return ordenOk;
    }

    private void ObtenerSolicitudes(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            SolicitudBC sol = new SolicitudBC();
            DataTable dt = sol.ObtenerSolicitudesCarga(int.Parse(ddl_buscarSite.SelectedValue));
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

    private void calcula_solicitud(object sender, EventArgs e)
    {
        this.stringLocalesSeleccionados((DataTable)Session["datosA"]);
        //   carga_playas();
        this.locales_Compatibles();
        //this.marcaCompatibles();
        this.marca_seleccion();

        //this.carga_trailers();
    }

    private void locales_Compatibles()
    {
        CaractCargaBC cc = new CaractCargaBC();
        DataTable dt = cc.obtenerCompatibles(this.hf_localesSeleccionados.Value, this.hf_caractSolicitud.Value);
        bool primero = true;
        string compatibles = "";
        foreach (DataRow dr in dt.Rows)
        {
            if (primero)
            {
                compatibles += dr[0].ToString();
                primero = false;
            }
            else
            {
                compatibles += string.Format(",{0}", dr[0].ToString());
            }
        }
        this.hf_localesCompatibles.Value = compatibles;
    }

    private void stringLocalesSeleccionados(DataTable dt)
    {
        try
        {
            this.hf_localesSeleccionados.Value = "";
            bool primero = true;
            string locales = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (primero)
                {
                    locales += dr["ID_LOCAL"].ToString();
                    primero = false;
                }
                else
                {
                    locales += string.Format(",{0}", dr["ID_LOCAL"].ToString());
                }
            }
            this.hf_localesSeleccionados.Value = locales;
        }
        catch (Exception)
        {
        }
    }

    private void marca_seleccion()
    {
        CaractCargaBC cc = new CaractCargaBC();
        DataTable dt = cc.caracteristicasdesdelocales(this.hf_localesSeleccionados.Value, this.hf_caractSolicitud.Value);

        //   this.chk_solFrio.Checked= Boolean.Parse( dt.Select("min(caca_orden) where caract_ID='20'")[0][0].ToString());

        int maxplancha = 6;
        int mincantidad = 32;
        int maxpallet = 0;

        foreach (DataRow dr in dt.Rows)
        {
            int orden = dr.Field<int>("ID");
            if (dr.Field<int>("caract_ID") == 10)
            {
                maxplancha = Math.Max(maxplancha, orden);
            }
            if (dr.Field<int>("caract_ID") == 0)
            {
                mincantidad = Math.Min(mincantidad, orden);
                maxpallet = maxpallet + dr.Field<int>("valor");
            }
            this.Session["MaxPallet"] = maxpallet;
        }
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