<?cs def:titletip_item(con)?>
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

<?cs def:titletip_start()?>
	<span class="f_title">
<?cs /def?>

<?cs def:titletip_end()?>
	</span><?cs #/*endfor: .f_title*/ ?>
<?cs /def?>

<?cs def:titletip()?>
	<?cs if:subcount(qfv.titletip) ?>
		<?cs call:titletip_start()?>
		<?cs #解析每条feed不同的title?>
		<?cs set:_end = subcount(qfv.titletip.con) - 1?>
		<?cs loop:i = 0, _end, 1?>
			<?cs call:titletip_item(qfv.titletip.con[i])?>
		<?cs /loop?>
		<?cs call:titletip_end()?>
	<?cs /if ?>
<?cs /def?>
