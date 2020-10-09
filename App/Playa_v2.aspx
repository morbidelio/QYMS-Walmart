<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
CodeFile="Playa_v2.aspx.cs" Inherits="App_Playa_v2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Maestro Playa
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
  
  <div class="col-xs-2">
    Site
    <br />
    <asp:DropDownList ID="ddl_site" OnSelectedIndexChanged="ddl_site_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
    </asp:DropDownList>
  </div>
  <div class="col-xs-1">
    Código
    <br />
    <asp:TextBox ID="txt_buscarCodigo" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
  </div>
  <div class="col-xs-2">
    Nombre
    <br />
    <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
  </div>
  <div class="col-xs-2">
    Zona
    <br />
    <asp:DropDownList ID="ddl_buscarZona" CssClass="form-control" runat="server">
    </asp:DropDownList>
  </div>
  <div class="col-xs-1">
    Solo Físicos
    <br />
    <asp:CheckBox ID="chk_buscarVirtual" runat="server" />
  </div>
  <div class="col-xs-2">
    <br />
    <asp:LinkButton ID="btn_buscarPlaya" OnClick="btn_buscarPlaya_Click" CssClass="btn btn-primary"
                    ToolTip="Buscar Playa" runat="server">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
    <asp:LinkButton ID="btn_nuevaPlaya" CssClass="btn btn-primary" ToolTip="Nueva Playa"
                    runat="server" OnClick="btn_nuevaPlaya_Click">
      <span class="glyphicon glyphicon-plus"></span>
    </asp:LinkButton>
  </div>
   
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      
      <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True"
                    DataKeyNames="ID" OnPageIndexChanging="gv_listar_PageIndexChanging"
                    OnRowCommand="gv_listar_RowCommand" OnRowEditing="gv_listar_RowEditing"
                    PageSize="8" OnSorting="gv_listar_Sorting" EmptyDataText="¡No se encontraron playas!"
                    AutoGenerateColumns="False" Width="100%" PagerSettings-Mode="NumericFirstLast"
                    CssClass="table table-bordered table-hover tablita" OnRowDataBound="gv_listar_RowDataBound">
        <Columns>
          <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
            <ItemTemplate>
              <asp:LinkButton ID="btn_modificarPlaya" runat="server" CausesValidation="false" CommandName="Edit"
                              CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                <span class="glyphicon glyphicon-pencil"></span>
              </asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
          </asp:TemplateField>
          <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
            <ItemTemplate>
              <asp:LinkButton ID="btn_eliminarPlaya" runat="server" CausesValidation="False" CommandName="ELIMINAR"
                              CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-sm btn-primary" ToolTip="Eliminar">
                <span class="glyphicon glyphicon-remove"></span>
              </asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
          </asp:TemplateField>
          <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
            <ItemTemplate>
              <asp:LinkButton ID="btn_CaracteristicsPlaya" runat="server" CausesValidation="False"
                              CommandName="CARACT" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-sm btn-primary"
                              ToolTip="Tipo Trailer">
                <span class="glyphicon glyphicon-th-list"></span>
              </asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
          </asp:TemplateField>
          <asp:BoundField ReadOnly="True" DataField="ID" Visible="false"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO" ></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Virtual" DataField="VIRTUAL" SortExpression="VIRTUAL"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Zona" DataField="ZONA" SortExpression="ZONA"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Tipo Playa" DataField="TIPO" SortExpression="TIPO"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Tipo Zona" DataField="ZONA_TIPO" SortExpression="ZONA_TIPO"></asp:BoundField>
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
       
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
  <!-- modal Playa  ---->
  <div class="modal fade" id="modalPlaya" data-backdrop="static" role="dialog">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Playa
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              
              <div class="col-xs-2">
                Código
                <br />
                <asp:TextBox ID="txt_editCodigo" CssClass="form-control input-mayus" runat="server"></asp:TextBox>
              </div>
              <div class="col-xs-3">
                Descripción
                <br />
                <asp:TextBox ID="txt_editDesc" CssClass="form-control input-mayus" runat="server"></asp:TextBox>
              </div>
              <div class="col-xs-3">
                Tipo Playa
                <br />
                <asp:DropDownList ID="ddl_editTipo" CssClass="form-control" runat="server">
                  <asp:ListItem Value="0" Text="Seleccione..."></asp:ListItem>
                </asp:DropDownList>
              </div>
              <div class="col-xs-3">
                Tipo Zona
                <br />
                <asp:DropDownList ID="ddl_editTipoZona" CssClass="form-control" runat="server">
                  <asp:ListItem Value="0" Text="Seleccione..."></asp:ListItem>
                </asp:DropDownList>
              </div>
               
              <div class="col-xs-12 separador">
              </div>
              
              <div class="col-xs-3">
                Site
                <br />
                <asp:DropDownList ID="ddl_editSite" AutoPostBack="true" OnSelectedIndexChanged="ddl_editSite_SelectedIndexChanged" CssClass="form-control" runat="server">
                </asp:DropDownList>
              </div>
              <div class="col-xs-6">
                Zona
                <br />
                <asp:DropDownList ID="ddl_editZona" CssClass="form-control" runat="server">
                </asp:DropDownList>
              </div>
              <div class="col-xs-2">
                Virtual
                <br />
                <asp:CheckBox ID="chk_editVirtual" runat="server" />
              </div>
               
              <div class="col-xs-12 separador">
              </div>     
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click"
                                  ValidationGroup="nuevoPlaya" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" data-dismiss="modal" class="btn btn-primary">
                <span class="glyphicon glyphicon-remove"></span></button>
            </div>
            <asp:RequiredFieldValidator ID="rfv_Codigo" runat="server" ControlToValidate="txt_editCodigo"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevoPlaya">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_Codigo">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_txt_editDesc" runat="server" ControlToValidate="txt_editDesc"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevoPlaya">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editDesc">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_ddl_editZona" runat="server" ControlToValidate="ddl_editZona"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0"
                                        ValidationGroup="nuevoPlaya">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_ddl_editZona">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_ddl_editTipo" runat="server" ControlToValidate="ddl_editTipo"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0"
                                        ValidationGroup="nuevoPlaya">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_ddl_editTipo">
            </asp:ValidatorCalloutExtender>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
  </div>
  <!-- Modal Eliminación -->
  <div class="modal fade" id="modalConfirmacion" data-backdrop="static" role="dialog">
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
              <asp:LinkButton ID="btnModalEliminar" runat="server" data-dismiss="modal" CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-ok"></span>
              </asp:LinkButton>
              <button type="button" data-dismiss="modal" class="btn btn-primary">
                <span class="glyphicon glyphicon-remove"></span></button>
            </div>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
  <!-- Modal Caracteristicas -->
  <div class="modal fade" id="ModalCaracteristicas" data-backdrop="static" role="dialog">
    <div class="modal-dialog" role="dialog">
      <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                Editar Tipos Trailer Soportados
              </h4>
            </div>
            <div class="modal-body" style="height:auto;overflow-y:auto">
              
              <div class="col-xs-2" style="width:20%">
                Ways
                <br />
                <asp:CheckBox id="chk_ways" runat="server" />
              </div>
              <div class="col-xs-2" style="width:20%">
                Seco
                <br />
                <asp:CheckBox id="chk_seco" runat="server" />
              </div>
              <div class="col-xs-2" style="width:20%">
                Frio
                <br />
                <asp:CheckBox ID="chk_frio" runat="server" />
              </div>
              <div class="col-xs-2" style="width:20%">
                Multifrío
                <br />
                <asp:CheckBox id="chk_mfrio" runat="server" />
              </div>
              <div class="col-xs-2" style="width:20%">
                Congelado
                <br />
                <asp:CheckBox id="chk_cong" runat="server" />
              </div>
               
              <div class="col-xs-12 separador"></div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_guardarTTrai" runat="server" onclick="btn_guardarTTrai_Click"
                                  CssClass="btn btn-primary" ToolTip="Guardar">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" data-dismiss="modal" class="btn btn-primary">
                <span class="glyphicon glyphicon-remove"></span>
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
      <asp:HiddenField ID="hf_idPlaya" runat="server" />
      <%--<asp:HiddenField ID="hf_excluyentes" runat="server" />
      <asp:HiddenField ID="hf_noexcluyentes" runat="server" />--%>
      <asp:HiddenField ID="hf_concaract" runat="server" />
      <asp:Button ID="btn_eliminarPlaya" OnClick="btn_EliminarPlaya_Click" runat="server"
                  Text="Button" />
      <%--<asp:Button ID="btn_GrabarCarac" OnClick="btn_GrabarCarac_Click" runat="server" Text="Button" />--%>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
  <script type="text/javascript">
    function modalEditarPlaya() {
        $("#modalPlaya").modal();
    }

    function modalConfirmacion() {
        $("#modalConfirmacion").modal();
    }

    function ModalCaracteristicas() {
        $("#ModalCaracteristicas").modal();
    }

    function eliminarPlaya() {
        var clickButton = document.getElementById("<%= this.btn_eliminarPlaya.ClientID %>");
        clickButton.click();
    }

    function llenarForm() {
        limpiarForm();
        var x = ex.split(',');
        var y = no_ex.split(',');
        for (var i = 0; i < x.length; i++) {
            $("#caractTipo_" + x[i]).prop('checked', true);
        }
        for (var i = 0; i < y.length; i++) {
            $("#op_drop_" + y[i]).prop('selected', true);
        }
    }
  </script>
</asp:Content>