<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-cn" lang="zh-cn" xmlns:qz="http://qzone.qq.com/" style="overflow:hidden;">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="Content-Language" content="zh-cn" />
<title>个人中心feeds</title>
<base href="http://qzs.qq.com/"></base>
<script type="text/javascript">
QZFL = {};QZFL.event = {};QZFL.event.getEvent = function(evt) {var evt = window.event || evt,c,cnt;if(!evt && window.Event){c = arguments.callee;cnt = 0;while(c){if((evt = c.arguments[0]) && typeof(evt.srcElement) != "undefined"){break;}else if(cnt > 9){break;}c = c.caller;++cnt;}}return evt;};QZFL.event.getTarget = function(evt) {var e = QZFL.event.getEvent(evt);if (e) {return e.srcElement || e.target;} else {return null;}};QZFL.media = {};var QZFF_M_img_ribr=[];QZFL.media.reduceImgByRule=function(ew,eh,opts,cb){
QZFF_M_img_ribr.push(QZFL.event.getTarget());};QZFL.media.adjustImageSize=function(w,h,trueSrc,cb,errCallback){QZFF_M_img_ribr.push(QZFL.event.getTarget());};QZFL.media.reduceImage=function(){QZFF_M_img_ribr.push(QZFL.event.getTarget());};QZFL.media.smartImage=function(){QZFF_M_img_ribr.push(QZFL.event.getTarget());};function restXHTML(s){return s.replace(/&amp;|&lt;|&gt;|&apos;|&#0?39;|&quot;/g,function(a){switch (a){case "&amp;" :return "&";case "&lt;" :return "<";case "&gt;" :return ">";case "&apos;":case "&#39;":case "&#039;":return "\x27";case "&quot;":return "\x22";}});}
</script>
<script type="text/javascript">
	var _s_=new Date();
	var _re = document.referrer,
		exp = /^http:\/\/([\d\w-]+)(?:\.s\d\d?)?\.(qzone|pengyou)\.qq\.com/i,
		exp2 =  /^http:\/\/([a-zA-Z0-9\-]+)?\.?(qzs|imgcache)\.qq\.com[\/]*#?/i;

	var 
		G_Param,
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
			heightHack: parseInt(G_Param.heightHack,10),
			errmsg: "主人最近比较忙，没有公开的空间更新。",
			PV_ACT_DOMAIN: "ic2act",
			PV_DOMAIN: "ic2",
			showbbor:false,/*是否显示分割线条*/
			showcount:10,/*每次显示条数*/
			showmore:false,/*是否显示更多按钮*/
			version:5/*v5还是v6,默认是v5*/
		};
		
		var _feedsdata={
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
		}};

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
		if(typeof(G_Param.specialCSS)!="undefined"&&!G_sExp.test(G_Param.specialCSS)){
			G_Param.CSSQueue.push(([cssBegin,decodeURIComponent(G_Param.specialCSS),cssEnd]).join(""));
		}else{
			if(G_Param.version==6){
				G_Param.CSSQueue.push(([cssBegin,'http://', g_sid,'qzone_v6/gb/skin/',G_Param.style,'.css',cssEnd]).join(""));
			}else{
				G_Param.CSSQueue.push(([cssBegin,'http://', g_sid,'qzonestyle/qzone_client_v5/css/',G_Param.style,'/color_out.css',cssEnd]).join(""));
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
			cssBegin,"http://",g_sid,'qzone_v5/app/app_icon.css',cssEnd,
			cssBegin,"http://",g_sid,'qzone_v6/feed.css',cssEnd
		];
		coreCSSQueue = coreCSSQueue.concat(G_Param.CSSQueue);
		document.write(coreCSSQueue.join(''));
		
		
</script>
</head>
<body class="ifeeds_body">
<div id="GRZOPTParserContainer" style="display:none"></div>
<div class="ifeeds feed mode_bg_opacity0" id="ifeedsContainer">
	<div id="ifeedsMode" class="feed_avaIcon_4 feed_inner feeds_style_4"><ul id="_hostFeedsLayout"><?cs var:feeds.html ?></ul></div>
	<div class="check_more c_tx bor none" id="ICFeedsTipFetchDataBTN">
		<b style="font-size: 15px;">&#8659;</b><b>查看更多动态</b>
	</div>
	<div class="check_more bor" id="ICFeedsTipMsg" style="cursor:default">
		<p>数据加载中，请稍候...</p>
	</div>
	<div id="emptyBlock" style="height:100px;display:none"></div>
	<div id="goProfileFeedsLink" style="display:none;padding-top:10px"><a href="javascript:;" class="c_tx" onclick="QZONE.FP.toApp('/profile/feeds');return false;">更多动态信息</a></div>
	<div><img style="width:1px;height:1px;display:none" src="about:blank" onerror="window.FIRST_PAGE_FEEDS_TIME=new Date();" /></div>
	<div style="height:5px;"></div>
</div>

<script type="text/javascript" id="icFeedsDataLoader"></script>
<script type="text/javascript">
	/*look up qzone framework frame, begin*/
	var fw = window,
		qz = window.QZONE || {};
	
	qz.FP = qz.FP || {};
		
	do{
		fw = fw.parent;
	}while(fw != top);

	qz.FP.includeQZFL = function(s){
	var v = '',
		tsl;

	if(fw.constructQZFL){
		eval("(" + (fw.constructQZFL._string || (fw.constructQZFL._string = fw.constructQZFL.toString())) + ")();");
	}else{
		tsl = fw.document.getElementsByTagName("script");
		if(tsl){
			for(var i = 0, len = tsl.length; i < len; ++i){
				if(tsl[i] && tsl[i].src && (tsl[i].src.toLowerCase().indexOf('/qzfl/qzfl') > -1)){
					v = tsl[i].src;
					break;
				}
			}
			document.write('<script type="text/javascript" charset="utf-8" src=' + (v || ('http://' + (fw.siDomain || 'qzonestyle.gtimg.cn') + '/ac/qzfl/release/qzfl_for_qzone.js')) + '><\/script>');
		}
	}
	};
	qz.FP.includeQZFL();
</script>
<script type="text/javascript">
setTimeout(function(){
	var list = QZFF_M_img_ribr;
	for(var i=list.length;i--;){
		list[i].src = "/ac/b.gif?delay";
	}
},0);

(function(){
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
	_jsBegin,'http://'+g_sid+'ac/qzfl/appclientlib.js',_jsEnd,
	_jsBegin,'http://'+g_sid+'qzone/v5/namecardv2.js',_jsEnd
	]).join(""));
}())
</script>
<script type="text/javascript">

(function(){
	var ary = [];
	document.write(([
	_jsBegin+"http://"+g_sid+"c/="+'/qzone/v6/ic_personal/ic_personal_config.js'
	,'/qzone/v6/newic/ic_lazy_loader.js'
	,'/qzone/v6/newic/ic_core_v3.0.js'
	,"/qzone/v6/newic/ic_controller.js"
	,"/qzone/v6/newic/ic_feed_tip.js"
	,'/qzone/v6/ic_personal/ic_patch.js'
	,"/qzone/v6/newic/ic_feeds_monitor.js"
	,"/qzone/v6/newic/ic_trigger_v3.0.js"
	,"/qzone/v6/newic/ic_feeds_protocol.js?"+QZONE.FP._t.g_V.ic+_jsEnd
	]).join(","));
}()) 
</script>
</body>
</html>
