using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CaractTipoCarga
/// </summary>
public partial class CaractTipoCarga
{
    private CaractTipoCargaTable[] itemsField;

    public CaractTipoCargaTable[] ItemsField
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

public partial class CaractTipoCargaTable
{
    private int idField;

    private string descripcionField;

    private bool excluyenteField;

    public bool EXCLUYENTE
    {
        get
        {
            return this.excluyenteField;
        }
        set
        {
            this.excluyenteField = value;
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