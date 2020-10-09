<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Tipo_Cargav2.aspx.cs" Inherits="App_Tipo_Carga2" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Maestro Tipo Carga
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12">
    <div class="col-xs-2">
      Descripción
      <br />
      <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
    </div>
    <div class="col-xs-2">
      <br />
      <asp:LinkButton ID="btn_buscarTipoCarga" OnClick="btn_buscarTipoCarga_Click" CssClass="btn btn-primary"
                      ToolTip="Buscar TipoCarga" runat="server">
        <span class="glyphicon glyphicon-search"></span>
      </asp:LinkButton>
      <asp:LinkButton ID="btn_nuevoTipoCarga" CssClass="btn btn-primary" ToolTip="Nuevo TipoCarga"
                      runat="server" OnClick="btn_nuevoTipoCarga_Click">
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
          <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" PageSize="8" GridLines="Horizontal"
                        OnPageIndexChanging="gv_listar_PageIndexChanging" OnRowCommand="gv_listar_RowCommand" OnRowEditing="gv_listar_RowEditing"
                        OnSorting="gv_listar_Sorting" OnRowDataBound="gv_listar_RowDataBound" EmptyDataText="No se encontraron tipos trailer!" 
                        AutoGenerateColumns="False" Width="70%" CssClass="table table-bordered table-hover tablita">
            <Columns>
              <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_modificarTipoCarga" runat="server" CausesValidation="false" CommandName="Edit"
                                  CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-pencil"></span>
                  </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_destinosTipoCarga" runat="server" CausesValidation="false" CommandName="DESTINO" CommandArgument='<%# Eval("ID") %>'
                                  CssClass="btn btn-sm btn-primary" ToolTip="Destinos">
                    <span class="glyphicon glyphicon-map-marker"></span>
                  </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>
              <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_eliminarTipoCarga" runat="server" CausesValidation="False" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>'
                                  CssClass="btn btn-sm btn-primary" ToolTip="Eliminar">
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
  <!-- modal TipoCarga  ---->
  <div class="modal fade" id="modalTipoCarga" data-backdrop="static" role="dialog">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Tipo Carga
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              <div class="col-xs-7">
                Descripción
                <br />
                <asp:TextBox ID="txt_editDesc" CssClass="form-control input-mayus" runat="server"></asp:TextBox>
              </div>
              <div class="col-xs-5">
                Pre-ingreso
                <br />
                <asp:CheckBox id="chk_editPreingreso" runat="server" />
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="nuevoTipoCarga"
                                  CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-primary" data-dismiss="modal">
                <span class="glyphicon glyphicon-remove"></span>
              </button>
            </div>
            <asp:RequiredFieldValidator ID="rfv_txt_editDesc" runat="server" ControlToValidate="txt_editDesc"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoTipoCarga">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editDesc">
            </asp:ValidatorCalloutExtender>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
  </div>
  <div class="modal fade" id="modalDestino" data-backdrop="static" role="dialog" >
    <div class="modal-dialog" style="width:1200px" role="dialog">
      <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                Destinos Tipo Carga
              </h4>
            </div>
            <div class="modal-body" style="height:auto;overflow:auto">
              
              <div class="col-xs-3">
                Site
                <br />
                <asp:DropDownList ID="ddl_destinoSite" OnSelectedIndexChanged="ddl_destinoSite_IndexChanged" CssClass="form-control" AutoPostBack="true" runat="server">
                  <asp:ListItem Value="0" Text="Seleccione..." />
                </asp:DropDownList>
              </div>
              <div class="col-xs-3">
                Zona
                <br />
                <asp:DropDownList ID="ddl_destinoZona" OnSelectedIndexChanged="ddl_destinoZona_IndexChanged" CssClass="form-control" Enabled="false" AutoPostBack="true" runat="server">
                  <asp:ListItem Value="0" Text="Seleccione..." />
                </asp:DropDownList>
              </div>
               
              <div class="col-xs-12 separador"></div>
              
              <div class="col-xs-6" style="overflow-y:auto;height:30vh">
                <asp:GridView ID="gv_noSeleccionados" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false"
                              width="100%" OnRowCommand="gv_noSeleccionados_RowCommand" runat="server" EmptyDataText="No hay playas para seleccionar!">
                  <Columns>
                    <asp:BoundField DataField="SITE_ID" visible="false"/>
                    <asp:BoundField DataField="PLAY_ID" visible="false"/>
                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Playa"/>
                    <asp:BoundField DataField="ZONA_ID" visible="false" />
                    <asp:BoundField DataField="ZONA" HeaderText="Zona" />
                    <%--<asp:BoundField DataField="TIIC_ID" visible="false" />--%>
                    <asp:TemplateField>
                      <ItemTemplate>
                        <asp:LinkButton ID="btn_agregar" CommandName="SEL"
                                        CommandArgument='<%# Container.DataItemIndex %>' runat="server">
                          <span class="glyphicon glyphicon-menu-right"></span>
                        </asp:LinkButton>
                      </ItemTemplate>
                    </asp:TemplateField>
                  </Columns>
                </asp:GridView>
              </div>
              <div class="col-xs-6" style="overflow-y:auto;height:30vh">
                <asp:GridView id="gv_seleccionados" CssClass="table table-bordered table-hover tablita" AutoGenerateColumns="false" EmptyDataText="No hay playas seleccionadas!"
                              width="100%" OnRowCommand="gv_seleccionados_RowCommand" OnRowDataBound="gv_seleccionados_RowDataBound" runat="server">
                  <Columns>
                    <asp:TemplateField>
                      <ItemTemplate>
                        <asp:LinkButton ID="btn_quitar" CommandName="SEL" cssclass="btn btn-primary btn-sm"
                                        CommandArgument='<%# Container.DataItemIndex %>' runat="server">
                          <span class="glyphicon glyphicon-erase"></span>
                        </asp:LinkButton>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                      <ItemTemplate>
                        <asp:LinkButton ID="btn_subir" CommandName="ARRIBA"
                                        CommandArgument='<%# Container.DataItemIndex %>' runat="server">
                          <span class="glyphicon glyphicon-menu-up"></span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btn_bajar" CommandName="ABAJO"
                                        CommandArgument='<%# Container.DataItemIndex %>' runat="server">
                          <span class="glyphicon glyphicon-menu-down"></span>
                        </asp:LinkButton>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SITE_ID" visible="false"/>
                    <asp:BoundField DataField="PLAY_ID_ORI" visible="false"/>
                    <asp:BoundField DataField="ORDEN" HeaderText="Orden" Visible="false" />
                    <%--<asp:BoundField DataField="ORIGEN" HeaderText/>--%>
                    <asp:BoundField DataField="PLAY_ID_DES" visible="false"/>
                    <asp:BoundField DataField="DESTINO" HeaderText="Playa Destino" />
                    <asp:BoundField DataField="ZONA_ID" visible="false" />
                    <asp:BoundField DataField="ZONA_DESTINO" HeaderText="Zona Destino" />
                    <asp:BoundField DataField="TIIC_ID" visible="false" />
                  </Columns>
                </asp:GridView>
              </div>
               
              <div class="col-xs-12 separador"></div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_asignarGuardar" runat="server"
                                  CssClass="btn btn-primary" OnClick="btn_asignarGuardar_Click">
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
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
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
              <asp:LinkButton ID="btnModalEliminar" runat="server"
                              CssClass="btn btn-primary" OnClick="btn_EliminarTipoCarga_Click">
                <span class="glyphicon glyphicon-ok"></span>
              </asp:LinkButton>
              <button type="button" class="btn btn-primary" data-dismiss="modal">
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
      <asp:HiddenField ID="hf_idTipoCarga" runat="server" />
      <asp:HiddenField ID="hf_seleccionados" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" Runat="Server">
      <script type="text/javascript">
    function modalTipoCarga() {
        $("#modalTipoCarga").modal();
    }
    
    function modalConfirmacion() {
        $("#modalConfirmacion").modal();
    }

    function modalDestino() {
        $("#modalDestino").modal();
    }
  </script>
</asp:Content>
