<%@ Control Language="C#" AutoEventWireup="true" CodeFile="nuevo_trailer2.ascx.cs" Inherits="nuevo_trailer2" %>
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
                <asp:TextBox ID="txt_editPlaca" CssClass="form-control" runat="server"></asp:TextBox>
              </div>
              <div class="col-xs-1">
                Externo
                <br />
                <asp:CheckBox ID="chk_editExterno" runat="server" OnCheckedChanged="cambia_interno" Checked="false"  AutoPostBack="true"/>
              </div>
              <div class="col-xs-1">
                N° Flota
                <br />
                <asp:TextBox ID="txt_editNumero" CssClass="form-control" runat="server" ></asp:TextBox>
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
                <asp:LinkButton ID="btn_editGrabar" runat="server" OnClientClick="agregarCaract();" OnClick="btn_editGrabar_Click" ValidationGroup="nuevoTrailer"
                                CssClass="btn btn-primary">
                  <span class="glyphicon glyphicon-floppy-disk"></span>
                </asp:LinkButton>
              </center>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">
              Cerrar</button>
          </div>

          <asp:RequiredFieldValidator ID="rfv_txt_editPlaca" runat="server" ControlToValidate="txt_editPlaca"
                                      Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                      ValidationGroup="nuevoTrailer">
          </asp:RequiredFieldValidator>
          <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomLeft"
                                        Enabled="True" TargetControlID="rfv_txt_editPlaca">
          </asp:ValidatorCalloutExtender>

          <asp:RequiredFieldValidator ID="rfv_numero" runat="server" ControlToValidate="txt_editNumero"
                                      Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                      ValidationGroup="nuevoTrailer">
          </asp:RequiredFieldValidator>
          <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                                        Enabled="True" TargetControlID="rfv_numero">
          </asp:ValidatorCalloutExtender>

          <asp:RequiredFieldValidator ID="rfv_tran" runat="server" ControlToValidate="ddl_editTran" InitialValue="0"
                                      Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
                                      ValidationGroup="nuevoTrailer">
          </asp:RequiredFieldValidator>
          <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                                        Enabled="True" TargetControlID="rfv_tran">
          </asp:ValidatorCalloutExtender>

          <asp:RequiredFieldValidator ID="rfv_tipo" runat="server" ControlToValidate="ddl_editTipo" InitialValue="0"
                                      Display="None" ErrorMessage="* Requerido" SetFocusOnError="True"
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
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
  <ContentTemplate>
    <asp:HiddenField ID="hf_idTrailer" runat="server" />
    <asp:HiddenField ID="hf_excluyentes" Value="" runat="server" />
    <asp:HiddenField ID="hf_noexcluyentes" Value="" runat="server" />
  </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
  function modalEditarTrailer() {
      $("#modalTrailer").modal();
  }

  function modalConfirmacion() {
      $("#modalConfirmacion").modal();
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
</script>
