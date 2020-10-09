<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
CodeFile="Trailer_inventario.aspx.cs" Inherits="App_Trailerinventario" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Maestro Trailer
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-xs-1">
                Site
                <br />
                <asp:DropDownList ID="ddl_buscarSite" OnSelectedIndexChanged="ddl_buscarSite_IndexChanged"
                    AutoPostBack="true" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                Zona
                <br />
                <asp:DropDownList ID="ddl_buscarZona" OnSelectedIndexChanged="ddl_buscarZona_IndexChanged"
                    AutoPostBack="true" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-xs-1">
                Playa
                <br />
                <asp:DropDownList ID="ddl_buscarPlaya" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="col-xs-2">
        Tipo Transporte
        <br />
        <asp:DropDownList ID="ddl_buscarTipo" CssClass="form-control" runat="server">
        </asp:DropDownList>
    </div>
    <div class="col-xs-2">
        Transportista
        <br />
        <asp:DropDownList ID="ddl_buscarTransportista" CssClass="form-control" runat="server">
        </asp:DropDownList>
    </div>
    <div class="col-xs-1">
        N° Flota
        <br />
        <asp:TextBox ID="txt_buscarNro" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
    </div>
    <div class="col-xs-1">
        Placa
        <br />
        <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
    </div>
    <div class="col-xs-1">
        Solo internos
        <br />
        <asp:CheckBox ID="chk_buscarInterno" runat="server" />
    </div>
    <div class="col-xs-1">
        <br />
        <asp:LinkButton ID="btn_buscarTrailer" OnClick="btn_buscarTrailer_Click" CssClass="btn btn-primary"
            ToolTip="Buscar Trailer" runat="server">
      <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_nuevoTrailer" CssClass="btn btn-primary" ToolTip="Nuevo Trailer"
            runat="server" OnClick="btn_nuevoTrailer_Click">
      <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
            <div class="col-xs-12">
                <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" DataKeyNames="ID" PageSize="7"
                    OnPageIndexChanging="gv_listaTrailer_PageIndexChanging1" OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting"
                    EmptyDataText="No se encontraron trailers!" AutoGenerateColumns="False" Width="100%"
                    CssClass="table table-bordered table-hover tablita" OnRowDataBound="gv_listar_RowDataBound">
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificarTrailer" runat="server" CommandName="MODIFICAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminarTrailer" runat="server" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_BloquearTrailer" runat="server" CommandName="BLOQUEAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Bloquear">
                  <span class="glyphicon glyphicon-lock" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_inventario" runat="server" CommandName="inventario" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="inventario">
                  <span class="glyphicon glyphicon-lock" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Id" DataField="ID" SortExpression="ID" Visible="false" />
                        <asp:BoundField HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO" />
                        <asp:BoundField HeaderText="Placa" DataField="PLACA" SortExpression="PLACA" />
                        <asp:BoundField HeaderText="Externo" DataField="EXTERNO" SortExpression="EXTERNO" />
                        <asp:BoundField HeaderText="Número" DataField="NUMERO" SortExpression="NUMERO" />
                        <asp:BoundField HeaderText="Transportista" DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" />
                        <asp:BoundField HeaderText="Tipo" DataField="TIPO_TRAILER" SortExpression="TIPO_TRAILER" />
                        <asp:BoundField HeaderText="estado" DataField="estado_trailer" SortExpression="estado_trailer" />
                        <asp:BoundField HeaderText="Bloqueado" DataField="BLOQUEADO" SortExpression="BLOQUEADO" />
                        <asp:BoundField HeaderText="Requiere Sello" DataField="REQ_SELLO" SortExpression="REQ_SELLO" />
                        <asp:BoundField HeaderText="SITE" DataField="SITE" SortExpression="SITE" Visible="false" />
                        <asp:BoundField HeaderText="PLAYA" DataField="PLAYA" SortExpression="PLAYA" />
                        <asp:BoundField HeaderText="POSICION" DataField="POSICION" SortExpression="POSICION" />
                    </Columns>
                    <PagerTemplate>
                        Página
                        <asp:DropDownList ID="paginasDropDownList" Font-Size="12px" AutoPostBack="true" runat="server"
                            OnSelectedIndexChanged="GoPage">
                        </asp:DropDownList>
                        de
                        <asp:Label ID="lblTotalNumberOfPages" runat="server" />
                        <asp:Button ID="Button4" runat="server" CommandName="Page" ToolTip="Prim. Pag" CommandArgument="First"
                            CssClass="pagfirst" />
                        <asp:Button ID="Button1" runat="server" CommandName="Page" ToolTip="Pág. anterior"
                            CommandArgument="Prev" CssClass="pagprev" />
                        <asp:Button ID="Button2" runat="server" CommandName="Page" ToolTip="Sig. página"
                            CommandArgument="Next" CssClass="pagnext" />
                        <asp:Button ID="Button3" runat="server" CommandName="Page" ToolTip="Últ. Pag" CommandArgument="Last"
                            CssClass="paglast" />
                    </PagerTemplate>
                    <PagerStyle ForeColor="#BBB" HorizontalAlign="Right" BackColor="White" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
  <!-- modal Trailer  ---->
  <div class="modal fade" id="modalTrailer" data-backdrop="static" role="dialog">
    <div class="modal-dialog" style="width: 900px;">
      <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Trailer
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              <div class="col-xs-12">
                <div class="col-xs-2">
                  Placa
                  <br />
                  <asp:TextBox ID="txt_editPlaca" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                  Externo
                  <br />
                  <asp:CheckBox ID="chk_editExterno" runat="server" OnCheckedChanged="cambia_interno"
                                Checked="false" AutoPostBack="true" />
                </div>
                <div class="col-xs-2">
                  N° Flota
                  <br />
                  <asp:TextBox ID="txt_editNumero" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-5">
                  Transportista
                  <br />
                  <asp:DropDownList ID="ddl_editTran" CssClass="form-control" runat="server">
                  </asp:DropDownList>
                </div>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <div class="col-xs-3">
                  Tipo
                  <br />
                  <asp:DropDownList ID="ddl_editTipo" CssClass="form-control" runat="server">
                  </asp:DropDownList>
                </div>
                <div class="col-xs-3">
                  ID Shortec
                  <br />
                  <asp:DropDownList ID="ddl_editShorteck" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0" Text="Seleccione..." />
                  </asp:DropDownList>
                </div>
                <div class="col-xs-3">
                  Puerta Abatible
                  <br />
                  <asp:CheckBox ID="chk_editSello" runat="server" />
                </div>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <asp:Literal ID="ltl_contenidoCaract" runat="server"></asp:Literal>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClientClick="agregarCaract();"
                                  OnClick="btn_editGrabar_Click" ValidationGroup="nuevoTrailer" CssClass="btn btn-primary">
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
                        <asp:RequiredFieldValidator ID="rfv_txt_editPlaca" runat="server" ControlToValidate="txt_editPlaca"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevoTrailer">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                            Enabled="True" TargetControlID="rfv_txt_editPlaca">
                        </asp:ValidatorCalloutExtender>
                        <asp:RequiredFieldValidator ID="rfv_numero" runat="server" ControlToValidate="txt_editNumero"
                            Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevoTrailer">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                            Enabled="True" TargetControlID="rfv_numero">
                        </asp:ValidatorCalloutExtender>
                        <asp:RequiredFieldValidator ID="rfv_tran" runat="server" ControlToValidate="ddl_editTran"
                            InitialValue="0" Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                            ValidationGroup="nuevoTrailer">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                            Enabled="True" TargetControlID="rfv_tran">
                        </asp:ValidatorCalloutExtender>
                        <asp:RequiredFieldValidator ID="rfv_tipo" runat="server" ControlToValidate="ddl_editTipo"
                            InitialValue="0" Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                            ValidationGroup="nuevoTrailer">
                        </asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" PopupPosition="BottomLeft"
                            Enabled="True" TargetControlID="rfv_tipo">
                        </asp:ValidatorCalloutExtender>
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
                            <div class="col-xs-4" style="margin-left: 70px">
                                <br />
                                <asp:DropDownList ID="ddTipoBloqueo" CssClass="form-control" Width="200px" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4 ocultar" style="margin-left: 40px">
                                Site
                                <br />
                                <asp:DropDownList ID="ddl_site" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_buscarSite_onChange"
                                    runat="server" Width="200px">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4 ocultar" style="margin-left: 70px">
                                Zona
                                <br />
                                <asp:DropDownList ID="ddl_zona" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_zona_onChange"
                                    runat="server" Width="200px">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4 ocultar" style="margin-left: 40px">
                                Playa
                                <br />
                                <asp:DropDownList ID="ddl_playa" CssClass="form-control" runat="server" Width="200px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddl_playa_onChange">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4 ocultar" style="margin-left: 70px">
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
                            <asp:LinkButton ID="btn_BloquearTrailer" runat="server" CssClass="btn btn-primary"
                                OnClick="btn_BloquearTrailer_Click">
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
    <!--   modal inventario -->
    <div class="modal fade" id="modalinventario" data-backdrop="static" role="dialog">
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>
                                <asp:Label ID="tituloinventario" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body" style="margin-left: 20px; height: 150px">
                            <div class="col-xs-4" style="margin-left: 70px">
                                Estado
                                <br />
                                <asp:DropDownList ID="ddl_estado" CssClass="form-control" Width="200px" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4 " style="margin-left: 40px">
                                Site
                                <br />
                                <asp:DropDownList ID="ddl_siteinventario" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddl_siteinventario_onChange" runat="server" Width="200px">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4 " style="margin-left: 70px">
                                Zona
                                <br />
                                <asp:DropDownList ID="ddl_zonainventario" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddl_zonainventario_onChange" runat="server" Width="200px">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4 " style="margin-left: 40px">
                                Playa
                                <br />
                                <asp:DropDownList ID="ddl_playainventario" CssClass="form-control" runat="server"
                                    Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddl_playainventario_onChange">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4 " style="margin-left: 70px">
                                lugar
                                <br />
                                <asp:DropDownList ID="ddl_lugarinventario" CssClass="form-control" runat="server"
                                    Width="200px">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="modal-footer">
                            <asp:LinkButton ID="confirmar" runat="server" CssClass="btn btn-primary" OnClick="btn_inventario_Click">
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
            <asp:HiddenField ID="hf_idTrailer" runat="server" />
            <asp:HiddenField ID="hf_excluyentes" Value="" runat="server" />
            <asp:HiddenField ID="hf_noexcluyentes" Value="" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
  <script type="text/javascript">
    function modalEditarTrailer() {
        $("#modalTrailer").modal();
    }

    function modalInventario() {
        $("#modalinventario").modal();
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