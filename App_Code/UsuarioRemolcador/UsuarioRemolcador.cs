using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de UsuarioRemolcador
/// </summary>
public partial class UsuarioRemolcador
{
    UsuarioRemolcadorTable[] usuarioRemolcadorField;

    public UsuarioRemolcadorTable[] UsuarioRemolcadorField
    {
        get
        {
            return this.usuarioRemolcadorField;
        }
        set
        {
            this.usuarioRemolcadorField = value;
        }
    }
}

public partial class UsuarioRemolcadorTable
{
    private int idField;

    private int usuaIdField;

    private int remoIdField;

    private int jornIdField;

    private int siteIdField;

    private DateTime fechaField;

    private int repdIdField;

    public int REPR_ID
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

    public int REPD_ID
    {
        get
        {
            return this.repdIdField;
        }
        set
        {
            this.repdIdField = value;
        }
    }

    public int USUA_ID
    {
        get
        {
            return this.usuaIdField;
        }
        set
        {
            this.usuaIdField = value;
        }
    }

    public int REMO_ID
    {
        get
        {
            return this.remoIdField;
        }
        set
        {
            this.remoIdField = value;
        }
    }

    public int JORN_ID
    {
        get
        {
            return this.jornIdField;
        }
        set
        {
            this.jornIdField = value;
        }
    }

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

    public DateTime FECHA
    {
        get
        {
            return this.fechaField;
        }
        set
        {
            this.fechaField = value;
        }
    }
}