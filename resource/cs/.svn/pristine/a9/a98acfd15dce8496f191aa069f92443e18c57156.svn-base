<?cs def:data_init_title()?>
	<?cs call:i()?>
<?cs /def?>

<?cs def:data_title_defaultTxt(text)?>
	<?cs call:data_con_txt("title.con." + i, text, "", 10)?>
	<?cs set:data_title_defaultTxt.ret.path = "title.con." + i?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_title_tipTxt(text)?>
	<?cs call:data_con_txt("title.con." + i, text, "tip", 10)?>
	<?cs set:data_title_tiptTxt.ret.path = "title.con." + i?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_title_url(text, url)?>
	<?cs call:data_con_url("title.con." + i, text, url, "link", 10)?>
	<?cs set:data_title_url.ret.path = "title.con." + i?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_title_txt_style(text, color, mr)?>
	<?cs call:data_con_txt("title.con." + i, text, color, mr)?>
	<?cs set:data_title_txt_style.ret.path = "title.con." + i?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_title_nick(uin, who, name)?>
	<?cs call:data_con_nick("title.con." + i, uin, who, name, "link", 10)?>
	<?cs set:data_title_nick.ret.path = "title.con." + i?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_title_startQuote()?>
	<?cs call:set("title.con." + i, "type", "quoteright")?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_title_endQuote()?>
	<?cs call:set("title.con." + i, "type", "quoteleft")?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_title_txtPopup(text, title, src, param, version, width, height)?>
	<?cs set:_path = "title.con." + i?>
	<?cs call:data_con_txt(_path, text, "", 10)?>
	<?cs call:data_popup(
			_path + ".action",
			title, src, param, version, width, height, "", "")?>
	<?cs set:data_title_txtPopup.ret.path = _path?>
	<?cs call:i++()?>
<?cs /def?>


<?cs #:
	/**/
	function data_title_richmsg(arguments){}
?>
<?cs def:data_title_richmsg(richmsgs) ?>
	<?cs set:data_title_richmsg_loop_count=subcount(richmsgs) - 1?>
	<?cs loop:j=0, data_title_richmsg_loop_count, 1 ?>
		<?cs call:data_rich_msg("title.con." + i, richmsgs[j], 0, 0) ?>
		<?cs call:i++()?>
	<?cs /loop ?>
<?cs /def ?>

<?cs #### {欲知为什么有title2，请参考 viewdata.xml文档?>
<?cs def:data_init_title2()?>
	<?cs set:_title2_i = 0?>
<?cs /def?>
<?cs def:_title2_i++()?>
	<?cs set:_title2_i = _title2_i + 1?>
<?cs /def?>

<?cs def:data_title2_defaultTxt(text)?>
	<?cs call:data_con_txt("title2.con." + _title2_i, text, "", 10)?>
	<?cs set:data_title_defaultTxt.ret.path = "title2.con." + _title2_i?>
	<?cs call:_title2_i++()?>
<?cs /def?>

<?cs def:data_title2_tipTxt(text)?>
	<?cs call:data_con_txt("title2.con." + _title2_i, text, "tip", 10)?>
	<?cs set:data_title_tiptTxt.ret.path = "title2.con." + _title2_i?>
	<?cs call:_title2_i++()?>
<?cs /def?>

<?cs def:data_title2_url(text, url)?>
	<?cs call:data_con_url("title2.con." + _title2_i, text, url, "link", 10)?>
	<?cs set:data_title_url.ret.path = "title2.con." + _title2_i?>
	<?cs call:_title2_i++()?>
<?cs /def?>

<?cs def:data_title2_txt_style(text, color, mr)?>
	<?cs call:data_con_txt("title2.con." + _title2_i, text, color, mr)?>
	<?cs set:data_title_txt_style.ret.path = "title2.con." + _title2_i?>
	<?cs call:_title2_i++()?>
<?cs /def?>

<?cs def:data_title2_nick(uin, who, name)?>
	<?cs call:data_con_nick("title2.con." + _title2_i, uin, who, name, "link", 10)?>
	<?cs set:data_title_nick.ret.path = "title2.con." + _title2_i?>
	<?cs call:_title2_i++()?>
<?cs /def?>

<?cs def:data_title2_richmsg(richmsgs)?>
	<?cs set:data_title_richmsg_loop_count=subcount(richmsgs) - 1?>
	<?cs loop:j=0, data_title_richmsg_loop_count, 1 ?>
		<?cs call:data_rich_msg("title2.con."+_title2_i, richmsgs[j], 0, 0) ?>
		<?cs call:_title2_i++()?>
	<?cs /loop ?>
<?cs /def?>
<?cs #}?>
