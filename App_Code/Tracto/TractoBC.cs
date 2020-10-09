using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de TractoBC
/// </summary>
public class TractoBC : TractoTable
{
    SqlTransaccion tran = new SqlTransaccion();

    public DataTable ObtenerTodos()
    {
        return tran.Tracto_ObtenerTodos();
    }

    public DataTable ObtenerBloqueo()
    {
        return tran.Tracto_ObtenerBloqueo();
    }

    public TractoBC ObtenerXId(int ID)
    {
        return tran.Tracto_ObtenerXId(ID);
    }

    public TractoBC ObtenerXPatente(string placa)
    {
        return tran.Tracto_ObtenerXPlaca(placa);
    }

    public TractoBC ObtenerTractoXLugar()
    {
        return tran.Tracto_ObtenerTractoXLugar(this.LUGA_ID);
    }

    public TractoBC ObtenerTractoXLugar(int luga_id)
    {
        return tran.Tracto_ObtenerTractoXLugar(luga_id);
    }

    public bool Eliminar()
    {
        return tran.Tracto_Eliminar(this.ID);
    }

    public bool Eliminar(int id)
    {
        return tran.Tracto_Eliminar(id);
    }

    public bool Crear()
    {
        return tran.Tracto_Crear(this);
    }

    public bool Crear(TractoBC t)
    {
        return tran.Tracto_Crear(t);
    }

    public bool Modificar()
    {
        return tran.Tracto_Modificar(this);
    }

    public bool Modificar(TractoBC t)
    {
        return tran.Tracto_Modificar(t);
    }

    public bool Bloquear()
    {
        return tran.Tracto_Bloquear(this);
    }

    public bool Bloquear(TractoBC t)
    {
        return tran.Tracto_Bloquear(t);
    }

    public bool Salida(out string error)
    {
        return tran.Tracto_Salida(this, out error);
    }

    public bool Salida(TractoBC t, out string error)
    {
        return tran.Tracto_Salida(t, out error);
    }

    public DataTable obtener_Reporte_XParametro(string placa, bool site_in, int tran_id)
    {
        return tran.Tracto_Obtener_ReporteXParametro(placa, site_in, tran_id);
    }

}