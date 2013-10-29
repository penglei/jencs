<?cs if:qz_metadata.feedtitle==1?>
	<strong class="quotes_symbols_left c_tx3">“</strong><?cs var:qz_metadata.photodesc ?><strong class="quotes_symbols_right c_tx3">”</strong>
<?cs /if ?>
<?cs if:qz_metadata.feedtitle!=1 ?>
	<?cs if:qz_metadata.privacy==1 || qz_metadata.privacy==4 ?>
		<?cs if:qz_metadata.edit==1 ?>
			使用照片编辑器编辑照片：
		<?cs else ?>
			上传照片：
		<?cs /if ?>
			<a class="c_tx" target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/photo/<?cs var:qz_metadata.albumid ?>/<?cs var:qz_metadata.lloc ?>/"><?cs var:qz_metadata.photoname ?></a>
	<?cs else ?>
		更新了相册
	<?cs /if ?>
<?cs /if ?>