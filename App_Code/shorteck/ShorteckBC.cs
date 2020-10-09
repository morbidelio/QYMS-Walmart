using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de ShorteckBC
/// </summary>
public class ShorteckBC : ShorteckTable
{
    SqlTransaccion tran = new SqlTransaccion();
	public ShorteckBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable ObtenerTodos()
    {
        return tran.Shorteck_ObtenerTodos();
    }

    public ShorteckBC ObtenerXId()
    {
        return tran.Shorteck_ObtenerXId(this.ID);
    }

    public ShorteckBC ObtenerXId(string id)
    {
        return tran.Shorteck_ObtenerXId(id);
    }

    public bool AgregarModificar()
    {
        return tran.Shorteck_AgregarModificar(this);
    }

    public bool AgregarModificar(ShorteckBC s)
    {
        return tran.Shorteck_AgregarModificar(s);
    }

    public bool Eliminar()
    {
        return tran.Shorteck_Eliminar(this.ID);
    }

    public bool Eliminar(string id)
    {
        return tran.Shorteck_Eliminar(id);
    }
}