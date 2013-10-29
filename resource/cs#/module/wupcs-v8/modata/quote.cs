<?cs def:_quote_desc_item(path, desc)?>
	<?cs call:data_rich_msg(path, desc, "", 0)?>
<?cs /def?>

<?cs def:data_quote_desc(descs)?>
	<?cs if:subcount(descs.0) > 0 ?>
		<?cs set:_end = subcount(descs) - 1?>
		<?cs loop:i=0, _end, 1?>
			<?cs call:_quote_desc_item("quote.con." + i, descs[i])?>
		<?cs /loop?>
	<?cs elif:subcount(descs) && descs.content ?>
		<?cs call:_quote_desc_item("quote.con", descs)?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * 引用组件（丢弃）
	 */
	 function data_quote(path, text, url){}
?>
<?cs def:data_quote(text)?>
	<?cs call:data_con_txt("quote.con", text, "", 0)?>
<?cs /def?>
