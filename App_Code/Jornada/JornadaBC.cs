using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de JornadaBC
/// </summary>
public class JornadaBC : JornadaTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();
    public DataTable obtenerTodas()
    {
        return tran.Jornada_ObtenerTodas();
    }
}