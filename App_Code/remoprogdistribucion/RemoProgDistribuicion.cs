using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de RemoProgDistribuicion
/// </summary>
public partial class RemoProgDistribuicion
{
	public RemoProgDistribuicion()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}

public partial class RemoProgDistribuicionTable
{
    private int idField;

    private string descripcionField;

    private int siteIdField;

    private bool activaField;

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

    public bool ACTIVA
    {
        get
        {
            return this.activaField;
        }
        set
        {
            this.activaField = value;
        }
    }
}