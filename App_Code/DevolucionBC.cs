using Qanalytics.Data.Access.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Devolucion
/// </summary>
public partial class DevolucionBC : DevolucionTable
{
    SqlTransaccion tran = new SqlTransaccion();
    public DevolucionBC()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public DataTable ObtenerTodo(int site_id = 0)
    {
        return tran.Devolucion_ObtenerTodo(site_id);
    }
    public DevolucionBC ObtenerXId()
    {
        return tran.Devolucion_ObtenerXId(this.DEVO_ID);
    }
    public DevolucionBC ObtenerXId(int devo_id)
    {
        return tran.Devolucion_ObtenerXId(devo_id);
    }
    public bool Reintentar()
    {
        return tran.Devolucion_Reintentar(this.DEVO_ID);
    }
    public bool Reintentar(int devo_id)
    {
        return tran.Devolucion_Reintentar(devo_id);
    }
    public bool Despachar(SolicitudBC s, DataSet ds, int devo_id, out string mensaje)
    {
        this.DEVO_ID = devo_id;
        this.SITE_ID = s.ID_SITE;
        this.TRAI_ID = s.ID_TRAILER_RESERVADO;
        this.USUA_ID_CARGA = s.ID_USUARIO;
        this.SOLICITUD_CARGA = s;
        return tran.Devolucion_Despacho(this, ds, out mensaje);
    }
    public bool Despachar(DevolucionBC d, DataSet ds, out string mensaje)
    {
        return tran.Devolucion_Despacho(d, ds, out mensaje);
    }
    public bool Despachar(DataSet ds, out string mensaje)
    {
        return tran.Devolucion_Despacho(this, ds, out mensaje);
    }
    public bool Trasvasije(SolicitudBC s, DevolucionBC d, int luga_id_descarga, DataSet ds, out string mensaje)
    {
        return tran.Devolucion_Trasvasije(s, d, luga_id_descarga, ds, out mensaje);
    }
    public bool Descargar(out string mensaje)
    {
        return tran.Devolucion_Descargar(this, out mensaje);
    }
    public bool Descargar(DevolucionBC d, out string mensaje)
    {
        return tran.Devolucion_Descargar(d, out mensaje);
    }
    public bool FinalizarDescarga(out string mensaje)
    {
        return tran.Devolucion_FinalizarDescarga(this, out mensaje);
    }
    public bool FinalizarDescarga(DevolucionBC d , out string mensaje)
    {
        return tran.Devolucion_FinalizarDescarga(d, out mensaje);
    }

    public bool FinalizarDevolucion(out string mensaje)
    {
        return tran.Devolucion_FinalizarDescarga(this, out mensaje);
    }

}
public partial class DevolucionTable
{
    public DevolucionTable()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public int DEVO_ID { get; set; }
    public int SITE_ID { get; set; }
    public int TRAI_ID { get; set; }
    public int DEES_ID { get; set; }
    public DateTime DEVO_FH { get; set; }
    public string DEVO_OBS { get; set; }
    public Int64 TRUE_COD_INTERNO_IN_SALIDA { get; set; }
    public int SOLI_ID_DEVOLUCION { get; set; }
    public int SOLI_ID_DESCARGA { get; set; }
    public int USUA_ID_DESCARGA { get; set; }
    public DateTime DEVO_FH_DESCARGA { get; set; }
    public int SOLI_ID_CARGA { get; set; }
    public int USUA_ID_CARGA { get; set; }
    public DateTime DEVO_FH_CARGA { get; set; }
    public DateTime DEVO_FH_CIERRE { get; set; }
    public int USUA_ID_ACTUALIZA { get; set; }
    public DateTime DEVO_FH_ACTUALIZA { get; set; }
    public SolicitudBC SOLICITUD_DEVOLUCION { get; set; }
    public SolicitudBC SOLICITUD_DESCARGA {get; set;}
    public SolicitudBC SOLICITUD_CARGA {get; set;}

}