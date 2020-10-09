using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Empresa
/// </summary>
public partial class Empresa
{
    private EmpresaTable[] itemsField;

    public EmpresaTable[] ItemsField
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

public partial class EmpresaTable
{
    private int idField;

    private string descripcionField;

    private string codigoField;

    private string rutField;
    
    private string razonSocialField;

    private string direccionField;

    private int comunaIdField;

    private string comunaField;

    private string giroField;

    private string nombreFantasiaField;

    private double latitudField;

    private double longitudField;
    
    private string telefonoField;
        
    private string nombreContactoField;

    private bool activoField;

    private string bodegaField;

    private string fechaCreacionField;

    private string fechaModificacionField;
        
    private string usrCreacionField;

    private string usrModificacionField;

    private bool existeField;

    private string emailField;

    public string EMAIL
    {
        get
        {
            return this.emailField;
        }
        set
        {
            this.emailField = value;
        }
    }

    public bool EXISTE
    {
        get
        {
            return this.existeField;
        }
        set
        {
            this.existeField = value;
        }
    }

    public string CODIGO
    {
        get
        {
            return this.codigoField;
        }
        set
        {
            this.codigoField = value;
        }
    }

    public string RUT
    {
        get
        {
            return this.rutField;
        }
        set
        {
            this.rutField = value;
        }
    }

    public string RAZON_SOCIAL
    {
        get
        {
            return this.razonSocialField;
        }
        set
        {
            this.razonSocialField = value;
        }
    }

    public string DIRECCION
    {
        get
        {
            return this.direccionField;
        }
        set
        {
            this.direccionField = value;
        }
    }

    public int ID_COMUNA
    {
        get
        {
            return this.comunaIdField;
        }
        set
        {
            this.comunaIdField = value;
        }
    }

    public string COMUNA
    {
        get
        {
            return this.comunaField;
        }
        set
        {
            this.comunaField = value;
        }
    }

    public string GIRO
    {
        get
        {
            return this.giroField;
        }
        set
        {
            this.giroField = value;
        }
    }

    public string NOMBRE_FANTASIA
    {
        get
        {
            return this.nombreFantasiaField;
        }
        set
        {
            this.nombreFantasiaField = value;
        }
    }

    public double LATITUD
    {
        get
        {
            return this.latitudField;
        }
        set
        {
            this.latitudField = value;
        }
    }

    public double LONGITUD
    {
        get
        {
            return this.longitudField;
        }
        set
        {
            this.longitudField = value;
        }
    }

    public string TELEFONO
    {
        get
        {
            return this.telefonoField;
        }
        set
        {
            this.telefonoField = value;
        }
    }

    public string NOMBRE_CONTACTO
    {
        get
        {
            return this.nombreContactoField;
        }
        set
        {
            this.nombreContactoField = value;
        }
    }

    public bool ACTIVO
    {
        get
        {
            return this.activoField;
        }
        set
        {
            this.activoField = value;
        }
    }

    public string BODEGA
    {
        get
        {
            return this.bodegaField;
        }
        set
        {
            this.bodegaField = value;
        }
    }

    public string FECHA_CREACION
    {
        get
        {
            return this.fechaCreacionField;
        }
        set
        {
            this.fechaCreacionField = value;
        }
    }

    public string FECHA_MODIFICACION
    {
        get
        {
            return this.fechaModificacionField;
        }
        set
        {
            this.fechaModificacionField = value;
        }
    }

    public string USR_CREACION
    {
        get
        {
            return this.usrCreacionField;
        }
        set
        {
            this.usrCreacionField = value;
        }
    }

    public string USR_MODIFICACION
    {
        get
        {
            return this.usrModificacionField;
        }
        set
        {
            this.usrModificacionField = value;
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
}