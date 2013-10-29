<?cs set:i = 0?><?cs #/*先初始化一次，防止cs引擎使用未定义的变量挂掉*/?>
<?cs def:i()?>
	<?cs set:i = 0?>
<?cs /def?>
<?cs def:i++()?>
	<?cs set:i = i + 1?>
<?cs /def?>
<?cs def:i--()?>
	<?cs set:i = i - 1?>
<?cs /def?>

<?cs ####
	/**
	 *生成主贴用户数据
	 */
?>
<?cs def:datag_main_user(path, color, mr, prefix)?>
	<?cs call:data_con_nick(
			path,
			qz_metadata.orgdata.uin,
			qz_metadata.orgdata.platformid,
			qz_metadata.orgdata.nickname,
			color,
			mr)?>
	<?cs if:prefix ?>
		<?cs call:data_nick_addprefix(path, prefix)?>
	<?cs /if?>
<?cs /def?>

<?cs #{//全局变量 ?>
	<?cs set:g_orgdata_item_count = subcount(qz_metadata.orgdata.itemdata) ?>
<?cs #}?>

<?cs with:b = qz_metadata.meta.bitmap?>
<?cs if:(bitmap_value_ex(b, 5, 1) ||
		 bitmap_value_ex(b, 7, 1) ||
		 bitmap_value_ex(b, 52, 1)) ?>
	<?cs set:g_qz_is_auth = 1?>
<?cs else ?>
	<?cs set:g_qz_is_auth = 0?>
<?cs /if?>
<?cs /with?>

