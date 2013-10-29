<?cs ####
	/**
	 * 相册基础函数
	 */
?>

<?cs set:SCOPE_FRIENDSHIP_ME_TO_FRIEND=1?>  <?cs #/*friendship场景标识:我对好友的操作*/#?>
<?cs set:SCOPE_FRIENDSHIP_FRIEND_TO_ME=2?>	<?cs #/*friendship场景标识:好友对我的操作*/#?>


<?cs #{/*相册subtype定义*/#?>
<?cs set:PHOTO_subtype_album=1?>			<?cs #/*相册类*/#?>
<?cs set:PHOTO_subtype_single=2?>			<?cs #/*单图类*/#?>
<?cs set:PHOTO_subtype_picmark=3?>			<?cs #/*圈圈类*/#?>
<?cs set:PHOTO_subtype_flash=4?>			<?cs #/*动感影集类*/#?>
<?cs set:PHOTO_subtype_indivalbum=5?>			<?cs #/*个性相册类*/#?>
<?cs set:PHOTO_subtype_activity=6?>			<?cs #/*照片活动类*/#?>
<?cs set:PHOTO_subtype_qpai=7?>			<?cs #/*Q拍类*/#?>
<?cs set:PHOTO_subtype_xyalvatar=8?>			<?cs #/*校友头像类*/#?>
<?cs set:PHOTO_subtype_batch=9?>			<?cs #/*批次类*/#?>
<?cs #}//end?>

<?cs #{/*相册Anonymity定义*/#?>
<?cs set:ALBUM_NORMAL=0?>			<?cs #/*普通相册*/#?>
<?cs set:ALBUM_INDIVALBUM=1?>			<?cs #/*个性相册*/#?>
<?cs set:ALBUM_PHOTOWALL=3?>			<?cs #/*照片墙*/#?>
<?cs set:ALBUM_BABY=5?>			<?cs #/*亲子相册*/#?>
<?cs #}//end?>

<?cs #{/*相册假feeds请求来源定义*/#?>
<?cs set:FEED_UPLOADPIC_ACTIVE=101?>			<?cs #/*单图主动feeds*/#?>
<?cs set:FEED_INDIVALBUM_ACTIVE=201?>			<?cs #/*个性相册feeds*/#?>
<?cs set:FEED_ZZPHOTO_ACTIVE=301?>			<?cs #/*转载单图主动*/#?>
<?cs set:FEED_ZZPHOTO_PASSIVE=302?>			<?cs #/*转载单图被动*/#?>
<?cs set:FEED_PHOTOCMT_PASSIVE=402?>			<?cs #/*单图评论被动*/#?>
<?cs set:FEED_QUANQUAN_ACTIVE=501?>			<?cs #/*圈人主动*/#?>
<?cs set:FEED_QUANQUAN_PASSIVE_OWNER=502?>			<?cs #/*圈人被动(主人)*/#?>
<?cs set:FEED_QUANQUAN_PASSIVE_TARGET=503?>			<?cs #/*圈圈被动（被圈人）*/#?>
<?cs set:FEED_PHOTO_ACTIVE=601?>			<?cs #/*单张照片feed*/#?>
<?cs set:FEED_ABOUT_PHOTOCMT=701?>			<?cs #/*与我相关图片评论*/#?>
<?cs set:FEED_ABOUT_ALBUMCMT=702?>			<?cs #/*与我相关相册评论*/#?>
<?cs set:FEED_UPLOADALBUM_ACTIVE=801?>			<?cs #/*相册上传feeds*/#?>
<?cs set:FEED_ALBUMCMT_PASSIVE=802?>			<?cs #/*相册评论被动feeds*/#?>
<?cs set:FEED_ALBUMALt_PASSIVE=901?>			<?cs #/*相册@被动feeds*/#?>
<?cs set:FEED_PHOTOALt_PASSIVE=902?>			<?cs #/*图片@被动feeds*/#?>
<?cs set:FEED_BATCHALt_PASSIVE=903?>			<?cs #/*批次@被动feeds*/#?>
<?cs set:FEED_ZZMULTI_ACTIVE=1001?>			<?cs #/*多图转载主动*/#?>
<?cs set:FEED_ZZMULTI_PASSIVE=1002?>			<?cs #/*多图转载被动*/#?>
<?cs #}//end?>


