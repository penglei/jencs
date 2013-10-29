<?cs #:
	/**
	 * 
	 */
	function attachmentPresenter(magicemotion){}
?>
<?cs def:attachmentPresenter(magicemotion) ?>
	<qz:plugin name="attachmentPresenter" config='<?cs call:ugc_as_html(magicemotion.config,1,1) ?>'>
		<a href="javascript:;">
			<img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,120,
										'http://qzonestyle.gtimg.cn/qzone/em/120/mb<?cs call:ugc_as_html(magicemotion.id,1,1) ?>.jpg');"/>
		</a>
	</qz:plugin>
<?cs /def ?>
