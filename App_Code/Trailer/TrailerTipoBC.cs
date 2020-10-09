using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de TrailerTipoBC
/// </summary>
public class TrailerTipoBC : TrailerTipoTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();

    public TrailerTipoBC()
    {
    }

    public DataTable obtenerTodo()
    {
        return tran.TrailerTipo_ObtenerTodos();
    }

    public TrailerTipoBC obtenerXID(int id)
    {
        return tran.TrailerTipo_ObtenerXId(id);
    }

    public DataTable obtenerXParametro(string descripcion)
    {
        return tran.TrailerTipo_ObtenerXParametro(descripcion);
    }

    public bool Crear(TrailerTipoBC trailer)
    {
        return tran.TrailerTipo_Crear(trailer);
    }

    public bool Modificar(TrailerTipoBC trailer)
    {
        return tran.TrailerTipo_Modificar(trailer);
    }

    public bool Eliminar(int id)
    {
        return tran.TrailerTipo_Eliminar(id);
    }

}