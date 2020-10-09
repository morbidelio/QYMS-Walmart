using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de TransportistaBC
/// </summary>
public class TransportistaBC : TransportistaTable
{
    public TransportistaBC()
    {
    }

    public DataTable ObtenerTodos()
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Transportista_ObtenerTodos();
    }

    public DataTable ObtenerMotivoBloqueo(string user)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.ObtenerMotivoBloqueo(user);
    }

    public TransportistaBC DatosPreIngreso(int ID )
    {
        SqlAccesoDatos accesoDatos = new SqlAccesoDatos(SqlTransaccion.STRING_CONEXION);
        TransportistaBC transportista = new TransportistaBC();
        try
        {
            accesoDatos.LimpiarSqlParametros(); accesoDatos.CargarSqlComando("[dbo].[PRC_LISTAR_DATOS_PRE_INGRESOS_YMS]");
            accesoDatos.AgregarSqlParametro("@ID", ID);

            accesoDatos.EjecutarSqlLector();
            while (accesoDatos.SqlLectorDatos.Read())
            {
                transportista = cargaDatosPreIngreso(accesoDatos.SqlLectorDatos);
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
    public TransportistaBC ObtenerTodoXRut(string rut)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Transportista_ObtenerXRut(rut);
    }

    public TransportistaBC ObtenerXId(int id)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Transportista_ObtenerXId(id);
    }

    public DataTable ObtenerXParametro(String rut, String nombre)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Transportista_ObtenerXParametro(rut, nombre);
    }

    public bool Crear()
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Transportista_Crear(this);
    }

    public bool Crear(TransportistaBC transportista)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Transportista_Crear(transportista);
    }

    public bool Crear(TransportistaBC transportista, out int id)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Transportista_Crear(transportista, out id);
    }

    public bool Crear(out int id)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Transportista_Crear(this, out id);
    }

    public bool Modificar(TransportistaBC transportista)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Transportista_Modificar(transportista);
    }

    public bool Eliminar(int id)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Transportista_Eliminar(id);
    }

    private TransportistaBC cargaDatosPreIngreso(SqlDataReader reader)
    {
        TransportistaBC transportista = new TransportistaBC();
        transportista.ESTADO = reader["ESTADO"].ToString();
        return transportista;
    }

}