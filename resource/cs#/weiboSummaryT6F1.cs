<?cs #:@人被动feeds模板 ?>


<?cs def:html_escape(source)?><?cs escape:"none"?><?cs var:source?><?cs /escape?><?cs /def?>
<?cs def:url_escape(source)?><?cs escape:"url"?><?cs var:source?><?cs /escape?><?cs /def?>
<?cs def:js_escape(source)?><?cs escape:"js"?><?cs var:source?><?cs /escape?><?cs /def?>

<?cs #:照片音乐等 ?>
<?cs def:richInfo(info) ?>
	<?cs def:richInfo-item(item) ?>
		<?cs if:item.type == 'img' ?>
			<div class="imgbox">
				<a href="javascript:void(0);" onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs var:item.url3 ?>', '<?cs var:item.url2 ?>', 374);return false;">
					<img alt=" " title="点击放大" class="bor3 zoom_img" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(160,150,'<?cs var:item.url1 ?>');"/>
				</a>
			</div>
		<?cs elif:item.type == 'movie' ?>
			<div class="imgbox video_img" richinfo="3" url1="<?cs var:item.cover ?>" url2="<?cs var:item.original ?>" url3="<?cs var:item.player ?>">
				<qz:popup version="3" param="3" src="/qzone/app/mood/richinfo_view.html" width="<?cs if:subcount(qz_metadata.rtweet) > 0 ?>374<?cs else ?>392<?cs /if ?>" height="365">
					<span>
						<span class="play_op">　</span>
						<img alt="视频缩略图" style="width:120px;height:90px;" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,90,'<?cs var:item.cover ?>');" />
					</span>
				</qz:popup>
			</div>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:info.0 || subcount(info.0) > 0 ?>
		<?cs each:item = info ?>
			<?cs call:richInfo-item(item) ?>
		<?cs /each ?>
	<?cs elif:subcount(info) > 0 ?>
		<?cs call:richInfo-item(info) ?>
	<?cs /if ?>
<?cs /def ?>


<?cs #:广播显示 ?>
<?cs def:richContent(cons,from) ?>
	<?cs def:richContent-item(item) ?>
		<?cs if:item.type == 'wbnick' ?>
			<a href="http://rc.qzone.qq.com/myhome/weibo/profile/<?cs var:item.wbaccount ?>/" target="_blank" class="c_tx" title="<?cs var:item.wbnick ?>(@<?cs var:item.wbaccount ?>)"><?cs var:item.wbnick ?></a>
		<?cs elif:item.type == 'nick' ?>
			<?cs if:string.length(item.who) == 0 || item.who == '1' ?><?cs #:空间1,校友2,微博3 ?>
				<a href="http://user.qzone.qq.com/<?cs var:item.uin ?>/" class="comment_nickname c_tx q_namecard q_des" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" target="_blank"><?cs var:item.name ?></a>
			<?cs /if ?>
		<?cs elif:item.type == 'topic' ?>
			<a href="http://rc.qzone.qq.com/myhome/weibo/topic/<?cs var:item.topic ?>/" target="_blank" class="c_tx">#<?cs var:item.topic ?>#</a>
		<?cs elif:item.type == 'url' ?>
			<a href="<?cs var:item.url ?>?type=1&from=19&f=2&s=<?cs var:from ?>" target="_blank" class="c_tx"><?cs var:item.url ?></a>
		<?cs else ?>
			<?cs var:item ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 ?>
		<?cs each:item = cons.con ?>
			<?cs call:richContent-item(item) ?>
		<?cs /each ?>
	<?cs elif:string.length(cons.con) > 0 || subcount(cons.con) > 0 ?>
		<?cs call:richContent-item(cons.con) ?>
	<?cs elif:string.length(cons) > 0 ?>
		<?cs var:cons ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:原帖 ?>
<?cs def:original(tweet) ?>
	<?cs if:subcount(tweet) > 0 ?>
		<div class="mood_rt_frame bgr3">
			<p class="mood_rt_text">
				<a href="http://rc.qzone.qq.com/myhome/weibo/profile/<?cs var:tweet.wbaccount ?>/" target="_blank" class="c_tx" title="<?cs var:tweet.wbnick ?>(@<?cs var:tweet.wbaccount ?>)"><?cs var:tweet.wbnick ?></a>
				<?cs if:tweet.flag1 ?><img src="http://qzonestyle.gtimg.cn/qzonestyle/qzone_client_v5/img/mlog_att.png" alt=" " title="微博认证" style="width:14px;height:14px;vertical-align:text-top;margin:0 3px;" /><?cs /if ?>：
				<span class="c_tx1"><?cs call:richContent(tweet.content,tweet.wbfrom) ?></span>
			</p>
			<?cs call:richInfo(tweet.richinfo) ?>
			<p class="mood_rt_info c_tx3"><span><?cs var:tweet.time ?></span> <span>通过<?cs var:tweet.wbsourceName ?></span> <a href="http://rc.qzone.qq.com/myhome/weibo/agg/<?cs var:tweet.wbaccount ?>/<?cs var:tweet.wbid ?>/<?cs var:qz_metadata.wbid ?>/" target="_blank" class="c_tx"><?cs var:tweet.zzcount ?>人转播</a></p>
		</div>
	<?cs /if ?>
