/* File Created: September 30, 2014 */
/**
 * [Map description]
 * @param {[type]}   container [description]
 * @param {Function} callback  [description]
 */
function Map(containerId, coordinates, callback) {
    var htmlContent = ''
        + '    <div class="col-sm-8" style="padding-left: 0px;">'
        + '        <input type="text" id="searchTextBox" class="form-control col-lg-8" placeholder="Please enter a location for searching" />'
        + '    </div>'
        + '    <button type="button" class="btn btn-default" id="addressSearchButton">Search</button>'
        + '    <span id="spanMessage"></span>'
        + '    <br />'
        + '    <div id="mapCanvas" style="height:500px;margin-top: 1px;"></div>';
    $("#" + containerId).append(htmlContent);

    var geocoder = new google.maps.Geocoder();
    //var latLng = new google.maps.LatLng(23.134583018105783, 113.31278795581966);
    var latLng = new google.maps.LatLng(40.7127837, -74.00594130000002);
    if (coordinates) {
        var arr = coordinates.split(",");
        latLng = new google.maps.LatLng(parseFloat(arr[0]), parseFloat(arr[1]));
    }

    //debugger;

    var map;
    var marker;
    var infowindow;

    var infoContent = '<div id="mapinfo">'
        //+ '    <div id="siteNotice"></div>'
        + '    <h3 id="firstHeading" class="firstHeading">Address</h3>'
        + '    <div id="bodyContent">'
        + '        <p>{0}</p>'
        + '    </div>'
        + '    <div id="window_btn"><button class="confirm_btn">Confirm</button></div>'
        + '</div>';

    function geocodePosition(pos) {
        geocoder.geocode({ latLng: pos }, function(responses) {
            if (responses && responses.length > 0) {
                updateMarkerAddress(responses[0].formatted_address);
            } else {
                updateMarkerAddress('Geocode was not found');
            }
        });
    }

    function updateMarkerStatus(str) {
        $("#spanMessage").html(str);
    }

    function updateMarkerPosition(latLng) {
        callback && callback(null, [latLng.lat(), latLng.lng()].join(', '));
    }

    function updateMarkerAddress(address) {
        var content = infoContent.replace(/\{0\}/g, address || "");
        infowindow.setContent(content);
        infowindow.open(map, marker);
    }

    function addMarkerListeners(marker) {
        // 添加拖动事件监听器  
        google.maps.event.addListener(marker, 'dragstart', function() {
            updateMarkerAddress('Searching...');
        });

        google.maps.event.addListener(marker, 'drag', function() {
            updateMarkerStatus('Searching...');
            //updateMarkerPosition(marker.getPosition());
        });

        google.maps.event.addListener(marker, 'dragend', function() {
            updateMarkerStatus('');
            geocodePosition(marker.getPosition());
        });

        google.maps.event.addListener(marker, 'click', function() {
            infowindow.open(map, marker);
        });
    }
    
    function findCoordinate(address) {
        updateMarkerStatus('');
        geocoder.geocode( { 'address': address }, function(results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                latLng = results[0].geometry.location;

                map.setCenter(latLng);
                marker.setPosition(latLng);
                marker.setTitle(results[0].geometry.premise);

                // 更新当前的位置信息  
                //updateMarkerPosition(latLng);
                geocodePosition(latLng);
            } else {
                updateMarkerStatus('Cannot find the location.');
            }
        });
    }

    $("#addressSearchButton").on("click", function() {
        var address = $("#searchTextBox").val();
        if ($.trim(address)) {
            findCoordinate(address);
        }
    });

    this.render = function() {
        map = new google.maps.Map(document.getElementById("mapCanvas"), {
            zoom: 16,
            center: latLng,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            /* 禁止鼠标滚轮放大缩小地图 */
            scrollwheel: false
        });

        marker = new google.maps.Marker({
            position: latLng,
            title: 'You can drag it to locate',
            //icon: "logo.png",
            map: map,
            draggable: true
        });

        addMarkerListeners(marker);

        infowindow = new google.maps.InfoWindow({ content: "" });
        google.maps.event.addListener(infowindow, "domready", function() {
            $("#window_btn button.confirm_btn").on("click", function(e) {
                e.preventDefault();
                updateMarkerPosition(marker.getPosition());
                infowindow.close();
                return false;
            });
        });

        // 更新当前的位置信息  
        //updateMarkerPosition(latLng);
        geocodePosition(latLng);

        $("#searchTextBox").on("keyup", function(e) {
            updateMarkerStatus('');
        });
    }

    // 加载应用程序。  
    google.maps.event.addDomListener(window, 'load', this.render);
}
