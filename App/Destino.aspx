<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Destino.aspx.cs" Inherits="App_Destino" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador"></div>
    <h2>Maestro Destino
    </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador"></div>
    <div class="col-xs-2">
        Nombre
    <br />
        <asp:TextBox ID="txt_buscarNombre" CssClass="form-control" runat="server" />
    </div>
    <div class="col-xs-1">
        <br />
        <asp:LinkButton ID="btn_buscar" runat="server" data-dismiss="modal" OnClick="btn_buscar_Click" CssClass="btn btn-primary">
      <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_nuevo" runat="server" data-dismiss="modal" OnClick="btn_nuevo_Click" CssClass="btn btn-primary">
      <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-8 col-xs-push-2">
                <asp:GridView ID="gv_listar" EmptyDataText="¡No se han encontrado destinos!" Width="100%" EnableViewState="false"
                    OnRowCommand="gv_listar_RowCommand" OnPageIndexChanging="gv_listar_PageIndexChanging"
                    AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" CssClass="table table-bordered table-hover tablita" runat="server">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificar" runat="server" CommandName="EDITAR" CssClass="btn btn-xs btn-primary" ToolTip="Modificar" CommandArgument='<%# Eval("ID") %>'>
                  <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" runat="server" CommandName="ELIMINAR" CssClass="btn btn-xs btn-primary" ToolTip="Eliminar" CommandArgument='<%# Eval("ID") %>'>
                  <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" Visible="false" />
                        <asp:BoundField DataField="CODIGO" HeaderText="Código" />
                        <asp:BoundField DataField="NOMBRE" HeaderText="Destino" />
                        <asp:BoundField DataField="TIPO" HeaderText="Tipo" />
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalDestino" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Datos Destino
                            </h4>
                        </div>
                        <div class="modal-body" style="overflow: auto; height: auto">
                            <div class="col-xs-3">
                                Código
                <br />
                                <asp:TextBox ID="txt_editCodigo" CssClass="form-control input-mayus" runat="server" />
                            </div>
                            <div class="col-xs-3">
                                Nombre
                <br />
                                <asp:TextBox ID="txt_editNombre" CssClass="form-control input-mayus" runat="server" />
                            </div>
                            <div class="col-xs-3">
                                Tipo
                <br />
                                <asp:DropDownList ID="ddl_editTipo" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 separador"></div>
                            <div class="col-xs-12" style="text-align: center">
                                <asp:LinkButton ID="btn_guardar" runat="server" OnClick="btn_guardar_Click" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                    <asp:RequiredFieldValidator ID="rfv_codigo" runat="server" ControlToValidate="txt_editCodigo"
                        Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="vce_codigo" PopupPosition="BottomLeft"
                        runat="server" Enabled="True" TargetControlID="rfv_codigo">
                    </asp:ValidatorCalloutExtender>
                    <asp:RequiredFieldValidator ID="rfv_nombre" runat="server" ControlToValidate="txt_editNombre"
                        Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="vce_nombre" PopupPosition="BottomLeft"
                        runat="server" Enabled="True" TargetControlID="rfv_nombre">
                    </asp:ValidatorCalloutExtender>
                    <asp:RequiredFieldValidator ID="rfv_tipo" runat="server" ControlToValidate="ddl_editTipo" InitialValue="0"
                        Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>
                    <asp:ValidatorCalloutExtender ID="vce_tipo" PopupPosition="BottomLeft"
                        runat="server" Enabled="True" TargetControlID="rfv_tipo">
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
                            <h4>Eliminar Destino
                            </h4>
                        </div>
                        <div class="modal-body">
                            Se eliminará el registro de la base de datos, ¿desea continuar?
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_Eliminar" runat="server" OnClick="btn_Eliminar_Click"
                                CssClass="btn btn-primary">
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
            <asp:HiddenField ID="hf_idDestino" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function modalDestino() {
            $("#modalDestino").modal();
        }
        function modalConfirmar() {
            $("#modalConfirmar").modal();
        }
    </script>
</asp:Content>
