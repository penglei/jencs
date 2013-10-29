<?cs #:引用组件 ?>
<?cs def:v8_quote() ?>
	<?cs #先判断是否有非空节点 ?>
	<?cs set:_has_con = 0?>
	<?cs set:_end = subcount(qfv.quote.con) - 1?>
	<?cs loop:i = 0, _end, 1?>
		<?cs if:qfv.quote.con[i].type && qfv.quote.con[i].type != "none"?>
			<?cs set:_has_con = 1?>
		<?cs /if ?>
	<?cs /loop?>
	<?cs if:_has_con ?>
		<div class="f-quote">
			<i class="ui_ico quote_before c_tx3">“</i>
			<?cs call:v8_conCommon(qfv.quote.con)?>
			<i class="ui_ico quote_after c_tx3">”</i>
		</div>
	<?cs /if ?>
<?cs /def ?>
