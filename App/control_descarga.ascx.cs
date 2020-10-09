using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class control_descarga : System.Web.UI.UserControl
{
    static UtilsWeb utils = new UtilsWeb();

    public event EventHandler ButtonClickDemo;

    public void Page_Load(object sender, EventArgs e)  
    {
       
    }


    public bool carga(string fecha, string hora, int site, string trailer, int id_lugar, int id_playa, int id_zona)
    {


        try
        {
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
            cargasite(site);
            ddl_destinoZona.SelectedValue = id_zona.ToString();
            ddl_destinoZona_SelectedIndexChanged(null, null);
            ddl_destinoPlaya.SelectedValue = id_playa.ToString();
            ddl_destinoPlaya_SelectedIndexChanged(null, null);
            ddl_destinoPos.SelectedValue = id_lugar.ToString();

            return buscar(trailer, site.ToString());
           
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje2", "alert('Lugar de Destino No permitido');", true);
            return false;     
        }

               


            //if (ddl_origenZona.SelectedIndex != 0)
            //{
            //    int id_zona = int.Parse(ddl_origenZona.SelectedValue);
            //    PlayaBC playa = new PlayaBC();
            //    utils.CargaDrop(ddl_origenPlaya, "ID", "DESCRIPCION", playa.ObtenerXZona(id_zona));
            //    if (ddl_origenPlaya.Items.Count > 1)
            //    {
            //        ddl_origenPlaya.Enabled = true;
            //        if (ddl_origenPlaya.SelectedIndex != 0)
            //        {
            //            int id_playa = int.Parse(ddl_origenPlaya.SelectedValue);
            //            LugarBC lugar = new LugarBC();
            //            utils.CargaDrop(ddl_origenPos, "ID", "DESCRIPCION", lugar.obtenerLugaresXPlaya(id_playa));
            //            if (ddl_origenPos.Items.Count > 1)
            //                ddl_origenPos.Enabled = true;
            //            else
            //                ddl_origenPos.Enabled = false;
            //        }
            //    }
            //    else
            //    {
            //        ddl_origenPlaya.Enabled = false;
            //        ddl_origenPos.Enabled = false;
            //    }
            //}

        //    if (ddl_destinoZona.SelectedIndex != 0)
        //    {
        //        int id_zona = int.Parse(ddl_destinoZona.SelectedValue);
        //        PlayaBC playa = new PlayaBC();
        //        utils.CargaDrop(ddl_destinoPlaya, "ID", "DESCRIPCION", playa.ObtenerXZona(id_zona));
        //        if (ddl_destinoPlaya.Items.Count > 1)
        //        {
        //            ddl_destinoPlaya.Enabled = true;
        //            if (ddl_destinoPlaya.SelectedIndex != 0)
        //            {
        //                int id_playa = int.Parse(ddl_destinoPlaya.SelectedValue);
        //                LugarBC lugar = new LugarBC();
        //                utils.CargaDrop(ddl_destinoPos, "ID", "DESCRIPCION", lugar.obtenerLugaresXPlaya(id_playa));
        //                if (ddl_destinoPos.Items.Count > 1)
        //                    ddl_destinoPos.Enabled = true;
        //                else
        //                    ddl_destinoPos.Enabled = false;
        //            }
        //        }
        //        else
        //        {
        //            ddl_destinoPlaya.Enabled = false;
        //            ddl_destinoPos.Enabled = false;
        //        }
            
        //}
    }



        

    public void cargasite(int site)
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();

   //     utils.CargaDrop(ddl_origenZona, "ID", "DESCRIPCION", yms.ObtenerZonas(site, "", "0"));
        utils.CargaDrop(ddl_destinoZona, "ID", "DESCRIPCION", yms.ObtenerZonas(site, "", null));

    }


    public void ddl_origenZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (!(ddl_origenZona.SelectedIndex == 0))
        //{
        //    int id_zona = int.Parse(ddl_origenZona.SelectedValue);
        //    PlayaBC playa = new PlayaBC();
        //    utils.CargaDrop(ddl_origenPlaya, "ID", "DESCRIPCION", playa.ObtenerXZona(id_zona));
        //    if (ddl_origenPlaya.Items.Count > 1)
        //    {
        //        ddl_origenPlaya.Enabled = true;
        //        if (ddl_origenPlaya.SelectedIndex != 0)
        //        {
        //            int id_playa = int.Parse(ddl_origenPlaya.SelectedValue);
        //            LugarBC lugar = new LugarBC();
        //            utils.CargaDrop(ddl_origenPos, "ID", "DESCRIPCION", lugar.obtenerLugaresXPlaya(id_playa));
        //            if (ddl_origenPos.Items.Count > 1)
        //                ddl_origenPos.Enabled = true;
        //            else
        //                ddl_origenPos.Enabled = false;
        //        }
        //    }
        //    else
        //    {
        //        ddl_origenPlaya.Enabled = false;
        //        ddl_origenPos.Enabled = false;
        //    }
        //}
        //else
        //{
        //    ddl_origenPos.Enabled = false;
        //    ddl_origenPlaya.Enabled = false;
        //    ddl_origenPos.SelectedIndex = 0;
        //    ddl_origenPlaya.SelectedIndex = 0;
        //}
    }
    public void ddl_origenPlaya_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (!(ddl_origenPlaya.SelectedIndex == 0) || (ddl_origenZona.SelectedIndex == 0))
        //{
        //    int id_playa = int.Parse(ddl_origenPlaya.SelectedValue);
        //    LugarBC lugar = new LugarBC();
        //    utils.CargaDrop(ddl_origenPos, "ID", "DESCRIPCION", lugar.obtenerLugaresXPlaya(id_playa));
        //    if (ddl_origenPos.Items.Count > 1)
        //        ddl_origenPos.Enabled = true;
        //    else
        //        ddl_origenPos.Enabled = false;
        //}
        //else
        //{
        //    ddl_origenPos.SelectedIndex = 0;
        //    ddl_origenPos.Enabled = false;
        //}
    }
    public void ddl_destinoZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(ddl_destinoZona.SelectedIndex == 0))
        {
            int id_zona = int.Parse(ddl_destinoZona.SelectedValue);
            PlayaBC playa = new PlayaBC();
            utils.CargaDrop(ddl_destinoPlaya, "ID", "DESCRIPCION", playa.ObtenerXZona(id_zona));
            if (ddl_destinoPlaya.Items.Count > 1)
            {
                ddl_destinoPlaya.Enabled = true;
                if (ddl_destinoPlaya.SelectedIndex != 0)
                {
                    int id_playa = int.Parse(ddl_destinoPlaya.SelectedValue);
                    LugarBC lugar = new LugarBC();
                    YMS_ZONA_BC yms = new YMS_ZONA_BC();
                    DataTable ds1 = yms.Obtenerlugares_playa(id_playa, null, "0");
                    utils.CargaDrop(ddl_destinoPos, "ID", "DESCRIPCION",ds1);
                    if (ddl_destinoPos.Items.Count > 1)
                        ddl_destinoPos.Enabled = true;
                    else
                        ddl_destinoPos.Enabled = false;
                }
            }
            else
            {
                ddl_destinoPlaya.Enabled = false;
                ddl_destinoPos.Enabled = false;
            }
        }
        else
        {
            ddl_destinoPos.Enabled = false;
            ddl_destinoPlaya.Enabled = false;
            ddl_destinoPos.SelectedIndex = 0;
            ddl_destinoPlaya.SelectedIndex = 0;
        }
   //     ButtonClickDemo(null, null);
    }
    public void ddl_destinoPlaya_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(ddl_destinoPlaya.SelectedIndex == 0) || (ddl_destinoZona.SelectedIndex == 0))
        {
            int id_playa = int.Parse(ddl_destinoPlaya.SelectedValue);
            LugarBC lugar = new LugarBC();
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds1 = yms.Obtenerlugares_playa(id_playa, null, "0");
            utils.CargaDrop(ddl_destinoPos, "ID", "DESCRIPCION", ds1);
            if (ddl_destinoPos.Items.Count > 1)
                ddl_destinoPos.Enabled = true;
            else
                ddl_destinoPos.Enabled = false;
        }
        else
        {
            ddl_destinoPos.SelectedIndex = 0;
            ddl_destinoPos.Enabled = false;
        }
        ButtonClickDemo(null,null);
    }





    public void btn_confirmar_Click(object sender, EventArgs e)
    {
        try
        {
            MovimientoBC mov = new MovimientoBC();
            //SolicitudBC solicitud = new SolicitudBC();
            //TrailerUltEstadoBC traiue = new TrailerUltEstadoBC();
            //int id = int.Parse(hf_trailerId.Value);
            //traiue = traiue.CargaTrue(id);
            //string fh = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            //solicitud.ID_TIPO = 2;
            //solicitud.ID_USUARIO = 1;   // Variable de sesión
            //solicitud.FECHA_CREACION = DateTime.Now;
            //solicitud.FECHA_PLAN_ANDEN = DateTime.Parse(fh);
            //solicitud.DOCUMENTO = traiue.DOC_INGRESO;
            //solicitud.OBSERVACION = "";
            //solicitud.ID_TRAILER = id;
            //solicitud.ID_DESTINO = int.Parse(ddl_destinoPos.SelectedValue);
            //if (solicitud.Descarga(solicitud))
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Solicitud ingresada correctamente');", true);
            //else
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Ocurrió un error!');", true);

            if (this.hf_trailerId.Value == "" || this.hf_trailerId.Value == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe seleccionar un trailer.');", true);
            }
            else
            {
                mov.ID_TRAILER = int.Parse(this.hf_trailerId.Value);
                mov.FECHA_CREACION = DateTime.Now;
                mov.OBSERVACION = "";
                mov.FECHA_ORIGEN = DateTime.Parse(DateTime.Now.ToString());
                mov.FECHA_DESTINO = mov.FECHA_ORIGEN.AddMinutes(15);
                mov.ID_DESTINO = int.Parse(this.ddl_destinoPos.SelectedValue);
                mov.ID_ESTADO = 10;
                mov.OBSERVACION = "";
                TrailerUltEstadoBC trailerUE = new TrailerUltEstadoBC();
                trailerUE.ID = int.Parse(this.hf_trailerId.Value);
                trailerUE.SITE_ID = 1; // Cambiar después de introducir variables de sesión
                trailerUE.SITE_IN = true;
                trailerUE.LUGAR_ID = int.Parse(this.ddl_destinoPos.SelectedValue);

                TrailerBC trailer = new TrailerBC();

                mov.MANT_EXTERNO = false;
                mov.ID_TRAILER = int.Parse(this.hf_trailerId.Value);

                //  trailer.PLACA = this.txt_buscarPatente.Text;
                //     trailer.CODIGO = "S/ CODIGO";

                //  trailer.NUMERO = this.txt_buscarNro.Text;
                trailer.ID = mov.ID_TRAILER;
                UsuarioBC usuario = (UsuarioBC)Session["USUARIO"];
                string resultado;
                bool ejecucion = mov.MOVIMIENTO(mov, trailerUE, trailer, usuario.ID, out resultado);

                if ( ejecucion && resultado=="")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Se ingresaron correctamente los datos.');", true);
                    ddl_destinoZona.SelectedValue = "0";
                    ddl_destinoZona_SelectedIndexChanged(null, null);
                    this.pnl_detalleLugar.Attributes.Remove("style");
                    this.pnl_detalleTrailer.CssClass = "";
                    this.pnl_detalleTrailer.Attributes.Remove("style");
                    this.img_reloj.ImageUrl = "";
                    this.img_trailer.ImageUrl = "";
                    this.lbl_lugar.Text = "";
                    this.lbl_origenZona.Text = "";
                    this.lbl_origenPlaya.Text = "";

                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Ocurrió un error!');", true);
        }

    }
    


    public bool buscar(string patente, string site)
    {
        TrailerBC trailer = new TrailerBC();
        LugarBC lugar = new LugarBC();
       this.pnl_detalleLugar.Attributes.Remove("style");
       this.pnl_detalleTrailer.CssClass = "";
       this.pnl_detalleTrailer.Attributes.Remove("style");
        this.img_reloj.ImageUrl = "";
        this.img_trailer.ImageUrl = "";
        this.lbl_lugar.Text = "";
        this.lbl_origenZona.Text = "";
        this.lbl_origenPlaya.Text = "";

        trailer = trailer.obtenerXPlaca(patente);
        if (trailer.ID == 0 || trailer.ID == null) //Trailer no existe
        {
            this.txt_nroPista.Text = "";
            this.txt_transporte.Text = "";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer no existe en la base de datos.');", true);
            return false;
        }
        else //Trailer existente, trae datos
        {
            if (trailer.SITE_ID == null || trailer.SITE_ID == 0 || !trailer.SITE_IN ||              //Si trailer no tiene un último estado se asume que no está en ningún site
                trailer.LUGAR_ID == null || trailer.LUGAR_ID == 0)
            {
                this.ddl_destinoZona.Enabled = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer no ingresado correctamente. Realice el proceso de entrada correspondiente.');", true);
                return false;
            }
            else if (trailer.SITE_ID != int.Parse(site))
            {
                this.ddl_destinoZona.Enabled = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer no se encuentra en el site. Realice el proceso de entrada correspondiente.');", true);
                return false;
            }
            else if (trailer.SOLI_ID != null &&
                     trailer.SOLI_ID != 0 )
            {
                this.ddl_destinoZona.Enabled = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer tiene solicitud pendiente.');", true);
                return false;
            }
            else if (trailer.MOVI_ID != null &&
                     trailer.MOVI_ID != 0 )
            {
                this.ddl_destinoZona.Enabled = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer tiene movimiento pendiente.');", true);
                return false;
            }
            else
            {
                lugar = lugar.obtenerXID(trailer.LUGAR_ID);
                DataTable dt_lugares = lugar.obtenerLugarEstado(trailer.LUGAR_ID);
                DataRow row_lugar = dt_lugares.Rows[0];
                this.hf_trailerId.Value = trailer.ID.ToString();
                if (row_lugar["MOVI_ORIGEN"].ToString() != "0" ||
                    row_lugar["MOVI_DEST"].ToString() != "0")    //Si tiene movimiento pendiente
                {
                    this.pnl_detalleLugar.Attributes.Add("style", "background-color:yellow;");
                }
                else if (row_lugar["LUGA_OCUPADO"].ToString() == "True")
                {
                    this.pnl_detalleLugar.Attributes.Add("style", "background-color:green;");
                }
                this.lbl_lugar.Text = row_lugar["LUGA_COD"].ToString();
                this.pnl_detalleTrailer.CssClass = "row columna-anden detalle-trailer";
                this.pnl_detalleTrailer.Attributes.Add("style", string.Format("background-color:{0};", row_lugar["TRTI_COLOR"].ToString()));
                int estado_sol = int.Parse(row_lugar["SOES_ID"].ToString());
                if (estado_sol != 0)         //Si tiene carga en proceso o está cargado, a la espera de finalizar solicitud
                {
                    if (row_lugar["TRUE_CARGADO"].ToString() == "False")//Si está en carga
                    {
                        if (int.Parse(row_lugar["SOAN_MINS_CARGA_REAL"].ToString()) <= 120 &&
                            int.Parse(row_lugar["SOAN_MINS_CARGA_REAL"].ToString()) >= 0)
                        {
                            this.img_reloj.ImageUrl = "./Img/relojverde.png";
                        }
                        else if (int.Parse(row_lugar["SOAN_MINS_CARGA_REAL"].ToString()) <= 180 &&
                                 int.Parse(row_lugar["SOAN_MINS_CARGA_REAL"].ToString()) >= 121)
                        {
                            this.img_reloj.ImageUrl = "./Img/relojnaranja.png";
                        }
                        else if (int.Parse(row_lugar["SOAN_MINS_CARGA_REAL"].ToString()) >= 181)
                        {
                            this.img_reloj.ImageUrl = "./Img/relojrojo.png";
                        }
                    }
                    else
                    {
                        if (int.Parse(row_lugar["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) <= 15 &&
                            int.Parse(row_lugar["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) >= 0)
                        {
                            this.img_reloj.ImageUrl = "./Img/relojverde.png";
                        }
                        else if (int.Parse(row_lugar["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) <= 30 &&
                                 int.Parse(row_lugar["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) >= 16)
                        {
                            this.img_reloj.ImageUrl = "./Img/relojnaranja.png";
                        }
                        else if (int.Parse(row_lugar["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) >= 31)
                        {
                            this.img_reloj.ImageUrl = "./Img/relojrojo.png";
                        }
                    }
                }
                if (row_lugar["TRAI_PLACA"].ToString() != "0")
                {
                    //strb.Append("<img ");
                    if (estado_sol >= 0 && estado_sol < 20)
                    {
                        this.img_trailer.ImageUrl = "./Img/tra_vacio.png";
                    }
                    else if (estado_sol >= 20 && estado_sol < 30)
                    {
                        this.img_trailer.ImageUrl = "./Img/tra_semivacio.png";
                    }
                    else if (estado_sol == 30)
                    {
                        this.img_trailer.ImageUrl = "./Img/tra_ocupado.png";
                    }
                }
                this.lbl_origenZona.Text = lugar.ZONA;
                this.lbl_origenPlaya.Text = lugar.PLAYA;
                this.ddl_destinoZona.Enabled = true;
                this.txt_nroPista.Text = trailer.PLACA.ToString();
                txt_transporte.Text = trailer.TRANSPORTISTA.ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Se cargaron los datos del trailer seleccionado.');", true);
                return true;
            }
        }
    }




}