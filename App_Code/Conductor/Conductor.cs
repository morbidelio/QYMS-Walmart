using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Conductor
/// </summary>
public partial class Conductor
{
	public Conductor()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}

public partial class ConductorTable
{
    private int idField;
    private int tranIdField;
    private string rutField;
    private string nombreField;
    private string imagenField;
    private bool activoField;
    private bool bloqueadoField;
    private string motivoBloqueoField;
    private bool extranjeroField;
    public string IMAGEN
    {
        get
        {
            return this.imagenField;
        }
        set
        {
            this.imagenField = value;
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

    public string RUT
    {
        get
        {
            return this.rutField;
        }
        set
        {
            this.rutField = value;
        }
    }

    public string NOMBRE
    {
        get
        {
            return this.nombreField;
        }
        set
        {
            this.nombreField = value;
        }
    }

    public bool ACTIVO
    {
        get
        {
            return this.activoField;
        }
        set
        {
            this.activoField = value;
        }
    }

    public bool BLOQUEADO
    {
        get
        {
            return this.bloqueadoField;
        }
        set
        {
            this.bloqueadoField = value;
        }
    }

    public string MOTIVO_BLOQUEO
    {
        get
        {
            return motivoBloqueoField;
        }

        set
        {
            motivoBloqueoField = value;
        }
    }

    public bool COND_EXTRANJERO
    {
        get
        {
            return extranjeroField;
        }

        set
        {
            extranjeroField = value;
        }
    }
}