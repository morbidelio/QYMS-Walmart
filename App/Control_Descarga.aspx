<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Control_Descarga.aspx.cs" Inherits="App_Control_Descarga" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Control Descarga
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
        <span class="glyphicon glyphicon-search"></span>
                    </asp:LinkButton>



                    <asp:LinkButton ID="btn_export" ToolTip="Exportar a Excel" CssClass="btn btn-primary" OnClick="btn_export_Click" runat="server">
            <span class="glyphicon glyphicon-import"></span>
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
                    CssClass="table table-bordered table-hover tablita" EmptyDataText="No existen solicitudes descarga!" AutoGenerateColumns="false"
                    AllowSorting="True" EnableSortingAndPagingCallbacks="false" OnRowDataBound="gv_listar_RowDataBound" OnRowCreated="gv_listar_RowCreated"
                    OnSorting="gv_listar_Sorting" OnRowCommand="gv_listar_RowCommand">
                    <Columns>
                        <asp:BoundField ReadOnly="True" HeaderText="ID" DataField="ID" SortExpression="ID" Visible="false"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Fecha" DataField="FECHA_HORA" SortExpression="FECHA_HORA"></asp:BoundField>
                        <%--<asp:BoundField ReadOnly="True" HeaderText="Hora" DataField="HORA" SortExpression="HORA"></asp:BoundField>--%>
                        <asp:BoundField ReadOnly="True" HeaderText="Proveedor" DataField="PROVEEDOR" SortExpression="PROVEEDOR"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Transportista" DataField="TRANSPORTE" SortExpression="TRANSPORTE"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Trailer Flota" DataField="NUMERO_FLOTA" SortExpression="NUMERO_FLOTA"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Trailer Patente" DataField="PLACA" SortExpression="PLACA"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Playa" DataField="PLAYA" SortExpression="PLAYA"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Andén Descarga" DataField="LUGAR" SortExpression="LUGAR"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Tiempo en Descarga" DataField="TIEMPO_DESCARGA" SortExpression="TIEMPO_DESCARGA"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Estado" DataField="ESTADO" SortExpression="ESTADO"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Tipo Carga" DataField="tipo_carga" SortExpression="tipo_carga"></asp:BoundField>

                        <asp:TemplateField HeaderText="Descarga" ShowHeader="False" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_descargaCompleta" runat="server" CausesValidation="True" CommandName="COMPLETAR"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Completar Descarga" CommandArgument='<%# Eval("ID") %>'>
                  <span class="glyphicon glyphicon-ok"></span>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar" ShowHeader="False" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" Visible="false"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_editar" runat="server" CausesValidation="True" CommandName="Edit" Visible="false"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Editar Solicitud" CommandArgument='<%# Eval("ID") %>'>
                  <span class="glyphicon glyphicon-pencil"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_editarPos" runat="server" CausesValidation="True" CommandName="EDITPOS" Visible="false"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Editar Posición" CommandArgument='<%# Eval("ID") %>'>
                  <span class="glyphicon glyphicon-screenshot"></span>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mover" ShowHeader="False" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" Visible="true"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_mover" runat="server" CausesValidation="True" CommandName="POSICION"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Cambiar Posición" CommandArgument='<%# Eval("ID") %>'>
                  <span class="glyphicon glyphicon-map-marker"></span>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bloqueo" ShowHeader="False" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_bloquear" runat="server" CausesValidation="True" CommandName="BLOQUEAR"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Bloquear Andén" CommandArgument='<%# Eval("ID") %>'>
                  <span class="glyphicon glyphicon-lock"></span>
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

    <div class="modal fade" id="modalConfirmacion" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Completar descarga
                            </h4>
                        </div>
                        <div class="modal-body" style="overflow-y: auto; height: auto">
                            <div class="col-xs-12">
                                La solicitud cambiará de estado a descarga completa y no se podrá modificar. ¿Desea continuar?
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_cargaFin" runat="server" OnClick="cargaFin_Click"
                                CssClass="btn btn-primary">
                <span class="glyphicon glyphicon-ok"></span>
                            </asp:LinkButton>
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove"></span>
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_cargaFin" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade" id="modalPosicion" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-lg" role="dialog">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Editar Posición
                            </h4>
                        </div>
                        <div class="modal-body" style="overflow-y: auto; height: auto">
                            <div class="col-xs-12">
                                <div class="col-xs-4">
                                    Zona
                  <br />
                                    <asp:DropDownList ID="ddl_editZona" CssClass="form-control" OnSelectedIndexChanged="ddl_editZona_IndexChanged" runat="server" AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="Seleccione..." />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-6">
                                    Playa
                  <br />
                                    <asp:DropDownList ID="ddl_editPlaya" CssClass="form-control" OnSelectedIndexChanged="ddl_editPlaya_IndexChanged" Enabled="true" runat="server" AutoPostBack="true">
                                        <asp:ListItem Value="0" Text="Seleccione..." />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-2">
                                    Posición
                  <br />
                                    <asp:DropDownList ID="ddl_editPos" CssClass="form-control" Enabled="false" runat="server">
                                        <asp:ListItem Value="0" Text="Seleccione..." />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12">
                                <center>
                  <asp:LinkButton ID="btn_mover" OnClick="btn_mover_Click" Visible="false" cssclass="btn btn-primary" runat="server">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                  <asp:LinkButton ID="btn_posModificar" OnClick="btn_posModificar_Click" Visible="false" cssclass="btn btn-primary" runat="server">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                  <asp:LinkButton ID="btn_finalizarExterno" OnClick="btn_finalizarExterno_Click" Visible="false" cssclass="btn btn-primary" runat="server">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove"></span>
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade" id="modalBloqueoAnden" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-m" role="dialog">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Bloquear Andén
                            </h4>
                        </div>
                        <div class="modal-body" style="overflow-y: auto; height: auto">
                            <div class="col-xs-12">
                                <div class="col-xs-6">
                                    Posición
                  <br />
                                    <asp:DropDownList ID="ddl_bloquearPos" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0" Text="Seleccione..." />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-2">
                                    <br />
                                    <asp:LinkButton ID="btn_bloquearAgregar" OnClick="btn_bloquearAgregar_Click" runat="server" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-plus"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12">
                                <asp:GridView ID="gv_bloqueo" OnRowCommand="gv_bloqueo_RowCommand" AutoGenerateColumns="false"
                                    CssClass="table table-bordered table-hover tablita" runat="server">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_quitar" runat="server" CausesValidation="True" CommandName="QUITAR"
                                                    CssClass="btn btn-primary" CommandArgument='<%# Eval("ID") %>'>
                          <span class="glyphicon glyphicon-erase"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" Visible="false" />
                                        <asp:BoundField DataField="ANDEN" HeaderText="Anden" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove"></span>
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
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

        function tabla() {
            if ($('#<%= this.gv_listar.ClientID %>')[0] != undefined && $('#<%= this.gv_listar.ClientID %>')[0].rows.length > 1) {
                $('#<%= this.gv_listar.ClientID %>').DataTable({
                    "scrollY": "320px",
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
            tick_recarga = setInterval(click_recarga, 10000);

        });

        function click_recarga() {

            if (($('.modal:visible').length && $('body').hasClass('modal-open')) != true)
                $('#btn_buscarSolicitud')[0].click();

        }
    </script>
</asp:Content>
