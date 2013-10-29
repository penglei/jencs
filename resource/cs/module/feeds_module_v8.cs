<!DOCTYPE HTML>
<html lang="zh-cn">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>个人中心feeds</title>
<base href="http://qzs.qq.com/"></base>
<script type="text/javascript">
QZONE = {};
QZFL = {};
QZFL.event = {};
QZFL.event.getEvent = function (evt) {
	var evt = window.event || evt,
		c, cnt;
	if (!evt && window.Event) {
		c = arguments.callee;
		cnt = 0;
		while (c) {
			if ((evt = c.arguments[0]) && typeof (evt.srcElement) != "undefined") {
				break;
			} else if (cnt > 9) {
				break;
			}
			c = c.caller;
			++cnt;
		}
	}
	return evt;
};
QZFL.event.getTarget = function (evt) {
	var e = QZFL.event.getEvent(evt);
	if (e) {
		return e.srcElement || e.target;
	} else {
		return null;
	}
};
QZFL.media = {};
var QZFF_M_img_ribr = [];
QZFL.media.reduceImgByRule = function (ew, eh, opts, cb) {
	QZFF_M_img_ribr.push(QZFL.event.getTarget());
};
QZFL.media.adjustImageSize = function (w, h, trueSrc, cb, errCallback) {
	QZFF_M_img_ribr.push(QZFL.event.getTarget());
};
QZFL.media.reduceImage = function () {
	QZFF_M_img_ribr.push(QZFL.event.getTarget());
};
QZFL.media.smartImage = function () {
	QZFF_M_img_ribr.push(QZFL.event.getTarget());
};

function restXHTML(s) {
	return s.replace(/&amp;|&lt;|&gt;|&apos;|&#0?39;|&quot;/g, function (a) {
		switch (a) {
		case "&amp;":
			return "&";
		case "&lt;":
			return "<";
		case "&gt;":
			return ">";
		case "&apos;":
		case "&#39;":
		case "&#039;":
			return "\x27";
		case "&quot;":
			return "\x22";
		}
	});
}
</script>
<script type="text/javascript">
	var _s_=new Date();
	var _re = document.referrer,
		exp = /^http:\/\/([\d\w-]+)(?:\.s\d\d?)?\.(qzone|pengyou)\.qq\.com/i,
		exp2 =  /^http:\/\/([a-zA-Z0-9\-]+)?\.?(qzs|imgcache)\.qq\.com[\/]*#?/i;

	var G_Param,
		g_sid,
		g_imgd,
		g_wd,
		g_rd,
		g_V;
	if(exp.test(_re)||exp2.test(_re)){
		document.domain="qq.com";
		g_sid = parent.siDomain || parent.parent.siDomain || "qzonestyle.gtimg.cn";
		g_sid = g_sid.replace("http://","");
		g_imgd = parent.imgcacheDomain || parent.parent.siDomain || "imgcache.qq.com";

		g_DPrefix = parent.g_DPrefix || parent.parent.g_DPrefix || "";
		g_rd = "r."+g_DPrefix+"qzone.qq.com";
		g_wd = "w."+g_DPrefix+"qzone.qq.com";
		g_imgd = g_imgd.replace("http://","");
		g_V = parent.g_V || parent.parent.g_V || {
			"qz":"_2.0.7.8"
		};
	}else{
		window.OUTSIDE_DOMAIN = true;
		g_sid = "qzonestyle.gtimg.cn";
		g_imgd = "imgcache.qq.com";
		g_rd = "r.qzone.qq.com";
		g_wd = "w.qzone.qq.com";
		g_V = {
			"qz":"_2.0.7.8"
		};
	}
	if(!/\/$/.test(g_sid)){
		g_sid+="/";
	}
	function commonDictionarySplit(s, esp, vq, eq) {
			var res = {};
			if(!s || typeof(s) != "string"){
				return res;
			}
			if (typeof(esp) != 'string') {
				esp = "&";
			}
			if (typeof(vq) != 'string') {
				vq = "";
			}
			if (typeof(eq) != 'string') {
				eq = "=";
			}

			var l = s.split(vq + esp),
				len = l.length,
				tmp,
				t = eq + vq,
				p;

			if(vq){
				tmp = l[len - 1].split(vq);
				l[len - 1] = tmp.slice(0, tmp.length - 1).join(vq);
			}

			for (var i = 0, len; i < len; i++) {
				if(eq){
					tmp = l[i].split(t);
					if (tmp.length > 1) {
						res[tmp[0]] = tmp.slice(1).join(t);
						continue;
					}
				}
				res[l[i]] = true;
			}
			return res;
		}

		var _jsBegin='<script type="text/javascript" charset="utf-8" src="',
			_jsEnd='"><\/script>',
			cssBegin = '<link rel="stylesheet" rev="stylesheet" href="',
			cssEnd = '" type="text/css" media="screen"/>';

		G_Param = commonDictionarySplit(location.search.substring(1),"&");
		if(!G_Param){
			G_Param = frameElement.G_Param || {};
		}
		var G_Param_map = {
			hideExtend: true,
			style: 100,
			DEFAULT_FEEDS_CGI: "http://rsh.qzone.qq.com/cgi-bin/feeds/feeds_html_act_all",
			i_uin: window.OUTSIDE_DOMAIN?0:(parent.g_iUin||parent.parent.g_iUin||0),
			i_login_uin: window.OUTSIDE_DOMAIN?0:(parent.g_iLoginUin||parent.parent.g_iLoginUin||0),
			mode: 3,
			noOpr: true,
			view: 1,
			scope: 0,
			filter: "all",
			max: -1,
			heightHack: parseInt(G_Param.heightHack, 10),
			errmsg: "主人最近比较忙，没有公开的空间更新。",
			PV_ACT_DOMAIN: "ic2act",
			PV_DOMAIN: "ic2",
			showbbor:false,/*是否显示分割线条*/
			showcount:10,/*每次显示条数*/
			showmore:false,/*是否显示更多按钮*/
			version:5/*v5还是v6,默认是v5*/
		};

