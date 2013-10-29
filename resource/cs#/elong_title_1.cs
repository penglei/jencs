<?cs #:这里是主入口哦 ?>
<?cs if:qz_metadata.feedtitle == 1 ?>
	评论我对<a class="c_tx" target="_blank" href="http://rc.qzone.qq.com/myhome/502?destid=<?cs var:qz_metadata.dest.id ?>"><?cs var:qz_metadata.dest.name ?></a>的点评：<a class="c_tx" target="_blank" href="http://rc.qzone.qq.com/myhome/502?ouin=<?cs var:qz_metadata.uin ?>&amp;tid=<?cs var:qz_metadata.id ?>"><?cs var:qz_metadata.content ?></a>
<?cs elif:qz_metadata.feedtitle == 2 ?>
	在对<a class="c_tx" target="_blank" href="http://rc.qzone.qq.com/myhome/502?destid=<?cs var:qz_metadata.dest.id ?>"><?cs var:qz_metadata.dest.name ?></a>的点评：<a class="c_tx" target="_blank" href="http://rc.qzone.qq.com/myhome/502?ouin=<?cs var:qz_metadata.uin ?>&amp;tid=<?cs var:qz_metadata.id ?>"><?cs var:qz_metadata.content ?></a>回复我
<?cs /if ?>


