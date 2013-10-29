<?cs set:UC_PLATFORM_ID_QZONE = 50?>				<?cs #/*空间*/?>
<?cs set:UC_PLATFORM_ID_PENGYOU = 51?>				<?cs #/*朋友*/?>
<?cs set:UC_PLATFORM_ID_MOBILE = 52?>				<?cs #/*手机*/?>
<?cs set:UC_PLATFORM_ID_WEIBO = 53?>				<?cs #/*微博*/?>
<?cs set:UC_PLATFORM_ID_THIRDAPP = 54?>				<?cs #/*第三方应用*/?>
<?cs set:UC_PLATFORM_ID_PHONEMANAGE = 55?>			<?cs #/*手机管家PC版*/?>
<?cs set:UC_PLATFORM_ID_OTHER = 59 ?>				<?cs #/*业务自定义，通过私有协议传*/?>

<?cs #/* 从10000开始为第三方应用*/?>

<?cs #/*空间子id定义*/?>
<?cs set:UC_PLATFORM_QZONE_SUBID_WEB = 0?>			<?cs #/*普通，来自qq空间*/?>
<?cs set:UC_PLATFORM_QZONE_SUBID_QQSIGN = 1?>		<?cs #/*QQ签名*/?>
<?cs set:UC_PLATFORM_QZONE_SUBID_TIMELINE = 2?>		<?cs #/*时光轴*/?>
<?cs set:UC_PLATFORM_QZONE_TAOTAO = 3?>				<?cs #/*taotao主站*/?>
<?cs set:UC_PLATFORM_QZONE_QZROBOT = 4?>			<?cs #/*心情机器人*/?>
<?cs set:UC_PLATFORM_QZONE_QQFOOD = 5?>				<?cs #/*QQ美食*/?>
<?cs set:UC_PLATFORM_QZONE_QQSHOWSTORE = 6?>		<?cs #/*QQ秀商城*/?>
<?cs set:UC_PLATFORM_QZONE_QQBROWSER = 7?>			<?cs #/*QQ浏览器*/?>
<?cs set:UC_PLATFORM_QZONE_QQ = 8?>					<?cs #/*通过QQ*/?>
<?cs set:UC_PLATFORM_QZONE_PAGE = 9?>				<?cs #/*通过网页*/?>
<?cs set:UC_PLATFORM_QZONE_CLOCK = 10?>				<?cs #/*通过QQ闹钟*/?>
<?cs set:UC_PLATFORM_QZONE_TOUCH = 11?>				<?cs #/*通过QQ空间触屏版*/?>

<?cs #/*朋友网子id定义*/?>
<?cs set:UC_PLATFORM_PY_SUBID_WEB = 0?>				<?cs #/*普通，来自朋友网*/?>
<?cs set:UC_PLATFORM_PY_SUBID_PENGYOUBUY = 1?>		<?cs #/*朋友二手交易*/?>
<?cs set:UC_PLATFORM_PY_SUBID_MOBILE = 2?>			<?cs #/*朋友客户端*/?>
<?cs set:UC_PLATFORM_PY_SUBID_MOBILE_IPHONE = 3?>	<?cs #/* iphone版朋友客户端，以后用新版吧UC_PLATFORM_MOBILE_SUBID_IPHONE_3*/?>
<?cs set:UC_PLATFORM_PY_SUBID_MOBILE_IPAD = 4?>		<?cs #/* Ipad版朋友客户端*/?>
<?cs set:UC_PLATFORM_PY_SUBID_MOBILE_ANDROID = 5?>	<?cs #/* Andriod版朋友客户端*/?>
<?cs set:UC_PLATFORM_PY_SUBID_MOBILE_SYBIAM = 6?>	<?cs #/* sybiam版朋友客户端*/?>
<?cs set:UC_PLATFORM_PY_SUBID_MOBILE_WM = 7?>		<?cs #/* wm版朋友客户端*/?>
<?cs set:UC_PLATFORM_PY_SUBID_MOBILE_WP7 = 8?>		<?cs #/* wm7版朋友客户端*/?>

