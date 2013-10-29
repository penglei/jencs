
<?cs def:data_upp_quote()?>
	<?cs call:get_upp_current_picindex() ?>
	
	<?cs #单图类的描述展示current pic对应的描述，多图类统一使用第一张描述 ?>
	<?cs if:qz_metadata.orgdata.albumdata.extendinfo["isbatch"] == UPP_ACTION_BATCH ?>
		<?cs call:data_quote_desc(qz_metadata.orgdata.itemdata[0].desc)?>
	<?cs else ?>
		<?cs call:data_quote_desc(qz_metadata.orgdata.itemdata[get_upp_current_picindex.ret].desc)?>
	<?cs /if?>
<?cs /def?>


<?cs ####
	/*upp内容区*/
?>
<?cs def:data_upp_contentbox()?>
	<?cs #相册当前操作的图片下标 ?>
	<?cs call:get_upp_current_picindex() ?>

	<?cs call:data_content_text(qz_metadata.orgdata.content) ?>
	<?cs #图片展示规格 ?>
	<?cs if: qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
		<?cs if:qz_metadata.orgdata.albumdata.extendinfo["isbatch"] == UPP_ACTION_BATCH
				&& string.length(qz_metadata.orgdata.albumdata.extendinfo.iCurPicIndex)==0 ?>
			<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE_SMALL , "") ?>
		<?cs else ?>
			<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_DEFAULT , "") ?>
		<?cs /if ?>
	<?cs else ?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_GRID_MODE , "") ?>
	<?cs /if ?>

	<?cs set:_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
	<?cs if:qz_metadata.meta.feedstype != UC_WUP_FEEDSTYPE_PSV &&qz_metadata.orgdata.albumdata.iPicNum > 9?><?cs #必须大于9才展示?>
		<?cs call:data_extendinfo_picnum(qz_metadata.orgdata.albumdata.iPicNum)?>
	<?cs /if?>
	
	<?cs call:data_content_text(qz_metadata.orgdata.content) ?>
	
	<?cs call:get_topicid()?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ?>
		<?cs if:qz_metadata.orgdata.albumdata.extendinfo["isbatch"] == UPP_ACTION_BATCH
				&& string.length(qz_metadata.orgdata.albumdata.extendinfo.iCurPicIndex)==0  ?>
			<?cs set:_psv_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
			<?cs if:_psv_end > 2 ?>
				<?cs set:_psv_end = 2?>
			<?cs /if?>
			<?cs loop:j=0, _psv_end, 1?>
				<?cs call:data_cntmedia_pic_popup(j, qz_metadata.orgdata.itemdata[j], "", "/qzone/photo/zone/icenter_popup.html", "") ?>			
				<?cs #新版浮层需要的参数 ?>
				<?cs set:_actionpath = data_cntmedia_pic_popup.ret + ".action"?>
				<?cs call:data_popup_add_attr(_actionpath, "topicid", get_topicid.ret) ?>
				<?cs call:data_popup_add_attr(_actionpath, "pickey", qz_metadata.orgdata.itemdata[j].itemid) ?>
				<?cs call:data_popup_add_attr(_actionpath, "appid", 421) ?>
				<?cs call:data_popup_add_attr(_actionpath, "imagesrc", qz_metadata.orgdata.itemdata[j].picinfo[3].url) ?>
			<?cs /loop?>
		<?cs else ?>
			<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[get_upp_current_picindex.ret], "", "/qzone/photo/zone/icenter_popup.html", "") ?>		
			<?cs #新版浮层需要的参数 ?>
			<?cs set:_actionpath = data_cntmedia_pic_popup.ret + ".action"?>
			<?cs call:data_popup_add_attr(_actionpath, "topicid", get_topicid.ret) ?>
			<?cs call:data_popup_add_attr(_actionpath, "pickey", qz_metadata.orgdata.itemdata[get_upp_current_picindex.ret].itemid) ?>
			<?cs call:data_popup_add_attr(_actionpath, "appid", 421) ?>
			<?cs call:data_popup_add_attr(_actionpath, "imagesrc", qz_metadata.orgdata.itemdata[get_upp_current_picindex.ret].picinfo[3].url) ?>			
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV ||
				qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ||
				qz_metadata.feedtype==UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY ?>
		<?cs if:qz_metadata.orgdata.albumdata.extendinfo["isbatch"] == UPP_ACTION_BATCH ?>
			<?cs set:_psv_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
			<?cs if:_psv_end>2 ?>
				<?cs set:_psv_end = 2?>
			<?cs /if?>
			<?cs loop:j=0, _psv_end, 1?>
				<?cs call:data_cntmedia_pic_popup(j, qz_metadata.orgdata.itemdata[j], "", "/qzone/photo/zone/icenter_popup.html", "") ?>
				<?cs call:data_popup_add_attr(_actionpath, "topicid", get_topicid.ret) ?>
				<?cs call:data_popup_add_attr(_actionpath, "pickey", qz_metadata.orgdata.itemdata[j].itemid) ?>
				<?cs call:data_popup_add_attr(_actionpath, "appid", 421) ?>
				<?cs call:data_popup_add_attr(_actionpath, "imagesrc", qz_metadata.orgdata.itemdata[j].picinfo[3].url) ?>					
			<?cs /loop?>
		<?cs else ?>
			<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[get_upp_current_picindex.ret], "", "/qzone/photo/zone/icenter_popup.html", "") ?>
			<?cs call:data_popup_add_attr(_actionpath, "topicid", get_topicid.ret) ?>
			<?cs call:data_popup_add_attr(_actionpath, "pickey", qz_metadata.orgdata.itemdata[get_upp_current_picindex.ret].itemid) ?>
			<?cs call:data_popup_add_attr(_actionpath, "appid", 421) ?>
			<?cs call:data_popup_add_attr(_actionpath, "imagesrc", qz_metadata.orgdata.itemdata[get_upp_current_picindex.ret].picinfo[3].url) ?>				
		<?cs /if?>
	<?cs else ?>
		<?cs #共XX张图 ?>
		<?cs call:data_extendinfo_picnum(qz_metadata.orgdata.albumdata.iPicNum)?>
		<?cs if:qz_metadata.orgdata.albumdata.extendinfo["isbatch"] == UPP_ACTION_BATCH ?>
			<?cs loop:j=0, _end, 1?>
				<?cs call:data_cntmedia_pic_popup(j, qz_metadata.orgdata.itemdata[j], "", "/qzone/photo/zone/icenter_popup.html", "") ?>				
				<?cs #新版浮层需要的参数 ?>
				<?cs set:_actionpath = data_cntmedia_pic_popup.ret + ".action"?>
				<?cs call:data_popup_add_attr(_actionpath, "topicid", get_topicid.ret) ?>
				<?cs call:data_popup_add_attr(_actionpath, "pickey", qz_metadata.orgdata.itemdata[j].itemid) ?>
				<?cs call:data_popup_add_attr(_actionpath, "appid", 421) ?>
				<?cs call:data_popup_add_attr(_actionpath, "imagesrc", qz_metadata.orgdata.itemdata[j].picinfo[3].url) ?>				
			<?cs /loop?>
		<?cs else ?>
			<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[get_upp_current_picindex.ret], "", "/qzone/photo/zone/icenter_popup.html", "") ?>			
			<?cs #新版浮层需要的参数 ?>
			<?cs set:_actionpath = data_cntmedia_pic_popup.ret + ".action"?>
			<?cs call:data_popup_add_attr(_actionpath, "topicid", get_topicid.ret) ?>
			<?cs call:data_popup_add_attr(_actionpath, "pickey", qz_metadata.orgdata.itemdata[get_upp_current_picindex.ret].itemid) ?>	
			<?cs call:data_popup_add_attr(_actionpath, "appid", 421) ?>
			<?cs call:data_popup_add_attr(_actionpath, "imagesrc", qz_metadata.orgdata.itemdata[get_upp_current_picindex.ret].picinfo[3].url) ?>			
		<?cs /if?>
	<?cs /if?>
<?cs /def?>
