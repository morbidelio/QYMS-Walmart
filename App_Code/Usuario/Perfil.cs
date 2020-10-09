using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Perfil
/// </summary>
public partial class Perfil
{
    private PerfilTable[] itemsField;

    public PerfilTable[] ItemsField
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

public partial class PerfilTable
{
    private int idField;

    private string nombreField;

    private string descripcionField;

    private string menuField;

    private bool mobileField;

    private int nivelPermisosField;

    public int NIVEL_PERMISOS
    {
        get
        {
            return this.nivelPermisosField;
        }
        set
        {
            this.nivelPermisosField = value;
        }
    }

    public bool MOBILE
    {
        get
        {
            return this.mobileField;
        }
        set
        {
            this.mobileField = value;
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

    public string MENU
    {
        get
        {
            return this.menuField;
        }
        set
        {
            this.menuField = value;
        }
    }
}