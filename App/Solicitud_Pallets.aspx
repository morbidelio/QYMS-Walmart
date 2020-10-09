<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
CodeFile="Solicitud_Pallets.aspx.cs" Inherits="App_Solicitud_Pallets" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Solicitud Pallets
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <asp:UpdatePanel runat="server" id="upfiltros">
    <ContentTemplate>
      
      <div class="col-lg-4 col-xs-12" style="padding:0">
        <asp:Panel ID="SITE" runat="server">
          <div class="col-lg-5 col-xs-5">
            Site
            <br />
            <asp:DropDownList runat="server" ID="dropsite" ClientIDMode="Static" AutoPostBack="true"
                              OnSelectedIndexChanged="drop_SelectedIndexChanged" CssClass="form-control">
            </asp:DropDownList>
          </div>
        </asp:Panel>
        <div class="col-lg-4 col-xs-4">
          Fecha
          <br />
          <asp:TextBox CssClass="form-control input-fecha" ID="txt_buscarFecha" runat="server" Width="90px"></asp:TextBox>
        </div>
        <div class="col-lg-3 col-xs-3">
          Hora
          <br />
          <asp:TextBox CssClass="form-control input-hora" ID="txt_buscarHora" MaxLength="4" Width="60px"
                       runat="server"></asp:TextBox>
        </div>
      </div>
      <div class="col-lg-6 col-xs-12" style="padding:0">
<%--        <div class="col-lg-3 col-xs-5">
          N° Flota
          <br />
          <asp:TextBox ID="txt_buscarNro" CssClass="form-control input-number" runat="server" Width="100px"></asp:TextBox>
          <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txt_buscarNro"
                                Operator="GreaterThanEqual" ValidationGroup="trailer1" ValueToCompare="0" Type="Integer"
                                ErrorMessage="ingrese un Número mayor a 0" Display="Dynamic" Text="*" />
          <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                                        Enabled="True" TargetControlID="CompareValidator2">
          </asp:ValidatorCalloutExtender>
        </div>--%>
        <div class="col-lg-3 col-xs-5">
          Patente
          <br />
          <asp:DropDownList ID="ddl_buscarPatente" OnSelectedIndexChanged="ddl_buscarPatente_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
          </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv_patente" runat="server" ControlToValidate="ddl_buscarPatente" InitialValue="0"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="buscar">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="vce_patente" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_patente">
            </asp:ValidatorCalloutExtender>
          <%--<asp:TextBox CssClass="form-control" ID="txt_buscarPatente" MaxLength="6" Width="90%"
          runat="server"></asp:TextBox>--%>
        </div>
