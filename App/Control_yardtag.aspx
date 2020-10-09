<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Control_yardtag.aspx.cs" Inherits="App_Control_yardtag" %>

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
            <div class="col-xs-3" style="visibility:hidden; display:none">
                Playa
        <br />
                <asp:DropDownList ID="ddl_buscarPlaya" CssClass="form-control" runat="server" >
                </asp:DropDownList>
            </div>
          <div class="col-xs-1" style="visibility:hidden; display:none">
                placa
                <br />
                <asp:TextBox ID="txt_placa" CssClass="form-control" runat="server" />  </div>
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
                    EmptyDataText="No Existen Movimientos Asignados!" AutoGenerateColumns="false" UseAccessibleHeader="true" DataKeyNames="YATA_COD, YAHI_ID, YAPE_ID, LUGA_ID, TRAI_ID"
                    OnRowCreated="gv_listar_RowCreated" OnRowDataBound="gv_listar_RowDataBound" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting">
                    <Columns>
                      
                       <asp:BoundField ReadOnly="True" HeaderText="YAHI_ID" DataField="YAHI_ID" SortExpression="YAHI_ID" HeaderStyle-Width="1%" Visible="false" />
                         <asp:BoundField ReadOnly="True" HeaderText="YATA_COD" DataField="YATA_COD" SortExpression="YATA_COD" HeaderStyle-Width="1%"  Visible="false" />
                         <asp:BoundField ReadOnly="True" HeaderText="YATA_PLACA" DataField="YATA_PLACA" SortExpression="YATA_PLACA" HeaderStyle-Width="1%" />
                         <asp:BoundField ReadOnly="True" HeaderText="UBIC_DEST" DataField="UBIC_DEST" SortExpression="UBIC_DEST" HeaderStyle-Width="1%" />
                         <asp:BoundField ReadOnly="True" HeaderText="YAPE_DESC" DataField="YAPE_DESC" SortExpression="YAPE_DESC" HeaderStyle-Width="1%" />
                         <asp:BoundField ReadOnly="True" HeaderText="FH_EVENTO_YT" DataField="FH_EVENTO_YT" SortExpression="FH_EVENTO_YT" HeaderStyle-Width="1%" />
                         <asp:BoundField ReadOnly="True" HeaderText="YAPE_ID" DataField="YAPE_ID" SortExpression="YAPE_ID" HeaderStyle-Width="1%" />
                         <asp:BoundField ReadOnly="True" HeaderText="YAPE_ACCION_MANUAL" DataField="YAPE_ACCION_MANUAL" SortExpression="YAPE_ACCION_MANUAL" HeaderStyle-Width="1%" Visible="false" />
                         <asp:BoundField ReadOnly="True" HeaderText="MOVI_ID" DataField="MOVI_ID" SortExpression="MOVI_ID" HeaderStyle-Width="1%" />
                         <asp:BoundField ReadOnly="True" HeaderText="MOES_DESC" DataField="MOES_DESC" SortExpression="MOES_DESC" HeaderStyle-Width="1%" />
                         <asp:BoundField ReadOnly="True" HeaderText="MOVI_FH_ASIGNACION" DataField="MOVI_FH_ASIGNACION" SortExpression="MOVI_FH_ASIGNACION" HeaderStyle-Width="1%" />
                         <asp:BoundField ReadOnly="True" HeaderText="TRAI_MOV_PEND" DataField="TRAI_MOV_PEND" SortExpression="TRAI_MOV_PEND" HeaderStyle-Width="1%" />
                         <asp:BoundField ReadOnly="True" HeaderText="REMO_COD" DataField="REMO_COD" SortExpression="REMO_COD" HeaderStyle-Width="1%" />
                         <asp:BoundField ReadOnly="True" HeaderText="REMO_DESCRIPCION" DataField="REMO_DESCRIPCION" SortExpression="REMO_DESCRIPCION" HeaderStyle-Width="1%" Visible="false" />


                        <asp:TemplateField HeaderText="Cuadrar" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_editar" runat="server" CommandName="DESTINO" CommandArgument='<%# Container.DataItemIndex %>'
                                    CssClass="btn btn-sm btn-primary" ToolTip="Cuadrar manualmente" >
                  <span class="glyphicon glyphicon-wrench" />
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
                                  CssClass="btn btn-primary">
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