<?cs #/*手机版子id定义*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_WEB = 0?>			<?cs #/*手机qq空间*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_CLIENT = 1?>		<?cs #/*软件版*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_IPHONE = 2?>		<?cs #/*iphone客户端*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_IPAD = 3?>		<?cs #/*Ipad客户端*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_ANDROID = 4?>		<?cs #/*andriod客户端*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_SYBIAM = 5?>		<?cs #/*sybiam客户端*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_WM = 6?>			<?cs #/*WM客户端*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_WP7 = 7?>			<?cs #/*WM7客户端*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_MQZONE = 8?>		<?cs #/*手机qzone*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_SMSG = 9?>		<?cs #/*短信*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_MMSG = 10?>		<?cs #/*彩信*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_QPAI = 11?>		<?cs #/*Q拍*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_IPHONE_3 = 12?>	<?cs #/*iPhone 3.0新版本*/?>

<?cs #/*微博子id定义*/?>
<?cs set:UC_PLATFORM_WB_SUBID_WEB = 0?>				<?cs #/*普通，来自腾讯微博*/?>
<?cs set:UC_PLATFORM_WB_SUBID_MOBILE = 1?>			<?cs #/*来自腾讯微博客户端*/?>
<?cs set:UC_PLATFORM_WB_SUBID_MOBILE_IPHONE = 2?>	<?cs #/*同上*/?>
<?cs set:UC_PLATFORM_WB_SUBID_MOBILE_IPA = 3?>
<?cs set:UC_PLATFORM_WB_SUBID_MOBILE_ANDROI = 4?>
<?cs set:UC_PLATFORM_WB_SUBID_MOBILE_SYBIA = 5?>
<?cs set:UC_PLATFORM_WB_SUBID_MOBILE_W = 6?>
<?cs set:UC_PLATFORM_WB_SUBID_MOBILE_WP7 = 7?>

