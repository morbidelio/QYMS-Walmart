using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de ServiciosExternosVehiculosBC
/// </summary>
public class ServiciosExternosVehiculosBC : ServiciosExternosVehiculosTable
{
    SqlTransaccion tran = new SqlTransaccion();
	public ServiciosExternosVehiculosBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable ObtenerTodos()
    {
        return tran.ServiciosExternosVehiculos_ObtenerTodos();
    }

    public DataTable ObtenerXParametros(string codigo, string placa)
    {
        return tran.ServiciosExternosVehiculos_ObtenerXParametro(codigo, placa);
    }

    public ServiciosExternosVehiculosBC ObtenerXId()
    {
        return tran.ServiciosExternosVehiculos_ObtenerXId(this.SEVE_ID);
    }

    public ServiciosExternosVehiculosBC ObtenerXId(int id)
    {
        return tran.ServiciosExternosVehiculos_ObtenerXId(id);
    }

    public ServiciosExternosVehiculosBC ObtenerXPlaca()
    {
        return tran.ServiciosExternosVehiculos_ObtenerXPlaca(this.PLACA);
    }

    public ServiciosExternosVehiculosBC ObtenerXPlaca(string placa)
    {
        return tran.ServiciosExternosVehiculos_ObtenerXPlaca(placa);
    }

    public ServiciosExternosVehiculosBC ObtenerXPlaca(out bool existe)
    {
        return tran.ServiciosExternosVehiculos_ObtenerXPlaca(this.PLACA, out existe);
    }

    public ServiciosExternosVehiculosBC ObtenerXPlaca(string placa, out bool existe)
    {
        return tran.ServiciosExternosVehiculos_ObtenerXPlaca(placa, out existe);
    }

    public ServiciosExternosVehiculosBC ObtenerXCodigo()
    {
        return tran.ServiciosExternosVehiculos_ObtenerXCodigo(this.CODIGO);
    }

    public ServiciosExternosVehiculosBC ObtenerXCodigo(string codigo)
    {
        return tran.ServiciosExternosVehiculos_ObtenerXCodigo(codigo);
    }

    public bool Crear()
    {
        return tran.ServiciosExternosVehiculos_Crear(this);
    }

    public bool Crear(ServiciosExternosVehiculosBC s)
    {
        return tran.ServiciosExternosVehiculos_Crear(s);
    }

    public bool Modificar()
    {
        return tran.ServiciosExternosVehiculos_Modificar(this);
    }

    public bool Modificar(ServiciosExternosVehiculosBC s)
    {
        return tran.ServiciosExternosVehiculos_Modificar(s);
    }

    public bool Eliminar()
    {
        return tran.ServiciosExternosVehiculos_Eliminar(this.SEVE_ID);
    }

    public bool Eliminar(int id)
    {
        return tran.ServiciosExternosVehiculos_Eliminar(id);
    }

    public bool Entrada()
    {
        return tran.ServiciosExternosVehiculos_Entrada(this);
    }

    public bool Entrada(ServiciosExternosVehiculosBC se)
    {
        return tran.ServiciosExternosVehiculos_Entrada(se);
    }

    public bool Salida()
    {
        return tran.ServiciosExternosVehiculos_Salida(this);
    }

    public bool Salida(ServiciosExternosVehiculosBC se)
    {
        return tran.ServiciosExternosVehiculos_Salida(se);
    }
}