<?cs ####
	/**
	 * 生日日期信息
	 * @return {String} 生日日期文字(农历|公历)
	 */
?>
<?cs def:gift_birthday_timeTxt()?>
	<?cs if:qz_metadata.orgdata.extendinfo.BirthdayFlag == 1?>
		<?cs set:_birthtxt = "农历" + qz_metadata.orgdata.extendinfo.LunarBirthday?>
	<?cs else ?>
		<?cs set:_birthtxt = qz_metadata.orgdata.extendinfo.BirthdayMonth+"月" + qz_metadata.orgdata.extendinfo.BirthdayDay + "日"?>
	<?cs /if?>
	<?cs set:gift_birthday_timeTxt.ret = _birthtxt?>
<?cs /def?>

<?cs ####
	/**
	 *根据礼物信息生成礼物图片地址
	 *@param {Object} giftItem 礼物数据对象
	 */
?>
<?cs def:gift_media_url(giftItem)?>
	<?cs set:_url = g_siDomain + "/qzone/space_item/pre/"?>
	<?cs set:_tmp = giftItem.itemid % 16 ?><?cs #/*必须分开写,直接相加有问题*/?>
	<?cs set:_url = _url + _tmp + "/"?>
	<?cs set:_url = _url + giftItem.itemid + "." + giftItem.extendinfo.strPreFormat?>
	<?cs set:gift_media_url.ret = _url?>
<?cs /def?>

<?cs ####
	/**
	 * 构建活动礼物弹框参数
	 * @param {Object} giftItem 礼物数据对象
	 * @return {String} 礼物参数字符串
	 */
	 function gift_event_popup_param(giftItem)
?>
<?cs def:gift_event_popup_param(giftItem)?>
	<?cs set:ret = ""?>

	<?cs #活动礼物是主动feeds，uin在传递礼物时很难确定，还是不要加了*/?>
	<?cs #set:ret = ret + "&uin=" + qz_metadata.orgdata.uin?>

	<?cs set:ret = ret + "&itemid=" + giftItem.itemid?>

	<?cs #set:ret = ret + "&itemtype=" + giftItem.type?>
	<?cs #set:ret = ret + "&gifttype=" + giftItem.extendinfo.nSpecialType?>
	<?cs #set:ret = ret + "&format=" + giftItem.extendinfo.strFormat?>
	<?cs #set:ret = ret + "&pre_format=" + giftItem.extendinfo.strPreFormat?>
	<?cs #set:ret = ret + "&name=" + giftItem.name?><?cs #/*TODO*/ escape #?>
	<?cs #set:ret = ret + "&desp=" + giftItem.desc.0.content?>
	<?cs #set:ret = ret + "&price=" + giftItem.extendinfo.nPrice ?>

	<?cs #/*set:_tmp = qz_metadata.orgdata.extendinfo.BirthdayYear + "-" + qz_metadata.orgdata.extendinfo.BirthdayMonth + "-" + qz_metadata.orgdata.extendinfo.BirthdayDay*/?>
	<?cs #/*set:ret = ret + "&birthday=" + _tmp*/?>
	<?cs #/*set:ret = ret + "&birthdaytab=1"*/?>

	<?cs set:gift_event_popup_param.ret = ret?>
<?cs /def?>

<?cs ####
	/**
	 * 构建生日礼物弹框参数
	 * @param {Object} giftItem 礼物数据对象
	 * @return {String} 礼物参数字符串
	 */