var _feedsdata = {
	"code" : '<?cs  var:feeds.code ?>',
	"subcode" : '<?cs  var:feeds.subcode ?>',
	"message" : '<?cs  var:feeds.message ?>',
	"default" : '<?cs  var:feeds.default ?>',
	"data":{
		main : {
			needFold:'<?cs  var:feeds.needFold ?>',
			icServerTime:'<?cs  var:feeds.icServerTime ?>',
			icView:'<?cs  var:feeds.icView ?>',
			daylist:'<?cs  var:feeds.g_daylist ?>',
			uinlist:'<?cs  var:feeds.g_uinlist ?>',
			hasMoreFeeds_0:<?cs  var:feeds.hasMoreFeeds_0 ?>,
			foldAllHostBTNClass:'<?cs  var:feeds.foldAllHostBTNClass ?>',
			foldAllHostBTNTitle:'<?cs  var:feeds.foldAllHostBTNTitle ?>',
			foldAllHostFeedDisplay:'<?cs  var:feeds.foldAllHostFeedDisplay ?>',
			friend_more:'<?cs  var:feeds.friend_more ?>',
			host_more:'<?cs  var:feeds.host_more ?>',
			aboutHostFeeds_new_cnt:'<?cs  var:feeds.aboutHostFeeds_new_cnt ?>',
			replyHostFeeds_new_cnt:'<?cs  var:feeds.replyHostFeeds_new_cnt ?>',
			myFeeds_new_cnt:'<?cs  var:feeds.myFeeds_new_cnt ?>',
			friendFeeds_new_cnt:'<?cs  var:feeds.friendFeeds_new_cnt ?>',
			newfeeds_uinlist:'<?cs  var:feeds.newfeeds_uinlist ?>',
			newfeeds_uinlist_more:'<?cs  var:feeds.newfeeds_uinlist_more ?>',
			newfeeds_special_cnt:'<?cs  var:feeds.newfeeds_special_cnt ?>',
			newfeeds_friend_cnt:'<?cs  var:feeds.newfeeds_friend_cnt ?>',
			newfeeds_follow_cnt:'<?cs  var:feeds.newfeeds_follow_cnt ?>',
			newfeeds_group_cnt:'<?cs  var:feeds.newfeeds_group_cnt ?>',
			tips:'<?cs  var:feeds.tips ?>',
			feedstips:'<?cs  var:feeds.feedstips ?>',
			js_showtips:'<?cs  var:feeds.js_showtips ?>',
			lastaccesstime:'<?cs  var:feeds.lastaccesstime ?>',
			lastAccessRelateTime:'<?cs  var:feeds.lastAccessRelateTime ?>',
			QzoneFeedsKey:'<?cs  var:feeds.QzoneFeedsKey ?>',
			offset:'<?cs  var:feeds.offsetNextTime ?>',
			total_number:'<?cs  var:feeds.total ?>'
		},
		firstpage_data:[
			<?cs each:item = feeds.firstpage_data ?>
			{
				ver:'<?cs  var:item.ver ?>',
				appid:'<?cs  var:item.appid ?>',
				typeid:'<?cs  var:item.typeid ?>',
				key:'<?cs  var:item.key ?>',
				flag:'<?cs  var:item.flag ?>',
				dataonly:'0',
				feedno:'<?cs  var:item.feedno ?>',
				title:'<?cs  var:json_encode(item.title, 1) ?>',
				summary:'<?cs  var:json_encode(item.summary, 1) ?>',
				appiconid:'<?cs  var:item.appiconid ?>',
				clscFold:'<?cs  var:item.clscFold ?>',
				abstime:'<?cs  var:item.abstime ?>',
				feedstime:'<?cs  var:item.feedstime ?>',
				userHome:'<?cs  var:item.userHome ?>',
				namecardLink:'<?cs  var:item.namecardLink ?>',
				ouin:'<?cs  var:item.ouin ?>',
				uin:'<?cs  var:item.uin ?>',
				foldFeed:'<?cs  var:item.foldFeed ?>',
				foldFeedTitle:'<?cs  var:item.foldFeedTitle ?>',
				showEbtn:'<?cs  var:item.showEbtn ?>',
				scope:'0',
				hideExtend:'<?cs  var:item.hideExtend ?>',
				nickname:'<?cs  var:json_encode(item.nickname, 1) ?>',
				remark:'<?cs  var:json_encode(item.remark, 1) ?>',
				type:'<?cs  var:item.type ?>',
				vip:'<?cs  var:item.vip ?>',
				info_user_name:'<?cs  var:item.info_user_name ?>',
				logimg:'<?cs  var:item.logimg ?>',
				bor:'<?cs  var:item.bor ?>',
				lastFeedBor:'<?cs  var:item.lastFeedBor ?>',
				list_bor2:'<?cs  var:item.list_bor2 ?>',
				info_user_display:'<?cs  var:item.info_user_display ?>',
				upernum:'<?cs  var:item.upernum ?>',
				oprType:'<?cs  var:item.oprType ?>',
				moreflag:'<?cs  var:item.moreflag ?>',
				otherflag:'<?cs  var:item.otherflag ?>',
				sameuser:{<?cs  var:item.sameuser ?>},
				uper_isfriend:[<?cs var:item.uper_isfriend ?>],
				uperlist:[<?cs  var:item.uperlist ?>]
		},
		<?cs /each ?>
		undefined
		]
	}
};

