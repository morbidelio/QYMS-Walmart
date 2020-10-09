using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de TimerProcesosBC
/// </summary>
public class TimerProcesosBC : TimerProcesosTable
{
    SqlTransaccion tran = new SqlTransaccion();
	public TimerProcesosBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable ObtenerTodos()
    {
        return tran.TimerProcesos_ObtenerTodos();
    }

    public TimerProcesosBC ObtenerXId(int id)
    {
        return tran.TimerProcesos_ObtenerXId(id);
    }

    public bool Crear(TimerProcesosBC tp)
    {
        return tran.TimerProcesos_Crear(tp);
    }

    public bool Modificar(TimerProcesosBC tp)
    {
        return tran.TimerProcesos_Modificar(tp);
    }

    public bool Eliminar(int id)
    {
        return tran.TimerProcesos_Eliminar(id);
    }
}