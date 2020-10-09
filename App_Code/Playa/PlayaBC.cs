using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de PlayaBC
/// </summary>
public class PlayaBC : PlayaTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();
	public PlayaBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable ObtenerXZona(int zona_id, int pyti_id = 0)
    {
        return tran.Playa_ObtenerTodas(zona_id, 0, pyti_id);
    }

    public DataTable ObtenerXSite(int site_id)
    {
        return tran.Playa_ObtenerTodas(0, site_id, 0);
    }

    public bool AgregarCaracteristica(int play_id, string caracteristicas)
    {
        return tran.AgregarCaracteristica(play_id,caracteristicas);
    }

    public DataTable ObtenerTodas()
    {
        return tran.Playa_ObtenerTodas(this.ZONA_ID, this.SITE_ID, this.ID_TIPOPLAYA);
    }

    public DataTable ObtenerTodas(int zona_id = 0, int site_id = 0, int pyti_id = 0)
    {
        return tran.Playa_ObtenerTodas(zona_id, site_id, pyti_id);
    }

    public DataTable ObtenerDrop(int site_id)
    {
        return tran.Playa_ObtenerDrop(site_id);
    }

    public DataTable ObtenerDrop(int site_id, int zona_id)
    {
        return tran.Playa_ObtenerDrop(site_id, zona_id);
    }

    public DataTable ObtenerTipos()
    {
        return tran.Playa_ObtenerTipos();
    }

    public PlayaBC ObtenerPlayaXId(int id)
    {
        return tran.Playa_ObtenerXId(id);
    }

    public string ObtenerCaracteristicasPlaya(int id)
    {
        return tran.ObtenerCaracteristicasPlaya(id);
    }

    public DataTable ObtenerPlayasXCriterio(string codigo, string descripcion, int zona_id, bool is_virtual, string playa_tipo=null)
    {
        return tran.Playa_ObtenerXParametro(codigo, descripcion, zona_id, is_virtual, playa_tipo);
    }

    public DataTable ObtenerPlayasXCriterioTipoCarga(string codigo, string descripcion, int zona_id, bool is_virtual, int tipo_carga, string playa_tipo = null)
    {
        return tran.Playa_ObtenerXParametrotipo_carga(codigo, descripcion, zona_id, is_virtual, tipo_carga, playa_tipo);
    }

    public bool Eliminar(int id)
    {
        return tran.Playa_Eliminar(id);
    }

    public bool Modificar(PlayaBC playa)
    {
        return tran.Playa_Modificar(playa);
    }

    public bool Crear(PlayaBC playa)
    {
        return tran.Playa_Crear(playa);
    }
}