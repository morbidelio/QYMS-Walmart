using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Descripción breve de Playa
/// </summary>
public partial class Playa
{
    private PlayaTable[] itemsField;

    public PlayaTable[] Items
    {
        get { return itemsField; }
        set { itemsField = value; }
    }

}

public partial class PlayaTable
{
    private int idField;

    private string codField;

    private string descField;

    private int zonaIdField;

    private bool virtualField;

    private double playaXField;

    private double playaYField;

    private int rotacionField;

    private double alturaField;

    private double anchoField;

    private int idTipoZonaField;

    private int idTipoPlayaField;

    private string DIRField;

    private string CARACTERISTICASField;

    private string excluyentesField;

    private string noExcluyentesField;

    private int siteIdField;
    //Inicio Setters/Getters


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

    public int ID_TIPOPLAYA
    {
        get
        {
            return this.idTipoPlayaField;
        }
        set
        {
            this.idTipoPlayaField = value;
        }
    }

    public double ALTO
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

    public double ANCHO
    {
        get
        {
            return this.anchoField;
        }
        set
        {
            this.anchoField = value;
        }
    }

    public int ID_TIPOZONA
    {
        get
        {
            return this.idTipoZonaField;
        }
        set
        {
            this.idTipoZonaField = value;
        }
    }

    public int ZONA_ID
    {
        get
        {
            return this.zonaIdField;
        }
        set
        {
            this.zonaIdField = value;
        }
    }

    public bool VIRTUAL
    {
        get
        {
            return this.virtualField;
        }
        set
        {
            this.virtualField = value;
        }
    }

    public string CODIGO
    {
        get
        {
            return this.codField;
        }
        set
        {
            this.codField = value;
        }
    }

    public string DIRECCION
    {
        get
        {
            return this.DIRField;
        }
        set
        {
            this.DIRField = value;
        }
    }

    public double PLAYA_X
    {
        get
        {
            return this.playaXField;
        }
        set
        {
            this.playaXField = value;
        }
    }

    public double PLAYA_Y
    {
        get
        {
            return this.playaYField;
        }
        set
        {
            this.playaYField = value;
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

    public string DESCRIPCION
    {
        get
        {
            return this.descField;
        }
        set
        {
            this.descField = value;
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
    public string CARACTERISTICAS
    {
        get
        {
            return this.CARACTERISTICASField;
        }
        set
        {
            this.CARACTERISTICASField = value;
        }
    }
}