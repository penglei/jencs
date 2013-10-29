<?cs if:qz_metadata.feedtitle==3 ?>
	评论我在照片活动:<a class="c_tx" target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/photo/activity/<?cs var:qz_metadata.activityid ?>/"><?cs var:qz_metadata.activityname ?></a>中上传的照片
<?cs elif:qz_metadata.feedtitle==4 ?>
	在照片活动:<a class="c_tx" target="_blank" href="http://rc.qzone.qq.com/photo/activity/<?cs var:qz_metadata.activityid ?>/"><?cs var:qz_metadata.activityname ?></a>中回复我
<?cs else ?>
	评论我在照片活动:<a class="c_tx" target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/photo/activity/<?cs var:qz_metadata.activityid ?>/"><?cs var:qz_metadata.activityname ?></a>中上传的照片
<?cs /if ?>