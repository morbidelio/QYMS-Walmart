





var notooltip = 0;

$(function () {
    $.contextMenu({
        selector: '.large',
        events: {
            show: function (opt, x, y) { $('.tool').remove(); },
            hide: function (opt, x, y) { notooltip = 1; }
        },
        callback: function (key, options) {
            var m = "clicked: " + key;
            //  window.console && console.log(m) || alert(m);
            zoomOut();
        },
        items: {
            "reset zoom": { name: "reset", icon: "cut" }

        }
    });


});



$(function () {

    $.contextMenu({
        selector: '.context-menu-one',
        animation: { duration: 500, show: "fadeIn", hide: "fadeOut" },
        events: {
            show: function (opt, x, y) { $('.tool').remove(); selecciona(this[0]); },
            hide: function (opt, x, y) { notooltip = 1; }
        },
        build: function ($trigger, e) {
            //       window.console && console.log($trigger) || alert($trigger); 

            return {
                callback: function (key, options) {

                    //     var m = "clicked: " + key;
                    //    window.console && console.log(m) || alert(m); 
                    destino($('#' + key)[0]);

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

            if (result.isOk == false)
                alert(result.message);
            else {
                for (var i = 0; i < result.d.length; i++) {
                    if (result.d[i].items != 'undefined')
                        $menu[result.d[i].ID.toString()] = { name: result.d[i].Nombre.toString(), icon: result.d[i].icono.toString(), items: armasubitmesitem(result.d[i]) };
                    else
                        $menu[result.d[i].ID.toString()] = { name: result.d[i].Nombre.toString(), icon: result.d[i].icono.toString() };

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
        $subitems[item.subitems[i].ID.toString()] = { name: item.subitems[i].Nombre.toString(), icon: item.subitems[i].icono.toString() };
    }

    return $subitems;
}


$(function () {
    $.contextMenu({
        selector: '.context-menu-two',
        animation: { duration: 500, show: "fadeIn", hide: "fadeOut" },
        events: {
            show: function (opt, x, y) { $('.tool').remove(); selecciona(); },
            hide: function (opt, x, y) { notooltip = 1; }
        },
        callback: function (key, options) {
            var m = "clicked: " + key;
            //  window.console && console.log(m) || alert(m);
        },
        items: {/*
                     "edit": { name: "Edit2", icon: "edit" } ,
                     "cut": { name: "Cut2", icon: "cut" },
                     "copy": { name: "Copy2", icon: "copy" },
                     "paste": { name: "Paste2", icon: "paste" },
                     "delete": { name: "Delete2", icon: "delete" },
                     "sep1": "---------",
                     "quit": { name: "Quit2", icon: function () {
                         return 'context-menu-icon context-menu-icon-quit';
                     }
                     }*/
            "Vacio": { name: "vacio" }
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
    destino();
    //     alert(elemento);


    var ultimo = $("#id_lugar_seleccionado").val();

    // if (ultimo!='') ($('#'+ultimo).parent().removeClass('selecciona'));

    $("#id_lugar_seleccionado").val('');
    $("#playa_seleccionada").val('');
    $("#lugar_seleccionado").val('');
    $('#trailer_seleccionado').val('');

    if (elemento == undefined) {
        $('#lugarpanel').fadeOut();
        lugarseleccionado = null;
        return;
    }

    lugarseleccionado = elemento;
    var $elemento = $(elemento);
    if (!$elemento.is('img')) $elemento = $elemento.find(".lugar");
    //  $elemento.parent().addClass('selecciona');
    $("#id_lugar_seleccionado").val($elemento.attr('id'))
    $("#playa_seleccionada").val($elemento.attr('playa'));
    $("#lugar_seleccionado").val($elemento.attr('codigo_lugar'));
    $('#trailer_seleccionado').val($elemento.attr('patente'));

    $('#lugarpanel_playa').text($elemento.attr('playa'));
    $('#lugarpanel_codigo').text($elemento.attr('codigo_lugar'));
    $('#lugarpanel_trailer').text($elemento.attr('patente'));
    $('#lugarpanel').fadeIn()
}


var lugardestino;

function destino(elemento) {


    var ultimo = $("#id_destino_seleccionado").val();

    if (ultimo != '') ($('#' + ultimo).parent().removeClass('selecciona_destino'));

    $("#id_destino_seleccionado").val('');
    $("#Playa_seleccionada_destino").val('');
    $("#Lugar_seleccionado_destino").val('');

    if (elemento == undefined) {
        lugardestino = null;
        return;
    }

    lugardestino = elemento;
    var $elemento = $(elemento);
    if (!$elemento.is('img')) $elemento = $elemento.find(".lugar");
    //   alert($elemento.parent());

    if ($('#trailer_seleccionado').val() == 'vacio') {
        lugardestino = null;
        return;
    }

    if (elemento.getAttribute('patente') != 'vacio') {
        lugardestino = null;
        return;
    }


    $elemento.parent().addClass('selecciona_destino');
    $("#id_destino_seleccionado").val($elemento.attr('id'))
    $("#Playa_seleccionada_destino").val($elemento.attr('playa'));
    $("#Lugar_seleccionado_destino").val($elemento.attr('codigo_lugar'));
    $("#id_destino_formulario").val($elemento.attr('id'));
    $("#ModalFiltro").modal();

    document.getElementById("btn_sol").click();
}


var disableImgEventHandlers = function () {
    var events = ['onclick', 'onmousedown', 'onmousemove', 'onmouseout', 'onmouseover',
                      'onmouseup', 'ondblclick', 'onfocus', 'onblur'];
    events.forEach(function (event) {
        img[event] = function () {
            return false;
        };
    });
};

var hammer;

var tick_recarga;


function click_recarga() {
    $('#nozzoom').click();

}

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
        $("#id_destino_formulario").val(ultimo);
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
            to: { width: 1398 }
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


function getTouchPos(canvasDom, touchEvent) {
    var rect = canvasDom.getBoundingClientRect();
    return {
        x: touchEvent.touches[0].clientX - rect.left,
        y: touchEvent.touches[0].clientY - rect.top
    };
}

function calculadistancia(touchEvent) {
    //   return 1;
    //    return Math.sqrt(Math.pow( (touchEvent.touches[0].clientX - touchEvent.touches[1].clientX),2) + Math.pow((touchEvent.touches[0].clientX - touchEvent.touches[1].clientX),2));
    return Math.hypot(touchEvent.touches[0].clientX - touchEvent.touches[1].clientX, touchEvent.touches[0].clientY - touchEvent.touches[1].clientY);
}

var lastMove = null;
function carga_canvas() {

    lugares = [];
    playas = [];


    canvas = document.querySelector("#myCanvas");
    context = canvas.getContext('2d',
                 { antialias: false,
                     depth: false
                 });

  // $("#myCanvas").parent().height($(window).height() - $("#myCanvas").parent().offset().top);
    $("#myCanvas").height($(window).height() - $("#myCanvas").offset().top - 10);
    canvas.height = $("#myCanvas").height();
    canvas.width = $("#myCanvas").width();
    $("#scrolls").height(canvas.height-20);
 //   $("#zoom").height($("#scrolls").height());
    



    var BB = canvas.getBoundingClientRect();
    offsetX = BB.left;
    offsetY = BB.top;

    visibleWidth = canvas.width;
    visibleHeight = canvas.height;
    width = canvas.width;
    height = canvas.height;
    $("#sidebar-wrapper").height($(window).height() - $("#sidebar-wrapper").offset().top - 21);
    $("#detalleplayas").height($(window).height() - $("#sidebar-wrapper").offset().top - 143);
    var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel"

    canvas.addEventListener(mousewheelevt, zoom, false);
    canvas.addEventListener("mousedown", setMouseDown, false);
    canvas.addEventListener("mouseup", setMouseUp, false);
    canvas.addEventListener("mousemove", move, false);
    canvas.addEventListener('contextmenu', handleContextMenu, false);
    var enzoom = false;
    canvas.addEventListener("touchstart", function (e) {
        mousePos = getTouchPos(canvas, e);
        var touch = e.touches[0];
        var mouseEvent = new MouseEvent("mousedown", {
            clientX: touch.clientX,
            clientY: touch.clientY
        });
        lastMove = e;
        //            console.log('start'+e.touches.length);
        canvas.dispatchEvent(mouseEvent);
    }, false);
    canvas.addEventListener("touchend", function (e) {
        //alert(lastMove.touches);



        if (enzoom == true) {

        }
        else {
            mousePos = getTouchPos(canvas, lastMove);
            var touch = lastMove.touches[0];
            var mouseEvent = new MouseEvent("mouseup", {
                clientX: touch.clientX,
                clientY: touch.clientY
            });
            lastMove = null;
            enzoom = false;
            canvas.dispatchEvent(mouseEvent);
        }
    }, false);
    canvas.addEventListener("touchmove", function (e) {
        var touch = e.touches[0];
        var mouseEvent;
        e.preventDefault();
        e.stopPropagation();
        //              console.log('continue' + e.touches.length);

        if (e.touches.length > 1 && lastMove != null && lastMove.touches.length > 1) {
            var distanciaoriginal = calculadistancia(lastMove);
            var nuevadistancia = calculadistancia(e);

            //                    console.log('antes' + distanciaoriginal);

            //                     console.log('ahoira' + nuevadistancia);
            enzoom = true;
            if (nuevadistancia > distanciaoriginal) {
                mouseEvent = {
                    clientX: (e.touches[0].clientX + e.touches[1].clientX) / 2 - this.getBoundingClientRect().left,
                    clientY: (e.touches[0].clienty + e.touches[1].clienty) / 2 - this.getBoundingClientRect().top,
                    detail: -1,
                    touches: [e.touches[0], e.touches[1]]
                };
                lastMove = mouseEvent;
                //    canvas.dispatchEvent(mouseEvent);
                //alert(e.touches[0].clientX);
                if (scale.toFixed(3) < MAX_ZOOM) zoom(mouseEvent);
                //                  else alert('bb');
            }
            else {

                mouseEvent = {
                    clientX: (e.touches[0].clientX + e.touches[1].clientX) / 2 - this.getBoundingClientRect().left,
                    clientY: (e.touches[0].clienty + e.touches[1].clienty) / 2 - this.getBoundingClientRect().top,
                    detail: 1,
                    touches: [e.touches[0], e.touches[1]]
                };
                lastMove = mouseEvent;
                //   canvas.dispatchEvent(mouseEvent);
                if (scale.toFixed(3) > MIN_ZOOM) zoom(mouseEvent);
                //		else alert('aa');
            }

            //    console.log(mouseEvent);

        }
        else {
            mouseEvent = new MouseEvent("mousemove", {
                clientX: touch.clientX,
                clientY: touch.clientY
            });
            lastMove = e;
            canvas.dispatchEvent(mouseEvent);
        }
    }, false);



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
    var zoomInBtn = document.querySelector("#zoomIn");
    zoomInBtn.addEventListener("click", zoomIn, false);
    var zoomOutBtn = document.querySelector("#zoomOut");
    zoomOutBtn.addEventListener("click", zoomOut, false);
    var maximizar = document.querySelector("#maximizar");
    maximizar.addEventListener("click", Maximizar, false);

    $('#trailers').change(function () {
        var selectedVal = $(this).val();
        seleccionatrailer(selectedVal);
    });


    //    var resetZoomBtn = document.querySelector("#resetZoom");
    //    resetZoomBtn.addEventListener("click", resetZoom, false);
    //     var resetPosBtn = document.querySelector("#resetPos");
    //     resetPosBtn.addEventListener("click", resetPos, false);

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
            normalizar_pos();
            colocacontroles();

        }

        traspasacanvas();
        var aux = $('#rlplayas');
        OnClientItemChecked(aux, null);
        traspasacanvas1();
    }, false);


}

// Draw the canvas
function drawCanvas(zoom) {
    //             context.fillStyle = "#FFFFFF";
    //            context.fillRect(0, 0, canvas.width, canvas.height);


    drawImage();
    if (zoom != undefined) zoomcanvas();
    traspasacanvas1();

}

var $menu = $('#contextMenu');

function reOffset() {

    if ($("#myCanvas") != undefined && ("#myCanvas") != null) {
        //  $("#myCanvas").parent().height($(window).height() - $("#myCanvas").parent().offset().top);
        $("#myCanvas").height($(window).height() - $("#myCanvas").offset().top - 10);
        //   $("#zoom").height($("#scrolls").height());
        //  $("#scrolls").height($("#myCanvas").height());

        var BB = canvas.getBoundingClientRect();

        canvas.height = $("#myCanvas").height();
        canvas.width = $("#myCanvas").width();
        //  $("#scrolls").height(canvas.height);

        offsetX = BB.left;
        offsetY = BB.top;


        visibleWidth = canvas.width;
        visibleHeight = canvas.height;
        width = canvas.width;
        height = canvas.height;




        drawImage1(true);

        drawCanvas();
        $("#sidebar-wrapper").height($(window).height() - $("#sidebar-wrapper").offset().top - 21);
        $("#detalleplayas").height($(window).height() - $("#sidebar-wrapper").offset().top - 143);
        colocacontroles();
    }

}

function colocacontroles() {

    $("#controles").css('margin-top', canvas.height - 80);
    $("#controles").css('margin-left', canvas.width - 40);
    $("#lugarpanel").css('margin-left', canvas.width - 140);



}

var offsetX, offsetY;
//      reOffset();
//     window.onscroll=function(e){ reOffset(); }
window.onresize = function (e) { reOffset(); }
window.onorientationchange = function () { reOffset(); }

function showContextMenu(r, x, y) {
    if (!r) { $menu.hide(); return; }
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
    //                         context.beginPath();
    //                      context.fillStyle='green';
    //                        context.rect(x*scale+drawPos[0],  y*scale+drawPos[1],50,50);
    if (rect != undefined) {
        selecciona(rect);
        $(rect).contextMenu({ x: xmouse + offsetX, y: ymouse + offsetY });
    }



    return (false);
}




function imagenen(x, y, offset) {
    //return playas[0].estaciones[0];


    if (offset == true) {
        x = (parseInt(x - offsetX) / scale - drawPos[0] / scale);
        y = (parseInt(y - offsetY) / scale - drawPos[1] / scale);

    }


    for (var j = 0; j < playas.length; j++) {
        var playa = playas[j];
        var x11 = playa.x1;
        var y11 = playa.y1;
        var rectRight1 = x11 + playa.width1;
        var rectBottom1 = y11 + playa.height1;

        var xmouse1 = x;
        var ymouse1 = y;

        if (xmouse1 >= x11 && xmouse1 <= rectRight1 && ymouse1 >= y11 && ymouse1 <= rectBottom1 && playa.mostrar == true) {

            for (var i = 0; i < playa.estaciones.length; i++) {
                var rect = playa.estaciones[i];
                var x1 = rect.x1;
                var y1 = rect.y1;
                var rectRight = x1 + rect.width1;
                var rectBottom = y1 + rect.height1;

                var xmouse = x;
                var ymouse = y;


                if (xmouse >= x1 && xmouse <= rectRight && ymouse >= y1 && ymouse <= rectBottom) {
                    return rect;

                }

            }


        }

    }

    //             for (var i = 0; i < lugares.length; i++) {
    //                 var rect = lugares[i];
    //                 var x1 = rect.x1 * scale + drawPos[0];
    //                 var y1 = rect.y1 * scale + drawPos[1];
    //                 var rectRight = x1 + rect.width1 * scale;
    //                 var rectBottom = y1 + rect.height1 * scale;

    //                 var xmouse = x * scale + drawPos[0];
    //                 var ymouse = y * scale + drawPos[1];


    //                 if (xmouse >= x1 && xmouse <= rectRight && ymouse >= y1 && ymouse <= rectBottom && rect.playa.mostrar == true) {
    //                     return rect;

    //                 }

    //             }

}





function drawImage1(forzar) {
    var w = image.width;
    var h = image.height;
    var escala = Math.round(canvas.width / w * 1000) / 1000;
    if (h * escala < canvas.height) escala = Math.round(canvas.height / h * 1000) / 1000;

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
    context.fillStyle = "#FFFFFF";
    context.fillRect(0, 0, canvas.width, canvas.height);

    var w = image.width * scale;
    var h = image.height * scale;
    // var x = drawPos[0];
    // var y = drawPos[1]; 
    var x = drawPos[0]; // - (w / 2);
    var y = drawPos[1]; //- (h / 2);
    //   context.drawImage(image, x, y, w, h);
    context.drawImage(image, x, y, w, h);

}

var zoomiando = false;

// Set the zoom with the mouse wheel
function zoom(e) {

    if (zoomiando == true) return;

    zoomiando = true;

    var wheelDelta;
    if (e.wheelDelta == undefined) wheelDelta = -e.detail
    else wheelDelta = e.wheelDelta;

    if (wheelDelta > 0) {
        zoomIn(e);
    }
    else {
        zoomOut(e);
    }

    zoomiando = false;
}

// Zoom in
function zoomIn(event) {

    var wheelDelta;
    if (event.wheelDelta == undefined) wheelDelta = event.detail
    else wheelDelta = event.wheelDelta;

    if (scale < MAX_ZOOM) {
        //    drawCanvas();
        var mousex = event.clientX - canvas.offsetLeft;
        var mousey = event.clientY - canvas.offsetTop;
        // Normalize wheel to +1 or -1.


        var wheel;

        if (wheelDelta == undefined || wheelDelta < 120) {
            wheel = 1;
            mousex = canvas.offsetLeft + canvas.width / 2;
            mousey = canvas.offsetTop + canvas.height / 2;
        }

        else wheel = wheelDelta / 120;

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

        var rawCenterX = (canvas.offsetLeft + canvas.width) / 2 * scale; ///scale;
        var rawCenterY = (canvas.offsetTop + canvas.height) / 2 * scale; ///scale;

        // Delta
        var deltaX = (rawCenterX - mousex) ;
        var deltaY = (rawCenterY - mousey) ;

        drawPos = [drawPos[0] + deltaX * wheel * scale, drawPos[1] + deltaY * wheel * scale];
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
    else {
        drawCanvas();
    }
}


function normalizar_pos() {
    var ancho = image.width * scale;
    var alto = image.height * scale;

    if (drawPos[0] > 0) drawPos[0] = 0;
    else if (drawPos[0] + ancho < canvas.width) drawPos[0] = canvas.width - ancho;

    if (drawPos[1] > 0) drawPos[1] = 0;
    else if (drawPos[1] + alto < canvas.height) drawPos[1] = canvas.height - alto;

    DRAW_POS = drawPos;
}
var maximizado

function Maximizar() {

    if (maximizado == undefined || maximizado == null) {
        $('#filtros').hide();
        $('#Container').hide();
        //  $('#scrolls').height('900px');
        $("#scrolls").height($(window).height() - $("#scrolls").offset().top +5);


        reOffset();
        normalizar_pos();
        maximizado = 1;

    }
    else {
        maximizado = null;

        $('#filtros').show();
        $('#Container').show();
        $("#scrolls").height($(window).height() - $("#scrolls").offset().top - 15);

        reOffset();
        normalizar_pos();

    }
    drawCanvas();

}

// Zoom out
function zoomOut(event) {
    var wheelDelta;
    if (event.wheelDelta == undefined) wheelDelta = event.detail
    else wheelDelta = event.wheelDelta;

    if (scale.toFixed(3) > MIN_ZOOM) {


        var mousex = event.clientX - canvas.offsetLeft;
        var mousey = event.clientY - canvas.offsetTop;
        // Normalize wheel to +1 or -1.
        var wheel;


        if (wheelDelta == undefined || wheelDelta < 120) {
            wheel = -1;
            mousex = canvas.offsetLeft + canvas.width / 2;
            mousey = canvas.offsetTop + canvas.height / 2;
        }

        else wheel = wheelDelta / 120;

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


        var rawCenterX = (canvas.offsetLeft + canvas.width) / 2 * scale; ///scale;
        var rawCenterY = (canvas.offsetTop + canvas.height) / 2 * scale; ///scale;

        // Delta
        var deltaX = (rawCenterX - mousex);
        var deltaY = (rawCenterY - mousey);

      //  drawPos = [drawPos[0] + deltaX * (wheel) * scale, drawPos[1] + deltaY * (wheel)* scale];
      //  DRAW_POS = drawPos;

        //       drawPos = [drawPos[0] - deltaX*scale/2, drawPos[1] - deltaY*scale/2];

        //  mousePos = [e.x, e.y];
        //  drawCanvas();


        // Update scale and others.
        scale *= zoom;
        visibleWidth = width / scale;
        visibleHeight = height / scale;

        if (scale < MIN_ZOOM) drawImage1();
        normalizar_pos();
        drawCanvas(zoom);
        // zoomcanvas()                       
    }
    else {
        drawCanvas();
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

// Toggle mouse status
function setMouseDown(e) {
    //      drawImage();
    //drawCanvas();
    xevento = e.x;
    yevento = e.y;

    arrastrado = imagenen(e.x, e.y, true);

    if (arrastrado == undefined) {

        ArrastrandoMapa = true;
        lugararrastrado = null;
        // drawImage();
        drawCanvas();
    }
    else {
        lugararrastrado = arrastrado;
        if (arrastrado != lugarseleccionado) {
            selecciona(arrastrado);
            ArrastrandoMapa = false;
            
            // recta(arrastrado,'orange');

        }

        drawCanvas();
    }



    mousePos = [e.x, e.y];
}


var timer;
var timer2;
var MouseX;
var MouseY;
var temp_e;


function setMouseUp(e) {
    clearTimeout(timer);
    ArrastrandoMapa = false;



    if ((e.x == xevento && e.y == yevento) || e.button != 0) {
        var rect = imagenen(e.x, e.y, true);
        destino();
        if (rect == undefined) selecciona();
        drawCanvas();
    }

    else {

        var rect = imagenen(e.x, e.y, true);

        if (rect == undefined) {
            //   lugardestino=null;
            destino();
            drawCanvas();

        }
        else if (lugararrastrado != null && rect != lugarseleccionado) {
            //         lugardestino=rect;
            destino(rect);
            drawCanvas();
            //      recta(rect,'blue');
        }
    }

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
    clearTimeout(timer);
    clearTimeout(timer2);
    // $('.tool').remove();
    // $('.tool1').remove();
    delta = [e.x - mousePos[0], e.y - mousePos[1]];
    MouseX = mousePos[0];
    MouseY = mousePos[1];
    temp_e = e;

    if (ArrastrandoMapa) {
        var dX = 0, dY = 0;
        drawPos = [drawPos[0] + delta[0], drawPos[1] + delta[1]];
        normalizar_pos();
        DRAW_POS = drawPos;
        mousePos = [e.x, e.y];
        drawCanvas();
     //   drawImage();
    //    clearTimeout(timer2);
     //   timer2 = setTimeout(drawCanvas, 1);
    }
    else {
        // drawimage();
        //     timer2 = setTimeout(drawCanvas, 50);

        var rect = imagenen(e.x, e.y, true);
        if (arrastrado != undefined) {
            var patente = arrastrado.getAttribute('patente');

            if (patente != 'vacio') {

                var dX = 0, dY = 0;
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
                // drawImage();
                //  timer2 = setTimeout(drawCanvas, 10);

                //    drawCanvas();
            }

        }


        drawCanvas();



        if (rect == undefined) {
            //  drawCanvas();
            timer = setTimeout(MouseStopped, 300);
        } else if (delta[0] != 0 && delta[1] != 0) {
            //    drawCanvas();
            //  drawImage();
            // timer2 = setTimeout(drawCanvas, 50);
            muestratooltip(e, rect);
        }

        if (arrastrado != undefined) {


            if (rect != undefined && patente != 'vacio') {

                //  drawCanvas(); 
                recta(rect, 'blue'); muestratooltip(e, rect);

            }

        }

    }

    if ((arrastrado != undefined && arrastrado.getAttribute('patente') != 'vacio') && (delta[0] != 0 || delta[1] != 0)) muestratooltip1(e, arrastrado)




}



function MouseStopped() {

    if (arrastrado != undefined && arrastrado.getAttribute('patente') != 'vacio') {
        //      alert(MouseX);
        drawPos = [drawPos[0] - delta[0] / 3, drawPos[1] - delta[1] / 3];
        DRAW_POS = drawPos;
        normalizar_pos();

        drawCanvas();
        muestratooltip1(temp_e, arrastrado);
        clearTimeout(timer);
        timer = setTimeout(MouseStopped, 3);
        // clearTimeout(timer);

    }



}




function cambio_mapa() {

    //              var imagen1="http://198.41.36.102/QYMS/images/yms_wallmart_hd2.jpg";
    //              var imagen2="http://198.41.36.102/QYMS/images/cd_quilicuda_walmart5.png";

    //              if (image.src=="")
    //                {
    //                  image.src = imagen1;
    //                  return;

    //                  }

    if ($('#_cambio_mapa').val() == 1) {
        scale = null;
        selecciona();
    }


    //  scale = null;
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





// Unlike images, textures do not have a width and height associated
// with them so we'll pass in the width and height of the texture
//         WebGLRenderingContext.prototype.drawImage = function (tex, texWidth, texHeight, dstX, dstY) {

//             var texture = this.createTexture();
//             this.activeTexture(this.TEXTURE0);
//             this.bindTexture(this.TEXTURE_2D, texture);
//         //    this.texImage2D(this.TEXTURE_2D, 0, tex);
//             this.generateMipmap(this.TEXTURE_2D);

//            

//             // Tell WebGL to use our shader program pair
//             this.useProgram(program);

//             // Setup the attributes to pull data from our buffers
//             this.bindBuffer(this.ARRAY_BUFFER, positionBuffer);
//             this.enableVertexAttribArray(positionLocation);
//             this.vertexAttribPointer(positionLocation, 2, this.FLOAT, false, 0, 0);
//             this.bindBuffer(this.ARRAY_BUFFER, texcoordBuffer);
//             this.enableVertexAttribArray(texcoordLocation);
//             this.vertexAttribPointer(texcoordLocation, 2, this.FLOAT, false, 0, 0);

//             // this matirx will convert from pixels to clip space
//             var matrix = m4.orthographic(0, this.canvas.width, this.canvas.height, 0, -1, 1);

//             // this matrix will translate our quad to dstX, dstY
//             matrix = m4.translate(matrix, dstX, dstY, 0);

//             // this matrix will scale our 1 unit quad
//             // from 1 unit to texWidth, texHeight units
//             matrix = m4.scale(matrix, texWidth, texHeight, 1);

//             // Set the matrix.
//             this.uniformMatrix4fv(matrixLocation, false, matrix);

//             // Tell the shader to get the texture from texture unit 0
//             this.uniform1i(textureLocation, 0);

//             // draw the quad (2 triangles, 6 vertices)
//             this.drawArrays(this.TRIANGLES, 0, 6);
//         }



CanvasRenderingContext2D.prototype.roundRect = function (x, y, w, h, r) {
    if (w < 2 * r) r = w / 2;
    if (h < 2 * r) r = h / 2;
    this.beginPath();
    this.moveTo(x + r, y);
    this.arcTo(x + w, y, x + w, y + h, r);
    this.arcTo(x + w, y + h, x, y + h, r);
    this.arcTo(x, y + h, x, y, r);
    this.arcTo(x, y, x + w, y, r);
    this.stroke();
    this.closePath();
    return this;
}


function muestratooltip(e, img) {
    // Hover over code

    if (notooltip == 1) {
        notooltip = 0;
        // $(this).data('tipText', title).removeAttr('title');
        return false;

    }

    var title = img.getAttribute('codigo_lugar') + ' ' + img.getAttribute('title1') +',' +img.getAttribute('patente');
    //   $(this).data('tipText', title).removeAttr('title');
    //             var $yo = $('<p class="tool"></p>')
    //        .text(title)
    //        .appendTo('#CD')
    //             // .fadeIn('fast');
    //       .show();

    var mousex = e.clientX + 10; //Get X coordinates
    var mousey = e.clientY + 5; //Get Y coordinates

    //        $yo.css({ top: mousey, left: mousex })


    var x1 = mousex - offsetX; // * scale; + drawPos[0];
    var y1 = mousey - offsetY; // * scale + drawPos[1];

    context.beginPath();
    context.fillStyle = 'green';
    context.globalAlpha = 0.5;
    //    context.rect(x1, y1, 200, 40);
    context.roundRect(x1, y1, 200, 40, 5);
    context.fill();
    context.globalAlpha = 1;

    context.font = 12 + 'px Arial';
    context.fillStyle = '#fff';
    context.textAlign = "center";
    context.translate(x1 + 105, y1 + 25);

    context.fillText(title, 0, 0);

    context.translate(-x1 - 105, -y1 - 25);





}
function muestratooltip1(e, img) {
    // Hover over code
    var title = img.getAttribute('patente');

    if (title != 'vacio') {

        if (notooltip == 1) {
            notooltip = 0;
            // $(this).data('tipText', title).removeAttr('title');
            return false;

        }


        //                 //   $(this).data('tipText', title).removeAttr('title');
        //                 var $yo = $('<p class="tool1"></p>')

        //        .text('Moviendo: ' + title)
        //        .appendTo('#CD');
        //                 //  var $tu=$(img);

        var mousex = e.clientX + 10; //Get X coordinates
        var mousey = e.clientY - 50 + 5; //Get Y coordinates

        //                 $yo.css({ top: mousey, left: mousex })
        //                 //  $tu.css({ top: mousey, left: mousex, display:'block', visibility:'visible', position:'absolute' }) 

        var x1 = mousex - offsetX; // * scale; + drawPos[0];
        var y1 = mousey - offsetY; // * scale + drawPos[1];

        context.beginPath();
        context.fillStyle = 'blue';
        context.globalAlpha = 0.5;
        // context.rect(x1, y1,80 , 40 );
        context.roundRect(x1, y1, 80, 40, 5);
        context.fill();
        context.globalAlpha = 1;

        context.font = 12 + 'px Arial';
        context.fillStyle = '#fff';
        context.textAlign = "center";
        context.translate(x1 + 40, y1 + 20);

        context.fillText(title, 0, 0);

        context.translate(-x1 - 40, -y1 - 20);




    }
}





function recta(rect, color) {



    var x1 = rect.x1 * scale + drawPos[0];
    var y1 = rect.y1 * scale + drawPos[1];

    context.beginPath();
    //      context.globalCompositeOperation='destination-over';
    context.fillStyle = color;
    context.globalAlpha = 0.3;
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
        var movimiento = imagen.getAttribute('movimiento');
        var error = null;
        error = imagen.getAttribute('repetido');

        var x = imagen.x1 * scale + drawPos[0];
        var y = imagen.y1 * scale + drawPos[1];
        var height = imagen.height1 * scale;
        var width = imagen.width1 * scale;

        if (movimiento != "0" && movimiento != "") {
            context.beginPath();
            context.fillStyle = "orange";
            context.globalAlpha = 0.2;
            context.rect(x, y, width, height);
            context.fill();
            context.globalAlpha = 1;
        }

        if (error != undefined && error == "si") {
            //       alert(title);
            context.beginPath();
            context.fillStyle = "red";
            context.rect(((imagen.x1 * scale + drawPos[0])), (imagen.y1 * scale + drawPos[1]), imagen.width1 * scale, imagen.height1 * scale);
            context.fill();
            context.globalAlpha = 1;
        }


        context.save();
        if (title != undefined && title != 'vacio') {
           // context.shadowColor = "#000000";
            //context.shadowBlur = 10;
           // context.shadowOffsetX = width / 10; // -4+drawPos[0]*scale/100;
           // context.shadowOffsetY = -height / 10; //  -2+drawPos[1]*scale/100;
        }



        context.drawImage(imagen, Math.floor(x), Math.floor(y), width, height);

        var tamano = Math.min(imagen.width1, imagen.height1);

        context.font = tamano * 1 / 2 * scale + 'pt Calibri';
        context.fillStyle = 'white';
        context.strokeStyle = '#444444';
        context.textAlign = "center";
        var x1 = x + width * 2 / 3;
        var y1 = y + height * 2 / 3;
        context.translate(x1, y1);

        if ((imagen.getAttribute('rotacion') == 1) || (imagen.getAttribute('rotacion') == 3)) {

            if (imagen.getAttribute('rotacion') == 3) context.translate(0, -height * 1 / 4);
            context.rotate(-Math.PI / 2);

            

        }

        context.strokeText(title, 0, 0);
      context.fillText(title, 0, 0);
      

     //   context.rotate(Math.PI / 2);

        //  context.translate(-x1, -y1);


        context.restore();

        if (imagen.getAttribute('estado_carga') != undefined) {
            var img = new Image;
            img.src = imagen.getAttribute('estado_carga');
            context.drawImage(img, x, y, width, height);
        }


        if (imagen.getAttribute('estado_reloj') != undefined) {
            var img2 = new Image;
            img2.src = imagen.getAttribute('estado_reloj');
            context.drawImage(img2, x, y, width, height);
        }


      
        //  if ((imagen.getAttribute('rotacion') == 1) || (imagen.getAttribute('rotacion') == 3)) {
        //      context.rotate(Math.PI / 2);
        //  }



        //                 context.translate(-x1, -y1);


        //          if (title != undefined && title != 'vacio') {
        //            context.shadowColor = "rgba(0, 0, 0, 0)";
        //          context.shadowBlur = 0;
        //        context.shadowOffsetX = 0;
        //        context.shadowOffsetY = 0;
        //   }





        var $ottawa = $(imagen).next();
        if ($ottawa.is('img')) {

            var desfasex = 0;
            var desfasey = 0;
            if (imagen.getAttribute('rotacion') == 2) { desfasex = imagen.width1 * scale; desfasey = imagen.height1 / 4 * scale; }
            if (imagen.getAttribute('rotacion') == 4) { desfasex = -imagen.width1 * scale; desfasey = imagen.height1 / 4 * scale; }
            if (imagen.getAttribute('rotacion') == 1) { desfasey = -imagen.height1 * scale / 2; desfasex = imagen.width1 / 4 * scale; }
            if (imagen.getAttribute('rotacion') == 3) { desfasey = imagen.height1 * scale; desfasex = imagen.width1 / 4 * scale; }

            context.drawImage($ottawa[0], imagen.x1 * scale + drawPos[0] + desfasex, imagen.y1 * scale + drawPos[1] + desfasey, imagen.width1 / 2 * scale, imagen.height1 / 2 * scale);


        }


        //                    context.restore();

    }
}


function dibujaottawa($ottawa) {
    alert($ottawa);
}

function dibujaplaya(playa) {

    if (playa.mostrar == true && 1 == 2) {
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
        imagen.width1 = w;
        imagen.height1 = h;
        imagen.mostrar = true;
        imagen.contextMenu = [imagen.x1, 'Two red'];
        //    context.drawImage(imagen,imagen.x1, imagen.y1, 100* scale, 100* scale);
        dibujaplaya(imagen);
        imagen.estaciones = [];

        $(this).find(".icono").each(function () {
            var imagen2 = $(this)[0];
            lugares.push(imagen2);
            imagen.estaciones.push(imagen2);
            var w = imagen2.style.width.replace("%", "") * ratio;
            var h = imagen2.style.height.replace("%", "") * ratio2;
            imagen2.height1 = h;
            imagen2.width1 = w;
            ubicalugar(imagen2, imagen, ratio, ratio2);
            imagen2.contextMenu = [imagen2.x1, 'Two red'];
            imagen2.playa = imagen;
            dibujalugar(imagen2);

        });
    });
}


function clear() {

}

//clears the canvas and draws the rectangle at the appropriate location
function redraw() {
    clear();
}

//setInterval(redraw, 30);


function OnClientItemChecked(sender, eventArgs) {
    // alert(sender);
    // alert(eventArgs);

    for (var j = 0; j < playas.length; j++) {
        var playa = playas[j];
        playa.mostrar = true;
        //  var elemento=sender.findItemByValue(playa.id);
        var elemento = sender.find("#chk_" + playa.id).find(":first-child");

        playa.mostrar = elemento.is(':checked');
    }

    drawCanvas();



}

function todos() {
    var aux = $('#rlplayas');
    var items = aux.find("INPUT[type=checkbox]");
    items.each(function () { $(this).prop("checked", true); });
    OnClientItemChecked(aux, null);
}

function ninguno() {
    var aux = $('#rlplayas');
    var items = aux.find("INPUT[type=checkbox]");
    items.each(function () { $(this).prop("checked", false); });
    OnClientItemChecked(aux, null);
}


function traspasacanvas1() {

    //     context.fillStyle = "#FFFFFF";
    //    context.fillRect(0, 0, canvas.width, canvas.height);
    //  context.clearRect(0,0,canvas.width, canvas.height);
    //             for (var j = 0; j < playas.length; j++) {
    //                 var playa = playas[j];
    //                 if (playa.mostrar == true) dibujaplaya(playa);
    //             }






    for (var i = 0; i < lugares.length; i++) {
        var imagen = lugares[i];

        dibujalugar(imagen);

    }

    if (lugarseleccionado != null) {
        recta(lugarseleccionado, 'orange');
    }
    if (lugardestino != null) {
        recta(lugardestino, 'blue');
    }


    //             context.setTransform(1, 0, 0, 1, 0, 0);

}



function zoomcanvas() {


    //  context.clearRect(0,0,canvas.width, canvas.height);
    for (var j = 0; j < playas.length; j++) {
        var playa = playas[j];

        //    context.drawImage(image,100, 100, 100, 100);
        //     image.width = image.width1 * scale;
        //     image.height= image.height1 * scale;

        var ratio = image.width / 100;
        var ratio2 = image.height / 100;

        var w = playa.style.width.replace("%", "") * ratio; //* scale;
        var h = playa.style.height.replace("%", "") * ratio2; //* scale;
        //     playa.x1=drawPos[0]+playa.style.left.replace("%","") *ratio;
        //    playa.y1=drawPos[1]+playa.style.top.replace("%","") *ratio;
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
            //    context.drawImage(imagen,imagen.x1,  imagen.y1, imagen.width1 , imagen.height1);


        }



    }





}

function seleccionatrailer(trailer) {


    for (var i = 0; i < lugares.length; i++) {
        var imagen = lugares[i];

        if (imagen.getAttribute('patente') == trailer) {
            selecciona(imagen);
            drawCanvas();
        }

    }

}





