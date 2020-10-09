using System;
using System.Linq;

/// <summary>
/// Descripción breve de SolicitudAndenes
/// </summary>
public partial class SolicitudAndenes
{
    private SolicitudAndenesTable[] itemsField;

    public SolicitudAndenesTable[] ItemsField
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

public partial class SolicitudAndenesTable : SolicitudTable
{
    private int idLugarField;

    private string lugarField;

    private int soanOrdenField;

    private int minsCargaEstField;

    private int minsCargaRealField;

    private int minsEstadiaField;

    private DateTime fechaArriboField;

    private DateTime fechaCargaFinField;

    private DateTime fechaSalidaField;

    private int idSolEstadoField;

    private int palletsCargadosField;

    private string localesField;

    private bool finalizadoField;

    public bool FINALIZADO
    {
        get
        {
            return this.finalizadoField;
        }
        set
        {
            this.finalizadoField = value;
        }
    }

    public string LOCALES
    {
        get
        {
            return this.localesField;
        }
        set
        {
            this.localesField = value;
        }
    }

    public int TIEMPO_ESTADIA
    {
        get
        {
            return this.minsEstadiaField;
        }
        set
        {
            this.minsEstadiaField = value;
        }
    }

    public int TIEMPO_CARGA_REAL
    {
        get
        {
            return this.minsCargaRealField;
        }
        set
        {
            this.minsCargaRealField = value;
        }
    }

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

    public string LUGAR
    {
        get
        {
            return this.lugarField;
        }
        set
        {
            this.lugarField = value;
        }
    }

    public int PALLETS_CARGADOS
    {
        get
        {
            return this.palletsCargadosField;
        }
        set
        {
            this.palletsCargadosField = value;
        }
    }

    public int LUGA_ID
    {
        get
        {
            return this.idLugarField;
        }
        set
        {
            this.idLugarField = value;
        }
    }

    public int MINS_CARGA_EST
    {
        get
        {
            return this.minsCargaEstField;
        }
        set
        {
            this.minsCargaEstField = value;
        }
    }

    public DateTime FECHA_ARRIBO
    {
        get
        {
            return this.fechaArriboField;
        }
        set
        {
            this.fechaArriboField = value;
        }
    }

    public DateTime FECHA_CARGA_FIN
    {
        get
        {
            return this.fechaCargaFinField;
        }
        set
        {
            this.fechaCargaFinField = value;
        }
    }

    public DateTime FECHA_SALIDA
    {
        get
        {
            return this.fechaSalidaField;
        }
        set
        {
            this.fechaSalidaField = value;
        }
    }

    public int ID_SOES
    {
        get
        {
            return this.idSolEstadoField;
        }
        set
        {
            this.idSolEstadoField = value;
        }
    }

}