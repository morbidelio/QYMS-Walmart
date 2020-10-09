<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Control_Carga.aspx.cs" Inherits="App_Control_Carga" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Control de Carga
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server" ID="updbuscar">
        <ContentTemplate>
            <div class="col-xs-12 separador">
            </div>
            <asp:Panel ID="SITE" runat="server">
                <div class="col-xs-1">
                    Site
          <br />
                    <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddl_buscarSite_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </asp:Panel>
            <div class="col-xs-2">
                Playa
        <br />
                <asp:DropDownList ID="ddl_buscarPlaya" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddl_buscarPlaya_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                Andén
        <br />
                <asp:DropDownList ID="ddl_buscarAnden" CssClass="form-control" runat="server">
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
                </asp:DropDownList>
            </div>
            <div class="col-xs-2">
                Transportista
        <br />
                <asp:DropDownList ID="ddl_buscarTransportista" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                ID Ruta
        <br />
                <asp:TextBox ID="txt_buscarRuta" CssClass="form-control" runat="server" />
            </div>
            <div class="col-xs-1">
                <br />
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" ClientIDMode="Static"
                    ToolTip="Buscar Solicitudes" runat="server" CausesValidation="false">
          <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_export" ToolTip="Exportar a Excel" CssClass="btn btn-primary" CausesValidation="false" OnClick="btn_export_Click" runat="server">
          <span class="glyphicon glyphicon-import" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_buscar" EventName="click" />
            <asp:PostBackTrigger ControlID="btn_export" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <div class="col-xs-12">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gv_listar" runat="server" DataKeyNames="ID_SOLICITUD,ID_SOLICITUDANDEN,ORDEN" Width="2400px"
                    CssClass="table table-bordered table-hover tablita" EmptyDataText="No Existen Movimientos Asignados!" AutoGenerateColumns="false"
                    AllowSorting="True" UseAccessibleHeader="true" EnableViewState="false"
                    OnSorting="gv_listar_Sorting" OnRowDataBound="gv_listar_RowDataBound" OnRowCreated="gv_listar_RowCreated" OnRowCommand="gv_listar_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Editar" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_editar" runat="server" CommandName="Edit"
                                    CssClass="btn btn-xs btn-primary" ToolTip="Modificar" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_AndenEmergencia" runat="server" CausesValidation="False" CommandName="AndenEmergencia"
                                    CssClass="btn btn-xs btn-primary" ToolTip="Anden Emergencia" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-pause" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_locales" runat="server" CausesValidation="False" CommandName="locales"
                                    CssClass="btn btn-xs btn-primary" ToolTip="Locales" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-pause" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Solicitud" DataField="ID_SOLICITUD" SortExpression="ID_SOLICITUD" HeaderStyle-Width="1%" />
                        <asp:BoundField HeaderText="Id Solicitud Anden" DataField="ID_SOLICITUDANDEN" SortExpression="ID_SOLICITUDANDEN" Visible="false" />
                        <asp:BoundField HeaderText="Orden" DataField="ORDEN" SortExpression="ORDEN" />
                        <asp:BoundField HeaderText="Andén Carga" DataField="LUGAR" SortExpression="LUGAR" />
                        <asp:BoundField HeaderText="ID Shortec" DataField="ID_SHORTEK" SortExpression="ID_SHORTEK" />
                        <asp:BoundField HeaderText="Fecha Cumplimiento" DataField="FECHA_PUESTA_ANDEN" SortExpression="FECHA_PUESTA_ANDEN_2" />
                        <asp:BoundField HeaderText="Tiempo en carga" DataField="TIEMPO_CARGA" SortExpression="TIEMPO_CARGA" />
                        <asp:BoundField HeaderText="ID Ruta" DataField="SOLI_RUTA" SortExpression="SOLI_RUTA" />
                        <asp:BoundField HeaderText="Estado Solicitud" DataField="ESTADOSOLICITUD" SortExpression="ESTADOSOLICITUD" />
                        <asp:BoundField HeaderText="Fecha Creación" DataField="SOLI_FH_CREACION" SortExpression="SOLI_FH_CREACION_2" />
                        <asp:BoundField HeaderText="Id Estado Andén" DataField="ID_ESTADOANDEN" SortExpression="ID_ESTADOANDEN" Visible="false" />
                        <asp:BoundField HeaderText="Id Estado Solicitud" DataField="ID_ESTADOSOLICITUD" SortExpression="ID_ESTADOSOLICITUD" Visible="false" />
                        <asp:BoundField HeaderText="Fecha Andén" DataField="FECHA_RESERVA_ANDEN" SortExpression="FECHA_RESERVA_ANDEN_2" />
                        <asp:BoundField HeaderText="Local" DataField="LOCALES" SortExpression="LOCALES" />
                        <asp:BoundField HeaderText="Regiones" DataField="REGIONES" SortExpression="REGIONES" />
                        <asp:BoundField HeaderText="Transporte" DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" />
                        <asp:BoundField HeaderText="N° Flota" DataField="NRO_FLOTA" SortExpression="NRO_FLOTA" />
                        <asp:BoundField HeaderText="Trailer Patente" DataField="PATENTE" SortExpression="PATENTE" />
                        <asp:BoundField HeaderText="Plancha" DataField="Plancha" SortExpression="Plancha" />
                        <asp:BoundField HeaderText="Playa" DataField="PLAYA" SortExpression="PLAYA" Visible="false" />
                        <asp:BoundField HeaderText="Id Lugar" DataField="ID_LUGAR" SortExpression="ID_LUGAR" Visible="false" />
                        <asp:BoundField HeaderText="Tipo Carga" DataField="FRIO" SortExpression="FRIO" />
                        <asp:BoundField HeaderText="Temp" DataField="TEMPERATURA" SortExpression="TEMPERATURA" />
                        <asp:BoundField HeaderText="Usuario" DataField="Usuario" SortExpression="Usuario" />
                        <asp:BoundField HeaderText="jornada" DataField="jornada" SortExpression="jornada" />
                        <asp:TemplateField HeaderText="Carga" ShowHeader="False" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_cargado" runat="server" CausesValidation="True" CommandName="Cargado"
                                    CssClass="btn btn-xs btn-primary" ToolTip="Completar Carga" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-check" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_sello" runat="server" CausesValidation="True" CommandName="colocar_sello"
                                    CssClass="btn btn-xs btn-primary" ToolTip="Sello" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-arrow-down" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_valida_sello" runat="server" CausesValidation="True" CommandName="validar_sello"
                                    CssClass="btn btn-xs btn-primary" ToolTip="A Estacionamiento" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-arrow-up" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Parcial" ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_cargaParcial" runat="server" CommandName="Parcial"
                                    CssClass="btn btn-xs btn-primary" ToolTip="Interrumpir Carga" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-pause" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_cargaContinuar" Visible="false" runat="server" CommandName="Continuar"
                                    CssClass="btn btn-xs btn-primary" ToolTip="Continuar Carga" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-play" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cancelar" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_solicitudCancelar" runat="server" CommandName="CANCELAR"
                                    CssClass="btn btn-xs btn-primary" ToolTip="Cancelar Solicitud" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
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
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
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
                                <asp:RequiredFieldValidator ID="rfv_pallets" runat="server" ControlToValidate="txt_palletsCargados"
                                    Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                    ValidationGroup="carga">
                                </asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                    Enabled="True" TargetControlID="rfv_pallets">
                                </asp:ValidatorCalloutExtender>
                            </div>
                            <div class="col-xs-4">
                                Fecha
                <br />
                                <asp:TextBox ID="txt_fechaCarga" CssClass="form-control input-fecha" runat="server" Enabled="false" />
                                <asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="txt_fechaCarga"
                                    Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                    ValidationGroup="carga">
                                </asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                    Enabled="True" TargetControlID="rfv_fecha">
                                </asp:ValidatorCalloutExtender>
                            </div>
                            <div class="col-xs-4">
                                Hora
                <br />
                                <asp:TextBox ID="txt_horaCarga" CssClass="form-control input-hora" runat="server" Enabled="false" />
                                <asp:RequiredFieldValidator ID="rfv_hora" runat="server" ControlToValidate="txt_palletsCargados"
                                    Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                    ValidationGroup="carga">
                                </asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                    Enabled="True" TargetControlID="rfv_hora">
                                </asp:ValidatorCalloutExtender>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton ID="btn_terminarCarga" runat="server" OnClick="btn_terminarCarga_Click" ValidationGroup="carga" CssClass="btn btn-primary">
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
                <Triggers>
                    <asp:PostBackTrigger ControlID="btn_terminarCarga" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="modalReanudar" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Reanudar Carga
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow-y: auto;">
                            <div class="col-lg-2 col-xs-3">
                                Local
                  <br />
                                <asp:TextBox runat="server" ID="txt_buscaLocal" OnTextChanged="txt_buscaLocal_TextChanged"
                                    CssClass="input-number form-control" AutoPostBack="true" />
                            </div>
                            <div class="col-lg-4 col-xs-8">
                                <br />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txt_descLocal" />
                            </div>
                            <div class="col-lg-4 col-xs-6">
                                Andén
                  <br />
                                <asp:DropDownList ID="ddl_origenAnden" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Value="0" Text="Seleccione..."></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-2">
                                <br />
                                <asp:LinkButton ID="btn_agregarCarga" CssClass="btn btn-primary" ToolTip="Nuevo Local" ValidationGroup="nuevaCarga"
                                    runat="server" OnClick="btn_agregarCarga_Click">
                    <span class="glyphicon glyphicon-plus" />
                                </asp:LinkButton>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12 container-fluid" style="overflow: auto; height: 45vh;">
                                <asp:GridView ID="gv_solLocales" runat="server" CellPadding="8" GridLines="Horizontal" OnRowCommand="gv_solLocales_RowCommand" OnRowDataBound="gv_solLocales_rowDataBound"
                                    CssClass="table table-bordered table-hover tablita" EmptyDataText="No Existen Andenes/Locales Asignados!" AutoGenerateColumns="false"
                                    Width="100%" AllowSorting="True">
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
                                        <asp:BoundField HeaderText="Id Andén" DataField="ID_ANDEN" SortExpression="ID_ANDEN" Visible="false" />
                                        <asp:BoundField HeaderText="id Local" DataField="ID_LOCAL" SortExpression="ID_LOCAL" Visible="false" />
                                        <asp:BoundField HeaderText="Orden Andén" DataField="SOAN_ORDEN" SortExpression="SOAN_ORDEN" Visible="false" />
                                        <asp:BoundField HeaderText="Orden Local" DataField="ORDEN" SortExpression="ORDEN" Visible="false" />
                                        <asp:BoundField HeaderText="N° Local" DataField="numero_LOCAL" SortExpression="numero_LOCAL" Visible="true" />
                                        <asp:BoundField HeaderText="Nombre Local" DataField="NOMBRE_LOCAL" SortExpression="NOMBRE_LOCAL" />
                                        <asp:BoundField HeaderText="Andén" DataField="ANDEN" SortExpression="ANDEN" />
                                        <asp:BoundField HeaderText="ID Estado" DataField="SOES_ID" SortExpression="SOES_ID" Visible="false" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12">
                                <center>
                  <asp:LinkButton ID="btn_reanudar" runat="server" OnClick="btn_reanudar_Click"
                                  CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                  </asp:LinkButton>
                  <asp:LinkButton ID="btn_emergencia" runat="server" OnClick="btn_emergencia_Click"
                                  CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                  </asp:LinkButton>
                </center>
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
    <div class="modal fade" id="modalConf" data-backdrop="static" role="dialog">
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
                            <asp:LinkButton ID="btn_eliminarLocal" runat="server"
                                CssClass="btn btn-primary" OnClick="btn_eliminarLocal_Click">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_eliminarSolicitud" runat="server"
                                CssClass="btn btn-primary" OnClick="btn_eliminarSolicitud_Click">
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
    <div class="modal fade" id="modal_locales" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-lg" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>
                                <asp:Label ID="Label1" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="col-xs-12">
                                <div class="col-lg-2 col-xs-3">
                                    Local
                  <br />
                                    <asp:TextBox runat="server" ID="local2" OnTextChanged="txt_buscaLocal2_TextChanged" CssClass="input-number form-control" AutoPostBack="true" />
                                </div>
                                <div class="col-lg-4 col-xs-8">
                                    <br />
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txt_local2" />
                                </div>
                                <div class="col-lg-4 col-xs-6">
                                    Andén
                  <br />
                                    <asp:DropDownList ID="ddl_anden_local2" CssClass="form-control" runat="server">
                                        <asp:ListItem Selected="True" Value="0" Text="Seleccione..."></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-2">
                                    <br />
                                    <asp:LinkButton ID="agrega_local2" CssClass="btn btn-primary" ToolTip="Nuevo Local" ValidationGroup="nuevaCarga2"
                                        runat="server" OnClick="btn_agregarCarga2_Click">
                    <span class="glyphicon glyphicon-plus" />
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <asp:GridView ID="gv_locales2" runat="server" OnRowCommand="gv_Locales2_RowCommand" OnRowDataBound="gv_Locales2_rowDataBound"
                                CssClass="table table-bordered table-hover tablita" EmptyDataText="No Existen Andenes/Locales Asignados!" AutoGenerateColumns="false"
                                Width="100%" AllowSorting="True">
                                <Columns>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btn_eliminarLocal" CausesValidation="False" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Container.DataItemIndex %>'
                                                CommandName="ELIMINAR" ToolTip="Eliminar" runat="server">
                        <span class="glyphicon glyphicon-erase" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSubir" CausesValidation="False" CommandArgument='<%# Container.DataItemIndex %>'
                                                CommandName="SUBIR" runat="server">
                        <span class="glyphicon glyphicon-menu-up" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnBajar" CausesValidation="False" CommandArgument='<%# Container.DataItemIndex %>'
                                                CommandName="BAJAR" runat="server">
                        <span class="glyphicon glyphicon-menu-down" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Id Andén" DataField="ID_ANDEN" SortExpression="ID_ANDEN" Visible="false" />
                                    <asp:BoundField HeaderText="id Local" DataField="ID_LOCAL" SortExpression="ID_LOCAL" Visible="false" />
                                    <asp:BoundField HeaderText="Orden Andén" DataField="SOAN_ORDEN" SortExpression="SOAN_ORDEN" Visible="false" />
                                    <asp:BoundField HeaderText="Orden Local" DataField="ORDEN" SortExpression="ORDEN" Visible="false" />
                                    <asp:BoundField HeaderText="N° Local" DataField="numero_LOCAL" SortExpression="numero_LOCAL" Visible="true" />
                                    <asp:BoundField HeaderText="Nombre Local" DataField="NOMBRE_LOCAL" SortExpression="NOMBRE_LOCAL" />
                                    <asp:BoundField HeaderText="Andén" DataField="ANDEN" SortExpression="ANDEN" />
                                    <asp:BoundField HeaderText="ID Estado" DataField="SOES_ID" SortExpression="SOES_ID" Visible="false" />
                                </Columns>
                            </asp:GridView>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12">
                                <center>
                  <asp:LinkButton ID="btn_guardar_local" runat="server" OnClick="btn_local_Click"
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idLugar" runat="server" />
            <asp:HiddenField ID="hf_idLocal" runat="server" />
            <asp:HiddenField ID="hf_idSolicitud" runat="server" />
            <asp:HiddenField ID="hf_idEstado" runat="server" />
            <asp:HiddenField ID="hf_orden" runat="server" />
            <asp:HiddenField ID="hf_localesSeleccionados" Value="" runat="server" />
            <asp:HiddenField ID="hf_localesCompatibles" Value="" runat="server" />
            <asp:HiddenField ID="hf_caractSolicitud" Value="" runat="server" />
            <asp:HiddenField ID="hf_timeStamp" Value="" runat="server" />
            <asp:HiddenField ID="hf_ordenAndenesOk" Value="true" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
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

        function EndRequestHandler1(sender, args) {
            setTimeout(tabla, 100);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);

        var calcDataTableHeight = function () {
            //   alert($(window).height() - $("#scrolls").offset().top);
            return $(window).height() - $("#scrolls").offset().top - 100;
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
                    "info":false
                });
                $('div.dataTables_scrollBody').scrollTop(pageScrollPos);
                $('div.dataTables_scrollBody').scrollLeft(pageScrollPosleft);
            }
        }

        function modalCarga() {
            $("#modalCarga").modal();
        }

        function modalLocales() {
            $("#modal_locales").modal();
        }

        function modalReanudar() {
            $("#modalReanudar").modal();
        }

        function modalConfirmar() {
            $("#modalConfirmar").modal();
        }

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
    </script>
</asp:Content>
