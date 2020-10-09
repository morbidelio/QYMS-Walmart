using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Lavado : System.Web.UI.Page
{
    private UsuarioBC user = new UsuarioBC();
    private static UtilsWeb utils = new UtilsWeb();
    private static CargaDrops drop = new CargaDrops();

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        ObtenerTrailer(true);
    }

    protected void gv_listar_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
            Response.Redirect("~/Inicio_QYMS2.aspx");
        user = (UsuarioBC)Session["Usuario"];
        if (!IsPostBack)
        {
            drop.Site_Normal(ddl_buscarSite,user.ID);
            txt_buscarDesde.Text = DateTime.Now.AddDays(-1).ToShortDateString();
            txt_buscarHasta.Text = DateTime.Now.ToShortDateString();
            ObtenerTrailer(true);
        }
    }

    private void ObtenerTrailer(bool forzarBD)
    {
        if (ViewState["lista"] == null || forzarBD)
        {
            TrailerBC t = new TrailerBC();
            ViewState["lista"] = t.ObtenerUltLavado(Convert.ToInt32(ddl_buscarSite.SelectedValue),DateTime.MinValue,DateTime.MinValue);
        }
        DataView dw = new DataView((DataTable)ViewState["lista"]);
        if (ViewState["sortExpresion"] != null && ViewState["sortExpresion"].ToString() != "")
            dw.Sort = (String)ViewState["sortExpresion"];
        this.gv_listar.DataSource = dw;
        this.gv_listar.DataBind();
    }
}