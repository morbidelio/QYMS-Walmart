using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Destino
/// </summary>
public partial class Destino
{
    private DestinoTable[] destinoField;

    public DestinoTable[] DestinoField
    {
        get
        {
            return this.destinoField;
        }
        set
        {
            this.destinoField = value;
        }
    }
}

public partial class DestinoTable
{
    private int idField;

    private string codigoField;

    private string nombreField;

    private int detiIdField;

    public int DETI_ID
    {
        get
        {
            return this.detiIdField;
        }
        set
        {
            this.detiIdField = value;
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