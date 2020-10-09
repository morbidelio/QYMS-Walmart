// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;

public partial class App_Panel_Control : System.Web.UI.Page
{
    static FuncionesGenerales funcion = new FuncionesGenerales();
    static UtilsWeb utils = new UtilsWeb();
    CargaDrops drops = new CargaDrops();
    UsuarioBC usuario = new UsuarioBC();

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (this.Session["Usuario"] != null)
        {
            this.usuario = (UsuarioBC)this.Session["Usuario"];

            if (this.usuario.numero_sites < 2)
            {

                this.ddl_Site.Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.usuario = (UsuarioBC)this.Session["Usuario"];
        if (!this.IsPostBack)
        {
            //SiteBC site = new SiteBC();
            this.drops.Site(this.ddl_Site, this.usuario.ID);
            //utils.CargaDrop(this.ddl_Site, "ID", "NOMBRE", site.ObtenerTodos());
            ddl_Site_SelectedIndexChanged(null, null);
            this.hf_idLugar.Value = "";
            TrailerTipoBC tt = new TrailerTipoBC();
            this.crearLeyenda(tt.obtenerTodo(), this.pnl_leyendaTipoTrailer, "DESCRIPCION");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "graba", "graba_tab();", true);
            TrailerTipoBC tt = new TrailerTipoBC();
            this.crearLeyenda(tt.obtenerTodo(), this.pnl_leyendaTipoTrailer, "DESCRIPCION");
            if (this.ddl_Site.SelectedIndex > 0)
            {
                this.dv_leyendaEstado.Visible = true;

                SolicitudBC s = new SolicitudBC();
                DataTable dtl = s.ObtenerColorXSite(int.Parse(this.ddl_Site.SelectedValue));
                //   crearLeyenda(dtl, pnl_leyendaEstado, "SOLICITUD_ESTADO");
                //leyendaEstadoSolicitud(int.Parse(ddl_Site.SelectedValue));
            }
            else
            {
                this.dv_leyendaEstado.Visible = false;
            }
        }
    }

    #region Buttons

    public void btn_pendienteClick(object sender, EventArgs e)
    {
        LugarBC lugar = new LugarBC();
        int id_lugar = int.Parse(this.hf_idLugar.Value);
        DataTable dt = lugar.obtenerSolicitudesPendientesXLugar(id_lugar);
        this.gv_solicitudesPendientes.DataSource = dt;
        this.gv_solicitudesPendientes.DataBind();

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#modalPendientes').modal();", true);
    }

    #endregion

    #region DropDownList