<?cs #{/*相册typeid定义（qz_metadata.meta.typeid这个字段尽量少用，只有指定人被动这些非主流的才用）*/#?>
<?cs set:FEED_TYPE_ALBUM=0?>			<?cs #/*多图主动feeds*/#?>
<?cs set:FEED_TYPE_PHOTOCMT=1?>			<?cs #/*照片评论/回复(包括圈圈)*/#?>
<?cs set:FEED_TYPE_PICMARK=2?>			<?cs #/*圈圈评论和回复(仅用于生成feedflag)*/#?>
<?cs set:FEED_TYPE_FLASH=3?>			<?cs #/*制作动感影集*/#?>
<?cs set:FEED_TYPE_FLASHCMT=4?>			<?cs #/*动感影集评论*/#?>
<?cs set:FEED_TYPE_ZZPHOTO=5?>			<?cs #/*转载照片[主被动通用]*/#?>
<?cs set:FEED_TYPE_INDIVALBUM=6?>			<?cs #/*个性相册主动*/#?>
<?cs set:FEED_TYPE_ALBUMCMT=7?>			<?cs #/*相册评论被动*/#?>
<?cs set:FEED_TYPE_PHOTOCMT_AUDIT=8?>			<?cs #/*单图审核被动*/#?>
<?cs set:FEED_TYPE_ALBUMCMT_AUDIT=9?>			<?cs #/*相册审核被动*/#?>
<?cs set:FEED_TYPE_FLASHCMT_AUDIT=10?>			<?cs #/*动感影集审核被动*/#?>
<?cs set:FEED_TYPE_PHOTO=11?>			<?cs #/*单图主动*/#?>
<?cs set:FEED_TYPE_SHARED_ACT=12?>			<?cs #/*共享相册(很少用)*/#?>
<?cs set:FEED_TYPE_SHARED_ZZPHOTO=13?>			<?cs #/*共享相册(很少用)*/#?>
<?cs set:FEED_TYPE_SHARED_PHOTOCMT=14?>			<?cs #/*共享相册(很少用)*/#?>
<?cs set:FEED_ABOUT_PHOTO=15?>			<?cs #/*单图与我相关*/#?>
<?cs set:FEED_ABOUT_ALBUM=16?>			<?cs #/*相册与我相关*/#?>
<?cs set:FEED_TYPE_QPAI_PHOTO=17?>			<?cs #/*Q拍主动*/#?>
<?cs set:FEED_TYPE_QPAI_CMT_PHOTO=18?>			<?cs #/*Q拍评论被动*/#?>
<?cs set:FEED_TYPE_PHOTO_CMTACT=19?>			<?cs #/*单图评论置顶feeds*/#?>
<?cs set:FEED_TYPE_AT_PHOTO=20?>			<?cs #/*单图@被动*/#?>
<?cs set:FEED_TYPE_AT_ALBUM=21?>			<?cs #/*相册@被动*/#?>
<?cs set:FEED_TYPE_PHOTO_CMTACT_PY_TICKER=23?>			<?cs #/*单图评论置顶feeds(ticker)*/#?>
<?cs set:FEED_TYPE_ZZ_MULTI=24?>			<?cs #/*多图转载主动*/#?>
<?cs set:FEED_TYPE_ZZ_MULTI_PASSIVE=25?>			<?cs #/*多图转载被动*/#?>
<?cs set:FEED_TYPE_FRI_PRIV_PASSIVE=26?>			<?cs #/*指定人可见被动*/#?>
<?cs set:FEED_TYPE_BATCH_CMT=27?>			<?cs #/*批次评论被动*/#?>
<?cs set:FEED_TYPE_AT_BATCH=28?>			<?cs #/*批次评论@被动*/#?>
<?cs #}//end?>

<?cs #{/*图片压缩算法*/#?>
<?cs set:PIC_COMPRESSION_LONGEDGE=0?>			<?cs #/*长边压缩*/#?>
<?cs set:PIC_COMPRESSION_SHORTEDGE=1?>			<?cs #/*短边压缩*/#?>
<?cs set:PIC_COMPRESSION_SHORTEDGE_ALIGNCENTER=2?>	<?cs #/*短边压缩 居中显示*/#?>
<?cs #}//end?>

<?cs ####
	/**
	 *获取相册当前操作图片下标
	 */
?>
<?cs def:get_photo_current_picindex()?>
	<?cs set:get_photo_current_picindex.ret = qz_metadata.orgdata.albumdata.extendinfo.iCurPicIndex ?>
<?cs /def?>


<?cs ####
	/**
	 * 获取相册类型
	 */
?>
<?cs def:get_photo_type()?>
	<?cs if:qz_metadata.orgdata.subtype == PHOTO_subtype_album ?>
		<?cs set:get_photo_type.ret = "相册" ?>
	<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_single ?>
		<?cs set:get_photo_type.ret = "照片" ?>
	<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_picmark ?>
		<?cs set:get_photo_type.ret = "圈圈" ?>
	<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_flash ?>
		<?cs set:get_photo_type.ret = "动感影集" ?>
	<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_indivalbum ?>
		<?cs set:get_photo_type.ret = "个性相册" ?>
	<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_activity ?>
		<?cs set:get_photo_type.ret = "相册活动" ?>
	<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_xyalvatar ?>
		<?cs set:get_photo_type.ret = "校友个性头相" ?>
	<?cs elif:qz_metadata.orgdata.subtype == PHOTO_subtype_batch ?>
		<?cs set:get_photo_type.ret = "相册" ?>
	<?cs else ?>
		<?cs set:get_photo_type.ret = "照片"?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *获取相册的uin
	 */
?>
<?cs def:get_photo_uin()?>
	<?cs set:get_photo_uin.uin = qz_metadata.orgdata.uin ?>
<?cs /def?>



<?cs ####
	/**
	 * 获取最后一个非自己的评论人
	 */
?>
<?cs def:photo_get_last_comment_pos()?>
	<?cs set:comment_count = subcount(qz_metadata.vt2body) ?>

	<?cs call:get_photo_uin()?>
	<?cs loop: i = 0, comment_count - 1, 1?>
		<?cs if:get_photo_uin.uin != qz_metadata.vt2body[i].uin ?>
			<?cs set:photo_get_last_comment_pos.ret = i ?>
		<?cs /if?>
	<?cs /loop?>
<?cs /def?>


<?cs ####
	/**
	 *根据uin和albumid生成相册跳转地址
	 */
?>
<?cs def:get_album_url(uin, albumid)?>
	<?cs set:album_url = "http://user.qzone.qq.com/" + uin + "/photo/" 
				+ albumid ?>
	<?cs set:get_album_url.ret = album_url?>
