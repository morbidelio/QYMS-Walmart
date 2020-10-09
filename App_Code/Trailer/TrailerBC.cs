using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qanalytics.Data.Access.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de TrailerBC
/// </summary>
public class TrailerBC : TrailerTable
{
    readonly SqlTransaccion tran = new SqlTransaccion();
	public TrailerBC()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
	public TrailerBC(int id)
	{
        this.ID = id;
        TrailerBC t = this.obtenerXID();
        this.PLACA = t.PLACA;
        this.CODIGO = t.CODIGO;
        this.EXTERNO = t.EXTERNO;
        this.NUMERO = t.NUMERO;
        this.TRAN_ID = t.TRAN_ID;
        this.TRANSPORTISTA = t.TRANSPORTISTA;
        this.CARACTERISTICAS = t.CARACTERISTICAS;
        this.EXCLUYENTES = t.EXCLUYENTES;
        this.NO_EXCLUYENTES = t.NO_EXCLUYENTES;
        this.TRTI_ID = t.TRTI_ID;
        this.TIPO = t.TIPO;
        this.BLOQUEADO = t.BLOQUEADO;
        this.SITE_ID = t.SITE_ID;
        this.SITE_IN = t.SITE_IN;
        this.LUGAR_ID = t.LUGAR_ID;
        this.LUGAR = t.LUGAR;
        this.SOCA_ID = t.SOCA_ID;
        this.SOLI_ID = t.SOLI_ID;
        this.CARGADO = t.CARGADO;
        this.PROV_ID = t.PROV_ID;
        this.MOVI_ID = t.MOVI_ID;
        this.MOES_ID = t.MOES_ID;
        this.FECHA_INGRESO = t.FECHA_INGRESO;
        this.FECHA_RETIRO = t.FECHA_RETIRO;
        this.DOC_INGRESO = t.DOC_INGRESO;
        this.SELLO_INGRESO = t.SELLO_INGRESO;
        this.SELLO_CARGA = t.SELLO_CARGA;
        this.CHOFER_RUT = t.CHOFER_RUT;
        this.CHOFER_NOMBRE = t.CHOFER_NOMBRE;
        this.ACOMP_RUT = t.ACOMP_RUT;
        this.SOLI_ID = t.SOLI_ID;
        this.SOES_ID = t.SOES_ID;
        this.OBSERVACION = t.OBSERVACION;
        this.PATENTE_TRACTO = t.PATENTE_TRACTO;
        this.TRES_ID = t.TRES_ID;
        this.USUA_ID = t.USUA_ID;
        this.TCAR_ID = t.TCAR_ID;
        this.TIIC_ID = t.TIIC_ID;
        this.MOIC_ID = t.MOIC_ID;
        this.PRING_ID = t.PRING_ID;
        this.DEST_ID = t.DEST_ID;
        this.LOCA_ID = t.LOCA_ID;
        this.COND_ID = t.COND_ID;
        this.REQ_SELLO = t.REQ_SELLO;
	}
    public DataSet CargaGrafico()
    {
        return tran.Trailer_CargaGrafico();
    }
    public DataTable obtenerTodoTrailer()
    {
        return tran.Trailer_ObtenerTodos();
    }
    public DataTable obtenerXSite(int site_id)
    {
        return tran.Trailer_ObtenerXSite(site_id);
    }
    public DataSet obtenerDatosSalida(string patente = null,string flota = null)
    {
        return tran.Trailer_ObtenerDatosSalida(patente,flota);
    }
    public TrailerBC obtenerXID(int id)
    {
        return tran.Trailer_ObtenerXId(id);
    }
    public TrailerBC obtenerXviaje(string viaje)
    {
        return tran.Trailer_ObtenerXViaje(viaje);
    }
    public DataTable ObtenerSalidaXViaje(string viaje)
    {
        return tran.Salida_ObtenerXViaje(viaje);
    }
    public TrailerBC obtenerXID()
    {
        return tran.Trailer_ObtenerXId(this.ID);
    }
    public DataTable obtenerXParametroSTOCK(int site, int estado, bool asignado, bool bloquado, bool plancha, int shortec, int capacidad)
    {
        return tran.Trailer_ObtenerXParametroSTOCK(site, estado, asignado, bloquado, plancha, shortec, capacidad);
    }
    public DataTable obtenerXParametro(string placa = null, string numero = null, bool externo = false, int tipo_id = 0, int tran_id = 0, int site_id = 0, int zona_id = 0, int play_id = 0)
    {
        return tran.Trailer_ObtenerXParametro(placa, numero, externo, tipo_id, tran_id, site_id, zona_id, play_id);
    }
    public DataTable obtenerXParametro(TrailerBC t, int zona_id, int play_id)
    {
        return tran.Trailer_ObtenerXParametro(t.PLACA, t.NUMERO, t.EXTERNO, t.TRTI_ID, t.TRAN_ID, t.SITE_ID, zona_id, play_id);
    }
    public DataTable obtener_Reporte_XParametro(string placa, string numero, bool externo, int tipo_id, int tran_id, int site_id)
    {
        return tran.Trailer_Obtener_ReporteXParametro(placa, numero, externo, tipo_id, tran_id,  site_id);
    }
    public DataTable obtenerXParametro(string placa,string externo, string sites = null)
    {
        return tran.Trailer_ObtenerXParametroOld(placa,externo, sites);
    }
    public DataTable obtenerDisponiblesDrop(int site_id, bool externo, bool cargado)
    {
        return tran.Trailer_ObtenerDisponiblesDrop(site_id, externo, cargado);
    }
    public DataTable obtenerXParametroBloqueo(string placa, string numero, bool externo, int tipo_id, int tran_id, int site_id, int mantenimiento)
    {
        return tran.Trailer_ObtenerXParametrobloqueo(placa, numero, externo, tipo_id, mantenimiento, tran_id, site_id);
    }
    public DataTable obtenerXParametrotaller(string placa, string numero, bool externo, int tipo_id, int tran_id, int site_id, int mantenimiento, int tipo_bloqueo)
    {
        return tran.Trailer_ObtenerXParametrotaller(placa, numero, externo, tipo_id, mantenimiento, tran_id, site_id, tipo_bloqueo);
    }
    public TrailerBC obtenerXPlaca(string placa)
    {
        return tran.Trailer_ObtenerXPlaca(placa);
    }
    public TrailerBC obtenerXPlaca()
    {
        return tran.Trailer_ObtenerXPlaca(this.PLACA);
    }
    public TrailerBC obtenerXNro(string numero)
    {
        return tran.Trailer_ObtenerXNumeroFlota(numero);
    }
    public TrailerBC obtenerXDoc(string doc, string id_site)
    {
        return tran.Trailer_ObtenerXDoc(doc,id_site);
    }
    public bool Crear(TrailerBC trailer)
    {
        return tran.Trailer_Crear(trailer);
    }
    public bool Crear()
    {
        return tran.Trailer_Crear(this);
    }
    public bool CrearGenerico(TrailerBC trailer, bool importado)
    {
        return tran.Trailer_CrearGenerico(trailer, importado);
    }
    public bool Modificar(TrailerBC trailer)
    {
        return tran.Trailer_Modificar(trailer);
    }
    public bool Modificar()
    {
        return tran.Trailer_Modificar(this);
    }
    public bool Eliminar(int id)
    {
        return tran.Trailer_Eliminar(id);
    }
    public bool Eliminar()
    {
        return tran.Trailer_Eliminar(this.ID);
    }
    public bool Comprobar()
    {
        return tran.Trailer_ComprobarPlacaNro(this.ID, this.PLACA, this.NUMERO);
    }
    public bool Comprobar(TrailerBC t)
    {
        return tran.Trailer_ComprobarPlacaNro(t.ID, t.PLACA, t.NUMERO);
    }
    public bool Comprobar(int id, string placa, string numero)
    {
        return tran.Trailer_ComprobarPlacaNro(id, placa, numero);
    }
    public bool A_Tracto(string placa, out string resultado)
    {
        return tran.Trailer_ATracto(placa, out resultado);
    }
    public DataTable obtener_listado_trailer(int SITIO, int DISPONIBILIDAD, bool BLOQUEADO, int CAPACIDAD, bool PLANCHA, bool asignado, int tipo_carga, string shortec)
    {
        return tran.obtener_listado_trailer(SITIO, DISPONIBILIDAD, BLOQUEADO, CAPACIDAD, PLANCHA, asignado, tipo_carga, shortec);
    }
    public bool Bloquear(int id , int TIPO_BLOQUEO, int usua_id, out  string resultado)
    {
        return tran.Trailer_Bloquear(id,TIPO_BLOQUEO,usua_id, out resultado);
    }
    public bool temporal_estado_cargado(int id)
    {
        return tran.temporal_estado_cargado(id);
    }
    public bool continuar(int id,  int usua_id, out  string resultado)
    {
        return tran.Trailer_continuar(id,  usua_id, out resultado);
    }
    public bool Desbloquear(int id, int LUGA_ID, int usua_id, out  string resultado)
    {
        return tran.Trailer_Desbloquear(id,  LUGA_ID, usua_id, out resultado);
    }
    public DataTable obtener_pre_ingresos(int site, int proveedor, string desde, string hasta)
    {
        return tran.obtener_pre_ingresos(site, proveedor, desde, hasta);
    }
    public DataTable ObtenerUltLavado(int site_id, DateTime desde, DateTime hasta)
    {
        return tran.Trailer_ObtenerLavado(site_id,desde,hasta);
    }
}