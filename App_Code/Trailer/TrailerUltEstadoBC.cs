using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de TrailerUltEstado
/// </summary>
public class TrailerUltEstadoBC : TrailerUltEstadoTable
{
    readonly SqlTransaccionMovimiento tran = new SqlTransaccionMovimiento();

	public TrailerUltEstadoBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public TrailerUltEstadoBC CargaTrue(int id)
    {
        return tran.TrailerUltEstado_CargaXId(id);
    }

    public DataSet CargaUltEstadoQMGPS()
    {
        return tran.TrailerUltEstado_CargaQMGPS(this);
    }

    public DataSet CargaUltEstadoQMGPS(TrailerUltEstadoBC t)
    {
        return tran.TrailerUltEstado_CargaQMGPS(t);
    }

}