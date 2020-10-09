<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterTms.master" AutoEventWireup="true"
    CodeFile="YMS_editorb.aspx.cs" Inherits="App_YMS_editorb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titulo" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Filtro" runat="Server">
    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
        <div id="deslizar" class="material-switch pull-right">
            <input id="someSwitchOptionPrimary" name="someSwitchOption001" type="checkbox" />
            <label for="someSwitchOptionPrimary" class="label-primary">
            </label>
        </div>
    </div>
    <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
        <h2>
            Filtros
        </h2>
    </div>
    <div class="col-lg-1">
        <div class="col-lg-12 separador">
        </div>
        Site
    </div>
    <asp:Panel CssClass="col-lg-1" runat="server" ID="camio_site">
        <div class="col-lg-12 separador">
        </div>
        <asp:DropDownList runat="server" ID="dropsite" ClientIDMode="Static" AutoPostBack="true"
            OnSelectedIndexChanged="drop_SelectedIndexChanged">
        </asp:DropDownList>
    </asp:Panel>
    <div class="col-lg-2 col-md-3 col-sm-4 col-xs-5" style="padding-left: 0px;">
        <h2>
            <img id="IMGCLIENTE" runat="server" src="" alt="" width="145" />
        </h2>
    </div>
    <div class="col-lg-1">
        <div class="col-lg-12 separador">
        </div>
        <asp:CheckBox ID="tamano" runat="server" ClientIDMode="Static" Text="tamaño" />
    </div>
    <div class="col-lg-1">
        <div class="col-lg-12 separador">
        </div>
        <asp:TextBox runat="server" ID="orientacion" Text="1" ClientIDMode="Static"></asp:TextBox>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenedor" runat="Server">
  <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
  </div>
  <div id="wrapper">
    <div id="sidebar-wrapper" style="margin-top:0px;height:450px;" class="NavLa monitoreo_unico">
      <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="padding-left: 0px; padding-right: 0px;
           background-color: white; border-radius: 6px;">
        <ul class="nav nav-tabs">
          <li>
            <a data-toggle="tab" href="#menu1">Playas</a>
          </li>
          <li>
            <a data-toggle="tab" href="#menu2">Lugares</a>
          </li>
        </ul>
        <div class="tab-content">
          <div id="menu1" class="tab-pane fade in active">
            <div class=" col-xs-12" style="padding-left: 0px; padding-right: 0px; height: 55px;">
              <center>
                <h5>
                  Listado de Playas
                </h5>
              </center>
            </div>
            <div class=" col-xs-12" style="padding-left: 0px; padding-right: 0px;">
              <div class=" col-xs-4">
                <asp:UpdatePanel ID="UPBM" runat="server">
                  <contenttemplate>
                    <asp:CheckBox ID="chktodos" runat="server"  Text="Todos"  Checked="true" ClientIDMode="Static" />
                  </contenttemplate>
                </asp:UpdatePanel>
              </div>
              <div class=" col-xs-12" style="padding-left: 0px; padding-right: 0px; overflow-y: auto;
                   height: 315px!important">
                <asp:Label ID="Label1" runat="server"></asp:Label>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                  <contenttemplate>
                    <asp:CheckBoxList ID="rlplayas" runat="server" CheckBoxes="true"  ClientIDMode="Static"
                                      CssClass="radlist"  OnClientItemChecked="OnClientItemChecked" 
                                      Visible="true" Width="100%">
                    </asp:CheckBoxList>
                  </contenttemplate>
                </asp:UpdatePanel>
              </div>
            </div>
          </div>
          <div id="menu2" class="tab-pane fade">
            <div class=" col-xs-12" style="padding-left: 0px; padding-right: 0px;">
              <div class=" col-xs-6 col-sm-6">
                Tipo
                <br />
                <asp:DropDownList ID="drop_tipo" runat="server" CssClass="drop" Width="100%">
                </asp:DropDownList>
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
                  <contenttemplate>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox form-control" Width="100%"></asp:TextBox>
                  </contenttemplate>
                </asp:UpdatePanel>
              </div>
              <div class=" col-xs-4">
                <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <contenttemplate>
                    <asp:LinkButton ID="BtnBuscarPI" Text="" runat="server" ToolTip="Buscar" CssClass="btn btn-primary">
                      <span class="glyphicon glyphicon-search"></span>
                    </asp:LinkButton>
                  </contenttemplate>
                </asp:UpdatePanel>
              </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            </div>
            <div class=" col-xs-12" style="padding-left: 0px; padding-right: 0px; overflow: scroll;
                 height: 400px!important">
              <asp:Label ID="TablaPto" runat="server"></asp:Label>
              <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <contenttemplate>
                  <asp:TextBox ID="txtID" runat="server" CssClass="ocultar"></asp:TextBox>
                </contenttemplate>
              </asp:UpdatePanel>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
    <div id="contenedor_mapa" style="width: 100%; height: 70vh;">
      <asp:UpdatePanel runat="server" ID="CD">
        <contenttemplate>
          <asp:HiddenField ID="_ultX" runat="server"  />
          <asp:HiddenField ID="_ultY" runat="server" />
          <asp:HiddenField ID="_ultscale" runat="server"  Value="1" />

          <div id="zoomG">

            <div id="zoom" class="zoom" style=" overflow:hidden; border:0px solid black; border-radius: 6px; ">

              <asp:Panel ID="keypanel" runat="server" CssClass="panel_indicadores" >
                Numero de Estacionamientos ocupados:<br /> 
                Numero de Estacionamientos Desocupados:

              </asp:Panel>
              <asp:Panel ID="lugarpanel" runat="server" CssClass="panel_lugar" >
                playa:<asp:Label runat="server" ID="lugarpanel_playa" ClientIDMode="Static"></asp:Label>  <br /> 
                posicion:<asp:Label runat="server" ID="lugarpanel_codigo" ClientIDMode="Static"></asp:Label>

              </asp:Panel>

              <canvas id="myCanvas" style="height:70vh; width:100%;border-radius: 6px; ">
                Your browser doesn't support canvas, fool.
              </canvas>
              <%--<p>
              Click and drag to move the image. <button type="button" id="resetPos">
              Reset Position
              </button>
              </p>
              <p>
              Zoom:

              <button type="button" id="resetZoom">
              Reset Zoom
              </button>
              </p>--%>

              <asp:panel  CssClass="large mostrado" runat="server" ClientIDMode="Static" id="large"  >

              </asp:panel>
              <asp:Image ID="siteimage" ClientIDMode="Static" runat="server" style="height:100%; width:100%;border-radius: 6px; display:none; " />
              <asp:panel ID="controles" runat="server"  CssClass="controles" >
                <span ID="zoomIn" class="glyphicon glyphicon-plus-sign" ></span>
                <span ID="zoomOut" class="glyphicon glyphicon-minus-sign" ></span>
                <%--<asp:Image ID="zoomIn" ClientIDMode="Static" runat="server"  ImageUrl="~/images/iconos/accept.png" />
                <asp:Button runat="server" ID="zin" OnClientClick="zoomIn();"   Text="Zoom In" Visible="false" />
                <asp:Button runat="server" ID="nozzoom" OnClientClick="guarda();" Text="No Zoom " Visible="true" OnClick="recagar" />

                <asp:Image ID="zoomOut" runat="server"   ImageUrl="~/images/iconos/close.png"   />--%>

              </asp:panel>
              <asp:panel ID="cambio_mapa" runat="server"  CssClass="cambiomapa" >
                <%--<asp:Image ID="Image2" runat="server" onclick="cambio_mapa()"  ImageUrl="~/images/iconos/add.png"   />--%>

              </asp:panel>
            </div>
          </div>
        </contenttemplate>
      </asp:UpdatePanel>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Modals" runat="Server">
  <div id="ModalFiltro" class="modal fade" role="dialog">
    <div class="modal-dialog ">
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">
            &times;
          </button>
          <h4 class="modal-title">
            Movimiento
          </h4>
        </div>
        <div class="modal-body modal-cuerpo">
          <div class="col-xs-6" style="overflow: scroll; height: 300px!important; overflow-x: hidden;">
            <asp:UpdatePanel ID="UPT" runat="server">
              <contenttemplate>
                <asp:Label ID="TablaTrans" runat="server"></asp:Label>
              </contenttemplate>
            </asp:UpdatePanel>
          </div>
          <div class="col-xs-6" style="overflow: scroll; height: 300px!important; overflow-x: hidden;">
            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>

            </ContentTemplate>
            </asp:UpdatePanel>--%>
            <asp:Label ID="TablaPats" runat="server"></asp:Label>
          </div>
          <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
          </div>
          <br />
          <asp:UpdatePanel ID="UPMG" runat="server">
            <contenttemplate>
              <div class="col-xs-6">
                <div class="col-xs-6">
                  <asp:Label ID="lbTipovehiculo" runat="server" Text="Tipo:" />
                  <asp:dropdownlist ID="ListaTPV" runat="server" Width="100%" AutoPostBack="true"
                                    MarkFirstMatch="true" CssClass="drop">
                  </asp:dropdownlist>
                </div>
                <div class="col-xs-6">
                  <asp:Label ID="lbMovil" runat="server" Text="Patente: " />
                  <asp:dropdownlist ID="ListaMoviles" runat="server" Width="100%" MarkFirstMatch="true"
                                    CssClass="drop">
                  </asp:dropdownlist>
                </div>
              </div>
              <div class="col-xs-6">
                <br />
                <div class="btn-group btn-group-justified ">
                  <asp:LinkButton CssClass="btn btn-primary" type="button" ID="BtnBuscarTipo_mod1"
                                  Text="" runat="server" ToolTip="Buscar">
                    <span class="glyphicon glyphicon-search"></span>
                  </asp:LinkButton>
                  <asp:LinkButton CssClass="btn btn-primary" type="button" ID="BtnFiltrar_mod1" Text=""
                                  runat="server" ToolTip="Filtrar">
                    <span class="glyphicon glyphicon-filter"></span>
                  </asp:LinkButton>
                  <asp:LinkButton CssClass="btn btn-primary" type="button" ID="BtnOK_mod1" Text=""
                                  runat="server" ToolTip="Aceptar">
                    <span class="glyphicon glyphicon-ok" style="color:White;"></span>
                  </asp:LinkButton>
                </div>
              </div>
              <asp:TextBox ID="txtTrans" runat="server" CssClass="ocultar"></asp:TextBox>
              <asp:TextBox ID="txtPats" runat="server" CssClass="ocultar"></asp:TextBox>
            </contenttemplate>
          </asp:UpdatePanel>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-primary" data-dismiss="modal">
            Cerrar</button>
        </div>
      </div>
    </div>
  </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ocultos" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="scripts" runat="Server">
    <style type="text/css">
        #zoomG
        {
            width: 95%;
            height: 80%;
        }
        .zoom
        {
            position:relative;
            width: 100%;
            height: 100%;
        }
        #large
        {
            width: 100%;
            height: 100%;
            border-radius: 6px;
            padding-right: 5px;
            padding-left: 5px;
            background-size: contain;
            background-repeat: no-repeat;
            display: none;
            width: 100%;
            height: 100%;
        }
        /*  .small img { width: 6px; height: 25px;  } */
        .small td
        {
            font-size: x-small;
            border: 1px solid black;
        }
        /* .large img {    filter: drop-shadow(0 2px 4px rgba(0,0,0,.5)); margin-left:auto; margin-right:auto; margin-top:auto; margin-bottom:auto;
      }
      */
        .lugar
        {
            filter: drop-shadow(0 2px 4px rgba(0,0,0,.5));
            margin-left: auto;
            margin-right: auto;
            margin-top: auto;
            margin-bottom: auto;
            transform: transale3d(0,0,0);
        }
        .patente_1
        {
            position: absolute;
            display: inline-block;
            -webkit-transform: rotate(-90deg);
            text-align: center;
            font-size: 3px;
            color: orange;
            z-index: 2; /* margin-left:3px;
      margin-bottom:3px;*/
            width: 3px;
            -webkit-transform-origin: 3.8px;
            margin-top: 10px;
            filter: drop-shadow(0 2px 4px rgba(0,0,0,.5));
            transform: transale3d(0,0,0);
        }
        .patente_2
        {
            position: absolute;
            display: inline-block;
            text-align: center;
            font-size: 3px;
            color: orange;
            z-index: 2;
            margin-top: 2px;
            margin-left: 3px;
            width: 3px;
            filter: drop-shadow(0 2px 4px rgba(0,0,0,.5));
            transform: transale3d(0,0,0);
        }
        .icono
        {
            vertical-align: middle;
            transform: transale3d(0,0,0);
        }
        .selecciona
        {
            background-color: green;
            border-radius: 2px;
            -moz-border-radius: 2px;
        }
        .selecciona_destino
        {
            background-color: blue;
            border-radius: 2px;
            -moz-border-radius: 2px;
        }
        .pallet_izq
        {
            /*    position:absolute; 
      margin-left:-3px;
      margin-right:auto;
      margin-top:4px;
      margin-bottom:auto;  */
        }
        .ottawa_der
        {
            /*          position:absolute;    
      margin-left:28px;
      margin-right:auto;
      margin-top:4px;
      margin-bottom:auto;  */
        }
        .ottawa_up
        {
            /*      position:absolute;    
      margin-left:3px;
      margin-right:auto;
      margin-top:-5px;
      margin-bottom:auto;  
      */
        }
        .pallet_down
        {
            /*   margin-left:3px;
      */
        }
        .tabla_lugar
        {
            /*   border:1px solid white; */
            width: 100%;
            height: 100%;
        }
        .tabla_lugar td
        {
            /*      border:1px solid white; */
        }
        .large td
        {
            font-size: x-large;
        }
        .escondido
        {
            visibility: hidden;
            display: none;
        }
        .mostrado
        {
            visibility: visible;
            display: block;
            height: 100%;
            width: 100%;
        }
        .mostradogrande
        {
            visibility: visible;
            display: block;
            height: 300%;
            width: 300%;
            background-size: cover;
        }
        .zona
        {
            position: absolute;
            transform: transale3d(0,0,0);
        }
        .tool
        {
            display: none;
            position: absolute;
            border: 1px solid #333;
            background-color: blue;
            border-radius: 5px;
            padding: 10px;
            color: #fff;
            font-size: 12px Arial;
            opacity: 0.5;
        }
        .tool1
        {
            position: absolute;
            border: 1px solid #333;
            background-color: blue;
            border-radius: 5px;
            padding: 10px;
            color: #fff;
            font-size: 12px Arial;
            opacity: 0.5;
        }
        .panel_indicadores
        {
            position: absolute;
            border: 1px solid #333;
            background-color: blue;
            border-radius: 5px;
            padding: 10px;
            color: #fff;
            font-size: 12px Arial;
            opacity: 0.5;
            z-index: 2;
            top:15px;
            left:15px;
            margin-top:0px;
        }
        .controles
        {
            position: absolute;
            border: 1px solid #333;
            background-color: blue;
            border-radius: 5px;
            padding: 7px;
            width:40px;
            color: #fff;
            font-size: 12px Arial;
            opacity: 0.5;
            z-index: 2;
            right:15px;
            bottom:15px;
        }
        .controles > .glyphicon
        {
            font-size:x-large;
            margin-bottom:5px;
            margin-top:5px;
        }
        .cambiomapa
        {
            position: absolute;
            border: 1px solid #333;
            background-color: blue;
            border-radius: 5px;
            padding: 10px;
            color: #fff;
            font-size: 12px Arial;
            opacity: 0.5;
            z-index: 2;
            margin-top: 10px;
            margin-left: 600px;
        }
        .panel_lugar
        {
            position: absolute;
            border: 1px solid #333;
            background-color: blue;
            border-radius: 5px;
            padding: 10px;
            color: #fff;
            font-size: 12px Arial;
            opacity: 0.5;
            z-index: 2;
            right:15px;
            top:15px;
            margin-top:0px;
        }
    </style>
    <script type="text/javascript">
        var notooltip = 0;

        $(function () {
            $.contextMenu({
                selector: '.zona',
                events: {
                    show: function (opt, x, y) {
                        $('.tool').remove();
                    },
                    hide: function (opt, x, y) {
                        notooltip = 1;
                    }
                },
                callback: function (key, options) {
                    var m = "clicked: " + key;
                    //  window.console && console.log(m) || alert(m);
                    grabaplaya(this);
                },
                items: {
                    "grabar": {
                        name: "grabar",
                        icon: "cut"
                    }

                }
            });

        });

        $(function () {

            $.contextMenu({
                selector: '.context-menu-one',
                animation: {
                    duration: 500,
                    show: "fadeIn",
                    hide: "fadeOut"
                },
                events: {
                    show: function (opt, x, y) {
                        $('.tool').remove();
                        selecciona(this[0]);
                    },
                    hide: function (opt, x, y) {
                        notooltip = 1;
                    }
                },
                build: function ($trigger, e) {
                    //       window.console && console.log($trigger) || alert($trigger); 

                    return {
                        callback: function (key, options) {

                            //     var m = "clicked: " + key;
                            //    window.console && console.log(m) || alert(m); 
                            destino($('#' + key));

                        },

                        items: getMenuItem($trigger)
                    }
                }
                //             items: {
                //                    "edit": { name: "Edit2", icon: "edit" }}
            });

        });

        var $id;

        function getMenuItem(mytrigger) {
            //1st try
            var $menu = {};
            var pagePath = window.location.pathname + "/GetMenu";
            $id = $(mytrigger);
            $.ajax({
                url: pagePath,
                type: "GET",
                data: 'usuario="' + mytrigger[0].id + '"',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                },
                success: function (result) {

                    if (result.isOk == false) {
                        alert(result.message);
                    } else {
                        for (var i = 0; i < result.d.length; i++) {
                            if (result.d[i].items != 'undefined') {
                                $menu[result.d[i].ID.toString()] = {
                                    name: result.d[i].Nombre.toString(),
                                    icon: result.d[i].icono.toString(),
                                    items: armasubitmesitem(result.d[i])
                                };
                            } else {
                                $menu[result.d[i].ID.toString()] = {
                                    name: result.d[i].Nombre.toString(),
                                    icon: result.d[i].icono.toString()
                                };
                            }

                            //want to control disabling and enable base on data[i].isEnabled also.
                        }
                        // alert($menu); // get as required.

                        //                          var _offset = $id.offset(),
                        //                            position = {
                        //                          x: _offset.left + 10, 
                        //                          y: _offset.top + 10
                        //                             }
                        //                            $id.contextMenu('hide');
                        //                            var yo={selector: '.context-menu-one',
                        //                                    animation: {duration: 500, show: "fadeIn", hide: "fadeOut"},
                        //                                    items: $menu};

                        //                          $id.data('runCallbackThingie', yo);
                        //                          $id.length=0;
                        //                          $id.contextMenu(position)

                    }

                },
                async: false
            });
            return $menu;
        }

        function armasubitmesitem(item) {
            var $subitems = {};
            for (var i = 0; i < item.subitems.length; i++) {
                $subitems[item.subitems[i].ID.toString()] = {
                    name: item.subitems[i].Nombre.toString(),
                    icon: item.subitems[i].icono.toString()
                };
            }

            return $subitems;
        }

        $(function () {
            $.contextMenu({
                selector: '.context-menu-two',
                animation: {
                    duration: 500,
                    show: "fadeIn",
                    hide: "fadeOut"
                },
                events: {
                    show: function (opt, x, y) {
                        $('.tool').remove();
                        selecciona();
                    },
                    hide: function (opt, x, y) {
                        notooltip = 1;
                    }
                },
                callback: function (key, options) {
                    var m = "clicked: " + key;
                    window.console && console.log(m) || alert(m);
                },
                items: {
                    "edit": {
                        name: "Edit2",
                        icon: "edit"
                    },
                    "cut": {
                        name: "Cut2",
                        icon: "cut"
                    },
                    "copy": {
                        name: "Copy2",
                        icon: "copy"
                    },
                    "paste": {
                        name: "Paste2",
                        icon: "paste"
                    },
                    "delete": {
                        name: "Delete2",
                        icon: "delete"
                    },
                    "sep1": "---------",
                    "quit": {
                        name: "Quit2",
                        icon: function () {
                            return 'context-menu-icon context-menu-icon-quit';
                        }
                    }
                }
            });

            $('.context-menu-two').on('click', function (e) {

                console.log('clicked', this);
            })
        });

        var updateLastScale = function () {
            lastScale = scale;
            $('#_ultscale').val(scale);
        };

        var lugarseleccionado;

        function selecciona(elemento) {

            // var cambio = false;

            //if (elemento != lugardestino) cambio = true;

            destino();
            //     alert(elemento);

            var ultimo = $("#id_lugar_seleccionado").val();

            // if (ultimo!='') ($('#'+ultimo).parent().removeClass('selecciona'));

            $("#id_lugar_seleccionado").val('');
            $("#playa_seleccionada").val('');
            $("#lugar_seleccionado").val('');

            if (elemento == undefined) {
                $('#lugarpanel').fadeOut();
                lugarseleccionado = null;
                return;
            }

            lugarseleccionado = elemento;
            var $elemento = $(elemento);
            // if (!$elemento.is('img')) $elemento=$elemento.find( ".lugar" );
            //  $elemento.parent().addClass('selecciona');
            $("#id_lugar_seleccionado").val($elemento.attr('id'))
            $("#playa_seleccionada").val($elemento.attr('playa') + '(' + $elemento.attr('SUPL_ID') + ')');
            $("#lugar_seleccionado").val($elemento.attr('codigo_lugar'));

            $('#lugarpanel_playa').text($elemento.attr('playa') + '(' + $elemento.attr('SUPL_ID') + ')' + '(' + $elemento.attr('lugares') + ')');
            $('#lugarpanel_codigo').text($elemento.attr('codigo_lugar'));
            if (cambio) {
                $('#orientacion').val($elemento.attr('rotacion'));
            }
            $('#lugarpanel').fadeIn()
        }

        var lugardestino;

        function destino(elemento) {

            var ultimo = $("#id_destino_seleccionado").val();

            if (ultimo != '') {
                ($('#' + ultimo).parent().removeClass('selecciona_destino'));
            }

            $("#id_destino_seleccionado").val('');
            $("#Playa_seleccionada_destino").val('');
            $("#Lugar_seleccionado_destino").val('');

            if (elemento == undefined) {
                lugardestino = null;
                return;
            }

            lugardestino = elemento;
            var $elemento = $(elemento);
            if (!$elemento.is('img')) {
                $elemento = $elemento.find(".lugar");
            }
            //   alert($elemento.parent());
            $elemento.parent().addClass('selecciona_destino');
            $("#id_destino_seleccionado").val($elemento.attr('id'))
            $("#Playa_seleccionada_destino").val($elemento.attr('playa'));
            $("#Lugar_seleccionado_destino").val($elemento.attr('codigo_lugar'));
            $("#ModalFiltro").modal();

        }

        var disableImgEventHandlers = function () {
            var events = [
            'onclick', 'onmousedown', 'onmousemove', 'onmouseout', 'onmouseover',
            'onmouseup', 'ondblclick', 'onfocus', 'onblur'
        ];
            events.forEach(function (event) {
                img[event] = function () {
                    return false;
                };
            });
        };

        var hammer;

        $(document).ready(function (e) {

            carga_canvas();
            $("#someSwitchOptionPrimary").click(function (e) {
                //e.preventDefault();
                $("#wrapper").toggleClass("toggled");
            });

        });

        var xelemento;
        var yelemento;

        function drag(e) {
            e.stopImmediatePropagation();
            e.dataTransfer.setData("text", e.target.id);
            xelemento = Math.round(e.clientX);
            yelemento = Math.round(e.clientY);
            updateLastPos();
            selecciona(e.currentTarget);

        }

        function end_drag(e) {

            var ultimo = $("#id_destino_seleccionado").val();

            if (ultimo != '' && $("#modalenviara").css('display') == 'none') {

                //             $( "#contenedor_mapa" ).effect( "size", {
                //                         to: { width: 1198 }
                //                    }, 1000 );
                //                
                //     $( "#modalenviara" ).toggle( "slide" );

                $("#ModalFiltro").modal();

            }
        }

        function fin_seleccion() {
            selecciona();
            if ($("#modalenviara").css('display') != 'none') {

                $("#contenedor_mapa").effect("size", {
                    to: {
                        width: 1398
                    }
                }, 1000);

                $('#modalenviara').toggle('slide');

            }
        }

        function allowDrop(ev) {
            ev.stopImmediatePropagation();
            destino(ev.currentTarget);
            ev.preventDefault();
            updateLastPos();

        }

        var context;
        var delta = [0, 0];
        var lugares = [];
        var playas = [];
        var originx = 0;
        var originy = 0;
        var visibleWidth;
        var visibleHeight;
        var width;
        var height;
        var image;
        var canvas;
        var drawPos;
        var scale;
        // Canvas
        var ArrastrandoMapa = false;
        var lugararrastrado = null;
        var mousePos = [0, 0];
        var DEFAULT_ZOOM = 0.2;
        var MAX_ZOOM = 3;
        var MIN_ZOOM = 0.2;
        var ZOOM_STEP = .1;
        var DRAW_POS = [0, 0];

        function guarda() {
            DRAW_POS = drawPos;
            // alert(DRAW_POS);
        }

        function carga_canvas() {

            lugares = [];
            playas = [];

            canvas = document.querySelector("#myCanvas");
            context = canvas.getContext("2d");

            canvas.height = $("#myCanvas").height();
            canvas.width = $("#myCanvas").width();
            var BB = canvas.getBoundingClientRect();
            offsetX = BB.left;
            offsetY = BB.top;

            visibleWidth = canvas.width;
            visibleHeight = canvas.height;
            width = canvas.width;
            height = canvas.height;

            canvas.addEventListener("mousewheel", zoom, false);
            canvas.addEventListener("mousedown", setMouseDown, false);
            canvas.addEventListener("mouseup", setMouseUp, false);
            canvas.addEventListener("mousemove", move, false);
            canvas.addEventListener('contextmenu', handleContextMenu, false);
            $('#chktodos').change(function () {
                if ($(this).is(':checked')) {
                    // Checkbox is checked..
                    todos();
                } else {
                    ninguno();
                    // Checkbox is not checked..
                }

            });

            $('#rlplayas').find('INPUT[type = checkbox]').change(function () {

                OnClientItemChecked($('#rlplayas'));

            });

            //    $('#chkninguno').addEventListener('click', ninguno, false);  

            // Defaults

            //  var DRAW_POS = [canvas.width / 2, canvas.height / 2];

            // Buttons
            $("#zoomIn").click(function (e) {
                zoomIn(e);
            });            
            $("#zoomOut").click(function (e) {
                zoomOut(e);
            });
            //        zoomInBtn.addEventListener("click", zoomIn, false);
            //        var zoomOutBtn = $("#zoomOut");
            //        zoomOutBtn.addEventListener("click", zoomOut, false);
            //        var resetZoomBtn = $("#resetZoom");
            //        resetZoomBtn.addEventListener("click", resetZoom, false);
            //        var resetPosBtn = $("#resetPos");
            //        resetPosBtn.addEventListener("click", resetPos, false);

            // Image
            var loaded = false;
            drawPos = DRAW_POS;
            //     var scale = DEFAULT_ZOOM;
            image = new Image();
            cambio_mapa();
            image.addEventListener("load", function (e) {
                loaded = true;
                //   drawCanvas();
                context.fillStyle = "#FFFFFF";
                context.fillRect(0, 0, canvas.width, canvas.height);
                if (loaded) {
                    drawImage1();
                }
                traspasacanvas();
                var aux = $('#rlplayas');
                OnClientItemChecked(aux, null);
                traspasacanvas1();
            }, false);

        }

        // Draw the canvas
        function drawCanvas(zoom) {
            context.fillStyle = "#FFFFFF";
            context.fillRect(0, 0, canvas.width, canvas.height);

            drawImage();
            if (zoom != undefined) {
                zoomcanvas();
            }
            traspasacanvas1();

        }

        var $menu = $('#contextMenu');

        function reOffset() {
            var BB = canvas.getBoundingClientRect();

            canvas.height = $("#myCanvas").height();
            canvas.width = $("#myCanvas").width();

            offsetX = BB.left;
            offsetY = BB.top;

            visibleWidth = canvas.width;
            visibleHeight = canvas.height;
            width = canvas.width;
            height = canvas.height;

            drawImage1(true);
            drawCanvas();

        }

        var offsetX, offsetY;

        //      reOffset();
        //     window.onscroll=function(e){ reOffset(); }
        window.onresize = function (e) {
            reOffset();
        }

        function showContextMenu(r, x, y) {
            if (!r) {
                $menu.hide();
                return;
            }
        }

        function handleContextMenu(e) {
            // tell the browser we're handling this event

            e.preventDefault();
            e.stopPropagation();
            drawCanvas();
            var ratio = scale;
            // get mouse position relative to the canvas
            var x = (parseInt(e.clientX - offsetX) / scale - drawPos[0] / scale);
            var y = (parseInt(e.clientY - offsetY) / scale - drawPos[1] / scale);

            // hide the context menu
            showContextMenu();

            // check each rect for hits

            var xmouse = x * scale + drawPos[0];
            var ymouse = y * scale + drawPos[1];
            var rect = imagenen(x, y, false);
            if (rect == undefined) {
                rect = playaen(x, y, false);
            }
            //                         context.beginPath();
            //                      context.fillStyle='green';
            //                        context.rect(x*scale+drawPos[0],  y*scale+drawPos[1],50,50);
            if (rect != undefined) {
                selecciona(rect);
                recta(rect, 'orange');
                $(rect).contextMenu({
                    x: xmouse + offsetX,
                    y: ymouse + offsetY
                });
            }

            return (false);
        }

        function imagenen(x, y, offset) {
            if (offset == true) {
                x = (parseInt(x - offsetX) / scale - drawPos[0] / scale);
                y = (parseInt(y - offsetY) / scale - drawPos[1] / scale);

            }

            for (var i = 0; i < lugares.length; i++) {
                var rect = lugares[i];
                var x1 = rect.x1 * scale + drawPos[0];
                var y1 = rect.y1 * scale + drawPos[1];
                var rectRight = x1 + rect.width1 * scale;
                var rectBottom = y1 + rect.height1 * scale;

                var xmouse = x * scale + drawPos[0];
                var ymouse = y * scale + drawPos[1];

                if (xmouse >= x1 && xmouse <= rectRight && ymouse >= y1 && ymouse <= rectBottom && rect.playa.mostrar == true) {
                    return rect;

                }

            }

        }

        function playaen(x, y, offset) {
            if (offset == true) {
                x = (parseInt(x - offsetX) / scale - drawPos[0] / scale);
                y = (parseInt(y - offsetY) / scale - drawPos[1] / scale);

            }

            for (var i = 0; i < playas.length; i++) {
                var rect = playas[i];
                var x1 = rect.x1 * scale + drawPos[0];
                var y1 = rect.y1 * scale + drawPos[1];
                var rectRight = x1 + rect.width1 * scale;
                var rectBottom = y1 + rect.height1 * scale;

                var xmouse = x * scale + drawPos[0];
                var ymouse = y * scale + drawPos[1];

                if (xmouse >= x1 && xmouse <= rectRight && ymouse >= y1 && ymouse <= rectBottom && rect.mostrar == true) {
                    return rect;

                }

            }

        }

        function drawImage1(forzar) {
            var w = image.width;
            var h = image.height;
            var escala = Math.round(canvas.width / w * 1000) / 1000;
            // var x = drawPos[0];
            // var y = drawPos[1]; 
            var x = drawPos[0]; // - (w / 2);
            var y = drawPos[1]; //- (h / 2);
            //   context.drawImage(image, x, y, w, h);

            if (scale == null || forzar != undefined) {
                scale = escala;
                MAX_ZOOM = escala * 4;
                MIN_ZOOM = escala;
                DEFAULT_ZOOM = scale;
            }

            context.drawImage(image, x, y, w * scale, h * scale);

        }

        // Draw the image
        function drawImage() {
            var w = image.width * scale;
            var h = image.height * scale;
            // var x = drawPos[0];
            // var y = drawPos[1]; 
            var x = drawPos[0]; // - (w / 2);
            var y = drawPos[1]; //- (h / 2);
            //   context.drawImage(image, x, y, w, h);
            context.drawImage(image, x, y, w, h);

        }

        // Set the zoom with the mouse wheel
        function zoom(e) {
            if (e.wheelDelta > 0) {
                zoomIn(e);
            } else {
                zoomOut(e);
            }
        }

        // Zoom in
        function zoomIn(event) {
            if (scale < MAX_ZOOM) {
                //    drawCanvas();
                var mousex = event.clientX - canvas.offsetLeft;
                var mousey = event.clientY - canvas.offsetTop;
                // Normalize wheel to +1 or -1.

                var wheel;

                if (event.wheelDelta == undefined) {
                    wheel = 1;
                    mousex = canvas.offsetLeft + canvas.width / 2;
                    mousey = canvas.offsetTop + canvas.height / 2;
                } else {
                    wheel = event.wheelDelta / 120;
                }

                // Compute zoom factor.
                var zoom = Math.exp(wheel * 0.1);

                // Translate so the visible origin is at the context's origin.
                //       context.translate(originx, originy);

                // Compute the new visible origin. Originally the mouse is at a
                // distance mouse/scale from the corner, we want the point under
                // the mouse to remain in the same place after the zoom, but this
                // is at mouse/new_scale away from the corner. Therefore we need to
                // shift the origin (coordinates of the corner) to account for this.
                // originx -= mousex/(scale*zoom) - mousex/scale;
                // originy -= mousey/(scale*zoom) - mousey/scale;
                //  delta = [originx, originy];

                var rawCenterX = canvas.offsetLeft + canvas.width / 2; ///scale;
                var rawCenterY = canvas.offsetTop + canvas.height / 2; ///scale;

                // Delta
                var deltaX = (rawCenterX - mousex) / scale;
                var deltaY = (rawCenterY - mousey) / scale;

                drawPos = [drawPos[0] + wheel * deltaX * scale / 2, drawPos[1] + wheel * deltaY * scale / 2];
                DRAW_POS = drawPos;

                // Scale it (centered around the origin due to the trasnslate above).
                //      context.scale(zoom, zoom);
                // Offset the visible origin to it's proper position.
                //      context.translate(-originx, -originy);

                // Update scale and others.
                scale *= zoom;
                visibleWidth = width / scale;
                visibleHeight = height / scale;

                normalizar_pos()

                drawCanvas(zoom);
                //zoomcanvas();
            }
        }

        function normalizar_pos() {

            if (drawPos[0] > 0) {
                drawPos[0] = 0;
            } else if (drawPos[0] + image.width * scale < canvas.width) {
                drawPos[0] = canvas.width - image.width * scale;
            }

            if (drawPos[1] > 0) {
                drawPos[1] = 0;
            } else if (drawPos[1] + image.height * scale < canvas.height) {
                drawPos[1] = canvas.height - image.height * scale;
            }

            DRAW_POS = drawPos;
        }

        // Zoom out
        function zoomOut(event) {
            if (scale.toFixed(3) > MIN_ZOOM) {

                var mousex = event.clientX - canvas.offsetLeft;
                var mousey = event.clientY - canvas.offsetTop;
                // Normalize wheel to +1 or -1.
                var wheel;

                if (event.wheelDelta == undefined) {
                    wheel = -1;
                    mousex = canvas.offsetLeft + canvas.width / 2;
                    mousey = canvas.offsetTop + canvas.height / 2;
                } else {
                    wheel = event.wheelDelta / 120;
                }

                // Compute zoom factor.
                var zoom = Math.exp(wheel * 0.1)

                // Translate so the visible origin is at the context's origin.
                // context.translate(originx, originy);

                // Compute the new visible origin. Originally the mouse is at a
                // distance mouse/scale from the corner, we want the point under
                // the mouse to remain in the same place after the zoom, but this
                // is at mouse/new_scale away from the corner. Therefore we need to
                // shift the origin (coordinates of the corner) to account for this.
                //     originx -= mousex/(scale*zoom) - mousex/scale;
                //     originy -= mousey/(scale*zoom) - mousey/scale;
                //  drawPos[0]=-originx;
                // drawPos[1]=-originy;
                // Scale it (centered around the origin due to the trasnslate above).
                // context.scale(zoom, zoom);
                // Offset the visible origin to it's proper position.
                // context.translate(-originx, -originy);

                var rawCenterX = canvas.offsetLeft + canvas.width / 2; ///scale;
                var rawCenterY = canvas.offsetTop + canvas.height / 2; ///scale;

                // Delta
                var deltaX = (rawCenterX - mousex) / scale;
                var deltaY = (rawCenterY - mousey) / scale;

                //       drawPos = [drawPos[0] - deltaX*scale/2, drawPos[1] - deltaY*scale/2];

                //  mousePos = [e.x, e.y];
                //  drawCanvas();

                // Update scale and others.
                scale *= zoom;
                visibleWidth = width / scale;
                visibleHeight = height / scale;

                if (scale < MIN_ZOOM) {
                    drawImage1();
                }
                normalizar_pos();
                drawCanvas(zoom);
                // zoomcanvas()                       
            }
        }

        // Reset the zoom
        function resetZoom(e) {
            scale = DEFAULT_ZOOM;
            drawPos[0] = 0;
            drawPos[1] = 0;
            DRAW_POS = drawPos;
            drawCanvas();
        }

        // Reset the position
        function resetPos(e) {
            drawPos = DRAW_POS;
            drawCanvas();
        }

        var xevento;
        var yevento;
        var arrastrado;
        var parrastrada;

        // Toggle mouse status
        function setMouseDown(e) {

            drawCanvas();
            xevento = e.x;
            yevento = e.y;

            arrastrado = imagenen(e.x, e.y, true);
            parrastrada = playaen(e.x, e.y, true);

            if (arrastrado == undefined && parrastrada == undefined) {

                ArrastrandoMapa = true;
                lugararrastrado = null;

            } else {

                if (arrastrado != lugarseleccionado) {
                    selecciona(arrastrado);
                    ArrastrandoMapa = false;
                    lugararrastrado = arrastrado;
                    // recta(arrastrado,'orange');

                } else if (parrastrada != lugarseleccionado) {
                    selecciona(parrastrada);
                    ArrastrandoMapa = false;
                    lugararrastrado = parrastrada;
                    // recta(arrastrado,'orange');

                }

            }

            drawCanvas();

            mousePos = [e.x, e.y];
        }

        function setMouseUp(e) {
            ArrastrandoMapa = false;

            if ((e.x == xevento && e.y == yevento) || event.button != 0) {
                var rect = imagenen(e.x, e.y, true);
                destino();
                if (rect == undefined) {
                    rect = playaen(e.x, e.y, true);
                }
                if (rect == undefined) {
                    selecciona();
                }
                drawCanvas();
            } else {

                var rect = imagenen(e.x, e.y, true);
                if (rect == undefined) {
                    rect = playaen(e.x, e.y, true);
                }
                if (rect == undefined) {
                    //   lugardestino=null;
                    destino();
                    drawCanvas();

                } else if (lugararrastrado != null && rect != lugarseleccionado) {
                    //         lugardestino=rect;
                    destino(rect);
                    drawCanvas();
                    //      recta(rect,'blue');
                }
            }
            parrastrada = undefined;
            arrastrado = undefined;
        }

        function muevezona2() {

            var rawCenterX = -originx + Math.min(canvas.width, image.width) / 2 / scale;
            var rawCenterY = -originy + Math.min(canvas.height, image.height) / 2 / scale;

            // Delta
            var deltaX = (rawCenterX - 0) * scale;
            var deltaY = (rawCenterY - 0) * scale;
            for (var i = 0; i < lugares.length; i++) {
                var image1 = lugares[i];

                image1.y1 = image1.y1 + deltaY;
                image1.x1 = image1.x1 + deltaX;

            }

        }

        // Move
        function move(e) {

            $('.tool').remove();
            $('.tool1').remove();
            delta = [e.x - mousePos[0], e.y - mousePos[1]];

            if (parrastrada != undefined) {
                var dX = 0,
                dY = 0;
                delta = [e.x - mousePos[0], e.y - mousePos[1]];
                //    drawPos = [drawPos[0] + delta[0], drawPos[1] + delta[1]];
                //    normalizar_pos();
                //    DRAW_POS=drawPos;
                mousePos = [e.x, e.y];

                if (!$('#tamano').is(':checked')) {
                    parrastrada.x1 = parrastrada.x1 + delta[0] / scale;
                    parrastrada.y1 = parrastrada.y1 + delta[1] / scale;

                } else {
                    parrastrada.width1 = parrastrada.width1 + delta[0] / scale;
                    parrastrada.height1 = parrastrada.height1 + delta[1] / scale;

                }
                drawCanvas();

            }

            if (ArrastrandoMapa) {
                var dX = 0,
                dY = 0;
                delta = [e.x - mousePos[0], e.y - mousePos[1]];
                drawPos = [drawPos[0] + delta[0], drawPos[1] + delta[1]];
                normalizar_pos();
                DRAW_POS = drawPos;
                mousePos = [e.x, e.y];
                drawCanvas();

            } else {

                var rect = imagenen(e.x, e.y, true);
                if (rect != undefined) {

                    if (delta[0] != 0 && delta[1] != 0) {
                        muestratooltip(e, rect);
                    }

                    if (arrastrado != undefined) {
                        drawCanvas();
                        recta(rect, 'blue');
                    }

                } else {
                    if (arrastrado != undefined) {

                        var dX = 0,
                        dY = 0;
                        delta = [e.x - mousePos[0], e.y - mousePos[1]];
                        //                              if (delta[0] <= 1 && delta[1] <= 1) {
                        //                                  delta[0] = 20;
                        //                                  delta[1] = 20;
                        //                              }
                        //                              
                        drawPos = [drawPos[0] - delta[0], drawPos[1] - delta[1]];
                        normalizar_pos();
                        DRAW_POS = drawPos;
                        mousePos = [e.x, e.y];
                        drawCanvas();
                    }

                }

            }

            if ((arrastrado != undefined) && (delta[0] != 0 || delta[1] != 0)) {
                muestratooltip1(e, arrastrado)
            }

        }

        function cambio_mapa() {

            //              var imagen1="<%=this.url %>/images/yms_wallmart_hd2.jpg";
            //              var imagen2="<%=this.url %>/images/cd_quilicuda_walmart5.png";

            //              if (image.src=="")
            //                {
            //                  image.src = imagen1;
            //                  return;

            //                  }

            image.src = document.getElementById('siteimage').src;

            //          var $yo=  $('#large');
            //              
            //              var src = (image.src  === imagen2)
            //            ? imagen1
            //            : imagen2;
            //            image.src=src;
            if (canvas != undefined) {

                //   drawImage1();
                drawCanvas();

            }

        }

        function muestratooltip(e, img) {
            // Hover over code
            if (notooltip == 1) {
                notooltip = 0;
                // $(this).data('tipText', title).removeAttr('title');
                return false;

            }

            var title = img.getAttribute('title1');
            //   $(this).data('tipText', title).removeAttr('title');
            var $yo = $('<p class="tool"></p>')
            .text(title)
            .appendTo('#CD')
            .fadeIn('slow');

            var mousex = e.clientX + 10; //Get X coordinates
            var mousey = e.clientY - 100 + 5; //Get Y coordinates

            $yo.css({
                top: mousey,
                left: mousex
            })

        }

        function muestratooltip1(e, img) {
            // Hover over code
            if (notooltip == 1) {
                notooltip = 0;
                // $(this).data('tipText', title).removeAttr('title');
                return false;

            }

            var title = img.getAttribute('patente');
            //   $(this).data('tipText', title).removeAttr('title');
            var $yo = $('<p class="tool1"></p>')

        .text('Moviendo: ' + title)
            .appendTo('#CD');
            //  var $tu=$(img);

            var mousex = e.clientX + 10; //Get X coordinates
            var mousey = e.clientY - 100 + 5; //Get Y coordinates

            $yo.css({
                top: mousey,
                left: mousex
            })
            //  $tu.css({ top: mousey, left: mousex, display:'block', visibility:'visible', position:'absolute' }) 

        }

        function recta(rect, color) {
            var x1 = rect.x1 * scale + drawPos[0];
            var y1 = rect.y1 * scale + drawPos[1];

            context.beginPath();
            //      context.globalCompositeOperation='destination-over';
            context.fillStyle = color;
            context.globalAlpha = 0.8;
            context.rect(x1, y1, rect.width1 * scale, rect.height1 * scale);
            context.fill();
            //    context.globalCompositeOperation='source-over';
            context.globalAlpha = 1;
            // dibujalugar(rect)

        }

        function dibujalugar(imagen) {

            if (imagen.playa == undefined) {
                alert(imagen);
            } else if (imagen.playa.mostrar == true) {
                var title = imagen.getAttribute('patente');
                //             context.save();
                if (title != undefined && title != 'vacio') {
                    context.shadowColor = "#000000";
                    context.shadowBlur = 10;
                    context.shadowOffsetX = imagen.width1 / 10 * scale; // -4+drawPos[0]*scale/100;
                    context.shadowOffsetY = -imagen.height1 / 10 * scale; //  -2+drawPos[1]*scale/100;
                }
                context.drawImage(imagen, imagen.x1 * scale + drawPos[0], imagen.y1 * scale + drawPos[1], imagen.width1 * scale, imagen.height1 * scale);

                var tamano = Math.min(imagen.width1, imagen.height1);

                context.font = tamano * 1 / 2 * scale + 'pt Calibri';
                context.fillStyle = 'gray';
                context.textAlign = "center";
                var x1 = imagen.x1 * scale + drawPos[0] + imagen.width1 * scale * 2 / 3;
                var y1 = imagen.y1 * scale + drawPos[1] + imagen.height1 * scale * 2 / 3;
                context.translate(x1, y1);

                if ((imagen.getAttribute('rotacion') == 1) || (imagen.getAttribute('rotacion') == 3)) {

                    context.rotate(-Math.PI / 2);
                }
                context.fillText(title, 0, 0);

                if ((imagen.getAttribute('rotacion') == 1) || (imagen.getAttribute('rotacion') == 3)) {
                    context.rotate(Math.PI / 2);
                }

                context.translate(-x1, -y1);

                if (title != undefined && title != 'vacio') {
                    context.shadowColor = "rgba(0, 0, 0, 0)";
                    context.shadowBlur = 0;
                    context.shadowOffsetX = 0;
                    context.shadowOffsetY = 0;
                }

                var $ottawa = $(imagen).next();
                if ($ottawa.is('img')) {

                    var desfasex = 0;
                    var desfasey = 0;
                    if (imagen.getAttribute('rotacion') == 2) {
                        desfasex = imagen.width1 * scale;
                        desfasey = imagen.height1 / 4 * scale;
                    }
                    if (imagen.getAttribute('rotacion') == 4) {
                        desfasex = -imagen.width1 * scale;
                        desfasey = imagen.height1 / 4 * scale;
                    }
                    if (imagen.getAttribute('rotacion') == 1) {
                        desfasey = -imagen.height1 * scale / 2;
                        desfasex = imagen.width1 / 4 * scale;
                    }
                    if (imagen.getAttribute('rotacion') == 3) {
                        desfasey = imagen.height1 * scale;
                        desfasex = imagen.width1 / 4 * scale;
                    }

                    context.drawImage($ottawa[0], imagen.x1 * scale + drawPos[0] + desfasex, imagen.y1 * scale + drawPos[1] + desfasey, imagen.width1 / 2 * scale, imagen.height1 / 2 * scale);

                }

                //                    context.restore();

            }
        }

        function dibujaottawa($ottawa) {
            alert($ottawa);
        }

        function dibujaplaya(playa) {

            if (playa.mostrar == true) {
                context.beginPath();
                context.fillStyle = "yellow";
                context.globalAlpha = 0.2;
                context.rect(((playa.x1 * scale + drawPos[0])), (playa.y1 * scale + drawPos[1]), playa.width1 * scale, playa.height1 * scale);
                context.fill();
                context.globalAlpha = 1;
            }

        }

        function ubicalugar(lugar, playa, ratio, ratio2) {
            lugar.x1 = lugar.style.left.replace("%", "") * ratio + playa.x1; //+ imagen.parentNode.left ;
            lugar.y1 = lugar.style.top.replace("%", "") * ratio2 + playa.y1; //+ imagen.parentNode.top;

        }

        function traspasacanvas() {
            playas = [];
            lugares = [];

            $(".zona").each(function () {

                //  alert(zonaX);
                //      $(this).css({ height: $(this)[0].style.height.replace("%","") *alto/100 ,width: $(this)[0].style.width.replace("%","")*ancho/100  });
                //  var imagen='./'+$(this).attr('src');
                //       context.drawImage($(this)[0],$(this).position().left, $(this).position().top, $(this).width(),  $(this).height);
                //           var image = new Image();
                ///   	        image.src = './images/BORRAR.png';
                var imagen = document.getElementById($(this)[0].id);
                playas.push(imagen);
                //    context.drawImage(image,100, 100, 100, 100);
                //                    var w = $(this).width() * scale;
                //    	            var h = $(this).height() * scale;
                //                    imagen.x1=$(this).position().left+delta[0];
                //                    imagen.y1=$(this).position().top+delta[1];
                var ratio = image.width / 100;
                var ratio2 = image.height / 100;
                var w = imagen.style.width.replace("%", "") * ratio; //* scale;
                var h = imagen.style.height.replace("%", "") * ratio2; //* scale;
                imagen.x1 = imagen.style.left.replace("%", "") * ratio;
                imagen.y1 = imagen.style.top.replace("%", "") * ratio2;
                imagen.mostrar = true;
                imagen.contextMenu = [imagen.x1, 'Two red'];
                //    context.drawImage(imagen,imagen.x1, imagen.y1, 100* scale, 100* scale);
                dibujaplaya(imagen);
                imagen.estaciones = [];
                imagen.width1 = w;
                imagen.height1 = h;
            });
        }

        function clear() {

        }

        //clears the canvas and draws the rectangle at the appropriate location
        function redraw() {
            clear();
        }

        function OnClientItemChecked(sender, eventArgs) {
            for (var j = 0; j < playas.length; j++) {
                var playa = playas[j];
                playa.mostrar = true;
                var elemento = sender.find("#chk_" + playa.id).find(":first-child");

                playa.mostrar = elemento.is(':checked');
            }

            drawCanvas();

        }

        function todos() {
            var aux = $('#rlplayas');
            var items = aux.find("INPUT[type=checkbox]");
            items.each(function () {
                $(this).prop("checked", true);
            });
            OnClientItemChecked(aux, null);
        }

        function ninguno() {
            var aux = $('#rlplayas');
            var items = aux.find("INPUT[type=checkbox]");
            items.each(function () {
                $(this).prop("checked", false);
            });
            OnClientItemChecked(aux, null);
        }

        function traspasacanvas1() {

            for (var j = 0; j < playas.length; j++) {
                var playa = playas[j];
                if (playa.mostrar == true) {
                    dibujaplaya(playa);
                }
            }

            if (lugarseleccionado != null) {
                recta(lugarseleccionado, 'orange');
            }
            if (lugardestino != null) {
                recta(lugardestino, 'blue');
            }

            context.setTransform(1, 0, 0, 1, 0, 0);

        }

        function zoomcanvas() {
            for (var j = 0; j < playas.length; j++) {
                var playa = playas[j];

                var ratio = image.width / 100;
                var ratio2 = image.height / 100;

                var w = playa.style.width.replace("%", "") * ratio; //* scale;
                var h = playa.style.height.replace("%", "") * ratio2; //* scale;
                context.fillStyle = "black";
                playa.height1 = h;
                playa.width1 = w;

                for (var i = 0; i < playa.estaciones.length; i++) {
                    var imagen2 = playa.estaciones[i];

                    var w = imagen2.style.width.replace("%", "") * ratio;
                    var h = imagen2.style.height.replace("%", "") * ratio2;
                    imagen2.height1 = h;
                    imagen2.width1 = w;
                    ubicalugar(imagen2, playa, ratio, ratio2);

                }
            }
        }

        function recarga() {
            carga_canvas();
            selecciona(lugarseleccionado);
        }

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(recarga);

        function grabaplaya(mytrigger) {

            var pagePath = window.location.pathname + "/graba";
            var orientacion = $('#orientacion').val();
            var ancho = mytrigger[0].width1 / image.width * 100;
            var alto = mytrigger[0].height1 / image.height * 100;
            var x = mytrigger[0].x1 / image.width * 100;
            var y = mytrigger[0].y1 / image.height * 100;
            var supl_id = mytrigger[0].getAttribute("SUPL_ID");

            $.ajax({
                url: pagePath,
                type: "POST",
                data: '{usuario:"' + mytrigger[0].id + '",supl_id:"' + supl_id + '",x:"' + x + '",y:"' + y + '",ancho:"' + ancho + '",alto:"' + alto + '",orientacion:"' + orientacion + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                },
                success: function (result) {

                    if (result.isOk == false) {
                        alert(result.message);
                    }
                },
                async: false
            });
        }
    </script>
</asp:Content>
