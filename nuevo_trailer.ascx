<%@ Control Language="C#" AutoEventWireup="true" CodeFile="nuevo_trailer.ascx.cs" Inherits="nuevo_trailer" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="modal fade" id="modalTrailer" data-backdrop="static" role="dialog">
  <div class="modal-dialog" style="width:900px;">
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
                  <asp:TextBox ID="txt_editPlaca" CssClass="form-control input-mayus" runat="server" OnTextChanged="txt_editPlaca_TextChanged" AutoPostBack="true"></asp:TextBox>
                </div>
                <div class="col-xs-5">
                  Transportista
                  <br />
                  <asp:DropDownList ID="ddl_editTran" CssClass="form-control" runat="server">
                  </asp:DropDownList>
                  <asp:CheckBox ID="importado" runat="server" Visible="false" />
                </div>
              </div>
              <div class="col-xs-12 separador"></div>
              <div class="col-xs-12">
                <center>
                  <asp:LinkButton ID="btn_editGrabar" runat="server" OnClick="btn_editGrabar_Click" ValidationGroup="nuevoTrailer"
                                  CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
                </center>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-default" data-dismiss="modal">
                <span class="glyphicon glyphicon-remove"></span>
              </button>
            </div>

            <asp:RequiredFieldValidator ID="rfv_txt_editPlaca" runat="server" ControlToValidate="txt_editPlaca"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoTrailer">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_txt_editPlaca">
            </asp:ValidatorCalloutExtender>

            <asp:RequiredFieldValidator ID="rfv_tran" runat="server" ControlToValidate="ddl_editTran" InitialValue="0"
                                        Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                        ValidationGroup="nuevoTrailer">
            </asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" PopupPosition="BottomLeft"
                                          Enabled="True" TargetControlID="rfv_tran">
            </asp:ValidatorCalloutExtender>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
</div>
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
  <ContentTemplate>
    <asp:HiddenField ID="hf_idTrailer" runat="server" />
  </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
  function modalEditarTrailer() {
      $("#modalTrailer").modal();
  }

  function modalConfirmacion() {
      $("#modalConfirmacion").modal();
  }

 
</script>
