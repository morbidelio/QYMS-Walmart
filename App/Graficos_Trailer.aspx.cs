using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class App_Graficos_Trailer : System.Web.UI.Page
{
    //protected void Page_PreLoad(object sender, EventArgs e)
    //{

    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(hf_html.Value))
        ltl_tablas.Text = CreaTablas();
        //else
        //    ltl_tablas.Text = hf_html.Value;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "grafico", hf_script.Value, true);
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "grafico", "graficoprueba();", true);
        //CreaGrafico();
    }

    //private void CreaGrafico()
    //{
    //    string[] script = (string[])ViewState["script"];
    //    int cont = 0;
    //    DataTable dt = (DataTable)ViewState["grafico"];
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        cont++;
    //        ClientScript.RegisterStartupScript(this.GetType(), "grafico_" + dr["SITE_ID"].ToString(), script[cont], true);
    //        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "grafico_" + dr["SITE_ID"].ToString(), script , true);
    //    }
    //}

    private string CreaTablas()
    {
        DataSet ds = new DataSet();
        TrailerBC t = new TrailerBC();
        ds = t.CargaGrafico();
        StringBuilder sb = new StringBuilder();
        DataTable dtSites = ds.Tables[0];
        ViewState["grafico"] = dtSites;
        int cont = 0;
        foreach (DataRow dr in dtSites.Rows)
        {
            sb.Append("<div class='col-xs-3'>").
                Append("<div class='col-xs-12'>").
                Append("<center>").
                Append("<h4>").
                Append(dr["SITE_DESC"].ToString()).
                Append("</h4>").
                Append("</center>");
            DataView dwSite = ds.Tables[1].AsDataView();
            dwSite.RowFilter = "SITE_ID = " + dr["SITE_ID"].ToString();
            DataTable dtSite = dwSite.ToTable();
            sb.Append(CreaTablaSite(dtSite));
            string datos = DatosGrafico(dtSite);
            string labels = LabelsGrafico(dtSite);
            string colores = ColoresGrafico(dtSite);
            sb.Append("</div>").
                Append("<div class='col-xs-12 separador'>").
                Append("</div>").
                Append("<div class='col-xs-12'>").
                Append("<canvas id='pieChart_").
                Append(dr["SITE_ID"].ToString()).
                //Append("' width='200' height='200'>").
                Append("' role='img'></canvas>").
                Append("</div>").
                Append("</div>");
            hf_script.Value += "cargaGrafico(" + dr["SITE_ID"].ToString() + "," + datos + "," + labels + "," + colores + ");";
            cont++;
        }
        DataTable dtRuta = ds.Tables[2];
        lbl_ruta.Text = dtRuta.Rows[0]["CANTIDAD"].ToString();
        cont++;
        //ViewState["script"] = script;
        hf_html.Value = sb.ToString();
        return sb.ToString();
    }

    private string CreaTablaSite(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<small>").
            Append("<table style='width:100%' class='table-hover'>").
            Append("<tr>").
            Append("<th>").
            Append("Estado Trailer").
            Append("</th>").
            Append("<th>").
            Append("Cantidad").
            Append("</th>").
            Append("</tr>");
        foreach (DataRow dr in dt.Rows)
        {
            sb.Append("<tr>").
                Append("<td").
                Append(" style='color:" + dr["TRES_COLOR"].ToString() + "'>").
                Append(dr["TRES_DESC"].ToString()).
                Append("</td>").
                Append("<td").
                Append(" style='color:" + dr["TRES_COLOR"].ToString() + "'>").
                Append(dr["CANTIDAD"].ToString()).
                Append("</td>").
                Append("</tr>");
        }
        sb.Append("</table>").
            Append("</small>");
        return sb.ToString();
    }

    private string LabelsGrafico(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        bool primero = true;
        sb.Append("[");
        foreach (DataRow dr in dt.Rows)
        {
            if (!primero)
                sb.Append(",");
            else
                primero = false;
            sb.Append("'").
                Append(dr["TRES_DESC"].ToString()).
                Append("'");
        }
        sb.Append("]");
        return sb.ToString();
    }

    private string DatosGrafico(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        bool primero = true;
        sb.Append("[");
        foreach (DataRow dr in dt.Rows)
        {
            if (!primero)
                sb.Append(",");
            else
                primero = false;
            sb.Append("'").
                Append(dr["CANTIDAD"].ToString()).
                Append("'");
        }
        sb.Append("]");
        return sb.ToString();
    }

    private string ColoresGrafico(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        bool primero = true;
        sb.Append("[");
        foreach (DataRow dr in dt.Rows)
        {
            if (!primero)
                sb.Append(",");
            else
                primero = false;
            sb.Append("'").
                Append(dr["TRES_COLOR"].ToString()).
                Append("'");
        }
        sb.Append("]");
        return sb.ToString();
    }
}