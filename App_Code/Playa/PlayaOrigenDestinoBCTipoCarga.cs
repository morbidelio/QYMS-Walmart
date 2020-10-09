using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de PlayaOrigenDestinoSelloBC
/// </summary>
public class PlayaOrigenDestinoTipoCargaBC
{
    SqlTransaccion tran = new SqlTransaccion();
    public PlayaOrigenDestinoTipoCargaBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable ObtenerTodas()
    {
        return tran.PlayaODTipoCarga_ObtenerTodas();
    }

    public DataTable ObtenerXPlayId(int play_id_ori)
    {
        return tran.PlayaODTipoCarga_ObtenerXPlayId(play_id_ori);
    }

    public bool Crear(DataTable dt, int idplaya, int tiic_id )
    {
        return tran.PlayaODTipoCarga_Crear(dt, idplaya, tiic_id);
    }

}