<?cs /def?>


<?cs ####
	/**
	 *根据uin和albumid和photoid生成图片跳转地址
	 */
?>
<?cs def:get_photo_url(uin, albumid,photoid)?>
	<?cs set:photo_url = "http://user.qzone.qq.com/" + uin + "/photo/" 
			+ albumid + "/" + photoid ?>
	<?cs set:get_photo_url.ret = photo_url?>
<?cs /def?>

<?cs ####
	/**
	 *根据uin和albumid和batchid生成批次跳转地址
	 */
?>
<?cs def:get_batch_url(uin, albumid,batchid)?>
	<?cs set:batch_url = "http://user.qzone.qq.com/" + uin + "/photo/batch/" 
			+ albumid + "/" + batchid ?>
	<?cs set:get_batch_url.ret = batch_url?>
<?cs /def?>

<?cs ####
	/**
	 *生成个性相册模板Url
	 */
?>
<?cs def:get_indivalbum_url(uin, bgid)?>
	<?cs set:get_indivalbum_url.ret = qz_metadata.orgdata.albumdata.extendinfo.sIndivTmpUrl?>
<?cs /def?>

<?cs ####
	/**
	 *评论参数里生成假feeds来源字段(reqfrom)
	 */
?>
<?cs def:get_fakefeeds_req(subtype,feedtype)?>
	<?cs if:subtype==PHOTO_subtype_single || subtype==PHOTO_subtype_qpai ?>
		<?cs if:feedtype==UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_RELATEPSV?>
			<?cs if:subcount(qz_metadata.relybody) > 0 ?>
				<?cs set:get_fakefeeds_req.ret=FEED_ZZPHOTO_ACTIVE ?>
			<?cs else ?>
				<?cs set:get_fakefeeds_req.ret=FEED_PHOTO_ACTIVE ?>
			<?cs /if?>
		<?cs elif:feedtype==UC_WUP_FEED_TYPE_COMMPSV || feedtype==UC_WUP_FEED_TYPE_REPLYPSV ?>
			<?cs set:get_fakefeeds_req.ret=FEED_PHOTOCMT_PASSIVE ?>
		<?cs elif:feedtype==UC_WUP_FEED_TYPE_ATMEPSV || feedtype==UC_WUP_FEED_TYPE_ATMEPSV_BY_COM || feedtype==UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY?>
			<?cs set:get_fakefeeds_req.ret=FEED_PHOTOALt_PASSIVE ?>
		<?cs elif:feedtype==UC_WUP_FEED_TYPE_NEWCOMMENT ?>
			<?cs set:get_fakefeeds_req.ret=FEED_PHOTO_ACTIVE ?>
		<?cs /if?>
	<?cs elif:subtype==PHOTO_subtype_album ?>
		<?cs if:feedtype==UC_WUP_FEED_TYPE_ACT?>
			<?cs if:subcount(qz_metadata.relybody) > 0 ?>
				<?cs set:get_fakefeeds_req.ret=FEED_ZZMULTI_ACTIVE ?>
			<?cs else ?>
				<?cs set:get_fakefeeds_req.ret=FEED_UPLOADALBUM_ACTIVE ?>
			<?cs /if?>
		<?cs elif:feedtype==UC_WUP_FEED_TYPE_COMMPSV || feedtype==UC_WUP_FEED_TYPE_REPLYPSV ?>
			<?cs set:get_fakefeeds_req.ret=FEED_ALBUMCMT_PASSIVE ?>
		<?cs elif:feedtype==UC_WUP_FEED_TYPE_ATMEPSV || feedtype==UC_WUP_FEED_TYPE_ATMEPSV_BY_COM || feedtype==UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY?>
			<?cs set:get_fakefeeds_req.ret=FEED_ALBUMALt_PASSIVE ?>
		<?cs elif:feedtype==UC_WUP_FEED_TYPE_RELATEPSV ?>
			<?cs set:get_fakefeeds_req.ret=FEED_UPLOADALBUM_ACTIVE ?>
		<?cs /if?>
	<?cs elif:subtype==PHOTO_subtype_batch ?>
		<?cs if:feedtype==UC_WUP_FEED_TYPE_ACT?>
			<?cs if:subcount(qz_metadata.relybody) > 0 ?>
				<?cs set:get_fakefeeds_req.ret=FEED_ZZMULTI_ACTIVE ?>
			<?cs else ?>
				<?cs set:get_fakefeeds_req.ret=FEED_UPLOADALBUM_ACTIVE ?>
			<?cs /if?>
		<?cs elif:feedtype==UC_WUP_FEED_TYPE_COMMPSV || feedtype==UC_WUP_FEED_TYPE_REPLYPSV ?>
			<?cs set:get_fakefeeds_req.ret=FEED_ALBUMCMT_PASSIVE ?>
		<?cs elif:feedtype==UC_WUP_FEED_TYPE_ATMEPSV || feedtype==UC_WUP_FEED_TYPE_ATMEPSV_BY_COM || feedtype==UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY?>
			<?cs set:get_fakefeeds_req.ret=FEED_BATCHALt_PASSIVE ?>
		<?cs elif:feedtype==UC_WUP_FEED_TYPE_RELATEPSV ?>
			<?cs set:get_fakefeeds_req.ret=FEED_UPLOADALBUM_ACTIVE ?>
		<?cs /if?>
	<?cs elif:subtype==PHOTO_subtype_indivalbum ?>
		<?cs if:feedtype==UC_WUP_FEED_TYPE_ACT?>
			<?cs set:get_fakefeeds_req.ret=FEED_INDIVALBUM_ACTIVE ?>
		<?cs /if?>
	<?cs elif:subtype==PHOTO_subtype_picmark ?>
		<?cs if:feedtype==UC_WUP_FEED_TYPE_ACT?>
			<?cs set:get_fakefeeds_req.ret=FEED_QUANQUAN_ACTIVE ?>
		<?cs elif:feedtype==UC_WUP_FEED_TYPE_COMMPSV ?>
			<?cs set:get_fakefeeds_req.ret=FEED_QUANQUAN_PASSIVE_TARGET ?>
		<?cs /if?>
	<?cs /if?>
	
	<?cs #/*特殊逻辑(单图类动作更新多图feeds)当前feeds为指定人被动，ref使用单张主动*/#?>
	<?cs if:qz_metadata.meta.typeid == FEED_TYPE_FRI_PRIV_PASSIVE ?>
		<?cs set:get_fakefeeds_req.ret=FEED_PHOTO_ACTIVE ?>
	<?cs /if?>
	
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_NEWCOMMENT ?>
		<?cs set:get_fakefeeds_req.ret=FEED_PHOTO_ACTIVE ?>
	<?cs /if?>
	
	<?cs if:qz_metadata.meta.typeid==FEED_TYPE_ZZPHOTO &&  feedtype!=UC_WUP_FEED_TYPE_ACT?>
		<?cs set:get_fakefeeds_req.ret=FEED_ZZPHOTO_PASSIVE ?>
	<?cs /if?>
	
	<?cs if:qz_metadata.meta.typeid==FEED_TYPE_ZZ_MULTI_PASSIVE?>
		<?cs set:get_fakefeeds_req.ret=FEED_ZZMULTI_PASSIVE ?>
	<?cs /if?>
	
	<?cs if:qz_metadata.meta.typeid == FEED_TYPE_FRI_PRIV_PASSIVE ?>
		<?cs set:get_fakefeeds_req.ret=FEED_PHOTO_ACTIVE ?>
	<?cs /if?>
	
	<?cs if:qz_metadata.meta.typeid == FEED_TYPE_ALBUM ?>
		<?cs set:get_fakefeeds_req.ret=FEED_UPLOADALBUM_ACTIVE ?>
	<?cs /if?>
	
	<?cs if:qz_metadata.meta.typeid == FEED_ABOUT_ALBUM ?>
		<?cs set:get_fakefeeds_req.ret=FEED_UPLOADALBUM_ACTIVE ?>
	<?cs /if?>
	
