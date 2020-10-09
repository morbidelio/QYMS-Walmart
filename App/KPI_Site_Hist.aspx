<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterTms.master" AutoEventWireup="true" CodeFile="KPI_Site_Hist.aspx.cs" Inherits="App_KPI_Site" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-12 separador"></div>
            <div class="col-lg-1" style="text-align: right">
                Site
            </div>
            <div class="col-lg-2">
                <asp:DropDownList ID="ddl_site" OnSelectedIndexChanged="ddl_site_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-lg-1" style="text-align: right">
                Desde
            </div>
            <div class="col-lg-1">
                <asp:TextBox ID="txt_desde" CssClass="form-control input-fecha" runat="server" />
            </div>
            <div class="col-lg-1" style="text-align: right">
                Hasta
            </div>
            <div class="col-lg-1">
                <asp:TextBox ID="txt_hasta" CssClass="form-control input-fecha" runat="server" />
            </div>
            <div class="col-lg-1">
                <asp:LinkButton ID="btn_buscar" CssClass="btn btn-primary" runat="server" OnClick="btn_buscar_Click">
                    <span class="glyphicon glyphicon-search" />
                </asp:LinkButton>
            </div>
            <div class="col-lg-1">
                <button class="btn btn-primary" type="button" onclick="javascript:window.location.href = './Panel_control.aspx'">
                    Panel Control
                </button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Timer ID="prueba" runat="server" OnTick="btn_buscar_Click" Interval="60000"></asp:Timer>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div style="margin: 2px;" class="panel panel-primary">
                    <div class="panel-heading" style="text-align: center">
                        <h2 class="panel-title">
                        KPI CENTRO DE DISTRIBUCION</h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-2" style="min-width: 240px">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title" style="min-height: 30px">Tiempo en andén carga</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" data-placement="right" title="Promedio" width="50" src="../images/yms/reloj_normal.png" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoPromedioCarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" data-placement="right" title="Mínimo" width="50" src="../images/yms/reloj_atiempo.png" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoMinCarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" data-placement="right" title="Máximo" width="50" src="../images/yms/reloj_atrasado.png" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoMaxCarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2" style="min-width: 260px">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <h3 class="panel-title" style="min-height: 30px">Tiempo en andén descarga</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" title="Promedio" data-placement="right" width="50" src="../images/yms/reloj_normal.png" runat="server" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoPromedioDescarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" title="Mínimo" data-placement="right" width="50" src="../images/yms/reloj_atiempo.png" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoMinDescarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                    <div class="col-lg-12 separador"></div>
                                    <div class="col-lg-6">
                                        <img data-toggle="tooltip" title="Máximo" data-placement="right" width="50" src="../images/yms/reloj_atrasado.png" />
                                    </div>
                                    <div class="col-lg-6">
                                <br />
                                        <asp:Label ID="lbl_tiempoMaxDescarga" Font-Bold="true" Text="text" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