<?cs def:getSourceid(platformid, subplatformid)?>
	<?cs set:_sourceid = 0?>
	<?cs if:UC_PLATFORM_ID_QZONE == platformid?>
		<?cs if:subplatformid == UC_PLATFORM_QZONE_SUBID_WEB?>
			<?cs set:_sourceid = 1?>
		<?cs elif:subplatformid == UC_PLATFORM_QZONE_SUBID_QQSIGN?>
			<?cs set:_sourceid = 3?>
		<?cs elif:subplatformid == UC_PLATFORM_QZONE_SUBID_TIMELINE?>
			<?cs set:_sourceid = 9?>
		<?cs elif:subplatformid == UC_PLATFORM_QZONE_TAOTAO?>
			<?cs set:_sourceid = 10?>
		<?cs elif:subplatformid == UC_PLATFORM_QZONE_QZROBOT?>
			<?cs set:_sourceid = 11?>
		<?cs elif:subplatformid == UC_PLATFORM_QZONE_QQFOOD?>
			<?cs set:_sourceid = 12?>
		<?cs elif:subplatformid == UC_PLATFORM_QZONE_QQSHOWSTORE?>
			<?cs set:_sourceid = 13?>
		<?cs elif:subplatformid == UC_PLATFORM_QZONE_QQBROWSER?>
			<?cs set:_sourceid = 14?>
		<?cs elif:subplatformid == UC_PLATFORM_QZONE_QQ?>
			<?cs set:_sourceid = 5?>
		<?cs elif:subplatformid == UC_PLATFORM_QZONE_PAGE?>
			<?cs set:_sourceid = 7?>
		<?cs elif:subplatformid == UC_PLATFORM_QZONE_CLOCK?>
			<?cs set:_sourceid = 44?>
		<?cs elif:subplatformid == UC_PLATFORM_QZONE_TOUCH?>
			<?cs set:_sourceid = 45?>
		<?cs else ?>
			<?cs set:_sourceid = 1?>
		<?cs /if?>
	<?cs elif:UC_PLATFORM_ID_PENGYOU == platformid?>
		<?cs if:subplatformid == UC_PLATFORM_PY_SUBID_WEB?>
			<?cs set:_sourceid = 8?>
		<?cs elif:subplatformid == UC_PLATFORM_PY_SUBID_PENGYOUBUY?>
			<?cs set:_sourceid = 15?>
		<?cs elif:subplatformid == UC_PLATFORM_PY_SUBID_MOBILE?>
			<?cs set:_sourceid = 16?>
		<?cs elif:subplatformid == UC_PLATFORM_PY_SUBID_MOBILE_IPHONE?>
			<?cs set:_sourceid = 17?>
		<?cs elif:subplatformid == UC_PLATFORM_PY_SUBID_MOBILE_IPAD?>
			<?cs set:_sourceid = 18?>
		<?cs elif:subplatformid == UC_PLATFORM_PY_SUBID_MOBILE_ANDROID?>
			<?cs set:_sourceid = 19?>
		<?cs elif:subplatformid == UC_PLATFORM_PY_SUBID_MOBILE_SYBIAM?>
			<?cs set:_sourceid = 20?>
		<?cs elif:subplatformid == UC_PLATFORM_PY_SUBID_MOBILE_WM?>
			<?cs set:_sourceid = 21?>
		<?cs elif:subplatformid == UC_PLATFORM_PY_SUBID_MOBILE_WP7?>
			<?cs set:_sourceid = 22?>
		<?cs else ?>
			<?cs set:_sourceid = 8?>
		<?cs /if?>
	<?cs elif:UC_PLATFORM_ID_MOBILE == platformid?>
		<?cs if:subplatformid == UC_PLATFORM_MOBILE_SUBID_WEB?>
			<?cs set:_sourceid = 2?>
		<?cs elif:subplatformid == UC_PLATFORM_MOBILE_SUBID_CLIENT?>
			<?cs set:_sourceid = 4?>
		<?cs elif:subplatformid == UC_PLATFORM_MOBILE_SUBID_IPHONE?>
			<?cs set:_sourceid = 23?>
		<?cs elif:subplatformid == UC_PLATFORM_MOBILE_SUBID_IPAD?>
			<?cs set:_sourceid = 24?>
		<?cs elif:subplatformid == UC_PLATFORM_MOBILE_SUBID_ANDROID?>
			<?cs set:_sourceid = 25?>
		<?cs elif:subplatformid == UC_PLATFORM_MOBILE_SUBID_SYBIAM?>
			<?cs set:_sourceid = 26?>
		<?cs elif:subplatformid == UC_PLATFORM_MOBILE_SUBID_WM?>
			<?cs set:_sourceid = 27?>
		<?cs elif:subplatformid == UC_PLATFORM_MOBILE_SUBID_WP7?>
			<?cs set:_sourceid = 28?>
		<?cs elif:subplatformid == UC_PLATFORM_MOBILE_SUBID_MQZONE?>
			<?cs set:_sourceid = 29?>
		<?cs elif:subplatformid == UC_PLATFORM_MOBILE_SUBID_SMSG?>
			<?cs set:_sourceid = 30?>
		<?cs elif:subplatformid == UC_PLATFORM_MOBILE_SUBID_MMSG?>
			<?cs set:_sourceid = 31?>
		<?cs elif:subplatformid == UC_PLATFORM_MOBILE_SUBID_QPAI?>
			<?cs set:_sourceid = 32?>
		<?cs elif:subplatformid == UC_PLATFORM_MOBILE_SUBID_IPHONE_3?>
			<?cs set:_sourceid = 41?>
		<?cs else ?>
			<?cs set:_sourceid = 2?>
		<?cs /if?>
	<?cs elif:UC_PLATFORM_ID_WEIBO == platformid?>
		<?cs if:subplatformid == UC_PLATFORM_WB_SUBID_WEB?>
			<?cs set:_sourceid = 6?>
		<?cs elif:subplatformid == UC_PLATFORM_WB_SUBID_MOBILE?>
			<?cs set:_sourceid = 33?>
		<?cs elif:subplatformid == UC_PLATFORM_WB_SUBID_MOBILE_IPHONE?>
			<?cs set:_sourceid = 34?>
		<?cs elif:subplatformid == UC_PLATFORM_WB_SUBID_MOBILE_IPAD?>
			<?cs set:_sourceid = 35?>
		<?cs elif:subplatformid == UC_PLATFORM_WB_SUBID_MOBILE_ANDROID?>
			<?cs set:_sourceid = 36?>
		<?cs elif:subplatformid == UC_PLATFORM_WB_SUBID_MOBILE_SYBIAM?>
			<?cs set:_sourceid = 37?>
		<?cs elif:subplatformid == UC_PLATFORM_WB_SUBID_MOBILE_WM?>
			<?cs set:_sourceid = 38?>
		<?cs elif:subplatformid == UC_PLATFORM_WB_SUBID_MOBILE_WP7?>
			<?cs set:_sourceid = 39?>
		<?cs else ?>
			<?cs set:_sourceid = 6?>
		<?cs /if?>
	<?cs elif:UC_PLATFORM_ID_THIRDAPP == platformid?>
		<?cs #/* TODO */
			/*从qz_metadata.orgdata.source_name
				qz_metadata.orgdata.source_url
			拿*/
		?>
	<?cs elif:UC_PLATFORM_ID_PHONEMANAGE == platformid?>
		<?cs set:_sourceid = 43?>
	<?cs elif:UC_PLATFORM_ID_OTHER == platformid?>		<?cs #/*自定义，走自己的通知*/?>
		<?cs #/* TODO **同上*/?>
	<?cs /if?>
	<?cs set:getSourceid.ret = _sourceid ?>
