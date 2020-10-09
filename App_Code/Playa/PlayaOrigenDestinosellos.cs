using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de PlayaOrigenDestinoSello
/// </summary>
public partial class PlayaOrigenDestinoSello
{
	public PlayaOrigenDestinoSello()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
}

public partial class PlayaOrigenDestinoSelloTable
{
    private int siteIdField;

    private int playIdOrigenField;

    private int playIdDestinoField;

    private int plodOrdenField;

    private int tiicIdField;

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

    public int PLAY_ID_ORIGEN
    {
        get
        {
            return this.playIdOrigenField;
        }
        set
        {
            this.playIdOrigenField = value;
        }
    }

    public int PLAY_ID_DESTINO
    {
        get
        {
            return this.playIdDestinoField;
        }
        set
        {
            this.playIdDestinoField = value;
        }
    }

    public int ORDEN
    {
        get
        {
            return this.plodOrdenField;
        }
        set
        {
            this.plodOrdenField = value;
        }
    }

    public int TIIC_ID
    {
        get
        {
            return this.tiicIdField;
        }
        set
        {
            this.tiicIdField = value;
        }
    }
}