<?cs /def?>


<?cs ####
	/**
	 *生成评论的cgi和参数
	 */
?>
<?cs def:get_cmt_cgi_param(subtype)?>
	<?cs #/*判断转载参数*/#?>
	<?cs if:subcount(qz_metadata.relybody) > 0 ?>
		<?cs set:zz_param = "&zz=1"?>
	<?cs else ?>
		<?cs set:zz_param = "&zz=0"?>
	<?cs /if?>
	<?cs call:get_photo_current_picindex() ?>
	
	<?cs #/*判断当前评论的来源*/#?>
	<?cs call:get_userWho_platform(qz_metadata.opinfo.t2body.platformid, qz_metadata.opinfo.t2body.platformsubid)?>
	<?cs if:get_userWho_platform.ret == USER_PLATFORM_WHO_PY ?>
		<?cs set:cmt_source = "&source=1"?>
	<?cs else ?>
		<?cs set:cmt_source = "&source=0"?>
	<?cs /if?>
	
	<?cs #/*组装评论类型*/#?>
	<?cs #/*多张主动走批次评论*/#?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_NEWCOMMENT || qz_metadata.meta.typeid == FEED_TYPE_ZZ_MULTI?>
		<?cs set:cmt_type = "&cmtType=0"?>
	<?cs elif:(qz_metadata.orgdata.albumdata.iIsFromMultiFeeds==1 || qz_metadata.orgdata.albumdata.iIsFromMultiFeeds==3) && qz_metadata.feedtype==UC_WUP_FEED_TYPE_ACT?>
		<?cs set:cmt_type = "&batchId=" + qz_metadata.orgdata.albumdata.sBatchUploadId + "&cmtType=4" ?>
	<?cs elif:subtype==PHOTO_subtype_single || subtype==PHOTO_subtype_qpai ?>
		<?cs set:cmt_type = "&cmtType=0"?>
	<?cs elif:subtype==PHOTO_subtype_album ?>
		<?cs set:cmt_type = "&cmtType=1"?>
	<?cs elif:subtype==PHOTO_subtype_batch ?>
		<?cs set:cmt_type = "&batchId=" + qz_metadata.orgdata.albumdata.sBatchUploadId + "&cmtType=4" ?>
	<?cs /if?>
	
	<?cs call:get_fakefeeds_req(qz_metadata.orgdata.subtype,qz_metadata.feedtype) ?>
	
	<?cs #/*按subtype来组cgi*/#?>
	<?cs #/*多图转载是特殊逻辑，默认评到第一张图*/#?>
	<?cs if:qz_metadata.meta.typeid == FEED_TYPE_ZZ_MULTI ?>
		<?cs set:get_cmt_cgi_param.ret.addcgi = "http://photo.qq.com/cgi-bin/common/cgi_add_piccomment"?>
		<?cs set:get_cmt_cgi_param.ret.addparam = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId
			+ "&forumindex=0" + "&lloc=" + qz_metadata.orgdata.itemdata[0].itemid + "&sloc=" + qz_metadata.orgdata.itemdata[0].itemid + "&privacy=" + qz_metadata.orgdata.albumdata.iPrivacy
			+ "&refer=qzone" + "&reqfrom=" + get_fakefeeds_req.ret + cmt_type + zz_param + "&picname=" + qz_metadata.orgdata.itemdata[0].name?>
		<?cs set:get_cmt_cgi_param.ret.delcgi = "http://photo.qq.com/cgi-bin/common/cgi_del_piccomment"?>
		<?cs set:get_cmt_cgi_param.ret.delparam = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId 
			+ "&did=" + qz_metadata.opinfo.t2body.seq + "&lloc=" + qz_metadata.orgdata.itemdata[0].itemid + "&sloc=" + qz_metadata.orgdata.itemdata[0].itemid + "&refer=qzone" + "&answeruin="
			+ qz_metadata.opinfo.t2body.uin	+cmt_source + cmt_type + "&replynum=2" + zz_param ?>
	<?cs elif:subtype==PHOTO_subtype_single || subtype==PHOTO_subtype_qpai ?>
		<?cs set:get_cmt_cgi_param.ret.addcgi = "http://photo.qq.com/cgi-bin/common/cgi_add_piccomment"?>
		<?cs set:get_cmt_cgi_param.ret.addparam = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId
			+ "&forumindex=0" + "&lloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&sloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&privacy=" + qz_metadata.orgdata.albumdata.iPrivacy
			+ "&refer=qzone" + "&reqfrom=" + get_fakefeeds_req.ret + cmt_type + zz_param + "&picname=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name?>
		<?cs set:get_cmt_cgi_param.ret.delcgi = "http://photo.qq.com/cgi-bin/common/cgi_del_piccomment"?>
		<?cs set:get_cmt_cgi_param.ret.delparam = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId 
			+ "&did=" + qz_metadata.opinfo.t2body.seq + "&lloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&sloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&refer=qzone" + "&answeruin="
			+ qz_metadata.opinfo.t2body.uin	+cmt_source + cmt_type + "&replynum=2" + zz_param ?>
	<?cs elif:subtype==PHOTO_subtype_album || subtype==PHOTO_subtype_batch ?>
		<?cs if:subtype == PHOTO_subtype_batch ?>
			<?cs set:lloc_info = "&lloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&sloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid?>
		<?cs /if?>
	
		<?cs set:get_cmt_cgi_param.ret.addcgi = "http://photo.qq.com/cgi-bin/common/cgi_add_piccomment"?>
		<?cs set:get_cmt_cgi_param.ret.addparam = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId
			+ "&forumindex=0" + "&lloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&sloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&privacy=" + qz_metadata.orgdata.albumdata.iPrivacy
			+ "&refer=qzone" + "&reqfrom=" + get_fakefeeds_req.ret + cmt_type + zz_param?>
		<?cs set:get_cmt_cgi_param.ret.delcgi = "http://photo.qq.com/cgi-bin/common/cgi_del_piccomment"?>
		<?cs set:get_cmt_cgi_param.ret.delparam = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId 
			+ "&did=" + qz_metadata.opinfo.t2body.seq + lloc_info + "&refer=qzone" + "&answeruin=" + qz_metadata.opinfo.t2body.uin	+cmt_source + cmt_type 
			+ "&replynum=2" + zz_param ?>
	<?cs /if?>
	
	<?cs #/*特殊逻辑当前feeds为指定人被动或者圈圈被动，使用的评论参数单独组装*/#?>
	<?cs if:qz_metadata.orgdata.subtype==PHOTO_subtype_picmark && qz_metadata.feedtype==UC_WUP_FEED_TYPE_COMMPSV?>
		<?cs set:quanid_param="&quanid=" + qz_metadata.orgdata.extendinfo.Quan_iQuanquanId ?>
	<?cs /if?>
	<?cs if:qz_metadata.meta.typeid == FEED_TYPE_FRI_PRIV_PASSIVE || (qz_metadata.orgdata.subtype==PHOTO_subtype_picmark && qz_metadata.feedtype==UC_WUP_FEED_TYPE_COMMPSV)?>
		<?cs set:get_cmt_cgi_param.ret.addcgi = "http://photo.qq.com/cgi-bin/common/cgi_add_piccomment"?>
		<?cs set:get_cmt_cgi_param.ret.addparam = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId
			+ "&forumindex=0" + "&lloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&sloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&privacy=" + qz_metadata.orgdata.albumdata.iPrivacy
			+ "&refer=qzone" + "&reqfrom=" + get_fakefeeds_req.ret + "&cmtType=0" + quanid_param ?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *生成回复的cgi和参数(主动feeds里使用)
	 */
