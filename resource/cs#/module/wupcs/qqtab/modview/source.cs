<?cs def:oprSource()?>
	<?cs if:subcount(qfv.source) > 0?>
		<?cs set:qfv.source.mr = "10"?>
		<?cs set:qfv.source.color = "tip"?>
		来自<?cs call:conCommon_item(qfv.source)?>
	<?cs /if?>
<?cs /def?>

<?cs def:cntText_oprSource()?>
	<?cs if:subcount(qfv.content.source) > 0?>
		<?cs set:qfv.content.source.mr = "10"?>
		<?cs set:qfv.content.source.color = "tip"?>
		来自<?cs call:conCommon_item(qfv.content.source)?>
	<?cs /if?>
<?cs /def?>