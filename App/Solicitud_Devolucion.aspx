<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Solicitud_Devolucion.aspx.cs" Inherits="App_Solicitud_Carga" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Solicitud Devolución
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador"></div>
    <asp:UpdatePanel runat="server" ID="filros">
        <ContentTemplate>
            <small>
                <div class="col-xs-4 col-lg-1">
                    N° Solicitud
            <br />
                    <asp:TextBox ID="txt_solNumero" CssClass="form-control" runat="server" Enabled="false" />
                </div>
                <asp:Panel ID="SITE" runat="server">
                    <div class="col-xs-7 col-lg-2">
                        Site
              <br />
                        <asp:DropDownList runat="server" ID="dropsite" ClientIDMode="Static" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="drop_SelectedIndexChanged" />
                    </div>
                </asp:Panel>
                <div class="col-xs-12 col-lg-3" style="padding: 0px">
                    <div class="col-xs-3 col-lg-2">
                        Ways
              <br />
                        <asp:RadioButton ID="chk_solWays" runat="server" AutoPostBack="true" OnCheckedChanged="chk_frio_CheckedChanged" GroupName="rbTemp" />
                    </div>
                    <div class="col-xs-3 col-lg-2">
                        Frío
              <br />
                        <asp:RadioButton ID="chk_solFrio" runat="server" AutoPostBack="true" OnCheckedChanged="chk_frio_CheckedChanged" GroupName="rbTemp" />
                    </div>
                    <div class="col-xs-3 col-lg-2">
                        Seco
              <br />
                        <asp:RadioButton ID="chk_solSeco" runat="server" AutoPostBack="true" OnCheckedChanged="chk_frio_CheckedChanged" GroupName="rbTemp" />
                    </div>
                    <div class="col-xs-3 col-lg-2">
                        Multifrío
              <br />
                        <asp:RadioButton ID="chk_solMultifrio" runat="server" AutoPostBack="true" OnCheckedChanged="chk_frio_CheckedChanged" GroupName="rbTemp" />
                    </div>
                    <div class="col-xs-3 col-lg-4">
                        Congelado
              <br />
                        <asp:RadioButton ID="chk_solCongelado" runat="server" AutoPostBack="true" OnCheckedChanged="chk_frio_CheckedChanged" GroupName="rbTemp" />
                    </div>
                </div>
                <div class="col-xs-4 col-lg-1">
                    Temp
            <br />
                    <asp:DropDownList ID="DDL_TEMP" OnSelectedIndexChanged="DDL_TEMP_IndexChanged" CssClass="form-control drop" AutoPostBack="true" runat="server">
                        <asp:ListItem Value="0" Text="Seleccione..." />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv_temp" runat="server" ControlToValidate="DDL_TEMP" InitialValue="0"
                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="conf" />
                    <asp:ValidatorCalloutExtender ID="vce_temp" runat="server" Enabled="True" TargetControlID="rfv_temp" />
                </div>
                <div class="col-xs-6 col-lg-2">
                    Ubicación
            <br />
                    <asp:DropDownList ID="ddl_solPlaya" OnSelectedIndexChanged="ddl_solPlaya_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control drop" Enabled="false" runat="server">
                        <asp:ListItem Value="0" Text="Seleccione..." />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv_playa" runat="server" ControlToValidate="ddl_solPlaya" InitialValue="0"
                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="conf" />
                    <asp:ValidatorCalloutExtender ID="vce_playa" runat="server" Enabled="True" TargetControlID="rfv_playa" />
                </div>
                <div class="col-lg-1 col-xs-4">
                    Fecha
            <br />
                    <asp:TextBox ID="txt_solFecha" CssClass="form-control input-fecha" runat="server" />
                </div>
                <div class="col-lg-1 col-xs-3">
                    Hora
            <br />
                    <asp:TextBox ID="txt_solHora" CssClass="form-control input-hora" runat="server" />
                </div>
                <div class="col-lg-1 col-xs-4" style="display: none; visibility: hidden">
                    Total Pallets
            <br />
                    <asp:TextBox ID="txt_totalPallets" Width="50px" Text="0" CssClass="form-control input-number" runat="server" />
                </div>
            </small>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid" style="max-height: 60vh; overflow-y: auto">
                <div class="col-lg-6 col-xs-12">
                    <telerik:RadListBox ID="rlcli" runat="server" CheckBoxes="true" showcheckall="true" Visible="false" Width="90%" Height="100px" />
                    <div class="col-lg-2 col-xs-3">
                        Local
              <br />
                        <asp:TextBox runat="server" ID="txt_buscaLocal" OnTextChanged="txt_buscaLocal_TextChanged" CssClass="input-number form-control" AutoPostBack="true" />
                        <asp:RequiredFieldValidator ID="rfv_txt_buscaLocal" runat="server" ControlToValidate="txt_buscaLocal"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevaCarga" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="rfv_txt_buscaLocal" />
                    </div>
                    <div class="col-lg-4 col-xs-8">
                        <br />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_descLocal" />
                    </div>
                    <div class="col-lg-1 col-xs-3" style="display: none; visibility: hidden">
                        Pallets
              <br />
                        <asp:TextBox ID="txt_destinoPallets" CssClass="form-control input-number" runat="server" Visible="false" Text="0" />
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txt_destinoPallets" Operator="DataTypeCheck" Type="Integer" ValidationGroup="nuevaCarga"
                            ErrorMessage="Ingrese un Número" Display="Dynamic" Text="*" />
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txt_destinoPallets" Operator="GreaterThanEqual" ValidationGroup="nuevaCarga"
                            ValueToCompare="0" Type="Integer" ErrorMessage="ingrese un Número mayor a 0" Display="Dynamic" Text="*" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                            Enabled="True" TargetControlID="CompareValidator1" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                            Enabled="True" TargetControlID="CompareValidator2" />
                        <asp:DropDownList ID="ddl_destinoPallets" runat="server" Visible="false">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv_pallets" runat="server" ControlToValidate="txt_destinoPallets"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                            ValidationGroup="nuevaCarga" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                            Enabled="True" TargetControlID="rfv_pallets" />
                    </div>
                    <div class="col-lg-4 col-xs-6">
                        Andén
              <br />
                        <asp:DropDownList ID="ddl_origenAnden" CssClass="form-control" runat="server" Enabled="false">
                            <asp:ListItem Selected="True" Value="0" Text="Seleccione..." />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv_origenAnden" runat="server" ControlToValidate="ddl_origenAnden" InitialValue="0"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevaCarga" />
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="rfv_origenAnden" />
                    </div>
                    <div class="col-xs-2">
                        <br />
                        <asp:LinkButton ID="btn_agregarCarga" CssClass="btn btn-primary" ToolTip="Nuevo Local" ValidationGroup="nuevaCarga" runat="server" OnClick="btn_agregarCarga_Click">
                <span class="glyphicon glyphicon-plus" />
                        </asp:LinkButton>
                    </div>
                    <div class="col-xs-2" style="visibility: hidden; display: none">
                        Temperatura
              <br />
                        <asp:DropDownList ID="ddl_solTemp" runat="server" CssClass="form-control" Visible="false">
                            <asp:ListItem Text="Seleccione..." Value="0" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-xs-12 separador"></div>
                    <div class="col-xs-12 container-fluid" style="overflow: auto; height: 35vh;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gv_solLocales" CssClass="table table-bordered table-hover tablita" Width="100%" runat="server"
                                    OnRowCommand="gv_solLocales_RowCommand" OnRowDataBound="gv_solLocales_rowDataBound"
                                    DataKeyNames="SOAN_ORDEN,LOCA_ID" EmptyDataText="No Existen Andenes/Locales Asignados!" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_eliminarLocal" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Container.DataItemIndex %>'
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
                                        <asp:BoundField HeaderText="Orden Local" DataField="SOLD_ORDEN" />
                                        <asp:BoundField HeaderText="Orden Local (old)" DataField="SOLD_ORDEN_OLD" />
                                        <asp:BoundField HeaderText="Nombre Local" DataField="LOCAL" />
                                        <asp:BoundField HeaderText="Andén" DataField="ANDEN" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-lg-6 col-xs-12">
                    <div class="col-xs-6" style="font-weight: bolder">
                        <div class="col-xs-12 separador"></div>
                        <asp:CheckBox ID="chk_plancha" Enabled="false" AutoPostBack="true" runat="server" Text="Plancha" CssClass="resaltarchk form-check-input" />
                       <asp:CheckBox ID="chk_TRASVASIJE" Enabled="true" AutoPostBack="true" runat="server" Text="Trasvasije" CssClass="resaltarchk form-check-input" OnCheckedChanged="visibleasignamovilmanual" />
                 
                    </div>
                    <div class="col-xs-6">
                        Local Largo Max
              <br />
                        <asp:DropDownList ID="ddl_largoMax" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="calcula_solicitud" Enabled="false" />
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="visiblasignamanual" runat="server">
                                <div class="col-xs-12 separador"></div>
                                <div class="col-lg-4 col-xs-12">
                                    ID Shortrec
                    <br />
                                    <telerik:RadComboBox rendermode="Lightweight" ID="ddl_idShortek" OnSelectedIndexChanged="ddl_idShortek_SelectedIndexChanged" runat="server" CollapseAnimation-Duration="0" Filter="Contains" AutoPostBack="true"
                                        AllowCustomText="False" MarkFirstMatch="true" EmptyMessage="Seleccione Shortrec" ExpandAnimation-Duration="0" Width="100%" />
                                    <div class="col-lg-12 separador"></div>
                                    Ruta
                    <br />
                                    <asp:TextBox ID="txt_ruta" CssClass="form-control" runat="server" />
                                </div>
                                <div class="col-xs-12 col-lg-8" style="border: 3px solid; border-radius: 10px; padding-bottom: 10px">
                                    <asp:Panel ID="pnl_trailer" Enabled="false" runat="server">
                                        <h4 style="color: Navy">Asignar Trailer Manualmente
                                        </h4>
                                        <asp:CheckBox ID="chk_trailer" runat="server" Enabled="false" Visible="false"
                                            TextAlign="Left" OnCheckedChanged="chk_trailer_CheckedChanged" AutoPostBack="true" />
                                        <div class="col-lg-5 col-xs-4">
                                            ID Shortrec
                                            <br />
                                            <telerik:RadComboBox ID="ddl_id_shortrec2" runat="server" MarkFirstMatch="true" EmptyMessage="Seleccione Shortrec" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="carga_trailers_sh2" />
                                        </div>
                                        <div class="col-lg-5 col-xs-4">
                                            Trailer Patente
                                            <br />
                                            <asp:DropDownList ID="ddl_trailers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_trailers_SelectChanged" CssClass="form-control" />
                                            <div class="col-xs-12 separador"></div>
                                            <asp:TextBox ID="txt_trailerPatente" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-xs-12 separador"></div>
                                        <div class="col-lg-5 col-xs-4">Numero Flota</div>
                                        <div class="col-lg-5 col-xs-5">
                                            <asp:TextBox ID="txt_trailerNro" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-xs-12 separador"></div>
                                        <div class="col-lg-5 col-xs-4">ID Shortec</div>
                                        <div class="col-lg-5 col-xs-5">
                                            <asp:TextBox ID="txt_trailerShortek" CssClass="form-control" runat="server" />
                                        </div>
                                        <div class="col-lg-1 col-xs-1">
                                            <asp:LinkButton ID="btn_buscarTrailer" CssClass="btn btn-primary" ToolTip="Buscar Conductor" runat="server" OnClick="btn_buscarTrailer_Click">
                          <span class="glyphicon glyphicon-search" />
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-xs-12 separador"></div>
                                        <div class="col-xs-6 col-lg-5">Trailer con Devolucion</div>
                                        <div class="col-xs-6 col-lg-5">
                                            <asp:TextBox ID="txt_trailerTransporte_1" runat="server" Enabled="false" CssClass="form-control" />
                                        </div>
                                        <div class="col-xs-12 separador"></div>
                                        <div class="col-xs-6 col-lg-5">Playa Trasvasije</div>
                                        <div class="col-xs-6 col-lg-5">
                                            <asp:DropDownList ID="ddl_playaDescarga" OnSelectedIndexChanged="ddl_playaDescarga_SelectedIndexChanged" AutoPostBack="true" Enabled="false" runat="server" CssClass="form-control" />
                                        </div>
                                        <div class="col-xs-12 separador"></div>
                                        <div class="col-xs-6 col-lg-5">Andén Trasvasije</div>
                                        <div class="col-xs-6 col-lg-5">
                                            <asp:DropDownList ID="ddl_andenDescarga" runat="server" Enabled="false" CssClass="form-control" />
                                        </div>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chk_trailer" EventName="checkedchanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-xs-12 separador"></div>
            <div class="col-xs-12" style="text-align: center">
                <asp:LinkButton ID="btn_confirmarMov" CssClass="btn btn-primary" runat="server" ValidationGroup="conf" ToolTip="Confirmar Movimiento" OnClick="btn_confirmarMov_Click">
            <span class="glyphicon glyphicon-ok" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_anularSol" CssClass="btn btn-primary" runat="server" ToolTip="Anular Solicitud" OnClick="btn_anularSol_Click" Visible="false">
            <span class="glyphicon glyphicon-trash" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_limpiarDatos" CssClass="btn btn-primary" runat="server" ToolTip="Limpiar Datos" OnClick="btn_limpiarDatos_Click">
            <span class="glyphicon glyphicon-erase" />
                </asp:LinkButton>
                <asp:LinkButton ID="volver" CssClass="btn btn-primary" runat="server" ToolTip="Volver" OnClick="volver_click">
            <span class="glyphicon glyphicon-remove" />
                </asp:LinkButton>
                <asp:LinkButton ID="volver2" CssClass="btn btn-primary" runat="server" ToolTip="Volver" OnClick="volver2_click">
            <span class="glyphicon glyphicon-remove" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_soliId" Value="0" runat="server" />
            <asp:HiddenField ID="hf_devoId" Value="0" runat="server" />
            <asp:HiddenField ID="hf_ordenAndenesOk" Value="true" runat="server" />
            <asp:HiddenField ID="hf_traiId" Value="" runat="server" />
            <asp:HiddenField ID="hf_traiIdOld" Value="" runat="server" />
            <asp:HiddenField ID="hf_trailerAuto" Value="false" runat="server" />
            <asp:HiddenField ID="hf_localesSeleccionados" Value="" runat="server" />
            <asp:HiddenField ID="hf_localesCompatibles" Value="" runat="server" />
            <asp:HiddenField ID="hf_caractSolicitud" Value="" runat="server" />
            <asp:HiddenField ID="hf_timeStamp" Value="" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function EndRequestHandler(sender, args) {
            $('#<%= txt_trailerPatente.ClientID %>').keypress(function (event) {
                $('#<%= txt_trailerNro.ClientID %>').val('');
            });
            $('#<%= txt_trailerNro.ClientID %>').keypress(function (event) {
                $('#<%= txt_trailerPatente.ClientID %>').val('');
            });
            $('#<%= btn_buscarTrailer.ClientID %>').click(function () {
                if ($('#<%= txt_trailerNro.ClientID %>').val() == '' &&
                    $('#<%= txt_trailerPatente.ClientID %>').val() == '') {
                    $('#<%= hf_traiId.ClientID %>').val('');
                    showAlertClass("buscarTrailer", "warn_placaNroVacio");
                    return false;
                }
            });
            $('#<%= btn_confirmarMov.ClientID %>').click(function () {
                if ($('#<%= hf_traiId.ClientID %>').val() == '') {
                    showAlertClass("editar", "warn_trailerVacio");
                    return false;
                }
                if ($('#<%= hf_traiId.ClientID %>').val() != $('#<%= hf_traiIdOld.ClientID %>').val() &&
                    ($('#<%= ddl_andenDescarga.ClientID %>').val() == null ||
                    $('#<%= ddl_andenDescarga.ClientID %>').val() == '' ||
                    $('#<%= ddl_andenDescarga.ClientID %>').val() == '0')) {
                    showAlertClass("editar", "warn_andenDescargaVacio");
                    return false;
                }
                if ($('#<%= hf_ordenAndenesOk.ClientID %>').val() == '') {
                    showAlertClass("editar", "warn_ordenAnden");
                    return false;
                }
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
    </script>
    <style type="text/css">
        .resaltarchk input {
            /* Doble-tamaño Checkboxes */
            -ms-transform: scale(2); /* IE */
            -moz-transform: scale(2); /* FF */
            -webkit-transform: scale(2); /* Safari y Chrome */
            -o-transform: scale(2); /* Opera */
            margin: 10px;
        }

        .resaltarchk label {
            font-size: large
        }
    </style>
</asp:Content>
