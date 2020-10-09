using System;
using System.Data;
using System.Data.SqlClient;

namespace Qanalytics.Data.Access.SqlClient
{
    public class SqlTransactionUsuario
    {
        FuncionesGenerales funcion = new FuncionesGenerales();
        SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

        public SqlTransactionUsuario()
        {
        }

        #region Usuario

        internal bool UsuarioCrear(UsuarioBC usuario)
        {
            bool exito = false;
          //  usuario.PASSWORD = funcion.Encriptar(usuario.PASSWORD, usuario.USERNAME.ToLower());
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[AGREGA_USUARIO]");
            accesoDatos.AgregarSqlParametro("@CODIGO", usuario.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", usuario.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@NOMBRE", usuario.NOMBRE);
            accesoDatos.AgregarSqlParametro("@APELLIDO", usuario.APELLIDO);
            accesoDatos.AgregarSqlParametro("@RUT", usuario.RUT);
            accesoDatos.AgregarSqlParametro("@EMAIL", usuario.EMAIL);
            accesoDatos.AgregarSqlParametro("@USERNAME", usuario.USERNAME);
            accesoDatos.AgregarSqlParametro("@PASSWORD", usuario.PASSWORD);
            if (usuario.ESTADO)
                accesoDatos.AgregarSqlParametro("@ESTADO", "Activo");
            else
                accesoDatos.AgregarSqlParametro("@ESTADO", "Inactivo");
            accesoDatos.AgregarSqlParametro("@OBSERVACION", usuario.OBSERVACION);
            accesoDatos.AgregarSqlParametro("@ID_EMPRESA", usuario.ID_EMPRESA);
            accesoDatos.AgregarSqlParametro("@ID_TIPO", usuario.ID_TIPO);
            accesoDatos.AgregarSqlParametro("@ID_SITE", usuario.SITE);
            if(usuario.ID_PROVEEDOR != 0 && usuario.ID_PROVEEDOR != null)
                accesoDatos.AgregarSqlParametro("@ID_PROV", usuario.ID_PROVEEDOR);
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (Exception ex)
            {
                exito = false;
              //  throw (ex);
            }
            return exito;
        }

        internal bool CambioPass(UsuarioBC usuario)
        {
            bool exito = false;
            //  usuario.PASSWORD = funcion.Encriptar(usuario.PASSWORD, usuario.USERNAME.ToLower());
            accesoDatos.CargarSqlComando("[dbo].[CAMBIO_PASS]");
            accesoDatos.AgregarSqlParametro("@USU_ID", usuario.ID);
            accesoDatos.AgregarSqlParametro("@NEW_PASS", usuario.PASSWORD2);
            usuario.PASSWORD = usuario.PASSWORD2;

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

        internal DataTable UsuarioObtenerTodos()
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_USUARIO]").Tables[0];
        }

