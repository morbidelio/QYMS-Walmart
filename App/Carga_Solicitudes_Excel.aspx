<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Carga_Solicitudes_Excel.aspx.cs" Inherits="App2_Documento_Legal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Carga Solicitudes Excel</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador"></div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-2">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
            <div class="col-xs-2">
                <asp:TextBox ID="txt_archivo" CssClass="form-control" runat="server" placeholder="Seleccione un archivo ...." Visible="false" />
            </div>
            <div class="col-xs-1">
                <asp:LinkButton ID="btn_subir" CssClass="btn btn-primary" runat="server" OnClick="UploadBtn_Click">
                    <span class="glyphicon glyphicon-upload" />
                </asp:LinkButton>
            </div>
            <div class="col-xs-1">
                <asp:LinkButton id="btn_procesar" OnClick="btn_procesar_Click" CssClass="btn btn-lg btn-primary" runat="server">
                    <span class="glyphicon glyphicon-ok" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_subir" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="gv_listar" runat="server" CssClass="table table-bordered table-hover tablita" Width="100%" AutoGenerateColumns="false" UseAccessibleHeader="true"
                EmptyDataText="No Existen Guías de Despacho!" OnRowCreated="gv_listar_RowCreated">
                <Columns>
                    <asp:BoundField DataField="SOLICITUD" HeaderText="Solicitud" />
                    <asp:BoundField DataField="SOAN_ORDEN" HeaderText="Orden" />
                    <asp:BoundField DataField="SITE" HeaderText="Site" />
                    <asp:BoundField DataField="ZONA" HeaderText="Zona" />
                    <asp:BoundField DataField="PLAYA" HeaderText="Playa" />
                    <asp:BoundField DataField="ANDEN" HeaderText="Anden" />
                    <asp:BoundField DataField="SHORTREC" HeaderText="Shortrec" />
                    <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                    <asp:BoundField DataField="HORA" HeaderText="Hora" />
                    <asp:BoundField DataField="LOCALES" HeaderText="Locales" />
                    <asp:BoundField DataField="TIPO_CARGA" HeaderText="Tipo carga" />
                    <asp:BoundField DataField="TEMPERATURA" HeaderText="Temperatura" />
                    <asp:BoundField DataField="TRAILER" HeaderText="Trailer" />
                     <asp:BoundField DataField="ruta" HeaderText="ruta" />
                    <asp:BoundField DataField="OBS" HeaderText="Observación" />
                    </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_codinterno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
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
            }
        }
        function EndRequestHandler1(sender, args) {
            setTimeout(tabla, 100);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);

    </script>
</asp:Content>
