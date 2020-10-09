using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Zona
/// </summary>
public partial class Zona
{
    private ZonaTable[] itemsField;

    public ZonaTable[] ItemsField
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

public partial class ZonaTable
{
    private int idField;

    private string codigoField;

    private string descripcionField;

    private int siteIdField;

    private int tipoZonaIdField;

    private double zonaXField;

    private double zonaYField;

    private int rotacionField;

    private int alturaField;

    private int anchuraField;

    public int ALTURA
    {
        get
        {
            return this.alturaField;
        }
        set
        {
            this.alturaField = value;
        }
    }

    public int ANCHURA
    {
        get
        {
            return this.anchuraField;
        }
        set
        {
            this.anchuraField = value;
        }
    }

    public int ROTACION
    {
        get
        {
            return this.rotacionField;
        }
        set
        {
            this.rotacionField = value;
        }
    }

    public double ZONA_Y
    {
        get
        {
            return this.zonaYField;
        }
        set
        {
            this.zonaYField = value;
        }
    }

    public double ZONA_X
    {
        get
        {
            return this.zonaXField;
        }
        set
        {
            this.zonaXField = value;
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

    public int ZOTI_ID
    {
        get
        {
            return this.tipoZonaIdField;
        }
        set
        {
            this.tipoZonaIdField = value;
        }
    }

    public tipo_zona estado;

    public enum tipo_zona
    {
        General = 0,
        Carga = 100,
        Descarga = 200,
        Mantenimiento = 204
    };  

}