?>
<?cs def:get_reply_cgi_param_bycid(subtype,i)?>
	<?cs call:get_fakefeeds_req(qz_metadata.orgdata.subtype,qz_metadata.feedtype) ?>
	<?cs call:get_photo_current_picindex() ?>
	
	<?cs #组装评论类型?>
	<?cs if:qz_metadata.vt2body[i].extendinfo.isPicCmt!=2 ?>
		<?cs set:cmt_type = "&cmtType=0"?>
	<?cs else?>
		<?cs set:cmt_type = "&batchId=" + qz_metadata.orgdata.albumdata.sBatchUploadId + "&cmtType=4" ?>
	<?cs /if?>
	
	<?cs #/*组装lloc参数*/#?>
	<?cs if:qz_metadata.vt2body[i].extendinfo.isPicCmt!=2 ?>
		<?cs set:lloc_info = "&lloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&sloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid ?>
	<?cs /if?>
	
	<?cs #/*组装相册图片名称*/#?>
	<?cs if:qz_metadata.vt2body[i].extendinfo.isPicCmt!=2 ?>
		<?cs set:topic_name = "&picname=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name ?>
	<?cs else ?>
		<?cs set:topic_name = "&albumname=" + qz_metadata.orgdata.albumdata.sAlbumName ?>
	<?cs /if?>
	
	
	<?cs #/*按isPicCmt来组回复cgi*/#?>
	<?cs if:qz_metadata.vt2body[i].extendinfo.isPicCmt!=2 ?>
		<?cs set:get_reply_cgi_param_bycid.ret.addcgi = "http://photo.qq.com/cgi-bin/common/cgi_add_icreply"?>
		<?cs set:get_reply_cgi_param_bycid.ret.addparam = "oweruin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId
			+ lloc_info + "&refer=qzone" + "&reqfrom=" + get_fakefeeds_req.ret + "&zz=0" + "&cmtid=" + qz_metadata.vt2body[i].seq
			+ "&picname=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name ?>
	<?cs else ?>
		<?cs set:get_reply_cgi_param_bycid.ret.addcgi = "http://photo.qq.com/cgi-bin/common/cgi_add_icreply"?>
		<?cs set:get_reply_cgi_param_bycid.ret.addparam = "oweruin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId
			+ lloc_info + "&refer=qzone" + "&reqfrom=" + get_fakefeeds_req.ret + "&zz=0" + "&cmtid=" + qz_metadata.vt2body[i].seq
			+ "&batchId=" + qz_metadata.orgdata.albumdata.sBatchUploadId + "&cmtType=4" + "&picname=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name ?>
	<?cs /if?>
	
	<?cs #/*按isPicCmt来组删除回复cgi*/#?>
	<?cs loop:j=0, subcount(qz_metadata.vt2body[i].vt3body)-1, 1 ?>	
		<?cs set:get_reply_cgi_param_bycid.ret.delcgi[j] = "http://photo.qq.com/cgi-bin/common/cgi_del_reply"?>
		<?cs set:get_reply_cgi_param_bycid.ret.delparam[j] = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId + lloc_info
			+ "&cmtid=" + qz_metadata.vt2body[i].seq + "&rtime_t=" + qz_metadata.vt2body[i].vt3body[j].ctime + "&quanquan=0&quanid=0&priv=1&refer=qzone"
			+ cmt_type + topic_name ?>
	<?cs /loop?>
	
