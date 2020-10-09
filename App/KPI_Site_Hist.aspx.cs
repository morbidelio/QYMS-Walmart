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
            txt_desde.Text = DateTime.Now.AddDays(-7).ToShortDateString();
            txt_hasta.Text = DateTime.Now.ToShortDateString();
            DataTable dt;
            dt = new YMS_ZONA_BC().ObteneSites(user.ID);
            utils.CargaDropNormal(ddl_site, "ID", "NOMBRE", dt);
            ObtenerReporte();
        }
    }
    private void ObtenerReporte()
    {
        int site_id = Convert.ToInt32(ddl_site.SelectedValue);
        DateTime desde = Convert.ToDateTime(txt_desde.Text);
        DateTime hasta = Convert.ToDateTime(txt_hasta.Text);
        DataTable dt = new ReportBC().Reporte_KPIHist(site_id, desde, hasta);
        string tiempo_promedio_anden_carga = dt.Rows[0]["TIEMPO_PROMEDIO_ANDEN_CARGA"].ToString();
        string tiempo_max_anden_carga = dt.Rows[0]["TIEMPO_MAXIMO_ANDEN_CARGA"].ToString();
        string tiempo_min_anden_carga = dt.Rows[0]["TIEMPO_MINIMO_ANDEN_CARGA"].ToString();
        string tiempo_promedio_anden_descarga = dt.Rows[0]["TIEMPO_PROMEDIO_ANDEN_DESCARGA"].ToString();
        string tiempo_max_anden_descarga = dt.Rows[0]["TIEMPO_MAXIMO_ANDEN_DESCARGA"].ToString();
        string tiempo_min_anden_descarga = dt.Rows[0]["TIEMPO_MINIMO_ANDEN_DESCARGA"].ToString();
        lbl_tiempoPromedioCarga.Text = tiempo_promedio_anden_carga.ToString();
        lbl_tiempoMaxCarga.Text = tiempo_max_anden_carga.ToString();
        lbl_tiempoMinCarga.Text = tiempo_min_anden_carga.ToString();
        lbl_tiempoPromedioDescarga.Text = tiempo_promedio_anden_descarga.ToString();
        lbl_tiempoMaxDescarga.Text = tiempo_max_anden_descarga.ToString();
        lbl_tiempoMinDescarga.Text = tiempo_min_anden_descarga.ToString();
    }

    protected void ddl_site_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerReporte();
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerReporte();
    }
}