for(var k in G_Param_map){
	if(typeof(G_Param[k])=="undefined"){
		G_Param[k] = G_Param_map[k];
	}
}
if ((G_Param.style + "").indexOf("v6/") == 0){
	G_Param.version = 6;
	G_Param.style = (G_Param.style + "").replace("v6/", "");
}
if(G_Param.MORE_FEEDS_CGI){
	G_Param.MORE_FEEDS_CGI = decodeURIComponent(G_Param.MORE_FEEDS_CGI);
}

/*这里特殊默认值的判断*/
if(typeof(G_Param.hideExtend)=="string"){
	G_Param.hideExtend = (G_Param.hideExtend != "false");
}

if(typeof(G_Param.mode)=="undefined"||isNaN(parseInt(G_Param.mode,10))){
	G_Param.mode = 1;
}

if(typeof(G_Param.style)=="undefined"||isNaN(parseInt(G_Param.style,10))){
	G_Param.style = 100;
}else{
	G_Param.style = parseInt(G_Param.style,10);
}

G_Param.CSSQueue = [];
var G_sExp = /[\<|\>|!|-|\(|\)]/ig;
if (typeof(G_Param.specialCSS) != "undefined" && !G_sExp.test(G_Param.specialCSS)) {
	G_Param.CSSQueue.push(([cssBegin,decodeURIComponent(G_Param.specialCSS),cssEnd]).join(""));
} else {
	if(G_Param.version == 8){
		G_Param.CSSQueue.push([
			cssBegin,'http://', g_sid,'aoi/skin/',G_Param.style, '.css" id="qz_skin_style', cssEnd
		].join(""));
	} else if (G_Param.version == 6){
		G_Param.CSSQueue.push(([cssBegin,'http://', g_sid,'qzone_v6/gb/skin/',G_Param.style,'.css',cssEnd]).join(""));
	}
}

