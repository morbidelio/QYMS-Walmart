using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de SolicitudTipoBC
/// </summary>
public class SolicitudTipoBC : SolicitudTipoTable
{
    readonly SqlTransaccionSolicitud tran = new SqlTransaccionSolicitud();

    public SolicitudTipoBC()
    {
    }

    public DataTable obtenerTodoSolicitudTipo()
    {
        return tran.SolicitudTipo_ObtenerTodos();
    }

    public SolicitudTipoBC obtenerXID(int id)
    {
        return tran.SolicitudTipo_ObtenerXId(id);
    }

    public DataTable obtenerXParametro(string descripcion)
    {
        return tran.SolicitudTipo_ObtenerXParametro(descripcion);
    }

    public bool Crear(SolicitudTipoBC trailer)
    {
        return tran.SolicitudTipo_Crear(trailer);
    }

    public bool Modificar(SolicitudTipoBC trailer)
    {
        return tran.SolicitudTipo_Modificar(trailer);
    }

    public bool Eliminar(int id)
    {
        return tran.SolicitudTipo_Eliminar(id);
    }
}