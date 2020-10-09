<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
    CodeFile="Trailer_A_Tracto.aspx.cs" Inherits="App_Trailer_A_Tracto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador">
    </div>
    <h2>Trailer a Tracto
    </h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-12 separador">
    </div>
    <h2>ESTA ACCIÓN ES IRREVERSIBLE. CONTINÚE BAJO SU PROPIA RESPONSABILIDAD.
    </h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-12 separador">
            </div>
            <div class="col-xs-12">
                <div class="col-lg-3" style="font-size: xx-large">
                    Placa a Consultar
                </div>
                <div class="col-lg-3">
                    <asp:TextBox ID="txt_placa" CssClass="form-control" runat="server" Style="height: 50px !important; font-size: xx-large" />
                </div>
                <div class="col-lg-3">
                    <asp:LinkButton ID="btn_buscar" CssClass="btn btn-primary" OnClick="btn_buscar_Click" runat="server" Style="height: 50px !important; font-size: xx-large">
            <span class="glyphicon glyphicon-search" />
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-lg-12 separador">
            </div>
            <div class="col-lg-12 separador">
            </div>
            <div class="col-xs-12">
                <center style="font-size: xx-large">Trailer</center>
                <br />
                <div class="col-xs-12" style="text-align: center">
                    <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True"
                        EnableViewState="false" DataKeyNames="ID" PageSize="8" EmptyDataText="No se encontraron trailers!"
                        AutoGenerateColumns="False" Width="1200px" CssClass="table table-bordered table-hover tablita">
                        <Columns>
                            <asp:BoundField HeaderText="Id" DataField="ID" SortExpression="ID" Visible="false" />
                            <asp:BoundField HeaderText="Código" DataField="CODIGO" SortExpression="CODIGO" />
                            <asp:BoundField HeaderText="Placa" DataField="PLACA" SortExpression="PLACA" />
                            <asp:BoundField HeaderText="Externo" DataField="EXTERNO" SortExpression="EXTERNO" />
                            <asp:BoundField HeaderText="Número" DataField="NUMERO" SortExpression="NUMERO" />
                            <asp:BoundField HeaderText="Transportista" DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" />
                            <asp:BoundField HeaderText="Tipo" DataField="TIPO_TRAILER" SortExpression="TIPO_TRAILER" />
                            <asp:BoundField HeaderText="Bloqueado" DataField="BLOQUEADO" SortExpression="BLOQUEADO" />
                            <asp:BoundField HeaderText="Requiere Sello" DataField="REQ_SELLO" SortExpression="REQ_SELLO" />
                        </Columns>
                        <PagerStyle CssClass="pagination-ys" />
                    </asp:GridView>
                </div>
            </div>
            <div class="col-lg-12 separador">
            </div>
            <div class="col-xs-12" style="text-align: center">
                <asp:LinkButton Visible="false" ID="btn_asociar" CssClass="btn btn-primary" OnClick="btn_asociar_Click"
                    runat="server" Style="height: 50px !important; font-size: xx-large">
            <span class="glyphicon glyphicon-arrow-down" />
                </asp:LinkButton>
            </div>
            <div class="col-xs-12">
                <center style="font-size: xx-large">Tracto</center>
                <div class="col-xs-12 separador">
                </div>
                <div class="col-xs-12" style="text-align: center">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="false" AllowSorting="True"
                        PageSize="8" Width="70%" EmptyDataText="No se encontraron Tractos!" AutoGenerateColumns="False"
                        CssClass="table table-bordered table-hover tablita">
                        <Columns>
                            <asp:BoundField HeaderText="Patente" DataField="PATENTE" SortExpression="PATENTE" />
                            <asp:BoundField HeaderText="Estado" DataField="ESTADO" SortExpression="ESTADO" />
                            <asp:BoundField HeaderText="Transportista" DataField="TRANSPORTISTA" SortExpression="TRANSPORTISTA" />
                        </Columns>
                        <PagerStyle ForeColor="#BBB" HorizontalAlign="Right" BackColor="White" />
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>
