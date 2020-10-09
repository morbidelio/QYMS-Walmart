<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Tipo_Solicitud.aspx.cs" Inherits="App_Tipo_Solicitud" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Maestro Tipo Solicitud
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12">
    <div class="col-xs-2">
      Descripción
      <br />
      <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
    </div>
    <div class="col-xs-2">
      <br />
      <asp:LinkButton ID="btn_buscarTipoSolicitud" OnClick="btn_buscarTipoSolicitud_Click" CssClass="btn btn-primary"
                      ToolTip="Buscar TipoSolicitud" runat="server">
        <span class="glyphicon glyphicon-search"></span>
      </asp:LinkButton>
      <asp:LinkButton ID="btn_nuevoTipoSolicitud" CssClass="btn btn-primary" ToolTip="Nuevo TipoSolicitud"
                      runat="server" OnClick="btn_nuevoTipoSolicitud_Click">
        <span class="glyphicon glyphicon-plus"></span>
      </asp:LinkButton>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="col-xs-12">
      <center>
        <asp:GridView ID="gv_listaTipoSolicitud" runat="server" AllowPaging="True" AllowSorting="True"
                      BorderColor="Black" CellPadding="8" DataKeyNames="ID" BackColor="White" GridLines="Horizontal"
                      EnableSortingAndPagingCallbacks="True" OnPageIndexChanging="gv_listaTipoSolicitud_PageIndexChanging1"
                      OnRowCommand="gv_listaTipoSolicitud_RowCommand" OnRowEditing="gv_listaTipoSolicitud_RowEditing"
                      OnSorting="gv_listaTipoSolicitud_Sorting" EmptyDataText="No se encontraron tipos de solicitud!" AutoGenerateColumns="False"
                      Width="60%" PagerSettings-Mode="NumericFirstLast" CssClass="table table-bordered table-hover tablita"
                      OnRowDataBound="gv_listaTipoSolicitud_RowDataBound">
          <Columns>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%"
                               ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_modificarTipoSolicitud" runat="server" CausesValidation="false" CommandName="Edit"
                                CssClass="btn btn-primary" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-pencil"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%"
                               Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_eliminarTipoSolicitud" runat="server" CausesValidation="False" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>'
                                CssClass="btn btn-primary" ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="ID" SortExpression="ID"
                            ItemStyle-Height="30px" Visible="false"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION"
                            ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
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
      </center>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" Runat="Server">
  <!-- modal TipoSolicitud  ---->
  <div class="modal fade" id="modalTipoSolicitud" data-backdrop="static" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Tipo Solicitud
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              <div class="col-xs-12">
                Descripción
                <br />
                <asp:TextBox ID="txt_editDesc" CssClass="form-control" runat="server"></asp:TextBox>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="nuevoTipoSolicitud"
                                  CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-primary" data-dismiss="modal">
                <span class="glyphicon glyphicon-remove"></span></button>
            </div>
            <asp:RequiredFieldValidator ID="rfv_txt_editDesc" runat="server" ControlToValidate="txt_editDesc"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoTipoSolicitud">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editDesc">
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
                <asp:Label ID="lblRazonEliminacion" runat="server"></asp:Label>
              </h4>
            </div>
            <div class="modal-body">
              <asp:Label ID="msjEliminacion" runat="server"></asp:Label>
            </div>
            <div class="modal-footer">
              <asp:LinkButton ID="btnModalEliminar" runat="server" data-dismiss="modal"
                              CssClass="btn btn-primary">
              </asp:LinkButton>
              <button type="button" data-dismiss="modal" class="btn btn-primary" >Cancelar</button>
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
      <asp:HiddenField ID="hf_idTipoSolicitud" runat="server" />
          <asp:Button ID="btn_eliminarTipoSolicitud" OnClick="btn_EliminarTipoSolicitud_Click" runat="server" Text="Button" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
    <asp:Content ID="Scripts" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
    function modalEditarTipoSolicitud() {
        $("#modalTipoSolicitud").modal();
    }
    
    function modalConfirmacion() {
        $("#modalConfirmacion").modal();
    }

    function eliminarTipoSolicitud() {
        $("#<%= btn_eliminarTipoSolicitud.ClientID %>").click();
    }
  </script>
</asp:Content>