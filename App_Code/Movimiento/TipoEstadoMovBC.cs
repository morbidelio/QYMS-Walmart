using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Qanalytics.Data.Access.SqlClient;
using System.Web;

/// <summary>
/// Descripción breve de TipoEstadoMovBC
/// </summary>
public class TipoEstadoMovBC : TipoEstadoMovTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();
    public TipoEstadoMovBC()
    {
    }

    public DataTable obtenerTodoTipoEstadoMov()
    {
        return tran.TipoEstadoMov_ObtenerTodos();
    }

    public TipoEstadoMovBC obtenerXID(int id)
    {
        return tran.TipoEstadoMov_ObtenerXId(id);
    }

    public DataTable obtenerXParametro(string descripcion)
    {
        return tran.TipoEstadoMov_ObtenerXParametro(descripcion);
    }

    public bool Crear(TipoEstadoMovBC trailer)
    {
        return tran.TipoEstadoMov_Crear(trailer);
    }

    public bool Modificar(TipoEstadoMovBC trailer)
    {
        return tran.TipoEstadoMov_Modificar(trailer);
    }

    public bool Eliminar(int id)
    {
        return tran.TipoEstadoMov_Eliminar(id);
    }
}