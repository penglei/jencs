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
		<?cs if:qz_metadata.lloc ?><?cs if:qz_metadata.uin ?><?cs set:qz_key_uin = qz_metadata.uin ?><?cs else ?><?cs set:qz_key_uin = qz_metadata.zzuin ?><?cs /if ?><qz:data key="http://user.qzone.qq.com/<?cs var:qz_key_uin ?>/photo/<?cs var:qz_metadata.albumid ?>/<?cs var:qz_metadata.lloc ?>" /><?cs /if ?>
		<div class=" feeds_tp_1">
			<?cs if:qz_metadata.feedtitle!=1 ?>
				<?cs if:string.length(qz_metadata.photodesc)>1 ?>
					<div class="txtbox_quote_txt">
						<p><strong class="quotes_symbols_left c_tx3">“</strong><?cs var:qz_metadata.photodesc ?><strong class="quotes_symbols_right c_tx3">”</strong></p>
					</div>
				<?cs /if ?>
			<?cs /if ?>
			<?cs call:imageViewer(qz_metadata) ?>
			<div class="feeds_tp_operate">
				<qz:popup title="添加照片到我的分享" param="3|<?cs var:qz_metadata.albumid ?>$||^<?cs var:qz_metadata.albumname_en ?>$||^<?cs var:qz_metadata.lloc ?>$||^<?cs var:qz_metadata.uin ?>$||^<?cs var:qz_metadata.photoname_en ?>$||^<?cs var:qz_metadata.photodesc_en ?>$||^<?cs var:qz_metadata.nick_en ?>$||^<?cs var:qz_metadata.smallurl_en ?>$||^<?cs var:qz_metadata.bigurl_en ?>" src="/qzone/photo/zone/sharecanvas.html" width="432" height="300">
						分享
				</qz:popup>
				<qz:popup title="转载到我的相册" param="<?cs var:qz_metadata.uin ?>$||^<?cs var:qz_metadata.photoname ?>$||^<?cs var:qz_metadata.photodesc ?>$||^<?cs var:qz_metadata.uin ?>$||^<?cs var:qz_metadata.phototype ?>$||^<?cs var:qz_metadata.lloc ?>$||^<?cs var:qz_metadata.sloc ?>$||^<?cs var:qz_metadata.uin ?>$||^<?cs var:qz_metadata.nick ?>$||^<?cs var:qz_metadata.albumid ?>$||^<?cs var:qz_metadata.photocubage ?>$||^<?cs var:qz_metadata.width ?>$||^<?cs var:qz_metadata.height ?>$||^<?cs var:qz_metadata.bigurl_en ?>$||^<?cs var:qz_metadata.photoname ?>$||^<?cs var:qz_metadata.photodesc ?>" src="/qzone/photo/zone/icenter_reprint.html" width="470" height="265">
					转载
				</qz:popup>
			</div>
			<?cs if: subcount(qz_metadata.qz_reply)>0 ?>
				<div class="feeds_comment">
					<div class="comment_arrow c_bg2">◆</div>
					<?cs if:qz_metadata.omit>0 ?>
						<div class="more_feeds_comment bg2">
							<a class="c_tx" href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/photo/<?cs var:qz_metadata.albumid ?>/<?cs var:qz_metadata.lloc ?>/" target="_blank">查看全部<?cs var:qz_metadata.total ?>条评论>></a>
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
									<p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:item.date ?></span></p>
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