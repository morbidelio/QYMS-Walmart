using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_Limpiar_Solicitudes : System.Web.UI.Page
{
    UsuarioBC user = new UsuarioBC();
    public void gv_listar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "BORRAR")
        {
            SolicitudBC s = new SolicitudBC();
            s.SOLI_ID = Convert.ToInt32(e.CommandArgument);
            if (s.EliminarInconsistentes(s.SOLI_ID))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Solicitud eliminada');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "msj", "alert('Error');", true);
            }
            ObtenerSolicitudes();
        }
    }

    public void btn_limpiar_Click(object sender, EventArgs e)
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["usuario"] == null)
        {
            Response.Redirect("~/InicioQYMS2.aspx");
        }
        user = (UsuarioBC)Session["usuario"];
        ObtenerSolicitudes();
    }

    private void ObtenerSolicitudes()
    {
        SolicitudBC s = new SolicitudBC();
        gv_listar.DataSource = s.ObtenerInconsistentes();
        gv_listar.DataBind();
    }
}