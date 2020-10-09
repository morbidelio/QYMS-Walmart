using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de PlayaOrigenDestinoBC
/// </summary>
public class PlayaOrigenDestinoBC
{
    SqlTransaccion tran = new SqlTransaccion();
	public PlayaOrigenDestinoBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable ObtenerTodas()
    {
        return tran.PlayaOD_ObtenerTodas();
    }

    public DataTable ObtenerXPlayId(int play_id_ori)
    {
        return tran.PlayaOD_ObtenerXPlayId(play_id_ori);
    }

    public bool Crear(DataTable dt, int idplaya )
    {
        return tran.PlayaOD_Crear(dt,idplaya);
    }

}