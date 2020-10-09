<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Local.aspx.cs" Inherits="reporte_App_Local" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Maestro Local
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  
  <div class="col-xs-1">
    Código
    <br />
    <asp:TextBox ID="txt_buscarCodigo" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
  </div>
  <div class="col-xs-3">
    Descripción
    <br />
    <asp:TextBox ID="txt_buscarDescripcion" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
  </div>
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <div class="col-xs-2">
        Región
        <br />
        <asp:DropDownList ID="ddl_buscarRegion" OnSelectedIndexChanged="ddl_buscarRegion_IndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
          <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
        </asp:DropDownList>
      </div>
      <div class="col-xs-2">
        Comuna
        <br />
        <asp:DropDownList ID="ddl_buscarComuna" CssClass="form-control" runat="server">
          <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
        </asp:DropDownList>
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
  <div class="col-xs-1">
    <br />
    <asp:LinkButton ID="btn_buscar" OnClick="btn_buscarLocal_Click" CssClass="btn btn-primary"
                    ToolTip="Buscar Local" runat="server">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
    <asp:LinkButton ID="btn_nuevoLocal" CssClass="btn btn-primary" ToolTip="Nuevo Local" Visible="false"
                    runat="server" OnClick="btn_nuevoLocal_Click">
      <span class="glyphicon glyphicon-plus"></span>
    </asp:LinkButton>
     <asp:LinkButton id="btn_exportar" CssClass="btn btn-primary" OnClick="btn_export_Click" runat="server">
        <span class="glyphicon glyphicon-import"></span>
      </asp:LinkButton>
  </div>
   
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      
      <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" PageSize="8" AutoGenerateColumns="False" EnableViewState="false" DataKeyNames="ID" 
                    OnPageIndexChanging="gv_listar_PageIndexChanging" OnRowCommand="gv_listaLocal_RowCommand" OnSorting="gv_listaLocal_Sorting" OnRowEditing="gv_listaLocal_RowEditing"
                    Width="100%" CssClass="table table-bordered table-hover tablita" EmptyDataText="No se encontraron locales!" >
        <Columns>
          <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%" Visible="false">
            <ItemTemplate>
              <asp:LinkButton ID="btn_modificarLocal" runat="server" CommandName="Edit" 
                              CssClass="btn btn-sm btn-primary" ToolTip="Modificar">
                <span class="glyphicon glyphicon-pencil"></span>
              </asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
          </asp:TemplateField>
          <asp:TemplateField ShowHeader="False" ItemStyle-Width="1%" Visible="false">
            <ItemTemplate>
              <asp:LinkButton ID="btn_eliminarLocal" runat="server" CommandName="ELIMINAR" CommandArgument='<%# Eval("ID") %>'
                              CssClass="btn btn-sm btn-primary" ToolTip="Eliminar">
                <span class="glyphicon glyphicon-remove"></span>
              </asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
          </asp:TemplateField>
          <asp:BoundField ReadOnly="True" DataField="ID" Visible="false"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Región" DataField="REGION" SortExpression="REGION"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Comuna" DataField="COMUNA" SortExpression="COMUNA"></asp:BoundField>                    
          <asp:BoundField ReadOnly="True" HeaderText="Dirección" DataField="DIRECCION" SortExpression="DIRECCION"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Acepta internos" DataField="internos" SortExpression="internos"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Acepta externos" DataField="externos" SortExpression="externos"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Tamaño" DataField="tamano" SortExpression="tamano"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Pallet U" DataField="palletu" SortExpression="palletu"></asp:BoundField>
          <asp:BoundField ReadOnly="True" HeaderText="Plancha" DataField="Plancha" SortExpression="Plancha"></asp:BoundField>
             <asp:BoundField ReadOnly="True" HeaderText="Tipo Trailer " DataField="TipoTrailer" SortExpression="TipoTrailer"></asp:BoundField>
                  <asp:BoundField ReadOnly="True" HeaderText="Es alto" DataField="alto" SortExpression="alto"></asp:BoundField>
             
        </Columns>
          <PagerStyle CssClass="pagination-ys" />
      </asp:GridView>
       
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" Runat="Server">
  <!-- modal Local  ---->
  <div class="modal fade" id="modalLocal" data-backdrop="static" role="dialog">
    <div class="modal-dialog" style="width:900px;">
      <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Local
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              
              <div class="col-xs-2">
                Código
                <br />
                <asp:TextBox ID="txt_editCodigo" CssClass="form-control input-mayus" runat="server"></asp:TextBox>
              </div>
              <div class="col-xs-5">
                Descripción
                <br />
                <asp:TextBox ID="txt_editDesc" CssClass="form-control input-mayus" runat="server"></asp:TextBox>
              </div>
              <div class="col-xs-2">
                Acepta internos
                <br />
                <asp:CheckBox ID="chk_editInternos" runat="server" />
              </div>
              <div class="col-xs-2">
                Acepta externos
                <br />
                <asp:CheckBox ID="chk_editExternos" runat="server" />
              </div>
              <div class="col-xs-12 separador"></div>
              <asp:UpdatePanel runat="server">
                <ContentTemplate>
                  <div class="col-xs-3">
                    Región
                    <br />
                    <asp:DropDownList ID="ddl_editRegion" OnSelectedIndexChanged="ddl_editRegion_IndexChanged" 
                                      AutoPostBack="true" runat="server" CssClass="form-control">
                      <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                    </asp:DropDownList>
                  </div>
                  <div class="col-xs-3">
                    Comuna
                    <br />
                    <asp:DropDownList ID="ddl_editComuna" Enabled="false" runat="server" CssClass="form-control">
                      <asp:ListItem Text="Seleccione..." Value="0"></asp:ListItem>
                    </asp:DropDownList>
                  </div>
                </ContentTemplate>
              </asp:UpdatePanel>
              <div class="col-xs-4">
                Dirección
                <br />
                <asp:TextBox ID="txt_editDireccion" CssClass="form-control" runat="server"></asp:TextBox>
              </div>
               
              <div class="col-xs-12 separador"></div>
              <%--<div class="col-lg-12 separador"></div>--%>
              
              <asp:Panel ID="pnl_contenidoCaract" runat="server">
              </asp:Panel>
              <asp:Literal ID="ltl_contenidoCaract" runat="server"></asp:Literal>
               
              <div class="col-lg-12 separador"></div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClientClick="generarHiddenField();" OnClick="btn_editGrabar_Click" ValidationGroup="nuevoLocal"
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
            <asp:RequiredFieldValidator ID="rfv_codigo" runat="server" ControlToValidate="txt_editCodigo"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoLocal">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_codigo">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_txt_editDesc" runat="server" ControlToValidate="txt_editDesc"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoLocal">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editDesc">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_txt_editDireccion" runat="server" ControlToValidate="txt_editDireccion"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoLocal">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editDireccion">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_ddl_editComuna" runat="server" ControlToValidate="ddl_editComuna"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" InitialValue="0"
                                        ValidationGroup="nuevoLocal">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_ddl_editComuna">
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
                Eliminar Local
              </h4>
            </div>
            <div class="modal-body">
              Se eliminará el local seleccionado, ¿desea continuar?
            </div>
            <div class="modal-footer">
              <asp:LinkButton ID="btnModalEliminar" runat="server"
                              CssClass="btn btn-primary" OnClick="btn_EliminarLocal_Click" >
                <span class="glyphicon glyphicon-ok">

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
      <asp:HiddenField ID="hf_idLocal" runat="server" />
      <asp:HiddenField ID="hf_caracteristicasLocal" Value="" runat="server" />
      <asp:HiddenField ID="hf_excluyentes" Value="" runat="server" />
      <asp:HiddenField ID="hf_noexcluyentes" Value="" runat="server" />
      <asp:Button ID="btnExportar" runat="server" CssClass="ocultar" OnClick="btnExportar_Click" />
    </ContentTemplate>
    <Triggers >
      <asp:PostBackTrigger ControlID="btnExportar"  />
    </Triggers>
  </asp:UpdatePanel>
</asp:Content>
    <asp:Content ID="Scripts" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
    function modalEditarLocal() {
        $("#modalLocal").modal();
    }

    function modalConfirmacion() {
        $("#modalConfirmacion").modal();
    }
    
    function generarHiddenField() {
        var checks = document.getElementsByClassName("check");
        var drops = document.getElementsByClassName("drop");
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
        $("#<% =hf_excluyentes.ClientID %>").val(xx);
        $("#<% =hf_noexcluyentes.ClientID %>").val(zz);
    }
    
    function llenarForm() {
        limpiarForm();
        var ex = $("#<% =hf_excluyentes.ClientID %>").val();
        var no_ex = $("#<% =hf_noexcluyentes.ClientID %>").val();
        var x = ex.split(',');
        var y = no_ex.split(',');
        for (var i = 0; i < x.length; i++) {
            $("#caractTipo_" + x[i]).prop('checked', true);
        }
        for (var i = 0; i < y.length; i++) {
            $("#op_drop_" + y[i]).prop('selected', true);
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

    function Exportar() {
        $get("<%=this.btnExportar.ClientID %>").click();
    }

  </script>
</asp:Content>