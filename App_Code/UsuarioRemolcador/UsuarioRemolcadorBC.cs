using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de UsuarioRemolcadorBC
/// </summary>
public class UsuarioRemolcadorBC : UsuarioRemolcadorTable
{
    SqlTransaccion tran = new SqlTransaccion();

    public DataTable obtenerTodos(int site_id)
    {
        return tran.UsuarioRemolcador_ObtenerTodos(site_id);
    }

    public DataTable obtenerTodosControl(int site_id)
    {
        return tran.UsuarioRemolcador_ObtenerTodosControl(site_id);
    }

    public DataTable obtenerUsuarioRemolcadorXRemoId(int remo_id)
    {
        return tran.UsuarioRemolcador_CargaXRemoId(remo_id);
    }

    public bool ComprobarRegistros(UsuarioRemolcadorBC ur, string repr_id = null)
    {
        return tran.UsuarioRemolcador_ComprobarRegistros(ur, repr_id);
    }

    public bool Guardar(UsuarioRemolcadorBC u)
    {
        return tran.UsuarioRemolcador_Agregar(u);
    }

    public bool Eliminar(string repr_id)
    {
        return tran.UsuarioRemolcador_Eliminar(repr_id);
    }
}