<?cs def:imgs(media, groupid, postid) ?>
<div class="imgbox">
	<?cs def:imgs-items(item) ?>
		<?cs if:postid ?>
			<a href="http://city.qq.com/group/thread.htm#gid=<?cs var:groupid?>&postid=<?cs var:postid?>&ADTAG=ISD.QZONE.FEED" target="_blank"><img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:item ?>');"/></a>
		<?cs else ?>
			<a href="http://city.qq.com/group/board.htm#gid=<?cs var:groupid?>&ADTAG=ISD.QZONE.FEED" target="_blank"><img class="bor3" alt="" src="<?cs var:item ?>"  /></a>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:media.img.0 || subcount(media.img.0) > 0 ?>
		<?cs each:item = media.img ?>
			<?cs call:imgs-items(item) ?>
		<?cs /each ?>
	<?cs elif:media.img || subcount(meida.img) > 0 ?>
		<?cs call:imgs-items(media.img) ?>
	<?cs /if ?>
</div>
<?cs /def ?>

<?cs #:活动feeds ?>
<?cs if:qz_metadata.type == 1 ?>
	<div class="feeds_tp_3">
		<?cs call:imgs(qz_metadata.summary.media, qz_metadata.action.groupid, qz_metadata.action.postid) ?>
		<div class="txtbox">
			<?cs if:qz_metadata.action.flag == 1 ||  qz_metadata.action.flag == 3?>
			<p>活动时间:<?cs var:qz_metadata.action.starttime ?></p>
			<?cs /if ?>
			<?cs if:qz_metadata.action.flag == 2 ||  qz_metadata.action.flag == 4?>
			<p>结束时间:<?cs var:qz_metadata.action.endtime ?></p>
			<?cs /if ?>
			<?cs if:qz_metadata.action.address ?>
            <p>活动地点:<?cs var:qz_metadata.action.address ?></p>
            <?cs /if ?>
            <p>活动介绍:<?cs var:qz_metadata.action.summary ?></p>
		</div>
		<div class="feeds_tp_operate"><a class="c_tx" href="http://city.qq.com/group/thread.htm#gid=<?cs var:qz_metadata.action.groupid?>&postid=<?cs var:qz_metadata.action.postid?>&ADTAG=ISD.QZONE.FEED" target="_blank">我也参加</a></div>
	</div>
<?cs /if ?>

<?cs #:创建兴趣组 ?>
<?cs if:qz_metadata.type == 2 ?>
	<div class="feeds_tp_1">
		<?cs call:imgs(qz_metadata.summary.media, qz_metadata.group.groupid, 0) ?>
		<div class="txtbox">
			<p>兴趣组简介：<?cs var:qz_metadata.group.groupsummary ?></p>
		</div>
		<div class="feeds_tp_operate"><a class="c_tx" href="http://city.qq.com/group/board.htm#gid=<?cs var:qz_metadata.group.groupid?>&ADTAG=ISD.QZONE.FEED" target="_blank">我也加入</a></div>
	</div>
<?cs /if ?>
<?cs #:发表帖子feeds ?>
<?cs if:qz_metadata.type == 3 ?>
	<div class="feeds_tp_1">
		<?cs call:imgs(qz_metadata.summary.media, qz_metadata.group.groupid, qz_metadata.group.postid) ?>
		<div class="txtbox">
			<p class="img_amount c_tx3">&lt;共<?cs var:qz_metadata.summary.media.imgcount ?>张图片&gt;</p>
			<p><?cs var:qz_metadata.group.postsummary ?></p>
		</div>
		<div class="feeds_tp_operate"><a class="c_tx" href="http://city.qq.com/group/thread.htm#gid=<?cs var:qz_metadata.group.groupid?>&postid=<?cs var:qz_metadata.group.postid?>&ADTAG=ISD.QZONE.FEED" target="_blank">查看详情</a></div>
	</div>
<?cs /if ?>
