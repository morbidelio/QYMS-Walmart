<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Control_Carga_New.aspx.cs" Inherits="App_Control_Carga_New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <h2>
    Control de Carga
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server" ID="updbuscar">
        <ContentTemplate>
            <div class="col-xs-12 separador">
            </div>
            <asp:Panel ID="SITE" runat="server">
                <div class="col-xs-1">
                    Site
          <br />
                    <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddl_buscarSite_SelectedIndexChanged" AutoPostBack="true" />
                </div>
            </asp:Panel>
            <div class="col-xs-2">
                Playa
        <br />
                <asp:DropDownList ID="ddl_buscarPlaya" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddl_buscarPlaya_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="0" Text="Todos" />
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                Andén
        <br />
                <asp:DropDownList ID="ddl_buscarAnden" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0" Text="Todos" />
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                N° Solicitud
        <br />
                <asp:TextBox ID="txt_buscarNumero" CssClass="form-control input-number" runat="server" />
            </div>
            <div class="col-xs-2">
                Estado Solicitud
        <br />
                <asp:DropDownList ID="ddl_buscarEstado" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0" Text="Todos" />
                </asp:DropDownList>
            </div>
            <div class="col-xs-2">
                Transportista
        <br />
                <asp:DropDownList ID="ddl_buscarTransportista" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0" Text="Todos" />
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                ID Ruta
        <br />
                <asp:TextBox ID="txt_buscarRuta" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                <br />
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" ToolTip="Buscar Solicitudes" runat="server" ClientIDMode="Static">
          <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_export" OnClick="btn_export_Click" CssClass="btn btn-primary" ToolTip="Exportar a Excel" runat="server">
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
    <div class="col-xs-12">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:GridView ID="gv_listar" runat="server" DataKeyNames="ID_SOLICITUD,ID_LUGAR,ORDEN" CssClass="table table-bordered table-hover tablita" EmptyDataText="No Existen Movimientos Asignados!" AutoGenerateColumns="false"
                    AllowSorting="True" UseAccessibleHeader="true" OnSorting="gv_listar_Sorting" OnRowDataBound="gv_listar_RowDataBound" OnRowCreated="gv_listar_RowCreated" OnRowCommand="gv_listar_RowCommand">
                    <HeaderStyle CssClass="header-color2" />
                    <Columns>
                        <asp:TemplateField HeaderText="Editar" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_editar" runat="server" CommandName="Edit" CssClass="btn btn-xs btn-primary" ToolTip="Modificar" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_AndenEmergencia" runat="server" CommandName="AndenEmergencia" CssClass="btn btn-xs btn-primary" ToolTip="Anden Emergencia" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-pause" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_locales" runat="server" CommandName="Locales" CssClass="btn btn-xs btn-primary" ToolTip="Locales" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-plus" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_encendido" runat="server" CommandName="encender" CssClass="btn btn-xs btn-primary" ToolTip="Encender Frio" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-off" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ReadOnly="True" HeaderText="Solicitud" DataField="ID_SOLICITUD" SortExpression="ID_SOLICITUD" HeaderStyle-Width="1%" />
                        <asp:BoundField ReadOnly="True" HeaderText="Id Solicitud Anden" DataField="ID_SOLICITUDANDEN" SortExpression="ID_SOLICITUDANDEN" Visible="false" />
                        <asp:BoundField ReadOnly="True" HeaderText="Orden" DataField="ORDEN" SortExpression="ORDEN" />
                        <asp:BoundField ReadOnly="True" HeaderText="Andén Carga" DataField="LUGAR" SortExpression="LUGAR" />
                        <asp:BoundField ReadOnly="True" HeaderText="ID Shortrec Sol." DataField="ID_SHORTEK" SortExpression="ID_SHORTEK" />
                         <asp:BoundField ReadOnly="True" HeaderText="ID Shortrec Trai." DataField="ID_SHORTEK_trailer" SortExpression="ID_SHORTEK_asignado" />
                        <asp:BoundField ReadOnly="True" HeaderText="Fecha Cumplimiento" DataField="FECHA_PUESTA_ANDEN" SortExpression="FECHA_PUESTA_ANDEN_2" />
                         <asp:BoundField ReadOnly="True" HeaderText="Tiempo en Anden" DataField="TIEMPO_ANDEN" SortExpression="TIEMPO_ANDEN" />
                        <asp:BoundField ReadOnly="True" HeaderText="Tiempo en carga" DataField="TIEMPO_CARGA" SortExpression="TIEMPO_CARGA" />
                                        <asp:BoundField DataField="TIEMPO_SELLO" SortExpression="TIEMPO_SELLO" HeaderText="Tiempo En Sello" Visible="false" />
                        <asp:BoundField ReadOnly="True" HeaderText="ID Ruta" DataField="SOLI_RUTA" SortExpression="SOLI_RUTA" />
                        <asp:BoundField ReadOnly="True" HeaderText="Estado Solicitud" DataField="ESTADOSOLICITUD" SortExpression="ESTADOSOLICITUD" />
                        <asp:BoundField ReadOnly="True" HeaderText="Fecha Creación" DataField="SOLI_FH_CREACION" SortExpression="SOLI_FH_CREACION_2" />
                        <asp:BoundField ReadOnly="True" HeaderText="Id Estado Andén" DataField="ID_ESTADOANDEN" SortExpression="ID_ESTADOANDEN" Visible="false" />
                        <asp:BoundField ReadOnly="True" HeaderText="Id Estado Solicitud" DataField="ID_ESTADOSOLICITUD" SortExpression="ID_ESTADOSOLICITUD" Visible="false" />
                        <asp:BoundField ReadOnly="True" HeaderText="Fecha Andén" DataField="FECHA_RESERVA_ANDEN" SortExpression="FECHA_RESERVA_ANDEN_2" />
                        <asp:BoundField ReadOnly="True" HeaderText="Local" DataField="LOCALES" SortExpression="LOCALES" />
                        <asp:BoundField ReadOnly="True" HeaderText="Regiones" DataField="REGIONES" SortExpression="REGIONES" />
                        <asp:BoundField ReadOnly="True" HeaderText="Transporte" DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" />
                        <asp:BoundField ReadOnly="True" HeaderText="N° Flota" DataField="NRO_FLOTA" SortExpression="NRO_FLOTA" />
                        <asp:BoundField ReadOnly="True" HeaderText="Trailer Patente" DataField="PATENTE" SortExpression="PATENTE" />
                        <asp:BoundField ReadOnly="True" HeaderText="Plancha" DataField="Plancha" SortExpression="Plancha" />
                        <asp:BoundField ReadOnly="True" HeaderText="Playa" DataField="PLAYA" SortExpression="PLAYA" Visible="false" />
                        <asp:BoundField ReadOnly="True" HeaderText="Id Lugar" DataField="ID_LUGAR" SortExpression="ID_LUGAR" Visible="false" />
                        <asp:BoundField ReadOnly="True" HeaderText="Tipo Carga" DataField="FRIO" SortExpression="FRIO" />
                        <asp:BoundField ReadOnly="True" HeaderText="Temp" DataField="TEMPERATURA" SortExpression="TEMPERATURA" />
                        <asp:BoundField ReadOnly="True" HeaderText="Usuario" DataField="Usuario" SortExpression="Usuario" />
                        <asp:BoundField ReadOnly="True" HeaderText="jornada" DataField="jornada" SortExpression="jornada" />
                        <asp:TemplateField HeaderText="Carga" ShowHeader="False" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_cargado" runat="server" CausesValidation="True" CommandName="Cargado" CssClass="btn btn-xs btn-primary" ToolTip="Completar Carga" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-check" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_anden" runat="server" CausesValidation="True" CommandName="anden" CssClass="btn btn-xs btn-primary" ToolTip="Anden Listo" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-thumbs-up" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_sello" runat="server" CausesValidation="True" CommandName="colocar_sello" CssClass="btn btn-xs btn-primary" ToolTip="Sello" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-arrow-down" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_valida_sello" runat="server" CausesValidation="True" CommandName="validar_sello" CssClass="btn btn-xs btn-primary" ToolTip="A Estacionamiento" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-arrow-up" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parcial" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_cargaParcial" runat="server" CommandName="Parcial" CssClass="btn btn-xs btn-primary" ToolTip="Interrumpir Carga" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-pause" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_cargaContinuar" Visible="false" runat="server" CommandName="Continuar" CssClass="btn btn-xs btn-primary" ToolTip="Continuar Carga" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-play" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cancelar" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_solicitudCancelar" runat="server" CommandName="CANCELAR" CssClass="btn btn-xs btn-primary" ToolTip="Cancelar Solicitud" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-ban-circle" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="header-color2" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
    <div id="modalReanudar" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="lbl_tituloModal" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow-y: auto;">
                            <div class="col-lg-6 col-xs-12">
                                Local
                <br />
                                <div class="col-lg-4 col-xs-4">
                                    <asp:TextBox ID="txt_reanudarCodLocal" OnTextChanged="txt_reanudarCodLocal_TextChanged" CssClass="input-number form-control" AutoPostBack="true" Width="90%" runat="server" />
                                </div>
                                <div class="col-lg-8 col-xs-8">
                                    <asp:TextBox ID="txt_reanudarLocal" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-xs-6">
                                Andén
                <br />
                                <asp:DropDownList ID="ddl_reanudarAnden" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..."></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-2">
                                <br />
                                <asp:LinkButton ID="btn_agregarCarga" CssClass="btn btn-primary" ToolTip="Nuevo Local" runat="server" OnClick="btn_agregarCarga_Click">
                  <span class="glyphicon glyphicon-plus" />
                                </asp:LinkButton>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12 container-fluid" style="overflow: auto; height: 45vh;">
                                <asp:GridView ID="gv_reanudarLocales" CssClass="table table-bordered table-hover tablita" EmptyDataText="No Existen Andenes/Locales Asignados!" AutoGenerateColumns="false"
                                    OnRowCommand="gv_reanudarLocales_RowCommand" OnRowDataBound="gv_reanudarLocales_RowDataBound" Width="100%" runat="server">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_eliminarLocal" CausesValidation="False" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Container.DataItemIndex %>'
                                                    CommandName="ELIMINAR" ToolTip="Eliminar" runat="server">
                          <span class="glyphicon glyphicon-erase" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSubir" CausesValidation="False" CommandArgument='<%# Container.DataItemIndex %>' CommandName="SUBIR" runat="server">
                          <span class="glyphicon glyphicon-menu-up" />
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnBajar" CausesValidation="False" CommandArgument='<%# Container.DataItemIndex %>' CommandName="BAJAR" runat="server">
                          <span class="glyphicon glyphicon-menu-down" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="N° Local" DataField="NUMERO" />
                                        <asp:BoundField HeaderText="Orden Andén" DataField="SOAN_ORDEN" />
                                        <asp:BoundField HeaderText="Orden Local" DataField="SOLD_ORDEN" Visible="false" />
                                        <asp:BoundField HeaderText="Orden Local (old)" DataField="SOLD_ORDEN_OLD" Visible="false" />
                                        <asp:BoundField HeaderText="Nombre Local" DataField="LOCAL" />
                                        <asp:BoundField HeaderText="Andén" DataField="ANDEN" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton ID="btn_reanudar" runat="server" OnClick="btn_reanudar_Click" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_emergencia" runat="server" OnClick="btn_emergencia_Click" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_loc" runat="server" OnClick="btn_locales_Click" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="modalCarga" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Carga andén
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow-y: auto;">
                            <div id="dv_pallets" runat="server" class="col-xs-4">
                                Pallets cargados
                <br />
                                <asp:TextBox ID="txt_palletsCargados" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-4">
                                Fecha
                <br />
                                <asp:TextBox ID="txt_fechaCarga" CssClass="form-control input-fecha" runat="server" Enabled="false" />
                            </div>
                            <div class="col-xs-4">
                                Hora
                <br />
                                <asp:TextBox ID="txt_horaCarga" CssClass="form-control input-hora" runat="server" Enabled="false" />
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton ID="btn_cargaParcial" runat="server" OnClick="btn_cargaParcial_Click" CssClass="btn btn-primary btn-carga">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_cargaTerminar" runat="server" OnClick="btn_cargaTerminar_Click" CssClass="btn btn-primary btn-carga">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="modalConfirmar" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-sm" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>
                                <asp:Label ID="lbl_confirmarTitulo" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lbl_confirmarMensaje" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_eliminarSolicitud" runat="server"
                                CssClass="btn btn-primary" OnClick="btn_eliminarSolicitud_Click">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                               
                            <asp:LinkButton ID="btn_andenListo" runat="server"
                                CssClass="btn btn-primary" OnClick="btn_andenListo_Click">
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
            <asp:HiddenField ID="hf_soliId" Value="0" runat="server" />
            <asp:HiddenField ID="hf_lugaId" Value="0" runat="server" />
            <asp:HiddenField ID="hf_soanOrden" Value="0" runat="server" />
            <asp:HiddenField ID="hf_localesSeleccionados" Value="" runat="server" />
            <asp:HiddenField ID="hf_localesCompatibles" Value="" runat="server" />
            <asp:HiddenField ID="hf_caractSolicitud" Value="" runat="server" />
            <asp:HiddenField ID="hf_maxPallets" Value="" runat="server" />
            <asp:HiddenField ID="hf_timeStamp" Value="" runat="server" />
            <asp:HiddenField ID="hf_accion" Value="" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        var tick_recarga;

        $(document).ready(function (e) {
            clearInterval(tick_recarga);
            tick_recarga = setInterval(click_recarga, 60000);
        });

        function click_recarga() {
            if (($('.modal:visible').length && $('body').hasClass('modal-open')) != true) {
                $('#btn_buscar')[0].click();
            }
        }

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);

        var pageScrollPos = 0;
        var pageScrollPosleft = 0;

        function BeginRequestHandler(sender, args) {
            pageScrollPos = $('div.dataTables_scrollBody').scrollTop();
            pageScrollPosleft = $('div.dataTables_scrollBody').scrollLeft();
        }

        function EndRequestHandler1(sender, args) {
            $('#<%= btn_agregarCarga.ClientID %>').click(function () {
                if ($('#<%= txt_reanudarCodLocal.ClientID %>').val() == '') {
                    showAlertClass("locales", "warn_localVacio");
                    return false;
                }
                if ($('#<%= ddl_reanudarAnden.ClientID %>').val() == '0') {
                    showAlertClass("locales", "warn_andenVacio");
                    return false;
                }
            });
            $('#<%= btn_cargaTerminar.ClientID %>').click(function () {
                if ($('#<%= txt_fechaCarga.ClientID %>').val() == '') {
                    showAlertClass("cargaCompleta", "warn_fechaVacio");
                    return false;
                }
                if ($('#<%= txt_horaCarga.ClientID %>').val() == '') {
                    showAlertClass("cargaCompleta", "warn_horaVacio");
                    return false;
                }
            });
            $('#<%= btn_cargaParcial.ClientID %>').click(function () {
                if ($('#<%= txt_palletsCargados.ClientID %>').val() == '') {
                    showAlertClass("cargaParcial", "warn_palletsVacio");
                    return false;
                }
                if ($('#<%= txt_fechaCarga.ClientID %>').val() == '') {
                    showAlertClass("cargaParcial", "warn_fechaVacio");
                    return false;
                }
                if ($('#<%= txt_horaCarga.ClientID %>').val() == '') {
                    showAlertClass("cargaParcial", "warn_horaVacio");
                    return false;
                }
            });
            setTimeout(tabla, 100);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);

        var calcDataTableHeight = function () {
            //   alert($(window).height() - $("#scrolls").offset().top);
            return $(window).height() - $("#scrolls").offset().top - 80;
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
                $('div.dataTables_scrollBody').scrollTop(pageScrollPos);
                $('div.dataTables_scrollBody').scrollLeft(pageScrollPosleft);
            }
        }
        function modalReanudar() {
            $("#modalReanudar").modal();
        }
        function modalCarga() {
            $("#modalCarga").modal();
        }
        function modalConfirmar() {
            $("#modalConfirmar").modal();
        }
    </script>
</asp:Content>

