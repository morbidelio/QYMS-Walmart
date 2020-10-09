<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Perfil_Mobile.aspx.cs" Inherits="Perfil_Mobile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" Runat="Server">
<div class="col-xs-12 separador"></div>
<h2>
    Maestro Perfil Mobile
</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" Runat="Server">
  <div class="col-xs-12 separador">
  </div>
  
  <div class="col-xs-2">
    Nombre
    <br />
    <asp:TextBox ID="txt_buscarNombre" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>
  </div>
  <div class="col-xs-1">
    <br />
    <asp:LinkButton ID="btn_buscarPerfil" runat="server" CssClass="btn btn-primary" OnClick="btn_buscar_Click"
                    ToolTip="Buscar Perfíl">
      <span class="glyphicon glyphicon-search"></span>
    </asp:LinkButton>
    <asp:LinkButton ID="btn_nuevoPerfil" runat="server" CssClass="btn btn-primary" ToolTip="Nuevo Perfíl" 
                    onclick="btn_nuevoPerfil_Click">
      <span class="glyphicon glyphicon-plus"></span>
    </asp:LinkButton>
  </div>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div class="col-xs-12">
          <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" PageSize="8"
                        DataKeyNames="ID" CssClass="table table-bordered table-hover tablita" EnableSortingAndPagingCallbacks="false"
                        EmptyDataText="No Existen Perfiles!" AutoGenerateColumns="False" PagerSettings-Mode="NumericFirstLast"
                        OnRowEditing="gv_listaPerfiles_RowEditing" OnRowCommand="gv_listaPerfiles_RowCommand"
                        OnSorting="gv_listaPerfiles_Sorting" OnRowDataBound="gv_listaPerfiles_RowDataBound" OnPageIndexChanging="gv_listaPerfiles_PageIndexChanging">
            <Columns>
              <asp:TemplateField HeaderStyle-Width="1%" ShowHeader="False">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_modificar" CssClass="btn btn-xs btn-primary" runat="server" CausesValidation="False"
                                  CommandName="EDIT" ToolTip="Modificar">
                    <span class="glyphicon glyphicon-pencil"></span>
                  </asp:LinkButton>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderStyle-Width="1%" ShowHeader="False">
                <ItemTemplate>
                  <asp:LinkButton ID="btn_eliminar" CssClass="btn btn-xs btn-primary" runat="server" CausesValidation="False" CommandArgument='<%# Eval("ID") %>'
                                  CommandName="ELIMINAR" ToolTip="Eliminar perfil">
                    <span class="glyphicon glyphicon-remove"></span>
                  </asp:LinkButton>
                </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField ReadOnly="True" HeaderText="Id" DataField="ID" SortExpression="ID"
                              Visible="false" />
              <asp:BoundField ReadOnly="True" HeaderText="Nombre" DataField="NOMBRE" SortExpression="NOMBRE" />
              <asp:BoundField ReadOnly="True" HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION" />
              <asp:BoundField ReadOnly="True" HeaderText="Nivel Permisos" DataField="NIVEL_PERMISOS" SortExpression="NIVEL_PERMISOS" />
              <asp:BoundField ReadOnly="True" DataField="CANTIDAD" ItemStyle-CssClass="ocultar" HeaderStyle-CssClass="ocultar" />
            </Columns>
            <PagerTemplate>
              Página
              <asp:DropDownList ID="paginasDropDownList" Font-Size="12px" AutoPostBack="true" runat="server"
                                OnSelectedIndexChanged="GoPage">
              </asp:DropDownList>
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
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" Runat="Server">
  <div id="modalPerfil" class="modal fade" data-backdrop="static" role="dialog">
    <div class="modal-dialog modal-lg" style="width: 1300px">
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
          <!-- Modal content-->
          <div class="modal-content">
            <div class="modal-header">
              <h4 class="modal-title">
                Datos Perfil
              </h4>
            </div>
            <div class="modal-body" style="height: auto; overflow-y: auto;">
              <div class="col-xs-12">
                <div class="col-xs-2">
                  Nombre
                  <br />
                  <asp:TextBox ID="txt_nombreEdita" runat="server" CssClass="form-control input-mayus"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_nombreEdita"
                                              Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ToolTip="Nombre Perfil"
                                              ValidationGroup="nuevoDestino">
                  </asp:RequiredFieldValidator>
                  <asp:ValidatorCalloutExtender ID="txt_nombreEdita_ValidatorCalloutExtender" runat="server"
                                                Enabled="True" TargetControlID="RequiredFieldValidator1">
                  </asp:ValidatorCalloutExtender>
                </div>
                <div class="col-xs-2">
                  Descripción
                  <br />
                  <asp:TextBox ID="txt_descripcionEdita" runat="server" CssClass="form-control input-mayus"></asp:TextBox>
                </div>
                <div class="col-xs-2">
                  Nivel Permisos
                  <br />
                  <asp:TextBox ID="txt_editPermisos" runat="server" CssClass="form-control input-number"></asp:TextBox>
                </div>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12">
                <asp:Literal ID="literarlMnu" runat="server"></asp:Literal>
              </div>
              <div class="col-xs-12 separador">
              </div>
              <div class="col-xs-12" style="text-align:center">
                  <asp:LinkButton ID="btn_grabarPerfil" runat="server" OnClick="btn_grabarPerfil_Click" ValidationGroup="nuevoDestino"
                                  CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk"></span>
                  </asp:LinkButton>
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" data-dismiss="modal" class="btn btn-primary" >
                <span class="glyphicon glyphicon-remove"></span>
              </button>
            </div>
          </div>
        </ContentTemplate>
        <Triggers>
          <asp:PostBackTrigger ControlID="btn_grabarPerfil" />
        </Triggers>
      </asp:UpdatePanel>
    </div>
  </div>
  <div class="modal fade" id="modalEliminar" data-backdrop="static" role="dialog" >
    <div class="modal-dialog" role="dialog">
      <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
          <div class="modal-content">
            <div class="modal-header">
              <h4>
                <asp:Label ID="lblRazonEliminacion" runat="server"></asp:Label>
              </h4>
            </div>
            <div class="modal-body">
              <asp:Label ID="msjEliminacion" runat="server"></asp:Label>
            </div>
            <div class="modal-footer">
              <asp:LinkButton ID="btnModalEliminar" runat="server" data-dismiss="modal" OnClick="btn_Eliminar_click"
                              CssClass="btn btn-primary">
                <span class="glyphicon glyphicon-ok"></span>
              </asp:LinkButton>
              <button type="button" data-dismiss="modal" class="btn btn-primary" >
                <span class="glyphicon glyphicon-remove"></span>
              </button>
            </div>
          </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" Runat="Server">
  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
      <asp:HiddenField ID="hf_checkMenus" runat="server" />
      <asp:HiddenField ID="hf_id" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" Runat="Server">
