<?cs if:qz_metadata.type == 1 ?>
	<?cs if:qz_metadata.action.flag == 1?>创建城市达人线下兴趣活动：<?cs /if ?>
	<?cs if:qz_metadata.action.flag == 2?>创建城市达人线上兴趣活动：<?cs /if ?>
	<?cs if:qz_metadata.action.flag == 3?>参加城市达人线下兴趣活动：<?cs /if ?>
	<?cs if:qz_metadata.action.flag == 4?>参加城市达人线上兴趣活动：<?cs /if ?>
	<a class="c_tx" href="http://city.qq.com/group/thread.htm#gid=<?cs var:qz_metadata.action.groupid?>&postid=<?cs var:qz_metadata.action.postid?>&ADTAG=ISD.QZONE.FEED" target="_blank"><?cs var:qz_metadata.action.title ?></a>
<?cs /if ?>

<?cs if:qz_metadata.type == 2 ?>
创建城市达人兴趣组:<a class="c_tx" href="http://city.qq.com/group/board.htm#gid=<?cs var:qz_metadata.group.groupid?>&ADTAG=ISD.QZONE.FEED" target="_blank"><?cs var:qz_metadata.group.groupname ?></a>
<?cs /if ?>

<?cs if:qz_metadata.type == 3 ?>
在城市达人<a class="c_tx" href="http://city.qq.com/group/board.htm#gid=<?cs var:qz_metadata.group.groupid?>&ADTAG=ISD.QZONE.FEED" target="_blank"><?cs var:qz_metadata.group.groupname ?></a>发表帖子<a class="c_tx" href="http://city.qq.com/group/thread.htm#gid=<?cs var:qz_metadata.group.groupid?>&postid=<?cs var:qz_metadata.group.postid?>&ADTAG=ISD.QZONE.FEED" target="_blank" ><?cs var:qz_metadata.group.posttitle ?></a>
<?cs /if ?>
