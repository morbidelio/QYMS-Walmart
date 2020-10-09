using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ServiciosExternos
/// </summary>
public class ServiciosExternos
{
	public ServiciosExternos()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}

public partial class ServiciosExternosTable
{
    private int idField;

    private string codigoField;

    public int SEEX_ID
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
}