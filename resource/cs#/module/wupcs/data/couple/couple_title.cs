<?cs ####
	/*分享标题区*/
?>

<?cs def:data_couple_title()?>
	<?cs call:i()?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_RELATEPSV ?>
		<?cs if:qz_metadata.orgdata.subtype == COUPLE_srctype_mood ?>
			<?cs call:data_title_tipTxt("发表爱情记录：")?>
			<?cs call:data_title_richmsg(qz_metadata.orgdata.content)?>
			<?cs call:data_title_url(qz_metadata.orgdata.srcurl, qz_metadata.orgdata.srcurl) ?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("情侣相册")?>
			<?cs set:album_url = "http://rc.qzone.qq.com/"+ COUPLE_SPACE_OPENID +"?app_mdl=album&app_uin="
				+ qz_metadata.orgdata.uin + "&app_aid=" + qz_metadata.orgdata.albumdata.sAlbumId ?>
			<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, album_url) ?>
			<?cs call:data_title_tipTxt("更新了照片")?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV?>
		<?cs if:qz_metadata.orgdata.subtype == COUPLE_srctype_mood ?>
			<?cs call:data_title_tipTxt("评论爱情记录")?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("评论情侣相片")?>

			<?cs if:subcount(qz_metadata.orgdata.itemdata) >0 ?>
				<?cs set:pic_url = "http://rc.qzone.qq.com/"+ COUPLE_SPACE_OPENID +"?app_mdl=album&app_uin="
					+ qz_metadata.orgdata.uin + "&app_aid=" + qz_metadata.orgdata.albumdata.sAlbumId
					+ "&app_pid=" + qz_metadata.orgdata.itemdata[0].itemid?>
				<?cs call:data_title_url(qz_metadata.orgdata.itemdata[0].name, pic_url) ?>
			<?cs else ?>
				<?cs set:album_url = "http://rc.qzone.qq.com/"+ COUPLE_SPACE_OPENID +"?app_mdl=album&app_uin="
					+ qz_metadata.orgdata.uin + "&app_aid=" + qz_metadata.orgdata.albumdata.sAlbumId ?>
				<?cs call:data_title_url("", album_url) ?>
			<?cs /if?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?>
		<?cs if:qz_metadata.orgdata.subtype == COUPLE_srctype_mood ?>
			<?cs call:data_title_tipTxt("回复爱情记录")?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("回复情侣相片")?>

			<?cs if:subcount(qz_metadata.orgdata.itemdata) >0 ?>
				<?cs set:pic_url = "http://rc.qzone.qq.com/"+ COUPLE_SPACE_OPENID +"?app_mdl=album&app_uin="
					+ qz_metadata.orgdata.uin + "&app_aid=" + qz_metadata.orgdata.albumdata.sAlbumId
					+ "&app_pid=" + qz_metadata.orgdata.itemdata[0].itemid?>
				<?cs call:data_title_url(qz_metadata.orgdata.itemdata[0].name, pic_url) ?>
			<?cs else ?>
				<?cs set:album_url = "http://rc.qzone.qq.com/"+ COUPLE_SPACE_OPENID +"?app_mdl=album&app_uin="
					+ qz_metadata.orgdata.uin + "&app_aid=" + qz_metadata.orgdata.albumdata.sAlbumId ?>
				<?cs call:data_title_url("", album_url) ?>
			<?cs /if?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY?>
		<?cs if:qz_metadata.orgdata.subtype == COUPLE_srctype_mood ?>
			<?cs call:data_title_tipTxt("在爱情记录回复中提到我")?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("在情侣相片回复中提到我")?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM?>
		<?cs if:qz_metadata.orgdata.subtype == COUPLE_srctype_mood ?>
			<?cs call:data_title_tipTxt("在爱情记录评论中提到我")?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("在情侣相片评论中提到我")?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT_NOTIFYPSV?>
		<?cs if:qz_metadata.orgdata.subtype == COUPLE_srctype_mood ?>
			<?cs call:data_title_tipTxt("对我发表爱情记录：")?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("更新我们的情侣相册")?>
			<?cs set:pic_url = "http://rc.qzone.qq.com/"+ COUPLE_SPACE_OPENID +"?app_mdl=album&app_uin="
				+ qz_metadata.orgdata.uin + "&app_aid=" + qz_metadata.orgdata.albumdata.sAlbumId
				+ "&app_pid=" + qz_metadata.orgdata.itemdata[0].itemid?>
			<?cs call:data_title_url(qz_metadata.orgdata.itemdata[0].name, pic_url) ?>
		<?cs /if?>
	<?cs else ?>
		<?cs #### /*call:data_title_tipTxt("转发：")*/?>
	<?cs /if?>
<?cs /def?>
