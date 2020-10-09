<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Control_Devolucion.aspx.cs" Inherits="App_Control_Devolucion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Control de devoluciones</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-12 separador"></div>
            <div class="col-lg-2">
                Site
                <asp:DropDownList ID="ddl_buscarSite" OnSelectedIndexChanged="ddl_site_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-lg-1">
                <br />
                <asp:LinkButton ID="btn_buscar" CssClass="btn btn-primary" OnClick="btn_buscar_Click" runat="server">
                    <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="gv_listar" CssClass="table table-hover table-bordered tablita" AutoGenerateColumns="false" EmptyDataText="No se encontraron devoluciones" runat="server"
                DataKeyNames="SOLI_ID_CARGA,LUGA_ID_CARGA,SOAN_ORDEN_CARGA,SOLI_ID_DESCARGA,LUGA_ID_DESCARGA,SOAN_ORDEN_DESCARGA,DEVO_ID" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting" OnRowCreated="gv_listar_RowCreated" OnRowDataBound="gv_listar_RowDataBound">
                <Columns>
                 <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_descargar" ToolTip="DESCARGAR" CommandName="DESCARGAR" CommandArgument='<%# Container.DataItemIndex %>' runat="server">
                                <span class="glyphicon glyphicon-save" />
                            </asp:LinkButton>
                               </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                                    <asp:LinkButton ID="btn_editar" ToolTip="RE - DESPACHAR" CommandName="EDITAR" CommandArgument='<%# Eval("DEVO_ID") %>' runat="server">
                                <span class="glyphicon glyphicon-share-alt" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DEVO_ID" SortExpression="DEVO_ID" HeaderText="Devo_id" />
                    <asp:BoundField DataField="SITE_NOMBRE" SortExpression="SITE_NOMBRE" HeaderText="Site" />
                    <asp:BoundField DataField="viaje" SortExpression="viaje" HeaderText="Viaje" />
                    <asp:BoundField DataField="NRO_FLOTA" SortExpression="NRO_FLOTA" HeaderText="N° Trailer" />
                    <asp:BoundField DataField="PLACA" SortExpression="PLACA" HeaderText="Patente Trailer" />
                     <asp:BoundField DataField="LUGAR" SortExpression="LUGAR" HeaderText="Posicion" />
                       <asp:BoundField DataField="tipo_carga" SortExpression="tipo_carga" HeaderText="Tipo Carga" />
                    <asp:BoundField DataField="motivo_carga" SortExpression="motivo_carga" HeaderText="Motivo" />

                    <asp:BoundField DataField="DEES_DESC" SortExpression="DEES_DESC" HeaderText="Estado" />
                    <asp:BoundField DataField="SOLI_ID_DEVOLUCION" SortExpression="SOLI_ID_DEVOLUCION" HeaderText="Solicitud Devolución" />
                    <asp:BoundField DataField="DEVO_FH" SortExpression="DEVO_FH" HeaderText="Fecha devolución" />
                    <asp:BoundField DataField="SOLI_ID_DESCARGA" SortExpression="SOLI_ID_DESCARGA" HeaderText="Solicitud Descarga" />
                    <asp:BoundField DataField="LUGAR_DESCARGA" SortExpression="LUGAR_DESCARGA" HeaderText="Andén Descarga" />
                    <asp:BoundField DataField="NRO_FLOTA_DESCARGA" SortExpression="NRO_FLOTA_DESCARGA" HeaderText="N° Trailer Descarga" />
                    <asp:BoundField DataField="PLACA_DESCARGA" SortExpression="PLACA_DESCARGA" HeaderText="Patente Trailer Descarga" />
                    <asp:BoundField DataField="DEVO_FH_DESCARGA" SortExpression="DEVO_FH_DESCARGA" HeaderText="Fecha descarga" />
                    <asp:BoundField DataField="ESTADO_DESCARGA" SortExpression="ESTADO_DESCARGA" HeaderText="Estado descarga" />

                    <asp:BoundField DataField="SOLI_ID_CARGA" SortExpression="SOLI_ID_CARGA" HeaderText="Solicitud Carga" />
                    <asp:BoundField DataField="LUGAR_CARGA" SortExpression="LUGAR_CARGA" HeaderText="Andén Carga" />
                    <asp:BoundField DataField="NRO_FLOTA_CARGA" SortExpression="NRO_FLOTA_CARGA" HeaderText="N° Trailer Carga" />
                    <asp:BoundField DataField="PLACA_CARGA" SortExpression="PLACA_CARGA" HeaderText="Patente Trailer Carga" />
                    <asp:BoundField DataField="DEVO_FH_CARGA" SortExpression="DEVO_FH_CARGA" HeaderText="Fecha carga" />
                    <asp:BoundField DataField="ESTADO_CARGA" SortExpression="ESTADO_CARGA" HeaderText="Estado carga" />
                    <asp:BoundField DataField="DEVO_OBS" SortExpression="DEVO_OBS" HeaderText="Observación" />
                    <asp:BoundField DataField="TRUE_COD_INTERNO_IN_SALIDA" SortExpression="TRUE_COD_INTERNO_IN_SALIDA" HeaderText="Cod Interno" />
                    <%--<asp:BoundField DataField="USUA_ID_DESCARGA" SortExpression="USUA_ID_DESCARGA" HeaderText="Usua_id_descarga" />--%>
                    <%--<asp:BoundField DataField="USUA_ID_CARGA" SortExpression="USUA_ID_CARGA" HeaderText="Usua_id_carga" />--%>
                    <asp:BoundField DataField="DEVO_FH_CIERRE" SortExpression="DEVO_FH_CIERRE" HeaderText="Devo_fh_cierre" />
                    <%--<asp:BoundField DataField="USUA_ID_ACTUALIZA" SortExpression="USUA_ID_ACTUALIZA" HeaderText="Usua_id_actualiza" />--%>
                    <asp:BoundField DataField="DEVO_FH_ACTUALIZA" SortExpression="DEVO_FH_ACTUALIZA" HeaderText="Devo_fh_actualiza" />
                    <asp:TemplateField HeaderText="Carga" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_cargaCompleta" ToolTip="Completar Carga" runat="server" CommandName="CARGA_COMPLETA" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-check" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_cargaAnden" ToolTip="Anden Listo" runat="server" CommandName="CARGA_COMIENZA" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-thumbs-up" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_cargaSello" ToolTip="Validar Sello" runat="server" CommandName="CARGA_SELLO" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Eval("SOLI_ID_CARGA") %>'>
                  <span class="glyphicon glyphicon-arrow-down" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_cargaEstacionamiento" ToolTip="A Estacionamiento" runat="server" CommandName="CARGA_FINALIZAR" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Eval("SOLI_ID_CARGA") %>'>
                  <span class="glyphicon glyphicon-arrow-up" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_cargaParcial" ToolTip="Interrumpir Carga" runat="server" CommandName="CARGA_PARCIAL" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Container.DataItemIndex %>'  visible="false">
                  <span class="glyphicon glyphicon-pause" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_cargaContinuar" ToolTip="Continuar Carga" runat="server" CommandName="CARGA_CONTINUAR" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-play" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descarga">
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_descargaCompleta" runat="server" CommandName="DESCARGA_COMPLETA" CssClass="btn btn-xs btn-primary" ToolTip="Completar Descarga" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                               <asp:LinkButton ID="btn_descargaBloquear" runat="server" CommandName="DESCARGA_BLOQUEAR" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-xs btn-primary" ToolTip="Bloquear Andén">
                  <span class="glyphicon glyphicon-lock" />
                            </asp:LinkButton>

                             <asp:LinkButton ID="btn_devolucionCompleta" runat="server" CommandName="DEVOLUCION_COMPLETA" CssClass="btn btn-xs btn-primary" ToolTip="Finalizar Devolucion" CommandArgument='<%# Container.DataItemIndex %>'>
                  <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>
                   
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
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
                            <div id="dv_pallets" runat="server" class="">
                                Pallets cargados
                <br />
                                <asp:TextBox ID="txt_palletsCargados" CssClass="form-control" runat="server" />
                            </div>
                            <div class="">
                                Fecha
                <br />
                                <asp:TextBox ID="txt_fechaCarga" CssClass="form-control input-fecha" runat="server" Enabled="false" />
                            </div>
                            <div class="">
                                Hora
                <br />
                                <asp:TextBox ID="txt_horaCarga" CssClass="form-control input-hora" runat="server" Enabled="false" />
                            </div>
                            <div class="separador"></div>
                            <div class="" style="text-align: center">
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
                            <div class="col-lg-6 ">
                                Local
                <br />
                                <div class="col-lg-4 ">
                                    <asp:TextBox ID="txt_reanudarCodLocal" OnTextChanged="txt_reanudarCodLocal_TextChanged" CssClass="input-number form-control" AutoPostBack="true" runat="server" />
                                </div>
                                <div class="col-lg-8 ">
                                    <asp:TextBox ID="txt_reanudarLocal" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="col-lg-4 ">
                                Andén
                <br />
                                <asp:DropDownList ID="ddl_reanudarAnden" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                            <div class="">
                                <br />
                                <asp:LinkButton ID="btn_agregarCarga" CssClass="btn btn-primary" ToolTip="Nuevo Local" runat="server" OnClick="btn_agregarCarga_Click">
                  <span class="glyphicon glyphicon-plus" />
                                </asp:LinkButton>
                            </div>
                            <div class="separador"></div>
                            <div class="container-fluid" style="overflow: auto; height: 45vh;">
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
                                                <asp:LinkButton ID="btnSubir" CommandArgument='<%# Container.DataItemIndex %>' CommandName="SUBIR" runat="server">
                          <span class="glyphicon glyphicon-menu-up" />
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnBajar" CommandArgument='<%# Container.DataItemIndex %>' CommandName="BAJAR" runat="server">
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
                            <div class="separador"></div>
                            <div class="" style="text-align: center">
                                <asp:LinkButton ID="btn_reanudar" runat="server" OnClick="btn_reanudar_Click" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_emergencia" runat="server" OnClick="btn_emergencia_Click" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_locales" runat="server" OnClick="btn_locales_Click" CssClass="btn btn-primary">
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
    <div class="modal fade" id="modalBloqueoAnden" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Bloquear Andén
                            </h4>
                        </div>
                        <div class="modal-body" style="overflow-y: auto; height: auto">
                            <div class="">
                                <div class="">
                                    Posición
                  <br />
                                    <asp:DropDownList ID="ddl_bloquearPos" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0" Text="Seleccione..." />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-12 separador"></div>
                                <div class="">
                                    <br />
                                    <asp:LinkButton ID="btn_bloquearAgregar" OnClick="btn_bloquearAgregar_Click" runat="server" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-plus" />
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="separador">
                            </div>
                            <div class="">
                                <asp:GridView ID="gv_bloqueo" OnRowCommand="gv_bloqueo_RowCommand" AutoGenerateColumns="false"
                                    CssClass="table table-bordered table-hover tablita" runat="server">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_quitar" runat="server" CommandName="QUITAR" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Eval("ID") %>'>
                          <span class="glyphicon glyphicon-erase" />
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
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="modalDescarga" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-lg" role="dialog">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4></h4>
                        </div>
                        <div class="modal-body" style="height:auto;overflow:auto;">
                            <div class="col-lg-2">
                                Fecha
                                <br />
                                <asp:TextBox ID="txt_descargaFecha" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-1">
                                Hora
                                <br />
                                <asp:TextBox ID="txt_descargaHora" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-2">
                                N° Flota
                                <br />
                                <asp:TextBox ID="txt_descargaNro" Enabled="false" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-2">
                                Placa
                                <br />
                                <asp:TextBox ID="txt_descargaPlaca" Enabled="false" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-3">
                                Transportista
                                <br />
                                <asp:TextBox ID="txt_descargaTransportista" Enabled="false" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-1">
                                Extranjero
                                <br />
                                <asp:CheckBox ID="chk_descargaExtranjero" Enabled="false" runat="server" />
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-6">
                                <h4>Origen
                                </h4>
                                <div class="col-lg-3">
                                    <asp:Panel ID="pnl_lugarOrigen" CssClass="lugar-especifico" runat="server" Style="">
                                        <asp:Panel ID="pnl_detalleLugar" CssClass="row columna-anden detalle-lugar-especifico" runat="server">
                                            <asp:Label ID="lbl_lugar" runat="server" />
                                        </asp:Panel>
                                        <asp:Panel ID="pnl_detalleTrailer" CssClass="row columna-anden detalle-trailer-especifico" runat="server">
                                            <asp:Panel Style="width: 20px; border-radius: 20px;" ID="pnl_imgAlerta" runat="server">
                                                <img style="width: 20px; border-radius: 20px;" src="../img/reloj.png" />
                                            </asp:Panel>
                                            <asp:Image ID="img_trailer" Width="20px" runat="server" />
                                        </asp:Panel>
                                    </asp:Panel>
                                </div>
                                <div id="dv_origen" class="col-lg-9 ">
                                    Zona:
                                    <asp:Label ID="lbl_descargaZonaOri" runat="server" />
                                    <br />
                                    Playa:
                                    <asp:Label ID="lbl_descargaPlayaOri" runat="server" />
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <h4>Destino
                                </h4>
                                <div class="col-lg-3">
                                    Zona
                                </div>
                                <div class="col-lg-9">
                                    <asp:DropDownList ID="ddl_descargaZona" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_destinoZona_SelectedIndexChanged">
                                        <asp:ListItem>Seleccione...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-12 separador"></div>
                                <div class="col-lg-3">
                                    Playa
                                </div>
                                <div class="col-lg-9">
                                    <asp:DropDownList ID="ddl_descargaPlaya" CssClass="form-control" runat="server" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddl_destinoPlaya_SelectedIndexChanged">
                                        <asp:ListItem>Seleccione...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-12 separador"></div>
                                <div class="col-lg-3">
                                    Posición
                                </div>
                                <div class="col-lg-9">
                                    <asp:DropDownList ID="ddl_descargaPos" CssClass="form-control" Enabled="false" runat="server">
                                        <asp:ListItem>Seleccione...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="col-lg-12 separador"></div>
                           <div style="visibility:hidden; display:none; ">
                            <h4>Destinos Bloqueados
                            </h4>
                            <div class="col-lg-2">
                                <asp:DropDownList ID="ddl_descargaLugarBloqueo" CssClass="form-control" Enabled="false" runat="server">
                                    <asp:ListItem>Seleccione...</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-1">
                                <asp:LinkButton ID="btn_descargaBloquear" CssClass="btn btn-primary" runat="server" ToolTip="Agregar A reservados" OnClick="btn_descargaBloquear_Click">
                <span class="glyphicon glyphicon-calendar" />
                                </asp:LinkButton>
                            </div>
                            <div class="col-lg-12 separador"></div>
                            <div class="col-lg-12">
                                <asp:GridView ID="gv_descargaBloqueados" CssClass="table table-bordered table-hover tablita" runat="server" EmptyDataText="Sin lugares reservados"
                                    AutoGenerateColumns="False" Width="100%" OnRowCommand="gv_Seleccionados_RowCommand" OnRowCreated="gv_Seleccionados_RowCreated">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_descargaDesbloquear" CssClass="btn btn-primary btn-xs" CommandArgument='<%# Eval("POSICION1") %>' CommandName="ELIMINAR" runat="server">
                        <span class="glyphicon glyphicon-erase" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="ZONA" DataField="ZONA" SortExpression="ZONA" ItemStyle-Width="30%" HeaderStyle-Width="30%" />
                                        <asp:BoundField HeaderText="PLAYA" DataField="PLAYA" SortExpression="PLAYA" ItemStyle-Width="30%" HeaderStyle-Width="30%" />
                                        <asp:BoundField HeaderText="POSICION" DataField="POSICION" SortExpression="POSICION" ItemStyle-Width="20%" HeaderStyle-Width="20%" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-lg-12 separador"></div>
                            </div>
                            <div class="col-lg-12" style="text-align:center">
                                <asp:LinkButton ID="btn_confirmar" CssClass="btn btn-primary" runat="server" ToolTip="Crear Descarga" OnClick="btn_confirmar_Click">
                            <span class="glyphicon glyphicon-ok" />
                                </asp:LinkButton>
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
    <div class="modal fade" id="modalConfirmar" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
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
                            <asp:LinkButton ID="btn_confEliminarCarga" CssClass="btn btn-primary" OnClick="btn_confEliminarCarga_Click" runat="server">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_confComenzarCarga" CssClass="btn btn-primary" OnClick="btn_confComenzarCarga_Click" runat="server">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_confFinalizarDevolucion" CssClass="btn btn-primary" OnClick="btn_confFinalizarDevolucion_Click" runat="server">
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
            <asp:HiddenField ID="hf_devoId" runat="server" />
            <asp:HiddenField ID="hf_soliId" runat="server" />
            <asp:HiddenField ID="hf_lugaId" runat="server" />
            <asp:HiddenField ID="hf_soanOrden" runat="server" />
            <asp:HiddenField ID="hf_localesSeleccionados" runat="server" />
            <asp:HiddenField ID="hf_localesCompatibles" runat="server" />
            <asp:HiddenField ID="hf_caractSolicitud" runat="server" />
            <asp:HiddenField ID="hf_maxPallets" runat="server" />
            <asp:HiddenField ID="hf_timeStamp" runat="server" />
            <asp:HiddenField ID="hf_accion" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);

        var pageScrollPos = 0;
        var pageScrollPosleft = 0;

        function BeginRequestHandler(sender, args) {
            pageScrollPos = $('div.dataTables_scrollBody').scrollTop();
            pageScrollPosleft = $('div.dataTables_scrollBody').scrollLeft();
        }
        var calcDataTableHeight = function () {
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
        function EndRequestHandler1(sender, args) {
            tabla();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
        var tick_recarga;
        $(document).ready(function (e) {

            clearInterval(tick_recarga);
            tick_recarga = setInterval(click_recarga, 90000);

        });

        function click_recarga() {

            if (($('.modal:visible').length && $('body').hasClass('modal-open')) != true)
                $('#<%= btn_buscar.ClientID %>')[0].click();

        }
    </script>
</asp:Content>

