using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Descripción breve de CargaDrops
/// </summary>
public class CargaDrops
{
    UtilsWeb u = new UtilsWeb();
    DataTable dt;

    #region Lugar
    LugarBC l = new LugarBC();

    public void Lugar_Todos(System.Web.UI.WebControls.DropDownList ddl, int site_id = 0, int play_id = 0, int ocupados = -1, int lues_id = -1)
    {
        UtilsWeb u = new UtilsWeb();
        DataTable dt;
        if (play_id != 0)
            dt = l.ObtenerXPlaya(play_id, ocupados, lues_id);
        else if (site_id != 0)
            dt = l.ObtenerXSite(site_id, ocupados, lues_id);
        else
            dt = l.ObtenerTodos1(ocupados, lues_id);
        DataView dw = dt.AsDataView();
        dw.Sort = "DESCRIPCION ASC";
        u.CargaDropTodos(ddl, "ID", "DESCRIPCION", dw.ToTable());
    }

    public void Lugar_Normal1(System.Web.UI.WebControls.DropDownList ddl, int site_id = 0, int play_id = 0, int ocupados = -1, int lues_id = -1)
    {
        UtilsWeb u = new UtilsWeb();
        DataTable dt;
        if (play_id != 0)
            dt = l.ObtenerXPlaya(play_id, ocupados, lues_id);
        else if (site_id != 0)
            dt = l.ObtenerXSite(site_id, ocupados, lues_id);
        else
            dt = l.ObtenerTodos1(ocupados, lues_id);
        DataView dw = dt.AsDataView();
        dw.Sort = "DESCRIPCION ASC";
        u.CargaDropNormal(ddl, "ID", "DESCRIPCION", dw.ToTable());
    }

 


    public void Lugar1(System.Web.UI.WebControls.DropDownList ddl, int site_id = 0, int play_id = 0, int ocupados = -1, int lues_id = -1, int trai_id=-1, int soli_tipo=0)
    {
        UtilsWeb u = new UtilsWeb();
        DataTable dt;
        if (soli_tipo!=0)
            dt = l.ObtenerXsolicitud(site_id,play_id, ocupados, lues_id, trai_id,soli_tipo);
        else if (play_id != 0)
            dt = l.ObtenerXPlaya(play_id, ocupados, lues_id, trai_id);
        else if (site_id != 0)
            dt = l.ObtenerXSite(site_id, ocupados, lues_id);
        else
            dt = l.ObtenerTodos1(ocupados, lues_id);
        DataView dw = dt.AsDataView();
        dw.Sort = "DESCRIPCION ASC";
        u.CargaDrop(ddl, "ID", "DESCRIPCION", dw.ToTable());
    }

    #endregion

    #region Playa
    PlayaBC p = new PlayaBC();

    public void Playa_Todos(System.Web.UI.WebControls.DropDownList ddl, int zona_id = 0, int site_id = 0)
    {
        UtilsWeb u = new UtilsWeb();
        DataTable dt;
        if (zona_id != 0)
            dt = p.ObtenerXZona(zona_id);
        else if (site_id != 0)
            dt = p.ObtenerXSite(site_id);
        else
            dt = p.ObtenerTodas();
        DataView dw = dt.AsDataView();
        dw.Sort = "DESCRIPCION ASC";
        u.CargaDropTodos(ddl, "ID", "DESCRIPCION", dw.ToTable());
    }

    public void Playa_Normal(System.Web.UI.WebControls.DropDownList ddl, int zona_id = 0, int site_id = 0)
    {
        if (zona_id != 0)
            dt = p.ObtenerXZona(zona_id);
        else if (site_id != 0)
            dt = p.ObtenerXSite(site_id);
        else
            dt = p.ObtenerTodas();
        DataView dw = dt.AsDataView();
        dw.Sort = "DESCRIPCION ASC";
        u.CargaDropNormal(ddl, "ID", "DESCRIPCION", dw.ToTable());
    }

    public void Playa(System.Web.UI.WebControls.DropDownList ddl, int zona_id = 0, int site_id = 0)
    {
        if (zona_id != 0)
            dt = p.ObtenerXZona(zona_id);
        else if (site_id != 0)
            dt = p.ObtenerXSite(site_id);
        else
            dt = p.ObtenerTodas();
        DataView dw = dt.AsDataView();
        dw.Sort = "DESCRIPCION ASC";
        u.CargaDrop(ddl, "ID", "DESCRIPCION", dw.ToTable());
    }

    #endregion

    #region Site
    SiteBC s = new SiteBC();

    public void Site_Todos(DropDownList ddl, int user_id = 0)
    {
        DataView dw;
        if (user_id != 0)
        {
            YMS_ZONA_BC y = new YMS_ZONA_BC();
            dw = y.ObteneSites(user_id).AsDataView();
        }
        else
        {
            SiteBC s = new SiteBC();
            dw = s.ObtenerTodos().AsDataView();
        }
        dw.Sort = "NOMBRE ASC";
        u.CargaDropTodos(ddl, "ID", "NOMBRE", dw.ToTable());
    }

    public void Site_Normal(DropDownList ddl, int user_id = 0)
    {
        DataView dw;
        if (user_id != 0)
        {
            YMS_ZONA_BC y = new YMS_ZONA_BC();
            dw = y.ObteneSites(user_id).AsDataView();
        }
        else
        {
            SiteBC s = new SiteBC();
            dw = s.ObtenerTodos().AsDataView();
        }
//        dw.Sort = "NOMBRE ASC";
        u.CargaDropNormal(ddl, "ID", "NOMBRE", dw.ToTable());
    }

