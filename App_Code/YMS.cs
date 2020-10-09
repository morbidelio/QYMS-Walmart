
[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.3038")]
[System.Serializable()]
[System.Diagnostics.DebuggerStepThrough()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "http://tempuri.org/Zonas.xsd")]
[System.Xml.Serialization.XmlRoot(Namespace = "http://tempuri.org/Zonas.xsd", IsNullable = false)]
public partial class YMS_ZONA
{
    private YMS_ZONA_TABLE[] itemsField;

    [System.Xml.Serialization.XmlElement("Table")]
    public YMS_ZONA_TABLE[] Items
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

[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.3038")]
[System.Serializable()]
[System.Diagnostics.DebuggerStepThrough()]
[System.ComponentModel.DesignerCategory("code")]
[System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "")]
public partial class YMS_ZONA_TABLE
{
    private int IDfield;

    public int ID
    {
        get
        {
            return IDfield;
        }

        set
        {
            IDfield = value;
        }
    }

    private int tipofield;
}
