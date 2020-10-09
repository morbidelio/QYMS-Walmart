using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TimerProcesos
/// </summary>
public partial class TimerProcesos
{
	public TimerProcesos()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}

public partial class TimerProcesosTable
{
    private int idField;

    private string codigoField;

    private string descripcionField;

    private int rojoField;

    private int amarilloField;

    private int verdeField;

    private int siteIdField;

    private int tresIdField;

    private int zotiIdField;

    private int pytiIdField;

    private int soesIdField;

    private int tiempoMaxField;

    private string colorField;

    public string COLOR
    {
        get
        {
            return this.colorField;
        }
        set
        {
            this.colorField = value;
        }
    }

    public int TIEMPO_MAX
    {
        get
        {
            return this.tiempoMaxField;
        }
        set
        {
            this.tiempoMaxField = value;
        }
    }

    public int SOES_ID
    {
        get
        {
            return this.soesIdField;
        }
        set
        {
            this.soesIdField = value;
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

    public string DESCRIPCION
    {
        get
        {
            return this.descripcionField;
        }
        set
        {
            this.descripcionField = value;
        }
    }

    public int ROJO
    {
        get
        {
            return this.rojoField;
        }
        set
        {
            this.rojoField = value;
        }
    }

    public int AMARILLO
    {
        get
        {
            return this.amarilloField;
        }
        set
        {
            this.amarilloField = value;
        }
    }

    public int VERDE
    {
        get
        {
            return this.verdeField;
        }
        set
        {
            this.verdeField = value;
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

    public int ZOTI_ID
    {
        get
        {
            return this.zotiIdField;
        }
        set
        {
            this.zotiIdField = value;
        }
    }

    public int PYTI_ID
    {
        get
        {
            return this.pytiIdField;
        }
        set
        {
            this.pytiIdField = value;
        }
    }
}