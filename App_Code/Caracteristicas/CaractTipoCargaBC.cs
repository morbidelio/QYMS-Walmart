using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de CaractTipoCargaBC
/// </summary>
public class CaractTipoCargaBC : CaractTipoCargaTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();

    public CaractTipoCargaBC()
    {
    }
    public DataTable obtenerTodoCaractTipoCarga()
    {
        return tran.CaractTipoCarga_ObtenerTodos();
    }
    public CaractTipoCargaBC obtenerXID()
    {
        return tran.CaractTipoCarga_ObtenerXId(this.ID);
    }
    public CaractTipoCargaBC obtenerXID(int id)
    {
        return tran.CaractTipoCarga_ObtenerXId(id);
    }
    public DataTable obtenerXParametro(string descripcion)
    {
        return tran.CaractTipoCarga_ObtenerXParametro(descripcion);
    }
    public bool Crear()
    {
        return tran.CaractTipoCarga_Crear(this);
    }
    public bool Crear(CaractTipoCargaBC cct)
    {
        return tran.CaractTipoCarga_Crear(cct);
    }
    public bool Modificar()
    {
        return tran.CaractTipoCarga_Modificar(this);
    }
    public bool Modificar(CaractTipoCargaBC cct)
    {
        return tran.CaractTipoCarga_Modificar(cct);
    }
    public bool Eliminar()
    {
        return tran.CaractTipoCarga_Eliminar(this.ID);
    }
    public bool Eliminar(int id)
    {
        return tran.CaractTipoCarga_Eliminar(id);
    }
}