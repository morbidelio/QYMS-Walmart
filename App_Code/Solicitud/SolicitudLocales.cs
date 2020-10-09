using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de SolicitudLocales
/// </summary>
public partial class SolicitudLocales
{
    private SolicitudLocalesTable[] itemsField;

    public SolicitudLocalesTable[] ItemsField
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

public partial class SolicitudLocalesTable : SolicitudAndenesTable
{
    private int idLocalField;

    private int palletsField;

    private int soldOrdenField;

    private int soanOrdenField;

    public int SOAN_ORDEN
    {
        get
        {
            return this.soanOrdenField;
        }
        set
        {
            this.soanOrdenField = value;
        }
    }

    public int LOCA_ID
    {
        get
        {
            return this.idLocalField;
        }
        set
        {
            this.idLocalField = value;
        }
    }

    public int SOLD_ORDEN
    {
        get
        {
            return this.soldOrdenField;
        }
        set
        {
            this.soldOrdenField = value;
        }
    }

    public int PALLETS
    {
        get
        {
            return this.palletsField;
        }
        set
        {
            this.palletsField = value;
        }
    }
}