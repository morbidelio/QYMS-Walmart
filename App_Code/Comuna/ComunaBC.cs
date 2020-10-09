using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de ComunaBC
/// </summary>
public class ComunaBC : ComunaTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();
	public ComunaBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
    }

    public DataTable obtenerTodoComuna()
    {
        return tran.Comuna_ObtenerTodos();
    }

    public ComunaBC obtenerXID(int id)
    {
        return tran.Comuna_ObtenerXId(id);
    }

    public DataTable obtenerComunasXRegion(int idRegion)
    {
        return tran.Comuna_ObtenerXRegion(idRegion);
    }
}