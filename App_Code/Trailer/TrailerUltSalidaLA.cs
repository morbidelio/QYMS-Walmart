using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TrailerUltSalidaLA
/// </summary>
public class TrailerUltSalidaLA
{
    public TrailerUltSalidaLA()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}

public partial class TrailerUltSalidaLATable : TrailerUltSalidaTable
{
    private int destIdField;
    private string mmppField;
    private int cajasField;
    private string guiaField;
    private int palletAzulField;
    private int palletRojoField;
    private int palletBlancoField;
    private int leñaField;

    public int DEST_ID
    {
        get
        {
            return destIdField;
        }

        set
        {
            destIdField = value;
        }
    }

    public string MMPP
    {
        get
        {
            return mmppField;
        }

        set
        {
            mmppField = value;
        }
    }

    public int CAJAS
    {
        get
        {
            return cajasField;
        }

        set
        {
            cajasField = value;
        }
    }

    public string GUIA
    {
        get
        {
            return guiaField;
        }

        set
        {
            guiaField = value;
        }
    }

    public int PALLET_AZUL
    {
        get
        {
            return palletAzulField;
        }

        set
        {
            palletAzulField = value;
        }
    }

    public int PALLET_ROJO
    {
        get
        {
            return palletRojoField;
        }

        set
        {
            palletRojoField = value;
        }
    }

    public int PALLET_BLANCO
    {
        get
        {
            return palletBlancoField;
        }

        set
        {
            palletBlancoField = value;
        }
    }

    public int LEÑA
    {
        get
        {
            return leñaField;
        }

        set
        {
            leñaField = value;
        }
    }
}