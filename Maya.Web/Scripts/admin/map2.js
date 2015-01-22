/* File Created: September 30, 2014 */

;(function($) {
    /**
     * [CoordinateSelector description]
     * @param {[type]}   container [description]
     * @param {Function} callback  [description]
     */
    function CoordinateSelector(el, options) {
        var that = this;
            defaults = {
                lat: 0, // 纬度值
                lng: 0, // 经度值
                onSelect: null  // 选中坐标之后的callback方法
            };

        that.element = el;  // 当前元素dom对象
        that.el = $(el);    // 当前元素jQuery对象
        that.options = $.extend({}, defaults, options);

        // A service for converting between an address and a LatLng
        that.geocoder = null;
        //that.latLng = new google.maps.LatLng(23.134583018105783, 113.31278795581966);
        that.latLng = new google.maps.LatLng(40.7127837, -74.00594130000002);
        that.map = null;        // google map instance
        that.marker = null;     // 地图上的标识点
        that.infowindow = null; // 信息窗口，可以展示标识点的详细信息
        // 信息窗口的内容
        that.infoContent = '<div id="mapinfo scrollFix">'
                         + '    <h5 class="firstHeading">地址</h5>'
                         + '    <div id="bodyContent">'
                         + '        <p>{0}</p>'
                         + '    </div>'
                         + '    <div id="window_btn"><button class="confirm_btn">Confirm</button></div>'
                         + '</div>';

        that.initialize();
    }

    CoordinateSelector.prototype = {
        initialize: function() {
            var that = this,
                container = that.el,
                options = that.options;

            container.html('<div class="input-group">'
                         + '    <input type="text" id="searchTextBox" class="form-control" placeholder="请输入地址关键字搜索" />'
                         + '    <span class="input-group-btn">'
                         + '        <button class="btn default" type="button" id="addressSearchButton"><i class="fa fa-search"></i></button>'
                         + '    </span>'
                         + '</div>'
                         + '<div id="mapCanvas" style="height:500px;margin-top: 1px;"></div>');

            that.mapCanvas = $("#mapCanvas", container);
            $("#addressSearchButton", container).on("click", function() {
                var address = $("#searchTextBox").val();
                if ($.trim(address)) {
                    that.findCoordinate(address);
                }
            });

            that.geocoder = new google.maps.Geocoder();
            if (options.lat && options.lng) {
                that.latLng = new google.maps.LatLng(parseFloat(options.lat), parseFloat(options.lng));
            }

            // 加载应用程序。  
            // 说明：这种调用方式会导致render方法里面this的指向问题，this会为window对象
            //google.maps.event.addDomListener(window, 'load', that.render);
            
            that.render();
        },
        /**
         * [render description]
         * @return {[type]} [description]
         */
        render: function() {
            var that = this,
                onSelect = that.options.onSelect;

            that.map = new google.maps.Map(that.mapCanvas.get(0), {
                zoom: 16,
                center: that.latLng,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                /* 禁止鼠标滚轮放大缩小地图 */
                scrollwheel: false
            });

            that.marker = new google.maps.Marker({
                position: that.latLng,
                title: '可以拖动到指定的地点',
                //icon: "logo.png",
                map: that.map,
                draggable: true
            });

            that.addMarkerListeners();

            that.infowindow = new google.maps.InfoWindow({ content: "" });
            google.maps.event.addListener(that.infowindow, "domready", function() {
                $("#window_btn button.confirm_btn").on("click", function(e) {
                    e.preventDefault();

                    // 触发回调方法
                    onSelect && onSelect({ lat: that.latLng.lat(), lng: that.latLng.lng() });
                    // 关闭信息窗口
                    that.infowindow.close();
                    return false;
                });
            });

            that.updateMarkerAddress();
        },
        /**
         * 根据坐标得到地址信息（latLng -> Address)
         * @return {[type]}     [description]
         */
        updateMarkerAddress: function() {
            var that = this,
                pos = that.latLng;

            that.geocoder.geocode({ latLng: pos }, function(responses) {
                if (responses && responses.length > 0) {
                    that.updateInfoWindowContent(responses[0].formatted_address);
                } else {
                    that.updateInfoWindowContent('未能找到坐标相关信息');
                }
            });
        },
        /**
         * 更新信息窗口的内容
         * @param  {String} str 需要显示的内容
         * @return {[type]}     [description]
         */
        updateInfoWindowContent: function (str) {
            var that = this;
                content = that.infoContent.replace(/\{0\}/g, str || "");
            that.infowindow.setContent(content);
            that.infowindow.open(that.map, that.marker);
        },
        /**
         * 给marker绑定事件
         */
        addMarkerListeners: function () {
            var that = this,
                _marker = that.marker,
                _map = that.map;

            
            // 添加拖动事件监听器  
            google.maps.event.addListener(_marker, 'dragstart', function() {
                that.updateInfoWindowContent("正在搜索...");
            });
            /*
            google.maps.event.addListener(_marker, 'drag', function() {
                
            });*/

            google.maps.event.addListener(_marker, 'dragend', function() {
                that.latLng = _marker.getPosition();
                that.updateMarkerAddress();
            });
            // 打开信息窗口
            google.maps.event.addListener(_marker, 'click', function() {
                that.infowindow.open(_map, _marker);
            });
        },
        /**
         * 查找指定地址的坐标 
         * @param  {String} address 用户输入的搜索地址
         * @return {[type]}         [description]
         */
        findCoordinate: function (address) {
            var that = this;
            that.geocoder.geocode( { 'address': address }, function(results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    var latLngSearched = that.latLng = results[0].geometry.location;

                    that.map.setCenter(latLngSearched);
                    that.marker.setPosition(latLngSearched);
                    that.marker.setTitle(results[0].geometry.premise);

                    that.updateMarkerAddress();
                } else {
                    //updateMarkerStatus('Cannot find the location.');
                    that.showMessage("找不到您搜索的地址，请修改搜索关键字之后重试。");
                }
            });
        },
        /**
         * 显示提示信息
         * @param  {[type]} str [description]
         * @return {[type]}     [description]
         */
        showMessage: function (str) {
            alert(str);
            //$("#spanMessage").html(str);
        },
        /**
         * [dispose description]
         * @return {[type]} [description]
         */
        dispose: function() {
            // Refer from: http://api.jquery.com/empty/
            // To avoid memory leaks, 
            // jQuery removes other constructs such as data and event handlers from the child elements before removing the elements themselves.
            this.el.empty().removeData("coordinateselector");
        }
    };

    $.fn.coordinateselector = function (options, args) {
        var dataKey = 'coordinateselector';
        // 获取实例对象
        if (arguments.length === 0) {
            return this.first().data(dataKey);
        }

        return this.each(function () {
            var $self = $(this),
                instance = $self.data(dataKey);

            if (typeof options === 'string') {
                // 调用控件方法
                if (instance && typeof instance[options] === 'function') {
                    instance[options](args);
                }
            } 
            else {
                // 初始化控件
                // If instance already exists, destroy it:
                if (instance && instance.dispose) {
                    instance.dispose();
                }
                instance = new CoordinateSelector(this, options);
                $self.data(dataKey, instance);
            }
        });
    };
})(jQuery);