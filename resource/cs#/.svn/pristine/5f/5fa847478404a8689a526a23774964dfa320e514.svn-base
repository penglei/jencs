<?cs ####
	/*相册标题区*/
?>


<?cs def:data_photo_title()?>
	<?cs call:i()?>
	<?cs call:get_photo_type()?>
	<?cs #相册当前操作的图片下标 ?>
	<?cs call:get_photo_current_picindex() ?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_NEWCOMMENT && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
		
		<?cs if:qz_metadata.orgdata.subtype == PHOTO_subtype_single ?>
			<?cs #call:data_title_tipTxt("照片")?>
			<?cs call:data_title_txt_style("照片","tip",0)?>
		<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_album ?>
			<?cs call:data_title_tipTxt("相册")?>
			<?cs call:get_photo_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId,qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid) ?>
			<?cs call:data_title_url(qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name, get_photo_url.ret) ?>
		<?cs else ?>
			<?cs #call:data_title_tipTxt("照片")?>
			<?cs call:data_title_txt_style("照片","tip",0)?>
			<?cs #call:get_photo_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId,qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid) ?>
			<?cs #call:data_title_url(qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name, get_photo_url.ret) ?>		
		<?cs /if?>
		

		<?cs call:data_title_tipTxt("有了")?>

		<?cs call:photo_get_last_comment_pos()?>
		<?cs call:get_userWho_platform(qz_metadata.vt2body[photo_get_last_comment_pos.ret].platformid, qz_metadata.vt2body[photo_get_last_comment_pos.ret].platformsubid) ?>
		<?cs if: get_userWho_platform.ret== USER_PLATFORM_WHO_PY ?>
			<?cs set:nick_url = "http://www.pengyou.com/index.php?mod=profile&u=" + qz_metadata.vt2body[photo_get_last_comment_pos.ret].uin ?>
			<?cs call:data_title_nick(qz_metadata.vt2body[photo_get_last_comment_pos.ret].uin, get_userWho_platform.ret, qz_metadata.vt2body[photo_get_last_comment_pos.ret].nickname) ?>
		<?cs else ?>
			<?cs set:nick_url = "http://user.qzone.qq.com/" + qz_metadata.vt2body[photo_get_last_comment_pos.ret].uin ?>
			<?cs call:data_title_nick(qz_metadata.vt2body[photo_get_last_comment_pos.ret].uin, get_userWho_platform.ret, qz_metadata.vt2body[photo_get_last_comment_pos.ret].nickname) ?>
		<?cs /if?>
		<?cs call:data_title_tipTxt("的新评论")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_RELATEPSV?>
		<?cs if:subcount(qz_metadata.relybody) > 0 ?>

			<?cs if:qz_metadata.orgdata.subtype == PHOTO_subtype_single || qz_metadata.orgdata.subtype == PHOTO_subtype_qpai
				|| qz_metadata.orgdata.subtype == PHOTO_subtype_xyalvatar || qz_metadata.orgdata.subtype ==PHOTO_subtype_batch ?>
				
				<?cs if:qz_metadata.orgdata.albumdata.iIsFromMultiFeeds == 2 ?>
					<?cs call:data_title_tipTxt("转载")?>
					<?cs call:data_title_nick(qz_metadata.relybody[0].uin, USER_PLATFORM_WHO_QZONE, qz_metadata.relybody[0].nickname)?>
					<?cs call:data_title_tipTxt("的照片到相册")?>

					<?cs set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
						+ qz_metadata.orgdata.albumdata.sAlbumId ?>
					<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, album_url) ?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("转载")?>
					<?cs call:data_title_nick(qz_metadata.relybody[0].uin, USER_PLATFORM_WHO_QZONE, qz_metadata.relybody[0].nickname)?>
					<?cs call:data_title_tipTxt("的照片")?>

					<?cs #set:photo_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
						+ qz_metadata.orgdata.albumdata.sAlbumId + "/" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid ?>
					<?cs #call:data_title_url(qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name, photo_url) ?>
				<?cs /if?>

			<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_album ?>
					<?cs call:data_title_tipTxt("转载")?>
					<?cs call:data_title_nick(qz_metadata.relybody[0].uin, USER_PLATFORM_WHO_QZONE, qz_metadata.relybody[0].nickname)?>
					<?cs call:data_title_tipTxt("的照片到相册")?>

					<?cs set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
						+ qz_metadata.orgdata.albumdata.sAlbumId ?>
					<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, album_url) ?>

			<?cs else ?>
				<?cs call:data_title_tipTxt("照片")?>
			<?cs /if?>
		<?cs else ?>
			<?cs #/*原创动作for主动*/#?>
			
			<?cs #/*某些单图可能被多图的subtype更新，所以要加上typeid的判断*/#?>
			<?cs if:qz_metadata.orgdata.subtype == PHOTO_subtype_single || qz_metadata.orgdata.subtype == PHOTO_subtype_qpai
				|| qz_metadata.orgdata.subtype == PHOTO_subtype_xyalvatar || qz_metadata.meta.typeid==FEED_TYPE_PHOTO || qz_metadata.meta.typeid==FEED_ABOUT_PHOTO?>

				<?cs if:qz_metadata.orgdata.albumdata.iIsFromMultiFeeds == 0 || qz_metadata.meta.typeid==FEED_ABOUT_PHOTO ?>
					<?cs if:qz_metadata.orgdata.albumdata.iAnonymity == ALBUM_BABY ?>
						<?cs call:data_title_tipTxt("上传1张照片到亲子相册:") ?>
					<?cs else ?>
						<?cs call:data_title_tipTxt("上传1张照片到相册:") ?>
					<?cs /if ?>
					<?cs set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
						+ qz_metadata.orgdata.albumdata.sAlbumId ?>
					<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, album_url) ?>
				<?cs else ?>
					<?cs if:qz_metadata.orgdata.albumdata.iAnonymity == ALBUM_NORMAL ?>
					<?cs set:data_desc = "上传" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片到相册:" ?>
				<?cs elif:qz_metadata.orgdata.albumdata.iAnonymity == ALBUM_INDIVALBUM ?>
					<?cs set:data_desc = "上传" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片到个性相册:" ?>
				<?cs elif:qz_metadata.orgdata.albumdata.iAnonymity == ALBUM_PHOTOWALL ?>
					<?cs set:data_desc = "上传" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片到照片墙:" ?>
				<?cs elif:qz_metadata.orgdata.albumdata.iAnonymity == ALBUM_BABY?>
					<?cs set:data_desc = "上传" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片到亲子相册:" ?>
				
				<?cs else ?>
						<?cs set:data_desc = "上传" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片到相册:" ?>
				<?cs /if?>
					<?cs call:data_title_tipTxt(data_desc) ?>
					
					<?cs set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
						+ qz_metadata.orgdata.albumdata.sAlbumId ?>
					<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, album_url) ?>
				<?cs /if?>
			<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_album || qz_metadata.orgdata.subtype == PHOTO_subtype_batch?>

				<?cs if:qz_metadata.orgdata.albumdata.iAnonymity == ALBUM_NORMAL ?>
					<?cs set:data_desc = "上传" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片到相册:" ?>
				<?cs elif:qz_metadata.orgdata.albumdata.iAnonymity == ALBUM_INDIVALBUM ?>
					<?cs set:data_desc = "上传" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片到个性相册:" ?>
				<?cs elif:qz_metadata.orgdata.albumdata.iAnonymity == ALBUM_PHOTOWALL ?>
					<?cs set:data_desc = "上传" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片到照片墙:" ?>
				<?cs elif:qz_metadata.orgdata.albumdata.iAnonymity == ALBUM_BABY ?>
					<?cs set:data_desc = "上传" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片到亲子相册:" ?>
				<?cs else ?>
						<?cs set:data_desc = "上传" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片到相册:" ?>
				<?cs /if?>
				<?cs call:data_title_tipTxt(data_desc) ?>
				
				<?cs set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
					+ qz_metadata.orgdata.albumdata.sAlbumId ?>
				<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, album_url) ?>

			<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_picmark ?>
				<?cs call:data_title_tipTxt("在")?>
				<?cs call:data_title_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
				<?cs call:data_title_tipTxt("的照片中被圈出来")?>
			<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_flash ?>
				<?cs call:data_title_tipTxt("制作动感影集:") ?>
				<?cs set:flash_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/vphoto/" 
					+ qz_metadata.orgdata.extendinfo.Flash_iFlashId ?>
				<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.Flash_strFlashName, flash_url) ?>
			<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_indivalbum ?>
				<?cs if:qz_metadata.meta.typeid == FEED_TYPE_INDIVALBUM ?>
					<?cs call:data_title_tipTxt("用") ?>
					<?cs set:indivalbum_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin
						+ "/photo/personal/1/" + qz_metadata.orgdata.albumdata.bgid + "/"
						+ qz_metadata.orgdata.albumdata.iTpid + "/"
						+ qz_metadata.orgdata.albumdata.sAlbumId ?>
					<?cs if:string.length(qz_metadata.orgdata.albumdata.sTpName) > 0 ?>
						<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sTpName, indivalbum_url) ?>
					<?cs elif:string.length(qz_metadata.orgdata.albumdata.sSonaName) > 0 ?>
						<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sSonaName, indivalbum_url) ?>
					<?cs /if?>
					<?cs call:data_title_tipTxt("装扮相册:") ?>
					<?cs set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
						+ qz_metadata.orgdata.albumdata.sAlbumId ?>
					<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, album_url) ?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("更新个性相册:") ?>
					<?cs set:indivalbum_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin
						+ "/photo/personal/1/" + qz_metadata.orgdata.albumdata.bgid + "/"
						+ qz_metadata.orgdata.albumdata.iTpid + "/"
						+ qz_metadata.orgdata.albumdata.sAlbumId ?>
					<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, indivalbum_url) ?>
				<?cs /if?>
			<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_activity ?>
				<?cs call:data_title_tipTxt("在照片活动")?>
				<?cs set:activity_url = "http://rc.qzone.qq.com/photo/activity/" + qz_metadata.orgdata.extendinfo.Activity_iActId ?>
				<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.Activity_sActName, activity_url) ?>
				<?cs call:data_title_tipTxt("中上传照片")?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("上传照片")?>
			<?cs /if?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEEDS_TYPE_SHARETOME?>
		<?cs call:data_title_tipTxt("在")?>
		<?cs set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
			+ qz_metadata.orgdata.albumdata.sAlbumId ?>
		<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, album_url) ?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs set:data_desc = "分享了" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片给" ?>
			<?cs call:data_title_tipTxt(data_desc)?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
		<?cs else ?>
			<?cs set:data_desc = "分享了" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片给我" ?>
			<?cs call:data_title_tipTxt(data_desc)?>
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT?>
		<?cs #指定好友可见被动 ?>
		<?cs if:qz_metadata.meta.typeid == FEED_TYPE_FRI_PRIV_PASSIVE ?>
			<?cs call:data_title_tipTxt("在")?>
			<?cs set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
				+ qz_metadata.orgdata.albumdata.sAlbumId ?>
			<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, album_url) ?>
			<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
				<?cs set:data_desc = "分享了" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片给" ?>
				<?cs call:data_title_tipTxt(data_desc)?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs else ?>
				<?cs set:data_desc = "分享了" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片给我" ?>
				<?cs call:data_title_tipTxt(data_desc)?>
			<?cs /if ?>
		<?cs ## /*elif:qz_metadata.orgdata.subtype == PHOTO_subtype_single || qz_metadata.orgdata.subtype == PHOTO_subtype_qpai
			|| qz_metadata.orgdata.subtype == PHOTO_subtype_xyalvatar ?>
			<?cs ##call:data_title_tipTxt("评论")?>
			<?cs #set:photo_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
				+ qz_metadata.orgdata.albumdata.sAlbumId + "/" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid ?>
			<?cs #call:data_title_url(qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name, photo_url)  */ ?>
		<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_album ||　qz_metadata.orgdata.subtype == PHOTO_subtype_indivalbum 
				|| qz_metadata.orgdata.subtype == PHOTO_subtype_batch ?>
			<?cs call:data_title_tipTxt("评论相册")?>
			<?cs set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
				+ qz_metadata.orgdata.albumdata.sAlbumId ?>
			<?cs #批次被动的跳转到批次页面 ?>
			<?cs if:qz_metadata.meta.typeid == FEED_TYPE_BATCH_CMT ?>
				<?cs set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/batch/" 
				+ qz_metadata.orgdata.albumdata.sAlbumId + "/" + qz_metadata.orgdata.albumdata.sBatchUploadId ?>
			<?cs /if?>
			<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, album_url)?>
		<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_picmark ?>
			<?cs if:qz_metadata.orgdata.uin == qz_metadata.meta.loginuin ?>
				<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
					<?cs call:data_title_tipTxt("在")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
					<?cs call:data_title_tipTxt("的照片中圈出")?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("在我的照片中圈出")?>
				<?cs /if ?>
				<?cs call:data_title_nick(qz_metadata.orgdata.extendinfo.Quan_iQuanedUin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.extendinfo.Quan_strQuanquanedNick)?>
			<?cs else ?>
				<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
					<?cs call:data_title_tipTxt("在照片中圈出了")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("在")?>
					<?cs call:data_title_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
					<?cs call:data_title_tipTxt("的照片中圈出了我")?>
				<?cs /if ?>	
			<?cs /if?>
		<?cs ## /*elif:qz_metadata.orgdata.subtype == PHOTO_subtype_flash ?>
			<?cs ##call:data_title_tipTxt("评论我的动感影集:") ?>
			<?cs ##set:flash_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/vphoto/" 
				+ qz_metadata.orgdata.extendinfo.Flash_iFlashId ?>
			<?cs ##call:data_title_url(qz_metadata.orgdata.extendinfo.Flash_strFlashName, flash_url) ?>
		<?cs ##elif:qz_metadata.orgdata.subtype == PHOTO_subtype_activity ?>
			<?cs ##call:data_title_tipTxt("评论我在照片活动:")?>
			<?cs ##set:activity_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin +
				"/photo/activity/" + qz_metadata.orgdata.extendinfo.Activity_iActId ?>
			<?cs ##call:data_title_url(qz_metadata.orgdata.extendinfo.Activity_sActName, activity_url) ?>
			<?cs ##call:data_title_tipTxt("上传的照片") */ ?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("评论")?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?>
		<?cs ## /*if:qz_metadata.orgdata.subtype == PHOTO_subtype_single || qz_metadata.orgdata.subtype == PHOTO_subtype_qpai
			|| qz_metadata.orgdata.subtype == PHOTO_subtype_xyalvatar || qz_metadata.orgdata.subtype == PHOTO_subtype_picmark ?>
			<?cs ##call:data_title_tipTxt("回复了我")?>
			<?cs #set:photo_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
				+ qz_metadata.orgdata.albumdata.sAlbumId + "/" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid ?>
			<?cs #call:data_title_url(qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name, photo_url) */ ?>
		<?cs if:qz_metadata.orgdata.subtype == PHOTO_subtype_album || qz_metadata.orgdata.subtype == PHOTO_subtype_batch?>
			<?cs call:data_title_tipTxt("在相册")?>
			<?cs set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
				+ qz_metadata.orgdata.albumdata.sAlbumId ?>
			<?cs if:qz_metadata.meta.typeid == FEED_TYPE_BATCH_CMT ?>
				<?cs set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/batch/" 
				+ qz_metadata.orgdata.albumdata.sAlbumId + "/" + qz_metadata.orgdata.albumdata.sBatchUploadId ?>
			<?cs /if?>
			<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, album_url) ?>
			<?cs call:data_title_tipTxt("回复：")?>
		<?cs ## /* elif:qz_metadata.orgdata.subtype == PHOTO_subtype_flash ?>
			<?cs ##call:data_title_tipTxt("在动感影集") ?>
			<?cs ##set:flash_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/vphoto/" 
				+ qz_metadata.orgdata.extendinfo.Flash_iFlashId ?>
			<?cs ##call:data_title_url(qz_metadata.orgdata.extendinfo.Flash_strFlashName, flash_url) ?>
			<?cs ##call:data_title_tipTxt("回复我")?>
		<?cs ##elif:qz_metadata.orgdata.subtype == PHOTO_subtype_indivalbum ?>
			<?cs ##call:data_title_tipTxt("个性相册") ?>
			<?cs ##set:album_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
				+ qz_metadata.orgdata.albumdata.sAlbumId ?>
			<?cs ##call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, album_url) ?>
			<?cs ##call:data_title_tipTxt("回复我")?>
		<?cs ##elif:qz_metadata.orgdata.subtype == PHOTO_subtype_activity ?>
			<?cs ##call:data_title_tipTxt("在照片活动")?>
			<?cs ##set:activity_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin +	"/photo/activity/" + qz_metadata.orgdata.extendinfo.Activity_iActId ?>
			<?cs ##call:data_title_url(qz_metadata.orgdata.extendinfo.Activity_sActName, activity_url) ?>
			<?cs ##call:data_title_tipTxt("回复我") */ ?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("回复")?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV?>
		<?cs call:data_title_tipTxt("在")?>		
		<?cs call:data_title_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>

		<?cs if:qz_metadata.orgdata.subtype == PHOTO_subtype_album ||　qz_metadata.orgdata.subtype == PHOTO_subtype_indivalbum 
			|| qz_metadata.orgdata.subtype == PHOTO_subtype_batch ?>
			<?cs call:data_title_tipTxt("相册:")?>
			<?cs call:get_album_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId) ?>
			<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, get_album_url.ret) ?>
		<?cs else ?>
			<?cs #call:data_title_tipTxt("照片")?>
			<?cs call:data_title_txt_style("照片","tip",0)?>
			<?cs #call:get_photo_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId,qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid) ?>
			<?cs #call:data_title_url(qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name, get_photo_url.ret) ?>
		<?cs /if ?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("提到")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>	
		<?cs else ?>
			<?cs call:data_title_tipTxt("提到我") ?>
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY?>
		<?cs call:data_title_tipTxt("在")?>
		<?cs call:data_title_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
		<?cs if:qz_metadata.orgdata.subtype == PHOTO_subtype_album ||　qz_metadata.orgdata.subtype == PHOTO_subtype_indivalbum 
			|| qz_metadata.orgdata.subtype == PHOTO_subtype_batch ?>
			<?cs call:data_title_tipTxt("相册:")?>
			<?cs call:get_album_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId) ?>
			<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, get_album_url.ret) ?>
		<?cs else ?>
			<?cs #call:data_title_tipTxt("在照片")?>
			<?cs call:data_title_txt_style("在照片","tip",0)?>
			<?cs #call:get_photo_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId,qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid) ?>
			<?cs #call:data_title_url(qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name, get_photo_url.ret) ?>
		<?cs /if?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("提到")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>	
		<?cs else ?>
			<?cs call:data_title_tipTxt("提到我") ?>
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM?>
		<?cs call:data_title_tipTxt("在")?>
		<?cs call:data_title_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
		<?cs if:qz_metadata.orgdata.subtype == PHOTO_subtype_album ||　qz_metadata.orgdata.subtype == PHOTO_subtype_indivalbum 
			|| qz_metadata.orgdata.subtype == PHOTO_subtype_batch ?>
			<?cs call:data_title_tipTxt("相册:")?>
			<?cs call:get_album_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId) ?>
			<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, get_album_url.ret) ?>
		<?cs else ?>
			<?cs #call:data_title_tipTxt("照片")?>
			<?cs call:data_title_txt_style("照片","tip",0)?>
			<?cs #call:get_photo_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId,qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid) ?>
			<?cs #call:data_title_url(qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name, get_photo_url.ret) ?>
		<?cs /if ?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("提到")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>	
		<?cs else ?>
			<?cs call:data_title_tipTxt("提到我") ?>
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT_NOTIFYPSV?>
		<?cs ##/*if:qz_metadata.orgdata.subtype == PHOTO_subtype_album ||　qz_metadata.orgdata.subtype == PHOTO_subtype_indivalbum */?>
		<?cs if:qz_metadata.orgdata.albumdata.iIsFromMultiFeeds == 2 ?>	
			<?cs set:zznum=subcount(qz_metadata.orgdata.itemdata) ?>
			<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
				<?cs call:data_title_tipTxt("转载") ?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>	
				<?cs set:data_desc = "的" + zznum + "张照片到相册:" ?>
				<?cs call:data_title_tipTxt(data_desc) ?>
			<?cs else ?>	
				<?cs set:data_desc = "转载我的" + zznum + "张照片到相册:" ?>
				<?cs call:data_title_tipTxt(data_desc) ?>
			<?cs /if ?>	
			<?cs call:get_album_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId) ?>
			<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, get_album_url.ret) ?>
		<?cs else ?>	
			<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
				<?cs call:data_title_tipTxt("转载")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>	
				<?cs call:data_title_tipTxt("的照片")?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("转载我的照片")?>
			<?cs /if ?>
			<?cs call:get_photo_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId,qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid)?>
			<?cs call:data_title_url(qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name, get_photo_url.ret) ?>
		<?cs /if ?>
	<?cs else ?>
		<?cs call:data_title_tipTxt("发表")?>
	<?cs /if?>

	<?cs if: qz_metadata.orgdata.albumdata.iPrivacy==6 ?>
		<?cs call:data_title_tipTxt("【指定好友可见】") ?>
	<?cs elif: qz_metadata.orgdata.albumdata.iPrivacy==3 ?>
		<?cs call:data_title_tipTxt("【仅自己可见】") ?>
	<?cs /if ?>
<?cs /def?>
