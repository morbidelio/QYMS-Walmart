<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Timer_Procesos.aspx.cs" Inherits="App_Timer_Procesos" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Maestro Timer Procesos
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <div class="col-xs-12">
    <div class="col-xs-2">
      Site
      <br />
      <asp:DropDownList ID="ddl_buscaSite" runat="server" CssClass="form-control">
      </asp:DropDownList>
    </div>
    <div class="col-xs-2">
      Nombre
      <br />
      <asp:TextBox ID="txt_buscaNombre" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="col-xs-2">
      Estado Trailer
      <br />
      <asp:DropDownList ID="ddl_buscaEstadoSol" runat="server" CssClass="form-control">
      </asp:DropDownList>
    </div>
    <div class="col-xs-2">
      Tipo Zona
      <br />
      <asp:DropDownList ID="ddl_buscaTipoZona" runat="server" CssClass="form-control">
      </asp:DropDownList>
    </div>        
    <div class="col-xs-2">
      Tipo Playa
      <br />
      <asp:DropDownList ID="ddl_buscaTipoPlaya" runat="server" CssClass="form-control">
        <asp:ListItem Value="0" Text="Seleccione..."></asp:ListItem>
        <asp:ListItem Value="100" Text="Andén"></asp:ListItem>
        <asp:ListItem Value="200" Text="Estacionamiento"></asp:ListItem>
      </asp:DropDownList>
    </div>
    <div class="col-xs-2">
      <br />
      <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary"
                      ToolTip="Buscar Playa" runat="server">
        <span class="glyphicon glyphicon-search"></span>
      </asp:LinkButton>
      <asp:LinkButton ID="btn_nuevo" CssClass="btn btn-primary" ToolTip="Nueva Playa"
                      runat="server" OnClick="btn_nuevo_Click">
        <span class="glyphicon glyphicon-plus"></span>
      </asp:LinkButton>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" Runat="Server">
  <div class="col-xs-12">
    <asp:UpdatePanel runat="server">
      <ContentTemplate>
        <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" PageSize="6"
                      BorderColor="Black" CellPadding="8" DataKeyNames="ID" BackColor="White" GridLines="Horizontal"
                      EnableSortingAndPagingCallbacks="True" OnPageIndexChanging="gv_listar_PageIndexChanging"
                      OnRowCommand="gv_listar_RowCommand" OnRowEditing="gv_listar_RowEditing" OnRowDataBound="gv_listar_RowDataBound"
                      OnSorting="gv_listar_Sorting" EmptyDataText="No se encontraron Timers!" AutoGenerateColumns="False"
                      Width="100%" PagerSettings-Mode="NumericFirstLast" CssClass="table table-bordered table-hover tablita">
          <Columns>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%"
                               ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_modificarTimer" runat="server" CausesValidation="false" CommandName="Edit"
                                CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-pencil"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%"
                               Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_eliminarTimer" runat="server" CausesValidation="False" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>'
                                CssClass="btn btn-sm btn-primary" ToolTip="Eliminar">
                  <span class="glyphicon glyphicon-remove"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:BoundField ReadOnly="True" HeaderText="ID" DataField="ID" SortExpression="ID"
                            ItemStyle-Height="30px" Visible="false"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO"
                            ItemStyle-Height="30px"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION"
                            ItemStyle-Height="30px" ItemStyle-Width="70px" HeaderStyle-Width="70px"></asp:BoundField>
            <%--<asp:BoundField ReadOnly="True" HeaderText="Alerta Roja" DataField="ROJO" SortExpression="ROJO"
                            ItemStyle-Height="30px"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Alerta Amarilla" DataField="AMARILLO" SortExpression="AMARILLO"
                            ItemStyle-Height="30px"></asp:BoundField>--%>
            <%--<asp:BoundField ReadOnly="True" HeaderText="Tiempo Verde" DataField="VERDE" SortExpression="VERDE"
                            ItemStyle-Height="30px"></asp:BoundField>--%>
            <asp:BoundField ReadOnly="True" HeaderText="Site" DataField="SITE" SortExpression="SITE"
                            ItemStyle-Height="30px"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Estado Solicitud" DataField="ESTADO_SOLICITUD" SortExpression="ESTADO_SOLICITUD"
                            ItemStyle-Height="30px"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Tipo Zona" DataField="TIPO_ZONA" SortExpression="TIPO_ZONA"
                            ItemStyle-Height="30px"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Tipo Playa" DataField="TIPO_PLAYA" SortExpression="TIPO_PLAYA"
                            ItemStyle-Height="30px"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Tiempo Max (Minutos)" DataField="TIEMPO_MAX" SortExpression="TIEMPO_MAX"
                            ItemStyle-Height="30px"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Color" DataField="COLOR" SortExpression="COLOR"
                            ItemStyle-Height="30px"></asp:BoundField>
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
  </div>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" Runat="Server">
  <!-- modal Playa  ---->
  <div class="modal fade" id="modalTimers" data-backdrop="static" role="dialog">
    <div class="modal-dialog" style="width:800px">
      <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Timer Procesos
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              <div class="col-xs-12">
                <div class="col-xs-2">
                  Código
                  <br />
                  <asp:TextBox ID="txt_editCodigo" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-6">
                  Descripción
                  <br />
                  <asp:TextBox ID="txt_editDesc" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-3">
                  Tiempo Max (Minutos)
                  <br />
                  <asp:TextBox ID="txt_editMins" CssClass="input-number form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                <br />
                  <asp:TextBox ID="txt_editColor" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <div class="col-xs-2">
                  Site
                  <br />
                  <asp:DropDownList ID="ddl_editSite" runat="server" CssClass="form-control">
                  </asp:DropDownList>
                </div>
                <div class="col-xs-4">
                  Estado Solicitud
                  <br />
                  <asp:DropDownList ID="ddl_editEstadoSoli" runat="server" CssClass="form-control">
                  </asp:DropDownList>
                </div>
                <div class="col-xs-3">
                  Tipo Zona
                  <br />
                  <asp:DropDownList ID="ddl_editTipoZona" runat="server" CssClass="form-control">
                  </asp:DropDownList>
                </div>        
                <div class="col-xs-3">
                  Tipo Playa
                  <br />
                  <asp:DropDownList ID="ddl_editTipoPlaya" runat="server" CssClass="form-control">
                    <asp:ListItem Value="0" Text="Seleccione..."></asp:ListItem>
                    <asp:ListItem Value="100" Text="Andén"></asp:ListItem>
                    <asp:ListItem Value="200" Text="Estacionamiento"></asp:ListItem>
                  </asp:DropDownList>
                </div>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="timer"
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
            <asp:RequiredFieldValidator ID="rfv_txt_editCodigo" runat="server" ControlToValidate="txt_editCodigo"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="timer">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editCodigo">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_txt_editDesc" runat="server" ControlToValidate="txt_editDesc"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="timer">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editDesc">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_txt_editMins" runat="server" ControlToValidate="txt_editMins"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="timer">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editMins">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_ddl_editSite" runat="server" ControlToValidate="ddl_editSite"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0"
                                        ValidationGroup="timer">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_ddl_editSite">
            </asp:ValidatorCalloutExtender>
          
            <asp:RequiredFieldValidator ID="rfv_ddl_editTipoZona" runat="server" ControlToValidate="ddl_editTipoZona"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="default"
                                        ValidationGroup="timer">
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
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <asp:HiddenField id="hf_idTimerProcesos" runat="server"/>
      <asp:Button ID="btn_eliminarTimer" OnClick="btn_eliminarTimer_Click" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" Runat="Server">
    <link rel="stylesheet" media="screen" type="text/css" href="../css2/spectrum.css" />
    <script type="text/javascript" src="../js2/spectrum.js"></script>
  <script type="text/javascript">
      function EndRequestHandler(sender, args) {
        $("#<%= txt_editColor.ClientID %>").spectrum({
            change: function (color) {
                $(this).val(color.toHexString());
            }
        });
    }
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
      
    function modalTimers() {
        $("#modalTimers").modal();
    }

    function modalConfirmacion() {
        $("#modalConfirmacion").modal();
    }

    function eliminarTimer() {
        var clickButton = document.getElementById("<%= this.btn_eliminarTimer.ClientID %>");
        clickButton.click();
    }

  </script>
</asp:Content>