<?cs /def?>


<?cs #:来源 ?>
<?cs set:qz_source_type[1] = "QQ空间"?>
<?cs set:qz_source_type[2] = "手机QQ空间"?>
<?cs set:qz_source_type[2].url = "http://mobile.qq.com/qzone/"?>
<?cs set:qz_source_type[3] = "QQ签名"?>
<?cs set:qz_source_type[4] = "QQ空间手机版"?>
<?cs set:qz_source_type[4].url = "http://z.qzone.com?from=kjavagrzxpl"?>
<?cs set:qz_source_type[5] = "QQ"?>
<?cs set:qz_source_type[6] = "腾讯微博"?>
<?cs set:qz_source_type[7] = "网页"?>
<?cs set:qz_source_type[8] = "朋友网"?>
<?cs set:qz_source_type[8].url = "http://www.pengyou.com/"?>
<?cs set:qz_source_type[9] = "时光轴"?>
<?cs set:qz_source_type[10] = "taotao主站"?>
<?cs set:qz_source_type[11] = "心情机器人"?>
<?cs set:qz_source_type[12] = "QQ美食"?>
<?cs set:qz_source_type[13] = "QQ秀"?>
<?cs set:qz_source_type[14] = "QQ浏览器"?>
<?cs set:qz_source_type[15] = "朋友网二手交易"?>
<?cs set:qz_source_type[16] = "朋友网客户端"?>
<?cs set:qz_source_type[17] = "朋友网iPhone客户端"?>
<?cs set:qz_source_type[18] = "朋友网iPad客户端"?>
<?cs set:qz_source_type[19] = "朋友网android客户端"?>
<?cs set:qz_source_type[20] = "朋友网symbian客户端"?>
<?cs set:qz_source_type[21] = "朋友网WM客户端"?>
<?cs set:qz_source_type[22] = "朋友网WP7客户端"?>
<?cs set:qz_source_type_pengyouClient  = "http://www.pengyou.com/mobile?from=home" ?>
<?cs set:qz_source_type[23] = "iPhone"?>
<?cs set:qz_source_type[23].url = "http://z.qzone.com?from=iphonegrzxpl"?>
<?cs set:qz_source_type[24] = "iPad"?>
<?cs set:qz_source_type[24].url = "http://z.qzone.com?from=ipadgrzxpl"?>
<?cs set:qz_source_type[25] = "Android"?>
<?cs set:qz_source_type[25].url = "http://z.qzone.com?from=androidgrzxpl"?>
<?cs set:qz_source_type[26] = "sybiam客户端"?>
<?cs set:qz_source_type[26].url = "http://mobile.qq.com/qzone/s60v5/"?>
<?cs set:qz_source_type[27] = "WM客户端"?>
<?cs set:qz_source_type[27].url = "http://mobile.qq.com/qzone/windowsmobile/"?>
<?cs set:qz_source_type[28] = "WP7客户端"?>
<?cs set:qz_source_type[28].url = "http://mobile.qq.com/qzone/wp7/"?>
<?cs set:qz_source_type[29] = "手机Qzone"?>
<?cs set:qz_source_type[30] = "手机短信"?>
<?cs set:qz_source_type[31] = "手机彩信"?>
<?cs set:qz_source_type[32] = "Q拍"?>
<?cs set:qz_source_type[33] = "微博客户端"?>
<?cs set:qz_source_type[34] = "微博iPhone客户端"?>
<?cs set:qz_source_type[35] = "微博iPad客户端"?>
<?cs set:qz_source_type[36] = "微博android客户端"?>
<?cs set:qz_source_type[37] = "微博symbian客户端"?>
<?cs set:qz_source_type[38] = "微博WM客户端"?>
<?cs set:qz_source_type[39] = "微博WP7客户端"?>
<?cs set:qz_source_type[40] = "影视频道"?>
<?cs set:qz_source_type[40].url = "http://rc.qzone.qq.com/appsetup/video?source=vote.feed" ?>
<?cs set:qz_source_type[41] = "iPhone"?>
<?cs set:qz_source_type[41].url = "http://mobile.qq.com/qzone/iphone/" ?>
<?cs set:qz_source_type[42] = "QQ影像空间版"?>
<?cs set:qz_source_type[42].url = "http://image.qq.com/qzone/" ?>
<?cs set:qz_source_type[43] = "手机管家PC版"?>
<?cs set:qz_source_type[43].url = "http://sj.qq.com/?qid=990484" ?>
<?cs set:qz_source_type[44] = "QQ提醒"?>
<?cs set:qz_source_type[44].url = "http://rc.qzone.qq.com/myhome/904" ?>
<?cs set:qz_source_type[45] = "QQ空间触屏版"?>



