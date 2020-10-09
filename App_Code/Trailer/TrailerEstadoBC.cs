using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de TrailerEstadoBC
/// </summary>
public class TrailerEstadoBC :TrailerEstadoTable
{
	public TrailerEstadoBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    SqlTransaccion tran = new SqlTransaccion();

    public DataTable ObtenerTodos()
    {
        return tran.Trailer_ObtenerEstados();
    }
    
    public TrailerEstadoBC ObtenerXId(int tres_id)
    {
        return tran.TrailerEstado_ObtenerXId(tres_id);
    }

    public DataTable ObtenerTodosSTOCK()
    {
        return tran.Trailer_ObtenerEstados_stock();
    }

}