using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Site
/// </summary>
public partial class Site
{
    private SiteTable[] itemsField;

    public SiteTable[] ItemsField
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

public partial class SiteTable
{
    private int idField;

    private string nombreField;

    private string descField;

    private int empr_idField;

    private bool trailerAutoField;

    private int codSapField;

    public bool TRAILER_AUTO
    {
        get
        {
            return this.trailerAutoField;
        }
        set
        {
            this.trailerAutoField = value;
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

    public string DESCRIPCION
    {
        get
        {
            return this.descField;
        }
        set
        {
            this.descField = value;
        }
    }

    public int EMPRESA_ID
    {
        get
        {
            return this.empr_idField;
        }
        set
        {
            this.empr_idField = value;
        }
    }

    public int COD_SAP
    {
        get
        {
            return codSapField;
        }

        set
        {
            codSapField = value;
        }
    }
}