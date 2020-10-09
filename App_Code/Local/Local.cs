using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Local
/// </summary>
public partial class Local
{
    private LocalTable[] itemsField;

    public LocalTable[] ItemsField
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

public partial class LocalTable
{
    private int idField;

    private string codigoField;

    private string codigo2Field;

    private string descripcionField;

    private string direccionField;

    private int comunaIdField;

    private int regionIdField;

    private int internosField;

    private int externosField;

    private string caracteristicasField;

    private string excluyentesField;

    private string noExcluyentesField;

    private int maximo;

    public int VALOR_CARACT_MAXIMO
    {
        get
        {
            return this.maximo;
        }
        set
        {
            this.maximo = value;
        }
    }

    public string CODIGO2
    {
        get
        {
            return this.codigo2Field;
        }
        set
        {
            this.codigo2Field = value;
        }
    }

    public string CARACTERISTICAS
    {
        get
        {
            return this.caracteristicasField;
        }
        set
        {
            this.caracteristicasField = value;
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

    public int COMUNA_ID
    {
        get
        {
            return this.comunaIdField;
        }
        set
        {
            this.comunaIdField = value;
        }
    }

    public int REGION_ID
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

    public int INTERNOS
    {
        get
        {
            return this.internosField;
        }
        set
        {
            this.internosField = value;
        }
    }

    public int EXTERNOS
    {
        get
        {
            return this.externosField;
        }
        set
        {
            this.externosField = value;
        }
    }

    public string EXCLUYENTES
    {
        get
        {
            return this.excluyentesField;
        }
        set
        {
            this.excluyentesField = value;
        }
    }

    public string NO_EXCLUYENTES
    {
        get
        {
            return this.noExcluyentesField;
        }
        set
        {
            this.noExcluyentesField = value;
        }
    }

}