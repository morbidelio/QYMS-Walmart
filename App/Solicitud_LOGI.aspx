<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
    CodeFile="Solicitud_LOGI.aspx.cs" Inherits="App_soli_logi" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <h2>Maestro Logística Inversa
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel ID="SITE" runat="server">
                <div class="col-xs-7 col-lg-2">
                    Site
          <br />
                    <asp:DropDownList runat="server" ID="dropsite" ClientIDMode="Static" CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="drop_SelectedIndexChanged" Width="90%">
                    </asp:DropDownList>
                </div>
            </asp:Panel>
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
            <div class="col-xs-1">
                N° Flota
        <br />
                <asp:TextBox ID="txt_buscarNro" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
            </div>
            <div class="col-xs-2">
                Placa
        <br />
                <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                Solo internos
        <br />
                <asp:CheckBox ID="chk_buscarInterno" runat="server" />
            </div>
            <div class="col-xs-1">
                <br />
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscarTrailer_Click" CssClass="btn btn-primary"
                    ToolTip="Buscar Trailer" runat="server">
          <span class="glyphicon glyphicon-search"></span>
                </asp:LinkButton>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="click" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-xs-12">
                <asp:GridView ID="gv_listar" runat="server" AllowPaging="false" AllowSorting="True" DataKeyNames="ID,SOLI_ID,luga_id,soan_orden,TIMESTAMP,EXTERNO" UseAccessibleHeader="true"
                    OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting" OnRowDataBound="gv_listar_RowDataBound" OnRowCreated="gv_listar_RowCreated"
                    EmptyDataText="No se encontraron trailers!" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tablita">
                    <Columns>
                        <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="ID" SortExpression="ID"
                            ControlStyle-CssClass="ocultar" HeaderStyle-CssClass="ocultar" ItemStyle-CssClass="ocultar"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO" Visible="false"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Placa" DataField="PLACA" SortExpression="PLACA"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="N° Flota" DataField="NUMERO" SortExpression="NUMERO"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Transportista" DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Tipo" DataField="TIPO_TRAILER" SortExpression="TIPO_TRAILER"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Guía" DataField="guia" SortExpression="guia"></asp:BoundField>

                        <asp:BoundField ReadOnly="True" HeaderText="Estado Trailer" DataField="estado_trailer_desc" SortExpression="estado_trailer_desc"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Tipo Carga" DataField="Tipo_Carga" SortExpression="Tipo_Carga"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Sol" DataField="soli_id" SortExpression="soli_id"
                            ControlStyle-CssClass="ocultar" HeaderStyle-CssClass="ocultar" ItemStyle-CssClass="ocultar"></asp:BoundField>

                        <asp:BoundField ReadOnly="True" HeaderText="Sol Tipo" DataField="solicitud_tipo" SortExpression="solicitud_tipo"></asp:BoundField>

                        <asp:BoundField ReadOnly="True" HeaderText="Sol Estado" DataField="solicitud_estado" SortExpression="solicitud_estado"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="soan_orden" DataField="soan_orden" SortExpression="soan_orden"
                            ControlStyle-CssClass="ocultar" HeaderStyle-CssClass="ocultar" ItemStyle-CssClass="ocultar"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="luga_id" DataField="luga_id" SortExpression="luga_id"
                            ControlStyle-CssClass="ocultar" HeaderStyle-CssClass="ocultar" ItemStyle-CssClass="ocultar"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Solicitud Estado" DataField="soes_id" SortExpression="soes_id"
                            ControlStyle-CssClass="ocultar" HeaderStyle-CssClass="ocultar" ItemStyle-CssClass="ocultar"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Movimiento" DataField="MOVI_ID" SortExpression="MOVI_ID"
                            ControlStyle-CssClass="ocultar" HeaderStyle-CssClass="ocultar" ItemStyle-CssClass="ocultar"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="Tiempo Descarga" DataField="tiempo_descarga" SortExpression="tiempo_descarga"></asp:BoundField>

                        <asp:BoundField ReadOnly="True" HeaderText="PLAYA" DataField="PLAYA" SortExpression="PLAYA"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="POSICION" DataField="POSICION" SortExpression="POSICION"></asp:BoundField>

                        <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_solicitud_descarga" runat="server" CausesValidation="False" CommandName="DEF_DESCARGA"
                                    CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-primary" ToolTip="Descarga">
                  <span class="glyphicon glyphicon-import"></span>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btn_solicitud_descarga_li" runat="server" CausesValidation="False" CommandName="DEF_DESCARGA_LI"
                                    CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-primary" ToolTip="Descarga LI">
                  <span class="glyphicon glyphicon-import"></span>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btn_solicitud_pallet" runat="server" CausesValidation="False" CommandName="DEF_PALLETS"
                                    CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-primary" ToolTip="Pallet">
                  <span class="glyphicon glyphicon-export"></span>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btn_solicitud_desechos" runat="server" CausesValidation="False" CommandName="DEF_DESECHOS"
                                    CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-primary" ToolTip="Devolución Pallets">
                  <span class="glyphicon glyphicon-oil"></span>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btn_mover" runat="server" CausesValidation="False" CommandName="DEF_MOVER"
                                    CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-primary" ToolTip="Movimiento Interno">
                  <span class="glyphicon glyphicon-random"></span>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btn_descargaCompleta" runat="server" CausesValidation="True" CommandName="DESCARGA_COMPLETA"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Completar Descarga" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-ok"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_descargaEditarPos" runat="server" CausesValidation="True" CommandName="DESCARGA_POSICION"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Editar Posición" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-screenshot"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_descargaMover" runat="server" CausesValidation="False" CommandName="DESCARGA_MOVER"
                                    CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-primary" ToolTip="Movimiento Interno">
                  <span class="glyphicon glyphicon-random"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_descargaBloquear" runat="server" CausesValidation="True" CommandName="DESCARGA_BLOQUEAR"
                                    CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-primary" ToolTip="Bloquear Andén">
                  <span class="glyphicon glyphicon-lock"></span>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btn_descargaLiCompleta" runat="server" CausesValidation="True" CommandName="DESCARGA_COMPLETA_LI"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Completar Descarga LI" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-ok"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_descargaLiEditarPos" runat="server" CausesValidation="True" CommandName="DESCARGA_POSICION_LI"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Editar Posición LI" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-screenshot"></span>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btn_desechosCompletar" runat="server" CausesValidation="True" CommandName="DESECHOS_COMPLETAR"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Completar Carga" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-ok"></span>
                                </asp:LinkButton>

                                <asp:LinkButton ID="btn_palletsTrasladoAnden" runat="server" CausesValidation="True" CommandName="PALLETS_TRASLADO_ANDEN"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Trasladar a Andén" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="	glyphicon glyphicon-log-in"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_palletsTrasladoEst" runat="server" CausesValidation="True" CommandName="PALLETS_TRASLADO_EST"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Trasladar a Estacionamiento" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-pause"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_palletsReiniciar" runat="server" CausesValidation="True" CommandName="PALLETS_REINICIAR"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Continuar Descarga" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-play"></span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_palletsCompletar" runat="server" CausesValidation="True" CommandName="PALLETS_COMPLETAR"
                                    CssClass="btn btn-sm btn-primary" ToolTip="Completar Descarga" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-ok"></span>
                                </asp:LinkButton>

                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <!-- modal Trailer  ---->
    <div class="modal fade" id="modalLogistica" data-backdrop="static" role="dialog">
        <div class="modal-dialog " style="width: 900px">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="lbl_tituloLogistica" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <small>
                                <div id="dv_tipo" visible="false" runat="server">
                                    <div class="col-lg-4" style="display: none; visibility: hidden">
                                        <asp:RadioButton ID="rb_Nada" Text="Solo Finalizar" OnCheckedChanged="rb_CheckedChanged" AutoPostBack="true" GroupName="rbLogistica" runat="server" Visible="false" />
                                    </div>
                                </div>

                                <div id="dv_descargadoPallets" runat="server">
                                    <div class="col-lg-3">
                                        <asp:RadioButton ID="rb_Pallets" OnCheckedChanged="rb_CheckedChanged" AutoPostBack="true" GroupName="rbLogistica" runat="server" />
                                        <br />
                                        Cargar con pallets
                                    </div>

                                </div>
                                <div id="dv_descargadoDevolucion" runat="server">
                                    <div class="col-lg-3">
                                        <asp:RadioButton ID="rb_Desechos" OnCheckedChanged="rb_CheckedChanged" AutoPostBack="true" GroupName="rbLogistica" runat="server" />
                                        <br />
                                        Devolución pallets
                                    </div>

                                </div>

                                <div id="dv_movimiento_auto" runat="server">

                                    <div class="col-lg-3">
                                        <asp:RadioButton ID="rb_estacionamiento" OnCheckedChanged="rb_CheckedChanged" AutoPostBack="true" GroupName="rbLogistica" runat="server" />
                                        <br />
                                        Enviar a estacionamiento (auto)
                                    </div>

                                </div>

                                <div id="dv_SolicitudWays" runat="server">

                                    <div class="col-lg-3">
                                        <asp:RadioButton ID="rb_solicitudWays" OnCheckedChanged="rb_CheckedChanged" AutoPostBack="true" GroupName="rbLogistica" runat="server" />
                                        <br />
                                        Solicitud Ways
                                    </div>

                                </div>

                                <div id="dv_descargar" runat="server">
                                    <div class="col-lg-2">
                                        <asp:RadioButton ID="rb_descargar" OnCheckedChanged="rb_CheckedChanged" AutoPostBack="true" GroupName="rbLogistica" runat="server" />
                                        <br />
                                        Descargar
                                    </div>
                                </div>


                                <div id="dv_movimiento_manual" runat="server">

                                    <div class="col-lg-3">
                                        <asp:RadioButton ID="rb_estacionamientoMan" OnCheckedChanged="rb_CheckedChanged" AutoPostBack="true" GroupName="rbLogistica" runat="server" />
                                        <br />
                                        Enviar a estacionamiento (manual)
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                </div>
                                <div class="col-lg-12 separador"></div>
                                <div id="dv_posicion" class="col-lg-6" visible="false" runat="server">
                                    <div class="col-lg-4">Zona</div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddl_zona" OnSelectedIndexChanged="ddl_zona_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                    <div class="col-lg-4">Playa</div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddl_playa" OnSelectedIndexChanged="ddl_playa_SelectedIndexChanged" Enabled="false" AutoPostBack="true" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                    <div class="col-lg-4">Posición</div>
                                    <div class="col-lg-8">
                                        <asp:DropDownList ID="ddl_posicion" Enabled="false" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                </div>
                                <div id="dv_solicitud" class="col-lg-6" visible="false" runat="server">
                                    <div class="col-lg-4">Documento</div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_doc" CssClass="form-control" runat="server" />
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                    <div class="col-lg-4">Observación</div>
                                    <div class="col-lg-8">
                                        <asp:TextBox ID="txt_obs" CssClass="form-control" runat="server" />
                                    </div>
                            </small>
                            <div class="col-lg-12 separador"></div>
                        </div>
                        <div class="col-lg-12">
                            <center>
                  <asp:linkbutton id="btn_guardar" cssclass="btn btn-primary" onclick="btn_guardar_Click" runat="server">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:linkbutton>
                </center>
                        </div>
                        </div>
            <div class="modal-footer">
                <button type="button" data-dismiss="modal" class="btn btn-primary">
                    <span class="glyphicon glyphicon-remove"></span>
                </button>
            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalConfirmar" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Completar Descarga
                            </h4>
                        </div>
                        <div class="modal-body" style="margin-left: 20px; height: 150px">
                        </div>
                        <br />
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
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
                                <div class="col-lg-12 separador"></div>
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
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idTrailer" runat="server" />
            <asp:HiddenField ID="hf_idSolicitud" runat="server" />
            <asp:HiddenField ID="hf_soanorden" runat="server" />
            <asp:HiddenField ID="hf_lugaid" runat="server" />
            <asp:HiddenField ID="hf_soli_tipo" runat="server" />
            <asp:HiddenField ID="hf_timestamp" runat="server" />

            <asp:HiddenField ID="hf_accion" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);

        var pageScrollPos = 0;
        var pageScrollPosleft = 0;
        function BeginRequestHandler(sender, args) {
            pageScrollPos = $('div.dataTables_scrollBody').scrollTop();
            pageScrollPosleft = $('div.dataTables_scrollBody').scrollLeft();
        }

        function modalLogistica() {
            $("#modalLogistica").modal();
        }

        function modalConfirmar() {
            $("#modalConfirmar").modal();
        }

        function modalBloqueoAnden() {
            $("#modalBloqueoAnden").modal();
        }

        function EndRequestHandler(sender, args) {
            setTimeout(tabla, 200);
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
                    "lengthChange": false
                });
                $('div.dataTables_scrollBody').scrollTop(pageScrollPos);
                $('div.dataTables_scrollBody').scrollLeft(pageScrollPosleft);

            }
        }



        var tick_recarga;

        $(document).ready(function (e) {
            clearInterval(tick_recarga);
            tick_recarga = setInterval(click_recarga, 60000);
        });

        function click_recarga() {
            if (($('.modal:visible').length && $('body').hasClass('modal-open')) != true) {
                $('#<%= this.btn_buscar.ClientID %> ')[0].click();
            }
        }
    </script>
</asp:Content>
