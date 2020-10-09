using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Lugar
/// </summary>
public partial class Lugar
{
    private LugarTable[] itemsField;

    public LugarTable[] Items
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

public partial class LugarTable
{
    public int ID { get; set; }
    public string SITE { get; set; }
    public string ESTADO { get; set; }
    public int ID_ZONA { get; set; }
    public string ZONA { get; set; }
    public string PLAYA { get; set; }
    public int ID_PLAYA { get; set; }
    public int ID_TRAILER { get; set; }
    public string TRAILER { get; set; }
    public string CODIGO { get; set; }
    public string DESCRIPCION { get; set; }
    public int ID_SITE { get; set; }
    public int ID_ESTADO { get; set; }
    public bool OCUPADO { get; set; }
    public double POSICION { get; set; }
    public double LUGAR_X { get; set; }
    public double LUGAR_Y { get; set; }
    public int ORDEN { get; set; }
    public double ANCHO { get; set; }
    public double ALTO { get; set; }
    public int ROTACION { get; set; }
}