?>
<?cs def:gift_birth_popup_param(giftItem)?>
	<?cs set:ret = ""?>

	<?cs if:qz_metadata.meta.loginuin != qz_metadata.meta.opuin?>
		<?cs set:ret = ret + "&uin=" + qz_metadata.meta.opuin?>
	<?cs /if?>
	<?cs set:ret = ret + "&itemid=" + giftItem.itemid?>

	<?cs #set:ret = ret + "&itemtype=" + giftItem.type?>
	<?cs #set:ret = ret + "&gifttype=" + giftItem.extendinfo.nSpecialType?>
	<?cs #set:ret = ret + "&format=" + giftItem.extendinfo.strFormat?>
	<?cs #set:ret = ret + "&pre_format=" + giftItem.extendinfo.strPreFormat?>
	<?cs #set:ret = ret + "&name=" + giftItem.name?>
	<?cs #set:ret = ret + "&desp=" + giftItem.desc.0.content?>
	<?cs #set:ret = ret + "&price=" + giftItem.extendinfo.nPrice ?>

	<?cs set:_tmp = qz_metadata.orgdata.extendinfo.BirthdayYear + "-" +
				 qz_metadata.orgdata.extendinfo.BirthdayMonth + "-" +
				 qz_metadata.orgdata.extendinfo.BirthdayDay?>
	<?cs set:ret = ret + "&birthday=" + _tmp?>
	<?cs set:ret = ret + "&birthdaytab=1"?>
	<?cs set:ret = ret + "&gender=" + qz_metadata.orgdata.extendinfo.Gender ?>
	<?cs set:gift_birth_popup_param.ret = ret?>
<?cs /def?>

<?cs ####
	/**
	 *发起送生日礼物参数
	 */
?>
<?cs def:gift_send_birth_param()?>
	<?cs set:_param = ""?>

	<?cs if:qz_metadata.orgdata.uin != qz_metadata.meta.loginuin?><?cs #/*不要自动对自己送礼*/?>
		<?cs set:_param = "&uin=" + qz_metadata.orgdata.uin ?>
	<?cs /if?>

	<?cs if:qz_metadata.orgdata.extendinfo.BirthdayFlag == 1?><?cs #/*如果为阴历的话，需要多传三个参数及标志位*/?>
		<?cs set:_birthday = qz_metadata.orgdata.extendinfo.LunarBirthdayYear + "-" +
					qz_metadata.orgdata.extendinfo.LunarBirthdayMonth + "-" +
					qz_metadata.orgdata.extendinfo.LunarBirthdayDay?>
		<?cs set:_param = _param + "&birthday=" + _birthday + "&type=563&birthdaytab=1&lunarFlag=1&gender="
			+ qz_metadata.orgdata.extendinfo.Gender ?>
	<?cs else ?>
		<?cs set:_birthday = qz_metadata.orgdata.extendinfo.BirthdayYear + "-" +
					qz_metadata.orgdata.extendinfo.BirthdayMonth + "-" +
					qz_metadata.orgdata.extendinfo.BirthdayDay?>
		<?cs set:_param = _param + "&birthday=" + _birthday + "&type=563&birthdaytab=1&gender=" + qz_metadata.orgdata.extendinfo.Gender ?>
	<?cs /if?>

	<?cs set:gift_send_birth_param.ret = _param?>
<?cs /def?>

<?cs set:data_gift_birth_opr_popup_statString["赠送礼物"]="giftfeed.opr.sendgiftbtn" ?>

<?cs ####
	/**
	 *回赠礼物参数
	 */
?>
<?cs def:gift_resend_birth_param()?>
	<?cs #{/*operate.opr.0*//*答谢或回赠礼物按钮*/?>
	<?cs set:_senderuin = qz_metadata.meta.loginuin?><?cs #/*登录态uin*/#?>
	<?cs set:_receiveruin = qz_metadata.meta.userid?><?cs #/*回赠永远是对方*/?>

	<?cs set:_param_cflag = 0?>
	<?cs if:qz_metadata.orgdata.platformid == UC_PLATFORM_ID_PENGYOU?>
		<?cs set:_param_cflag = 1?>
	<?cs /if?>


	<?cs set:_param = "&uin=" + _senderuin +
			"&touin=" + _receiveruin +
			"&giveback=1" +
			"&source=" + _param_cflag +
			"&nick=" + uri_encode(qz_metadata.meta.username)?><?cs #/*username是平台无关的昵称*/?>

	<?cs set:_gift_answerid = qz_metadata.orgdata.extendinfo.sRecordId?>

	<?cs if:qz_metadata.orgdata.extendinfo.gifttype == APP_GIFT?>
	<?cs else ?>
		<?cs #if:qz_metadata.orgdata.uin == qz_metadata.meta.userid ?><?cs #/*这样有一个朋友的问题/?>
		<?cs if:qz_metadata.orgdata.uin != qz_metadata.meta.loginuin?><?cs #/*考虑下面A、B送礼的情况，并且orgdata是收礼人的uin，就能得到这个隐藏逻辑*/?>
			<?cs #/*A送B礼物，B评论，A收到被动feeds，这个时候A的被动feeds有一个“回赠礼物”按钮，但是A是不能回赠给B的，当成普通送礼?>
			<?cs #/*没有answerid就不是!回赠礼物!这是什么鬼逻辑???*/?>
			<?cs set:_gift_answerid = ""?>
		<?cs /if?>
	<?cs /if?>
	<?cs set:_param = _param + "&answerid=" + _gift_answerid?>
	<?cs set:gift_resend_birth_param.ret = _param?>
