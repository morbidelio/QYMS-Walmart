using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.Services;
using System.Web.Script.Services;

public class menuitem3
{
    public string ID { get; set; }

    public string Nombre { get; set; }
    public string icono { get; set; }
    public List<menuitem3> subitems { get; set; }
}


public partial class yms4 : System.Web.UI.Page
{
    public int maxzoom = 16;
    public string url = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/');

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public static menuitem3[] GetMenu(string usuario)
    {
        if (  HttpContext.Current.Session["Counter"] == null)
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();

            List<menuitem3> menuprincipal = new List<menuitem3>();
            menuprincipal.Add(new menuitem3() { ID = "0", Nombre = "Enviar a", icono = "2" });



            List<menuitem3> lst = new List<menuitem3>();
            DataTable ds = yms.Obtenermenu_lugar(Convert.ToInt32(usuario.Replace("lug_", "")));

            for (int value = 0; value <= ds.Rows.Count - 1; value++)

                lst.Add(new menuitem3() { ID = ds.Rows[value]["id"].ToString(), Nombre = ds.Rows[value]["nombre"].ToString(), icono = ds.Rows[value]["icono"].ToString() });
            menuprincipal[0].subitems = lst;

            return menuprincipal.ToArray();
        }
        else
            return null;
    }
    static UtilsWeb utils = new UtilsWeb();
  //  control_descarga Spinner1;
    protected void Page_Load(object sender, System.EventArgs e)
    {
     
        ScriptManager.GetCurrent(this).AsyncPostBackTimeout = 3600;
        Server.ScriptTimeout = 36000;
        _cambio_mapa.Value = "0";
        this.solicud_descarga.ButtonClickDemo += new EventHandler(cambiacombo);
      if (IsPostBack == false)
        {
            cargasites();
            cargaYMS(false);

        }




        
      
        if (IsPostBack)
        {
        //    if (Spinner1!=null && Spinner1.Visible == true)
        //    {
        //        Spinner1.Page_Load(sender, e);
        //    }
        }
    }


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


    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        _cambio_mapa.Value = "1";
        large.Controls.Clear();
        rlplayas.Items.Clear();
        cargaYMS(true);

    }

    protected void recargar_yms(object sender, EventArgs e) 
    {
        large.Controls.Clear();
        cargaYMS(false);
        carga_trailers();
    }



    public void llenaPlaya(DataRow Playa)
    {
        double top;
        double left;
        double width;
        double height;
        int desfasey;
        int desfasex;
        desfasey = 0;
        desfasex = 0;
        top = double.Parse( Playa["Playa_Y"].ToString());
        left = double.Parse(Playa["Playa_X"].ToString());
        width = double.Parse(Playa["width"].ToString());
        height = double.Parse(Playa["height"].ToString());

        Panel zona1l = new Panel();
        zona1l.ClientIDMode = ClientIDMode.Static;
        large.Controls.Add(zona1l);
        zona1l.Style["width"] = width.ToString().Replace(",", ".") + "%";
        zona1l.Style["height"] = height.ToString().Replace(",", ".") + "%";
        zona1l.ID = "play_" + Playa["id"].ToString();
        zona1l.CssClass = "zona";

        zona1l.CssClass = "zona";

        zona1l.Style["top"] = (top + desfasey).ToString().Replace(",", ".") + "%";
        zona1l.Style["left"] = (left + desfasex).ToString().Replace(",", ".") + "%";

        // 
        // zona1l.Style.Item("top") = 50.ToString + "%"
        // zona1l.Style.Item("left") = 50.ToString + "%"
        int lugares = Convert.ToInt32(Playa["lugares"].ToString());

        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        DataTable ds = yms.Obtenerlugares_playa(Convert.ToInt32(Playa["id"].ToString()), null/* TODO Change to default(_) if this is not a reference type */, null/* TODO Change to default(_) if this is not a reference type */);


        for (int value = 0; value <= lugares - 1; value++)
        {
             Control anterior=zona1l.FindControl("lug_" + ds.Rows[value]["id"].ToString());
            if (anterior != null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alerta" + ds.Rows[value]["CODIGO"].ToString(), "console.log('" + ds.Rows[value]["CODIGO"] + " repetido: " + ds.Rows[value]["trai_placa"].ToString() + ", " + ((Image)(anterior)).Attributes["patente"] + " ' );", true);
                                
                ((Image)anterior).Attributes.Add("repetido", "si");
                anterior = null;
            }
            else
                {

                Panel celda2 = new Panel();
                celda2.ClientIDMode = ClientIDMode.Static;
                Image imagen2 = new Image();
                imagen2.ClientIDMode = ClientIDMode.Static;
                imagen2.ID = "lug_" + ds.Rows[value]["id"].ToString();

             
                // celda2.Style.Add("display", "inline-grid")
                // celda2.CssClass = "icono1"
                // celda2.Width = ds.Rows(value)("ancho").ToString()
                // celda2.Height = ds.Rows(value)("alto").ToString()

                Label texto_trailer = new Label();
                texto_trailer.CssClass = "patente_" + ds.Rows[value]["rotacion"].ToString();
                texto_trailer.Text = ds.Rows[value]["trai_id"].ToString();
                // celda2.Controls.Add(texto_trailer)
                Image pallet = new Image();
                Image ottawa = new Image();
                // Dim tabla As New Table
                // tabla.Style.Add("border", "1px solid white")

                // tabla.CssClass = "tabla_lugar"
                imagen2.Attributes.Add("rotacion", ds.Rows[value]["rotacion"].ToString());

                int estado_sol = int.Parse(ds.Rows[value]["SOES_ID"].ToString());


                if (ds.Rows[value]["trai_id"].ToString() == "0")
                {
                    imagen2.Attributes.Add("patente", "vacio");
                    imagen2.ImageUrl = "../images/yms_estacionamiento_" + ds.Rows[value]["rotacion"].ToString() + ".png";
                    imagen2.Width = Unit.Percentage(double.Parse(ds.Rows[value]["ancho"].ToString())); // ;(ds.Rows(value)("ancho") - 2).ToString() + "%"
                    imagen2.Height = Unit.Percentage(double.Parse(ds.Rows[value]["alto"].ToString())); // ; (ds.Rows(value)("alto") - 2).ToString() + "%"
                    imagen2.CssClass = "context-menu-two icono masterTooltip lugar";
                    imagen2.Attributes.Add("title1", "Estacionamiento Vacío");
                    texto_trailer.Text = ""; // ds.Rows(value)("trai_id").ToString()
                }
                else
                {
                    imagen2.Attributes.Add("patente", ds.Rows[value]["trai_placa"].ToString());
                    if (!System.IO.File.Exists(  Server.MapPath(@"~/images" )  + "yms_trailer_" + ds.Rows[value]["rotacion"].ToString() + '_' + ds.Rows[value]["TRTI_COLOR"].ToString() ))
                        imagen2.ImageUrl = "../images/yms_trailer_" + ds.Rows[value]["rotacion"].ToString() + '_' + "gray";
                    else
                        imagen2.ImageUrl = "../images/yms_trailer_" + ds.Rows[value]["rotacion"].ToString() + '_' + ds.Rows[value]["TRTI_COLOR"].ToString();
                 


                    // imagen2.ImageUrl = "../images/yms_trailer_" + "1"+ '_' + "red";
                    imagen2.Width = Unit.Percentage(double.Parse(ds.Rows[value]["ancho"].ToString())); // (ds.Rows(value)("ancho") - 2).ToString() + "%"
                    imagen2.Height = Unit.Percentage(double.Parse(ds.Rows[value]["alto"].ToString())); // ; (ds.Rows(value)("alto") - 2).ToString() + "%"
                    imagen2.CssClass = "context-menu-one icono masterTooltip lugar";
                    imagen2.Attributes.Add("title1", "Doc Entrada:" + ds.Rows[value]["TRUE_DOC_INGRESO"].ToString());
                    texto_trailer.Text = ds.Rows[value]["trai_id"].ToString();


                 


                    if (estado_sol >= 0 && estado_sol < 20)
                    {
                  //      imagen2.ImageUrl += "_"+"empty";
                        imagen2.Attributes.Add("estado_carga", "../images/empty_" + ds.Rows[value]["rotacion"].ToString() + ".png");
                    }
                    else if (estado_sol >= 20 && estado_sol < 30)
                    {
                  //      imagen2.ImageUrl += "_" + "medium";
                        imagen2.Attributes.Add("estado_carga", "../images/medium_" + ds.Rows[value]["rotacion"].ToString() + ".png");
                    }
                    else if (estado_sol == 30)
                    {
                    //    imagen2.ImageUrl += "_" + "full";
                        imagen2.Attributes.Add("estado_carga", "../images/full_" + ds.Rows[value]["rotacion"].ToString() + ".png");
                    }



                    if (estado_sol != 0)         //Si tiene carga en proceso o está cargado, a la espera de finalizar solicitud
                    {
                        if (ds.Rows[value]["TRUE_CARGADO"].ToString() == "False")//Si está en carga
                        {
                            if (int.Parse(ds.Rows[value]["SOAN_MINS_CARGA_REAL"].ToString()) <= 120 &&
                                int.Parse(ds.Rows[value]["SOAN_MINS_CARGA_REAL"].ToString()) >= 0)
                            {
                      //          imagen2.ImageUrl +="_"+"ok";
                                imagen2.Attributes.Add("estado_reloj", "../images/ok_" + ds.Rows[value]["rotacion"].ToString() + ".png");
                            }
                            else if (int.Parse(ds.Rows[value]["SOAN_MINS_CARGA_REAL"].ToString()) <= 180 &&
                                     int.Parse(ds.Rows[value]["SOAN_MINS_CARGA_REAL"].ToString()) >= 121)
                            {
                        //        imagen2.ImageUrl += "_" + "enhora";
                                imagen2.Attributes.Add("estado_reloj", "../images/enhora_" + ds.Rows[value]["rotacion"].ToString() + ".png");
                            }
                            else if (int.Parse(ds.Rows[value]["SOAN_MINS_CARGA_REAL"].ToString()) >= 181)
                            {
                          //      imagen2.ImageUrl += "_" + "atrasado";
                                imagen2.Attributes.Add("estado_reloj", "../images/atrasado_" + ds.Rows[value]["rotacion"].ToString() + ".png");
                            }
                        }
                        else
                        {
                            if (int.Parse(ds.Rows[value]["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) <= 15 &&
                                int.Parse(ds.Rows[value]["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) >= 0)
                            {
                                imagen2.Attributes.Add("estado_reloj", "../images/ok_" + ds.Rows[value]["rotacion"].ToString() + ".png");
                        //        imagen2.ImageUrl += "_" + "ok";
                            }
                            else if (int.Parse(ds.Rows[value]["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) <= 30 &&
                                     int.Parse(ds.Rows[value]["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) >= 16)
                            {
                          //      imagen2.ImageUrl += "_" + "enhora";
                                imagen2.Attributes.Add("estado_reloj", "../images/enhora_" + ds.Rows[value]["rotacion"].ToString() + ".png");
                            }
                            else if (int.Parse(ds.Rows[value]["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) >= 31)
                            {
                            //    imagen2.ImageUrl += "_" + "atrasado";
                                imagen2.Attributes.Add("estado_reloj", "../images/atrasado_" + ds.Rows[value]["rotacion"].ToString() + ".png");
                            }
                        }
                    }


                    imagen2.ImageUrl += ".png";


                }


                


                imagen2.Attributes.Add("playa", ds.Rows[value]["PLAY_COD"].ToString());
                imagen2.Attributes.Add("posicion", ds.Rows[value]["orden"].ToString());
                imagen2.Attributes.Add("codigo_lugar", ds.Rows[value]["CODIGO"].ToString());
                imagen2.Attributes.Add("trai_id", ds.Rows[value]["TRAI_ID"].ToString());

                if (ds.Rows[value]["MOVI_ORIGEN"].ToString() != "0") imagen2.Attributes.Add("movimiento", ds.Rows[value]["MOVI_ORIGEN"].ToString());

                else if (ds.Rows[value]["MOVI_DEST"].ToString() != "0") imagen2.Attributes.Add("movimiento", ds.Rows[value]["MOVI_DEST"].ToString()); 

                else imagen2.Attributes.Add("movimiento", "");


                // prueba tabla
                if ((ds.Rows[value]["rotacion"].ToString() == "2") | (ds.Rows[value]["rotacion"].ToString() == "4"))
                {


                    imagen2.Style["top"] = Unit.Percentage(double.Parse(ds.Rows[value]["LUGAR_y"].ToString())).ToString().Replace(",", "."); // topimagen.ToString() + "%"
                    imagen2.Style["left"] = Unit.Percentage(double.Parse(ds.Rows[value]["LUGAR_X"].ToString())).ToString().Replace(",", "."); // (leftimagen).ToString() + "%"
                }
                else if ((ds.Rows[value]["rotacion"].ToString() == "1") | (ds.Rows[value]["rotacion"].ToString() == "3"))
                {



                    imagen2.Style["top"] = Unit.Percentage(double.Parse(ds.Rows[value]["LUGAR_y"].ToString())).ToString().Replace(",", "."); // topimagen.ToString() + "%"
                    imagen2.Style["left"] = Unit.Percentage(double.Parse(ds.Rows[value]["LUGAR_X"].ToString())).ToString().Replace(",", "."); // (leftimagen).ToString() + "%"
                }



                // celda2.Controls.Add(tabla)


                celda2.Controls.Add(imagen2);

                if ((1 == 1))
                {
                    pallet.ImageUrl = "../images/yms_pallet_vacio.png";
                    pallet.Width = 3; // (ds.Rows(value)("ancho") - 2).ToString()
                    pallet.Height = 3; // (ds.Rows(value)("alto") - 2).ToString()
                }


                if ((ds.Rows[value]["ottawa"].ToString() != "0"))
                {
                    ottawa.ImageUrl = "../images/ottawa_" + ds.Rows[value]["rotacion"].ToString() + ".png";

                    celda2.Controls.Add(ottawa);
                }

                zona1l.Controls.Add(celda2);

                if (imagen2.ID == "lug_2776")
                {
                    imagen2.Attributes["prueba"] = "pueba";
                }

            }
        }
    }


    public void cargasites()
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
        UtilsWeb utilidades = new UtilsWeb();
        utilidades.CargaDropNormal(dropsite, "ID", "NOMBRE", ds);
    }



    public void cargaYMS(bool forzarcargaplayas)
    {
        // aquí va el sp




        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        var imagen = yms.ObteneSite(Convert.ToInt32( dropsite.SelectedValue)).Rows[0]["SITE_IMAGEN"];
        siteimage.ImageUrl = url + "/images/" + imagen;
        DataTable ds = yms.ObtenerPlayas_Site(Convert.ToInt32(dropsite.SelectedValue), null, null);
        carga_trailers();
        try
        {
            int i = 0;
            while (i < ds.Rows.Count)
            {
                llenaPlaya(ds.Rows[i]);
                 if (IsPostBack == false || forzarcargaplayas==true  ) {

                agregafiltroplaya(ds.Rows[i]);
                 }
                i = i + 1;
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void carga_trailers()
    {

        TrailerBC cc = new TrailerBC();
        DataTable dt = cc.obtenerXParametro("", null, dropsite.SelectedValue);
        utils.CargaDrop(trailers, "PLACA", "PLACA", dt);
    }

    public void agregafiltroplaya(DataRow playa)
    {
        ListItem item = new ListItem(playa["descripcion"].ToString(), "play_" + playa["id"].ToString());
       item.Selected = true;
       item.Attributes.Add("id", "chk_play_" + playa["id"].ToString());
       item.Attributes.Add("onclick", "chk_play_" + playa["id"].ToString());
        rlplayas.Items.Add(item);
    }

   

    private void MMensaje(string mensaje)
    {
        System.Text.StringBuilder scriptMsj = new System.Text.StringBuilder();
        scriptMsj.Append("<script language='javascript'>");
        scriptMsj.Append("alert('");
        scriptMsj.Append(mensaje);
        scriptMsj.Append("');</script>");
        this.ClientScript.RegisterStartupScript(this.GetType(), "MENSAJE", scriptMsj.ToString());
    }

    protected void llenaformulario(object sender, EventArgs e)
    {
        
    //    cargaYMS(false);
      //  uptform.Controls.Clear();

               LugarBC lugar = new LugarBC();

      lugar = lugar.obtenerXID(Int32.Parse(id_destino_formulario.Value.Replace("lug_", "")));
        

        if (solicitudMovimiento.SelectedValue=="1"|| 1==1)

        {
            
          //  Spinner1 = (control_descarga)LoadControl("~/control_descarga.ascx");
            solicud_descarga.Visible = true;
            this.btn_confirmar.Enabled = solicud_descarga.carga(DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), Convert.ToInt32(dropsite.SelectedValue), trailer_seleccionado.Value,lugar.ID,lugar.ID_PLAYA,lugar.ID_ZONA);
      

            //    ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(Spinner1);
            
         //   uptform.Controls.Add(Spinner1);

         //   AsyncPostBackTrigger trigger1 = new AsyncPostBackTrigger();
         //   trigger1.EventName = "selectedindexchanged";
         //   trigger1.ControlID = Spinner1.FindControl("ddl_origenZona").UniqueID;
         //   this.UPT.Triggers.Add(trigger1);
  
        
        }

        UPT.Update();

        this.ClientScript.RegisterStartupScript(this.GetType(), "modal2", " $('#ModalFiltro').modal();");
    }

    protected void confirmar(Object sender, EventArgs e)
    { 
        if (this.solicud_descarga.Visible)
           {
               this.solicud_descarga.btn_confirmar_Click(null, null);
               cargaYMS(false);
        
            }
        
    
    }

    private void cambiacombo(object sender, EventArgs e)
    {
        cargaYMS(false);

    }

}
