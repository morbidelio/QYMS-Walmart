using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de PerfilBC
/// </summary>
public class PerfilBC : PerfilTable
{
    readonly SqlTransactionUsuario tran = new SqlTransactionUsuario();
    public DataTable ObtenerTodo()
    {
        return tran.Perfil_ObtenerTodo();
    }

    public DataTable ObtenerTodo(bool mobile)
    {
        return tran.Perfil_ObtenerTodo(mobile);
    }

    public PerfilBC ObtenerXId()
    {
        return tran.Perfil_ObtenerPorId(this.ID, this.MOBILE);
    }

    public PerfilBC ObtenerXId(int id,bool mobile = false)
    {
        return tran.Perfil_ObtenerPorId(id, mobile);
    }

    public bool Ingresa()
    {
        return tran.Perfil_Crear(this);
    }

    public bool Ingresa(PerfilBC perfil)
    {
        return tran.Perfil_Crear(perfil);
    }

    public bool Modifica()
    {
        return tran.Perfil_Modificar(this);
    }

    public bool Modifica(PerfilBC perfil)
    {
        return tran.Perfil_Modificar(perfil);
    }

    public bool Elimina(int id)
    {
        return tran.Perfil_Eliminar(id);
    }
}