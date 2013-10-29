<?cs def:markPhotoInfo(mod) ?>
	<?cs def:markPhotoCnt(info)?>
		<p>相册名称：<a target="_blank" href="<?cs var:info.album.url ?>"><?cs var:info.album.name ?></a></p>
		<p><?cs var:info.describe ?></p>
		<p><a target="_blank" href="http://user.qzone.qq.com/<?cs var:info.markuser.uin ?>/photo/marked" ?>查看<?cs var:info.markcount?>张圈有<?cs var:info.markuser.name?>的照片</a></p>
	<?cs /def ?>
	<?cs if:subcount(qz_metadata.content_box.markphoto_info) > 0 ?>
	<div class="txt_box">
		<?cs call:markPhotoCnt(qz_metadata.content_box.markphoto_info) ?>
	</div>
	<?cs /if ?>
<?cs /def ?>