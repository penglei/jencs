<?cs ####
	/*应用内容区*/
?>

<?cs def:data_app_contentbox()?>
	<?cs call:i()?>

	<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
		<?cs if:qz_metadata.orgdata.subtype == APP_subtype_addapp?>
			<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
				<?cs call:data_cntTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname) ?>
				<?cs call:data_cntTitle_tipTxt(" 添加了应用")?>
				<?cs call:data_cntTitle_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
			<?cs else ?>
				<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname) ?>
				<?cs call:data_textTitle_tipTxt("添加了应用")?>
				<?cs call:data_textTitle_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
			<?cs /if ?>
			
		<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_share?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname) ?>
			<?cs call:data_textTitle_tipTxt("推荐试玩")?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
		<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_game?>
			<?cs #call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname) ?>
			<?cs #call:data_textTitle_rich(qz_metadata.orgdata.itemdata[0].desc)?>
			<?cs #call:data_textTitle_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
			<?cs #call:data_textTitle_tipTxt(qz_metadata.orgdata.itemdata[0].extendinfo.strAppComment)?>
		<?cs /if?>
	<?cs /if?>

	<?cs if:qz_metadata.orgdata.subtype == APP_subtype_invite?>
		<?cs set:_path = "content.appinfo" ?>
		<?cs call:set(_path, "app_img", qz_metadata.orgdata.itemdata[0].picinfo[0].url) ?>
		<?cs call:set(_path, "app_url", qz_metadata.orgdata.itemdata[0].action) ?>
		<?cs call:set(_path, "desc", qz_metadata.orgdata.itemdata[0].desc.0.content) ?>
		<?cs call:set(_path + ".star", "percent", qz_metadata.orgdata.itemdata[0].extendinfo.strScore * 10) ?>
		<?cs call:set(_path + ".star", "score", qz_metadata.orgdata.itemdata[0].extendinfo.strScore) ?>

		<?cs call:data_content_text(qz_metadata.orgdata.content)?>
	<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_invite_with_mqzone?>
		<?cs set:_path = "content.appinfo" ?>
		<?cs call:set(_path, "app_img", qz_metadata.orgdata.itemdata[0].picinfo[0].url) ?>
		<?cs call:set(_path, "app_url", qz_metadata.orgdata.itemdata[0].action) ?>
		<?cs call:set(_path, "desc", qz_metadata.orgdata.itemdata[0].desc.0.content) ?>
		
		<?cs call:data_content_text(qz_metadata.orgdata.content)?>
	<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_addapp?>
		<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_SMALL_V8_MODE , "") ?>
		<?cs call:data_textTitle_url(qz_metadata.orgdata.itemdata[0].name,qz_metadata.orgdata.itemdata[0].action) ?>
		<?cs set:_path = "content.appinfo" ?>
		<?cs call:set(_path, "app_id", qz_metadata.orgdata.itemdata[0].itemid) ?>
		<?cs call:set(_path, "app_img", qz_metadata.orgdata.itemdata[0].picinfo[0].url) ?>
		<?cs call:set(_path, "app_url", qz_metadata.orgdata.itemdata[0].action) ?>
		<?cs call:set(_path, "desc", qz_metadata.orgdata.itemdata[0].desc.0.content) ?>
		<?cs call:set(_path + ".star", "percent", qz_metadata.orgdata.itemdata[0].extendinfo.strScore * 10) ?>
		<?cs call:set(_path + ".star", "score", qz_metadata.orgdata.itemdata[0].extendinfo.strScore) ?>
		<?cs call:data_extendinfo_nick(0, qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname) ?>
		<?cs if:subcount(qz_metadata.orgdata.itemdata) == 2?>
			<?cs call:data_extendinfo_txt(0, "还添加了")?>
			<?cs call:data_extendinfo_url(0, qz_metadata.orgdata.itemdata[1].name, qz_metadata.orgdata.itemdata[1].action)?>
		<?cs elif:subcount(qz_metadata.orgdata.itemdata) == 3?>
			<?cs call:data_extendinfo_txt(0, "还添加了")?>
			<?cs call:data_extendinfo_url(0, qz_metadata.orgdata.itemdata[1].name, qz_metadata.orgdata.itemdata[1].action)?>
			<?cs call:data_extendinfo_txt(0, "和")?>
			<?cs call:data_extendinfo_url(0, qz_metadata.orgdata.itemdata[2].name, qz_metadata.orgdata.itemdata[2].action)?>
		<?cs elif:subcount(qz_metadata.orgdata.itemdata) > 3?>
			<?cs #call:data_content_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname) ?>
			<?cs call:data_extendinfo_txt(0, "还添加了")?>
			<?cs call:data_extendinfo_url(0, qz_metadata.orgdata.itemdata[1].name, qz_metadata.orgdata.itemdata[1].action)?>
			<?cs call:data_extendinfo_txt(0, "、")?>
			<?cs call:data_extendinfo_url(0, qz_metadata.orgdata.itemdata[2].name, qz_metadata.orgdata.itemdata[2].action)?>
			<?cs call:data_extendinfo_txt(0, "和")?>
			<?cs call:data_extendinfo_url(0, qz_metadata.orgdata.itemdata[3].name, qz_metadata.orgdata.itemdata[3].action)?>
			<?cs call:data_extendinfo_txt(0, "等" + qz_metadata.orgdata.itemcount + "款应用")?>
		<?cs /if?>
	<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_activate?>
		<?cs call:data_content_init(G_LAYOUT_LEFTIMG, G_IMG_SMALL_MODE , "") ?>

		<?cs #call:data_content_url(qz_metadata.orgdata.itemdata[0].action, qz_metadata.orgdata.itemdata[0].desc)?>
		<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], qz_metadata.orgdata.itemdata[0].action, "", "") ?>

		<?cs call:data_content_text(qz_metadata.orgdata.itemdata[0].desc)?>
		<?cs call:data_content_p_start() ?>
		<?cs call:data_content_url(qz_metadata.orgdata.itemdata[0].extendinfo.strLinkURL, qz_metadata.orgdata.itemdata[0].extendinfo.strLinkText)?>
		<?cs call:data_content_p_end() ?>

		<?cs set:iSameUserCount = subcount(qz_metadata.orgdata.extendinfo.uininfo.uin_invit) ?>
		<?cs if:iSameUserCount == 2 ?>
			<?cs call:data_extendinfo_nick(0, qz_metadata.orgdata.extendinfo.uininfo.uin_invit[1], qz_metadata.orgdata.extendinfo.uininfo.who_invit[1], qz_metadata.orgdata.extendinfo.uininfo.nick_invit[1]) ?>
			<?cs call:data_extendinfo_txt(0, "也添加了此应用")?>
		<?cs elif:iSameUserCount == 3 ?>
			<?cs call:data_extendinfo_nick(0, qz_metadata.orgdata.extendinfo.uininfo.uin_invit[1], qz_metadata.orgdata.extendinfo.uininfo.who_invit[1], qz_metadata.orgdata.extendinfo.uininfo.nick_invit[1]) ?>
			<?cs call:data_extendinfo_txt(0, "和")?>
			<?cs call:data_extendinfo_nick(0, qz_metadata.orgdata.extendinfo.uininfo.uin_invit[2], qz_metadata.orgdata.extendinfo.uininfo.who_invit[2], qz_metadata.orgdata.extendinfo.uininfo.nick_invit[2]) ?>
			<?cs call:data_extendinfo_txt(0, "也添加了此应用")?>
		<?cs elif:iSameUserCount == 4 ?>
			<?cs call:data_extendinfo_nick(0, qz_metadata.orgdata.extendinfo.uininfo.uin_invit[1], qz_metadata.orgdata.extendinfo.uininfo.who_invit[1], qz_metadata.orgdata.extendinfo.uininfo.nick_invit[1]) ?>
			<?cs call:data_extendinfo_txt(0, "、")?>
			<?cs call:data_extendinfo_nick(0, qz_metadata.orgdata.extendinfo.uininfo.uin_invit[2], qz_metadata.orgdata.extendinfo.uininfo.who_invit[2], qz_metadata.orgdata.extendinfo.uininfo.nick_invit[2]) ?>
			<?cs call:data_extendinfo_txt(0, "和")?>
			<?cs call:data_extendinfo_nick(0, qz_metadata.orgdata.extendinfo.uininfo.uin_invit[3], qz_metadata.orgdata.extendinfo.uininfo.who_invit[3], qz_metadata.orgdata.extendinfo.uininfo.nick_invit[3]) ?>
			<?cs call:data_extendinfo_txt(0, "也添加了此应用")?>
		<?cs /if?>
	<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_share?>
		<?cs call:data_content_init(G_LAYOUT_LEFTIMG, G_IMG_SMALL_MODE , "") ?>
		<?cs call:data_textTitle_rich(qz_metadata.orgdata.desc)?>

		<?cs call:data_content_p_start() ?>
		<?cs call:data_content_text(qz_metadata.orgdata.itemdata[0].desc)?>
		<?cs call:data_content_p_end() ?>
		<?cs call:qfv("content.tryplay.pic.0.type","tryplay") ?>
		<?cs call:qfv("meta.itemid",qz_metadata.orgdata.mkey) ?>
		<?cs set:_app_tryplay_param="{action:3,flashurl:'"+qz_metadata.orgdata.itemdata[0].extendinfo.strActURL+"',appid:"+qz_metadata.orgdata.mkey+",flashid:'flash_"+qz_metadata.orgdata.mkey+"',playbtn:'controls_"+qz_metadata.orgdata.mkey+"',redarea:'player_"+qz_metadata.orgdata.mkey+"'}" ?>
		<?cs call:data_popup("content.tryplay.pic.0.action", "", "/open/fusion/demo_sharing.js", _app_tryplay_param, 4, 120, 120, "GamingFeed", "game_try_play") ?>
		<?cs call:data_popup("content.tryplay.unfold.action", "收起", "/open/fusion/demo_sharing.js", "{action:4"+",flashurl:'"+qz_metadata.orgdata.itemdata[0].extendinfo.strActURL+"',appid:352,flashid:'flash_"+qz_metadata.orgdata.mkey+"',playbtn:'controls_"+qz_metadata.orgdata.mkey+"',redarea:'player_"+qz_metadata.orgdata.mkey+"'}", 4, 375, 169, "GamingFeed", "game_try_play_unfold") ?>
		<?cs call:data_popup_add_attr("content.tryplay.unfold.action","className","video_retract_bt bor3 bg2 c_tx") ?>
		<?cs call:data_popup_add_attr("content.tryplay.unfold.action","cssText","float:right") ?>
		<?cs call:data_popup_add_attr("content.tryplay.unfold.action","needContainer",1) ?>
		<?cs call:_cnt_adapt_pic_url(qz_metadata.orgdata.itemdata[0], "")?>
		<?cs call:data_cntmedia_pic(0, _cnt_adapt_pic_url.ret, "")?>

		<?cs #call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], qz_metadata.orgdata.itemdata[0].extendinfo.strActURL, "", "") ?>

	<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_game?>
		<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_SMALL_V8_MODE , "") ?>
		<?cs call:data_textTitle_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
		<?cs call:data_content_text(qz_metadata.orgdata.itemdata[0].desc)?>
		<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], qz_metadata.orgdata.itemdata[0].action, "", "") ?>
	<?cs /if?>
<?cs /def?>
