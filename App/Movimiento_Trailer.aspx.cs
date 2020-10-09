// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using System.Web.UI;

public partial class App_Movimiento_Trailer : System.Web.UI.Page
{
    static UtilsWeb utils = new UtilsWeb();
    CargaDrops drops = new CargaDrops();

    protected void Page_Load(object sender, EventArgs e)
    {
        ZonaBC zona = new ZonaBC();
        PlayaBC playa = new PlayaBC();
        LugarBC lugar = new LugarBC();
        TransportistaBC transportista = new TransportistaBC();
        if (!this.IsPostBack)
        {
            this.txt_fechaMovimiento.Text = DateTime.Now.ToString("dd/MM/yyyy") ;
            utils.CargaDrop(this.ddl_transportista, "ID", "NOMBRE", transportista.ObtenerTodos());
            YMS_ZONA_BC yms = new YMS_ZONA_BC();
            DataTable ds = yms.ObteneSites(((UsuarioBC)Session["Usuario"]).ID);
            utils.CargaDropNormal(this.dropsite, "ID", "NOMBRE", ds);
            this.drop_SelectedIndexChanged(null, null);
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

    #region DropDownList

    protected void drop_SelectedIndexChanged(object sender, EventArgs e)
    {
        YMS_ZONA_BC yms = new YMS_ZONA_BC();
        limpiar();
        drops.Zona(ddl_destinoZona, int.Parse(this.dropsite.SelectedValue));
        ddl_destinoZona_SelectedIndexChanged(null, null);
        if (this.ddl_destinoZona.Items.Count > 1)
            this.ddl_destinoZona.Enabled = true;
        else
            this.ddl_destinoZona.Enabled = false;
        //utils.CargaDrop(this.ddl_destinoZona, "ID", "DESCRIPCION", yms.ObtenerZonas(int.Parse(this.dropsite.SelectedValue), "", null));
    }

    protected void ddl_destinoZona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_destinoZona.SelectedIndex > 0)
        {
            int id_zona = int.Parse(this.ddl_destinoZona.SelectedValue);
            drops.Playa(ddl_destinoPlaya, id_zona);
            ddl_destinoPlaya_SelectedIndexChanged(null, null);
            if (this.ddl_destinoPlaya.Items.Count > 1)
                this.ddl_destinoPlaya.Enabled = true;
            else
                this.ddl_destinoPlaya.Enabled = false;
            //PlayaBC playa = new PlayaBC();
            //utils.CargaDrop(this.ddl_destinoPlaya, "ID", "DESCRIPCION", playa.ObtenerXZona(id_zona));
            //if (this.ddl_destinoPlaya.Items.Count > 1)
            //{
            //    this.ddl_destinoPlaya.Enabled = true;
            //    if (this.ddl_destinoPlaya.SelectedIndex != 0)
            //    {
            //        this.ddl_destinoPlaya_SelectedIndexChanged(null, null);
            //    }
            //}
            //else
            //{
            //    this.ddl_destinoPlaya.Enabled = false;
            //    this.ddl_destinoPos.Enabled = false;
            //}
        }
        else
        {
            this.ddl_destinoPos.Enabled = false;
            this.ddl_destinoPlaya.Enabled = false;
            this.ddl_destinoPos.ClearSelection();
            this.ddl_destinoPlaya.ClearSelection();
        }
    }

