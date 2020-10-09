using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Movimiento
/// </summary>
public partial class Movimiento
{
    private MovimientoTable[] itemsField;

    public MovimientoTable[] ItemsField
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

public partial class MovimientoTable
{
    private int idField;

    private DateTime fechaCreacionField;

    private DateTime fechaAsignacionField;

    private DateTime fechaEjecucionField;

    private int idEstadoField;

    private string observacionesField;

    private string petroleoField;

    private int idOrigenField;

    private DateTime fechaOrigenField;

    private int idDestinoField;

    private DateTime fechaDestinoField;

    private int idSolicitudField;

    private int idTrailerField;

    private int ordenField;

    private int idRemolcadorField;

    private string patenteTractoField;

    private int localDesdeField;

    private int localHastaField;

    private string numeroField;

    private bool mantExternoField;

    public int ID
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    public DateTime FECHA_CREACION
    {
        get
        {
            return this.fechaCreacionField;
        }
        set
        {
            this.fechaCreacionField = value;
        }
    }

    public DateTime FECHA_ASIGNACION
    {
        get
        {
            return this.fechaAsignacionField;
        }
        set
        {
            this.fechaAsignacionField = value;
        }
    }

    public DateTime FECHA_EJECUCION
    {
        get
        {
            return this.fechaEjecucionField;
        }
        set
        {
            this.fechaEjecucionField = value;
        }
    }

    public int ID_ESTADO
    {
        get
        {
            return this.idEstadoField;
        }
        set
        {
            this.idEstadoField = value;
        }
    }

    public string OBSERVACION
    {
        get
        {
            return this.observacionesField;
        }
        set
        {
            this.observacionesField = value;
        }
    }

    public string petroleo
    {
        get
        {
            return this.petroleoField;
        }
        set
        {
            this.petroleoField = value;
        }
    }

    public int ID_ORIGEN
    {
        get
        {
            return this.idOrigenField;
        }
        set
        {
            this.idOrigenField = value;
        }
    }

    public DateTime FECHA_ORIGEN
    {
        get
        {
            return this.fechaOrigenField;
        }
        set
        {
            this.fechaOrigenField = value;
        }
    }

    public int ID_DESTINO
    {
        get
        {
            return this.idDestinoField;
        }
        set
        {
            this.idDestinoField = value;
        }
    }

    public DateTime FECHA_DESTINO
    {
        get
        {
            return this.fechaDestinoField;
        }
        set
        {
            this.fechaDestinoField = value;
        }
    }

    public int ID_SOLICITUD
    {
        get
        {
            return this.idSolicitudField;
        }
        set
        {
            this.idSolicitudField = value;
        }
    }

    public int ID_TRAILER
    {
        get
        {
            return this.idTrailerField;
        }
        set
        {
            this.idTrailerField = value;
        }
    }

    public int ORDEN
    {
        get
        {
            return this.ordenField;
        }
        set
        {
            this.ordenField = value;
        }
    }

    public int ID_REMOLCADOR
    {
        get
        {
            return this.idRemolcadorField;
        }
        set
        {
            this.idRemolcadorField = value;
        }
    }

    public string PATENTE_TRACTO
    {
        get
        {
            return this.patenteTractoField;
        }
        set
        {
            this.patenteTractoField = value;
        }
    }

    public int LOCAL_DESDE
    {
        get
        {
            return this.localDesdeField;
        }
        set
        {
            this.localDesdeField = value;
        }
    }

    public int LOCAL_HASTA
    {
        get
        {
            return this.localHastaField;
        }
        set
        {
            this.localHastaField = value;
        }
    }

    public string NUMERO
    {
        get
        {
            return this.numeroField;
        }
        set
        {
            this.numeroField = value;
        }
    }

    public bool MANT_EXTERNO
    {
        get
        {
            return this.mantExternoField;
        }
        set
        {
            this.mantExternoField = value;
        }
    }

    public estado_movimiento estado;


    public  enum estado_movimiento
    {
        MovimientoCreado = 10,
        RecibidoRemolcador = 20,
        RetiradoOrigen = 30,
        RetiradoOrigenModificado = 35,
        FinalizadoDestino = 40,
        FinalizadoDestinoModificado = 50,
        Cancelado = 60,



    }



}