<%--        <div class="col-xs-1">
          <br />
          <asp:LinkButton ID="btnBuscar" CssClass="btn btn-primary" ValidationGroup="buscar"
                          ToolTip="Buscar Descarga" runat="server" OnClick="btnBuscar_Click">
            <span class="glyphicon glyphicon-search"></span>
          </asp:LinkButton>
        </div>--%>
      </div>
       
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      
      <div class="col-lg-1 col-xs-5">
        N° Flota
        <br />
        <asp:TextBox ID="txt_nroFlota" Enabled="false" CssClass="form-control"
                     runat="server"></asp:TextBox>
      </div>
      <div class="col-lg-3 col-xs-7">
        Transporte
        <br />
        <asp:TextBox ID="txt_transporte" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
      </div>
      <div class="col-xs-12 separador">
      </div>
      <div class="col-xs-12 col-lg-3">
        <h4>
          Origen
        </h4>
        <div class="col-lg-3 col-xs-4">
          <asp:Panel ID="pnl_lugarOrigen" CssClass="lugar-especifico" runat="server" style="">
            <asp:Panel ID="pnl_detalleLugar" CssClass="row columna-anden detalle-lugar-especifico" runat="server">
              <asp:Label ID="lbl_lugar" runat="server"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="pnl_detalleTrailer" CssClass="row columna-anden detalle-trailer-especifico" runat="server">
              <asp:Panel style="width:20px;border-radius:20px;" ID="pnl_imgAlerta" runat="server">
                <img style="width:20px;border-radius:20px;" src="../img/reloj.png" />
              </asp:Panel>
              <%--<asp:Image ID="img_reloj" ImageUrl="../img/reloj.png" Width="25px" runat="server" />--%>
              <asp:Image ID="img_trailer" Width="20px" runat="server" />
            </asp:Panel>
          </asp:Panel>
        </div>
        <div id="dv_origen" class="col-lg-9 col-xs-8">
          Zona:
          <asp:Label ID="lbl_origenZona" runat="server"></asp:Label>
          <br />
          Playa:
          <asp:Label ID="lbl_origenPlaya" runat="server"></asp:Label>
        </div>
      </div>
      <div class="col-lg-3 col-xs-12">
        <h4>
          Anden Origen
        </h4>
           
        <div class="col-xs-4">
          Zona
        </div>
        <div class="col-xs-8">
          <asp:DropDownList ID="ddl_destinoZona1" CssClass="form-control" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddl_destinoZona1_SelectedIndexChanged" Enabled="False">
            <asp:ListItem>Seleccione...</asp:ListItem>
          </asp:DropDownList>
        </div>
            
        <div class="col-xs-12 separador">
        </div>
           
        <div class="col-xs-4">
          Playa
        </div>
        <div class="col-xs-8">
          <asp:DropDownList ID="ddl_destinoPlaya1" CssClass="form-control" runat="server" Enabled="false"
                            AutoPostBack="true" OnSelectedIndexChanged="ddl_destinoPlaya1_SelectedIndexChanged">
            <asp:ListItem>Seleccione...</asp:ListItem>
          </asp:DropDownList>
        </div>
            
        <div class="col-xs-12 separador">
        </div>
           
        <div class="col-xs-4">
          Posición
        </div>
        <div class="col-xs-8">
          <asp:DropDownList ID="ddl_destinoPos1" CssClass="form-control" Enabled="false" runat="server">
            <asp:ListItem>Seleccione...</asp:ListItem>
          </asp:DropDownList>
        </div>
            
      </div>
      <div class="col-lg-3 col-xs-12">
        <h4>
          Anden Destino
        </h4>
        <div class="col-xs-12">
          <div class="col-xs-4">
            Zona
          </div>
          <div class="col-xs-8">
            <asp:DropDownList ID="ddl_destinoZona2" CssClass="form-control" runat="server" AutoPostBack="true"
                              OnSelectedIndexChanged="ddl_destinoZona2_SelectedIndexChanged" Enabled="False">
              <asp:ListItem>Seleccione...</asp:ListItem>
            </asp:DropDownList>
          </div>
        </div>
        <div class="col-xs-12 separador">
        </div>
        <div class="col-xs-12">
          <div class="col-xs-4">
            Playa
          </div>
          <div class="col-xs-8">
            <asp:DropDownList ID="ddl_destinoPlaya2" CssClass="form-control" runat="server" Enabled="false"
                              AutoPostBack="true" OnSelectedIndexChanged="ddl_destinoPlaya2_SelectedIndexChanged">
              <asp:ListItem>Seleccione...</asp:ListItem>
            </asp:DropDownList>
          </div>
        </div>
        <div class="col-xs-12 separador">
        </div>
        <div class="col-xs-12">
          <div class="col-xs-4">
            Posición
          </div>
          <div class="col-xs-8">
            <asp:DropDownList ID="ddl_destinoPos2" CssClass="form-control" Enabled="false" runat="server">
              <asp:ListItem>Seleccione...</asp:ListItem>
            </asp:DropDownList>
          </div>
        </div>
      </div>
       
      <div class="col-xs-12 separador">
      </div>
      <div class="col-xs-8">
        <center>
          <asp:LinkButton ID="btn_limpiar" CssClass="btn btn-primary" runat="server" Tooltip="Limpiar"
                          Visible="true" OnClick="limpia" >
            <span class="glyphicon glyphicon-erase"></span>
          </asp:LinkButton>
          <asp:LinkButton ID="btn_anular" CssClass="btn btn-primary" runat="server" Tooltip="Anular Solicitud"
                          Visible="false" >
            <span class="glyphicon glyphicon-trash"></span>
          </asp:LinkButton>
          <asp:LinkButton ID="btn_confirmar" CssClass="btn btn-primary" runat="server" Tooltip="Confirmar Movimiento"
                          OnClick="btn_confirmar_Click" >
            <span class="glyphicon glyphicon-ok"></span>
          </asp:LinkButton>
        </center>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hf_trailerId" runat="server" />
      <asp:HiddenField ID="hf_soliId" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
  <script type="text/javascript">
  </script>
</asp:Content>
