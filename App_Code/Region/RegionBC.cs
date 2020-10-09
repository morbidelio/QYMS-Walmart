using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de RegionBC
/// </summary>
public class RegionBC : RegionTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();
	public RegionBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable obtenerTodoRegion()
    {
        return tran.Region_ObtenerTodos();
    }

    public RegionBC obtenerXID(int id)
    {
        return tran.Region_ObtenerXId(id);
    }

}