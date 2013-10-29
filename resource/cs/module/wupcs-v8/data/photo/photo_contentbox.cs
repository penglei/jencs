<?cs ####
	/*是否圈人*/
?>
<?cs def:data_photo_has_markuser()?>
	<?cs if:qz_metadata.orgdata.extendinfo.Quan_iQuanedUin ?>
		<?cs set:data_photo_has_markuser.ret = 1 ?>
	<?cs /if ?>
<?cs /def?>

<?cs ####
	/*亲子相册*/
?>
<?cs def:data_photo_lovekid() ?>
	<?cs if:string.length(qz_metadata.orgdata.albumdata.extendinfo.sAge) >0  && string.length(qz_metadata.orgdata.albumdata.iPicNum)>0 ?>
		<?cs set:qfv.extend = 1 ?>
		<?cs set:qfv.extend.extendinfo = qz_metadata.orgdata.albumdata.extendinfo.sAge ?>
		<?cs set:qfv.extend.url =  "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
						+ qz_metadata.orgdata.albumdata.sAlbumId ?> 
		<?cs set:qfv.extend.num = qz_metadata.orgdata.albumdata.iPicNum ?>
		<?cs set:qfv.extend.albumName = qz_metadata.orgdata.albumdata.sAlbumName ?>
		<?cs set:qfv.extend.type = qz_metadata.orgdata.albumdata.iAnonymity ?>
	<?cs /if ?>
<?cs /def ?>

<?cs ####
	/*旅游相册*/
?>
<?cs def:data_photo_travel() ?>
		<?cs set:qfv.extend = 1 ?>
		<?cs set:qfv.extend.albumName = qz_metadata.orgdata.albumdata.sAlbumName?>
		<?cs set:qfv.extend.url =  "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/photo/" 
						+ qz_metadata.orgdata.albumdata.sAlbumId ?> 
		<?cs set:qfv.extend.type = qz_metadata.orgdata.albumdata.iAnonymity ?>
<?cs /def ?>

<?cs ####
	/*相册内容区*/