<?cs /def?>

<?cs ####
	/**
	 生日礼物操作区的特殊按钮popup展现数据对象
	 *@param {String} 数据存放路径
	 */
?>
<?cs def:data_gift_birth_opr_popup(index, text)?>

	<?cs call:gift_send_birth_param()?>
	<?cs set:statString=data_gift_birth_opr_popup_statString[text] ?>
	<?cs if: !statString ?>
		<?cs set:statString="unknow" ?>
	<?cs /if ?>
	<?cs call:data_opr_txtPopup(index,
			text,
			"送礼物",
			"http://imgcache.qq.com/qzone/gift/send_list.html",
			gift_send_birth_param.ret,
			1, 673, 431,statString)?>
<?cs /def?>

<?cs ####
	/**
	 *根据礼物信息构建popup展现数据对象
	 *@param {String} path 数据存放的路径
	 *@param {String} param 礼物浮层的参数
	 */
?>
<?cs def:data_gift_popup(path, param,stat)?>
	<?cs #/*生成popup动作参数*/?>
	<?cs call:data_popup(path + ".action",
				"送礼物",
				"http://imgcache.qq.com/qzone/gift/send_single.html",
				param, 1, 623, 339, "", stat)?>
<?cs /def?>

<?cs ####
	/**
	 *根据礼物信息构建popup展现数据对象
	 *@param {String} path 数据存放的路径
	 *@param {String} param 礼物浮层的参数
	 */
?>
<?cs def:data_delivergift_popup(path, param,stat)?>
	<?cs #/*生成popup动作参数*/?>
	<?cs call:data_popup(path + ".action",
				"送礼物",
				"http://imgcache.qq.com/qzone/gift/send_single.html?transfer=1",
				param, 1, 623, 339, "", stat)?>
<?cs /def?>

<?cs def:data_gift_event_popup(path, giftItem,stat)?>
	<?cs call:gift_event_popup_param(giftItem)?>
	<?cs call:data_gift_popup(path, gift_event_popup_param.ret,stat)?>
<?cs /def?>

<?cs def:data_gift_birth_popup(path, giftItem,stat)?>
	<?cs call:gift_birth_popup_param(giftItem)?>
	<?cs call:data_gift_popup(path, gift_birth_popup_param.ret,stat)?>
<?cs /def?>

<?cs def:data_delivergift_event_popup(path, giftItem,stat)?>
	<?cs call:gift_event_popup_param(giftItem)?>
	<?cs call:data_delivergift_popup(path, gift_event_popup_param.ret,stat)?>
<?cs /def?>

<?cs ####
	/**
	 *生日、活动礼物交互被动feeds
	 *!注意!回复被动也会用到
	 */
