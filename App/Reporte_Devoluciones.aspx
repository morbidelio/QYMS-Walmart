<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Devoluciones.aspx.cs" Inherits="App_Reporte_Devoluciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Reporte Devoluciones</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-2">
      Site
      <br />
      <asp:DropDownList ID="ddl_site" CssClass="form-control"  runat="server">
      </asp:DropDownList>
    </div>

    <div class="col-lg-1">
        Desde
        <br />
        <asp:TextBox ID="txt_desde" CssClass="form-control input-fecha" runat="server" />
    </div>
    <div class="col-lg-1">
        Hasta
        <br />
        <asp:TextBox ID="txt_hasta" CssClass="form-control input-fecha" runat="server" />
    </div>
    <div class="col-lg-1">
        Local
        <br />
        <asp:DropDownList ID="ddl_local" CssClass="form-control" runat="server" />
    </div>
    <div class="col-lg-1">
        N° Viaje
        <br />
        <asp:TextBox ID="txt_nroViaje" CssClass="form-control input-number" runat="server" />
    </div>
    <div class="col-lg-1">
        Placa
        <br />
        <asp:TextBox ID="txt_placa" CssClass="form-control" runat="server" />
    </div>
    <div class="col-lg-1">
        N° Flota
        <br />
        <asp:TextBox ID="txt_nroFlota" CssClass="form-control input-number" runat="server" />
    </div>
    <div class="col-xs-2">
        <br />
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
        <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_export" OnClick="btn_export_Click" CssClass="btn btn-primary" runat="server">
        <span class="glyphicon glyphicon-import" />
                </asp:LinkButton>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_export" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-12">
                <asp:GridView ID="gv_listar" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" AllowSorting="true" UseAccessibleHeader="true" Width="100%" runat="server"
                    OnRowCreated="gv_listar_RowCreated" OnSorting="gv_listar_Sorting" EmptyDataText="No existen devoluciones local">
                    <Columns>
                         <asp:BoundField DataField="LOCAL" SortExpression="LOCAL" HeaderText="Local" />
		                 <asp:BoundField DataField="NRO_VIAJE" SortExpression="NRO_VIAJE" HeaderText="N° Viaje" />
		                 <asp:BoundField DataField="TIPO_FLUJO" SortExpression="TIPO_FLUJO" HeaderText="Tipo Flujo" />
		                 <asp:BoundField DataField="MOTIVO" SortExpression="MOTIVO" HeaderText="Motivo" />
                         <asp:BoundField DataField="PATENTE" SortExpression="PATENTE" HeaderText="Patente" />
		                 <asp:BoundField DataField="NRO_FLOTA" SortExpression="NRO_FLOTA" HeaderText="N° Flota" />
		                 <asp:BoundField DataField="ZONA" SortExpression="ZONA" HeaderText="Zona" />
		                 <asp:BoundField DataField="PLAYA" SortExpression="PLAYA" HeaderText="Playa" />
		                 <asp:BoundField DataField="POSICION" SortExpression="POSICION" HeaderText="Posición" />
		                 <asp:BoundField DataField="DIAS_EN_CD" SortExpression="DIAS_EN_CD" HeaderText="Días En CD" />
		                 <asp:BoundField DataField="FH_INGRESO" SortExpression="FH_INGRESO" HeaderText="Fh Ingreso" />
                         <asp:BoundField DataField="CONDUCTOR_RUT" SortExpression="CONDUCTOR_RUT" HeaderText="RUT Conductor" />
                        <asp:BoundField DataField="CONDUCTOR_NOMBRE" SortExpression="CONDUCTOR_NOMBRE" HeaderText="Nombre Conductor" />
                        <asp:BoundField DataField="OBSERVACIONES" SortExpression="OBSERVACIONES" HeaderText="Observaciones" />
                  
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript" src="../js2/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="../js2/css/defaultTheme.css" media="screen" />
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);

        var pageScrollPos = 0;
        var pageScrollPosleft = 0;

        function BeginRequestHandler(sender, args) {
            pageScrollPos = $('div.dataTables_scrollBody').scrollTop();
            pageScrollPosleft = $('div.dataTables_scrollBody').scrollLeft();
        }

        function EndRequestHandler1(sender, args) {
            setTimeout(tabla, 100);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);

        var calcDataTableHeight = function () {
            return $(window).height() - $("#<%= this.gv_listar.ClientID %>").offset().top - 100;
        };

        function reOffset1() {

            $('div.dataTables_scrollBody').height(calcDataTableHeight());
        }

        window.onresize = function (e) {
            reOffset1();
        }

        function tabla() {
            if ($('#<%= this.gv_listar.ClientID %>')[0] != undefined && $('#<%= this.gv_listar.ClientID %>')[0].rows.length > 1) {
              $('#<%= this.gv_listar.ClientID %>').DataTable({
                  "scrollY": calcDataTableHeight(),
                  "scrollX": true,
                  "scrollCollapse": true,
                  "paging": false,
                  "ordering": false,
                  "searching": false,
                  "lengthChange": false
              });
              //    alert(pageScrollPos);
              $('div.dataTables_scrollBody').scrollTop(pageScrollPos);
              $('div.dataTables_scrollBody').scrollLeft(pageScrollPosleft);

          }
      }
    </script>
</asp:Content>