<script type="text/javascript">
    function modalPerfil() {
        $("#modalPerfil").modal();
    }

    function modalConfirmar() {
        $("#modalEliminar").modal();
    }

    function check(objeto) {
        var paginas = $get("<%=this.hf_checkMenus.ClientID %>").value;
        var arrpaginas = [];
        if (paginas != "")
            arrpaginas = paginas.split(",");
        var newpaginas = "";
        var idCheck = objeto.id.substring(objeto.id.indexOf("_") + 1);
        if (objeto.checked) {
            arrpaginas.push(idCheck);
        }
        else {
            var index = arrpaginas.indexOf(idCheck);
            arrpaginas.splice(index, 1);
        }
        newpaginas = arrpaginas.join();

        $get("<%=this.hf_checkMenus.ClientID %>").value = newpaginas;
    }

    function nocheck() {
        if ($get("<%=this.hf_checkMenus.ClientID %>").value = "") {
            alert("Debe seleccionar al menos un módulo");
            return false;
        }
        else
            return true;
    }

    function checkSorting() {
        if ($get("<%=this.hf_checkMenus.ClientID %>") != null) {
            var paginas = $get("<%=this.hf_checkMenus.ClientID %>").value;
            var paginaArray = paginas.split(",");
            for (var i = 0; i < paginaArray.length; i++) {
                var pag = paginaArray[i];
                if (pag != "") {
                    var chk = document.getElementById("menu_" + pag);
                    chk.checked = true;
                }
            }
        }
        validarCheckSinHijos();
    }

    function validarTieneHijosCheck(ordenPadre) {
        for (var i = ordenPadre + 1; i < ordenPadre + 100; i++) {
            var id = document.getElementById("orden_" + i);
            if (id != null && id != undefined) {
                var check = document.getElementById("menu_" + id.value);
                if (check.checked)
                    return true;
            }
            else {
                return false;
            }
        }
        return false;
    }

    function validarExisteOrden(orden) {
        var ordenHidden = document.getElementById("orden_" + orden);
        return (ordenHidden != null && ordenHidden != undefined)
    }

    function validarCheckSinHijos() {
        var checks = document.getElementsByName("menu");
        for (var i = 0; i < checks.length; i++) {
            var orden = checks[i].value;
            var numOrden = parseInt(orden);
            if (numOrden % 100 == 0) {
                var hijo = document.getElementById("menu_" + (numOrden + 1));
                if (!validarExisteOrden(numOrden + 1))
                    checks[i].disabled = false;
            }
        }
    }
  </script>
</asp:Content>