// Example header text. Can be configured in the options.
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App2_Documento_Legal : System.Web.UI.Page
{
    UsuarioBC user;
    UtilsWeb utils = new UtilsWeb();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["usuario"] == null)
        {
            this.Response.Redirect("~/InicioTMS.aspx");
        }
        this.user = (UsuarioBC)this.Session["usuario"];
        if (!this.IsPostBack)
        {
            btn_procesar.Enabled = false;
        }
    }
    #region Buttons
    protected void UploadBtn_Click(object sender, EventArgs e)
    {
        string sExt = string.Empty;
        string sName = string.Empty;
        string Ruta1;
        bool ejecuto = false;
        Ruta1 = "../xls/";

        var RUTA = this.Server.MapPath(Ruta1);
        if (this.FileUpload1.HasFile)
        {
            sName = this.FileUpload1.FileName;
            sExt = Path.GetExtension(sName);
            if (this.ValidaExtension(sExt))
            {
                try
                {
                    this.FileUpload1.SaveAs(string.Format("{0}{1}", RUTA, sName));
                    foreach (string Archivo in Directory.GetFiles(RUTA, sName))
                    {
                        string Nombre = "";
                        string Extension = "";
                        Nombre = System.IO.Path.GetFileName(Archivo);
                        Extension = System.IO.Path.GetExtension(Archivo);
                        if (Extension == ".xlsx" || Extension == ".xls")
                        {
                            ejecuto = this.LeerArchivoExcel(string.Format("{0}{1}", RUTA, Nombre), "hoja1", Extension);
                        }
                    }
                    string[] xlsx = Directory.GetFiles(RUTA, sName);
                    foreach (string f in xlsx)
                    {
                        File.Delete(f);
                    }
                }
                catch (Exception ex)
                {
                    utils.ShowMessage(this, ex.Message, "error", false);
                    ejecuto = false;
                }

                if (ejecuto)
                {
                    utils.ShowMessage2(this, "cargaExcel", "success");
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseProgressbar", string.Format("HideProgressGD('{0}');", sExt), true);
                }
            }
        }
    }
    protected void btn_procesar_Click(object sender, EventArgs e)
    {
        SolicitudBC s = new SolicitudBC();
        try
        {
            string msj;
            if (s.ProcesarExcel(hf_codinterno.Value, user.ID, out msj))
                utils.ShowMessage2(this, "procesa", "success");
            else
                utils.ShowMessage(this, msj, "error", false);
        }
        catch (Exception ex)
        {
            utils.ShowMessage(this, ex.Message, "error", false);
        }
        finally
        {
            hf_codinterno.Value = "";
            gv_listar.DataSource = null;
            gv_listar.DataBind();
            btn_procesar.Enabled = false;
        }
    }
    #endregion
    #region GridView
    protected void gv_listar_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    #endregion
    private bool ValidaExtension(string sExtension)
    {
        switch (sExtension)
        {
            case ".xlsx":
                return true;
            case ".xls":
                return true;
            default:
                return false;
        }
    }
    private bool LeerArchivoExcel(string RutaCompleta, string Hoja, string extension)
    {
        try
        {
            string ConexionString = string.Format("Data Source={0}", RutaCompleta);

            if (extension == ".xls")
            {
                ConexionString += string.Format(";Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties='Excel 8.0;HDR=NO;IMEX=1'");
            }
            else
            {
                ConexionString += string.Format(";Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 12.0 Xml;HDR=NO;IMEX=1'");
            }

            using (OleDbConnection Conexion = new OleDbConnection(ConexionString))
            {
                string Sql = string.Format("SELECT * FROM [{0}$]", Hoja);
                OleDbDataAdapter Adapter = new OleDbDataAdapter(Sql, Conexion);
                DataTable DT = new DataTable("Excel");
                Adapter.Fill(DT);
                try
                {
                    DataTable dtIn = new DataTable();
                    int y = 0;
                    while (y < DT.Columns.Count)
                    {
                        if (String.IsNullOrWhiteSpace(DT.Rows[0][y].ToString())) break;
                        dtIn.Columns.Add(DT.Rows[0][y].ToString(), Type.GetType("System.String"));
                        if (!string.IsNullOrEmpty(DT.Rows[0][y].ToString()))
                            DT.Columns[y].ColumnName = DT.Rows[0][y].ToString();
                        else
                            break;
                        y++;
                    }
                    DT.Rows.RemoveAt(0);
                    foreach(DataRow dr in DT.Rows)
                    {
                        int x = 0;
                        while (x < y)
                        {
                            if (!String.IsNullOrWhiteSpace(dr[x].ToString()))
                            {
                                dtIn.ImportRow(dr);
                                break;
                            }
                            x++;
                        }
                    }
                    DataTable dtOut = new SolicitudBC().CargaExcel(dtIn, user.ID);
                    hf_codinterno.Value = dtOut.Rows[0]["COD_INTERNO"].ToString();
                    gv_listar.DataSource = dtOut;
                    gv_listar.DataBind();
                    btn_procesar.Enabled = true;
                }
                catch(Exception ex)
                {
                    btn_procesar.Enabled = false;
                    utils.ShowMessage(this, ex.Message, "error", false);
                }
                return true;
            }
        }
        catch (Exception)
        {
            btn_procesar.Enabled = false;
            utils.ShowMessage2(this, "cargaExcel", "error");
            return false;
        }
    }

    // viewstate
    protected override void SavePageStateToPersistenceMedium(object state)
    {
        string file = this.GenerateFileName();

        FileStream filestream = new FileStream(file, FileMode.Create);

        LosFormatter formator = new LosFormatter();

        formator.Serialize(filestream, state);

        filestream.Flush();

        filestream.Close();

        filestream = null;
    }

    protected override object LoadPageStateFromPersistenceMedium()
    {
        object state = null;
        try
        {
            StreamReader reader = new StreamReader(this.GenerateFileName());

            LosFormatter formator = new LosFormatter();

            state = formator.Deserialize(reader);

            reader.Close();
        }
        catch (Exception)
        {
            this.Response.Redirect(string.Format("{0}.aspx", Path.GetFileNameWithoutExtension(this.Page.AppRelativeVirtualPath)));
        }
        return state;
    }

    private string GenerateFileName()
    {
        string pageName = Path.GetFileNameWithoutExtension(this.Page.AppRelativeVirtualPath);

        string file = string.Format("{0}{1}.txt", pageName, this.Session.SessionID.ToString());

        //       file = Path.Combine(Server.MapPath("~/ViewStateFiles") + "/" + file);  
        file = string.Format("{0}\\{1}", this.utils.pathviewstate(), file);

        return file;
    }
}