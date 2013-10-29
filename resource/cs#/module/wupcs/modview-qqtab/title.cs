<?cs def:title_item(con)?>
	<?cs if:con.action.type == "popup"?>
		<?cs call:con_popup(con)?>
	<?cs else ?>
		<?cs if:con.mr == 10?>
			<?cs set:con.mr=5 ?>
		<?cs /if?>
		<?cs if:con.type == "txt"?>
			<?cs call:con_txt(con)?>
		<?cs elif:con.type == "url"?>
			<?cs call:con_url(con)?>
		<?cs elif:con.type == "nick"?>
			<?cs call:con_nick(con)?>
		<?cs elif:con.type == "quoteright"?>
			<i class="ui_ico quote_before c_tx3">“</i>
		<?cs elif:con.type == "quoteleft"?>
			<i class="ui_ico quote_after c_tx3">”</i>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs def:title_start()?>
	<div class="f_title">
<?cs /def?>

<?cs def:title_end()?>
	</div><?cs #/*endfor: .f_title*/ ?>
<?cs /def?>

<?cs def:title()?>
	<?cs call:title_start()?>
	<?cs #解析每条feed不同的title?>
	<?cs set:_end = subcount(qfv.title.con) - 1?>
	<?cs loop:i = 0, _end, 1?>
		<?cs call:title_item(qfv.title.con[i])?>
	<?cs /loop?>
	<?cs call:title_end()?>
<?cs /def?>

