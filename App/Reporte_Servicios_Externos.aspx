<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
CodeFile="Reporte_Servicios_Externos.aspx.cs" Inherits="App_Reporte_servicios_externos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Reporte Servicios Externos
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
  <div class="col-xs-12 separador">
  </div>
      <div class="container-fluid filtro">
        <div class="col-xs-12">
        <div class="col-xs-2">
        Site
        <br />
        <asp:DropDownList ID="ddl_site" CssClass="form-control" OnSelectedIndexChanged="ddl_site_SelectedIndexChanged" runat="server">
        </asp:DropDownList>
      </div>
       <div class="col-xs-1">
    Desde
    <br />
    <asp:TextBox ID="txt_desde" CssClass="form-control input-fecha" runat="server" />
  </div>
  <div class="col-xs-1">
    Hasta
    <br />
    <asp:TextBox ID="txt_hasta" CssClass="form-control input-fecha" runat="server" />
  </div>

      <div class="col-xs-2 ocultar" >
        Proveedor
        <br />
        <asp:DropDownList ID="ddl_buscarTransportista" CssClass="form-control" Width="90%" Visible="false"
                          runat="server">
        </asp:DropDownList>
      </div>
      <div class="col-xs-2">
        Placa
        <br />
        <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
      </div>
      <div class="col-xs-2">
       Entrada/Salida
        <br />
    
        <asp:DropDownList ID="chk_buscarInternos" CssClass="form-control" Width="90%" Visible="true"
                          runat="server">
                          <asp:ListItem Value="-1" Text="Todos"></asp:ListItem>
                           <asp:ListItem Value="1" Text="Entrada"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Salida"></asp:ListItem>
        </asp:DropDownList>   
      </div>
      <div class="col-xs-2">
        <br />
        <asp:LinkButton ID="btn_buscarTrailer" OnClick="btn_buscarTrailer_Click" CssClass="btn btn-primary"  
                        ToolTip="Buscar Trailer" runat="server">
          <span class="glyphicon glyphicon-search"></span>
        </asp:LinkButton>
        <asp:LinkButton ID="btn_export"  ToolTip="Exportar a Excel" CssClass="btn btn-primary"  OnClick="btn_export_Click" runat="server">
          <span class="glyphicon glyphicon-import"></span>
        </asp:LinkButton>
      </div>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server" class="container-fluid col-xs-12 cuerpo">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="col-xs-12">

        <asp:GridView ID="gv_listar" runat="server" AllowPaging="false" AllowSorting="true" OnSorting="gv_listar_Sorting" OnRowCreated="gv_listar_RowCreated"
                      EmptyDataText="No se encontraron servicios externos!" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-hover tablita">
          <Columns>
            <asp:BoundField DataField="PATENTE" SortExpression="PATENTE" HeaderText="Patente" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
            <asp:BoundField DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" HeaderText="Transportista" HeaderStyle-Width="300px" ItemStyle-Width="300px" Visible="false" />
            <asp:BoundField DataField="SITE" SortExpression="SITE" HeaderText="Site" HeaderStyle-Width="80px" ItemStyle-Width="80px"  />
             <asp:BoundField DataField="servicio" SortExpression="servicio" HeaderText="servicio" HeaderStyle-Width="80px" ItemStyle-Width="80px"  />
              <asp:BoundField DataField="FECHA" SortExpression="FECHA" HeaderText="Fecha"  />
            <asp:BoundField DataField="HORA" SortExpression="HORA" HeaderText="Hora" />
            <asp:BoundField DataField="DIAS" SortExpression="DIAS" HeaderText="Días en CD" />
            <asp:BoundField DataField="USUARIO" SortExpression="USUARIO" HeaderText="Usuario" />
            <asp:BoundField DataField="CONDUCTOR_RUT" SortExpression="CONDUCTOR_RUT" HeaderText="RUT Conductor" />
            <asp:BoundField DataField="CONDUCTOR_NOMBRE" SortExpression="CONDUCTOR_NOMBRE" HeaderText="Nombre Conductor" />
            <asp:BoundField DataField="Entrada" SortExpression="Entrada" HeaderText="Entrada/Salida" />
          </Columns>
        </asp:GridView>
      </div>

    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hf_idTrailer" runat="server" />
      <asp:HiddenField ID="hf_excluyentes" Value="" runat="server" />
      <asp:HiddenField ID="hf_noexcluyentes" Value="" runat="server" />
      <asp:Button ID="btnExportar" runat="server" CssClass="ocultar" OnClick="btnExportar_Click" />
    </ContentTemplate>
    <Triggers >
      <asp:PostBackTrigger ControlID="btnExportar"  />
    </Triggers>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
  <script type="text/javascript">
      function EndRequestHandler(sender, args) {
          setTimeout(tabla, 200);
      }
      Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    function Exportar() {
        $get("<%=this.btnExportar.ClientID %>").click();
    }

    function tabla() {
        if ($('#<%= this.gv_listar.ClientID %>')[0] != undefined && $('#<%= this.gv_listar.ClientID %>')[0].rows.length > 1) {
            $('#<%= this.gv_listar.ClientID %>').DataTable({
                "scrollY": "320px",
                "scrollX": true,
                "paging": false,
                "ordering": false,
                "searching": false,
                "lengthChange": false
            });
        }
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
  </script>
</asp:Content>