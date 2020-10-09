using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de CaractCargaBC
/// </summary>
public class CaractCargaBC : CaractCargaTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();
	public CaractCargaBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    public DataTable obtenerTodoYTipos()
    {
        return tran.CaractCarga_todoYTipos();
    }
    public DataTable obtenerTodo()
    {
        return tran.CaractCarga_ObtenerTodo(null, null, 0);
    }
    public CaractCargaBC obtenerSeleccionado()
    {
        return tran.CaractCarga_ObtenerSeleccionado(this.ID, null);
    }
    public CaractCargaBC obtenerSeleccionado(int id)
    {
        return tran.CaractCarga_ObtenerSeleccionado(id, null);
    }
    public CaractCargaBC obtenerSeleccionado(int id, string codigo)
    {
        return tran.CaractCarga_ObtenerSeleccionado(id, codigo);
    }
    public DataTable obtenerXParametro(string descripcion, string codigo, int cact_id)
    {
        return tran.CaractCarga_ObtenerTodo(descripcion, codigo, cact_id);
    }
    public DataTable caracteristicasdesdelocales(string locales, string seleccionados)
    {
        return tran.CaractCarga_desdelocales(locales, seleccionados);
    }
    public DataTable obtenerCompatibles(string locales, string seleccionados)
    {
        return tran.CaractCarga_ObtenerCompatibles(locales, seleccionados);
    }
    public DataTable obtenertrailersCompatibles(string locales, int cantidad_pallets,string seleccionados,int id_site)
    {
        return tran.obtenertrailersCompatibles(locales, seleccionados, cantidad_pallets, id_site);
    }
    public DataTable obtenerplayasCompatibles(string locales, string seleccionados, int site_id)
    {
        return tran.obtenerplayasCompatibles(locales, seleccionados, site_id, "");
    }
    public DataTable obtenerXLocal(int id)
    {
        return tran.CaractCarga_ObtenerXLocalId(id);
    }
    public DataTable obtenerXTipo(int id_tipo)
    {
        return tran.CaractCarga_ObtenerTodo(null, null, id_tipo);
    }
    public bool Crear(CaractCargaBC trailer)
    {
        return tran.CaractCarga_Crear(trailer);
    }
    public bool Modificar(CaractCargaBC trailer)
    {
        return tran.CaractCarga_Modificar(trailer);
    }
    public bool Eliminar(int id)
    {
        return tran.CaractCarga_Eliminar(id);
    }
}