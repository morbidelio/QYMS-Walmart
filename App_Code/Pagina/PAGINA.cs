﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión del motor en tiempo de ejecución:2.0.50727.5448
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// Este código fuente fue generado automáticamente por xsd, Versión=2.0.50727.3038.
// 


/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/PAGINA.xsd")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://tempuri.org/PAGINA.xsd", IsNullable=false)]
public partial class PAGINA {
    
    private PAGINATable[] itemsField;
    
    /// <comentarios/>
    [System.Xml.Serialization.XmlElementAttribute("Table")]
    public PAGINATable[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }
}

/// <comentarios/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://tempuri.org/PAGINA.xsd")]
public partial class PAGINATable {
    
    private int idField;
    
    private string nOMBREField;
    
    private string uRLField;
    
    private string dESCRIPCIONField;
    
    private int oRDENField;
    
    /// <comentarios/>
    public int ID {
        get {
            return this.idField;
        }
        set {
            this.idField = value;
        }
    }
    
    /// <comentarios/>
    public string NOMBRE {
        get {
            return this.nOMBREField;
        }
        set {
            this.nOMBREField = value;
        }
    }
    
    /// <comentarios/>
    public string URL {
        get {
            return this.uRLField;
        }
        set {
            this.uRLField = value;
        }
    }
    
    /// <comentarios/>
    public string DESCRIPCION {
        get {
            return this.dESCRIPCIONField;
        }
        set {
            this.dESCRIPCIONField = value;
        }
    }
    
    /// <comentarios/>
    public int ORDEN {
        get {
            return this.oRDENField;
        }
        set {
            this.oRDENField = value;
        }
    }
}
