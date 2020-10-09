using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de RemoProgDistribuicionBC
/// </summary>
public class RemoProgDistribuicionBC : RemoProgDistribuicionTable
{
    SqlTransaccion tran = new SqlTransaccion();

    public DataTable ObtenerTodo(int site_id)
    {
        return tran.RemoProgDistribuicion_CargaTodo(site_id);
    }

    public DataTable ObtenerPlayas()
    {
        return tran.RemoProgDistribuicionPlayas_CargaTodo();
    }

    public DataTable ObtenerPlayas(int repd_id)
    {
        return tran.RemoProgDistribuicionPlayas_CargaTodo(repd_id);
    }

    public RemoProgDistribuicionBC ObtenerXId(int id)
    {
        return tran.RemoProgDistribuicion_CargaXId(id);
    }

    public bool Crear(RemoProgDistribuicionBC r)
    {
        return tran.RemoProgDistribuicion_Crear(r);
    }

    public bool Modificar(RemoProgDistribuicionBC r)
    {
        return tran.RemoProgDistribuicion_Modificar(r);
    }

    public bool Asignar(DataTable dt, int id)
    {
        return tran.RemoProgDistribuicion_Asignar(dt, id);
    }

    public bool ActivarDesactivar(int repd_id)
    {
        return tran.RemoProgDistribuicion_ActivarDesactivar(repd_id);
    }
}