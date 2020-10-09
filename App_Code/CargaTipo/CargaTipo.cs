using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CargaTipo
/// </summary>
public partial class CargaTipo
{
    private CargaTipoTable[] itemsField;

    public CargaTipoTable[] ItemsField
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

public partial class CargaTipoTable
{
    private int idField;

    private string descripcionField;

    private bool preingresoField;

    public bool PREINGRESO
    {
        get
        {
            return this.preingresoField;
        }
        set
        {
            this.preingresoField = value;
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