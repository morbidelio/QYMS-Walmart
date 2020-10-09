<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
    CodeFile="Reporte_Trailer.aspx.cs" Inherits="App_Reporte_Trailer" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <h2>Reporte de Trailer
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <div class="container-fluid filtro">
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
            <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" />
        </div>
        <div class="col-xs-2">
            Solo internos
        <br />
            <asp:CheckBox ID="chk_buscarInterno" runat="server" />
        </div>
        <div class="col-xs-2">
            <br />
            <asp:LinkButton ID="btn_buscarTrailer" OnClick="btn_buscarTrailer_Click" CssClass="btn btn-primary" ToolTip="Buscar Trailer" runat="server">
          <span class="glyphicon glyphicon-search" />
            </asp:LinkButton>
            <asp:LinkButton ID="btn_export" ToolTip="Exportar a Excel" CssClass="btn btn-primary" OnClick="btn_export_Click" runat="server">
            <span class="glyphicon glyphicon-import" />
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server" class="container-fluid col-xs-12 cuerpo">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-xs-12">

                <asp:GridView ID="gv_listar" runat="server" AllowPaging="false" AllowSorting="true" UseAccessibleHeader="true"
                    OnSorting="gv_listaTrailer_Sorting" OnRowCreated="gv_listar_RowCreated" EmptyDataText="No se encontraron trailers!"
                    AutoGenerateColumns="False" Width="100%" PagerSettings-Mode="NumericFirstLast" CssClass="table table-bordered table-hover tablita">
                    <Columns>
                        <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="ID" SortExpression="ID"
                            ItemStyle-Height="30px" Visible="false"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO"
                            ItemStyle-Width="10%" Visible="false"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Placa" DataField="PLACA" SortExpression="PLACA"
                            ItemStyle-Width="20%" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="ID SHORTEC" DataField="ID_SHORTEK" SortExpression="ID_SHORTEK"
                            ItemStyle-Width="10%" Visible="TRUE"></asp:BoundField>

                        <asp:BoundField ReadOnly="True" HeaderText="Externo" DataField="EXTERNO" SortExpression="EXTERNO"
                            ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center" Visible="false"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Número" DataField="NUMERO" SortExpression="NUMERO"
                            ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Transportista" DataField="TRANSPORTISTA"
                            SortExpression="TRANSPORTISTA" ItemStyle-Width="30%" HeaderStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Tipo" DataField="TIPO_TRAILER" SortExpression="TIPO_TRAILER"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="ALTO" DataField="ALTO" SortExpression="ALTO"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="SITE" DataField="SITE" SortExpression="SITE"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="PLAYA" DataField="PLAYA" SortExpression="PLAYA"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="POSICION" DataField="POSICION" SortExpression="POSICION"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="VACIO" DataField="VACIO" SortExpression="VACIO" Visible="false"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="CARGADO" DataField="CARGADO" SortExpression="CARGADO" Visible="false"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="SOLICITUD" DataField="soes_desc" SortExpression="soes_desc"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>

                        <asp:BoundField ReadOnly="True" HeaderText="ESTADO TRAILER" DataField="BLOQUEO" SortExpression="BLOQUEO"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="ESTADO" DataField="ESTADO" SortExpression="ESTADO"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="ULT. MOV" DataField="FH_ULTIMO" SortExpression="FH_ULTIMO"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="DIAS EN CD" DataField="DIAS" SortExpression="DIAS"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>



                    </Columns>
                    <EditRowStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="header-color2" />
                </asp:GridView>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idTrailer" runat="server" />
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
            setTimeout(tabla, 100);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);

        function Exportar() {
            $get("<%=this.btnExportar.ClientID %>").click();
        }

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
                    "paging": false,
                    "ordering": false,
                    "searching": false,
                  "lengthChange": false,
                    "info": false
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
