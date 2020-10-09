<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Usuario.aspx.cs" Inherits="App_Usuario" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <h2>Maestro Usuario
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador">
    </div>

    <div class="col-xs-2">
        Tipo Usuario
    <br />
        <asp:DropDownList ID="ddl_buscarTipoUsuario" CssClass="form-control" runat="server">
        </asp:DropDownList>
    </div>
    <div class="col-xs-2">
        RUT
    <br />
        <asp:TextBox ID="txt_buscarRUT" runat="server" AutoCompleteType="Search" CssClass="form-control" />
    </div>
    <div class="col-xs-2">
        Nombre
    <br />
        <asp:TextBox ID="txt_buscarNombre" runat="server" AutoCompleteType="Search" CssClass="form-control" />
    </div>
    <div class="col-xs-2">
        Apellido
    <br />
        <asp:TextBox ID="txt_buscarApellido" runat="server" AutoCompleteType="Search" CssClass="form-control" />
    </div>
    <div class="col-xs-2">
        username
    <br />
        <asp:TextBox ID="txt_buscarusername" runat="server" AutoCompleteType="Search" CssClass="form-control" />
    </div>
    <div class="col-xs-1">
        Solo Activos
    <br />
        <asp:CheckBox ID="chk_buscarActivos" runat="server" />
    </div>
    <div class="col-xs-1">
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:LinkButton ID="btn_buscar" OnClick="btn_buscarUsuario_Click" CssClass="btn btn-primary" ToolTip="Buscar Usuario" runat="server">
          <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_nuevo" CssClass="btn btn-primary" ToolTip="Nuevo Usuario" runat="server" OnClick="btn_agregarUsuario_Click">
          <span class="glyphicon glyphicon-plus" />
                </asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gv_listar" runat="server" AllowPaging="true" AllowSorting="True" Width="100%" PageSize="8"
                DataKeyNames="ID" CssClass="table table-bordered table-hover tablita" EmptyDataText="No se encontraron Usuarios!" AutoGenerateColumns="False"
                OnRowDataBound="gv_listar_RowDataBound" OnSorting="gv_listar_Sorting" OnPageIndexChanging="gv_listar_PageIndexChanging" OnRowCommand="gv_listar_RowCommand">
                <Columns>
                    <asp:TemplateField ShowHeader="false" HeaderStyle-Width="1%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_modificar" CausesValidation="False" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Eval("ID") %>' CommandName="EDITAR" ToolTip="Modificar" runat="server">
                <span class="glyphicon glyphicon-pencil" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderStyle-Width="1%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_activar" CausesValidation="False" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Eval("ID") %>' CommandName="ACTIVAR" ToolTip="Activar/Desactivar" runat="server">
                <span class="glyphicon glyphicon-off" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False" HeaderStyle-Width="1%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_asignar" CausesValidation="False" CssClass="btn btn-xs btn-primary" CommandArgument='<%# Eval("ID") %>' CommandName="ASIGNAR" ToolTip="Asignar Lugares" runat="server">
                <span class="glyphicon glyphicon-tags" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="Id" Visible="False" SortExpression="ID" />
                    <asp:BoundField DataField="RUT" HeaderText="Rut" SortExpression="RUT" />
                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre" SortExpression="NOMBRE" />
                    <asp:BoundField DataField="APELLIDO" HeaderText="Apellido" SortExpression="APELLIDO" />
                    <asp:BoundField DataField="USERNAME" HeaderText="Username" SortExpression="USERNAME" />
                    <asp:BoundField DataField="TIPO" HeaderText="Tipo" SortExpression="TIPO" />
                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" SortExpression="ESTADO" />
                    <asp:BoundField DataField="OBS" HeaderText="Observación" SortExpression="OBS" />
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <div id="modalAsignar" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 900px">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Asignar posición
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow-y: auto;">
                            <asp:Panel ID="pnl_asignar" CssClass="col-xs-12" runat="server">
                            </asp:Panel>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton OnClientClick="asignarLugares();" runat="server" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-tags" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Modal Usuario -->
    <div id="modalEdit" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Usuario
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow-y: auto;">
                            <div class="col-xs-3">
                                Nombre
                <br />
                                <asp:TextBox ID="txt_editNombre" runat="server" CssClass="form-control input-mayus" />
                            </div>
                            <div class="col-xs-3">
                                Apellido
                <br />
                                <asp:TextBox ID="txt_editApellido" runat="server" CssClass="form-control input-mayus" />
                            </div>
                            <div class="col-xs-2">
                                Rut
                <br />
                                <asp:TextBox ID="txt_editRut" OnTextChanged="txt_editRut_TextChanged" runat="server" CssClass="form-control" AutoPostBack="true" />
                            </div>
                            <div class="col-xs-4">
                                Email
                <br />
                                <asp:TextBox ID="txt_editEmail" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-4">
                                Empresa
                <br />
                                <asp:DropDownList ID="ddl_editEmpresa" runat="server" CssClass="drop form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4">
                                Perfil
                <br />
                                <asp:DropDownList ID="ddl_editTipoUsuario" OnSelectedIndexChanged="ddl_editTipoUsuario_IndexChanged" runat="server" CssClass="drop form-control" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4">
                                Estado
                <br />
                                <asp:DropDownList ID="ddl_editEstado" runat="server" CssClass="drop form-control">
                                    <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                    <asp:ListItem Value="1">Activo</asp:ListItem>
                                    <asp:ListItem Value="2">Inactivo</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-4">
                                Site
                <br />
                                <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1">
                                    <div class="list-panel">
                                        <telerik:RadListBox ID="rlcli" runat="server" CheckBoxes="true" ShowCheckAll="true"
                                            Height="100px" Visible="true">
                                        </telerik:RadListBox>
                                        <br />
                                    </div>
                                </telerik:RadAjaxPanel>
                            </div>
                            <div class="col-xs-4">
                                Observación
                <br />
                                <asp:TextBox ID="txt_editObservacion" runat="server" CssClass="textAreaUsuario" Rows="4"
                                    TextMode="MultiLine" Height="100%">
                                </asp:TextBox>
                            </div>
                            <div id="dv_proveedores" class="col-xs-4" runat="server" style="display: none">
                                Proveedor
                <br />
                                <asp:DropDownList ID="ddl_editProveedores" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-3">
                                Username
                <br />
                                <asp:TextBox ID="txt_editUsername" runat="server" CssClass="form-control input-minus" ValidationGroup="nuevoUsuario" />

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="nuevoUsuario"
                                    ControlToValidate="txt_editUsername" ErrorMessage="Solo se permiten minusculas y números."
                                    ValidationExpression="^(?=.*[a-z0-9])[a-z0-9]+$"></asp:RegularExpressionValidator>

                            </div>
                            <div class="col-xs-3">
                                Password
                <br />
                                <asp:TextBox ID="txt_editPassword" runat="server" MaxLength="10" CssClass="form-control" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="nuevoUsuario"
                                    ControlToValidate="txt_editPassword" ErrorMessage="No se permite ñ."
                                    ValidationExpression="^([^ñÑ]*)+$"></asp:RegularExpressionValidator>

                            </div>
                            <div class="col-xs-1">
                                <br />
                                <asp:LinkButton ID="btn_editGenPass" CssClass="btn btn-primary"
                                    ToolTip="Generar Password" runat="server" OnClick="btnGenerar_Click">
                  <span class="glyphicon glyphicon-certificate" />
                                </asp:LinkButton>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton ID="btn_grabarUsuario" runat="server" CssClass="btn btn-primary" OnClick="btn_grabarUsuario_Click"
                                    ValidationGroup="nuevoUsuario" CausesValidation="true">
                        <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_editNombre"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevoUsuario">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" PopupPosition="BottomLeft"
                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                        </asp:ValidatorCalloutExtender>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_editApellido"
                            Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoUsuario" SetFocusOnError="True">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender" PopupPosition="BottomLeft"
                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                        </asp:ValidatorCalloutExtender>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_editRut"
                            Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoUsuario" SetFocusOnError="True">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" PopupPosition="BottomLeft"
                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                        </asp:ValidatorCalloutExtender>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_editEmail"
                            Display="None" ErrorMessage=" El email ingresado no tiene formáto válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="nuevoUsuario" SetFocusOnError="true">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_editEmail"
                            Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoUsuario" SetFocusOnError="True">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" PopupPosition="BottomLeft"
                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                        </asp:ValidatorCalloutExtender>
                        <asp:ValidatorCalloutExtender ID="RegularExpressionValidator1_ValidatorCalloutExtender" PopupPosition="BottomLeft"
                            runat="server" Enabled="True" TargetControlID="RegularExpressionValidator1">
                        </asp:ValidatorCalloutExtender>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_editUsername"
                            Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoUsuario" SetFocusOnError="True">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" PopupPosition="BottomLeft"
                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                        </asp:ValidatorCalloutExtender>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_editPassword"
                            Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoUsuario" SetFocusOnError="True">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="RequiredFieldValidator4_ValidatorCalloutExtender"
                            PopupPosition="BottomLeft" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                        </asp:ValidatorCalloutExtender>

                        <asp:CompareValidator ID="CompareDropEmpresa" runat="server" ControlToValidate="ddl_editEmpresa"
                            Display="Dynamic" Text="<strong>  </strong>" ErrorMessage="* Requerido" SetFocusOnError="true"
                            Type="Integer" Operator="GreaterThan" InitialValue="Seleccione..." ValueToCompare="0"
                            ValidationGroup="nuevoUsuario">
                        </asp:CompareValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" PopupPosition="BottomLeft"
                            TargetControlID="CompareDropEmpresa">
                        </asp:ValidatorCalloutExtender>

                        <asp:CompareValidator ID="CompareDropTipo" runat="server" ControlToValidate="ddl_editTipoUsuario"
                            Display="Dynamic" Text="<strong>  </strong>" ErrorMessage="* Requerido" SetFocusOnError="true"
                            Type="Integer" Operator="GreaterThan" InitialValue="Seleccione..." ValueToCompare="0"
                            ValidationGroup="nuevoUsuario">
                        </asp:CompareValidator>
                        <asp:ValidatorCalloutExtender ID="vce1" runat="server" Enabled="True" TargetControlID="CompareDropTipo" PopupPosition="BottomLeft">
                        </asp:ValidatorCalloutExtender>

                        <asp:CompareValidator ID="CompareDropEstado" runat="server" ControlToValidate="ddl_editEstado"
                            Display="Dynamic" Text="<strong>  </strong>" ErrorMessage="* Requerido" SetFocusOnError="true"
                            Type="Integer" Operator="GreaterThan" InitialValue="Seleccione..." ValueToCompare="0"
                            ValidationGroup="nuevoUsuario">
                        </asp:CompareValidator>
                        <asp:ValidatorCalloutExtender ID="vce3" runat="server" Enabled="True" TargetControlID="CompareDropEstado" PopupPosition="BottomLeft">
                        </asp:ValidatorCalloutExtender>

                        <asp:CompareValidator ID="CompareDropProveedor" runat="server" ControlToValidate="ddl_editProveedores" Enabled="false"
                            Display="Dynamic" Text="<strong>  </strong>" ErrorMessage="* Requerido" SetFocusOnError="true"
                            Type="Integer" Operator="GreaterThan" InitialValue="Seleccione..." ValueToCompare="0"
                            ValidationGroup="nuevoUsuario">
                        </asp:CompareValidator>
                        <asp:ValidatorCalloutExtender ID="vce2" runat="server" Enabled="True" TargetControlID="CompareDropProveedor" PopupPosition="BottomLeft">
                        </asp:ValidatorCalloutExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Modal Eliminar -->
    <div class="modal fade" id="modalConf" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>
                                <asp:Label ID="lblRazonEliminacion" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="msjEliminacion" runat="server"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_Activar" OnClick="btn_Activar_click" CssClass="btn btn-primary" runat="server">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_Desactivar" OnClick="btn_Desactivar_click" CssClass="btn btn-primary" runat="server">
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
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idUsuario" runat="server" />
            <asp:HiddenField ID="hf_idLugares" runat="server" />
            <asp:Button ID="btn_Asignar" OnClick="btn_asignarLugares_Click" runat="server" Text="Button" CssClass="ocultar" />
            <asp:Button ID="Button5" runat="server" Text="Button" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">

        function modalAsignar() {
            $("#modalAsignarLugar").modal();
        }

        function modalUsuario() {
            $("#modalUsuario").modal();
        }

        function modalConfirmacion() {
            $("#modalEliminar").modal();
        }

        function asignarLugares() {
            var cadena = '';
            var primero = true;
            var lugaresSeleccionados = $(".droplugar");
            for (x = 0; x < lugaresSeleccionados.length; x++) {
                if (lugaresSeleccionados[x].value != 0 && lugaresSeleccionados[x].value != null) {
                    if (primero == true) {
                        primero = false;
                    }
                    else {
                        cadena += ",";
                    }
                    cadena += lugaresSeleccionados[x].value;
                }
            }
            document.getElementById('<%= hf_idLugares.ClientID %>').value = cadena;
            var clickButton = document.getElementById("<%= btn_Asignar.ClientID %>");
            clickButton.click();
        }

        function ValidateListaSite(source, args) {
            var chkListModules = document.getElementById('<%= rlcli.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }

        function activarUsuario() {
            var clickButton = document.getElementById("<%= btn_Activar.ClientID %>");
            clickButton.click();
            //            $("#modalEliminar").modal("dismiss");
        }

        function desactivarUsuario() {
            var clickButton = document.getElementById("<%= btn_Desactivar.ClientID %>");
            clickButton.click();
        }

    </script>
</asp:Content>
