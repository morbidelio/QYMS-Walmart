<%@ Control Language="C#" AutoEventWireup="true" CodeFile="control_descarga.ascx.cs" Inherits="control_descarga" %>

  <div class="col-xs-12">
            <div class="col-xs-4">
                Trailer 
                <br />
                <asp:TextBox ID="txt_nroPista" Width="50%" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-xs-4">
                Transporte
                <br />
                <asp:TextBox ID="txt_transporte" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
    </div>
    <div class="col-xs-12 separador"></div>
    <asp:UpdatePanel runat="server"   ClientIDMode="AutoID">
        <ContentTemplate>
            <div class="col-xs-6">
                <div class="col-xs-12">
                    <div class="col-xs-2">
                        Origen
                    </div>
                    <div class="col-xs-12 separador"></div>
                    <div class="col-xs-3">
                        <asp:Panel ID="pnl_lugarOrigen" CssClass="lugar" runat="server">
                            <asp:Panel ID="pnl_detalleLugar" CssClass="row columna-anden detalle-lugar" runat="server">
                                <asp:Label ID="lbl_lugar" runat="server"></asp:Label>
                            </asp:Panel>
                            <asp:Panel ID="pnl_detalleTrailer" CssClass="row columna-anden detalle-trailer" runat="server">
                                <asp:Image ID="img_reloj" Width="17px" runat="server" />
                                <asp:Image ID="img_trailer" Width="17px" runat="server" />
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                    <div id="dv_origen" class="col-xs-9">
                        Zona: 
                        <asp:Label ID="lbl_origenZona" runat="server"></asp:Label>
                        <br />
                        Playa: 
                        <asp:Label ID="lbl_origenPlaya" runat="server"></asp:Label>
                    </div>
                </div>
                </div>
                <div class="col-xs-6">
                    <div class="col-xs-12">
                        Destino
                    </div>
                    <div class="col-xs-12 separador"></div>
                    <div class="col-xs-12">
                        <div class="col-xs-5">Zona </div>
                        <div class="col-xs-7">
                            <asp:DropDownList ID="ddl_destinoZona" CssClass="form-control" runat="server" AutoPostBack="true"
                                onselectedindexchanged="ddl_destinoZona_SelectedIndexChanged" 
                                Enabled="False">
                                <asp:ListItem>Seleccione...</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <div class="col-xs-5">Playa </div>
                        <div class="col-xs-7">
                            <asp:DropDownList ID="ddl_destinoPlaya" CssClass="form-control" runat="server" Enabled="false" AutoPostBack="true"
                                onselectedindexchanged="ddl_destinoPlaya_SelectedIndexChanged">
                                <asp:ListItem>Seleccione...</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <div class="col-xs-5">Posición </div>
                        <div class="col-xs-7">
                            <asp:DropDownList ID="ddl_destinoPos" CssClass="form-control" Enabled="false" runat="server">
                                <asp:ListItem>Seleccione...</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
       
      <asp:AsyncPostBackTrigger ControlID="ddl_destinoZona" EventName="SelectedIndexChanged" />
                          <asp:AsyncPostBackTrigger ControlID="ddl_destinoPlaya" EventName="SelectedIndexChanged" />
      <asp:AsyncPostBackTrigger ControlID="ddl_destinoPos" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="col-xs-12 separador"></div>
  
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_trailerId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>