if(typeof(G_Param.needDelOpr)=="string"){
	G_Param.needDelOpr = (G_Param.needDelOpr != "false");
}
var tmp_begin = '<li class="f_single imgBlock bor2"><div class="f_aside imgBlock_img"><qz:user param=\'<%userHome%>|<%logimg%>|<%namecardLink%>|<%info_user_name%>||<%vip%>|<%type%>|<%uin%>|<%ouin%>|<%otherflag%>\' nick=\'<%nickname%>\'></qz:user></div><div class="f_wrap imgBlock_ct">',
	tmp_end = '</div></li>',
	host_tmp = '<div class="f_item" link="0" id="feed_<%uin%>_<%appid%>_<%typeid%>_<%abstime%>_<%feedno%>_<%scope%>_<%ver%>" ><div class="f_info"><a target="_blank" href="http://user.qzone.qq.com/<%uin%>/profile" class="nickname q_namecard c_tx q_des ui_mr5" link="nameCard_<%uin%> des_<%uin%>"><%nickname%></a><%famouslogo%><%title%></div><div class="qz_summary" id="hex_<%appid%>_<%abstime%>_<%uin%>_<%scope%>"><%summary%></div><div class="f_op" id="opr_<%uin%>_<%appid%>_<%typeid%>_<%abstime%>_<%feedno%>"><qz:fo class="<%foldFeed%>" title="<%foldFeedTitle%>" param="<%appid%>|<%abstime%>|<%ouin%>|<%typeid%>|<%feedno%>|<%feedstime%>|<%showEbtn%>|<%scope%>|<%oprType%>|<%key%>|<%otherflag%>|<%rightflag%>|<%ver%>"></qz:fo></div></div>',
	friend_tmp = '<div class="f_item" link="1" id="feed_<%uin%>_<%appid%>_<%typeid%>_<%abstime%>_<%feedno%>_<%scope%>_<%ver%>" ><div class="f_info"><a target="_blank" href="http://user.qzone.qq.com/<%uin%>/profile" class="nickname q_namecard c_tx q_des ui_mr5" link="nameCard_<%uin%> des_<%uin%>"><%nickname%></a><%famouslogo%><%title%></div><div class="qz_summary" id="hex_<%appid%>_<%abstime%>_<%uin%>_<%scope%>"><%summary%></div><div class="f_op"  '+(G_Param.hideExtend?'style="display:none"':'')+' id="opr_<%uin%>_<%appid%>_<%typeid%>_<%abstime%>_<%feedno%>"><qz:fo class="<%foldFeed%>" title="<%foldFeedTitle%>" param="<%appid%>|<%abstime%>|<%uin%>|<%typeid%>|<%feedno%>|<%feedstime%>|<%showEbtn%>|<%scope%>|<%oprType%>|<%key%>|<%otherflag%>|<%rightflag%>|<%ver%>"></qz:fo></div></div>';


var coreCSSQueue = [
	cssBegin,"http://",g_sid,'qzone_v5/app/app_icon.css',cssEnd,/*TODO 这个样式是不是需要去掉?*/
	cssBegin,"http://",g_sid,'aoi/feed-home.css',cssEnd,
	cssBegin,"http://",g_sid,'aoi/old-feed.css',cssEnd
];
coreCSSQueue = coreCSSQueue.concat(G_Param.CSSQueue);
document.write(coreCSSQueue.join(''));
</script>
</head>
<?cs if:paramstring?>
	<?cs set:devide = string.find(paramstring, "|")?>
	<?cs set:platform_class = string.slice(paramstring, 0, devide)?>
	<?cs set:opacity = string.slice(paramstring, devide + 1, string.length(paramstring))?>
	<?cs if:opacity < 30?>
		<?cs set:opacity = 30?>
	<?cs /if?>
<?cs /if?>
<?cs #var:user_platform_class ?>
<?cs #mode_bg_opacity?>
<body class="ifeeds_body <?cs if:paramstring?><?cs var:html_encode(platform_class, 1)?> mode_bg_opacity<?cs var:html_encode(opacity, 1)?><?cs /if?>" data-opacity="<?cs var:html_encode(opacity, 1)?>">
<div class="ifeeds feed feed-home" id="ifeedsContainer">

	<div id="ifeedsMode" class="feed_avaIcon_4 feed-inner feeds_style_4">
		<ul id="host_home_feeds"><?cs var:feeds.html ?></ul>
	</div>

	<div class="check-more update-more bor" id="ICFeedsTipMsg" style="cursor:pointer">
		<p class="b-inline data_is_loading none">数据加载中，请稍候...</p>
		<?cs if:(feeds.hasMoreFeeds_0 == 'false' || !feeds.hasMoreFeeds_0)?>
			<p class="b-inline data_no_more">没有更多动态</p>
		<?cs else ?>
			<p class="b-inline data_no_more none">没有更多动态</p>
			<p class="b-inline data_btn_more"><b style="font-size: 15px;">&#8659;</b><b>查看更多动态</b></p>
		<?cs /if?>
	</div>

	<div id="goProfileFeedsLink" style="display:none;padding-top:10px">
		<a href="javascript:;" class="c_tx" onclick="QZONE.FP.toApp('/profile/feeds');return false;">更多动态信息</a>
	</div>

