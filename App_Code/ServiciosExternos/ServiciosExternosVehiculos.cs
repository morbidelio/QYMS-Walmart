using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ServiciosExternosVehiculos
/// </summary>
public partial class ServiciosExternosVehiculos
{
	public ServiciosExternosVehiculos()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}

public partial class ServiciosExternosVehiculosTable
{
    private int idField;
    private string codigoField;
    private string placaField;
    private int provIdField;
    private int siteIdField;
    private bool siteInField;
    private int codInternoField;
    private DateTime fhIngresoField;
    private int idIngresoField;
    private DateTime fhSalidaField;
    private string condRutField;
    private string condNombreField;
    private string obsField;
    private int usuaIdField;
    private int seexIdField;
    private ServiciosExternosConductorBC conductorField;
    public int SEVE_ID
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

    public int COD_INTERNO_IN
    {
        get
        {
            return this.codInternoField;
        }
        set
        {
            this.codInternoField = value;
        }
    }

    public DateTime FH_INGRESO
    {
        get
        {
            return this.fhIngresoField;
        }
        set
        {
            this.fhIngresoField = value;
        }
    }

    public int ID_INGRESO
    {
        get
        {
            return this.idIngresoField;
        }
        set
        {
            this.idIngresoField = value;
        }
    }

    public DateTime FH_SALIDA
    {
        get
        {
            return this.fhSalidaField;
        }
        set
        {
            this.fhSalidaField = value;
        }
    }

    public string COSE_RUT
    {
        get
        {
            return this.condRutField;
        }
        set
        {
            this.condRutField = value;
        }
    }

    public string COSE_NOMBRE
    {
        get
        {
            return this.condNombreField;
        }
        set
        {
            this.condNombreField = value;
        }
    }

    public string OBSERVACION
    {
        get
        {
            return this.obsField;
        }
        set
        {
            this.obsField = value;
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

    public int SEEX_ID
    {
        get
        {
            return this.seexIdField;
        }
        set
        {
            this.seexIdField = value;
        }
    }

    public ServiciosExternosConductorBC CONDUCTOR
    {
        get
        {
            return conductorField;
        }

        set
        {
            conductorField = value;
        }
    }
}