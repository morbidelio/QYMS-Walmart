using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TrailerUltSalida
/// </summary>
public class TrailerUltSalida
{
	public TrailerUltSalida()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}

public partial class TrailerUltSalidaTable : TrailerTable
{
    private int traiIdField;
	
    private int siteIdField;
    private int provIdField;
    private DateTime fechaField;
    private int estadoField;
    private string docIngresoField;
    private string selloIngresoField;
    private string selloCargaField;
    private string choferRutField;
    private string choferNombreField;
    private string acompRutField;
    private string patenteTractoField;
    private bool cargadoField;
    private int moicIdField;
    private int tiicIdField;
    private string observacionField;
    private int condIdField;
    private long trueCodInternoInField;
    private string viajeField;

    public long TRUE_COD_INTERNO_IN
    {
        get
        {
            return trueCodInternoInField;
        }

        set
        {
            trueCodInternoInField = value;
        }
    }

    public int TRAI_ID
    {
        get
        {
            return this.traiIdField;
        }
        set
        {
            this.traiIdField = value;
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

    public int PROV_ID
    {
        get
        {
            return this.provIdField;
        }
        set
        {
            this.provIdField = value;
        }
    }

    public DateTime FECHA
    {
        get
        {
            return this.fechaField;
        }
        set
        {
            this.fechaField = value;
        }
    }

    public int ESTADO
    {
        get
        {
            return this.estadoField;
        }
        set
        {
            this.estadoField = value;
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

    public string VIAJE
    {
        get
        {
            return viajeField;
        }

        set
        {
            viajeField = value;
        }
    }

}