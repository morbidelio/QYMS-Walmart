using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de ZonaBC
/// </summary>
public class ZonaBC : ZonaTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();

    public DataTable ObtenerTodas()
    {
        return tran.Zona_ObtenerTodas();
    }

    public ZonaBC ObtenerXId(int id)
    {
        return tran.Zona_ObtenerXId(id);
    }

    public DataTable ObtenerXSite(int site_id, bool incuir_virtual)
    {
        return tran.Zona_ObtenerXSite(site_id, incuir_virtual);
    }

    public DataTable ObtenerXSite(int site_id = 0, int zoti_id = 0)
    {
        return tran.Zona_ObtenerXSite(site_id, zoti_id);
    }

    public DataTable ObtenerXParametros()
    {
        return tran.Zona_ObtenerXParametros(this);
    }

    public DataTable ObtenerXParametros(ZonaBC z)
    {
        return tran.Zona_ObtenerXParametros(z);
    }


    public DataTable ObtenerXParametrosMant(ZonaBC z)
    {
        return tran.Zona_ObtenerXParametrosMant(z);
    }

    public DataTable ObtenerLI(int site_id, int zoti_id = 0, bool zoti_igual = true, int pyti_id = 0)
    {
        return tran.Zona_ObtenerLI(site_id, zoti_id, zoti_igual, pyti_id);
    }

    public bool Crear(ZonaBC zona)
    {
        return tran.Zona_Crear(zona);
    }

    public bool Modificar(ZonaBC zona)
    {
        return tran.Zona_Modificar(zona);
    }

    public bool Eliminar(int id)
    {
        return tran.Zona_Eliminar(id);
    }

    public bool Virtual(int id)
    {
        return tran.Zona_Virtual(id);
    }

    public DataTable ObtenerXCriterio(string descripcion, int site_id, int zoti_id)
    {
        return tran.Zona_ObtenerXParametro(descripcion, site_id, zoti_id);
    }

}