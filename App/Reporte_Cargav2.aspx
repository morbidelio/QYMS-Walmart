<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Reporte_Cargav2.aspx.cs" Inherits="App_Reporte_Cargav2" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" Runat="Server">
  <div class="col-xs-12 separador"></div>
  <h2>
    Control de Carga
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" Runat="Server">
 <asp:UpdatePanel runat="server" ID="updbuscar" >
      <ContentTemplate>
  <div class="col-xs-12 separador">
  </div>
  <div class="col-xs-12">
    <asp:Panel ID="SITE" runat="server" >
      <div class="col-xs-1">
        Site
        <br />
        <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" runat="server" onselectedindexchanged="ddl_buscarSite_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
      </div>
    </asp:Panel>

        <div class="col-xs-2">
          Playa
          <br />
          <asp:DropDownList ID="ddl_buscarPlaya" CssClass="form-control" runat="server" onselectedindexchanged="ddl_buscarPlaya_SelectedIndexChanged" AutoPostBack="true">
          </asp:DropDownList>
        </div>
        <div class="col-xs-2">
          Andén
          <br />
          <asp:DropDownList ID="ddl_buscarAnden" CssClass="form-control" runat="server">
          </asp:DropDownList>
        </div>

    <div class="col-xs-1">
      N° Solicitud
      <br />
      <asp:TextBox ID="txt_buscarNumero" CssClass="form-control input-number" runat="server"></asp:TextBox>
    </div>
    <div class="col-xs-2">
      Estado Solicitud
      <br />
      <asp:DropDownList ID="ddl_buscarEstado" CssClass="form-control" runat="server">
      </asp:DropDownList>
    </div>
    <div class="col-xs-2" >
      Transportista
      <br />
      <asp:DropDownList ID="ddl_buscarTransportista" CssClass="form-control" runat="server" >
      </asp:DropDownList>
    </div>
    <div class="col-xs-2">
      <br />
       <asp:LinkButton ID="cargafiltros" OnClick="btn_cargafiltros_Click" CssClass="btn btn-primary" ClientIDMode="Static"
                      ToolTip="cargar filtros" runat="server"  CausesValidation="false">
        <span class="glyphicon glyphicon-plus"></span>
      </asp:LinkButton>
   
      <asp:LinkButton ID="btn_buscarSolicitud" OnClick="btn_buscarSolicitud_Click" CssClass="btn btn-primary" ClientIDMode="Static"
                      ToolTip="Buscar Solicitudes" runat="server"  CausesValidation="false">
        <span class="glyphicon glyphicon-search"></span>
      </asp:LinkButton>
      <asp:LinkButton ID="btn_export"  ToolTip="Exportar a Excel" CssClass="btn btn-primary"  CausesValidation="false"  OnClick="btn_export_Click" runat="server">
        <span class="glyphicon glyphicon-import"></span>
      </asp:LinkButton>
    </div>
  </div>
  
  </ContentTemplate>
    <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btn_buscarSolicitud" EventName="click" />
       <asp:AsyncPostBackTrigger ControlID="cargafiltros" EventName="click" />

    </Triggers>
      </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" Runat="Server">
  <div class="col-xs-12">
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
      <ContentTemplate>
        <asp:GridView ID="gv_listar" runat="server" CellPadding="8" AllowPaging="false" GridLines="Horizontal" DataKeyNames="ID_SOLICITUD,ID_SOLICITUDANDEN,ORDEN" Width="100%"
                      CssClass="table table-bordered table-hover tablita" EmptyDataText="No Existen Movimientos Asignados!" AutoGenerateColumns="false" 
                      OnRowEditing="gv_listar_RowEdit" AllowSorting="True" EnableSortingAndPagingCallbacks="false" OnRowCreated="gv_listar_RowCreated" OnRowDataBound="gv_listar_RowDataBound" OnRowCommand="gv_listar_RowCommand">
          <Columns>
            <asp:BoundField ReadOnly="True" HeaderText="Solicitud" DataField="ID_SOLICITUD" SortExpression="ID_SOLICITUD"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Id Solicitud Anden" DataField="ID_SOLICITUDANDEN" SortExpression="ID_SOLICITUDANDEN" Visible="false"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Orden" DataField="ORDEN" SortExpression="ORDEN"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Andén Carga" DataField="LUGAR" SortExpression="LUGAR"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="jornada" DataField="jornada" SortExpression="jornada"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Estado Solicitud" DataField="ESTADOSOLICITUD" SortExpression="ESTADOSOLICITUD"></asp:BoundField>
            <%--<asp:BoundField ReadOnly="True" HeaderText="Estado Andén" DataField="ESTADOANDEN" SortExpression="ESTADOANDEN"></asp:BoundField>--%>
            <asp:BoundField ReadOnly="True" HeaderText="Fecha Creación" DataField="SOLI_FH_CREACION" SortExpression="SOLI_FH_CREACION"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Usuario" DataField="Usuario" SortExpression="Usuario"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Id Estado Andén" DataField="ID_ESTADOANDEN" SortExpression="ID_ESTADOANDEN" Visible="false"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Id Estado Solicitud" DataField="ID_ESTADOSOLICITUD" SortExpression="ID_ESTADOSOLICITUD" Visible="false"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Fecha Andén" DataField="FECHA_RESERVA_ANDEN" SortExpression="FECHA_RESERVA_ANDEN"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Fecha Cumplimiento" DataField="FECHA_PUESTA_ANDEN" SortExpression="FECHA_PUESTA_ANDEN"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Tiempo en carga" DataField="TIEMPO_CARGA" SortExpression="TIEMPO_CARGA"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Local" DataField="LOCALES" SortExpression="LOCALES"></asp:BoundField>
       
            <asp:BoundField ReadOnly="True" HeaderText="Regiones" DataField="REGIONES" SortExpression="REGIONES"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Transporte" DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="N° Flota" DataField="NRO_FLOTA" SortExpression="NRO_FLOTA"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Trailer Patente" DataField="PATENTE" SortExpression="PATENTE"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="ID Shortec" DataField="ID_SHORTEK" SortExpression="ID_SHORTEK"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Plancha" DataField="Plancha" SortExpression="Plancha"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Playa" DataField="PLAYA" SortExpression="PLAYA" Visible="false"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Id Lugar" DataField="ID_LUGAR" SortExpression="ID_LUGAR" Visible="false"></asp:BoundField>

            <asp:BoundField ReadOnly="True" HeaderText="Tipo Carga" DataField="FRIO" SortExpression="FRIO"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Temp" DataField="TEMPERATURA" SortExpression="TEMPERATURA"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="ID Ruta Shortec" DataField="SOLI_RUTA" SortExpression="SOLI_RUTA"></asp:BoundField>

            <asp:TemplateField HeaderText="Cargado" ShowHeader="False" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" Visible="false"
                               HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_cargado" runat="server" CausesValidation="True" CommandName="Cargado"
                                CssClass="btn btn-primary" ToolTip="Completar Carga" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-check"></span>
                </asp:LinkButton>

                <asp:LinkButton ID="btn_sello" runat="server" CausesValidation="True" CommandName="colocar_sello" Visible="false"
                                CssClass="btn btn-primary" ToolTip="Sello" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-arrow-down"></span>
                </asp:LinkButton>

                <asp:LinkButton ID="btn_valida_sello" runat="server" CausesValidation="True" CommandName="validar_sello"  Visible="false"
                                CssClass="btn btn-primary" ToolTip="A Estacionamiento" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-arrow-up"></span>
                </asp:LinkButton>

              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Carga Parcial" ShowHeader="False" ItemStyle-Width="5%"  ItemStyle-HorizontalAlign="Center" Visible="false"
                               HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_cargaParcial" runat="server" CausesValidation="False" CommandName="Parcial"
                                CssClass="btn btn-primary" ToolTip="Interrumpir Carga" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-pause"></span>
                </asp:LinkButton>
                <asp:LinkButton ID="btn_cargaContinuar" Visible="false" runat="server" CausesValidation="False" CommandName="Continuar"
                                CssClass="btn btn-primary" ToolTip="Continuar Carga" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-play"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Editar" ShowHeader="False" ItemStyle-Width="5%" Visible="false" ItemStyle-HorizontalAlign="Center"
                               HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_editar" runat="server" CausesValidation="True" CommandName="Edit"
                                CssClass="btn btn-primary" ToolTip="Modificar" Text="Modificar" CommandArgument='<%# Eval("ID_SOLICITUD") + ";" + Eval("ID_LUGAR")+ ";" + Eval("orden") %>'>
                  <span class="glyphicon glyphicon-pencil"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
          </Columns>
        <HeaderStyle CssClass="header-color2" />
        </asp:GridView>
      </ContentTemplate>
    </asp:UpdatePanel>

  </div>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" Runat="Server">
  <!-- Modal Carga completa / parcial-->
  <div id="modalCarga" class="modal fade" data-backdrop="static" role="dialog">
    <div class="modal-dialog">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <!-- Modal content-->
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title">
                Carga andén
              </h4>
            </div>
            <div class="modal-body" style="height: auto; width: auto; overflow-y: auto;">
              <div class="col-xs-12">
                <div id="dv_pallets" runat="server" class="col-xs-4">
                  Pallets cargados
                  <br />
                  <asp:TextBox ID="txt_palletsCargados" CssClass="form-control" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfv_pallets" runat="server" ControlToValidate="txt_palletsCargados"
                                              Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                              ValidationGroup="carga">
                  </asp:RequiredFieldValidator>
                  <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                Enabled="True" TargetControlID="rfv_pallets">
                  </asp:ValidatorCalloutExtender>
                </div>
                <div class="col-xs-4">
                  Fecha
                  <br />
                  <asp:TextBox ID="txt_fechaCarga" CssClass="form-control input-fecha" runat="server" Enabled="false"></asp:TextBox>
                  <%--<asp:CalendarExtender ID="txt_Fecha_CalendarExtender" runat="server" Enabled="True"
                  TargetControlID="txt_fechaCarga" Format="dd/MM/yyyy">
                  </asp:CalendarExtender>--%>
                  <asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="txt_fechaCarga"
                                              Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                              ValidationGroup="carga">
                  </asp:RequiredFieldValidator>
                  <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                Enabled="True" TargetControlID="rfv_fecha">
                  </asp:ValidatorCalloutExtender>
                </div>
                <div class="col-xs-4">
                  Hora
                  <br />
                  <asp:TextBox ID="txt_horaCarga" CssClass="form-control input-hora" runat="server" Enabled="false"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfv_hora" runat="server" ControlToValidate="txt_palletsCargados"
                                              Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                              ValidationGroup="carga">
                  </asp:RequiredFieldValidator>
                  <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                Enabled="True" TargetControlID="rfv_hora">
                  </asp:ValidatorCalloutExtender>
                </div>
              </div>
              <div class="col-xs-12 separador"></div>
              <div class="col-xs-12">
                <asp:LinkButton ID="btn_terminarCarga" runat="server" OnClick="btn_terminarCarga_Click" ValidationGroup="carga"
                                CssClass="btn btn-primary">
                  <span class="glyphicon glyphicon-floppy-disk"></span>
                </asp:LinkButton>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-default" data-dismiss="modal">
                Cerrar</button>
            </div>
          </div>
        </ContentTemplate>
        <Triggers>
          <asp:PostBackTrigger ControlID="btn_terminarCarga" />
        </Triggers>
      </asp:UpdatePanel>
    </div>
  </div>
  <!-- Modal reanudar carga -->
  <div id="modalReanudar" class="modal fade" data-backdrop="static" role="dialog">
    <div class="modal-dialog" style="width:1200px">
      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
          <!-- Modal content-->
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title">
                Reanudar Carga
              </h4>
            </div>
            <div class="modal-body" style="height: auto; width: auto; overflow-y: auto;">
              
              <div class="col-xs-12">
                <div class="col-lg-2 col-xs-3">
                  Local
                  <br />
                  <asp:TextBox runat="server" ID="txt_buscaLocal" OnTextChanged="txt_buscaLocal_TextChanged"
                               CssClass="input-number form-control" AutoPostBack="true" Width="90%"></asp:TextBox>
                </div>
                <div class="col-lg-4 col-xs-8">
                  <br />
                  <asp:TextBox runat="server" CssClass="form-control" ID="txt_descLocal" ></asp:TextBox>
                </div>
                <div class="col-lg-4 col-xs-6">
                  Andén
                  <br />
                  <asp:DropDownList ID="ddl_origenAnden" CssClass="form-control" runat="server" >
                    <asp:ListItem Selected="True" Value="0" Text="Seleccione..."></asp:ListItem>
                  </asp:DropDownList>
                </div>
                <div class="col-xs-2">
                  <br />
                  <asp:LinkButton ID="btn_agregarCarga" CssClass="btn btn-primary" ToolTip="Nuevo Local" ValidationGroup="nuevaCarga"
                                  runat="server" OnClick="btn_agregarCarga_Click">
                    <span class="glyphicon glyphicon-plus"></span>
                  </asp:LinkButton>
                </div>
              </div>
              <div class="col-xs-12 separador"></div>
              <div class="col-xs-12 container-fluid" style="overflow:auto;min-height:100px;max-height:200px;">
         
                <asp:GridView ID="gv_solLocales" runat="server" CellPadding="8" GridLines="Horizontal" OnRowCommand="gv_solLocales_RowCommand" OnRowDataBound="gv_solLocales_rowDataBound"
                              CssClass="table table-bordered table-hover tablita" EmptyDataText="No Existen Andenes/Locales Asignados!" AutoGenerateColumns="false"
                              Width="100%" AllowSorting="True" EnableSortingAndPagingCallbacks="false">
                  <Columns>
                    <asp:TemplateField ShowHeader="false">
                      <ItemTemplate>
                        <asp:LinkButton ID="btn_eliminarLocal" CausesValidation="False" CssClass="btn btn-sm btn-primary" CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="ELIMINAR" ToolTip="Eliminar" runat="server">
                          <span class="glyphicon glyphicon-erase"></span>
                        </asp:LinkButton>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="false">
                      <ItemTemplate>
                        <asp:LinkButton ID="btnSubir" CausesValidation="False" CssClass="btn btn-sm btn-primary" CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="SUBIR" runat="server">
                          <span class="glyphicon glyphicon-menu-up"></span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnBajar" CausesValidation="False" CssClass="btn btn-sm btn-primary" CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="BAJAR" runat="server">
                          <span class="glyphicon glyphicon-menu-down"></span>
                        </asp:LinkButton>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ReadOnly="True" HeaderText="Id Andén" DataField="ID_ANDEN" SortExpression="ID_ANDEN" Visible="false"></asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="id Local" DataField="ID_LOCAL" SortExpression="ID_LOCAL" Visible="false"></asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Orden" DataField="ORDEN" SortExpression="ORDEN" Visible="true"></asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="N° Local" DataField="numero_LOCAL" SortExpression="numero_LOCAL" Visible="true"></asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Nombre Local" DataField="NOMBRE_LOCAL" SortExpression="NOMBRE_LOCAL"></asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Andén" DataField="ANDEN" SortExpression="ANDEN"></asp:BoundField>
                    <asp:BoundField ReadOnly="True" HeaderText="Id Solicitud" DataField="SOES_ID" SortExpression="SOES_ID" Visible="false"></asp:BoundField>
                  </Columns>
                </asp:GridView>
                 
              </div>
              <div class="col-xs-12">
                <center>
                <asp:LinkButton ID="btn_reanudar" runat="server" OnClick="btn_reanudar_Click"
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
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
  <div class="modal fade" id="modalConfirmacion" data-backdrop="static" role="dialog" >
    <div class="modal-dialog modal-sm" role="dialog">
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                <asp:Label ID="lblRazonConfirmacion" runat="server"></asp:Label>
              </h4>
            </div>
            <div class="modal-body">
              <asp:Label ID="msjConfirmacion" runat="server"></asp:Label>
            </div>
            <div class="modal-footer">
              <asp:LinkButton ID="btnModalConfirmacion" runat="server" data-dismiss="modal"
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
      <asp:HiddenField ID="hf_idLugar" runat="server" />
      <asp:HiddenField ID="hf_idSolicitud" runat="server" />
      <asp:HiddenField ID="hf_idEstado" runat="server" />
      <asp:HiddenField ID="hf_orden" runat="server" />
      <asp:HiddenField ID="hf_localesSeleccionados" Value="" runat="server" />
      <asp:HiddenField ID="hf_localesCompatibles" Value="" runat="server" />
      <asp:HiddenField ID="hf_caractSolicitud" Value="" runat="server" />
      <asp:HiddenField ID="hf_timeStamp" Value="" runat="server" />
      <asp:Button ID="btnExportar" runat="server" CssClass="ocultar" OnClick="btnExportar_Click" />
    </ContentTemplate>
    <Triggers >
      <asp:PostBackTrigger ControlID="btnExportar"  />
    </Triggers>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" Runat="Server">
  <script type="text/javascript">
      var calcDataTableHeight = function () {
          return $(window).height() - $("#<%= this.gv_listar.ClientID %>").offset().top - 80;
      };

      function reOffset1() {

          $('div.dataTables_scrollBody').height(calcDataTableHeight());
      }

      window.onresize = function (e) {
          reOffset1();
      }

      function tabla() {
          if ($('#<%= this.gv_listar.ClientID %>')[0] != undefined && $('#<%= this.gv_listar.ClientID %>')[0].rows.length > 1) {
              $('#<%= this.gv_listar.ClientID %>').DataTable({
                  "scrollY": calcDataTableHeight(),
                  "scrollX": true,
                  "scrollCollapse": true,
                  "paging": false,
                  "ordering": false,
                  "searching": false,
                  "lengthChange": false,
                  "info":false
              });
          }
      }
      function EndRequestHandler1(sender, args) {
          setTimeout(tabla, 100);
      }
      Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);

    

    function Exportar() {
        $get("<%=this.btnExportar.ClientID %>").click();
    }

    function modalCarga() {
        $("#modalCarga").modal();
    }

    function modalReanudar() {
        $("#modalReanudar").modal();
    }

    var tick_recarga;

    $(document).ready(function (e) {
        setTimeout(tabla, 100);

        clearInterval(tick_recarga);
        tick_recarga = setInterval(click_recarga, 120000);

    });




    function click_recarga() {

        if (($('.modal:visible').length && $('body').hasClass('modal-open')) != true)
            $('#btn_buscarSolicitud')[0].click();

    }

  </script>
</asp:Content>