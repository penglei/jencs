<?cs ####
	/*商城内容区*/
?>

<?cs def:shangcheng_opr_custom()?>
	<?cs if:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_ACTIVE || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_V6
			|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_SHARE_ACTIVE || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_ACTIVE_TMP1
			|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_ACTIVE_TMP2 || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_SPACIAL_DRESS?>
		<?cs if:string.length(qz_metadata.orgdata.extendinfo.join_words) > 0 && string.length(qz_metadata.orgdata.extendinfo.join_url) > 0?>
				<?cs call:data_opr_url(0,qz_metadata.orgdata.extendinfo.join_words,qz_metadata.orgdata.extendinfo.join_url,"")?>
		<?cs /if?>
	<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_VIP_UPGRADE || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_VISTIOR_GIFT
			|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_COMMON_DRESS || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_7YEAR_ANNUAL?>
		<?cs if:string.length(qz_metadata.orgdata.extendinfo.sJoinText) > 0 && string.length(qz_metadata.orgdata.extendinfo.sJoinURL) > 0?>
				<?cs call:data_opr_url(0,qz_metadata.orgdata.extendinfo.sJoinText,qz_metadata.orgdata.extendinfo.sJoinURL,"")?>
		<?cs /if?>			
	<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_WEAR_HAT?>
		<?cs call:data_opr_url(0,"我也戴帽子","http://rc.qzone.qq.com/profile/qqhat","")?>
	<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_VIPSPACE_ACTIVE || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_JOIN_VIPSPACE_ACTIVE 
			|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_APPPUSH || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_DOWNLOAD_APP ?>
		<?cs if:string.length(qz_metadata.orgdata.extendinfo.sSeeMore) > 0 && string.length(qz_metadata.orgdata.extendinfo.sSeeMoreURL) > 0?>
			<?cs call:data_opr_url(0,qz_metadata.orgdata.extendinfo.sSeeMore,qz_metadata.orgdata.extendinfo.sSeeMoreURL,"")?>
		<?cs elif:string.length(qz_metadata.orgdata.extendinfo.sSeeMore) > 0 && string.length(qz_metadata.orgdata.extendinfo.sActURL) > 0?>
			<?cs call:data_opr_url(0,qz_metadata.orgdata.extendinfo.sSeeMore,qz_metadata.orgdata.extendinfo.sActURL,"")?>
		<?cs /if?>	
	<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_ACTIVITY_INVITE?>
		<?cs if:qz_metadata.orgdata.extendinfo.type == SHANGCHENG_INVITE_TYPE_INVITE?>
			<?cs call:data_opr_url(0,"接受邀请","http://qzs.qq.com/open/act/mqzone/index.html?from=BDfeed","")?>
		<?cs else?>
			<?cs call:data_opr_url(0,"立即获取抽奖码","http://qzs.qq.com/open/act/mqzone/index.html?from=FKfeed","")?>
		<?cs /if?>
	<?cs /if?>
	
	<?cs if:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_7DAYS_DRESS || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_BIGPIC_DRESS
			|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_COMMON_DRESS?>
		<?cs call:shangcheng_make_share_param()?>
		<?cs call:shangcheng_forward_btn(shangcheng_make_share_param.ret)?>
	<?cs /if?>
<?cs /def?>

