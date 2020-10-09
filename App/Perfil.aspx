<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true" CodeFile="Perfil.aspx.cs" Inherits="App_Perfil" %>

<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" runat="Server">
    <div class="col-lg-12 separador">
    </div>
    <h2>Maestro Perfil
    </h2>
</asp:Content>
<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-12 separador">
    </div>

    <div class="col-lg-2">
        Nombre
    <br />
        <asp:TextBox ID="txt_buscarNombre" runat="server" Width="90%" CssClass="form-control" />
    </div>
    <div class="col-lg-1">
        <br />
        <asp:LinkButton ID="btn_buscar" runat="server" CssClass="btn btn-primary" OnClick="btn_buscar_Click"
            ToolTip="Buscar Perfíl">
      <span class="glyphicon glyphicon-search" />
        </asp:LinkButton>
        <asp:LinkButton ID="btn_nuevo" runat="server" CssClass="btn btn-primary" ToolTip="Nuevo Perfíl"
            OnClick="btn_nuevo_Click">
      <span class="glyphicon glyphicon-plus" />
        </asp:LinkButton>
    </div>

</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="col-lg-12">
                <asp:GridView ID="gv_listar" runat="server" AllowPaging="True" AllowSorting="True" PageSize="8"
                    DataKeyNames="ID" CssClass="table table-bordered table-hover tablita" EmptyDataText="No Existen Perfiles!" AutoGenerateColumns="False"
                    OnRowCommand="gv_listar_RowCommand" OnSorting="gv_listar_Sorting"
                    OnRowDataBound="gv_listar_RowDataBound" OnPageIndexChanging="gv_listar_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="1%" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_modificar" CssClass="btn btn-xs btn-primary" runat="server" CommandArgument='<%# Eval("ID") %>'
                                    CommandName="MODIFICAR" ToolTip="Modificar">
                  <span class="glyphicon glyphicon-pencil" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="1%" ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btn_eliminar" CssClass="btn btn-xs btn-primary" runat="server" CommandArgument='<%# Eval("ID") %>'
                                    CommandName="ELIMINAR" ToolTip="Eliminar perfil">
                  <span class="glyphicon glyphicon-remove" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Id" DataField="ID" SortExpression="ID" Visible="false" />
                        <asp:BoundField HeaderText="Nombre" DataField="NOMBRE" SortExpression="NOMBRE" />
                        <asp:BoundField HeaderText="Descripción" DataField="DESCRIPCION" SortExpression="DESCRIPCION" />
                        <asp:BoundField DataField="CANTIDAD" ItemStyle-CssClass="ocultar" HeaderStyle-CssClass="ocultar" />
                        <asp:BoundField HeaderText="Nivel de permisos" DataField="NIVEL_PERMISOS" />
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Modals" ContentPlaceHolderID="Modals" runat="Server">
    <!-- Modal Perfíl-->
    <div id="modalPerfil" class="modal fade" data-backdrop="static" role="dialog">
        <div class="modal-dialog modal-lg" style="width: 1300px">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Datos Perfil
                            </h4>
                        </div>
                        <div class="modal-body" style="height: auto; overflow-y: auto;">
                            <div class="col-lg-2">
                                Nombre
                <br />
                                <asp:TextBox ID="txt_nombreEdita" runat="server" CssClass="form-control input-mayus" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_nombreEdita"
                                    Display="None" ErrorMessage="* Requerido" SetFocusOnError="True" ToolTip="Nombre Perfil"
                                    ValidationGroup="nuevoDestino">
                                </asp:RequiredFieldValidator>
                                <asp:ValidatorCalloutExtender ID="txt_nombreEdita_ValidatorCalloutExtender" runat="server"
                                    Enabled="True" TargetControlID="RequiredFieldValidator1">
                                </asp:ValidatorCalloutExtender>
                            </div>
                            <div class="col-lg-2">
                                Descripción
                <br />
                                <asp:TextBox ID="txt_perfilDescripcion" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-lg-2">
                                Nivel de permisos
                <br />
                                <asp:TextBox ID="txt_perfilPermisos" runat="server" CssClass="form-control input-number" />
                            </div>
                            <div class="col-lg-12 separador">
                            </div>
                            <div class="col-lg-12">
                                <asp:Literal ID="literarlMnu" runat="server"></asp:Literal>
                            </div>
                            <div class="col-lg-12 separador">
                            </div>
                            <div class="col-lg-12">
                                <center>
                  <asp:LinkButton ID="btn_grabar" runat="server" OnClick="btn_grabar_Click" ValidationGroup="nuevoDestino"
                                  CssClass="btn btn-primary">
                    <span class="glyphicon glyphicon-floppy-disk" />
                  </asp:LinkButton>
                </center>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
                                <span class="glyphicon glyphicon-remove" />
                            </button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade" id="modalEliminar" data-backdrop="static" role="dialog">
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
                            <asp:LinkButton ID="btnModalEliminar" runat="server" data-dismiss="modal"
                                CssClass="btn btn-primary">
                <span class="glyphicon glyphicon-ok" />
                            </asp:LinkButton>
                            <button type="button" data-dismiss="modal" class="btn btn-primary">
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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hf_checkMenus" runat="server" />
            <asp:HiddenField ID="hf_id" runat="server" />
            <asp:Button ID="btn_Eliminar" OnClick="btn_eliminar_click" runat="server" Text="Button" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Scripts" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
    function modalPerfil() {
        $("#modalPerfil").modal();
    }
    
    function modalConfirmar() {
        $("#modalEliminar").modal();
    }

    function eliminarPerfil() {
        var clickButton = document.getElementById("<%= btn_Eliminar.ClientID %>");
        clickButton.click();
    }
        
    function checkHijo(objeto) {
        var paginas = $get("<%=this.hf_checkMenus.ClientID %>").value;
        var arrpaginas = [];
        if (paginas != "")
            arrpaginas = paginas.split(",");
        var newpaginas = "";
        var valor = objeto.value;
        var idCheck = objeto.id.substring(objeto.id.indexOf("_") + 1);
        var ordenPadre = parseInt(valor) - (parseInt(valor) % 100);
        var id = document.getElementById("orden_" + ordenPadre);
        if (objeto.checked) {
            arrpaginas.push(idCheck);
            if (!document.getElementById("menu_" + id.value).checked) {
                document.getElementById("menu_" + id.value).checked = true;
                arrpaginas.push(id.value);
            }
        }
        else {
            arrpaginas.splice(arrpaginas.indexOf(idCheck), 1);
            var tieneHijos = validarTieneHijosCheck(ordenPadre);
            if (!tieneHijos) {
                document.getElementById("menu_" + id.value).checked = false;
                arrpaginas.splice(arrpaginas.indexOf(id.value), 1);
            }
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
