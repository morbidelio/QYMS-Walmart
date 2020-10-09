using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_KPI_Site : System.Web.UI.Page
{
    UtilsWeb utils = new UtilsWeb();
    UsuarioBC user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
            Response.Redirect("~/InicioQYMS2.aspx");
        user = (UsuarioBC)Session["usuario"];
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt = new YMS_ZONA_BC().ObteneSites(user.ID);
            utils.CargaDropNormal(ddl_site, "ID", "NOMBRE", dt);
            ObtenerReporte();
        }
    }
    private void ObtenerReporte()
    {
        int site_id = Convert.ToInt32(ddl_site.SelectedValue);
        DataTable dt = new ReportBC().Reporte_KPI(site_id);
        int trailer_total = Convert.ToInt32(dt.Rows[0]["TRAILER_TOTAL"]);
        int trailer_vacios = Convert.ToInt32(dt.Rows[0]["TRAILER_VACIOS"]);
        int trailer_cargados = Convert.ToInt32(dt.Rows[0]["TRAILER_CARGADOS"]);
        int andenes_vacios = Convert.ToInt32(dt.Rows[0]["ANDENES_VACIOS"]);
        int andenes_ocupados = Convert.ToInt32(dt.Rows[0]["ANDENES_OCUPADOS"]);
        string tiempo_promedio_anden_carga = dt.Rows[0]["TIEMPO_PROMEDIO_ANDEN_CARGA"].ToString();
        string tiempo_max_anden_carga = dt.Rows[0]["TIEMPO_MAXIMO_ANDEN_CARGA"].ToString();
        string tiempo_min_anden_carga = dt.Rows[0]["TIEMPO_MINIMO_ANDEN_CARGA"].ToString();
        string tiempo_promedio_anden_descarga = dt.Rows[0]["TIEMPO_PROMEDIO_ANDEN_DESCARGA"].ToString();
        string tiempo_max_anden_descarga = dt.Rows[0]["TIEMPO_MAXIMO_ANDEN_DESCARGA"].ToString();
        string tiempo_min_anden_descarga = dt.Rows[0]["TIEMPO_MINIMO_ANDEN_DESCARGA"].ToString();
        string tiempo_estacionamiento = dt.Rows[0]["TIEMPO_ESTACIONAMIENTO"].ToString();
        string cantidad_estacionamiento = dt.Rows[0]["cantidad_estacionamiento"].ToString();
        string cantidad_anden_cargado = dt.Rows[0]["cantidad_anden_cargado"].ToString();
        string cantidad_anden_descargado = dt.Rows[0]["cantidad_anden_vacio"].ToString();
        string script = string.Format("cargaGauge({1},{0});", andenes_ocupados, andenes_vacios);
        string cantidad_estacionamiento_cargado = dt.Rows[0]["cantidad_estacionamiento_cargado"].ToString();
        string cantidad_estacionamiento_vacio = dt.Rows[0]["cantidad_estacionamiento_vacio"].ToString();
        lbl_trailerVacio.Text = trailer_vacios.ToString();
        lbl_trailerCargado.Text = trailer_cargados.ToString();
        lbl_tiempoPromedioCarga.Text = tiempo_promedio_anden_carga.ToString();
        lbl_tiempoMaxCarga.Text = tiempo_max_anden_carga.ToString();
        lbl_tiempoMinCarga.Text = tiempo_min_anden_carga.ToString();
        lbl_tiempoPromedioDescarga.Text = tiempo_promedio_anden_descarga.ToString();
        lbl_tiempoMaxDescarga.Text = tiempo_max_anden_descarga.ToString();
        lbl_tiempoMinDescarga.Text = tiempo_min_anden_descarga.ToString();
        lbl_tiempoPromedioEstacionamiento.Text = tiempo_estacionamiento.ToString();
        lbl_cantidadEstacionamiento.Text = cantidad_estacionamiento.ToString();
        lbl_cantidad_cargado.Text = cantidad_anden_cargado;
        lbl_cantidad_vacio.Text = cantidad_anden_descargado;
        lbl_cantidadEstacionamiento_cargado.Text = cantidad_estacionamiento_cargado;
        lbl_cantidadEstacionamiento_vacio.Text = cantidad_estacionamiento_vacio;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "gauge", script, true);
    }

    protected void ddl_site_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerReporte();
    }
    protected void recargaplayas(object sender, EventArgs e)
    {

        ObtenerReporte();

    }
}