using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Descripción breve de PreEntrada
/// </summary>
public partial class PreEntrada
{
    private PreEntradaTable[] itemsField;

    public PreEntradaTable[] ItemsField
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

public partial class PreEntradaTable
{
    public DateTime FECHA_HORA { get; set; }
    public int COND_ID { get; set; }
    public bool CARGADO { get; set; }
    public int MOIC_ID { get; set; }
    public int TIIC_ID { get; set; }
    public int ID { get; set; }
    public int SITE_ID { get; set; }
    public int PROV_ID { get; set; }
    public int TRAI_ID { get; set; }
    public DateTime FECHA { get; set; }
    public int ESTADO { get; set; }
    public string DOC_INGRESO { get; set; }
    public string SELLO_INGRESO { get; set; }
    public string SELLO_CARGA { get; set; }
    public string RUT_CHOFER { get; set; }
    public string Observacion { get; set; }
    public string NOMBRE_CHOFER { get; set; }
    public string RUT_ACOMP { get; set; }
    public string PATENTE_TRACTO { get; set; }
    public bool extranjero { get; set; }
    public string PRING_FONO { get; set; }
    public DataTable citas { get; set; }
    public ConductorBC CONDUCTOR { get; set; }
}