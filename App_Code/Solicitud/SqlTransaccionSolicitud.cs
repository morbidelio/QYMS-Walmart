// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

/// <summary>
/// Descripción breve de SqlTransaccionSolicitud
/// </summary>
namespace Qanalytics.Data.Access.SqlClient
{
    public sealed class SqlTransaccionSolicitud
    {
        readonly public static String STRING_CONEXION = "CsString";
        readonly SqlAccesoDatos accesoDatos = new SqlAccesoDatos(STRING_CONEXION);

        #region Logistica
        internal DataTable TrailerLogi_ObtenerXParametro(string placa, string numero, bool externo, int tipo_id, int tran_id = 0, int site_id = 0)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILERS_LOGISTICA_X_CRITERIO_V2]");
            if (tipo_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_TIPO", tipo_id);
            }
            if (tran_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_TRAN", tran_id);
            }
            if (!String.IsNullOrEmpty(numero))
            {
                accesoDatos.AgregarSqlParametro("@NRO_FLOTA", numero);
            }
            if (!String.IsNullOrEmpty(placa))
            {
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
            }
            if (externo)
            {
                accesoDatos.AgregarSqlParametro("@EXTERNO", externo);
            }
            if (site_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            }
            try
            {
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        internal DataTable TrailerLogi_ObtenerXParametroAdmin(string placa, string numero, bool externo, int tipo_id, int tran_id = 0, int site_id = 0)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILERS_PANEL_CONTROL_X_CRITERIO_V2]");
            if (tipo_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_TIPO", tipo_id);
            }
            if (tran_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_TRAN", tran_id);
            }
            if (!String.IsNullOrEmpty(numero))
            {
                accesoDatos.AgregarSqlParametro("@NRO_FLOTA", numero);
            }
            if (!String.IsNullOrEmpty(placa))
            {
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
            }
            if (externo)
            {
                accesoDatos.AgregarSqlParametro("@EXTERNO", externo);
            }
            if (site_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            }
            try
            {
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        internal DataTable TrailerLogi_ObtenerXParametroDevolucion(string placa, string numero, bool externo, int tipo_id, int tran_id = 0, int site_id = 0)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILERS_DEVOLUCION_X_CRITERIO_V2]");
            if (tipo_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_TIPO", tipo_id);
            }
            if (tran_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_TRAN", tran_id);
            }
            if (!String.IsNullOrEmpty(numero))
            {
                accesoDatos.AgregarSqlParametro("@NRO_FLOTA", numero);
            }
            if (!String.IsNullOrEmpty(placa))
            {
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
            }
            if (externo)
            {
                accesoDatos.AgregarSqlParametro("@EXTERNO", externo);
            }
            if (site_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            }
            try
            {
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        internal DataTable TrailerLogi_ObtenerXParametroDesc(string placa, string numero, bool externo, int tipo_id, int tran_id = 0, int site_id = 0,  int play_id= 0)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILERS_DESCARGA_X_CRITERIO_V2]");
            if (tipo_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_TIPO", tipo_id);
            }
            if (tran_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_TRAN", tran_id);
            }
            if (!String.IsNullOrEmpty(numero))
            {
                accesoDatos.AgregarSqlParametro("@NRO_FLOTA", numero);
            }
            if (!String.IsNullOrEmpty(placa))
            {
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
            }
            if (externo)
            {
                accesoDatos.AgregarSqlParametro("@EXTERNO", externo);
            }
            if (site_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            }
            if (play_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@PLAY_ID", play_id);
            }
       


            try
            {
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
        #region Pallets
        internal bool Pallets_Crear(SolicitudBC s, int luga_id_des, out string error)
        {
            bool exito = true;
            error = "";
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[AGREGA_SOLICITUD_PALLET]");
                this.accesoDatos.AgregarSqlParametro("@SITE_ID", s.ID_SITE);
                this.accesoDatos.AgregarSqlParametro("@USUA_ID", s.ID_USUARIO);
                this.accesoDatos.AgregarSqlParametro("@DOCUMENTO", s.DOCUMENTO);
                this.accesoDatos.AgregarSqlParametro("@OBSERVACION", s.OBSERVACION);
                this.accesoDatos.AgregarSqlParametro("@TRAI_ID", s.ID_TRAILER);
                this.accesoDatos.AgregarSqlParametro("@ID_DESTINO", luga_id_des);
                this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
                SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);
                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                if (string.IsNullOrEmpty(error))
                {
                    exito = true;
                }
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool Pallets_TrasladoEst(int id, int luga_id, int luga_id_dest, int soan_orden, int usua_id, out string error)
        {
            bool exito = true;
            error = "";
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_PALLET_CARGA_COMPLETA_TRASLADO_ESTACIONAMIENTO]");
                this.accesoDatos.AgregarSqlParametro("@SOLI_ID", id);
                this.accesoDatos.AgregarSqlParametro("@LUGA_ID_DES", luga_id_dest);
                this.accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);

