<?cs if:qz_metadata.albumid ?>
	<?cs if:qz_metadata.privacy==1 || qz_metadata.privacy==4 ?>
		<qz:data key="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/photo/<?cs var:qz_metadata.albumid ?>" />
		<div class="feeds_tp_1">
			<?cs if:string.length(qz_metadata.albumdesc)>2 ?>
				<div class="txtbox quote_txt">
					<p><strong class="quotes_symbols_left c_tx3">“</strong><?cs var:qz_metadata.albumdesc ?><strong class="quotes_symbols_right c_tx3">”</strong></p>
				</div>
			<?cs /if ?>
			<?cs if:subcount(qz_metadata.piclist)>0 ?>
				<div class="imgbox"><?cs if:subcount(qz_metadata.piclist.0)>0 ?>
					<?cs each:item=qz_metadata.piclist ?>
						<qz:popup param="<?cs var:qz_metadata.uin ?>$||^<?cs var:qz_metadata.albumid ?>$||^<?cs var:item.photoid ?>$||^<?cs var:item.photourl ?>$||^<?cs var:qz_metadata.feedtime ?>" src="/qzone/photo/zone/icenter_popup.html#timeStamp=<?cs var:qz_metadata.feedtime ?>" version="2" title="<?cs var:qz_metadata.albumname ?>">
							<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:item.photourl ?>');"/>
						</qz:popup>
					<?cs /each ?>
			<?cs elif:subcount(qz_metadata.piclist)>0 ?>
				<qz:popup param="<?cs var:qz_metadata.uin ?>$||^<?cs var:qz_metadata.albumid ?>$||^<?cs var:qz_metadata.piclist.photoid ?>$||^<?cs var:qz_metadata.piclist.photourl ?>$||^<?cs var:qz_metadata.feedtime ?>" src="/qzone/photo/zone/icenter_popup.html#timeStamp=<?cs var:qz_metadata.feedtime ?>" version="2" title="<?cs var:qz_metadata.albumname ?>">
				<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(160,150,'<?cs var:qz_metadata.piclist.photourl ?>');"/>
				</qz:popup>
			<?cs /if ?>
		</div>
	<?cs /if ?>
	<div class="feeds_tp_operate">
		<qz:popup title="添加相册到我的分享" param="2|<?cs var:qz_metadata.albumid ?>$||^<?cs var:qz_metadata.uin ?>$||^<?cs var:qz_metadata.albumname_en ?>$||^<?cs var:qz_metadata.albumdesc_en ?>$||^<?cs var:qz_metadata.nick_en ?>$||^<?cs var:qz_metadata.albumcoverurl_en ?>$||^<?cs var:qz_metadata.picnum ?>" src="/qzone/photo/zone/sharecanvas.html" width="432" height="300">
		<a href="#" onclick="return false;" class="c_tx">分享</a>
		</qz:popup>
	</div>
	<?cs if:subcount(qz_metadata.qz_reply)>0 ?>
		<div class="feeds_comment">
			<div class="comment_arrow c_bg2">◆</div>
			<?cs if:qz_metadata.omit>0 ?>
				<div class="more_feeds_comment bg2">
					<a class="c_tx" href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/photo/<?cs var:qz_metadata.albumid ?>/" target="_blank">查看全部<?cs var:qz_metadata.total ?>条评论>></a>
				</div>
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
								<?cs var:item.msg ?>
							</p>
							<p class="feeds_comment_op">
								<span class="feeds_time c_tx3"><?cs var:item.date ?></span>
							</p>
						</div>
						<?cs if:subcount(item.reply.0)>0 ?>
							<?cs each:item2=item.reply ?>
								<div class="comment_reply_list">
									<div class="bbor5">
										<div class="comment_reply_cont">
											<p class="comment_reply_text">
												<?cs if:item2.uin_type==1 ?>
													<a href="http://xiaoyou.qq.com/index.php?mod=profile&amp;u=<?cs var:item2.uin ?>" target="_blank" class="comment_nickname c_tx">
														<?cs var:item2.nick ?>
													</a>
												<?cs elif:item2.uin_type==2 ?>
													<a href="http://bai.qq.com/index.php?mod=profile&amp;u=<?cs var:item2.uin ?>" target="_blank" class="comment_nickname c_tx">
														<?cs var:item2.nick ?>
													</a>
												<?cs else ?>
													<a href="http://user.qzone.qq.com/<?cs var:item2.uin ?>/" target="_blank" link="nameCard_<?cs var:item2.uin ?> des_<?cs var:item2.uin ?>" class="comment_nickname c_tx q_namecard q_des">
														<?cs var:item2.nick ?>
													</a>
												<?cs /if ?>
												<?cs var:item2.msg ?>
											</p>
											<p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:item2.date ?></span></p>
										</div>
									</div>
								</div>
							<?cs /each ?>
						<?cs elif:subcount(item.reply)>0 ?>
							<div class="comment_reply_list">
								<div class="bbor5">
									<div class="comment_reply_cont">
										<p class="comment_reply_text">
											<?cs if:item.reply.uin_type==1 ?>
												<a href="http://xiaoyou.qq.com/index.php?mod=profile&amp;u=<?cs var:item.reply.uin ?>" target="_blank" class="comment_nickname c_tx">
													<?cs var:item.reply.nick ?>
												</a>
											<?cs elif:item.reply.uin_type==2 ?>
												<a href="http://bai.qq.com/index.php?mod=profile&amp;u=<?cs var:item.reply.uin ?>" target="_blank" class="comment_nickname c_tx">
													<?cs var:item.reply.nick ?>
												</a>
											<?cs else ?>
												<a href="http://user.qzone.qq.com/<?cs var:item.reply.uin ?>/" target="_blank" link="nameCard_<?cs var:item.reply.uin ?> des_<?cs var:item.reply.uin ?>" class="comment_nickname c_tx q_namecard q_des">
													<?cs var:item.reply.nick ?>
												</a>
											<?cs /if ?>
											<?cs var:item.reply.msg ?>
										</p>
										<p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:item.reply.date ?></span></p>
									</div>
								</div>
							</div>
						<?cs /if ?>
					</div>
				<?cs /each ?>
			<?cs /if ?>
			<?cs if:subcount(qz_metadata.qz_reply)>0 ?>
				<qz:reply action="<?cs var:qz_metadata.qz_reply.action ?>" param="<?cs var:qz_metadata.qz_reply.param ?>" type="ubb" charset="UTF8" maxLength="150" version="6" btnstyle="6.1">回复</qz:reply>
			<?cs /if ?>
		</div>
	<?cs /if ?>
	</div>
	<?cs /if ?>
	<?cs if:qz_metadata.privacy==5||qz_metadata.privacy==2 ?>
		<div class="feeds_tp_7">
			<div class="feeds_tp_operate">
				<span class="encrypt_feeds c_tx3">该相册需回答问题访问</span>
				<a class="c_tx" href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/photo/<?cs var:qz_metadata.albumid ?>/" target="_blank">点击查看</a>
			</div>
	<?cs /if ?>
<?cs /if ?>
