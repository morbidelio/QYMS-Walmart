using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de LocalBC
/// </summary>
public class LocalBC : LocalTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();
	public LocalBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable obtenerTodoLocal()
    {
        return tran.Local_ObtenerTodos();
    }

    public LocalBC obtenerXID()
    {
        return tran.Local_ObtenerXId(this.ID);
    }

    public LocalBC obtenerXID(int id)
    {
        return tran.Local_ObtenerXId(id);
    }

    public LocalBC obtenerXCodigo() 
    {
        return tran.Local_ObtenerXCodigo(this.CODIGO);
    }

    public LocalBC obtenerXCodigo(string codigo) 
    {
        return tran.Local_ObtenerXCodigo(codigo);
    }

    public DataTable obtenerXParametro(string descripcion, int idComuna, int idRegion, string codigo) 
    {
        return tran.Local_ObtenerXParametro(descripcion, idComuna, idRegion,codigo);
    }

    public bool Crear(LocalBC local)
    {
        return tran.Local_Crear(local);
    }

    public bool Modificar(LocalBC local)
    {
        return tran.Local_Modificar(local);
    }

    public bool Eliminar(int id)
    {
        return tran.Local_Eliminar(id);
    }
}