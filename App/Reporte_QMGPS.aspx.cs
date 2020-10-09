// Example header text. Can be configured in the options.
using System;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq;
using System.Web.UI;

public partial class App_Reporte_QMGPS : System.Web.UI.Page
{
    UsuarioBC usuario = new UsuarioBC();
    UtilsWeb utils = new UtilsWeb();

    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        this.ObtenerReporte(true);
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (this.usuario.numero_sites < 2)
        {
            this.SITE.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["Usuario"] == null)
        {
            this.Response.Redirect("~/InicioQYMS2.aspx");
        }
        this.usuario = (UsuarioBC)this.Session["Usuario"];

        if (!this.IsPostBack)
        {
            TrailerBC t = new TrailerBC();
            YMS_ZONA_BC y = new YMS_ZONA_BC();
            this.utils.CargaDropNormal(this.ddl_buscarSite, "ID", "NOMBRE", y.ObteneSites(this.usuario.ID));
            this.ObtenerReporte(true);
        }
        else
        {
            this.ObtenerReporte(false);
        }
    }

    protected void ddl_buscarSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ObtenerReporte(true);
    }

    private void ObtenerReporte(bool forzarBD)
    {
        if (this.ViewState["listar"] == null || forzarBD)
        {
            TrailerUltEstadoBC t = new TrailerUltEstadoBC();
            t.NUMERO = txt_buscarNro.Text;
            t.PLACA = txt_buscarPlaca.Text;
            t.SITE_ID = Convert.ToInt32(this.ddl_buscarSite.SelectedValue);
            t.SITE_IN = !this.chk_sitein.Checked;
            this.ViewState["listar"] = this.ComparaRegistros(t.CargaUltEstadoQMGPS());
        }
        this.gv_listar.DataSource = this.ViewState["listar"];
        this.gv_listar.DataBind();
    }

    private DataTable ComparaRegistros(DataSet ds)
    {
        DataTable dt1 = ds.Tables[0];
        DataTable dt2 = ds.Tables[1];
        var results = from table1 in dt1.AsEnumerable()
                      join table2 in dt2.AsEnumerable() 
                      on table1.Field<string>("PLACA") equals table2.Field<string>("PLACA")
                      select new
                      {
                          Placa = table1["PLACA"].ToString(),
                          Numero = table1["NRO_FLOTA"].ToString(),
                          SiteIn = (bool)table1["TRUE_SITE_IN"],
                          FechaIngreso = Convert.ToDateTime(table1["FECHA_INGRESO"]),
                          FechaUltReporte = Convert.ToDateTime(table2["FH_ULT_REPORTE"])
                      };
        DataTable dt = new DataTable();
        dt.Columns.Add("PLACA");
        dt.Columns.Add("NRO_FLOTA");
        dt.Columns.Add("Site In");
        dt.Columns.Add("FH YMS");
        dt.Columns.Add("FH QMGPS");
        foreach (var row in results)
        {
            //dt.Rows.Add(row.Placa, row.Placa2, row.SiteIn, row.FechaIngreso, row.FechaUltReporte);
            dt.Rows.Add(row.Placa, row.Numero, row.SiteIn, row.FechaIngreso, row.FechaUltReporte);
        }
        return dt;
    }

    // viewstate
    protected override void SavePageStateToPersistenceMedium(object state)
    {
        string file = this.GenerateFileName();

        FileStream filestream = new FileStream(file, FileMode.Create);

        LosFormatter formator = new LosFormatter();

        formator.Serialize(filestream, state);

        filestream.Flush();

        filestream.Close();

        filestream = null;
    }

    protected override object LoadPageStateFromPersistenceMedium()
    {
        object state = null;
        try
        {
            StreamReader reader = new StreamReader(this.GenerateFileName());

            LosFormatter formator = new LosFormatter();

            state = formator.Deserialize(reader);

            reader.Close();
        }
        catch (Exception)
        {
            this.Response.Redirect(string.Format("{0}.aspx", Path.GetFileNameWithoutExtension(this.Page.AppRelativeVirtualPath)));
        }
        return state;
    }

    private string GenerateFileName()
    {
        string pageName = Path.GetFileNameWithoutExtension(this.Page.AppRelativeVirtualPath);

        string file = string.Format("{0}{1}.txt", pageName, this.Session.SessionID.ToString());

        //       file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);  
        file = string.Format("{0}\\{1}", this.utils.pathviewstate(), file);

        return file;
    }
}