?>
<?cs def:data_photo_contentbox()?>
	<?cs #相册当前操作的图片下标 ?>
	<?cs call:get_photo_current_picindex() ?>

	<?cs call:data_content_text(qz_metadata.orgdata.content) ?>

	<?cs set:_end = subcount(qz_metadata.orgdata.itemdata) - 1?>

	<?cs if:qz_metadata.feedtype==UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype==UC_WUP_FEED_TYPE_RELATEPSV?>
		<?cs #:亲子相册入口 ?>
		<?cs if:qz_metadata.orgdata.albumdata.iAnonymity == ALBUM_BABY ?>
			<?cs call:data_photo_lovekid() ?>
		<?cs #:旅游相册入口 ?>
		<?cs elif:qz_metadata.orgdata.albumdata.iAnonymity == ALBUM_TRAVEL ?>
			<?cs call:data_photo_travel() ?>
		<?cs /if ?>
	<?cs /if ?>
	<?cs #图片展示规格 ?>
	<?cs if:qz_metadata.meta.typeid == FEED_TYPE_INDIVALBUM ?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_DEFAULT , "") ?>
	<?cs elif:qz_metadata.feedtype==UC_WUP_FEED_TYPE_NEWCOMMENT || qz_metadata.meta.typeid==FEED_TYPE_PHOTO || qz_metadata.meta.typeid==FEED_ABOUT_PHOTO ?>
		<?cs #由于9宫格的bug,单图主动必须继续选G+模式 ?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GPLUS_MODE , "") ?>
	<?cs elif:qz_metadata.feedtype==UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype==UC_WUP_FEED_TYPE_RELATEPSV?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE , "") ?>
	<?cs else ?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND || qz_metadata.scope == SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
			<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE , "") ?>
		<?cs else ?>
			<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE_SMALL, "") ?>
			<?cs else ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_DEFAULT , "") ?>
			<?cs /if ?>
		<?cs /if ?>
	<?cs /if?>

	<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT_NOTIFYPSV ?>
		<?cs call:data_extendinfo_picnum(qz_metadata.orgdata.albumdata.iPicNum)?>
	<?cs /if ?>

	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV ||
			qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ||
			qz_metadata.feedtype == UC_WUP_FEEDS_TYPE_SHARETOME ||
			qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT ||
			qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT_NOTIFYPSV
	?>
		<?cs call:data_photo_has_markuser()?>
		<?cs if:qz_metadata.orgdata.subtype == PHOTO_subtype_batch ||
				qz_metadata.orgdata.subtype==PHOTO_subtype_album ||
				((qz_metadata.meta.typeid == FEED_TYPE_FRI_PRIV_PASSIVE||qz_metadata.meta.typeid == FEED_TYPE_ZZ_MULTI_PASSIVE)&& 
					qz_metadata.orgdata.albumdata.iIsFromMultiFeeds!=0) ?>
			<?cs set:_psv_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
			<?cs if:_psv_end > 2 ?>
				<?cs set:_psv_end = 2?>
			<?cs /if?>
			<?cs loop:j=0, _psv_end, 1?>
				<?cs call:get_photo_popup_param(j) ?>
				<?cs call:data_cntmedia_pic_popup(j, qz_metadata.orgdata.itemdata[j], get_photo_popup_param.ret, "/qzone/photo/zone/icenter_popup.html", "") ?>
				<?cs #新版浮层需要的参数 ?>
				<?cs set:_actionpath = data_cntmedia_pic_popup.ret + ".action"?>
				<?cs #originurl为图片原图的url，和后台约定放到下标为4的位置 ?>
				<?cs set:_originurl = qz_metadata.orgdata.itemdata[j].picinfo.4.url + "|" + qz_metadata.orgdata.itemdata[j].picinfo.4.width + "|" + qz_metadata.orgdata.itemdata[j].picinfo.4.height?>
				<?cs call:data_popup_add_attr(_actionpath, "originurl", _originurl) ?>
				<?cs call:data_popup_add_attr(_actionpath, "topicid", qz_metadata.orgdata.albumdata.sAlbumId) ?>
				<?cs call:data_popup_add_attr(_actionpath, "pickey", qz_metadata.orgdata.itemdata[j].itemid) ?>
			<?cs /loop?>
		<?cs elif:data_photo_has_markuser.ret ?>
			<?cs call:get_photo_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId,qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid) ?>
			<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret], get_photo_url.ret, "", "") ?>
		<?cs else ?>
			<?cs call:get_photo_popup_param(get_photo_current_picindex.ret) ?>
			<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret], get_photo_popup_param.ret, "/qzone/photo/zone/icenter_popup.html", "") ?>
			<?cs #新版浮层需要的参数 ?>
			<?cs set:_actionpath = data_cntmedia_pic_popup.ret + ".action"?>
			<?cs #originurl为图片原图的url，和后台约定放到下标为4的位置 ?>
			<?cs set:_originurl = qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].picinfo.4.url + "|" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].picinfo.4.width + "|" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].picinfo.4.height?>
			<?cs call:data_popup_add_attr(_actionpath, "originurl", _originurl) ?>
			<?cs call:data_popup_add_attr(_actionpath, "topicid", qz_metadata.orgdata.albumdata.sAlbumId) ?>
			<?cs call:data_popup_add_attr(_actionpath, "pickey", qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid) ?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV ||
				qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ||
				qz_metadata.feedtype==UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY ?>
		<?cs if:qz_metadata.meta.typeid==FEED_TYPE_AT_BATCH || qz_metadata.meta.typeid==FEED_TYPE_AT_ALBUM ?>
			<?cs set:_psv_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
			<?cs if:_psv_end>2 ?>
				<?cs set:_psv_end = 2?>
			<?cs /if?>
			<?cs loop:j=0, _psv_end, 1?>
				<?cs call:get_photo_popup_param(j) ?>
				<?cs call:data_cntmedia_pic_popup(j, qz_metadata.orgdata.itemdata[j], get_photo_popup_param.ret, "/qzone/photo/zone/icenter_popup.html", "") ?>
			<?cs /loop?>
		<?cs else ?>
			<?cs call:get_photo_popup_param(get_photo_current_picindex.ret) ?>
			<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret], get_photo_popup_param.ret, "/qzone/photo/zone/icenter_popup.html", "") ?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_NEWCOMMENT ?>
		<?cs #共XX张图 ?>
		<?cs call:data_extendinfo_picnum(qz_metadata.orgdata.albumdata.iPicNum)?>
		<?cs call:get_photo_popup_param(get_photo_current_picindex.ret) ?>
		<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret], get_photo_popup_param.ret, "/qzone/photo/zone/icenter_popup.html", "") ?>
		<?cs #新版浮层需要的参数 ?>
		<?cs set:_actionpath = data_cntmedia_pic_popup.ret + ".action"?>
		<?cs #originurl为图片原图的url，和后台约定放到下标为4的位置 ?>
			<?cs set:_originurl = qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].picinfo.4.url + "|" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].picinfo.4.width + "|" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].picinfo.4.height?>
		<?cs call:data_popup_add_attr(_actionpath, "originurl", _originurl) ?>
		<?cs call:data_popup_add_attr(_actionpath, "topicid", qz_metadata.orgdata.albumdata.sAlbumId) ?>
		<?cs call:data_popup_add_attr(_actionpath, "pickey", qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid) ?>
	<?cs else ?>
		<?cs #共XX张图 ?>
		<?cs if:qz_metadata.meta.typeid != FEED_TYPE_INDIVALBUM ?>
			<?cs call:data_extendinfo_picnum(qz_metadata.orgdata.albumdata.iPicNum)?>
		<?cs /if?>
		<?cs #主动feeds的话，单张操作非批次，则主动只展示一张图 ?>
		<?cs if:qz_metadata.meta.typeid == FEED_TYPE_INDIVALBUM ?>
			<?cs call:get_indivalbum_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.bgid)?>
			<?cs call:get_album_url(qz_metadata.orgdata.uin,qz_metadata.orgdata.albumdata.sAlbumId)?>
			<?cs set:indivdata.picinfo.0.url = get_indivalbum_url.ret?>
			<?cs call:data_cntmedia_pic_urlaction(0, indivdata, get_album_url.ret, "", "") ?>
		<?cs elif:(qz_metadata.orgdata.subtype==PHOTO_subtype_single && qz_metadata.orgdata.albumdata.iIsFromMultiFeeds==0) || qz_metadata.meta.typeid==FEED_ABOUT_PHOTO?>
			<?cs call:get_photo_popup_param(get_photo_current_picindex.ret) ?>
			<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret], get_photo_popup_param.ret, "/qzone/photo/zone/icenter_popup.html", "") ?>
			<?cs #新版浮层需要的参数 ?>
			<?cs set:_actionpath = data_cntmedia_pic_popup.ret + ".action"?>
			<?cs #originurl为图片原图的url，和后台约定放到下标为4的位置 ?>
			<?cs set:_originurl = qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].picinfo.4.url + "|" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].picinfo.4.width + "|" + qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].picinfo.4.height?>
			<?cs call:data_popup_add_attr(_actionpath, "originurl", _originurl) ?>
			<?cs call:data_popup_add_attr(_actionpath, "topicid", qz_metadata.orgdata.albumdata.sAlbumId) ?>
			<?cs call:data_popup_add_attr(_actionpath, "pickey", qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid) ?>
		<?cs else ?>
			<?cs loop:j=0, _end, 1?>
				<?cs call:get_photo_popup_param(j) ?>
				<?cs call:data_cntmedia_pic_popup(j, qz_metadata.orgdata.itemdata[j], get_photo_popup_param.ret, "/qzone/photo/zone/icenter_popup.html", "") ?>
				<?cs #新版浮层需要的参数 ?>
				<?cs set:_actionpath = data_cntmedia_pic_popup.ret + ".action"?>
				<?cs #originurl为图片原图的url，和后台约定放到下标为4的位置 ?>
				<?cs set:_originurl = qz_metadata.orgdata.itemdata[j].picinfo.4.url + "|" + qz_metadata.orgdata.itemdata[j].picinfo.4.width + "|" + qz_metadata.orgdata.itemdata[j].picinfo.4.height?>
				<?cs call:data_popup_add_attr(_actionpath, "originurl", _originurl) ?>
				<?cs call:data_popup_add_attr(_actionpath, "topicid", qz_metadata.orgdata.albumdata.sAlbumId) ?>
				<?cs call:data_popup_add_attr(_actionpath, "pickey", qz_metadata.orgdata.itemdata[j].itemid) ?>
			<?cs /loop?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>
