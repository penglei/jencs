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
<?cs #set:UC_PLATFORM_MOBILE_SUBID_QPAI = 11?>		<?cs #/*Q拍，废掉，转给触屏版使用*/?>
<?cs set:UC_PLATFORM_MOBILE_SUBID_TOUCH = 11?>		<?cs #/*通过QQ空间触屏版*/?>
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


<?cs #:来源列表，只列出需要展示的 ?>
<?cs set:qz_source_type[UC_PLATFORM_ID_MOBILE][UC_PLATFORM_MOBILE_SUBID_TOUCH] = "QQ空间触屏版"?>
<?cs set:qz_source_type[UC_PLATFORM_ID_MOBILE][UC_PLATFORM_MOBILE_SUBID_TOUCH].url = "http://z.qzone.com/?bg=9&from=pcfeed"?>
<?cs set:qz_source_type[UC_PLATFORM_ID_MOBILE][UC_PLATFORM_MOBILE_SUBID_TOUCH].hotclickPath = "qzonetouch"?>
<?cs set:qz_source_type[UC_PLATFORM_ID_MOBILE][UC_PLATFORM_MOBILE_SUBID_IPHONE] = "iPhone"?>
<?cs set:qz_source_type[UC_PLATFORM_ID_MOBILE][UC_PLATFORM_MOBILE_SUBID_IPHONE].url = "http://z.qzone.com?from=iphonegrzxpl"?>
<?cs set:qz_source_type[UC_PLATFORM_ID_MOBILE][UC_PLATFORM_MOBILE_SUBID_IPAD] = "iPad"?>
<?cs set:qz_source_type[UC_PLATFORM_ID_MOBILE][UC_PLATFORM_MOBILE_SUBID_IPAD].url = "http://z.qzone.com?from=ipadgrzxpl"?>
<?cs set:qz_source_type[UC_PLATFORM_ID_MOBILE][UC_PLATFORM_MOBILE_SUBID_ANDROID] = "Android"?>
<?cs set:qz_source_type[UC_PLATFORM_ID_MOBILE][UC_PLATFORM_MOBILE_SUBID_ANDROID].url = "http://z.qzone.com?from=androidgrzxpl"?>
<?cs set:qz_source_type[UC_PLATFORM_ID_MOBILE][UC_PLATFORM_MOBILE_SUBID_IPHONE_3] = "iPhone"?>
<?cs set:qz_source_type[UC_PLATFORM_ID_MOBILE][UC_PLATFORM_MOBILE_SUBID_IPHONE_3].url = "http://mobile.qq.com/qzone/iphone/" ?>


<?cs def:_data_source(text, url, hotclickPath)?>
	<?cs if:text?>
		<?cs if:url?>
			<?cs call:set("source", "type", "url")?>
			<?cs call:qfv("source.url", url)?>
		<?cs else ?>
			<?cs call:set("source", "type", "txt")?>
		<?cs /if?>
		<?cs call:qfv("source.text", text)?>
		<?cs if:hotclickPath?>
			<?cs call:qfv("source.hotclickPath", hotclickPath)?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs #/* 来源信息，如果有转发链就取最外层 */?>
<?cs def:data_source()?>
	<?cs if:qz_metadata.relybody.0.platformid ?>
		<?cs set:_platformid = qz_metadata.relybody.0.platformid?>
		<?cs set:_subplatformid = qz_metadata.relybody.0.platformsubid?>
		<?cs set:_useragent = qz_metadata.relybody.0.useragent?>
	<?cs else?>
		<?cs set:_platformid = qz_metadata.orgdata.platformid?>
		<?cs set:_subplatformid = qz_metadata.orgdata.platformsubid?>
		<?cs set:_useragent = qz_metadata.orgdata.useragent?>
	<?cs /if?>
	<?cs if:qz_source_type[_platformid][_subplatformid] ?>
		<?cs if:_useragent?>
			<?cs call:_data_source(_useragent, qz_source_type[_platformid][_subplatformid].url, qz_source_type[_platformid][_subplatformid].hotclickPath)?>
		<?cs else?>
			<?cs call:_data_source(qz_source_type[_platformid][_subplatformid], qz_source_type[_platformid][_subplatformid].url, qz_source_type[_platformid][_subplatformid].hotclickPath)?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs #/* 来源信息，用户定制的*/?>
<?cs def:data_source_custom(text, url)?>
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

<?cs set:USER_PLATFORM_WHO_QZONE = 1?>
<?cs set:USER_PLATFORM_WHO_PY = 2?>
<?cs set:USER_PLATFORM_WHO_WEIBO = 3?>
<?cs ####
	/**
	 *从platformid和subplatformid解析出who的值
	 */
?>
<?cs def:get_userWho_platform(platformid, subplatformid)?>
	<?cs set:_user_platform_who = USER_PLATFORM_WHO_QZONE?>
	<?cs if:platformid == UC_PLATFORM_ID_QZONE || platformid == USER_PLATFORM_WHO_QZONE ?>
	<?cs elif:platformid == UC_PLATFORM_ID_PENGYOU || platformid== USER_PLATFORM_WHO_PY ?>
		<?cs set:_user_platform_who = USER_PLATFORM_WHO_PY?>
	<?cs elif:platformid == UC_PLATFORM_ID_WEIBO || platformid==USER_PLATFORM_WHO_WEIBO?>
		<?cs set:_user_platform_who = USER_PLATFORM_WHO_WEIBO?>
	<?cs /if?>
	<?cs set:get_userWho_platform.ret = _user_platform_who?>
<?cs /def?>
