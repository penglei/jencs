
<?cs def:data_app_operate()?>
	<?cs call:i()?>

	<?cs if:qz_metadata.orgdata.subtype == APP_subtype_invite?>
		<?cs call:data_opr_url(0, "接受邀请", qz_metadata.orgdata.itemdata[0].extendinfo.strAcceptURL,"") ?>
		<?cs call:data_opr_url(1, "查看更多应用", qz_metadata.orgdata.itemdata[0].extendinfo.strSeeMore,"") ?>
	<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_invite_with_mqzone?>
		<?cs call:data_opr_url(0, "接受邀请", qz_metadata.orgdata.itemdata[0].extendinfo.strAcceptURL,"") ?>
	<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_activate?>
		<?cs call:data_opr_url(0, qz_metadata.orgdata.itemdata[0].extendinfo.strAcceptText, qz_metadata.orgdata.itemdata[0].extendinfo.strAcceptURL, "") ?>
	<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_video?>
		<?cs call:data_opr_url(0, "接受邀请", qz_metadata.orgdata.itemdata[0].action,"") ?>
		<?cs call:data_opr_url(1, "观看更多影片", qz_metadata.orgdata.itemdata[0].extendinfo.sIndexUrl,"") ?>
	<?cs /if?>
<?cs /def?>
