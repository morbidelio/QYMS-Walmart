using System;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Data;
using AjaxControlToolkit;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
using Telerik.Web.UI;
using System.Web.UI;

/// <summary>
/// Descripción breve de UtilsWeb
/// </summary>
public class UtilsWeb
{
    readonly int intervalo_preingreso = 6;
    public UtilsWeb()
    {
    }
    
    public int Intervalo_preingreso
    {
        get
        {
            return this.intervalo_preingreso;
        }
    }

    public void ShowMessage(Page p, string msj, string clase, bool hide)
    {
        string script = string.Format("msj(\"{0}\",\"{1}\",{2});", msj.Replace("\"","'"), clase, hide.ToString().ToLower());
        ScriptManager.RegisterStartupScript(p, p.GetType(), "msj", script, true);
    }
    public void ShowMessage2(Page p, string accion, string clase)
    {
        ScriptManager.RegisterStartupScript(p.Page, p.GetType(), "msj", string.Format("showAlertClass(\"{0}\",\"{1}\");", accion, clase), true);
    }
    public void AbrirModal(Page p,string nombreModal)
    {
        ScriptManager.RegisterStartupScript(p.Page, p.GetType(), "modal", string.Format("abrirModal('{0}');", nombreModal), true);
    }
    public void CerrarModal(Page p,string nombreModal)
    {
        ScriptManager.RegisterStartupScript(p.Page, p.GetType(), "modal", string.Format("cerrarModal('{0}');", nombreModal), true);
    }
    public string rutANumero(string rut)
    {
        rut = rut.Replace(".", "");
        rut = rut.Replace("-", "");
        return rut;
    }

    public string formatearRut(string rut)
    {
        if(!rut.Contains("-"))
        {
            string r = rut.Substring(0, rut.Length - 1);
            string id = rut.Substring(rut.Length - 1);
            return r + "-" + id;
        }
        else
            return rut;
    }

    public bool validarRut(string rut)
    {
        bool validacion = false;
        try
        {
            rut = rut.ToUpper();
            rut = rut.Replace(".", "");
            rut = rut.Replace("-", "");
            int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

            char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

            int m = 0, s = 1;
            for (; rutAux != 0; rutAux /= 10)
            {
                s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
            }
            if (dv == (char)(s != 0 ? s + 47 : 75))
            {
                validacion = true;
            }
        }
        catch (Exception e)
        {
            validacion = false;
        }
        return validacion;
    }

    public bool patentevalida(string patente)
    {
        if (patente.Length!=6) 
        {
            return false;
        }

        if ((char.IsLetter(patente[0]) == true && char.IsLetter(patente[1]) == true && char.IsDigit(patente[2]) == true && char.IsDigit(patente[3]) == true && char.IsDigit(patente[4]) == true && char.IsDigit(patente[5]) == true)
            ||
            ((char.IsLetter(patente[0]) == true && char.IsLetter(patente[1]) == true && char.IsLetter(patente[2]) == true && char.IsLetter(patente[3]) == true && char.IsDigit(patente[4]) == true && char.IsDigit(patente[5]) == true)))
        {
            return true;
        }
        else
            return false;


    }


