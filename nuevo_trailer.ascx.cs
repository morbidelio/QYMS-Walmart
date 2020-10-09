using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nuevo_trailer : System.Web.UI.UserControl
{
    static UtilsWeb utils = new UtilsWeb();

    public event EventHandler ButtonClickDemo;

    public void Page_Load(object sender, EventArgs e)  
    {
        if (IsPostBack == false)
        {
            TransportistaBC tran = new TransportistaBC();
            TrailerTipoBC tipo = new TrailerTipoBC();
            utils.CargaDrop(ddl_editTran, "ID", "NOMBRE", tran.ObtenerTodos());
               CaractCargaBC catt = new CaractCargaBC();
            DataTable dt = catt.obtenerTodo();
            //   rlcli.DataSource = dt;
            //   rlcli.DataTextField = "DESCRIPCION";
            //  rlcli.DataValueField = "ID";
            //  rlcli.DataBind();
        }
    }

    public void limpiarForm()
    {
          hf_idTrailer.Value = "";
        //     txt_editCodigo.Text = "";
        txt_editPlaca.Text = "";
         ddl_editTran.ClearSelection();
         //rlcli.ClearChecked();
    }


    public void setplaca(string placa, Boolean importado )
    {
        hf_idTrailer.Value = "";
        //     txt_editCodigo.Text = "";
        txt_editPlaca.Text = placa;
        this.importado.Checked = importado;
        //rlcli.ClearChecked();
    }

  
  

    private void llenarForm(TrailerBC trailer)
    {
        //if (!String.IsNullOrEmpty(trailer.CARACTERISTICAS))
        //{
        //    string[] caracts = trailer.CARACTERISTICAS.Split(',');
        //    //rlcli.ClearChecked();
        //    //foreach (string c in caracts)
        //    //{
        //    //    //rlcli.FindItemByValue(c).Checked = true;
        //    //}
        //}
        //else
        //rlcli.ClearChecked();
        hf_idTrailer.Value = trailer.ID.ToString();
        //   txt_editCodigo.Text = trailer.CODIGO;
        txt_editPlaca.Text = trailer.PLACA;
        ddl_editTran.SelectedValue = trailer.TRAN_ID.ToString();
     }



    public void btn_editGrabar_Click(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        t.PLACA = txt_editPlaca.Text;
        t.TRAN_ID = int.Parse(ddl_editTran.SelectedValue);
        if (t.CrearGenerico(t,   importado.Checked ))
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer agregado correctamente.');", true);
            this.hf_idTrailer.Value = t.ID.ToString();
            limpiarForm();
            ButtonClickDemo(t, null);
            //      this.txt_buscarPatente.Text = t.PLACA;
            //     this.ddl_transportista.SelectedValue = t.ID_TRANSPORTISTA.ToString();
            //    btnBuscarTrailer_Click(null, null);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "cerrar", "$('#modalTrailer').modal('hide');", true);
        }
    }


    protected void txt_editPlaca_TextChanged(object sender, EventArgs e)
    {
        TrailerBC t = new TrailerBC();
        t = t.obtenerXPlaca(txt_editPlaca.Text);
        if (t.ID != 0 && t.ID != null)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "mensaje", "alert('Trailer ya existe en la base de datos!');", true);
            txt_editPlaca.Text = "";
            txt_editPlaca.Focus();
        }
    }
}