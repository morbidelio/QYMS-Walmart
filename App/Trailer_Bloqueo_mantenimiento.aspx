<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
    CodeFile="Trailer_Bloqueo_mantenimiento.aspx.cs" Inherits="App_Trailer_Bloqueo1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <h2>Tráiler Bloqueado
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
<asp:UpdatePanel runat="server" ID="updbuscar">
        <ContentTemplate>
    <div class="col-xs-12 separador">
    </div>

    <asp:Panel ID="site" runat="server">
        <div class="col-xs-2">
            Site
      <br />
            <asp:DropDownList ID="ddl_site" CssClass="form-control" runat="server">
            </asp:DropDownList>
        </div>
    </asp:Panel>
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
        <asp:TextBox ID="txt_buscarNro" runat="server" CssClass="form-control" />
    </div>
    <div class="col-xs-1">
        Placa
    <br />
        <asp:TextBox ID="txt_buscarNombre" runat="server" CssClass="form-control" />
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
        <asp:LinkButton ID="btn_buscar" OnClick="btn_buscar_Click" CssClass="btn btn-primary" ToolTip="Buscar Trailer" runat="server" >
      <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_nuevo" OnClick="btn_nuevo_Click" CssClass="btn btn-primary" ToolTip="Nuevo Trailer" Visible="false" runat="server">
      <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>
      </ContentTemplate>
  
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
   
            <div class="col-xs-12">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <asp:GridView ID="gv_listar" runat="server" AllowPaging="false" AllowSorting="True" PageSize="8" EnableViewState="false" UseAccessibleHeader="true"
                    OnPageIndexChanging="gv_listar_PageIndexChanging" OnRowCommand="gv_listar_RowCommand" OnRowEditing="gv_listar_RowEditing" OnSorting="gv_listar_Sorting" OnRowDataBound="gv_listar_RowDataBound" OnRowCreated="gv_listar_OnRowCreated"
                    EmptyDataText="No se encontraron trailers!" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tablita">
                    <Columns>
                        <asp:TemplateField ShowHeader="False" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_bloquear" runat="server" CommandName="BLOQUEAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Bloquear">
                  <span class="glyphicon glyphicon-lock" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btn_desbloquear" runat="server" CommandName="DESBLOQUEAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Desbloquear">
                  <span class="glyphicon glyphicon-eject" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_continuar" runat="server" CommandName="CONTINUAR" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Continuar">
                  <span class="glyphicon glyphicon-play" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_mover" runat="server" CommandName="MOVER" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-xs btn-primary" ToolTip="Mover Trailer">
                  <span class="glyphicon glyphicon-transfer" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="ID" SortExpression="ID" ItemStyle-Height="30px" Visible="false" />
                        <asp:BoundField ReadOnly="True" HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO" Visible="false" ItemStyle-Width="10%" />
                        <asp:BoundField ReadOnly="True" HeaderText="Placa" DataField="PLACA" SortExpression="PLACA" HeaderStyle-Width="5%" />
                        <asp:BoundField ReadOnly="True" HeaderText="Externo" DataField="EXTERNO" SortExpression="EXTERNO" Visible="false" />
                        <asp:BoundField ReadOnly="True" HeaderText="Número" DataField="NUMERO" SortExpression="NUMERO" />
                        <asp:BoundField ReadOnly="True" HeaderText="Transportista" DataField="TRANSPORTISTA" HeaderStyle-Width="15%" SortExpression="TRANSPORTISTA" />
                        <asp:BoundField ReadOnly="True" HeaderText="Playa" DataField="Playa" HeaderStyle-Width="13%" SortExpression="Playa" />
                        <asp:BoundField ReadOnly="True" HeaderText="Posicion" DataField="Lugar" SortExpression="Lugar" />
                        <asp:BoundField ReadOnly="True" HeaderText="Tipo" DataField="TIPO_TRAILER" SortExpression="TIPO_TRAILER" />
                        <asp:BoundField ReadOnly="True" HeaderText="Bloqueado" DataField="BLOQUEADO" SortExpression="BLOQUEADO" HeaderStyle-Width="13%" />
                        <asp:BoundField ReadOnly="True" HeaderText="Usuario" DataField="usuario" SortExpression="usuario" />
                        <asp:BoundField ReadOnly="True" HeaderText="FH" DataField="TRAI_FH_BLOQUEO" SortExpression="TRAI_FH_BLOQUEO" />
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
           
        </ContentTemplate>
    </asp:UpdatePanel>
     </div>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <!-- modal Trailer  ---->
    <div class="modal fade" id="modalTrailer" data-backdrop="static" role="dialog">
        <div class="modal-dialog" style="width: 900px;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Trailer
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow: auto">

                            <div class="col-xs-2">
                                Placa
                <br />
                                <asp:TextBox ID="txt_editPlaca" CssClass="form-control" runat="server" />
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
                                <asp:TextBox ID="txt_editNumero" CssClass="form-control" runat="server" />
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

                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12">
                                <asp:Literal ID="ltl_contenidoCaract" runat="server"></asp:Literal>
                            </div>
                            <div class="col-xs-12 separador">
                            </div>
                            <div class="col-xs-12" style="text-align:center;">
			                  <asp:LinkButton ID="btn_grabar" runat="server" OnClientClick="agregarCaract();" OnClick="btn_grabar_Click" ValidationGroup="nuevoTrailer" CssClass="btn btn-primary">
			                    <span class="glyphicon glyphicon-floppy-disk" />
			                  </asp:LinkButton>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
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
        <div class="modal-dialog" role="dialog">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>
                                <asp:Label ID="lblRazonEliminacion" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body" style="overflow-y: auto; height: auto">

                            <div id="dv_motivo" runat="server" class="col-xs-4">
                                Motivo de Bloqueo
                <br />
                                <asp:DropDownList ID="ddTipoBloqueo" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-4" style="display: none;">
                                Site
                <br />
                                <asp:DropDownList ID="ddl_buscarSite" CssClass="form-control" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddl_buscarSite_onChange" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div id="dv_zona" runat="server" class="col-xs-4">
                                Zona
                <br />
                                <asp:DropDownList ID="ddl_bloqZona" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_bloqZona_onChange"
                                    runat="server">
                                </asp:DropDownList>
                            </div>
                            <div id="dv_playa" runat="server" class="col-xs-4">
                                Playa
                <br />
                                <asp:DropDownList ID="ddl_bloqPlaya" CssClass="form-control" runat="server" Enabled="false"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddl_bloqPlaya_onChange">
                                </asp:DropDownList>
                            </div>
                            <div id="dv_lugar" runat="server" class="col-xs-4">
                                Lugar
                <br />
                                <asp:DropDownList ID="ddl_bloqLugar" CssClass="form-control" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <br />
                        <div class="modal-footer">
                            <asp:LinkButton ID="btn_bloquear" OnClick="btn_bloquear_Click" runat="server"
                                CssClass="btn btn-primary" ToolTip="Bloquear">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_desbloquear" OnClick="btn_desbloquear_Click" runat="server"
                                CssClass="btn btn-primary" ToolTip="Desbloquear">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btn_mover" OnClick="btn_mover_Click" runat="server"
                                CssClass="btn btn-primary" ToolTip="Mover">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <button type="button" class="btn btn-primary" data-dismiss="modal">
                                <span class="glyphicon glyphicon-remove" />
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

        var pageScrollPos = 0;
        var pageScrollPosleft = 0;
        function BeginRequestHandler(sender, args) {
            pageScrollPos = $('div.dataTables_scrollBody').scrollTop();
            pageScrollPosleft = $('div.dataTables_scrollBody').scrollLeft();
        }


        function modalEditarTrailer() {
            $("#modalTrailer").modal();
        }

        function modalBloqueo() {
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

        function modalCerrar(modal) {
            $("#" + modal).modal('hide');
        }

        function EndRequestHandler(sender, args) {
            setTimeout(tabla, 100);
        }

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);

        var calcDataTableHeight = function () {
            //   alert($(window).height() - $("#scrolls").offset().top);
            return $(window).height() - $("#scrolls").offset().top - 100;
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
                    "paging": false,
                    "ordering": false,
                    "searching": false,
                    "lengthChange": false,
                    "info":false
                });
                $('div.dataTables_scrollBody').scrollTop(pageScrollPos);
                $('div.dataTables_scrollBody').scrollLeft(pageScrollPosleft);
            }
        }

        function click_recarga() {
            if (($('.modal:visible').length && $('body').hasClass('modal-open')) != true) {
                $("#<% =btn_buscar.ClientID %>")[0].click();
            }
        }

        var tick_recarga;

        $(document).ready(function (e) {
            clearInterval(tick_recarga);
            tick_recarga = setInterval(click_recarga, 180000);
        });


    </script>
</asp:Content>
