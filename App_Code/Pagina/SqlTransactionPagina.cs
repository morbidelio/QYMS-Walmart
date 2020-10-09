using System;
using System.Collections.Generic;

using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Qanalytics.Data.Access.SqlClient
{
    /// <summary>
    /// Descripción breve de SqlTransactionPagina
    /// </summary>
    public class SqlTransactionPagina
    {
        public SqlTransactionPagina()
        {
        }

        #region Vehiculo


        public DataTable obtenerEncabezadosMenu() {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            return accesoDatos.dsCargarSqlQuery("[dbo].[LISTAR_ENCABEZADOS_PAGINAS_MENU] ").Tables[0];
        }

        public DataTable obtenerPaginasMenu(int OrdenEncabezado)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            return accesoDatos.dsCargarSqlQuery("[dbo].[LISTAR_PAGINAS_MENU] "+OrdenEncabezado).Tables[0];
        }

        public DataTable obtenerMenu(int idPerfil)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            return accesoDatos.dsCargarSqlQuery("[dbo].[LISTAR_MENU] "+ idPerfil).Tables[0];
        }

        public DataTable obtenerTodas()
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            return accesoDatos.dsCargarSqlQuery("[dbo].[LISTAR_MENU] ").Tables[0];
        }

        public String obtenerIdsPaginasPorPerfil(int idPerfil)
        {
            String ids = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[LISTAR_IDS_PAGINAS_TIPO_USUARIO]");
                accesoDatos.AgregarSqlParametro("@TIPO_USUARIO", idPerfil);
                accesoDatos.EjecutarSqlLector();
                int columnas = accesoDatos.SqlLectorDatos.FieldCount;
                while (accesoDatos.SqlLectorDatos.Read())
                {
                    ids = accesoDatos.SqlLectorDatos["IDS"].ToString();
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
            return ids;
        }
        
        #endregion
    }
}