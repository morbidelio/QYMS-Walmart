using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Solicitud
/// </summary>
public partial class Solicitud
{
    private SolicitudTable[] itemsField;

    public SolicitudTable[] ItemsField
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }
}

public partial class SolicitudTable
{
    public string BLOQUEADOS { get; set; }
    public int PLAY_ID { get; set; }
    public DateTime TIMESTAMP { get; set; }
    public string ID_SHORTECK { get; set; }
    public DateTime FECHA_MOD { get; set; }
    public string LOCALES { get; set; }
    public string TEMPERATURA { get; set; }
    public int TETR_ID { get; set; }
    public string CARACTERISTICAS { get; set; }
    public string RUTA { get; set; }
    public int ID_SITE { get; set; }
    public DateTime FECHA_PLAN_ANDEN { get; set; }
    public string ESTADO { get; set; }
    public string PLACA_TRAILER { get; set; }
    public string TIPO { get; set; }
    public string USUARIO { get; set; }
    public int SOLI_ID { get; set; }
    public int ID_TIPO { get; set; }
    public int ID_USUARIO { get; set; }
    public DateTime FECHA_CREACION { get; set; }
    public DateTime FECHA_FIN { get; set; }
    public string DOCUMENTO { get; set; }
    public string OBSERVACION { get; set; }
    public int ID_TRAILER_RESERVADO { get; set; }
    public int ID_TAMANO { get; set; }
    public int POS_DESTINO { get; set; }
    public DateTime FECHA_REALIZACION { get; set; }
    public int ID_TRAILER { get; set; }
    public int ID_DESTINO { get; set; }
    public int ID_DESTINO_PALLET { get; set; }
    public int SOES_ID { get; set; }
    public int Pallets { get; set; }
    public SolicitudAndenesBC[] SOLICITUD_ANDEN { get; set; }
    public estado_solicitud estado;
    public  enum estado_solicitud {
        SolicituddescargaCreada = 0,
        SolicitudDescargaIngresada = 1,
        SolicitudAndenesCreada = 100,
        SolicitudAndenesIngresada = 3,
        Cargaenproceso = 10,
        CargaAndenesenProceso = 12,
        Cargapendiente = 15,
        CargaParcial = 20,
        CargaSuspendida = 21,
        CargaAndénSuspendida = 22,
        CargaCompleta = 30,
        CargaAndénCompleta = 32,
        SolicitudDescargaFinalizada = 40,
        SolicitudAndénFinalizada = 42,
        Solicitudcreada = 50,
        SolicitudIngresada = 51,
        Descargaenproceso = 60,
        Descargacompleta = 70,
        SolicitudFinalizada = 80,
        Movimiento = 90,
    };  
}