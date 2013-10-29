<?cs def:imageViewer(map) ?>
	<div class="img_thumb">
	<?cs if:string.length(map.dynamicurl) > 0 ?>
		<a onclick="QZFL.widget.simpleImageViewer.show(this.parentNode,'<?cs var:map.bigurl ?>', '<?cs var:map.dynamicurl ?>',  410, 0, 0);return false;" href="javascript:;">
	<?cs else ?>
		<a onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs var:map.bigurl ?>', '<?cs var:map.bigurl ?>', 410, 0, 0);return false;" href="javascript:;">
	<?cs /if ?>
			<img alt="点击查看大图" title="点击放大" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(160,150,'<?cs var:map.smallurl ?>');" />
		</a>
	</div>
<?cs /def ?>


<?cs if:qz_metadata.albumid ?>
	<div class="feeds_tp_1">
		<?cs if:string.length(qz_metadata.photodesc)>1 ?>
			<div class="txtbox quote_txt">
				<p>
					<strong class="quotes_symbols_left c_tx3">“</strong>
					<?cs var:qz_metadata.photodesc ?>
						<strong class="quotes_symbols_right c_tx3">”</strong>
				</p>
			</div>
		<?cs /if ?>
		<?cs call:imageViewer(qz_metadata) ?>
		<?cs if:subcount(qz_metadata.qz_reply)>0 ?>
			<div class="feeds_comment">
				<?cs if:qz_metadata.omit>0 ?>
					<div class="more_feeds_comment bg2">
						<a class="c_tx" href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/photo/<?cs var:qz_metadata.albumid ?>/" target="_blank">查看全部<?cs var:qz_metadata.total ?>条评论>></a>
					</div>
				<?cs /if ?>
				<?cs if:subcount(qz_metadata.comment)==0 ?>
					<qz:reply action="<?cs var:qz_metadata.qz_reply.action ?>" param="<?cs var:qz_metadata.qz_reply.param ?>" type="ubb" charset="UTF8" maxLength="150" version="6.3" btnstyle="<?cs var:qz_metadata.extend.qz_reply.btnstyle ?>">评论</qz:reply>
				<?cs /if ?>
				<?cs if:subcount(qz_metadata.comment.0)>0 ?>
					<?cs each:item=qz_metadata.comment ?>
						<div class="feeds_comm_list bg2">
							<div class="feeds_comment_cont">
								<p class="feeds_comment_text">
									<?cs if:item.uin_type==1 ?>
										<a href="http://xiaoyou.qq.com/index.php?mod=profile&amp;u=<?cs var:item.uin ?>" target="_blank" class="comment_nickname c_tx">
											<?cs var:item.nick ?>
										</a>
									<?cs elif:item.uin_type==2 ?>
										<a href="http://bai.qq.com/index.php?mod=profile&amp;u=<?cs var:item.uin ?>" target="_blank" class="comment_nickname c_tx">
											<?cs var:item.nick ?>
										</a>
									<?cs else ?>
										<a href="http://user.qzone.qq.com/<?cs var:item.uin ?>/" target="_blank" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" class="comment_nickname c_tx q_namecard q_des">
											<?cs var:item.nick ?>
										</a>
									<?cs /if ?>
									<?cs var:item.content ?>
								</p>
								<p class="feeds_comment_op">
									<span class="feeds_time c_tx3"><?cs var:item.date ?></span>
								</p>
							</div> 
						</div>
					<?cs /each ?>
				<?cs elif:qz_metadata.comment.uin ?>
					<div class="feeds_comm_list bg2">
						<div class="feeds_comment_cont">
							<p class="feeds_comment_text">
								<?cs if:qz_metadata.comment.uin_type==1 ?>
									<a href="http://xiaoyou.qq.com/index.php?mod=profile&amp;u=<?cs var:qz_metadata.comment.uin ?>" target="_blank" class="comment_nickname c_tx">
										<?cs var:qz_metadata.comment.nick ?>
									</a>
								<?cs elif:qz_metadata.comment.uin_type==2 ?>
									<a href="http://bai.qq.com/index.php?mod=profile&amp;u=<?cs var:qz_metadata.comment.uin ?>" target="_blank" class="comment_nickname c_tx">
										<?cs var:qz_metadata.comment.nick ?>
									</a>
								<?cs else ?>
									<a href="http://user.qzone.qq.com/<?cs var:qz_metadata.comment.uin ?>/" target="_blank" link="nameCard_<?cs var:qz_metadata.comment.uin ?> des_<?cs var:qz_metadata.comment.uin ?>" class="comment_nickname c_tx q_namecard q_des">
										<?cs var:qz_metadata.comment.nick ?>
									</a>
								<?cs /if ?>
								<?cs var:qz_metadata.comment.content ?>
							</p>
							<p class="feeds_comment_op">
								<span class="feeds_time c_tx3"><?cs var:qz_metadata.comment.date ?></span>
							</p>
						</div> 
					</div>
				<?cs /if ?>
				<?cs if:subcount(qz_metadata.comment)>0 ?>
					<qz:reply action="<?cs var:qz_metadata.qz_reply.action ?>" param="<?cs var:qz_metadata.qz_reply.param ?>" type="ubb" charset="UTF8" maxLength="150" version="6" btnstyle="6.1">回复</qz:reply>
				<?cs /if ?>
			</div>
		<?cs /if ?>
	</div>
<?cs /if ?>