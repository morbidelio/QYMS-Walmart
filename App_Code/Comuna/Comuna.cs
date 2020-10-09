using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Comuna
/// </summary>
public partial class Comuna
{
    private ComunaTable[] itemsField;

    public ComunaTable[] ItemsField
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }
}

public partial class ComunaTable
{
    private int idField;

    private string nombreField;

    private int regionIdField;

    private string regionField;

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

    public int ID_REGION
    {
        get
        {
            return this.regionIdField;
        }
        set
        {
            this.regionIdField = value;
        }
    }

    public string REGION
    {
        get
        {
            return this.regionField;
        }
        set
        {
            this.regionField = value;
        }
    }
}