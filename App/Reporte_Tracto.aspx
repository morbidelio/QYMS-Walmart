<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
CodeFile="Reporte_Tracto.aspx.cs" Inherits="App_Reporte_tracto" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Reporte de Tracto
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <div class="container-fluid filtro">
    <div class="col-xs-2">
      Transportista
      <br />
      <asp:DropDownList ID="ddl_buscarTransportista" CssClass="form-control" Width="90%"
                        runat="server">
      </asp:DropDownList>
    </div>
    <div class="col-xs-2">
      Placa
      <br />
      <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
    </div>
    <div class="col-lg-1">
      Solo en CD
      <br />
      <asp:CheckBox id="chk_buscarInternos" runat="server" />
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
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server" class="container-fluid col-xs-12 cuerpo">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <asp:GridView ID="gv_listar" onsorting="gv_listar_Sorting" OnRowCreated="gv_listar_RowCreated" AllowSorting="true"
                    emptydatatext="No se encontraron tractos!" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" runat="server" >
        <Columns>
          <asp:BoundField DataField="PATENTE" SortExpression="PATENTE" HeaderText="Patente" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
          <asp:BoundField DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" HeaderText="Transportista" HeaderStyle-Width="300px" ItemStyle-Width="300px" />
          <asp:BoundField DataField="SITE" SortExpression="SITE" HeaderText="Site" HeaderStyle-Width="80px" ItemStyle-Width="80px"  />
          <asp:BoundField DataField="ZONA" SortExpression="ZONA" HeaderText="Zona" HeaderStyle-Width="140px" ItemStyle-Width="140px"  />
          <asp:BoundField DataField="PLAYA" SortExpression="PLAYA" HeaderText="Playa" HeaderStyle-Width="250px" ItemStyle-Width="250px"  />
          <asp:BoundField DataField="LUGAR" SortExpression="LUGAR" HeaderText="Posición" HeaderStyle-Width="70px" ItemStyle-Width="70px"  />
          <asp:BoundField DataField="ESTADO" SortExpression="ESTADO" HeaderText="Estado" HeaderStyle-Width="155px" ItemStyle-Width="155px" />
          <asp:BoundField DataField="FECHA" SortExpression="FECHA" HeaderText="Fecha"  />
          <asp:BoundField DataField="HORA" SortExpression="HORA" HeaderText="Hora" />
          <asp:BoundField DataField="DIAS" SortExpression="DIAS" HeaderText="Días en CD" />
          <asp:BoundField DataField="USUARIO" SortExpression="USUARIO" HeaderText="Usuario Ingreso" />
          <asp:BoundField DataField="CONDUCTOR_RUT" SortExpression="CONDUCTOR_RUT" HeaderText="RUT Conductor" />
          <asp:BoundField DataField="CONDUCTOR_NOMBRE" SortExpression="CONDUCTOR_NOMBRE" HeaderText="Nombre Conductor" />
          <asp:BoundField DataField="CITA" SortExpression="CITA" HeaderText="Nro Cita" />
          <asp:BoundField DataField="DOCUMENTO" SortExpression="DOCUMENTO" HeaderText="Documento" />
          <asp:BoundField DataField="PROVEEDOR" SortExpression="PROVEEDOR" HeaderText="Proveedor" />
          <asp:BoundField DataField="SELLO" SortExpression="SELLO" HeaderText="Sello" />
          <asp:BoundField DataField="TIPO_CARGA" SortExpression="TIPO_CARGA" HeaderText="Tipo Carga" />
          <asp:BoundField DataField="MOTIVO" SortExpression="MOTIVO" HeaderText="Motivo" />
            <asp:BoundField DataField="SITE_IN" SortExpression="SITE_IN" HeaderText="En CD" />
        </Columns>
        <EditRowStyle HorizontalAlign="Center" />
        <HeaderStyle CssClass="header-color2" />
      </asp:GridView>
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

    function EndRequestHandler(sender, args) {
        setTimeout(tabla, 200);
    }

    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);

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