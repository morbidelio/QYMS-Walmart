<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Conductor_Serv_Ext.aspx.cs" Inherits="App_Conductor_Serv_Ext" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador"></div>
    <h2>Maestro Conductor Externo</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-12 separador"></div>
    <div class="col-xs-2">
        Nombre
    <br />
        <asp:TextBox ID="txt_buscarNombre" CssClass="form-control" runat="server" />
    </div>
    <div class="col-xs-1">
        RUT
    <br />
        <asp:TextBox ID="txt_buscarRut" CssClass="form-control" runat="server" />
    </div>
    <div class="col-xs-1">
        <br />
        <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary"
            ToolTip="Buscar" runat="server">
      <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_nuevo" class="btn btn-primary" ToolTip="Nuevo" runat="server" OnClick="btn_nuevo_Click">
      <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:GridView ID="gv_listar" AllowSorting="true" AllowPaging="true" EmptyDataText="¡No existen conductores!" Width="100%" EnableViewState="false"
                OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting" OnPageIndexChanging="gv_listar_PageIndexChanging"
                CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" PageSize="8" runat="server">
                <Columns>
                    <asp:TemplateField HeaderStyle-Width="1%" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_edit" runat="server" CommandName="EDITAR" CommandArgument='<%# Eval("COSE_ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Modificar">
                <span class="glyphicon glyphicon-pencil" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="1%" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btn_elim" runat="server" CommandName="ELIM" CommandArgument='<%# Eval("COSE_ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Eliminar">
                <span class="glyphicon glyphicon-remove" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="COND_ID" Visible="false" />
                    <asp:BoundField DataField="NOMBRE" SortExpression="NOMBRE" HeaderText="Nombre" />
                    <asp:BoundField DataField="RUT" SortExpression="RUT" HeaderText="Rut" />
                    <asp:BoundField DataField="ACTIVO" SortExpression="ACTIVO" HeaderText="Activo" />
                    <asp:BoundField DataField="BLOQUEADO" SortExpression="BLOQUEADO" HeaderText="Bloqueado" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Image ID="img_foto" Height="50px" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pagination-ys" />
            </asp:GridView>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Conductor
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">

                            <div class="col-xs-3">
                                Rut
                <br />
                                <asp:TextBox ID="txt_editRut" OnTextChanged="txt_editRut_TextChanged" AutoPostBack="true"
                                    CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-xs-3">
                                Nombre
                <br />
                                <asp:TextBox ID="txt_editNombre" CssClass="form-control input-mayus" runat="server" />
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_grabar_Click" ValidationGroup="edit" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                        <asp:RequiredFieldValidator ID="rfv_txtRut" runat="server" ControlToValidate="txt_editRut"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                            ValidationGroup="edit">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                            Enabled="True" TargetControlID="rfv_txtRut">
                        </asp:ValidatorCalloutExtender>

                        <asp:RequiredFieldValidator ID="rfv_txtNombre" runat="server" ControlToValidate="txt_editNombre"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                            ValidationGroup="edit">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                            Enabled="True" TargetControlID="rfv_txtNombre">
                        </asp:ValidatorCalloutExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalConf" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>
                                <asp:Label ID="lbl_tituloConfirmar" runat="server" />
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lbl_mensajeConfirmar" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_Conf" runat="server" CssClass="btn btn-primary" OnClick="btn_Conf_Click">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <button class="btn btn-primary" data-dismiss="modal">
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_id" runat="server" />
            <asp:HiddenField ID="hf_confirmar" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function EndRequestHandler1(sender, args) {
            $("#<%= btn_editGrabar.ClientID %>").click(function () {
                if ($('#<%= txt_editRut.ClientID %>').val() == '') {
                    showAlertClass('crear', 'warn_rutVacio');
                    return false;

                }
                if ($('#<%= txt_editNombre.ClientID %>').val() == '') {
                    showAlertClass('crear', 'warn_nombreVacio');
                    return false;

                }
            });
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);
    </script>
</asp:Content>

