﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
CodeFile="Trailer_Bloqueo.aspx.cs" Inherits="App_Trailer_Bloqueo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <h2>
    Bloqueo Trailer
  </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
  <div class="col-xs-12 separador">
  </div>
  <div class="col-xs-12">
  <asp:panel ID="site" runat="server">
    <div class="col-xs-2">
      Site
      <br />
      <asp:DropDownList ID="ddl_site" CssClass="form-control" runat="server">
      </asp:DropDownList>
    </div>
    </asp:panel>
    <div class="col-xs-2">
      Tipo Transporte
      <br />
      <asp:DropDownList ID="ddl_buscarTipo" CssClass="form-control" runat="server">
      </asp:DropDownList>
    </div>
    <div class="col-xs-2">
      Transportista
      <br />
      <asp:DropDownList ID="ddl_buscarTransportista" CssClass="form-control" runat="server">
      </asp:DropDownList>
    </div>
    <div class="col-xs-1">
      N° Flota
      <br />
      <asp:TextBox ID="txt_buscarNro" runat="server" CssClass="form-control" Width="90%"></asp:TextBox>
    </div>
    <div class="col-xs-1">
      Placa
      <br />
      <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" Width="80%"></asp:TextBox>
    </div>
    <div class="col-xs-1">
      Solo internos
      <br />
      <asp:CheckBox ID="chk_buscarInterno" runat="server" />
    </div>
    <div class="col-xs-2">
      Descripcion
      <br />
      <asp:DropDownList ID="ddl_buscarMotivo" CssClass="form-control" runat="server">
      </asp:DropDownList>
    </div>
    <div class="col-xs-1">
      <br />
      <asp:LinkButton ID="btn_buscarTrailer" OnClick="btn_buscarTrailer_Click" CssClass="btn btn-primary"
                      ToolTip="Buscar Trailer" runat="server">
        <span class="glyphicon glyphicon-search"></span>
      </asp:LinkButton>
      <asp:LinkButton ID="btn_nuevoTrailer" CssClass="btn btn-primary" ToolTip="Nuevo Trailer"
                      Visible="false" runat="server" OnClick="btn_nuevoTrailer_Click">
        <span class="glyphicon glyphicon-plus"></span>
      </asp:LinkButton>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="col-xs-12" style="max-height: 60vh; min-height: 60vh; padding-left: 0px;
           padding-right: 0px;">
        <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True"
                      BorderColor="Black" CellPadding="8" DataKeyNames="ID" BackColor="White" GridLines="Horizontal"
                      EnableSortingAndPagingCallbacks="True" OnPageIndexChanging="gv_listaTrailer_PageIndexChanging1"
                      OnRowCommand="gv_listaTrailer_RowCommand" OnRowEditing="gv_listaTrailer_RowEditing"
                      PageSize="6" OnSorting="gv_listaTrailer_Sorting" EmptyDataText="No se encontraron trailers!"
                      AutoGenerateColumns="False" Width="100%" PagerSettings-Mode="NumericFirstLast"
                      CssClass="table table-bordered table-hover tablita" OnRowDataBound="gv_listaTrailer_RowDataBound">
          <Columns>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%" Visible="true" ItemStyle-HorizontalAlign="Center"
                               HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_bloquear" runat="server" CausesValidation="False"
                                CommandName="BLOQUEAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-sm btn-primary"
                                ToolTip="Bloquear">
                  <span class="glyphicon glyphicon-lock"></span>
                </asp:LinkButton>
                <asp:LinkButton ID="btn_desbloquear" runat="server" CausesValidation="False" oolTip="Actualizar Playa"
                                CommandName="DESBLOQUEAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-sm btn-primary"
                                ToolTip="Desbloquear">
                  <span class="glyphicon glyphicon-eject"></span>
                </asp:LinkButton>

                  <asp:LinkButton ID="btn_desbloquear_nomov" runat="server" CausesValidation="False" ToolTip="sin mover"
                                CommandName="DESBLOQUEAR_nomov" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-sm btn-primary"
                                >
                  <span class="glyphicon glyphicon-eject"></span>
                </asp:LinkButton>

              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%" Visible="true" ItemStyle-HorizontalAlign="Center"
                               HeaderStyle-HorizontalAlign="Center">
              <ItemTemplate>
                <asp:LinkButton ID="btn_continuar" runat="server" CausesValidation="False"
                                CommandName="CONTINUAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-sm btn-primary"
                                ToolTip="Continuar">
                  <span class="glyphicon glyphicon-play"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                               HeaderStyle-HorizontalAlign="Center" Visible="false">
              <ItemTemplate>
                <asp:LinkButton ID="btn_mover" runat="server" CausesValidation="False"
                                CommandName="MOVER" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-sm btn-primary"
                                ToolTip="Mover Trailer">
                  <span class="glyphicon glyphicon-transfer"></span>
                </asp:LinkButton>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="ID" SortExpression="ID"
                            ItemStyle-Height="30px" Visible="false"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO" Visible="false"
                            ItemStyle-Width="10%"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Placa" DataField="PLACA" SortExpression="PLACA"
                            ItemStyle-Width="20%" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Externo" DataField="EXTERNO" SortExpression="EXTERNO" Visible="false"
                            ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Número" DataField="NUMERO" SortExpression="NUMERO"
                            ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Transportista" DataField="TRANSPORTISTA"
                            SortExpression="TRANSPORTISTA" ItemStyle-Width="30%" HeaderStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
        <asp:BoundField ReadOnly="True" HeaderText="Playa" DataField="Playa"
                            SortExpression="Playa" ItemStyle-Width="30%" HeaderStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>  
                            
                   <asp:BoundField ReadOnly="True" HeaderText="Posicion" DataField="Lugar"
                            SortExpression="Lugar" ItemStyle-Width="30%" HeaderStyle-Width="10%"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>


            <asp:BoundField ReadOnly="True" HeaderText="Tipo" DataField="TIPO_TRAILER" SortExpression="TIPO_TRAILER"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Bloqueado" DataField="BLOQUEADO" SortExpression="BLOQUEADO"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>

            <asp:BoundField ReadOnly="True" HeaderText="Razon" DataField="MOET_DESC" SortExpression="MOET_DESC"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
            <asp:BoundField ReadOnly="True" HeaderText="Ottawa" DataField="Ottawa" SortExpression="Ottawa"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
             <asp:BoundField ReadOnly="True" HeaderText="Fecha" DataField="fh_bloqueo" SortExpression="fh_bloqueo"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
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
      </div>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
  <!-- modal Trailer  ---->
  <div class="modal fade" id="modalTrailer" data-backdrop="static" role="dialog">
    <div class="modal-dialog" style="width: 900px;">
      <div class="modal-content">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Trailer
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow: auto">
              <div class="col-xs-12">
                <div class="col-xs-2">
                  Placa
                  <br />
                  <asp:TextBox ID="txt_editPlaca" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-1">
                  Externo
                  <br />
                  <asp:CheckBox ID="chk_editExterno" runat="server" OnCheckedChanged="cambia_interno"
                                Checked="false" AutoPostBack="true" />
                </div>
                <div class="col-xs-1">
                  N° Flota
                  <br />
                  <asp:TextBox ID="txt_editNumero" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-xs-5">
                  Transportista
                  <br />
                  <asp:DropDownList ID="ddl_editTran" CssClass="form-control" runat="server">
                  </asp:DropDownList>
                </div>
                <div class="col-xs-3">
                  Tipo
                  <br />
                  <asp:DropDownList ID="ddl_editTipo" CssClass="form-control" runat="server">
                  </asp:DropDownList>
                </div>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <asp:Literal ID="ltl_contenidoCaract" runat="server"></asp:Literal>
                <%--                Características
                <br />
                <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1">
                <div class="list-panel">
                <telerik:RadListBox ID="rlcli" runat="server" CheckBoxes="true"  ShowCheckAll="true"
                Width="90%" Height="100px" Visible="true" >
                </telerik:RadListBox>
                <br />
                </div>
                </telerik:RadAjaxPanel>--%>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClientClick="agregarCaract();"
                                  OnClick="btn_editGrabar_Click" ValidationGroup="nuevoTrailer" CssClass="btn btn-primary">
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
            <asp:RequiredFieldValidator ID="rfv_txt_editPlaca" runat="server" ControlToValidate="txt_editPlaca"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevoTrailer">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editPlaca">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_numero" runat="server" ControlToValidate="txt_editNumero"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ValidationGroup="nuevoTrailer">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_numero">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_tran" runat="server" ControlToValidate="ddl_editTran"
                                        InitialValue="0" Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoTrailer">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_tran">
            </asp:ValidatorCalloutExtender>
            <asp:RequiredFieldValidator ID="rfv_tipo" runat="server" ControlToValidate="ddl_editTipo"
                                        InitialValue="0" Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoTrailer">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_tipo">
            </asp:ValidatorCalloutExtender>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
  </div>
  <!-- Modal Eliminación -->
  <div class="modal fade" id="modalBloqueo" data-backdrop="static" role="dialog">
    <div class="modal-dialog" style="width:fit-content;" role="dialog">
      <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                <asp:Label ID="lblRazonEliminacion" runat="server"></asp:Label>
              </h4>
            </div>
            <div class="modal-body" style="overflow-y:auto; height: auto">
              <div id="dv_motivo" runat="server" style="width:200px;float:left;">
                Motivo de Bloqueo
                <br />
                <asp:DropDownList ID="ddTipoBloqueo" CssClass="form-control" runat="server">
                </asp:DropDownList>
              </div>
              <div class="col-xs-12">
              <div style="width:200px;float:left;display:none;padding-right: 20px;">
                Site
                <br />
                <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" AutoPostBack="true"
                                  OnSelectedIndexChanged="ddl_buscarSite_onChange" runat="server">
                </asp:DropDownList>
              </div>
              <div id="dv_zona" style="width:200px;float:left;padding-right: 20px;" runat="server">
                Zona
                <br />
                <asp:DropDownList ID="ddl_bloqZona" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_bloqZona_onChange"
                                  runat="server">
                </asp:DropDownList>
              </div>
              <div id="dv_playa" style="width:200px;float:left;padding-right: 20px;" runat="server">
                Playa
                <br />
                <asp:DropDownList ID="ddl_bloqPlaya" CssClass="form-control" runat="server" Enabled="false"
                                  AutoPostBack="true" OnSelectedIndexChanged="ddl_bloqPlaya_onChange">
                </asp:DropDownList>
              </div>
              <div id="dv_lugar" style="width:200px;float:left;padding-right: 20px;" runat="server">
                Lugar
                <br />
                <asp:DropDownList ID="ddl_bloqLugar" CssClass="form-control" runat="server" Enabled="false">
                </asp:DropDownList>
              </div>
               
              </div>
            </div>
            <br />
            <div class="modal-footer">
              <asp:LinkButton ID="btn_bloquear" OnClick="btn_bloquear_Click" runat="server" 
              CssClass="btn btn-primary" ToolTip="Bloquear">
                <span class="glyphicon glyphicon-ok"></span>
              </asp:LinkButton>
              <asp:LinkButton ID="btn_desbloquear" OnClick="btn_desbloquear_Click" runat="server" 
              CssClass="btn btn-primary" ToolTip="Desbloquear">
                <span class="glyphicon glyphicon-ok"></span>
              </asp:LinkButton>
              <asp:LinkButton ID="btn_mover" OnClick="btn_mover_Click" runat="server" 
              CssClass="btn btn-primary" ToolTip="Mover">
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
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hf_idTrailer" runat="server" />
      <asp:HiddenField ID="hf_excluyentes" Value="" runat="server" />
      <asp:HiddenField ID="hf_noexcluyentes" Value="" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
  <script type="text/javascript">
    function modalEditarTrailer() {
        $("#modalTrailer").modal();
    }


    function modalCerrar(modal) {
        $("#" + modal).modal('hide');
    }
    

    function modalBloqueo(a) {
//        if (a == true) {
//            $('#<%= dv_motivo.ClientID %>').css('display', 'block');
//            $('#<%= dv_zona.ClientID %>').css('display', 'none');
//            $('#<%= dv_playa.ClientID %>').css('display', 'none');
//            $('#<%= dv_lugar.ClientID %>').css('display', 'none');
//            $('#<%= btn_bloquear.ClientID %>').css('display', 'inline-block');
//            $('#<%= btn_desbloquear.ClientID %>').css('display', 'none');
//        }
//        else {
////            $('#<%= dv_motivo.ClientID %>').css('display', 'none');
////            $('#<%= dv_zona.ClientID %>').css('display', 'block');
////            $('#<%= dv_playa.ClientID %>').css('display', 'block');
////            $('#<%= dv_lugar.ClientID %>').css('display', 'block');
////            $('#<%= btn_bloquear.ClientID %>').css('display', 'none');
////            $('#<%= btn_desbloquear.ClientID %>').css('display', 'inline-block');
//        }
        $("#modalBloqueo").modal();
    }

    function agregarCaract() {
        var checks = document.getElementsByName("check");
        var drops = document.getElementsByName("drop");
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
            $("#op_drop_" + y[i]).attr('selected', true);
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


  </script>
</asp:Content>