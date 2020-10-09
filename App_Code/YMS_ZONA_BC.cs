using System;
using System.Linq;
using System.Data;
using Qanalytics.Data.Access.SqlClient;


public class YMS_ZONA_BC : YMS_ZONA_TABLE
{
    private bool _logueado;


    public YMS_ZONA_BC()
    {
    }


    public DataTable ObtenerZonasTipoCarga(int id_site, string descripcion, string tipo, int tipo_carga, int playa_tipo)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtenerZonasTipoCarga(id_site, descripcion, tipo, tipo_carga, playa_tipo);
    }

    public DataTable ObtenerZonas(int id_site, string descripcion, string tipo)
    {
         SqlTransaction_YMS tran = new SqlTransaction_YMS();
         return tran.obtenerZonas(id_site,descripcion, tipo);
    }

    public DataTable obtenerTipoCarga( string descripcion)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtener_tipoCarga(descripcion);
    }
    public DataTable obtenerTipoCarga( string descripcion,bool preentrada, bool entrada)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtener_tipoCarga(descripcion,preentrada,entrada);
    }

    public DataTable obtenerMotivoTipoCarga(string id_tipo_carga,  string descripcion)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtener_motivotipoCarga(id_tipo_carga,descripcion);
    }

    public DataTable ObtenerZonasDescarga(int id_site)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtenerZonasDescarga(id_site);
    }

    public DataTable ObteneSites(int id_user)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtenersites(id_user);
    }


    public DataTable ObteneSite(int id)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtenersite(id);
    }

    public DataTable Obtenerimagensite(int id_user)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtenersite(id_user);
    }

    public DataTable ObtenerPlayas_Site(int id_site, string descripcion, string tipo)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtenerPlayas_X_SITe(id_site, descripcion, Convert.ToInt32( tipo));
    }

    public DataTable ObtenersubPlayas_Site(int id_site, string descripcion, string tipo)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtenersubPlayas_X_SITe(id_site, descripcion, Convert.ToInt32(tipo));
    }

    public DataTable ObtenerTodos(int zona, string descripcion, string tipo)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtenerPlayas_X_Zona(zona, descripcion, Convert.ToInt32( tipo));
    }

    public DataTable Obtenerlugares_playa(int id_playa, string descripcion, string ocupado)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtenerlugares_X_playa(id_playa, descripcion, ocupado);
    }

    public DataTable Obtenerlugares_SUBplaya(int id_playa,  int SUPL_ID,string descripcion, string ocupado)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtenerlugares_X_Subplaya(id_playa,  SUPL_ID, descripcion, ocupado);
    } 
    public Boolean visibleasignartrailer(int id_site, int id_usuario)
        {
            SqlTransaction_YMS tran = new SqlTransaction_YMS();
            return tran.visibleasignartrailer(id_site,id_usuario);
    }

    public DataTable Obtenermenu_lugar(int id_lugar)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.obtenermenu_lugar(id_lugar);
    }


    public DataTable guarda_playa(int id_playa, double x, double y, double ancho, double alto, int orientacion)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.guarda_playa(id_playa, x, y, ancho, alto, orientacion);
    }


    public DataTable guarda_sub_playa(int id_playa, int supl_id,double x, double y, double ancho, double alto, int orientacion)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.guarda_sub_playa(id_playa, supl_id, x, y, ancho, alto, orientacion);
    }

    public bool cuadra(string YATA_COD,int YAHI_ID,int YAPE_ID,int LUGA_ID,int TRAI_ID , int usua_id, out string mensaje)
    {
        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.cuadra_yardtag(YATA_COD, YAHI_ID, YAPE_ID, LUGA_ID, TRAI_ID, usua_id, out mensaje);
    }


    public DataTable yardtag_cuadra_ObtenerTodos(string TRAI_PLACA, int play_id, int zona_id, int site_id)
    {

        SqlTransaction_YMS tran = new SqlTransaction_YMS();
        return tran.yardtag_cuadra_ObtenerTodos(TRAI_PLACA, play_id, zona_id, site_id);
    }


}
