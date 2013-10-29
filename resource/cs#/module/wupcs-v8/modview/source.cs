<?cs def:v8_source()?>
	<?cs if:subcount(qfv.source) > 0?>
		<span class="ui-mr8 state">
		<?cs #set:qfv.source.mr = "10"?>
		<?cs set:qfv.source.color = "tip"?>
		来自<?cs call:v8_conCommon_item(qfv.source)?>
		</span>
	<?cs /if?>
<?cs /def?>

<?cs def:v8_cntText_oprSource()?>
	<?cs if:subcount(qfv.content.source) > 0?>
		<?cs set:qfv.content.source.mr = "10"?>
		<?cs set:qfv.content.source.color = "tip"?>
		来自<?cs call:v8_conCommon_item(qfv.content.source)?>
	<?cs /if?>
<?cs /def?>
