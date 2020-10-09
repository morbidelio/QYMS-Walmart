using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de PreEntradaBC
/// </summary>
public class PreEntradaBC : PreEntradaTable
{
    SqlTransaccionMovimiento tran = new SqlTransaccionMovimiento();
	public PreEntradaBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    public bool Crear(PreEntradaBC p, out int id, out string error)
    {
        return tran.PreEntrada_Crear(p,out id, out error);
    }
    public bool Crear(out int id, out string error)
    {
        return tran.PreEntrada_Crear(this,out id, out error);
    }
    public bool CrearV2(PreEntradaBC p, out int id, out string error)
    {
        return tran.PreEntrada_CrearV2(p,out id, out error);
    }
    public bool CrearV2(out int id, out string error)
    {
        return tran.PreEntrada_CrearV2(this,out id, out error);
    }
    public PreEntradaBC CargarPreEntrada(int trai_id, int site_id, string doc=null)
    {
        return tran.PreEntrada_Carga(trai_id, site_id, doc);
    }
    public DataTable CargarReporte(DateTime desde, DateTime hasta, int site_id, int prov_id, string numero)
    {
        return tran.PreEntrada_Reporte(desde, hasta, site_id, prov_id,numero);
    }
    public DataTable CargarCitas(string num_cita = "", int site = 0, int prov_id = 0)
    {
        return tran.PreEntrada_CargarCitas(num_cita, site, prov_id);
    }
    public  bool ObtenerXDoc(string doc, string id_site)
    {
        return tran.preentrada_ObtenerXDoc(doc, id_site);
    }
}