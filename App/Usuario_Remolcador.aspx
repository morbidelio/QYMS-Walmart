<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Usuario_Remolcador.aspx.cs" Inherits="App_Usuario_Remolcador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Asignación Tareas Ottawa
  </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  
  <asp:Panel ID="SITE" runat="server" >
    <div class="col-xs-2">
      Site
      <br />
      <asp:DropDownList runat="server" id="dropsite" ClientIDMode="Static" AutoPostBack="true" 
                        OnSelectedIndexChanged="drop_SelectedIndexChanged" cssclass="form-control"></asp:DropDownList>
    </div>
  </asp:Panel>
  <div class="col-xs-1">
    <br />
    <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary"
                    ToolTip="Buscar" runat="server">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
    <asp:LinkButton ID="btn_nuevo" CssClass="btn btn-primary" ToolTip="Nueva asociación"
                    runat="server" OnClick="btn_nuevo_Click">
      <span class="glyphicon glyphicon-plus"></span>
    </asp:LinkButton>        
  </div>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <div class="col-xs-12" style="overflow-x: auto;">
  <asp:UpdatePanel runat="server" ID="up_grilla"><ContentTemplate>
    <asp:GridView ID="gv_listar" AllowSorting="true" AutoGenerateColumns="false" EnableViewState="false" EmptyDataText="No se encontraron asociaciones usuario/remolcador" 
                  OnRowCommand="gv_listar_RowCommand" OnRowDataBound="gv_listar_RowDataBound" CssClass="table table-bordered table-hover tablita" Width="100%" runat="server">
      <Columns>
        <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
          <ItemTemplate>
            <asp:LinkButton ID="btnEditar" runat="server" CausesValidation="False" CommandName="EDITAR"
                            CommandArgument='<%# Eval("REMO_ID") %>' CssClass="btn btn-sm btn-primary" ToolTip="Eliminar">
              <span class="glyphicon glyphicon-pencil"></span>
            </asp:LinkButton>
          </ItemTemplate>
          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        </asp:TemplateField>
        <asp:BoundField DataField="REMO_ID" />
        <asp:BoundField DataField="REMO_COD" HeaderText="Código" />
        <asp:BoundField DataField="REMOLCADOR" HeaderText="Remolcador" />
        <asp:BoundField DataField="ITINERARIO_AYER" HeaderText="Ayer" />
        <asp:BoundField DataField="ITINERARIO_HOY" HeaderText="Hoy" />
        <asp:BoundField DataField="ITINERARIO_MAÑANA" HeaderText="Dos Días" />
        <asp:BoundField DataField="ITINERARIO_3dias" HeaderText="Tres Días" />
        <asp:BoundField DataField="ITINERARIO_4dias" HeaderText="Cuatro Días" />
        <asp:BoundField DataField="ITINERARIO_5dias" HeaderText="Cinco Días" Visible="false"  />
        <asp:BoundField DataField="ITINERARIO_6dias" HeaderText="Seís Días" Visible="false"  />
        <asp:BoundField DataField="ITINERARIO_7dias" HeaderText="Siete Días " Visible="false" />
      </Columns>
          <PagerStyle CssClass="pagination-ys" />
    </asp:GridView>
    </ContentTemplate></asp:UpdatePanel>
  </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
  <div class="modal fade" id="modalUsuarioRemolcador" data-backdrop="static" role="dialog">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="always" >
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Usuario / Remolcador
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              <div class="col-xs-12">
                <asp:Panel ID="pnl_crearSite" runat="server" >
                  <div class="col-xs-4">
                    Site
                    <br />
                    <asp:DropDownList runat="server" id="ddl_siteEdit" CssClass="form-control" AutoPostBack="true" 
                                      OnSelectedIndexChanged="ddl_siteEdit_IndexChanged" ></asp:DropDownList>
                  </div>
                </asp:Panel>
                <div class="col-xs-4">
                  Remolcador
                  <br />
                  <asp:DropDownList id="ddl_remoEdit" CssClass="form-control" runat="server">
                  </asp:DropDownList>
                </div>
              </div>
              <div class="col-xs-12 separador"></div>
              <div class="col-xs-12" style="border:solid 2px;border-radius:5px; padding-top:5px;padding-bottom:5px;">
                <div class="col-xs-2">
                  Fecha
                  <br />
                  <asp:TextBox ID="txt_fechaEdit" CssClass="form-control" runat="server" />
                </div>
                <div class="col-xs-3">
                  Usuario
                  <br />
                  <asp:DropDownList ID="ddl_usuaEdit" CssClass="form-control" runat="server">
                  </asp:DropDownList>
                </div>
                <div class="col-xs-3">
                  Jornada
                  <br />
                  <asp:DropDownList ID="ddl_jornadaEdit" CssClass="form-control" runat="server">
                  </asp:DropDownList>
                </div>
                <div class="col-xs-3">
                  Programación
                  <br />
                  <asp:DropDownList ID="ddl_programacionEdit" CssClass="form-control" runat="server">
                  </asp:DropDownList>
                </div>
                <div class="col-xs-1">
                  <br />
                  <asp:LinkButton ID="btn_agregarEdit" CssClass="btn btn-primary" ToolTip="Agregar jornada"
                                  runat="server" OnClick="btn_agregarEdit_Click" ValidationGroup="detalle">
                    <span class="glyphicon glyphicon-plus"></span>
                  </asp:LinkButton>
                </div>
                <div class="col-xs-12 separador"></div>
                <div class="col-xs-12">
                  <asp:GridView ID="gv_jornadaUsuario" EmptyDataText="No se han asociado usuarios/jornadas" width="100%" UseAccessibleHeader="true"
                                OnRowCommand="gv_jornadaUsuario_RowCommand" CssClass="table table-hover table-bordered tablita" 
                                OnRowDataBound="gv_jornadaUsuario_RowDataBound" GridLines="Horizontal" EnableSortingAndPagingCallbacks="True"
                                AutoGenerateColumns="false" runat="server">
                    <Columns>
                      <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                      <asp:BoundField DataField="USUA_ID" Visible="false" />
                      <asp:BoundField DataField="USUARIO" HeaderText="Usuario" />
                      <asp:BoundField DataField="JORN_ID" Visible="false" />
                      <asp:BoundField DataField="JORNADA" HeaderText="Jornada" />
                      <asp:BoundField DataField="REPD_ID" Visible="false" />
                      <asp:BoundField DataField="PROGRAMACION" HeaderText="Programación" />
                      <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%">
                        <ItemTemplate>
                          <asp:LinkButton ID="btn_eliminarRemolcador" runat="server" CausesValidation="False" CommandName="ELIMINAR" CommandArgument='<%# Container.DataItemIndex %>'
                                          CssClass="btn btn-primary" ToolTip="Eliminar">
                            <span class="glyphicon glyphicon-remove"></span>
                          </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                      </asp:TemplateField>
                    </Columns>
                  </asp:GridView>
                </div>
              </div>
              <div class="col-xs-12 separador"></div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click"
                                  CssClass="btn btn-primary" ValidationGroup="guardar">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-primary" data-dismiss="modal">
                <span class="glyphicon glyphicon-remove"></span></button>
            </div>
            <asp:RequiredFieldValidator ID="rfv_usua" runat="server" ControlToValidate="ddl_usuaEdit" InitialValue="0"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="detalle">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="vce_usua" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_usua">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_remo1" runat="server" ControlToValidate="ddl_remoEdit" InitialValue="0"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="detalle">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="vce_remo1" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_remo1">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_jornada" runat="server" ControlToValidate="ddl_jornadaEdit" InitialValue="0"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="detalle">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="vce_jornada" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_jornada">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_remo2" runat="server" ControlToValidate="ddl_remoEdit" InitialValue="0"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="guardar">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="vce_remo2" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_remo2">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_prog" runat="server" ControlToValidate="ddl_programacionEdit" InitialValue="0"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="detalle">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="vce_prog" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_prog">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="txt_fechaEdit"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="detalle">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="vce_fecha" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_fecha">
            </asp:ValidatorCalloutExtender>

          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
  </div>
  <div class="modal fade" id="modalBorrarAsoc" data-backdrop="static" role="dialog" >
    <div class="modal-dialog" role="dialog">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                Advertencia
              </h4>
            </div>
            <div class="modal-body">
              Al cambiar este campo todas las asociaciones se eliminarán, ¿está seguro?
            </div>
            <div class="modal-footer">
              <asp:LinkButton ID="btn_Conf" OnClick="btn_Conf_Click" runat="server" CssClass="btn btn-primary" Visible="true"  >
                Continuar
              </asp:LinkButton>
              <asp:LinkButton ID="btn_Canc" OnClick="btn_Canc_Click" runat="server" CssClass="btn btn-primary">
                Cancelar
              </asp:LinkButton>
            </div>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" Runat="Server">
  <asp:UpdatePanel runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hf_idsite" runat="server" />
      <asp:HiddenField ID="hf_idremo" runat="server" />
      <asp:HiddenField ID="hf_eliminados" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript" src="../js2/jquery.dataTables.min.js"></script>
  <link rel="stylesheet" href="../js2/css/defaultTheme.css" media="screen" />
  <script type="text/javascript">
    $.datetimepicker.setLocale('es'); /*cambia el lenguaje predeterminado del calendario a español*/
    /*Funcion para los calendarios dentro de un updatepanel*/
    function EndRequestHandler(sender, args) {
        $('#<%= txt_fechaEdit.ClientID %>').datetimepicker({ dayofweekstart: 1, format: "d-m-Y", weeks: true, timepicker: false });
//        tabla();
        setTimeout(tabla, 200);
    }
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);

    function tabla() {
        if ($('#<%= gv_jornadaUsuario.ClientID %>')[0] != undefined && $('#<%= gv_jornadaUsuario.ClientID %>')[0].rows.length > 1)
            $('#<%= gv_jornadaUsuario.ClientID %>').DataTable({
                "scrollY": "220px",
                "scrollCollapse": true,
                "paging": false,
                "ordering": true,
                "searching": false,
                "lengthChange": false
            });
    }

    function modalUsuarioRemolcador() {
        $("#modalUsuarioRemolcador").modal();
    }
    function modalBorrarAsoc() {
        $("#modalUsuarioRemolcador").modal('hide');
        $("#modalBorrarAsoc").modal();
    }
    function cerrarBorrarAsoc() {
        $("#modalBorrarAsoc").modal('hide');
        $("#modalUsuarioRemolcador").modal();
    }
  </script>
</asp:Content>