<?cs /def?>


<?cs ####
	/**
	 *生成回复的cgi和参数
	 */
?>
<?cs def:get_reply_cgi_param(subtype)?>
	<?cs call:get_fakefeeds_req(qz_metadata.orgdata.subtype,qz_metadata.feedtype) ?>
	<?cs call:get_photo_current_picindex() ?>
	
	<?cs #/*组装lloc参数*/#?>
	<?cs if:subtype != PHOTO_subtype_album ?>
		<?cs set:lloc_info = "&lloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&sloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid ?>
	<?cs /if?>
	
	<?cs #/*组装评论类型*/#?>
	<?cs if:subtype==PHOTO_subtype_single || subtype==PHOTO_subtype_qpai ?>
		<?cs set:cmt_type = "&cmtType=0"?>
	<?cs elif:subtype==PHOTO_subtype_album ?>
		<?cs set:cmt_type = "&cmtType=1"?>
	<?cs elif:subtype==PHOTO_subtype_batch ?>
		<?cs set:cmt_type = "&batchId=" + qz_metadata.orgdata.albumdata.sBatchUploadId + "&cmtType=4" ?>
	<?cs /if?>
	
	<?cs #/*组装相册图片名称*/#?>
	<?cs if:subtype != PHOTO_subtype_album ?>
		<?cs set:topic_name = "&picname=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name ?>
	<?cs else ?>
		<?cs set:topic_name = "&albumname=" + qz_metadata.orgdata.albumdata.sAlbumName ?>
	<?cs /if?>
	
	
	<?cs #/*按subtype来组回复cgi*/#?>
	<?cs if:subtype==PHOTO_subtype_single || subtype==PHOTO_subtype_qpai ?>
		<?cs set:get_reply_cgi_param.ret.addcgi = "http://photo.qq.com/cgi-bin/common/cgi_add_icreply"?>
		<?cs set:get_reply_cgi_param.ret.addparam = "oweruin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId
			+ lloc_info + "&refer=qzone" + "&reqfrom=" + get_fakefeeds_req.ret + "&zz=0" + "&cmtid=" + qz_metadata.opinfo.t2body.seq
			+ "&picname=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name ?>
	<?cs elif:subtype==PHOTO_subtype_album ?>
		<?cs set:get_reply_cgi_param.ret.addcgi = "http://photo.qq.com/cgi-bin/common/cgi_add_icreply"?>
		<?cs set:get_reply_cgi_param.ret.addparam = "oweruin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId
			+ "&sloc=" + qz_metadata.orgdata.albumdata.sAlbumConverId + "&refer=qzone" + "&reqfrom=" + get_fakefeeds_req.ret + "&archive=0" + "&cmtid=" + qz_metadata.opinfo.t2body.seq
			+ "&albumname=" + qz_metadata.orgdata.albumdata.sAlbumName ?>
	<?cs elif:subtype==PHOTO_subtype_batch ?>
		<?cs set:get_reply_cgi_param.ret.addcgi = "http://photo.qq.com/cgi-bin/common/cgi_add_icreply"?>
		<?cs set:get_reply_cgi_param.ret.addparam = "oweruin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId
			+ lloc_info + "&refer=qzone" + "&reqfrom=" + get_fakefeeds_req.ret + "&zz=0" + "&cmtid=" + qz_metadata.opinfo.t2body.seq
			+ "&batchId=" + qz_metadata.orgdata.albumdata.sBatchUploadId + "&cmtType=4" + "&picname=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name ?>
	<?cs /if?>
	
	<?cs #/*按subtype来组删除回复cgi*/#?>
	<?cs loop:i=0, subcount(qz_metadata.opinfo.t2body.vt3body)-1, 1 ?>	
		<?cs set:get_reply_cgi_param.ret.delcgi[i] = "http://photo.qq.com/cgi-bin/common/cgi_del_reply"?>
		<?cs set:get_reply_cgi_param.ret.delparam[i] = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId + lloc_info
			+ "&cmtid=" + qz_metadata.opinfo.t2body.seq + "&rtime_t=" + qz_metadata.opinfo.t2body.vt3body[i].ctime + "&quanquan=0&quanid=0&priv=1&refer=qzone"
			+ cmt_type + topic_name ?>
	<?cs /loop?>
