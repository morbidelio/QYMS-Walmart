<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Control_Movimientos.aspx.cs" Inherits="App_Control_Movimientos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Movimientos Pendientes
    </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server" ID="updbuscar">
        <ContentTemplate>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-2">
                Site
        <br />
                <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddl_buscarSite_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-xs-3">
                Playa
        <br />
                <asp:DropDownList ID="ddl_buscarPlaya" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                Remolcador
        <br />
                <asp:DropDownList ID="ddl_buscarRemolcador" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                <br />
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscarMovimientos_Click" CssClass="btn btn-primary" ClientIDMode="Static"
                    ToolTip="Buscar Movimientos" runat="server">
          <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_export" ToolTip="Exportar a Excel" CssClass="btn btn-primary" OnClick="btn_export_Click" runat="server">
          <span class="glyphicon glyphicon-import" />
                </asp:LinkButton>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <div class="col-xs-12">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gv_listar" runat="server" CellPadding="8" AllowSorting="true" Width="1200px" CssClass="table table-bordered table-hover tablita"
                    EmptyDataText="No Existen Movimientos Asignados!" AutoGenerateColumns="false" UseAccessibleHeader="true"
                    OnRowCreated="gv_listar_RowCreated" OnRowDataBound="gv_listar_RowDataBound" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting">
                    <Columns>
                        <asp:BoundField DataField="MOVI_ID" SortExpression="MOVI_ID" />
                        <asp:BoundField DataField="MOVI_FH_CREACION" SortExpression="MOVI_FH_CREACION" HeaderText="FH Creación" />
                        <asp:BoundField DataField="MOVI_FH_ASIGNACION" SortExpression="MOVI_FH_ASIGNACION" HeaderText="FH Asignación" />
                        <asp:BoundField DataField="ESTADO" SortExpression="ESTADO" HeaderText="Estado" />
                        <asp:BoundField DataField="NRO_FLOTA" SortExpression="NRO_FLOTA" HeaderText="N° Flota" />
                        <asp:BoundField DataField="PLACA_TRAILER" SortExpression="PLACA_TRAILER" HeaderText="Placa" />
                        <asp:BoundField DataField="POS_ORI" SortExpression="POS_ORI" HeaderText="Pos. Origen" />
                        <asp:BoundField DataField="PLAY_ORI" SortExpression="PLAY_ORI" HeaderText="Playa Origen" />
                        <asp:BoundField DataField="POS_DEST" SortExpression="POS_DEST" HeaderText="Pos. Destino" />
                        <asp:BoundField DataField="PLAY_DEST" SortExpression="PLAY_DEST" HeaderText="Playa Destino" />
                        <asp:BoundField DataField="ORDEN" SortExpression="ORDEN" HeaderText="Orden" />
                        <asp:BoundField DataField="REMOLCADOR" SortExpression="REMOLCADOR" HeaderText="Remolcador" />
                        <asp:BoundField DataField="USUARIO" SortExpression="USUARIO" HeaderText="Usuario" />
                        <%--<asp:BoundField DataField="MOVI_OBSERVACIONES" SortExpression="MOVI_OBSERVACIONES" HeaderText="Obs." />--%>
                        <asp:TemplateField HeaderText="Ordenar" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_subir" runat="server" CommandName="SUBIR" CommandArgument='<%# Eval("MOVI_ID") %>'>
                  <span class="glyphicon glyphicon-menu-up" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_bajar" runat="server" CommandName="BAJAR" CommandArgument='<%# Eval("MOVI_ID") %>'>
                  <span class="glyphicon glyphicon-menu-down" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_editar" runat="server" CommandName="DESTINO"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Editar Destino" CommandArgument='<%# Eval("MOVI_ID") %>'>
                  <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Anular" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_anular" runat="server" CommandName="ANULAR"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Anular" CommandArgument='<%# Eval("MOVI_ID") %>'>
                  <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Completar" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_confirmar" runat="server" CommandName="CONFIRMAR"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Confirmar" CommandArgument='<%# Eval("MOVI_ID") %>'>
                  <span class="glyphicon glyphicon-ok" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalDestino" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Cambiar Destino
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-xs-12">
                                <div class="col-xs-6">
                                    Playa
                  <br />
                                    <asp:DropDownList ID="ddl_playaDestino" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_playaDestino_IndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="Seleccione..."></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-6">
                                    Posición
                  <br />
                                    <asp:DropDownList ID="ddl_posDestino" runat="server" CssClass="form-control" Enabled="false">
                                        <asp:ListItem Value="0" Text="Seleccione..."></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12">
                                <center>
                  <asp:LinkButton ID="btn_cambiarDestino" runat="server" ValidationGroup="destino"
                                  CssClass="btn btn-primary" OnClick="btn_cambiarDestino_Click">
                    <span class="glyphicon glyphicon-floppy-disk" />
                  </asp:LinkButton>
                </center>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                    <asp:RequiredFieldValidator ID="rfv_posDestino" ErrorMessage="* Requerido" ControlToValidate="ddl_posDestino" ValidationGroup="destino"
                        runat="server" InitialValue="0" Visible="false" />
                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" PopupPosition="BottomLeft"
                        runat="server" Enabled="True" TargetControlID="rfv_posDestino">
                    </asp:ValidatorCalloutExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="modalFinalizar" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Confirmar Movimiento
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">

                            <div class="col-xs-6">
                                Observación
                <br />
                                <asp:TextBox ID="txt_compObs" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-6">
                                Motivo
                <br />
                                <asp:DropDownList ID="ddl_compMotivo" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                                    <div class="col-xs-12">
                                        ¿Cambiar Destino?
                    <br />
                                        <asp:CheckBox ID="chk_cambiarDestino" runat="server" OnCheckedChanged="chk_cambiarDestino_Checked" AutoPostBack="true" />
                                    </div>

                                    <div id="dv_cambiarD" runat="server" style="display: none">
                                        <div class="col-xs-12 separador"></div>

                                        <div class="col-xs-6">
                                            Playa
                      <br />
                                            <asp:DropDownList ID="ddl_compPlaya" CssClass="form-control" OnSelectedIndexChanged="ddl_compPlaya_IndexChanged" runat="server">
                                                <asp:ListItem Value="0" Text="Seleccione..."></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-6">
                                            Posición
                      <br />
                                            <asp:DropDownList ID="ddl_compPos" runat="server" CssClass="form-control" Enabled="false">
                                                <asp:ListItem Value="0" Text="Seleccione..."></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12">
                                <center>
                  <asp:LinkButton ID="btn_completar" runat="server" ValidationGroup="confirmar"
                                  CssClass="btn btn-primary" OnClick="btn_completar_Click">
                    <span class="glyphicon glyphicon-floppy-disk" />
                  </asp:LinkButton>
                </center>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                    <asp:RequiredFieldValidator ID="rfv_posicionConf" ErrorMessage="* Requerido" ValidationGroup="confirmar" ControlToValidate="ddl_confPos"
                        runat="server" InitialValue="0" Visible="false" />
                    <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" PopupPosition="BottomLeft"
                        runat="server" Enabled="True" TargetControlID="rfv_posicionConf">
                    </asp:ValidatorCalloutExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="modalConfirmar" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Anular Movimiento
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            Se anulará el movimiento seleccionado, ¿desea continuar?
              <div class="col-xs-12 separador"></div>
                            <div class="col-xs-5">
                                Motivo
                <br />
                                <asp:DropDownList CssClass="form-control" ID="ddl_confMotivo" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_Conf" runat="server"
                                CssClass="btn btn-primary" OnClick="btn_anular_Click">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_id" runat="server" />
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

        function modalDestino() {
            $("#modalDestino").modal();
        }

        function modalConfirmar() {
            $("#modalConfirmar").modal();
        }

        function modalFinalizar() {
            $("#modalFinalizar").modal();
        }

        var tick_recarga;

        $(document).ready(function (e) {

            clearInterval(tick_recarga);
            tick_recarga = setInterval(click_recarga, 120000);

        });

        function click_recarga() {
            if (($('.modal:visible').length && $('body').hasClass('modal-open')) != true) {
                $('#btn_buscar')[0].click();
            }
        }

    </script>
</asp:Content>
