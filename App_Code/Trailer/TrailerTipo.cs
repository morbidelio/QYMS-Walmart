using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TrailerTipo
/// </summary>
public partial class TrailerTipo
{
    private TrailerTipoTable[] itemsField;

    public TrailerTipoTable[] ItemsField
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

public partial class TrailerTipoTable
{
    private int idField;

    private string descripcionField;

    private string colorField;

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

    public string COLOR
    {
        get
        {
            return this.colorField;
        }
        set
        {
            this.colorField = value;
        }
    }

}