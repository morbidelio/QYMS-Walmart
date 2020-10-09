using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de DestinoBC
/// </summary>
public class DestinoBC : DestinoTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();

    public DataTable ObtenerTodo()
    {
        return tran.Destino_ObtenerTodo();
    }

    public DataTable ObtenerXTipo(int id_tipo)
    {
        return tran.Destino_ObtenerXTipo(id_tipo);
    }

    public DataTable ObtenerXTipo(string cod_tipo)
    {
        return tran.Destino_ObtenerXTipo(cod_tipo);
    }

    public DataTable ObtenerXParametros(string nombre)
    {
        return tran.Destino_ObtenerXParametros(nombre);
    }

    public DestinoBC ObtenerSeleccionado(int id, string codigo)
    {
        return tran.Destino_ObtenerSeleccionado(id, codigo);
    }

    public bool Agregar(DestinoBC d)
    {
        return tran.Destino_Agregar(d);
    }

    public bool Modificar(DestinoBC d)
    {
        return tran.Destino_Modificar(d);
    }

    public bool Eliminar(int id)
    {
        return tran.Destino_Eliminar(id);
    }
}