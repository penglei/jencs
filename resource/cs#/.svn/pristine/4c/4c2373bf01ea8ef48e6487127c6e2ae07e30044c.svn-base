<?cs ####
	/*个人资料标题区*/
?>

<?cs def:data_profile_title()?>
	<?cs call:i()?>
	<?cs call:get_tuin_and_tid()?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_RELATEPSV?>
		<?cs if: qz_metadata.orgdata.subtype == PROFILE_TYPE_UPLOAD_AVATAR?>
			<?cs call:data_title_tipTxt("更换了空间头像")?>	
		<?cs elif:qz_metadata.orgdata.subtype == PROFILE_TYPE_UPDATE_COVER?>
			<?cs call:data_title_tipTxt("我刚刚更新了手机QQ空间的背景，快来看看吧！客户端官网链接：")?>	
			<?cs call:data_title_url("http://z.qzone.com", "http://z.qzone.com")?>
		<?cs else?>
			<?cs call:data_title_tipTxt("更新了个人资料")?>	
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV?>
		<?cs call:data_title_tipTxt("评论")?>	
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?>
		<?cs call:data_title_tipTxt("回复")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV?>
		<?cs call:data_title_tipTxt("提到了我")?>	
	<?cs /if?>
<?cs /def?>
