<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Master/MasterTms.master" CodeFile="YMS4.aspx.cs" EnableViewState="true" 
    Inherits="yms4"   %>
    <%@ Register Src="~/control_descarga.ascx" TagName="uc" TagPrefix="uc1" %>
  <%@ Reference Control="~/control_descarga.ascx"  %>    

    
<asp:Content ID="Titulo" ContentPlaceHolderID="titulo" Runat="Server">
 
    <div class="col-xs-12 separador">
    </div>
    <h2>
        Posiciones
    </h2>
</asp:Content>


<asp:Content ID="Filtro" ContentPlaceHolderID="Filtro" Runat="Server">
       <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
            <%--   <asp:LinkButton CssClass="deslizar btn btn-primary" ID="deslizar" Text="" runat="server">
                 <span class="glyphicon glyphicon-play"></span>
            </asp:LinkButton>--%>
            <div id="deslizar" class="material-switch pull-right">
                <input id="someSwitchOptionPrimary" name="someSwitchOption001" type="checkbox" />
                <label for="someSwitchOptionPrimary" class="label-primary">
                </label>
            </div>
            <%--  <asp:ImageButton ID="deslizar" CssClass="deslizar" runat="server" ImageUrl="img/flecha.png"
                ToolTip="Deslizar" Width="50px" Height="50px" />--%>
        </div>


        <div class="col-lg-9 col-md-8 col-sm-7 col-xs-6">
             

               <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true"  >
               <ContentTemplate>
                    
                 Filtros
                <asp:panel runat="server" ID="camio_site" >
          
           <asp:Panel ID="SITE" runat="server" >
             Site
            <asp:DropDownList runat="server" id="dropsite" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="drop_SelectedIndexChanged"  ></asp:DropDownList>
             </asp:panel>
             <asp:Button runat="server" ID="nozzoom"  ClientIDMode="Static" OnClientClick="guarda();" Text="Recarga" Visible="true" OnClick="recargar_yms" />
            
           
              </asp:Panel>
     
               </ContentTemplate>
               </asp:UpdatePanel>
              
        </div>

        <div class="col-lg-2 col-md-3 col-sm-4 col-xs-5" style="padding-left: 0px;">
            <h2>
                <img id="IMGCLIENTE" runat="server" src="" alt="" width="145" /></h2>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
        </div>
        <div id="wrapper">
            <div id="sidebar-wrapper" class="NavLa monitoreo_unico">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: 0px; padding-right: 0px;
                    background-color: white; border-radius: 6px;">
                    <ul class="nav nav-tabs">
                        <li><a data-toggle="tab" href="#menu1">Playas</a></li>
                  
                    </ul>
                    <div class="tab-content">
                        <div id="menu1" class="tab-pane fade in active">
                        <div class="col-xs-12 separador" ></div>

                        <div class="col-xs-12">
                       <asp:UpdatePanel runat="server" ID="uptrailer" >
                       <ContentTemplate>
                         Trailer:
                                      <asp:DropDownList ID="trailers" runat="server" AutoPostBack="false" ClientIDMode="Static"  ></asp:DropDownList>
                       </ContentTemplate>

                       </asp:UpdatePanel>
                            
                        </div>

                            <div class=" col-xs-12" style="padding-left: 0px; padding-right: 0px; height:55px;">
                                <center>
                                    <h5>
                                        Listado de Playas</h5>
                                </center>
                            </div>
                           
                            <div class=" col-xs-12" style="padding-left: 0px; padding-right: 0px;">
                                <div class=" col-xs-4">
                                    <asp:UpdatePanel ID="UPBM" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <ContentTemplate>


                                            <asp:CheckBox ID="chktodos" runat="server"  Text="Todos"  Checked="true" ClientIDMode="Static" />
                                        </ContentTemplate>


                                    </asp:UpdatePanel>
                                </div>

                            <div class="" style=" width:100%; padding-left: 0px; padding-right: 0px; overflow: scroll;height:317px;" id="detalleplayas">
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                     
                                    <asp:CheckBoxList ID="rlplayas" runat="server" CheckBoxes="true"  ClientIDMode="Static"
                                            CssClass="radlist"  OnClientItemChecked="OnClientItemChecked" 
                                            Visible="true" Width="100%">
                                    </asp:CheckBoxList>

                             

                                    </ContentTemplate>
                                    <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dropsite" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                                                                                      
                            
                         </div>

                         </div>


                        <div id="menu2" class="tab-pane fade">
                            <div class=" col-xs-12" style="padding-left: 0px; padding-right: 0px;">
                                <div class=" col-xs-6 col-sm-6">
                                    Tipo
                                    <br />
                                    <asp:DropDownList ID="drop_tipo" runat="server" CssClass="drop" Width="100%" ></asp:DropDownList>
                                </div>
                                <div class=" col-xs-6 col-sm-6">
                                    Código
                                    <br />
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="textbox form-control" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            </div>
                            <div class=" col-xs-12" style="padding-left: 0px; padding-right: 0px;">
                                <div class=" col-xs-6">
                                    Nombre
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox form-control" Width="100%"></asp:TextBox>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class=" col-xs-4">
                                    <br />
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:LinkButton ID="BtnBuscarPI" Text="" runat="server" ToolTip="Buscar" CssClass="btn btn-primary"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <%--   <asp:UpdatePanel ID="UPBPI" runat="server">
                                        <ContentTemplate>
                                            <asp:ImageButton ID="BtnBuscarPI" CssClass="ocultar" runat="server" ImageUrl="img/img_boton/boton_blancos/32x32/buscar.png"
                                                ToolTip="Buscar" Width="32px" Height="32px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            </div>
                            <div class=" col-xs-12" style="padding-left: 0px; padding-right: 0px; overflow: scroll;
                                height: 400px!important">
                                <asp:Label ID="TablaPto" runat="server"></asp:Label>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtID" runat="server" CssClass="ocultar"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
