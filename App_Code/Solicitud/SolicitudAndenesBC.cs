using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de SolicitudAndenes
/// </summary>
public class SolicitudAndenesBC : SolicitudAndenesTable
{
    readonly SqlTransaccionSolicitud tran = new SqlTransaccionSolicitud();

    public bool CompletarCarga(SolicitudAndenesBC anden, int id_usuario, out string resultado)
    {
        return tran.SolicitudAnden_CompletarCarga(anden, id_usuario, out resultado);
    }

    public bool CancelarCarga(SolicitudAndenesBC anden, int id_usuario, out string resultado)
    {
        return tran.SolicitudAnden_CancelarCarga(anden, id_usuario, out resultado);
    }
    public bool InterrumpirCarga(SolicitudAndenesBC anden, int id_usuario, out string resultado)
    {
        return tran.SolicitudAnden_InterrumpirCarga(anden, id_usuario, out resultado);
    }

    public bool ReanudarCarga(DataSet ds, int id_usuario, out string resultado)
    {
        return tran.SolicitudAnden_ReanudarCarga(ds, id_usuario, out resultado);
    }

    public bool ReanudarCarga(int soli_id, int id_usuario, out string resultado)
    {
        return tran.SolicitudAnden_ReanudarCarga(soli_id, id_usuario, out resultado);
    }

    public bool AgregarAnden()
    {
        return tran.SolicitudAnden_Agregar(this);
    }

    public bool AgregarAnden(SolicitudAndenesBC anden)
    {
        return tran.SolicitudAnden_Agregar(anden);
    }

    public bool AgregarAnden(SolicitudAndenesBC anden, out int soan_orden)
    {
        return tran.SolicitudAnden_Agregar(anden, out soan_orden);
    }

    public bool Bloquear(SolicitudAndenesBC s, int usua_id, out string resultado)
    {
        return tran.SolicitudAnden_Bloquear(s, usua_id, out resultado);
    }

    public bool Bloquear(int usua_id, out string resultado)
    {
        return tran.SolicitudAnden_Bloquear(this, usua_id, out resultado);
    }

    public bool Liberar(int soli_id, int luga_id, int usua_id, out string resultado)
    {
        return tran.SolicitudAnden_Liberar(soli_id, luga_id, usua_id, out resultado);
    }

    public bool Liberar(int usua_id, out string resultado)
    {
        return tran.SolicitudAnden_Liberar(this.SOLI_ID, this.LUGA_ID, usua_id, out resultado);
    }

    public SolicitudAndenesBC ObtenerXId()
    {
        return tran.SolicitudAnden_ObtenerXId(this.SOLI_ID, this.SOAN_ORDEN);
    }

    public SolicitudAndenesBC ObtenerXId(int soli_id, int soan_orden)
    {
        return tran.SolicitudAnden_ObtenerXId(soli_id, soan_orden);
    }

    public int ObtenerPlayaId()
    {
        return tran.SolicitudAnden_ObtenerPlayaId(this.SOLI_ID);
    }

    public int ObtenerPlayaId(int soli_id)
    {
        return tran.SolicitudAnden_ObtenerPlayaId(soli_id);
    }

    public DataTable ObtenerBloqueados()
    {
        return tran.SolicitudAnden_ObtenerBloqueados(this.SOLI_ID);
    }

    public DataTable ObtenerBloqueados(int soli_id)
    {
        return tran.SolicitudAnden_ObtenerBloqueados(soli_id);
    }

    public int ObtenerUltimoOrden()
    {
        return tran.SolicitudAnden_ObtenerUltimoOrden(this.SOLI_ID);
    }

    public int ObtenerUltimoOrden(int soli_id)
    {
        return tran.SolicitudAnden_ObtenerUltimoOrden(soli_id);
    }

    public DataSet ObtenerTodo(int soli_id = 0, int soan_orden = 0, int loca_id = 0)
    {
        return tran.SolicitudAnden_ObtenerTodo(soli_id, soan_orden, loca_id);
    }
    public DataSet ObtenerFinalizados(int soli_id = 0, int soan_orden = 0, int loca_id = 0)
    {
        return tran.SolicitudAnden_ObtenerFinalizados(soli_id, soan_orden, loca_id);
    }

    public bool SelloValidar(int usua_id, out string resultado)
    {
        return tran.SolicitudAnden_colocar_sello(this.SOLI_ID, usua_id, out resultado);
    }

    public bool SelloValidado(int usua_id, out string resultado)
    {
        return tran.SolicitudAnden_validado_sello(this.SOLI_ID, usua_id, out resultado);
    }

    public bool Locales(DataTable dt)
    {
        return tran.SolicitudAnden_Locales(dt);
    }

    public bool Emergencia(DataTable dt, DataTable dt2,  int id_usuario, out string mensaje)
    {
        return tran.SolicitudAnden_Emergencia(dt, dt2, id_usuario, out mensaje);
    }

}