// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class App_Panel_Control_cuadratura : System.Web.UI.Page
{
    static FuncionesGenerales funcion = new FuncionesGenerales();
    static UtilsWeb utils = new UtilsWeb();
    CargaDrops drops = new CargaDrops();
    UsuarioBC usuario = new UsuarioBC();

    public void ddl_estadoTrailer_SelectedIndexChanged(object sender, EventArgs e)
    {
       if (this.hf_idTrailer.Value!="") btn_guarda_cambios.Enabled = true;
       else btn_guarda_cambios.Enabled = false;
    }

    protected void txt_nroTrailer_TextChanged(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        t = t.obtenerXNro(this.txt_nroTrailer.Text);
        if (t.ID != 0)
        {
            if (t.SOLI_ID != null && t.SOLI_ID != 0)
            {
                this.limpiarTrailer();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert4('Trailer tiene solicitud pendiente.');", true);
            }
            else if (t.MOVI_ID != null && t.MOVI_ID != 0)
            {
                this.limpiarTrailer();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert4('Trailer tiene movimiento pendiente.');", true);
            }
            else
            {
                this.llenarDatos(t);
            }
        }
        else
        {
            this.limpiarTrailer();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert4('No existe ningún trailer con ese número de flota.');", true);
        }
    }

    protected void txt_placaTrailer_TextChanged(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        t = t.obtenerXPlaca(this.txt_placaTrailer.Text);
        if (t.ID != 0)
        {
            if (t.SOLI_ID != null && t.SOLI_ID != 0)
            {
                this.limpiarTrailer();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert4('Trailer tiene solicitud pendiente.');", true);
            }
            else if (t.MOVI_ID != null && t.MOVI_ID != 0)
            {
                this.limpiarTrailer();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert4('Trailer tiene movimiento pendiente.');", true);
            }
            else
            {
                this.llenarDatos(t);
            }
        }
        else
        {
            this.limpiarTrailer();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert4('No existe ningún trailer con esa patente.');", true);
        }
    }

    protected void txt_placaTracto_TextChanged(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        t = t.obtenerXPlaca(this.txt_placaTracto.Text);
        if (t.ID != 0 && t.ID != null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "showAlert4('Placa corresponde a Trailer.');", true);
            this.limpiarTracto();
            btn_guarda_cambios.Enabled = false;
        }
        else
            btn_guarda_cambios.Enabled = true;
    }

    protected void rb_estado_CheckedChanged(object sender, EventArgs e)
    {
        if (this.rb_vacio.Checked)
        {   
            this.limpiarTracto();
            this.limpiarTrailer();

            this.activarTracto(false);
            this.activarTrailer(false);
            this.btn_guarda_cambios.Enabled = true;
        }
        else if (this.rb_tracto.Checked)
        {
            
            this.limpiarTrailer();
            this.activarTracto(true);
            this.activarTrailer(false);
        }
        else if (this.rb_trailer.Checked)
        {
            this.limpiarTracto();
             this.activarTracto(false);
            this.activarTrailer(true);
        }
    }

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
        lugar = lugar.obtenerXID(int.Parse(this.hf_idLugar.Value));
        DataTable dt = lugar.obtenerLugarEstado(lugar.ID_PLAYA,lugar.ID, lugar.ID_SITE );
        DataRow l = dt.Rows[0]; 
        this.pnl_detalleLugar.Style.Add("background-color", l["COLOR_LUGAR"].ToString());
        this.pnl_detalleLugar.Style.Add("color", "black");
        this.lbl_lugar.Text = l["LUGA_COD"].ToString();
        this.pnl_detalleTrailer.Attributes.Add("style", string.Format("background-color:{0};", l["TRTI_COLOR"].ToString()));
        this.lbl_origenZona.Text = lugar.ZONA;
        this.lbl_origenPlaya.Text = lugar.PLAYA;
        if (l["COLOR"].ToString() != "#FFFFFF")
        {
            this.pnl_imgAlerta.BackColor = System.Drawing.ColorTranslator.FromHtml(l["COLOR"].ToString());
            this.img_alerta.Visible = true;
        }
        else
        {
            this.img_alerta.Visible = false;
        }

        if (l["tres_icono"].ToString() != "")
        {
            this.img_trailer.ImageUrl = l["tres_icono"].ToString();
        }

        limpiar();

        switch (l["TRAI_ID"].ToString())
        {
            case "-1":
                this.rb_tracto.Checked = true;
                activarTracto(true);
                activarTrailer(false);
                this.img_trailer.Visible = false;
                this.spn_tracto.Visible = true;
                TractoBC trac = new TractoBC();
                trac.LUGA_ID = Convert.ToInt32(l["LUGA_ID"]);
                trac = trac.ObtenerTractoXLugar(trac.LUGA_ID);
                this.llenarDatos(trac);
                break;
            case "0":
                this.rb_vacio.Checked = true;
                activarTracto(false);
                activarTrailer(false);
                this.img_trailer.Visible = false;
                this.spn_tracto.Visible = false;
                break;
            default:
                this.rb_trailer.Checked = true;
                activarTracto(false);
                activarTrailer(true);
                this.img_trailer.Visible = true;
                this.spn_tracto.Visible = false;
                TrailerBC t = new TrailerBC();
                t.ID = Convert.ToInt32(l["TRAI_ID"]);
                t = t.obtenerXID();
                this.llenarDatos(t);
                break;
        }
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "$('#modalPendientes').modal();", true);
    }

    public void btn_guarda_cambios_click(object sender, EventArgs e)
    {
        LugarBC lugar = new LugarBC();
        lugar.ID = int.Parse(this.hf_idLugar.Value);
        TrailerBC t = new TrailerBC();
        t.NUMERO = this.txt_nroTrailer.Text;

        if (this.rb_tracto.Checked == true)
        {
            t.PATENTE_TRACTO = this.txt_placaTracto.Text;
                
            t.ID = 0;
            string msj;

            if (lugar.Cuadratura(this.usuario.ID, t, "", out msj) && msj == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalPendientes');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('cambios realizados correctamente!');", true);
                this.recargaplayas(null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", msj), true);
            }
        }
        else
        {
            if (this.rb_trailer.Checked)
            {
                t.PLACA = this.txt_placaTrailer.Text;
                t.ID = Convert.ToInt32(this.hf_idTrailer.Value);
            }
            else if (this.rb_vacio.Checked)
            {
                t.PLACA = "";
                t.ID = 0;
            }
            t.PATENTE_TRACTO = "";

            string msj;
            if (lugar.Cuadratura(this.usuario.ID, t, this.ddl_estadoTrailer.SelectedValue, out msj) && msj == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "cerrarModal('modalPendientes');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('cambios realizados correctamente!');", true);
                this.recargaplayas(null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", string.Format("alert('{0}');", msj), true);
            }
        }
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

    private void llenarDatos(TrailerBC t)
    {
        this.hf_idTrailer.Value = t.ID.ToString();
        this.txt_placaTrailer.Text = t.PLACA;
        this.txt_nroTrailer.Text = t.NUMERO;
        if (t.TRES_ID == 100)
        {
            this.ddl_estadoTrailer.SelectedValue = "0";
        }
        if( t.TRES_ID == 400)
        {
            this.ddl_estadoTrailer.SelectedValue = "1";
        }

        if (this.hf_idTrailer.Value != "") btn_guarda_cambios.Enabled = true;
        else btn_guarda_cambios.Enabled = false;
    }

    private void llenarDatos(TractoBC t)
    {
        this.hf_idTrailer.Value = t.ID.ToString();
        this.txt_placaTracto.Text = t.PATENTE;
    }

    private void limpiar()
    {
        this.rb_trailer.Checked = false;
        this.rb_tracto.Checked = false;
        this.rb_vacio.Checked = false;
        this.limpiarTracto();
        this.limpiarTrailer();
    }

    private void limpiarTrailer()
    {
        this.hf_idTrailer.Value = "";
        this.txt_placaTrailer.Text = "";
        this.txt_nroTrailer.Text = "";
        this.ddl_estadoTrailer.ClearSelection();
    }

    private void limpiarTracto()
    {
        this.txt_placaTracto.Text = "";
        this.ddl_transTracto.ClearSelection();
    }

    private void activarTrailer(bool activar)
    {
        this.txt_placaTracto.Text = "";
        this.txt_placaTrailer.Enabled = activar;
        this.txt_nroTrailer.Enabled = activar;
        this.ddl_estadoTrailer.Enabled = false;
        this.btn_guarda_cambios.Enabled = false;
    }

    private void activarTracto(bool activar)
    {
        this.txt_nroTrailer.Text = "";
        this.txt_placaTrailer.Text = "";
        this.txt_placaTracto.Enabled = activar;
        this.ddl_transTracto.Enabled = activar;
        this.btn_guarda_cambios.Enabled = false;
    }

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
                DataTable dt_lugares =lugar.obtenerLugarEstado(idplaya, 0, int.Parse(this.ddl_Site.SelectedValue));  
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
                    

                    Celda.ID = string.Format("cont_lugar_{0}", row_lugar["LUGA_ID"].ToString() + random.Next(1, 1000000).ToString());
              
               //     Celda.ID = string.Format("cont_lugar_{0}", row_lugar["LUGA_ID"].ToString());
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
}