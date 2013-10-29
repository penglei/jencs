<?cs #:
	/**
	 * 
	 */
	function attachmentPresenter(magicemotion){}
?>
<?cs def:attachmentPresenter(magicemotion) ?>
	<qz:plugin name="attachmentPresenter" config='<?cs var:magicemotion.config ?>'>
		<a href="javascript:;">
			<img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,120,
										'http://qzonestyle.gtimg.cn/qzone/em/120/mb<?cs var:magicemotion.id ?>.jpg');"/>
		</a>
	</qz:plugin>
<?cs /def ?>
