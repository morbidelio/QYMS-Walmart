using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Transportista
/// </summary>
public partial class Transportista
{
    private TransportistaTable[] itemsField;

    public TransportistaTable[] ItemsField
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

public partial class TransportistaTable
{
    private int idField;

    private string nombreField;

    private string rutField;

    private string passField;

    private int rolField;

    private bool existeField;

    private string estadoField;

    public bool EXISTE
    {
        get
        {
            return this.existeField;
        }
        set
        {
            this.existeField = value;
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

    public string PASS
    {
        get
        {
            return this.passField;
        }
        set
        {
            this.passField = value;
        }
    }


    public string ESTADO
    {
        get
        {
            return this.estadoField;
        }
        set
        {
            this.estadoField = value;
        }
    }
    public int ROL
    {
        get
        {
            return this.rolField;
        }
        set
        {
            this.rolField = value;
        }
    }
}