    public void Site(DropDownList ddl, int user_id = 0)
    {
        DataView dw;
        if (user_id != 0)
        {
            YMS_ZONA_BC y = new YMS_ZONA_BC();
            dw = y.ObteneSites(user_id).AsDataView();
        }
        else
        {
            SiteBC s = new SiteBC();
            dw = s.ObtenerTodos().AsDataView();
        }
        dw.Sort = "NOMBRE ASC";
        u.CargaDrop(ddl, "ID", "NOMBRE", dw.ToTable());
    }

    #endregion

    #region User
    readonly UsuarioBC usuario = new UsuarioBC();

    public void Usuario_Todos(DropDownList ddl, int site_id = 0, bool remolcador = false, bool guardia = false)
    {
        if (site_id > 0)
            dt = usuario.ObtenerXSite(site_id);
        else if (remolcador)
            dt = usuario.ObtenerUsuariosRemolcador(site_id);
        else if (guardia)
            dt = usuario.ObtenerUsuariosGuardia(site_id);
        else
            dt = usuario.ObtenerTodos();
        DataView dw = dt.AsDataView();
        dw.Sort = "USERNAME ASC";
        u.CargaDropTodos(ddl, "ID", "USERNAME", dw.ToTable());
    }

    public void Usuario_Normal(DropDownList ddl, int site_id = -1, bool remolcador = false, bool guardia = false)
    {
        if (site_id > 0)
            dt = usuario.ObtenerXSite(site_id);
        else if (remolcador)
            dt = usuario.ObtenerUsuariosRemolcador();
        else if (guardia)
            dt = usuario.ObtenerUsuariosGuardia();
        else
            dt = usuario.ObtenerTodos();
        DataView dw = dt.AsDataView();
        dw.Sort = "USERNAME ASC";
        u.CargaDropNormal(ddl, "ID", "USERNAME", dw.ToTable());
    }

    public void Usuario(DropDownList ddl, int site_id = -1, bool remolcador = false, bool guardia = false)
    {

        int tipo=0;
        if (remolcador==true)
            tipo=1020;
        else if (guardia==true)
            tipo=15;

        if (site_id > 0)
        //    dt = usuario.ObtenerXSite(site_id);
        //else if (remolcador)
        //    dt = usuario.ObtenerUsuariosRemolcador();
        //else if (guardia)
        //    dt = usuario.ObtenerUsuariosGuardia();
        {

            dt = usuario.ObtenerUsuariosXTIPO(tipo, site_id);
        }
            
        else
            dt = usuario.ObtenerTodos();




        DataView dw = dt.AsDataView();
        dw.Sort = "USERNAME ASC";
        u.CargaDrop(ddl, "ID", "USERNAME", dw.ToTable());
    }

    #endregion

    #region Zona
    ZonaBC z = new ZonaBC();

    public void Zona_Todos(DropDownList ddl, int site_id = 0, int zoti_id = 0)
    {
        dt = z.ObtenerXSite(site_id,true);
        DataView dw = dt.AsDataView();
        dw.Sort = "DESCRIPCION ASC";
        u.CargaDropTodos(ddl, "ID", "DESCRIPCION", dw.ToTable());
    }

    public void Zona_Normal(DropDownList ddl, int site_id = 0, int zoti_id = 0)
    {
        dt = z.ObtenerXSite(site_id, zoti_id);
        DataView dw = dt.AsDataView();
        dw.Sort = "DESCRIPCION ASC";
        u.CargaDropNormal(ddl, "ID", "DESCRIPCION", dw.ToTable());
    }

    public void Zona(DropDownList ddl, int site_id = 0, int zoti_id = 0)
    {
        dt = z.ObtenerXSite(site_id, zoti_id);
        DataView dw = dt.AsDataView();
        dw.Sort = "DESCRIPCION ASC";
        u.CargaDrop(ddl, "ID", "DESCRIPCION", dw.ToTable());
    }

    #endregion

    #region Proveedor

    readonly ProveedorBC prov = new ProveedorBC();

    public void Proveedor_Todos(DropDownList ddl)
    {
        dt = prov.obtenerTodo();
        DataView dw = dt.AsDataView();
        dw.Sort = "DESCRIPCION ASC";
        u.CargaDropTodos(ddl, "ID", "DESCRIPCION", dw.ToTable());
    }

    public void Proveedor_Normal(DropDownList ddl)
    {
        dt = prov.obtenerTodo();
        DataView dw = dt.AsDataView();
        dw.Sort = "DESCRIPCION ASC";
        u.CargaDropNormal(ddl, "ID", "DESCRIPCION", dw.ToTable());
    }

    public void Proveedor(DropDownList ddl)
    {
        dt = prov.obtenerTodo();
        DataView dw = dt.AsDataView();
        dw.Sort = "DESCRIPCION ASC";
        u.CargaDrop(ddl, "ID", "DESCRIPCION", dw.ToTable());
    }

    #endregion

    #region Transportista

    TransportistaBC tran = new TransportistaBC();

    public void Transportista_Todos(DropDownList ddl)
    {
        dt = tran.ObtenerTodos();
        DataView dw = dt.AsDataView();
        dw.Sort = "NOMBRE ASC";
        u.CargaDropTodos(ddl, "ID", "NOMBRE", dw.ToTable());
    }

    public void Transportista_Normal(DropDownList ddl)
    {
        dt = tran.ObtenerTodos();
        DataView dw = dt.AsDataView();
        dw.Sort = "NOMBRE ASC";
        u.CargaDropNormal(ddl, "ID", "NOMBRE", dw.ToTable());
    }

    public void Transportista(DropDownList ddl)
    {
        dt = tran.ObtenerTodos();
        DataView dw = dt.AsDataView();
        dw.Sort = "NOMBRE ASC";
        u.CargaDrop(ddl, "ID", "NOMBRE", dw.ToTable());
    }

    #endregion
}