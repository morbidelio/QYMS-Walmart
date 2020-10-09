using System;

using System.Linq;

using System.Data;
using System.Data.SqlClient;


namespace Qanalytics.Data.Access.SqlClient
{
    public class SqlTransaction_YMS
    {
        public SqlTransaction_YMS()
        {
        }

        internal DataTable obtenerZonasTipoCarga(int ID_SITE, string descripcion, string tipo_zona, int tipo_carga, int playa_tipo)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_ZONA_TIPO_CARGA]");

            accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
            if (tipo_carga != 0)
                accesoDatos.AgregarSqlParametro("@tipo_carga", tipo_carga);
            else
                accesoDatos.AgregarSqlParametro("@tipo_carga", DBNull.Value);

            if (playa_tipo != 0)
                accesoDatos.AgregarSqlParametro("@pyti_id", playa_tipo);

            accesoDatos.AgregarSqlParametro("@SITE_ID", ID_SITE);
            accesoDatos.AgregarSqlParametro("@ZOTI_ID", tipo_zona);
            accesoDatos.AgregarSqlParametro("@ZOTI_igual", "1");

            return accesoDatos.EjecutarSqlquery2();
        }

        internal DataTable obtenerZonas(int ID_SITE, string descripcion, string tipo_zona)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TODO_ZONA]");

            accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
            accesoDatos.AgregarSqlParametro("@SITE_ID", ID_SITE);
            accesoDatos.AgregarSqlParametro("@ZOTI_ID", tipo_zona);
            return accesoDatos.EjecutarSqlquery2();
        }


        internal DataTable obtener_tipoCarga(string descripcion)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TIPOS_INGRESO_CARGA_X_CRITERIO_v2]");
            if (!string.IsNullOrEmpty(descripcion))
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable obtener_tipoCarga(string descripcion, bool preentrada, bool entrada)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_TIPOS_INGRESO_CARGA_X_CRITERIO_v2]");
            if (!string.IsNullOrEmpty(descripcion))
                accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
            if (preentrada)
                accesoDatos.AgregarSqlParametro("@espreentrada", preentrada);
            if (entrada)
                accesoDatos.AgregarSqlParametro("@esentrada", entrada);
            return accesoDatos.EjecutarSqlquery2();
        }



        internal DataTable obtener_motivotipoCarga(string id_tipo_carga, string descripcion)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_Motivos_TIPO_CARGA_X_CRITERIO]");
            accesoDatos.AgregarSqlParametro("@id_tipo_carga", id_tipo_carga);
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);

            return accesoDatos.EjecutarSqlquery2();
        }



        internal DataTable obtenerZonasDescarga(int ID_SITE)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_ZONAS_DESCARGA]");

            accesoDatos.AgregarSqlParametro("@ID_SITE", ID_SITE);
            return accesoDatos.EjecutarSqlquery2();
        }

        internal DataTable obtenerPlayas_X_Zona(int zona, string descripcion, int VIRTUAL)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("YMS_ConnectionString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_PLAYAS_X_CRITERIO]");
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
            accesoDatos.AgregarSqlParametro("@ZONA_ID", zona);
            accesoDatos.AgregarSqlParametro("@VIRTUAL", VIRTUAL);
            return accesoDatos.EjecutarSqlquery2();
        }


        internal DataTable obtenersites(int id_user)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_SITES_X_CRITERIO]");
            accesoDatos.AgregarSqlParametro("@id_usuario", id_user);
            return accesoDatos.EjecutarSqlquery2();
        }

        internal DataTable obtenersite(int id)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_SITE_X_ID]");
            accesoDatos.AgregarSqlParametro("@ID", id);
            return accesoDatos.EjecutarSqlquery2();
        }



        internal DataTable obtenerPlayas_X_SITe(int ID_SITE, string descripcion, int VIRTUAL)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_PLAYAS_X_CRITERIO_site]");
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
            accesoDatos.AgregarSqlParametro("@id_site", ID_SITE);
            accesoDatos.AgregarSqlParametro("@VIRTUAL", VIRTUAL);
            return accesoDatos.EjecutarSqlquery2();
        }


        internal DataTable obtenersubPlayas_X_SITe(int ID_SITE, string descripcion, int VIRTUAL)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_subPLAYAS_X_CRITERIO_site]");
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
            accesoDatos.AgregarSqlParametro("@id_site", ID_SITE);
            accesoDatos.AgregarSqlParametro("@VIRTUAL", VIRTUAL);
            return accesoDatos.EjecutarSqlquery2();
        }


        internal DataTable obtenerlugares_X_playa(int ID_PLAYA, string descripcion, string ocupado)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_LUGARES_X_CRITERIOXplaya]");
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
            accesoDatos.AgregarSqlParametro("@playa_ID", ID_PLAYA);
            accesoDatos.AgregarSqlParametro("@OCUPADO", ocupado);
            return accesoDatos.EjecutarSqlquery2();
        }
        internal DataTable obtenerlugares_X_Subplaya(int ID_PLAYA, int SUPL_ID, string descripcion, string ocupado)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_LUGARES_X_CRITERIOXSubplaya]");
            accesoDatos.AgregarSqlParametro("@DESCRIPCION", descripcion);
            accesoDatos.AgregarSqlParametro("@playa_ID", ID_PLAYA);
            accesoDatos.AgregarSqlParametro("@SUPL_ID", SUPL_ID);
            accesoDatos.AgregarSqlParametro("@OCUPADO", ocupado);
            return accesoDatos.EjecutarSqlquery2();
        }

        internal Boolean visibleasignartrailer(int id_site, int id_usuario)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[visible_asignar_trailer]");
            accesoDatos.AgregarSqlParametro("@id_site", id_site);
            accesoDatos.AgregarSqlParametro("@id_usuario", id_usuario);

            string resultado = accesoDatos.EjecutarSqlquery2().Rows[0][0].ToString();
            //   int resultado = 1;
            switch (resultado)
            {
                case "False": return false;
                case "True": return true;
                default:
                    throw new InvalidOperationException("Integer value is not valid");
            }

        }


        internal DataTable obtenermenu_lugar(int id_lugar)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[CARGA_menu_lugar]");
            accesoDatos.AgregarSqlParametro("@lugar_id", id_lugar);
            return accesoDatos.EjecutarSqlquery2();
        }


        internal DataTable guarda_playa(int id_playa, double x, double y, double ancho, double alto, int orientacion)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[guarda_playa]");
            accesoDatos.AgregarSqlParametro("@playa_id", id_playa);
            accesoDatos.AgregarSqlParametro("@x", x);
            accesoDatos.AgregarSqlParametro("@y", y);
            accesoDatos.AgregarSqlParametro("@ancho", ancho);
            accesoDatos.AgregarSqlParametro("@alto", alto);
            accesoDatos.AgregarSqlParametro("@orientacion", orientacion);
            return accesoDatos.EjecutarSqlquery2();
        }


        internal DataTable guarda_sub_playa(int id_playa, int supl_id, double x, double y, double ancho, double alto, int orientacion)
        {
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[guarda_sub_playa]");
            accesoDatos.AgregarSqlParametro("@playa_id", id_playa);
            accesoDatos.AgregarSqlParametro("@supl_id", supl_id);
            accesoDatos.AgregarSqlParametro("@x", x);
            accesoDatos.AgregarSqlParametro("@y", y);
            accesoDatos.AgregarSqlParametro("@ancho", ancho);
            accesoDatos.AgregarSqlParametro("@alto", alto);
            accesoDatos.AgregarSqlParametro("@orientacion", orientacion);
            return accesoDatos.EjecutarSqlquery2();
        }




        internal bool cuadra_yardtag(string YATA_COD, int YAHI_ID, int YAPE_ID, int LUGA_ID, int TRAI_ID, int usua_id, out string mensaje) //Solicitud Carga
        {
            bool exito = false;
            mensaje = "";
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            accesoDatos.CargarSqlComando("[dbo].[prcYARDTAG_EVENTOS_CUADRATURA_MANUAL_CUADRAR]");
            accesoDatos.AgregarSqlParametro("@YATA_COD", YATA_COD);
            accesoDatos.AgregarSqlParametro("@YAHI_ID", YAHI_ID);
            accesoDatos.AgregarSqlParametro("@YAPE_ID", YAPE_ID);
            accesoDatos.AgregarSqlParametro("@LUGA_ID", LUGA_ID);
            accesoDatos.AgregarSqlParametro("@TRAI_ID", TRAI_ID);
            accesoDatos.AgregarSqlParametro("@usua_id", usua_id);
            //accesoDatos.AgregarSqlParametro("@RUTA", d.SOLICITUD_CARGA.RUTA);
            //accesoDatos.AgregarSqlParametro("@OBSERVACION", d.SOLICITUD_CARGA.OBSERVACION);
            //accesoDatos.AgregarSqlParametro("@ANDENES", ds.Tables["ANDENES"]);
            //accesoDatos.AgregarSqlParametro("@LOCALES", ds.Tables["LOCALES"]);
            //accesoDatos.AgregarSqlParametro("@USUA_ID", d.USUA_ID_CARGA);
            //if (d.SOLICITUD_CARGA.ID_SHORTECK != "0" && d.SOLICITUD_CARGA.ID_SHORTECK != "")
            //    accesoDatos.AgregarSqlParametro("@SHOR_ID", d.SOLICITUD_CARGA.ID_SHORTECK);
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
                mensaje = ex.Message;
              //  throw (ex);
            }
            finally
            {
                accesoDatos.LimpiarSqlParametros();
                accesoDatos.CerrarSqlConeccion();
            }
            return exito;
        }


        internal DataTable yardtag_cuadra_ObtenerTodos(string TRAI_PLACA, int play_id, int zona_id, int site_id)
        {
            DataTable dt = new DataTable();
            SqlAccesoDatos accesoDatos = new SqlAccesoDatos("CsString");
            try
            {
                accesoDatos.CargarSqlComando("[dbo].[prcYARDTAG_EVENTOS_CUADRATURA_MANUAL_CONSULTAR]");

                accesoDatos.AgregarSqlParametro("@TRAI_PLACA", TRAI_PLACA);
                if (play_id != 0)
                    accesoDatos.AgregarSqlParametro("@PLAY_ID", play_id);
                if (zona_id != 0)
                    accesoDatos.AgregarSqlParametro("@ZONA_ID", zona_id);
                if (site_id != 0)
                    accesoDatos.AgregarSqlParametro("@SITE_ID", site_id);

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

    }
}
