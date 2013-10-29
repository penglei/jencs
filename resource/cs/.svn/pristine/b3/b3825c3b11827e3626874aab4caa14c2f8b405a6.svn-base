<div class="feeds_tp_8">
	<div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div>
		<div class="more_feeds_comment bg2"><a href="http://user.qzone.qq.com/<?cs var:qz_metadata.receiver ?>/myhome/360/" onclick="QZONE.FP.toApp(\'/myhome/360\');return false;" target="_blank" class="c_tx">查看更多爱情记事&gt;&gt;</a></div>
		<?cs def:comment-items(item) ?>
			<?cs if:subcount(item.uin) > 0 ?>
				<div class="feeds_comm_list bg2">
				<div class="feeds_comment_cont">
				<p class="feeds_comment_text">
					<a href="http://user.qzone.qq.com/<?cs var:item.uin ?>/" target="_blank" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" class="comment_nickname c_tx q_namecard q_des"><?cs var:item.nick ?></a>
					<?cs var:item.content ?>
				</p>
				<p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:item.time ?></span>
					<?cs if:subcount(item.reply_del.action) > 0 ?>
						<qz:delete action="<?cs var:item.reply_del.action ?>" param="<?cs var:item.reply_del.param ?>">删除</qz:delete>
					<?cs /if ?>
				</p>
				</div>
				</div>
			<?cs /if ?>
		<?cs /def ?>
		<?cs if:qz_metadata.comments.0 || subcount(qz_metadata.comments.0) > 0 ?>
			<?cs each:item = qz_metadata.comments ?>
				<?cs call:comment-items(item) ?>
			<?cs /each ?>
		<?cs elif:qz_metadata.comments || subcount(qz_metadata.comments) > 0 ?>
			<?cs call:comment-items(qz_metadata.comments) ?>
		<?cs /if ?>
		<?cs if:subcount(qz_metadata.qz_comment.action) > 0 ?>
			 <qz:reply action="<?cs var:qz_metadata.qz_comment.action ?>" param="<?cs var:qz_metadata.qz_comment.param ?>" type="ubb" charset="UTF8" maxLength="150" version="6" btnstyle="6.1">回复</qz:reply>
		<?cs /if ?>
	 </div>
</div>
