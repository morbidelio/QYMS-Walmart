using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class App_Trailer_A_Tracto : System.Web.UI.Page
{
    protected void btn_asociar_Click(object sender, EventArgs e)
    {
        if (this.txt_placa.Text != "")
        {
            TrailerBC t = new TrailerBC();
            string resultado;
            if (t.A_Tracto(this.txt_placa.Text, out resultado))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('acción exitosa');", true);

                //ObtenerTrailer(true);
                //btn_buscar1_click(null, null);
                //btn_asociar.Visible = false;
                btn_buscar_Click(null, null);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", string.Format("alert('{0}');", resultado), true);
            }
        }
        else

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", string.Format("alert('{0}');", "Ingrese placa"), true);
   

    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        btn_asociar.Visible = false;
        if (this.txt_placa.Text != "")
        {
            ObtenerTrailer(true);
            btn_buscar1_click(null, null);
            if (gv_listar.Rows.Count==1 )
                btn_asociar.Visible = true;
        }
        else

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", string.Format("alert('{0}');", "Ingrese placa"), true);
   


    }
    private void ObtenerTrailer(bool forzarBD)
    {
        if (this.ViewState["lista"] == null || forzarBD)
        {
            TrailerBC trailer = new TrailerBC();
            DataTable dt = trailer.obtenerXParametro(this.txt_placa.Text, "", false, 0, 0);
            this.ViewState["lista"] = dt;
        }
        DataView dw = new DataView((DataTable)this.ViewState["lista"]);
        if (this.ViewState["sortExpresion"] != null && this.ViewState["sortExpresion"].ToString() != "")
        {
            dw.Sort = (String)this.ViewState["sortExpresion"];
        }
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }
    public void btn_buscar1_click(object sender, EventArgs e)
    {
        string filtros = "";
        bool primero = true;

        if (!string.IsNullOrEmpty(txt_placa.Text))
        {
            filtros += "PATENTE LIKE '%" + txt_placa.Text + "%' ";
        }

        if (string.IsNullOrEmpty(filtros) || ViewState["filtradostr"] == null)
        {
            ObtenerTracto(true);
        }

        if (!string.IsNullOrEmpty(filtros))
        {

            DataView dw = new DataView((DataTable)ViewState["listaTR"]);
            dw.RowFilter = filtros;
            ViewState["filtradostr"] = dw.ToTable();
            ObtenerTracto(false);
        }
    }
    private void ObtenerTracto(bool forzarBD)
    {
        if (ViewState["listaTR"] == null || forzarBD)
        {
            TractoBC t = new TractoBC();
            DataTable dt = t.ObtenerTodos();
            ViewState["listaTR"] = dt;
            ViewState.Remove("filtradostr");
        }

        DataView dw;
        if (ViewState["filtradostr"] == null)
            dw = new DataView((DataTable)ViewState["listaTR"]);
        else
            dw = new DataView((DataTable)ViewState["filtradostr"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.GridView1.DataSource = dw;
        this.GridView1.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}