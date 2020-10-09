using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Shorteck
/// </summary>
public class Shorteck
{
	public Shorteck()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}

public partial class ShorteckTable
{
    private string idField;

    private string descripcionField;
    
    public string ID
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
}