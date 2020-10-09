using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TrailerEstado
/// </summary>
public partial class TrailerEstado
{
	public TrailerEstado()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}

public partial class TrailerEstadoTable
{
    private int idField;

    private string descripcionField;

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
}