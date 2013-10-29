<?cs ####
	/*分享标题区*/
?>

<?cs def:data_follow_contentbox()?>
	<?cs call:i()?>

	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ?>
		<?cs call:data_title_tipTxt("关注")?>
		<?cs if:subcount(qz_metadata.orgdata.itemdata) == 1 ?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.itemdata[0].itemid, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.itemdata[0].itemid) ?>
		<?cs elif:subcount(qz_metadata.orgdata.itemdata) == 2 ?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.itemdata[0].itemid, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.itemdata[0].itemid) ?>
			<?cs call:data_title_tipTxt("和")?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.itemdata[1].itemid, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.itemdata[1].itemid) ?>
		<?cs elif:subcount(qz_metadata.orgdata.itemdata) >= 3 ?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.itemdata[0].itemid, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.itemdata[0].itemid) ?>
			<?cs call:data_title_tipTxt("和")?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.itemdata[1].itemid, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.itemdata[1].itemid) ?>
			<?cs call:data_title_tipTxt("等" + qz_metadata.orgdata.itemcount + "个认证空间")?>
		<?cs /if?>
	<?cs /if?>

	<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_SMALL_MODE , "") ?>
	<?cs loop:j=0, _end, 1?>
		<?cs call:data_cntmedia_pic_urlaction(j, qz_metadata.orgdata.itemdata[j], qz_metadata.orgdata.itemdata[j].action,"","" ) ?>
	<?cs /loop?>

<?cs /def?>

<?cs ####
	/**
	 *生成手机cover回复参数
	 *
	 *@param {vt2body} t2body 当前评论的索引
	 */
?>
<?cs def:_cover_psv_commentReply_param(t2body)?>
	<?cs set:_param = qz_metadata.meta.appid + "''"
						+ qz_metadata.orgdata.uin + "''"
						+ qz_metadata.orgdata.mkey + "''"
						+ t2body.seq + "''"
						+ t2body.uin + "''"
						+ qz_metadata.feedtype + "''0''"
						+ qz_metadata.orgdata.subtype ?>
	<?cs set:_app_psv_commentReply_param.ret = _param?>
<?cs /def?>