<?cs def:_data_source(text, url)?>
	<?cs if:text?>
		<?cs if:url?>
			<?cs call:set("source", "type", "url")?>
			<?cs call:qfv("source.url", url)?>
		<?cs else ?>
			<?cs call:set("source", "type", "txt")?>
		<?cs /if?>
		<?cs call:qfv("source.text", text)?>
	<?cs /if?>
<?cs /def?>

<?cs def:data_source_id()?>
	<?cs set:_txt = ""?>
	<?cs set:_url = ""?>
	<?cs call:getSourceid(qz_metadata.orgdata.platformid, qz_metadata.orgdata.platformsubid)?>
	<?cs with:sourceid = getSourceid.ret?>
		<?cs if:sourceid && sourceid != 1 && qz_source_type[sourceid]?><?cs #/*"QQ空间来源" 不展示*/?>
			<?cs call:_data_source(qz_source_type[sourceid], qz_source_type[sourceid].url)?>
		<?cs elif:qz_metadata.orgdata.source_name && qz_metadata.orgdata.source_name != "QQ空间"?>
			<?cs call:_data_source(qz_metadata.orgdata.source_name, qz_metadata.orgdata.source_url)?>
		<?cs /if?>
	<?cs /with?>
<?cs /def ?>

<?cs def:data_source()?>
	<?cs #/* 只显示指定来源 */?>
	<?cs set:_platformid = qz_metadata.orgdata.platformid?>
	<?cs set:_subplatformid = qz_metadata.orgdata.platformsubid?>
	<?cs if:(_platformid == UC_PLATFORM_ID_QZONE && _subplatformid == 11) || (_platformid == UC_PLATFORM_ID_MOBILE && _subplatformid <= 12) ?>
		<?cs call:data_source_id()?>
	<?cs /if?>	
