using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de ProveedorBC
/// </summary>
public class ProveedorBC : ProveedorTable
{
    SqlTransaccion tran = new SqlTransaccion();
	public ProveedorBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    public DataTable obtenerTodo()
    {
        return tran.Proveedor_ObtenerTodos();
    }
    public DataTable obtenerLocal()
    {
        return tran.Proveedor_ObtenerXLocales("", "");
    }
    public DataTable ObtenerXParametros(string codigo, string descripcion)
    {
        return tran.Proveedor_ObtenerXParametros(codigo, descripcion);
    }
    public ProveedorBC ObtenerXId(int id)
    {
        return tran.Proveedor_ObtenerXId(id);
    }
    public ProveedorBC ObtenerXId()
    {
        return tran.Proveedor_ObtenerXId(this.PROV_ID);
    }
    public bool Crear()
    {
        return tran.Proveedor_Crear(this);
    }
    public bool Crear(ProveedorBC prov)
    {
        return tran.Proveedor_Crear(prov);
    }
    public bool Modificar()
    {
        return tran.Proveedor_Modificar(this);
    }
    public bool Modificar(ProveedorBC prov)
    {
        return tran.Proveedor_Modificar(prov);
    }
    public bool Eliminar()
    {
        return tran.Proveedor_Eliminar(this.PROV_ID);
    }
    public bool Eliminar(int id)
    {
        return tran.Proveedor_Eliminar(id);
    }
    public ProveedorBC ComprobarCodigoExistente()
    {
        return tran.Proveedor_CodigoExiste(this.PROV_ID, this.CODIGO, this.RUT);
    }
    public ProveedorBC ComprobarCodigoExistente(int id, string codigo, string rut)
    {
        return tran.Proveedor_CodigoExiste(id, codigo, rut);
    }
    public bool EliminarPreIngreso(int id)
    {
        return tran.EliminarPreIngreso(id);
    }
    public DataTable obtenerVendorXParametros(int prov_id = 0, string prve_numero = null)
    {
        return tran.Proveedor_ObtenerVendorXParametros(prov_id, prve_numero);
    }
    public bool ComprobarNroVendorExistente(int prve_numero)
    {
        return tran.Proveedor_ComprobarNroVendor(prve_numero);
    }
    public bool AgregarVendor(int prov_id, int prve_numero)
    {
        return tran.Proveedor_AgregarVendor(prov_id, prve_numero);
    }
    public bool EliminarVendor(int prve_id)
    {
        return tran.Proveedor_EliminarVendor(prve_id);
    }
    public DataTable ObtenerReservaPreingresoXParametros(int site_id, DateTime desde, DateTime hasta, int proveedor_id, string numcita, int tipocarga_id, bool preingreso)
    {
        return tran.PreIngresoReserva_ObtenerXParametros(site_id, desde, hasta, proveedor_id, numcita, tipocarga_id, preingreso);
    }
    public DataRow ObtenerReservaPreingresoXNumero(int numcita)
    {
        return tran.PreIngresoReserva_ObtenerXNumero(numcita);
    }
    public bool EditarPreIngresoReserva(int prov_id, int site_id, int prve_id, int num_cita, DateTime fecha, int tiic_id)
    {
        return tran.PreIngresoReserva_Editar(prov_id, site_id, prve_id, num_cita, fecha, tiic_id);
    }
}