</div>

<div id="GRZOPTParserContainer" style="display:none"></div>

<script type="text/javascript">
	var ver = top.g_V || {};
	ver.qz = ver.qz || "_2.1.31";
	ver.sea = ver.sea || "2.0.0";
	ver.jq = ver.jq || "2.0.0";
	document.write([
		_jsBegin,'http://'+ g_sid + 'ac/qzone/qzfl/qzfl', ver.qz, ".js", _jsEnd,
		_jsBegin,'http://'+ g_sid + 'ac/lib/seajs/sea-', ver.sea, ".js", _jsEnd,
		_jsBegin,'http://'+ g_sid + 'qzone/v8/core/seajs_config.js', _jsEnd
	].join(""));
</script>
<script type="text/javascript">
setTimeout(function(){
	var list = QZFF_M_img_ribr;
	for(var i=list.length;i--;){
		list[i].src = "/ac/b.gif?delay";
	}
}, 0);

if (G_Param.DEFAULT_FEEDS_CGI.indexOf("feeds_ic_unaided_xml") > -1) {
	G_Param.DEFAULT_FEEDS_CGI = "http://" + g_rd + "/cgi-bin/feeds/feeds_html_act_all";
}
if (G_Param.DEFAULT_FEEDS_CGI.indexOf("feeds_ic_interactive_xml") > -1) {
	G_Param.DEFAULT_FEEDS_CGI = "http://" + g_rd + "/cgi-bin/feeds/feeds_html_interactive";
}
if(G_Param.hasGoProfileFeedsLink){
	var d = document.getElementById("goProfileFeedsLink");
	if(d){
		d.style.display = "";
	}
}
document.write(([
	_jsBegin,'http://'+g_sid+'qzone/v5/namecardv2.js',_jsEnd
]).join(""));

function waitClientLib(){
	if (top.QZONE && top.QZONE.FrontPage){
		seajs.use('http://'+g_sid+'ac/qzfl/appclientlib.js', onClientLibOK);
	} else {
		setTimeout(waitClientLib, 100);
	}
}
waitClientLib();

function onClientLibOK(){
	seajs.use(['v8/engine/cpu', 'v8/ic/feeds', 'v8/ic/home_more_feeds'], function(cpujs, Feeds, HomeMoreFeeds){
		cpujs.bootstrap();
		this.cpujs = cpujs;/*暴露全局接口*/
		/*初始化feeds*/
		/*interface clientlib映射interface完成*/
		Feeds.bootstrap({
			sence:"home_personal",
			feedsData:_feedsdata.data
		});
		/*启动拉取更多feeds的逻辑*/
		HomeMoreFeeds.bootstrap();


		var $body = $j("body");
		function onOpacityChange(_, newOpacity){
			var newOpacityClassName = "mode_bg_opacity" + newOpacity;
			$body.removeClass("mode_bg_opacity" + $body.data("opacity")).addClass(newOpacityClassName).data("opacity", newOpacity);
		}

		function onStyleChange(evt, dataObj){
			if (dataObj && dataObj.styleid){
				var styleCssHref = 'http://' + g_sid + 'aoi/skin/' + dataObj.styleid + '.css';
				$j("#qz_skin_style").attr("href", styleCssHref);
			}
		}

		top.QZONE.qzEvent.addEventListener('QZ_CUSTOM_OPACITY_CHANGE', onOpacityChange);
		top.QZONE.qzEvent.addEventListener('QZ_CUSTOM_CHANGE_STYLE', onStyleChange);
		$j(window).bind("beforeunload", function(){
			top.QZONE.qzEvent.removeEventListener('QZ_CUSTOM_CHANGE_OPACITY', onOpacityChange);
			top.QZONE.qzEvent.removeEventListener('QZ_CUSTOM_CHANGE_STYLE', onStyleChange);
		});

		QZONE.namecard.init("host_home_feeds");
	});

}

</script>
</body>
</html>
