using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cambiar_Estado_Trailer : System.Web.UI.Page
{
    UtilsWeb u = new UtilsWeb();
    public void site_indexChanged(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        u.CargaDropTodos(ddl_trailer, "ID", "PLACA", t.obtenerXSite(Convert.ToInt32(ddl_site.SelectedValue)));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaDrops c = new CargaDrops();
            c.Site_Normal(ddl_site);
            site_indexChanged(null, null);
        }
    }
}