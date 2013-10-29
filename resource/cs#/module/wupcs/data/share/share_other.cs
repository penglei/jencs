<?cs ####
	/*除标题和contentbox外的数据填充区*/
?>

<?cs def:share_music_setbg_param()?>
	<?cs set:_param = "songid:" + qz_metadata.orgdata.mkey + "|songtype:3"?>
	<?cs set:share_music_setbg_param.ret = _param?>
<?cs /def?>

<?cs def:share_opr_share_action()?>
	<?cs set:_param = "http://imgcache.qq.com/qzone/app/qzshare/popup.html#uin=" + qz_metadata.orgdata.uin + "&itemid=" + qz_metadata.orgdata.mkey?>
	<?cs set:share_opr_share_action.ret = _param?>
<?cs /def?>

<?cs def:data_share_operate()?>
	<?cs call:i()?>
	<?cs ####if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_collect || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_list?>
		<?cs #/*设为背景音乐*/#?>
		<?cs ####call:share_music_setbg_param()?>
		<?cs ####call:data_opr_txtPopup(0, "设为背景音乐", "设为背景音乐", "http://music.qq.com/musicbox/like/mylove_add2bgproxy.html"
			, share_music_setbg_param.ret, 1, 360, 240,"share.opr.setBgMusic")?>
		<?cs ####if:g_isV8?>
			<?cs #call:data_popup_add_attr(data_opr_txtPopup.ret, "mr", 0)?>
			<?cs ####call:data_popup_add_attr(data_opr_txtPopup.ret + ".action", "mr", 0)?>
		<?cs ####/if?>
	<?cs ####/if?>

	<?cs if:qz_metadata.orgdata.extendinfo.purviewbit == 1 ?>
		<?cs call:data_opr_txtPopup(0, qz_metadata.orgdata.extendinfo.app_action, "分享", 
			qz_metadata.orgdata.extendinfo.app_url, "", 1, 372, 110,"share.opr.doshare")?>
	<?cs else ?>
		<?cs if:qz_metadata.orgdata.extendinfo.app_action ?>
			<?cs call:data_opr_url(0, qz_metadata.orgdata.extendinfo.app_action, qz_metadata.orgdata.extendinfo.app_url,"share.other") ?>
		<?cs /if ?>
	<?cs /if?>

	<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_space_dress?>
		<?cs call:data_opr_url(0, "我也换装扮", qz_metadata.orgdata.source_url,"share.changeToo")?>
		<?cs if:g_isV8?>
			<?cs call:data_addClassName(data_opr_url.ret.path, "item")?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>