<?cs def:data_like_contentbox()?>
	<?cs call:i()?>
	<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_SMALL_MODE , "") ?>
	<?cs if:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_blog?>
		<?cs call:data_content_text(qz_metadata.orgdata.content) ?>
		<?cs if:subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs set:blog_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/blog/" + qz_metadata.orgdata.mkey ?>
			<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], blog_url,"","" ) ?>
		<?cs /if?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_photo || qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_qun?>
		<?cs if:subcount(qz_metadata.orgdata.itemdata)>1 ?>
			<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE_SMALL , "") ?>
		<?cs else ?>
			<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND || qz_metadata.scope == SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE , "") ?>
			<?cs else ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_DEFAULT , "") ?>
			<?cs /if ?>
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
				<?cs #赞相册用新版浮层，群相册topicid要组装 ?>
				<?cs if:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_qun ?>
					<?cs set:topicid = qz_metadata.orgdata.albumdata.extendinfo["groupcode"] + "_" + qz_metadata.orgdata.albumdata.sAlbumId ?>
				<?cs else ?>
					<?cs set:topicid = qz_metadata.orgdata.itemdata[0].albumid ?>
				<?cs /if ?>
				<?cs call:data_cntmedia_pic_popup_v2(j, qz_metadata.orgdata.itemdata[j], topicid, 
					qz_metadata.orgdata.itemdata[j].itemid, qz_metadata.orgdata.extendinfo.appid) ?>
			<?cs /loop?>
		<?cs /if?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_app?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_SMALL_MODE , "") ?>
		<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname) ?>
		<?cs call:data_textTitle_tipTxt("：")?>
		<?cs if:subcount(qz_metadata.orgdata.itemdata[0].desc) > 0 ?>
			<?cs call:data_textTitle_rich(qz_metadata.orgdata.itemdata[0].desc)?>
		<?cs else?>
			<?cs call:data_textTitle_tipTxt("我把手机空间的个人主页背景换成了自己的照片，还不错哦~你也来试试吧~")?>
		<?cs /if?>
		<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], "http://z.qzone.com/", "", "") ?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_main?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_DEFAULT , "") ?>
		<?cs set:_attr=1 ?>
		<?cs set:_attr.0.key="title" ?>
		<?cs set:_attr.0.value="点击去往主页查看赞详情" ?>
		<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], "http://user.qzone.qq.com/"+qz_metadata.orgdata.uin+"/main", "", "") ?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_share?>
		<?cs call:get_share_uin_and_shareid() ?>

		<?cs if:qz_metadata.orgdata.extendinfo.sharetype != SHARE_srctype_weibo?>
			<?cs if:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_vedio?>
				<?cs set:jump_url = "http://www.urlshare.cn/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=" 
					+ uri_encode(qz_metadata.orgdata.srcurl) + "&shareid="  + get_share_uin_and_shareid.shareid ?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_blog?>
				<?cs set:jump_url = "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_blogdetail?blogurl=" 
					+ uri_encode(qz_metadata.orgdata.srcurl) + "&shareuin=" 
					+ get_share_uin_and_shareid.uin + "&itemid=" 
					+ get_share_uin_and_shareid.shareid + "&spaceuin=&cginame=&isfriend=1" ?>
			<?cs else ?>
				<?cs set:jump_url = qz_metadata.orgdata.srcurl ?>
			<?cs /if?>
			<?cs if:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_url || qz_metadata.orgdata.extendinfo.sharetype ==SHARE_srctype_appurl ?>
				<?cs if:subcount(qz_metadata.orgdata.itemdata[0]) ?>
					<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_SMALL_V8_MODE , "") ?>
					<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], qz_metadata.orgdata.srcurl,"","") ?>
					<?cs else ?>
					<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_NOIMG , "") ?>
				<?cs /if ?>

			<?cs /if?>
			<?cs if:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_space_dress?>
				<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content + "装扮", jump_url)?>
			<?cs else ?>
				<?cs call:data_textTitle_url(qz_metadata.orgdata.title.0.content, jump_url)?>
			<?cs /if?>
		<?cs /if?>
		<?cs call:data_content_text(qz_metadata.orgdata.content) ?>

		<?cs if:subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs if:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_vedio ?>
				<?cs set:play_param = "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareget_onedetail?uin=" + get_share_uin_and_shareid.uin
					+ "&itemid=" + get_share_uin_and_shareid.shareid + "&cginame=popup&spaceuin=0" ?>
				<?cs call:data_cntmedia_video_show(0, qz_metadata.orgdata.itemdata[0], "", play_param, "") ?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_album || qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_picture ?>
				<?cs set:popup_param = qz_metadata.orgdata.extendinfo.share_subtype + "|"
					+ "0" + "|"
					+ get_share_uin_and_shareid.uin + "|"
					+ get_share_uin_and_shareid.shareid + "|"
					+ qz_metadata.orgdata.mkey + "|"
					+ qz_metadata.orgdata.platformid + "|"
					+ qz_metadata.orgdata.uin ?>
				<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[0], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
				<?cs call:data_cntmedia_pic_add_popup_attr("env","subappid:"+qz_metadata.orgdata.extendinfo.appid) ?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_blog ?>
				<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], qz_metadata.orgdata.srcurl,"","") ?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_picture_ext ?>
				<?cs set:popup_param = qz_metadata.orgdata.extendinfo.share_subtype + "|"
					+ "0" + "|"
					+ get_share_uin_and_shareid.uin + "|"
					+ get_share_uin_and_shareid.shareid + "|"
					+ qz_metadata.orgdata.mkey + "|"
					+ qz_metadata.orgdata.platformid + "|"
					+ qz_metadata.orgdata.uin ?>
				<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[0], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
				<?cs call:data_cntmedia_pic_add_popup_attr("env","subappid:"+qz_metadata.orgdata.extendinfo.appid) ?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_space_dress ?>
				<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], qz_metadata.orgdata.srcurl,"","") ?>
			<?cs /if?>
		<?cs /if?>
	<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_mood?>
		<?cs if:subcount(qz_metadata.orgdata.itemdata)>1 ?>
			<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE_SMALL , "") ?>
		<?cs else ?>
			<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND || qz_metadata.scope == SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE , "") ?>
			<?cs else ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_DEFAULT , "") ?>
			<?cs /if ?>
		<?cs /if ?>
		<?cs call:get_share_uin_and_shareid() ?>

		<?cs if:subcount(qz_metadata.relybody) == 0 ?>
			<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND || qz_metadata.scope == SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
				<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, "")?>
			<?cs else ?>
				<?cs call:data_textTitle_nick(qz_metadata.meta.loginuin, USER_PLATFORM_WHO_QZONE, "")?>
			<?cs /if ?>
			<?cs call:data_textTitle_tipTxt("：")?>
		<?cs /if?>

		<?cs if:qz_metadata.orgdata.extendinfo.subtype == FOLLOW_moodtype_timeline ?>
			<?cs call:data_textTitle_tipTxt("添加时光轴记录")?>
			<?cs if:qz_metadata.orgdata.ctime != 0 ?>
				<?cs set:jump_url = "http://user.qzone.qq.com/" + get_share_uin_and_shareid.uin 
					+ "/main?mode=gfp_timeline#eid=s" + get_share_uin_and_shareid.shareid + "&time=" + qz_metadata.orgdata.ctime ?>
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
		<?cs /if?>

		<?cs if:subcount(qz_metadata.relybody) > 0 ?>
			<?cs call:data_content_text(qz_metadata.relybody.msg) ?>
			<?cs if:qz_metadata.orgdata.extendinfo.rt_ttype == FOLLOW_moodrttype_weibo && qz_metadata.orgdata.extendinfo.rt_nosrc == 1 ?>
				<?cs call:data_textTitle_nick(qz_metadata.orgdata.extendinfo.rt_account, USER_PLATFORM_WHO_WEIBO, qz_metadata.orgdata.nickname)?>
			<?cs else ?>
				<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
			<?cs /if?>
			<?cs call:data_textTitle_tipTxt("：")?>
			<?cs call:data_textTitle_rich(qz_metadata.orgdata.content) ?>
		<?cs else ?>
			<?cs call:data_textTitle_rich(qz_metadata.orgdata.content) ?>
		<?cs /if?>

		<?cs if:qz_metadata.orgdata.extendinfo.richtype == FOLLOW_richtype_img && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs loop:j=0, subcount(qz_metadata.orgdata.itemdata) - 1, 1?>
				<?cs set:popup_param =  get_share_uin_and_shareid.shareid + "|" + get_share_uin_and_shareid.uin + "|" + j ?>
				<?cs call:data_cntmedia_pic_popup(j, qz_metadata.orgdata.itemdata[j], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
				<?cs call:data_cntmedia_pic_add_popup_attr("env","subappid:"+qz_metadata.orgdata.extendinfo.appid) ?>
			<?cs /loop?>
		<?cs elif:qz_metadata.orgdata.extendinfo.richtype == FOLLOW_richtype_video && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs call:data_cntmedia_video_show(0, qz_metadata.orgdata.itemdata[0], qz_metadata.orgdata.extendinfo.richtype, "/qzone/app/mood/richinfo_view.html", "") ?>
		<?cs elif:qz_metadata.orgdata.extendinfo.richtype == FOLLOW_richtype_music && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
			<?cs if:subcount(qz_metadata.orgdata.extendinfo.songid) == 1 ?>
				<?cs set:music_param = "{action:3,shareuin:'" + qz_metadata.orgdata.uin + "',songid:" + qz_metadata.orgdata.itemdata[0].extendinfo.songid[0] 
					+ ",flashid:'flash_" + qz_metadata.orgdata.mkey + "',playbtn:'controls_" + qz_metadata.orgdata.mkey 
					+ "',redarea:'player_" + qz_metadata.orgdata.mkey + "',quality:0}" ?>
			<?cs elif:subcount(qz_metadata.orgdata.itemdata[0].extendinfo.songid) > 0 ?>
				<?cs set:songlist = qz_metadata.orgdata.itemdata[0].extendinfo.songid[0] ?>
				<?cs loop:j=1, subcount(qz_metadata.orgdata.extendinfo.songid) - 1, 1?>
					<?cs set:songlist = songlist + "," + qz_metadata.orgdata.itemdata[0].extendinfo.songid[j] ?>
				<?cs /loop?>
				<?cs set:music_param = "{action:6,songid:'" + songlist + "',flashid:'flash_" + qz_metadata.orgdata.mkey 
					+ "',redarea:'player_" + qz_metadata.orgdata.mkey + "',playbtn:'controls_" + qz_metadata.orgdata.mkey + "'}" ?>
			<?cs /if?>

			<?cs call:data_music(qz_metadata.orgdata.itemdata[0].albumname, qz_metadata.orgdata.mkey, 1, qz_metadata.orgdata.itemdata[0].audiourl, 
				qz_metadata.orgdata.itemdata[0].picinfo[0].url, qz_metadata.orgdata.itemdata[0].duration, music_param, songlist) ?>
		<?cs /if?>

		<?cs if:subcount(qz_metadata.orgdata.multimedia.magic.itemdata) > 0 ?>
			<?cs call:data_magic(qz_metadata.orgdata.multimedia.magic.itemdata[0].itemid, qz_metadata.orgdata.multimedia.magic.itemdata[0].picinfo[0].url) ?>
		<?cs /if?>

		<?cs if:subcount(qz_metadata.orgdata.multimedia.attach.itemdata) > 0 ?>
			<?cs call:data_attach(qz_metadata.orgdata.multimedia.attach.itemdata[0].itemid, qz_metadata.orgdata.multimedia.attach.itemdata[0].name, 
				qz_metadata.orgdata.multimedia.attach.itemdata[0].extendinfo.path, qz_metadata.orgdata.multimedia.attach.itemdata[0].authorname, 
				qz_metadata.orgdata.multimedia.attach.itemdata[0].authoruin, qz_metadata.orgdata.multimedia.attach.itemdata[0].extendinfo.size) ?>
		<?cs /if?>

		<?cs if:subcount(qz_metadata.orgdata.multimedia.voice.itemdata) > 0 ?>
			<?cs loop:j=0, subcount(qz_metadata.orgdata.multimedia.voice.itemdata) - 1, 1?>
				<?cs call:data_voice(qz_metadata.orgdata.multimedia.voice.itemdata[j].itemid, qz_metadata.orgdata.multimedia.voice.itemdata[j].audiourl, 
					qz_metadata.orgdata.multimedia.voice.itemdata[j].duration, qz_metadata.orgdata.multimedia.voice.itemdata[j].extendinfo.cipher, 
					qz_metadata.orgdata.multimedia.voice.itemdata[j].extendinfo.expire_time) ?>
			<?cs /loop?>
		<?cs /if?>

	<?cs /if?>
<?cs /def?>

<?cs def:data_main_contentbox()?>
	<?cs if:qz_metadata.orgdata.subtype == FOLLOW_mtype_follow ?>
		<?cs call:data_follow_contentbox() ?>
	<?cs else ?>
		<?cs call:data_like_contentbox() ?> 
	<?cs /if?>
<?cs /def?>
