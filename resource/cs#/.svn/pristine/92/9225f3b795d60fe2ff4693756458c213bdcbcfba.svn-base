<?cs #:这里是主入口哦 ?>
<div class="feeds_tp_8">
	<?cs if qz_metadata.optype == 3 ?>
		<div class="txtbox quote_txt">
			<p><strong class="quotes_symbols_left c_tx3">“</strong><?cs var:qz_metadata.message ?><strong class="quotes_symbols_right c_tx3">”</strong></p>
		</div>
		
		<?cs #: 展示图片 ?>
		<?cs if qz_metadata.imgs.total > 1 ?>
		<?cs elif qz_metadata.imgs.total == 1 ?>
			<div class="imgbox img_thumb">
				<a onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs var:qz_metadata.imgs.img.url2 ?>', '<?cs var:qz_metadata.imgs.img.url2 ?>', null);return false;" href="javascript:;">
					<img alt="点击查看大图" title="点击放大" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,90,'<?cs var:qz_metadata.imgs.img.url1 ?>');" />
				</a>
			</div>
		<?cs /if ?>
		
		<?cs #: > 1 的时候才能用遍历... ?>
		<?cs if qz_metadata.together.total > 1 ?>
		<?cs set:idx = 0 ?>
		和<?cs each:item = qz_metadata.together.item 
			?><?cs set:idx = idx + 1 ?><a class="c_tx q_des q_namecard" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" target="_blank" href="http://user.qzone.qq.com/<?cs var:item.uin ?>"><?cs var:item.nick ?></a
			><?cs if idx != qz_metadata.together.total && idx != 3 ?>、<?cs /if ?>
			<?cs /each ?><?cs if qz_metadata.together.total > 3 ?>等<?cs var:qz_metadata.together.total ?>个好友<?cs /if ?>在一起
		<?cs /if ?>
		
		<?cs if qz_metadata.together.total == 1 ?>
			和<a class="c_tx q_des q_namecard" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.together.item.uin ?>"><?cs var:qz_metadata.together.item.nick ?></a>在一起
		<?cs /if ?>
	<?cs /if ?>
	<div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div>
			<?cs def:feeds_comment-items(item) ?>
				<div class="feeds_comm_list bg2">
					<div class="feeds_comment_cont">
						<p class="feeds_comment_text">
							<a class="comment_nickname q_des c_tx q_namecard" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" href="http://user.qzone.qq.com/<?cs var:item.uin ?>" target="_blank"><?cs var:item.nick ?><span class="none"></span></a>
							<?cs var:item.msg ?>
						</p>
						<p class="feeds_comment_op">
							<span class="feeds_time c_tx3"><?cs var:item.replydate ?></span>
						</p>
					</div>
				</div>
			<?cs /def ?>
			<?cs if:qz_metadata.reply.0 || subcount(qz_metadata.reply.0) > 0 ?>
				<?cs each:item = qz_metadata.reply ?>
					<?cs call:feeds_comment-items(item) ?>
				<?cs /each ?>
			<?cs elif:qz_metadata.reply || subcount(qz_metadata.reply) > 0 ?>
				<?cs call:feeds_comment-items(qz_metadata.reply) ?>
			<?cs /if ?>
		<?cs if:qz_metadata.reply.rreplycount > 4 ?>
		<div class="more_feeds_comment bg2">
			<?cs if:qz_metadata.optype == 10 || qz_metadata.optype == 11 ?>
				<?cs if:qz_metadata.opadmin == 1 && qz_metadata.source == 2 ?>
					<a class="c_tx" href="http://meishi.qq.com/reviews/<?cs var:qz_metadata.timestamp ?>_<?cs var:qz_metadata.sid ?>_<?cs var:qz_metadata.uin ?>_2" target="_blank">查看全部<?cs var:qz_metadata.reply.rreplycount ?>条回复&gt;&gt;</a>
				<?cs else ?>
					<a class="c_tx" href="http://rc.qzone.qq.com/myhome/qqmeishi?vc=<?cs var:qz_metadata.uin ?>_<?cs var:qz_metadata.sid ?>" target="_blank">查看全部<?cs var:qz_metadata.reply.rreplycount ?>条回复&gt;&gt;</a>
				<?cs /if ?>
			<?cs /if ?>
			<?cs if:qz_metadata.optype == 12 || qz_metadata.optype == 13 ?>
				<a target="_blank" href="http://rc.qzone.qq.com/myhome/qqmeishi?checkin=<?cs var:qz_metadata.uin ?>_<?cs var:qz_metadata.cid ?>_<?cs var:qz_metadata.sid ?>&amp;uc=<?cs var:qz_metadata.ugcshop ?>" class="c_tx">查看全部"<?cs var:qz_metadata.reply.rreplycount ?>" 条回复&gt;&gt;</a>
			<?cs /if ?>
			<?cs if:qz_metadata.optype == 14 || qz_metadata.optype == 15 ?>
				<a target="_blank" href="http://rc.qzone.qq.com/myhome/qqmeishi?medal=<?cs var:qz_metadata.uin ?>_<?cs var:qz_metadata.cid ?>_<?cs var:qz_metadata.sid ?>&amp;uc=<?cs var:qz_metadata.ugcshop ?>" class="c_tx">查看全部"<?cs var:qz_metadata.reply.rreplycount ?>"条回复&gt;&gt;</a>
			<?cs /if ?>
			<?cs if:qz_metadata.optype == 16 || qz_metadata.optype == 17 ?>
				<a target="_blank" href="http://rc.qzone.qq.com/myhome/qqmeishi?king=<?cs var:qz_metadata.uin ?>_<?cs var:qz_metadata.cid ?>_<?cs var:qz_metadata.sid ?>&amp;uc=<?cs var:qz_metadata.ugcshop ?>" class="c_tx">查看全部"<?cs var:qz_metadata.reply.rreplycount ?>"条回复&gt;&gt;</a>
			<?cs /if ?>
		</div>
		<?cs /if ?>
			<?cs def:feeds_rreply-items(item) ?>
				<?cs if:item.rreplycount > 0 ?>
					<?cs def:feeds_rreply_comment-items(rreply_item) ?>
						<div class="feeds_comm_list bg2">
							<div class="feeds_comment_cont">
								<p class="feeds_comment_text">
									<a class="q_namecard q_des comment_nickname c_tx" href="http://user.qzone.qq.com/<?cs var:rreply_item.uin ?>" target="_blank" link="nameCard_<?cs var:rreply_item.uin ?> des_<?cs var:rreply_item.uin ?>"><?cs var:rreply_item.nick ?><span class="none"></span></a><?cs var:rreply_item.msg ?><span class="none"></span>
								</p>
								<p class="feeds_comment_op">
									<span class="feeds_time c_tx3"><?cs var:rreply_item.replydate ?></span>
								</p>
							</div>
						</div>
					<?cs /def ?>
					<?cs if:item.rreply.0 || subcount(item.rreply.0) > 0 ?>
						<?cs each:rreply_item = item.rreply ?>
							<?cs call:feeds_rreply_comment-items(rreply_item) ?>
						<?cs /each ?>
					<?cs elif:item.rreply || subcount(item.rreply) > 0 ?>
						<?cs call:feeds_rreply_comment-items(item.rreply) ?>
					<?cs /if ?>					
				<?cs /if ?>
				<qz:reply action="http://meishi.qzone.qq.com/ajax/add_rep_lv2_meta.php" param="<?cs var:qz_metadata.cid ?>,<?cs var:item.rid ?>,<?cs var:qz_metadata.optype ?>" type="text" charset="UTF-8" maxLength="200" version="6" btnstyle="6.1">回复</qz:reply>
			<?cs /def ?>	
			<?cs if:qz_metadata.reply.0 || subcount(qz_metadata.reply.0) > 0 ?>
				<?cs each:item = qz_metadata.reply ?>
					<?cs call:feeds_rreply-items(item) ?>
				<?cs /each ?>
			<?cs elif:qz_metadata.reply || subcount(qz_metadata.reply) > 0 ?>
				<?cs call:feeds_rreply-items(qz_metadata.reply) ?>
			<?cs /if ?>			
	</div>
</div>
