﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="StockTrailerV2.aspx.cs" Inherits="App_stockTrailerv2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <h2>Stock de Trailer
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-xs-12 separador">
    </div>
    <div class="col-xs-12">
        <div class="col-xs-1">
            Site:
      <br />
            <asp:DropDownList ID="DDL_SITE" CssClass="form-control" runat="server">
            </asp:DropDownList>
        </div>
        <div class="col-xs-2">
            Disponibilidad
      <br />
            <asp:DropDownList ID="DDL_disponibilidad" CssClass="form-control" runat="server">
            </asp:DropDownList>
        </div>
        <div class="col-xs-1">
            Asignado
      <br />
            <asp:CheckBox ID="CHK_Asignado" runat="server" />
        </div>

        <div class="col-xs-1">
            Bloqueado
      <br />
            <asp:CheckBox ID="CHK_bloqeuado" runat="server" />
        </div>


        <div class="col-xs-1">
            Plancha
      <br />
            <asp:CheckBox ID="chk_plancha" runat="server" />
        </div>
        <div class="col-xs-1">
            Shortec
      <br />
            <asp:DropDownList ID="ddl_shortec" CssClass="form-control" runat="server">
                <asp:ListItem Selected="True" Text="Seleccione" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-xs-2">
            Capacidad
      <br />
            <asp:DropDownList ID="ddl_capacidad" CssClass="form-control" runat="server">
            </asp:DropDownList>
        </div>
        <div class="col-xs-2">
            Tipo Carga
      <br />
            <asp:DropDownList ID="ddl_tipocarga" CssClass="form-control" runat="server">
            </asp:DropDownList>
        </div>
        <div class="col-xs-1">
            <br />
            <asp:LinkButton ID="btn_buscarTrailer" OnClick="btn_buscarTrailer_Click" CssClass="btn btn-primary"
                ToolTip="Buscar Trailer" runat="server">
        <span class="glyphicon glyphicon-search"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="btn_export" ToolTip="Exportar a Excel" CssClass="btn btn-primary" OnClick="btn_export_Click" runat="server">
            <span class="glyphicon glyphicon-import"></span>
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-xs-12" style="max-height: 80vh; min-height: 80vh; padding-left: 0px; padding-right: 0px;">
                <asp:GridView ID="gv_listaTrailer" runat="server" AllowPaging="false" AllowSorting="true" UseAccessibleHeader="true" OnRowCreated="gv_listar_RowCreated"
                    BorderColor="Black" CellPadding="8" DataKeyNames="" BackColor="White" GridLines="Horizontal"
                    EnableSortingAndPagingCallbacks="True" PageSize="6"
                    OnSorting="gv_listaTrailer_Sorting" EmptyDataText="No se encontraron trailers!" AutoGenerateColumns="False"
                    Width="100%" PagerSettings-Mode="NumericFirstLast" CssClass="table table-bordered table-hover tablita"
                    OnRowDataBound="gv_listaTrailer_RowDataBound">
                    <Columns>
                        <asp:BoundField ReadOnly="True" HeaderText="PLACA" DataField="PLACA" SortExpression="PLACA"
                            ItemStyle-Width="20%" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="FLOTA" DataField="FLOTA" SortExpression="FLOTA"
                            ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="CAPACIDAD" DataField="CAPACIDAD" SortExpression="CAPACIDAD"
                            ItemStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="PLANCHA" DataField="PLANCHA" SortExpression="PLANCHA"
                            ItemStyle-Width="30%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="FRIO SECO" DataField="FRIO_SECO" SortExpression="FRIO_SECO"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="LUGAR" DataField="LUGAR" SortExpression="LUGAR"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="BLOQUEADO" DataField="BLOQUEADO" SortExpression="BLOQUEADO"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="TRANSPORTISTA" DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="ESTADO TRAILER" DataField="BLOQUEO" SortExpression="BLOQUEO"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="ESTADO" DataField="ESTADO" SortExpression="ESTADO"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="FECHA ULTIMO MOVIMIENTO" DataField="FH_ULTIMO" SortExpression="FH_ULTIMO"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="solicitud" DataField="soli_id" SortExpression="FH_ULTIMO"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="movimiento" DataField="movi_id" SortExpression="movi_id"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="estado" DataField="soes_desc" SortExpression="soes_desc"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="fh mod" DataField="SOLI_FH_TIMESTAMP" SortExpression="SOLI_FH_TIMESTAMP"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField ReadOnly="True" HeaderText="tipo soli" DataField="SOTI_DESC" SortExpression="SOTI_DESC"
                            ItemStyle-Width="20%" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center"></asp:BoundField>

                        <asp:BoundField ReadOnly="True" HeaderText="SOLI_FH_CREACION" DataField="SOLI_FH_CREACION" SortExpression="SOLI_FH_CREACION"
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
<asp:Content ID="Hidden" ContentPlaceHolderID="ocultos" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_idTrailer" runat="server" />
            <asp:HiddenField ID="hf_excluyentes" Value="" runat="server" />
            <asp:HiddenField ID="hf_noexcluyentes" Value="" runat="server" />
            <asp:Button ID="btnExportar" runat="server" CssClass="ocultar" OnClick="btnExportar_Click" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        function modalEditarTrailer() {
            $("#modalTrailer").modal();
        }

        function modalConfirmacion() {
            $("#modalConfirmacion").modal();
        }

        function Exportar() {
            $get("<%=this.btnExportar.ClientID %>").click();
        }



        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    var button = document.getElementById("<% =this.btn_buscarTrailer.ClientID %>");
                //                __doPostBack("<% =this.btn_buscarTrailer.ClientID %>".replace("_","$") ,'');
                //                $("#<% =this.btn_buscarTrailer.ClientID %>").click();
                button.click();
                return true;
            }
        });
    });


        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);

        var pageScrollPos = 0;
        var pageScrollPosleft = 0;
        function BeginRequestHandler(sender, args) {
            pageScrollPos = $('div.dataTables_scrollBody').scrollTop();
            pageScrollPosleft = $('div.dataTables_scrollBody').scrollLeft();
        }


        function EndRequestHandler1(sender, args) {
            setTimeout(tabla, 100);
        }
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(EndRequestHandler1);

        var calcDataTableHeight = function () {
            //   alert($(window).height() - $("#scrolls").offset().top);
            return $(window).height() - $("#scrolls").offset().top - 100;
        };

        function reOffset1() {

            $('div.dataTables_scrollBody').height(calcDataTableHeight());
        }


        window.onresize = function (e) { reOffset1(); }


        function tabla() {
            if ($('#<%= this.gv_listaTrailer.ClientID %>')[0] != undefined && $('#<%= this.gv_listaTrailer.ClientID %>')[0].rows.length > 1) {
            $('#<%= this.gv_listaTrailer.ClientID %>').DataTable({
                    "scrollY": calcDataTableHeight(),
                    "scrollX": true,
                    "scrollCollapse": true,
                    "paging": false,
                    "ordering": false,
                    "searching": false,
                    "lengthChange": false
                });
                //    alert(pageScrollPos);
                $('div.dataTables_scrollBody').scrollTop(pageScrollPos);
                $('div.dataTables_scrollBody').scrollLeft(pageScrollPosleft);

            }
        }


    </script>
</asp:Content>
