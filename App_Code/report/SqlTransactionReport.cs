using System;
using System.Collections.Generic;

using System.Web;
using System.Data;

namespace Qanalytics.Data.Access.SqlClient
{
    /// <summary>
    /// Descripción breve de SqlTransactionOrigen
    /// </summary>
    public class SqlTransactionReport
    {
        public SqlTransactionReport()
        {
        }

        #region Report
        internal DataTable Reporte_CargaPreingresoXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[CARGA_PREINGRESO_V2]");
            accesoDatos.AgregarSqlParametro("@ID", id);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_IngresosDescarga(int site_id, int tipo, DateTime desde, DateTime hasta)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_INGRESO]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            accesoDatos.AgregarSqlParametro("@TIIC_ID", tipo);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_Movimientos(int site_id, DateTime desde, DateTime hasta, bool no_ottawa)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_MOVIMIENTOS]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            accesoDatos.AgregarSqlParametro("@no_ottawa", no_ottawa);
            return accesoDatos.EjecutarSqlquery2();
        }

        internal DataTable Reporte_yardtag(int site_id, DateTime desde, DateTime hasta, string placa)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_yardtag]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            accesoDatos.AgregarSqlParametro("@placa", placa);
            return accesoDatos.EjecutarSqlquery2();
        }

        internal DataTable Reporte_yardtag_general(int site_id, DateTime desde, DateTime hasta, string placa, string playa, string zona)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_yardtag_general]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            accesoDatos.AgregarSqlParametro("@placa", placa);
            accesoDatos.AgregarSqlParametro("@id_playa", playa);
            accesoDatos.AgregarSqlParametro("@id_zona", zona);
            return accesoDatos.EjecutarSqlquery2();
        }

        internal DataTable Reporte_yardtag_ult_dato(int site_id, DateTime desde, DateTime hasta, string placa, string playa)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_yardtag_ult_dato]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            accesoDatos.AgregarSqlParametro("@placa", placa);
            accesoDatos.AgregarSqlParametro("@id_playa", playa);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_SolicitudesCarga(int site_id, DateTime desde, DateTime hasta, int estado)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_SOLICITUDES_CARGA]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            if (estado!=0)
                accesoDatos.AgregarSqlParametro("@id_estado", estado);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_SolicitudesCargaDetalle(int soli_id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_SOLICITUDES_CARGA_DETALLE]");
            accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_SolicitudesDescarga(DateTime desde, DateTime hasta, string placa, int site_id,  int play_id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_SOLICITUDES_DESCARGA]");
            if (desde > DateTime.MinValue)
                accesoDatos.AgregarSqlParametro("@DESDE", desde);
            if (hasta > DateTime.MinValue)
                accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            if (!string.IsNullOrEmpty(placa))
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
            if (site_id != 0)
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            if (play_id != 0)
                accesoDatos.AgregarSqlParametro("@PLAY_ID", play_id);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_Devoluciones(DateTime desde, DateTime hasta, int prov_id, int soli_id, string placa, string nro_flota, int site_id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_DEVOLUCIONES]");
            if (desde > DateTime.MinValue)
                accesoDatos.AgregarSqlParametro("@DESDE", desde);
            if (hasta > DateTime.MinValue)
                accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            if (prov_id > 0)
                accesoDatos.AgregarSqlParametro("@PROV_ID", prov_id);
            if (soli_id > 0)
                accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
            if (site_id > 0)
                accesoDatos.AgregarSqlParametro("@site_id", site_id);
            if (!string.IsNullOrEmpty(placa))
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
            if (!string.IsNullOrEmpty(nro_flota))
                accesoDatos.AgregarSqlParametro("@NRO_FLOTA", nro_flota);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_Entrada(int site_id, DateTime desde, DateTime hasta)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_ENTRADA_V2]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_Salida(int site_id, DateTime desde, DateTime hasta)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_SALIDA]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_SalidaLoAguirre(DateTime desde, DateTime hasta)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_SALIDA_LO_AGUIRRE]");
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_Ottawa(int site_id, int remo_id, int moti_id, int usua_id, string asistencia, DateTime desde, DateTime hasta)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_OTTAWA_X_HR]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@remo_id", remo_id);
            accesoDatos.AgregarSqlParametro("@MOTI_ID", moti_id);
            accesoDatos.AgregarSqlParametro("@usua_id", usua_id);
            accesoDatos.AgregarSqlParametro("@FH_INI", desde);
            accesoDatos.AgregarSqlParametro("@FH_FIN", hasta);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_OttawaXMov(DateTime desde, DateTime hasta, string site)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_OTTAWA_X_MOV]");
            accesoDatos.AgregarSqlParametro("@FH_INICIO", desde);
            accesoDatos.AgregarSqlParametro("@FH_FIN", hasta);
            accesoDatos.AgregarSqlParametro("@id_site", site);
            return accesoDatos.EjecutarSqlquery2();
        }


        internal DataTable Historia_bloqueos(DateTime desde, DateTime hasta)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_HISTORIA_BLOQUEOS]");
            accesoDatos.AgregarSqlParametro("@FH_INICIO", desde);
            accesoDatos.AgregarSqlParametro("@FH_FIN", hasta);

            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_TrailerUltEstado(int site_id, int trai_id, DateTime desde, DateTime hasta)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_TRAILER_ULT_ESTADO]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@TRAI_ID", trai_id);
            accesoDatos.AgregarSqlParametro("@FH_DESDE", desde);
            accesoDatos.AgregarSqlParametro("@FH_HASTA", hasta);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_TrailerGPS(bool externo, string placa, string numero, int tipo_id, int tran_id, int site_id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_TRAILER_GPS]");
            accesoDatos.AgregarSqlParametro("@TRAI_EXTERNO", externo);
            if (tipo_id != 0)
                accesoDatos.AgregarSqlParametro("@TRTI_ID", tipo_id);
            if (tran_id != 0)
                accesoDatos.AgregarSqlParametro("@TRAN_ID", tran_id);
            if (!String.IsNullOrEmpty(numero))
                accesoDatos.AgregarSqlParametro("@TRAI_NRO", numero);
            if (!String.IsNullOrEmpty(placa))
                accesoDatos.AgregarSqlParametro("@TRAI_PLACA", placa);
            if (site_id != 0)
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            try
            {
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            
        }
        internal DataTable Reporte_TrailerGPS(string placa, string numero, int tipo_id, int tran_id, int site_id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_TRAILER_GPS]");
            if (tipo_id != 0)
                accesoDatos.AgregarSqlParametro("@TRTI_ID", tipo_id);
            if (tran_id != 0)
                accesoDatos.AgregarSqlParametro("@TRAN_ID", tran_id);
            if (!String.IsNullOrEmpty(numero))
                accesoDatos.AgregarSqlParametro("@TRAI_NRO", numero);
            if (!String.IsNullOrEmpty(placa))
                accesoDatos.AgregarSqlParametro("@TRAI_PLACA", placa);
            if (site_id != 0)
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            try
            {
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            
        }
        internal DataTable Reporte_Entradaysalida(int site_id, DateTime desde, DateTime hasta, String trai_placa, string rut_conductor, int id_transportista)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_ENTRADA_Salida_V2]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            if (!string.IsNullOrEmpty(trai_placa))
                accesoDatos.AgregarSqlParametro("@PLACA", trai_placa);
            if (!string.IsNullOrEmpty(rut_conductor))
                accesoDatos.AgregarSqlParametro("@rut_conductor", rut_conductor);
            if (id_transportista != 0)
                accesoDatos.AgregarSqlParametro("@id_transportista", id_transportista);

            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_KPI(int site_id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_KPI_SITE]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Reporte_KPIHist(int site_id, DateTime desde, DateTime hasta)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_KPI_SITE_HIST]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);
            return accesoDatos.EjecutarSqlquery2();
        }


        #endregion
    }
}