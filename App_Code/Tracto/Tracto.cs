using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Tracto
/// </summary>
public partial class Tracto
{
	public Tracto()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}

public partial class TractoTable
{
    private int idField;

    private string patenteField;

    private int idEstadoField;

    private int idtranField;

    private DateTime fechaCreacionField;

    private int idUsuarioCreacionField;

    private int idBloqueoField;
    
    private DateTime fechaBloqueoField;

    private int idUsuarioBloqueoField;

    private int idsiteField;

    private bool INsiteField;

    private int codigoInternoField;

    private DateTime fhIngresoField;

    private int condIdIngresoField;

    private DateTime fhSalidaField;

    private int condIdSalidaField;

    private string obsField;

    private int usuaIdField;

    private int lugaIdField;

    private DateTime fhLugaIdField;

    private string acompField;

    public int COD_INTERNO
    {
        get
        {
            return this.codigoInternoField;
        }
        set
        {
            this.codigoInternoField = value;
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

    public int COND_ID_INGRESO
    {
        get
        {
            return this.condIdIngresoField;
        }
        set
        {
            this.condIdIngresoField = value;
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

    public int COND_ID_SALIDA
    {
        get
        {
            return this.condIdSalidaField;
        }
        set
        {
            this.condIdSalidaField = value;
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

    public int LUGA_ID
    {
        get
        {
            return this.lugaIdField;
        }
        set
        {
            this.lugaIdField = value;
        }
    }

    public DateTime FH_LUGA_ID
    {
        get
        {
            return this.fhLugaIdField;
        }
        set
        {
            this.fhLugaIdField = value;
        }
    }

    public string ACOMPAÑANTE
    {
        get
        {
            return this.acompField;
        }
        set
        {
            this.acompField = value;
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

    public string PATENTE
    {
        get
        {
            return this.patenteField;
        }
        set
        {
            this.patenteField = value;
        }
    }

    public int TRAE_ID
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

    public int TRAN_ID
    {
        get
        {
            return this.idtranField;
        }
        set
        {
            this.idtranField = value;
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

    public int USUA_ID_CREACION
    {
        get
        {
            return this.idUsuarioCreacionField;
        }
        set
        {
            this.idUsuarioCreacionField = value;
        }
    }

    public int SITE_ID
    {
        get
        {
            return this.idsiteField;
        }
        set
        {
            this.idsiteField = value;
        }
    }

    public bool SITE_IN
    {
        get
        {
            return this.INsiteField;
        }
        set
        {
            this.INsiteField = value;
        }
    }

    public int TRAB_ID
    {
        get
        {
            return this.idBloqueoField;
        }
        set
        {
            this.idBloqueoField = value;
        }
    }

    public DateTime FECHA_BLOQUEO
    {
        get
        {
            return this.fechaBloqueoField;
        }
        set
        {
            this.fechaBloqueoField = value;
        }
    }

    public int USUA_ID_BLOQUEO
    {
        get
        {
            return this.idUsuarioBloqueoField;
        }
        set
        {
            this.idUsuarioBloqueoField = value;
        }
    }
}