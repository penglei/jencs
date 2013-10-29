<?cs ####
	/*说说内容标题区*/
?>

<?cs set:PSV_PIC_NUM = 3?>

<?cs def:data_mood_subtitle()?>
	<?cs call:i()?>
	<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
		<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT_NOTIFYPSV?>
			<?cs set:rely_pos = 1 ?>
		<?cs else ?>
			<?cs set:rely_pos = 0 ?>
		<?cs /if?>
		<?cs set:name=subcount(qz_metadata.relybody) ?>
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

<?cs /def?>

<?cs ####
	/*说说内容区*/
?>

<?cs def:data_mood_contentbox()?>
	<?cs call:get_tuin_and_tid()?>
	<?cs if: qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
		<?cs if:subcount(qz_metadata.orgdata.itemdata)>1?>
			<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE_SMALL , "") ?>
		<?cs else ?>
			<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND || qz_metadata.scope == SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE , "") ?>
			<?cs else ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_DEFAULT , "") ?>
			<?cs /if ?>
		<?cs /if ?>
	<?cs else ?>

		<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_GRID_MODE , "") ?>
	<?cs /if ?>
	<?cs set:_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
	<?cs if:qz_metadata.orgdata.itemcount > 9?><?cs #说说必须大于9才展示?>
		<?cs call:data_extendinfo_picnum(qz_metadata.orgdata.itemcount)?>
	<?cs /if?>
	<?cs if:qz_metadata.orgdata.mediatype == UC_MEDIA_TYPE_VEDIO && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
		<?cs set:play_param = "/qzone/app/mood/richinfo_view.html"?>
		<?cs call:data_cntmedia_video_show(0, qz_metadata.orgdata.itemdata[0], 3, play_param, "") ?>
	<?cs elif:qz_metadata.orgdata.mediatype == UC_MEDIA_TYPE_PIC && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
		<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
			<?cs set:_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
			<?cs if:_end > (PSV_PIC_NUM - 1)?>
				<?cs set:_end = (PSV_PIC_NUM - 1)?>
			<?cs /if?>
			<?cs loop:j=0, _end, 1?>
				<?cs set:popup_param = get_tuin_and_tid.tid + "|" + get_tuin_and_tid.uin + "|" + j?>
				<?cs call:data_cntmedia_pic_popup(j, qz_metadata.orgdata.itemdata[j], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
				<?cs set:_pickey = qz_metadata.orgdata.itemdata[j].picinfo.0.extendinfo.mood_pickey?>
				<?cs if:!_is_in_qqtab && _pickey ?>
					<?cs #新版浮层需要的参数 ?>
					<?cs set:_actionpath = data_cntmedia_pic_popup.ret + ".action"?>
					<?cs set:topicid = get_tuin_and_tid.uin + "_" + get_tuin_and_tid.tid + "_" + qz_metadata.orgdata.extendinfo.t1_source?>
					<?cs call:data_popup_add_attr(_actionpath, "topicid", topicid) ?>
					<?cs call:data_popup_add_attr(_actionpath, "pickey", _pickey) ?>
				<?cs /if?>
			<?cs /loop?>
		<?cs else?>
			<?cs set:_count = subcount(qz_metadata.orgdata.itemdata)?>
			<?cs loop:j=0, _count-1, 1?>
			<?cs set:popup_param = get_tuin_and_tid.tid + "|" + get_tuin_and_tid.uin + "|" + j?>
			<?cs call:data_cntmedia_pic_popup(j, qz_metadata.orgdata.itemdata[j], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
			<?cs set:_pickey = qz_metadata.orgdata.itemdata[j].picinfo.0.extendinfo.mood_pickey?>
			<?cs if:!_is_in_qqtab && _pickey ?>
				<?cs #新版浮层需要的参数 ?>
				<?cs set:_actionpath = data_cntmedia_pic_popup.ret + ".action"?>
				<?cs set:topicid = get_tuin_and_tid.uin + "_" + get_tuin_and_tid.tid + "_" + qz_metadata.orgdata.extendinfo.t1_source?>
				<?cs call:data_popup_add_attr(_actionpath, "topicid", topicid) ?>
				<?cs call:data_popup_add_attr(_actionpath, "pickey", _pickey) ?>
				<?cs call:data_popup_add_attr(_actionpath, "hotclickPath", "0_appid_311.pic_count_" + _count + ".pic_" + j) ?>
				<?cs call:data_popup_add_attr(_actionpath, "hotdomain", "icv6act.qzone.qq.com") ?>
			<?cs /if?>
			<?cs /loop?>

		<?cs /if?>
	<?cs elif:qz_metadata.orgdata.mediatype == UC_MEDIA_TYPE_AUDIO && subcount(qz_metadata.orgdata.itemdata) > 0?>
		<?cs if:!qz_metadata.orgdata.extendinfo.iQuality ?>
			<?cs set:qz_metadata.orgdata.extendinfo.iQuality=0 ?>
		<?cs /if?>
		<?cs set:songids = qz_metadata.orgdata.itemdata[0].itemid?>
		<?cs loop:i = 1, subcount(qz_metadata.orgdata.itemdata) - 1 ,1?>
			<?cs if:qz_metadata.orgdata.itemdata[i].itemid?>
				<?cs set:songids = songids + "," + qz_metadata.orgdata.itemdata[i].itemid?>
			<?cs /if?>
		<?cs /loop?>
		<?cs set:music_param = "{action:6,shareuin:'" + get_tuin_and_tid.uin + "',songid:'" + songids 
			+ "',flashid:'flash_" + get_tuin_and_tid.tid + "',playbtn:'controls_" + get_tuin_and_tid.tid 
			+ "',redarea:'player_" + get_tuin_and_tid.tid + "',quality:" + qz_metadata.orgdata.extendinfo.iQuality+"}" ?>

		<?cs call:data_music(qz_metadata.orgdata.itemdata[0].name, get_tuin_and_tid.tid, 1, 
			qz_metadata.orgdata.itemdata[0].audiourl, qz_metadata.orgdata.itemdata[0].picinfo[0].url, 
			qz_metadata.orgdata.itemdata[0].duration, music_param, "") ?>
	<?cs /if?>

	<?cs if:subcount(qz_metadata.orgdata.multimedia.attach.itemdata) > 0?>
		<?cs set:_end = subcount(qz_metadata.orgdata.multimedia.attach.itemdata) - 1?>
		
		<?cs loop:i=0, _end, 1?>
			<?cs call:data_attach("", 
								  qz_metadata.orgdata.multimedia.attach.itemdata[i].extendinfo.name, 
								  qz_metadata.orgdata.multimedia.attach.itemdata[i].extendinfo.path, 
								  "", 
								  qz_metadata.orgdata.multimedia.attach.itemdata[i].extendinfo.owner, 
								  qz_metadata.orgdata.multimedia.attach.itemdata[i].extendinfo.size) ?>
		<?cs /loop?>
	<?cs /if?>
	<?cs if:qz_metadata.orgdata.extendinfo.attachnum ?>
		<?cs call:data_more_attach(qz_metadata.orgdata.extendinfo) ?>
	<?cs /if ?>
	<?cs call:_mood_is_have_contentbox() ?>
	<?cs if:_mood_is_have_contentbox.ret == 1?>
		<?cs call:qfv("content.con_more",qz_metadata.orgdata.extendinfo.con_more) ?>
		<?cs call:get_userWho_platform(qz_metadata.orgdata.platformid,qz_metadata.orgdata.subplatformid) ?>
		<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, get_userWho_platform.ret, qz_metadata.orgdata.nickname) ?>
		<?cs call:data_textTitle_tipTxt("：") ?>

		<?cs if:_mood_is_have_contentbox.ret == 1
			&& qz_metadata.orgdata.platformid == UC_PLATFORM_ID_QZONE 
			&& qz_metadata.orgdata.platformsubid == UC_PLATFORM_QZONE_SUBID_TIMELINE?>

			<?cs call:data_textTitle_tipTxt("添加时光轴记录")?>
			<?cs if:string.length(qz_metadata.orgdata.extendinfo.lbs_abstime) > 0?>
				<?cs set:time_url = "http://user.qzone.qq.com/"
									+ qz_metadata.orgdata.uin 
									+ "/main?mode=gfp_timeline#eid=s"
									+ qz_metadata.orgdata.mkey
									+ "&time="
									+ qz_metadata.orgdata.extendinfo.lbs_abstime?>

				<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.lbs_time, time_url)?>
			<?cs /if?>
			<?cs if:qz_metadata.orgdata.extendinfo.uin_timeline_num > 0?>

				<?cs call:data_textTitle_tipTxt("和")?>
				<?cs set:_end = qz_metadata.orgdata.extendinfo.uin_timeline_num - 2 ?>
				<?cs loop:j=0, _end, 1?>
					<?cs set:name_url = "http://user.qzone.qq.com/"
										+ qz_metadata.orgdata.extendinfo.uininfo.uin_timeline[j]
										+ "/main?mode=gfp_timeline"?>
					<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.uininfo.nick_timeline[j], name_url)?>
					<?cs call:data_textTitle_tipTxt(",")?>
				<?cs /loop?>

				<?cs set:last_pos = qz_metadata.orgdata.extendinfo.uin_timeline_num - 1?>
				<?cs set:name_url = "http://user.qzone.qq.com/"
									+ qz_metadata.orgdata.extendinfo.uininfo.uin_timeline[last_pos]
									+ "/main?mode=gfp_timeline"?>
				<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.uininfo.nick_timeline[last_pos], name_url)?>
				<?cs call:data_textTitle_tipTxt("一起：")?>
			<?cs else?>
				<?cs if:subcount(qz_metadata.orgdata.content)?>
					<?cs call:data_textTitle_tipTxt("：")?>
				<?cs /if?>
			<?cs /if?>
		<?cs /if?>


		<?cs call:data_textTitle_rich(qz_metadata.orgdata.content) ?>
		<?cs call:data_extend_time(qz_metadata.orgdata.strtime) ?>
	<?cs /if ?>
	<?cs if:subcount(qz_metadata.orgdata.multimedia.voice.itemdata) > 0?>
		<?cs call:data_voice(qz_metadata.orgdata.multimedia.voice.itemdata[0].extendinfo.voice_id,
							 qz_metadata.orgdata.multimedia.voice.itemdata[0].extendinfo.voice_url,
							 qz_metadata.orgdata.multimedia.voice.itemdata[0].extendinfo.voice_time,
							 qz_metadata.orgdata.multimedia.voice.itemdata[0].extendinfo.voice_cipher,
							 "")?>
	<?cs /if?>

	<?cs if:subcount(qz_metadata.orgdata.multimedia.magic.itemdata)>0 ?>
		<?cs call:data_magic(qz_metadata.orgdata.multimedia.magic.itemdata[0].itemid, qz_metadata.orgdata.multimedia.magic.itemdata[0].picinfo[0].url) ?>
	<?cs /if?>
<?cs /def?>
