<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Solicitudes_Carga.aspx.cs" Inherits="App_Reporte_Solicitudes_Carga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Reporte Solicitudes Carga</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-12 separador"></div>
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
            <div class="col-xs-2">
                Estado Solicitud
        <br />
                <asp:DropDownList ID="ddl_buscarEstado" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0" Text="Todos" />
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                <br />
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" runat="server">
      <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_exportar" CssClass="btn btn-primary" OnClick="btn_export_Click" runat="server">
      <span class="glyphicon glyphicon-export" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_exportar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gv_listar" CssClass="table table-bordered table-hover tablita" AllowSorting="true" Width="100%" runat="server"
                OnSorting="gv_listar_Sorting" OnRowCreated="gv_listar_RowCreated" OnRowCommand="gv_listar_RowCommand" UseAccessibleHeader="true"
                EmptyDataText="No hay ingresos!" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton id="btn_detalle" CssClass="btn btn-primary btn-xs" CommandArgument='<%# Eval("solicitud") %>' CommandName="VER" runat="server">
                                <span class="glyphicon glyphicon-list-alt" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="solicitud" SortExpression="solicitud" HeaderText="solicitud" />
                    <asp:BoundField DataField="ANDENES" SortExpression="ANDENES" HeaderText="Andenes" />
                    <asp:BoundField DataField="LOCALES" SortExpression="LOCALES" HeaderText="Locales" />
                    <asp:BoundField DataField="estado" SortExpression="estado" HeaderText="Estado" />
                    <asp:BoundField DataField="USUA_ELIMINACION" SortExpression="USUA_ELIMINACION" HeaderText="Usuario Elimina" />
                    <asp:BoundField DataField="CARACTERISTICAS" SortExpression="CARACTERISTICAS" HeaderText="Solicitud Tipo" />
                    <asp:BoundField DataField="RUTA" SortExpression="RUTA" HeaderText="Ruta" />
                    <asp:BoundField DataField="USUARIO_SOLICITUD" SortExpression="USUARIO_SOLICITUD" HeaderText="Usuario Solicitud" />
                    <asp:BoundField DataField="MODELO_SOLICITADO" SortExpression="MODELO_SOLICITADO" HeaderText="Modelo Solicitado" />
                    <asp:BoundField DataField="MODELO_ASIGNADO" SortExpression="MODELO_ASIGNADO" HeaderText="Modelo Asignado" />
                    <asp:BoundField DataField="NRO_FLOTA" SortExpression="NRO_FLOTA" HeaderText="Nro Flota" />
                    <asp:BoundField DataField="PATENTE" SortExpression="PATENTE" HeaderText="Patente" />
                    <asp:BoundField DataField="FH_SOLICITUD" SortExpression="FH_SOLICITUD" HeaderText="FH Solicitud" />
                    <asp:BoundField DataField="HORA_LIMITE" SortExpression="HORA_LIMITE" HeaderText="Hora Límite" />
                    <asp:BoundField DataField="FH_ASIGNACION" SortExpression="FH_ASIGNACION" HeaderText="FH Asignación" />
                    <asp:BoundField DataField="USUARIO_ASIGNACION" SortExpression="USUARIO_ASIGNACION" HeaderText="Usuario Asignación" />
                    <asp:BoundField DataField="TIEMPO_ASIGNACION" SortExpression="TIEMPO_ASIGNACION" HeaderText="Tiempo Asignación" />
                    <asp:BoundField DataField="FH_CARRO_ANDEN" SortExpression="FH_CARRO_ANDEN" HeaderText="FH Carro en Andén" />
                    <asp:BoundField DataField="USUARIO_OTTAWA" SortExpression="USUARIO_OTTAWA" HeaderText="Usuario Ottawa" />
                    <asp:BoundField DataField="TIEMPO_PROGRAMADO" SortExpression="TIEMPO_PROGRAMADO" HeaderText="Tiempo Programado" />
                    <asp:BoundField DataField="TIEMPO_PATIO" SortExpression="TIEMPO_PATIO" HeaderText="Tiempo en Patio" />
                    <asp:BoundField ReadOnly="True" HeaderText="Tiempo en Anden" DataField="TIEMPO_ANDEN" SortExpression="TIEMPO_ANDEN" />
                    <asp:BoundField DataField="TIEMPO_CARGA" SortExpression="TIEMPO_CARGA" HeaderText="Tiempo en Carga" />
                     <asp:BoundField DataField="TIEMPO_SELLO" SortExpression="TIEMPO_SELLO" HeaderText="Tiempo En Sello"  Visible="false"  />
                
                    <asp:BoundField DataField="TIEMPO_TOTAL" SortExpression="TIEMPO_TOTAL" HeaderText="Tiempo Total" />
                <asp:BoundField DataField="fecha_limite" SortExpression="fecha_limite" HeaderText="Fecha Limite" />
                <asp:BoundField DataField="Jornada" SortExpression="Jornada" HeaderText="Jornada" />
                <asp:BoundField DataField="SELLOS" SortExpression="SELLOS" HeaderText="Sellos" />
                </Columns>
                <HeaderStyle CssClass="header-color2" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalDetalle" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Label ID="lbl_modalDetalleTitulo" runat="server" />
                        </div>
                        <div class="modal-body" style="height:auto;overflow-y:auto;">
                            <div class="col-lg-12">
                                <asp:GridView ID="gv_detalle" AutoGenerateColumns="false" CssClass="table table-bordered table-hover tablita" OnRowCreated="gv_detalle_RowCreated" UseAccessibleHeader="true" runat="server">
                                    <Columns>
                                        <asp:BoundField DataField="USUA_USERNAME" SortExpression="USUA_USERNAME" HeaderText="Usuario" />
                                        <asp:BoundField DataField="SOES_DESC" SortExpression="SOES_DESC" HeaderText="Estado" />
                                        <asp:BoundField DataField="SOLI_FH_TIMESTAMP" SortExpression="SOLI_FH_TIMESTAMP" HeaderText="Timestamp" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
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
        var calcDataTableHeight = function () {
            return $(window).height() - $("#scrolls").offset().top - 100;
        };

        function reOffset1() {

            $('div.dataTables_scrollBody').height(calcDataTableHeight());
        }

        window.onresize = function (e) {
            reOffset1();
        }

        function EndRequestHandler(sender, args) {
            setTimeout(tabla, 100);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);

        function tabla() {
            if ($('#<%= this.gv_listar.ClientID %>')[0] != undefined && $('#<%= this.gv_listar.ClientID %>')[0].rows.length > 1) {
              $('#<%= this.gv_listar.ClientID %>').DataTable({
                  "scrollY": calcDataTableHeight(),
                    "scrollX": true,
                    "paging": false,
                    "ordering": false,
                    "searching": false,
                    "lengthChange": false
                });
            }
            $('div.dataTables_scrollBody').scrollTop(pageScrollPos);
            $('div.dataTables_scrollBody').scrollLeft(pageScrollPosleft);
        }
    </script>
</asp:Content>

