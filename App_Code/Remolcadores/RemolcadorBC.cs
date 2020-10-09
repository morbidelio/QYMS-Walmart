using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de RemolcadoresBC
/// </summary>
public class RemolcadorBC : RemolcadorTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();
	public RemolcadorBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable obtenerTodos(int site_id = 0)
    {
        return tran.Remolcador_ObtenerTodos(site_id);
    }

    public RemolcadorBC obtenerXId()
    {
        return tran.Remolcador_ObtenerXId(this.ID);
    }

    public RemolcadorBC obtenerXId(int id)
    {
        return tran.Remolcador_ObtenerXId(id);
    }

    public DataTable obtenerXParametro(int site_id, string codigo, string descripcion)
    {
        return tran.Remolcador_ObtenerXParametro(site_id, codigo, descripcion);
    }

    public bool Crear(RemolcadorBC remolcador)
    {
        return tran.Remolcador_Crear(remolcador);
    }

    public bool Modificar(RemolcadorBC remolcador)
    {
        return tran.Remolcador_Modificar(remolcador);
    }

    public bool Eliminar(int id)
    {
        return tran.Remolcador_Eliminar(id);
    }
}