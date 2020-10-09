using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de SolicitudBC
/// </summary>
public class SolicitudBC : SolicitudTable
{
    readonly SqlTransaccionSolicitud tran = new SqlTransaccionSolicitud();
    public SolicitudBC()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public DataTable ObtenerEstados()
    {
        return tran.Solicitud_Estados();
    }
    public DataTable ObtenerTodoColorEstadoSolicitud()
    {
        return tran.LugarColorSolicitud_ObtenerTodo();
    }
    public DataTable ObtenerColorEstadoSolicitud(int soes_id)
    {
        return tran.LugarColorSolicitud_ObtenerTodo(soes_id);
    }
    public DataTable ObtenerColorXSite(int site_id)
    {
        return tran.LugarColorSolicitud_ObtenerXSite(site_id);
    }
    public DataTable ObtenerColorXSite(int site_id,int soes_id)
    {
        return tran.LugarColorSolicitud_ObtenerXSite(site_id,soes_id);
    }
    public DataTable ObtenerColorXSite(int site_id,int soes_id,int soti_id)
    {
        return tran.LugarColorSolicitud_ObtenerXSite(site_id,soes_id,soti_id);
    }
    public bool EditarColorEstadoSite(DataTable dt, int soes_id)
    {
        return tran.LugarColorSolicitud_Agregar(dt, soes_id);
    }
    public DataTable Obtenertemperaturas(bool frio, bool congelado, bool seco, bool multifrio, bool ways)
    {
        return tran.obtenertemperaturas(frio, congelado, seco,multifrio,ways);
    }
    public DataTable ObtenerEstadosCarga()
    {
        return tran.Solicitud_EstadosCarga();
    }
    public DataTable ObtenerEstadosCargaReporte()
    {
        return tran.Solicitud_EstadosCarga_reporte();
    }
    public DataTable ObtenerInconsistentes()
    {
        return tran.Solicitud_ObtenerInconsistentes();
    }
    public DataTable ObtenerSolicitudesCarga(int site_id = 0, int play_id = 0, int luga_id = 0, int soli_id = 0, int soes_id = 0, int tran_id = 0, string ruta_id = null)
    {
        return tran.SolicitudAnden_SolicitudesCarga(site_id, play_id, luga_id, soli_id, soes_id, tran_id, ruta_id);
    }
    public DataTable ObtenerSolicitudesDescarga(int site_id = 0, int tran_id = 0, int luga_id = 0)
    {
        return tran.Solicitud_SolicitudesDescarga(site_id, tran_id, luga_id);
    }
    public DataTable ObtenerSolicitudesPallets(int site_id = 0, int tran_id = 0, int luga_id = 0)
    {
        return tran.Solicitud_CargaSolicitudesPallets(site_id, tran_id, luga_id);
    }
    public bool Carga(out int soli_id, out string mensaje)                                                          //Deprecated
    {
        return tran.Solicitud_Carga(this, out soli_id, out mensaje);
    }
    public bool Carga(SolicitudBC solicitud, string caracteristicas, out int soli_id, out string mensaje)           //Deprecated
    {
        return tran.Solicitud_Carga(solicitud, out soli_id, out mensaje);
    }
    public bool Carga(DataSet ds, out string mensaje)
    {
        return tran.Solicitud_Carga(this, ds, out mensaje);
    }
    public bool Carga(SolicitudBC solicitud, DataSet ds, string caracteristicas, out string mensaje)
    {
        return tran.Solicitud_Carga(solicitud, ds, out mensaje);
    }
    public bool ModificarCarga(out string mensaje)
    {
        return tran.Solicitud_CargaModificar(this, out mensaje);
    }
    public bool ModificarCarga(SolicitudBC solicitud, out string mensaje)
    {
        return tran.Solicitud_CargaModificar(solicitud, out mensaje);
    }
    public bool ModificarCarga(DataSet ds, out string mensaje)
    {
        return tran.Solicitud_CargaModificar(this, ds, out mensaje);
    }
    public bool ModificarCarga(SolicitudBC solicitud, DataSet ds, out string mensaje)
    {
        return tran.Solicitud_CargaModificar(solicitud, ds, out mensaje);
    }
    public bool Descarga(SolicitudBC solicitud, string bloqueados, out string resultado )
    {
        return tran.Solicitud_Descarga(solicitud,bloqueados,  out resultado);
    }
    public bool pallet(SolicitudBC solicitud, out string resultado)
    {
        return tran.Solicitud_pallet(solicitud , out resultado);
    }
    public bool CargaPallets(int soli_id, int luga_id, out  string resultado, int usua_id)
    {
        return tran.Solicitud_CargaPallets(soli_id, luga_id, out resultado, usua_id);
    }
    public bool DescargaPallets(int soli_id, int luga_id, out  string resultado, int usua_id)
    {
        return tran.Solicitud_DescargaPallets(soli_id, luga_id, out resultado, usua_id);
    }
    public bool ModificarDescarga(SolicitudBC solicitud)
    {
        return tran.Solicitud_DescargaModifica(solicitud);
    }
    public bool DescargaCompleta(int id,  out string resultado,int usua_id, int luga_id = 0)
    {
        return tran.Solicitud_CompletarDescarga(id, luga_id,out resultado, usua_id);
    }
    public bool DescargaMovimiento(MovimientoBC movimiento, int site_id, int usua_id, out string resultado)
    {
        return tran.Solicitud_DescargaMovimiento(movimiento, site_id, usua_id, out resultado);
    }
    public bool andenListo(int id_user, int soan_orden, int Luga_id, out string error)
    {
        return tran.Solicitud_andenListo(this.SOLI_ID, soan_orden,  Luga_id, id_user, out error);
    }
    public bool Eliminar(int id_user, out string error)
    {
        return tran.Solicitud_Eliminar(this.SOLI_ID, id_user, out error);
    }
    public bool Encender_termo(int id_user, out string error)
    {
        return tran.Solicitud_encender_termo(this.SOLI_ID, id_user, out error);
    }
    public bool Eliminar(int id, int id_user, out string error)
    {
        return tran.Solicitud_Eliminar(id, id_user, out error);
    }
    public bool EliminarInconsistentes(int id)
    {
        return tran.Solicitud_EliminarInconsistentes(id);
    }
    public SolicitudBC ObtenerXId()
    {
        return tran.Solicitud_ObtenerXId(this.SOLI_ID);
    }
    public SolicitudBC ObtenerXId(int id)
    {
        return tran.Solicitud_ObtenerXId(id);
    }
    public SolicitudBC ObtenerFinalizadaXId()
    {
        return tran.Solicitud_ObtenerFinalizadaXId(this.SOLI_ID);
    }
    public SolicitudBC ObtenerFinalizadaXId(int id)
    {
        return tran.Solicitud_ObtenerFinalizadaXId(id);
    }
    public DataTable ObtenerAndenesXSolicitudId(int id)
    {
        return tran.SolicitudAnden_ObtenerXSolicitudId(id);
    }
    public DataTable ObtenerAndenesXNewWays(string id_trailer, out string resultado)
    {
        return tran.ObtenerAndenesXNewWays(id_trailer, out resultado);
    }
    public bool validarTimeStamp()
    {
        SolicitudBC s = tran.Solicitud_ObtenerXId(this.SOLI_ID);
        if (s.TIMESTAMP == this.TIMESTAMP)
            return true;
        else
            return false;
    }
    public bool validarTimeStamp(int id, DateTime timestamp)
    {
        SolicitudBC s = tran.Solicitud_ObtenerXId(id);
        if (s.TIMESTAMP == timestamp)
            return true;
        else
            return false;
    }
    public DataTable CargaExcel(DataTable dt, int usua_id)
    {
        return tran.Solicitud_CargaExcel(dt, usua_id);
    }
    public bool ProcesarExcel(string cod_interno, int usua_id, out string msj)
    {
        return tran.Solicitud_ProcesarExcel(cod_interno, usua_id, out msj);
    }
}