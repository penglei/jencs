<?cs #:评论组件评论组件 ?>
<?cs def:randerLikeInfo(mod) ?>
	<?cs if: qz_metadata.qz_data.version==1 ?>
		<?cs if:qz_metadata.qz_data.key2.LIKE.cnt>0 ?>
			<?cs set:qz_data_key='key2' ?>
		<?cs elif:qz_metadata.qz_data.key1.LIKE.cnt>0 ?>
			<?cs set:qz_data_key='key1' ?>
		<?cs else ?>
			<?cs set:qz_data_key='key2' ?>
		<?cs /if ?>
		<?cs def:setFriend() ?>
			<?cs #:限制最多展示三个好友 ?>
			<?cs set:len=subcount(qz_metadata.qz_data[qz_data_key].LIKE.user) ?>
			<?cs if:len>6 ?>
				<?cs set:len=5 ?>
			<?cs elif:len==0&&qz_metadata.qz_data[qz_data_key].LIKE.user ?>
				<?cs call:userLink(qz_metadata.qz_data[qz_data_key].LIKE.user,'') ?>
			<?cs else ?>
				<?cs set:len=len-1 ?>
			<?cs /if ?>
			<?cs if:subcount(qz_metadata.qz_data[qz_data_key].LIKE.user[0]) ?>
				<?cs loop:i = 0, len, 1 ?>
					<?cs call:userLink(qz_metadata.qz_data[qz_data_key].LIKE.user[i],'') ?><?cs if:i<len ?>、<?cs /if ?>
				<?cs /loop ?>
			<?cs /if ?>
		<?cs /def ?>
		<?cs #:likeinfo表示好友赞的人数 ?>
		<?cs if:subcount(qz_metadata.qz_data[qz_data_key].LIKE.user)>0 ?>
			<?cs set:likeinfo=subcount(qz_metadata.qz_data[qz_data_key].LIKE.user) ?>
		<?cs elif:subcount(qz_metadata.qz_data[qz_data_key].LIKE.user[0]) ?>
			<?cs set:likeinfo = 1 ?>
		<?cs else ?>
			<?cs set:likeinfo = 0 ?>
		<?cs /if ?>
		<?cs if:likeinfo>6 ?>
			<?cs set:likeinfo=6 ?>
		<?cs /if ?>
		<?cs if:mod.like=='on'&&qz_metadata.qz_data[qz_data_key].LIKE.cnt>0 ?>
			<div class="f_like _likeInfo" likeinfo="<?cs var:likeinfo ?>">
				<i class="ui_ico icon_hand ui_ico_f qz_like_btn_v3 hand" style="cursor: pointer;"></i>
				<span class="_ilike">
					<?cs if:qz_metadata.qz_data[qz_data_key].LIKE.isliked ?>我<?cs if:qz_metadata.qz_data[qz_data_key].LIKE.cnt > 1?>和<?cs /if ?><?cs /if ?>
				</span>
				<?cs call:setFriend() ?>
				<?cs set:_randerLikeInfo_num="" ?>
				<?cs if:likeinfo>0 && (qz_metadata.qz_data[qz_data_key].LIKE.cnt-likeinfo-qz_metadata.qz_data[qz_data_key].LIKE.isliked)>0 ?>
					<?cs set:_randerLikeInfo_tmp=qz_metadata.qz_data[qz_data_key].LIKE.cnt ?>
					<?cs set:_randerLikeInfo_pre="等" ?>
					<?cs set:_randerLikeInfo_num=(_randerLikeInfo_tmp+"人") ?>
					<?cs set:_randerLikeInfo_tail="都" ?>
				<?cs elif:likeinfo<=0 ?>
						<?cs set:_randerLikeInfo_pre="" ?>
						<?cs set:_randerLikeInfo_tail="" ?>
						<?cs if:qz_metadata.qz_data[qz_data_key].LIKE.isliked>0 ?>
							<?cs set:_randerLikeInfo_tmp=(qz_metadata.qz_data[qz_data_key].LIKE.cnt-1) ?>
							<?cs if:_randerLikeInfo_tmp>=1 ?>
								<?cs set:_randerLikeInfo_num=(_randerLikeInfo_tmp+"人") ?>
							<?cs /if ?>
						<?cs elif:qz_metadata.qz_data[qz_data_key].LIKE.cnt>0 ?>
							<?cs set:_randerLikeInfo_tmp=qz_metadata.qz_data[qz_data_key].LIKE.cnt ?>
							<?cs set:_randerLikeInfo_num=(_randerLikeInfo_tmp+"人") ?>
						<?cs /if ?>
				<?cs /if ?>
				<?cs if:qz_metadata.qz_data.key2 ?>
					<?cs set:unikey=qz_metadata.qz_data.key2 ?>
				<?cs elif:qz_metadata.qz_data.key1 ?>
					<?cs set:unikey=qz_metadata.qz_data.key1 ?>
				<?cs /if ?>
				<span class="_likecnt <?cs if:likeinfo>0 ?>ui_ml5<?cs /if ?>"><?cs var:_randerLikeInfo_pre ?><a href="javascript:;" class="c_tx _showLikeList" data-unikey="<?cs var:unikey ?>" ><?cs var:_randerLikeInfo_num ?></a><?cs var:_randerLikeInfo_tail ?></span>
				觉得很赞
			</div>
		<?cs /if ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:isAuthUser() ?>
	<?cs if: qz_metadata.hdf.bitmap ?>
		<?cs if:bitmap_value_ex(qz_metadata.hdf.bitmap,52,1)>0 || bitmap_value_ex(qz_metadata.hdf.bitmap,5,1)>0 || bitmap_value_ex(qz_metadata.hdf.bitmap,7,1)>0 ?>
			<?cs set:isAuthUser_r=1 ?>
		<?cs else ?>
			<?cs set:isAuthUser_r=0 ?>
		<?cs /if ?>
	<?cs else ?>
		<?cs if: qz_metadata.noreply ?>
			<?cs set:isAuthUser_r=1 ?>
		<?cs else ?>
			<?cs set:isAuthUser_r=0 ?>
		<?cs /if ?>
	<?cs /if ?>
