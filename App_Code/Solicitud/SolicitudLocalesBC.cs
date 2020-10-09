using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de SolicitudLocalesBC
/// </summary>
public class SolicitudLocalesBC : SolicitudLocalesTable
{
    readonly SqlTransaccionSolicitud tran = new SqlTransaccionSolicitud();
    public SolicitudLocalesBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public bool AgregarLocal()
    {
        return tran.SolicitudLocales_Crear(this);
    }

    public bool AgregarLocal(SolicitudLocalesBC local)
    {
        return tran.SolicitudLocales_Crear(local);
    }

    public bool Eliminar()
    {
        return tran.SolicitudLocales_Eliminar(this);
    }

    public bool Eliminar(SolicitudLocalesBC s)
    {
        return tran.SolicitudLocales_Eliminar(s);
    }

    public DataTable CargaLocalesXSolicitudTrailer(int id,  string viaje, int site_id)
    {
        return tran.SolicitudLocales_XSolicitudTrai(id,   viaje,  site_id);
    }
}