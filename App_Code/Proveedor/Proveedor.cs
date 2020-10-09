using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Proveedor
/// </summary>
public class Proveedor
{
	public Proveedor()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}

public partial class ProveedorTable
{
    private int prov_idField;

    private string descripcionField;

    private string direccionField;

    private string codigoField;

    private string rutField;

    public int PROV_ID
    {
        get
        {
            return this.prov_idField;
        }
        set
        {
            this.prov_idField = value;
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

    public string DIRECCION
    {
        get
        {
            return this.direccionField;
        }
        set
        {
            this.direccionField = value;
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
}