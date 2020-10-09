using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Trailer
/// </summary>
public partial class Trailer
{
    private TrailerTable[] itemsField;

    public TrailerTable[] ItemsField
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

public partial class TrailerTable
{
    private int idField;
    private string placaField;
    private string codigoField;
    private bool externoField;
    private string numeroField;
    private int tranIdField;
    private int PYTI_IDField;
    private string transportistaField;
    private string caracteristicasField;
    private string excluyentesField;
    private string noExcluyentesField;
    private int tipoIdField;
    private string tipoField;
    private string BloqueadoField;
    private string idShortekField;
    private bool reqSelloField;
    public bool REQ_SELLO
    {
        get
        {
            return this.reqSelloField;
        }
        set
        {
            this.reqSelloField = value;
        }
    }
    public string ID_SHORTEK
    {
        get
        {
            return this.idShortekField;
        }
        set
        {
            this.idShortekField = value;
        }
    }
    public string EXCLUYENTES
    {
        get
        {
            return this.excluyentesField;
        }
        set
        {
            this.excluyentesField = value;
        }
    }
    public string NO_EXCLUYENTES
    {
        get
        {
            return this.noExcluyentesField;
        }
        set
        {
            this.noExcluyentesField = value;
        }
    }
    public string TIPO
    {
        get
        {
            return this.tipoField;
        }
        set
        {
            this.tipoField = value;
        }
    }
    public int TRTI_ID
    {
        get
        {
            return this.tipoIdField;
        }
        set
        {
            this.tipoIdField = value;
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
    public string PLACA
    {
        get
        {
            return this.placaField;
        }
        set
        {
            this.placaField = value;
        }
    }
    public string CODIGO
    {
        get
        {
            return this.codigoField;
        }
        set
        {
            this.codigoField = value;
        }
    }
    public bool EXTERNO
    {
        get
        {
            return this.externoField;
        }
        set
        {
            this.externoField = value;
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
    public int TRAN_ID
    {
        get
        {
            return this.tranIdField;
        }
        set
        {
            this.tranIdField = value;
        }
    }
    public int PYTI_ID
    {
        get
        {
            return this.PYTI_IDField;
        }
        set
        {
            this.PYTI_IDField = value;
        }
    }
    public string TRANSPORTISTA
    {
        get
        {
            return this.transportistaField;
        }
        set
        {
            this.transportistaField = value;
        }
    }
    public string CARACTERISTICAS
    {
        get
        {
            return this.caracteristicasField;
        }
        set
        {
            this.caracteristicasField = value;
        }
    }
    public string BLOQUEADO
    {
        get
        {
            return this.BloqueadoField;
        }
        set
        {
            this.BloqueadoField = value;
        }
    }
    //Datos de ultimo estado trailer
    private int siteIdField;
    private bool siteInField;
    private int lugarIdField;
    private string lugarField;
    private int socaIdField;
    private bool cargadoField;
    private int proveedorIdField;
    private int movimientoIdField;
    private int movEstadoIdField;
    private int tempField;
    private int trnpIdField;
    private DateTime fechaIngresoField;
    private DateTime fechaRetiroField;
    private string docIngresoField;
    private string selloIngresoField;
    private string selloCargaField;
    private string choferRutField;
    private string choferNombreField;
    private string acompRutField;
    private int solicitudIdField;
    private int solEstadoIdField;
    private string observacionField;
    private string patenteTracto;
    private int tresIdField;
    private int usuaIdField;
    private int tcarIdField;
    private int tiicIdField;
    private int moicIdField;
    private int pringIdField;
    private int destIdField;
    private int locaIdField;
    private string TemperaturaField;
    private int condIdField;
    private string ESTADO_YMSfield;
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
    public string OBSERVACION
    {
        get
        {
            return this.observacionField;
        }
        set
        {
            this.observacionField = value;
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
    public string SELLO_CARGA
    {
        get
        {
            return this.selloCargaField;
        }
        set
        {
            this.selloCargaField = value;
        }
    }
    public string PATENTE_TRACTO
    {
        get
        {
            return this.patenteTracto;
        }
        set
        {
            this.patenteTracto = value;
        }
    }
    public int TRES_ID
    {
        get
        {
            return this.tresIdField;
        }
        set
        {
            this.tresIdField = value;
        }
    }
    public int USUA_ID
    {
        get
        {
            return this.usuaIdField;
        }
        set
        {
            this.usuaIdField = value;
        }
    }
    public int TCAR_ID
    {
        get
        {
            return this.tcarIdField;
        }
        set
        {
            this.tcarIdField = value;
        }
    }
    public int TIIC_ID
    {
        get
        {
            return this.tiicIdField;
        }
        set
        {
            this.tiicIdField = value;
        }
    }
    public int MOIC_ID
    {
        get
        {
            return this.moicIdField;
        }
        set
        {
            this.moicIdField = value;
        }
    }
    public int PRING_ID
    {
        get
        {
            return this.pringIdField;
        }
        set
        {
            this.pringIdField = value;
        }
    }
    public int SOES_ID
    {
        get
        {
            return this.solEstadoIdField;
        }
        set
        {
            this.solEstadoIdField = value;
        }
    }
    public int DEST_ID
    {
        get
        {
            return this.destIdField;
        }
        set
        {
            this.destIdField = value;
        }
    }
    public int LOCA_ID
    {
        get
        {
            return this.locaIdField;
        }
        set
        {
            this.locaIdField = value;
        }
    }
    public string temperatura
    {
        get
        {
            return this.TemperaturaField;
        }
        set
        {
            this.TemperaturaField = value;
        }
    }
    public string ESTADO_YMS
    {
        get
        {
            return this.ESTADO_YMSfield;
        }
        set
        {
            this.ESTADO_YMSfield = value;
        }
    }
}