<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Tipo_Trailer.aspx.cs" Inherits="App_Tipo_Trailer" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Maestro Tipo Trailer
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-2">
    Descripción
    <br />
    <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control"></asp:TextBox>
  </div>
  <div class="col-xs-1">
    <br />
    <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary"
                    ToolTip="Buscar" runat="server">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
    <asp:LinkButton ID="btn_nuevo" CssClass="btn btn-primary" ToolTip="Nuevo"
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
      <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" enableviewState="false" DataKeyNames="ID" AutoGenerateColumns="False"
                      OnRowDataBound="gv_listar_RowDataBound"  OnPageIndexChanging="gv_listar_PageIndexChanging" OnRowCommand="gv_listar_RowCommand" OnRowEditing="gv_listar_RowEditing"
                      OnSorting="gv_listar_Sorting" EmptyDataText="No se encontraron tipo_trailers!" Width="70%" CssClass="table table-bordered table-hover tablita" >
          <Columns>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="3%"
                               ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_modificarTipoTrailer" runat="server" CausesValidation="false" CommandName="Edit"
                                CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-pencil"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="3%"
                               Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_eliminarTipoTrailer" runat="server" CausesValidation="False" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>'
                                CssClass="btn btn-sm btn-primary" ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="ID" SortExpression="ID"
                            ItemStyle-Height="30px" Visible="false"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION"
                            ItemStyle-Width="50%" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Color" DataField="COLOR" SortExpression="COLOR"
                            ItemStyle-Width="5%" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
          </Columns>
          <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
      </center>
        
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" Runat="Server">
  <!-- modal TipoTrailer  ---->
  <div class="modal fade" id="modalTipoTrailer" data-backdrop="static" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Tipo Trailer
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              <div class="col-xs-9">
                Descripción
                <br />
                <asp:TextBox ID="txt_editDesc" CssClass="form-control input-mayus" runat="server"></asp:TextBox>
              </div>
              <div class="col-xs-3">
                Color
                <br />
                  <asp:TextBox id="txt_editColor" CssClass="color" runat="server" />
                <%--<telerik:RadColorPicker ID="rcp_editColor" runat="server">
                </telerik:RadColorPicker>--%>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_grabar_Click" ValidationGroup="nuevoTipoTrailer"
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
                                        ValidationGroup="nuevoTipoTrailer">
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
                Eliminar Tipo Trailer
              </h4>
            </div>
            <div class="modal-body">
              Se eliminará el tipo trailer seleccionado, ¿desea continuar?
            </div>
            <div class="modal-footer">
              <asp:LinkButton ID="btnModalEliminar" runat="server"
                              CssClass="btn btn-primary" OnClick="btn_eliminar_Click">
                              <span class="glyphicon glyphicon-ok"></span>
              </asp:LinkButton>
              <button type="button" class="btn btn-primary" data-dismiss="modal">
                <span class="glyphicon glyphicon-remove"></span></button>
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
      <asp:HiddenField ID="hf_idTipoTrailer" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
    <asp:Content ID="Scripts" ContentPlaceHolderID="scripts" Runat="Server">
    <link rel="stylesheet" media="screen" type="text/css" href="../css2/spectrum.css" />
    <script type="text/javascript" src="../js2/spectrum.js"></script>
  <script type="text/javascript">
    function modalEditarTipoTrailer() {
    $("#modalTipoTrailer").modal();
        }
    
    function modalConfirmacion() {
    $("#modalConfirmacion").modal();
    }

    function EndRequestHandler(sender, args) {
        $(".color").spectrum({
            change: function (color) {
                $(this).val(color.toHexString());
            }
        });
    }

       Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
  </script>
</asp:Content>
