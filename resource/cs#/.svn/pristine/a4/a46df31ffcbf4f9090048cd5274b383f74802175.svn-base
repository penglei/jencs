<?cs include:"wupcs/data/follow/const.cs"?>
<?cs include:"wupcs/data/follow/common.cs"?>
<?cs include:"wupcs/data/follow/follow_contentbox.cs"?>
<?cs include:"wupcs/data/follow/follow_title.cs"?>

<?cs call:data_main_title()?>
<?cs call:data_main_contentbox()?>
<?cs call:data_listSameAction()?>

<?cs #call:data_opr_forward()?>
<?cs call:data_oprtime()?>
<?cs call:data_source()?>
<?cs call:data_extendinfo_time() ?>	
<?cs #call:data_like()?>
<?cs if:qz_metadata.orgdata.extendinfo.appid != FOLLOW_srctype_main?>
<?cs call:data_opr_comment()?>
<?cs /if ?>
<?cs call:data_opr_more()?>
<?cs call:data_opr_prevent()?>
<?cs call:data_opr_delfeed()?>
<?cs if:qz_metadata.orgdata.subtype == FOLLOW_mtype_follow ?>


<?cs else ?>
	<?cs call:ugc_as_json_in_html(qz_metadata.opinfo.xynick,1,0) ?>
	<?cs set:_xynick = ugc_as_json_in_html.ret ?>
	<?cs call:get_remark(qz_metadata.opinfo.opuin, qz_metadata.opinfo.nickname) ?>
	<?cs call:ugc_as_json_in_html(get_remark.ret,1,0) ?>
	<?cs set:_nickname = ugc_as_json_in_html.ret ?>
	<?cs if:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_mood ?>
		<?cs if:subcount(qz_metadata.relybody) > 0 && qz_metadata.meta.loginuin == qz_metadata.relybody[0].uin ?>
			<?cs set:mood_param = "t1_source=" + qz_metadata.orgdata.mkey + "&t1_uin=" 
				+ qz_metadata.relybody[0].uin + "&t1_tid=" + qz_metadata.relybody[0].mkey + "&signin=0&sceneid=0" ?>
		<?cs else?>
			<?cs set:mood_param = "t1_source=" + qz_metadata.orgdata.mkey + "&t1_uin=" 
				+ qz_metadata.orgdata.uin + "&t1_tid=" + qz_metadata.orgdata.mkey + "&signin=0&sceneid=0" ?>
		<?cs /if?>

		<?cs call:get_userWho_platform(qz_metadata.opinfo.platformid, qz_metadata.opinfo.platformsubid)?>
		<?cs if:get_userWho_platform.ret == USER_PLATFORM_WHO_PY ?>
			<?cs set:mood_config = "{config:'1|1|1|1,b52,with_fwd,同时转发;0|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick', atuin:'" 
				+ qz_metadata.opinfo.xyuin + ",atnick:'" + _xynick + "',atwho:2}" ?>
		<?cs else ?>
			<?cs set:mood_config = "{config:'1|1|1|1,b52,with_fwd,同时转发;0|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick', atuin:" 
				+ qz_metadata.opinfo.opuin + ",atnick:'" + _nickname + "'}" ?>
		<?cs /if?>

		<?cs call:data_comments_inputbox(mood_config, mood_param, qz_metadata.meta.userid, "http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds", "utf-8")?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_share ?>
		<?cs #call:data_extendinfo_share() ?>

		<?cs set:share_param = qz_metadata.relybody[0].uin + "''" + qz_metadata.relybody[0].mkey + "''-1''0''''100''0" ?>

		<?cs call:get_userWho_platform(qz_metadata.opinfo.platformid, qz_metadata.opinfo.platformsubid)?>
		<?cs if:get_userWho_platform.ret == USER_PLATFORM_WHO_PY ?>
			<?cs set:share_config = "{config:'1|1|0|0|0|0|0', atuin:'" 
				+ qz_metadata.opinfo.xyuin + ",atnick:'" + _xynick + "',atwho:2}" ?>
		<?cs else ?>
			<?cs set:share_config = "{config:'1|1|0|0|0|0|0', atuin:" 
				+ qz_metadata.opinfo.opuin + ",atnick:'" + _nickname + "'}" ?>
		<?cs /if?>
		<?cs call:data_comments_inputbox(share_config, share_param, qz_metadata.meta.userid, "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareaddcomment", "utf-8")?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_blog ?>
		<?cs set:blog_param = qz_metadata.orgdata.uin + "," + qz_metadata.orgdata.mkey + ",0" ?>

		<?cs call:get_userWho_platform(qz_metadata.opinfo.platformid, qz_metadata.opinfo.platformsubid)?>
		<?cs if:get_userWho_platform.ret == USER_PLATFORM_WHO_PY ?>
			<?cs set:blog_config = "{config:'1|1|0|0|0|0|0', atuin:'" 
				+ qz_metadata.opinfo.xyuin + ",atnick:'" + _xynick + "',atwho:2}" ?>
		<?cs else ?>
			<?cs set:blog_config = "{config:'1|1|0|0|0|0|0', atuin:" 
				+ qz_metadata.opinfo.opuin + ",atnick:'" + _nickname + "'}" ?>
		<?cs /if?>
		<?cs call:data_comments_inputbox(blog_config, blog_param, qz_metadata.meta.userid, "http://b.qzone.qq.com/cgi-bin/blognew/blog_add_comment", "GB")?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_photo ?>

		<?cs if:qz_metadata.orgdata.extendinfo.subtype == 1 ?>
			<?cs set:photo_param = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.itemdata[0].albumid 
				+ "&forumindex=0&lloc=" + qz_metadata.orgdata.itemdata[0].itemid + "&refer=qzone&sloc=" 
				+ qz_metadata.orgdata.itemdata[0].itemid + "&reqfrom=601" ?>
		<?cs else ?>
			<?cs set:photo_param = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.itemdata[0].albumid
				+ "&forumindex=0&refer=qzone&sloc=" + qz_metadata.orgdata.itemdata[0].itemid + "&reqfrom=801" ?>
		<?cs /if?>

		<?cs call:get_userWho_platform(qz_metadata.opinfo.platformid, qz_metadata.opinfo.platformsubid)?>
		<?cs if:get_userWho_platform.ret == USER_PLATFORM_WHO_PY ?>
			<?cs set:photo_config = "{config:'1|1|0|0|0|0|0', atuin:'" 
				+ qz_metadata.opinfo.xyuin + ",atnick:'" + _xynick + "',atwho:2}" ?>
		<?cs else ?>
			<?cs set:photo_config = "{config:'1|1|0|0|0|0|0', atuin:" 
				+ qz_metadata.opinfo.opuin + ",atnick:'" + _nickname + "'}" ?>
		<?cs /if?>
		<?cs call:data_comments_inputbox(photo_config, photo_param, qz_metadata.meta.userid, "http://photo.qq.com/cgi-bin/common/cgi_add_piccomment", "GB")?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_app  ?>
		<?cs call:_cover_psv_commentReply_param(qz_metadata.opinfo.t2body)?>
		<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", _app_psv_commentReply_param.ret, qz_metadata.meta.userid, 
										"http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_qun  ?>
		<?cs call:qfv("meta.subappid", qz_metadata.orgdata.extendinfo.appid)?>
		<?cs set:qun_param = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.itemdata[0].albumid 
			+ "&forumindex=0&lloc=" + qz_metadata.orgdata.itemdata[0].itemid + "&refer=qzone&sloc=" 
			+ qz_metadata.orgdata.itemdata[0].itemid + "&reqfrom=601" ?>
		<?cs set:qun_config = "{config:'1|1|0|0|0|0|0', atuin:" 
			+ qz_metadata.opinfo.opuin + ",atnick:'" + _nickname + "'}" ?>
		<?cs call:data_comments_inputbox(qun_config, qun_param, qz_metadata.meta.userid, "http://u.photo.qq.com/cgi-bin/upp/qun_add_uc_cmt", "utf-8")?>

	<?cs /if?>
<?cs /if?>
