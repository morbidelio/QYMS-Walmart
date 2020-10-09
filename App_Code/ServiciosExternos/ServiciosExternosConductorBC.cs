using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de ConductorBC
/// </summary>
public class ServiciosExternosConductorBC : ServiciosExternosConductorTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();

    public ServiciosExternosConductorBC()
    {

    }

    public ServiciosExternosConductorBC(int id)
    {
        this.ID = id;
        ServiciosExternosConductorBC c = ObtenerXId();
        this.RUT = c.RUT;
        this.NOMBRE = c.NOMBRE;
        this.IMAGEN = c.IMAGEN;
        this.BLOQUEADO = c.BLOQUEADO;
        this.ACTIVO = c.ACTIVO;
    }

    public ServiciosExternosConductorBC(string rut)
    {
        this.RUT = rut;
        ServiciosExternosConductorBC c = ObtenerXRut();
        this.ID = c.ID;
        this.NOMBRE = c.NOMBRE;
        this.IMAGEN = c.IMAGEN;
        this.BLOQUEADO = c.BLOQUEADO;
        this.ACTIVO = c.ACTIVO;
    }
    public DataTable ObtenerTodo()
    {
        return tran.ServiciosExternosConductor_ObtenerTodos();
    }
    public DataTable ObtenerXParametros(string rut, string nombre)
    {
        return tran.ServiciosExternosConductor_ObtenerXParametros(rut, nombre);
    }
    public ServiciosExternosConductorBC ObtenerXId(int id)
    {
        return tran.ServiciosExternosConductor_ObtenerXId(id);
    }
    public ServiciosExternosConductorBC ObtenerXId()
    {
        return tran.ServiciosExternosConductor_ObtenerXId(this.ID);
    }
    public ServiciosExternosConductorBC ObtenerXRut(string rut)
    {
        return tran.ServiciosExternosConductor_ObtenerXRut(rut);
    }
    public ServiciosExternosConductorBC ObtenerXRut()
    {
        return tran.ServiciosExternosConductor_ObtenerXRut(this.RUT);
    }
    public bool Agregar(ServiciosExternosConductorBC c)
    {
        return tran.ServiciosExternosConductor_Agregar(c);
    }
    public bool Agregar()
    {
        return tran.ServiciosExternosConductor_Agregar(this);
    }
    public bool Modificar(ServiciosExternosConductorBC c)
    {
        return tran.ServiciosExternosConductor_Modificar(c);
    }
    public bool Modificar()
    {
        return tran.ServiciosExternosConductor_Modificar(this);
    }
    public bool Eliminar()
    {
        return tran.ServiciosExternosConductor_Eliminar(this.ID);
    }
    public bool Eliminar(int id)
    {
        return tran.ServiciosExternosConductor_Eliminar(id);
    }
}