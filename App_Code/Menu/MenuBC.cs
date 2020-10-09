using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de MenuBC
/// </summary>
public class MenuBC
{
    SqlTransactionUsuario tran = new SqlTransactionUsuario();
    public DataTable ObtenerTodo(bool mobile = false)
    {
        if (!mobile)
            return tran.Menu_ObtenerTodo();
        else
            return tran.MenuMobile_ObtenerTodo();
    }
}