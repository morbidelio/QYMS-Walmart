using System;
using System.Net;

public partial class Master_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Usuario"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
       // DownloadString("");
    }


    public static void DownloadString (string address)
{
    WebClient client = new WebClient ();
    string reply = client.DownloadString ("http://www.itruck.cl/telemetrica/ppp/fepasakmz.asp?TM=302909");
        
//    Console.WriteLine (reply);
    }
}
