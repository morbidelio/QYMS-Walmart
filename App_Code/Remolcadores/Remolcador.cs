using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Remolcador
/// </summary>
public partial class Remolcador
{
    public RemolcadorTable[] itemsField;

    public RemolcadorTable[] ItemsField
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

public partial class RemolcadorTable
{
    private int idField;

    private string codigoField;

    private string descripcionField;

    private int idUsuarioField;

    private string playasField;

    private int siteIdField;

    public int SITE_ID
    {
        get
        {
            return this.siteIdField;
        }
        set
        {
            this.siteIdField = value;
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

    public int ID_USUARIO
    {
        get
        {
            return this.idUsuarioField;
        }
        set
        {
            this.idUsuarioField = value;
        }
    }

    public string PLAYAS
    {
        get
        {
            return this.playasField;
        }
        set
        {
            this.playasField = value;
        }
    }

}