<?cs /def?>

<?cs ####
	/**
	 *生成审核通过和删除的cgi和参数
	 */
?>
<?cs def:get_audit_cgi_param(subtype)?>
	<?cs #/*按subtype来组审核cgi*/#?>
	<?cs if:subtype==PHOTO_subtype_single || subtype==PHOTO_subtype_qpai ?>
		<?cs set:cmttypeinfo = "&cmtType=0" ?>
	<?cs elif:subtype==PHOTO_subtype_album ?>
		<?cs set:cmttypeinfo = "&cmtType=1" ?>
	<?cs elif:subtype==PHOTO_subtype_batch ?>
		<?cs set:cmttypeinfo = "&cmtType=4" ?>
	<?cs /if?>
	
	<?cs set:get_audit_cgi_param.ret.passcgi = "http://photo.qq.com/cgi-bin/common/cgi_audit_comment" ?>
	<?cs set:get_audit_cgi_param.ret.passparam = "uin=" + qz_metadata.orgdata.uin + "&id=" + qz_metadata.opinfo.t2body.seq + "&refer=feeds"
			+ cmttypeinfo + "&audit=1" ?>
	<?cs set:get_audit_cgi_param.ret.delcgi = "http://photo.qq.com/cgi-bin/common/cgi_audit_comment" ?>
	<?cs set:get_audit_cgi_param.ret.delparam = "uin=" + qz_metadata.orgdata.uin + "&id=" + qz_metadata.opinfo.t2body.seq + "&refer=feeds"
			+ cmttypeinfo + "&audit=0" ?>
<?cs /def?>


<?cs ####
	/**
	 *相册生成更多评论cgi和参数
	 */
?>
<?cs def:get_morecmt_cgi_param(subtype)?>
	<?cs #/*判断转载参数*/#?>
	<?cs if:subcount(qz_metadata.relybody) > 0 ?>
		<?cs set:zz_param = "&zz=1"?>
	<?cs else ?>
		<?cs set:zz_param = "&zz=0"?>
	<?cs /if?>
	
	<?cs call:get_photo_current_picindex() ?>

	<?cs #/*组装评论类型*/#?>
	<?cs #/*批次类主动(与我相关)单独逻辑*/#?>
	<?cs if:(qz_metadata.feedtype==UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype==UC_WUP_FEED_TYPE_RELATEPSV) && qz_metadata.orgdata.albumdata.iIsFromMultiFeeds!=0 ?>
		<?cs set:cmt_type = "&batchId=" + qz_metadata.orgdata.albumdata.sBatchUploadId + "&cmtType=4" ?>
	<?cs elif:subtype==PHOTO_subtype_single || subtype==PHOTO_subtype_qpai ?>
		<?cs set:cmt_type = "&cmtType=0"?>
	<?cs elif:subtype==PHOTO_subtype_album ?>
		<?cs set:cmt_type = "&cmtType=1"?>
	<?cs elif:subtype==PHOTO_subtype_batch ?>
		<?cs set:cmt_type = "&batchId=" + qz_metadata.orgdata.albumdata.sBatchUploadId + "&cmtType=4" ?>
	<?cs /if?>
	<?cs call:get_fakefeeds_req(qz_metadata.orgdata.subtype,qz_metadata.feedtype) ?>
	
	<?cs set:get_morecmt_cgi_param.ret.morecgi = "http://photo.qq.com/cgi-bin/common/cgi_add_piccomment" ?>
	<?cs set:get_morecmt_cgi_param.ret.moreparam = "uin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId + "&forumindex=0&lloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&sloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + cmt_type + "&refer=qzone&privacy=" + qz_metadata.orgdata.albumdata.iPrivacy + zz_param + "&reqfrom=" + get_fakefeeds_req.ret + "&isfakereq=1&picname=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name ?>
	
	<?cs #/*批次和多图主动相关feeds要跳批次页*/#?>
	<?cs if:subtype==PHOTO_subtype_batch || qz_metadata.meta.typeid==FEED_TYPE_ALBUM || qz_metadata.meta.typeid==FEED_ABOUT_ALBUM?>
		<?cs call:get_batch_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId,qz_metadata.orgdata.albumdata.sBatchUploadId)?>
		<?cs set:get_morecmt_cgi_param.ret.moreurl = get_batch_url.ret ?>
	<?cs elif:subtype==PHOTO_subtype_single || subtype==PHOTO_subtype_qpai ?>
		<?cs call:get_photo_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId,qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid)?>
		<?cs set:get_morecmt_cgi_param.ret.moreurl = get_photo_url.ret ?>
	<?cs elif:subtype==PHOTO_subtype_album ?>
		<?cs call:get_album_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId)?>
		<?cs set:get_morecmt_cgi_param.ret.moreurl = get_album_url.ret ?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *相册生成更多回复cgi和参数
	 */
