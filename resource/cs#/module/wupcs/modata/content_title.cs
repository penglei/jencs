<?cs #{/*内容区标题*/?>

<?cs #初始化 ?>
<?cs def:data_init_cntTitle()?>
	<?cs call:qfv("content", 1)?><?cs #内容标题在内容区，因此把“内容”标志置1?>
	<?cs call:i()?>
<?cs /def?>

<?cs #内容区标题默认文字 ?>
<?cs def:data_cntTitle_defaultText(text)?>
	<?cs set:_cntTitle_path = "content.title.con." + i?>
	<?cs call:data_con_txt(_cntTitle_path, text, "", 5)?>
	<?cs set:data_cntTitle_defaultText.ret.path = _cntTitle_path?>
	<?cs call:i++()?>
<?cs /def?>

<?cs #内容区标题系统提示性文字?>
<?cs def:data_cntTitle_tipTxt(text)?>
	<?cs set:_cntTitle_path = "content.title.con." + i?>
	<?cs call:data_con_txt(_cntTitle_path, text, "tip", 5)?>
	<?cs set:data_cntTitle_tipTxt.ret.path = _cntTitle_path?>
	<?cs call:i++()?>
<?cs /def?>

<?cs #内容区标题的跳转?>
<?cs def:data_cntTitle_url(text, url)?>
	<?cs set:_cntTitle_path = "content.title.con." + i?>
	<?cs call:data_con_url(_cntTitle_path, text, url, "link", 5)?>
	<?cs set:data_cntTitle_url.ret.path = _cntTitle_path?>
	<?cs call:i++()?>
<?cs /def?>

<?cs #内容区标题的用户链接?>
<?cs def:data_cntTitle_nick(uin, who, name)?>
	<?cs set:_cntTitle_path = "content.title.con." + i?>
	<?cs call:data_con_nick(_cntTitle_path, uin, who, name, "link", 5)?>
	<?cs set:data_cntTitle_url.ret.path = _cntTitle_path?>
	<?cs call:i++()?>
<?cs /def?>
<?cs #}/*内容区标题结束*/?>

<?cs #:
	/**/
	function data_cntTitle_rich(richmsgs){}
?>
<?cs def:data_cntTitle_rich(richmsgs) ?>
	<?cs set:data_cntTitle_rich_loop_count = subcount(richmsgs) ?>
	<?cs if:data_cntTitle_rich_loop_count?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs loop:j=0, data_cntTitle_rich_loop_count - 1, 1 ?>
		<?cs call:data_rich_msg("content.title.con." + i, richmsgs[j], 0, 0) ?>
		<?cs call:i++()?>
	<?cs /loop ?>
	<?cs /if?>
<?cs /def ?>
