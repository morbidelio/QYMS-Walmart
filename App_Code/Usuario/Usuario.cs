
public partial class Usuario
{

    private UsuarioTable[] itemsField;

    public UsuarioTable[] Items
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
public partial class UsuarioTable
{
    private int idField;

    private string codigoField;

    private string descripcionField;

    private string nombreField;

    private string apellidoField;

    private string rutField;

    private string emailField;

    private string usernameField;

    private string passwordField;

    private string password2Field;

    private bool estadoField;

    private string observacionField;

    private int id_tipoField;

    private string tipoField;

    private int id_empresaField;

    private int id_proveedorField;
    private string inicioField;

    private string proveedorField;

    private string siteField;

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

    public string PROVEEDOR
    {
        get
        {
            return this.proveedorField;
        }
        set
        {
            this.proveedorField = value;
        }
    }

    public int ID_PROVEEDOR
    {
        get
        {
            return this.id_proveedorField;
        }
        set
        {
            this.id_proveedorField = value;
        }
    }

    public string TIPO
    {
        get
        {
            return this.tipoField;
        }
        set
        {
            this.tipoField = value;
        }
    }

    public int ID_EMPRESA
    {
        get
        {
            return this.id_empresaField;
        }
        set
        {
            this.id_empresaField = value;
        }
    }


    public string INICIO
    {
        get
        {
            return this.inicioField;
        }
        set
        {
            this.inicioField = value;
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

    public string APELLIDO
    {
        get
        {
            return this.apellidoField;
        }
        set
        {
            this.apellidoField = value;
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

    public string USERNAME
    {
        get
        {
            return this.usernameField;
        }
        set
        {
            this.usernameField = value;
        }
    }

    public string PASSWORD
    {
        get
        {
            return this.passwordField;
        }
        set
        {
            this.passwordField = value;
        }
    }
    public string PASSWORD2
    {
        get
        {
            return this.password2Field;
        }
        set
        {
            this.password2Field = value;
        }
    }
    public string OBSERVACION
    {
        get
        {
            return this.observacionField;
        }
        set
        {
            this.observacionField = value;
        }
    }

    public int ID_TIPO
    {
        get
        {
            return this.id_tipoField;
        }
        set
        {
            this.id_tipoField = value;
        }
    }

    public bool ESTADO
    {
        get
        {
            return this.estadoField;
        }
        set
        {
            this.estadoField = value;
        }
    }

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

    public string SITE
    {
        get
        {
            return this.siteField;
        }
        set
        {
            this.siteField = value;
        }
    }

    private int numero_sitesfield;
    public int numero_sites
    {
        get
        {
            return this.numero_sitesfield;
        }
        set
        {
            this.numero_sitesfield = value;
        }
    }

}

