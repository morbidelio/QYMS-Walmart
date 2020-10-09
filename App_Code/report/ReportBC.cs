using System;
using System.Collections.Generic;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;
using System.Collections;


/// <summary>
/// Descripción breve de OrigenBC
/// </summary>
public class ReportBC : DSDetalleXGuiaBARRAS
{
    readonly SqlTransactionReport tran = new SqlTransactionReport();
    public ReportBC()
    {
    }
    public DataTable CargarPreEntradaDT(int id)
    {
        return tran.Reporte_CargaPreingresoXId(id);
    }
    public DataTable CargarIngresos(int site_id, int tipo, DateTime desde, DateTime hasta)
    {
        return tran.Reporte_IngresosDescarga(site_id, tipo, desde, hasta);
    }
    public DataTable CargarMovimientos(int site_id, DateTime desde, DateTime hasta, bool no_ottawa)
    {
        return tran.Reporte_Movimientos(site_id, desde, hasta,no_ottawa);
    }

    public DataTable Cargaryardtag(int site_id, DateTime desde, DateTime hasta, string placa)
    {
        return tran.Reporte_yardtag(site_id, desde, hasta, placa);
    }

    public DataTable Cargaryardtaggeneral(int site_id, DateTime desde, DateTime hasta, string placa, string playa, string zona)
    {
        return tran.Reporte_yardtag_general(site_id, desde, hasta, placa,playa, zona);

    }

    public DataTable Cargaryardtagultdato(int site_id, DateTime desde, DateTime hasta, string placa, string playa)
    {
        return tran.Reporte_yardtag_ult_dato(site_id, desde, hasta, placa,playa);

    }
    public DataTable CargarSolicitudesCarga(int site_id, DateTime desde, DateTime hasta, int estado)
    {
        return tran.Reporte_SolicitudesCarga(site_id, desde, hasta,estado);
    }
    public DataTable CargarSolicitudesCargaDetalle(int soli_id)
    {
        return tran.Reporte_SolicitudesCargaDetalle(soli_id);
    }
    public DataTable CargarSolicitudesDescarga(DateTime desde, DateTime hasta, string placa, int site_id, int play_id)
    {
        return tran.Reporte_SolicitudesDescarga(desde, hasta, placa, site_id, play_id);
    }
    public DataTable CargarDevoluciones(DateTime desde, DateTime hasta, int prov_id, int soli_id, string placa, string nro_flota, int site_id)
    {
        return tran.Reporte_Devoluciones(desde, hasta, prov_id, soli_id, placa, nro_flota, site_id);
    }
    public DataTable CargarEntradas(int site_id, DateTime desde, DateTime hasta)
    {
        return tran.Reporte_Entrada(site_id, desde, hasta);
    }
    public DataTable CargarSalidas(int site_id, DateTime desde, DateTime hasta)
    {
        return tran.Reporte_Salida(site_id, desde, hasta);
    }
    public DataTable CargarSalidasLoAguirre(DateTime desde, DateTime hasta)
    {
        return tran.Reporte_SalidaLoAguirre(desde, hasta);
    }
    public DataTable Ottawa(int site_id, int remo_id, int moti_id, int usua_id, string asistencia, DateTime desde, DateTime hasta)
    {
        return tran.Reporte_Ottawa(site_id, remo_id, moti_id, usua_id, asistencia, desde, hasta);
    }
    public DataTable OttawaXMov(DateTime desde, DateTime hasta, string site)
    {
        return tran.Reporte_OttawaXMov(desde, hasta, site);
    }
    public DataTable TrailerUltEstado(int site_id, int trai_id, DateTime desde, DateTime hasta)
    {
        return tran.Reporte_TrailerUltEstado(site_id, trai_id, desde, hasta);
    }
    public DataTable Reporte_TrailerGPS(bool externo, string placa = null, string numero = null, int tipo_id = 0, int tran_id = 0, int site_id = 0)
    {
        return tran.Reporte_TrailerGPS(externo, placa, numero, tipo_id, tran_id, site_id);
    }
    public DataTable Reporte_TrailerGPS(string placa = null, string numero = null, int tipo_id = 0, int tran_id = 0, int site_id = 0)
    {
        return tran.Reporte_TrailerGPS(placa, numero, tipo_id, tran_id, site_id);
    }
    public DataTable Reporte_Entradaysalida(int site_id, DateTime desde, DateTime hasta, String trai_placa, string rut_conductor, int id_transportista)
    {
        return tran.Reporte_Entradaysalida(site_id, desde, hasta, trai_placa, rut_conductor, id_transportista);
    }

    public DataTable Historia_bloqueos(DateTime desde, DateTime hasta)
    {
        return tran.Historia_bloqueos(desde, hasta);
    }
    public DataTable Reporte_KPI(int site_id)
    {
        return tran.Reporte_KPI(site_id);
    }
    public DataTable Reporte_KPIHist(int site_id, DateTime desde, DateTime hasta)
    {
        return tran.Reporte_KPIHist(site_id, desde, hasta);
    }
}

