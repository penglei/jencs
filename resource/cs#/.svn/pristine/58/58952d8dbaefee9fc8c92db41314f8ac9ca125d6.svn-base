<?cs def:imgs(map) ?>
	<div class="imgbox">
		<span class="none">.</span>
		<?cs each:item=map.media.image ?>
			<qz:popup param="3,<?cs var:item.param ?>" src="/qzone/photo/zone/icenter_popup.html" version="2"> 
				<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:item?>');" />
			</qz:popup>
		<?cs /each ?>
	</div>
<?cs /def ?>

<?cs def:replyInfo(map) ?>
	<?cs if:map.replynum>4 ?>
		<div class="feeds_comm_list bg2">
			<div class="feeds_comment_cont">
				<p class="feeds_comment_text">
					<a href="http://user.qzone.qq.com/<?cs var:map.uin ?>/msgboard" target="_blank">中间省略<?cs var:map.replynum-subcount(map.reply) ?>条回复>>
					</a>
				</p>
			</div>
		</div>
	<?cs /if ?>
	<?cs if:subcount(map.reply.0)>0 ?>
		<?cs each:item=map.reply ?>
			<div class="feeds_comm_list bg2">
				<div class="feeds_comment_cont">
					<p class="feeds_comment_text">
						<a href="http://user.qzone.qq.com/<?cs var:item.uin ?>" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" class="comment_nickname c_tx q_des q_namecard" target="_blank">
							<?cs var:item.nick ?>
							<span class="none">.</span>
						</a>
						<?cs var:item.msg ?>
					</p>
					<p class="feeds_comment_op">
						<span class="feeds_time c_tx3">
							<?cs var:item.replydate ?>
							<span class="none">.</span>
						</span>
						<?cs if:item.qz_delete ?>
							<span class="c_tx">
								<qz:delete action="http://m.qzone.qq.com/cgi-bin/new/msgb_del_comment.cgi" param="<?cs var:item.qz_delete.param ?>" title="删除此信息的同时，也将删除原留言中对应的回复。是否确认删除?">删除</qz:delete>
							</span>
						<?cs /if ?>
					</p>
				</div>
			</div>
		<?cs /each ?>
	<?cs elif:subcount(map.reply)>0 ?>
		<div class="feeds_comm_list bg2">
			<div class="feeds_comment_cont">
				<p class="feeds_comment_text">
					<a href="http://user.qzone.qq.com/<?cs var:map.reply.uin ?>" link="nameCard_<?cs var:map.reply.uin ?> des_<?cs var:map.reply.uin ?>" class="comment_nickname c_tx q_des q_namecard" target="_blank">
						<?cs var:map.reply.nick ?>
						<span class="none">.</span>
					</a>
					<?cs var:map.reply.msg ?>
				</p>
				<p class="feeds_comment_op">
					<span class="feeds_time c_tx3">
						<?cs var:map.reply.replydate ?>
						<span class="none">.</span>
					</span>
					<?cs if:map.reply.qz_delete ?>
						<span class="c_tx">
							<qz:delete action="http://m.qzone.qq.com/cgi-bin/new/msgb_del_comment.cgi" param="<?cs var:map.reply.qz_delete.param ?>" title="删除此信息的同时，也将删除原留言中对应的回复。是否确认删除?">删除</qz:delete>
						</span>
					<?cs /if ?>
				</p>
			</div>
		</div>
	<?cs /if ?>
<?cs /def ?>

<?cs def:replyArea(map) ?>
	<qz:reply action="http://m.qzone.qq.com/cgi-bin/new/msgb_comment_answer.cgi" version="6" param="<?cs var:map.qz_reply.param ?>" type="ubb" charset="GB" maxLength="400">回复</qz:reply>
<?cs /def ?>

<div class="feeds_tp_1">
	<div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div>
			<div class="feeds_comm_list bg2">
				<div class="feeds_comment_cont">
					<p class="feeds_comment_text">
						<a href="http://user.qzone.qq.com/<?cs var:qz_metadata.commenter ?>" link="nameCard_<?cs var:qz_metadata.commenter ?> des_<?cs var:qz_metadata.commenter ?>" class="comment_nickname c_tx q_des q_namecard" target="_blank">
							<?cs var:qz_metadata.nickname ?>
							<span class="none">.</span>
						</a>
						<?cs var:qz_metadata.message ?>
					</p>
				<?cs if:qz_metadata.media.image.0 ?>
					<?cs call:imgs(qz_metadata) ?>
				<?cs elif:qz_metadata.media.image ?>
					<div class="imgbox">
						<span class="none">.</span>
						<qz:popup param="3,<?cs var:map.media.image.param ?>" src="/qzone/photo/zone/icenter_popup.html" version="2"> 
							<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:map.media.image ?>');" />
						</qz:popup>
					</div>
				<?cs /if ?>
				<p class="feeds_comment_op">
					<span class="feeds_time c_tx3">
						<?cs var:qz_metadata.msgdate ?>
						<span class="none">.</span>
					</span>
					<?cs if:qz_metadata.qz_delete ?>
						<span class="c_tx">
							<qz:delete action="http://m.qzone.qq.com/cgi-bin/new/msgb_delanswer_ic.cgi" param="<?cs var:qz_metadata.qz_delete.param ?>" title="删除此信息的同时，也将删除原留言，是否确认删除?">删除</qz:delete>
						</span>
					<?cs /if ?>
				</p>
			</div>
		</div>
		<?cs call:replyInfo(qz_metadata) ?>
		<?cs call:replyArea(qz_metadata) ?>
	</div>
</div>