<?cs def:data_shangcheng_contentbox()?>
	<?cs call:get_tuin_and_tid()?>
	<?cs set:_max_pic_index = 0?>
	
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_RELATEPSV?>
		<?cs set:_max_pic_index = subcount(qz_metadata.orgdata.itemdata) - 1?>
		<?cs if:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_7DAYS_DRESS || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_BIGPIC_DRESS ?>
			<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_GPLUS_MODE , "") ?>	
		<?cs else?>
			<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_DEFAULT , "")?>				
		<?cs /if?>
	<?cs else?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_SMALL_MODE , "")?>
		<?cs if:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_ACTIVE || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_V6
				|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_SHARE_ACTIVE || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_ACTIVE_TMP1
				|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_ACTIVE_TMP2?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
			<?cs call:data_textTitle_tipTxt("参与了")?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.activity_name, qz_metadata.orgdata.extendinfo.activity_url)?>
			<?cs call:data_textTitle_tipTxt(qz_metadata.orgdata.extendinfo.activity_desc)?>
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_HOT?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
			<?cs call:data_textTitle_tipTxt("在空间装扮了")?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.item_name, qz_metadata.orgdata.extendinfo.url_free)?>
			<?cs call:data_textTitle_tipTxt(qz_metadata.orgdata.extendinfo.hotspot_desc)?>			
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_DRESS?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
			<?cs call:data_textTitle_tipTxt("为自己换了一套")?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.topic, qz_metadata.orgdata.extendinfo.topic_url)?>
			<?cs call:data_textTitle_tipTxt(qz_metadata.orgdata.extendinfo.hotspot_desc)?>
			<?cs call:data_textTitle_tipTxt("主题的")?>
			<?cs call:data_textTitle_tipTxt("装扮，你也来换个装扮，换个心情吧！")?>	
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_VIP_UPGRADE || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_7DAYS_DRESS
				|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_BIGPIC_DRESS || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_COMMON_DRESS
				|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_7YEAR_ANNUAL || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_VISTIOR_GIFT?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
			<?cs call:data_textTitle_rich(qz_metadata.orgdata.title)?>
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_SPACIAL_DRESS?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>	
			<?cs call:data_textTitle_tipTxt("在空间商城更新了")?>
			<?cs call:data_textTitle_tipTxt("装扮")?>
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_WEAR_HAT?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>	
			<?cs call:data_textTitle_tipTxt("戴上了一顶")?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.hat_name,"http://rc.qzone.qq.com/profile/qqhat")?>
			<?cs call:data_textTitle_tipTxt("帽子，")?>
			<?cs call:data_textTitle_tipTxt(qz_metadata.orgdata.extendinfo.hat_desc)?>
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_VIPSPACE_ACTIVE?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>	
			<?cs call:data_textTitle_tipTxt("发布了优惠券：")?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.sActName,qz_metadata.orgdata.extendinfo.sActURL)?>
			<?cs call:data_textTitle_tipTxt("快来领取吧！")?>
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_JOIN_VIPSPACE_ACTIVE?>		
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>	
			<?cs call:data_textTitle_tipTxt("领取了")?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.extendinfo.orguin, USER_PLATFORM_WHO_QZONE,"")?>				
			<?cs call:data_textTitle_tipTxt("的优惠券：")?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.sActName,qz_metadata.orgdata.extendinfo.sActURL)?>
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_APPPUSH?>
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>	
			<?cs call:data_textTitle_tipTxt("推荐您使用：")?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.sActName,qz_metadata.orgdata.extendinfo.sActURL)?>
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_DOWNLOAD_APP?>		
			<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>	
			<?cs call:data_textTitle_tipTxt("下载了：")?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.extendinfo.sActName,qz_metadata.orgdata.extendinfo.sActURL)?>
		<?cs /if?>
	<?cs /if?>
	<?cs call:data_content_text(qz_metadata.orgdata.content) ?>
	
	<?cs if:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_NOTE_INVITE && qz_metadata.feedtype != UC_WUP_FEED_TYPE_ACT ?>
		<?cs if:qz_metadata.orgdata.extendinfo.sURLType == SHANGCHENG_NOTE_URL_TYPE_AGREE?>
			<?cs call:data_tieban(qz_metadata.orgdata.extendinfo.sCommitURL,qz_metadata.orgdata.extendinfo.sTiebanId,"同意并进入该共享相册") ?>		
		<?cs else?>
			<?cs call:data_tieban(qz_metadata.orgdata.extendinfo.sCommitURL,qz_metadata.orgdata.extendinfo.sTiebanId,"进入共享相册") ?>
		<?cs /if?>
		<?cs set:_param=1 ?>
		<?cs set:_param.0.key="js" ?>
		<?cs set:_param.0.value=1 ?>
		<?cs set:_param.1.key="notarget" ?>
		<?cs set:_param.1.value=1 ?>
		<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], "javascript:;", "", _param) ?>
	<?cs elif: qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_VIPSPACE_ACTIVE || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_JOIN_VIPSPACE_ACTIVE ?>
		<?cs call:set("content.coupon", "url", qz_metadata.orgdata.itemdata[0].action) ?>
		<?cs call:set("content.coupon", "pic", qz_metadata.orgdata.itemdata[0].picinfo[0].url) ?>
		<?cs call:set("content.coupon", "btn", "点击领取") ?>
		<?cs call:set("content.coupon", "height", "380") ?>
	<?cs elif: qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_APPPUSH || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_DOWNLOAD_APP?>
		<?cs call:set("content.coupon", "url", qz_metadata.orgdata.itemdata[0].action) ?>
		<?cs call:set("content.coupon", "pic", qz_metadata.orgdata.itemdata[0].picinfo[0].url) ?>
		<?cs call:set("content.coupon", "btn", "查看详情") ?>
		<?cs call:set("content.coupon", "height", "240") ?>
	<?cs else ?>
		<?cs set:_end = _max_pic_index ?>
		<?cs loop:j=0, _end, 1?>
			<?cs call:data_cntmedia_pic_urlaction(j, qz_metadata.orgdata.itemdata[j], qz_metadata.orgdata.itemdata[j].action, "", "") ?>
		<?cs /loop?>
		
		<?cs if:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_BIGPIC_DRESS ?>
			<?cs if: qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT && subcount(qz_metadata.orgdata.itemdata) > 1 ?>
				<?cs set:_param=1 ?>
				<?cs set:_param.0.key="type" ?>
				<?cs set:_param.0.value="tips" ?>
				<?cs set:_param.1.key="tips" ?>
				<?cs set:_param.1.value="更多推荐" ?>
				<?cs call:data_cntmedia_pic_urlaction(7, qz_metadata.orgdata.itemdata[1], qz_metadata.orgdata.itemdata[1].action, "", _param) ?>
			<?cs /if ?>
		<?cs /if ?>
	<?cs /if ?>

<?cs /def?>
