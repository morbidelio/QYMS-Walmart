using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de TrailerUltSalidaBC
/// </summary>
public class TrailerUltSalidaBC : TrailerUltSalidaTable
{
    SqlTransaccionMovimiento tran = new SqlTransaccionMovimiento();

	public TrailerUltSalidaBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

	public TrailerUltSalidaBC(int id)
	{
        this.TRAI_ID = id;
        ObtenerXId();
	}

    public TrailerUltSalidaBC ObtenerXId()
    {
        return tran.TrailerUltSalida_Carga(this.TRAI_ID);
    }

    public TrailerUltSalidaBC ObtenerXId(int id)
    {
        return tran.TrailerUltSalida_Carga(id);
    }

    public TrailerUltSalidaBC ObtenerXPlaca(string placa)
    {
        return tran.TrailerUltSalida_Carga(0,placa);
    }

    public TrailerUltSalidaBC ObtenerXNumero(string numero)
    {
        return tran.TrailerUltSalida_Carga(0,null,numero);
    }

    public TrailerUltSalidaBC ObtenerXShortek(string shortek)
    {
        return tran.TrailerUltSalida_Carga(0,null,null,shortek);
    }

    public bool ProcesoSalida(TrailerUltSalidaBC t, int id_usuario,  string viaje, string GPS,  out  string resultado)
    {
        return tran.TrailerUltEstado_ProcesoSalida(t, id_usuario, viaje,  GPS, out resultado);
    }
    public bool ProcesoSalida(TrailerUltSalidaBC t, DataTable dt, int id_usuario, string viaje, string GPS, out  string resultado)
    {
        return tran.TrailerUltEstado_ProcesoSalida(t, dt, id_usuario, viaje, GPS, out resultado);
    }

}