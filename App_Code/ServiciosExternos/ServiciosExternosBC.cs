using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de ServiciosExternosBC
/// </summary>
public class ServiciosExternosBC : ServiciosExternosTable
{
    SqlTransaccion tran = new SqlTransaccion();
	public ServiciosExternosBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
    }
    public DataTable ObtenerTodos()
    {
        return tran.ServiciosExternos_ObtenerTodos();
    }

    public ServiciosExternosBC ObtenerXId()
    {
        return tran.ServiciosExternos_ObtenerXId(this.SEEX_ID);
    }

    public ServiciosExternosBC ObtenerXId(int id)
    {
        return tran.ServiciosExternos_ObtenerXId(id);
    }

    public ServiciosExternosBC ObtenerXCodigo()
    {
        return tran.ServiciosExternos_ObtenerXCodigo(this.CODIGO);
    }

    public ServiciosExternosBC ObtenerXCodigo(string codigo)
    {
        return tran.ServiciosExternos_ObtenerXCodigo(codigo);
    }

    public DataTable ObtenerXParametros(string codigo)
    {
        return tran.ServiciosExternos_ObtenerXParametros(codigo);
    }

    public bool Crear()
    {
        return tran.ServiciosExternos_Crear(this);
    }

    public bool Crear(ServiciosExternosBC s)
    {
        return tran.ServiciosExternos_Crear(s);
    }

    public bool Modificar()
    {
        return tran.ServiciosExternos_Modificar(this);
    }

    public bool Modificar(ServiciosExternosBC s)
    {
        return tran.ServiciosExternos_Modificar(s);
    }

    public bool Eliminar()
    {
        return tran.ServiciosExternos_Eliminar(this.SEEX_ID);
    }

    public bool Eliminar(int id)
    {
        return tran.ServiciosExternos_Eliminar(id);
    }

    public DataTable obtener_Reporte_XParametro(string placa, string site_in, int tran_id, int site, DateTime desde, DateTime hasta)
    {
        return tran.servicios_Obtener_ReporteXParametro(placa, site_in, tran_id, site, desde, hasta);
    }
}