?>
<?cs def:data_gift_psv_oprs()?>
	<?cs #{/*operate.opr.0*//*答谢或回赠礼物按钮*/?>
	<?cs set:_senderuin = qz_metadata.meta.loginuin?><?cs #/*登录态uin*/#?>
	<?cs set:_receiveruin = qz_metadata.meta.userid?><?cs #/*回赠永远是对方*/?>

	<?cs set:_param_cflag = 0?>
	<?cs if:qz_metadata.orgdata.platformid == UC_PLATFORM_ID_PENGYOU?>
		<?cs set:_param_cflag = 1?>
	<?cs /if?>


	<?cs set:_param = "&uin=" + _senderuin +
			"&touin=" + _receiveruin +
			"&giveback=1" +
			"&source=" + _param_cflag +
			"&nick=" + uri_encode(qz_metadata.meta.username)?><?cs #/*username是平台无关的昵称*/?>

	<?cs set:_gift_answerid = qz_metadata.orgdata.extendinfo.sRecordId?>

	<?cs if:qz_metadata.orgdata.extendinfo.gifttype == APP_GIFT?>
		<?cs #/*第三方应用中发的礼物*/#?>
		<?cs set:_titletxt = "答谢"?>
		<?cs set:statString="gift.opr.daxie" ?>
	<?cs else ?>
		<?cs #if:qz_metadata.orgdata.uin == qz_metadata.meta.userid ?><?cs #/*这样有一个朋友的问题/?>
		<?cs if:qz_metadata.orgdata.uin != qz_metadata.meta.loginuin?><?cs #/*考虑下面A、B送礼的情况，并且orgdata是收礼人的uin，就能得到这个隐藏逻辑*/?>
			<?cs #/*A送B礼物，B评论，A收到被动feeds，这个时候A的被动feeds有一个“回赠礼物”按钮，但是A是不能回赠给B的，当成普通送礼?>
			<?cs #/*没有answerid就不是!回赠礼物!这是什么鬼逻辑???*/?>
			<?cs set:_gift_answerid = ""?>
		<?cs /if?>
		<?cs set:_titletxt = "回赠礼物"?>
		<?cs set:statString="gift.opr.huizeng" ?>
	<?cs /if?>
	<?cs set:_param = _param + "&answerid=" + _gift_answerid?>

	<?cs call:i()?>
	<?cs call:data_opr_txtPopup(i,
			_titletxt,
			"答谢",
			"http://imgcache.qq.com/qzone/gift/view_send.html",
			_param, 1, 625, 450,statString)?>

	<?cs if:qz_metadata.orgdata.extendinfo.gifttype == APP_GIFT ?>
		<?cs #/*只有appgift有领取礼物按钮*/?>
		<?cs call:i++()?>
		<?cs call:data_opr_url(i, "领取礼物", qz_metadata.orgdata.srcurl,"gift.opr.getgift")?>
	<?cs elif:!(qz_metadata.orgdata.itemdata.0.extendinfo.nSpecialType == 11 || 
		qz_metadata.orgdata.itemdata.0.extendinfo.nSpecialType == 12 ||
		qz_metadata.orgdata.itemdata.0.extendinfo.nSpecialType == 13 ||
		qz_metadata.orgdata.extendinfo.gifttype == GIFT_REALGIFT) ?>
		<?cs #/*语音礼物不要展示这个按钮 11表示语音礼物*/?>
		<?cs call:i++()?>
		<?cs call:data_opr_txt(i, "转赠礼物")?>

		<?cs set:_paramOprPopup = "&itemid=" + qz_metadata.orgdata.itemdata.0.itemid?>
		<?cs set:_paramOprPopup = _paramOprPopup + "&birthday=" +
					qz_metadata.orgdata.extendinfo.BirthdayYear + "-" +
					qz_metadata.orgdata.extendinfo.BirthdayMonth + "-" +
					qz_metadata.orgdata.extendinfo.BirthdayDay + "&birthdaytab=1&gender=" + 
					qz_metadata.orgdata.extendinfo.Gender ?>

		<?cs call:data_gift_popup(data_opr_txt.ret.path, _paramOprPopup,"gift.opr.transfer")?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *生成评论参数
	 *也会在回复被动feeds中用到
	 *@param {Integer} seq 对应评论的序号
	 *@param {Integer} uin 回复所有者或者feed opuin
	 *@param {Boolean} feedflag 是否是假feeds (1)
	 */
?>
<?cs def:_gift_psv_comments_param(seq, uin, feedflag)?>
	<?cs set:_param = appid + "''"
				+ qz_metadata.meta.opuin + "''"
				+ qz_metadata.orgdata.mkey + "''"
				+ seq + "''"
				+ uin + "''"
				+ qz_metadata.feedtype + "''"
				+ feedflag + "''"
				+ qz_metadata.orgdata.subtype ?>

	<?cs set:_gift_psv_comments_param.ret = _param?>
<?cs /def?>
