<?cs #/*活动礼物*/#?>

<?cs with:giftItem = qz_metadata.orgdata.itemdata.0?><?cs #/*title里面都用第一个礼物*/#?>

<?cs #/*---生成title逻辑---*/?>
<?cs set:_nTitleType = giftItem.extendinfo.strExtendInfo1.nTitleType?>

<?cs call:i()?>
<?cs if:_nTitleType == TITILE_type_with_host?>
	<?cs call:data_title_tipTxt("为")?>
	<?cs call:i++()?>
	<?cs call:datag_main_user("title.con" + i, "link", 10, "")?>
<?cs /if?>
<?cs call:data_title_tipTxt(giftItem.extendinfo.strExtendInfo1)?>

<?cs #/*title里有礼物名称*/#?>
<?cs call:data_title_defaultTxt(giftItem.name)?>
<?cs if:_nTitleType == TITLE_type_with_host?>
	<?cs call:data_delivergift_event_popup(data_title_defaultTxt.ret.path, giftItem,"gift.title.giftname")?>
<?cs else ?>
	<?cs call:data_gift_event_popup(data_title_defaultTxt.ret.path, giftItem,"gift.title.giftname")?>
<?cs /if?>

<?cs #/*title最后一项，有些礼物需要加上"赠送次数"*/#?>
<?cs if:giftItem.extendinfo.strSendUserInfo?>
	<?cs call:data_title_tipTxt(giftItem.extendinfo.strSendUserInfo)?>
<?cs elif:giftItem.extendinfo.nGiftCount ?>
	<?cs call:data_title_tipTxt("该礼物已被赠送" + giftItem.extendinfo.nGiftCount + "次")?>
<?cs /if?>


<?cs #/*---desc---*/?>
<?cs call:data_quote_desc(giftItem.desc)?>

<?cs #/*---contentbox 只有一张图片---*/?>
<?cs call:gift_media_url(giftItem)?>
<?cs call:data_cntmedia_pic(0, gift_media_url.ret, "")?>
<?cs if:_nTitleType == TITLE_type_with_host?>
	<?cs call:data_delivergift_event_popup(data_cntmedia_pic.ret, giftItem,"gift.content.pic")?>
<?cs else ?>
	<?cs call:data_gift_event_popup(data_cntmedia_pic.ret, giftItem,"gift.content.pic")?>
<?cs /if?>

<?cs #{/*extend_info.con*/?>
<?cs #/* TODO 活动礼物没有其它扩展信息，只有与前台相关的合并信息(__samefeed_users)*/#?>
<?cs #/*"也" + strExtendInfo1 + "礼物"*/?>


<?cs #{/*---operate.opr---*/?>
<?cs call:i()?>
<?cs if:qz_metadata.orgdata.extendinfo.nActionType == ACTION_TYPE_URL?>

	<?cs set:_uin = 0?>
	<?cs if:_nTitleType == TITLE_type_with_host?>
		<?cs set:_uin = qz_metadata.orgdata.uin?>
	<?cs /if?>
	<?cs #/*我擦，说是olympic，其实是点击直接发请求的按钮*/?>
	<?cs set:_param = "{type_id:6,gift_id:" + giftItem.itemid + 
					",suin:" + _uin + 
					",msg_id:" + qz_metadata.orgdata.extendinfo.nActionData
					+ ",action:'olympic'}"?>

	<?cs call:data_opr_txtPopup(i,
			"我也送礼",
			"送礼物",
			"http://imgcache.qq.com/qzone/gift/send_list.html",
			_param,
			6, 0, 0,"gift.opr.metoo")?>
<?cs else ?>
	<?cs if:_nTitleType == TITLE_type_with_host?>
		<?cs call:gift_event_popup_param(giftItem)?>
		<?cs call:data_opr_txtPopup(i,
			"我也送礼",
			"送礼物",
			"http://imgcache.qq.com/qzone/gift/send_single.html?transfer=1",
			gift_event_popup_param.ret,
			1, 623, 339,"gift.opr.metoo")?>

	<?cs else ?>
		<?cs call:gift_event_popup_param(giftItem)?>
		<?cs call:data_opr_txtPopup(i,
			"我也送礼",
			"送礼物",
			"http://imgcache.qq.com/qzone/gift/send_single.html",
			gift_event_popup_param.ret,
			1, 623, 339,"gift.opr.metoo")?>

	<?cs /if?>
<?cs /if?>

<?cs #/*第二个按钮*/?>
<?cs call:i++()?>
<?cs call:gift_event_popup_param(giftItem)?>
<?cs set:_param = gift_event_popup_param.ret + "&uin=" + qz_metadata.orgdata.uin + "&type=556&"?>
<?cs call:data_opr_txtPopup(i, "选择其它礼物",
			"送礼物",
			"http://imgcache.qq.com/qzone/gift/send_list.html",
			_param,
			1, 625, 595,"gift.opr.athorgift")?>

<?cs /with?><?cs #/*giftItem*/?>

<?cs call:data_source()?>
<?cs call:data_oprtime()?>
<?cs call:data_like()?>
<?cs call:data_opr_more()?>
<?cs call:data_opr_delfeed()?>
