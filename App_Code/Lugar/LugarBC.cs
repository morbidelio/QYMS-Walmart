using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de LugarBC
/// </summary>
public class LugarBC : LugarTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();
	public LugarBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public LugarBC obtenerLugarAuto(int site_id, int usua_id, string luga_id)
    {
        return tran.Lugar_ObtenerAuto(site_id,usua_id,luga_id);
    }

    public DataTable ObtenerTodos1(int ocupados = -1, int lues_id = -1, int trai_id = -1, int play_id = 0, int zona_id = 0, int site_id = 0, int playa_tipo=0)
    {
        return tran.Lugar_ObtenerTodos(ocupados, lues_id, trai_id, play_id, zona_id, site_id, playa_tipo);
    }
    
    public DataTable ObtenerTodos(int site_id = 0, int zona_id = 0, int play_id = 0, int playa_tipo=0)
    {
        return tran.Lugar_ObtenerTodos(site_id, zona_id, play_id, playa_tipo);
    }
    
    public DataTable ObtenerXPlaya(int playa_id, int ocupados = -1, int lues_id = -1, int trai_id = -1)
    {
        return tran.Lugar_ObtenerTodos(ocupados, lues_id, trai_id, playa_id,0,0,0);
    }

    public DataTable ObtenerXsolicitud(int site_id,int playa_id, int ocupados , int lues_id , int trai_id , int solicitud_tipo)
    {
        return tran.Lugar_ObtenerXSolicitud(site_id,playa_id, ocupados, lues_id, trai_id,solicitud_tipo);
    }
    public DataTable ObtenerXZona(int zona_id, int ocupados = -1, int lues_id = -1, int trai_id = -1)
    {
        return tran.Lugar_ObtenerXZona(zona_id, ocupados, lues_id, trai_id);
    }

    public DataTable ObtenerXSite(int site_id, int ocupados = -1, int lues_id = -1, int trai_id = -1)
    {
        return tran.Lugar_ObtenerXSite(site_id, ocupados, lues_id, trai_id);
    }

    public DataTable obtenerLugaresXPlayaDrop(int id_playa)
    {
        return tran.Lugar_ObtenerLugarXPlayaDrop(id_playa);
    }

    public DataTable obtenerLugarXPlayaZona()
    {
        return tran.Lugar_ObtenerLugarXPlayaZona();
    }



    public DataTable obtenerLugarEstado(int id_playa = 0,int id_lugar = 0, int site_id = 0)
    {
        return tran.Lugar_ObtenerLugarEstado(id_playa,id_lugar,  site_id);
    }

    public DataTable obtenerSolicitudesPendientesXLugar(int id_lugar)
    {
        return tran.Lugar_ObtenerSolicitudesPendientesXLugar(id_lugar);
    }

    public DataTable obtenerTodoLugarPlayaZona()
    {
        return tran.Lugar_ObtenerTodoLugarPlayaZona();
    }

    public DataTable obtenerLugaresGuardia()
    {
        return tran.Lugar_ObtenerGuardias();
    }

    public LugarBC obtenerXID(int id)
    {
        return tran.Lugar_ObtenerXId(id);
    }

    public DataTable obtenerXParametro(string codigo, string descripcion, int site_id, bool ocupado,int zona_id , int playa_id)
    {
        return tran.Lugar_ObtenerXParametro(codigo, descripcion, site_id, ocupado,zona_id ,playa_id);
    }

    public bool Crear(LugarBC lugar)
    {
        return tran.Lugar_Crear(lugar);
    }

    public bool Modificar(LugarBC lugar, out string mensaje)
    {
        try
        {
            mensaje="";
            return tran.Lugar_Modificar(lugar);
        }
        catch (Exception ex)
        {
            mensaje = ex.Message;
            return false;
        }

    }

    public bool Eliminar(int id)
    {
        return tran.Lugar_Eliminar(id);
    }

    public bool Habilitar(int id, out string mensaje)
    {
        mensaje="";
        try
        {
            return tran.Lugar_Habilitar(id);

        }
        catch (Exception ex)
        {
            mensaje = ex.Message;
            return false;
        }

    }
    public bool Cuadratura(int usua_id, TrailerBC trai, string cargado, out string error)
    {
        return tran.Lugar_Cuadratura(this.ID, usua_id, trai,  cargado, out error);
    }

    public bool Cuadratura(int luga_id, int usua_id, TrailerBC trai, string cargado, out string error)
    {
        return tran.Lugar_Cuadratura(luga_id, usua_id, trai, cargado,out error);
    }
}