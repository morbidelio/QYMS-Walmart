<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
    CodeFile="Trailer.aspx.cs" Inherits="App_Trailer" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador">
    </div>
    <h2>Maestro Trailer
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-12 separador">
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-2">
                Tipo Transporte
    <br />
                <asp:DropDownList ID="ddl_buscarTipo" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-lg-2">
                Transportista
    <br />
                <asp:DropDownList ID="ddl_buscarTransportista" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-lg-1">
                N° Flota
    <br />
                <asp:TextBox ID="txt_buscarNro" runat="server" CssClass="form-control" />
            </div>
            <div class="col-lg-2">
                Placa
    <br />
                <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" />
            </div>
            <div class="col-lg-1">
                Solo internos
    <br />
                <asp:CheckBox ID="chk_buscarInterno" runat="server" />
            </div>
            <div class="col-lg-2">
                <br />
                <asp:LinkButton ID="btn_buscarTrailer" OnClick="btn_buscar_Click" CssClass="btn btn-primary" ToolTip="Buscar Trailer" runat="server">
      <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_nuevoTrailer" OnClick="btn_nuevo_Click" CssClass="btn btn-primary" ToolTip="Nuevo Trailer" runat="server">
      <span class="glyphicon glyphicon-plus" />
                </asp:LinkButton>
                <asp:LinkButton ID="btn_export" ToolTip="Exportar a Excel" CssClass="btn btn-primary" CausesValidation="false" OnClick="btn_export_Click" runat="server">
          <span class="glyphicon glyphicon-export" />
                </asp:LinkButton>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_export" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12" style="text-align: center">
                <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" Width="100%"
                    OnPageIndexChanging="gv_listar_PageIndexChanging" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting" OnRowDataBound="gv_listar_RowDataBound"
                    PageSize="9" EmptyDataText="No se encontraron trailers!" CssClass="table table-bordered table-hover tablita">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificarTrailer" runat="server" CommandName="MODIFICAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminarTrailer" runat="server" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Eliminar">
                    <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_BloquearTrailer" runat="server" CommandName="BLOQUEAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Bloquear">
                    <span class="glyphicon glyphicon-lock" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Id" DataField="ID" SortExpression="ID" Visible="false" />
                        <asp:BoundField HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO" ItemStyle-Width="10%" />
                        <asp:BoundField HeaderText="Placa" DataField="PLACA" SortExpression="PLACA" ItemStyle-Width="20%" HeaderStyle-Width="20%" />
                        <asp:BoundField HeaderText="Externo" DataField="EXTERNO" SortExpression="EXTERNO" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="Número" DataField="NUMERO" SortExpression="NUMERO" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                        <asp:BoundField HeaderText="Transportista" DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" ItemStyle-Width="30%" HeaderStyle-Width="30%" />
                        <asp:BoundField HeaderText="Tipo" DataField="TIPO_TRAILER" SortExpression="TIPO_TRAILER" ItemStyle-Width="20%" HeaderStyle-Width="20%" />
                        <asp:BoundField HeaderText="Bloqueado" DataField="BLOQUEADO" SortExpression="BLOQUEADO" ItemStyle-Width="20%" HeaderStyle-Width="20%" />
                        <asp:BoundField HeaderText="Requiere Sello" DataField="REQ_SELLO" SortExpression="REQ_SELLO" ItemStyle-Width="20%" HeaderStyle-Width="20%" />
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <!-- modal Trailer  ---->
    <div class="modal fade" id="modalEdit" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 900px;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Trailer
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">
                            <div class="col-lg-2">
                                Placa
                <br />
                                <asp:TextBox ID="txt_editPlaca" OnTextChanged="txt_editPlaca_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-1">
                                Externo
                <br />
                                <asp:CheckBox ID="chk_editExterno" runat="server" OnCheckedChanged="cambia_interno"
                                    Checked="false" AutoPostBack="true" />
                            </div>
                            <div class="col-lg-2">
                                N° Flota
                <br />
                                <asp:TextBox ID="txt_editNumero" OnTextChanged="txt_editNumero_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-lg-5">
                                Transportista
                <br />
                                <asp:DropDownList ID="ddl_editTran" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-12 separador">
                            </div>
                            <div class="col-lg-3">
                                Tipo
                <br />
                                <asp:DropDownList ID="ddl_editTipo" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                ID Shortec
                <br />
                                <asp:DropDownList ID="ddl_editShorteck" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Text="Seleccione..." />
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                Puerta Abatible
                <br />
                                <asp:CheckBox ID="chk_editSello" runat="server" />
                            </div>
                            <div class="col-lg-12 separador">
                            </div>
                            <asp:Literal ID="ltl_contenidoCaract" runat="server"></asp:Literal>
                            <div class="col-lg-12 separador">
                            </div>
                            <div class="col-lg-12" style="text-align: center;">
                                <asp:LinkButton ID="btn_editGrabar" runat="server" OnClientClick="agregarCaract();"
                                    OnClick="btn_editGrabar_Click" ValidationGroup="nuevoTrailer" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
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
    <!-- Modal Eliminación -->
    <div class="modal fade" id="modalConfirmar" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>
                                <asp:Label ID="lblRazonEliminacion" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body" style="margin-left: 20px; height: 150px">
                            <asp:Label ID="msjEliminacion" runat="server"></asp:Label>
                            <div class="col-lg-4" style="margin-left: 70px">
                                <br />
                                <asp:DropDownList ID="ddTipoBloqueo" CssClass="form-control" Width="200px" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-4 ocultar" style="margin-left: 40px">
                                Site
                <br />
                                <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddl_buscarSite_onChange" runat="server" Width="200px">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-4 ocultar" style="margin-left: 70px">
                                Zona
                <br />
                                <asp:DropDownList ID="ddl_zona" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_zona_onChange"
                                    runat="server" Width="200px">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-4 ocultar" style="margin-left: 40px">
                                Playa
                <br />
                                <asp:DropDownList ID="ddl_playa" CssClass="form-control" runat="server" Width="200px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddl_playa_onChange">
                                </asp:DropDownList>
                            </div>
                            <div class="col-lg-4 ocultar" style="margin-left: 70px">
                                lugar
                <br />
                                <asp:DropDownList ID="ddl_lugar" CssClass="form-control" runat="server" Width="200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnModalEliminar" runat="server" CssClass="btn btn-primary" OnClick="btn_EliminarTrailer_Click">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_BloquearTrailer" runat="server" CssClass="btn btn-primary" OnClick="btn_BloquearTrailer_Click">
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
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idTrailer" runat="server" />
            <asp:HiddenField ID="hf_excluyentes" Value="" runat="server" />
            <asp:HiddenField ID="hf_noexcluyentes" Value="" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function modalEdit() {
            $("#modalEdit").modal();
        }

        function modalConfirmar() {
            $("#modalConfirmar").modal();
        }

        function agregarCaract() {
            var checks = document.getElementsByName("check");
            var drops = document.getElementsByName("drop");
            var x = [];
            var z = [];
            for (var i = 0; i < checks.length; i++) {
                if (checks[i].checked) {
                    x.push(checks[i].value);
                }
            }
            for (var i = 0; i < drops.length; i++) {
                z.push(drops[i].value);
            }
            var xx = x.join();
            var zz = z.join();
            $("#<% =this.hf_excluyentes.ClientID %>").val(xx);
            $("#<% =this.hf_noexcluyentes.ClientID %>").val(zz);
        }

        function llenarForm() {
            limpiarForm();
            var ex = $("#<% =this.hf_excluyentes.ClientID %>").val();
            var no_ex = $("#<% =this.hf_noexcluyentes.ClientID %>").val();
            var x = ex.split(',');
            var y = no_ex.split(',');
            for (var i = 0; i < x.length; i++) {
                $("#caractTipo_" + x[i]).prop('checked', true);
            }
            for (var i = 0; i < y.length; i++) {
                $("#op_drop_" + y[i]).attr('selected', true);
            }
        }

        function limpiarForm() {
            var checks = document.getElementsByName("check");
            var drops = document.getElementsByName("drop");
            for (var i = 0; i < checks.length; i++) {
                checks[i].checked = false;
            }
            for (var i = 0; i < drops.length; i++) {
                drops[i].options.item(0).selected = true;
            }
        }
    </script>
</asp:Content>
