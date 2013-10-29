<?cs ####
	/**
	 * 输出评论前ico
	 */
?>
<?cs def:con_icon(img)?>
	<img src="<?cs var:html_encode(img.src, 1)?>"
		<?cs call:echoTextClass(img)?>
		<?cs if:img.alt?> alt="<?cs var:img.alt?>"<?cs /if?>
		<?cs if:img.name?> name="<?cs var:img.name?>"<?cs /if?>
	 />
<?cs /def?>
