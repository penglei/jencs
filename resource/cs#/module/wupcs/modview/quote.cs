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
	<?cs #先判断是否有非空节点 ?>
	<?cs set:_has_con = 0?>
	<?cs set:_end = subcount(qfv.quote.con) - 1?>
	<?cs loop:i = 0, _end, 1?>
		<?cs if:qfv.quote.con[i].type && qfv.quote.con[i].type != "none"?>
			<?cs set:_has_con = 1?>
		<?cs /if ?>
	<?cs /loop?>
	<?cs if:_has_con ?>
		<div class="f_quote">
			<i class="ui_ico quote_before c_tx3">“</i>
				<?cs loop:i = 0, _end, 1?>
					<?cs call:quote_item(qfv.quote.con[i])?>
				<?cs /loop?>
			<i class="ui_ico quote_after c_tx3">”</i>
		</div>
	<?cs /if ?>
<?cs /def?>