<?cs /def ?>
<?cs def:isAlpha() ?>
	<?cs if:bitmap_value_ex(qz_metadata.hdf.bitmap,58,1)>0?>
		<?cs set:isAlpha_r=1 ?>
	<?cs else ?>
		<?cs set:isAlpha_r=0 ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:comment(mod) ?>
	<?cs if:qz_metadata.metadata.uin == qz_metadata.metadata.useruin ?>
		<?cs set:qz_isOwner = 1 ?>
	<?cs else ?>
		<?cs set:qz_isOwner = 0 ?>
	<?cs /if ?>
	<?cs if:qz_metadata.qz_data.key2.LIKE.cnt>0 ?>
		<?cs set:qz_data_key='key2' ?>
	<?cs elif:qz_metadata.qz_data.key1.LIKE.cnt>0 ?>
		<?cs set:qz_data_key='key1' ?>
	<?cs else ?>
		<?cs set:qz_data_key='key2' ?>
	<?cs /if ?>
	<?cs #:如果没有评论内容的话,这个箭头就不显示了,设计师,我们伤不起啊. ?>
	<?cs if:qz_metadata.comments.comment.0 || subcount(qz_metadata.comments.comment.0) > 0 || subcount(qz_metadata.comments.comment) > 0 || (mod.like=='on' && qz_metadata.qz_data[qz_data_key].LIKE.cnt>0)?>
		<?cs set:qz_arrow_display = '' ?>
	<?cs else ?>
		<?cs set:qz_arrow_display = 'none' ?>
	<?cs /if ?>
	<?cs def:comment_icon(cmt)?>
		<?cs if:subcount(cmt.icon) > 0?>
			<a href="<?cs var:cmt.icon.action?>" target="_blank"><img src="<?cs var:cmt.icon.src?>" alt="" class="icon_feed_postlist_img" /></a>
		<?cs /if?>
	<?cs /def?>
