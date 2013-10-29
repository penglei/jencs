<?cs def:data_init_titletip()?>
	<?cs call:i()?>
<?cs /def?>

<?cs def:data_titletip_defaultTxt(text)?>
	<?cs call:data_con_txt("titletip.con." + i, text, "", 10)?>
	<?cs set:data_titletip_defaultTxt.ret.path = "titletip.con." + i?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_titletip_tipTxt(text)?>
	<?cs call:data_con_txt("titletip.con." + i, text, "tip", 10)?>
	<?cs set:data_titletip_tiptTxt.ret.path = "titletip.con." + i?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_titletip_url(text, url)?>
	<?cs call:data_con_url("titletip.con." + i, text, url, "link", 10)?>
	<?cs set:data_titletip_url.ret.path = "titletip.con." + i?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_titletip_txt_style(text, color, mr)?>
	<?cs call:data_con_txt("titletip.con." + i, text, color, mr)?>
	<?cs set:data_titletip_txt_style.ret.path = "titletip.con." + i?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_titletip_nick(uin, who, name)?>
	<?cs call:data_con_nick("titletip.con." + i, uin, who, name, "link", 10)?>
	<?cs set:data_titletip_nick.ret.path = "titletip.con." + i?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_titletip_startQuote()?>
	<?cs call:set("titletip.con." + i, "type", "quoteright")?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_titletip_endQuote()?>
	<?cs call:set("titletip.con." + i, "type", "quoteleft")?>
	<?cs call:i++()?>
<?cs /def?>

<?cs def:data_titletip_txtPopup(text, title, src, param, version, width, height)?>
	<?cs set:_path = "titletip.con." + i?>
	<?cs call:data_con_txt(_path, text, "", 10)?>
	<?cs call:data_popup(
			_path + ".action",
			title, src, param, version, width, height, "", "")?>
	<?cs set:data_titletip_txtPopup.ret.path = _path?>
	<?cs call:i++()?>
<?cs /def?>


<?cs #:
	/**/
	function data_titletip_richmsg(arguments){}
?>
<?cs def:data_titletip_richmsg(richmsgs) ?>
	<?cs set:data_titletip_richmsg_loop_count=subcount(richmsgs) - 1?>
	<?cs loop:j=0, data_titletip_richmsg_loop_count, 1 ?>
		<?cs call:data_rich_msg("titletip.con."+i, richmsgs[j], 0, 0) ?>
		<?cs call:i++()?>
	<?cs /loop ?>
<?cs /def ?>
