using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de Tipo_ZonaBC
/// </summary>
public class Tipo_ZonaBC : Tipo_ZonaTable
{
	public Tipo_ZonaBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable ObtenerTodos()
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.TipoZona_ObtenerTodos();
    }

    public Tipo_ZonaBC ObtenerTipoZonaXId(int id)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.TipoZona_ObtenerXId(id);
    }

    public DataTable ObtenerTiposZonaXCriterio(string descripcion)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.TipoZona_ObtenerXParametro(descripcion);
    }
    
    public bool Crear(Tipo_ZonaBC zoti)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.TipoZona_Crear(zoti);
    }
    public bool Modificar(Tipo_ZonaBC zoti)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.TipoZona_Modificar(zoti);
    }
        
    public bool Eliminar(int id)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.TipoZona_Eliminar(id);
    }
}