<div class="feeds_comment_v2">
	<div class="f_ang_t bor2 qz_comment_arrow <?cs var:qz_arrow_display ?>">
		<div class="ang_i ang_t_d bor2"></div>
		<div class="ang_i ang_t_u bor_bg"></div>
	</div>
	<?cs call:randerLikeInfo(mod) ?>
	<?cs if:subcount(qz_metadata.comments) > 0 ?>
		<?cs set:cmtdata = qz_metadata.comments ?>
		<?cs def:comment-items(item) ?>
			<li class="comments_item bor3" data-type="comment" data-tid="<?cs var:item.commentid ?>" data-uin="<?cs var:item.user.uin ?>">
				<div class="comments_item_bd">
					<div class="ui_avatar"><?cs #:头像 ?><?cs call:userIcon(item.user) ?></div>
					<div class="comments_content">
						<?cs #:昵称 ?><?cs call:cmtUserLink(item.user, '') ?>
						<?cs #:评论内容 ?> : 
						<?cs call:comment_icon(item)?>
						<?cs call:richContent(item.content) ?>
						<div class="comments_op">
							<span class="c_tx3 ui_mr10"><?cs #:日期 ?><?cs var:item.time.text ?></span>

						<?cs if:qz_metadata.metadata.fwd_config>0 ?>
							<span class="ui_mr10">
								<a 
									data-cmd="qz_popup" 
									data-isnewtype="1" 
									href="javascript:void(0)" 
									data-version="4" 
									data-type="RetweetBox" 
									data-needcontainer="2" 
									data-scene="comment" 
									data-src="/qzone/app/mood/retweetBoxFacade.js" 
									data-link="/qzonestyle/qzone_app/app_feeds_v1/mood_feeds.css" 
									class="c_tx ui_mr10" >
									转发
								</a>
							</span>
						<?cs /if ?>

						<?cs if:qz_metadata.metadata.reshare_config > 0?>
							<span class="ui_mr10">
								<a 
									data-cmd="qz_popup" 
									href="javascript:void(0)" 
									data-version="1" 
									data-height="150" 
									data-width="435" 
									title="分享" 
									style="margin-right:0;" 
									class="c_tx ui_mr10" 
											data-src="http://qzs.qq.com/qzone/app/qzshare/popup.html#uin=<?cs var:qz_metadata.metadata.uin ?>&itemid=<?cs var:qz_metadata.metadata.blogid ?>&reshare=1&charset=gbk<?cs call:buildReshareParam(1, item, null) ?>" 
								>
									分享
								</a>
							</span>
						<?cs /if ?>


						<?cs if:subcount(item.qz_reply) > 0 ?>
							<span class="ui_mr10">
								<qz:reply 
									version="6.<?cs alt:item.qz_reply.btnstyle?>2<?cs /alt ?>" 
									action="<?cs var:item.qz_reply.action ?>" 
									param="<?cs var:item.qz_reply.param ?>" 
									charset="<?cs var:item.qz_reply.charset ?>" 
									maxLength="<?cs var:item.qz_reply.maxlength ?>" 
									tuin="<?cs var:item.qz_reply.tuin ?>" 
									config="<?cs var:item.qz_reply.config ?>"
								>
									回复
								</qz:reply>
							</span>
						<?cs /if ?>

						<?cs if:subcount(item.qz_audit) > 0 ?>
							<a class="c_tx ui_mr10" 
								href="javascript:;" 
								onclick="QZONE.ICFeeds.Interface.auditPassExtend({dataonly:1,src:'<?cs var:item.qz_audit.src ?>',param:'<?cs var:item.qz_audit.param ?>'})"
							>
								通过审核
							</a>
						<?cs /if ?>

						<?cs if:qz_isOwner && item.qz_delete.action ?>
							<span class="ui_mr10">
								<qz:delete action="<?cs var:item.qz_delete.action ?>" 
									param="<?cs var:item.qz_delete.param ?>">
									删除
								</qz:delete>
							</span>
						<?cs /if ?>
						</div>
					</div>

						<?cs def:reply-items(comment,item) ?>
							<li class="comments_item bor3" data-type="reply" data-tid="<?cs var:item.replyid ?>" data-uin="<?cs var:item.user.uin ?>">
								<div class="comments_item_bd">
									<div class="ui_avatar"><?cs #:头像 ?><?cs call:userIcon(item.user) ?></div>
									<div class="comments_content">
										<?cs #:昵称 ?><?cs call:cmtUserLink(item.user, '') ?>
										<?cs #:评论内容 ?> : <?cs call:richContent(item.content) ?>

										<div class="comments_op">
											<span class="c_tx3 ui_mr10"><?cs #:日期 ?><?cs var:item.time.text ?></span>

										<?cs if:qz_metadata.metadata.fwd_config>1 ?>
											<span class="ui_mr10">
												<a 
													data-cmd="qz_popup" 
													href="javascript:void(0)" 
													data-version="4" 
													data-isnewtype="1"
													data-type="RetweetBox" 
													data-needcontainer="2" 
													data-scene="reply" 
													data-src="/qzone/app/mood/retweetBoxFacade.js" 
													class="c_tx ui_mr10" 
													data-link="/qzonestyle/qzone_app/app_feeds_v1/mood_feeds.css"
												>
													转发
												</a>
											</span>
										<?cs /if ?>
										<?cs if:qz_metadata.metadata.reshare_config > 1 ?>
											<span class="ui_mr10">
												<a 
													data-cmd="qz_popup" 
													href="javascript:void(0)" 
													data-version="1" 
													data-height="150" 
													data-width="435" 
													title="分享" 
													style="margin-right:0;" 
													class="c_tx ui_mr10" 
														data-src="http://qzs.qq.com/qzone/app/qzshare/popup.html#uin=<?cs var:qz_metadata.metadata.uin ?>&itemid=<?cs var:qz_metadata.metadata.blogid ?>&reshare=2&charset=gbk<?cs call:buildReshareParam(2, comment, item) ?>" 
													>
													分享
												</a>
											</span>
										<?cs /if ?>
										<?cs if:subcount(item.qz_reply) > 0 ?>
											<span class="ui_mr10">
												<qz:reply 
													version="6.4" 
													action="<?cs var:item.qz_reply.action ?>" 
													param="<?cs var:item.qz_reply.param ?>" 
													charset="<?cs var:item.qz_reply.charset ?>" 
													maxLength="<?cs var:item.qz_reply.maxlength ?>" 
													tuin="<?cs var:item.qz_reply.tuin ?>" 
													config="<?cs var:item.qz_reply.config ?>"
												>
												回复
												</qz:reply>
											</span>
										<?cs /if ?>

										<?cs if:subcount(item.qz_audit) > 0 ?>
											<a class="c_tx ui_mr10" 
												href="javascript:;" 
												onclick="QZONE.ICFeeds.Interface.auditPassExtend({dataonly:1,src:'<?cs var:item.qz_auidit.src ?>',param:'<?cs var:item.qz_audit.param ?>'})">
												通过审核
											</a>
										<?cs /if ?>

										<?cs if:qz_isOwner && item.qz_delete.action ?>
											<span class="ui_mr10">
												<qz:delete 
													action="<?cs var:item.qz_delete.action ?>" 
													param="<?cs var:item.qz_delete.param ?>"
												>删除</qz:delete>
											</span>
										<?cs /if ?>
									</div>
								</div>
							</div>
						</li>
					<?cs /def ?>
					<?cs if:qz_metadata.qz_data.feedtype!=1&&isAuthUser_r==1 && item.reply_count && item.reply_count>=0 ?>
						<?cs set:reply_loop=1 ?>
						<?cs set:reply_count=item.reply_count ?>
					<?cs elif:(qz_metadata.qz_data.feedtype==1||!isAuthUser_r) && (item.replies.reply.0 || subcount(item.replies.reply.0) > 0)?>
						<?cs set:reply_count=subcount(item.replies.reply) ?>
					<?cs elif:(qz_metadata.qz_data.feedtype==1||!isAuthUser_r) && subcount(item.replies.reply) > 0 ?>
						<?cs set:reply_count=1 ?>
					<?cs else ?>
						<?cs set:reply_count=0 ?>
					<?cs /if ?>

					<?cs if:reply_count > 0 ?>
					<div class="comments_list mod_comments_sub">
						<?cs if:item.replies.qz_more.action ?>
							<div class="comments_list_more">
							<qz:more 
								action="<?cs var:item.replies.qz_more.action ?>" 
								param="<?cs var:item.replies.qz_more.param ?>" 
								charset="<?cs var:item.replies.qz_more.charset ?>" 
							>展开剩余<?cs var:item.replies.qz_more.count ?>条回复</qz:more></div>
						<?cs /if ?>
						<?cs if:item.replies.qz_more.url ?>
							<div class="comments_list_more">
								<a href="<?cs var:item.replies.qz_more.url ?>" class="c_tx" target="_blank">
									查看剩余<?cs var:item.replies.qz_more.count ?>条回复
								</a>
							</div>
						<?cs /if ?>
						<ul>

						<?cs if:reply_count >1||(reply_count==1 && qz_metadata.qz_data.feedtype!=1&&isAuthUser_r==1) ?>
							<?cs loop:i =0, reply_count-1, 1 ?>
								<?cs call:reply-items(item,item.replies.reply[i]) ?>
							<?cs /loop ?>
						<?cs elif:reply_count > 0 ?>
							<?cs call:reply-items(item,item.replies.reply) ?>
						<?cs /if ?>
						</ul>
					</div>
					<?cs /if ?>
				</div>
			</li>
		<?cs /def ?>
		<?cs if:qz_metadata.qz_data.feedtype!=1&&isAuthUser_r==1 && qz_metadata.comments.comment_count && qz_metadata.comments.comment_count>=0 ?>
			<?cs set:commont_num=qz_metadata.comments.comment_count ?>
			<?cs set:commont_loop=1 ?>
		<?cs elif:(qz_metadata.qz_data.feedtype==1||!isAuthUser_r) &&( qz_metadata.comments.comment.0 || subcount(qz_metadata.comments.comment.0) > 0 ) ?>
			<?cs set:commont_num=subcount(qz_metadata.comments.comment) ?>
		<?cs elif:(qz_metadata.qz_data.feedtype==1||!isAuthUser_r) && subcount(qz_metadata.comments.comment) > 0 ?>
			<?cs set:commont_num=1 ?>
		<?cs else ?>
			<?cs set:commont_num=0 ?>
		<?cs /if ?>
		<?cs if:commont_num>0 ?> 
			<div class="mod_comments"> 
				<?cs #:这里如果是认证空间,要把节点隐藏掉,避免空节点 ?>
				<?cs #:如果没有好友评论就隐藏吧 ?>

				<div class="comments_list <?cs if:isAuthUser_r==1 && commont_num==0 ?>none<?cs /if ?>">
					<?cs if:qz_metadata.totalcomment > 3 && qz_metadata.comments.qz_more.action ?>
						<div class="comments_list_more">
						<qz:more 
							action="<?cs var:qz_metadata.comments.qz_more.action ?>" 
							param="<?cs var:qz_metadata.comments.qz_more.param ?>" 
							charset="<?cs var:qz_metadata.comments.qz_more.charset ?>" 
						>展开剩余<?cs var:qz_metadata.comments.qz_more.count ?>条评论</qz:more>
						</div>
					<?cs /if ?>
					<?cs if:(qz_metadata.metadata.appid==311  || qz_metadata.metadata.appid==4) ?>
						<?cs if:(isAuthUser_r==1 && commont_num<qz_metadata.totalcomment && qz_metadata.comments.qz_more.url)||(isAuthUser_r==0 &&qz_metadata.comments.qz_more.url) ?>
							<div class="comments_list_more"><a href="<?cs var:qz_metadata.comments.qz_more.url ?>" class="c_tx" target="_blank">查看剩余全部评论</a></div>
						<?cs /if ?>
					<?cs else ?>
						<?cs if:!isAuthUser_r && qz_metadata.comments.qz_more.url ?>
							<div class="comments_list_more"><a href="<?cs var:qz_metadata.comments.qz_more.url ?>" class="c_tx" target="_blank">查看剩余<?cs var:qz_metadata.comments.qz_more.count ?>条评论</a></div>
						<?cs /if ?>
					<?cs /if ?>
					<?cs if:commont_num > 1  || (commont_num==1&&qz_metadata.qz_data.feedtype!=1&&isAuthUser_r==1) ?>
						<ul>
						<?cs loop:i = 0, commont_num-1, 1 ?>
							<?cs call:comment-items(qz_metadata.comments.comment[i]) ?>
						<?cs /loop ?>
						</ul>
					<?cs elif:commont_num > 0 ?>
						<ul>
							<?cs call:comment-items(qz_metadata.comments.comment) ?>
						</ul>
					<?cs /if ?>
				</div>
			</div>
		<?cs /if ?>
		<?cs if:subcount(qz_metadata.comments.qz_reply) > 0 ?>
			<qz:reply 
				unrend="true" 
				version="6" 
				action="<?cs var:qz_metadata.comments.qz_reply.action ?>" 
				param="<?cs var:qz_metadata.comments.qz_reply.param ?>" 
				charset="<?cs var:qz_metadata.comments.qz_reply.charset ?>" 
				maxLength="<?cs var:qz_metadata.comments.qz_reply.maxlength ?>" 
				tuin="<?cs var:qz_metadata.comments.qz_reply.tuin ?>" 
				config="<?cs var:qz_metadata.comments.qz_reply.config ?>">

				<div class="mod_commnets_poster bg2">
					<div class="comments_poster_bd comments_poster_default">
						<div class="comments_box">
							<div class="textinput textinput_default bor2" alt="replybtn" placeholder="我也说一句"><a class="c_tx3" href="javascript:void(0);" alt="replybtn">我也说一句</a></div>
						</div>
					</div>
				</div>

			</qz:reply>
		<?cs /if ?>
	<?cs /if ?>
</div>
<?cs /def ?>