    protected void ddl_destinoPlaya_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddl_destinoPlaya.SelectedIndex > 0)
        {
            int id_playa = int.Parse(this.ddl_destinoPlaya.SelectedValue);
            drops.Lugar1(ddl_destinoPos, 0, id_playa,0,1);
            //LugarBC lugar = new LugarBC();
            //YMS_ZONA_BC yms = new YMS_ZONA_BC();
            //DataTable ds1 = yms.Obtenerlugares_playa(id_playa, null, "0");
            //utils.CargaDrop(this.ddl_destinoPos, "ID", "DESCRIPCION", ds1);
            if (this.ddl_destinoPos.Items.Count > 1)
                this.ddl_destinoPos.Enabled = true;
            else
                this.ddl_destinoPos.Enabled = false;
        }
        else
        {
            this.ddl_destinoPos.ClearSelection();
            this.ddl_destinoPos.Enabled = false;
        }
    }

    #endregion

    #region Buttons


    protected void busca(object sender, EventArgs e)
    {
        btn_buscarTrailer_Click(sender, e);
    }
    protected void btn_buscarTrailer_Click(object sender, EventArgs e)
    {
        TrailerBC trailer = new TrailerBC();
        if (this.txt_buscarNro.Text != "" || this.txt_buscarPatente.Text != "")
        {
            if (this.txt_buscarNro.Text != "")
                trailer = trailer.obtenerXNro(txt_buscarNro.Text);
         
            else if (utils.patentevalida(txt_buscarPatente.Text) == true)
                trailer = trailer.obtenerXPlaca(txt_buscarPatente.Text);
            else
            {
                limpiar();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Patente Invalida.');", true);
            }
            if (trailer.ID == 0 || trailer.ID == null ) //Trailer no existe
            {
                limpiar();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer no existe en la base de datos.');", true);
            }
            else //Trailer existente, trae datos
            {
                if (trailer.SITE_ID == null || trailer.SITE_ID == 0 || !trailer.SITE_IN               //Si trailer no tiene un último estado se asume que no está en ningún site
                    )
                {
                    limpiar();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer en ruta.');", true);
                }
                else if (trailer.SITE_ID != int.Parse(this.dropsite.SelectedValue))
                {
                    limpiar();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer no se encuentra en el site.');", true);
                }
                else if (trailer.SOLI_ID != null &&
                         trailer.SOLI_ID != 0)
                {
                    limpiar();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer tiene solicitud pendiente.');", true);
                }
                else if (trailer.MOVI_ID != null &&
                         trailer.MOVI_ID != 0)
                {
                    limpiar();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer tiene movimiento pendiente.');", true);
                }

                else if (trailer.BLOQUEADO == "True")
                {
                    limpiar();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer bloqueado.');", true);
                }
                else
                {
                    drop_SelectedIndexChanged(null, null);

                    this.hf_idTrailer.Value = trailer.ID.ToString();
                    dibujo(trailer.LUGAR_ID);
                    this.txt_buscarPatente.Text = trailer.PLACA;
                    this.txt_buscarNro.Text = trailer.NUMERO;
                    this.ddl_transportista.SelectedValue = trailer.TRAN_ID.ToString();
                    if (trailer.EXTERNO)
                    {
                        this.rb_trailerPropio.Checked = false;
                        this.rb_trailerExterno.Checked = true;
                    }
                    else
                    {
                        this.rb_trailerPropio.Checked = true;
                        this.rb_trailerExterno.Checked = false;
                    }
                    this.ddl_destinoZona.Enabled = true;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Se cargaron los datos del trailer seleccionado.');", true);
                }
            }
        }
        else
        {
            limpiar();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe ingresar un numero de flota o patente');", true);
            ddl_destinoZona.Enabled = false;
            this.hf_idTrailer.Value = "";
        }
    }

    protected void btn_confirmar_Click(object sender, EventArgs e)
    {
        MovimientoBC mov = new MovimientoBC();
        if (this.hf_idTrailer.Value == "" || this.hf_idTrailer.Value == null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Debe seleccionar un trailer.');", true);
        }
        else
        {
            mov.ID_TRAILER = int.Parse(this.hf_idTrailer.Value);
            mov.FECHA_CREACION = DateTime.Now;
            mov.OBSERVACION = "";
            mov.FECHA_ORIGEN = DateTime.Parse(this.txt_fechaMovimiento.Text);
            mov.FECHA_DESTINO = mov.FECHA_ORIGEN.AddMinutes(15);
            mov.ID_DESTINO = int.Parse(this.ddl_destinoPos.SelectedValue);
            mov.ID_ESTADO = 10;
            mov.OBSERVACION = this.txt_refOp.Text;
            mov.petroleo = null;
            TrailerUltEstadoBC trailerUE = new TrailerUltEstadoBC();
            trailerUE.ID = int.Parse(this.hf_idTrailer.Value);
            trailerUE.SITE_ID = int.Parse(this.dropsite.SelectedValue);    //   1; // Cambiar después de introducir variables de sesión
            trailerUE.SITE_IN = true;
            trailerUE.LUGAR_ID = int.Parse(this.ddl_destinoPos.SelectedValue);

            TrailerBC trailer = new TrailerBC();

            mov.MANT_EXTERNO = false;
            mov.ID_TRAILER = int.Parse(this.hf_idTrailer.Value);

            trailer.PLACA = this.txt_buscarPatente.Text;
            trailer.CODIGO = "S/ CODIGO";
            if (this.rb_trailerExterno.Checked)
            {
                trailer.EXTERNO = true;
            }
            else
            {
                trailer.EXTERNO = false;
            }
            trailer.NUMERO = this.txt_buscarNro.Text;
            trailer.ID = int.Parse(this.ddl_transportista.SelectedValue);
            UsuarioBC usuario = (UsuarioBC)Session["USUARIO"];
                 string resultado;
                bool ejecucion=mov.MOVIMIENTO(mov, trailerUE, trailer, usuario.ID, out resultado);
            if ( resultado=="" && ejecucion )
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "showAlert('Se ingresaron correctamente los datos.');", true);
                limpiar();
                ddl_destinoZona.SelectedValue = "0";
                ddl_destinoZona_SelectedIndexChanged(null, null);
                this.pnl_detalleLugar.Attributes.Remove("style");
                this.pnl_detalleTrailer.CssClass = "";
                this.pnl_detalleTrailer.Attributes.Remove("style");
                //this.img_reloj.ImageUrl = "";
                this.img_trailer.ImageUrl = "";
                this.lbl_lugar.Text = "";
                this.lbl_origenZona.Text = "";
                this.lbl_origenPlaya.Text = "";
                

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('"+ resultado +"');", true);
            }
        }
    }

    #endregion

    private void dibujo(int luga_id)
    {
        LugarBC lugar = new LugarBC();
        lugar = lugar.obtenerXID(luga_id);
        DataTable dt_lugares = lugar.obtenerLugarEstado(lugar.ID_PLAYA, luga_id, lugar.ID_SITE);

        DataRow row_lugar = dt_lugares.Rows[0];

        this.pnl_detalleLugar.Style.Add("background-color", row_lugar["COLOR_LUGAR"].ToString());
        this.pnl_detalleLugar.Style.Add("color", "black");
        this.lbl_lugar.Text = row_lugar["LUGA_COD"].ToString();
        this.pnl_detalleTrailer.Attributes.Add("style", string.Format("background-color:{0};", row_lugar["TRTI_COLOR"].ToString()));
        int estado_sol = int.Parse(row_lugar["SOES_ID"].ToString());
        this.pnl_imgAlerta.BackColor = System.Drawing.ColorTranslator.FromHtml(row_lugar["COLOR"].ToString());
        //if (estado_sol != 0)         //Si tiene carga en proceso o está cargado, a la espera de finalizar solicitud
        //{
        //    if (row_lugar["TRUE_CARGADO"].ToString() == "False")//Si está en carga
        //    {
        //        if (int.Parse(row_lugar["SOAN_MINS_CARGA_REAL"].ToString()) <= 120 &&
        //            int.Parse(row_lugar["SOAN_MINS_CARGA_REAL"].ToString()) >= 0)
        //        {
        //            this.img_reloj.ImageUrl = "../Img/relojverde.png";
        //        }
        //        else if (int.Parse(row_lugar["SOAN_MINS_CARGA_REAL"].ToString()) <= 180 &&
        //                 int.Parse(row_lugar["SOAN_MINS_CARGA_REAL"].ToString()) >= 121)
        //        {
        //            this.img_reloj.ImageUrl = "../Img/relojnaranja.png";
        //        }
        //        else if (int.Parse(row_lugar["SOAN_MINS_CARGA_REAL"].ToString()) >= 181)
        //        {
        //            this.img_reloj.ImageUrl = "../Img/relojrojo.png";
        //        }
        //    }
        //    else
        //    {
        //        if (int.Parse(row_lugar["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) <= 15 &&
        //            int.Parse(row_lugar["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) >= 0)
        //        {
        //            this.img_reloj.ImageUrl = "../Img/relojverde.png";
        //        }
        //        else if (int.Parse(row_lugar["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) <= 30 &&
        //                 int.Parse(row_lugar["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) >= 16)
        //        {
        //            this.img_reloj.ImageUrl = "../Img/relojnaranja.png";
        //        }
        //        else if (int.Parse(row_lugar["MOVI_MINS_ASIGNA_EJECUTA"].ToString()) >= 31)
        //        {
        //            this.img_reloj.ImageUrl = "../Img/relojrojo.png";
        //        }
        //    }
        //}
        if (row_lugar["tres_icono"].ToString() != "")
            img_trailer.ImageUrl = row_lugar["tres_icono"].ToString();
        //if (row_lugar["TRAI_PLACA"].ToString() != "0")
        //{
        //    //strb.Append("<img ");
        //    if (estado_sol >= 0 && estado_sol < 20)
        //    {
        //        this.img_trailer.ImageUrl = "../Img/tra_vacio.png";
        //    }
        //    else if (estado_sol >= 20 && estado_sol < 30)
        //    {
        //        this.img_trailer.ImageUrl = "../Img/tra_semivacio.png";
        //    }
        //    else if (estado_sol == 30)
        //    {
        //        this.img_trailer.ImageUrl = "../Img/tra_ocupado.png";
        //    }
        //}
        this.lbl_origenZona.Text = lugar.ZONA;
        this.lbl_origenPlaya.Text = lugar.PLAYA;

    }

    private void limpiar()
    {
        this.hf_idTrailer.Value = "";
        this.ddl_transportista.ClearSelection();
        this.ddl_destinoZona.Enabled = false;
        this.ddl_destinoPos.Enabled = false;
        this.ddl_destinoPlaya.Enabled = false;
        this.ddl_destinoPos.ClearSelection();
        this.ddl_destinoPlaya.ClearSelection();
        this.ddl_destinoZona.ClearSelection();
        this.txt_buscarPatente.Text = "";
        this.txt_buscarNro.Text = "";
        this.pnl_lugarOrigen.Attributes.Remove("style");
        this.pnl_detalleLugar.Attributes.Remove("style");
        this.pnl_detalleTrailer.Attributes.Remove("style");
        this.img_trailer.ImageUrl = "";
        //this.img_reloj.ImageUrl = "";
        this.lbl_lugar.Text = "";
        lbl_origenZona.Text = "";
        lbl_origenPlaya.Text = "";
    }
}