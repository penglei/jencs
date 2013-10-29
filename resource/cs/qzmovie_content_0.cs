<?cs def:opts(root) ?>
	<p  class="feeds_tp_operate"> 
	
		<?cs def:opts-items(item) ?>
			<?cs if:item.fold == 1 ?>
				<qz:fc>    收起评论    </qz:fc> 
			<?cs elif:subcount(item.extend.qz_reply) > 0 ?>
				<qz:reply type="link" charset="UTF-8" maxLength="<?cs var:item.extend.qz_reply.maxLength ?>" version="6" btnstyle="1"> 
					<?cs var:item.text ?>
				</qz:reply>
			<?cs elif:subcount(item.extend.qz_popup) > 0 ?>
				<qz:popup title="<?cs var:item.extend.qz_popup.title ?>" param="<?cs var:item.extend.qz_popup.param ?>" src="<?cs var:item.extend.qz_popup.src ?>" width="<?cs var:item.extend.qz_popup.width ?>" height="<?cs var:item.extend.qz_popup.height ?>">
					<?cs var:item.text ?>
				</qz:popup>			
			<?cs else ?>
				<span  class="feeds_tp_operate"> 
					<a href="<?cs var:item.url ?>" class="c_tx" target="_blank"> 
						<?cs var:item.text ?>
					</a> 
				</span> 
			<?cs /if ?>
		<?cs /def ?>
		
		<?cs if:root.opts.opt.0 || subcount(root.opts.opt.0) > 0 ?>
			<?cs each:item = root.opts.opt ?>
				<?cs call:opts-items(item) ?>
			<?cs /each ?>
		<?cs elif:root.opts.opt || subcount(root.opts.opt) > 0 ?>
			<?cs call:opts-items(root.opts.opt) ?>
		<?cs /if ?>	
	</p> 
<?cs /def ?>

<?cs def:qz_reply(root) ?>
	<qz:reply action="<?cs var:root.reply.qz_reply.action ?>" param="<?cs var:root.reply.qz_reply.param ?>" type="<?cs var:root.reply.qz_reply.type ?>" charset="<?cs var:root.reply.qz_reply.charset ?>" maxLength="<?cs var:root.reply.qz_reply.maxLength ?>" version="6" btnstyle="<?cs var:root.reply.qz_reply.btnstyle ?>">回复</qz:reply> 
<?cs /def ?>

<?cs def:qz_reply_content(root) ?>
	<div  class="comment_reply_cont"> 
		<?cs if:string.length(root.nick.url) == 0 ?>
			<p  class="comment_reply_text"> 
			<span  style="margin-right:5px;" class="comment_nickname"> 
				<?cs var:root.nick ?>
			</span> 
			<?cs var:root.content ?>
			</p>
		<?cs /if ?>
		<?cs if:root.uin.qz_user_type == 0 || string.find(root.nick.url, 'user.qzone.qq.com') > -1 ?>
			<p  class="comment_reply_text"> 
				<a  href="<?cs var:root.nick.url ?>" target="_blank" link="nameCard_<?cs var:root.uin ?> des_<?cs var:root.uin ?>" class="comment_nickname c_tx q_namecard q_des"> 
					<?cs var:root.nick ?>
				</a> 
				<?cs var:root.content ?>
			</p> 
		<?cs else ?>
			<p  class="comment_reply_text"> 
				<a  href="<?cs var:root.nick.url ?>" target="_blank" class="comment_nickname c_tx"> 
					<?cs var:root.nick ?>
				</a> 
				<?cs var:root.content ?>
			</p> 
		<?cs /if ?>

		<p  class="comment_reply_op"> 
			<span  class="feeds_time c_tx3"> 
				<?cs var:root.datetime ?>
			</span> 
		</p> 
	</div> 
<?cs /def ?>

<?cs def:qz_replylist(root) ?>	
		<?cs if:subcount(root.comment.item) > 0 ?>
			<xsl:for-each select="comment/item"> \
			<?cs def:comment-items(item, pos) ?>
				<div  class="comment_reply_list"> 
					<?cs if:root.comment.item == pos ?>
						<div> 
							<?cs call:qz_reply_content(item) ?>
						</div> 
					<?cs else ?>
						<div  class="bbor5"> 
							<?cs call:qz_reply_content(item) ?>
						</div> 
					<?cs /if ?>
				</div> 
			<?cs /def ?>
			<?cs if:root.comment.item.0 || subcount(root.comment.item.0) > 0 ?>
				<?cs set:tmpindex = 0 ?>
				<?cs each:item = root.comment.item ?>
					<?cs call:comment-items(item, tmpindex) ?>
					<?cs set:tmpindex = tmpindex + 1 ?>
				<?cs /each ?>
			<?cs elif:root.comment.item || subcount(root.comment.item) > 0 ?>
				<?cs call:comment-items(root.comment.item, 0) ?>
			<?cs /if ?>
		<?cs /if ?>
<?cs /def ?>

