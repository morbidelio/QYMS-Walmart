<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Zona.aspx.cs" Inherits="App_Zona" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Maestro Zona
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
  <div class="col-xs-2">
    Nombre
    <br />
    <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
  </div>
  <div class="col-xs-2">
    Site
    <br />
    <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" runat="server">
    </asp:DropDownList>
  </div>
  <div class="col-xs-2">
    Tipo Zona
    <br />
    <asp:DropDownList ID="ddl_buscarTipoZona" CssClass="form-control" runat="server">
    </asp:DropDownList>
  </div>
  <div class="col-xs-2">
    <br />
    <asp:LinkButton ID="btn_buscarZona" OnClick="btn_buscarZona_Click" CssClass="btn btn-primary"
                    ToolTip="Buscar Zona" runat="server">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
    <asp:LinkButton  id="btn_nuevoZona" class="btn btn-primary" tooltip="Nuevo Zona" runat="server" OnClick="btn_nuevoZona_Click">
      <span class="glyphicon glyphicon-plus"></span>
    </asp:LinkButton>
  </div>
       
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      
      <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" PageSize="8" DataKeyNames="ID" Width="100%"
                    OnPageIndexChanging="gv_listar_PageIndexChanging" OnRowCommand="gv_listar_RowCommand" OnRowEditing="gv_listar_RowEditing" OnSorting="gv_listar_Sorting"
                    EmptyDataText="No se encontraron Zonas!" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tablita">
        <Columns>
          <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
            <ItemTemplate>
              <asp:LinkButton ID="btn_modificarZona" runat="server" CausesValidation="false" CommandName="Edit"
                              CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                <span class="glyphicon glyphicon-pencil"></span>
              </asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
          </asp:TemplateField>
          <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
            <ItemTemplate>
              <asp:LinkButton ID="btn_eliminarZona" runat="server" CausesValidation="False" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>'
                              CssClass="btn btn-sm btn-primary" ToolTip="Eliminar">
                <span class="glyphicon glyphicon-remove"></span>
              </asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
          </asp:TemplateField>
          <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
            <ItemTemplate>
              <asp:LinkButton ID="btn_zonaVirtual" runat="server" CausesValidation="true" CommandName="VIRTUAL" CommandArgument='<%# Eval("ID") %>'
                              CssClass="btn btn-sm btn-primary" ToolTip="Virtual/Física">
                <span class="glyphicon glyphicon-cloud"></span>
              </asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
          </asp:TemplateField>
          <asp:BoundField ReadOnly="True" DataField="ID" Visible="false"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Site" DataField="SITE" SortExpression="SITE"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Tipo" DataField="TIPO_ZONA" SortExpression="TIPO_ZONA"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Zona Virtual" DataField="ZONA_VIRTUAL" SortExpression="ZONA_VIRTUAL"></asp:BoundField>
        </Columns>
          <PagerStyle CssClass="pagination-ys" />
      </asp:GridView>
       
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" Runat="Server">
  <!-- modal Zona  ---->
  <div class="modal fade" id="modalZona" data-backdrop="static" role="dialog">
    <div class="modal-dialog">
      <div class="modal-content">
        <asp:UpdatePanel runat="server">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Zona
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              
              <div class="col-xs-3">
                Código
                <br />
                <asp:TextBox ID="txt_editCodigo" CssClass="form-control input-mayus" Width="90%" runat="server"></asp:TextBox>
              </div>           
              <div class="col-xs-9">
                Descripción
                <br />
                <asp:TextBox ID="txt_editDesc" CssClass="form-control input-mayus" Width="90%" runat="server"></asp:TextBox>
              </div>
               
              <div class="col-xs-12 separador">
              </div>
                                   
              <div class="col-xs-5">
                Site
                <br />
                <asp:DropDownList ID="ddl_editSite" CssClass="form-control" runat="server">
                </asp:DropDownList>
              </div>
              <div class="col-xs-5">
                Tipo Zona
                <br />
                <asp:DropDownList ID="ddl_editTipoZona" CssClass="form-control" runat="server">
                </asp:DropDownList>
              </div>
               
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="nuevoZona"
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
            <asp:RequiredFieldValidator ID="rfv_txtCodigo" runat="server" ControlToValidate="txt_editCodigo"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoZona">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txtCodigo">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_txtDesc" runat="server" ControlToValidate="txt_editDesc"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoZona">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txtDesc">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_ddl_editSite" runat="server" ControlToValidate="ddl_editSite"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="default"
                                        ValidationGroup="nuevoZona">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_ddl_editSite">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_ddl_editTipoZona" runat="server" ControlToValidate="ddl_editTipoZona"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="default"
                                        ValidationGroup="nuevoZona">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_ddl_editTipoZona">
            </asp:ValidatorCalloutExtender>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
  </div>
  <!-- Modal Eliminación -->
  <div class="modal fade" id="modalConfirmacion" data-backdrop="static" role="dialog" >
    <div class="modal-dialog" role="dialog">
      <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                Eliminar Zona
              </h4>
            </div>
            <div class="modal-body">
              Se eliminará la zona seleccionada, ¿desea continuar?
            </div>
            <div class="modal-footer">
              <asp:LinkButton ID="btnModalEliminar" runat="server"
                              CssClass="btn btn-primary" OnClick="btn_EliminarZona_Click">
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
  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hf_idZona" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
    function modalEditarZona(a) {
        if (a == true)
            limpiarForm();
        $("#modalZona").modal();
    }
    
    function modalConfirmacion() {
        $("#modalConfirmacion").modal();
    }

    function limpiarForm() {
        $("#<%= hf_idZona.ClientID %>").val("");
        $("#<%= txt_editCodigo.ClientID %>").val("");
        $("#<%= txt_editDesc.ClientID %>").val("");
        $("#<%= ddl_editSite.ClientID %>").val("0");
        $("#<%= ddl_editTipoZona.ClientID %>").val("default");
        //    hf_idZona.Value = "";
        //    txt_editCodigo.Text = "";
        //    txt_editDesc.Text = "";
        //    ddl_editSite.ClearSelection();
        //    ddl_editTipoZona.ClearSelection();
    }
    
  </script>
</asp:Content>