<?cs /def ?>

<?cs #:头像 ?>
<?cs def:face(uin,nick) ?>
	<?cs set:uinmod = uin % 4 + 1?>
	<?cs set:uinsrc = "http://qlogo"+uinmod+".store.qq.com/qzone/"+uin+"/"+uin+"/30" ?>
	<a href="http://user.qzone.qq.com/<?cs var:uin ?>/" class="feeds_comment_portrait" target="_blank">
		<span class="skin_portrait_round"></span>
		<img src="<?cs var:uinsrc ?>" alt=" " title="<?cs var:nick ?>" />
	</a>
<?cs /def ?>


<?cs #:回复列表 ?>
<?cs def:replyList(reply) ?>
	<?cs def:reply-item(item) ?>
		<div class="comment_reply_list">
			<div class="bbor5">
				<?cs call:face(item.uin,item.nick) ?>
				<div class="comment_reply_cont">
					<p class="comment_reply_text"><a href="http://user.qzone.qq.com/<?cs var:item.uin ?>/" class="comment_nickname c_tx q_namecard q_des" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" target="_blank"><?cs var:item.nick ?></a><?cs call:richContent(item.content,22) ?></p>
					<p class="comment_reply_op"><span class="feeds_time c_tx3"><?cs var:item.time ?></span></p>
				</div>
			</div>
		</div>
	<?cs /def ?>
	<?cs if:reply.0 || subcount(reply.0) > 0 ?>
		<?cs each:item = reply ?>
			<?cs call:reply-item(item) ?>
		<?cs /each ?>
	<?cs elif:subcount(reply) > 0 ?>
		<?cs call:reply-item(reply) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:评论列表 ?>
<?cs def:commentList(comment) ?>
	<?cs def:comment-item(item) ?>
		<div class="feeds_comment_list bg2">
			<?cs call:face(item.uin,item.nick) ?>
			<div class="feeds_comment_cont">
				<p class="feeds_comment_text"><a href="http://user.qzone.qq.com/<?cs var:item.uin ?>/" class="comment_nickname c_tx q_namecard q_des" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" target="_blank"><?cs var:item.nick ?></a><?cs call:richContent(item.content,22) ?></p>
				<p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:item.time ?></span></p>
			</div>
			<?cs call:replyList(item.reply) ?>
		</div>
	<?cs /def ?>
	<?cs if:comment.0 || subcount(comment.0) > 0 ?>
		<?cs each:item = comment ?>
			<?cs call:comment-item(item) ?>
		<?cs /each ?>
	<?cs elif:subcount(comment) > 0 ?>
		<?cs call:comment-item(comment) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:主函数 ?>
<?cs def:main() ?>
	<div class="feeds_tp_5">
		<?cs #:转发的原帖 ?>
		<?cs call:original(qz_metadata.rtweet) ?>
		<?cs #:照片音乐等 ?>
		<?cs call:richInfo(qz_metadata.richinfo) ?>
		<?cs #:操作区 ?>
		<div class="feeds_tp_operate">
			<qz:reply type="link">评论</qz:reply>
		</div>
		<?cs #:评论 ?>
		<div class="feeds_comment">
			<div class="comment_arrow c_bg2">◆</div>
			<?cs #:评论列表 ?>
			<?cs call:commentList(qz_metadata.comment) ?>
			<?cs #:评论框 ?>
			<qz:reply action="http://wb.qzone.qq.com/cgi-bin/add_comment_ic.cgi" charset="UTF-8" param="topicid=<?cs var:qz_metadata.wbid ?>&amp;parename=<?cs var:qz_metadata.wbaccount ?>&amp;tmpl=<?cs if:string.length(qz_metadata.feedstmpl) > 0 ?><?cs var:qz_metadata.feedstmpl ?><?cs else ?>0<?cs /if?>&amp;statfrom=<?cs if:subcount(qz_metadata.rtweet) > 0 ?>2<?cs else ?>1<?cs /if ?>" type="text" version="6" maxLength="140" lengthMode="1" config="0|1|0|b41,1,towb,点评到微博|0|0|0">评论</qz:reply>
		</div>
	</div>
<?cs /def ?>

<?cs call:main() ?>