<?cs ####
	/*商城标题区*/
?>

<?cs def:data_shangcheng_title()?>
	<?cs call:i()?>
	<?cs call:get_tuin_and_tid()?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_RELATEPSV ?>
		<?cs if:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_ACTIVE || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_V6
				|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_SHARE_ACTIVE || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_ACTIVE_TMP1
				|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_ACTIVE_TMP1 || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_ACTIVE_TMP2 ?>
			<?cs call:data_title_tipTxt("参与了")?>	
			<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.activity_name, qz_metadata.orgdata.extendinfo.activity_url)?>
			<?cs call:data_title_tipTxt(qz_metadata.orgdata.extendinfo.activity_desc)?>	
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_HOT?>
			<?cs call:data_title_tipTxt("在空间装扮了")?>
			<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.item_name, qz_metadata.orgdata.extendinfo.url_free)?>
			<?cs call:data_title_tipTxt(qz_metadata.orgdata.extendinfo.hotspot_desc)?>	
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_DRESS?>
			<?cs call:data_title_tipTxt("为自己换了一套")?>	
			<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.topic, qz_metadata.orgdata.extendinfo.topic_url)?>
			<?cs call:data_title_tipTxt("主题的")?>	
			<?cs call:data_title_tipTxt("装扮，你也来换个装扮，换个心情吧！")?>
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_VIP_UPGRADE || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_7DAYS_DRESS
				|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_BIGPIC_DRESS || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_COMMON_DRESS
				|| qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_7YEAR_ANNUAL || qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_VISTIOR_GIFT ?>
			<?cs call:data_title_richmsg(qz_metadata.orgdata.title)?>
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_SPACIAL_DRESS?>
			<?cs call:data_title_tipTxt("在空间商城更新了")?>	
			<?cs call:data_title_tipTxt("装扮")?>
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_WEAR_HAT?>
			<?cs call:data_title_tipTxt("戴上了一顶")?>	
			<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.hat_name, "http://rc.qzone.qq.com/profile/qqhat")?>
			<?cs call:data_title_tipTxt(qz_metadata.orgdata.extendinfo.hat_desc)?>	
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_VIPSPACE_ACTIVE?>
			<?cs call:data_title_tipTxt("发布了优惠券：")?>
			<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.sActName, qz_metadata.orgdata.extendinfo.sActURL)?>			
			<?cs call:data_title_tipTxt("快来领取吧！")?>
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_JOIN_VIPSPACE_ACTIVE?>
			<?cs call:data_title_tipTxt("领取了")?>
			<?cs call:data_title_nick(qz_metadata.orgdata.extendinfo.orguin,USER_PLATFORM_WHO_QZONE,"")?>
			<?cs call:data_title_tipTxt("的优惠券：")?>
			<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.sActName, qz_metadata.orgdata.extendinfo.sActURL)?>	
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_APPPUSH?>
			<?cs call:data_title_tipTxt("推荐您安装：")?>
			<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.sActName, qz_metadata.orgdata.extendinfo.sActURL)?>	
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_DOWNLOAD_APP?>
			<?cs call:data_title_tipTxt("下载了：")?>
			<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.sActName, qz_metadata.orgdata.extendinfo.sActURL)?>	
		<?cs else?>
			<?cs call:data_title_tipTxt("装扮活动")?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV?>
		<?cs if:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_ACTIVITY_INVITE?>
			<?cs if:qz_metadata.orgdata.extendinfo.type == SHANGCHENG_INVITE_TYPE_INVITE?>
				<?cs call:data_title_tipTxt("邀请我安装")?>
				<?cs call:data_title_url("QQ空间手机版", "http://qzs.qq.com/open/act/mqzone/index.html?from=BDfeed")?>						
			<?cs elif:qz_metadata.orgdata.extendinfo.type == SHANGCHENG_INVITE_TYPE_ACCEPT?>
				<?cs call:data_title_tipTxt("接受了你的邀请，下载并登录了")?>
				<?cs call:data_title_url("QQ空间安卓最新版", "http://qzs.qq.com/open/act/mqzone/index.html?from=FKfeed")?>										
			<?cs /if?>
		<?cs elif:qz_metadata.orgdata.subtype == SHANGCHENG_TYPE_NOTE_INVITE?>
			<?cs call:data_title_tipTxt("邀请你加入共享相册：")?>
			<?cs call:data_title_tipTxt(qz_metadata.orgdata.extendinfo.sNoteName)?>
		<?cs else?>
			<?cs call:data_title_tipTxt("评论")?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?>
		<?cs call:data_title_tipTxt("回复了我")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV?>
		<?cs call:data_title_tipTxt("提到了我")?>	
	<?cs /if?>
<?cs /def?>
