<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Movimiento_Trailer_petroleo.aspx.cs" Inherits="App_Movimiento_Trailer_petroleo" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <h2>
    Movimiento de Trailer
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <asp:UpdatePanel runat="server">
  <ContentTemplate>
  

  <div class="col-xs-12">
    <div class="col-lg-1 col-xs-4">
      N° Flota
      <br />
      <asp:TextBox ID="txt_buscarNro" CssClass="form-control" runat="server"    ></asp:TextBox>
      <asp:CompareValidator ID="CompareValidator1" runat="server"    ControlToValidate="txt_buscarNro"   Operator="DataTypeCheck"  Type="Integer"   ValidationGroup="trailer1"
                            ErrorMessage="Ingrese un Número"  Display="Dynamic"  Text="*" />
      <asp:CompareValidator ID="CompareValidator2" runat="server"    ControlToValidate="txt_buscarNro"   Operator="GreaterThanEqual"  ValidationGroup="trailer1"
                            ValueToCompare="0"   Type="Integer"   ErrorMessage="ingrese un Número mayor a 0"  Display="Dynamic" Text="*" />
      <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" PopupPosition="BottomLeft"
                                    Enabled="True" TargetControlID="CompareValidator1">
      </asp:ValidatorCalloutExtender>
      <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                                    Enabled="True" TargetControlID="CompareValidator2">
      </asp:ValidatorCalloutExtender>
    </div>
    <div class="col-lg-1 col-xs-4">
      Patente
      <br />
      <asp:TextBox ID="txt_buscarPatente" CssClass="form-control" runat="server" MaxLength="6"  ></asp:TextBox>
    </div>
    <div class="col-lg-1 col-xs-2">
      <br />
      <asp:LinkButton ID="btn_buscarTrailer" CssClass="btn btn-primary" ValidationGroup="trailer1"
                      ToolTip="Buscar Trailer" runat="server" onclick="btn_buscarTrailer_Click">
        <span class="glyphicon glyphicon-search"></span>
      </asp:LinkButton>
    </div>
  </div>
  </ContentTemplate></asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" Runat="Server">
  <div class="col-lg-4 col-xs-12">
    <asp:Panel ID="SITE" runat="server" >
      <div class="col-lg-4 col-xs-5">
        Site
        <br />
        <asp:DropDownList CssClass="form-control" runat="server" id="dropsite" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="drop_SelectedIndexChanged" ></asp:DropDownList>
      </div>
    </asp:Panel>
    <div class="col-lg-3 col-xs-4">
      Fecha
      <br />
      <asp:TextBox CssClass="form-control" ID="txt_fechaMovimiento" Width="90px" runat="server"></asp:TextBox>
    </div>
    <div class="col-lg-5 col-xs-4">
      Ref. Operador
      <br />
      <asp:TextBox ID="txt_refOp" CssClass="form-control" runat="server"></asp:TextBox>
    </div>

   
  </div>
  <div class="col-lg-6 col-xs-12">
    <div class="col-lg-3 col-xs-3">
      Propio
      <br />
      <asp:RadioButton ID="rb_trailerPropio" GroupName="rb_trailer" runat="server" Enabled="false" />
    </div>
    <div class="col-lg-3 col-xs-3">
      Externo
      <br />
      <asp:RadioButton ID="rb_trailerExterno" GroupName="rb_trailer" runat="server" Enabled="false" />
    </div>
    <div class="col-lg-3 col-xs-3">
     Petroleo
      <br />
      <asp:CheckBox ID="petroleo" runat="server" Checked="false" OnCheckedChanged="petroleo_checked" AutoPostBack="true" />
    </div>

    <div class="col-lg-3 col-xs-3">
      Transporte
      <br />
      <asp:DropDownList ID="ddl_transportista" CssClass="form-control" runat="server" Enabled="false">
        <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
      </asp:DropDownList>
    </div>
  </div>
  <div class="col-xs-12 separador"></div>
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <div class="col-xs-12">
        <div class="col-lg-6 col-xs-12">
          <h4>
            Origen
          </h4>
          <div class="col-xs-3">
            <asp:Panel ID="pnl_lugarOrigen" CssClass="lugar lugar-especifico" runat="server">
              <asp:Panel ID="pnl_detalleLugar" CssClass="row columna-anden detalle-lugar-especifico" runat="server">
                <asp:Label ID="lbl_lugar" runat="server"></asp:Label>
              </asp:Panel>
              <asp:Panel ID="pnl_detalleTrailer" CssClass="row columna-anden detalle-trailer-especifico" runat="server">
                  <asp:Panel style="width:20px;border-radius:20px;" ID="pnl_imgAlerta" runat="server">
                    <img style="width:20px;border-radius:20px;" src="../img/reloj.png" />
                  </asp:Panel>
                <%--<asp:Image ID="img_reloj" Width="17px" runat="server" />--%>
                <asp:Image ID="img_trailer" Width="20px" runat="server" />
              </asp:Panel>
            </asp:Panel>
          </div>
          <div id="dv_origen" class="col-xs-9">
            Zona: 
            <asp:Label ID="lbl_origenZona" runat="server"></asp:Label>
            <br />
            Playa: 
            <asp:Label ID="lbl_origenPlaya" runat="server"></asp:Label>
          </div>
        </div>
        <div class="col-lg-6 col-xs-12">
        <asp:Panel ID="pnl_destino" runat="server">
          <h4>
            Destino
          </h4>
          <div class="col-xs-12">
            <div class="col-lg-3 col-xs-4">Zona </div>
            <div class="col-lg-5 col-xs-8">
              <asp:DropDownList ID="ddl_destinoZona" runat="server" AutoPostBack="true" enabled="false"
                                onselectedindexchanged="ddl_destinoZona_SelectedIndexChanged" CssClass="form-control">
                <asp:ListItem>Seleccione...</asp:ListItem>
              </asp:DropDownList>
            </div>
          </div>
          <div class="col-xs-12 separador"></div>
          <div class="col-xs-12">
            <div class="col-lg-3 col-xs-4">Playa </div>
            <div class="col-lg-5 col-xs-8">
              <asp:DropDownList ID="ddl_destinoPlaya" Enabled="false" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddl_destinoPlaya_SelectedIndexChanged" CssClass="form-control">
                <asp:ListItem>Seleccione...</asp:ListItem>
              </asp:DropDownList>
            </div>
          </div>
          <div class="col-xs-12 separador"></div>
          <div class="col-xs-12">
            <div class="col-lg-3 col-xs-4">Posición </div>
            <div class="col-lg-5 col-xs-8">
              <asp:DropDownList ID="ddl_destinoPos" Enabled="false" runat="server" CssClass="form-control">
                <asp:ListItem Value="0">Seleccione...</asp:ListItem>
              </asp:DropDownList>
              <asp:RequiredFieldValidator ID="rfv_destinoPos" runat="server" ControlToValidate="ddl_destinoPos" 
                                          Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0"
                                          ValidationGroup="movimiento">
              </asp:RequiredFieldValidator>
              <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                            Enabled="True" TargetControlID="rfv_destinoPos">
              </asp:ValidatorCalloutExtender>
            </div>
          </div>
          </asp:Panel>
        </div>
      </div>
      <div class="col-xs-12 separador"></div>
  <div class="col-xs-12">
    <center>
      <asp:Button ID="btn_actualizar" CssClass="btn btn-primary" runat="server" Text="Actualizar Playa"  Visible="false"/>
      <asp:Button ID="btn_confirmar" CssClass="btn btn-primary" runat="server" ValidationGroup="movimiento"
                  Text="Confirmar Movimiento" onclick="btn_confirmar_Click" />
    </center>
  </div>
    </ContentTemplate>
  </asp:UpdatePanel>
  
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" Runat="Server">
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hf_idTrailer" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
    $.datetimepicker.setLocale('es'); /*cambia el lenguaje predeterminado del calendario a español*/
    /*Funcion para los calendarios dentro de un updatepanel*/
    function EndRequestHandler(sender, args) {
        $('#<%= txt_fechaMovimiento.ClientID %>').datetimepicker({ dayofweekstart: 1, format: "d-m-Y", weeks: true, timepicker: false });
        // Función jquery para ingresar sólo números (válido para todos los elementos de clase css input-number)
        $('.input-number').on('input', function () {
            this.value = this.value.replace(/[^0-9]/g, '');
        });
    }


    $(document).ready(function () {
        $(window).keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                var button = document.getElementById("<% =this.btn_buscarTrailer.ClientID %>");
                //                __doPostBack("<% =this.btn_buscarTrailer.ClientID %>".replace("_","$") ,'');
                //                $("#<% =this.btn_buscarTrailer.ClientID %>").click();
                button.click();
                return true;
            }
        });
    });


    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
  </script>
</asp:Content>