<?cs def:comment(root) ?>
	<?cs if:subcount(root.reply) > 0 ?>
		<div  class="feeds_comment"> 
			<div  class="comment_arrow c_bg2">◆</div> 
			<?cs if:root.reply.totalnum > subcount(root.reply.item) ?>
				<div  class="more_feeds_comment bg2"> 
					<?cs if:root.frompingfen ?>
					<a  href="http://rc.qzone.qq.com/myhome/QzoneMovie/#action=url&url=http%3A%2F%2Flive.qq.com%2Fqzmovie%2Fonescollect.html%3Fowner%3D<?cs var:root.uin ?>" target="_blank" class="c_tx">查看全部<?cs var:root.reply.totalnum ?>条评论&gt;&gt; 
					</a> 
					<?cs else ?>
					<a  href="http://rc.qzone.qq.com/myhome/QzoneMovie/#action=topic&amp;topicid=<?cs var:root.topicid ?>&amp;owner=<?cs var:root.uin ?>" target="_blank" class="c_tx">查看全部<?cs var:root.reply.totalnum ?>条评论&gt;&gt; 
					</a> 
					<?cs /if ?>
				</div> 
			<?cs /if ?>
			<?cs def:reply-items(item) ?>
				<div  class="feeds_comm_list bg2"> 
					<div  class="feeds_comment_cont"> 
						<?cs if:item.replyuin.qz_user_type == 0 || string.find(item.replynick.url, 'user.qzone.qq.com') > -1 ?>
							<p  class="feeds_comment_text"> 
								<a  href="<?cs var:item.replynick.url ?>" target="_blank" link="nameCard_<?cs var:item.replyuin ?> des_<?cs var:item.replyuin ?>" class="comment_nickname c_tx q_namecard q_des"> 
									<?cs var:item.replynick ?>
								</a> 
								<?cs var:item.replycontent ?>
							</p> 
						<?cs else ?>
							<p  class="feeds_comment_text"> 
								<a  href="<?cs var:item.replynick.url ?>" target="_blank" class="comment_nickname c_tx"> 
									<?cs var:item.replynick ?>
								</a> 
								<?cs var:item.replycontent ?>
							</p> 
						<?cs /if ?>
						<p  class="feeds_comment_op"> 
							<span  class="feeds_time c_tx3"> 
								<?cs var:item.datetime ?>
							</span> 
							<?cs if:subcount(item.origin) > 0 ?>
								<span  class="ifeeds_origin c_tx3"> 
									<?cs var:item.origin ?>
								</span> 
							<?cs /if ?>
							<?cs if:item.replyuin.qz_user_type == 0 && subcount(item.opt) > 0 ?>
								<qz:reply action="<?cs var:item.opt.extend.qz_reply.action ?>" param="<?cs var:item.opt.extend.qz_reply.param ?>" type="<?cs var:item.opt.extend.qz_reply.type ?>" charset="<?cs var:item.opt.extend.qz_reply.charset ?>" maxLength="<?cs var:item.opt.extend.qz_reply.maxLength ?>" version="6.2">回复</qz:reply> 
							<?cs /if ?>
						</p> 
					</div> 
					<?cs call:qz_replylist(item) ?>
				</div> 
			<?cs /def ?>
			<?cs if:root.reply.item.0 || subcount(root.reply.item.0) > 0 ?>
				<?cs each:item = root.reply.item ?>
					<?cs call:reply-items(item) ?>
				<?cs /each ?>
			<?cs elif:root.reply.item || subcount(root.reply.item) > 0 ?>
				<?cs call:reply-items(root.reply.item) ?>
			<?cs /if ?>
			
			<?cs if:subcount(root.reply.qz_reply) > 0 ?>
				<?cs call:qz_reply(root) ?>
			<?cs /if ?>
		</div> 
	<?cs /if ?>
<?cs /def ?>

<div class="feeds_tp_1"> 
	<div class="imgbox"> 
		<a target="_blank" href="http://rc.qzone.qq.com/myhome/QzoneMovie/#action=detail&amp;coverid=<?cs var:qz_metadata.coverid ?>" class="c_tx"> 
			<img alt="<?cs var:qz_metadata.movietitle ?>" title="<?cs var:qz_metadata.movietitle ?>" onerror="<?cs var:qz_metadata.poster.onerror ?>" class="bor3" style="width:75px; height:105px;" src="<?cs var:qz_metadata.poster ?>"/> 
		</a>
	</div> 
	<div class="txtbox quote_txt"> 
		<?cs if:qz_metadata.summary ?>
		<p> 
			<strong class="quotes_symbols_left c_tx3">“</strong>
			<?cs var:qz_metadata.summary ?>
			<strong class="quotes_symbols_right c_tx3">”</strong>
		</p> 		
		<?cs /if ?>
	</div>
	<?cs if:qz_metadata.score.detail?>
	<div class="grade_tp">
		<strong class="feeds_grade" title="<?cs var:qz_metadata.score.txt?>">
			<span class="<?cs var:qz_metadata.score.score?>" style="<?cs var:qz_metadata.score.width?>"></span>
		</strong>
		<span class="grade_txt"><?cs var:qz_metadata.score.detail?></span><span class="grade_txt"><?cs var:qz_metadata.score.aux?></span><span class="grade_txt"><?cs var:qz_metadata.score.aux?></span>
	</div>
	<?cs /if ?>
	<?cs if:subcount(qz_metadata.opts) > 0 ?>
		<?cs call:opts(qz_metadata) ?>
		<xsl:call-template name="opts"/> 
	<?cs /if ?>
	<?cs call:comment(qz_metadata) ?>
</div> 
