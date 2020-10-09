using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de PlayaOrigenDestinoSelloBC
/// </summary>
public class PlayaOrigenDestinoSelloBC
{
    SqlTransaccion tran = new SqlTransaccion();
	public PlayaOrigenDestinoSelloBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable ObtenerTodas()
    {
        return tran.PlayaODSello_ObtenerTodas();
    }

    public DataTable ObtenerXPlayId(int play_id_ori)
    {
        return tran.PlayaODSello_ObtenerXPlayId(play_id_ori);
    }

    public bool Crear(DataTable dt, int idplaya )
    {
        return tran.PlayaODSello_Crear(dt,idplaya);
    }

}