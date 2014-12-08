/**
 * [formatDate description]
 * @param  {[type]} date [description]
 * @return {[type]}      [description]
 */
function formatDate(date) {
	date = date || new Date();
    var y = date.getFullYear(),
    	m = date.getMonth() + 1,
    	d = date.getDate();

    return y + "-" + ("0" + m).slice(-2) + "-" + ("0" + d).slice(-2);
}
/**
 * [isValidDate description]
 * @param  {[type]}  dateStr [description]
 * @return {Boolean}         [description]
 */
function isValidDate(dateStr) {
	return (new Date(dateStr).toString() != "Invalid Date");
}
/**
 * [checkDateRange description]
 * @param  {[type]} date [description]
 * @return {[type]}      [description]
 */
function checkDateRange(date) {
	var _minDate = new Date(2010, 1, 1);
	var _maxDate = new Date(2030, 12, 31);

	return date <= _maxDate && date >= _minDate;
}
/**
 * [calc_res description]
 * @return {[type]} [description]
 */
function calc_res() {
	var dateStr = $("#date-input").val();

	// 验证日期格式
	if (!dateStr || !isValidDate(dateStr)) { return; }
	// 验证日期范围
	var date = new Date(dateStr);
	if (!checkDateRange(date)) { return; }
    
    var d = date.getDate(),
        m = date.getMonth() + 1,
        y = date.getFullYear(),
        z = parseInt(localStorage.TimeZone),
        lo = parseFloat(localStorage.Lng),
        la = parseFloat(localStorage.Lat);

    var obj = Cal(mjd(d,m,y,0.0), z, lo, la);
    var ret = "";
    if(obj["rise"] == undefined){
        ret = "太阳不升";
    }
    else{
        ret = "日出时间:<span><strong>" + obj["rise"] + "</strong> (北京时间)</span>";
        if(obj["set"] == undefined){
            ret += "太阳不落";
        } else {
            ret += "日落时间:<span class='nr'><strong>" + obj["set"] + "</strong> (北京时间)</span>";
        }
    }
    
    $(".sunrise-result").html(ret);
};
/**
 * [app description]
 * @type {Object}
 */
var app = {
	/**
	 * [initialize description]
	 * @return {[type]} [description]
	 */
	initialize: function() {
		var that = this;
		// 显示地点信息
	    that.showLocation();
	    // 初始化地址选择控件
	    $("#citybox .citybox-bd").locationsetter({
	        serviceUrl: config.serviceUrl + '/services/locations',
	        paramName: "dn",
	        ajaxSettings: { dataType: "jsonp" },
	        idField: "DistrictId",
	        onSelect: function (suggestion) {
	            af.ui.toggleSideMenu();
	            //alert('You selected: ' + suggestion.Name + ', ' + suggestion.DistrictId);
	            that.saveLocation(suggestion);

	            that.showLocation();
	        }
	    });
	    // 初始化日期日期选择控件
	    $('#date-input').val(formatDate(new Date())).on("change", function(e){
	    	calc_res();
	    }).datepicker({ 
	    	monthNames: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月" ], 
	    	shortDayNames: ["日", "一", "二", "三", "四", "五", "六"]
	    });
	    // 计算日出日落时间
	    calc_res();
	    /*
	    var hash = window.location.hash;
	    if (hash == "#dldy") {
	    	//that.showArticle($("ul.navbtn li:eq(0) a")[0]);
	    	$("ul.navbtn li:eq(0) a").trigger("click");
	    };*/
	},
	/**
	 * [checkLocation description]
	 * @return {[type]} [description]
	 */
	checkLocation: function() {
		if (!localStorage.Id) {
            this.changeLocation();
            return false;
        }
        return true;
	},
	/**
	 * [showLocation description]
	 * @return {[type]} [description]
	 */
	showLocation: function() {
		var that = this;

		if (!that.checkLocation()) { return; };

		var locName = localStorage.Name;
        var locLng = localStorage.Lng;
        var locLat = localStorage.Lat;

        $("#header .infotitle a").text(locName);
        $("#citybox .citybox-hd span").text(locName);
        $("#header p.infocont").text(that.translateLat(locLat) + "," + that.translateLng(locLng));
	},
	/**
	 * [changeLocation description]
	 * @return {[type]} [description]
	 */
	changeLocation: function() {
		af.ui.toggleSideMenu();
	},
	/**
	 * [saveLocation description]
	 * @param  {[type]} district [description]
	 * @return {[type]}          [description]
	 */
	saveLocation: function(district) {
		localStorage.Id = district.DistrictId;
        localStorage.Name = district.Name;
        localStorage.Lng = district.Lng;
        localStorage.Lat = district.Lat;
        localStorage.TimeZone = district.TimeZone || 8;

        this.showLocation();
	},
	/**
	 * [showArticle description]
	 * @param  {[type]} anchor [description]
	 * @return {[type]}        [description]
	 */
	showArticle: function(anchor) {
		if (!this.checkLocation()) return;

        var api = config.serviceUrl + "/services/articles?"
        var params = {
            type: $(anchor).data("type"),
            did: localStorage.Id
        }

        api += $.param(params);

        $.ui.loadAjax(api, false, false, "slide", anchor);
	},
	/**
	 * [showArticle2 description]
	 * @param  {[type]} panel [description]
	 * @return {[type]}       [description]
	 */
	showArticle2: function(panel) {
		$.ui.showMask("正在加载...");
		//debugger;
		var el = $(panel);

		var ajaxSettings = {
			url: config.serviceUrl + "/services/articles",
			dataType: "html",
			data: {
				type: el.prop("id").toUpperCase(),
				did: localStorage.Id
			}
		}

		$.ajax(ajaxSettings).fail(function(jqXHR, textStatus, errorThrown) {
			$("#afui").popup("应用出现了点问题，请稍后再试。");
		}).always(function() {
			$.ui.hideMask();
		}).done(function(data) {
			$.ui.updatePanel(el.prop("id"), data);
			console.log("article load.")
		});
	},
	/**
	 * [translateLng description]
	 * @param  {[type]} lng [description]
	 * @return {[type]}     [description]
	 */
	translateLng: function(lng) {
		if (!lng) { return "未知经度" };
        if (lng.substr(0,1) == "-") { 
            return "西经" + lng.substr(1);
        }
        else {
            return "东经" + lng;
        }
	},
	/**
	 * [translateLat description]
	 * @param  {[type]} lat [description]
	 * @return {[type]}     [description]
	 */
	translateLat: function(lat) {
		if (!lat) { return "未知纬度" };
        if (lat.substr(0,1) == "-") { 
            return "南纬" + lat.substr(1);
        }
        else {
            return "北纬" + lat;
        }
	}
}
