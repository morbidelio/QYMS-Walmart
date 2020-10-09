<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Gps.aspx.cs" Inherits="App_Reporte_Gps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Reporte GPS</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-12 separador"></div>
    <div class="col-lg-1">
        Site
        <br />
        <asp:DropDownList ID="ddl_site" CssClass="form-control" runat="server">
        </asp:DropDownList>
    </div>
    <div class="col-xs-2">
        Tipo Transporte
        <br />
        <asp:DropDownList ID="ddl_buscarTipo" CssClass="form-control" runat="server">
        </asp:DropDownList>
    </div>
    <div class="col-xs-2">
        Transportista
        <br />
        <asp:DropDownList ID="ddl_buscarTransportista" CssClass="form-control" runat="server">
        </asp:DropDownList>
    </div>
    <div class="col-xs-2">
        N° Flota
        <br />
        <asp:TextBox ID="txt_buscarNro" runat="server" CssClass="form-control" />
    </div>
    <div class="col-xs-2">
        Placa
        <br />
        <asp:TextBox ID="txt_buscarPlaca" runat="server" CssClass="form-control" />
    </div>
    <div class="col-xs-2">
        Solo internos
        <br />
        <asp:CheckBox ID="chk_buscarInterno" Checked="true" runat="server" />
    </div>
    <div class="col-xs-2">
        <br />
        <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" ToolTip="Buscar" runat="server">
          <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_export" OnClick="btn_export_Click" CssClass="btn btn-primary" ToolTip="Exportar a Excel" runat="server">
            <span class="glyphicon glyphicon-import" />
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <div class="col-lg-12 separador"></div>
    <div class="col-lg-12">
        <asp:GridView ID="gv_listar" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" Width="100%" runat="server"
            OnRowCommand="gv_listar_RowCommand" OnRowCreated="gv_listar_RowCreated" DataKeyNames="TRAI_ID,LAT,LON,TRAI_PLACA,GPS_FH_ULT_MOV,SENTIDO,VEL,TRAN_NOMBRE">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <span class="glyphicon glyphicon-map-marker" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton id="btn_gps" CommandName="GPS" CommandArgument='<%#Container.DataItemIndex%>' runat="server">
                            <span class="glyphicon glyphicon-map-marker" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TRAI_NUMERO" SortExpression="TRAI_NUMERO" HeaderText="TRAI_NUMERO" />
                <asp:BoundField DataField="TRAI_PLACA" SortExpression="TRAI_PLACA" HeaderText="TRAI_PLACA" />
                <asp:BoundField DataField="YMS_SITE" SortExpression="YMS_SITE" HeaderText="YMS_SITE" />
                <asp:BoundField DataField="YMS_ESTADO" SortExpression="YMS_ESTADO" HeaderText="YMS_ESTADO" />
                <asp:BoundField DataField="YMS_FH_ULT_MOV" SortExpression="YMS_FH_ULT_MOV" HeaderText="YMS_FH_ULT_MOV" />
                <asp:BoundField DataField="YMS_FH_SALIDA" SortExpression="YMS_FH_SALIDA" HeaderText="YMS_FH_SALIDA" />
                <asp:BoundField DataField="GPS_NRO_VIAJE" SortExpression="GPS_NRO_VIAJE" HeaderText="GPS_NRO_VIAJE" />
                <asp:BoundField DataField="GPS_FH_ULT_MOV" SortExpression="GPS_FH_ULT_MOV" HeaderText="GPS_FH_ULT_MOV" />
                <asp:BoundField DataField="GPS_FH_SALIDA" SortExpression="GPS_FH_SALIDA" HeaderText="GPS_FH_SALIDA" />
                <asp:BoundField DataField="GPS_ESTADO" SortExpression="GPS_ESTADO" HeaderText="GPS_ESTADO" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalGPS" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">
                                Ubicación Trailer
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDjAnu30d80TbLCujKOmvnMKcEj2GI5H3o&sensor=false&lenguage=es&v=3.20"></script>
                            <script type="text/javascript" src="../js/gmaps.js"></script>
                            <div id="map" style="width: 100%; height: 500px; min-width: 200px;min-height: 200px;">
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function modalGps(id, placa, lat, lon, fec, dir, vel, tran){
            $('#modalGPS').modal();
            lat = parseFloat(lat);
            lon = parseFloat(lon);
            cargamapa(10, document.getElementById('map'), lat, lon);
            marcaTrailer(id, placa, lat, lon, fec, dir, vel, tran);
        }
        function EndRequestHandler(sender, args) {
            setTimeout(tabla, 100);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);

        var calcDataTableHeight = function () { 
            //   alert($(window).height() - $("#scrolls").offset().top);
            return $(window).height() - $("#scrolls").offset().top - 100;
        };

        function reOffset1() {

            $('div.dataTables_scrollBody').height(calcDataTableHeight());
        }

        function tabla() {
            if ($('#<%= this.gv_listar.ClientID %>')[0] != undefined && $('#<%= this.gv_listar.ClientID %>')[0].rows.length > 1) {
                $('#<%= this.gv_listar.ClientID %>').DataTable({
                    "scrollY": calcDataTableHeight(),
                    "scrollX": true,
                    "ordering": false,
                    "searching": false,
                    "paging": true,
                    "info": false,
                    "lengthChange": false,
                    "paging": true,
                    "pageLength": 300,
                    "language": {
                        "paginate": {
                            "previous": "Ant.",
                            "next": "Sig."
                        }
                    }
                });
            }
        }
    </script>
</asp:Content>

