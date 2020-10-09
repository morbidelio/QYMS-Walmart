
function trim (myString)
{
return myString.replace(/^\s+/g,'').replace(/\s+$/g,'')
}


var listboxID;
var listbox = null;

var markers = {};
var mypolygon = {};
var myLabel = {};
var map;
var popup;

function cargamapa(zoom1) {
    var z = zoom1 || 10;

    var n = 1;
    var options = {
        zoom: z
        , center: new google.maps.LatLng(-33.45238466, -70.65735526)
        , mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    map = new google.maps.Map(document.getElementById('map'), options);
    
};


function marca(name, lat, lon, fec, dir, vel,id,selecciona) {

//function marca(name, dir,lat,lon,fec, id,vel) {
    //veo la direccion del movil
  
  //  var id=1;
    var icono
    if (dir > 338 || dir <= 22)
        icono = "./images/ico/camnegro_N.png";

    else if (dir > 22 && dir <= 67)
        icono = "./images/ico/camnegro_NE.png";

    else if (dir > 67 && dir <= 112)
        icono = "./images/ico/camnegro_E.png";

    else if (dir > 112 && dir <= 157)
        icono = "./images/ico/camnegro_SE.png";

    else if (dir > 157 && dir <= 202)
        icono = "./images/ico/camnegro_S.png";

    else if (dir > 202 && dir <= 257)
        icono = "./images/ico/camnegro_SO.png";

    else if (dir > 257 && dir <= 292)
        icono = "./images/ico/camnegro_O.png";

    else if (dir > 292 && dir <= 338)
        icono = "./images/ico/camnegro_NO.png";
    else if (dir > 338 && dir <= 360)
        icono = "./images/ico/camnegro_N.gif";
    else
        icono = "./images/ico/camnegro_N.gif";


    marker(id, name, icono, new google.maps.LatLng(lat, lon), fec, vel, selecciona);
}

function marker(id, marca, icono, location, fec, vel,selecciona) {
    if (markers[id]) {
        markers[id].setPosition(location);
        markers[id].setIcon(icono);
    }
    else {
        markers[id] = new google.maps.Marker({
            map: map
            , position: location
            , title: marca
            , icon: icono
            , visible: true
        });
    }
    if (selecciona == true) {

        if (document.getElementById('txt_movsel') == null) {
            window.parent.RadWindow2.document.getElementById('txt_movsel').innerHTML = marca;
        }
        else {
            document.getElementById('txt_movsel').innerHTML = marca;
        }
    }

    var mov1 = trim(marca);

    if (document.getElementById('txt_movsel') == null) {
        var mov2 = trim(window.parent.RadWindow2.document.getElementById('txt_movsel').innerHTML);
    }
    else {
        var mov2 = trim(document.getElementById('txt_movsel').innerHTML);
    }

    if (mov2 == mov1)
    
            map.setCenter(location);
    
            //esto es lo nuevoo creado por mi :)
            var label = new Label({
                map: map
            });
            label.bindTo('position', markers[id], 'position');
            label.bindTo('text', markers[id], 'title');
            myLabel[id] = label;         
           
            google.maps.event.addListener(markers[id], 'click', function() {                
                if (!popup) {
                    popup = new google.maps.InfoWindow();
                }
                var note = '<a style=" font-size:small; color:Black; font-family:Calibri">Móvil: ' + this.title + " <br />"
                            + 'Fecha: ' + fec + " <br />"
                            + 'Velocidad: ' + vel + "km/h <br /></a>";

                /*Nuevo*/
                for (var i = 1; i < markers.length; i++) {
                    if (markers[i].formatted_address)
                        note += '<a>' + markers[i].formatted_address + '</a><br />';
                    else
                        note += '<a>No se encontró información.</a>';
                }
                /*Nuevo*/
                            
                            
                popup.setContent(note);
                popup.open(map, this);
            });
}


function borra() {
    for (var m in markers) {      
        if (myLabel[m] != null) {
            myLabel[m].setMap(null);
            myLabel[m] = null;
            myPushpin = null;
        }        
        markers[m].setPosition(null);
        }
}


////////// Define the overlay, derived from google.maps.OverlayView
function Label(opt_options) {
    // Initialization
    this.setValues(opt_options);

    // Label specific
    var span = this.span_ = document.createElement('span');
    span.style.cssText = 'position: relative; left: -50%; top: -50px; ' +
                      'white-space: nowrap; border: 1px solid Black; ' +
                      'padding: 2px; background-color:White; font-size:small; color:Black; font-family:Calibri;margin:auto';

    var div = this.div_ = document.createElement('div');
    div.appendChild(span);
    div.style.cssText = 'position: absolute; display: none;';
};
Label.prototype = new google.maps.OverlayView;

// Implement onAdd
Label.prototype.onAdd = function() {
    var pane = this.getPanes().overlayLayer;
    pane.appendChild(this.div_);

    // Ensures the label is redrawn if the text or position is changed.
    var me = this;
    this.listeners_ = [
   google.maps.event.addListener(this, 'position_changed',
       function() { me.draw(); }),
   google.maps.event.addListener(this, 'text_changed',
       function() { me.draw(); })
 ];
};

// Implement onRemove
Label.prototype.onRemove = function() {
    this.div_.parentNode.removeChild(this.div_);

    // Label is removed from the map, stop updating its position/text.
    for (var i = 0, I = this.listeners_.length; i < I; ++i) {
        google.maps.event.removeListener(this.listeners_[i]);
    }
};

// Implement draw
Label.prototype.draw = function() {
    var projection = this.getProjection();
    var position = projection.fromLatLngToDivPixel(this.get('position'));
    var div = this.div_;
    div.style.left = position.x + 'px';
    div.style.top = position.y + 'px';
    div.style.display = 'block';
    this.span_.innerHTML = this.get('text').toString();
};

//para buscar direccion
var markerDIR
function buscaDIR() {
    // Obtenemos la dirección y la asignamos a una variable
    var address = document.getElementById("address").value;
    // Creamos el Objeto Geocoder
    var geocoder = new google.maps.Geocoder();
    // Hacemos la petición indicando la dirección e invocamos la función
    // geocodeResult enviando todo el resultado obtenido
    geocoder.geocode({ 'address': address }, geocodeResult);
}

function geocodeResult(results, status) {
    // Verificamos el estatus
    BorraMapa();
    if (status == 'OK') {
        // Si hay resultados encontrados, centramos y repintamos el mapa
        // esto para eliminar cualquier pin antes puesto
        var mapOptions = {
            center: results[0].geometry.location,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        //map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
        // fitBounds acercará el mapa con el zoom adecuado de acuerdo a lo buscado
        map.fitBounds(results[0].geometry.viewport);
        // Dibujamos un marcador con la ubicación del primer resultado obtenido
        var markerOptions = { position: results[0].geometry.location, icon: "ico/result.png" }
        markerDIR = new google.maps.Marker(markerOptions);
        markerDIR.setMap(map);
    } else {
        // En caso de no haber resultados o que haya ocurrido un error
        // lanzamos un mensaje con el error
        alert("Geocoding no tuvo éxito debido a: " + status);
    }
}

function BorraMapa() {
    if (markerDIR!= null)
        markerDIR.setPosition(null);
}

    //////////// para buscar direccion para las rutas /////////////////

    var directionDisplay;
    var directionsService = new google.maps.DirectionsService();

    function calcRoute() {
        directionsDisplay = new google.maps.DirectionsRenderer();
        directionsDisplay.setMap(map);
        directionsDisplay.setPanel(document.getElementById("directionsPanel"));
        var start = document.getElementById("start").value;
        var end = document.getElementById("end").value;
        
        //pageTracker._trackEvent('ruta', 'apretar_boton_ruta_pagina_especifica', 'de:'+start+' a:'+end); //Evento Google Analytics
        var request = {
            origin: start,
            destination: end,
            travelMode: google.maps.DirectionsTravelMode.DRIVING
        };
        directionsService.route(request, function(response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
            }
        });

        geocoder = new google.maps.Geocoder();


        geocoder.geocode({ 'address': start }, function(results, status) {
            if (status = !google.maps.GeocoderStatus.OK) {

                alert("Geocode was not successful for the following reason: " + status);
            } else {
                document.getElementById('uno').innerHTML = results[0].geometry.location;
            }
        });

        geocoder.geocode({ 'address': end }, function(results, status) {
            if (status = !google.maps.GeocoderStatus.OK) {

                alert("Geocode was not successful for the following reason: " + status);
            } else {
                document.getElementById('dos').innerHTML = results[0].geometry.location;
            }
        });
    }


    function buscaMov(mov) {
        var x, y;
        var zoom;

       
        //document.images["btcruz"].style.height = "10px";
        //document.getElementById("<% response.write(txt_movsel.clientid) %>").innerHTML = mov;
        document.getElementById('txt_movsel').innerHTML = mov;        
        zoom = map.getZoom();
        for (var m in markers) {
            if (myLabel[m] != null) {
                myLabel[m].setMap(null);
                myLabel[m] = null;
                myPushpin = null;
            }            
            //var lat = markers[m].position.Na;
            //var lng = markers[m].position.Oa;
            var markerLatLng = markers[m].getPosition();
            var lat = markerLatLng.lat();
            var lng = markerLatLng.lng();
            var myLatLng = new google.maps.LatLng(lat, lng)                  
                if (markers[m].title==mov)
                    map.setCenter(myLatLng);


        }
        }

        function marca_movil_viaje(name, lat, lon, fec, dir, vel) {
            var icono

            if (dir > 338 || dir <= 22)
                icono = "./images/ico/camnegro_N.png";

            else if (dir > 22 && dir <= 67)
                icono = "./images/ico/camnegro_NE.png";

            else if (dir > 67 && dir <= 112)
                icono = "./images/ico/camnegro_E.png";

            else if (dir > 112 && dir <= 157)
                icono = "./images/ico/camnegro_SE.png";

            else if (dir > 157 && dir <= 202)
                icono = "./images/ico/camnegro_S.png";

            else if (dir > 202 && dir <= 257)
                icono = "./images/ico/camnegro_SO.png";

            else if (dir > 257 && dir <= 292)
                icono = "./images/ico/camnegro_O.png";

            else if (dir > 292 && dir <= 338)
                icono = "./images/ico/camnegro_NO.png";
            else if (dir > 338 && dir <= 360)
                icono = "./images/ico/camnegro_N.gif";
            else
                icono = "./images/ico/camnegro_N.gif";

            //alert(icono);
            
            marker_movil_viaje(1, name, icono, new google.maps.LatLng(lat, lon), fec, vel);
        }

        function marker_movil_viaje(id, marca, icono, location, fec, vel) {
            if (markers[id]) {
                markers[id].setPosition(location);
                markers[id].setIcon(icono);
            }
            else {
                markers[id] = new google.maps.Marker({
                    map: map
            , position: location
            , title: marca
            , icon: icono
            , visible: true
                });
            }

            var mov1 = trim(marca);
            map.setCenter(location);

            var label = new Label({
                map: map
            });

            label.bindTo('position', markers[id], 'position');
            label.bindTo('text', markers[id], 'title');
            myLabel[id] = label;
       }

        function attachPolygonInfoWindow(polygon, html){
            polygon.infoWindow = new google.maps.InfoWindow({
                content: html
            });


            //mouseover

            google.maps.event.addListener(polygon, 'click', function(e) {
                var latLng = e.latLng;
                this.setOptions({fillOpacity:0.35});
                polygon.infoWindow.setPosition(latLng);
                polygon.infoWindow.open(map);
            });

//            google.maps.event.addListener(polygon, 'mouseout', function() {
//                this.setOptions({fillOpacity:0.35});
//                polygon.infoWindow.close();
//            });
        }

       function procesa() {
            var icono;
            
           routePoints = new Array();
           var k = 0;
           routePoints[k] = new google.maps.MVCArray();

           //cargaIdListbox(id);

           //document.getElementById('map')

           listbox = document.getElementById('ListClientes')


           var cant, i, mivar, x, nombre, polyline;

           cant = listbox.options.length;
           
           //alert(cant);


           var puntos = new Array();
           var puntos2 = new Array();
           mivar = listbox.options[0].text;
           var arreglo = mivar.split('*');
           nombre = arreglo[0];
           x = 0;
           var j = 0;
           var Poligono;

           var location1 = new Array();
           var location2 = new Array();
           var lat_ant, lon_ant;
            var radio = 0;
            var is_poligono, estado, llegada;
            var color_fondo = "#FF0000"; //default rojo

           for (i = 0; i <= cant - 1; i++) {
                mivar = listbox.options[i].text;
                var arreglo = mivar.split('*');

                if (estado == "Origen") {
                    color_fondo = "#0000FF"; //azul
                } else {
                    color_fondo = "#FF0000"; //rojo
                }

                if (nombre != arreglo[0]) {
                    if (is_poligono == "0"){
                        var populationOptions = {
                            strokeColor: color_fondo,
                            strokeOpacity: 0.8,
                            strokeWeight: 2,
                            fillColor: color_fondo,
                            fillOpacity: 0.35,
                            map: map,
                            center: new google.maps.LatLng(lat_ant, lon_ant),
                            radius: radio
                        };

                        cityCircle = new google.maps.Circle(populationOptions);
                        
                        if (estado=="Realizado"){
                            icono = "./images/casa_ok.png";
                            attachPolygonInfoWindow(cityCircle, '<strong>' + nombre + '</strong><br/>' + estado + '<br/>' + llegada);
                        }
                        else{
                            icono = "./images/casa.png";
                            attachPolygonInfoWindow(cityCircle, '<strong>' + nombre + '</strong><br/>' + estado);
                        }
                    }
                    else{
                        routePath = new google.maps.Polygon({
                            path: routePoints[k],
                            map: map,
                            strokeColor: color_fondo,
                            strokeOpacity: 0.8,
                            strokeWeight: 2,
                            fillColor: color_fondo,
                            fillOpacity: 0.35
                        });
                        
                        if (estado=="Realizado"){
                            icono = "./images/casa_ok.png";
                            attachPolygonInfoWindow(routePath, '<strong>' + nombre + '</strong><br/>' + estado + '<br/>' + llegada);
                        }
                        else{
                            icono = "./images/casa.png";
                            attachPolygonInfoWindow(routePath, '<strong>' + nombre + '</strong><br/>' + estado);
                        }
                    }

                    if (estado != "Origen") {
                        var marker = new google.maps.Marker({
                            position: new google.maps.LatLng(lat_ant, lon_ant)
                        , map: map
                        , title: nombre
                        , icon: icono
                        });
                    }
                    
                    k++;
                    routePoints[k] = new google.maps.MVCArray();
                    nombre = arreglo[0];
                }
                
                if (x == 0) {
                    cadena = arreglo[0];

                    var address = new google.maps.LatLng(arreglo[1], arreglo[2]);
                    lat_ant = arreglo[1];
                    lon_ant =  arreglo[2];
                    radio = parseInt(arreglo[3]);
                    is_poligono = arreglo[4];
                    estado = arreglo[6];
                    llegada = arreglo[7];

                    routePoints[k].insertAt(routePoints[k].length, address);
                }
            }

            //FIN FOR

            if (estado == "Origen") {
                color_fondo = "#0000FF"; //azul
            } else {
                color_fondo = "#FF0000"; //rojo
            }
            
            if (is_poligono == "0"){
                var populationOptions = {
                    strokeColor: color_fondo,
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: color_fondo,
                    fillOpacity: 0.35,
                    map: map,
                    center: new google.maps.LatLng(lat_ant, lon_ant),
                    radius: radio
                };

                cityCircle = new google.maps.Circle(populationOptions);

                if (estado=="Realizado"){
                    icono = "./images/casa_ok.png";
                    attachPolygonInfoWindow(cityCircle, '<strong>' + cadena + '</strong><br/>' + estado + '<br/>' + llegada);
                }
                else{
                    icono = "./images/casa.png";
                    attachPolygonInfoWindow(cityCircle, '<strong>' + cadena + '</strong><br/>' + estado);
                }
            }
            else{
                routePath = new google.maps.Polygon({
                    path: routePoints[k],
                    map: map,
                    strokeColor: color_fondo,
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: color_fondo,
                    fillOpacity: 0.35
                });
                
                if (estado=="Realizado"){
                    icono = "./images/casa_ok.png";
                    attachPolygonInfoWindow(routePath, '<strong>' + cadena + '</strong><br/>' + estado + '<br/>' + llegada);
                }
                else{
                    icono = "./images/casa.png";
                    attachPolygonInfoWindow(routePath, '<strong>' + cadena + '</strong><br/>' + estado);
                }
            }

            if (estado != "Origen") {
                var marker = new google.maps.Marker({
                    position: new google.maps.LatLng(lat_ant, lon_ant)
                , map: map
                , title: cadena
                , icon: icono
                });
            }
       }

        function cargaIdListbox(id) {
            listboxID = id;
            getListBoxControl();
        }

        function getListBoxControl() {
            if (null == listbox || listbox.options.length == 0) {
                listbox = document.getElementById(listboxID);
            }
        }

        function carga_pe(lat, lon, radio, nom, dir) {
            var radio_int = 0;
            radio_int = parseInt(radio);

            var populationOptions = {
                strokeColor: "#FF0000",
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: "#FF0000",
                fillOpacity: 0.35,
                map: map,
                center: new google.maps.LatLng(lat, lon),
                radius: radio_int
            };

            cityCircle = new google.maps.Circle(populationOptions);
            attachPolygonInfoWindow(cityCircle, '<strong>' + nom + '</strong><br/>' + dir);
            map.setCenter(new google.maps.LatLng(lat, lon));
            map.setZoom(12);

        }

        function carga_punto(lat, lon) {
            var marker = new google.maps.Marker({
                position: new google.maps.LatLng(lat, lon)
                , map: map
                , title: ""
            });

            map.setCenter(new google.maps.LatLng(lat, lon));
        }