        internal DataTable UsuarioObtenerXSite(int site_id)
        {
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_TODO_USUARIO] @site_id = " + site_id).Tables[0];
        }

        internal UsuarioBC UsuarioObtenerPorId(int id)
        {
            UsuarioBC usuario = new UsuarioBC();
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_USUARIO]");
                accesoDatos.AgregarSqlParametro("@USUA_ID", id);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    usuario = cargarDatosUsuario(accesoDatos.SqlLectorDatos);
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
            return usuario;
        }

        internal bool UsuarioModificar(UsuarioBC usuario)
        {
            bool exito = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            accesoDatos.CargarSqlComando("[dbo].[MODIFICA_USUARIO]");
            accesoDatos.AgregarSqlParametro("@ID", usuario.ID);
            //accesoDatos.AgregarSqlParametro("@CODIGO", usuario.CODIGO);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", usuario.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@NOMBRE", usuario.NOMBRE);
            accesoDatos.AgregarSqlParametro("@APELLIDO", usuario.APELLIDO);
            accesoDatos.AgregarSqlParametro("@RUT", usuario.RUT);
            accesoDatos.AgregarSqlParametro("@EMAIL", usuario.EMAIL);
            accesoDatos.AgregarSqlParametro("@USERNAME", usuario.USERNAME);
            accesoDatos.AgregarSqlParametro("@PASSWORD", usuario.PASSWORD);
            if (usuario.ESTADO)
                accesoDatos.AgregarSqlParametro("@ESTADO", "Activo");
            else
                accesoDatos.AgregarSqlParametro("@ESTADO", "Inactivo");
            accesoDatos.AgregarSqlParametro("@OBSERVACION", usuario.OBSERVACION);
            accesoDatos.AgregarSqlParametro("@ID_EMPRESA", usuario.ID_EMPRESA);
            accesoDatos.AgregarSqlParametro("@ID_TIPO", usuario.ID_TIPO);
            accesoDatos.AgregarSqlParametro("@ID_SITE", usuario.SITE);
            if (usuario.ID_PROVEEDOR != 0 && usuario.ID_PROVEEDOR != null)
                accesoDatos.AgregarSqlParametro("@ID_PROV", usuario.ID_PROVEEDOR);
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

        internal bool UsuarioModificarPass(UsuarioBC usuario)
        {
            bool esExitosa = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[ACTUALIZA_PASS_USUARIO]");
                accesoDatos.AgregarSqlParametro("@ID", usuario.ID);
                accesoDatos.AgregarSqlParametro("@PASS", usuario.PASSWORD);
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

        internal bool EliminarUsuario(int id)
        {
            bool esExitosa = false;
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[ELIMINA_USUARIO]");
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

        internal DataTable LoguearUsuario(string username, string password)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
             try
            {
                accesoDatos.CargarSqlComando("[dbo].[LOGIN_USUARIO]");
                accesoDatos.AgregarSqlParametro("@USERNAME", username);
                accesoDatos.AgregarSqlParametro("@PASSWORD", password);
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            // return accesoDatos.dsCargarSqlQuery("[dbo].[LOGIN_USUARIO] @USERNAME = '" + username + "', @PASSWORD = '" + password + "'").Tables[0];
        }

        internal DataTable LoguearUsuarioproveedor(string username, string password, string proveedor)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[LOGIN_USUARIO]");
                accesoDatos.AgregarSqlParametro("@USERNAME", username);
                accesoDatos.AgregarSqlParametro("@PASSWORD", password);
                accesoDatos.AgregarSqlParametro("@proveedor", proveedor);
                return accesoDatos.EjecutarSqlquery2();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            // return accesoDatos.dsCargarSqlQuery("[dbo].[LOGIN_USUARIO] @USERNAME = '" + username + "', @PASSWORD = '" + password + "'").Tables[0];
        }

        internal bool UsuarioDesactivar(int id)
        {
            bool esExitosa = false;
            SqlAccesoDatos accesoDatos;
            accesoDatos = null;
            accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[DESACTIVA_USUARIO]");
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

        internal bool UsuarioActivar(int id)
        {
            bool esExitosa = false;
            SqlAccesoDatos accesoDatos;
            accesoDatos = null;
            accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[ACTIVA_USUARIO]");
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

        internal UsuarioBC UsuarioObtenerPorRut(string rut)
        {
            UsuarioBC usuario = new UsuarioBC();
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_USUARIO]");
                accesoDatos.AgregarSqlParametro("@RUT", rut);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    usuario = cargarDatosUsuario(accesoDatos.SqlLectorDatos);
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
            return usuario;

        }

        internal UsuarioBC obtenerPorUsername(string userName)
        {
            UsuarioBC usuario = new UsuarioBC();
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_USUARIO]");
                accesoDatos.AgregarSqlParametro("@USERNAME", userName);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    usuario = cargarDatosUsuario(accesoDatos.SqlLectorDatos);
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
            return usuario;
        }

        private UsuarioBC cargarDatosUsuario(SqlDataReader reader) {
            UsuarioBC usuario = new UsuarioBC();
            usuario.ID = int.Parse(reader["ID"].ToString());
            usuario.NOMBRE = reader["NOMBRE"].ToString();
            usuario.APELLIDO = reader["APELLIDO"].ToString();
            usuario.RUT = reader["RUT"].ToString();
            usuario.USERNAME = reader["USERNAME"].ToString();
            usuario.PASSWORD = reader["PASS"].ToString();
            usuario.OBSERVACION = reader["OBS"].ToString();
            usuario.ID_TIPO = Convert.ToInt32(reader["ID_USUARIO_TIPO"].ToString());
            usuario.TIPO = reader["TIPO"].ToString();
            usuario.PROVEEDOR = reader["PROVEEDOR"].ToString();
            if(!string.IsNullOrEmpty(reader["PROV_ID"].ToString()))
                usuario.ID_PROVEEDOR = Convert.ToInt32(reader["PROV_ID"].ToString());
            usuario.numero_sites = Convert.ToInt32(reader["numero_sites"].ToString());
            if (reader["ESTADO"].ToString() == "Activo")
                usuario.ESTADO = true;
            else
                usuario.ESTADO = false;
            usuario.EMAIL = reader["CORREO"].ToString();
            usuario.SITE = reader["ID_SITES"].ToString();
            usuario.ID_EMPRESA = int.Parse(reader["ID_EMPRESA"].ToString());
            if (reader["NIVEL_PERMISOS"] != DBNull.Value)
            {
                usuario.NIVEL_PERMISOS = Convert.ToInt32(reader["NIVEL_PERMISOS"]);
            }
            try {
                usuario.INICIO = reader["INICIO"].ToString();
            }
            catch
            {
                usuario.INICIO = "~/App/Inicio.aspx";
            }

            return usuario;
        }

        internal DataTable UsuarioObtenerAsignados(int usua_id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            return accesoDatos.dsCargarSqlQuery("[dbo].[CARGA_LUGARES_ASIGNADOS_SITE_USUARIO] " + usua_id).Tables[0];
        }

        internal DataTable UsuarioObtenerXTipo(int usti_id, int site_id)
        {
            //string query = "[dbo].[CARGA_TODO_USUARIO] @USTI_ID = " + usti_id;
            //if (site_id != 0)
            //    query += ", @SITE_ID = " + site_id;
            //return accesoDatos.dsCargarSqlQuery(query).Tables[0];

    
            SqlAccesoDatos accesoDatos;
            accesoDatos = null;
            accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_USUARIO]");
                if (usti_id != 0)
                accesoDatos.AgregarSqlParametro("@USTI_ID", usti_id);
                if (site_id != 0)
                accesoDatos.AgregarSqlParametro("@site_id", site_id);
                return accesoDatos.EjecutarSqlquery2();
   
            }
            catch (Exception ex)
            {
          
                throw ex;
            }

        }

        internal DataTable UsuarioObtenerXParametro(String rut, String nombre, String apellido, bool solo_activos, int id_tipo_usuario)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);

            String query = "[dbo].[CARGA_USUARIOS_X_CRITERIO] ";

            if (rut != null && rut != String.Empty)
                query += "@RUT = N'" + rut + "',";
            else
                query += "@RUT = NULL,";

            if (nombre != null && nombre != String.Empty)
                query += "@NOMBRE = N'" + nombre + "',";
            else
                query += "@NOMBRE = NULL,";

            if (apellido != null && apellido != String.Empty)
                query += "@APELLIDO = N'" + apellido + "',";
            else
                query += "@APELLIDO = NULL,";

            if (solo_activos)
                query += "@ESTADO = 'ACTIVO',";
            else
                query += "@ESTADO = null,";

            if (id_tipo_usuario != 0)
                query += "@ID_TIPOUSUARIO = " + id_tipo_usuario + "";
            else
                query += "@ID_TIPOUSUARIO = NULL";


            return accesoDatos.dsCargarSqlQuery(query).Tables[0];
        }



        internal bool UsuarioAsignarLugares(int usua_id, string luga_id)
        {
            bool esExitosa = false;
            SqlAccesoDatos accesoDatos;
            accesoDatos = null;
            accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[AGREGA_LUGARES_ESPECIALES_USUARIO]");
                accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
                accesoDatos.AgregarSqlParametro("@LUGA_ID", luga_id);
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


        #endregion

        #region Perfil

        internal DataTable Perfil_ObtenerTodo(bool mobile)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_PERFIL]");
                if (mobile != null)
                    accesoDatos.AgregarSqlParametro("@MOBILE", mobile);
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

        internal DataTable Perfil_ObtenerTodo()
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_PERFIL]");
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

        internal DataTable Perfil_ObtenerAutorizados(int usua_id)
        {
            DataTable dt = new DataTable();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[OBTENER_PERFILES_AUTORIZADOS]");
                accesoDatos.AgregarSqlParametro("@USUA_ID", usua_id);
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

        internal PerfilBC Perfil_ObtenerPorId(int id, bool mobile)
        {
            PerfilBC perfil = new PerfilBC();
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[CARGA_PERFIL]");
                accesoDatos.AgregarSqlParametro("@USTI_ID", id);
                accesoDatos.AgregarSqlParametro("@MOBILE", mobile);
                accesoDatos.EjecutarSqlLector();
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    perfil = cargarDatosPerfil(accesoDatos.SqlLectorDatos);
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
            return perfil;
        }

        internal bool Perfil_Crear(PerfilBC perfil)
        {
            bool exito = false;
            accesoDatos.CargarSqlComando("[dbo].[EDITA_PERFIL]");
            accesoDatos.AgregarSqlParametro("@USTI_NOMBRE", perfil.NOMBRE);
            accesoDatos.AgregarSqlParametro("@USTI_DESCRIPCION", perfil.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@MENU", perfil.MENU);
            accesoDatos.AgregarSqlParametro("@USTI_MOBILE", perfil.MOBILE);
            accesoDatos.AgregarSqlParametro("@NIVEL_PERMISOS", perfil.NIVEL_PERMISOS);
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (SqlException ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }

        internal bool Perfil_Modificar(PerfilBC perfil)
        {
            bool exito = false;
            accesoDatos.CargarSqlComando("[dbo].[EDITA_PERFIL]");
            accesoDatos.AgregarSqlParametro("@USTI_ID", perfil.ID);
            accesoDatos.AgregarSqlParametro("@USTI_NOMBRE", perfil.NOMBRE);
            accesoDatos.AgregarSqlParametro("@USTI_DESCRIPCION", perfil.DESCRIPCION);
            accesoDatos.AgregarSqlParametro("@MENU", perfil.MENU);
            accesoDatos.AgregarSqlParametro("@USTI_MOBILE", perfil.MOBILE);
            accesoDatos.AgregarSqlParametro("@NIVEL_PERMISOS", perfil.NIVEL_PERMISOS);
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (SqlException ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }

        internal bool Perfil_Eliminar(int id)
        {
            bool exito = false;
            accesoDatos.CargarSqlComando("[dbo].[ELIMINA_TIPO_USUARIO]");
            accesoDatos.AgregarSqlParametro("@ID", id);
            try
            {
                accesoDatos.EjecutarSqlEscritura();
                exito = true;
            }
            catch (SqlException ex)
            {
                exito = false;
                throw (ex);
            }
            return exito;
        }

        private PerfilBC cargarDatosPerfil(SqlDataReader reader)
        {
            PerfilBC perfil = new PerfilBC();
            perfil.ID = int.Parse(reader["ID"].ToString());
            perfil.NOMBRE = reader["NOMBRE"].ToString();
            perfil.DESCRIPCION = reader["DESCRIPCION"].ToString();
            perfil.NIVEL_PERMISOS = int.Parse(reader["NIVEL_PERMISOS"].ToString());
            perfil.MENU = reader["MENUS"].ToString();
            if (reader["MOBILE"].ToString().ToLower() == "true")
                perfil.MOBILE = true;
            else
                perfil.MOBILE = false;
            return perfil;
        }

        #endregion

        #region Menu

        internal DataTable Menu_ObtenerTodo()
        {
            String query = "[dbo].[CARGA_TODO_MENU]";
            return accesoDatos.dsCargarSqlQuery(query).Tables[0];
        }

        internal DataTable MenuMobile_ObtenerTodo()
        {
            String query = "[dbo].[CARGA_TODO_MENU_MOBILE]";
            return accesoDatos.dsCargarSqlQuery(query).Tables[0];
        }

        #endregion

    }
}