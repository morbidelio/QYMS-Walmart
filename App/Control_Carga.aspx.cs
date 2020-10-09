// Example header text. Can be configured in the options.
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Control_Carga : System.Web.UI.Page
{
    UsuarioBC usuario = new UsuarioBC();
    UtilsWeb utils = new UtilsWeb();
    CargaDrops drops = new CargaDrops();

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (this.Session["Usuario"] != null)
        {
            UsuarioBC usuario = new UsuarioBC();
            usuario = (UsuarioBC)this.Session["Usuario"];

            if (usuario.numero_sites < 2)
            {
                this.SITE.Visible = false;
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["usuario"] == null)
        {
            this.Response.Redirect("../InicioQYMS.aspx");
        }
        this.usuario = (UsuarioBC)this.Session["Usuario"];
        if (!this.IsPostBack)
        {
            YMS_ZONA_BC y = new YMS_ZONA_BC();
            SolicitudBC sol = new SolicitudBC();
            TransportistaBC t = new TransportistaBC();
            this.utils.CargaDropNormal(this.ddl_buscarSite, "ID", "NOMBRE", y.ObteneSites(this.usuario.ID));
            this.drops.Playa_Todos(this.ddl_buscarPlaya, 0, Convert.ToInt32(this.ddl_buscarSite.SelectedValue));
            this.drops.Lugar_Todos(this.ddl_buscarAnden, Convert.ToInt32(this.ddl_buscarSite.SelectedValue), Convert.ToInt32(this.ddl_buscarPlaya.SelectedValue));
            this.utils.CargaDrop(this.ddl_buscarEstado, "ID", "DESCRIPCION", sol.ObtenerEstadosCarga());
            this.utils.CargaDrop(this.ddl_buscarTransportista, "ID", "NOMBRE", t.ObtenerTodos());
        }
        this.ObtenerSolicitudes(false);
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
    protected void txt_buscaLocal2_TextChanged(object sender, EventArgs e)
    {
        LocalBC l = new LocalBC();
        l = l.obtenerXCodigo(this.local2.Text);

        if (l.ID != 0)
        {
            this.ViewState["id_local"] = l.ID;
            this.txt_local2.Text = string.Format("{0}({1})", l.CODIGO2, l.VALOR_CARACT_MAXIMO);
        }
        else
        {
            this.ViewState["id_local"] = null;
            this.txt_local2.Text = "Local no encontrado";
        }
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
    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Sort")
        {
            string[] arg = new string[3];
            arg = e.CommandArgument.ToString().Split(';');
            hf_idSolicitud.Value = arg[0];//  index.ToString();// gv_listar.DataKeys[index].Values[0].ToString();
            hf_idLugar.Value = arg[1];// gv_listar.DataKeys[index].Values[1].ToString();
            hf_orden.Value = arg[2];// gv_listar.DataKeys[index].Values[2].ToString();
            txt_fechaCarga.Text = DateTime.Now.ToShortDateString();
            txt_horaCarga.Text = DateTime.Now.ToShortTimeString();
            hf_idEstado.Value = e.CommandName;
            SolicitudBC s = new SolicitudBC();
            DataTable dsol;
            switch (e.CommandName)
            {
                case "Cargado":
                    this.dv_pallets.Visible = false;
                    utils.AbrirModal(this, "modalCarga");
                    break;

                case "locales":
                    this.dv_pallets.Visible = false;
                     s = s.ObtenerXId(Convert.ToInt32(this.hf_idSolicitud.Value));
                    DataView View=s.ObtenerAndenesXSolicitudId(s.SOLI_ID).AsDataView();
                    utils.CargaDropNormal(this.ddl_anden_local2, "id_anden", "anden", View.ToTable(true, "id_anden", "anden"));
                    hf_caractSolicitud.Value = s.CARACTERISTICAS;
                    hf_localesSeleccionados.Value = s.LOCALES;
                    hf_timeStamp.Value = s.TIMESTAMP.ToString();
                    dsol = s.ObtenerAndenesXSolicitudId(s.SOLI_ID);
                    Ordenar2(dsol, true);
                    gv_locales2.DataSource = this.ViewState["datosA2"];
                    gv_locales2.DataBind();
                    utils.AbrirModal(this, "modalLocales");

                    break;
                case "Parcial":
                    this.dv_pallets.Visible = true;
                    utils.AbrirModal(this, "modalCarga");
                    break;
                case "AndenEmergencia":
                    s = s.ObtenerXId(Convert.ToInt32(this.hf_idSolicitud.Value));
                    drops.Lugar1(this.ddl_origenAnden, 0, s.PLAY_ID, 0, 1);
                    hf_caractSolicitud.Value = s.CARACTERISTICAS;
                    hf_localesSeleccionados.Value = s.LOCALES;
                    hf_timeStamp.Value = s.TIMESTAMP.ToString();
                    dsol = s.ObtenerAndenesXSolicitudId(s.SOLI_ID);
                    Ordenar(dsol, true);
                    gv_solLocales.DataSource = this.ViewState["datosA"];
                    gv_solLocales.DataBind();
                    btn_reanudar.Visible = false;
                    btn_emergencia.Visible = true;
                    utils.AbrirModal(this, "modalReanudar");
                    break;
                case "Continuar":
                    this.btn_reanudar.Visible = true;
                    this.btn_emergencia.Visible = false;
                    s = s.ObtenerXId(Convert.ToInt32(this.hf_idSolicitud.Value));
                    this.drops.Lugar1(this.ddl_origenAnden, 0, s.PLAY_ID, 0, 1);
                    this.hf_caractSolicitud.Value = s.CARACTERISTICAS;
                    this.hf_localesSeleccionados.Value = s.LOCALES;
                    this.hf_timeStamp.Value = s.TIMESTAMP.ToString();
                    dsol = s.ObtenerAndenesXSolicitudId(s.SOLI_ID);
                    this.Ordenar(dsol, true);
                    this.gv_solLocales.DataSource = this.ViewState["datosA"];
                    this.gv_solLocales.DataBind();
                    utils.AbrirModal(this, "modalReanudar");
                    break;
                case "Edit":
                    string id = this.hf_idSolicitud.Value;
                    string url = string.Format("Solicitud_Carga.aspx?id={0}&type=edit", id);
                    this.Response.Redirect(url);
                    break;
                case "colocar_sello":
                    this.validar_sello();
                    break;
                case "validar_sello":
                    this.validado_sello();
                    break;
                case "CANCELAR":
                    this.lbl_confirmarTitulo.Text = "Eliminar Solicitud";
                    this.lbl_confirmarMensaje.Text = "Se eliminará la solicitud de la base de datos ¿Desea continuar?";
                    this.btn_eliminarLocal.Visible = false;
                    this.btn_eliminarSolicitud.Visible = true;
                    utils.AbrirModal(this, "modalConf");
                    break;
            }
        }
    }
    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id_solicitud = Convert.ToInt32(this.gv_listar.DataKeys[e.Row.RowIndex].Values[0].ToString());
            if (e.Row.RowIndex > 0)
            {
                int anterior = Convert.ToInt32(this.gv_listar.DataKeys[e.Row.RowIndex - 1].Values[0].ToString());
                if (anterior != id_solicitud)
                {
                    if (this.gv_listar.Rows[e.Row.RowIndex - 1].CssClass == "")
                    {
                        e.Row.CssClass = "row-color2";
                    }
                }
                else
                {
                    e.Row.CssClass = this.gv_listar.Rows[e.Row.RowIndex - 1].CssClass;
                }
            }
            bool estado = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "SOLI_REASIGNACION_TRAILER"));
            if (estado)
            {
                foreach (TableCell tc in e.Row.Cells)
                {
                    if (!string.IsNullOrEmpty(tc.CssClass))
                    {
                        tc.CssClass += " ";
                    }
                    tc.CssClass += "row-color3";
                }
            }
            LinkButton btnCompletar = (LinkButton)e.Row.FindControl("btn_cargado");
            LinkButton btnParcial = (LinkButton)e.Row.FindControl("btn_cargaParcial");
            LinkButton btnContinuar = (LinkButton)e.Row.FindControl("btn_cargaContinuar");
            LinkButton btnEditar = (LinkButton)e.Row.FindControl("btn_editar");
            LinkButton btneliminar = (LinkButton)e.Row.FindControl("btn_solicitudCancelar");

            LinkButton btncolocarsello = (LinkButton)e.Row.FindControl("btn_sello");
            LinkButton btnvalidasello = (LinkButton)e.Row.FindControl("btn_valida_sello");
            LinkButton btnemergencia = (LinkButton)e.Row.FindControl("btn_AndenEmergencia");
            btnCompletar.Visible = true;
            btnParcial.Visible = true;
            btnContinuar.Visible = false;
            btneliminar.Visible = false;
            btnemergencia.Visible = false;
            switch (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ID_ESTADOSOLICITUD")))
            {
                case (int)SolicitudBC.estado_solicitud.SolicitudAndenesCreada: //Solicitud Creada
                    btncolocarsello.Visible = false;
                    btnvalidasello.Visible = false;
                    btnParcial.Visible = false;
                    btnCompletar.Visible = false;
                    btneliminar.Visible = true;
                    break;

                case 101: //Solicitud Creada
                    btncolocarsello.Visible = false;
                    btnvalidasello.Visible = false;
                    btnParcial.Visible = false;
                    btnCompletar.Visible = false;
                    btneliminar.Visible = true;
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
                        btnemergencia.Visible = true;
                        btneliminar.Visible = true;
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
            //e.Row.CssClass = "header-color2";
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
    }
    protected void gv_Locales2_rowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnsubir = (LinkButton)e.Row.FindControl("btnSubir");
            LinkButton btnbajar = (LinkButton)e.Row.FindControl("btnBajar");
            DataTable dt = (DataTable)this.ViewState["datosA2"];
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

            int cell = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SOES_ID").ToString());
            if (cell != -1)
            {
                LinkButton btn_eliminar = (LinkButton)e.Row.FindControl("btn_eliminarLocal");
                DataView dw = dt.AsDataView();
                dw.RowFilter = "SOES_ID = -1";
                if (dw.ToTable().Rows.Count == 0 && cell == 100)
                {
                    btn_eliminar.Visible = true;
                }
                else
                {
                    btn_eliminar.Visible = false;
                }
                btnsubir.Visible = false;
                btnbajar.Visible = false;
            }
            else
            {
                if (index == 0)
                {
                    btnsubir.Visible = false;
                    if (registros == 0)
                    {
                        btnbajar.Visible = false;
                    }
                }
                if (index == registros - 1)
                {
                    btnbajar.Visible = false;
                }
                if (registros > 1 && index > 0)
                {
                    try
                    {
                        cell = Convert.ToInt32(dt.Rows[index - 1]["SOES_ID"].ToString());
                        if (cell != -1)
                        {
                            btnsubir.Visible = false;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        btnsubir.Visible = false;
                    }
                    try
                    {
                        cell = Convert.ToInt32(dt.Rows[index + 1]["SOES_ID"].ToString());
                        if (cell != -1)
                        {
                            btnbajar.Visible = false;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        btnbajar.Visible = false;
                    }
                }
            }
        }
    }
    protected void gv_solLocales_rowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnsubir = (LinkButton)e.Row.FindControl("btnSubir");
            LinkButton btnbajar = (LinkButton)e.Row.FindControl("btnBajar");
            DataTable dt = (DataTable)this.ViewState["datosA"];
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

            int cell = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SOES_ID").ToString());
            if (cell != -1)
            {
                LinkButton btn_eliminar = (LinkButton)e.Row.FindControl("btn_eliminarLocal");
                DataView dw = dt.AsDataView();
                dw.RowFilter = "SOES_ID = -1";
                if (dw.ToTable().Rows.Count == 0 && cell == 100)
                {
                    btn_eliminar.Visible = true;
                }
                else 
                {
                    btn_eliminar.Visible = false;
                }
                btnsubir.Visible = false;
                btnbajar.Visible = false;
            }
            else
            {
                if (index == 0)
                {
                    btnsubir.Visible = false;
                    if (registros == 0)
                    {
                        btnbajar.Visible = false;
                    }
                }
                if (index == registros - 1)
                {
                    btnbajar.Visible = false;
                }
                if (registros > 1 && index > 0)
                {
                    try
                    {
                        cell = Convert.ToInt32(dt.Rows[index - 1]["SOES_ID"].ToString());
                        if (cell != -1)
                        {
                            btnsubir.Visible = false;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        btnsubir.Visible = false;
                    }
                    try
                    {
                        cell = Convert.ToInt32(dt.Rows[index + 1]["SOES_ID"].ToString());
                        if (cell != -1)
                        {
                            btnbajar.Visible = false;
                        }
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
            DataTable dt = (DataTable)this.ViewState["datosA"];
            if (dt.Rows[index]["SOES_ID"].ToString() == "-1")
            {
                dt.Rows.RemoveAt(index);
                this.Ordenar(dt, true);
                this.gv_solLocales.DataSource = this.ViewState["datosA"];
                this.gv_solLocales.DataBind();
                this.stringLocalesSeleccionados((DataTable)this.ViewState["datosA"]);
                this.marca_seleccion();
                this.calcula_solicitud(null, null);
            }
            else
            {
                this.hf_idSolicitud.Value = dt.Rows[index]["SOLI_ID"].ToString();
                this.hf_idLocal.Value = dt.Rows[index]["ID_LOCAL"].ToString();
                this.hf_orden.Value = dt.Rows[index]["SOAN_ORDEN"].ToString();
                this.hf_idLugar.Value = dt.Rows[index]["LUGA_ID"].ToString();
                this.lbl_confirmarTitulo.Text = "Eliminar Local";
                this.lbl_confirmarMensaje.Text = "Se eliminará el local de la base de datos ¿Desea continuar?";
                this.btn_eliminarLocal.Visible = true;
                this.btn_eliminarSolicitud.Visible = false;
        utils.AbrirModal(this, "modalConf");
            }
        }
        if (e.CommandName == "BAJAR")
        {
            Int32 index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)this.ViewState["datosA"];

            int sold_a = Convert.ToInt32(dt.Rows[index]["ORDEN"].ToString());
            int sold_b = Convert.ToInt32(dt.Rows[index + 1]["ORDEN"].ToString());
            dt.Rows[index]["ORDEN"] = sold_b;
            dt.Rows[index + 1]["ORDEN"] = sold_a;
            this.Ordenar(dt, false);
            this.gv_solLocales.DataSource = this.ViewState["datosA"];
            this.gv_solLocales.DataBind();
        }
        if (e.CommandName == "SUBIR")
        {
            Int32 index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)this.ViewState["datosA"];

            int sold_a = Convert.ToInt32(dt.Rows[index]["ORDEN"].ToString());
            int sold_b = Convert.ToInt32(dt.Rows[index - 1]["ORDEN"].ToString());
            dt.Rows[index]["ORDEN"] = sold_b;
            dt.Rows[index - 1]["ORDEN"] = sold_a;
            this.Ordenar(dt, false);
            this.gv_solLocales.DataSource = this.ViewState["datosA"];
            this.gv_solLocales.DataBind();
        }
    }
    protected void gv_Locales2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ELIMINAR")
        {
            Int32 index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)this.ViewState["datosA2"];
            if (dt.Rows[index]["SOES_ID"].ToString() == "-1")
            {
                dt.Rows.RemoveAt(index);
                this.Ordenar2(dt, true);
                this.gv_locales2.DataSource = this.ViewState["datosA2"];
                this.gv_locales2.DataBind();
                this.stringLocalesSeleccionados((DataTable)this.ViewState["datosA2"]);
                this.marca_seleccion();
                this.calcula_solicitud(null, null);
            }
            else
            {
                this.hf_idSolicitud.Value = dt.Rows[index]["SOLI_ID"].ToString();
                this.hf_idLocal.Value = dt.Rows[index]["ID_LOCAL"].ToString();
                this.hf_orden.Value = dt.Rows[index]["SOAN_ORDEN"].ToString();
                this.hf_idLugar.Value = dt.Rows[index]["LUGA_ID"].ToString();
                this.lbl_confirmarTitulo.Text = "Eliminar Local";
                this.lbl_confirmarMensaje.Text = "Se eliminará el local de la base de datos ¿Desea continuar?";
                this.btn_eliminarLocal.Visible = true;
                this.btn_eliminarSolicitud.Visible = false;
                utils.AbrirModal(this, "modalConf");
            }
        }
        if (e.CommandName == "BAJAR")
        {
            Int32 index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)this.ViewState["datosA2"];

            int sold_a = Convert.ToInt32(dt.Rows[index]["ORDEN"].ToString());
            int sold_b = Convert.ToInt32(dt.Rows[index + 1]["ORDEN"].ToString());
            dt.Rows[index]["ORDEN"] = sold_b;
            dt.Rows[index + 1]["ORDEN"] = sold_a;
            this.Ordenar(dt, false);
            this.gv_locales2.DataSource = this.ViewState["datosA2"];
            this.gv_locales2.DataBind();
        }
        if (e.CommandName == "SUBIR")
        {
            Int32 index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)this.ViewState["datosA2"];

            int sold_a = Convert.ToInt32(dt.Rows[index]["ORDEN"].ToString());
            int sold_b = Convert.ToInt32(dt.Rows[index - 1]["ORDEN"].ToString());
            dt.Rows[index]["ORDEN"] = sold_b;
            dt.Rows[index - 1]["ORDEN"] = sold_a;
            this.Ordenar(dt, false);
            this.gv_locales2.DataSource = this.ViewState["datosA2"];
            this.gv_locales2.DataBind();
        }
    }
    #endregion

    #region Buttons
    protected void btn_eliminarSolicitud_Click(object sender, EventArgs e)
    {
        SolicitudBC s = new SolicitudBC();
        s.SOLI_ID = Convert.ToInt32(this.hf_idSolicitud.Value);
        string error = "";
        if (s.Eliminar(this.usuario.ID, out error) && error == "")
        {
            utils.ShowMessage2(this, "eliminarSolicitud", "success");
            utils.CerrarModal(this, "modalConf");
        }
        else
        {
            utils.ShowMessage2(this, "eliminarSolicitud", "error");
        }
        this.ObtenerSolicitudes(true);
    }
    protected void btn_eliminarLocal_Click(object sender, EventArgs e)
    {
        SolicitudLocalesBC s = new SolicitudLocalesBC();
        s.SOLI_ID = Convert.ToInt32(this.hf_idSolicitud.Value);
        s.LOCA_ID = Convert.ToInt32(this.hf_idLocal.Value);
        s.SOAN_ORDEN = Convert.ToInt32(this.hf_orden.Value);
        s.LUGA_ID = Convert.ToInt32(this.hf_idLugar.Value);
        if (s.Eliminar())
        {
            SolicitudBC sol = new SolicitudBC();
            this.Ordenar(sol.ObtenerAndenesXSolicitudId(s.SOLI_ID), true);
            this.gv_solLocales.DataSource = this.ViewState["datosA"];
            this.gv_solLocales.DataBind();
            utils.ShowMessage2(this, "eliminarLocal", "success");
            utils.CerrarModal(this, "modalConf");
        }
        else
        {
            utils.ShowMessage2(this, "eliminarLocal", "error");
        }
    }
    protected void btn_agregarCarga_Click(object sender, EventArgs e)
    {
        if (this.ViewState["id_local"] == null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe seleccionar local.');", true);
        }
        else if (this.ddl_origenAnden.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe ingresar un andén de origen.');", true);
        }
        else
        {
            DataTable dt;
            if (this.ViewState["datosA"] == null)
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
                this.ViewState["datosA"] = dt;
            }
            dt = (DataTable)ViewState["datosA"];
            LocalBC l = new LocalBC(); 
            l.ID = (int)this.ViewState["id_local"];
            l = l.obtenerXID();
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

            dt.Rows.Add(ddl_origenAnden.SelectedValue, hf_idSolicitud.Value, l.ID, null, l.CODIGO, l.DESCRIPCION, ddl_origenAnden.SelectedValue, ddl_origenAnden.SelectedItem.Text, 0, -1, null);
            Ordenar(dt, true);
            gv_solLocales.DataSource = this.ViewState["datosA"];
            gv_solLocales.DataBind();

            calcula_solicitud(null, null);
            ViewState["id_local"] = null;
            txt_descLocal.Text = "";
            txt_buscaLocal.Text = "";
        }
    }
    protected void btn_agregarCarga2_Click(object sender, EventArgs e)
    {
        if (this.ViewState["id_local"] == null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe seleccionar local.');", true);
        }
        else if (this.ddl_anden_local2.SelectedValue=="0")
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe ingresar un andén de origen.');", true);
        }
        else
        {
            DataTable dt;
            if (this.ViewState["datosA2"] == null)
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
                this.ViewState["datosA2"] = dt;
            }
            dt = this.ViewState["datosA2"] as DataTable;
            LocalBC l = new LocalBC();
            l.ID = (int)this.ViewState["id_local"];
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

            dt.Rows.Add(this.ddl_anden_local2.SelectedValue, this.hf_idSolicitud.Value, l.ID, null, l.CODIGO, l.DESCRIPCION, this.ddl_anden_local2.SelectedValue, this.ddl_anden_local2.SelectedItem.Text, 0, -1, null);
            this.Ordenar2(dt, true);
            this.gv_locales2.DataSource = this.ViewState["datosA2"];
            this.gv_locales2.DataBind();

            this.calcula_solicitud(null, null);
            this.ViewState["id_local"] = null;
            this.txt_local2 .Text = "";
            this.local2.Text = "";
        }
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
        else
        {
            utils.ShowMessage2(this, "exportar", "warn_sinFilas");
        }
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        this.ObtenerSolicitudes(true);
        string filtro = "";
        bool primero = true;
        if (this.ddl_buscarPlaya.SelectedIndex > 0)
        {
            primero = false;
            filtro += string.Format("PLAY_ID = {0} ", this.ddl_buscarPlaya.SelectedValue);
        }
        if (this.ddl_buscarAnden.SelectedIndex > 0)
        {
            if (!primero)
            {
                filtro += "and ";
            }
            primero = false;
            filtro += string.Format("ID_LUGAR = {0} ", this.ddl_buscarAnden.SelectedValue);
        }
        if (!string.IsNullOrEmpty(this.txt_buscarNumero.Text))
        {
            if (!primero)
            {
                filtro += ", ";
            }
            primero = false;
            filtro += string.Format("ID_SOLICITUD = {0} ", this.txt_buscarNumero.Text);
        }
        if (this.ddl_buscarEstado.SelectedIndex > 0)
        {
            if (!primero)
            {
                filtro += "and ";
            }
            primero = false;
            filtro += string.Format("ID_ESTADOSOLICITUD = {0} ", this.ddl_buscarEstado.SelectedValue);
        }
        if (this.ddl_buscarTransportista.SelectedIndex > 0)
        {
            if (!primero)
            {
                filtro += "and ";
            }
            primero = false;
            filtro += string.Format("ID_TRANSPORTISTA = {0} ", this.ddl_buscarTransportista.SelectedValue);
        }
        if (!string.IsNullOrEmpty(this.txt_buscarRuta.Text))
        {
            if (!primero)
            {
                filtro += "and ";
            }
            primero = false;
            filtro += string.Format("SOLI_RUTA LIKE '%{0}%' ", this.txt_buscarRuta.Text);
        }
        if (!string.IsNullOrEmpty(filtro))
        {
            DataTable dt = (DataTable)this.ViewState["lista"];
            DataView dw = dt.AsDataView();
            dw.RowFilter = filtro;
            dt = dw.ToTable();
            //string query = "[dbo].[CARGA_TODO_SOLICITUDES_ANDENES] @SITE_ID=" + site_id;
            //if (playa_id != 0)
            //    query += ",@PLAYA_ID=" + playa_id;
            //if (anden_id != 0)
            //    query += ",@ANDEN_ID=" + anden_id;
            //if (!String.IsNullOrEmpty(id_soli))
            //    query += ",@SOLI_ID=" + Convert.ToInt32(id_soli);

            //if (!String.IsNullOrEmpty(estado_soli))
            //    query += ",@SOLI_estado=" + Convert.ToInt32(estado_soli);

            //if (!String.IsNullOrEmpty(transportista))
            //    query += ",@transportista=" + transportista + "";

            //DataTable dt = sol.ObtenerSolicitudesCarga(Convert.ToInt32(ddl_buscarSite.SelectedValue), Convert.ToInt32(ddl_buscarPlaya.SelectedValue), Convert.ToInt32(ddl_buscarAnden.SelectedValue), txt_buscarNumero.Text, ddl_buscarEstado.SelectedValue, ddl_buscarTransportista.SelectedValue);
            this.ViewState["filtrados"] = dt;
            this.ObtenerSolicitudes(false);
        }
    }
    protected void btn_local_Click(object sender, EventArgs e)
    {
        if (this.hf_ordenAndenesOk.Value.ToLower() == "true")
        {
            SolicitudAndenesBC sa = new SolicitudAndenesBC();
            SolicitudBC s = new SolicitudBC();
            bool exito = false;
            s.SOLI_ID = Convert.ToInt32(this.hf_idSolicitud.Value);
            s.TIMESTAMP = DateTime.Parse(this.hf_timeStamp.Value);
            if (!s.validarTimeStamp())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Timestamp incorrecto.');", true);
                exito = false;
            }
            else
            {
                sa.SOLI_ID = s.SOLI_ID;
                bool registros = false;
                DataView dw = new DataView((DataTable)this.ViewState["datosA2"]);
                dw.RowFilter = "SOES_ID = -1";
                DataTable dt = dw.ToTable(true, "SOAN_ORDEN", "ID_ANDEN");
                DataTable dt2 = dw.ToTable();
                exito = true;

                DataView dw2 = new DataView((DataTable)this.ViewState["datosA2"]);
                dw2.RowFilter = "SOES_ID = -1 or SOES_ID = 100";
                DataTable dt4 = dw2.ToTable();
                registros = (dt4.Rows.Count > 0);
                //    exito = registros;

         
                if (exito)
                {
                    foreach (DataRow row in dt2.Rows)
                    {
                        SolicitudLocalesBC sl = new SolicitudLocalesBC();
                        sl.SOLI_ID = s.SOLI_ID;
                        sl.LUGA_ID = Convert.ToInt32(row["ID_ANDEN"].ToString());
                        sl.LOCA_ID = Convert.ToInt32(row["ID_LOCAL"].ToString());
                        sl.PALLETS = Convert.ToInt32(row["PALLETS"].ToString());
                        sl.SOAN_ORDEN = Convert.ToInt32(row["SOAN_ORDEN"].ToString());
                        sl.SOLD_ORDEN = Convert.ToInt32(row["ORDEN"].ToString());
                        if (sl.AgregarLocal(sl))
                        {
                            exito = true;
                        }
                        else
                        {
                            exito = false;
                            break;
                        }
                    }

                    if (!registros)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Debe agregar al menos un andén de carga para continuar la carga.');", true);
                    }
                    else if (exito)
                    {
                        //  int nuevolugar;
                        //if (rb_nuevoSi.Checked)
                        //    nuevolugar = 0;
                        //else
                        //    nuevolugar = Convert.ToInt32(ddl_nuevoAnden.SelectedValue);
                        string resultado;
   
                        this.btn_buscar_Click(null, null);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Error');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error: Compruebe el órden de los andenes seleccionados.');", true);
        }
    }
    protected void btn_reanudar_Click(object sender, EventArgs e)
    {
        if (this.hf_ordenAndenesOk.Value.ToLower() == "true")
        {
            SolicitudAndenesBC sa = new SolicitudAndenesBC();
            SolicitudBC s = new SolicitudBC();
            bool exito = false;
            s.SOLI_ID = Convert.ToInt32(this.hf_idSolicitud.Value);
            s.TIMESTAMP = DateTime.Parse(this.hf_timeStamp.Value);
            if (!s.validarTimeStamp())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Timestamp incorrecto.');", true);
                exito = false;
            }
            else
            {
                sa.SOLI_ID = s.SOLI_ID;
                bool registros = false;
                DataView dw = new DataView((DataTable)this.ViewState["datosA"]);
                dw.RowFilter = "SOES_ID = -1";
                DataTable dt = dw.ToTable(true, "SOAN_ORDEN", "ID_ANDEN");
                DataTable dt2 = dw.ToTable();
                exito = true;

                DataView dw2 = new DataView((DataTable)this.ViewState["datosA"]);
                dw2.RowFilter = "SOES_ID = -1 or SOES_ID = 100";
                DataTable dt4 = dw2.ToTable();
                registros = (dt4.Rows.Count > 0);
                //    exito = registros;

                foreach (DataRow row in dt.Rows)
                {
                    exito = true;
                    registros = true;
                    sa.MINS_CARGA_EST = 60; //Variable calculada automáticamente
                    sa.LUGA_ID = Convert.ToInt32(row["ID_ANDEN"].ToString());
                    sa.SOAN_ORDEN = Convert.ToInt32(row["SOAN_ORDEN"].ToString());
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
                        sl.LUGA_ID = Convert.ToInt32(row["ID_ANDEN"].ToString());
                        sl.LOCA_ID = Convert.ToInt32(row["ID_LOCAL"].ToString());
                        sl.PALLETS = Convert.ToInt32(row["PALLETS"].ToString());
                        sl.SOAN_ORDEN = Convert.ToInt32(row["SOAN_ORDEN"].ToString());
                        sl.SOLD_ORDEN = Convert.ToInt32(row["ORDEN"].ToString());
                        if (sl.AgregarLocal(sl))
                        {
                            exito = true;
                        }
                        else
                        {
                            exito = false;
                            break;
                        }
                    }

                    if (!registros)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Debe agregar al menos un andén de carga para continuar la carga.');", true);
                    }
                    else if (exito)
                    {
                        string resultado;
                        bool ejecucion = sa.ReanudarCarga(sa.SOLI_ID, this.usuario.ID, out resultado);
                        if (resultado == "" && ejecucion)
                        {
                            utils.ShowMessage2(this, "reanudar", "success");
                            utils.CerrarModal(this, "modalReanudar");
                        }
                        else
                        {
                            utils.ShowMessage(this, resultado, "error", false);
                        }
                        ObtenerSolicitudes(true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Error');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error: Compruebe el órden de los andenes seleccionados.');", true);
        }
    }
    protected void btn_emergencia_Click(object sender, EventArgs e)
    {
        if (this.hf_ordenAndenesOk.Value.ToLower() == "true")
        {
            SolicitudAndenesBC sa = new SolicitudAndenesBC();
            SolicitudBC s = new SolicitudBC();
            bool exito = false;
            s.SOLI_ID = Convert.ToInt32(this.hf_idSolicitud.Value);
            s.TIMESTAMP = DateTime.Parse(this.hf_timeStamp.Value);
            if (!s.validarTimeStamp())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Timestamp incorrecto.');", true);
                exito = false;
            }
            else
            {
                sa.SOLI_ID = s.SOLI_ID;
                bool registros = false;
                DataView dw = new DataView((DataTable)this.ViewState["datosA"]);
                dw.RowFilter = "SOES_ID = -1";
                DataTable dt = dw.ToTable(true, "SOAN_ORDEN", "ID_ANDEN");
                DataTable dt2 = dw.ToTable();
                exito = true;

                DataView dw2 = new DataView((DataTable)this.ViewState["datosA"]);
                dw2.RowFilter = "SOES_ID = -1 or SOES_ID = 100";
                DataTable dt4 = dw2.ToTable();
                registros = (dt4.Rows.Count > 0);
                //    exito = registros;

                foreach (DataRow row in dt.Rows)
                {
                    exito = true;
                    registros = true;
                    sa.MINS_CARGA_EST = 60; //Variable calculada automáticamente
                    sa.LUGA_ID = Convert.ToInt32(row["ID_ANDEN"].ToString());
                    sa.SOAN_ORDEN = Convert.ToInt32(row["SOAN_ORDEN"].ToString());
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
                        sl.LUGA_ID = Convert.ToInt32(row["ID_ANDEN"].ToString());
                        sl.LOCA_ID = Convert.ToInt32(row["ID_LOCAL"].ToString());
                        sl.PALLETS = Convert.ToInt32(row["PALLETS"].ToString());
                        sl.SOAN_ORDEN = Convert.ToInt32(row["SOAN_ORDEN"].ToString());
                        sl.SOLD_ORDEN = Convert.ToInt32(row["ORDEN"].ToString());
                        if (sl.AgregarLocal(sl))
                        {
                            exito = true;
                        }
                        else
                        {
                            exito = false;
                            break;
                        }
                    }

                    if (!registros)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Debe agregar al menos un andén de carga para continuar la carga.');", true);
                    }
                    else if (exito)
                    {
                        string resultado;
                        SolicitudAndenesBC anden = new SolicitudAndenesBC();
                        anden.SOLI_ID = Convert.ToInt32(this.hf_idSolicitud.Value);
                        anden.LUGA_ID = Convert.ToInt32(this.hf_idLugar.Value);
                        anden.SOAN_ORDEN = Convert.ToInt32(this.hf_orden.Value);
                        anden.FECHA_CARGA_FIN = DateTime.Now;
                        bool ejecucion = anden.CancelarCarga(anden, this.usuario.ID, out resultado);

                        if (resultado == "" && ejecucion)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje8", "showAlert('Se reanudó la carga correctamente.');", true);
                            utils.CerrarModal(this, "modalReanudar");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", resultado), true);
                        }
                        this.btn_buscar_Click(null, null);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Error');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Error: Compruebe el órden de los andenes seleccionados.');", true);
        }
    }
    protected void btn_terminarCarga_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC anden = new SolicitudAndenesBC();
        anden.SOLI_ID = Convert.ToInt32(this.hf_idSolicitud.Value);
        anden.LUGA_ID = Convert.ToInt32(this.hf_idLugar.Value);
        anden.SOAN_ORDEN = Convert.ToInt32(this.hf_orden.Value);
        switch (this.hf_idEstado.Value)
        {
            case "Cargado":
       
                anden.FECHA_CARGA_FIN = DateTime.Parse(string.Format("{0} {1}", this.txt_fechaCarga.Text, this.txt_horaCarga.Text));
                string resultado;
                bool ejecucion = anden.CompletarCarga(anden, this.usuario.ID, out resultado);
                if (ejecucion && resultado == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje9", "showAlert('CARGA COMPLETA, DISPONIBLE PARA PONER SELLO');", true);
                            utils.CerrarModal(this, "modalCarga");
                    this.ObtenerSolicitudes(true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", string.Format("alert('{0}');", resultado), true);
                }
                break;
            case "Parcial":
                anden.FECHA_CARGA_FIN = DateTime.Parse(string.Format("{0} {1}", this.txt_fechaCarga.Text, this.txt_horaCarga.Text));
                anden.PALLETS_CARGADOS = Convert.ToInt32(this.txt_palletsCargados.Text);
                string resultado1;
                bool ejecucion1 = anden.InterrumpirCarga(anden, this.usuario.ID, out resultado1);
                if (ejecucion1 && resultado1 == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('CARGA PARCIAL');", true);
                            utils.CerrarModal(this, "modalCarga");
                    this.ObtenerSolicitudes(true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", string.Format("alert('{0}');", resultado1), true);
                }
                break;
            default:
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "alert('Se produjo un error');", true);
                break;
        }
        this.btn_buscar_Click(null, null);
    }
    protected void validar_sello()
    {
        SolicitudAndenesBC anden = new SolicitudAndenesBC();
        anden.SOLI_ID = Convert.ToInt32(this.hf_idSolicitud.Value);
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
        anden.SOLI_ID = Convert.ToInt32(this.hf_idSolicitud.Value);
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
        this.drops.Lugar_Todos(this.ddl_buscarAnden, 0, Convert.ToInt32(this.ddl_buscarPlaya.SelectedValue));
        if (this.ddl_buscarAnden.Items.Count > 1)
        {
            this.ddl_buscarAnden.Enabled = true;
        }
        else
        {
            this.ddl_buscarAnden.Enabled = false;
        }
    }
    protected void ddl_buscarSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.drops.Playa_Todos(this.ddl_buscarPlaya, 0, Convert.ToInt32(this.ddl_buscarSite.SelectedValue));
        this.drops.Lugar_Todos(this.ddl_buscarAnden, Convert.ToInt32(this.ddl_buscarSite.SelectedValue), Convert.ToInt32(this.ddl_buscarPlaya.SelectedValue));
        this.ObtenerSolicitudes(true);
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
    private void Ordenar(DataTable dt, bool reordenar)
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
        if (!this.OrdenarAnden(dt))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alerta", "showAlert2('ADVERTENCIA: Órden de los andenes incorrecto.');", true);
        }
        this.ViewState["datosA"] = dt;
    }
    private void Ordenar2(DataTable dt, bool reordenar)
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
        //if (!this.OrdenarAnden(dt))
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alerta", "showAlert2('ADVERTENCIA: Órden de los andenes incorrecto.');", true);
        //}
        this.ViewState["datosA2"] = dt;
    }
    private bool OrdenarAnden(DataTable dt)
    {
        int orden = 1;
        int index = 0;
        List<String> andenes = new List<String>();
        bool ordenOk = true;
        foreach (DataRow dr in dt.Rows)
        {
            if (Convert.ToInt32(dr["SOES_ID"].ToString()) == -1)
            {
                if (index > 0)
                {
                    orden = Convert.ToInt32(dt.Rows[index - 1]["SOAN_ORDEN"].ToString());
                    int estadob = Convert.ToInt32(dt.Rows[index - 1]["SOES_ID"].ToString());
                    if (dt.Rows[index - 1]["ID_ANDEN"].ToString() != dr["ID_ANDEN"].ToString() || estadob != -1)
                    {
                        if (andenes.Contains(dr["ID_ANDEN"].ToString()))
                        {
                            ordenOk = false;
                        }
                        andenes.Add(dr["ID_ANDEN"].ToString());
                        orden++;
                    }
                }
                else
                {
                    andenes.Add(dr["ID_ANDEN"].ToString());
                }
                dr["SOAN_ORDEN"] = orden;
            }
            index++;
        }
        this.hf_ordenAndenesOk.Value = ordenOk.ToString();
        return ordenOk;
    }
    private void ObtenerSolicitudes(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            SolicitudBC sol = new SolicitudBC();
            DataTable dt = sol.ObtenerSolicitudesCarga(Convert.ToInt32(this.ddl_buscarSite.SelectedValue));
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
    private void calcula_solicitud(object sender, EventArgs e)
    {
        this.stringLocalesSeleccionados((DataTable)this.Session["datosA"]);
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