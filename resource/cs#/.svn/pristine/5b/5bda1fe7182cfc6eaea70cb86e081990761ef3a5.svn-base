<?cs #:引用组件，支持richmsg ?>
<?cs def:quote_item(con)?>
	<?cs if:con.action.type == "popup"?>
		<?cs call:con_popup(con)?>
	<?cs else ?>
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

<?cs def:quote()?>
	<?cs if:subcount(qfv.quote.con) > 0 ?>
		<div class="f_quote">
			<i class="ui_ico quote_before c_tx3">“</i>
				<?cs set:_end = subcount(qfv.quote.con) - 1?>
				<?cs loop:i = 0, _end, 1?>
					<?cs call:quote_item(qfv.quote.con[i])?>
				<?cs /loop?>
			<i class="ui_ico quote_after c_tx3">”</i>
		</div>
	<?cs /if ?>
<?cs /def?>