<?cs /def?>

<?cs #:
	/*把源信息展示在正文中，一般用于那些转发分享等feeds*/
	function data_cntText_source(){}
?>
<?cs def:data_cntText_source() ?>
	<?cs call:getSourceid(qz_metadata.orgdata.platformid, qz_metadata.orgdata.platformsubid)?>
	<?cs with:sourceid = getSourceid.ret?>
		<?cs if:sourceid && sourceid != 1 && qz_source_type[sourceid]?><?cs #/*"QQ空间来源" 不展示*/?>
			<?cs call:data_extendinfo_source(0, qz_source_type[sourceid], qz_source_type[sourceid].url)?>
		<?cs elif:qz_metadata.orgdata.source_name && qz_metadata.orgdata.source_name != "QQ空间"?>
			<?cs call:data_extendinfo_source(0, qz_metadata.orgdata.source_name, qz_metadata.orgdata.source_url)?>
		<?cs /if?>
	<?cs /with?>
<?cs /def ?>

<?cs #:
	/**/
	function data_curNode_source(){}
?>
<?cs def:data_curNode_source() ?>
	<?cs call:getSourceid(qz_metadata.RelyBody.0.platformid, qz_metadata.RelyBody.0.platformsubid)?>
	<?cs with:sourceid = getSourceid.ret?>
		<?cs if:sourceid && sourceid != 1 && qz_source_type[sourceid]?><?cs #/*"QQ空间来源" 不展示*/?>
			<?cs call:_data_source(qz_source_type[sourceid], qz_source_type[sourceid].url)?>
		<?cs elif:qz_metadata.RelyBody.0.source_name && qz_metadata.RelyBody.0.source_name != "QQ空间"?>
			<?cs call:_data_source(qz_metadata.RelyBody.0.source_name, qz_metadata.RelyBody.0.source_url)?>
		<?cs /if?>
	<?cs /with?>
	<?cs call:data_cntText_source() ?>
<?cs /def ?>

<?cs set:USER_PLATFORM_WHO_QZONE = 1?>
<?cs set:USER_PLATFORM_WHO_PY = 2?>
<?cs set:USER_PLATFORM_WHO_WEIBO = 3?>
<?cs #
	/*
	 *从platformid和subplatformid解析出who的值
	 */
?>
<?cs def:get_userWho_platform(platformid, subplatformid)?>
	<?cs set:_user_platform_who = USER_PLATFORM_WHO_QZONE?>
	<?cs if:platformid == UC_PLATFORM_ID_QZONE ?>
	<?cs elif:platformid == UC_PLATFORM_ID_PENGYOU ?>
		<?cs set:_user_platform_who = USER_PLATFORM_WHO_PY?>
	<?cs elif:platformid == UC_PLATFORM_ID_WEIBO ?>
		<?cs set:_user_platform_who = USER_PLATFORM_WHO_WEIBO?>
	<?cs /if?>
	<?cs set:get_userWho_platform.ret = _user_platform_who?>
<?cs /def?>
