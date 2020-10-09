using System;
using System.Collections.Generic;

using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de VehiculoBC
/// </summary>
public class PaginaBC : PAGINATable
{
    private string _tipo;
    public string TIPO
    {
        get { return this._tipo; }
        set { _tipo = value;} 
    }
    public PaginaBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

    public DataTable obtenerEncabezadosMenu() {
        SqlTransactionPagina tran = new SqlTransactionPagina();
        return tran.obtenerEncabezadosMenu();
    }

    public DataTable obtenerPaginasMenu(int OrdenEncabezado)
    {
        SqlTransactionPagina tran = new SqlTransactionPagina();
        return tran.obtenerPaginasMenu(OrdenEncabezado);
    }

    public DataTable obtenerMenu(int idPerfil)
    {
        SqlTransactionPagina tran = new SqlTransactionPagina();
        return tran.obtenerMenu(idPerfil);
    }

    public DataTable obtenerTodas()
    {
        SqlTransactionPagina tran = new SqlTransactionPagina();
        return tran.obtenerTodas();
    }
    
}