<?cs ####
	/*分享标题区*/
?>

<?cs def:data_share_subtitle()?>
	<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
		<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT_NOTIFYPSV?>
			<?cs set:rely_pos = 1 ?>
		<?cs else ?>
			<?cs set:rely_pos = 0 ?>
		<?cs /if?>
		<?cs set:name=subcount(qz_metadata.relybody) ?>
		<?cs #:设置音乐feeds不用转了此条信息 ?>

		<?cs if:qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music
			&&	qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_collect 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_list 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_mv 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_songlist 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_album 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_listen_song 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_love_song 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_order_song 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_oder_dir 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_follow_dir 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_update_dir 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_love_mv 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_order_album 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_type_ordersong 
			&&  qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_type_newdir ?>
			<?cs if:subcount(qz_metadata.relybody) > rely_pos ?>
				<?cs call:get_userWho_platform(qz_metadata.relybody[rely_pos].platformid, qz_metadata.relybody[rely_pos].platformsubid)?>
				<?cs call:data_init_cntTitle() ?>
				<?cs call:data_cntTitle_nick(qz_metadata.relybody[rely_pos].uin, get_userWho_platform.ret, qz_metadata.relybody[rely_pos].nickname) ?>

				<?cs if:subcount(qz_metadata.relybody[rely_pos].msg) > 0 && 
					(subcount(qz_metadata.relybody[rely_pos].msg) > 1 || qz_metadata.relybody[rely_pos].msg[0].type != 0 || string.length(qz_metadata.relybody[rely_pos].msg[0].content) > 0 ) ?>
					<?cs call:data_cntTitle_tipTxt("转：") ?>
					<?cs call:data_cntTitle_rich(qz_metadata.relybody[rely_pos].msg) ?>
				<?cs else ?>
						<?cs call:data_cntTitle_tipTxt("转了此条信息") ?>
				<?cs /if?>
			<?cs /if?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs def:data_share_contentbox()?>
	<?cs call:i()?>
	<?cs call:get_tuin_and_tid()?>

	<?cs if:qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_weibo 
		&& qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_list
		&& qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_collect
		&& qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_space_dress
		&& qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music
		&& qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_songlist
		&& qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_album
		&& qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_follow_dir
		&& qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_update_dir
		&& qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_music_type_newdir ?>


		<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_vedio 
			|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_url
			|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_news
			|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_urlsif ?>

			<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl) ?>

		<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_blog ?>

			<?cs call:get_userWho_platform(qz_metadata.orgdata.platformid, qz_metadata.orgdata.platformsubid)?>
			<?cs if:get_userWho_platform.ret == USER_PLATFORM_WHO_PY && string.length(qz_metadata.orgdata.xyuin) > 0 ?>
				<?cs set:blog_param = "http://baseapp.pengyou.com/" + qz_metadata.orgdata.xyuin 
					+ "/blog/" + qz_metadata.orgdata.mkey ?>
			<?cs else ?>
				<?cs set:blog_param = "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_blogdetail?blogurl=" 
					+ uri_encode(qz_metadata.orgdata.srcurl)
					+ "&shareuin=" + get_tuin_and_tid.uin 
					+ "&itemid=" + get_tuin_and_tid.tid 
					+ "&spaceuin=&cginame=&isfriend=1" ?>
			<?cs /if?>

			<?cs if:qz_metadata.orgdata.action == UC_API_ACTION_RUBLISHED ?>
				<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname) ?>
				<?cs call:data_textTitle_tipTxt("发表日志") ?>
				<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, blog_param) ?>
			<?cs elif:qz_metadata.orgdata.action == UC_API_ACTION_FORWARD ?>
				<?cs call:data_textTitle_nick(qz_metadata.orgdata.extendinfo.shareduin, USER_PLATFORM_WHO_QZONE, "") ?>
				<?cs call:data_textTitle_tipTxt("转载") ?>
				<?cs call:data_textTitle_nick(qz_metadata.orgdata.extendinfo.oriauthor, USER_PLATFORM_WHO_QZONE, "") ?>
				<?cs call:data_textTitle_tipTxt("的日志") ?>
				<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, blog_param) ?>
			<?cs /if?>

			<?cs ##call:data_textTitle_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl) ?>

		<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_album 
			|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_picture ?>

			<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname) ?>
			<?cs call:data_textTitle_tipTxt("上传照片到相册") ?>

			<?cs call:get_userWho_platform(qz_metadata.orgdata.platformid, qz_metadata.orgdata.platformsubid)?>
			<?cs if:get_userWho_platform.ret == USER_PLATFORM_WHO_PY && string.length(qz_metadata.orgdata.xyuin) > 0 ?>
				<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_album ?>
					<?cs set:photo_url = "http://baseapp.pengyou.com/" + qz_metadata.orgdata.xyuin + "/photo/" + qz_metadata.orgdata.extendinfo.albumid ?>
				<?cs else ?>
					<?cs set:photo_url = "http://baseapp.pengyou.com/" + qz_metadata.orgdata.xyuin + "/photo/" 
						+ qz_metadata.orgdata.extendinfo.albumid + "/" + qz_metadata.orgdata.mkey ?>
				<?cs /if?>

				<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, photo_url) ?>
				<?cs ##call:data_textTitle_url(qz_metadata.orgdata.title.0.content, photo_url) ?>
			<?cs else ?>
				<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl) ?>
			<?cs /if?>

		<?cs else ?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl) ?>
		<?cs /if?>
	<?cs /if?>
	<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_url && subcount(qz_metadata.orgdata.content) == 1 ?>
		 <?cs call:data_con_url(_cnt_summary_path + "con."+data_content_i,qz_metadata.orgdata.content.0.content, qz_metadata.orgdata.srcurl, "normal", 0) ?>
		 <?cs call:data_content_i++() ?>
	<?cs else?>
		<?cs call:data_content_text(qz_metadata.orgdata.content) ?>
	<?cs /if?>
	<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_appurl 
		&& string.length(qz_metadata.orgdata.extendinfo.app_url) > 0 && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_SMALL_V8_MODE , "") ?>
			<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0].picinfo[0].url, qz_metadata.orgdata.srcurl, "", "") ?>
	<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_songlist 
		|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_album
		|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_follow_dir
		|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_update_dir
		|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_type_newdir ?>


			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_songlist ?>
				<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ACT?>
					<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
					<?cs call:data_textTitle_tipTxt("分享歌单")?>
					<?cs call:data_textTitle_rich(qz_metadata.orgdata.desc) ?>
				<?cs /if?>
				<?cs set:music_param = "{action:5,shareuin:'" + get_tuin_and_tid.uin + "',uin:'" + qz_metadata.orgdata.extendinfo.uHostUin 
					+ "',listid:'" + qz_metadata.orgdata.extendinfo.iPlaylistId +"',flashid:'flash_" + get_tuin_and_tid.tid
					+ "',playbtn:'controls_" + get_tuin_and_tid.tid + "',redarea:'player_" + get_tuin_and_tid.tid + "'}" ?>
				
				<?cs call:data_music(qz_metadata.orgdata.extendinfo.sPlaylistName, get_tuin_and_tid.tid, 1, 
					"/music/qzone/music_ic.js", qz_metadata.orgdata.itemdata[0].picinfo[0].url, 
					qz_metadata.orgdata.itemdata[0].duration, music_param, "") ?>

			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_update_dir ?>
				<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ACT?>
					<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
					<?cs call:data_textTitle_tipTxt("在")?>
					<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.sPlaylistName, qz_metadata.orgdata.extendinfo.sPlaylistUrl)?>
					<?cs call:data_textTitle_tipTxt("添加音乐")?>
					<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl)?>
				<?cs /if?>
				<?cs if:!qz_metadata.orgdata.extendinfo.iQuality ?>
					<?cs set:qz_metadata.orgdata.extendinfo.iQuality=0 ?>
				<?cs /if?>
				<?cs set:music_param = "{action:3,shareuin:'" + get_tuin_and_tid.uin + "',songid:" + qz_metadata.orgdata.mkey 
					+ ",flashid:'flash_" + get_tuin_and_tid.tid + "',playbtn:'controls_" + get_tuin_and_tid.tid 
					+ "',redarea:'player_" + get_tuin_and_tid.tid + "',quality:" + qz_metadata.orgdata.extendinfo.iQuality + "}" ?>

				<?cs call:data_music(qz_metadata.orgdata.title, get_tuin_and_tid.tid, 1, 
					qz_metadata.orgdata.srcurl, qz_metadata.orgdata.itemdata[0].picinfo[0].url, 
					qz_metadata.orgdata.itemdata[0].duration, music_param, "") ?>

			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_album ?>
				<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ACT?>
					<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
					<?cs call:data_textTitle_tipTxt("分享专辑")?>
					<?cs call:data_textTitle_rich(qz_metadata.orgdata.desc) ?>
				<?cs /if?>

				<?cs set:music_param = "{action:5,shareuin:'" + get_tuin_and_tid.uin + "',uin:'" + qz_metadata.orgdata.extendinfo.uHostUin 
					+ "',listid:'" + qz_metadata.orgdata.itemdata[0].albumid +"|1',flashid:'flash_" + get_tuin_and_tid.tid
					+ "',playbtn:'controls_" + get_tuin_and_tid.tid + "',redarea:'player_" + get_tuin_and_tid.tid + "'}" ?>

				<?cs call:data_music(qz_metadata.orgdata.itemdata[0].albumname, get_tuin_and_tid.tid, 1, 
					qz_metadata.orgdata.itemdata[0].action, qz_metadata.orgdata.itemdata[0].picinfo[0].url, 
					qz_metadata.orgdata.itemdata[0].duration, music_param, "") ?>

			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_album ?>
				<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ACT?>
					<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
					<?cs call:data_textTitle_tipTxt("分享专辑")?>
					<?cs call:data_textTitle_rich(qz_metadata.orgdata.desc) ?>
				<?cs /if?>

				<?cs set:music_param = "{action:5,shareuin:'" + get_tuin_and_tid.uin + "',uin:'" + qz_metadata.orgdata.extendinfo.uHostUin 
					+ "',listid:'" + qz_metadata.orgdata.itemdata[0].albumid +"|1',flashid:'flash_" + get_tuin_and_tid.tid
					+ "',playbtn:'controls_" + get_tuin_and_tid.tid + "',redarea:'player_" + get_tuin_and_tid.tid + "'}" ?>

				<?cs call:data_music(qz_metadata.orgdata.itemdata[0].albumname, get_tuin_and_tid.tid, 1, 
					qz_metadata.orgdata.itemdata[0].action, qz_metadata.orgdata.itemdata[0].picinfo[0].url, 
					qz_metadata.orgdata.itemdata[0].duration, music_param, "") ?>

			<?cs else ?>

				<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ACT?>
					<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>

				<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_follow_dir ?>
					<?cs call:data_textTitle_tipTxt("订阅歌单")?>
				<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_type_newdir ?>
					<?cs call:data_textTitle_tipTxt("创建歌单")?>
				<?cs /if?>

				<?cs set:music_param = "{action:5,shareuin:'" + get_tuin_and_tid.uin + "',uin:'" + qz_metadata.orgdata.extendinfo.uHostUin 
					+ "',listid:'" + qz_metadata.orgdata.extendinfo.iPlaylistId +"',flashid:'flash_" + get_tuin_and_tid.tid
					+ "',playbtn:'controls_" + get_tuin_and_tid.tid + "',redarea:'player_" + get_tuin_and_tid.tid + "'}" ?>

				<?cs call:data_music(qz_metadata.orgdata.extendinfo.sPlaylistName, get_tuin_and_tid.tid, 1, 
					qz_metadata.orgdata.extendinfo.sPlaylistUrl, qz_metadata.orgdata.itemdata[0].picinfo[0].url, 
					qz_metadata.orgdata.itemdata[0].duration, music_param, "") ?>

			<?cs /if?>
	<?cs #:音乐  ?>
	<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music 
		|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_list
		|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_collect ?>
		<?cs #:音乐走了新的模版 ?>

		<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ACT?>
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_list ?>
				<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
				<?cs call:data_textTitle_tipTxt("设置了背景音乐:")?>
				<?cs #分享音乐列表要单独处理一下?>
			<?cs elif:(qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_collect) ?>
				<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
				<?cs call:data_textTitle_tipTxt("收藏音乐:")?>
			<?cs /if?>
		<?cs /if?>

		<?cs call:data_music_attr("type", "bgmusic")?>
		<?cs if:!qz_metadata.orgdata.extendinfo.iQuality ?>
			<?cs set:qz_metadata.orgdata.extendinfo.iQuality=0 ?>
		<?cs /if?>
		<?cs set:music_param = "{action:3,shareuin:'" + get_tuin_and_tid.uin + "',songid:" + qz_metadata.orgdata.mkey 
			+ ",flashid:'flash_" + get_tuin_and_tid.tid + "',playbtn:'controls_" + get_tuin_and_tid.tid 
			+ "',redarea:'player_" + get_tuin_and_tid.tid + "',quality:" + qz_metadata.orgdata.extendinfo.iQuality + "}" ?>

		<?cs call:data_music(qz_metadata.orgdata.title.0.content, get_tuin_and_tid.tid, 1,
			qz_metadata.orgdata.srcurl, qz_metadata.orgdata.itemdata[0].picinfo[0].url, 
			qz_metadata.orgdata.itemdata[0].duration, music_param, "") ?>

	<?cs else ?>
		<?cs #:空间装扮  ?>
		<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_space_dress ?>
					<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
					<?cs call:data_textTitle_tipTxt("保存")?>
					<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl)?>
					<?cs call:data_textTitle_tipTxt("装扮")?>
		<?cs /if?>

		<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
			<?cs #:v6 被动分享视频feeds ?>
			<?cs #:排除掉宽度>400 主要是为了不让视频来源是腾讯视频的feeds走这里的逻辑 ?>
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_vedio && !g_isV8 && qz_metadata.orgdata.itemdata[0].width<400 ?>
				<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_SMALL_V8_MODE , "") ?>
			<?cs else ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_SMALL_MODE , "") ?>
			<?cs /if ?>
		<?cs else ?>
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_space_dress ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_GPLUS_MODE , "") ?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_blog || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_picture || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_album ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_GRID_MODE , "") ?>
			<?cs elif: qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_vedio ?>
				<?cs if:g_isV8 || qz_metadata.orgdata.itemdata[0].width>=400 ?><?cs #:排除掉宽度>400 主要是为了不让视频来源是腾讯视频的feeds走这里的逻辑 ?>
					<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_GRID_MODE , "") ?>
				<?cs else ?>
					<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_SMALL_V8_MODE , "") ?>
				<?cs /if ?>
			<?cs else ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_SMALL_MODE , "") ?>
			<?cs /if?>
		<?cs /if?>
		
		<?cs set:_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
		<?cs #:视频  ?>
		<?cs if:qz_metadata.orgdata.mediatype == UC_MEDIA_TYPE_VEDIO && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs set:play_param = "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareget_onedetail?uin=" + get_tuin_and_tid.uin
				+ "&itemid=" + get_tuin_and_tid.tid + "&cginame=popup&spaceuin=0" ?>
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_douding?> 
				<?cs #分享类型为文档的话，把视频图片type属性设置为3 ?>
				<?cs call:data_cntmedia_video_show(0, qz_metadata.orgdata.itemdata[0], "", play_param, "img_doc") ?>
			<?cs else ?>
				<?cs call:data_cntmedia_video_show(0, qz_metadata.orgdata.itemdata[0], "", play_param, "") ?>
			<?cs /if?>
		<?cs #:图片  ?>	
		<?cs elif:qz_metadata.orgdata.mediatype == UC_MEDIA_TYPE_PIC && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs call:data_extendinfo_picnum(qz_metadata.orgdata.itemcount)?>
			<?cs #:分享第三方页面走了新的模版 ?>
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_url 
				|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_appurl
				|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_weibo ?>
				<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_SMALL_V8_MODE , "") ?>
			<?cs /if ?>

			<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
				<?cs set:pic_idx = 0 ?>
				<?cs if:(qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV)
					&& string.length(qz_metadata.opinfo.t2body.extendinfo.pic_index) > 0 ?>
					<?cs set:pic_idx = qz_metadata.opinfo.t2body.extendinfo.pic_index ?>
					<?cs if:pic_idx < 0 ?>
						<?cs set:pic_idx = 0 ?>
					<?cs /if ?>
				<?cs /if?>

				<?cs if:qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_picture && qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_album ?>

					<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[pic_idx], qz_metadata.orgdata.srcurl,"","") ?>
				<?cs else ?>
					<?cs set:popup_param = qz_metadata.orgdata.extendinfo.share_subtype + "|"
						+ pic_idx + "|"
						+ get_tuin_and_tid.uin + "|"
						+ get_tuin_and_tid.tid + "|"
						+ qz_metadata.orgdata.mkey + "|"
						+ qz_metadata.orgdata.platformid + "|"
						+ qz_metadata.orgdata.uin ?>
					<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[pic_idx], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
				<?cs /if?>
			<?cs else ?>
				<?cs loop:j=0, _end, 1?>
					<?cs if:qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_picture && qz_metadata.orgdata.extendinfo.share_subtype != SHARE_srctype_album ?>
						<?cs call:data_cntmedia_pic_urlaction(j, qz_metadata.orgdata.itemdata[j], qz_metadata.orgdata.srcurl,"","" ) ?>
					<?cs else ?>
						<?cs set:popup_param = qz_metadata.orgdata.extendinfo.share_subtype + "|"
							+ j + "|"
							+ get_tuin_and_tid.uin + "|"
							+ get_tuin_and_tid.tid + "|"
							+ qz_metadata.orgdata.mkey + "|"
							+ qz_metadata.orgdata.platformid + "|"
							+ qz_metadata.orgdata.uin ?>

						<?cs call:data_cntmedia_pic_popup(j, qz_metadata.orgdata.itemdata[j], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
					<?cs /if?>
				<?cs /loop?>
			<?cs /if?>
		<?cs elif:qz_metadata.orgdata.mediatype == UC_MEDIA_TYPE_TXT ?>	
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_url || qz_metadata.orgdata.extendinfo.share_subtype ==SHARE_srctype_appurl ?>
				<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_NOIMG , "") ?>
			<?cs /if ?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>
