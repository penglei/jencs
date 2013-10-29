<?cs ####
	/*甩一甩标题区*/
?>

<?cs def:data_shake_subtitle()?>
	<?cs if:subcount(qz_metadata.relybody) > 0?>
		<?cs call:get_userWho_platform(qz_metadata.relybody[0].platformid, qz_metadata.relybody[0].platformsubid)?>
		<?cs call:data_init_cntTitle() ?>
		<?cs call:data_cntTitle_nick(qz_metadata.relybody[0].uin, get_userWho_platform.ret, qz_metadata.relybody[0].nickname) ?>
		<?cs if:subcount(qz_metadata.relybody[0].msg) > 0?>
			<?cs call:data_cntTitle_tipTxt("转发：") ?>
			<?cs call:data_cntTitle_rich(qz_metadata.relybody[0].msg) ?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>


<?cs def:data_shake_title()?>
	<?cs call:i()?>
	<?cs if:qz_metadata.orgdata.extendinfo.appid == SHAKE_SUBAPP_COMM && qz_metadata.orgdata.subtype == SHAKE_COMM_SUBTYPE_LINK ?>
		<?cs call:data_title_tipTxt("您通过手机发送了新内容") ?>
	<?cs else ?>
		<?cs call:data_title_tipTxt("您通过手机“甩”了新内容") ?>
	<?cs /if ?>
<?cs /def?>
