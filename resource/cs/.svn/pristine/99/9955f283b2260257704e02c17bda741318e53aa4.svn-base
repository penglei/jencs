<?cs ####
	/*个人档内容区*/
?>

<?cs def:data_profile_contentbox()?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_RELATEPSV?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_DEFAULT , "") ?>
		<?cs if:qz_metadata.orgdata.subtype == PROFILE_TYPE_UPLOAD_AVATAR?>
			<?cs call:data_cntmedia_pic(0,qz_metadata.orgdata.extendinfo.profile_avatar,"") ?>
		<?cs elif:qz_metadata.orgdata.subtype == PROFILE_TYPE_UPDATE_COVER?>
			<?cs if:subcount(qz_metadata.orgdata.itemdata)>1?>
				<?cs set:_cover_popup_param = ""?>
				<?cs if:subcount(qz_metadata.orgdata.itemdata[0].picinfo)>1?>
					<?cs set:_cover_popup_param = qz_metadata.orgdata.itemdata[0].picinfo[0].url?>
				<?cs /if?>
				<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[0], _cover_popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
			<?cs /if?>
		<?cs else ?>
			<?cs set:_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
			<?cs loop:j=0, _end, 1?>
				<?cs if:qz_metadata.orgdata.itemdata[j].itemid == PROFILE_ID_MARRIAGE?>
					<?cs call:data_textTitle_tipTxt(qz_metadata.orgdata.itemdata[j].extendinfo.org_data)?>
				<?cs elif: qz_metadata.orgdata.itemdata[j].itemid == PROFILE_ID_ADDRESS?>
					<?cs call:data_textTitle_tipTxt("从")?>
					<?cs call:data_textTitle_tipTxt(qz_metadata.orgdata.itemdata[j].extendinfo.org_data)?>
					<?cs call:data_textTitle_tipTxt("搬到")?>
					<?cs call:data_textTitle_tipTxt(qz_metadata.orgdata.itemdata[j].extendinfo.dst_data)?>
				<?cs elif: qz_metadata.orgdata.itemdata[j].itemid == PROFILE_ID_HOME?>
					<?cs call:data_textTitle_tipTxt("家乡是")?>
					<?cs call:data_textTitle_tipTxt(qz_metadata.orgdata.itemdata[j].extendinfo.org_data)?>
				<?cs /if?>
			<?cs /loop?>
		<?cs /if ?>
	<?cs else?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT,G_IMG_SMART , "") ?>
		<?cs if:qz_metadata.orgdata.subtype == PROFILE_TYPE_UPLOAD_AVATAR?>
			<?cs call:data_cntTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
			<?cs call:data_cntTitle_tipTxt(" 更换了空间头像")?>
			
			<?cs call:data_cntmedia_pic(0,qz_metadata.orgdata.extendinfo.profile_avatar,"") ?>
		<?cs elif:qz_metadata.orgdata.subtype == PROFILE_TYPE_UPDATE_COVER?>
			<?cs call:data_cntTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
			<?cs call:data_cntTitle_tipTxt("我刚刚更新了手机QQ空间的背景，快来看看吧！客户端官网链接：")?>
			<?cs call:data_cntTitle_url("http://z.qzone.com","http://z.qzone.com")?>
			
			<?cs if:subcount(qz_metadata.orgdata.itemdata)>1?>
				<?cs set:_cover_popup_param = ""?>
				<?cs if:subcount(qz_metadata.orgdata.itemdata[0].picinfo)>1?>
					<?cs set:_cover_popup_param = qz_metadata.orgdata.itemdata[0].picinfo[0].url?>
				<?cs /if?>
				<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[0], _cover_popup_param, "/qzone/photo/zone/icenter_popup.html", "")?>
			<?cs /if?>
		<?cs else ?>
			<?cs set:_profile_jump_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/profile/"?>
			<?cs call:data_cntTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname) ?>
			<?cs call:data_cntTitle_tipTxt(" 更新了")?>
			<?cs call:data_cntTitle_url("个人资料", _profile_jump_url)?>
			
			<?cs set:_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
			<?cs loop:j=0, _end, 1?>
				<?cs if:qz_metadata.orgdata.itemdata[j].itemid == PROFILE_ID_MARRIAGE?>
					<?cs call:data_textTitle_tipTxt(qz_metadata.orgdata.itemdata[j].extendinfo.org_data)?>
				<?cs elif: qz_metadata.orgdata.itemdata[j].itemid == PROFILE_ID_ADDRESS?>
					<?cs call:data_textTitle_tipTxt("从")?>
					<?cs call:data_textTitle_tipTxt(qz_metadata.orgdata.itemdata[j].extendinfo.org_data)?>
					<?cs call:data_textTitle_tipTxt("搬到")?>
					<?cs call:data_textTitle_tipTxt(qz_metadata.orgdata.itemdata[j].extendinfo.dst_data)?>
				<?cs elif: qz_metadata.orgdata.itemdata[j].itemid == PROFILE_ID_HOME?>
					<?cs call:data_textTitle_tipTxt("家乡是")?>
					<?cs call:data_textTitle_tipTxt(qz_metadata.orgdata.itemdata[j].extendinfo.org_data)?>
				<?cs /if?>
			<?cs /loop?>
		<?cs /if ?>
	<?cs /if ?>
<?cs /def?>