<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Descarga.aspx.cs" Inherits="App_Reporte_Descarga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Reporte Solicitudes Descarga</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-2">
                Site
        <br />
                <asp:DropDownList ID="ddl_site" OnSelectedIndexChanged="ddl_site_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-lg-2">
                Playa
        <br />
                <asp:DropDownList ID="ddl_playa" CssClass="form-control" runat="server" />
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
                Placa
        <br />
                <asp:TextBox ID="txt_placa" CssClass="form-control" runat="server" />
            </div>
            <div class="col-xs-2">
                <br />
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
        <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_export" OnClick="btn_export_Click" CssClass="btn btn-primary" runat="server">
        <span class="glyphicon glyphicon-import" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_export" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-12">
                <asp:GridView ID="gv_listar" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" AllowSorting="true" UseAccessibleHeader="true" Width="100%" runat="server"
                    OnRowCreated="gv_listar_RowCreated" OnSorting="gv_listar_Sorting" EmptyDataText="No existen solicitudes descarga">
                    <Columns>
                        <asp:BoundField DataField="PROVEEDOR" SortExpression="PROVEEDOR" HeaderText="Proveedor" />
                        <asp:BoundField DataField="NRO_SOLICITUD" SortExpression="NRO_SOLICITUD" HeaderText="N° Solicitud" />
                        <asp:BoundField DataField="TIPO_FLUJO" SortExpression="TIPO_FLUJO" HeaderText="Tipo de Flujo" />
                        <asp:BoundField DataField="PLACA" SortExpression="PLACA" HeaderText="Placa" />
                        <asp:BoundField DataField="NRO_FLOTA" SortExpression="NRO_FLOTA" HeaderText="Nro Flota" />
                        <asp:BoundField DataField="ZONA" SortExpression="ZONA" HeaderText="Zona" />
                        <asp:BoundField DataField="PLAYA" SortExpression="PLAYA" HeaderText="Playa" />
                        <asp:BoundField DataField="ANDEN" SortExpression="ANDEN" HeaderText="Anden" />
                        <asp:BoundField DataField="TIEMPO_EN_DESCARGA" SortExpression="TIEMPO_EN_DESCARGA" HeaderText="Tiempo En Descarga" />
                        <asp:BoundField DataField="FH_CITA" SortExpression="FH_CITA" HeaderText="FH Cita" />
                        <asp:BoundField DataField="FH_DESCARGA" SortExpression="FH_DESCARGA" HeaderText="FH Descarga" />
                        <asp:BoundField DataField="citas" SortExpression="citas" HeaderText="Cita" />
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
            return $(window).height() - $("#<%= this.gv_listar.ClientID %>").offset().top - 80;
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
                    "lengthChange": false,
                    "info": false
                });
                //    alert(pageScrollPos);
                $('div.dataTables_scrollBody').scrollTop(pageScrollPos);
                $('div.dataTables_scrollBody').scrollLeft(pageScrollPosleft);

            }
        }
    </script>
</asp:Content>
