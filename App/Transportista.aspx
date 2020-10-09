<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Transportista.aspx.cs" Inherits="App_Transportista" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Maestro Transportista
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" Runat="Server">
  
  <div class="col-xs-2">
    Rut
    <br />
    <asp:TextBox ID="txt_buscarRut" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
  </div>
  <div class="col-xs-2">
    Nombre
    <br />
    <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
  </div>
  <div class="col-xs-2">
    <br />
    <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary"
                    ToolTip="Buscar Transportista" runat="server">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
    <asp:LinkButton ID="btn_nuevo" CssClass="btn btn-primary" ToolTip="Nueva Transportista"
                    runat="server" OnClick="btn_nuevo_Click">
      <span class="glyphicon glyphicon-plus"></span>
    </asp:LinkButton>
  </div>
   
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="col-xs-12">
        <center>
          <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" PageSize="8" Width="70%" DataKeyNames="ID" enableviewstate="false"
                        OnPageIndexChanging="gv_listar_PageIndexChanging" OnRowCommand="gv_listar_RowCommand" OnRowEditing="gv_listar_RowEditing" OnSorting="gv_listar_Sorting"
                        EmptyDataText="No se encontraron Clientes!" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tablita">
            <Columns>
              <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_modificar" runat="server" CausesValidation="false" CommandName="Edit"
                                  CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-pencil"></span>
                  </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_eliminar" runat="server" CausesValidation="False" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>'
                                  CssClass="btn btn-sm btn-primary" ToolTip="Eliminar">
                    <span class="glyphicon glyphicon-remove"></span>
                  </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="ID" SortExpression="ID"
                              ItemStyle-Height="30px" Visible="false"></asp:BoundField>
              <asp:BoundField ReadOnly="True" HeaderText="Rut" DataField="RUT" SortExpression="RUT"
                              ItemStyle-Height="30px" ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                              HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"></asp:BoundField>
              <asp:BoundField ReadOnly="True" HeaderText="Nombre" DataField="NOMBRE" SortExpression="NOMBRE"
                              ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                              HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
              <asp:BoundField ReadOnly="True" HeaderText="Rol" DataField="ROL"
                              SortExpression="ROL" ItemStyle-Width="10%" HeaderStyle-Width="10%"
                              ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            </Columns>
          <PagerStyle CssClass="pagination-ys" />
          </asp:GridView>
        </center>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" Runat="Server">
  <!-- modal Transportista  ---->
  <div class="modal fade" id="modalTransportista" data-backdrop="static" role="dialog">
    <div class="modal-dialog">
      <div class="modal-content">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Transportista
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              
              <div class="col-xs-3">
                Rut
                <br />
                <asp:TextBox ID="txt_editRut" runat="server" Width="100px" CssClass="form-control" OnTextChanged="txt_editRut_TextChanged"
                             AutoPostBack="True">
                </asp:TextBox>
              </div>
              <div class="col-xs-5">
                Nombre
                <br />
                <asp:TextBox ID="txt_editNombre" runat="server" CssClass="form-control input-mayus"></asp:TextBox>
              </div>
              <div class="col-xs-4">
                Rol
                <br />
                <asp:TextBox ID="txt_editRol" runat="server" CssClass="input-number form-control"></asp:TextBox>
              </div>
               
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabarNuevo" runat="server" OnClick="btn_grabar_Click" ValidationGroup="nuevoCliente"
                                  CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" data-dismiss="modal" class="btn btn-primary" >
                <span class="glyphicon glyphicon-remove"></span>
              </button>
            </div>

            <asp:RequiredFieldValidator ID="rfv_txt_editRut" runat="server" ControlToValidate="txt_editRut"
                                        Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" PopupPosition="BottomLeft"
                                          runat="server" Enabled="True" TargetControlID="rfv_txt_editRut">
            </asp:ValidatorCalloutExtender>
            <asp:CustomValidator ID="customRut" ClientValidationFunction="validarRut" runat="server"
                                 ControlToValidate="txt_editRut" ValidationGroup="nuevoCliente">
            </asp:CustomValidator>

            <asp:RequiredFieldValidator ID="rfv_txt_editNombre" runat="server" ControlToValidate="txt_editNombre"
                                        Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft" Enabled="True"
                                          TargetControlID="rfv_txt_editNombre">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_txt_editRol" runat="server" ControlToValidate="txt_editRol"
                                        Display="None" ErrorMessage="* Requerido" ValidationGroup="nuevoCliente" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" PopupPosition="BottomLeft"
                                          TargetControlID="rfv_txt_editRol">
            </asp:ValidatorCalloutExtender>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
  </div>
  <!-- modal eliminación -->
  <div class="modal fade" id="modalEliminar" data-backdrop="static" role="dialog" >
    <div class="modal-dialog" role="dialog">
      <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                Eliminar transportista
              </h4>
            </div>
            <div class="modal-body">
              Se eliminará el transportista seleccionado. ¿Desea continuar?
            </div>
            <div class="modal-footer">
              <asp:LinkButton ID="btnModalEliminar" runat="server" Tooltip="Eliminar"
                              CssClass="btn btn-primary" OnClick="btn_eliminar_click">
                <span class="glyphicon glyphicon-ok"></span>
              </asp:LinkButton>
              <button type="button" data-dismiss="modal" class="btn btn-primary" >
                <span class="glyphicon glyphicon-remove"></span>
              </button>
            </div>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hf_id" runat="server" />
      <asp:HiddenField ID="hf_rut" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
    function modalTransportista() {
        $("#modalTransportista").modal();
    }
    
    function modalEliminar() {
        $("#modalEliminar").modal();
    }

    function validarRut(sender, e) {
        var sw = true;
        if ($get("<%=this.txt_editRut.ClientID %>").value != "") {
            sw = validaRut($get("<%=this.txt_editRut.ClientID %>").value);
        }
        if (sw){
            formatearRut($get("<%=this.txt_editRut.ClientID %>"));
        }
        e.IsValid = sw;
    }
  </script>
</asp:Content>