using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Jornada
/// </summary>
public partial class Jornada
{
    private JornadaTable[] jornadaField;

    public JornadaTable[] JornadaField
    {
        get
        {
            return this.jornadaField;
        }
        set
        {
            this.jornadaField = value;
        }
    }
}

public partial class JornadaTable
{
    private int idField;

    private string nombreField;

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

    public string NOMBRE
    {
        get
        {
            return this.nombreField;
        }
        set
        {
            this.nombreField = value;
        }
    }
}