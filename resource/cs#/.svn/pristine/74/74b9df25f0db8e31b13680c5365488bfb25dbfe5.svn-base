<?cs ####
	/* 甩一甩内容区*/
?>

<?cs def:data_shake_contentbox()?>
	<?cs call:i()?>
	<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_SMALL_MODE , "") ?>
	<?cs if:qz_metadata.orgdata.extendinfo.appid == SHAKE_SUBAPP_BLOG?>
		<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, "")?>
		<?cs call:data_textTitle_tipTxt(" 发表日志 ")?>
		<?cs if:subcount(qz_metadata.orgdata.title) > 0?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl)?>
		<?cs /if?>
		<?cs call:data_content_text(qz_metadata.orgdata.content) ?>
		<?cs if:subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs set:blog_jump_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/blog/" + qz_metadata.orgdata.mkey ?>
			<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], blog_jump_url,"","" ) ?>
		<?cs /if?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == SHAKE_SUBAPP_PHOTO?>
		<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, "")?>
		<?cs call:data_textTitle_tipTxt(" 上传照片到相册 ")?>
		<?cs if:subcount(qz_metadata.orgdata.title) > 0?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl)?>
		<?cs /if ?>	
		<?cs if:subcount(qz_metadata.orgdata.itemdata) > 1 ?>
			<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE_SMALL , "") ?>
		<?cs else ?>
			<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_DEFAULT , "") ?>
		<?cs /if ?>
		<?cs if:subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs set:pic_count = 3 ?>
			<?cs if:subcount(qz_metadata.orgdata.itemdata) < 3 ?>
				<?cs set:pic_count = subcount(qz_metadata.orgdata.itemdata) ?>
			<?cs /if?>
			<?cs loop:j=0, pic_count-1, 1?>
				<?cs set:photo_param = qz_metadata.orgdata.uin + "|"
					+ qz_metadata.orgdata.itemdata[j].albumid + "|"
					+ qz_metadata.orgdata.itemdata[j].itemid + "|"
					+ qz_metadata.orgdata.itemdata[j].picinfo[0].url + "|"
					+ qz_metadata.orgdata.ctime ?>
				<?cs #甩相册用新版浮层 ?>
				<?cs call:data_cntmedia_pic_popup_v2(j, qz_metadata.orgdata.itemdata[j], qz_metadata.orgdata.itemdata[0].albumid, 
					qz_metadata.orgdata.itemdata[j].itemid, qz_metadata.orgdata.extendinfo.appid) ?>
			<?cs /loop?>
		<?cs /if?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == SHAKE_SUBAPP_SHARE?>
		<?cs call:get_share_uin_and_shareid() ?>

		<?cs if:qz_metadata.orgdata.extendinfo.sharetype != SHAKE_SHARE_TYPE_WEIBO?>
			<?cs if:qz_metadata.orgdata.extendinfo.sharetype == SHARE_SHARE_TYPE_VIDEO?>
				<?cs set:jump_url = "http://www.urlshare.cn/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=" 
					+ uri_encode(qz_metadata.orgdata.srcurl) + "&shareid="  + get_share_uin_and_shareid.shareid ?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHAKE_SHARE_TYPE_BLOG?>
				<?cs set:jump_url = "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_blogdetail?blogurl=" 
					+ uri_encode(qz_metadata.orgdata.srcurl) + "&shareuin=" 
					+ get_share_uin_and_shareid.uin + "&itemid=" 
					+ get_share_uin_and_shareid.shareid + "&spaceuin=&cginame=&isfriend=1" ?>
			<?cs else ?>
				<?cs set:jump_url = qz_metadata.orgdata.srcurl ?>
			<?cs /if?>
			
			<?cs if:qz_metadata.orgdata.extendinfo.sharetype == SHARE_SHARE_TYPE_URL || qz_metadata.orgdata.extendinfo.sharetype == SHARE_SHARE_TYPE_APPURL ?>
				<?cs if:subcount(qz_metadata.orgdata.itemdata) > 0?>
					<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_SMALL_V8_MODE , "") ?>
					<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], qz_metadata.orgdata.srcurl,"","") ?>
					<?cs else ?>
					<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_NOIMG , "") ?>
				<?cs /if ?>
			<?cs /if?>
			
			<?cs if:subcount(qz_metadata.orgdata.title) > 0?>
				<?cs if:qz_metadata.orgdata.extendinfo.sharetype == SHAKE_SHARE_TYPE_DRESS?>
					<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content + "装扮", jump_url)?>
				<?cs else ?>
					<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, jump_url)?>
				<?cs /if?>
			<?cs /if?>
		<?cs /if?>
		
		<?cs call:data_content_text(qz_metadata.orgdata.content) ?>

		<?cs if:subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs if:qz_metadata.orgdata.extendinfo.sharetype == SHARE_SHARE_TYPE_VIDEO ?>
				<?cs set:play_param = "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareget_onedetail?uin=" + get_share_uin_and_shareid.uin
					+ "&itemid=" + get_share_uin_and_shareid.shareid + "&cginame=popup&spaceuin=0" ?>
				<?cs call:data_cntmedia_video_show(0, qz_metadata.orgdata.itemdata[0], "", play_param, "") ?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHARE_SHARE_TYPE_ALBUM || qz_metadata.orgdata.extendinfo.sharetype == SHARE_SHARE_TYPE_PIC ?>
				<?cs set:popup_param = qz_metadata.orgdata.extendinfo.sharetype + "|"
					+ "0" + "|"
					+ get_share_uin_and_shareid.uin + "|"
					+ get_share_uin_and_shareid.shareid + "|"
					+ qz_metadata.orgdata.mkey + "|"
					+ qz_metadata.orgdata.platformid + "|"
					+ qz_metadata.orgdata.uin ?>
				<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[0], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
				<?cs call:data_cntmedia_pic_add_popup_attr("env","subappid:"+qz_metadata.orgdata.extendinfo.appid) ?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHAKE_SHARE_TYPE_BLOG ?>
				<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], qz_metadata.orgdata.srcurl,"","") ?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHAKE_SHARE_TYPE_PIC_EXT ?>
				<?cs set:popup_param = qz_metadata.orgdata.extendinfo.sharetype + "|"
					+ "0" + "|"
					+ get_share_uin_and_shareid.uin + "|"
					+ get_share_uin_and_shareid.shareid + "|"
					+ qz_metadata.orgdata.mkey + "|"
					+ qz_metadata.orgdata.platformid + "|"
					+ qz_metadata.orgdata.uin ?>
				<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[0], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
				<?cs call:data_cntmedia_pic_add_popup_attr("env","subappid:"+qz_metadata.orgdata.extendinfo.appid) ?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHAKE_SHARE_TYPE_DRESS ?>
				<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], qz_metadata.orgdata.srcurl,"","") ?>
			<?cs /if?>
		<?cs /if?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == SHAKE_SUBAPP_MOOD?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_DEFAULT , "") ?>
		<?cs call:get_share_uin_and_shareid() ?>

		<?cs if:qz_metadata.orgdata.extendinfo.subtype == SHAKE_MOOD_SUBTYPE_TIMELINE ?>
			<?cs call:data_textTitle_tipTxt("添加时光轴记录")?>
				<?cs set:jump_url = "http://user.qzone.qq.com/" + get_share_uin_and_shareid.uin
					  + "/main?mode=gfp_timeline#eid=s" + get_share_uin_and_shareid.shareid + "&time=" + qz_metadata.orgdata.ctime?>
				<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.tl_time, jump_url)?>

				<?cs if:subcount(qz_metadata.orgdata.extendinfo.uininfo.uin_tl) > 0 ?>
					<?cs call:data_textTitle_tipTxt("，和")?>
					<?cs set:jump_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.extendinfo.uininfo.uin_tl[0] + "/main?mode=gfp_timeline" ?>
					<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.uininfo.nick_tl[0], jump_url) ?>

					<?cs loop:j=1, subcount(qz_metadata.orgdata.extendinfo.uininfo.uin_tl) - 1, 1?>
						<?cs call:data_textTitle_tipTxt("、")?>
						<?cs set:jump_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.extendinfo.uininfo.uin_tl[j] + "/main?mode=gfp_timeline" ?>
						<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.uininfo.nick_tl[j], jump_url) ?>
					<?cs /loop?>
					<?cs call:data_textTitle_tipTxt("一起：")?>
				<?cs else ?>
					<?cs call:data_textTitle_tipTxt("：")?>
				<?cs /if?>
		<?cs /if?>

		<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
		<?cs call:data_textTitle_tipTxt("：")?>
		<?cs call:data_textTitle_rich(qz_metadata.orgdata.content) ?>
		<?cs if:qz_metadata.orgdata.extendinfo.richtype == SHAKE_RICHTYPE_IMG && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs set:popup_param = get_share_uin_and_shareid.shareid + "|" + get_share_uin_and_shareid.uin + "|" + "0" ?>
			<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[0], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
			<?cs call:data_cntmedia_pic_add_popup_attr("env","subappid:"+ qz_metadata.orgdata.extendinfo.appid) ?>
		<?cs elif:qz_metadata.orgdata.extendinfo.richtype == SHAKE_RICHTYPE_VIDEO && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs call:data_cntmedia_video_show(0, qz_metadata.orgdata.itemdata[0], qz_metadata.orgdata.extendinfo.richtype, "/qzone/app/mood/richinfo_view.html", "") ?>
		<?cs elif:subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs set:popup_param = get_share_uin_and_shareid.shareid + "|" + get_share_uin_and_shareid.uin + "|" + 0 ?>
			<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[0], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
			<?cs call:data_cntmedia_pic_add_popup_attr("env","subappid:"+qz_metadata.orgdata.extendinfo.appid) ?>
		<?cs /if?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == SHAKE_SUBAPP_COMM?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE_SMALL , "") ?>
		<?cs if:subcount(qz_metadata.orgdata.title) > 0?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl)?>
		<?cs /if?>
		<?cs call:data_content_text(qz_metadata.orgdata.content) ?>
		<?cs if:subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], qz_metadata.orgdata.srcurl,"","" ) ?>
		<?cs /if?> 
	<?cs /if?>

<?cs /def?>