    public void CargaDropCliente(object nombreDrop, string value, string text, DataTable dt)
    {
        if (nombreDrop is ComboBox)
        {
            ComboBox drop = (ComboBox)nombreDrop;
            drop.DataSource = null;
            drop.DataBind();
            //drop.SelectedIndex = 0;
            drop.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                drop.DataSource = dt;
                drop.DataValueField = value;
                drop.DataTextField = text;
                drop.DataBind();
            }
            ListItem li = new ListItem("Todos", "0");
            drop.Items.Insert(0, li);
            drop.SelectedIndex = 0;
        }
        else
        {
            DropDownList drop = (DropDownList)nombreDrop;
            drop.DataSource = null;
            drop.DataBind();
            //drop.SelectedIndex = 0;
            drop.Items.Clear();
            ListItem li = new ListItem("Todos", "0");
            drop.Items.Add(li);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem l2 = new ListItem();
                l2.Value = dt.Rows[i][value].ToString();
                l2.Text = dt.Rows[i][text].ToString();
                drop.Items.Add(l2);
            }
            drop.SelectedIndex = 0;
        }
    }

    public string concatenaId(RadListBox listbox)
    /* Concatena los valores de checkbox seleccionados
     * para métodos de agregación a base de datos con
     * tablas de paso
     * */
    {
        IList<RadListBoxItem> collection = listbox.CheckedItems;
        string id = "";
        bool primero = true;
        foreach (RadListBoxItem item in collection)
        {
            if (!primero)
                id += ",";
            primero = false;
            id += item.Value;
        }
        return id;
    }

    public void CargaItemsRadList(RadListBox listbox, string ids)
    {
        listbox.ClearChecked();
        if(ids!="")
            foreach (string id in ids.Split(",".ToCharArray()))
                listbox.FindItemByValue(id).Checked = true;
    }

    public void CargaDrop(object nombreDrop, string value, string text, DataTable dt, string[] atributos =null )
    {
        if (nombreDrop is ComboBox)
        {
            ComboBox drop = (ComboBox)nombreDrop;
            drop.DataSource = null;
            drop.DataBind();
            //drop.SelectedIndex = 0;
            drop.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                drop.DataSource = dt;
                drop.DataValueField = value;
                drop.DataTextField = text;
                drop.DataBind();
            }
            ListItem li = new ListItem("Seleccione...", "0");
            li.Attributes.Add("Style", "font-weight:bold;");
            drop.Items.Insert(0, li);
            drop.SelectedIndex = 0;
        }
        else if (nombreDrop is RadComboBox)
        {

            RadComboBox drop = (RadComboBox)nombreDrop;
            drop.DataSource = null;
            drop.SelectedIndex = -1;
            drop.ClearSelection();
            drop.SelectedValue = null;
            drop.DataBind();
            drop.Items.Clear();
            RadComboBoxItem li = new RadComboBoxItem("Seleccione...", "0");
            drop.Items.Add(li);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RadComboBoxItem l2 = new RadComboBoxItem();
                l2.Value = dt.Rows[i][value].ToString();
                l2.Text = dt.Rows[i][text].ToString().ToUpper();
                drop.Items.Add(l2);
                if (atributos != null)
                    for (int j = 0; j < atributos.Length; j++)
                    {
                        l2.Attributes.Add(atributos[j], dt.Rows[i][atributos[j]].ToString());
                    }


            }
            drop.SelectedIndex = 0;


            if (drop.Items.Count == 2)
            {
                drop.SelectedIndex = 1;
            }
        }
        else
        {
            DropDownList drop = (DropDownList)nombreDrop;
            drop.DataSource = null;
            drop.SelectedIndex = -1;
            drop.ClearSelection();
            drop.SelectedValue = null;
            drop.DataBind();
            drop.Items.Clear();
            ListItem li = new ListItem("Seleccione...", "0");
            drop.Items.Add(li);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem l2 = new ListItem();
                l2.Value = dt.Rows[i][value].ToString();
                l2.Text = dt.Rows[i][text].ToString().ToUpper();
                drop.Items.Add(l2);
                if (atributos != null)
                for (int j=0; j < atributos.Length;j++)
                {
                    l2.Attributes.Add(atributos[j], dt.Rows[i][atributos[j]].ToString());
                }

                
            }
            drop.SelectedIndex = 0;


            if (drop.Items.Count == 2)
            {
                drop.SelectedIndex = 1;
            }
        }        
    }


    public void CargaDrop_patentes(object nombreDrop, string value, string text, DataTable dt, string[] atributos = null, string campo_marca = null, string valor_marca= null)
    {
        if (nombreDrop is ComboBox)
        {
            ComboBox drop = (ComboBox)nombreDrop;
            drop.DataSource = null;
            drop.DataBind();
            //drop.SelectedIndex = 0;
            drop.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                drop.DataSource = dt;
                drop.DataValueField = value;
                drop.DataTextField = text;
                drop.DataBind();
            }
            ListItem li = new ListItem("Seleccione...", "0");
            li.Attributes.Add("Style", "font-weight:bold;");
            drop.Items.Insert(0, li);
            drop.SelectedIndex = 0;
        }
        else
        {
            DropDownList drop = (DropDownList)nombreDrop;
            drop.DataSource = null;
            drop.SelectedIndex = -1;
            drop.ClearSelection();
            drop.SelectedValue = null;
            drop.DataBind();
            drop.Items.Clear();
            ListItem li = new ListItem("Seleccione...", "0");
            drop.Items.Add(li);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem l2 = new ListItem();
                l2.Value = dt.Rows[i][value].ToString();
                l2.Text = dt.Rows[i][text].ToString().ToUpper(); //+ " (" + dt.Rows[i][campo_marca].ToString().ToUpper() + ")";
                drop.Items.Add(l2);
                if (atributos != null)
                    for (int j = 0; j < atributos.Length; j++)
                    {
                        l2.Attributes.Add(atributos[j], dt.Rows[i][atributos[j]].ToString());
                    }
                if (campo_marca != null)
                {
                    if (dt.Rows[i][campo_marca].ToString() == valor_marca)
                    {
                        l2.Attributes.Add("class", "marcado");
                    }
                }
            }


            drop.SelectedIndex = 0;


            if (drop.Items.Count == 2)
            {
                drop.SelectedIndex = 1;
            }
        }
    }
    public void CargaDropDefaultValue(object nombreDrop, string value, string text, DataTable dt, string[] atributos = null)
    {
        DropDownList drop = (DropDownList)nombreDrop;
        drop.DataSource = null;
        drop.DataBind();
        //drop.SelectedIndex = 0;
        drop.Items.Clear();
        ListItem li = new ListItem("Seleccione...", "default");
        drop.Items.Add(li);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ListItem l2 = new ListItem();
            l2.Value = dt.Rows[i][value].ToString();
            l2.Text = dt.Rows[i][text].ToString().ToUpper();
            drop.Items.Add(l2);
            if (atributos != null)
                for (int j = 0; j < atributos.Length; j++)
                {
                    l2.Attributes.Add(atributos[j], dt.Rows[i][atributos[j]].ToString());
                }


        }
        drop.SelectedIndex = 0;


        if (drop.Items.Count == 2)
        {
            drop.SelectedIndex = 1;
        }
    }

    public void CargaDropNormal(object nombreDrop, string value, string text, DataTable dt)
    {
        if (nombreDrop is ComboBox)
        {
            ComboBox drop = (ComboBox)nombreDrop;
            drop.DataSource = null;
            drop.DataBind();
            //drop.SelectedIndex = 0;
            drop.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                drop.DataSource = dt;
                drop.DataValueField = value;
                drop.DataTextField = text;
                drop.DataBind();
            }
            drop.SelectedIndex = 0;
        }
        else
        {
            DropDownList drop = (DropDownList)nombreDrop;
            drop.DataSource = null;
            drop.DataBind();
            //drop.SelectedIndex = 0;
            drop.Items.Clear();
           
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem l2 = new ListItem();
                l2.Value = dt.Rows[i][value].ToString();
                l2.Text = dt.Rows[i][text].ToString().ToUpper();
                drop.Items.Add(l2);
            }
            drop.SelectedIndex = 0;
        }
    }

    public void CargaDropTodos(object nombreDrop, string value, string text, DataTable dt)
    {
        if (nombreDrop is ComboBox)
        {
            ComboBox drop = (ComboBox)nombreDrop;
            drop.DataSource = null;
            drop.DataBind();
            //drop.SelectedIndex = 0;
            drop.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                drop.DataSource = dt;
                drop.DataValueField = value;
                drop.DataTextField = text;
                drop.DataBind();
            }
            ListItem li = new ListItem("Todos...", "0");
            li.Attributes.Add("Style", "font-weight:bold;");
            drop.Items.Insert(0, li);
            drop.SelectedIndex = 0;
        }
        else
        {
            DropDownList drop = (DropDownList)nombreDrop;
            drop.DataSource = null;
            drop.DataBind();
            //drop.SelectedIndex = 0;
            drop.Items.Clear();
            ListItem li = new ListItem("Todos...", "0");
            drop.Items.Add(li);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem l2 = new ListItem();
                l2.Value = dt.Rows[i][value].ToString();
                l2.Text = dt.Rows[i][text].ToString().ToUpper();
                drop.Items.Add(l2);
            }
            drop.SelectedIndex = 0;
        }
    }

    public void LimpiarDrop(object nombreDrop)
    {
        CargaDrop(nombreDrop, "ID", "NOMBRE", new DataTable());
    }

    public string ConvertSortDirectionToSql(String order)
    {
        string newSortDirection = String.Empty;
        switch (order)
        {
            case "ASC":
                newSortDirection = "DESC";
                break;
            case "DESC":
                newSortDirection = "ASC";
                break;
            default:
                newSortDirection = "DESC";
                break;
        }

        return newSortDirection;
    }

    public string NuevaGeoRef(string direccion)
    {
        XmlDocument doc = new XmlDocument();
        XmlNodeList nodes;
        string lat = "";
        string lon = "";                    
        direccion = direccion.Replace(" ", "+");
        //' Load data  
        doc.Load("https://maps.google.com/maps/api/geocode/xml?address=" + direccion + "&sensor=true");

        nodes = doc.SelectNodes("/GeocodeResponse/result/geometry/location");
        string a = "";
        int i = 1;

        foreach (XmlNode node in nodes)
        {
            if (i == 1)
            {
                lat = node.FirstChild.InnerText;
                lon = node.LastChild.InnerText;
                i = i + 1;
            }
        }


        return (lat +"+"+ lon);
        
    }

    public string constructuirGeoURL(string direccion)
    {
        string geoURL = "";
        

        if (direccion != "")
        {
            direccion = direccion.Trim();
            direccion = direccion.Replace(" ", "+");

            geoURL = @"http://maps.google.com/maps/geo?q=###ADDRESS###&output=###OUTPUT###";//&key=###KEY###";

            //Sustitución de las variables
            geoURL = geoURL.Replace("###ADDRESS###", direccion);
            geoURL = geoURL.Replace("###OUTPUT###", "csv");
            //     geoURL = geoURL.Replace("###KEY###", "");


        }

        return geoURL;
    }

    public void cargarLatitudLatitud(string geoURL, TextBox txtLatitud, TextBox txtLongitud)
    {
        string csvValues = "";
        string Respuesta_geo = "";
        try
        {
            WebRequest objWebRequest = WebRequest.Create(geoURL);
            WebResponse objWebResponse = objWebRequest.GetResponse();
            Stream objWebStream = objWebResponse.GetResponseStream();

            using (StreamReader objStreamReader = new StreamReader(objWebStream))
            {
                csvValues = objStreamReader.ReadToEnd();
            }

            if (csvValues != null)
            {
                string[] geoValues = csvValues.Split(new char[] { ',' });
                if (geoValues.Length > 0)
                {
                    Respuesta_geo = geoValues[0].ToString();

                    if (Respuesta_geo == "200")
                    {
                        txtLatitud.Text = geoValues[2].ToString();
                        txtLongitud.Text = geoValues[3].ToString();
                        //Latitud = geoValues[2].ToString();
                        //Longitud = geoValues[3].ToString();
                    }
                }
            }

        }
        catch (Exception)
        {
            // Response.Write(exp.Message);
        }
    }

    public void LLenarComboNumeros(ComboBox combo, int cantidad )
    {
        ComboBox drop = (ComboBox)combo;
        drop.Items.Clear();
        for (int i = 0; i < cantidad; i++)
        {
            ListItem list = new ListItem();
            list.Value = i.ToString("D2");
            list.Text = i.ToString("D2");
            drop.Items.Add(list);
        }
        drop.SelectedIndex = 0;
    }

    public void LLenarDropNumeros(DropDownList combo, int cantidad)
    {
        DropDownList drop = (DropDownList)combo;
        drop.Items.Clear();
        for (int i = 0; i < cantidad; i++)
        {
            ListItem list = new ListItem();
            list.Value = i.ToString("D2");
            list.Text = i.ToString("D2");
            drop.Items.Add(list);
        }
        drop.SelectedIndex = 0;
    }

    public String llenarCerosIzquierda(long number, int cantidadDigitos) {
        return number.ToString("D" + cantidadDigitos);
    }

    //Formato de la fecha debe ser dd-MM-yyyy HH:mm
    public DateTime sumarHorasFecha(string fechaConHora, int horas)
    {
        int dia = Convert.ToInt32(fechaConHora.Substring(0, 2));
        int mes = Convert.ToInt32(fechaConHora.Substring(3, 2));
        int agno = Convert.ToInt32(fechaConHora.Substring(6, 4));
        int hora = Convert.ToInt32(fechaConHora.Substring(11, 2));
        int minu = Convert.ToInt32(fechaConHora.Substring(14, 2));
        DateTime fecha = new DateTime(agno, mes, dia, hora, minu, 0);
        fecha = fecha.AddHours(horas);
        return fecha;
    }

    public string buscarArchivo(string fileName)
    {
        string path = System.Web.Configuration.WebConfigurationManager.AppSettings["pathFiles"];
        // Create a reference to the current directory.
        DirectoryInfo di = new DirectoryInfo(path);
        // Create an array representing the files in the current directory.
        FileInfo[] fi = di.GetFiles();
        //Console.WriteLine("The following files exist in the current directory:");
        // Print out the names of the files in the current directory.
        foreach (FileInfo fiTemp in fi)
        {
            if (fiTemp.Name.Contains(fileName + "."))
            {
                return fiTemp.Extension;
            }
        }

        return null;
    }

    public string pathviewstate()
    {
        string path = System.Web.Configuration.WebConfigurationManager.AppSettings["viewstatefiles"];


        return path;
    }
}