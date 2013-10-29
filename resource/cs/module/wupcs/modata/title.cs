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
		<?cs call:data_rich_msg("title.con."+i, richmsgs[j], 0, 0) ?>
		<?cs call:i++()?>
	<?cs /loop ?>
<?cs /def ?>
