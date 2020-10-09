using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TipoEstadoMov
/// </summary>
public partial class TipoEstadoMov
{
    private TipoEstadoMovTable[] itemsField;

    public TipoEstadoMovTable[] ItemsField
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

public partial class TipoEstadoMovTable
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