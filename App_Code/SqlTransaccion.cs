// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Data.SqlClient;

namespace Qanalytics.Data.Access.SqlClient
{
    public sealed class SqlTransaccion
    {
        readonly public static String STRING_CONEXION = "CsString";
        readonly SqlAccesoDatos accesoDatos = new SqlAccesoDatos(STRING_CONEXION);

        #region CargaTipo
        internal DataTable CargaTipo_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TIPO_INGRESO_CARGA]").Tables[0];
        }
        internal CargaTipoBC CargaTipo_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            CargaTipoBC trailer_tipo = new CargaTipoBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_TIPO_INGRESO_CARGA]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    trailer_tipo = this.cargarDatosCargaTipo(accesoDatos.SqlLectorDatos);
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
            return trailer_tipo;
        }
        internal bool CargaTipo_Crear(CargaTipoBC trailer_tipo)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[EDITA_TIPO_INGRESO_CARGA]");
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", trailer_tipo.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@PREINGRESO", trailer_tipo.PREINGRESO);
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
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool CargaTipo_Modificar(CargaTipoBC trailer_tipo)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_TIPO_INGRESO_CARGA]");
            accesoDatos.AgregarSqlParametro("@ID", trailer_tipo.ID);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", trailer_tipo.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@PREINGRESO", trailer_tipo.PREINGRESO);
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
        internal bool CargaTipo_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_TIPO_INGRESO_CARGA]");
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
        internal DataTable CargaTipo_CargaDestinos(int tiic_id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_TIPO_CARGA_DESTINOS] {0}", tiic_id)).Tables[0];
        }
        internal bool CargaTipo_EliminarDestinos(int tiic_id)
        {
            bool exito = false;
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_TIPO_CARGA_DESTINO]");
            try
            {
                accesoDatos.AgregarSqlParametro("@TIIC_ID", tiic_id);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal bool CargaTipo_AsignarDestinos(DataTable dt, int tiic_id)
        {
            bool exito = false;
            if (dt.Rows.Count == 0)
            {
                exito = this.CargaTipo_EliminarDestinos(tiic_id);
            }
            else if (this.CargaTipo_EliminarDestinos(tiic_id))
            {
                accesoDatos.CargarSqlComando("[dbo].[AGREGA_TIPO_CARGA_DESTINO]");
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        accesoDatos.AgregarSqlParametro("@PLAY_ID_DES", Convert.ToInt32(dr["PLAY_ID_DES"].ToString()));
                        accesoDatos.AgregarSqlParametro("@PCTD_ORDEN", Convert.ToInt32(dr["ORDEN"].ToString()));
                        accesoDatos.AgregarSqlParametro("@TIIC_ID", Convert.ToInt32(dr["TIIC_ID"].ToString()));
                        accesoDatos.AgregarSqlParametro("@SITE_ID", Convert.ToInt32(dr["SITE_ID"].ToString()));
                        accesoDatos.EjecutarSqlEscritura();
                        exito = true;
                    }
                    catch (Exception)
                    {
                        exito = false;
                        break;
                    }
                    finally
                    {
                        accesoDatos.LimpiarSqlParametros();
                    }
                }
                accesoDatos.CerrarSqlConeccion();
            }
            else
            {
                exito = false;
            }

            return exito;
        }
        private CargaTipoBC cargarDatosCargaTipo(SqlDataReader reader)
        {
            CargaTipoBC trailer_tipo = new CargaTipoBC();
            trailer_tipo.ID = Convert.ToInt32(reader["ID"]);
            trailer_tipo.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            trailer_tipo.PREINGRESO = Convert.ToBoolean(reader["PREINGRESO"]);
            return trailer_tipo;
        }
        #endregion
        #region CaractCarga
        internal bool AgregarCaracteristica(int play_id, string caracteristicas)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_CARACTERISTICAS_PLAYA_V2]");
            accesoDatos.AgregarSqlParametro("@PLAY_ID", play_id);
            accesoDatos.AgregarSqlParametro("@CARACT", caracteristicas);
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
        }//WORKS
        internal string ObtenerCaracteristicasPlaya(int id)
        {
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_CARACTERISTICAS_PLAYA_X_ID]");
            accesoDatos.AgregarSqlParametro("@ID", id);
            accesoDatos.EjecutarSqlLector();
            string cadena = "";
            bool primero = true;
            while (accesoDatos.SqlLectorDatos.Read())
            {
                if (primero)
                {
                    primero = false;
                }
                else
                {
                    cadena += ",";
                }
                cadena += accesoDatos.SqlLectorDatos["CODIGO"].ToString();
            }
            return cadena;
        }//WORKS
        internal DataTable CaractCarga_ObtenerTodo(string descripcion, string codigo, int cact_id)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_CARACT_CARGA]");
                if (!string.IsNullOrEmpty(descripcion))
                    accesoDatos.AgregarSqlParametro("@CACA_DESC", descripcion);
                if (!string.IsNullOrEmpty(codigo))
                    accesoDatos.AgregarSqlParametro("@CACA_COD", codigo);
                if (cact_id != 0)
                    accesoDatos.AgregarSqlParametro("@CACT_ID", cact_id);
                dt = accesoDatos.EjecutarSqlquery2();
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
            return dt;
        }
        internal CaractCargaBC CaractCarga_ObtenerXId(int id)
        {
            CaractCargaBC carga_tipo = new CaractCargaBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_CARACT_CARGA]");
                accesoDatos.AgregarSqlParametro("@CACA_ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    carga_tipo = cargarDatosCaractCarga(accesoDatos.SqlLectorDatos);
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
            return carga_tipo;
        }
        internal DataTable CaractCarga_desdelocales(string locales, string seleccionados, string trailers = "")
        {
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[COMPARA_CARACT_DESDE_LOCALES_NUEVA_SOLIC]");
            accesoDatos.AgregarSqlParametro("@ID_LOCALES", locales);
            accesoDatos.AgregarSqlParametro("@ID_TRAILERS", trailers);
            accesoDatos.AgregarSqlParametro("@PROPIAS", seleccionados);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable obtenertrailersCompatibles(string locales, string seleccionados, int cantidad_pallets, int id_site, string trailers = "")
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

            String query = "[dbo].[COMPARA_CARACT_TRAILERS_NUEVA_SOLIC] ";
            if (locales != null && locales != String.Empty)
            {
                query += string.Format("@ID_LOCALES = '{0}',", locales);
            }
            else
            {
                query += "@ID_LOCALES = NULL,";
            }

            if (trailers != null && trailers != String.Empty)
            {
                query += string.Format("@ID_TRAILERS = '{0}',", trailers);
            }
            else
            {
                query += "@ID_TRAILERS = NULL,";
            }
            query += string.Format("@cantidad_minima = {0},", cantidad_pallets);
            query += string.Format("@id_site = {0},", id_site);
            if (seleccionados != null && seleccionados != String.Empty)
            {
                query += string.Format("@PROPIAS = '{0}'", seleccionados);
            }
            else
            {
                query += "@PROPIAS = NULL";
            }

            return accesoDatos.dsCargarSqlQuery(query).Tables[0];
        }
        internal DataTable obtenerplayasCompatibles(string locales, string seleccionados, int site_id, string trailers = "")
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

            String query = "[dbo].[COMPARA_CARACT_playa_NUEVA_SOLIC] ";
            query += string.Format("@SITE_ID = '{0}',", site_id);

            if (locales != null && locales != String.Empty)
            {
                query += string.Format("@ID_LOCALES = '{0}',", locales);
            }
            else
            {
                query += "@ID_LOCALES = NULL,";
            }

            if (trailers != null && trailers != String.Empty)
            {
                query += string.Format("@ID_TRAILERS = '{0}',", trailers);
            }
            else
            {
                query += "@ID_TRAILERS = NULL,";
            }

            if (seleccionados != null && seleccionados != String.Empty)
            {
                query += string.Format("@PROPIAS = '{0}'", seleccionados);
            }
            else
            {
                query += "@PROPIAS = NULL";
            }

            return accesoDatos.dsCargarSqlQuery(query).Tables[0];
        }
        internal DataTable CaractCarga_ObtenerXLocalId(int id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_CARACTERISTICAS_X_LOCAL] @ID = {0}", id)).Tables[0];
        }
        internal DataTable CaractCarga_ObtenerCompatibles(string locales, string seleccionados, string trailers = "")
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

            String query = "[dbo].[COMPARA_CARACT_LOCALES_NUEVA_SOLIC] ";
            if (locales != null && locales != String.Empty)
            {
                query += string.Format("@ID_LOCALES = '{0}',", locales);
            }
            else
            {
                query += "@ID_LOCALES = NULL,";
            }

            if (trailers != null && trailers != String.Empty)
            {
                query += string.Format("@ID_TRAILERS = '{0}',", trailers);
            }
            else
            {
                query += "@ID_TRAILERS = NULL,";
            }

            if (seleccionados != null && seleccionados != String.Empty)
            {
                query += string.Format("@PROPIAS = '{0}'", seleccionados);
            }
            else
            {
                query += "@PROPIAS = NULL";
            }

            return accesoDatos.dsCargarSqlQuery(query).Tables[0];
        }
        internal bool CaractCarga_Crear(CaractCargaBC caract_carga)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_CARACT_CARGA]");
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", caract_carga.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@CODIGO", caract_carga.CODIGO);
            accesoDatos.AgregarSqlParametro("@VALOR", caract_carga.VALOR);
            accesoDatos.AgregarSqlParametro("@ID_CARACT_CARGA_TIPO", caract_carga.CACT_ID);
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
        internal bool CaractCarga_Modificar(CaractCargaBC caract_carga)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_CARACT_CARGA]");
            accesoDatos.AgregarSqlParametro("@ID", caract_carga.ID);
            accesoDatos.AgregarSqlParametro("@CODIGO", caract_carga.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", caract_carga.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@VALOR", caract_carga.VALOR);
            accesoDatos.AgregarSqlParametro("@ID_CARACT_CARGA_TIPO", caract_carga.CACT_ID);
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
        internal bool CaractCarga_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_CARACT_CARGA]");
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
        internal CaractCargaBC CaractCarga_ObtenerSeleccionado(int id, string codigo)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            CaractCargaBC carga_tipo = new CaractCargaBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_CARACT_CARGA]");
                if (id != 0 && id != null)
                {
                    accesoDatos.AgregarSqlParametro("@ID", id);
                }
                if (!string.IsNullOrEmpty(codigo))
                {
                    accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                }
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    carga_tipo = this.cargarDatosCaractCarga(accesoDatos.SqlLectorDatos);
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
            return carga_tipo;
        }
        internal DataTable CaractCarga_todoYTipos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_CARACT_CARGA_Y_TIPO]").Tables[0];
        }
        private CaractCargaBC cargarDatosCaractCarga(SqlDataReader reader)
        {
            CaractCargaBC caract_carga = new CaractCargaBC();
            if (reader["ID"] != DBNull.Value)
                caract_carga.ID = Convert.ToInt32(reader["ID"]);
            if (reader["CACT_ID"] != DBNull.Value)
                caract_carga.CACT_ID = Convert.ToInt32(reader["CACT_ID"]);
            if (reader["CARACT_CARGA_TIPO"] != DBNull.Value)
                caract_carga.TIPO_CARACT_CARGA = Convert.ToString(reader["CARACT_CARGA_TIPO"]);
            if (reader["CODIGO"] != DBNull.Value)
                caract_carga.CODIGO = Convert.ToString(reader["CODIGO"]);
            if (reader["DESCRIPCION"] != DBNull.Value)
                caract_carga.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            if (reader["VALOR"] != DBNull.Value)
                caract_carga.VALOR = Convert.ToInt32(reader["VALOR"]);
            if (reader["ORDEN"] != DBNull.Value)
                caract_carga.ORDEN = Convert.ToInt32(reader["ORDEN"]);
            return caract_carga;
        }
        #endregion
        #region CaractTipoCarga
        internal DataTable CaractTipoCarga_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TIPO_CARACT_CARGA]").Tables[0];
        }
        internal CaractTipoCargaBC CaractTipoCarga_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            CaractTipoCargaBC carga_tipo = new CaractTipoCargaBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TIPO_CARACT_CARGA_X_ID]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    carga_tipo = this.cargarDatosCaractTipoCarga(accesoDatos.SqlLectorDatos);
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
            return carga_tipo;
        }
        internal DataTable CaractTipoCarga_ObtenerXParametro(string descripcion)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

            String query = "[dbo].[CARGA_TIPOS_CARACT_CARGA_X_CRITERIO] ";

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
        internal bool CaractTipoCarga_Crear(CaractTipoCargaBC carga_tipo)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_TIPO_CARACT_CARGA]");
            accesoDatos.AgregarSqlParametro("@ID", carga_tipo.ID);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", carga_tipo.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@EXCLUYENTE", carga_tipo.EXCLUYENTE);
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
        internal bool CaractTipoCarga_Modificar(CaractTipoCargaBC carga_tipo)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_TIPO_CARACT_CARGA]");
            accesoDatos.AgregarSqlParametro("@ID", carga_tipo.ID);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", carga_tipo.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@EXCLUYENTE", carga_tipo.EXCLUYENTE);
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
        internal bool CaractTipoCarga_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_TIPO_CARACT_CARGA]");
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
        private CaractTipoCargaBC cargarDatosCaractTipoCarga(SqlDataReader reader)
        {
            CaractTipoCargaBC carga_tipo = new CaractTipoCargaBC();
            carga_tipo.ID = Convert.ToInt32(reader["ID"].ToString());
            carga_tipo.EXCLUYENTE = Convert.ToBoolean(reader["EXCLUYENTE"]);
            carga_tipo.DESCRIPCION = reader["DESCRIPCION"].ToString();
            return carga_tipo;
        }
        #endregion
        #region Cliente
        internal DataTable Cliente_TarifaObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[LISTAR_CLIENTE_TARIFAS]").Tables[0];
        }
        #endregion
        #region Comuna
        public DataTable Comuna_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_COMUNA]").Tables[0];
        }
        public DataTable Comuna_ObtenerXRegion(int idRegion)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_COMUNAS_X_REGION] {0}", idRegion)).Tables[0];
        }
        public ComunaBC Comuna_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            ComunaBC comuna = new ComunaBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_COMUNA_X_ID]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    comuna = this.cargarDatosComuna(accesoDatos.SqlLectorDatos);
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
            return comuna;
        }
        private ComunaBC cargarDatosComuna(SqlDataReader reader)
        {
            ComunaBC comuna = new ComunaBC();
            comuna.ID = Convert.ToInt32(reader["ID"]);
            comuna.NOMBRE = Convert.ToString(reader["NOMBRE"]);
            comuna.ID_REGION = Convert.ToInt32(reader["ID_REGION"]);
            comuna.REGION = Convert.ToString(reader["REGION"]);
            return comuna;
        }
        #endregion
        #region Conductor
        internal DataTable Conductor_ObtenerXParametro(string rut, string nombre, int tran_id)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_CONDUCTOR_v2]");
                if(!string.IsNullOrEmpty(rut))
                accesoDatos.AgregarSqlParametro("@RUT", rut);
                if(!string.IsNullOrEmpty(nombre))
                accesoDatos.AgregarSqlParametro("@NOMBRE", nombre);
                if(tran_id != 0)
                accesoDatos.AgregarSqlParametro("@TRAN_ID", tran_id);
                dt = accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal ConductorBC Conductor_ObtenerXId(int id)
        {
            ConductorBC c = new ConductorBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_CONDUCTOR_v2]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    c = cargaConductor(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception)
            {
                c.ID = 0;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return c;
        }
        internal ConductorBC Conductor_ObtenerXRut(string rut)
        {
            ConductorBC c = new ConductorBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_CONDUCTOR_X_RUT]");
                accesoDatos.AgregarSqlParametro("@RUT", rut);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    c = this.cargaConductor(accesoDatos.SqlLectorDatos);
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
            return c;
        }
        internal ConductorBC Conductor_ObtenerXRutSAP(string rut)
        {
            ConductorBC c = new ConductorBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_CONDUCTOR_X_RUT_SAP]");
                accesoDatos.AgregarSqlParametro("@RUT", rut);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    c = this.cargaConductor(accesoDatos.SqlLectorDatos);
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
            return c;
        }
        internal bool Conductor_Agregar(ConductorBC c)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[EDITA_CONDUCTOR]");
                accesoDatos.AgregarSqlParametro("@RUT", c.RUT);
                accesoDatos.AgregarSqlParametro("@NOMBRE", c.NOMBRE);
                accesoDatos.AgregarSqlParametro("@TRAN_ID", c.TRAN_ID);
                accesoDatos.AgregarSqlParametro("@COND_EXTRANJERO", c.COND_EXTRANJERO);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal int Conductor_AgregarIdentity(ConductorBC c)
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[EDITA_CONDUCTOR]");
                accesoDatos.AgregarSqlParametro("@RUT", c.RUT);
                accesoDatos.AgregarSqlParametro("@NOMBRE", c.NOMBRE);
                accesoDatos.AgregarSqlParametro("@TRAN_ID", c.TRAN_ID);
                accesoDatos.AgregarSqlParametro("@COND_EXTRANJERO", c.COND_EXTRANJERO);
                accesoDatos.EjecutaSqlInsertIdentity();
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return accesoDatos.ID;
        }
        internal bool Conductor_Modificar(ConductorBC c)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[EDITA_CONDUCTOR]");
                accesoDatos.AgregarSqlParametro("@COND_ID", c.ID);
                accesoDatos.AgregarSqlParametro("@NOMBRE", c.NOMBRE);
                accesoDatos.AgregarSqlParametro("@TRAN_ID", c.TRAN_ID);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool Conductor_Eliminar(int id)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[ELIMINA_CONDUCTOR]");
                accesoDatos.AgregarSqlParametro("@COND_ID", id);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool Conductor_Bloquear(int id, string motivo,  int id_user)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[BLOQUEA_CONDUCTOR]");
                accesoDatos.AgregarSqlParametro("@COND_ID", id);
                accesoDatos.AgregarSqlParametro("@COND_MOTIVO_BLOQUEO", motivo);
                accesoDatos.AgregarSqlParametro("@ID_USER", id_user);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool Conductor_Activar(int id)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[ACTIVA_CONDUCTOR]");
                accesoDatos.AgregarSqlParametro("@COND_ID", id);

                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool Conductor_AgregarFoto(int id, string imagen)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[AGREGA_FOTO_CONDUCTOR]");
                accesoDatos.AgregarSqlParametro("@IMAGEN", imagen);
                accesoDatos.AgregarSqlParametro("@COND_ID", id);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        private ConductorBC cargaConductor(SqlDataReader s)
        {
            ConductorBC c = new ConductorBC();
            if (s["COND_ID"] != DBNull.Value)
                c.ID = Convert.ToInt32(s["COND_ID"]);
            if (s["NOMBRE"] != DBNull.Value)
                c.NOMBRE = Convert.ToString(s["NOMBRE"]);
            if (s["TRAN_ID"] != DBNull.Value)
                c.TRAN_ID = Convert.ToInt32(s["TRAN_ID"]);
            if (s["RUT"] != DBNull.Value)
                c.RUT = Convert.ToString(s["RUT"]);
            if (s["COND_BLOQUEADO"] != DBNull.Value)
                c.BLOQUEADO = Convert.ToBoolean(s["COND_BLOQUEADO"]);
            if (s["COND_ACTIVO"] != DBNull.Value)
                c.ACTIVO = Convert.ToBoolean(s["COND_ACTIVO"]);
            if (s["MOTIVO_BLOQUEO"] != DBNull.Value)
                c.MOTIVO_BLOQUEO = Convert.ToString(s["MOTIVO_BLOQUEO"]);
            if (s["COND_EXTRANJERO"] != DBNull.Value)
                c.COND_EXTRANJERO = Convert.ToBoolean(s["COND_EXTRANJERO"]);
            return c;
        }
        #endregion
        #region Destino
        internal DataTable Destino_ObtenerTodo()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_DESTINO]").Tables[0];
        }
        internal DataTable Destino_ObtenerXParametros(string nombre)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_DESTINO]");
                accesoDatos.AgregarSqlParametro("@NOMBRE", nombre);
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
        internal DestinoBC Destino_ObtenerSeleccionado(int id, string codigo)
        {
            DestinoBC d = new DestinoBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_DESTINO]");
                if (id != 0)
                {
                    accesoDatos.AgregarSqlParametro("@ID", id);
                }
                if (!string.IsNullOrEmpty(codigo))
                {
                    accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                }
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    d = this.cargarDatosDestino(accesoDatos.SqlLectorDatos);
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
            return d;
        }
        internal DataTable Destino_ObtenerXTipo(int id_tipo)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_TODO_DESTINO] @DETI_ID = {0}", id_tipo)).Tables[0];
        }
        internal DataTable Destino_ObtenerXTipo(string codigo_tipo)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_TODO_DESTINO] @DETI_CODIGO = {0}", codigo_tipo)).Tables[0];
        }
        internal bool Destino_Agregar(DestinoBC d)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_DESTINO]");
            accesoDatos.AgregarSqlParametro("@CODIGO", d.CODIGO);
            accesoDatos.AgregarSqlParametro("@NOMBRE", d.NOMBRE);
            accesoDatos.AgregarSqlParametro("@DETI_ID", d.DETI_ID);

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
        internal bool Destino_Modificar(DestinoBC d)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_DESTINO]");
            accesoDatos.AgregarSqlParametro("@ID", d.ID);
            accesoDatos.AgregarSqlParametro("@CODIGO", d.CODIGO);
            accesoDatos.AgregarSqlParametro("@NOMBRE", d.NOMBRE);
            accesoDatos.AgregarSqlParametro("@DETI_ID", d.DETI_ID);

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
        internal bool Destino_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_DESTINO]");
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
        private DestinoBC cargarDatosDestino(SqlDataReader s)
        {
            DestinoBC d = new DestinoBC();
            d.ID = Convert.ToInt32(s["ID"]);
            d.CODIGO = Convert.ToString(s["CODIGO"]);
            d.NOMBRE = Convert.ToString(s["NOMBRE"]);
            if (s["DETI_ID"] != DBNull.Value)
                d.DETI_ID = Convert.ToInt32(s["DETI_ID"]);
            return d;
        }
        #endregion
        #region Devolucion
        internal DataTable Devolucion_ObtenerTodo(int site_id)
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_DEVOLUCION]");
                if (site_id != 0)
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex )
            {
                throw ex;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
        }
        internal DevolucionBC Devolucion_ObtenerXId(int devo_id)
        {
            DevolucionBC d = new DevolucionBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_DEVOLUCION]");
                if (devo_id != 0)
                    accesoDatos.AgregarSqlParametro("@devo_id", devo_id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    if (accesoDatos.SqlLectorDatos["DEVO_ID"] != DBNull.Value)
                        d.DEVO_ID = Convert.ToInt32(accesoDatos.SqlLectorDatos["DEVO_ID"]);
                    if (accesoDatos.SqlLectorDatos["SITE_ID"] != DBNull.Value)
                        d.SITE_ID = Convert.ToInt32(accesoDatos.SqlLectorDatos["SITE_ID"]);
                    if (accesoDatos.SqlLectorDatos["TRAI_ID"] != DBNull.Value)
                        d.TRAI_ID = Convert.ToInt32(accesoDatos.SqlLectorDatos["TRAI_ID"]);
                    if (accesoDatos.SqlLectorDatos["DEES_ID"] != DBNull.Value)
                        d.DEES_ID = Convert.ToInt32(accesoDatos.SqlLectorDatos["DEES_ID"]);
                    if (accesoDatos.SqlLectorDatos["DEVO_FH"] != DBNull.Value)
                        d.DEVO_FH = Convert.ToDateTime(accesoDatos.SqlLectorDatos["DEVO_FH"]);
                    if (accesoDatos.SqlLectorDatos["DEVO_OBS"] != DBNull.Value)
                        d.DEVO_OBS = Convert.ToString(accesoDatos.SqlLectorDatos["DEVO_OBS"]);
                    if (accesoDatos.SqlLectorDatos["TRUE_COD_INTERNO_IN_SALIDA"] != DBNull.Value)
                        d.TRUE_COD_INTERNO_IN_SALIDA = Convert.ToInt64(accesoDatos.SqlLectorDatos["TRUE_COD_INTERNO_IN_SALIDA"]);
                    if (accesoDatos.SqlLectorDatos["SOLI_ID_DEVOLUCION"] != DBNull.Value)
                        d.SOLI_ID_DEVOLUCION = Convert.ToInt32(accesoDatos.SqlLectorDatos["SOLI_ID_DEVOLUCION"]);
                    if (accesoDatos.SqlLectorDatos["SOLI_ID_DESCARGA"] != DBNull.Value)
                        d.SOLI_ID_DESCARGA = Convert.ToInt32(accesoDatos.SqlLectorDatos["SOLI_ID_DESCARGA"]);
                    if (accesoDatos.SqlLectorDatos["USUA_ID_DESCARGA"] != DBNull.Value)
                        d.USUA_ID_DESCARGA = Convert.ToInt32(accesoDatos.SqlLectorDatos["USUA_ID_DESCARGA"]);
                    if (accesoDatos.SqlLectorDatos["DEVO_FH_DESCARGA"] != DBNull.Value)
                        d.DEVO_FH_DESCARGA = Convert.ToDateTime(accesoDatos.SqlLectorDatos["DEVO_FH_DESCARGA"]);
                    if (accesoDatos.SqlLectorDatos["SOLI_ID_CARGA"] != DBNull.Value)
                        d.SOLI_ID_CARGA = Convert.ToInt32(accesoDatos.SqlLectorDatos["SOLI_ID_CARGA"]);
                    if (accesoDatos.SqlLectorDatos["USUA_ID_CARGA"] != DBNull.Value)
                        d.USUA_ID_CARGA = Convert.ToInt32(accesoDatos.SqlLectorDatos["USUA_ID_CARGA"]);
                    if (accesoDatos.SqlLectorDatos["DEVO_FH_CARGA"] != DBNull.Value)
                        d.DEVO_FH_CARGA = Convert.ToDateTime(accesoDatos.SqlLectorDatos["DEVO_FH_CARGA"]);
                    if (accesoDatos.SqlLectorDatos["DEVO_FH_CIERRE"] != DBNull.Value)
                        d.DEVO_FH_CIERRE = Convert.ToDateTime(accesoDatos.SqlLectorDatos["DEVO_FH_CIERRE"]);
                    if (accesoDatos.SqlLectorDatos["USUA_ID_ACTUALIZA"] != DBNull.Value)
                        d.USUA_ID_ACTUALIZA = Convert.ToInt32(accesoDatos.SqlLectorDatos["USUA_ID_ACTUALIZA"]);
                    if (accesoDatos.SqlLectorDatos["DEVO_FH_ACTUALIZA"] != DBNull.Value)
                        d.DEVO_FH_ACTUALIZA = Convert.ToDateTime(accesoDatos.SqlLectorDatos["DEVO_FH_ACTUALIZA"]);
                }
                return d;
            }
            catch (Exception ex )
            {
                throw ex;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
        }
        internal bool Devolucion_Reintentar(int devo_id)
        {
            throw new NotImplementedException();
        }
        internal bool Devolucion_Despacho(DevolucionBC d, DataSet ds, out string mensaje) //Solicitud Carga
        {
            bool exito = false;
            mensaje = "";
            accesoDatos.CargarSqlComando("[dbo].[prcDEVOLUCIONES_CREA_DESPACHO]");
            accesoDatos.AgregarSqlParametro("@DEVO_ID", d.DEVO_ID);
            accesoDatos.AgregarSqlParametro("@SITE_ID", d.SITE_ID);
            accesoDatos.AgregarSqlParametro("@TRAI_ID", d.TRAI_ID);
            accesoDatos.AgregarSqlParametro("@FH_PLAN_ANDEN", d.SOLICITUD_CARGA.FECHA_PLAN_ANDEN);
            accesoDatos.AgregarSqlParametro("@PALLETS", d.SOLICITUD_CARGA.Pallets);
            accesoDatos.AgregarSqlParametro("@CACA_ID", d.SOLICITUD_CARGA.CARACTERISTICAS);
            accesoDatos.AgregarSqlParametro("@TETR_ID", d.SOLICITUD_CARGA.TETR_ID);
            accesoDatos.AgregarSqlParametro("@RUTA", d.SOLICITUD_CARGA.RUTA);
            accesoDatos.AgregarSqlParametro("@OBSERVACION", d.SOLICITUD_CARGA.OBSERVACION);
            accesoDatos.AgregarSqlParametro("@ANDENES", ds.Tables["ANDENES"]);
            accesoDatos.AgregarSqlParametro("@LOCALES", ds.Tables["LOCALES"]);
            accesoDatos.AgregarSqlParametro("@USUA_ID", d.USUA_ID_CARGA);
            if (d.SOLICITUD_CARGA.ID_SHORTECK != "0" && d.SOLICITUD_CARGA.ID_SHORTECK != "")
                accesoDatos.AgregarSqlParametro("@SHOR_ID", d.SOLICITUD_CARGA.ID_SHORTECK);
            accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
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
        internal bool Devolucion_Trasvasije(SolicitudBC s, DevolucionBC d, int luga_id_descarga, DataSet ds, out string mensaje) //Solicitud Carga
        {
            bool exito = false;
            mensaje = "";
            accesoDatos.CargarSqlComando("[dbo].[prcDEVOLUCIONES_CREA_TRASVASIJE_DESPACHO]");
            accesoDatos.AgregarSqlParametro("@DEVO_ID", d.DEVO_ID);
            accesoDatos.AgregarSqlParametro("@SITE_ID", s.ID_SITE);
            accesoDatos.AgregarSqlParametro("@TRAI_ID_DESCARGA", d.TRAI_ID);
            accesoDatos.AgregarSqlParametro("@LUGA_ID_DESCARGA", luga_id_descarga);
            accesoDatos.AgregarSqlParametro("@TRAI_ID_CARGA", s.ID_TRAILER_RESERVADO);
            accesoDatos.AgregarSqlParametro("@FH_PLAN_ANDEN", s.FECHA_PLAN_ANDEN);
            accesoDatos.AgregarSqlParametro("@CACA_ID", s.CARACTERISTICAS);
            accesoDatos.AgregarSqlParametro("@TETR_ID", s.TETR_ID);
            accesoDatos.AgregarSqlParametro("@RUTA", s.RUTA);
            accesoDatos.AgregarSqlParametro("@ANDENES", ds.Tables["ANDENES"]);
            accesoDatos.AgregarSqlParametro("@LOCALES", ds.Tables["LOCALES"]);
            accesoDatos.AgregarSqlParametro("@USUA_ID", s.ID_USUARIO);
            if (s.ID_SHORTECK != "0" && s.ID_SHORTECK != "")
                accesoDatos.AgregarSqlParametro("@SHOR_ID", s.ID_SHORTECK);
            accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
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
        internal bool Devolucion_Descargar(DevolucionBC d, out string mensaje) //Solicitud Carga
        {
            bool exito = false;
            mensaje = "";
            accesoDatos.CargarSqlComando("[dbo].[prcDEVOLUCIONES_CREA_DESCARGA]");
            accesoDatos.AgregarSqlParametro("@DEVO_ID", d.DEVO_ID);
            accesoDatos.AgregarSqlParametro("@SITE_ID", d.SITE_ID);
            accesoDatos.AgregarSqlParametro("@TRAI_ID", d.TRAI_ID);
            accesoDatos.AgregarSqlParametro("@USUA_ID", d.USUA_ID_ACTUALIZA);
            accesoDatos.AgregarSqlParametro("@LUGA_ID_DESCARGA", d.SOLICITUD_DESCARGA.ID_DESTINO);
            accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
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
        internal bool Devolucion_FinalizarDescarga(DevolucionBC d, out string mensaje)
        {
            bool exito = false;
            mensaje = "";
            accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_DESCARGA_ESTADO_COMPLETA_ASIGNADA_DEVOLUCION]");
            accesoDatos.AgregarSqlParametro("@DEVO_ID", d.DEVO_ID);
            accesoDatos.AgregarSqlParametro("@SOLI_ID", d.SOLI_ID_DESCARGA);
            accesoDatos.AgregarSqlParametro("@USUA_ID", d.USUA_ID_DESCARGA);
            accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
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

        internal bool Devolucion_FinalizarDevolucion(DevolucionBC d, out string mensaje)
        {
            bool exito = false;
            mensaje = "";
            accesoDatos.CargarSqlComando("[dbo].[prcSOLICITUD_DESCARGA_ESTADO_COMPLETA_ASIGNADA_DEVOLUCION]");
            accesoDatos.AgregarSqlParametro("@DEVO_ID", d.DEVO_ID);
            accesoDatos.AgregarSqlParametro("@SOLI_ID", d.SOLI_ID_DESCARGA);
            accesoDatos.AgregarSqlParametro("@USUA_ID", d.USUA_ID_DESCARGA);
            accesoDatos.AgregarSqlParametroOUT("@ERROR", exito);
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


        #endregion
        #region Empresa
        internal DataTable Empresa_ObtenerTodas()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_EMPRESA]").Tables[0];
        }
        internal EmpresaBC Empresa_ObtenerEmpresaXRut(string rut)
        {
            EmpresaBC empresa = new EmpresaBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_EMPRESA_X_RUT]");
                accesoDatos.AgregarSqlParametro("@RUT", rut);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    empresa = this.cargarDatosEmpresa(accesoDatos.SqlLectorDatos);
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
            return empresa;
        }
        internal EmpresaBC Empresa_ObtenerEmpresaXId(int id)
        {
            EmpresaBC empresa = new EmpresaBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_EMPRESA_X_ID]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    empresa = this.cargarDatosEmpresa(accesoDatos.SqlLectorDatos);
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
            return empresa;
        }
        internal bool Empresa_Crear(EmpresaBC empresa)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_EMPRESA]");
            accesoDatos.AgregarSqlParametro("@CODIGO", empresa.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", empresa.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@RUT", empresa.RUT);
            accesoDatos.AgregarSqlParametro("@RAZON_SOCIAL", empresa.RAZON_SOCIAL);
            accesoDatos.AgregarSqlParametro("@DIRECCION", empresa.DIRECCION);
            accesoDatos.AgregarSqlParametro("@ID_COMUNA", empresa.ID_COMUNA);
            accesoDatos.AgregarSqlParametro("@GIRO", empresa.GIRO);
            accesoDatos.AgregarSqlParametro("@NOMBRE_FANTASIA", empresa.NOMBRE_FANTASIA);
            accesoDatos.AgregarSqlParametro("@LATITUD", empresa.LATITUD);
            accesoDatos.AgregarSqlParametro("@LONGITUD", empresa.LONGITUD);
            accesoDatos.AgregarSqlParametro("@TELEFONO", empresa.TELEFONO);
            accesoDatos.AgregarSqlParametro("@EMAIL", empresa.EMAIL);
            accesoDatos.AgregarSqlParametro("@NOMBRE_CONTACTO", empresa.NOMBRE_CONTACTO);
            accesoDatos.AgregarSqlParametro("@ACTIVO", empresa.ACTIVO);
            accesoDatos.AgregarSqlParametro("@BODEGA", empresa.BODEGA);
            accesoDatos.AgregarSqlParametro("@USR_CREACION", empresa.USR_CREACION);
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
        internal bool Empresa_Modificar(EmpresaBC empresa)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_EMPRESA]");
            accesoDatos.AgregarSqlParametro("@ID", empresa.ID);
            accesoDatos.AgregarSqlParametro("@CODIGO", empresa.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", empresa.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@RUT", empresa.RUT);
            accesoDatos.AgregarSqlParametro("@RAZON_SOCIAL", empresa.RAZON_SOCIAL);
            accesoDatos.AgregarSqlParametro("@DIRECCION", empresa.DIRECCION);
            accesoDatos.AgregarSqlParametro("@ID_COMUNA", empresa.ID_COMUNA);
            accesoDatos.AgregarSqlParametro("@GIRO", empresa.GIRO);
            accesoDatos.AgregarSqlParametro("@NOMBRE_FANTASIA", empresa.NOMBRE_FANTASIA);
            accesoDatos.AgregarSqlParametro("@LATITUD", empresa.LATITUD);
            accesoDatos.AgregarSqlParametro("@LONGITUD", empresa.LONGITUD);
            accesoDatos.AgregarSqlParametro("@TELEFONO", empresa.TELEFONO);
            accesoDatos.AgregarSqlParametro("@EMAIL", empresa.EMAIL);
            accesoDatos.AgregarSqlParametro("@NOMBRE_CONTACTO", empresa.NOMBRE_CONTACTO);
            accesoDatos.AgregarSqlParametro("@ACTIVO", empresa.ACTIVO);
            accesoDatos.AgregarSqlParametro("@BODEGA", empresa.BODEGA);
            accesoDatos.AgregarSqlParametro("@USR_MODIFICACION", empresa.USR_MODIFICACION);
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
        internal bool Empresa_Eliminar(int id)
        {
            bool esExitosa = false;
            SqlAccesoDatos accesoDatos;
            accesoDatos = null;
            accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[ELIMINA_EMPRESA]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlEscritura();
                esExitosa = true;
            }
            catch (Exception ex)
            {
                esExitosa = false;
                throw ex;
            }
            return esExitosa;
        }
        internal DataTable Empresa_ObtenerXParametro(String rut, String razon_social, String nombre_fantasia)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

            String query = "[dbo].[CARGA_EMPRESAS_X_CRITERIO] ";

            if (rut != null && rut != String.Empty)
            {
                query += string.Format("@RUT = N'{0}',", rut);
            }
            else
            {
                query += "@RUT = NULL,";
            }

            if (razon_social != null && razon_social != String.Empty)
            {
                query += string.Format("@RAZON_SOCIAL = N'{0}',", razon_social);
            }
            else
            {
                query += "@RAZON_SOCIAL = NULL,";
            }

            if (nombre_fantasia != null && nombre_fantasia != String.Empty)
            {
                query += string.Format("@NOMBRE_FANTASIA = N'{0}'", nombre_fantasia);
            }
            else
            {
                query += "@NOMBRE_FANTASIA = NULL";
            }

            return accesoDatos.dsCargarSqlQuery(query).Tables[0];
        }
        private EmpresaBC cargarDatosEmpresa(SqlDataReader reader)
        {
            EmpresaBC empresa = new EmpresaBC();
            if (reader["ID"] != DBNull.Value)
                empresa.ID = Convert.ToInt32(reader["ID"]);
            if (reader["DESCRIPCION"] != DBNull.Value)
                empresa.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            if (reader["CODIGO"] != DBNull.Value)
                empresa.CODIGO = Convert.ToString(reader["CODIGO"]);
            if (reader["RUT"] != DBNull.Value)
                empresa.RUT = Convert.ToString(reader["RUT"]);
            if (reader["RAZON_SOCIAL"] != DBNull.Value)
                empresa.RAZON_SOCIAL = Convert.ToString(reader["RAZON_SOCIAL"]);
            if (reader["DIRECCION"] != DBNull.Value)
                empresa.DIRECCION = Convert.ToString(reader["DIRECCION"]);
            if (reader["EMAIL"] != DBNull.Value)
                empresa.EMAIL = Convert.ToString(reader["EMAIL"]);
            if (reader["ID_COMUNA"] != DBNull.Value)
                empresa.ID_COMUNA = Convert.ToInt32(reader["ID_COMUNA"]);
            if (reader["COMUNA"] != DBNull.Value)
                empresa.COMUNA = Convert.ToString(reader["COMUNA"]);
            if (reader["GIRO"] != DBNull.Value)
                empresa.GIRO = Convert.ToString(reader["GIRO"]);
            if (reader["NOMBRE_FANTASIA"] != DBNull.Value)
                empresa.NOMBRE_FANTASIA = Convert.ToString(reader["NOMBRE_FANTASIA"]);
            if (reader["LATITUD"] != DBNull.Value)
                empresa.LATITUD = Convert.ToDouble(reader["LATITUD"]);
            if (reader["LONGITUD"] != DBNull.Value)
                empresa.LONGITUD = Convert.ToDouble(reader["LONGITUD"]);
            if (reader["TELEFONO"] != DBNull.Value)
                empresa.TELEFONO = Convert.ToString(reader["TELEFONO"]);
            if (reader["NOMBRE_CONTACTO"] != DBNull.Value)
                empresa.NOMBRE_CONTACTO = Convert.ToString(reader["NOMBRE_CONTACTO"]);
            if (reader["ACTIVO"] != DBNull.Value)
                empresa.ACTIVO = Convert.ToBoolean(reader["ACTIVO"]);
            if (reader["BODEGA"] != DBNull.Value)
                empresa.BODEGA = Convert.ToString(reader["BODEGA"]);
            if (reader["FECHA_CREACION"] != DBNull.Value)
                empresa.FECHA_CREACION = Convert.ToString(reader["FECHA_CREACION"]);
            if (reader["FECHA_MODIFICACION"] != DBNull.Value)
                empresa.FECHA_MODIFICACION = Convert.ToString(reader["FECHA_MODIFICACION"]);
            if (reader["USR_CREACION"] != DBNull.Value)
                empresa.USR_CREACION = Convert.ToString(reader["USR_CREACION"]);
            if (reader["USR_MODIFICACION"] != DBNull.Value)
                empresa.USR_MODIFICACION = Convert.ToString(reader["USR_MODIFICACION"]);
            empresa.EXISTE = (reader.HasRows);
            return empresa;
        }
        #endregion
        #region Jornada
        internal DataTable Jornada_ObtenerTodas()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_JORNADA]").Tables[0];
        }
        #endregion
        #region Local
        internal DataTable Local_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_LOCAL]").Tables[0];
        }
        internal LocalBC Local_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            LocalBC local = new LocalBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_LOCAL]");
                accesoDatos.AgregarSqlParametro("@LOCA_ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    local = this.cargarDatosLocal(accesoDatos.SqlLectorDatos);
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
            return local;
        }
        internal LocalBC Local_ObtenerXCodigo(string codigo)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            LocalBC local = new LocalBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_LOCAL]");
                accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    local = this.cargarDatosLocal(accesoDatos.SqlLectorDatos);
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
            return local;
        }
        internal DataTable Local_ObtenerXParametro(string descripcion, int idComuna, int idRegion, string codigo)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_LOCALES_X_CRITERIO_V2]");
                if (!string.IsNullOrEmpty(codigo))
                    accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                if (!string.IsNullOrEmpty(descripcion))
                    accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
                if (idComuna != 0)
                    accesoDatos.AgregarSqlParametro("@COMUNA", idComuna);
                if (idRegion != 0)
                    accesoDatos.AgregarSqlParametro("@REGION", idRegion);
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
                accesoDatos.LimpiarSqlParametros();
            }
        }
        internal bool Local_Crear(LocalBC local)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_LOCAL_V3]");
            accesoDatos.AgregarSqlParametro("@CODIGO", local.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", local.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@DIRECCION", local.DIRECCION);
            accesoDatos.AgregarSqlParametro("@COMUNA", local.COMUNA_ID);
            accesoDatos.AgregarSqlParametro("@REGION", local.REGION_ID);
            accesoDatos.AgregarSqlParametro("@INTERNOS", local.INTERNOS);
            accesoDatos.AgregarSqlParametro("@EXTERNOS", local.EXTERNOS);
            accesoDatos.AgregarSqlParametro("@EXCLUYENTES", local.EXCLUYENTES);
            accesoDatos.AgregarSqlParametro("@NO_EXCLUYENTES", local.NO_EXCLUYENTES);
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
        internal bool Local_Modificar(LocalBC local)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_LOCAL_V3]");
            accesoDatos.AgregarSqlParametro("@ID", local.ID);
            accesoDatos.AgregarSqlParametro("@CODIGO", local.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", local.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@DIRECCION", local.DIRECCION);
            accesoDatos.AgregarSqlParametro("@COMUNA", local.COMUNA_ID);
            accesoDatos.AgregarSqlParametro("@REGION", local.REGION_ID);
            accesoDatos.AgregarSqlParametro("@INTERNOS", local.INTERNOS);
            accesoDatos.AgregarSqlParametro("@EXTERNOS", local.EXTERNOS);
            accesoDatos.AgregarSqlParametro("@EXCLUYENTES", local.EXCLUYENTES);
            accesoDatos.AgregarSqlParametro("@NO_EXCLUYENTES", local.NO_EXCLUYENTES);
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
        internal bool Local_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_LOCAL_V2]");
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
        private LocalBC cargarDatosLocal(SqlDataReader reader)
        {
            LocalBC local = new LocalBC();
            if (reader["ID"] != DBNull.Value)
                local.ID = Convert.ToInt32(reader["ID"]);
            if (reader["CODIGO"] != DBNull.Value)
                local.CODIGO = Convert.ToString(reader["CODIGO"]);
            if (reader["CODIGO2"] != DBNull.Value)
                local.CODIGO2 = Convert.ToString(reader["CODIGO2"]);
            if (reader["DESCRIPCION"] != DBNull.Value)
                local.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            if (reader["DIRECCION"] != DBNull.Value)
                local.DIRECCION = Convert.ToString(reader["DIRECCION"]);
            if (reader["COMU_ID"] != DBNull.Value)
                local.COMUNA_ID = Convert.ToInt32(reader["COMU_ID"]);
            if (reader["MAXIMO"] != DBNull.Value)
                local.VALOR_CARACT_MAXIMO = Convert.ToInt32(reader["MAXIMO"]);
            if (reader["REGI_ID"] != DBNull.Value)
                local.REGION_ID = Convert.ToInt32(reader["REGI_ID"]);
            if (reader["INTERNOS"] != DBNull.Value)
                local.INTERNOS = Convert.ToInt32(reader["INTERNOS"]);
            if (reader["EXTERNOS"] != DBNull.Value)
                local.EXTERNOS = Convert.ToInt32(reader["EXTERNOS"]);
            if (reader["CARACTERISTICAS"] != DBNull.Value)
                local.CARACTERISTICAS = Convert.ToString(reader["CARACTERISTICAS"]);
            if (reader["EXCLUYENTES"] != DBNull.Value)
                local.EXCLUYENTES = Convert.ToString(reader["EXCLUYENTES"]);
            if (reader["NO_EXCLUYENTES"] != DBNull.Value)
                local.NO_EXCLUYENTES = Convert.ToString(reader["NO_EXCLUYENTES"]);
            return local;
        }
        #endregion
        #region Lugar
        internal LugarBC Lugar_ObtenerAuto(int site_id, int usua_id, string luga_id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            LugarBC lugar = new LugarBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_LUGAR_AUTO]");
                if (site_id != 0)
                {
                    accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                    accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
                }
                else
                {
                    accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);
                }
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    lugar = this.cargarDatosLugar(accesoDatos.SqlLectorDatos);
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
            return lugar;
        }
        internal DataTable Lugar_ObtenerTodos(int ocupados, int lues_id, int trai_id, int play_id, int zona_id, int site_id, int playa_tipo)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_LUGAR]");
                if (ocupados != -1)
                    accesoDatos.AgregarSqlParametro("@OCUPADOS", ocupados);
                if (lues_id != -1)
                    accesoDatos.AgregarSqlParametro("@LUES_ID", lues_id);
                if (trai_id != -1)
                    accesoDatos.AgregarSqlParametro("@TRAI_ID", trai_id);
                if (play_id != 0)
                    accesoDatos.AgregarSqlParametro("@PLAY_ID", play_id);
                if (zona_id != 0)
                    accesoDatos.AgregarSqlParametro("@ZONA_ID", zona_id);
                if (site_id != 0)
                    accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                if (playa_tipo != 0)
                    accesoDatos.AgregarSqlParametro("@PYTI_ID", playa_tipo);
                dt = accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal DataTable Lugar_ObtenerTodos(int site_id, int zona_id, int play_id, int playa_tipo)
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_LUGAR]");
                if (play_id != 0)
                    accesoDatos.AgregarSqlParametro("@PLAY_ID", play_id);
                if (zona_id != 0)
                    accesoDatos.AgregarSqlParametro("@ZONA_ID", zona_id);
                if (site_id != 0)
                    accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                if (playa_tipo != 0)
                    accesoDatos.AgregarSqlParametro("@PYTI_ID", playa_tipo);
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
        internal DataTable Lugar_ObtenerXPlaya(int playa_id, int ocupados, int lues_id, int trai_id)
        {
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_LUGAR]");
            accesoDatos.AgregarSqlParametro("@PLAY_ID", playa_id);
            if (ocupados != -1)
                accesoDatos.AgregarSqlParametro("@OCUPADOS", ocupados);
            if (lues_id != -1)
                accesoDatos.AgregarSqlParametro("@LUES_ID", lues_id);
            if (trai_id != -1)
                accesoDatos.AgregarSqlParametro("@TRAI_ID", trai_id);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Lugar_ObtenerXSolicitud(int site_id, int playa_id, int ocupados, int lues_id, int trai_id, int solicitud_tipo)
        {
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_LUGAR_SOLICITUD]");

            accesoDatos.AgregarSqlParametro("@site_id", site_id);
            accesoDatos.AgregarSqlParametro("@solicitud_tipo", solicitud_tipo);
            accesoDatos.AgregarSqlParametro("@PLAY_ID", playa_id);
            if (ocupados != -1)
            {
                accesoDatos.AgregarSqlParametro("@OCUPADOS", ocupados);
            }
            if (lues_id != -1)
            {
                accesoDatos.AgregarSqlParametro("@LUES_ID", lues_id);
            }
            if (trai_id != -1)
            {
                accesoDatos.AgregarSqlParametro("@TRAI_ID", trai_id);
            }
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Lugar_ObtenerXZona(int zona_id, int ocupados, int lues_id, int trai_id)
        {
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_LUGAR]");
            accesoDatos.AgregarSqlParametro("@ZONA_ID", zona_id);
            if (ocupados != -1)
            {
                accesoDatos.AgregarSqlParametro("@OCUPADOS", ocupados);
            }
            if (lues_id != -1)
            {
                accesoDatos.AgregarSqlParametro("@LUES_ID", lues_id);
            }
            if (trai_id != -1)
            {
                accesoDatos.AgregarSqlParametro("@TRAI_ID", trai_id);
            }
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Lugar_ObtenerXSite(int site_id, int ocupados, int lues_id, int trai_id)
        {
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_LUGAR]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            if (ocupados != -1)
            {
                accesoDatos.AgregarSqlParametro("@OCUPADOS", ocupados);
            }
            if (lues_id != -1)
            {
                accesoDatos.AgregarSqlParametro("@LUES_ID", lues_id);
            }
            if (trai_id != -1)
            {
                accesoDatos.AgregarSqlParametro("@TRAI_ID", trai_id);
            }
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Lugar_ObtenerLugarXPlayaZona()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_LUGAR_PLAYA_ZONA]").Tables[0];
        }
        internal DataTable Lugar_ObtenerLugarXPlayaDrop(int id_playa)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_LUGARES_X_PLAYA_DROP] {0}", id_playa)).Tables[0];
        }
   
        internal DataTable Lugar_ObtenerLugarEstado(int id_playa, int id_lugar,  int id_site)
        {
        //    return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[prcESTADO_LUGAR_X_PLAYA] @PLAY_ID = {0}, @LUGA_ID = {1}", id_playa, id_lugar)).Tables[0];
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[prcESTADO_LUGAR_X_PLAYA_2]");
                if (id_site != 0)
                    accesoDatos.AgregarSqlParametro("@SITE_ID", id_site);
                if (id_playa != 0)
                    accesoDatos.AgregarSqlParametro("@PLAY_ID", id_playa);
                if (id_lugar!=0) 
                    accesoDatos.AgregarSqlParametro("@LUGA_ID", id_lugar);
                return accesoDatos.EjecutarSqlquery2();
             
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
       
        
        }
            internal DataTable Lugar_ObtenerSolicitudesPendientesXLugar(int id_lugar)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[SOLICITUDES_CARGA_PENDIENTES_X_LUGAR] {0}", id_lugar)).Tables[0];
        }
        internal DataTable Lugar_ObtenerTodoLugarPlayaZona()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_LUGAR_PLAYA_ZONA]").Tables[0];
        }
        internal DataTable Lugar_ObtenerGuardias()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_CASETAS_GUARDIA]").Tables[0];
        }
        internal LugarBC Lugar_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            LugarBC lugar = new LugarBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_LUGAR]");
                accesoDatos.AgregarSqlParametro("@LUGA_ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    lugar = this.cargarDatosLugar(accesoDatos.SqlLectorDatos);
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
            return lugar;
        }
        internal DataTable Lugar_ObtenerXParametro(string codigo, string descripcion, int site_id, bool ocupado, int zona_id, int playa_id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

            String query = "[dbo].[CARGA_LUGARES_X_CRITERIO] ";

            if (codigo != null && codigo != String.Empty)
            {
                query += string.Format("@CODIGO = N'{0}',", codigo);
            }
            else
            {
                query += "@CODIGO = NULL,";
            }

            if (descripcion != null && descripcion != String.Empty)
            {
                query += string.Format("@DESCRIPCION = N'{0}',", descripcion);
            }
            else
            {
                query += "@DESCRIPCION = NULL,";
            }

            if (site_id != null && site_id != 0)
            {
                query += string.Format("@SITE_ID = {0},", site_id);
            }
            else
            {
                query += "@SITE_ID = NULL,";
            }

            if (zona_id != null && zona_id != 0)
            {
                query += string.Format("@ZONA_ID = {0},", zona_id);
            }
            else
            {
                query += "@ZONA_ID = NULL,";
            }

            if (playa_id != null && playa_id != 0)
            {
                query += string.Format("@PLAYA_ID = {0},", playa_id);
            }
            else
            {
                query += "@PLAYA_ID = NULL,";
            }

            if (ocupado)
            {
                query += "@OCUPADO = FALSE";
            }
            else
            {
                query += "@OCUPADO = NULL";
            }

            return accesoDatos.dsCargarSqlQuery(query).Tables[0];
        }
        internal bool Lugar_Crear(LugarBC lugar)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_LUGAR]");
            accesoDatos.AgregarSqlParametro("@CODIGO", lugar.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", lugar.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@ID_SITE", lugar.ID_SITE);
            //accesoDatos.AgregarSqlParametro("@POSICION", lugar.POSICION);
            //accesoDatos.AgregarSqlParametro("@LUGAR_X", lugar.LUGAR_X);
            //accesoDatos.AgregarSqlParametro("@LUGAR_Y", lugar.LUGAR_Y);
            //accesoDatos.AgregarSqlParametro("@ORDEN", lugar.ORDEN);
            //accesoDatos.AgregarSqlParametro("@ANCHO", lugar.ANCHO);
            //accesoDatos.AgregarSqlParametro("@ALTO", lugar.ALTO);
            //accesoDatos.AgregarSqlParametro("@ROTACION", lugar.ROTACION);
            accesoDatos.AgregarSqlParametro("@ID_ZONA", lugar.ID_ZONA);
            accesoDatos.AgregarSqlParametro("@ID_PLAYA", lugar.ID_PLAYA);
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
        internal bool Lugar_Modificar(LugarBC lugar)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_LUGAR]");
            accesoDatos.AgregarSqlParametro("@ID", lugar.ID);
            accesoDatos.AgregarSqlParametro("@CODIGO", lugar.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", lugar.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@ID_SITE", lugar.ID_SITE);
            accesoDatos.AgregarSqlParametro("@ID_ZONA", lugar.ID_ZONA);
            accesoDatos.AgregarSqlParametro("@ID_PLAYA", lugar.ID_PLAYA);
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
        internal bool Lugar_Habilitar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[HABILITAR_DESHABILITAR_LUGAR]");
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
        internal bool Lugar_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_LUGAR_V2]");
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
        internal bool Lugar_Cuadratura(int luga_id, int usua_id, TrailerBC trai, string cargado, out string error)
        {
            bool exito = false;
            error = "";
            int errorint = 0;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[prcLUGAR_CUADRATURA_WEB]");
            accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);
            if (trai.ID != 0)
            {
                accesoDatos.AgregarSqlParametro("@TRAI_ID", trai.ID);
                accesoDatos.AgregarSqlParametro("@TRAC_PLACA", DBNull.Value);
                //    accesoDatos.AgregarSqlParametro("@TRAI_NUMERO", trai.NUMERO);

                accesoDatos.AgregarSqlParametro("@trai_cargado", cargado);
            }
            else if (trai.PATENTE_TRACTO != "")
            {
                accesoDatos.AgregarSqlParametro("@TRAI_ID", DBNull.Value);
                accesoDatos.AgregarSqlParametro("@TRAC_PLACA", trai.PATENTE_TRACTO);
                accesoDatos.AgregarSqlParametro("@trai_cargado", DBNull.Value);
                //  accesoDatos.AgregarSqlParametro("@TRAI_NUMERO", DBNull.Value);
            }
            else
            {
                accesoDatos.AgregarSqlParametro("@TRAI_ID", DBNull.Value);
                accesoDatos.AgregarSqlParametro("@TRAC_PLACA", DBNull.Value);
                //  accesoDatos.AgregarSqlParametro("@TRAI_NUMERO", DBNull.Value);
                accesoDatos.AgregarSqlParametro("@trai_cargado", DBNull.Value);
            }

            accesoDatos.AgregarSqlParametro("@TRUE_OBS", "");

            accesoDatos.AgregarSqlParametro("@TRNP_ID", DBNull.Value);
            accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
            SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", error);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            accesoDatos.AgregarSqlParametro("@error", errorint).Direction = ParameterDirection.Output;

            try
            {
                accesoDatos.EjecutarSqlEscritura();
                error = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        private LugarBC cargarDatosLugar(SqlDataReader reader)
        {
            LugarBC lugar = new LugarBC();
            if (reader["ID"] != DBNull.Value)
                lugar.ID = Convert.ToInt32(reader["ID"]);
            if (reader["CODIGO"] != DBNull.Value)
                lugar.CODIGO = Convert.ToString(reader["CODIGO"]);
            if (reader["DESCRIPCION"] != DBNull.Value)
                lugar.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            if (reader["ID_SITE"] != DBNull.Value)
                lugar.ID_SITE = Convert.ToInt32(reader["ID_SITE"]);
            if (reader["ID_ESTADO"] != DBNull.Value)
                lugar.ID_ESTADO = Convert.ToInt32(reader["ID_ESTADO"]);
            if (reader["OCUPADO"] != DBNull.Value)
                lugar.OCUPADO = Convert.ToBoolean(reader["OCUPADO"]);
            if (reader["PLAY_ID"] != DBNull.Value)
                lugar.ID_PLAYA = Convert.ToInt32(reader["PLAY_ID"]);
            if (reader["PLAYA"] != DBNull.Value)
                lugar.PLAYA = Convert.ToString(reader["PLAYA"]);
            if (reader["ZONA_ID"] != DBNull.Value)
                lugar.ID_ZONA = Convert.ToInt32(reader["ZONA_ID"]);
            if (reader["ZONA"] != DBNull.Value)
                lugar.ZONA = Convert.ToString(reader["ZONA"]);
            return lugar;
        }
        #endregion
        #region Playa
        internal DataTable Playa_ObtenerTodas(int zona_id, int site_id, int pyti_id)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_PLAYA]");
                if (zona_id != 0)
                {
                    accesoDatos.AgregarSqlParametro("@ZONA_ID", zona_id);
                }
                if (site_id != 0)
                {
                    accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                }
                if (pyti_id != 0)
                {
                    accesoDatos.AgregarSqlParametro("@PYTI_ID", pyti_id);
                }
                dt = accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal DataTable Playa_ObtenerDrop(int site_id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_PLAYAS_FILTRO] @ID_SITE={0}", site_id)).Tables[0];
        }
        internal DataTable Playa_ObtenerDrop(int site_id, int zona_id)
        {
            string query = string.Format("[dbo].[CARGA_PLAYAS_FILTRO] @ID_SITE={0}", site_id);
            if (zona_id != 0)
            {
                query += string.Format(",@ID_ZONA={0}", zona_id);
            }
            return accesoDatos.dsCargarSqlQuery(query).Tables[0];
        }
        internal PlayaBC Playa_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            PlayaBC playa = new PlayaBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_PLAYA_X_ID]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    playa = this.cargarDatosPlaya(accesoDatos.SqlLectorDatos);
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
            return playa;
        }
        internal DataTable Playa_ObtenerXParametrotipo_carga(string codigo, string descripcion, int zona_id, bool is_virtual, int tipo_carga, string playa_tipo = null)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_PLAYAS_X_CRITERIO_tipo_carga]");

            if (!String.IsNullOrEmpty(codigo))
            {
                accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
            }

            if (!String.IsNullOrEmpty(descripcion))
            {
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
            }

            if (is_virtual)
            {
                accesoDatos.AgregarSqlParametro("@VIRTUAL", 0);
            }

            if (zona_id != null && zona_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ZONA_ID", zona_id);
            }

            if (tipo_carga != null && tipo_carga != 0)
            {
                accesoDatos.AgregarSqlParametro("@tipo_carga", tipo_carga);
            }
            else
            {
                accesoDatos.AgregarSqlParametro("@tipo_carga", DBNull.Value);
            }

            if (playa_tipo != null && playa_tipo != "")
            {
                accesoDatos.AgregarSqlParametro("@playa_tipo", playa_tipo);
            }

            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Playa_ObtenerXParametro(string codigo, string descripcion, int zona_id, bool is_virtual, string playa_tipo = null)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_PLAYAS_X_CRITERIO]");

            if (!String.IsNullOrEmpty(codigo))
            {
                accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
            }

            if (!String.IsNullOrEmpty(descripcion))
            {
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
            }

            if (is_virtual)
            {
                accesoDatos.AgregarSqlParametro("@VIRTUAL", 0);
            }

            if (zona_id != null && zona_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ZONA_ID", zona_id);
            }

            if (playa_tipo != null && playa_tipo != "")
            {
                accesoDatos.AgregarSqlParametro("@playa_tipo", playa_tipo);
            }

            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable Playa_ObtenerTipos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TIPO_PLAYA]").Tables[0];
        }
        internal bool Playa_Crear(PlayaBC playa)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_PLAYA]");
            accesoDatos.AgregarSqlParametro("@CODIGO", playa.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", playa.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@ZONA_ID", playa.ZONA_ID);
            accesoDatos.AgregarSqlParametro("@VIRTUAL", playa.VIRTUAL);
            accesoDatos.AgregarSqlParametro("@PYTI_ID", playa.ID_TIPOPLAYA);
            accesoDatos.AgregarSqlParametro("@ZOTI_ID", playa.ID_TIPOZONA);
            accesoDatos.AgregarSqlParametro("@site_id", playa.SITE_ID);
            //accesoDatos.AgregarSqlParametro("@PLAYA_X", playa.PLAYA_X);
            //accesoDatos.AgregarSqlParametro("@PLAYA_Y", playa.PLAYA_Y);
            //accesoDatos.AgregarSqlParametro("@ROTACION", playa.ROTACION);
            //accesoDatos.AgregarSqlParametro("@ALTO", playa.ALTO);
            //accesoDatos.AgregarSqlParametro("@ANCHO", playa.ANCHO);
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
        internal bool Playa_Modificar(PlayaBC playa)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_PLAYA]");
            accesoDatos.AgregarSqlParametro("@ID", playa.ID);
            accesoDatos.AgregarSqlParametro("@CODIGO", playa.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", playa.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@ZONA_ID", playa.ZONA_ID);
            accesoDatos.AgregarSqlParametro("@VIRTUAL", playa.VIRTUAL);
            accesoDatos.AgregarSqlParametro("@PYTI_ID", playa.ID_TIPOPLAYA);
            accesoDatos.AgregarSqlParametro("@ZOTI_ID", playa.ID_TIPOZONA);
            accesoDatos.AgregarSqlParametro("@site_id", playa.SITE_ID);
            //accesoDatos.AgregarSqlParametro("@PLAYA_X", playa.PLAYA_X);
            //accesoDatos.AgregarSqlParametro("@PLAYA_Y", playa.PLAYA_Y);
            //accesoDatos.AgregarSqlParametro("@ROTACION", playa.ROTACION);
            //accesoDatos.AgregarSqlParametro("@ALTO", playa.ALTO);
            //accesoDatos.AgregarSqlParametro("@ANCHO", playa.ANCHO);
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
        internal bool Playa_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_PLAYA]");
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
        private PlayaBC cargarDatosPlaya(SqlDataReader reader)
        {
            PlayaBC playa = new PlayaBC();
            if (reader["ID"] != DBNull.Value)
                playa.ID = Convert.ToInt32(reader["ID"]);
            if (reader["CODIGO"] != DBNull.Value)
                playa.CODIGO = Convert.ToString(reader["CODIGO"]);
            if (reader["DESCRIPCION"] != DBNull.Value)
                playa.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            if (reader["ZONA_ID"] != DBNull.Value)
                playa.ZONA_ID = Convert.ToInt32(reader["ZONA_ID"]);
            if (reader["VIRTUAL"] != DBNull.Value)
                playa.VIRTUAL = Convert.ToBoolean(reader["VIRTUAL"]);
            if (reader["PLAYA_X"] != DBNull.Value)
                playa.PLAYA_X = Convert.ToDouble(reader["PLAYA_X"]);
            if (reader["PLAYA_Y"] != DBNull.Value)
                playa.PLAYA_Y = Convert.ToDouble(reader["PLAYA_Y"]);
            if (reader["ROTACION"] != DBNull.Value)
                playa.ROTACION = Convert.ToInt32(reader["ROTACION"]);
            if (reader["ALTO"] != DBNull.Value)
                playa.ALTO = Convert.ToDouble(reader["ALTO"]);
            if (reader["ANCHO"] != DBNull.Value)
                playa.ANCHO = Convert.ToDouble(reader["ANCHO"]);
            if (reader["ZOTI_ID"] != DBNull.Value)
                playa.ID_TIPOZONA = Convert.ToInt32(reader["ZOTI_ID"]);
            if (reader["PYTI_ID"] != DBNull.Value)
                playa.ID_TIPOPLAYA = Convert.ToInt32(reader["PYTI_ID"]);
            if (reader["SITE_ID"] != DBNull.Value)
                playa.SITE_ID = Convert.ToInt32(reader["SITE_ID"]);
            return playa;
        }
        #endregion
        #region PlayaOrigenDestinos
        internal DataTable PlayaOD_ObtenerTodas()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_PLAYA_ORIGEN_DESTINOS]").Tables[0];
        }
        internal DataTable PlayaOD_ObtenerXPlayId(int play_id_ori)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_PLAYA_ORIGEN_DESTINOS] @PLAY_ID_ORI={0}", play_id_ori)).Tables[0];
        }
        internal bool PlayaOD_Crear(DataTable dt, int idplaya)
        {
            bool exito = false;
            if (dt.Rows.Count == 0)
            {
                this.PlayaOD_Eliminar(idplaya);
                exito = true;
            }
            else if (this.PlayaOD_Eliminar(Convert.ToInt32(dt.Rows[0]["PLAY_ID_ORI"].ToString())))
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[AGREGA_PLAYA_ORIGEN_DESTINO]");
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        accesoDatos.LimpiarSqlParametros();
                        accesoDatos.AgregarSqlParametro("@PLAY_ID_ORI", Convert.ToInt32(dr["PLAY_ID_ORI"].ToString()));
                        accesoDatos.AgregarSqlParametro("@PLAY_ID_DES", Convert.ToInt32(dr["PLAY_ID_DES"].ToString()));
                        accesoDatos.AgregarSqlParametro("@PLOD_ORDEN", Convert.ToInt32(dr["ORDEN"].ToString()));
                        accesoDatos.AgregarSqlParametro("@TIIC_ID", Convert.ToInt32(dr["TIIC_ID"].ToString()));
                        accesoDatos.AgregarSqlParametro("@SITE_ID", Convert.ToInt32(dr["SITE_ID"].ToString()));
                        accesoDatos.EjecutarSqlEscritura();
                        exito = true;
                    }
                    catch (Exception)
                    {
                        exito = false;
                        break;
                    }
                }
                accesoDatos.CerrarSqlConeccion();
            }
            else
            {
                exito = false;
            }

            return exito;
        }
        private bool PlayaOD_Eliminar(int play_id)
        {
            bool exito = false;
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_PLAYA_ORIGEN_DESTINO]");
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.AgregarSqlParametro("@PLAY_ID", play_id);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        #endregion
        #region PlayaOrigenDestinosSellos
        internal DataTable PlayaODSello_ObtenerTodas()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_PLAYA_ORIGEN_DESTINOS_SELLOS]").Tables[0];
        }
        internal DataTable PlayaODSello_ObtenerXPlayId(int play_id_ori)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_PLAYA_ORIGEN_DESTINOS_SELLOS] @PLAY_ID_ORI={0}", play_id_ori)).Tables[0];
        }
        internal bool PlayaODSello_Crear(DataTable dt, int idplaya)
        {
            bool exito = false;
            if (dt.Rows.Count == 0)
            {
                this.PlayaODSello_Eliminar(idplaya);
                exito = true;
            }
            else if (this.PlayaODSello_Eliminar(Convert.ToInt32(dt.Rows[0]["PLAY_ID_ORI"].ToString())))
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[AGREGA_PLAYA_ORIGEN_DESTINO_SELLOS]");
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        accesoDatos.LimpiarSqlParametros();
                        accesoDatos.AgregarSqlParametro("@PLAY_ID_ORI", Convert.ToInt32(dr["PLAY_ID_ORI"].ToString()));
                        accesoDatos.AgregarSqlParametro("@PLAY_ID_DES", Convert.ToInt32(dr["PLAY_ID_DES"].ToString()));
                        accesoDatos.AgregarSqlParametro("@PLOD_ORDEN", Convert.ToInt32(dr["ORDEN"].ToString()));
                        accesoDatos.AgregarSqlParametro("@SITE_ID", Convert.ToInt32(dr["SITE_ID"].ToString()));
                        accesoDatos.EjecutarSqlEscritura();
                        exito = true;
                    }
                    catch (Exception Ex)
                    {
                        exito = false;
                        throw Ex;
                    }
                }
                accesoDatos.CerrarSqlConeccion();
            }
            else
            {
                exito = false;
            }

            return exito;
        }
        private bool PlayaODSello_Eliminar(int play_id)
        {
            bool exito = false;
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_PLAYA_ORIGEN_DESTINO_SELLOS]");
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.AgregarSqlParametro("@PLAY_ID", play_id);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        #endregion
        #region PlayaOrigenDestinosTipoCarga
        internal DataTable PlayaODTipoCarga_ObtenerTodas()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_PLAYA_ORIGEN_DESTINOS_TIPO_CARGA]").Tables[0];
        }
        internal DataTable PlayaODTipoCarga_ObtenerXPlayId(int play_id_ori)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_PLAYA_ORIGEN_DESTINOS_TIPO_CARGA] @PLAY_ID_ORI={0}", play_id_ori)).Tables[0];
        }
        internal bool PlayaODTipoCarga_Crear(DataTable dt, int idplaya, int tiic_id)
        {
            bool exito = false;
            if (dt.Rows.Count == 0)
            {
                this.PlayaODTipoCarga_Eliminar(idplaya);
                exito = true;
            }
            else if (this.PlayaODTipoCarga_Eliminar(Convert.ToInt32(dt.Rows[0]["PLAY_ID_ORI"].ToString())))
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[AGREGA_PLAYA_ORIGEN_DESTINO_TIPO_CARGA]");
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        accesoDatos.LimpiarSqlParametros();
                        accesoDatos.AgregarSqlParametro("@PLAY_ID_DES", Convert.ToInt32(dr["PLAY_ID_DES"].ToString()));
                        accesoDatos.AgregarSqlParametro("@PLOD_ORDEN", Convert.ToInt32(dr["ORDEN"].ToString()));
                        accesoDatos.AgregarSqlParametro("@SITE_ID", Convert.ToInt32(dr["SITE_ID"].ToString()));
                        accesoDatos.AgregarSqlParametro("@tiic_id", tiic_id);
                        accesoDatos.EjecutarSqlEscritura();
                        exito = true;
                    }
                    catch (Exception Ex)
                    {
                        exito = false;
                        throw Ex;
                    }
                }
                accesoDatos.CerrarSqlConeccion();
            }
            else
            {
                exito = false;
            }

            return exito;
        }
        private bool PlayaODTipoCarga_Eliminar(int play_id)
        {
            bool exito = false;
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_PLAYA_ORIGEN_DESTINO_TIPO_CARGA]");
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.AgregarSqlParametro("@PLAY_ID", play_id);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        #endregion
        #region PreIngreso
        internal DataTable obtener_pre_ingresos(int site, int proveedor, string desde, string hasta)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[PRC_LISTAR_PRE_INGRESOS_YMS]");

            accesoDatos.AgregarSqlParametro("@SITE", site);
            accesoDatos.AgregarSqlParametro("@PROVEEDOR", proveedor);
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);

            try
            {
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        internal bool EliminarPreIngreso(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[prcELIMINAR_PREINGRESO]");
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
        #endregion
        #region PreIngresoReserva
        internal DataTable PreIngresoReserva_ObtenerXParametros(int site_id, DateTime desde, DateTime hasta, int proveedor_id, string numcita, int tipocarga_id, bool preingreso)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_PRE_INGRESO_RESERVA]");
                if (site_id != 0)
                    accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                if (desde > DateTime.MinValue)
                    accesoDatos.AgregarSqlParametro("@DESDE", desde);
                if (hasta > DateTime.MinValue)
                    accesoDatos.AgregarSqlParametro("@HASTA", hasta);
                if (proveedor_id != 0)
                    accesoDatos.AgregarSqlParametro("@PROV_ID", proveedor_id);
                if (string.IsNullOrEmpty(numcita))
                    accesoDatos.AgregarSqlParametro("@NUM_CITA_BUSCAR", numcita);
                if (tipocarga_id != 0)
                    accesoDatos.AgregarSqlParametro("@TIIC_ID", tipocarga_id);
                if (preingreso)
                    accesoDatos.AgregarSqlParametro("@PRE_INGRESO", preingreso);
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
        internal DataRow PreIngresoReserva_ObtenerXNumero(int numcita)
        {
            DataRow dr;
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_PRE_INGRESO_RESERVA]");
                accesoDatos.AgregarSqlParametro("@NUM_CITA", numcita);
                dr = accesoDatos.EjecutarSqlquery2().Rows[0];
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
            return dr;
        }
        internal bool PreIngresoReserva_Editar(int prov_id, int site_id, int prve_id, int num_cita, DateTime fecha, int tiic_id)
        {
            bool exito = false;
            accesoDatos.CargarSqlComando("[dbo].[EDITA_PRE_INGRESO_RESERVA]");
            accesoDatos.AgregarSqlParametro("@PROV_ID", prov_id);
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@PRVE_ID", prve_id);
            accesoDatos.AgregarSqlParametro("@NUM_CITA", num_cita);
            accesoDatos.AgregarSqlParametro("@FECHA", fecha);
            accesoDatos.AgregarSqlParametro("@TIIC_ID", tiic_id);
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
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
            return exito;
        }
        #endregion
        #region Proveedor
        internal ProveedorBC Proveedor_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            ProveedorBC prov = new ProveedorBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_PROVEEDOR]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    prov = this.cargarDatosProveedor(accesoDatos.SqlLectorDatos);
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
            return prov;
        }
        internal DataTable Proveedor_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_PROVEEDOR]").Tables[0];
        }
        internal DataTable Proveedor_ObtenerXParametros(string codigo, string descripcion)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_PROVEEDOR]");
                accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
                dt = accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal DataTable Proveedor_ObtenerXLocales(string codigo, string descripcion)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_PROVEEDOR_LOCAL]");
                accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
                dt = accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal bool Proveedor_Crear(ProveedorBC prov)
        {
            bool exito = false;
            accesoDatos.CargarSqlComando("[dbo].[EDITA_PROVEEDOR]");
            accesoDatos.AgregarSqlParametro("@CODIGO", prov.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", prov.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@DIRECCION", prov.DIRECCION);
            accesoDatos.AgregarSqlParametro("@RUT", prov.RUT);
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
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
            return exito;
        }
        internal bool Proveedor_Modificar(ProveedorBC prov)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_PROVEEDOR]");
            accesoDatos.AgregarSqlParametro("@ID", prov.PROV_ID);
            accesoDatos.AgregarSqlParametro("@CODIGO", prov.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", prov.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@DIRECCION", prov.DIRECCION);
            accesoDatos.AgregarSqlParametro("@RUT", prov.RUT);
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
        internal bool Proveedor_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_PROVEEDOR]");
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
        internal DataTable Proveedor_ObtenerVendorXParametros(int prov_id, string prve_numero)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_PROVEEDORES_VENDOR]");
                if (prov_id != 0)
                    accesoDatos.AgregarSqlParametro("@PROV_ID", prov_id);
                if (string.IsNullOrEmpty(prve_numero))
                    accesoDatos.AgregarSqlParametro("@PRVE_NUMERO_BUSCAR", prve_numero);
                dt = accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal bool Proveedor_ComprobarNroVendor(int prve_numero)
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_PROVEEDORES_VENDOR]");
                accesoDatos.AgregarSqlParametro("@PRVE_NUMERO", prve_numero);
                return (accesoDatos.EjecutarSqlquery2().Rows.Count == 0);
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
        internal bool Proveedor_AgregarVendor(int prov_id, int prve_numero)
        {
            bool exito = false;
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_PROVEEDORES_VENDOR]");
            accesoDatos.AgregarSqlParametro("@PROV_ID", prov_id);
            accesoDatos.AgregarSqlParametro("@PRVE_NUMERO", prve_numero);
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
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
            return exito;
        }
        internal bool Proveedor_EliminarVendor(int prve_id)
        {
            bool exito = false;
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_PROVEEDORES_VENDOR]");
            accesoDatos.AgregarSqlParametro("@PRVE_ID", prve_id);
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
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
            return exito;
        }
        internal ProveedorBC Proveedor_CodigoExiste(int id, string codigo, string rut)
        {
            ProveedorBC p = new ProveedorBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[COMPROBAR_CODIGO_PROVEEDOR]");
                if (id != 0)
                    accesoDatos.AgregarSqlParametro("@ID", id);
                if (!string.IsNullOrEmpty(codigo))
                    accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                if (!string.IsNullOrEmpty(rut))
                    accesoDatos.AgregarSqlParametro("@RUT", rut);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    if (Convert.ToInt32(accesoDatos.SqlLectorDatos["ID"]) != 0)
                        p = cargarDatosProveedor(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
                accesoDatos.LimpiarSqlParametros();
            }
            return p;
        }
        private ProveedorBC cargarDatosProveedor(SqlDataReader reader)
        {
            ProveedorBC prov = new ProveedorBC();
            if (reader["ID"] != DBNull.Value)
                prov.PROV_ID = Convert.ToInt32(reader["ID"]);
            if (reader["CODIGO"] != DBNull.Value)
                prov.CODIGO = Convert.ToString(reader["CODIGO"]);
            if (reader["DESCRIPCION"] != DBNull.Value)
                prov.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            if (reader["DIRECCION"] != DBNull.Value)
                prov.DIRECCION = Convert.ToString(reader["DIRECCION"]);
            if (reader["RUT"] != DBNull.Value)
                prov.RUT = Convert.ToString(reader["RUT"]);
            return prov;
        }
        #endregion
        #region Region
        public DataTable Region_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_REGION]").Tables[0];
        }
        public RegionBC Region_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            RegionBC region = new RegionBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_REGION_X_ID]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    region = this.cargarDatosRegion(accesoDatos.SqlLectorDatos);
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
            return region;
        }
        private RegionBC cargarDatosRegion(SqlDataReader reader)
        {
            RegionBC region = new RegionBC();
            if (reader["ID"] != DBNull.Value)
                region.ID = Convert.ToInt32(reader["ID"]);
            if (reader["CODIGO"] != DBNull.Value)
                region.CODIGO = Convert.ToString(reader["CODIGO"]);
            if (reader["NOMBRE"] != DBNull.Value)
                region.NOMBRE = Convert.ToString(reader["NOMBRE"]);
            if (reader["DESCRIPCION"] != DBNull.Value)
                region.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            if (reader["ORDEN"] != DBNull.Value)
                region.ORDEN = Convert.ToInt32(reader["ORDEN"]);
            return region;
        }
        #endregion
        #region Remolcador
        internal DataTable Remolcador_ObtenerTodos(int site_id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_TODO_REMOLCADOR] @SITE_ID = {0}", site_id)).Tables[0];
        }
        internal DataTable Remolcador_ObtenerXParametro(int site_id, string codigo, string descripcion)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_REMOLCADOR]");
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
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
        internal RemolcadorBC Remolcador_ObtenerXId(int id)
        {
            RemolcadorBC remolcador = new RemolcadorBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_REMOLCADOR]");
                accesoDatos.AgregarSqlParametro("@REMO_ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    remolcador = this.cargarDatosRemolcador(accesoDatos.SqlLectorDatos);
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
            return remolcador;
        }
        internal bool Remolcador_Crear(RemolcadorBC remolcador)
        {
            bool exito = false;
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_REMOLCADOR]");
            accesoDatos.AgregarSqlParametro("@CODIGO", remolcador.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", remolcador.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@ID_USUARIO", remolcador.ID_USUARIO);
            //accesoDatos.AgregarSqlParametro("@PLAYAS", remolcador.PLAYAS);
            //if (remolcador.SITE_ID != 0 && remolcador.SITE_ID != null)
            accesoDatos.AgregarSqlParametro("@SITE_ID", remolcador.SITE_ID);

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
        internal bool Remolcador_Modificar(RemolcadorBC remolcador)
        {
            bool exito = false;
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_REMOLCADOR]");
            accesoDatos.AgregarSqlParametro("@ID", remolcador.ID);
            accesoDatos.AgregarSqlParametro("@CODIGO", remolcador.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", remolcador.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@ID_USUARIO", remolcador.ID_USUARIO);
            if (remolcador.SITE_ID != 0 && remolcador.SITE_ID != null)
            {
                accesoDatos.AgregarSqlParametro("@SITE_ID", remolcador.SITE_ID);
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
        internal bool Remolcador_Eliminar(int id)
        {
            bool esExitosa = false;
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[ELIMINA_REMOLCADOR]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlEscritura();
                esExitosa = true;
            }
            catch (Exception ex)
            {
                esExitosa = false;
                throw ex;
            }
            return esExitosa;
        }
        private RemolcadorBC cargarDatosRemolcador(SqlDataReader reader)
        {
            RemolcadorBC remolcador = new RemolcadorBC();
            if (reader["ID"] != DBNull.Value)
                remolcador.ID = Convert.ToInt32(reader["ID"]);
            if (reader["CODIGO"] != DBNull.Value)
                remolcador.CODIGO = Convert.ToString(reader["CODIGO"]);
            if (reader["DESCRIPCION"] != DBNull.Value)
                remolcador.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            if (reader["ID_USUARIO"] != DBNull.Value)
                remolcador.ID_USUARIO = Convert.ToInt32(reader["ID_USUARIO"]);
            if (reader["SITE_ID"] != DBNull.Value)
                remolcador.SITE_ID = Convert.ToInt32(reader["SITE_ID"]);
            return remolcador;
        }
        #endregion
        #region RemoProgDistribuicion
        internal DataTable RemoProgDistribuicion_CargaTodo(int site_id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_REMO_PROG_DISTRIBUCION] @SITE_ID = {0}", site_id)).Tables[0];
        }
        internal DataTable RemoProgDistribuicionPlayas_CargaTodo()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_REMO_PROG_DISTRIBUICION_PLAYAS]").Tables[0];
        }
        internal DataTable RemoProgDistribuicionPlayas_CargaTodo(int repd_id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_REMO_PROG_DISTRIBUICION_PLAYAS_V2] {0}", repd_id)).Tables[0];
        }
        internal RemoProgDistribuicionBC RemoProgDistribuicion_CargaXId(int id)
        {
            RemoProgDistribuicionBC rpd = new RemoProgDistribuicionBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_REMO_PROG_DISTRIBUCION]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    rpd = this.cargaDatosRemoProgDistribuicion(accesoDatos.SqlLectorDatos);
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
            return rpd;
        }
        internal bool RemoProgDistribuicion_Crear(RemoProgDistribuicionBC r)
        {
            bool exito;
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[AGREGA_REMO_PROG_DISTRIBUICION]");
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", r.DESCRIPCION);
                accesoDatos.AgregarSqlParametro("@SITE_ID", r.SITE_ID);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool RemoProgDistribuicion_Modificar(RemoProgDistribuicionBC r)
        {
            bool exito;
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[MODIFICA_REMO_PROG_DISTRIBUICION]");
                accesoDatos.AgregarSqlParametro("@REPD_ID", r.ID);
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", r.DESCRIPCION);
                accesoDatos.AgregarSqlParametro("@SITE_ID", r.SITE_ID);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool RemoProgDistribuicion_Asignar(DataTable dt, int id)
        {
            bool exito = false;
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[EDITA_REMO_PROG_DISTRIBUICION_PLAYAS]");
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.AgregarSqlParametro("@REPD_ID", id);
                accesoDatos.AgregarSqlParametro("@PLAYAS", dt);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception e)
            {
                exito = false;
                throw e;
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool RemoProgDistribuicion_ActivarDesactivar(int repd_id)
        {
            bool exito = false;
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ACTIVAR_DESACTIVAR_REMO_PROG_DISTRIBUICION]");
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.AgregarSqlParametro("@REPD_ID", repd_id);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        private RemoProgDistribuicionBC cargaDatosRemoProgDistribuicion(SqlDataReader reader)
        {
            RemoProgDistribuicionBC rpd = new RemoProgDistribuicionBC();
            if (reader["ID"] != DBNull.Value)
                rpd.ID = Convert.ToInt32(reader["ID"]);
            if (reader["DESCRIPCION"] != DBNull.Value)
                rpd.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            if (reader["SITE_ID"] != DBNull.Value)
                rpd.SITE_ID = Convert.ToInt32(reader["SITE_ID"]);
            if (reader["ACTIVA"] != DBNull.Value)
                rpd.ACTIVA = Convert.ToBoolean(reader["ACTIVA"]);
            return rpd;
        }
        #endregion
        #region ServiciosExternos
        internal DataTable servicios_Obtener_ReporteXParametro(string placa, string site_in, int tran_id, int site, DateTime desde, DateTime hasta)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[REPORTE_SERVICIOS_EXTERNOS]");

            if (tran_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_TRAN", tran_id);
            }

            if (!String.IsNullOrEmpty(placa))
            {
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
            }

            if (site_in != "-1")
            {
                accesoDatos.AgregarSqlParametro("@SITE_IN", site_in);
            }


            accesoDatos.AgregarSqlParametro("@SITE_ID", site);
            accesoDatos.AgregarSqlParametro("@DESDE", desde);
            accesoDatos.AgregarSqlParametro("@HASTA", hasta);

            try
            {
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        internal DataTable ServiciosExternos_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_SERVICIOS_EXTERNOS]").Tables[0];
        }
        internal ServiciosExternosBC ServiciosExternos_ObtenerXId(int id)
        {
            ServiciosExternosBC s = new ServiciosExternosBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SERVICIOS_EXTERNOS]");
                accesoDatos.AgregarSqlParametro("@SEEX_ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    s = this.cargaDatosServiciosExternos(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return s;
        }
        internal ServiciosExternosBC ServiciosExternos_ObtenerXCodigo(string codigo)
        {
            ServiciosExternosBC s = new ServiciosExternosBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SERVICIOS_EXTERNOS]");
                accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    s = this.cargaDatosServiciosExternos(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return s;
        }
        internal DataTable ServiciosExternos_ObtenerXParametros(string codigo)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SERVICIOS_EXTERNOS]");
                accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                accesoDatos.AgregarSqlParametro("@BUSCAR", true);
                dt = accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal bool ServiciosExternos_Crear(ServiciosExternosBC s)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[EDITA_SERVICIOS_EXTERNOS]");
                accesoDatos.AgregarSqlParametro("@CODIGO", s.CODIGO);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool ServiciosExternos_Modificar(ServiciosExternosBC s)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[EDITA_SERVICIOS_EXTERNOS]");
                accesoDatos.AgregarSqlParametro("@SEEX_ID", s.SEEX_ID);
                accesoDatos.AgregarSqlParametro("@CODIGO", s.CODIGO);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool ServiciosExternos_Eliminar(int id)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[ELIMINA_SERVICIOS_EXTERNOS]");
                accesoDatos.AgregarSqlParametro("@SEEX_ID", id);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        private ServiciosExternosBC cargaDatosServiciosExternos(SqlDataReader reader)
        {
            ServiciosExternosBC s = new ServiciosExternosBC();
            s.SEEX_ID = Convert.ToInt32(reader["SEEX_ID"]);
            s.CODIGO = Convert.ToString(reader["CODIGO"]);
            return s;
        }
        #endregion
        #region ServiciosExternosVehiculos
        internal DataTable ServiciosExternosVehiculos_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_SERVICIOS_EXTERNOS_VEHICULOS]").Tables[0];
        }
        internal DataTable ServiciosExternosVehiculos_ObtenerXParametro(string codigo, string placa)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SERVICIOS_EXTERNOS_VEHICULOS]");
                accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
                accesoDatos.AgregarSqlParametro("@BUSCAR", true);
                dt = accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal ServiciosExternosVehiculosBC ServiciosExternosVehiculos_ObtenerXId(int id)
        {
            ServiciosExternosVehiculosBC s = new ServiciosExternosVehiculosBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SERVICIOS_EXTERNOS_VEHICULOS]");
                accesoDatos.AgregarSqlParametro("@SEVE_ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    s = this.cargaDatosServiciosExternosVehiculos(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return s;
        }
        internal ServiciosExternosVehiculosBC ServiciosExternosVehiculos_ObtenerXCodigo(string codigo)
        {
            ServiciosExternosVehiculosBC s = new ServiciosExternosVehiculosBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SERVICIOS_EXTERNOS_VEHICULOS]");
                accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    s = this.cargaDatosServiciosExternosVehiculos(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return s;
        }
        internal ServiciosExternosVehiculosBC ServiciosExternosVehiculos_ObtenerXPlaca(string placa)
        {
            ServiciosExternosVehiculosBC s = new ServiciosExternosVehiculosBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SERVICIOS_EXTERNOS_VEHICULOS]");
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    s = this.cargaDatosServiciosExternosVehiculos(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return s;
        }
        internal ServiciosExternosVehiculosBC ServiciosExternosVehiculos_ObtenerXPlaca(string placa, out bool existe)
        {
            ServiciosExternosVehiculosBC s = new ServiciosExternosVehiculosBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SERVICIOS_EXTERNOS_VEHICULOS]");
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    s = this.cargaDatosServiciosExternosVehiculos(accesoDatos.SqlLectorDatos);
                }
                existe = accesoDatos.SqlLectorDatos.HasRows;
            }
            catch (Exception)
            {
                existe = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return s;
        }
        internal bool ServiciosExternosVehiculos_Crear(ServiciosExternosVehiculosBC s)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[EDITA_SERVICIOS_EXTERNOS_VEHICULOS]");
                accesoDatos.AgregarSqlParametro("@CODIGO", s.CODIGO);
                accesoDatos.AgregarSqlParametro("@PLACA", s.PLACA);
                accesoDatos.AgregarSqlParametro("@PROV_ID", s.PROV_ID);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool ServiciosExternosVehiculos_Modificar(ServiciosExternosVehiculosBC s)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[EDITA_SERVICIOS_EXTERNOS_VEHICULOS]");
                accesoDatos.AgregarSqlParametro("@SEVE_ID", s.SEVE_ID);
                accesoDatos.AgregarSqlParametro("@CODIGO", s.CODIGO);
                accesoDatos.AgregarSqlParametro("@PLACA", s.PLACA);
                accesoDatos.AgregarSqlParametro("@PROV_ID", s.PROV_ID);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool ServiciosExternosVehiculos_Eliminar(int id)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[ELIMINA_SERVICIOS_EXTERNOS_VEHICULOS]");
                accesoDatos.AgregarSqlParametro("@SEVE_ID", id);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool ServiciosExternosVehiculos_Entrada(ServiciosExternosVehiculosBC se)
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[PROCESO_ENTRADA_SERVICIO_EXTERNO]");
                accesoDatos.AgregarSqlParametro("@PLACA", se.PLACA);
                accesoDatos.AgregarSqlParametro("@PROV_ID", se.PROV_ID);
                accesoDatos.AgregarSqlParametro("@SITE_ID", se.SITE_ID);
                accesoDatos.AgregarSqlParametro("@FECHA", se.FH_INGRESO);
                accesoDatos.AgregarSqlParametro("@COSE_RUT", se.CONDUCTOR.RUT);
                accesoDatos.AgregarSqlParametro("@COSE_NOMBRE", se.CONDUCTOR.NOMBRE);
                accesoDatos.AgregarSqlParametro("@COSE_EXTRANJERO", se.CONDUCTOR.COSE_EXTRANJERO);
                accesoDatos.AgregarSqlParametro("@OBS", se.OBSERVACION);
                accesoDatos.AgregarSqlParametro("@USUA_ID", se.USUA_ID);
                accesoDatos.AgregarSqlParametro("@SEEX_ID", se.SEEX_ID);
                accesoDatos.EjecutarSqlEscritura();
                return true;
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
        internal bool ServiciosExternosVehiculos_Salida(ServiciosExternosVehiculosBC se)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[PROCESO_SALIDA_SERVICIO_EXTERNO]");
                accesoDatos.AgregarSqlParametro("@ID", se.SEVE_ID);
                accesoDatos.AgregarSqlParametro("@FECHA", se.FH_SALIDA);
                accesoDatos.AgregarSqlParametro("@COSE_RUT", se.COSE_RUT);
                accesoDatos.AgregarSqlParametro("@COSE_NOMBRE", se.COSE_NOMBRE);
                accesoDatos.AgregarSqlParametro("@OBS", se.OBSERVACION);
                accesoDatos.AgregarSqlParametro("@USUA_ID", se.USUA_ID);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        private ServiciosExternosVehiculosBC cargaDatosServiciosExternosVehiculos(SqlDataReader reader)
        {
            ServiciosExternosVehiculosBC s = new ServiciosExternosVehiculosBC();
            if (reader["SEVE_ID"] != DBNull.Value)
                s.SEVE_ID = Convert.ToInt32(reader["SEVE_ID"]);
            if (reader["CODIGO"] != DBNull.Value)
                s.CODIGO = Convert.ToString(reader["CODIGO"]);
            if (reader["PLACA"] != DBNull.Value)
                s.PLACA = Convert.ToString(reader["PLACA"]);
            if (reader["PROV_ID"] != DBNull.Value)
                s.PROV_ID = Convert.ToInt32(reader["PROV_ID"]);
            if (reader["SITE_ID"] != DBNull.Value)
                s.SITE_ID = Convert.ToInt32(reader["SITE_ID"]);
            if (reader["SITE_IN"] != DBNull.Value)
                s.SITE_IN = Convert.ToBoolean(reader["SITE_IN"]);
            if (reader["COD_INTERNO"] != DBNull.Value)
                s.COD_INTERNO_IN = Convert.ToInt32(reader["COD_INTERNO"]);
            if (reader["FH_INGRESO"] != DBNull.Value)
                s.FH_INGRESO = Convert.ToDateTime(reader["FH_INGRESO"]);
            if (reader["FH_SALIDA"] != DBNull.Value)
                s.FH_SALIDA = Convert.ToDateTime(reader["FH_SALIDA"]);
            if (reader["ID_INGRESO"] != DBNull.Value)
                s.ID_INGRESO = Convert.ToInt32(reader["ID_INGRESO"]);
            if (reader["COND_RUT"] != DBNull.Value)
                s.COSE_RUT = Convert.ToString(reader["COND_RUT"]);
            if (reader["COND_NOMBRE"] != DBNull.Value)
                s.COSE_NOMBRE = Convert.ToString(reader["COND_NOMBRE"]);
            if (reader["OBSERVACION"] != DBNull.Value)
                s.OBSERVACION = Convert.ToString(reader["OBSERVACION"]);
            if (reader["USUA_ID"] != DBNull.Value)
                s.USUA_ID = Convert.ToInt32(reader["USUA_ID"]);
            if (reader["SEEX_ID"] != DBNull.Value)
                s.SEEX_ID = Convert.ToInt32(reader["SEEX_ID"]);
            s.CONDUCTOR = new ServiciosExternosConductorBC();
            return s;
        }
        #endregion
        #region ServiciosExternosConductor
        internal DataTable ServiciosExternosConductor_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_CONDUCTOR_SERVICIO_EXTERNO]").Tables[0];
        }
        internal DataTable ServiciosExternosConductor_ObtenerXParametros(string rut, string nombre)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_CONDUCTOR_SERVICIO_EXTERNO]");
                accesoDatos.AgregarSqlParametro("@RUT", rut);
                accesoDatos.AgregarSqlParametro("@NOMBRE", nombre);
                dt = accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal ServiciosExternosConductorBC ServiciosExternosConductor_ObtenerXId(int id)
        {
            ServiciosExternosConductorBC c = new ServiciosExternosConductorBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_CONDUCTOR_SERVICIO_EXTERNO]");
                accesoDatos.AgregarSqlParametro("@COSE_ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    c = this.cargaServiciosExternosConductor(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return c;
        }
        internal ServiciosExternosConductorBC ServiciosExternosConductor_ObtenerXRut(string rut)
        {
            ServiciosExternosConductorBC c = new ServiciosExternosConductorBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_CONDUCTOR_SERV_EXT_X_RUT]");
                accesoDatos.AgregarSqlParametro("@RUT", rut);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    c = this.cargaServiciosExternosConductor(accesoDatos.SqlLectorDatos);
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
            return c;
        }
        private ServiciosExternosConductorBC cargaServiciosExternosConductor(SqlDataReader reader)
        {
            ServiciosExternosConductorBC c = new ServiciosExternosConductorBC();
            if (reader["COSE_ID"] != DBNull.Value)
                c.ID = Convert.ToInt32(reader["COSE_ID"]);
            if (reader["IMAGEN"] != DBNull.Value)
                c.IMAGEN = Convert.ToString(reader["IMAGEN"]);
            if (reader["RUT"] != DBNull.Value)
                c.RUT = Convert.ToString(reader["RUT"]);
            if (reader["NOMBRE"] != DBNull.Value)
                c.NOMBRE = Convert.ToString(reader["NOMBRE"]);
            if (reader["ACTIVO"] != DBNull.Value)
                c.ACTIVO = Convert.ToBoolean(reader["ACTIVO"]);
            if (reader["BLOQUEADO"] != DBNull.Value)
                c.BLOQUEADO = Convert.ToBoolean(reader["BLOQUEADO"]);
            return c;
        }
        internal bool ServiciosExternosConductor_Agregar(ServiciosExternosConductorBC c)
        {
            bool exito;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[EDITA_CONDUCTOR_SERVICIO_EXTERNO]");
                accesoDatos.AgregarSqlParametro("@RUT", c.RUT);
                accesoDatos.AgregarSqlParametro("@NOMBRE", c.NOMBRE);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal int ServiciosExternosConductor_AgregarIdentity(ServiciosExternosConductorBC c)
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[EDITA_CONDUCTOR_SERVICIO_EXTERNO]");
                accesoDatos.AgregarSqlParametro("@RUT", c.RUT);
                accesoDatos.AgregarSqlParametro("@NOMBRE", c.NOMBRE);
                accesoDatos.EjecutaSqlInsertIdentity();
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return accesoDatos.ID;
        }
        internal bool ServiciosExternosConductor_Modificar(ServiciosExternosConductorBC c)
        {
            bool exito;
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[EDITA_CONDUCTOR_SERVICIO_EXTERNO]");
                accesoDatos.AgregarSqlParametro("@COSE_ID", c.ID);
                accesoDatos.AgregarSqlParametro("@NOMBRE", c.NOMBRE);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool ServiciosExternosConductor_Eliminar(int id)
        {
            bool exito;
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[ELIMINA_CONDUCTOR_SERVICIO_EXTERNO]");
                accesoDatos.AgregarSqlParametro("@COSE_ID", id);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        #endregion
        #region Shorteck
        internal DataTable Shorteck_ObtenerTodos()
        {
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SHORTECK]");
            return accesoDatos.EjecutarSqlquery2();
        }
        internal ShorteckBC Shorteck_ObtenerXId(string id)
        {
            ShorteckBC s = new ShorteckBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SHORTECK]");
                accesoDatos.AgregarSqlParametro("@SHOR_ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    s = this.cargaDatosShorteck(accesoDatos.SqlLectorDatos);
                }
            }
            catch (Exception)
            {
                s.ID = "";
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return s;
        }
        internal bool Shorteck_AgregarModificar(ShorteckBC s)
        {
            bool exito;
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[EDITA_SHORTECK]");
                accesoDatos.AgregarSqlParametro("@SHOR_ID", s.ID);
                accesoDatos.AgregarSqlParametro("@SHOR_DESC", s.DESCRIPCION);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool Shorteck_Eliminar(string id)
        {
            bool exito;
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[ELIMINA_SHORTECK]");
                accesoDatos.AgregarSqlParametro("@SHOR_ID", id);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception)
            {
                exito = false;
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        private ShorteckBC cargaDatosShorteck(SqlDataReader reader)
        {
            ShorteckBC s = new ShorteckBC();
            s.ID = reader["SHOR_ID"].ToString();
            s.DESCRIPCION = reader["SHOR_DESC"].ToString();
            return s;
        }
        #endregion
        #region Site
        internal DataTable Site_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_SITE]").Tables[0];
        }
        internal DataTable Site_ObtenerXParametro(string nombre, int empr_id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SITE]");
            if (!string.IsNullOrEmpty(nombre))
            {
                accesoDatos.AgregarSqlParametro("@NOMBRE", nombre);
            }
            if (empr_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_EMPRESA", empr_id);
            }
            return accesoDatos.EjecutarSqlquery2();
        }
        internal SiteBC Site_ObtenerXId(int id)
        {
            SiteBC site = new SiteBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_SITE]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    site = this.cargarDatosSite(accesoDatos.SqlLectorDatos);
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
            return site;
        }
        internal bool Site_Crear(SiteBC site)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[EDITA_SITE]");
            accesoDatos.AgregarSqlParametro("@NOMBRE", site.NOMBRE);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", site.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@EMPRESA_ID", site.EMPRESA_ID);
            accesoDatos.AgregarSqlParametro("@COD_SAP", site.COD_SAP);
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
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal bool Site_Modificar(SiteBC site)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[EDITA_SITE]");
            accesoDatos.AgregarSqlParametro("@ID", site.ID);
            accesoDatos.AgregarSqlParametro("@NOMBRE", site.NOMBRE);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", site.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@EMPRESA_ID", site.EMPRESA_ID);
            accesoDatos.AgregarSqlParametro("@COD_SAP", site.COD_SAP);
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
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal bool Site_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_SITE]");
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
        internal bool Site_TrailerAuto(int usua_id, int site_id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[SITE_TRAILER_AUTO]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
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
        internal bool Site_Virtual(int site_id, int play_id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ASIGNA_PLAYA_VIRTUAL]");
            accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            accesoDatos.AgregarSqlParametro("@PLAY_ID", play_id);
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
        private SiteBC cargarDatosSite(SqlDataReader reader)
        {
            SiteBC site = new SiteBC();
            if (reader["ID"] != DBNull.Value)
                site.ID = Convert.ToInt32(reader["ID"]);
            if (reader["DESCRIPCION"] != DBNull.Value)
                site.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            if (reader["NOMBRE"] != DBNull.Value)
                site.NOMBRE = Convert.ToString(reader["NOMBRE"]);
            if (reader["EMPR_ID"] != DBNull.Value)
                site.EMPRESA_ID = Convert.ToInt32(reader["EMPR_ID"]);
            if (reader["COD_SAP"] != DBNull.Value)
                site.COD_SAP = Convert.ToInt32(reader["COD_SAP"]);
            return site;
        }
        #endregion
        #region TimerProcesos
        internal DataTable TimerProcesos_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TIMER_PROCESOS_V2]").Tables[0];
        }
        internal TimerProcesosBC TimerProcesos_ObtenerXId(int id)
        {
            TimerProcesosBC tp = new TimerProcesosBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_TIMER_PROCESOS_V2]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    tp = this.cargarDatosTimerProcesos(accesoDatos.SqlLectorDatos);
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
            return tp;
        }
        internal bool TimerProcesos_Crear(TimerProcesosBC tp)
        {
            bool exito = false;
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_TIMER_PROCESOS]");
            accesoDatos.AgregarSqlParametro("@CODIGO", tp.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", tp.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@MINS", tp.TIEMPO_MAX);
            accesoDatos.AgregarSqlParametro("@COLOR", tp.COLOR);
            accesoDatos.AgregarSqlParametro("@SITE_ID", tp.SITE_ID);
            accesoDatos.AgregarSqlParametro("@SOES_ID", tp.SOES_ID);
            accesoDatos.AgregarSqlParametro("@ZOTI_ID", tp.ZOTI_ID);
            accesoDatos.AgregarSqlParametro("@PYTI_ID", tp.PYTI_ID);
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
        internal bool TimerProcesos_Modificar(TimerProcesosBC tp)
        {
            bool exito = false;
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_TIMER_PROCESOS]");
            accesoDatos.AgregarSqlParametro("@ID", tp.ID);
            accesoDatos.AgregarSqlParametro("@CODIGO", tp.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", tp.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@MINS", tp.TIEMPO_MAX);
            accesoDatos.AgregarSqlParametro("@COLOR", tp.COLOR);
            //accesoDatos.AgregarSqlParametro("@SITE_ID", tp.SITE_ID);
            //accesoDatos.AgregarSqlParametro("@TRES_ID", tp.TRES_ID);
            //accesoDatos.AgregarSqlParametro("@ZOTI_ID", tp.ZOTI_ID);
            //accesoDatos.AgregarSqlParametro("@PYTI_ID", tp.PYTI_ID);
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
        internal bool TimerProcesos_Eliminar(int id)
        {
            bool esExitosa = false;
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[ELIMINA_TIMER_PROCESOS]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlEscritura();
                esExitosa = true;
            }
            catch (Exception ex)
            {
                esExitosa = false;
                throw ex;
            }
            return esExitosa;
        }
        private TimerProcesosBC cargarDatosTimerProcesos(SqlDataReader reader)
        {
            TimerProcesosBC tp = new TimerProcesosBC();
            if (reader["ID"] != DBNull.Value)
                tp.ID = Convert.ToInt32(reader["ID"]);
            if (reader["CODIGO"] != DBNull.Value)
                tp.CODIGO = Convert.ToString(reader["CODIGO"]);
            if (reader["DESCRIPCION"] != DBNull.Value)
                tp.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            if (reader["SITE_ID"] != DBNull.Value)
                tp.SITE_ID = Convert.ToInt32(reader["SITE_ID"]);
            if (reader["ZOTI_ID"] != DBNull.Value)
                tp.ZOTI_ID = Convert.ToInt32(reader["ZOTI_ID"]);
            if (reader["PYTI_ID"] != DBNull.Value)
                tp.PYTI_ID = Convert.ToInt32(reader["PYTI_ID"]);
            if (reader["SOES_ID"] != DBNull.Value)
                tp.SOES_ID = Convert.ToInt32(reader["SOES_ID"]);
            if (reader["TIEMPO_MAX"] != DBNull.Value)
                tp.TIEMPO_MAX = Convert.ToInt32(reader["TIEMPO_MAX"]);
            if (reader["COLOR"] != DBNull.Value)
                tp.COLOR = Convert.ToString(reader["COLOR"]);
            return tp;
        }
        #endregion
        #region TipoDestino
        internal DataTable TipoDestino_ObtenerTodo()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TIPO_DESTINO]").Tables[0];
        }
        internal DestinoTipoBC TipoDestino_ObtenerSeleccionado(int id, string codigo)
        {
            DestinoTipoBC d = new DestinoTipoBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_TIPO_DESTINO]");
                if (id != 0 || id != null)
                {
                    accesoDatos.AgregarSqlParametro("@ID", id);
                }
                if (!string.IsNullOrEmpty(codigo))
                {
                    accesoDatos.AgregarSqlParametro("@CODIGO", codigo);
                }
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    d = this.cargaTipoDestino(accesoDatos.SqlLectorDatos);
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
            return d;
        }
        private DestinoTipoBC cargaTipoDestino(SqlDataReader s)
        {
            DestinoTipoBC d = new DestinoTipoBC();
            d.ID = Convert.ToInt32(s["ID"]);
            d.CODIGO = Convert.ToString(s["CODIGO"]);
            d.NOMBRE = Convert.ToString(s["NOMBRE"]);
            return d;
        }
        #endregion
        #region TipoEstadoMovimiento
        internal DataTable TipoEstadoMov_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TIPO_ESTADO_MOVIMIENTO]").Tables[0];
        }
        internal TipoEstadoMovBC TipoEstadoMov_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            TipoEstadoMovBC tem = new TipoEstadoMovBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TIPO_ESTADO_MOVIMIENTO_X_ID]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    tem = this.cargarDatosTipoEstadoMov(accesoDatos.SqlLectorDatos);
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
            return tem;
        }
        internal DataTable TipoEstadoMov_ObtenerXParametro(string descripcion)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

            String query = "[dbo].[CARGA_TIPOS_ESTADO_MOVIMIENTO_X_CRITERIO] ";

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
        internal bool TipoEstadoMov_Crear(TipoEstadoMovBC tem)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_TIPO_ESTADO_MOVIMIENTO]");
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", tem.DESCRIPCION);
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
        internal bool TipoEstadoMov_Modificar(TipoEstadoMovBC tem)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_TIPO_ESTADO_MOVIMIENTO]");
            accesoDatos.AgregarSqlParametro("@ID", tem.ID);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", tem.DESCRIPCION);
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
        internal bool TipoEstadoMov_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_TIPO_ESTADO_MOVIMIENTO]");
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
        private TipoEstadoMovBC cargarDatosTipoEstadoMov(SqlDataReader reader)
        {
            TipoEstadoMovBC tem = new TipoEstadoMovBC();
            tem.ID = Convert.ToInt32(reader["ID"]);
            tem.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            return tem;
        }
        #endregion
        #region Tipo Zona
        internal DataTable TipoZona_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TIPO_ZONA]").Tables[0];
        }
        internal Tipo_ZonaBC TipoZona_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            Tipo_ZonaBC zoti = new Tipo_ZonaBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_TIPO_ZONA]");
                accesoDatos.AgregarSqlParametro("@ZOTI_ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    zoti = this.cargarDatosTipoZona(accesoDatos.SqlLectorDatos);
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
            return zoti;
        }
        internal DataTable TipoZona_ObtenerXParametro(string descripcion)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

            String query = "[dbo].[CARGA_TIPOS_ZONA_X_CRITERIO] ";

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
        internal bool TipoZona_Crear(Tipo_ZonaBC tipoZona)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_TIPO_ZONA]");
            accesoDatos.AgregarSqlParametro("@CODIGO", tipoZona.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", tipoZona.DESCRIPCION);
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
        internal bool TipoZona_Modificar(Tipo_ZonaBC tipoZona)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_TIPO_ZONA]");
            accesoDatos.AgregarSqlParametro("@ID", tipoZona.ID);
            accesoDatos.AgregarSqlParametro("@CODIGO", tipoZona.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", tipoZona.DESCRIPCION);
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
        internal bool TipoZona_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_TIPO_ZONA]");
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
        private Tipo_ZonaBC cargarDatosTipoZona(SqlDataReader reader)
        {
            Tipo_ZonaBC zoti = new Tipo_ZonaBC();
            zoti.ID = Convert.ToInt32(reader["ID"]);
            zoti.CODIGO = reader["CODIGO"].ToString();
            zoti.DESCRIPCION = reader["DESCRIPCION"].ToString();
            return zoti;
        }
        #endregion
        #region Tracto
        internal DataTable Tracto_Obtener_ReporteXParametro(string placa, bool site_in, int tran_id = 0)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_REPORTE_TRACTO]");

            if (tran_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_TRAN", tran_id);
            }

            if (!String.IsNullOrEmpty(placa))
            {
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
            }

            if (site_in)
            {
                accesoDatos.AgregarSqlParametro("@SITE_IN", site_in);
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
        internal bool Tracto_Crear(TractoBC t)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[EDITA_TRACTO]");
            accesoDatos.AgregarSqlParametro("@PATENTE", t.PATENTE);
            accesoDatos.AgregarSqlParametro("@USUA_ID", t.USUA_ID_CREACION);
            accesoDatos.AgregarSqlParametro("@TRAN_ID", t.TRAN_ID);
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
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal bool Tracto_Modificar(TractoBC t)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_TRACTO]");
            accesoDatos.AgregarSqlParametro("@ID", t.ID);
            accesoDatos.AgregarSqlParametro("@PATENTE", t.PATENTE);
            accesoDatos.AgregarSqlParametro("@TRAN_ID", t.TRAN_ID);
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
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal bool Tracto_Eliminar(int id)
        {
            bool esExitosa = false;
            SqlAccesoDatos accesoDatos;
            accesoDatos = null;
            accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[prcELIMINAR_TRACTO]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlEscritura();
                esExitosa = true;
            }
            catch (Exception ex)
            {
                esExitosa = false;
                throw ex;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return esExitosa;
        }
        internal bool Tracto_Bloquear(TractoBC t)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[BLOQUEO_TRACTO]");
            accesoDatos.AgregarSqlParametro("@ID", t.ID);
            accesoDatos.AgregarSqlParametro("@TRAB_ID", t.TRAB_ID);
            accesoDatos.AgregarSqlParametro("@USUA_ID", t.USUA_ID_BLOQUEO);
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
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal bool Tracto_Salida(TractoBC t, out string error)
        {
            bool exito = false;
            error = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                int errorint = 0;
                error = "";
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[PROCESO_SALIDA_TRACTO]");
                accesoDatos.AgregarSqlParametro("@trac_id", t.ID);
                accesoDatos.AgregarSqlParametro("@COND_ID", t.COND_ID_SALIDA);
                accesoDatos.AgregarSqlParametro("@obs", t.OBSERVACION);
                accesoDatos.AgregarSqlParametro("@USUA_ID", t.USUA_ID);
                accesoDatos.AgregarSqlParametro("@SITE_ID", t.SITE_ID);

                SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", error);
                param.Direction = ParameterDirection.Output;
                param.Size = 1000;

                accesoDatos.AgregarSqlParametro("@error", errorint).Direction = ParameterDirection.Output;

                accesoDatos.EjecutarSqlEscritura();
                error = param.Value.ToString();
                exito = true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal DataTable Tracto_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TRACTO]").Tables[0];
        }
        internal TractoBC Tracto_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            TractoBC t = new TractoBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_TRACTO]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    t = this.cargaDatosTracto(accesoDatos.SqlLectorDatos);
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
            return t;
        }
        internal TractoBC Tracto_ObtenerXPlaca(string placa)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            TractoBC t = new TractoBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_TRACTO]");
                accesoDatos.AgregarSqlParametro("@PATENTE", placa);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    t = this.cargaDatosTracto(accesoDatos.SqlLectorDatos);
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
            return t;
        }
        internal TractoBC Tracto_ObtenerTractoXLugar(int luga_id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            TractoBC t = new TractoBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_TRACTO]");
                accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    t = this.cargaDatosTracto(accesoDatos.SqlLectorDatos);
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
            return t;
        }
        internal DataTable Tracto_ObtenerBloqueo()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_BLOQUEO_TRACTO]").Tables[0];
        }
        private TractoBC cargaDatosTracto(SqlDataReader reader)
        {
            TractoBC t = new TractoBC();
            if (reader["ID"] != DBNull.Value)
                t.ID = Convert.ToInt32(reader["ID"]);
            if (reader["PATENTE"] != DBNull.Value)
                t.PATENTE = Convert.ToString(reader["PATENTE"]);
            if (reader["TRAE_ID"] != DBNull.Value)
                t.TRAE_ID = Convert.ToInt32(reader["TRAE_ID"]);
            if (reader["TRAN_ID"] != DBNull.Value)
                t.TRAN_ID = Convert.ToInt32(reader["TRAN_ID"]);
            if (reader["FH_CREACION"] != DBNull.Value)
                t.FECHA_CREACION = Convert.ToDateTime(reader["FH_CREACION"]);
            if (reader["USUA_ID_CREACION"] != DBNull.Value)
                t.USUA_ID_CREACION = Convert.ToInt32(reader["USUA_ID_CREACION"]);
            if (reader["TRAB_ID"] != DBNull.Value)
            {
                t.TRAB_ID = Convert.ToInt32(reader["TRAB_ID"]);
                if (reader["USUA_ID_BLOQUEO"] != DBNull.Value)
                    t.USUA_ID_BLOQUEO = Convert.ToInt32(reader["USUA_ID_BLOQUEO"]);
            }
            if (reader["SITE_ID"] != DBNull.Value)
                t.SITE_ID = Convert.ToInt32(reader["SITE_ID"]);
            if (reader["SITE_IN"] != DBNull.Value)
                t.SITE_IN = Convert.ToBoolean(reader["SITE_IN"]);
            if (reader["COND_ID_INGRESO"] != DBNull.Value)
                t.COND_ID_INGRESO = Convert.ToInt32(reader["COND_ID_INGRESO"]);
            if (reader["COND_ID_SALIDA"] != DBNull.Value)
                t.COND_ID_SALIDA = Convert.ToInt32(reader["COND_ID_SALIDA"]);
            return t;
        }
        #endregion
        #region Trailer
        internal DataTable Trailer_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TRAILER_V2]").Tables[0];
        }
        internal DataTable Trailer_ObtenerXSite(int site_id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_TODO_TRAILER_V2] @SITE_ID = {0}", site_id)).Tables[0];
        }
        internal DataTable Trailer_ObtenerDisponiblesDrop(int site_id, bool externo, bool cargado)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILER_DISPONIBLE_MIN]");
            if (site_id != 0)
            {
                accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
            }
            accesoDatos.AgregarSqlParametro("@EXTERNO", externo);
            accesoDatos.AgregarSqlParametro("@CARGADO", cargado);
            try
            {
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        internal bool temporal_estado_cargado(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[prcTEMPORAL_MODIFICAR_TRAILER_ESTADO_CARGADO]");
                accesoDatos.AgregarSqlParametro("@TRAI_ID", id);
                accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
            return true;
        }
        internal DataSet Trailer_CargaGrafico()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_GRAFICO_TRAILER]");
        }
        internal bool Trailer_continuar(int id, int usua_id, out string resultado)
        {
            bool exito = false;
            bool errorint = false;
            resultado = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[DESBLOQUEA_TRAILER]");
            accesoDatos.AgregarSqlParametro("@TRAI_ID", id);
            accesoDatos.AgregarSqlParametro("@usua_id", usua_id);
            //accesoDatos.AgregarSqlParametro("@TRBT_ID", null);
            accesoDatos.AgregarSqlParametro("@LUGA_ID_dest", null);
            accesoDatos.AgregarSqlParametro("@strOBSERVAC", null);
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
        internal bool Trailer_Bloquear(int id, int TIPO_BLOQUEO, int usua_id, out string resultado)
        {
            bool exito = false;
            bool errorint = false;
            resultado = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[BLOQUEA_TRAILER]");
            accesoDatos.AgregarSqlParametro("@TRAI_ID", id);
            accesoDatos.AgregarSqlParametro("@TIPO_BLOQUEO", TIPO_BLOQUEO);
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
        internal bool Trailer_Desbloquear(int id, int LUGA_ID, int usua_id, out string resultado)
        {
            bool exito = false;
            int errorint = 0;
            resultado = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[DESBLOQUEA_TRAILER]");
            accesoDatos.AgregarSqlParametro("@TRAI_ID", id);
            accesoDatos.AgregarSqlParametro("@usua_id", usua_id);
            // accesoDatos.AgregarSqlParametro("@LUGA_ID", LUGA_ID);
            accesoDatos.AgregarSqlParametro("@LUGA_ID_dest", LUGA_ID);
            // accesoDatos.AgregarSqlParametro("@strOBSERVAC", null);
            SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            SqlParameter paramerror = accesoDatos.AgregarSqlParametro("@error", errorint);
            paramerror.Direction = ParameterDirection.Output;
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();

                if (resultado == "")
                {
                    exito = true;
                }
                else
                {
                    exito = false;
                }
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool Trailer_ATracto(string placa, out string resultado)
        {
            bool exito = false;
            int errorint = 0;
            resultado = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[Trailer_a_tracto]");
            accesoDatos.AgregarSqlParametro("@TRAC_PLACA", placa);
            SqlParameter param = accesoDatos.AgregarSqlParametro("@ERROR_MSG", resultado);
            param.Direction = ParameterDirection.Output;
            param.Size = 1000;

            SqlParameter paramerror = accesoDatos.AgregarSqlParametro("@error", errorint);
            paramerror.Direction = ParameterDirection.Output;
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                resultado = param.Value.ToString();

                if (resultado == "")
                {
                    exito = true;
                }
                else
                {
                    exito = false;
                }
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal DataTable Trailer_ObtenerLavado(int site_id, DateTime desde, DateTime hasta)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_TRAILER_ULT_LAVADO] @SITE_ID = {0}", site_id)).Tables[0];
        }
        internal bool Trailer_CrearGenerico(TrailerBC trailer, bool importado)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_TRAILER_GENERICO]");
            accesoDatos.AgregarSqlParametro("@PLACA", trailer.PLACA);
            accesoDatos.AgregarSqlParametro("@ID_TRANSPORTISTA", trailer.TRAN_ID);
            accesoDatos.AgregarSqlParametro("@IMPORTADO", importado);
            try
            {
                accesoDatos.EjecutaSqlInsertIdentity();
                trailer.ID = accesoDatos.ID;
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal TrailerBC Trailer_ObtenerXDoc(string doc, string id_site)
        {
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
                    //    trailer = cargarDatosTrailer(accesoDatos.SqlLectorDatos);
                    if (!string.IsNullOrEmpty(accesoDatos.SqlLectorDatos["ID"].ToString()))
                    {
                        trailer.ID = Convert.ToInt32(accesoDatos.SqlLectorDatos["ID"].ToString());
                    }
                    trailer.PLACA = accesoDatos.SqlLectorDatos["PLACA"].ToString();
                    trailer.NUMERO = accesoDatos.SqlLectorDatos["NUMERO"].ToString();
                    trailer.CODIGO = accesoDatos.SqlLectorDatos["CODIGO"].ToString();
                    if (accesoDatos.SqlLectorDatos["EXTERNO"].ToString() == "True")
                    {
                        trailer.EXTERNO = true;
                    }
                    else
                    {
                        trailer.EXTERNO = false;
                    }
                    if (!string.IsNullOrEmpty(accesoDatos.SqlLectorDatos["TRAN_ID"].ToString()))
                    {
                        trailer.TRAN_ID = Convert.ToInt32(accesoDatos.SqlLectorDatos["TRAN_ID"].ToString());
                    }
                    if (!string.IsNullOrEmpty(accesoDatos.SqlLectorDatos["PROV_ID"].ToString()))
                    {
                        trailer.PROV_ID = Convert.ToInt32(accesoDatos.SqlLectorDatos["PROV_ID"].ToString());
                    }
                    if (accesoDatos.SqlLectorDatos["CARGADO"].ToString() == "True")
                    {
                        trailer.CARGADO = true;
                    }
                    else
                    {
                        trailer.CARGADO = false;
                    }
                    if (accesoDatos.SqlLectorDatos["TRUE_SITE_IN"].ToString() == "True")
                    {
                        trailer.SITE_IN = true;
                    }
                    else
                    {
                        trailer.SITE_IN = false;
                    }
                    trailer.PATENTE_TRACTO = accesoDatos.SqlLectorDatos["PATENTE_TRACTO"].ToString();
                    trailer.CHOFER_RUT = accesoDatos.SqlLectorDatos["RUT_CHOFER"].ToString();
                    trailer.CHOFER_NOMBRE = accesoDatos.SqlLectorDatos["NOMBRE_CHOFER"].ToString();
                    trailer.ACOMP_RUT = accesoDatos.SqlLectorDatos["RUT_ACOMP"].ToString();
                    if (!string.IsNullOrEmpty(accesoDatos.SqlLectorDatos["TCAR_ID"].ToString()))
                    {
                        trailer.TCAR_ID = Convert.ToInt32(accesoDatos.SqlLectorDatos["TCAR_ID"].ToString());
                    }
                    if (!string.IsNullOrEmpty(accesoDatos.SqlLectorDatos["MOIC_ID"].ToString()))
                    {
                        trailer.MOIC_ID = Convert.ToInt32(accesoDatos.SqlLectorDatos["MOIC_ID"].ToString());
                    }
                    if (!string.IsNullOrEmpty(accesoDatos.SqlLectorDatos["SITE_ID"].ToString()))
                    {
                        trailer.SITE_ID = Convert.ToInt32(accesoDatos.SqlLectorDatos["SITE_ID"].ToString());
                    }
                    trailer.OBSERVACION = accesoDatos.SqlLectorDatos["OBSERVACION"].ToString();

                    if (!string.IsNullOrEmpty(accesoDatos.SqlLectorDatos["PRING_ID"].ToString()))
                    {
                        trailer.PRING_ID = Convert.ToInt32(accesoDatos.SqlLectorDatos["SITE_ID"].ToString());
                    }
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
            return trailer;
        }
        internal bool Trailer_ComprobarPlacaNro(int id, string placa, string numero)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            bool existe = false;
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[COMPROBAR_PLACA_NRO_FLOTA]");
                if (id != 0)
                {
                    accesoDatos.AgregarSqlParametro("@TRAI_ID", id);
                }
                if (!string.IsNullOrEmpty(placa))
                {
                    accesoDatos.AgregarSqlParametro("@TRAI_PLACA", placa);
                }
                if (!string.IsNullOrEmpty(numero))
                {
                    accesoDatos.AgregarSqlParametro("@TRAI_NRO", numero);
                }
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    existe = Convert.ToBoolean(accesoDatos.SqlLectorDatos["EXISTE"]);
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
            return existe;
        }
        internal TrailerBC Trailer_ObtenerXViaje(string viaje)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            TrailerBC trailer2 = new TrailerBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILER_SALIDA]");
                accesoDatos.AgregarSqlParametro("@viaje", viaje);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    trailer2 = this.Trailer_ObtenerXPlaca(accesoDatos.SqlLectorDatos["TRAILER"].ToString());
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
            return trailer2;
        }
        internal DataTable Salida_ObtenerXViaje(string viaje)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILER_SALIDA]");
                accesoDatos.AgregarSqlParametro("@viaje", viaje);
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
        internal TrailerBC Trailer_ObtenerXId(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            TrailerBC trailer = new TrailerBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_TRAILER_V2]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    trailer = this.cargarDatosTrailer(accesoDatos.SqlLectorDatos);
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
            return trailer;
        }
        internal DataSet Trailer_ObtenerDatosSalida(string patente, string flota)
        {
            try
            {
                accesoDatos.CargarSqlComando("CARGA_TRAILER_SALIDA");
                if (!string.IsNullOrEmpty(patente))
                    accesoDatos.AgregarSqlParametro("@PLACA", patente);
                if (!string.IsNullOrEmpty(flota))
                    accesoDatos.AgregarSqlParametro("@FLOTA", flota);
                return accesoDatos.RetornaDS();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
                accesoDatos.LimpiarSqlParametros();
            }
        }
        internal TrailerBC Trailer_ObtenerXPlaca(string placa)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            TrailerBC trailer = new TrailerBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_TRAILER_V2]");
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    trailer = this.cargarDatosTrailer(accesoDatos.SqlLectorDatos);
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
            return trailer;
        }
        internal TrailerBC Trailer_ObtenerXNumeroFlota(string numero)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            TrailerBC trailer = new TrailerBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILER_X_PLACA]");
                accesoDatos.AgregarSqlParametro("@NUMERO", numero);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    trailer = this.cargarDatosTrailer(accesoDatos.SqlLectorDatos);
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
            return trailer;
        }
        internal DataTable Trailer_ObtenerXParametrobloqueo(string placa, string numero, bool externo, int tipo_id, int mantenimiento, int tran_id = 0, int site_id = 0)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILERS_X_CRITERIO_V2_bloqueo]");
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
            if (mantenimiento != 0)
            {
                accesoDatos.AgregarSqlParametro("@mantenimiento", mantenimiento);
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
        internal DataTable Trailer_ObtenerXParametrotaller(string placa, string numero, bool externo, int tipo_id, int mantenimiento, int tran_id = 0, int site_id = 0, int tipo_bloqueo = 0)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILERS_X_CRITERIO_V2_taller]");
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
            if (mantenimiento != 0)
            {
                accesoDatos.AgregarSqlParametro("@mantenimiento", mantenimiento);
            }

            if (tipo_bloqueo != 0)
            {
                accesoDatos.AgregarSqlParametro("@tipo_bloqueo", tipo_bloqueo);
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
        internal DataTable Trailer_ObtenerXParametro(string placa, string numero, bool externo, int tipo_id, int tran_id, int site_id, int zona_id, int play_id )
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILERS_X_CRITERIO_V3]");
                if (tipo_id != 0)
                    accesoDatos.AgregarSqlParametro("@TRTI_ID", tipo_id);
                if (tran_id != 0)
                    accesoDatos.AgregarSqlParametro("@TRAN_ID", tran_id);
                if (!String.IsNullOrEmpty(numero))
                    accesoDatos.AgregarSqlParametro("@TRAI_NUMERO", numero);
                if (!String.IsNullOrEmpty(placa))
                    accesoDatos.AgregarSqlParametro("@TRAI_PLACA", placa);
                if (externo)
                    accesoDatos.AgregarSqlParametro("@TRAI_EXTERNO", externo);
                if (site_id != 0)
                    accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                if (zona_id != 0)
                    accesoDatos.AgregarSqlParametro("@ZONA_ID", zona_id);
                if (play_id != 0)
                    accesoDatos.AgregarSqlParametro("@PLAY_ID", play_id);
                return accesoDatos.EjecutarSqlquery3();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
        }
        internal DataTable Trailer_ObtenerXParametroSTOCK(int site, int estado, bool asignado, bool bloquado, bool plancha, int shortec, int capacidad)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILERS_X_CRITERIO_STOCK]");
            if (site != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_site", site);
            }
            if (estado != 0)
            {
                accesoDatos.AgregarSqlParametro("@ID_estado", estado);
            }
            accesoDatos.AgregarSqlParametro("@asignado", asignado);
            accesoDatos.AgregarSqlParametro("@bloqueado", bloquado);
            accesoDatos.AgregarSqlParametro("@plancha", plancha);
            if (shortec != 0)
            {
                accesoDatos.AgregarSqlParametro("@shortec", shortec);
            }
            if (capacidad != 0)
            {
                accesoDatos.AgregarSqlParametro("@capacidad", capacidad);
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
        internal DataTable Trailer_Obtener_ReporteXParametro(string placa, string numero, bool externo, int tipo_id, int tran_id = 0, int site_id =0)
        {
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILERS_X_CRITERIO_V2_REPORTE]");
            if (tipo_id != 0)
                accesoDatos.AgregarSqlParametro("@ID_TIPO", tipo_id);
            if (tran_id != 0)
                accesoDatos.AgregarSqlParametro("@ID_TRAN", tran_id);
            if (!String.IsNullOrEmpty(numero))
                accesoDatos.AgregarSqlParametro("@NRO_FLOTA", numero);
            if (!String.IsNullOrEmpty(placa))
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
            if (externo)
                accesoDatos.AgregarSqlParametro("@EXTERNO", externo);

            if (site_id != 0)
                accesoDatos.AgregarSqlParametro("@site_id", site_id);
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
            }
        }
        internal DataTable Trailer_ObtenerXParametroOld(string placa, string externo, string sites = null)
        {
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TRAILERS_X_CRITERIO]");
            accesoDatos.AgregarSqlParametro("@EXTERNO", externo);
            if (!String.IsNullOrEmpty(placa))
                accesoDatos.AgregarSqlParametro("@PLACA", placa);
            if (!String.IsNullOrEmpty(sites))
                accesoDatos.AgregarSqlParametro("@Site_ID", sites);
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
            }
        }
        internal bool Trailer_Crear(TrailerBC trailer)
        {
            bool exito = false;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[EDITA_TRAILER]");
                accesoDatos.AgregarSqlParametro("@PLACA", trailer.PLACA);
                accesoDatos.AgregarSqlParametro("@CODIGO", trailer.CODIGO);
                accesoDatos.AgregarSqlParametro("@EXTERNO", trailer.EXTERNO);
                accesoDatos.AgregarSqlParametro("@NUMERO", trailer.NUMERO);
                accesoDatos.AgregarSqlParametro("@ID_TRANSPORTISTA", trailer.TRAN_ID);
                accesoDatos.AgregarSqlParametro("@ID_TIPO", trailer.TRTI_ID);
                accesoDatos.AgregarSqlParametro("@EXCLUYENTES", trailer.EXCLUYENTES);
                accesoDatos.AgregarSqlParametro("@NO_EXCLUYENTES", trailer.NO_EXCLUYENTES);
                accesoDatos.AgregarSqlParametro("@ID_SHORTEK", trailer.ID_SHORTEK);
                accesoDatos.AgregarSqlParametro("@REQ_SELLO", trailer.REQ_SELLO);
                accesoDatos.EjecutarSqlEscritura();
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
            }
            return exito;
        }
        internal bool Trailer_Modificar(TrailerBC trailer)
        {
            bool exito = false;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[EDITA_TRAILER]");
                accesoDatos.AgregarSqlParametro("@TRAI_ID", trailer.ID);
                accesoDatos.AgregarSqlParametro("@PLACA", trailer.PLACA);
                accesoDatos.AgregarSqlParametro("@CODIGO", trailer.CODIGO);
                accesoDatos.AgregarSqlParametro("@EXTERNO", trailer.EXTERNO);
                accesoDatos.AgregarSqlParametro("@NUMERO", trailer.NUMERO);
                accesoDatos.AgregarSqlParametro("@ID_TRANSPORTISTA", trailer.TRAN_ID);
                accesoDatos.AgregarSqlParametro("@ID_TIPO", trailer.TRTI_ID);
                accesoDatos.AgregarSqlParametro("@EXCLUYENTES", trailer.EXCLUYENTES);
                accesoDatos.AgregarSqlParametro("@NO_EXCLUYENTES", trailer.NO_EXCLUYENTES);
                accesoDatos.AgregarSqlParametro("@ID_SHORTEK", trailer.ID_SHORTEK);
                accesoDatos.AgregarSqlParametro("@REQ_SELLO", trailer.REQ_SELLO);
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (SqlException ex)
            {
                exito = false;
                throw (ex);
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        internal bool Trailer_Eliminar(int id)
        {
            bool exito = false;
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_TRAILER]");
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
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
            return exito;
        }
        private TrailerBC cargarDatosTrailer(SqlDataReader reader)
        {
            TrailerBC trailer = new TrailerBC();
            trailer.ID = Convert.ToInt32(reader["ID"].ToString());
            trailer.PLACA = reader["PLACA"].ToString();
            trailer.CODIGO = reader["CODIGO"].ToString();
            trailer.EXTERNO = Convert.ToBoolean(reader["EXTERNO"]);
            trailer.NUMERO = reader["NUMERO"].ToString();

            if (!String.IsNullOrEmpty(reader["TRAN_ID"].ToString()))
            {
                trailer.TRAN_ID = Convert.ToInt32(reader["TRAN_ID"].ToString());
            }
            else
            {
                trailer.TRAN_ID = 0;
            }

            if (!String.IsNullOrEmpty(reader["PYTI_ID"].ToString()))
            {
                trailer.PYTI_ID = Convert.ToInt32(reader["PYTI_ID"].ToString());
            }
            else
            {
                trailer.PYTI_ID = 0;
            }

            trailer.TRANSPORTISTA = reader["TRANSPORTISTA"].ToString();

            try
            {
                trailer.TRTI_ID = Convert.ToInt32(reader["ID_TIPO"].ToString());
            }
            catch (Exception)
            {
                trailer.TRTI_ID = 0;
            }

            try
            {
                trailer.SITE_ID = Convert.ToInt32(reader["SITE_ID"].ToString());
            }
            catch (Exception)
            {
                trailer.SITE_ID = 0;
            }
            if (!String.IsNullOrEmpty(reader["TRUE_SITE_IN"].ToString()))
            {
                trailer.SITE_IN = Convert.ToBoolean(reader["TRUE_SITE_IN"].ToString());
            }
            else
            {
                trailer.SITE_IN = false;
            }

            if (!String.IsNullOrEmpty(reader["CARGADO"].ToString()))
            {
                trailer.CARGADO = Convert.ToBoolean(reader["CARGADO"].ToString());
            }
            else
            {
                trailer.CARGADO = false;
            }
            if (!String.IsNullOrEmpty(reader["PROV_ID"].ToString()))
            {
                trailer.PROV_ID = Convert.ToInt32(reader["PROV_ID"].ToString());
            }
            else
            {
                trailer.PROV_ID = 0;
            }
            if (!String.IsNullOrEmpty(reader["MOVI_ID"].ToString()))
            {
                trailer.MOVI_ID = Convert.ToInt32(reader["MOVI_ID"].ToString());
            }
            else
            {
                trailer.MOVI_ID = 0;
            }
            if (!String.IsNullOrEmpty(reader["FECHA_INGRESO"].ToString()))
            {
                trailer.FECHA_INGRESO = Convert.ToDateTime(reader["FECHA_INGRESO"].ToString());
            }
            if (!String.IsNullOrEmpty(reader["FECHA_RETIRO"].ToString()))
            {
                trailer.FECHA_RETIRO = Convert.ToDateTime(reader["FECHA_RETIRO"].ToString());
            }
            trailer.OBSERVACION = reader["OBSERVACION"].ToString();
            trailer.DOC_INGRESO = reader["DOCUMENTO"].ToString();
            trailer.SELLO_INGRESO = reader["SELLO"].ToString();
            trailer.SELLO_CARGA = reader["SELLO_CARGA"].ToString();
            trailer.CHOFER_RUT = reader["RUT_CHOFER"].ToString();
            trailer.CHOFER_NOMBRE = reader["NOMBRE_CHOFER"].ToString();
            trailer.ACOMP_RUT = reader["RUT_ACOMPANANTE"].ToString();
            trailer.PATENTE_TRACTO = reader["PATENTE_TRACTO"].ToString();
            try
            {
                trailer.MOES_ID = Convert.ToInt32(reader["MOES_ID"].ToString());
            }
            catch
            {
                trailer.MOES_ID = 0;
            }
            try
            {
                trailer.SOES_ID = Convert.ToInt32(reader["SOES_ID"].ToString());
            }
            catch
            {
                trailer.SOES_ID = 0;
            }
            if (!String.IsNullOrEmpty(reader["LUGA_ID"].ToString()))
            {
                trailer.LUGAR_ID = Convert.ToInt32(reader["LUGA_ID"].ToString());
            }
            else
            {
                trailer.LUGAR_ID = 0;
            }
            if (!String.IsNullOrEmpty(reader["soli_id"].ToString()))
            {
                trailer.SOLI_ID = Convert.ToInt32(reader["soli_id"].ToString());
            }
            else
            {
                trailer.SOLI_ID = 0;
            }

            try
            {
                trailer.TRES_ID = Convert.ToInt32(reader["tres_id"].ToString());
            }
            catch (Exception)
            {
            }

            trailer.TIPO = reader["TIPO_TRAILER"].ToString();
            trailer.CARACTERISTICAS = reader["CARACTERISTICAS"].ToString();
            trailer.EXCLUYENTES = reader["EXCLUYENTES"].ToString();
            trailer.NO_EXCLUYENTES = reader["NO_EXCLUYENTES"].ToString();
            trailer.BLOQUEADO = reader["TRAI_BLOQUEADO"].ToString();
            if (!string.IsNullOrEmpty(reader["ID_SHORTEK"].ToString()))
            {
                trailer.ID_SHORTEK = reader["ID_SHORTEK"].ToString();
            }
            else
            {
                trailer.ID_SHORTEK = "0";
            }
            try
            {
                trailer.COND_ID = Convert.ToInt32(reader["COND_ID"].ToString());
            }
            catch (Exception)
            {
                trailer.COND_ID = 0;
            }
            return trailer;
        }
        internal DataTable Trailer_ObtenerEstados()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TRAILER_ESTADOS]").Tables[0];
        }
        internal TrailerEstadoBC TrailerEstado_ObtenerXId(int tres_id)
        {
            TrailerEstadoBC t = new TrailerEstadoBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_TRAILER_ESTADOS]");
                accesoDatos.AgregarSqlParametro("@TRES_ID", tres_id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    t.ID = Convert.ToInt32(accesoDatos.SqlLectorDatos["ID"]);
                    t.DESCRIPCION = Convert.ToString(accesoDatos.SqlLectorDatos["DESCRIPCION"]);
                }
                return t;
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
        internal DataTable Trailer_ObtenerEstados_stock()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TRAILER_ESTADOS_STOCK]").Tables[0];
        }
        internal DataTable obtener_listado_trailer(int SITIO, int DISPONIBILIDAD, bool BLOQUEADO, int CAPACIDAD, bool PLANCHA, bool asignado, int tipo_carga, string shortec)
        {
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[LISTAR_REPORTE_TRAILER]");
                accesoDatos.AgregarSqlParametro("@SITIO", SITIO);
                accesoDatos.AgregarSqlParametro("@DISPONIBILIDAD", DISPONIBILIDAD);
                accesoDatos.AgregarSqlParametro("@BLOQUEADO", BLOQUEADO);
                accesoDatos.AgregarSqlParametro("@CAPACIDAD", CAPACIDAD);
                accesoDatos.AgregarSqlParametro("@PLANCHA", PLANCHA);
                accesoDatos.AgregarSqlParametro("@asignado", asignado);
                accesoDatos.AgregarSqlParametro("@tipo_carga", tipo_carga);
                accesoDatos.AgregarSqlParametro("@shortec", shortec);
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
        }
        #endregion
        #region TrailerTipo
        internal DataTable TrailerTipo_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TIPO_TRAILER]").Tables[0];
        }
        internal TrailerTipoBC TrailerTipo_ObtenerXId(int id)
        {
            TrailerTipoBC trailer_tipo = new TrailerTipoBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_TIPO_TRAILER]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    trailer_tipo = this.cargarDatosTrailerTipo(accesoDatos.SqlLectorDatos);
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
            return trailer_tipo;
        }
        internal DataTable TrailerTipo_ObtenerXParametro(string descripcion)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_TIPO_TRAILER]");
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
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
        internal bool TrailerTipo_Crear(TrailerTipoBC trailer_tipo)
        {
            bool exito = false;
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_TIPO_TRAILER]");
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", trailer_tipo.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@COLOR", trailer_tipo.COLOR);
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
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool TrailerTipo_Modificar(TrailerTipoBC trailer_tipo)
        {
            bool exito = false;
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_TIPO_TRAILER]");
            accesoDatos.AgregarSqlParametro("@ID", trailer_tipo.ID);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", trailer_tipo.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@COLOR", trailer_tipo.COLOR);
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
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool TrailerTipo_Eliminar(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_TIPO_TRAILER]");
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
        private TrailerTipoBC cargarDatosTrailerTipo(SqlDataReader reader)
        {
            TrailerTipoBC trailer_tipo = new TrailerTipoBC();
            trailer_tipo.ID = Convert.ToInt32(reader["ID"]);
            trailer_tipo.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
            trailer_tipo.COLOR = Convert.ToString(reader["COLOR"]);
            return trailer_tipo;
        }
        #endregion
        #region Transportista
        internal DataTable ObtenerMotivoBloqueo(string user)
        {
            //  return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_MOTIVO_BLOQUEO]").Tables[0];
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            //  TransportistaBC transportista = new TransportistaBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_MOTIVO_BLOQUEO]");
                accesoDatos.AgregarSqlParametro("@solo_user", user);
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
        }
        internal DataTable Transportista_ObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_TRANSPORTISTA]").Tables[0];
        }
        internal TransportistaBC Transportista_ObtenerXRut(string rut)
        {
            TransportistaBC transportista = new TransportistaBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TRANSPORTISTA_X_RUT]");
                accesoDatos.AgregarSqlParametro("@RUT", rut);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    transportista = this.cargarDatosTransportista(accesoDatos.SqlLectorDatos);
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
            return transportista;
        }
        internal TransportistaBC Transportista_ObtenerXId(int id)
        {
            TransportistaBC transportista = new TransportistaBC();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TRANSPORTISTA_X_ID]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    transportista = this.cargarDatosTransportista(accesoDatos.SqlLectorDatos);
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
            return transportista;
        }
        internal bool Transportista_Crear(TransportistaBC transportista)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_TRANSPORTISTA]");
            accesoDatos.AgregarSqlParametro("@NOMBRE", transportista.NOMBRE);
            accesoDatos.AgregarSqlParametro("@RUT", transportista.RUT);
            //accesoDatos.AgregarSqlParametro("@PASS", transportista.PASS);
            accesoDatos.AgregarSqlParametro("@ROL", transportista.ROL);
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
        internal bool Transportista_Crear(TransportistaBC transportista, out int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_TRANSPORTISTA]");
            accesoDatos.AgregarSqlParametro("@NOMBRE", transportista.NOMBRE);
            accesoDatos.AgregarSqlParametro("@RUT", transportista.RUT);
            //accesoDatos.AgregarSqlParametro("@PASS", transportista.PASS);
            accesoDatos.AgregarSqlParametro("@ROL", transportista.ROL);
            try
            {
                accesoDatos.EjecutaSqlInsertIdentity();
                id = accesoDatos.ID;
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }
        internal bool Transportista_Modificar(TransportistaBC transportista)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_TRANSPORTISTA]");
            accesoDatos.AgregarSqlParametro("@ID", transportista.ID);
            accesoDatos.AgregarSqlParametro("@NOMBRE", transportista.NOMBRE);
            accesoDatos.AgregarSqlParametro("@RUT", transportista.RUT);
            //accesoDatos.AgregarSqlParametro("@PASS", transportista.PASS);
            accesoDatos.AgregarSqlParametro("@ROL", transportista.ROL);
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
        internal bool Transportista_Eliminar(int id)
        {
            bool esExitosa = false;
            SqlAccesoDatos accesoDatos;
            accesoDatos = null;
            accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[ELIMINA_TRANSPORTISTA]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlEscritura();
                esExitosa = true;
            }
            catch (Exception ex)
            {
                esExitosa = false;
                throw ex;
            }
            return esExitosa;
        }
        internal DataTable Transportista_ObtenerXParametro(String rut, String nombre)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TRANSPORTISTAS_X_CRITERIO]");
                accesoDatos.AgregarSqlParametro("@RUT", rut);
                accesoDatos.AgregarSqlParametro("@NOMBRE", nombre);
                dt = accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        private TransportistaBC cargarDatosTransportista(SqlDataReader reader)
        {
            TransportistaBC transportista = new TransportistaBC();
            if (reader["ID"] != DBNull.Value)
                transportista.ID = Convert.ToInt32(reader["ID"]);
            if (reader["NOMBRE"] != DBNull.Value)
                transportista.NOMBRE = Convert.ToString(reader["NOMBRE"]);
            if (reader["RUT"] != DBNull.Value)
                transportista.RUT = Convert.ToString(reader["RUT"]);
            if (reader["PASS"] != DBNull.Value)
                transportista.PASS = Convert.ToString(reader["PASS"]);
            if (reader["ROL"] != DBNull.Value)
                transportista.ROL = Convert.ToInt32(reader["ROL"]);
            transportista.EXISTE = reader.HasRows;
            return transportista;
        }
        #endregion
        #region UsuarioRemolcador
        internal DataTable UsuarioRemolcador_ObtenerTodos(int site_id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_TODO_USUARIO_REMOLCADOR] {0}", site_id)).Tables[0];
        }
        internal DataTable UsuarioRemolcador_ObtenerTodosControl(int site_id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_CONTROL_USUARIO_REMOLCADOR] {0}", site_id)).Tables[0];
        }
        internal bool UsuarioRemolcador_Agregar(UsuarioRemolcadorBC u)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_USUARIO_REMOLCADOR]");
            accesoDatos.AgregarSqlParametro("@USUA_ID", u.USUA_ID);
            accesoDatos.AgregarSqlParametro("@REMO_ID", u.REMO_ID);
            accesoDatos.AgregarSqlParametro("@JORN_ID", u.JORN_ID);
            accesoDatos.AgregarSqlParametro("@SITE_ID", u.SITE_ID);
            accesoDatos.AgregarSqlParametro("@REPD_ID", u.REPD_ID);
            accesoDatos.AgregarSqlParametro("@FECHA", u.FECHA);
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
        internal bool UsuarioRemolcador_Eliminar(string repr_id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_USUARIO_REMOLCADOR]");
            accesoDatos.AgregarSqlParametro("@REPR_ID", repr_id);
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
        internal bool UsuarioRemolcador_ComprobarRegistros(UsuarioRemolcadorBC ur, string repr_id)
        {
            try
            {
                int cantidad = 0;
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[COMPROBAR_REGISTROS_USUARIO_REMOLCADOR]");
                if (!string.IsNullOrEmpty(repr_id))
                {
                    accesoDatos.AgregarSqlParametro("@REPR_ID", repr_id);
                }
                accesoDatos.AgregarSqlParametro("@REMO_ID", ur.REMO_ID);
                accesoDatos.AgregarSqlParametro("@JORN_ID", ur.JORN_ID);
                accesoDatos.AgregarSqlParametro("@SITE_ID", ur.SITE_ID);
                accesoDatos.AgregarSqlParametro("@FECHA", ur.FECHA);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    cantidad = Convert.ToInt32(accesoDatos.SqlLectorDatos["CANTIDAD"].ToString());
                }
                if (cantidad > 0)
                {
                    return false;
                }
                else
                {
                    return true;
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
        }
        internal DataTable UsuarioRemolcador_CargaXRemoId(int remo_id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_USUARIO_REMOLCADOR_X_ID_REMO] {0}", remo_id)).Tables[0];
        }
        #endregion
        #region Zona
        internal DataTable Zona_ObtenerTodas()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_ZONA]").Tables[0];
        }
        internal DataTable Zona_ObtenerTodas(int zoti_id)
        {
            return accesoDatos.dsCargarSqlQuery(string.Format("[dbo].[CARGA_TODO_ZONA] @ZOTI_ID = {0}", zoti_id)).Tables[0];
        }
        internal DataTable Zona_ObtenerXSite(int SITE_ID, bool incluirvirtual)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_ZONA]");
                accesoDatos.AgregarSqlParametro("@SITE_ID", SITE_ID);
                if (incluirvirtual)
                {
                    accesoDatos.AgregarSqlParametro("@virtual", DBNull.Value);

                }
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                accesoDatos.CerrarSqlConeccion();
            }
        }
        internal DataTable Zona_ObtenerXParametros(ZonaBC z)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_ZONA]");
                accesoDatos.AgregarSqlParametro("@SITE_ID", z.SITE_ID);
                accesoDatos.AgregarSqlParametro("@ZOTI_ID", z.ZOTI_ID);
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", z.DESCRIPCION);
                dt = accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal DataTable Zona_ObtenerXParametrosMant(ZonaBC z)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_ZONA]");
                accesoDatos.AgregarSqlParametro("@SITE_ID", z.SITE_ID);
                accesoDatos.AgregarSqlParametro("@ZOTI_ID", z.ZOTI_ID);
                accesoDatos.AgregarSqlParametro("@virtual", DBNull.Value);
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", z.DESCRIPCION);
                dt = accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception)
            {
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return dt;
        }
        internal DataTable Zona_ObtenerXParametro(string descripcion, int site_id, int zoti_id = 0)
        {
            try
            {

                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_ZONA]");
                if (!string.IsNullOrEmpty(descripcion))
                    accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
                if (site_id != 0)
                    accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);
                if (zoti_id != 0)
                {
                    accesoDatos.AgregarSqlParametro("@ZOTI_ID", zoti_id);
                    accesoDatos.AgregarSqlParametro("@ZOTI_IGUAL", true);
                }
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
        }
        internal DataTable Zona_ObtenerXSite(int id_site, int zoti_id)
        {
            try
            {

                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_ZONA]");
                if (id_site != 0)
                    accesoDatos.AgregarSqlParametro("@SITE_ID", id_site);
                if (zoti_id != 0)
                    accesoDatos.AgregarSqlParametro("@ZOTI_ID", zoti_id);
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
            }
        }
        internal DataTable Zona_ObtenerLI(int id_site, int zoti_id, bool zoti_igual, int pyti_id)
        {
            try
            {

                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_ZONA]");
                if (id_site != 0)
                    accesoDatos.AgregarSqlParametro("@SITE_ID", id_site);
                if (zoti_id != 0)
                    accesoDatos.AgregarSqlParametro("@ZOTI_ID", zoti_id);
                if (zoti_id != 0)
                    accesoDatos.AgregarSqlParametro("@ZOTI_IGUAL", zoti_igual);
                if (zoti_id != 0)
                    accesoDatos.AgregarSqlParametro("@PYTI_ID", pyti_id);
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
        internal ZonaBC Zona_ObtenerXId(int id)
        {
            ZonaBC zona = new ZonaBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_ZONA]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    zona = this.cargarDatosZona(accesoDatos.SqlLectorDatos);
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
            return zona;
        }
        internal bool Zona_Crear(ZonaBC zona)
        {
            bool exito = false;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[EDITA_ZONA]");
                accesoDatos.AgregarSqlParametro("@CODIGO", zona.CODIGO);
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", zona.DESCRIPCION);
                accesoDatos.AgregarSqlParametro("@SITE_ID", zona.SITE_ID);
                accesoDatos.AgregarSqlParametro("@TIPO_ZONA_ID", zona.ZOTI_ID);
                accesoDatos.EjecutarSqlEscritura();
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
        internal bool Zona_Modificar(ZonaBC zona)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[EDITA_ZONA]");
            accesoDatos.AgregarSqlParametro("@ID", zona.ID);
            accesoDatos.AgregarSqlParametro("@CODIGO", zona.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", zona.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@SITE_ID", zona.SITE_ID);
            accesoDatos.AgregarSqlParametro("@TIPO_ZONA_ID", zona.ZOTI_ID);
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
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }
        internal bool Zona_Eliminar(int id)
        {
            bool exito = false;
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[ELIMINA_ZONA]");
                accesoDatos.AgregarSqlParametro("@ID", id);
                accesoDatos.EjecutarSqlEscritura();
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
        internal bool Zona_Virtual(int id)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.LimpiarSqlParametros();
            accesoDatos.CargarSqlComando("[dbo].[ZONA_VIRTUAL]");
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
        private ZonaBC cargarDatosZona(SqlDataReader reader)
        {
            ZonaBC zona = new ZonaBC();
            zona.ID = Convert.ToInt32(reader["ID"]);
            zona.CODIGO = reader["CODIGO"].ToString();
            zona.DESCRIPCION = reader["DESCRIPCION"].ToString();
            if (reader["SITE_ID"] != DBNull.Value)
                zona.SITE_ID = Convert.ToInt32(reader["SITE_ID"]);
            if (reader["ZOTI_ID"] != DBNull.Value)
                zona.ZOTI_ID = Convert.ToInt32(reader["ZOTI_ID"]);
            return zona;
        }
        #endregion
    }
}