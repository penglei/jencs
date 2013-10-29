<?cs #qz_metadata.magicemotion ?>
<?cs def:attachmentMagicemotionPresenter() ?>
<?cs if: subcount(qz_metadata.magicemotion) > 0?>
<qz:plugin name="attachmentPresenter" config='<?cs var:qz_metadata.magicemotion.config ?>'>
<a href="javascript:;">
	<img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,120,'http://qzonestyle.gtimg.cn/qzone/em/120/mb<?cs var:qz_metadata.magicemotion.id ?>.jpg');"/>
</a>
</qz:plugin>
<?cs /if ?>
<?cs /def ?>