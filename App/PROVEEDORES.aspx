<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
    CodeFile="proveedores.aspx.cs" Inherits="App_proveedores" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador">
    </div>
    <h2>Maestro Proveedores
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-3">
        Descripción
    <br />
        <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" />
    </div>
    <div class="col-lg-2">
        Código
    <br />
        <asp:TextBox ID="txt_buscarCodigo" runat="server" CssClass="form-control input-mayus" />
    </div>
    <div class="col-lg-2">
        <br />
        <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" ToolTip="Buscar Proveedor" runat="server">
      <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_nueva" OnClick="btn_nuevo_Click" CssClass="btn btn-primary" ToolTip="Nueva Playa" runat="server">
      <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12">
                <asp:GridView ID="gv_listar" runat="server" AllowSorting="True" AllowPaging="true" PageSize="9" DataKeyNames="ID" UseAccessibleHeader="true"
                    OnPageIndexChanging="gv_listar_PageIndexChanging"  OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting"
                    EmptyDataText="No se encontraron Clientes!" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-hover tablita">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificar" runat="server" CommandName="MODIFICAR"
                                    CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" runat="server" CommandName="ELIMINAR"
                                    CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_vendor" runat="server" CommandName="VENDOR"
                                    CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Editar Vendor">
                  <span class="glyphicon glyphicon-th-list" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="ID" DataField="ID" SortExpression="ID" Visible="false" />
                        <asp:BoundField HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION" />
                        <asp:BoundField HeaderText="Direccion" DataField="DIRECCION" SortExpression="DIRECCION" />
                        <asp:BoundField HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO" />
                        <asp:BoundField HeaderText="Rut" DataField="RUT" SortExpression="RUT" />
                        <asp:BoundField HeaderText="N° Vendor Asociados" DataField="NUM_VENDOR" SortExpression="NUM_VENDOR" />
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Proveedor
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-lg-3">
                                Código
                <br />
                                <asp:TextBox ID="txt_editCodigo" CssClass="form-control input-mayus" OnTextChanged="txt_editCodigo_TextChanged" AutoPostBack="true" runat="server" />
                            </div>
                            <div class="col-lg-3">
                                Rut
                <br />
                                <asp:TextBox ID="txt_editRut" CssClass="form-control input-word" OnTextChanged="txt_editRut_TextChanged" AutoPostBack="true" runat="server" />
                            </div>
                            <div class="col-lg-6">
                                Descripción
                <br />
                                <asp:TextBox ID="txt_editDesc" CssClass="form-control input-mayus" runat="server" />
                            </div>
                            <div class="col-lg-12 separador">
                            </div>
                            <div class="col-lg-12">
                                Dirección
                <br />
                                <asp:TextBox ID="txt_editDirec" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-12 separador">
                            </div>
                            <div class="col-lg-12" style="text-align: center">
                                <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
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
                            <h4>Eliminar Proveedor
                            </h4>
                        </div>
                        <div class="modal-body">
                            Se eliminará el proveedor seleccionado, ¿desea continuar?
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnModalEliminar" runat="server" OnClick="btn_Eliminar_Click" CssClass="btn btn-primary">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <button type="button" class="btn btn-primary" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="modalVendor" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Proveedor
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-lg-7">
                                Número
                <br />
                                <asp:TextBox ID="txt_vendorNro" CssClass="form-control input-number" OnTextChanged="txt_editCodigo_TextChanged" AutoPostBack="true" runat="server" />
                            </div>
                            <div class="col-lg-5">
                                <br />
                                <asp:LinkButton id="btn_vendorBuscar" OnClick="btn_vendorBuscar_Click" CssClass="btn btn-primary" runat="server">
                                    <span class="glyphicon glyphicon-search" />
                                </asp:LinkButton>
                                <asp:LinkButton id="btn_vendorAgregar" OnClick="btn_vendorAgregar_Click" CssClass="btn btn-primary" runat="server">
                                    <span class="glyphicon glyphicon-plus" />
                                </asp:LinkButton>
                            </div>
                            <div class="col-lg-12 separador">
                            </div>
                            <div class="col-lg-12">
                                <asp:GridView ID="gv_vendor" CssClass="table table-hover table-bordered tablita" AutoGenerateColumns="false"
                                    OnRowCommand="gv_vendor_RowCommand" OnRowDataBound="gv_vendor_RowDataBound" runat="server">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="5px">
                                            <HeaderTemplate><span class="glyphicon glyphicon-remove" /></HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton id="btn_eliminarVendor" CommandArgument='<%# Eval("PRVE_ID") %>' CommandName="ELIMINAR" CssClass="btn btn-xs btn-primary" runat="server"><span class="glyphicon glyphicon-remove" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PRVE_NUMERO" HeaderText="Número" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_id" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>
