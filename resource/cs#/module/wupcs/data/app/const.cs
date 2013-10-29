
<?cs #/*分享内容的类型列表*/#?>
<?cs set:APP_subtype_invite = 2?>
<?cs set:APP_subtype_addapp = 3?>
<?cs set:APP_subtype_activate = 5?>
<?cs set:APP_subtype_video = 6?>
<?cs set:APP_subtype_share = 7?>
<?cs set:APP_subtype_game = 8?>
<?cs set:APP_subtype_invite_with_mqzone = 11?><?cs #/*和触屏版打通的应用邀请feeds,和APP_subtype_invite基本一样,少些组件而已*/#?>
<?cs set:APP_subtype_mobile_cover = 12?><?cs #/*用户在手机上换了背景皮肤在PC上展示的feeds*/#?>
<?cs set:SCOPE_FRIENDSHIP_ME_TO_FRIEND=1?>  <?cs #/*friendship场景标识:我对好友的操作*/#?>
<?cs set:SCOPE_FRIENDSHIP_FRIEND_TO_ME=2?>  <?cs #/*friendship场景标识:好友对我的操作*/#?>

<?cs ####
	/**
	 *生成回复参数
	 *也会在回复被动feeds中用到
	 *@param {vt2body} t2body 当前评论的索引
	 */
?>
<?cs def:_app_psv_commentReply_param(t2body)?>
	<?cs set:_param = qz_metadata.meta.appid + "''"
						+ qz_metadata.orgdata.uin + "''"
						+ qz_metadata.orgdata.mkey + "''"
						+ t2body.seq + "''"
						+ t2body.uin + "''"
						+ qz_metadata.feedtype + "''0''"
						+ qz_metadata.orgdata.subtype ?>
	<?cs set:_app_psv_commentReply_param.ret = _param?>
<?cs /def?>

<?cs ####
	/**
	 *生成评论参数
	 *也会在评论被动feeds中用到
	 */
?>
<?cs def:_app_psv_comment_param()?>

	<?cs set:_param = qz_metadata.meta.appid + "''"
				+ qz_metadata.orgdata.uin + "''"
				+ qz_metadata.orgdata.mkey + "''-1''0''"
				+ qz_metadata.feedtype + "''0''"
				+ qz_metadata.orgdata.subtype ?>

	<?cs set:_app_psv_comment_param.ret = _param?>
<?cs /def?>
