using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de CargaTipoBC
/// </summary>
public class CargaTipoBC : CargaTipoTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();
    public CargaTipoBC()
    {
    }

    public DataTable obtenerTodo()
    {
        return tran.CargaTipo_ObtenerTodos();
    }

    public CargaTipoBC obtenerXID(int id)
    {
        return tran.CargaTipo_ObtenerXId(id);
    }

    public bool Crear(CargaTipoBC trailer)
    {
        return tran.CargaTipo_Crear(trailer);
    }

    public bool Modificar(CargaTipoBC trailer)
    {
        return tran.CargaTipo_Modificar(trailer);
    }

    public bool Eliminar(int id)
    {
        return tran.CargaTipo_Eliminar(id);
    }

    public DataTable CargaDestinos(int tiic_id)
    {
        return tran.CargaTipo_CargaDestinos(tiic_id);
    }

    public bool AsignarDestinos(DataTable dt, int tiic_id)
    {
        return tran.CargaTipo_AsignarDestinos(dt, tiic_id);
    }
}