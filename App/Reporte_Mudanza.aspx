<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Mudanza.aspx.cs" Inherits="App_Reporte_Entrada_salida" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">

  <div class="col-xs-12 separador"></div>
  <h2>
    Reporte Mudanzas
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <div class="container-fluid filtro">
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
     <div class="col-xs-2" >
      Transportista
      <br />
       <telerik:RadComboBox rendermode="Lightweight" ID="ddl_buscarTransportista" runat="server" CollapseAnimation-Duration="0" Filter="Contains"
                                    AllowCustomText="true" MarkFirstMatch="true" EmptyMessage="Seleccione transportista" ExpandAnimation-Duration="0" Width="90%" />
           
    </div>
    <div class="col-xs-1">
      Placa
      <br />
      <asp:TextBox ID="txtplaca" CssClass="form-control" runat="server" />
    </div>

    <div class="col-xs-2">
      Rut Conductor
      <br />
      <asp:TextBox ID="txt_rut" CssClass="form-control" runat="server" />
    </div>
    <div class="col-xs-1">
      <br />
      <asp:LinkButton id="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
        <span class="glyphicon glyphicon-search"></span>
      </asp:LinkButton>
      <asp:LinkButton id="btn_exportar" CssClass="btn btn-primary" OnClick="btn_export_Click" runat="server">
        <span class="glyphicon glyphicon-import"></span>
      </asp:LinkButton>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server" class="container-fluid col-xs-12 cuerpo">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <asp:GridView ID="gv_listar" onsorting="gv_listar_Sorting" OnRowCreated="gv_listar_RowCreated" AllowSorting="true"
                    emptydatatext="No hay ingresos!" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" runat="server" >
        <Columns>
          <asp:BoundField DataField="PROVEEDOR" SortExpression="PROVEEDOR" HeaderText="Origen Mercaderia"  visible="true"/>
            <asp:BoundField DataField="SITE" SortExpression="SITE" HeaderText="CD Destino" HeaderStyle-Width="80px" ItemStyle-Width="80px" visible="true"  />
            <asp:BoundField DataField="CITA" SortExpression="CITA" HeaderText="N° Cita" visible="true" />

          <asp:BoundField DataField="PATENTE" SortExpression="PATENTE" HeaderText="Patente Trailer" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
          <asp:BoundField DataField="PATENTE_TRACTO" SortExpression="PATENTE_TRACTO" HeaderText="Patente Tracto" HeaderStyle-Width="80px" ItemStyle-Width="80px" />
          <asp:BoundField DataField="TRANSPORTISTA_TRACTO" SortExpression="TRANSPORTISTA_TRACTO" HeaderText="Transportista Tracto" HeaderStyle-Width="300px" ItemStyle-Width="300px" Visible="false" />

          <asp:BoundField DataField="FECHA" SortExpression="FECHA" HeaderText="Fecha"  />
          <asp:BoundField DataField="HORA" SortExpression="HORA" HeaderText="Hora" />
         
          <asp:BoundField DataField="USUARIO" SortExpression="USUARIO" HeaderText="Usuario Ingreso" Visible="false"/>
          <asp:BoundField DataField="CONDUCTOR_RUT" SortExpression="CONDUCTOR_RUT" HeaderText="RUT Conductor" />
          <asp:BoundField DataField="CONDUCTOR_NOMBRE" SortExpression="CONDUCTOR_NOMBRE" HeaderText="Nombre Conductor" />
              <asp:BoundField DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" HeaderText="Transportista" HeaderStyle-Width="300px" ItemStyle-Width="300px" Visible="true"/>
              <asp:BoundField DataField="observacion" SortExpression="observacion" HeaderText="observacion" visible="true" />
           

          <asp:BoundField DataField="SELLO" SortExpression="SELLO" HeaderText="Sello"  visible="true"/>
            <asp:BoundField DataField="DIAS" SortExpression="DIAS" HeaderText="salida/Llegada" Visible="true" />

          <asp:BoundField DataField="salida_FECHA_EGRESO" SortExpression="salida_FECHA_EGRESO" HeaderText="Fecha Egreso"  />
          <asp:BoundField DataField="salida_HORA_EGRESO" SortExpression="salida_HORA_EGRESO" HeaderText="Hora Egreso" />
            <asp:BoundField DataField="estado_actual" SortExpression="estado_actual" HeaderText="Estado Actual" visible="true" />
       


           <asp:BoundField DataField="TIEMPO_EN_DESCARGA" SortExpression="TIEMPO_EN_DESCARGA" HeaderText="Tiempo En Descarga" />
                   
        </Columns>
        <EditRowStyle HorizontalAlign="Center" />
        <HeaderStyle CssClass="header-color2" />
      </asp:GridView>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <asp:Button ID="btnExportar" runat="server" CssClass="ocultar" OnClick="btnExportar_Click" />
    </ContentTemplate>
    <Triggers >
      <asp:PostBackTrigger ControlID="btnExportar"  />
    </Triggers>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript" src="../js2/jquery.dataTables.min.js"></script>
  <link rel="stylesheet" href="../js2/css/defaultTheme.css" media="screen" />
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
  </script>
</asp:Content>

