using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de EmpresaBC
/// </summary>
public class EmpresaBC : EmpresaTable
{
	public EmpresaBC()
	{
	}

    public DataTable ObtenerTodas()
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Empresa_ObtenerTodas();
    }

    public EmpresaBC ObtenerTodoXRut(string rut)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Empresa_ObtenerEmpresaXRut(rut);
    }

    public EmpresaBC ObtenerTodoXId(int id)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Empresa_ObtenerEmpresaXId(id);
    }

    public DataTable ObtenerXParametro(String rut, String razon_social, String nombre_fantasia)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Empresa_ObtenerXParametro(rut, razon_social, nombre_fantasia);
    }

    public bool Crear(EmpresaBC empresa)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Empresa_Crear(empresa);
    }

    public bool Modificar(EmpresaBC empresa)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Empresa_Modificar(empresa);
    }

    public bool Eliminar(int id)
    {
        SqlTransaccion tran = new SqlTransaccion();
        return tran.Empresa_Eliminar(id);
    }
}