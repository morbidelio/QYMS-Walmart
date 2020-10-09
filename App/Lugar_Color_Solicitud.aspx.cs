using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Lugar_Color_Solicitud : System.Web.UI.Page
{
    Panel panel_temp;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //SiteBC s = new SiteBC();
            //DataTable dt = s.ObtenerTodos();
            //ltl_menu.Text = crearMenu(dt);
            ObtenerEstados(true);
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["sessionsites"] != null)
        {
            panel_temp = (Panel)Session["sessionsites"];
            pnl_Sites.Controls.Add(panel_temp);
        }
    }

    #region Button

    protected void btn_grabar_Click(object sender, EventArgs e)
    {
        SiteBC s = new SiteBC();
        DataTable dt = s.ObtenerTodos();
        DataTable dt2 = new DataTable();
        dt2.Columns.Add("SITE_ID");
        dt2.Columns.Add("COLOR");
        bool exito = true;
        foreach (DataRow dr in dt.Rows)
        {
            TextBox txtsite = (TextBox)pnl_Sites.FindControl("txt_colorSite_" + dr["ID"].ToString());
            if (string.IsNullOrEmpty(txtsite.Text))
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", "alert('Debe completar todos los datos');", true);
                exito = false;
                break;
            }
            else
            {
                dt2.Rows.Add(dr["ID"].ToString(), txtsite.Text);
            }
        }
        if (exito)
        {
            SolicitudBC sol = new SolicitudBC();
            if (sol.EditarColorEstadoSite(dt2, int.Parse(hf_id.Value)))
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ok", "alert('Todo OK');", true);
            else
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", "alert('Error');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", "alert('Debe completar todos los datos');", true);
        //DropDownList ddlzona = (DropDownList)panel_temp.FindControl(dr["SITE_ID"].ToString() + "ZONA__DDL");    // new DropDownList();
    }

    #endregion

    #region GridView

    protected void gv_listar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager && (gv_listar.DataSource != null))
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = gv_listar.PageCount.ToString();

            //LLENA LA LISTA CON EL NUMERO DE PAGINAS
            DropDownList list = (DropDownList)e.Row.FindControl("paginasDropDownList");
            for (int i = 1; i <= Convert.ToInt32(gv_listar.PageCount); i++)
            {
                list.Items.Add(i.ToString());
            }
            list.SelectedValue = Convert.ToString(gv_listar.PageIndex + 1);
        }
    }

    protected void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EDITAR")
        {
            hf_id.Value = e.CommandArgument.ToString();
            int soes_id = int.Parse(hf_id.Value);
            SolicitudBC s = new SolicitudBC();
            DataTable dt = s.ObtenerColorEstadoSolicitud(soes_id);
            //ltl_color.Text = crearContenido(dt);
            crearContenido(dt);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "modal", "modalColor();", true);
        }
    }

    protected void gv_listar_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (e.NewPageIndex >= 0)
        {
            gv_listar.PageIndex = e.NewPageIndex;
        }
        ObtenerEstados(false);
    }
    
    protected void GoPage(object sender, System.EventArgs e)
    {
        DropDownList oIraPag = (DropDownList)sender;
        int iNumPag = 0;
        if (int.TryParse((oIraPag.Text), out iNumPag) && iNumPag > 0 && iNumPag <= gv_listar.PageCount)
        {
            if (int.TryParse(oIraPag.Text, out iNumPag) && iNumPag > 0 && iNumPag <= gv_listar.PageCount)
            {
                gv_listar.PageIndex = iNumPag - 1;
            }
            else
            {
                gv_listar.PageIndex = 0;
            }
        }
        ObtenerEstados(false);
    }

    #endregion

    #region UtilsPagina

    internal void crearContenido(DataTable dt)
    {
        //StringBuilder strb = new StringBuilder();
        //strb.Append("<div class='tab-content'>");


        if (pnl_Sites.FindControl("paneltemp") == null)
        {
            panel_temp = new Panel();
            panel_temp.ID = "paneltemp";
            panel_temp.EnableViewState = true;
            pnl_Sites.Controls.Add(panel_temp);
        }
        else
        {
            panel_temp = (Panel)pnl_Sites.FindControl("paneltemp");
            panel_temp.Controls.Clear();
        }
        Session["sessionsites"] = panel_temp;


        string site, site_id, color;
        foreach (DataRow dr in dt.Rows)
        {
            site_id = dr["SITE_ID"].ToString();
            site = dr["SITE"].ToString();
            color = dr["COLOR"].ToString();
            Panel pnlsite = new Panel();
            pnlsite.ID = "cont_site_" + site_id;
            pnlsite.CssClass = "col-xs-2";
            Label lblsite = new Label();
            lblsite.Text = "<h5>" + site + "</h5>";
            //Panel pnltexto = new Panel();
            //pnltexto.CssClass = "col-xs-6";
            TextBox txtsite = new TextBox();
            txtsite.ID = "txt_colorSite_" + site_id;
            txtsite.Text = color;
            txtsite.CssClass = "color form-control";
            //txtsite.ReadOnly = true;
            txtsite.ClientIDMode = ClientIDMode.Static;
            //Panel pnlcolor = new Panel();
            //pnlcolor.ID = "pnl_colorSite_" + site_id;
            //pnlcolor.CssClass = "col-xs-6 form-control";
            //pnlcolor.Attributes.Add("style", "width:50%;border:inherit;");
            //pnlcolor.ClientIDMode = ClientIDMode.Static;
            //pnltexto.Controls.Add(txtsite);
            pnlsite.Controls.Add(lblsite);
            pnlsite.Controls.Add(txtsite);
            //pnlsite.Controls.Add(pnlcolor);
            panel_temp.Controls.Add(pnlsite);

            //strb.AppendFormat("<div id='cont_site_{0}' class='col-xs-3'>", site_id).
            //    AppendFormat("<h4>{0}</h4>", site).
            //    AppendFormat("<input id='txt_colorSite_{0}' type='text' class='color form-control' value='{1}'></input>", site_id, color).
            //    Append("</div>");
        }

        //strb.Append("</div>");
        //return strb.ToString();
    }

    private void ObtenerEstados(bool forzarBD)
    {
        DataTable dt;
        if (ViewState["listar"] == null || forzarBD)
        {
            SolicitudBC s = new SolicitudBC();
            ViewState["listar"] = s.ObtenerEstados();
        }
        dt = (DataTable)ViewState["listar"];
        gv_listar.DataSource = dt;
        gv_listar.DataBind();
    }

    #endregion
}