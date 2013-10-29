<?cs # common#?>
<?cs set:UPP_TYPE_ACT_PHOTO = 0?>
<?cs set:UPP_TYPE_ACT_ALBUM = 4?>
<?cs set:UPP_TYPE_ACT_BATCH = 5?>
<?cs set:UPP_subtype_single = 2?>			<?cs #/*单图类*/#?>
<?cs set:UPP_subtype_batch = 9?>			<?cs #/*批次类*/#?>

<?cs set:UPP_ACTION_BATCH = "1" ?>

<?cs def:get_upp_album_url(groupcode)?>
	<?cs set:album_url = "http://qun.qzone.qq.com/group#!/" + groupcode + "/photo" ?>
	<?cs set:get_upp_album_url.ret = album_url?>
<?cs /def?>


<?cs def:get_upp_current_picindex()?>
	<?cs set:get_upp_current_picindex.ret = qz_metadata.orgdata.albumdata.extendinfo.iCurPicIndex ?>
<?cs /def?>

<?cs def:get_topicid()?>
	<?cs set:get_topicid.ret = qz_metadata.orgdata.albumdata.extendinfo["groupcode"] + "_" + qz_metadata.orgdata.albumdata.sAlbumId ?>
<?cs /def?>