// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Linq;
using Qanalytics.Data.Access.SqlClient;

/// <summary>
/// Descripción breve de TrailerBC
/// </summary>
public class TrailerLogiBC : TrailerLogiTable
{
    readonly SqlTransaccionSolicitud tran = new SqlTransaccionSolicitud();

    #region Logistica
    public TrailerLogiBC()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataTable obtenerXParametro(string placa, string numero, bool externo, int tipo_id, int tran_id, int site_id = 0)
    {
        return this.tran.TrailerLogi_ObtenerXParametro(placa, numero, externo, tipo_id, tran_id, site_id);
    }

    public DataTable obtenerXParametroDesc(string placa, string numero, bool externo, int tipo_id, int tran_id, int site_id = 0,  int play_id= 0 )
    {
        return this.tran.TrailerLogi_ObtenerXParametroDesc(placa, numero, externo, tipo_id, tran_id, site_id, play_id);
    }

    public DataTable obtenerXParametroAdmin(string placa, string numero, bool externo, int tipo_id, int tran_id, int site_id = 0)
    {
        return this.tran.TrailerLogi_ObtenerXParametroAdmin(placa, numero, externo, tipo_id, tran_id, site_id);
    }

    public DataTable obtenerXParametroDevolucion(string placa, string numero, bool externo, int tipo_id, int tran_id, int site_id = 0)
    {
        return this.tran.TrailerLogi_ObtenerXParametroDevolucion(placa, numero, externo, tipo_id, tran_id, site_id);
    }


    #endregion 

    #region Pallets

    public bool Pallets_Crear(SolicitudBC s, int luga_id_des, out string error)
    {
        return this.tran.Pallets_Crear(s, luga_id_des, out error);
    }

    public bool Pallets_TrasladoEst(int id, int luga_id, int luga_id_dest, int soan_orden, int usua_id, out string error)
    {
        return this.tran.Pallets_TrasladoEst(id, luga_id, luga_id_dest, soan_orden, usua_id, out error);
    }

    public bool Pallets_Reiniciar(int id, int luga_id, int luga_id_dest, int usua_id, int soan_orden, int soan_orden_des, out string error)
    {
        return this.tran.Pallets_Reiniciar(id, luga_id, luga_id_dest, usua_id, soan_orden, soan_orden_des, out error);
    }

    public bool PALLETS_Completar(int soli_id, int luga_id, int soan_orden, int usua_id, out string error)
    {
        error = "";
        return this.tran.pallets_Completar(soli_id, luga_id, soan_orden, usua_id, out error);
    }

    #endregion

    #region Desechos

    public bool Desechos_Crear(SolicitudBC s, int luga_id_des, out string error)
    {
        return this.tran.Desechos_Crear(s, luga_id_des, out error);
    }

    public bool Desechos_TrasladoEst(int id, int luga_id, int luga_id_dest, int soan_orden, int usua_id, out string error)
    {
        return this.tran.Desechos_TrasladoEst(id, luga_id, luga_id_dest, soan_orden, usua_id, out error);
    }

    public bool Desechos_Reiniciar(int id, int luga_id, int luga_id_dest, int usua_id, int soan_orden,int soan_orden_des, out string error)
    {
        return this.tran.Desechos_Reiniciar(id, luga_id, luga_id_dest, usua_id, soan_orden, soan_orden_des, out error);
    }

    public bool Desechos_Completar(int soli_id, int luga_id, int soan_orden, int usua_id, out string error)
    {
        error = "";
        return this.tran.Desechos_Completar(soli_id, luga_id, soan_orden, usua_id,   out error);
    }

    #endregion

    #region DescargaLI

    public bool DescargaLI_Crear(SolicitudBC s, int luga_id_des, out string error)
    {
        return this.tran.DescargaLI_Crear(s, luga_id_des, out error);
    }

    public bool DescargaLI_Completar(int soli_id, int luga_id, int soan_orden, int usua_id, out string error)
    {
        error = "";
        return this.tran.DescargaLI_Completar(soli_id, luga_id, soan_orden, usua_id, out error);
    }

    #endregion
}