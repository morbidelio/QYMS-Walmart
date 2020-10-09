using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Menu
/// </summary>
public partial class Menu
{
    private MenuTable[] itemsField;

    public MenuTable[] ItemsField
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

public partial class MenuTable
{
    private int IdField;

    private int PidField;

    private string TituloField;

    private string LinkField;

    private int OrdenField;

    public int ID
    {
        get
        {
            return this.IdField;
        }
        set
        {
            this.IdField = value;
        }
    }

    public int PID
    {
        get
        {
            return this.PidField;
        }
        set
        {
            this.PidField = value;
        }
    }

    public string TITULO
    {
        get
        {
            return this.TituloField;
        }
        set
        {
            this.TituloField = value;
        }
    }

    public string LINK
    {
        get
        {
            return this.LinkField;
        }
        set
        {
            this.LinkField = value;
        }
    }

    public int ORDEN
    {
        get
        {
            return this.OrdenField;
        }
        set
        {
            this.OrdenField = value;
        }
    }
}