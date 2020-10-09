using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CaractCarga
/// </summary>
public partial class CaractCarga
{
    private CaractCargaTable[] itemsField;

    public CaractCargaTable[] ItemsField
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

public partial class CaractCargaTable
{
    private int idField;

    private string codigoField;

    private string descripcionField;

    private int caractCargaTipoIdField;

    private string caractCargaTipoField;

    private int valorField;

    private int ordenField;

    public int ORDEN
    {
        get
        {
            return this.ordenField;
        }
        set
        {
            this.ordenField = value;
        }
    }

    public string TIPO_CARACT_CARGA
    {
        get
        {
            return this.caractCargaTipoField;
        }
        set
        {
            this.caractCargaTipoField = value;
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

    public int CACT_ID
    {
        get
        {
            return this.caractCargaTipoIdField;
        }
        set
        {
            this.caractCargaTipoIdField = value;
        }
    }

    public int VALOR
    {
        get
        {
            return this.valorField;
        }
        set
        {
            this.valorField = value;
        }
    }
}