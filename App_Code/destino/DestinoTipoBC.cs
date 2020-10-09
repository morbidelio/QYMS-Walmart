using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de DestinoTipoBC
/// </summary>
public class DestinoTipoBC : DestinoTipoTable
{
    SqlTransaccion tran = new SqlTransaccion();
    public DataTable obtenerTodo()
    {
        return tran.TipoDestino_ObtenerTodo();
    }

    public DestinoTipoBC ObtenerSeleccionado(int id, string codigo)
    {
        return tran.TipoDestino_ObtenerSeleccionado(id, codigo);
    }

}