?>
<?cs def:get_morereply_cgi_param(subtype)?>

	<?cs #/*组装评论类型*/#?>
	<?cs if:subtype==PHOTO_subtype_single || subtype==PHOTO_subtype_qpai ?>
		<?cs set:cmt_type = "&cmtType=0"?>
	<?cs elif:subtype==PHOTO_subtype_album ?>
		<?cs set:cmt_type = "&cmtType=1"?>
	<?cs elif:subtype==PHOTO_subtype_batch ?>
		<?cs set:cmt_type = "&batchId=" + qz_metadata.orgdata.albumdata.sBatchUploadId + "&cmtType=4" ?>
	<?cs /if?>
	
	<?cs #/*判断转载参数*/#?>
	<?cs if:subcount(qz_metadata.relybody) > 0 ?>
		<?cs set:zz_param = "&zz=1"?>
	<?cs else ?>
		<?cs set:zz_param = "&zz=0"?>
	<?cs /if?>
	
	<?cs call:get_photo_current_picindex() ?>
	<?cs call:get_fakefeeds_req(qz_metadata.orgdata.subtype,qz_metadata.feedtype) ?>
	
	<?cs set:get_morereply_cgi_param.ret.morecgi = "http://photo.qq.com/cgi-bin/common/cgi_add_icreply" ?>
	<?cs set:get_morereply_cgi_param.ret.moreparam = "oweruin=" + qz_metadata.orgdata.uin + "&albumid=" + qz_metadata.orgdata.albumdata.sAlbumId + "&lloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "&sloc=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + cmt_type + zz_param + "&reqfrom=" + get_fakefeeds_req.ret + "&cmtid=" + qz_metadata.opinfo.t2body.seq + "&isfakereq=1&picname=" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].name ?>

	<?cs #/*纯评论回复被动，动作类型决定跳转类型*/#?>
	<?cs if:subtype==PHOTO_subtype_batch?>
		<?cs call:get_batch_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId,qz_metadata.orgdata.albumdata.sBatchUploadId)?>
		<?cs set:get_morereply_cgi_param.ret.moreurl = get_batch_url.ret ?>
	<?cs elif:subtype==PHOTO_subtype_single || subtype==PHOTO_subtype_qpai ?>
		<?cs call:get_photo_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId,qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid)?>
		<?cs set:get_morereply_cgi_param.ret.moreurl = get_photo_url.ret ?>
	<?cs elif:subtype==PHOTO_subtype_album ?>
		<?cs call:get_album_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId)?>
		<?cs set:get_morereply_cgi_param.ret.moreurl = get_album_url.ret ?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *相册生成图片浮层的参数(新规则传偏移值)
	 */
?>

<?cs def:get_photo_popup_param(index)?>
	<?cs set:get_photo_popup_param.ret = qz_metadata.orgdata.uin + "|" + qz_metadata.orgdata.albumdata.sAlbumId + "|"
			+ qz_metadata.orgdata.itemdata[index].itemid + "|" + qz_metadata.orgdata.itemdata[index].picinfo[2].url  ?>
<?cs /def?>


<?cs ####
	/**
	 *相册生成图片浮层的参数(新规则传图片参数,评论小图标使用)
	 */
?>

<?cs def:get_photo_popup_paramByPicInfo(picid,picurl)?>
	<?cs set:get_photo_popup_paramByPicInfo.ret = qz_metadata.orgdata.uin + "|" + qz_metadata.orgdata.albumdata.sAlbumId + "|"
			+ picid + "|" + picurl  ?>
<?cs /def?>

<?cs ####
	/**
	 *相册单条评论里当前评论图片的下标，供缩略小图使用
	 */
?>
<?cs def:get_photo_comment_picIndex(picid)?>
	<?cs loop: i = 0, subcount(qz_metadata.orgdata.itemdata) - 1, 1?>
		<?cs if:picid== qz_metadata.orgdata.itemdata[i].itemid ?>
			<?cs set:get_photo_comment_picIndex.index = i ?>
		<?cs /if?>
	<?cs /loop?>
<?cs /def?>

<?cs ####
	/**
	 *相册单图调整t2count数
	 */
?>
<?cs def:modify_pav_t2count()?>
	<?cs call:get_photo_current_picindex() ?>
	<?cs if:(qz_metadata.meta.typeid==FEED_TYPE_PHOTOCMT ||  qz_metadata.meta.typeid==FEED_TYPE_AT_PHOTO)
		&& qz_metadata.orgdata.albumdata.iIsFromMultiFeeds!=0 && qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].extendinfo.t2count!=0?>
		<?cs set:qz_metadata.vt2count = qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].extendinfo.t2count ?>
	<?cs /if?>	
<?cs /def?>

