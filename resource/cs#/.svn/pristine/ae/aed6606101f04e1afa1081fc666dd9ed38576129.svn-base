<?cs def:imgs(root) ?>
<div class="imgbox">
<?cs if:root.media.img.0 || subcount(root.media.img.0) ?>
	<?cs set:tmpindex = 0 ?>
	<?cs each:item = root.media.img ?>
		<a href="<?cs var:qz_metadata.title.url ?>" target="_blank" class="c_tx"><img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:item ?>');"/></a>
		<?cs set:tmpindex = tmpindex + 1 ?>
	<?cs /each ?>
<?cs elif:root.media.img ?>
		<a href="<?cs var:qz_metadata.title.url ?>" target="_blank"><img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:qz_metadata.media.img ?>');"/></a>
<?cs /if ?>
</div>
<?cs if:qz_metadata.media.img_num > 0 ?>
	<p class="img_amount c_tx3">共<?cs var:qz_metadata.media.img_num ?>张图片</p>
<?cs /if ?>
<?cs /def ?>

<div class="feeds_tp_1">
	<div class="blog_feeds_tp bor2">
		<?cs call:imgs(qz_metadata) ?>
		<div class="txtbox">
			<p><?cs var:qz_metadata.summary.content ?></p>
		</div>
	</div>
	<div class="feeds_tp_operate">
		<qz:popup height="300" param="{type:99,url:'<?cs var:qz_metadata.summary.share ?>'}" src="/qzone/app/qzshare/popup.html" title="分享" width="405">分享</qz:popup><a href="<?cs var:qz_metadata.title.url ?>" target="_blank" class="c_tx">查看全文</a>
	</div>
</div>