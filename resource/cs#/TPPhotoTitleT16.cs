<?cs if:qz_metadata.privacy==1 || qz_metadata.privacy==4?>
	<?cs if:qz_metadata.edit==1 ?>����Ƭ�༭��<?cs /if ?>��
	<?cs if:qz_metadata.anonymity == 0 ?>���
		<a class="c_tx" target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/photo/<?cs var:qz_metadata.albumid ?>/"><?cs var:qz_metadata.albumname ?></a>����������
	<?cs /if ?>
	<?cs if:qz_metadata.anonymity == 1 ?>������᣺
		<a class="c_tx" target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/photo/personal/1/<?cs var:qz_metadata.bgid ?>/<?cs var:qz_metadata.tpid ?>/<?cs var:qz_metadata.albumid ?>/"><?cs var:qz_metadata.albumname ?></a>
	<?cs /if ?>
	<?cs if:qz_metadata.anonymity == 3 ?>��Ƭǽ��
		<a class="c_tx" target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/photo/wallview/1/<?cs var:qz_metadata.albumid ?>/"><?cs var:qz_metadata.albumname ?></a>
	<?cs /if ?>	
<?cs else ?>
	���������
<?cs /if ?>
<?cs if:qz_metadata.from==22 ?><span class="ifeeds_origin c_tx3">ͨ���ֻ�����</span><?cs /if ?>