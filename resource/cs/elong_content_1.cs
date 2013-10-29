<?cs def:textUserLink(p_uin,p_name) ?>
	<a class="q_namecard q_des comment_nickname c_tx" href="http://user.qzone.qq.com/<?cs var:p_uin ?>" link="nameCard_<?cs var:p_uin ?> des_<?cs var:p_uin ?>" target="_blank">
	    <?cs var:p_name ?>
	</a>
<?cs /def ?>

<?cs def:replyList(p_replycount,p_replylist) ?>
	<?cs if:p_replycount>1 ?>
		<?cs each:item=p_replylist ?>
			<div class="feeds_comm_list bg2">
				<div class="feeds_comment_cont">
					<p class="feeds_comment_text">
					    <?cs call:textUserLink(item.uin,item.nick) ?>
						<?cs var:item.content ?>
					</p>
					<p class="feeds_comment_op">
					    <span class="feeds_time c_tx3"><?cs var:item.date ?></span>
					</p>
				</div>
			</div>
		<?cs /each ?>
	<?cs elif:p_replycount==1 ?>
		<div class="feeds_comm_list bg2">
			<div class="feeds_comment_cont">
				<p class="feeds_comment_text">
				    <?cs call:textUserLink(p_replylist.uin,p_replylist.nick) ?>
					<?cs var:p_replylist.content ?>
				</p>
				<p class="feeds_comment_op">
				    <span class="feeds_time c_tx3"><?cs var:p_replylist.date ?></span>
				</p>
			</div>
		</div>
	<?cs /if ?>
<?cs /def ?>

<div class="feeds_tp_6">
    <div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div>
		<div class="feeds_comm_list bg2">
			<div class="feeds_comment_cont">
				<p class="feeds_comment_text">
					<?cs call:textUserLink(qz_metadata.comment.uin,qz_metadata.comment.nick) ?>
					<?cs var:qz_metadata.comment.content ?>
				</p>
				<p class="feeds_comment_op">
					<span  class="feeds_time c_tx3">
						<?cs var:qz_metadata.comment.date ?>
					</span>
				</p>
			</div>
		</div>
		<?cs if:qz_metadata.comment.replys.total>#4 ?>
			<div class="more_feeds_comment bg2">
				<a target="_blank" href="http://rc.qzone.qq.com/myhome/502?ouin=<?cs var:qz_metadata.uin ?>&amp;tid=<?cs var:qz_metadata.id ?>" class="c_tx">
					 查看全部<?cs var:qz_metadata.comment.replys.total ?>条回复<![CDATA[>>]]>
				</a>
			</div>
		<?cs /if ?>
		<?cs if:subcount(qz_metadata.comment.replys.reply)||subcount(qz_metadata.comment.replys.reply.0) ?>
			<?cs call:replyList(qz_metadata.comment.replys.total,qz_metadata.comment.replys.reply) ?>
	    <?cs /if ?>
        <qz:reply action="http://elong.qzone.qq.com/cgi-bin/elong_tour_add_comment_reply.cgi" maxLength="100" version="6" param="ouin=<?cs var:qz_metadata.uin ?>&amp;topicid=<?cs var:qz_metadata.id ?>&amp;commentid=<?cs var:qz_metadata.comment.id ?>&amp;typeid=2" type="text" charset="UTF-8">回复</qz:reply>
	</div>
</div>