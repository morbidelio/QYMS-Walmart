<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Empresa.aspx.cs" Inherits="App_Empresa" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <h2>Maestro Empresa
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-2">
        Rut
      <br />
        <asp:TextBox ID="txt_buscarRut" runat="server" CssClass="form-control" />
    </div>
    <div class="col-xs-2">
        Razón Social
      <br />
        <asp:TextBox ID="txt_buscarRSocial" runat="server" CssClass="form-control" />
    </div>
    <div class="col-xs-2">
        Nombre Fantasía
      <br />
        <asp:TextBox ID="txt_buscarNFantasia" runat="server" CssClass="form-control" />
    </div>
    <div class="col-xs-2">
        <br />
        <asp:LinkButton ID="btn_buscarEmpresa" OnClick="btn_buscarEmpresa_Click" CssClass="btn btn-primary" ToolTip="Buscar Empresa" runat="server">
        <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_nuevaEmpresa" CssClass="btn btn-primary" ToolTip="Nueva Empresa" runat="server" OnClick="btn_nuevaEmpresa_Click">
        <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-12">
                <asp:GridView ID="gv_listar" runat="server" AllowSorting="True" EmptyDataText="No se encontraron Clientes!" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-hover tablita"
                    OnRowCreated="gv_listar_RowCreated" OnRowCommand="gv_listaEmpresas_RowCommand" OnSorting="gv_listaEmpresas_Sorting">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificar" runat="server" CommandName="EDITAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-primary" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" runat="server" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-primary" ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Id" DataField="ID" SortExpression="ID" Visible="false" />
                        <asp:BoundField HeaderText="Rut" DataField="RUT" SortExpression="RUT" ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="Razón Social" DataField="RAZON_SOCIAL" SortExpression="RAZON_SOCIAL" />
                        <asp:BoundField HeaderText="Giro" DataField="GIRO" SortExpression="GIRO" />
                        <asp:BoundField HeaderText="Nombre Fantasia" DataField="NOMBRE_FANTASIA" SortExpression="NOMBRE_FANTASIA" />
                        <asp:BoundField HeaderText="Dirección" DataField="DIRECCION" SortExpression="DIRECCION" />
                        <asp:BoundField HeaderText="Comuna" DataField="COMUNA" SortExpression="COMUNA" />
                        <asp:BoundField HeaderText="Telefono" DataField="TELEFONO" SortExpression="TELEFONO" />
                        <asp:BoundField HeaderText="Email" DataField="EMAIL" SortExpression="EMAIL" />
                        <asp:TemplateField HeaderText="Detalle" ShowHeader="False" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_detalle" CssClass="btn btn-xs btn-primary" CommandName="VER" runat="server">
                      <span class="glyphicon glyphicon-list-alt" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <!-- modal Empresa  ---->
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 1200px;">
            <div class="modal-content">
                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Empresa
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-xs-12">
                                <div class="col-xs-2">
                                    Rut
                  <br />
                                    <asp:TextBox ID="txt_editRut" runat="server" CssClass="form-control" OnTextChanged="txt_editRut_TextChanged" AutoPostBack="True">
                                    </asp:TextBox>
                                </div>
                                <div class="col-xs-2">
                                    Código
                  <br />
                                    <asp:TextBox ID="txt_editCodigo" runat="server" CssClass="form-control" OnTextChanged="txt_editRut_TextChanged" AutoPostBack="True">
                                    </asp:TextBox>
                                </div>
                                <div class="col-xs-4">
                                    Razón Social
                  <br />
                                    <asp:TextBox ID="txt_editRsocial" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-xs-4">
                                    Giro
                  <br />
                                    <asp:TextBox ID="txt_editGiro" Width="200px" runat="server" CssClass="form-control" />
                                </div>
                                <asp:Panel ID="pnl_editBodega" Visible="false" runat="server">
                                    <div class="col-xs-2">
                                        Bodega
                    <br />
                                        <asp:TextBox ID="txt_editBodega" runat="server" />
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                                <div class="col-xs-3">
                                    Nombre de fantasía
                  <br />
                                    <asp:TextBox ID="txt_editNombreFantasia" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-xs-2">
                                    Teléfono
                  <br />
                                    <asp:TextBox ID="txt_editTelefono" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-xs-4">
                                    Nombre Contacto
                  <br />
                                    <asp:TextBox ID="txt_editContacto" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-xs-3">
                                    Email
                  <br />
                                    <asp:TextBox ID="txt_editEmail" runat="server" AutoCompleteType="Email" CssClass="form-control" />
                                </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12">
                                <div class="col-xs-3">
                                    Región
                  <br />
                                    <asp:DropDownList ID="ddl_editRegion" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddl_editRegionIndexChanged" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-3">
                                    Comuna
                  <br />
                                    <asp:DropDownList ID="ddl_editComuna" Enabled="false" CssClass="form-control" runat="server">
                                        <asp:ListItem Selected="True" Text="Seleccione..." Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-6">
                                    Dirección
                  <br />
                                    <asp:TextBox ID="txt_editDireccion" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabarNuevo_Click" ValidationGroup="nuevoCliente" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_editRut"
                            Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" PopupPosition="BottomLeft"
                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                        </asp:ValidatorCalloutExtender>
                        <asp:CustomValidator ID="customRut" ClientValidationFunction="validarRut" runat="server"
                            ControlToValidate="txt_editRut" ValidationGroup="nuevoCliente">
                        </asp:CustomValidator>

                        <asp:RequiredFieldValidator ID="rfv_codigo" runat="server" ControlToValidate="txt_editCodigo"
                            Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" PopupPosition="BottomLeft"
                            runat="server" Enabled="True" TargetControlID="rfv_codigo">
                        </asp:ValidatorCalloutExtender>
                        <asp:CustomValidator ID="CustomValidator1" ClientValidationFunction="validarRut" runat="server"
                            ControlToValidate="txt_editCodigo" ValidationGroup="nuevoCliente">
                        </asp:CustomValidator>

                        <asp:FilteredTextBoxExtender ID="filterRSocial" runat="server" TargetControlID="txt_editRsocial"
                            Enabled="True" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ ,.()[]">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rfv_RSocial" runat="server" ControlToValidate="txt_editRsocial"
                            Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" PopupPosition="BottomLeft"
                            TargetControlID="rfv_RSocial">
                        </asp:ValidatorCalloutExtender>

                        <asp:FilteredTextBoxExtender ID="FilteredGiro" runat="server" TargetControlID="txt_editGiro"
                            Enabled="True" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ ,.()[]">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rfv_giro" runat="server" ControlToValidate="txt_editGiro"
                            Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" PopupPosition="BottomLeft"
                            TargetControlID="rfv_giro">
                        </asp:ValidatorCalloutExtender>

                        <asp:FilteredTextBoxExtender ID="filteredNombreFantasia" runat="server" TargetControlID="txt_editNombreFantasia"
                            Enabled="True" ValidChars="ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyzüÜáéíóúÁÉÍÓ ,.()[]">
                        </asp:FilteredTextBoxExtender>
                        <asp:RequiredFieldValidator ID="rfv_nombreFantasia" runat="server" ControlToValidate="txt_editNombreFantasia"
                            Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" PopupPosition="BottomLeft"
                            TargetControlID="rfv_nombreFantasia">
                        </asp:ValidatorCalloutExtender>

                        <asp:RequiredFieldValidator ID="rfv_comuna" runat="server" ControlToValidate="ddl_editComuna" InitialValue="0"
                            Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" PopupPosition="BottomLeft"
                            TargetControlID="rfv_comuna">
                        </asp:ValidatorCalloutExtender>

                        <asp:RequiredFieldValidator ID="rfv_direccion" runat="server" ControlToValidate="txt_editDireccion"
                            Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" PopupPosition="BottomLeft"
                            TargetControlID="rfv_direccion">
                        </asp:ValidatorCalloutExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- modal eliminación -->
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
                            <asp:LinkButton ID="btnModalEliminar" runat="server" CssClass="btn btn-primary">
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
            <asp:HiddenField ID="hf_idEmpresa" runat="server" />
            <asp:Button ID="btn_eliminarEmpresa" OnClick="btn_eliminarEmpresa_click" runat="server" Text="Button" />
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
            $("#<%= btn_editGrabar.ClientID %>").click(function () {
                if ($("#<%=this.txt_editCodigo.ClientID %>").val() == '') {
                    showAlertClass('crear', 'warn_codigoVacio');
                    return false;
                }
            });
            tabla();
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
        function validarRut(sender, e) {
            var sw = true;
            if ($get("<%=this.txt_editRut.ClientID %>").value != "") {
                sw = validaRut($get("<%=this.txt_editRut.ClientID %>").value);
            }
            if (sw) {
                formatearRut($get("<%=this.txt_editRut.ClientID %>"));
            }
            e.IsValid = sw;
        }

        function eliminarEmpresa() {
            var clickButton = document.getElementById("<%= this.btn_eliminarEmpresa.ClientID %>");
            clickButton.click();
        }
    </script>
</asp:Content>
