using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TrailerUltEstado
/// </summary>
public partial class TrailerUltEstado
{
    private TrailerUltEstadoTable[] itemsField;

    public TrailerUltEstadoTable[] ItemsField
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

public partial class TrailerUltEstadoTable : TrailerTable
{
    private int idField;

    private int siteIdField;

    private bool siteInField;

    private int lugarIdField;

    private string lugarField;

    private int socaIdField;

    private int movEstadoIdField;

    private bool cargadoField;

    private int proveedorIdField;

    private int movimientoIdField;

    private DateTime fechaIngresoField;

    private DateTime fechaRetiroField;

    private string docIngresoField;

    private string selloIngresoField;

    private string choferRutField;

    private string choferNombreField;

    private string acompRutField;

    private int solicitudIdField;

    private int condIdField;

    public int COND_ID
    {
        get
        {
            return this.condIdField;
        }
        set
        {
            this.condIdField = value;
        }
    }

    public int SOLI_ID
    {
        get
        {
            return this.solicitudIdField;
        }
        set
        {
            this.solicitudIdField = value;
        }
    }

    public string LUGAR
    {
        get
        {
            return this.lugarField;
        }
        set
        {
            this.lugarField = value;
        }
    }

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

    public int SITE_ID
    {
        get
        {
            return this.siteIdField;
        }
        set
        {
            this.siteIdField = value;
        }
    }

    public bool SITE_IN
    {
        get
        {
            return this.siteInField;
        }
        set
        {
            this.siteInField = value;
        }
    }

    public int LUGAR_ID
    {
        get
        {
            return this.lugarIdField;
        }
        set
        {
            this.lugarIdField = value;
        }
    }

    public int SOCA_ID
    {
        get
        {
            return this.socaIdField;
        }
        set
        {
            this.socaIdField = value;
        }
    }

    public int MOES_ID
    {
        get
        {
            return this.movEstadoIdField;
        }
        set
        {
            this.movEstadoIdField = value;
        }
    }

    public bool CARGADO
    {
        get
        {
            return this.cargadoField;
        }
        set
        {
            this.cargadoField = value;
        }
    }

    public int PROV_ID
    {
        get
        {
            return this.proveedorIdField;
        }
        set
        {
            this.proveedorIdField = value;
        }
    }

    public int MOVI_ID
    {
        get
        {
            return this.movimientoIdField;
        }
        set
        {
            this.movimientoIdField = value;
        }
    }

    public DateTime FECHA_INGRESO
    {
        get
        {
            return this.fechaIngresoField;
        }
        set
        {
            this.fechaIngresoField = value;
        }
    }

    public DateTime FECHA_RETIRO
    {
        get
        {
            return this.fechaRetiroField;
        }
        set
        {
            this.fechaRetiroField = value;
        }
    }

    public string DOC_INGRESO
    {
        get
        {
            return this.docIngresoField;
        }
        set
        {
            this.docIngresoField = value;
        }
    }

    public string SELLO_INGRESO
    {
        get
        {
            return this.selloIngresoField;
        }
        set
        {
            this.selloIngresoField = value;
        }
    }

    public string CHOFER_RUT
    {
        get
        {
            return this.choferRutField;
        }
        set
        {
            this.choferRutField = value;
        }
    }

    public string CHOFER_NOMBRE
    {
        get
        {
            return this.choferNombreField;
        }
        set
        {
            this.choferNombreField = value;
        }
    }

    public string ACOMP_RUT
    {
        get
        {
            return this.acompRutField;
        }
        set
        {
            this.acompRutField = value;
        }
    }

    private string TIPO_INGRESO_CARGA_FIELD;

    public string TIPO_INGRESO_CARGA
    {
        get
        {
            return this.TIPO_INGRESO_CARGA_FIELD;
        }
        set
        {
            this.TIPO_INGRESO_CARGA_FIELD = value;
        }
    }


    private string motivo_TIPO_INGRESO_CARGA_FIELD;

    public string motivo_TIPO_INGRESO_CARGA
    {
        get
        {
            return this.motivo_TIPO_INGRESO_CARGA_FIELD;
        }
        set
        {
            this.motivo_TIPO_INGRESO_CARGA_FIELD = value;
        }
    }

    private string pring_idFIELD;
    public string pring_id
    {
        get
        {
            return this.pring_idFIELD;
        }
        set
        {
            this.pring_idFIELD = value;
        }
    }


    private string guiaField;
    public string GUIA
    {
        get
        {
            return this.guiaField;
        }
        set
        {
            this.guiaField = value;
        }
    }


}