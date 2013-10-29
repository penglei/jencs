<div class="feeds_tp_3">
	<?cs #:有用户评论 ?>
	<?cs if:qz_metadata.comment ?>
		<div class="txtbox quote_txt"><strong class="quotes_symbols_left c_tx3">“</strong><?cs var:qz_metadata.comment ?><strong class="quotes_symbols_right c_tx3">”</strong></div>
	<?cs /if ?>

	<?cs #:有图的情况 ?>
	<?cs if:qz_metadata.showimg.url ?>
		<div class="img_txt_tp">
			<div class="img_ex bor3">
				<a href="<?cs var:qz_metadata.titleurl ?>" target="_blank"><img onerror="this.src='/ac/qzone_v5/photo/photo_none_ss.png'" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:qz_metadata.showimg.url ?>');"/></a>
				
			</div>
			<div class="txt_ex">
				<p><?cs var:qz_metadata.summary ?></p>
			</div>
		</div>
	<?cs else ?>
	<?cs #:没有图的情况 ?>
		<div class="txtbox">
			<p><?cs var:qz_metadata.summary ?></p>
		</div>
	<?cs /if ?>
	<div class="feeds_tp_operate"><qz:popup height="300" param="{type:99,url:'<?cs var:qz_metadata.shareurl.url ?>'}" src="/qzone/app/qzshare/popup.html" title="分享" width="405">分享</qz:popup><a class="c_tx" href="<?cs var:qz_metadata.source.url ?>" target="_blank">来自:<?cs var:qz_metadata.source.name ?></a></div>
</div>