// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Reporte_Cargav2 : System.Web.UI.Page
{
    UsuarioBC usuario = new UsuarioBC();
    UtilsWeb utils = new UtilsWeb();
    CargaDrops drops = new CargaDrops();

    public void gv_listar_RowCreated(object sender, GridViewRowEventArgs e)
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
            this.utils.CargaDropNormal(this.ddl_buscarSite, "ID", "NOMBRE", y.ObteneSites(this.usuario.ID));
        }
    }

    #region GridView

    protected void gv_solLocales_rowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.RowIndex > 0)
            //{
            LinkButton btnsubir = (LinkButton)e.Row.FindControl("btnSubir");
            LinkButton btnbajar = (LinkButton)e.Row.FindControl("btnBajar");
            DataTable dt = (DataTable)this.Session["datosA"];
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
            if (cell >= 105)
            {
                LinkButton btn_eliminar = (LinkButton)e.Row.FindControl("btn_eliminarLocal");
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
                        cell = int.Parse(dt.Rows[index - 1]["SOES_ID"].ToString());
                        if (cell >= 105)
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
                        cell = int.Parse(dt.Rows[index + 1]["SOES_ID"].ToString());
                        if (cell >= 105)
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
            DataTable dt = (DataTable)this.Session["datosA"];
            for (int i = index + 1; i < dt.Rows.Count; i++)
            {
                int orden = int.Parse(dt.Rows[i]["ORDEN"].ToString());
                dt.Rows[i]["ORDEN"] = (orden - 1).ToString();
            }
            dt.Rows.RemoveAt(index);
            DataView dw = new DataView();
            dw = dt.DefaultView;
            dw.Sort = "ORDEN asc";
            dt = dw.ToTable();
            this.gv_solLocales.DataSource = dt;
            this.gv_solLocales.DataBind();
            this.ViewState["datosA"] = dt;
            this.stringLocalesSeleccionados((DataTable)this.Session["datosA"]);
            this.marca_seleccion();
            this.calcula_solicitud(null, null);
        }
        if (e.CommandName == "BAJAR")
        {
            Int32 index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)this.Session["datosA"];

            DataView dw = new DataView();
            dw = dt.DefaultView;
            int ordeno = int.Parse(dt.Rows[index]["ORDEN"].ToString());
            int ordend;
            foreach (DataRow rowdest in dt.Rows)
            {
                ordend = int.Parse(rowdest["ORDEN"].ToString());
                if (ordend == (ordeno + 1))
                {
                    rowdest["ORDEN"] = ordeno;
                    break;
                }
            }
            dt.Rows[index]["ORDEN"] = ordeno + 1;
            dw = dt.DefaultView;
            dw.Sort = "ORDEN asc";
            dt = dw.ToTable();
            this.gv_solLocales.DataSource = dt;
            this.gv_solLocales.DataBind();
            this.ViewState["datosA"] = dt;
            //this.calcula_solicitud(null, null);
        }
        if (e.CommandName == "SUBIR")
        {
            Int32 index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)this.Session["datosA"];

            DataView dw = new DataView();
            dw = dt.DefaultView;
            int ordeno = int.Parse(dt.Rows[index]["ORDEN"].ToString());
            int ordend;
            foreach (DataRow rowdest in dt.Rows)
            {
                ordend = int.Parse(rowdest["ORDEN"].ToString());
                if (ordend == (ordeno - 1))
                {
                    rowdest["ORDEN"] = ordeno;
                    break;
                }
            }
            dt.Rows[index]["ORDEN"] = ordeno - 1;
            dw = dt.DefaultView;
            dw.Sort = "ORDEN asc";
            dt = dw.ToTable();
            this.gv_solLocales.DataSource = dt;
            this.gv_solLocales.DataBind();
            this.ViewState["datosA"] = dt;
            //this.calcula_solicitud(null, null);
        }
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            this.gv_listar.PageIndex = e.NewPageIndex;
        }
        this.ObtenerSolicitudes(false);
    }

    protected void gv_listar_RowEdit(object sender, GridViewEditEventArgs e)
    {
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Page")
        {
            //      int index = int.Parse(e.CommandArgument.ToString());
            string[] arg = new string[3];
            arg = e.CommandArgument.ToString().Split(';');
            this.hf_idSolicitud.Value = arg[0];//  index.ToString();// gv_listar.DataKeys[index].Values[0].ToString();
            this.hf_idLugar.Value = arg[1];// gv_listar.DataKeys[index].Values[1].ToString();
            this.hf_orden.Value = arg[2];// gv_listar.DataKeys[index].Values[2].ToString();
            this.txt_fechaCarga.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txt_horaCarga.Text = DateTime.Now.ToShortTimeString();
            this.hf_idEstado.Value = e.CommandName;
            switch (e.CommandName)
            {
                case "Cargado":
                    this.dv_pallets.Visible = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalCarga();", true);
                    break;
                case "Parcial":
                    this.dv_pallets.Visible = true;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalCarga();", true);
                    break;
                case "Continuar":
                    SolicitudBC s = new SolicitudBC();
                    LugarBC l = new LugarBC();
                    s = s.ObtenerXId(int.Parse(this.hf_idSolicitud.Value));
                    this.drops.Lugar1(this.ddl_origenAnden, 0, s.PLAY_ID, 0, 1);
                    this.hf_caractSolicitud.Value = s.CARACTERISTICAS;
                    this.hf_localesSeleccionados.Value = s.LOCALES;
                    this.hf_timeStamp.Value = s.TIMESTAMP.ToString();
                    DataTable dsol = s.ObtenerAndenesXSolicitudId(s.SOLI_ID);
                    this.ViewState["datosA"] = dsol;
                    this.gv_solLocales.DataSource = dsol;
                    this.gv_solLocales.DataBind();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalReanudar();", true);
                    break;
                case "Edit":
                    string id = this.hf_idSolicitud.Value;
                    string url = string.Format("Solicitud_Carga.aspx?id={0}&type=edit", id);
                    this.Response.Redirect(url);

                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('" + hf_idSolicitud.Value + "');", true);
                    break;
                case "colocar_sello":
                    this.validar_sello();
                    break;
                case "validar_sello":
                    this.validado_sello();
                    break;
            }
        }
    }

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (this.gv_listar.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = this.gv_listar.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(this.gv_listar.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = Convert.ToString(this.gv_listar.PageIndex + 1);
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id_solicitud = int.Parse(this.gv_listar.DataKeys[e.Row.RowIndex].Values[0].ToString());
            if (e.Row.RowIndex > 0)
            {
                int anterior = int.Parse(this.gv_listar.DataKeys[e.Row.RowIndex - 1].Values[0].ToString());
                if (anterior != id_solicitud)
                {
                    e.Row.BackColor = this.cambiaColor(gv_listar.Rows[e.Row.RowIndex - 1].BackColor);
                }
                else
                {
                    e.Row.BackColor = this.gv_listar.Rows[e.Row.RowIndex - 1].BackColor;
                }
                foreach (GridViewRow row in this.gv_listar.Rows)
                {
                    if (row.Cells[0].Text == e.Row.Cells[0].Text)
                    {
                        e.Row.Cells[0].Text = "";
                        e.Row.Cells[2].Text = "";
                        e.Row.Cells[4].Text = "";
                        e.Row.Cells[6].Text = "";
                        break;
                    }
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

    #endregion

    #region Buttons

    protected void btn_cargafiltros_Click(object sender, EventArgs e)
    {
        SolicitudBC sol = new SolicitudBC();
        TransportistaBC t = new TransportistaBC();
     
        this.drops.Playa_Todos(this.ddl_buscarPlaya, 0, int.Parse(this.ddl_buscarSite.SelectedValue));
        //utils.CargaDrop(ddl_buscarPlaya, "ID", "DESCRIPCION", p.ObtenerDrop(int.Parse(ddl_buscarSite.SelectedValue)));
        this.drops.Lugar_Todos(this.ddl_buscarAnden, int.Parse(this.ddl_buscarPlaya.SelectedValue));
        //utils.CargaDrop(ddl_buscarAnden, "ID", "DESCRIPCION", l.obtenerTodoLugar(int.Parse(ddl_buscarSite.SelectedValue), 0, int.Parse(ddl_buscarPlaya.SelectedValue)));
        //utils.CargaDrop(ddl_buscarUsuario, "ID", "USERNAME", u.ObtenerTodos());
        this.utils.CargaDrop(this.ddl_buscarEstado, "ID", "DESCRIPCION", sol.ObtenerEstadosCarga());
        this.utils.CargaDrop(this.ddl_buscarTransportista, "ID", "NOMBRE", t.ObtenerTodos());
        //   ObtenerSolicitudes(true);
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
            DataView dw = new DataView();
            if (this.ViewState["datosA"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("ID_ANDEN");
                dt.Columns.Add("ID_LOCAL");
                dt.Columns.Add("ORDEN");
                dt.Columns.Add("NUMERO_LOCAL");
                dt.Columns.Add("NOMBRE_LOCAL");
                dt.Columns.Add("ANDEN");
                dt.Columns.Add("PALLETS");
                dt.Columns.Add("SOES_ID");
                this.ViewState["datosA"] = dt;
            }
            dt = this.ViewState["datosA"] as DataTable;
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

            int orden;
            if (dt.Rows.Count == 0)
            {
                orden = 1;
            }
            else
            {
                orden = int.Parse(dt.Rows[dt.Rows.Count - 1]["ORDEN"].ToString()) + 1;
            }
            dt.Rows.Add(this.ddl_origenAnden.SelectedValue, l.ID, orden, l.CODIGO, l.DESCRIPCION, this.ddl_origenAnden.SelectedItem.Text, 0, 100);
            this.ViewState["id_local"] = null;
            this.txt_descLocal.Text = "";
            this.txt_buscaLocal.Text = "";
            dw = dt.DefaultView;
            dw.Sort = "ORDEN asc";
            dt = dw.ToTable();
            this.gv_solLocales.DataSource = dt;
            this.gv_solLocales.DataBind();
            this.ViewState["datosA"] = dt;

            this.calcula_solicitud(null, null);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        DataView view = new DataView();
        view.Table = (DataTable)this.ViewState["lista"];
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

    protected void btn_buscarSolicitud_Click(object sender, EventArgs e)
    {
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
        if (string.IsNullOrEmpty(filtro))
        {
            this.ObtenerSolicitudes(true);
        }
        else
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
            //    query += ",@SOLI_ID=" + int.Parse(id_soli);

            //if (!String.IsNullOrEmpty(estado_soli))
            //    query += ",@SOLI_estado=" + int.Parse(estado_soli);

            //if (!String.IsNullOrEmpty(transportista))
            //    query += ",@transportista=" + transportista + "";

            //DataTable dt = sol.ObtenerSolicitudesCarga(int.Parse(ddl_buscarSite.SelectedValue), int.Parse(ddl_buscarPlaya.SelectedValue), int.Parse(ddl_buscarAnden.SelectedValue), txt_buscarNumero.Text, ddl_buscarEstado.SelectedValue, ddl_buscarTransportista.SelectedValue);
            this.ViewState["filtrados"] = dt;
            this.ObtenerSolicitudes(false);
        }
    }

    protected void btn_reanudar_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC sa = new SolicitudAndenesBC();
        SolicitudBC s = new SolicitudBC();
        bool exito = false;
        s.SOLI_ID = int.Parse(this.hf_idSolicitud.Value);
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
            DataTable dt2 = this.ViewState["datosA"] as DataTable;
            foreach (DataRow row in dt2.Rows)
            {
                exito = true;
                registros = true;
                if (row["SOES_ID"].ToString() == "100")
                {
                    registros = true;
                    sa.MINS_CARGA_EST = 60; //Variable calculada automáticamente
                    sa.LUGA_ID = int.Parse(row["ID_ANDEN"].ToString());
                    int orden;
                    if (sa.AgregarAnden(sa, out orden) && orden > 0)
                    {
                        DataRow[] dtLocales = dt2.Select(string.Format("ID_ANDEN = {0} and orden= {1}", sa.LUGA_ID, orden));
                        foreach (DataRow rowLocal in dtLocales)
                        {
                            SolicitudLocalesBC sl = new SolicitudLocalesBC();
                            sl.SOLI_ID = s.SOLI_ID;
                            sl.LUGA_ID = int.Parse(rowLocal["ID_ANDEN"].ToString());
                            sl.LOCA_ID = int.Parse(rowLocal["ID_LOCAL"].ToString());
                            sl.PALLETS = int.Parse(rowLocal["PALLETS"].ToString());
                            sl.SOAN_ORDEN = orden;
                            sl.SOLD_ORDEN = int.Parse(rowLocal["ORDEN"].ToString());
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
                    }
                    else
                    {
                        exito = false;
                        break;
                    }
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
                //    nuevolugar = int.Parse(ddl_nuevoAnden.SelectedValue);
                string resultado;
                bool ejecucion = sa.ReanudarCarga(sa.SOLI_ID, this.usuario.ID, out resultado);
                if (resultado == "" && ejecucion)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Se reanudó la carga correctamente.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", resultado), true);
                }
                this.btn_buscarSolicitud_Click(null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Error');", true);
            }
        }
    }

    protected void btn_terminarCarga_Click(object sender, EventArgs e)
    {
        SolicitudAndenesBC anden = new SolicitudAndenesBC();
        anden.SOLI_ID = int.Parse(this.hf_idSolicitud.Value);
        anden.LUGA_ID = int.Parse(this.hf_idLugar.Value);
        anden.SOAN_ORDEN = int.Parse(this.hf_orden.Value);
        switch (this.hf_idEstado.Value)
        {
            case "Cargado":
                anden.FECHA_CARGA_FIN = DateTime.Parse(string.Format("{0} {1}", this.txt_fechaCarga.Text, this.txt_horaCarga.Text));
                string resultado;
                bool ejecucion = anden.CompletarCarga(anden, this.usuario.ID, out resultado);
                if (ejecucion && resultado == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('CARGA COMPLETA, DISPONIBLE PARA PONER SELLO');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalCarga');", true);
                    this.ObtenerSolicitudes(true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", string.Format("alert('{0}');", resultado), true);
                }
                break;
            case "Parcial":
                anden.FECHA_CARGA_FIN = DateTime.Parse(string.Format("{0} {1}", this.txt_fechaCarga.Text, this.txt_horaCarga.Text));
                anden.PALLETS_CARGADOS = int.Parse(this.txt_palletsCargados.Text);
                string resultado1;
                bool ejecucion1 = anden.InterrumpirCarga(anden, this.usuario.ID, out resultado1);
                if (ejecucion1 && resultado1 == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "showAlert('CARGA PARCIAL');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalCarga');", true);
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
        this.btn_buscarSolicitud_Click(null, null);
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
        this.drops.Lugar_Todos(this.ddl_buscarAnden, 0, int.Parse(this.ddl_buscarPlaya.SelectedValue));
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
        this.drops.Playa_Todos(this.ddl_buscarPlaya, 0, int.Parse(this.ddl_buscarSite.SelectedValue));
        this.drops.Lugar_Todos(this.ddl_buscarAnden, int.Parse(this.ddl_buscarSite.SelectedValue), int.Parse(this.ddl_buscarPlaya.SelectedValue));
        //PlayaBC p = new PlayaBC();
        //LugarBC l = new LugarBC();
        //utils.CargaDrop(ddl_buscarPlaya, "ID", "DESCRIPCION", p.ObtenerDrop(int.Parse(ddl_buscarSite.SelectedValue)));
        //utils.CargaDrop(ddl_buscarAnden, "ID", "DESCRIPCION", l.obtenerTodoLugar(int.Parse(ddl_buscarSite.SelectedValue), 0, int.Parse(ddl_buscarPlaya.SelectedValue)));
        this.ObtenerSolicitudes(true);
    }

    #endregion

    private System.Drawing.Color cambiaColor(System.Drawing.Color color)
    {
        if (color == System.Drawing.ColorTranslator.FromHtml("#a9a9a9"))
        {
            color = System.Drawing.Color.White;
        }
        else
        {
            color = System.Drawing.ColorTranslator.FromHtml("#a9a9a9");
        }
        return color;
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

    private void ObtenerSolicitudes(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            SolicitudBC sol = new SolicitudBC();
            DataTable dt = sol.ObtenerSolicitudesCarga(int.Parse(this.ddl_buscarSite.SelectedValue));
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