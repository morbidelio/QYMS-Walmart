using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de DestinoTipo
/// </summary>
public class DestinoTipo
{
	public DestinoTipo()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}

public partial class DestinoTipoTable
{
    private int idField;

    private string codigoField;

    private string nombreField;

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
}