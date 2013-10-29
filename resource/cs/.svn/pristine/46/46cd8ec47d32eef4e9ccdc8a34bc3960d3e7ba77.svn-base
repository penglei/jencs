<?cs ####
	/*upp标题区*/
?>

<?cs def:data_upp_title()?>
	<?cs call:i()?>
	<?cs call:get_tuin_and_tid()?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_RELATEPSV?>
		<?cs call:data_title_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
		<?cs set:data_desc = "上传" + qz_metadata.orgdata.albumdata.iMultiUpNumber + "张照片到群相册:" ?>
		<?cs call:data_title_tipTxt(data_desc) ?>
		<?cs call:get_upp_album_url(qz_metadata.orgdata.albumdata.extendinfo["groupcode"]) ?>
		<?cs call:data_title_url(qz_metadata.orgdata.albumdata.sAlbumName, get_upp_album_url.ret) ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV?>
		<?cs call:data_title_tipTxt("评论了我群相册的照片")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM?>
		<?cs call:data_title_tipTxt("在群相册的照片提到我")?>
	<?cs /if?>
<?cs /def?>