    protected void ddl_Site_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ZonaBC zona = new ZonaBC();
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        if (this.ddl_Site.SelectedIndex != 0)
        {
            int site = int.Parse(this.ddl_Site.SelectedValue);
            DataTable dt = yms.ObtenerZonas(site, "", null);
            // this.ltl_menuZonas.Text = this.crearMenu(zona.ObtenerXSite(site)); //Variable de sesión, cambiar
            this.ltl_menuZonas.Text = this.crearMenu(dt); //Variable de sesión, cambiar
            //this.crearPlayasLugares2(zona.ObtenerXSite(site));
            this.crearPlayasLugares2(dt);
  
            //leyendaEstadoSolicitud(int.Parse(ddl_Site.SelectedValue));
            this.dv_leyendaEstado.Visible = true;
        }
        else
        {
            this.dv_leyendaEstado.Visible = false;
        }
        this.upplayas.Update();
    }

    #endregion

    #region UtilsPagina

    private void crearLeyenda(DataTable dt, Panel panel, string descripcion)
    {
        foreach (DataRow row in dt.Rows)
        {
            Panel pnl_leyendacolor = new Panel();
            Panel pnl_leyenda = new Panel();
            Panel pnl_color = new Panel();
            pnl_leyendacolor.Style.Add("width", "inherit");
            pnl_leyendacolor.Style.Add("float", "left");
            pnl_leyendacolor.Style.Add("padding-bottom", "2px");
            pnl_leyenda.Attributes.Add("style", "float:left; margin-right:2px; font-size:x-small");
            pnl_leyenda.Style.Add("float", "left");
            pnl_leyenda.Style.Add("margin-right", "2px");
            pnl_leyenda.Style.Add("font-size", "x-small");
            pnl_color.Style.Add("border", "1px solid");
            pnl_color.Style.Add("float", "left");
            pnl_color.Style.Add("margin-right", "2px;");
            pnl_color.Width = 17;
            pnl_color.Height = 17;
            try
            {
                pnl_color.BackColor = System.Drawing.ColorTranslator.FromHtml(row["COLOR"].ToString());
            }
            catch (Exception)
            {
                pnl_color.BackColor = System.Drawing.Color.Empty;
            }
            Label lbl_color = new Label();
            lbl_color.Text = row[descripcion].ToString();
            pnl_leyenda.Controls.Add(lbl_color);
            pnl_leyendacolor.Controls.Add(pnl_color);
            pnl_leyendacolor.Controls.Add(pnl_leyenda);
            panel.Controls.Add(pnl_leyendacolor);
        }
    }

    internal string crearMenu(DataTable dataTable)
    {
        string id, nombre;
        System.Text.StringBuilder strb = new System.Text.StringBuilder();
        strb.Append("<ul class='nav nav-pills'>");
        foreach (DataRow row in dataTable.Rows)
        {
            id = row["ID"].ToString();
            nombre = row["DESCRIPCION"].ToString();
            strb.AppendFormat("<li><a data-toggle='tab' id='zona_{0}' href='#cont_zona_{1}'>{2}</a></li>", id, id, nombre);
        }
        strb.Append("</ul>");
        return strb.ToString();
    }

    internal string crearPlayasLugares2(DataTable dt_zona)
    {
        SolicitudBC s = new SolicitudBC();
        DataTable dtl = s.ObtenerColorXSite(int.Parse(this.ddl_Site.SelectedValue));
        this.crearLeyenda(dtl, this.pnl_leyendaEstado, "SOLICITUD_ESTADO");

        PlayaBC playa = new PlayaBC();
        LugarBC lugar = new LugarBC();

        bool primero = false;
        int idzona, idplaya;
        Panel tab;
        foreach (DataRow row_zona in dt_zona.Rows)                          //Tabs Zonas
        {
            if (!primero)
            {
                tab = new Panel();
                tab.ClientIDMode = ClientIDMode.Static;
                tab.ID = string.Format("cont_zona_{0}", row_zona["ID"].ToString());
                tab.CssClass = "tab-pane fade active in";
                this.playaslugares.Controls.Add(tab);
                primero = true;
            }
            else
            {
                tab = new Panel();
                tab.ClientIDMode = ClientIDMode.Static;
                tab.ID = string.Format("cont_zona_{0}", row_zona["ID"].ToString());
                tab.CssClass = "tab-pane fade";
                this.playaslugares.Controls.Add(tab);
            }
            idzona = int.Parse(row_zona["ID"].ToString());

            DataTable dt_playas = playa.ObtenerXZona(idzona);         //Playas Agrupadas
            foreach (DataRow row_playa in dt_playas.Rows)
            {
                HtmlGenericControl h4 = new HtmlGenericControl("h4");
                h4.InnerText = row_playa["DESCRIPCION"].ToString();
                Panel contenido = new Panel();
                contenido.ClientIDMode = ClientIDMode.Static;
                contenido.ID = string.Format("cont_playa_{0}", row_playa["ID"].ToString());
                tab.Controls.Add(h4);
                h4.Style.Add("display", "inline-block");
                tab.Controls.Add(contenido);

                //Append("<table>");
                idplaya = int.Parse(row_playa["ID"].ToString());
                DataTable dt_lugares = lugar.obtenerLugarEstado(idplaya, 0, int.Parse(this.ddl_Site.SelectedValue));  
                int cont = 20;
                int cont2 = 1;
                Panel Row = new Panel();
                Row.CssClass = "panel-control";
                Row.Style.Add("display", "inline-block");
                contenido.Controls.Add(Row);
                Random random = new Random();
                foreach (DataRow row_lugar in dt_lugares.Rows)              //Andenes
                {
                    if (cont == 10)
                    {
                        Row = new Panel();
                        Row.CssClass = "panel-control";
                        Row.Style.Add("display", "inline-block");
                        contenido.Controls.Add(Row);                           //Nueva Row cada vez que empieza o cada 20 registros
                        cont = 0;
                    }

                    Panel Celda = new Panel();
                    Celda.ClientIDMode = ClientIDMode.Static;
                   
                     
                    Celda.ID = string.Format("cont_lugar_{0}", row_lugar["LUGA_ID"].ToString() + random.Next(1, 1000000).ToString() );
                    Celda.CssClass = "lugar";



                    Row.Controls.Add(Celda);

                    Panel DetallessuperiorCelda = new Panel();
                    Celda.Controls.Add(DetallessuperiorCelda);

                    Panel DetallesinferiorCelda = new Panel();
                    Celda.Controls.Add(DetallesinferiorCelda);

                    Panel PanelColorAlerta = new Panel();
                    Panel PanelIconoTrailer = new Panel();
                    PanelColorAlerta.CssClass = "col-xs-6";
                    PanelColorAlerta.Style.Add("padding", "0");
                    PanelIconoTrailer.CssClass = "col-xs-6";
                    PanelIconoTrailer.Style.Add("padding", "0");

                    //Image imagenreloj = new Image();
                    Image iconotrailer = new Image();
                    DetallessuperiorCelda.Style.Add("background-color", row_lugar["COLOR_LUGAR"].ToString());
                    DetallessuperiorCelda.Style.Add("color", "black");
                    if (row_lugar["LUES_ID"].ToString() != "10")            //Si está habilitado/disponible
                    {
                        DetallessuperiorCelda.CssClass = "row columna-anden detalle-lugar";
                        //if (row_lugar["MOVI_ORIGEN"].ToString() != "0" ||
                        //    row_lugar["MOVI_DEST"].ToString() != "0")
                        //{ //Si tiene movimiento pendiente
                        //    DetallessuperiorCelda.Style.Add("background-color", "yellow");
                        //}
                        //else if (row_lugar["LUGA_OCUPADO"].ToString() == "True")
                        //{
                        //    DetallessuperiorCelda.Style.Add("background-color", "lime");
                        //}

                        HyperLink lnk = new HyperLink();
                        // LinkButton lnk = new LinkButton();
                        lnk.NavigateUrl = "#";
                        // lnk.Click += detallesolicitudes;
                        // lnk.CommandArgument = row_lugar["LUGA_ID"].ToString();
                        lnk.Attributes.Add("onclick", string.Format("modalPendientes({0});", row_lugar["LUGA_ID"].ToString()));

                        lnk.Text = row_lugar["LUGA_COD"].ToString();
                        DetallessuperiorCelda.Controls.Add(lnk);
                        string clrAlerta = row_lugar["COLOR"].ToString();
                        if (clrAlerta != "#FFFFFF")
                        {
                            PanelColorAlerta.Style.Add("background-color", clrAlerta);
                            PanelColorAlerta.Style.Add("border-radius", "10px");
                            Image imagen = new Image();
                            PanelColorAlerta.Controls.Add(imagen);
                            imagen.Style.Add("position", "absolute");
                            imagen.Style.Add("top", "0px");
                            imagen.Style.Add("left", "0px");
                            imagen.ImageUrl = "../img/reloj.png";
                        }
                        DetallesinferiorCelda.CssClass = "row columna-anden detalle-trailer";
                        DetallesinferiorCelda.Style.Add("background-color", row_lugar["TRTI_COLOR"].ToString());
                        PanelColorAlerta.Width = 10;
                        PanelColorAlerta.Height = 10;

                        if (row_lugar["TRAI_ID"].ToString() == "-1")
                        {
                            Label tracto = new Label();
                            tracto.Text = "T";
                            PanelIconoTrailer.Controls.Add(tracto);
                        }
                        else
                        {
                            if (row_lugar["TRAI_PLACA"].ToString() != "0")
                            {
                                iconotrailer.Width = 17;
                                string mensaje = string.Format("Anden: {0}", row_lugar["LUGA_COD"].ToString());
                                mensaje += string.Format(" Placa: {0}", row_lugar["TRAI_PLACA"].ToString());
                                mensaje += string.Format(" Número Flota: {0}", row_lugar["TRAI_NUMERO"].ToString());
                                mensaje += string.Format(" Locales Solicitud: {0}", row_lugar["locales"].ToString());
                                PanelIconoTrailer.Attributes.Add("onmouseover", string.Format("mostrarDatos('{0}', {1});", mensaje, row_playa["ID"].ToString()));
                                PanelIconoTrailer.Attributes.Add("onmouseout", string.Format("mostrarDatos('', {0});", row_playa["ID"].ToString()));
                                PanelIconoTrailer.Style.Add("padding-left", "2px");
                                if (row_lugar["tres_icono"].ToString() != "")
                                {
                                    iconotrailer.ImageUrl = row_lugar["tres_icono"].ToString();
                                }
                            }
                            PanelIconoTrailer.Controls.Add(iconotrailer);
                        }
                    }
                    else
                    {
                        Celda.CssClass = string.Format("{0} inhabilitado", Celda.CssClass);
                        Label codigo = new Label();
                        codigo.Text = row_lugar["LUGA_COD"].ToString();
                        DetallessuperiorCelda.Controls.Add(codigo);
                    }
                    DetallesinferiorCelda.Controls.Add(PanelColorAlerta);
                    DetallesinferiorCelda.Controls.Add(PanelIconoTrailer);

                    cont++;
                    cont2++;
                }
                Panel div_mensaje = new Panel();
                
                contenido.Controls.Add(div_mensaje);
                div_mensaje.ClientIDMode = ClientIDMode.Static;

                div_mensaje.ID = string.Format("msj_play_{0}", row_playa["ID"].ToString());
                //  div_mensaje.CssClass = "col-xs-12";
                div_mensaje.Style.Add("display", "block");
                div_mensaje.Style.Add("clear", "both");
            }
        }

        //   return strb.ToString();
        return "";
    }

    protected void detallesolicitudes(object sender, EventArgs e)
    {
    }

    protected void recargaplayas(object sender, EventArgs e)
    {
        if (this.ddl_Site.SelectedIndex != 0)
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            int site = int.Parse(this.ddl_Site.SelectedValue);
            DataTable dt = yms.ObtenerZonas(site, "", null);
            this.ltl_menuZonas.Text = this.crearMenu(dt); //Variable de sesión, cambiar

            dt.Clear();
            dt = yms.ObtenerZonas(site, "", null);
            this.crearPlayasLugares2(dt);
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "aciva", "reactivatab();", true);
        this.upplayas.Update();
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