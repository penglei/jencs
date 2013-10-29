<?cs #/*feed_title_icon.cs是给feeds模版用的，不应该在这里调用的!*/?>
<?cs set:qz_metadata.hdf.yybitmap=item.yybitmap ?>
<?cs set:qz_metadata.source.type=item.source.type ?>
<?cs set:qz_metadata.source.name=item.source.name ?>
<?cs #if:item.source.platformid?>
	<?cs set:qz_metadata.source.platformid = item.source.platformid?>
	<?cs set:qz_metadata.source.subplatformid = item.source.subplatformid?>
	<?cs set:qz_metadata.source.useragent = item.source.useragent?>
<?cs #/if?>
