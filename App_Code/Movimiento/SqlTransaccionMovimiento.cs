// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Qanalytics.Data.Access.SqlClient
{
    public sealed class SqlTransaccionMovimiento
    {
        readonly public static String STRING_CONEXION = "CsString";
        readonly SqlAccesoDatos accesoDatos = new SqlAccesoDatos(STRING_CONEXION);

        #region TrailerUltSalida
        internal DataTable TrailerUltSalida_CargaTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TRAILER_ULT_SALIDA]").Tables[0];
        }
        internal TrailerUltSalidaBC TrailerUltSalida_Carga(int id = 0, string placa = null, string numero = null, string shortek = null)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            TrailerUltSalidaBC tusa = new TrailerUltSalidaBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILER_ULT_SALIDA]");
                if (id != 0)
                {
                    accesoDatos.AgregarSqlParametro("@TRAI_ID", id);
                }
                else if (!string.IsNullOrEmpty(placa))
                {
                    accesoDatos.AgregarSqlParametro("@TRAI_PLACA", placa);
                }
                else if (!string.IsNullOrEmpty(numero))
                {
                    accesoDatos.AgregarSqlParametro("@TRAI_NRO", numero);
                }
                else if (!string.IsNullOrEmpty(shortek))
                {
                    accesoDatos.AgregarSqlParametro("@TRAI_SHORTEK", shortek);
                }
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    tusa = this.cargarDatosTusa(accesoDatos.SqlLectorDatos);
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
            return tusa;
        }
        private TrailerUltSalidaBC cargarDatosTusa(SqlDataReader reader)
        {
            TrailerUltSalidaBC tusa = new TrailerUltSalidaBC();
            tusa.TRAI_ID = int.Parse(reader["TRAI_ID"].ToString());
            tusa.SITE_ID = int.Parse(reader["SITE_ID"].ToString());
            tusa.PROV_ID = int.Parse(reader["PROV_ID"].ToString());
            tusa.MOIC_ID = int.Parse(reader["MOIC_ID"].ToString());
            tusa.TIIC_ID = int.Parse(reader["TIIC_ID"].ToString());
            tusa.COND_ID = int.Parse(reader["COND_ID"].ToString());
            tusa.FECHA = DateTime.Parse(reader["FECHA"].ToString());
            tusa.ESTADO = int.Parse(reader["ESTADO"].ToString());
            tusa.DOC_INGRESO = reader["DOC_INGRESO"].ToString();
            tusa.SELLO_INGRESO = reader["SELLO_INGRESO"].ToString();
            tusa.SELLO_CARGA = reader["SELLO_CARGA"].ToString();
            tusa.CHOFER_RUT = reader["CHOFER_RUT"].ToString();
            tusa.CHOFER_NOMBRE = reader["CHOFER_NOMBRE"].ToString();
            tusa.ACOMP_RUT = reader["ACOMP_RUT"].ToString();
            tusa.PATENTE_TRACTO = reader["PATENTE_TRACTO"].ToString();

            if (reader["CARGADO"].ToString() == "1")
            {
                tusa.CARGADO = true;
            }
            else
            {
                tusa.CARGADO = false;
            }
            return tusa;
        }
        #endregion
        #region TrailerUltEstado
        internal DataSet TrailerUltEstado_CargaQMGPS(TrailerUltEstadoBC t)
        {
            string query;
            if (!t.SITE_IN)
            {
                query = string.Format("[dbo].[CARGA_TRAILER_QMGPS] @NRO = '{0}', @PLACA = '{1}', @SITE_ID = {2}, @SITE_IN = {3}", t.NUMERO, t.PLACA, t.SITE_ID, t.SITE_IN);
            }
            else
            {
                query = string.Format("[dbo].[CARGA_TRAILER_QMGPS] @NRO = '{0}', @PLACA = '{1}', @SITE_ID = {2}", t.NUMERO, t.PLACA, t.SITE_ID);
            }
            return accesoDatos.dsCargarSqlQuery(query);
        }
        internal DataTable TrailerUltEstado_CargaTodo()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TRAILER_ULT_ESTADO]").Tables[0];
        }
        internal DataTable TrailerUltEstado_CargaXParametros(TrailerUltEstadoBC t)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILER_ULT_ESTADO]");
                if (t.ID != 0)
                {
                    accesoDatos.AgregarSqlParametro("@ID_TRAILER", t.ID);
                }
                if (t.SITE_ID != 0)
                {
                    accesoDatos.AgregarSqlParametro("@SITE_ID", t.SITE_ID);
                }
                if (!t.SITE_IN)
                {
                    accesoDatos.AgregarSqlParametro("@SITE_IN", t.SITE_IN);
                }
                dt = accesoDatos.EjecutarSqlquery2();
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
            return dt;
        }
        internal TrailerUltEstadoBC TrailerUltEstado_CargaXId(int id)
        {
            TrailerUltEstadoBC trailerUE = new TrailerUltEstadoBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILER_ULT_ESTADO]");
                accesoDatos.AgregarSqlParametro("@ID_TRAILER", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    trailerUE = this.cargarDatosTrue(accesoDatos.SqlLectorDatos);
                }
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
            return trailerUE;
        }
        internal bool TrailerUltEstado_ProcesoSalida(TrailerUltSalidaBC t, int id_usuario, string viaje, string GPS,  out string resultado)
        {
            //  TrailerUltEstadoBC trailerUE = new TrailerUltEstadoBC();
            bool exito = false;
            resultado = "";
            try
            {
                int error = 0;
                accesoDatos.CargarSqlComando("[dbo].[PROCESO_SALIDA_V2]");
                accesoDatos.AgregarSqlParametro("@TRAI_ID", t.ID);
                accesoDatos.AgregarSqlParametro("@ID_SITE", t.SITE_ID);
                accesoDatos.AgregarSqlParametro("@USUA_ID", id_usuario);
                if (!string.IsNullOrEmpty(t.PATENTE_TRACTO))
                    accesoDatos.AgregarSqlParametro("@PATENTE_TRACTO", t.PATENTE_TRACTO);
                if (!string.IsNullOrEmpty(t.CHOFER_RUT))
                    accesoDatos.AgregarSqlParametro("@RUT_CHOFER", t.CHOFER_RUT);
                if (!string.IsNullOrEmpty(t.CHOFER_NOMBRE))
                    accesoDatos.AgregarSqlParametro("@NOM_CHOFER", t.CHOFER_NOMBRE);
                if (t.DEST_ID != 0)
                    accesoDatos.AgregarSqlParametro("@DEST_ID", t.DEST_ID);
                if (t.LOCA_ID != 0)
                    accesoDatos.AgregarSqlParametro("@LOCA_ID", t.LOCA_ID);
                if ( t.ESTADO_YMS != null)
                    accesoDatos.AgregarSqlParametro("@ESTADO_YMS", t.ESTADO_YMS);
                accesoDatos.AgregarSqlParametro("@LOCALES_YMS", t.OBSERVACION);
                accesoDatos.AgregarSqlParametro("@COND_ID", t.COND_ID);
                accesoDatos.AgregarSqlParametro("@viaje", viaje);
                accesoDatos.AgregarSqlParametro("@estado_GPS", GPS);
                accesoDatos.AgregarSqlParametro("@ERROR", error).Direction = ParameterDirection.Output;
                SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
                param.Direction = ParameterDirection.Output;
                param.Size = 1000;

                accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                resultado = ex.Message;
                throw (ex);
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool TrailerUltEstado_ProcesoSalida(TrailerUltSalidaBC t, DataTable dt, int id_usuario, string viaje, string GPS,  out string resultado)
        {
            //  TrailerUltEstadoBC trailerUE = new TrailerUltEstadoBC();
            bool exito = false;
            resultado = "";
            try
            {
                int error = 0;
                accesoDatos.CargarSqlComando("[dbo].[PROCESO_SALIDA_V3]");
                accesoDatos.AgregarSqlParametro("@TRAI_ID", t.ID);
                accesoDatos.AgregarSqlParametro("@ID_SITE", t.SITE_ID);
                accesoDatos.AgregarSqlParametro("@USUA_ID", id_usuario);
                if (!string.IsNullOrEmpty(t.PATENTE_TRACTO))
                    accesoDatos.AgregarSqlParametro("@PATENTE_TRACTO", t.PATENTE_TRACTO);
                if (!string.IsNullOrEmpty(t.CHOFER_RUT))
                    accesoDatos.AgregarSqlParametro("@RUT_CHOFER", t.CHOFER_RUT);
                if (!string.IsNullOrEmpty(t.CHOFER_NOMBRE))
                    accesoDatos.AgregarSqlParametro("@NOM_CHOFER", t.CHOFER_NOMBRE);
                if (t.DEST_ID != 0)
                    accesoDatos.AgregarSqlParametro("@DEST_ID", t.DEST_ID);
                if (t.LOCA_ID != 0)
                    accesoDatos.AgregarSqlParametro("@LOCA_ID", t.LOCA_ID);
                if ( t.ESTADO_YMS != null)
                    accesoDatos.AgregarSqlParametro("@ESTADO_YMS", t.ESTADO_YMS);
                accesoDatos.AgregarSqlParametro("@LOCALES_YMS", t.OBSERVACION);
                accesoDatos.AgregarSqlParametro("@LOCALES", dt);
                accesoDatos.AgregarSqlParametro("@COND_ID", t.COND_ID);
                accesoDatos.AgregarSqlParametro("@viaje", viaje);
                accesoDatos.AgregarSqlParametro("@estado_GPS", GPS);
                accesoDatos.AgregarSqlParametro("@ERROR", error).Direction = ParameterDirection.Output;
                SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
                param.Direction = ParameterDirection.Output;
                param.Size = 1000;

                accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                resultado = ex.Message;
                throw (ex);
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool TrailerUltEstado_ProcesoSalidaLoAguirre(TrailerUltSalidaLABC tu, DataTable detalle, string locales, int id_usuario, out string resultado)
        {
            //  TrailerUltEstadoBC trailerUE = new TrailerUltEstadoBC();
            bool exito = false;
            resultado = "";
            try
            {
                if (detalle == null)
                {
                    detalle = new DataTable();
                    detalle.Columns.Add("LOCA_ID", Type.GetType("System.String"));
                    detalle.Columns.Add("LOCA_COD", Type.GetType("System.String"));
                    detalle.Columns.Add("LOCAL", Type.GetType("System.String"));
                    detalle.Columns.Add("SECUENCIA", Type.GetType("System.String"));
                    detalle.Columns.Add("TRUE_COD_INTERNO_IN", Type.GetType("System.String"));
                    detalle.Columns.Add("SITE_ID", Type.GetType("System.String"));
                    detalle.Columns.Add("TRAI_ID", Type.GetType("System.String"));

                    detalle.Columns.Add("SELLO", Type.GetType("System.String"));
                    detalle.Columns.Add("SOBRE", Type.GetType("System.String"));
                    detalle.Columns.Add("CARRO", Type.GetType("System.String"));
                    detalle.Columns.Add("EMBARQUE", Type.GetType("System.String"));

                }


                int error = 0;
                accesoDatos.CargarSqlComando("[dbo].[PROCESO_SALIDA_LO_AGUIRRE]");
                accesoDatos.AgregarSqlParametro("@TRAI_ID", tu.ID);
                accesoDatos.AgregarSqlParametro("@USUA_ID", id_usuario);
                accesoDatos.AgregarSqlParametro("@LOCALES_YMS", locales);
                accesoDatos.AgregarSqlParametro("@LOCALES", detalle);
                accesoDatos.AgregarSqlParametro("@TRUE_COD_INTERNO_IN", tu.TRUE_COD_INTERNO_IN);
                if (!string.IsNullOrEmpty(tu.PATENTE_TRACTO))
                    accesoDatos.AgregarSqlParametro("@PATENTE_TRACTO", tu.PATENTE_TRACTO);
                if (!string.IsNullOrEmpty(tu.CHOFER_RUT))
                    accesoDatos.AgregarSqlParametro("@RUT_CHOFER", tu.CHOFER_RUT);
                if (!string.IsNullOrEmpty(tu.CHOFER_NOMBRE))
                    accesoDatos.AgregarSqlParametro("@NOM_CHOFER", tu.CHOFER_NOMBRE);
                if (tu.DEST_ID != 0)
                    accesoDatos.AgregarSqlParametro("@DEST_ID", tu.DEST_ID);
                if (tu.LOCA_ID != 0)
                    accesoDatos.AgregarSqlParametro("@LOCA_ID", tu.LOCA_ID);
                if (tu.ESTADO_YMS != null)
                    accesoDatos.AgregarSqlParametro("@ESTADO_YMS", tu.ESTADO_YMS);
                if (!string.IsNullOrEmpty(tu.SELLO_CARGA))
                    accesoDatos.AgregarSqlParametro("@SELLO_CARGA", tu.SELLO_CARGA);
                if (!string.IsNullOrEmpty(tu.MMPP))
                    accesoDatos.AgregarSqlParametro("@MMPP", tu.MMPP);
                if (!string.IsNullOrEmpty(tu.GUIA))
                    accesoDatos.AgregarSqlParametro("@GUIA", tu.GUIA);
                if (tu.CAJAS != 0)
                    accesoDatos.AgregarSqlParametro("@CAJAS", tu.CAJAS);
                if (tu.PALLET_AZUL != 0)
                    accesoDatos.AgregarSqlParametro("@PALLET_AZUL", tu.PALLET_AZUL);
                if (tu.PALLET_ROJO != 0)
                    accesoDatos.AgregarSqlParametro("@PALLET_ROJO", tu.PALLET_ROJO);
                if (tu.PALLET_BLANCO != 0)
                    accesoDatos.AgregarSqlParametro("@PALLET_BLANCO", tu.PALLET_BLANCO);
                if (tu.LEÑA != 0)
                    accesoDatos.AgregarSqlParametro("@LEÑA", tu.LEÑA);
                accesoDatos.AgregarSqlParametro("@COND_ID", tu.COND_ID);
                accesoDatos.AgregarSqlParametro("@VIAJE", tu.VIAJE);
                accesoDatos.AgregarSqlParametro("@ERROR", error).Direction = ParameterDirection.Output;
                SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
                param.Direction = ParameterDirection.Output;
                param.Size = 1000;

                accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                resultado = ex.Message;
                throw (ex);
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        private TrailerUltEstadoBC cargarDatosTrue(SqlDataReader reader)
        {
            TrailerUltEstadoBC trailerUE = new TrailerUltEstadoBC();
            trailerUE.ID = int.Parse(reader["ID"].ToString());
            trailerUE.LUGAR_ID = int.Parse(reader["LUGA_ID"].ToString());
            trailerUE.LUGAR = reader["ULTIMO_LUGAR"].ToString();
            if (reader["CARGADO"].ToString() == "1")
            {
                trailerUE.CARGADO = true;
            }
            else
            {
                trailerUE.CARGADO = false;
            }
            int prov_id;
            int.TryParse(reader["PROV_ID"].ToString(), out prov_id);
            trailerUE.PROV_ID = prov_id;
            trailerUE.DOC_INGRESO = reader["DOCUMENTO"].ToString();
            trailerUE.SELLO_INGRESO = reader["SELLO"].ToString();
            trailerUE.SOLI_ID = int.Parse(reader["SOLI_ID"].ToString());
            return trailerUE;
        }
        #endregion
        #region Movimiento
        internal DataTable Movimiento_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_MOVIMIENTO]").Tables[0];
        }
        internal DataTable Movimiento_ObtenerControl(int site_id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_MOVIMIENTOS_CONTROL] {0}", site_id)).Tables[0];
        }
        internal bool Movimiento_Confirmar(int id, int usua_id, string o, int luga_id, int moet_id, int moes_id, out string resultado)
        {
            accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            resultado = "";
            int error = 0;
            accesoDatos.CargarSqlComando("[dbo].[prcMOVIMIENTOS_ESTADO_FINALIZA]");
            accesoDatos.AgregarSqlParametro("@MOVI_ID", id);
            accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
            accesoDatos.AgregarSqlParametro("@MOVI_OBS", o);
            if (moet_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@MOET_ID", moet_id);
            }
            else
            {
                accesoDatos.AgregarSqlParametro("@MOET_ID", DBNull.Value);
            }
            
            if (moes_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@MOES_ID", moes_id);
            }

            accesoDatos.AgregarSqlParametro("@ERROR", error).Direction = ParameterDirection.Output;
            SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            if (luga_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@LUGA_ID_DESTINO_MODIF", luga_id);
            }
            else
            {
                accesoDatos.AgregarSqlParametro("@LUGA_ID_DESTINO_MODIF", DBNull.Value);
            }
            try
            {
                accesoDatos.EjecutarSqlEscritura();
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
        internal bool Movimiento_Anular(int id, int moet_id)
        {
            accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            accesoDatos.CargarSqlComando("[dbo].[ANULAR_MOVIMIENTO]");
            accesoDatos.AgregarSqlParametro("@ID", id);
            if (moet_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@MOET_ID", moet_id);
            }
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
        internal bool Movimiento_ModificarDestino(int id, int id_destino, int usua_id, out string resultado)
        {
            accesoDatos.LimpiarSqlParametros();
            bool exito = false;
            resultado = "";
            int error = 0;
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_DESTINO_MOVIMIENTO_v2]");
            accesoDatos.AgregarSqlParametro("@ID_MOV", id);
            accesoDatos.AgregarSqlParametro("@ID_DESTINO", id_destino);
            accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
           
            accesoDatos.AgregarSqlParametro("@ERROR", error).Direction = ParameterDirection.Output;

            SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);

            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            try
            {
                accesoDatos.EjecutarSqlEscritura();
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
        internal bool Movimiento_Entrada(MovimientoBC movimiento, TrailerUltEstadoBC trailerUE, TrailerBC trailer, int usua_id, out string resultado)
        {
            bool exito = false;
            bool errorint = false;
            resultado = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[PROCESO_ENTRADA]");
            accesoDatos.AgregarSqlParametro("@ID_TRANSPORTISTA", trailer.TRAN_ID);
            //        accesoDatos.AgregarSqlParametro("@ID_ORIGEN",DBNull.Value);
            accesoDatos.AgregarSqlParametro("@ID_DESTINO", movimiento.ID_DESTINO);
            if (movimiento.ID_TRAILER != 0)
                accesoDatos.AgregarSqlParametro("@ID_TRAILER", movimiento.ID_TRAILER);
            else
                accesoDatos.AgregarSqlParametro("@ID_TRAILER", DBNull.Value);
            accesoDatos.AgregarSqlParametro("@ID_SITE", trailerUE.SITE_ID);
            accesoDatos.AgregarSqlParametro("@ID_PROVEEDOR", trailerUE.PROV_ID);
            accesoDatos.AgregarSqlParametro("@true_PATENTE_TRACTO", movimiento.PATENTE_TRACTO);
            accesoDatos.AgregarSqlParametro("@guia", trailerUE.GUIA);
            accesoDatos.AgregarSqlParametro("@TRUE_CARGADO", trailerUE.CARGADO);
            accesoDatos.AgregarSqlParametro("@TRUE_DOCUMENTO", trailerUE.DOC_INGRESO);
            accesoDatos.AgregarSqlParametro("@TRUE_SELLO", trailerUE.SELLO_INGRESO);
            accesoDatos.AgregarSqlParametro("@TRUE_CHOFER_RUT", trailerUE.CHOFER_RUT);
            accesoDatos.AgregarSqlParametro("@TRUE_CHOFER_NOMBRE", trailerUE.CHOFER_NOMBRE);
            accesoDatos.AgregarSqlParametro("@TRUE_ACOMP_RUT", trailerUE.ACOMP_RUT);
            accesoDatos.AgregarSqlParametro("@TRUE_OBS", movimiento.OBSERVACION);
            accesoDatos.AgregarSqlParametro("@TRUE_TEMPERATURA", null);
            accesoDatos.AgregarSqlParametro("@TRNP_ID", null);
            accesoDatos.AgregarSqlParametro("@COND_ID", trailerUE.COND_ID);

            accesoDatos.AgregarSqlParametro("@usua_id", usua_id);
            if (!((trailerUE.TIPO_INGRESO_CARGA == "") || (trailerUE.TIPO_INGRESO_CARGA == "0")))
                accesoDatos.AgregarSqlParametro("@tiic_id", trailerUE.TIPO_INGRESO_CARGA);
            if (!((trailerUE.motivo_TIPO_INGRESO_CARGA == "") || (trailerUE.motivo_TIPO_INGRESO_CARGA == "0")))
                accesoDatos.AgregarSqlParametro("@moic_id", trailerUE.motivo_TIPO_INGRESO_CARGA);
            if (trailerUE.pring_id != "0")
                accesoDatos.AgregarSqlParametro("@pring_id", trailerUE.pring_id);

            SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;
            accesoDatos.AgregarSqlParametro("@error", errorint).Direction = ParameterDirection.Output;

            try
            {
                accesoDatos.EjecutarSqlEscritura();
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
        internal bool Movimiento_automatico_estacionamiento(MovimientoBC movimiento, int site_id, int usua_id, out string resultado)
        {
            bool exito = false;
            bool errorint = false;
            resultado = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[movimiento_automatico_estacionamiento]");
            accesoDatos.AgregarSqlParametro("@ID_TRAILER", movimiento.ID_TRAILER);
            accesoDatos.AgregarSqlParametro("@ID_SITE", site_id);
            accesoDatos.AgregarSqlParametro("@movi_OBS", movimiento.OBSERVACION);
            accesoDatos.AgregarSqlParametro("@usua_id", usua_id);

            SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            accesoDatos.AgregarSqlParametro("@error", errorint).Direction = ParameterDirection.Output;

            try
            {
                accesoDatos.EjecutarSqlEscritura();
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
        internal bool Movimiento_Interno(MovimientoBC movimiento, int site_id, int usua_id, out string resultado)
        {
            bool exito = false;
            bool errorint = false;
            resultado = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[movimiento_interno]");
            accesoDatos.AgregarSqlParametro("@ID_TRAILER", movimiento.ID_TRAILER);
            accesoDatos.AgregarSqlParametro("@ID_SITE", site_id);
            if (movimiento.petroleo != null && movimiento.petroleo == "True")
            {
                accesoDatos.AgregarSqlParametro("@petroleo", "1");
                accesoDatos.AgregarSqlParametro("@ID_DESTINO", DBNull.Value);
                movimiento.OBSERVACION = string.Format("(Carga Petroleo) {0}", movimiento.OBSERVACION);
            }
            else
            {
                accesoDatos.AgregarSqlParametro("@ID_DESTINO", movimiento.ID_DESTINO);
            }
            accesoDatos.AgregarSqlParametro("@movi_OBS", movimiento.OBSERVACION);
            accesoDatos.AgregarSqlParametro("@usua_id", usua_id);

            SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            accesoDatos.AgregarSqlParametro("@error", errorint).Direction = ParameterDirection.Output;

            try
            {
                accesoDatos.EjecutarSqlEscritura();
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
        internal bool Movimiento_Interno(MovimientoBC movimiento, TrailerUltEstadoBC trailerUE, TrailerBC trailer, int usua_id, out string resultado)
        {
            bool exito = false;
            bool errorint = false;
            resultado = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[movimiento_interno]");
            accesoDatos.AgregarSqlParametro("@ID_TRAILER", movimiento.ID_TRAILER);
            accesoDatos.AgregarSqlParametro("@ID_SITE", trailerUE.SITE_ID);
            if (movimiento.petroleo != null && movimiento.petroleo == "True")
            {
                accesoDatos.AgregarSqlParametro("@petroleo", "1");
                accesoDatos.AgregarSqlParametro("@ID_DESTINO", DBNull.Value);
                movimiento.OBSERVACION = string.Format("(Carga Petroleo) {0}", movimiento.OBSERVACION);
            }
            else
            {
                accesoDatos.AgregarSqlParametro("@ID_DESTINO", movimiento.ID_DESTINO);
            }
            accesoDatos.AgregarSqlParametro("@movi_OBS", movimiento.OBSERVACION);
            accesoDatos.AgregarSqlParametro("@usua_id", usua_id);

            SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            accesoDatos.AgregarSqlParametro("@error", errorint).Direction = ParameterDirection.Output;
  
            try
            {
                accesoDatos.EjecutarSqlEscritura();
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
        internal MovimientoBC Movimiento_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            MovimientoBC movimiento = new MovimientoBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_MOVIMIENTO_X_ID]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    movimiento = this.cargarDatosMovimiento(accesoDatos.SqlLectorDatos);
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
            return movimiento;
        }
        internal MovimientoBC Movimiento_ObtenerXPlaca(string placa)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            MovimientoBC movimiento = new MovimientoBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_MOVIMIENTO_X_PLACA]");
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    movimiento = this.cargarDatosMovimiento(accesoDatos.SqlLectorDatos);
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
            return movimiento;
        }
        internal DataTable Movimiento_ObtenerXParametro(string placa, bool externo)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

            String query = "[dbo].[CARGA_MOVIMIENTOS_X_CRITERIO] ";
            if (placa != null && placa != "")
            {
                query += string.Format("@PLACA = {0},", placa);
            }
            else
            {
                query += "@PLACA = NULL,";
            }

            if (externo)
            {
                query += "@EXTERNO = FALSE";
            }
            else
            {
                query += "@EXTERNO = NULL";
            }

            return accesoDatos.dsCargarSqlQuery(query).Tables[0];
        }
        internal bool Movimiento_Modificar(MovimientoBC movimiento)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_MOVIMIENTO]");
            accesoDatos.AgregarSqlParametro("@ID", movimiento.ID);
            accesoDatos.AgregarSqlParametro("@FECHA_CREACION", movimiento.FECHA_CREACION);
            accesoDatos.AgregarSqlParametro("@ID_ESTADO", movimiento.ID_ESTADO);
            accesoDatos.AgregarSqlParametro("@OBSERVACION", movimiento.OBSERVACION);
            accesoDatos.AgregarSqlParametro("@ID_ORIGEN", movimiento.ID_ORIGEN);
            accesoDatos.AgregarSqlParametro("@FECHA_ORIGEN", movimiento.FECHA_ORIGEN);
            accesoDatos.AgregarSqlParametro("@ID_DESTINO", movimiento.ID_DESTINO);
            accesoDatos.AgregarSqlParametro("@FECHA_DESTINO", movimiento.FECHA_DESTINO);
            accesoDatos.AgregarSqlParametro("@ID_SOLICITUD", movimiento.ID_SOLICITUD);
            accesoDatos.AgregarSqlParametro("@ID_TRAILER", movimiento.ID_TRAILER);
            accesoDatos.AgregarSqlParametro("@ORDEN", movimiento.ORDEN);
            accesoDatos.AgregarSqlParametro("@ID_REMOLCADOR", movimiento.ID_REMOLCADOR);
            accesoDatos.AgregarSqlParametro("@PATENTE_TRACTO", movimiento.PATENTE_TRACTO);
            accesoDatos.AgregarSqlParametro("@LOCAL_DESDE", movimiento.LOCAL_DESDE);
            accesoDatos.AgregarSqlParametro("@LOCAL_HASTA", movimiento.LOCAL_HASTA);
            accesoDatos.AgregarSqlParametro("@NUMERO", movimiento.NUMERO);
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
        internal bool Movimiento_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_MOVIMIENTO]");
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
        internal bool Movimiento_CambiarOrden(int id, bool subir)
        {
            bool exito = false;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CAMBIAR_ORDEN_MOVIMIENTO]");
                accesoDatos.AgregarSqlParametro("@MOVI_ID", id);
                accesoDatos.AgregarSqlParametro("@SUBIR", subir);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal int Movimiento_MinOrden(int site_id)
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_MENOR_ORDEN_MOVIMIENTO_SITE]");
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                accesoDatos.EjecutaSqlInsertIdentity();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return accesoDatos.ID;
        }
        internal int Movimiento_MaxOrden(int site_id)
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_MAYOR_ORDEN_MOVIMIENTO_SITE]");
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                accesoDatos.EjecutaSqlInsertIdentity();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return accesoDatos.ID;
        }
        internal DataTable Movimiento_ObtenerTipos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TIPOS_MOVIMIENTO]").Tables[0];
        }
        private MovimientoBC cargarDatosMovimiento(SqlDataReader reader)
        {
            MovimientoBC movimiento = new MovimientoBC();
            int origen, destino, soli, desde, hasta;
            movimiento.ID = int.Parse(reader["ID"].ToString());

            movimiento.FECHA_CREACION = DateTime.Parse(reader["FECHA_CREACION"].ToString());

            movimiento.FECHA_ASIGNACION = DateTime.Parse(reader["FECHA_ASIGNACION"].ToString());

            movimiento.FECHA_EJECUCION = DateTime.Parse(reader["FECHA_EJECUCION"].ToString());

            movimiento.ID_ESTADO = int.Parse(reader["ID_ESTADO"].ToString());
            movimiento.OBSERVACION = reader["OBSERVACION"].ToString();

            if (int.TryParse(reader["ID_ORIGEN"].ToString(), out origen))
            {
                movimiento.ID_ORIGEN = origen;
            }
            else
            {
                movimiento.ID_ORIGEN = 0;
            }

            movimiento.FECHA_ORIGEN = DateTime.Parse(reader["FECHA_ORIGEN"].ToString());

            if (int.TryParse(reader["ID_DESTINO"].ToString(), out destino))
            {
                movimiento.ID_DESTINO = destino;
            }
            else
            {
                movimiento.ID_DESTINO = 0;
            }

            movimiento.FECHA_DESTINO = DateTime.Parse(reader["FECHA_DESTINO"].ToString());

            if (int.TryParse(reader["ID_SOLICITUD"].ToString(), out soli))
            {
                movimiento.ID_SOLICITUD = soli;
            }
            else
            {
                movimiento.ID_SOLICITUD = 0;
            }

            movimiento.ID_TRAILER = int.Parse(reader["ID_TRAILER"].ToString());

            movimiento.ORDEN = int.Parse(reader["ORDEN"].ToString());

            movimiento.ID_REMOLCADOR = int.Parse(reader["ID_REMOLCADOR"].ToString());

            movimiento.PATENTE_TRACTO = reader["PATENTE_TRACTO"].ToString();

            if (int.TryParse(reader["LOCAL_DESDE"].ToString(), out desde))
            {
                movimiento.LOCAL_DESDE = desde;
            }
            else
            {
                movimiento.LOCAL_DESDE = 0;
            }
            if (int.TryParse(reader["LOCAL_HASTA"].ToString(), out hasta))
            {
                movimiento.LOCAL_HASTA = hasta;
            }
            else
            {
                movimiento.LOCAL_HASTA = 0;
            }

            movimiento.NUMERO = reader["NUMERO"].ToString();
            return movimiento;
        }
        #endregion
        #region MovimientoEstado
        internal DataTable MovEstSubTipo_ObtenerTodo()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_MOV_EST_SUB_TIPO]").Tables[0];
        }
        internal DataTable MovEstSubTipo_ObtenerXEstado(int moes_id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_TODO_MOV_EST_SUB_TIPO] @MOES_ID = {0}", moes_id)).Tables[0];
        }
        internal DataTable MovEstSubTipo_ObtenerXEstado(int moes_id, bool moet_activo)
        {
            string query = string.Format("[dbo].[CARGA_TODO_MOV_EST_SUB_TIPO] @MOES_ID = {0}", moes_id);
            if (moet_activo)
            {
                query += ",@MOET_ACTIVO = 1";
            }
            else
            {
                query += ",@MOET_ACTIVO = 0";
            }
            return accesoDatos.dsCargarSqlQuery(query).Tables[0];
        }
        #endregion
        #region PreEntrada
        internal bool PreEntrada_Crear(PreEntradaBC p, out int id, out string resultado)
        {
            bool exito = false;
            bool errorint = false;
            resultado = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[PREINGRESO_MANUAL]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", p.SITE_ID);
            accesoDatos.AgregarSqlParametro("@PROV_ID", p.PROV_ID);
            accesoDatos.AgregarSqlParametro("@TRAI_ID", p.TRAI_ID);
            accesoDatos.AgregarSqlParametro("@FECHA", p.FECHA);
            accesoDatos.AgregarSqlParametro("@ESTADO", p.ESTADO);
            accesoDatos.AgregarSqlParametro("@observacion", p.Observacion);
            accesoDatos.AgregarSqlParametro("@DOC_INGRESO", p.DOC_INGRESO);
            accesoDatos.AgregarSqlParametro("@CARGADO", p.CARGADO);
            accesoDatos.AgregarSqlParametro("@COND_ID", p.COND_ID);
            accesoDatos.AgregarSqlParametro("@extranjero", p.extranjero);
            accesoDatos.AgregarSqlParametro("@citasnuevas", p.citas);
            if (!string.IsNullOrEmpty(p.SELLO_INGRESO) && p.extranjero)
                accesoDatos.AgregarSqlParametro("@SELLO_INGRESO", p.SELLO_INGRESO);
            if (!string.IsNullOrEmpty(p.SELLO_CARGA))
                accesoDatos.AgregarSqlParametro("@SELLO_CARGA", p.SELLO_CARGA);
            if (!string.IsNullOrEmpty(p.RUT_CHOFER))
                accesoDatos.AgregarSqlParametro("@RUT_CHOFER", p.RUT_CHOFER);
            if (!string.IsNullOrEmpty(p.NOMBRE_CHOFER))
                accesoDatos.AgregarSqlParametro("@NOMBRE_CHOFER", p.NOMBRE_CHOFER);
            if (!string.IsNullOrEmpty(p.RUT_ACOMP))
                accesoDatos.AgregarSqlParametro("@RUT_ACOMP", p.RUT_ACOMP);
            if (!string.IsNullOrEmpty(p.PATENTE_TRACTO))
                accesoDatos.AgregarSqlParametro("@PATENTE_TRACTO", p.PATENTE_TRACTO);
            if (p.MOIC_ID != 0)
                accesoDatos.AgregarSqlParametro("@MOIC_ID", p.MOIC_ID);
            if (p.TIIC_ID != 0)
                accesoDatos.AgregarSqlParametro("@TIIC_ID", p.TIIC_ID);
            if (!string.IsNullOrEmpty(p.PRING_FONO))
                accesoDatos.AgregarSqlParametro("@PRING_FONO", p.PRING_FONO);
            SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            accesoDatos.AgregarSqlParametro("@error", errorint).Direction = ParameterDirection.Output;
         
            try
            {
                accesoDatos.EjecutaSqlInsertIdentity();
                exito = true;
                resultado = param.Value.ToString();
                id = accesoDatos.ID;
            }
            catch (Exception ex)
            {
                id = 0;
                exito = false;
                //throw (ex);
                resultado = ex.Message;
            }
            return exito;
        }
        internal bool PreEntrada_CrearV2(PreEntradaBC p, out int id, out string resultado)
        {
            bool exito = false;
            bool errorint = false;
            resultado = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[PROCESO_PREINGRESO_v2]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", p.SITE_ID);
            accesoDatos.AgregarSqlParametro("@PROV_ID", p.PROV_ID);
            accesoDatos.AgregarSqlParametro("@TRAI_ID", p.TRAI_ID);
            accesoDatos.AgregarSqlParametro("@FECHA", p.FECHA);
            accesoDatos.AgregarSqlParametro("@ESTADO", p.ESTADO);
            accesoDatos.AgregarSqlParametro("@observacion", p.Observacion);
            accesoDatos.AgregarSqlParametro("@DOC_INGRESO", p.DOC_INGRESO);
            accesoDatos.AgregarSqlParametro("@CARGADO", p.CARGADO);
            accesoDatos.AgregarSqlParametro("@COND_ID", p.COND_ID);
            accesoDatos.AgregarSqlParametro("@extranjero", p.extranjero);
            accesoDatos.AgregarSqlParametro("@citasnuevas", p.citas);
            if (!string.IsNullOrEmpty(p.SELLO_INGRESO) && p.extranjero)
                accesoDatos.AgregarSqlParametro("@SELLO_INGRESO", p.SELLO_INGRESO);
            if (!string.IsNullOrEmpty(p.SELLO_CARGA))
                accesoDatos.AgregarSqlParametro("@SELLO_CARGA", p.SELLO_CARGA);
            if (!string.IsNullOrEmpty(p.RUT_CHOFER))
                accesoDatos.AgregarSqlParametro("@RUT_CHOFER", p.RUT_CHOFER);
            if (!string.IsNullOrEmpty(p.NOMBRE_CHOFER))
                accesoDatos.AgregarSqlParametro("@NOMBRE_CHOFER", p.NOMBRE_CHOFER);
            if (!string.IsNullOrEmpty(p.RUT_ACOMP))
                accesoDatos.AgregarSqlParametro("@RUT_ACOMP", p.RUT_ACOMP);
            if (!string.IsNullOrEmpty(p.PATENTE_TRACTO))
                accesoDatos.AgregarSqlParametro("@PATENTE_TRACTO", p.PATENTE_TRACTO);
            if (p.MOIC_ID != 0)
                accesoDatos.AgregarSqlParametro("@MOIC_ID", p.MOIC_ID);
            if (p.TIIC_ID != 0)
                accesoDatos.AgregarSqlParametro("@TIIC_ID", p.TIIC_ID);
            if (!string.IsNullOrEmpty(p.PRING_FONO))
                accesoDatos.AgregarSqlParametro("@PRING_FONO", p.PRING_FONO);
            SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            accesoDatos.AgregarSqlParametro("@error", errorint).Direction = ParameterDirection.Output;
         
            try
            {
                accesoDatos.EjecutaSqlInsertIdentity();
                exito = true;
                resultado = param.Value.ToString();
                id = accesoDatos.ID;
            }
            catch (Exception ex)
            {
                id = 0;
                exito = false;
                //throw (ex);
                resultado = ex.Message;
            }
            return exito;
        }
        internal PreEntradaBC PreEntrada_Carga(int trai_id, int site_id, string doc)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            PreEntradaBC p = new PreEntradaBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_PREINGRESO]");
                accesoDatos.AgregarSqlParametro("@TRAI_ID", trai_id);
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                accesoDatos.AgregarSqlParametro("@FECHA_DESDE", DBNull.Value);
                accesoDatos.AgregarSqlParametro("@FECHA_HASTA", DBNull.Value);
                accesoDatos.AgregarSqlParametro("@PROV_ID", DBNull.Value);
                accesoDatos.AgregarSqlParametro("@TRAN_ID", DBNull.Value);
                accesoDatos.AgregarSqlParametro("@TRAI_PLACA", DBNull.Value);
                accesoDatos.AgregarSqlParametro("@CHOFER", DBNull.Value);
                accesoDatos.AgregarSqlParametro("@ESTADO", 1);
                accesoDatos.AgregarSqlParametro("@doc", doc);
                  
                accesoDatos.EjecutarSqlquery2();
                //                while (accesoDatos.SqlLectorDatos.Read())
                //              {
                p = this.cargarDatosPreEntrada(accesoDatos.EjecutarSqlquery2());
                //            }
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
            return p;
        }
        internal DataTable PreEntrada_Reporte(DateTime desde, DateTime hasta, int site_id, int prov_id, string numero)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[REPORTE_PREINGRESO]");
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                accesoDatos.AgregarSqlParametro("@DESDE", desde);
                accesoDatos.AgregarSqlParametro("@HASTA", hasta);
                accesoDatos.AgregarSqlParametro("@numero", numero);
                if (prov_id != 0)
                {
                    accesoDatos.AgregarSqlParametro("@PROV_ID", prov_id);
                }
                dt = accesoDatos.EjecutarSqlquery2();
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
            return dt;
        }
        internal DataTable PreEntrada_CargarCitas(string num_cita, int site_id, int prov_id)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_CITAS_NO_INGRESADAS_V2]");
                if (site_id != 0)
                    accesoDatos.AgregarSqlParametro("@CD", site_id);
                if (prov_id != 0)
                    accesoDatos.AgregarSqlParametro("@PROV_ID", prov_id);
                if (!string.IsNullOrEmpty(num_cita))
                    accesoDatos.AgregarSqlParametro("@NUM_CITA", num_cita);
                dt = accesoDatos.EjecutarSqlquery2();
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
            return dt;
        }
        private PreEntradaBC cargarDatosPreEntrada(DataTable reader)
        { 
            PreEntradaBC p = new PreEntradaBC();
            if (reader.Rows.Count > 0)
            {
                p.ID = int.Parse(reader.Rows[0]["PRING_ID"].ToString());
                if (reader.Rows[0]["SITE_ID"] != null)
                {
                    p.SITE_ID = int.Parse(reader.Rows[0]["SITE_ID"].ToString());
                }
                if (!string.IsNullOrEmpty(reader.Rows[0]["PROV_ID"].ToString()))
                {
                    p.PROV_ID = int.Parse(reader.Rows[0]["PROV_ID"].ToString());
                }
                if (reader.Rows[0]["TRAI_ID"] != null)
                {
                    p.TRAI_ID = int.Parse(reader.Rows[0]["TRAI_ID"].ToString());
                }
                p.FECHA = DateTime.Parse(reader.Rows[0]["FECHA"].ToString());
                p.FECHA_HORA = DateTime.Parse(reader.Rows[0]["FECHA_HORA"].ToString());
                //  if (reader["ESTADO"] != null)
                //     p.ESTADO = int.Parse(reader["ESTADO"].ToString());
                p.DOC_INGRESO = reader.Rows[0]["DOC_INGRESO"].ToString();
                p.SELLO_INGRESO = reader.Rows[0]["SELLO_INGRESO"].ToString();
                p.SELLO_CARGA = reader.Rows[0]["SELLO_CARGA"].ToString();
                p.RUT_CHOFER = reader.Rows[0]["RUT_CHOFER"].ToString();
                p.NOMBRE_CHOFER = reader.Rows[0]["NOMBRE_CHOFER"].ToString();
                p.RUT_ACOMP = reader.Rows[0]["RUT_ACOMP"].ToString();
                p.PATENTE_TRACTO = reader.Rows[0]["PATENTE_TRACTO"].ToString();
                if (reader.Rows[0]["CARGADO"].ToString() == "True")
                {
                    p.CARGADO = true;
                }
                else
                {
                    p.CARGADO = false;
                }
                if (!string.IsNullOrEmpty(reader.Rows[0]["MOIC_ID"].ToString()))
                {
                    p.MOIC_ID = int.Parse(reader.Rows[0]["MOIC_ID"].ToString());
                }
                if (!string.IsNullOrEmpty(reader.Rows[0]["TIIC_ID"].ToString()))
                {
                    p.TIIC_ID = int.Parse(reader.Rows[0]["TIIC_ID"].ToString());
                }
                if (!string.IsNullOrEmpty(reader.Rows[0]["COND_ID"].ToString()))
                {
                    p.COND_ID = int.Parse(reader.Rows[0]["COND_ID"].ToString());
                }
                try
                {
                    p.Observacion = reader.Rows[0]["OBSERVACION"].ToString();
                }
                catch (Exception)
                {
                    p.Observacion = "";
                }

                try
                {
                    p.extranjero = bool.Parse(reader.Rows[0]["extranjero"].ToString());
                }
                catch (Exception)
                {
                    p.extranjero = false;
                }
            }
            return p;
        }
        internal bool preentrada_ObtenerXDoc(string doc, string id_site)
        {
            bool encontrado = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            TrailerBC trailer = new TrailerBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILER_X_DOC]");
                accesoDatos.AgregarSqlParametro("@DOC", doc);
                accesoDatos.AgregarSqlParametro("@id_site", id_site);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    encontrado = true;
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
            return encontrado;
        }
        #endregion
    }
}