                this.accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
                this.accesoDatos.AgregarSqlParametro("@soan_orden", soan_orden);
                this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
                SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);
                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                if (string.IsNullOrEmpty(error))
                {
                    exito = true;
                }
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool Pallets_Reiniciar(int id, int luga_id, int luga_id_dest, int usua_id, int soan_orden, int soan_orden_des, out string error)
        {
            bool exito = true;
            error = "";
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[SOLICITUD_LOGISTICA_PALLET_ANDEN_DESCARGA]");
                this.accesoDatos.AgregarSqlParametro("@SOLI_ID", id);
                this.accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);
                this.accesoDatos.AgregarSqlParametro("@LUGA_ID_DES", luga_id_dest);
                if (soan_orden > 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@soan_orden", soan_orden);
                }
                else
                {
                    this.accesoDatos.AgregarSqlParametro("@soan_orden", DBNull.Value);
                }
                
                this.accesoDatos.AgregarSqlParametro("@soan_orden_des", soan_orden_des);
                this.accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
                this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
                SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);
                paramerror.Size = 9999;
                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                if (string.IsNullOrEmpty(error))
                {
                    exito = true;
                }
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool pallets_Completar(int soli_id, int luga_id, int soan_orden, int usua_id, out string error)
        {
            bool exito = true;
            error = "";
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_PALLET_DESCARGA_COMPLETA]");
                this.accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
                this.accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);
                if (soan_orden > 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@soan_orden", soan_orden);
                }
                else
                {
                    this.accesoDatos.AgregarSqlParametro("@soan_orden", DBNull.Value);
                }
                
                this.accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
                this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
                SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);
                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                if (string.IsNullOrEmpty(error))
                {
                    exito = true;
                }
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        #endregion
        #region Desechos
        internal bool Desechos_Crear(SolicitudBC s, int luga_id_des, out string error)
        {
            bool exito = true;
            error = "";
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[AGREGA_SOLICITUD_DESECHOS]");
                this.accesoDatos.AgregarSqlParametro("@SITE_ID", s.ID_SITE);
                this.accesoDatos.AgregarSqlParametro("@USUA_ID", s.ID_USUARIO);
                this.accesoDatos.AgregarSqlParametro("@DOCUMENTO", s.DOCUMENTO);
                this.accesoDatos.AgregarSqlParametro("@OBSERVACION", s.OBSERVACION);
                this.accesoDatos.AgregarSqlParametro("@TRAI_ID", s.ID_TRAILER);
                this.accesoDatos.AgregarSqlParametro("@ID_DESTINO", luga_id_des);
                this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
                SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);
                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                if (string.IsNullOrEmpty(error))
                {
                    exito = true;
                }
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool Desechos_TrasladoEst(int id, int luga_id, int luga_id_dest, int soan_orden, int usua_id, out string error)
        {
            bool exito = true;
            error = "";
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_DESECHOS_CARGA_COMPLETA_TRASLADO_ESTACIONAMIENTO]");
                this.accesoDatos.AgregarSqlParametro("@SOLI_ID", id);
                this.accesoDatos.AgregarSqlParametro("@LUGA_ID_DES", luga_id_dest);
                this.accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);

                this.accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
                this.accesoDatos.AgregarSqlParametro("@soan_orden", soan_orden);
                this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
                SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);

                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                if (string.IsNullOrEmpty(error))
                {
                    exito = true;
                }
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool Desechos_Reiniciar(int id, int luga_id, int luga_id_dest, int usua_id, int soan_orden, int soan_orden_des, out string error)
        {
            bool exito = true;
            error = "";
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[SOLICITUD_LOGISTICA_DESECHOS_ANDEN_DESCARGA]");
                this.accesoDatos.AgregarSqlParametro("@SOLI_ID", id);
                this.accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);
                this.accesoDatos.AgregarSqlParametro("@LUGA_ID_DES", luga_id_dest);
                if (soan_orden > 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@soan_orden", soan_orden);
                }
                else
                {
                    this.accesoDatos.AgregarSqlParametro("@soan_orden", DBNull.Value);
                }
                
                this.accesoDatos.AgregarSqlParametro("@soan_orden_des", soan_orden_des);
                this.accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
                this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
                SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);
                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                if (string.IsNullOrEmpty(error))
                {
                    exito = true;
                }
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool Desechos_Completar(int soli_id, int luga_id, int soan_orden, int usua_id,  out string error)
        {
            bool exito = true;
            error = "";
            try
            {
               // this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_DESECHOS_DESCARGA_COMPLETA]");
                this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_DESECHOS_CARGA_COMPLETA]");
                
                this.accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
                this.accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);
            //    this.accesoDatos.AgregarSqlParametro("@LUGA_ID_dest", luga_id_dest);
                this.accesoDatos.AgregarSqlParametro("@soan_orden", soan_orden);
                this.accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
                this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
                SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);
                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                if (string.IsNullOrEmpty(error))
                {
                    exito = true;
                }
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        #endregion
        # region descargaLI
        internal bool DescargaLI_Crear(SolicitudBC s, int luga_id_des, out string error)
        {
            bool exito = true;
            error = "";
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[AGREGA_SOLICITUD_DESCARGA_LI]");
                this.accesoDatos.AgregarSqlParametro("@SITE_ID", s.ID_SITE);
                this.accesoDatos.AgregarSqlParametro("@ID_USUARIO", s.ID_USUARIO);
                this.accesoDatos.AgregarSqlParametro("@DOCUMENTO", s.DOCUMENTO);
                this.accesoDatos.AgregarSqlParametro("@OBSERVACION", s.OBSERVACION);
                this.accesoDatos.AgregarSqlParametro("@ID_TRAILER", s.ID_TRAILER);
                this.accesoDatos.AgregarSqlParametro("@ID_DESTINO", luga_id_des);
                this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
                SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);
                paramerror.Size = 9999;
                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                if (string.IsNullOrEmpty(error))
                {
                    exito = true;
                }
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool DescargaLI_Completar(int soli_id, int luga_id, int soan_orden, int usua_id, out string error)
        {
            bool exito = true;
            error = "";
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_DESC_LI_DESCARGADO]");
                this.accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
                //      accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);
                //      accesoDatos.AgregarSqlParametro("@soan_orden", soan_orden);
                this.accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
                this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
                SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);
                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                if (string.IsNullOrEmpty(error))
                {
                    exito = true;
                }
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        #endregion
        #region LugarColorSolicitud
        internal DataTable LugarColorSolicitud_ObtenerTodo()
        {
            return this.accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_LUGAR_COLOR_X_SOLICITUD]").Tables[0];
        }
        internal DataTable LugarColorSolicitud_ObtenerTodo(int soes_id)
        {
            return this.accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_TODO_LUGAR_COLOR_X_SOLICITUD] @SOES_ID = {0}", soes_id)).Tables[0];
        }
        internal DataTable LugarColorSolicitud_ObtenerXSite(int site_id, int soes_id = 0, int soti_id = 0)
        {
            string query = "[dbo].[CARGA_TODO_LUGAR_COLOR_X_SOLICITUD]";
            query += string.Format(" @SITE_ID = {0}", site_id);
            if (soes_id != 0)
            {
                query += string.Format(" @SOES_ID = {0}", soes_id);
            }
            if (soti_id != 0)
            {
                query += string.Format(" @SOTI_ID = {0}", soti_id);
            }
            return this.accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_TODO_LUGAR_COLOR_X_SOLICITUD] @SITE_ID = {0}", site_id)).Tables[0];
        }
        internal bool LugarColorSolicitud_Agregar(DataTable dt, int soes_id)
        {
            bool exito = false;
            this.accesoDatos.CargarSqlComando("[dbo].[AGREGA_LUGAR_COLOR_X_SOLICITUD]");
            foreach (DataRow dr in dt.Rows)
            {
                this.accesoDatos.LimpiarSqlParametros();
                this.accesoDatos.AgregarSqlParametro("@SOES_ID", soes_id);
                this.accesoDatos.AgregarSqlParametro("@SITE_ID", dr["SITE_ID"].ToString());
                this.accesoDatos.AgregarSqlParametro("@COLOR", dr["COLOR"].ToString());
                try
                {
                    this.accesoDatos.EjecutarSqlEscritura();
                    exito = true;
                }
                catch (Exception)
                {
                    exito = false;
                    break;
                }
            }
            return exito;
        }
        #endregion
        #region SolicitudAnden
        internal DataTable SolicitudAnden_SolicitudesCarga()
        {
            return this.accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_SOLICITUDES_ANDENES]").Tables[0];
        }
        internal DataTable SolicitudAnden_SolicitudesCarga(int site_id)
        {
            return this.accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_TODO_SOLICITUDES_ANDENES] @SITE_ID={0}", site_id)).Tables[0];
        }
        internal DataTable SolicitudAnden_SolicitudesCarga(int site_id, int play_id, int luga_id, int soli_id, int soes_id, int tran_id,string ruta_id = null)
        {
            this.accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SOLICITUD_CARGA]");
            DataTable dt = new DataTable();
            try
            {
                if (site_id != 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@ID_SITE", site_id);
                }
                if (play_id != 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@PLAYA_ID", play_id);
                }
                if (luga_id != 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@ANDEN_ID", luga_id);
                }
                if (soli_id != 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
                }
                if (soes_id != 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@SOLI_estado", soes_id);
                }
                if (tran_id != 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@transportista", tran_id);
                }
                if (!string.IsNullOrEmpty(ruta_id))
                {
                    this.accesoDatos.AgregarSqlParametro("@ruta_id", ruta_id);
                }
                dt = this.accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception)
            {
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
                this.accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal DataTable SolicitudAnden_ObtenerXSolicitudId(int id)
        {
            return this.accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_ANDENES_X_SOLICITUD_ID] {0}", id)).Tables[0];
        }
        internal DataTable ObtenerAndenesXNewWays(string id_trailer, out string resultado)
        {
            this.accesoDatos.CargarSqlComando("[dbo].[CARGA_ANDENES_X_NEW_SOLICITUD_WAYS]");
            resultado = "";
            DataTable dt = new DataTable();
            this.accesoDatos.AgregarSqlParametro("@id_trailer", id_trailer);
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;
            try
            {
                dt= this.accesoDatos.EjecutarSqlquery2();
                resultado = param.Value.ToString();
             //   exito = true;
            }
            catch (Exception ex)
            {
               // exito = false;
                throw (ex);
            }
            return dt;

        }
        internal bool SolicitudAnden_InterrumpirCarga(SolicitudAndenesBC anden, int id_usuario, out string resultado)
        {
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            resultado = "";
            this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_CARGA_ANDEN_CARGA_PARCIAL]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", anden.SOLI_ID);
            this.accesoDatos.AgregarSqlParametro("@LUGA_ID", anden.LUGA_ID);
            this.accesoDatos.AgregarSqlParametro("@cant_pallet", anden.PALLETS_CARGADOS);
            this.accesoDatos.AgregarSqlParametro("@SOAN_ORDEN", anden.SOAN_ORDEN);
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", id_usuario);
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool SolicitudAnden_ReanudarCarga(DataSet ds, int id_usuario, out string resultado) //int nuevo_lugar,int id_usuario)
        {
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            resultado = "";
            this.accesoDatos.CargarSqlComando("[dbo].[REANUDAR_SOLICITUD_CARGA]");
            this.accesoDatos.AgregarSqlParametro("@ANDENES", ds.Tables["ANDENES"]);
            this.accesoDatos.AgregarSqlParametro("@LOCALES", ds.Tables["LOCALES"]);
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", id_usuario);
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool SolicitudAnden_ReanudarCarga(int soli_id, int id_usuario, out string resultado) //int nuevo_lugar,int id_usuario)
        {
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            resultado = "";
            this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_REINICIAR_CARGA]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", id_usuario);
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            //accesoDatos.AgregarSqlParametro("@ID_LUGAR", anden.ID_LUGAR);
            //if (nuevo_lugar != 0)
            //    accesoDatos.AgregarSqlParametro("@ID_NUEVOLUGAR", nuevo_lugar);
            //accesoDatos.AgregarSqlParametro("@ORDEN_ANDEN", anden.ORDEN);

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool SolicitudAnden_colocar_sello(int id_solicitud, int id_usuario, out string resultado) //int nuevo_lugar,int id_usuario)
        {
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            resultado = "";
            this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_ESTADO_SELLO_VALIDAR]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", id_solicitud);
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", id_usuario);
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            //accesoDatos.AgregarSqlParametro("@ID_LUGAR", anden.ID_LUGAR);
            //if (nuevo_lugar != 0)
            //    accesoDatos.AgregarSqlParametro("@ID_NUEVOLUGAR", nuevo_lugar);
            //accesoDatos.AgregarSqlParametro("@ORDEN_ANDEN", anden.ORDEN);

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool SolicitudAnden_validado_sello(int id_solicitud, int id_usuario, out string resultado) //int nuevo_lugar,int id_usuario)
        {
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            resultado = "";
            this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_ESTADO_SELLO_VALIDAdo]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", id_solicitud);
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", id_usuario);
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            //accesoDatos.AgregarSqlParametro("@ID_LUGAR", anden.ID_LUGAR);
            //if (nuevo_lugar != 0)
            //    accesoDatos.AgregarSqlParametro("@ID_NUEVOLUGAR", nuevo_lugar);
            //accesoDatos.AgregarSqlParametro("@ORDEN_ANDEN", anden.ORDEN);

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool SolicitudAnden_Eliminar(string andenes)
        {
            bool exito = false;
            this.accesoDatos.LimpiarSqlParametros();
            this.accesoDatos.CargarSqlComando("[dbo].[ELIMINA_SOLICITUD_ANDENES]");
            this.accesoDatos.AgregarSqlParametro("@ANDENES", andenes);

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool SolicitudAnden_Agregar(DataTable dt)
        {
            bool exito = false;
            this.accesoDatos.LimpiarSqlParametros();
            this.accesoDatos.CargarSqlComando("[dbo].[EDITA_SOLICITUD_ANDEN]");
            this.accesoDatos.AgregarSqlParametro("@ANDENES", dt);
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool SolicitudAnden_Agregar(SolicitudAndenesBC solAnden)
        {
            bool exito = false;
            this.accesoDatos.LimpiarSqlParametros();
            this.accesoDatos.CargarSqlComando("[dbo].[AGREGA_SOLICITUD_ANDENES_V2]");
            this.accesoDatos.AgregarSqlParametro("@ID_SOLICITUD", solAnden.SOLI_ID);
            this.accesoDatos.AgregarSqlParametro("@ID_LUGAR", solAnden.LUGA_ID);
            this.accesoDatos.AgregarSqlParametro("@MIN_CARGA_EST", solAnden.MINS_CARGA_EST);
            this.accesoDatos.AgregarSqlParametro("@SOAN_ORDEN", solAnden.SOAN_ORDEN);
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool SolicitudAnden_Agregar(SolicitudAndenesBC solAnden, out int soan_orden)
        {
            bool exito = false;
            this.accesoDatos.LimpiarSqlParametros();
            this.accesoDatos.CargarSqlComando("[dbo].[AGREGA_SOLICITUD_ANDENES]");
            this.accesoDatos.AgregarSqlParametro("@ID_SOLICITUD", solAnden.SOLI_ID);
            this.accesoDatos.AgregarSqlParametro("@ID_LUGAR", solAnden.LUGA_ID);
            this.accesoDatos.AgregarSqlParametro("@MIN_CARGA_EST", solAnden.MINS_CARGA_EST);
            this.accesoDatos.AgregarSqlParametro("@SOAN_ORDEN", SqlDbType.Int);
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            soan_orden = (int)this.accesoDatos.obtenerValorParametro("@SOAN_ORDEN");
            return exito;
        }
        internal int SolicitudAnden_ObtenerPlayaId(int soli_id)
        {
            int play_id = 0;
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[CARGA_PLAYA_SOLICITUD]");
                this.accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
                this.accesoDatos.EjecutarSqlLector();
                while (this.accesoDatos.SqlLectorDatos.Read())
                {
                    play_id = int.Parse(this.accesoDatos.SqlLectorDatos["PLAY_ID"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return play_id;
        }
        internal bool SolicitudAnden_CompletarCarga(SolicitudAndenesBC anden, int usua_id, out string resultado)
        {
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            resultado = "";
            this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_CARGA_ANDEN_CARGA_COMPLETA]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", anden.SOLI_ID);
            this.accesoDatos.AgregarSqlParametro("@LUGA_ID", anden.LUGA_ID);
            //     accesoDatos.AgregarSqlParametro("@HORA_FIN_CARGA", anden.FECHA_CARGA_FIN);
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
            this.accesoDatos.AgregarSqlParametro("@SOAN_ORDEN", anden.SOAN_ORDEN);
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                if (string.IsNullOrEmpty(resultado))
                {
                    exito = true;
                }
            }
            catch (Exception ex)
            {
                exito = false;
                resultado = ex.Message;
                throw (ex);
            }
            return exito;
        }
        internal bool SolicitudAnden_CancelarCarga(SolicitudAndenesBC anden, int usua_id, out string resultado)
        {
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            resultado = "";
            this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_CARGA_ANDEN_CANCELADO]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", anden.SOLI_ID);
            this.accesoDatos.AgregarSqlParametro("@LUGA_ID", anden.LUGA_ID);
            //     accesoDatos.AgregarSqlParametro("@HORA_FIN_CARGA", anden.FECHA_CARGA_FIN);
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
            this.accesoDatos.AgregarSqlParametro("@SOAN_ORDEN", anden.SOAN_ORDEN);
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                if (string.IsNullOrEmpty(resultado))
                {
                    exito = true;
                }
            }
            catch (Exception ex)
            {
                exito = false;
                resultado = ex.Message;
                throw (ex);
            }
            return exito;
        }
        internal DataTable SolicitudAnden_ObtenerBloqueados(int soli_id)
        {
            return this.accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[prcSOLICITUD_DESCARGA_ANDENES_BLOQUEADOS_LISTAR] {0}", soli_id)).Tables[0];
        }
        internal bool SolicitudAnden_Bloquear(SolicitudAndenesBC s, int usua_id, out string resultado)
        {
            bool exito = false;
            resultado = "";
            this.accesoDatos.CargarSqlComando("[dbo].[BLOQUEAR_anden_Solicitud_descarga]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", s.SOLI_ID);
            this.accesoDatos.AgregarSqlParametro("@LUGA_ID", s.LUGA_ID);
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
            this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
            SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", resultado);
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = paramerror.Value.ToString();
                if (string.IsNullOrEmpty(resultado))
                {
                    exito = true;
                    resultado = "Todo OK";
                }
            }
            catch (Exception ex)
            {
                exito = false;
                resultado = ex.Message;
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal bool SolicitudAnden_Liberar(int soli_id, int luga_id, int usua_id, out string resultado)
        {
            bool exito = false;
            resultado = "";
            this.accesoDatos.CargarSqlComando("[dbo].[Liberar_anden_Solicitud_descarga]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
            this.accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
            this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
            SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", resultado);
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = paramerror.Value.ToString();
                if (string.IsNullOrEmpty(resultado))
                {
                    exito = true;
                    resultado = "Todo OK";
                }
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal int SolicitudAnden_ObtenerUltimoOrden(int soli_id)
        {
            int orden = 0;
            this.accesoDatos.CargarSqlComando("[dbo].[CARGA_ULTIMO_SOAN_ORDEN]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
            try
            {
                this.accesoDatos.EjecutarSqlLector();
                orden = Convert.ToInt32(this.accesoDatos.SqlLectorDatos["SOAN_ORDEN"]);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
            }
            return orden;
        }
        internal DataSet SolicitudAnden_ObtenerTodo(int soli_id, int soan_orden, int loca_id)
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SOLICITUD_ANDEN]");
                if (soli_id != 0)
                    accesoDatos.AgregarSqlParametro("@soli_id", soli_id);
                if (soan_orden != 0)
                    accesoDatos.AgregarSqlParametro("@soan_orden", soan_orden);
                if (loca_id != 0)
                    accesoDatos.AgregarSqlParametro("@loca_id", loca_id);
                DataSet ds = accesoDatos.RetornaDS();
                ds.Tables[0].TableName = "ANDENES";
                ds.Tables[1].TableName = "LOCALES";
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
        }
        internal DataSet SolicitudAnden_ObtenerFinalizados(int soli_id, int soan_orden, int loca_id)
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_SOLICITUD_ANDEN_FINALIZADA]");
                if (soli_id != 0)
                    accesoDatos.AgregarSqlParametro("@soli_id", soli_id);
                if (soan_orden != 0)
                    accesoDatos.AgregarSqlParametro("@soan_orden", soan_orden);
                if (loca_id != 0)
                    accesoDatos.AgregarSqlParametro("@loca_id", loca_id);
                DataSet ds = accesoDatos.RetornaDS();
                ds.Tables[0].TableName = "ANDENES";
                ds.Tables[1].TableName = "LOCALES";
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
        }
        internal SolicitudAndenesBC SolicitudAnden_ObtenerXId(int soli_id, int soan_orden)
        {
            SolicitudAndenesBC sa = new SolicitudAndenesBC();
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[CARGA_SOLICITUD_ANDENES_X_ID_V2]");
                this.accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
                this.accesoDatos.AgregarSqlParametro("@SOAN_ORDEN", soan_orden);
                this.accesoDatos.EjecutarSqlLector();
                while (this.accesoDatos.SqlLectorDatos.Read())
                {
                    sa = this.cargarDatosSolicitudAnden(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return sa;
        }
        internal bool SolicitudAnden_Emergencia(DataTable dt, DataTable dt2,  int id_usuario ,out string mensaje)
        {
            bool exito = false;
            mensaje = "";
            this.accesoDatos.CargarSqlComando("[dbo].EDITA_SOLICITUD_EMERGENCIA");
        this.accesoDatos.AgregarSqlParametro("@LOCALES", dt);

        this.accesoDatos.AgregarSqlParametro("@ANDENES", dt2);
        this.accesoDatos.AgregarSqlParametro("@id_usuario", id_usuario);    
        SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", mensaje);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;


            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                mensaje = param.Value.ToString();

                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                mensaje = ex.Message.ToString();
                throw (ex);
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal bool SolicitudAnden_Locales(DataTable dt)
        {
            bool exito = false;
            this.accesoDatos.CargarSqlComando("[dbo].[EDITA_SOLICITUD_LOCAL]");
            this.accesoDatos.AgregarSqlParametro("@LOCALES", dt);
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        private SolicitudAndenesBC cargarDatosSolicitudAnden(SqlDataReader reader)
        {
            SolicitudAndenesBC sa = new SolicitudAndenesBC();
            sa.SOLI_ID = Convert.ToInt32(reader["SOLI_ID"]);
            sa.LUGA_ID = Convert.ToInt32(reader["LUGA_ID"]);
            sa.PLAY_ID = Convert.ToInt32(reader["PLAY_ID"]);
            sa.LUGAR = reader["LUGA_ID"].ToString();
            sa.SOAN_ORDEN = Convert.ToInt32(reader["SOAN_ORDEN"]);
            if (reader["TIEMPO_ESTIMADO"] != DBNull.Value)
            {
                sa.MINS_CARGA_EST = Convert.ToInt32(reader["TIEMPO_ESTIMADO"]);
            }
            if (reader["TIEMPO_REAL"] != DBNull.Value)
            {
                sa.TIEMPO_CARGA_REAL = Convert.ToInt32(reader["TIEMPO_REAL"]);
            }
            if (reader["TIEMPO_ESTADIA"] != DBNull.Value)
            {
                sa.TIEMPO_ESTADIA = Convert.ToInt32(reader["TIEMPO_ESTADIA"]);
            }
            if (reader["FECHA_ARRIBO"] != DBNull.Value)
            {
                sa.FECHA_ARRIBO = Convert.ToDateTime(reader["FECHA_ARRIBO"]);
            }
            if (reader["FECHA_FIN_CARGA"] != DBNull.Value)
            {
                sa.FECHA_CARGA_FIN = Convert.ToDateTime(reader["FECHA_FIN_CARGA"]);
            }
            if (reader["FECHA_SALIDA"] != DBNull.Value)
            {
                sa.FECHA_SALIDA = Convert.ToDateTime(reader["FECHA_SALIDA"]);
            }
            sa.ID_SOES = Convert.ToInt32(reader["SOES_ID"]);
            sa.PALLETS_CARGADOS = Convert.ToInt32(reader["PALLETS"]);
            sa.LOCALES = reader["LOCALES"].ToString();
            sa.CARACTERISTICAS= reader["CARACTERISTICAS"].ToString();
            if (reader["TIMESTAMP"] != DBNull.Value)
            {
                sa.TIMESTAMP = Convert.ToDateTime(reader["TIMESTAMP"]);
            }
            return sa;
        }
        #endregion
        #region SolicitudLocales
        internal bool SolicitudLocales_Crear(SolicitudLocalesBC solLocal)
        {
            bool exito = false;
            this.accesoDatos.LimpiarSqlParametros();
            this.accesoDatos.CargarSqlComando("[dbo].[AGREGA_SOLICITUD_LOCAL_DESTINO_V3]");
            this.accesoDatos.AgregarSqlParametro("@ID_SOLICITUD", solLocal.SOLI_ID);
            this.accesoDatos.AgregarSqlParametro("@ID_LOCAL", solLocal.LOCA_ID);
            this.accesoDatos.AgregarSqlParametro("@ID_LUGAR", solLocal.LUGA_ID);
            this.accesoDatos.AgregarSqlParametro("@PALLETS", solLocal.PALLETS);
            this.accesoDatos.AgregarSqlParametro("@SOAN_ORDEN", solLocal.SOAN_ORDEN);
            this.accesoDatos.AgregarSqlParametro("@SOLD_ORDEN", solLocal.SOLD_ORDEN);
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool SolicitudLocales_Eliminar(SolicitudLocalesBC s)
        {
            bool exito = false;
            this.accesoDatos.LimpiarSqlParametros();
            this.accesoDatos.CargarSqlComando("[dbo].[ELIMINA_SOLICITUD_LOCAL]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", s.SOLI_ID);
            this.accesoDatos.AgregarSqlParametro("@LUGA_ID", s.LUGA_ID);
            this.accesoDatos.AgregarSqlParametro("@SOAN_ORDEN", s.SOAN_ORDEN);
            this.accesoDatos.AgregarSqlParametro("@LOCA_ID", s.LOCA_ID);
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal DataTable SolicitudLocales_XSolicitudTrai(int id,  string viaje, int site_id)
        {
            this.accesoDatos.CargarSqlComando("[dbo].[OBTENER_LOCALES_X_SOLICITUD_TRAILER_V2]");
            this.accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            if (id != 0)
                this.accesoDatos.AgregarSqlParametro("@TRAI_ID", id);
            if (viaje!="")
                this.accesoDatos.AgregarSqlParametro("@VIAJE_ID", viaje);
           
            try
            {
              return   this.accesoDatos.EjecutarSqlquery2();

            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
            }
         
        
        }
        #endregion
        #region Solicitud
        internal DataTable Solicitud_CargaExcel(DataTable dt, int usua_id)
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[AGREGA_SOLICITUDES_EXCEL]");
                accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
                accesoDatos.AgregarSqlParametro("@EXCEL", dt);
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
        }
        internal bool Solicitud_ProcesarExcel(string cod_interno, int usua_id, out string msj)
        {
            msj = "";
            bool exito = false;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[PROCESAR_INTEGRA_EXCEL_SOLICITUDES]");
                accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
                accesoDatos.AgregarSqlParametro("@COD_INTERNO", cod_interno);
                accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
                SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", msj);
                param.Direction = ParameterDirection.Output;
                param.Size = 1000;
                accesoDatos.EjecutarSqlEscritura();
                msj = param.Value.ToString();
                exito = string.IsNullOrEmpty(msj);
                return exito;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
        }
        internal bool Solicitud_Carga(SolicitudBC s, DataSet ds, out string mensaje) //Solicitud Carga
        {
            bool exito = false;
            mensaje = "";
            accesoDatos.CargarSqlComando("[dbo].[EDITA_SOLICITUD_CARGA_V2]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", s.ID_SITE);
            accesoDatos.AgregarSqlParametro("@USUA_ID", s.ID_USUARIO);
            accesoDatos.AgregarSqlParametro("@FH_CREACION", s.FECHA_CREACION);
            accesoDatos.AgregarSqlParametro("@FH_PLAN_ANDEN", s.FECHA_PLAN_ANDEN);
            accesoDatos.AgregarSqlParametro("@OBSERVACION", s.OBSERVACION);
            accesoDatos.AgregarSqlParametro("@TETR_ID", s.TETR_ID);
            accesoDatos.AgregarSqlParametro("@PALLETS", s.Pallets);
            accesoDatos.AgregarSqlParametro("@CACA_ID", s.CARACTERISTICAS);
            accesoDatos.AgregarSqlParametro("@RUTA", s.RUTA);
            accesoDatos.AgregarSqlParametro("@ANDENES", ds.Tables["ANDENES"]);
            accesoDatos.AgregarSqlParametro("@LOCALES", ds.Tables["LOCALES"]);
            accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
            if (s.ID_TRAILER_RESERVADO != 0)
                accesoDatos.AgregarSqlParametro("@TRAI_ID", s.ID_TRAILER_RESERVADO);
            if (s.ID_SHORTECK != "0" && s.ID_SHORTECK != "")
                accesoDatos.AgregarSqlParametro("@SHOR_ID", s.ID_SHORTECK);
            SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", mensaje);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                mensaje = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool Solicitud_Carga(SolicitudBC solicitud, out int soli_id, out string mensaje) //Solicitud Carga
        {
            bool exito = false;
            mensaje = "";
            this.accesoDatos.CargarSqlComando("[dbo].[AGREGA_SOLICITUD_CARGA_V2]");
            this.accesoDatos.AgregarSqlParametro("@SITE_ID", solicitud.ID_SITE);
            this.accesoDatos.AgregarSqlParametro("@ID_USUARIO", solicitud.ID_USUARIO);
            this.accesoDatos.AgregarSqlParametro("@FECHA_CREACION", solicitud.FECHA_CREACION);
            this.accesoDatos.AgregarSqlParametro("@FECHA_PLAN_ANDEN", solicitud.FECHA_PLAN_ANDEN);
            this.accesoDatos.AgregarSqlParametro("@DOCUMENTO", solicitud.DOCUMENTO);
            this.accesoDatos.AgregarSqlParametro("@OBSERVACION", solicitud.OBSERVACION);
            if (solicitud.ID_TRAILER_RESERVADO != 0 && solicitud.ID_TRAILER_RESERVADO != null)
            {
                this.accesoDatos.AgregarSqlParametro("@ID_TRAILER", solicitud.ID_TRAILER_RESERVADO);
            }
            this.accesoDatos.AgregarSqlParametro("@TETR_ID", solicitud.TETR_ID);
            this.accesoDatos.AgregarSqlParametro("@pallets", solicitud.Pallets);
            this.accesoDatos.AgregarSqlParametro("@caracteristica", solicitud.CARACTERISTICAS);
            this.accesoDatos.AgregarSqlParametro("@RUTA", solicitud.RUTA);
            if (solicitud.ID_SHORTECK != "0" && solicitud.ID_SHORTECK != "")
            {
                this.accesoDatos.AgregarSqlParametro("@SHOR_ID", solicitud.ID_SHORTECK);
            }
            this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", mensaje);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;
            try
            {
                this.accesoDatos.EjecutaSqlInsertIdentity();
                soli_id = this.accesoDatos.ID;
                solicitud.SOLI_ID = this.accesoDatos.ID;
                mensaje = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal bool Solicitud_CargaModificar(SolicitudBC solicitud, out string mensaje) //Solicitud Carga
        {
            bool exito = false;
            mensaje = "";
            this.accesoDatos.CargarSqlComando("[dbo].[MODIFICA_SOLICITUD_CARGA]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", solicitud.SOLI_ID);
            this.accesoDatos.AgregarSqlParametro("@pallets", solicitud.Pallets);
            this.accesoDatos.AgregarSqlParametro("@caracteristicas", solicitud.CARACTERISTICAS);
            this.accesoDatos.AgregarSqlParametro("@TEMPERATURA", solicitud.TETR_ID);
            if (solicitud.ID_TRAILER_RESERVADO != 0)
            {
                this.accesoDatos.AgregarSqlParametro("@ID_TRAILER", solicitud.ID_TRAILER_RESERVADO);
            }
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", solicitud.ID_USUARIO);
            this.accesoDatos.AgregarSqlParametro("@RUTA", solicitud.RUTA);
            if (solicitud.ID_SHORTECK != "0")
            {
                this.accesoDatos.AgregarSqlParametro("@SHOR_ID", solicitud.ID_SHORTECK);
            }
            else
            {
                this.accesoDatos.AgregarSqlParametro("@SHOR_ID", DBNull.Value);
            }
            this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", mensaje);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                mensaje = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal bool Solicitud_CargaModificar(SolicitudBC solicitud, DataSet ds, out string mensaje) //Solicitud Carga
        {
            bool exito = false;
            mensaje = "";
            this.accesoDatos.CargarSqlComando("[dbo].[EDITA_SOLICITUD_CARGA_V2]");
            this.accesoDatos.AgregarSqlParametro("@SITE_ID", solicitud.ID_SITE);
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", solicitud.SOLI_ID);
            this.accesoDatos.AgregarSqlParametro("@pallets", solicitud.Pallets);
            this.accesoDatos.AgregarSqlParametro("@CACA_ID", solicitud.CARACTERISTICAS);
            this.accesoDatos.AgregarSqlParametro("@TETR_ID", solicitud.TETR_ID);
            if (solicitud.ID_TRAILER_RESERVADO != 0)
            {
                this.accesoDatos.AgregarSqlParametro("@TRAI_ID", solicitud.ID_TRAILER_RESERVADO);
            }
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", solicitud.ID_USUARIO);
            this.accesoDatos.AgregarSqlParametro("@RUTA", solicitud.RUTA);
            if (solicitud.ID_SHORTECK != "0")
            {
                this.accesoDatos.AgregarSqlParametro("@SHOR_ID", solicitud.ID_SHORTECK);
            }
            this.accesoDatos.AgregarSqlParametro("@ANDENES", ds.Tables["ANDENES"]);
            this.accesoDatos.AgregarSqlParametro("@LOCALES", ds.Tables["LOCALES"]);
            this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", mensaje);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                mensaje = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal bool Solicitud_Descarga(SolicitudBC solicitud, string bloqueados, out string resultado) //Solicitud Descarga
        {
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            bool errorint = false;
            resultado = "";
            this.accesoDatos.CargarSqlComando("[dbo].[AGREGA_SOLICITUD_DESCARGA]");
            this.accesoDatos.AgregarSqlParametro("@SITE_ID", solicitud.ID_SITE);
            this.accesoDatos.AgregarSqlParametro("@ID_USUARIO", solicitud.ID_USUARIO);
            //       accesoDatos.AgregarSqlParametro("@FECHA_CREACION", solicitud.FECHA_CREACION);
            //       accesoDatos.AgregarSqlParametro("@FECHA_PLAN_ANDEN", solicitud.FECHA_PLAN_ANDEN);
            this.accesoDatos.AgregarSqlParametro("@DOCUMENTO", solicitud.DOCUMENTO);
            this.accesoDatos.AgregarSqlParametro("@OBSERVACION", solicitud.OBSERVACION);
            this.accesoDatos.AgregarSqlParametro("@ID_TRAILER", solicitud.ID_TRAILER);
            //accesoDatos.AgregarSqlParametro("@ID_ORIGEN", solicitud.ID_ORIGEN);
            this.accesoDatos.AgregarSqlParametro("@ID_DESTINO", solicitud.ID_DESTINO);
            this.accesoDatos.AgregarSqlParametro("@BLOQUEADOS", bloqueados);

            //accesoDatos.AgregarSqlParametro("@TEMPERATURA", 0);
            this.accesoDatos.AgregarSqlParametro("@pallets", DBNull.Value);
            //         accesoDatos.AgregarSqlParametro("@caracteristicas", "");
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            this.accesoDatos.AgregarSqlParametro("@error", errorint).Direction = ParameterDirection.Output;

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }

            return exito;
        }
        internal bool Solicitud_pallet(SolicitudBC solicitud, out string resultado) //Solicitud Descarga
        {
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            bool errorint = false;
            resultado = "";
            this.accesoDatos.CargarSqlComando("[dbo].[AGREGA_SOLICITUD_pallet]");
            this.accesoDatos.AgregarSqlParametro("@SITE_ID", solicitud.ID_SITE);
            this.accesoDatos.AgregarSqlParametro("@ID_USUARIO", solicitud.ID_USUARIO);
            //       accesoDatos.AgregarSqlParametro("@FECHA_CREACION", solicitud.FECHA_CREACION);
            //       accesoDatos.AgregarSqlParametro("@FECHA_PLAN_ANDEN", solicitud.FECHA_PLAN_ANDEN);
            this.accesoDatos.AgregarSqlParametro("@DOCUMENTO", solicitud.DOCUMENTO);
            this.accesoDatos.AgregarSqlParametro("@OBSERVACION", solicitud.OBSERVACION);
            this.accesoDatos.AgregarSqlParametro("@ID_TRAILER", solicitud.ID_TRAILER);
            //accesoDatos.AgregarSqlParametro("@ID_ORIGEN", solicitud.ID_ORIGEN);
            this.accesoDatos.AgregarSqlParametro("@ID_DESTINO", solicitud.ID_DESTINO);
            this.accesoDatos.AgregarSqlParametro("@ID_DESTINO_PALLET", solicitud.ID_DESTINO_PALLET);
            //accesoDatos.AgregarSqlParametro("@TEMPERATURA", 0);
            this.accesoDatos.AgregarSqlParametro("@pallets", DBNull.Value);
            //         accesoDatos.AgregarSqlParametro("@caracteristicas", "");
            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            this.accesoDatos.AgregarSqlParametro("@error", errorint).Direction = ParameterDirection.Output;
            exito = false;
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                if (resultado == "")
                {
                    exito = true;
                }
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }

            return exito;
        }
        internal bool Solicitud_DescargaModifica(SolicitudBC solicitud) //Solicitud Descarga
        {
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            this.accesoDatos.CargarSqlComando("[dbo].[MODIFICA_SOLICITUD_DESCARGA]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", solicitud.SOLI_ID);
            this.accesoDatos.AgregarSqlParametro("@OBSERVACION", solicitud.OBSERVACION);
            this.accesoDatos.AgregarSqlParametro("@ID_LUGA_MOD", solicitud.ID_DESTINO);
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool Solicitud_DescargaMovimiento(MovimientoBC movimiento, int site_id, int usua_id, out string resultado)
        {
            bool exito = false;
            bool errorint = false;
            resultado = "";
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[MOVIMIENTO_INTERNO_SOLICITUD_DESCARGA]");
                this.accesoDatos.AgregarSqlParametro("@ID_DESTINO", movimiento.ID_DESTINO);
                this.accesoDatos.AgregarSqlParametro("@ID_TRAILER", movimiento.ID_TRAILER);
                this.accesoDatos.AgregarSqlParametro("@ID_SITE", site_id);
                this.accesoDatos.AgregarSqlParametro("@movi_OBS", movimiento.OBSERVACION);
                this.accesoDatos.AgregarSqlParametro("@usua_id", usua_id);
                this.accesoDatos.AgregarSqlParametro("@SOLI_ID", movimiento.ID_SOLICITUD);
                SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
                param.Direction = ParameterDirection.Output;
                param.Size = 1000;

                this.accesoDatos.AgregarSqlParametro("@error", errorint).Direction = ParameterDirection.Output;

                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                if (!string.IsNullOrEmpty(resultado))
                {
                    exito = true;
                }
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal DataTable Solicitud_Estados()
        {
            return this.accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_SOLICITUD_ESTADOS]").Tables[0];
        }
        internal DataTable Solicitud_EstadosCarga()
        {
            return this.accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_ESTADOS_SOLICITUD_CARGA]").Tables[0];
        }
        internal DataTable Solicitud_EstadosCarga_reporte()
        {
            return this.accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_ESTADOS_SOLICITUD_CARGA_reporte]").Tables[0];
        }
        internal DataTable Solicitud_SolicitudesDescarga(int site_id, int tran_id, int luga_id)
        {
            this.accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SOLICITUD_DESCARGA]");
            DataTable dt = new DataTable();
            try
            {
                if (site_id != 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@ID_SITE", site_id);
                }
                if (tran_id != 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@ID_TRAN", tran_id);
                }
                if (luga_id != 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@ID_LUGAR", luga_id);
                }
                dt = this.accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception)
            {
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
                this.accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal DataTable Solicitud_CargaSolicitudesPallets(int site_id, int tran_id, int luga_id)
        {
            this.accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SOLICITUD_PALLETS]");
            DataTable dt = new DataTable();
            try
            {
                if (site_id != 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@ID_SITE", site_id);
                }
                if (tran_id != 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@ID_TRAN", tran_id);
                }
                if (luga_id != 0)
                {
                    this.accesoDatos.AgregarSqlParametro("@ID_LUGAR", luga_id);
                }
                dt = this.accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception)
            {
            }
            finally
            {
                this.accesoDatos.LimpiarSqlParametros();
                this.accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal DataTable Solicitud_ObtenerInconsistentes()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_SOLICITUDES_INCONSISTENTES]").Tables[0];
        }
        internal bool Solicitud_CargaPallets(int soli_id, int luga_id, out string resultado, int usua_id)
        {
            this.accesoDatos.LimpiarSqlParametros();
            resultado = "";
            bool exito = false;
            this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_PALLET_CARGA_COMPLETA]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
            this.accesoDatos.AgregarSqlParametro("@SOAN_ORDEN", 1);
            this.accesoDatos.AgregarSqlParametro("@usua_id", usua_id);
            this.accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);

            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool Solicitud_DescargaPallets(int soli_id, int luga_id, out string resultado, int usua_id)
        {
            this.accesoDatos.LimpiarSqlParametros();
            resultado = "";
            bool exito = false;
            this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_PALLET_DESCARGA_COMPLETA]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", soli_id);
            this.accesoDatos.AgregarSqlParametro("@SOAN_ORDEN", 2);
            this.accesoDatos.AgregarSqlParametro("@usua_id", usua_id);
            this.accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);

            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            exito = false;
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                if (resultado == "")
                {
                    exito = true;
                }
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool Solicitud_CompletarDescarga(int id, int luga_id, out string resultado, int usua_id)
        {
            this.accesoDatos.LimpiarSqlParametros();
            resultado = "";
            bool exito = false;
            this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_ESTADO_DESCARGA_COMPLETA]");
            this.accesoDatos.AgregarSqlParametro("@SOLI_ID", id);
            this.accesoDatos.AgregarSqlParametro("@usua_id", usua_id);
            if (luga_id != 0)
            {
                this.accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);
            }

            SqlParameter param = this.accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                if (string.IsNullOrEmpty(resultado))
                {
                    exito = true;
                }
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool Solicitud_andenListo(int id, int soan_orden, int Luga_id, int id_user, out string error)
        {
            error = "";
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            this.accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_CARGA_ANDEN_CARGA_INICIAR]");
            this.accesoDatos.AgregarSqlParametro("@soli_ID", id);
            
            this.accesoDatos.AgregarSqlParametro("@Luga_id", Luga_id);
            this.accesoDatos.AgregarSqlParametro("@soan_orden", soan_orden);
            this.accesoDatos.AgregarSqlParametro("@usua_id", id_user);
            this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
            SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool Solicitud_Eliminar(int id, int id_user, out string error)
        {
            error = "";
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            this.accesoDatos.CargarSqlComando("[dbo].[ELIMINA_SOLICITUD_CARGA]");
            this.accesoDatos.AgregarSqlParametro("@soli_ID", id);
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", id_user);
            this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
            SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool Solicitud_encender_termo(int id, int id_user, out string error)
        {
            error = "";
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            this.accesoDatos.CargarSqlComando("[dbo].[ENCENDER_TERMO]");
            this.accesoDatos.AgregarSqlParametro("@soli_ID", id);
            this.accesoDatos.AgregarSqlParametro("@USUA_ID", id_user);
            this.accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
            SqlParameter paramerror = this.accesoDatos.AgregarSqlParametroOUT("@ERROR_MSG", error);

            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                error = paramerror.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool Solicitud_EliminarInconsistentes(int id)
        {
            this.accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            this.accesoDatos.CargarSqlComando("[dbo].[ELIMINA_SOLICITUDES_INCONSISTENTES]");
            this.accesoDatos.AgregarSqlParametro("@ID", id);
            try
            {
                this.accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal SolicitudBC Solicitud_ObtenerXId(int id)
        {
            SolicitudBC solicitud = new SolicitudBC();
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[CARGA_SOLICITUD_X_ID]");
                this.accesoDatos.AgregarSqlParametro("@ID", id);
                this.accesoDatos.EjecutarSqlLector();
                while (this.accesoDatos.SqlLectorDatos.Read())
                {
                    solicitud = this.cargarDatosSolicitud(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return solicitud;
        }
        internal SolicitudBC Solicitud_ObtenerFinalizadaXId(int id)
        {
            SolicitudBC solicitud = new SolicitudBC();
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[CARGA_SOLICITUD_FINALIZADA]");
                this.accesoDatos.AgregarSqlParametro("@ID", id);
                this.accesoDatos.EjecutarSqlLector();
                while (this.accesoDatos.SqlLectorDatos.Read())
                {
                    solicitud = this.cargarDatosSolicitud(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return solicitud;
        }
        internal DataTable obtenertemperaturas(bool frio, bool congelado, bool seco, bool multifrio, bool ways)
        {
            DataTable dt;
            try
            {
                this.accesoDatos.CargarSqlComando("[dbo].[CARGA_TEMPERATURA]");
                this.accesoDatos.AgregarSqlParametro("@SECO", seco);
                this.accesoDatos.AgregarSqlParametro("@FRIO", frio);
                this.accesoDatos.AgregarSqlParametro("@congelado", congelado);
                this.accesoDatos.AgregarSqlParametro("@multifrio", multifrio);
                this.accesoDatos.AgregarSqlParametro("@ways", ways);
                dt = this.accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                this.accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        private SolicitudBC cargarDatosSolicitud(SqlDataReader reader)
        {
            SolicitudBC solicitud = new SolicitudBC();
            solicitud.SOLI_ID = int.Parse(reader["SOLI_ID"].ToString());
            solicitud.TIPO = reader["TIPO_SOLICITUD"].ToString();
            solicitud.USUARIO = reader["USUARIO_SOLICITUD"].ToString();
            solicitud.FECHA_CREACION = DateTime.Parse(reader["FECHA_CREACION"].ToString());
            solicitud.DOCUMENTO = reader["DOCUMENTO"].ToString();
            solicitud.OBSERVACION = reader["OBSERVACIONES"].ToString();
            solicitud.FECHA_PLAN_ANDEN = DateTime.Parse(reader["FECHA_PLAN_ANDEN"].ToString());
            if (!String.IsNullOrEmpty(reader["SOTI_ID"].ToString()))
            {
                solicitud.ID_TIPO = int.Parse(reader["SOTI_ID"].ToString());
            }
            else
            {
                solicitud.ID_TIPO = 0;
            }
            if (!String.IsNullOrEmpty(reader["ID_TRAI_RESERVADO"].ToString()))
            {
                solicitud.ID_TRAILER_RESERVADO = int.Parse(reader["ID_TRAI_RESERVADO"].ToString());
            }
            else
            {
                solicitud.ID_TRAILER_RESERVADO = 0;
            }
            if (!String.IsNullOrEmpty(reader["ID_TAMANO"].ToString()))
            {
                solicitud.ID_TAMANO = int.Parse(reader["ID_TAMANO"].ToString());
            }
            else
            {
                solicitud.ID_TAMANO = 0;
            }
            if (!String.IsNullOrEmpty(reader["POS_DESTINO"].ToString()))
            {
                solicitud.POS_DESTINO = int.Parse(reader["POS_DESTINO"].ToString());
            }
            else
            {
                solicitud.POS_DESTINO = 0;
            }
            if (!String.IsNullOrEmpty(reader["TRAI_ID"].ToString()))
            {
                solicitud.ID_TRAILER = int.Parse(reader["TRAI_ID"].ToString());
            }
            else
            {
                solicitud.ID_TRAILER = 0;
            }
            if (!String.IsNullOrEmpty(reader["SOES_ID"].ToString()))
            {
                solicitud.SOES_ID = int.Parse(reader["SOES_ID"].ToString());
            }
            else
            {
                solicitud.SOES_ID = 0;
            }
            if (!String.IsNullOrEmpty(reader["TOTAL_PALLETS"].ToString()))
            {
                solicitud.Pallets = int.Parse(reader["TOTAL_PALLETS"].ToString());
            }
            else
            {
                solicitud.Pallets = 0;
            }
            if (!String.IsNullOrEmpty(reader["SITE_ID"].ToString()))
            {
                solicitud.ID_SITE = int.Parse(reader["SITE_ID"].ToString());
            }
            else
            {
                solicitud.ID_SITE = 0;
            }
            solicitud.PLACA_TRAILER = reader["TRAILER_PLACA"].ToString();
            solicitud.ESTADO = reader["ESTADO"].ToString();
            solicitud.CARACTERISTICAS = reader["CARACTERISTICAS"].ToString();
            solicitud.RUTA = reader["RUTA"].ToString();
            if (!String.IsNullOrEmpty(reader["TETR_ID"].ToString()))
            {
                solicitud.TETR_ID = int.Parse(reader["TETR_ID"].ToString());
            }
            else
            {
                solicitud.TETR_ID = 0;
            }
            if (!String.IsNullOrEmpty(reader["TEMPERATURA"].ToString()))
            {
                solicitud.TEMPERATURA = reader["TEMPERATURA"].ToString();
            }
            else
            {
                solicitud.TEMPERATURA = "0";
            }

            if (!String.IsNullOrEmpty(reader["SHOR_ID"].ToString()))
            {
                solicitud.ID_SHORTECK = reader["SHOR_ID"].ToString();
            }
            else
            {
                solicitud.ID_SHORTECK = "0";
            }

            if (!String.IsNullOrEmpty(reader["TIMESTAMP"].ToString()))
            {
                solicitud.TIMESTAMP = DateTime.Parse(reader["TIMESTAMP"].ToString());
            }
            else
            {
                solicitud.TIMESTAMP = DateTime.MinValue;
            }

            if (!String.IsNullOrEmpty(reader["PLAY_ID"].ToString()))
            {
                solicitud.PLAY_ID = int.Parse(reader["PLAY_ID"].ToString());
            }
            else
            {
                solicitud.PLAY_ID = 0;
            }
            return solicitud;
        }
        #endregion
        #region Tipo
        internal DataTable SolicitudTipo_ObtenerTodos()
        {
            return this.accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TIPO_SOLICITUD]").Tables[0];
        }
        internal SolicitudTipoBC SolicitudTipo_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            SolicitudTipoBC solicitud_tipo = new SolicitudTipoBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TIPO_SOLICITUD_X_ID]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    solicitud_tipo = this.cargarDatosSolicitudTipo(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return solicitud_tipo;
        }
        internal DataTable SolicitudTipo_ObtenerXParametro(string descripcion)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

            String query = "[dbo].[CARGA_TIPOS_INGRESO_CARGA_X_CRITERIO] ";

            if (descripcion != null && descripcion != String.Empty)
            {
                query += string.Format("@DESCRIPCION = N'{0}'", descripcion);
            }
            else
            {
                query += "@DESCRIPCION = NULL";
            }

            return accesoDatos.dsCargarSqlQuery(query).Tables[0];
        }
        internal bool SolicitudTipo_Crear(SolicitudTipoBC solicitud_tipo)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_TIPO_SOLICITUD]");
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", solicitud_tipo.DESCRIPCION);
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool SolicitudTipo_Modificar(SolicitudTipoBC solicitud_tipo)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_TIPO_SOLICITUD]");
            accesoDatos.AgregarSqlParametro("@ID", solicitud_tipo.ID);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", solicitud_tipo.DESCRIPCION);
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool SolicitudTipo_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_TIPO_SOLICITUD]");
            accesoDatos.AgregarSqlParametro("@ID", id);
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        private SolicitudTipoBC cargarDatosSolicitudTipo(SqlDataReader reader)
        {
            SolicitudTipoBC solicitud_tipo = new SolicitudTipoBC();
            solicitud_tipo.ID = int.Parse(reader["ID"].ToString());
            solicitud_tipo.DESCRIPCION = reader["DESCRIPCION"].ToString();
            return solicitud_tipo;
        }
        #endregion
    }
}