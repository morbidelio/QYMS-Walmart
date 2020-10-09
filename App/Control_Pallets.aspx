<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Control_Pallets.aspx.cs" Inherits="App_Control_Pallets" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Control Pallets
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server" ID="updbuscar">
        <ContentTemplate>

            <div class="col-xs-12 separador"></div>
            <div class="col-xs-12">
                <asp:Panel ID="SITE" runat="server">
                    <div class="col-xs-2">
                        Site
        <br />
                        <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddl_buscarSite_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <div class="col-xs-2">
                    Transportista
      <br />
                    <asp:DropDownList ID="ddl_buscarTrans" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                    Tipo Carga
      <br />
                    <asp:DropDownList ID="ddl_buscarTipoCarga" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                    Playa
      <br />
                    <asp:DropDownList ID="ddl_buscarPlaya" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="col-xs-1">
                    <br />


                    <asp:LinkButton ID="btn_buscarSolicitud" OnClick="btn_buscarSolicitud_Click" CssClass="btn btn-primary" ClientIDMode="Static"
                        ToolTip="Buscar Solicitudes" runat="server">
        <span class="glyphicon glyphicon-search" />
                    </asp:LinkButton>



                    <asp:LinkButton ID="btn_export" ToolTip="Exportar a Excel" CssClass="btn btn-primary" OnClick="btn_export_Click" runat="server">
            <span class="glyphicon glyphicon-import" />
                    </asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_buscarSolicitud" EventName="click" />

        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <div class="col-xs-12 separador"></div>
    <div class="col-xs-12">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>

                <asp:GridView ID="gv_listar" runat="server" CellPadding="8" DataKeyNames="ID" UseAccessibleHeader="true"
                    CssClass="table table-bordered table-hover tablita" EmptyDataText="No existen solicitudes pallets!" AutoGenerateColumns="false"
                    AllowSorting="True" EnableSortingAndPagingCallbacks="false" OnRowDataBound="gv_listar_RowDataBound" OnRowCreated="gv_listar_RowCreated"
                    OnSorting="gv_listar_Sorting" OnRowCommand="gv_listar_RowCommand">
                    <Columns>
                        <asp:BoundField ReadOnly="True" HeaderText="ID" DataField="ID" SortExpression="ID" Visible="false" />
                        <asp:BoundField ReadOnly="True" HeaderText="Fecha" DataField="FECHA_HORA" SortExpression="FECHA_HORA" />
                        <%--<asp:BoundField ReadOnly="True" HeaderText="Hora" DataField="HORA" SortExpression="HORA" />--%>
                        <asp:BoundField ReadOnly="True" HeaderText="Proveedor" DataField="PROVEEDOR" SortExpression="PROVEEDOR" />
                        <asp:BoundField ReadOnly="True" HeaderText="Transportista" DataField="TRANSPORTE" SortExpression="TRANSPORTE" />
                        <asp:BoundField ReadOnly="True" HeaderText="Trailer Flota" DataField="NUMERO_FLOTA" SortExpression="NUMERO_FLOTA" />
                        <asp:BoundField ReadOnly="True" HeaderText="Trailer Patente" DataField="PLACA" SortExpression="PLACA" />
                        <asp:BoundField ReadOnly="True" HeaderText="Playa 1" DataField="PLAYA1" SortExpression="PLAYA1" />
                        <asp:BoundField ReadOnly="True" HeaderText="Andén 1" DataField="ANDEN1" SortExpression="ANDEN1" />
                        <%--<asp:BoundField ReadOnly="True" HeaderText="Tiempo Andén 1" DataField="TIEMPO_ANDEN1" SortExpression="TIEMPO_ANDEN1" />--%>
                        <asp:BoundField ReadOnly="True" HeaderText="Playa 2" DataField="PLAYA2" SortExpression="PLAYA2" />
                        <asp:BoundField ReadOnly="True" HeaderText="Andén 2" DataField="ANDEN2" SortExpression="ANDEN2" />
                        <asp:BoundField ReadOnly="True" HeaderText="Tiempo Descarga" DataField="TIEMPO_DESCARGA" SortExpression="TIEMPO_DESCARGA" />
                        <asp:BoundField ReadOnly="True" HeaderText="Estado" DataField="ESTADO" SortExpression="ESTADO" />
                        <asp:BoundField ReadOnly="True" HeaderText="Tipo Carga" DataField="tipo_carga" SortExpression="tipo_carga" />
                        <asp:TemplateField HeaderText="Descarga" ShowHeader="False" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_llegadaAnden1" runat="server" CommandName="ANDEN1"
                                    CssClass="btn btn-xs btn-primary" ToolTip="Llegada a Andén 1" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-ok" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_llegadaAnden2" runat="server" CommandName="ANDEN2" Visible="false"
                                    CssClass="btn btn-xs btn-primary" ToolTip="Llegada a Andén 2" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-ok" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idSolicitud" runat="server" />
            <asp:HiddenField ID="hf_timestamp" runat="server" />
            <asp:Button ID="btnExportar" runat="server" CssClass="ocultar" OnClick="btnExportar_Click" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript" src="../js2/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="../js2/css/defaultTheme.css" media="screen" />
    <script type="text/javascript">
        function EndRequestHandler(sender, args) {
            tabla();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);

        var calcDataTableHeight = function () {
            //   alert($(window).height() - $("#scrolls").offset().top);
            return $(window).height() - $("#scrolls").offset().top - 100;
        };

        function reOffset1() {

            $('div.dataTables_scrollBody').height(calcDataTableHeight());
        }

        window.onresize = function (e) { reOffset1(); }

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
            }
        }


        function Exportar() {
            $get("<%=this.btnExportar.ClientID %>").click();
        }

        function modalConfirmacion() {
            $("#modalConfirmacion").modal();
        }
        function modalPosicion() {
            $("#modalPosicion").modal();
        }
        function modalBloqueoAnden() {
            $("#modalBloqueoAnden").modal();
        }

        var tick_recarga;
        $(document).ready(function (e) {

            clearInterval(tick_recarga);
            tick_recarga = setInterval(click_recarga, 120000);

        });

        function click_recarga() {

            if (($('.modal:visible').length && $('body').hasClass('modal-open')) != true)
                $('#btn_buscarSolicitud')[0].click();

        }
    </script>
</asp:Content>
