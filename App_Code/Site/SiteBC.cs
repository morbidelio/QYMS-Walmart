using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de SiteBC
/// </summary>
public class SiteBC : SiteTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();

	public SiteBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable ObtenerTodos()
    {
        return tran.Site_ObtenerTodos();
    }

    public SiteBC ObtenerXId(int id)
    {
        return tran.Site_ObtenerXId(id);
    }

    public DataTable ObtenerXCriterio(string nombre, int empresa_id)
    {
        return tran.Site_ObtenerXParametro(nombre,empresa_id);
    }

    public bool Eliminar(int id)
    {
        return tran.Site_Eliminar(id);
    }

    public bool Crear(SiteBC site)
    {
        return tran.Site_Crear(site);
    }

    public bool Modificar(SiteBC site)
    {
        return tran.Site_Modificar(site);
    }

    public bool TrailerAuto(int id, int usua_id)
    {
        return tran.Site_TrailerAuto(id, usua_id);
    }

    public bool Virtual(int site_id, int play_id)
    {
        return tran.Site_Virtual(site_id, play_id);
    }
}