<asp:Content ID="Contenido" ContentPlaceHolderID="Contenedor" Runat="Server" ClientIDMode="Static">

    
 
 

            <div id="contenedor_mapa" style="width:100%; height: 100%; " >
      <asp:UpdatePanel runat="server" ID="CD" style="height:100%"  >
    <ContentTemplate>
    



   <div id="zoomG" style="height:100%">
  
    <div id="zoom" class="zoom" style=" overflow:hidden; border:0px solid black; border-radius: 6px; height:100%; ">


  

   <asp:Panel ID="keypanel" runat="server" CssClass="panel_indicadores" ClientIDMode="Static" >
    Nº Ocupados:  <asp:Label runat="server" ID="ocupados"></asp:Label>  <br /> 
    Nº Desocupados: <asp:Label runat="server" ID="desocupados"></asp:Label>  

   </asp:Panel>
   <asp:Panel ID="lugarpanel" runat="server" CssClass="panel_lugar" ClientIDMode="Static" >
   playa:<asp:Label runat="server" ID="lugarpanel_playa" ClientIDMode="Static"></asp:Label>  <br /> 
   posicion:<asp:Label runat="server" ID="lugarpanel_codigo" ClientIDMode="Static"></asp:Label> <br /> 
   Trailer:<asp:Label runat="server" ID="lugarpanel_trailer" ClientIDMode="Static"></asp:Label>
   
   </asp:Panel>

               

   	<canvas id="myCanvas" style="height:50%; width:100%;border-radius: 6px; ">
		Your browser doesn't support canvas, fool.
	</canvas>
        


   <asp:panel  CssClass="large mostrado" runat="server" ClientIDMode="Static" id="large"  >

      </asp:panel>

   <asp:Image ID="siteimage" ClientIDMode="Static" runat="server" style="height:100%; width:100%;border-radius: 6px; display:none; " />
   <asp:panel ID="controles" runat="server"  CssClass="controles"  ClientIDMode="Static" >
               <asp:Image ID="maximizar" ClientIDMode="Static" runat="server"  ImageUrl="~/images/iconos/add.png" OnClientClick="maximizar();"   />
  <asp:Button runat="server" ClientIDMode="Static" ID="zin" OnClientClick="zoomIn();"   Text="Zoom In" Visible="false" />
   <span ID="zoomIn" class="glyphicon glyphicon-plus-sign" ></span>
              <span ID="zoomOut" class="glyphicon glyphicon-minus-sign" />
    
    
     </asp:panel>
   </div>

   </div>

   <asp:HiddenField ID="_ultX" runat="server"  />
    <asp:HiddenField ID="_ultY" runat="server" />
    <asp:HiddenField ID="_ultscale" runat="server"  Value="1" />
    <asp:HiddenField ID="_cambio_mapa" runat="server"  Value="0" ClientIDMode="Static" />
  
        <asp:HiddenField ID="id_lugar_seleccionado" runat="server"  ClientIDMode="static" />
    <asp:HiddenField ID="playa_seleccionada" runat="server" ClientIDMode="static" />
    <asp:HiddenField ID="lugar_seleccionado" runat="server"  ClientIDMode="static" />
     <asp:HiddenField ID="trailer_seleccionado" runat="server"  ClientIDMode="static" />
     <asp:HiddenField ID="id_destino_seleccionado" runat="server"  ClientIDMode="static" />
    <asp:HiddenField ID="Playa_seleccionada_destino" runat="server" ClientIDMode="static" />
    <asp:HiddenField ID="Lugar_seleccionado_destino" runat="server"  ClientIDMode="static" />
    <asp:HiddenField ID="id_destino_formulario" runat="server"  ClientIDMode="static" />
    </ContentTemplate>
    <Triggers>
    
        <asp:AsyncPostBackTrigger  ControlID="Timer_recarga" EventName="Tick" />
  
    </Triggers>
    </asp:UpdatePanel>
      <asp:Timer runat="server" ID="Timer_recarga" ClientIDMode="Static" OnTick="recargar_yms" Interval="600000"  ></asp:Timer>
           
        </div>

 

              
        <div id="ModalFiltro" class="modal fade" role="dialog">
            <div class="modal-dialog ">
                <!-- Modal content-->



                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">
                            Movimiento
                        </h4>
                    </div>
                    <div class="modal-body modal-cuerpo">
                     
                            <asp:UpdatePanel ID="UPT" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                 <asp:button id="btn_sol" runat="server" onclick="llenaformulario" AutoPostBack="true" ClientIDMode="Static" style="visibility:hidden;display:none" />
                               <asp:dropdownlist id="solicitudMovimiento" runat="Server" OnSelectedIndexChanged="llenaformulario" AutoPostBack="true" ClientIDMode="Static" >
                              
                 <asp:ListItem Text="Solicitud" Value="1">      </asp:ListItem> 
                 <asp:ListItem Text="Movimiento" Value="2">      </asp:ListItem> 
                
                </asp:dropdownlist>
                <asp:Panel ID="uptform" runat="server" ClientIDMode="Static" >
                    <uc1:uc runat="server" ID="solicud_descarga" Visible="false" /> 

                </asp:Panel>
                 <div class="col-xs-12">
        <center>
                   <asp:Button ID="btn_confirmar" CssClass="btn btn-primary" runat="server"  OnClick="confirmar"
                Text="Confirmar Movimiento" />
        </center>
        </div>
        

                                </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="solicitudmovimiento" EventName="SelectedIndexChanged" />
                                    </Triggers>
                            </asp:UpdatePanel>
                    
                        <br />
                    
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">
                            Cerrar</button>
                    </div>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0"  >
                        <ProgressTemplate>
                            <div class="overlay" />
                            <div class="overlayContent">
                                <center>
                                    <img src="img/progress_mmpfq.gif" alt="" />
                                </center>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
            </div>
        </div>
        
  
          <script type="text/javascript">

              $(document).ready(function (e) {

                  carga_canvas();
                  $("#someSwitchOptionPrimary").click(function (e) {
                      //e.preventDefault();
                      $("#wrapper").toggleClass("toggled");
                  });

                  tick_recarga = setInterval(click_recarga, 60000);
              //    selecciona(lugarseleccionado);
              });

              function recarga() {

              //    reOffset();
                  carga_canvas();

                  //   drawCanvas();
                  selecciona(lugarseleccionado);
                  // traspasacanvas1();



              }

              Sys.WebForms.PageRequestManager.getInstance().add_endRequest(recarga);




	</script>

   
  



</asp:Content>

 

