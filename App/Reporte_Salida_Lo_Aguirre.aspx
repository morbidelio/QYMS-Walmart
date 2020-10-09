<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Salida_Lo_Aguirre.aspx.cs" Inherits="App_Reporte_Salida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">

    <div class="col-xs-12 separador"></div>
    <h2>Reporte Salidas</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador"></div>
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
    <div class="col-xs-1">
        <br />
        <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
      <span class="glyphicon glyphicon-search"></span>
        </asp:LinkButton>
        <asp:LinkButton ID="btn_exportar" CssClass="btn btn-primary" OnClick="btn_export_Click" runat="server">
      <span class="glyphicon glyphicon-export"></span>
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gv_listar" OnSorting="gv_listar_Sorting" OnRowCreated="gv_listar_RowCreated" AllowSorting="true"
                EmptyDataText="No hay ingresos!" Width="100%"
                CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:BoundField DataField="FECHA_EGRESO" SortExpression="FECHA_EGRESO" HeaderText="Fecha Egreso" />
                    <asp:BoundField DataField="HORA_EGRESO" SortExpression="HORA_EGRESO" HeaderText="Hora Egreso" />
                    <asp:BoundField DataField="TIPO_DESTINO" SortExpression="TIPO_DESTINO" HeaderText="Tipo Destino" />
                    <asp:BoundField DataField="DESTINO" SortExpression="DESTINO" HeaderText="Destino" />
                    
                    <asp:BoundField DataField="RUTA" SortExpression="RUTA" HeaderText="Ruta" />
                    <asp:BoundField DataField="PATENTE_TRACTO" SortExpression="PATENTE_TRACTO" HeaderText="Patente Tracto" />
                    <asp:BoundField DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" HeaderText="Transportista" />
                    <asp:BoundField DataField="PATENTE" SortExpression="PATENTE" HeaderText="Patente" />
                    <asp:BoundField DataField="NRO_FLOTA" SortExpression="NRO_FLOTA" HeaderText="Nro Flota" />
                    <asp:BoundField DataField="CONDUCTOR_RUT" SortExpression="CONDUCTOR_RUT" HeaderText="Rut Conductor" />
                    <asp:BoundField DataField="CONDUCTOR_NOMBRE" SortExpression="CONDUCTOR_NOMBRE" HeaderText="Conductor" />
                    <asp:BoundField DataField="USUARIO_SALIDA" SortExpression="USUARIO_SALIDA" HeaderText="Usuario" />
                    <asp:BoundField DataField="TEMPERATURA" SortExpression="TEMPERATURA" HeaderText="Temperatura" />
                    <asp:BoundField DataField="GPS" SortExpression="GPS" HeaderText="GPS" />
                    <asp:BoundField DataField="ESTADO_CARGA" SortExpression="ESTADO_CARGA" HeaderText="Estado Carga" />
                    <asp:BoundField DataField="MARCAS_PROPIAS" SortExpression="MARCAS_PROPIAS" HeaderText="Marcas Propias" />
                    <asp:BoundField DataField="DEVOLUCION_CAJAS" HeaderText="Devolución Cajas" />
                    <asp:BoundField DataField="GUIA" SortExpression="GUIA" HeaderText="Guia" />
                    <asp:BoundField DataField="PALLET_AZUL" HeaderText="Pallet Azul" />
                    <asp:BoundField DataField="PALLET_ROJO" HeaderText="Pallet Rojo" />
                    <asp:BoundField DataField="PALLET_BLANCO" HeaderText="Pallet Blanco" />
                    <asp:BoundField DataField="PALLET_LEÑA" HeaderText="Pallet Leña" />
                    <asp:BoundField DataField="Local" HeaderText="Local" />
                    <asp:BoundField DataField="SELLO" SortExpression="SELLO" HeaderText="Sello" />
                     <asp:BoundField DataField="Sobre" HeaderText="Sobre" />
                </Columns>
                <HeaderStyle CssClass="header-color2" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnExportar" runat="server" CssClass="ocultar" OnClick="btnExportar_Click" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript" src="../js2/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="../js2/css/defaultTheme.css" media="screen" />
    <script type="text/javascript">
        function EndRequestHandler(sender, args) {
            setTimeout(tabla, 100);
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

