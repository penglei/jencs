<?cs def:qz_reply(root) ?>
	<qz:reply action="<?cs var:root.qz_reply.action ?>" param="<?cs var:root.qz_reply.param ?>" type="<?cs var:root.qz_reply.type ?>" charset="<?cs var:root.qz_reply.charset ?>" maxLength="<?cs var:root.qz_reply.maxLength ?>" version="6" btnstyle="<?cs var:root.qz_reply.btnstyle ?>">回复</qz:reply> 
<?cs /def ?>

<?cs def:qz_delete(root) ?>
	<qz:delete action="<?cs var:root.qz_delete.action ?>" param="<?cs var:root.qz_delete.param ?>" title="<?cs var:root.qz_delete.title ?>">删除</qz:delete> 
<?cs /def ?>

<?cs def:qz_reply2(root) ?>
	<qz:reply action="<?cs var:root.qz_reply.action ?>" param="<?cs var:root.qz_reply.param ?>" type="<?cs var:root.qz_reply.type ?>" charset="<?cs var:root.qz_reply.charset ?>" maxLength="<?cs var:root.qz_reply.maxLength ?>" version="6" btnstyle="<?cs var:root.qz_reply.btnstyle ?>">回复</qz:reply> 
<?cs /def ?>

<?cs def:reply(root) ?>
	<?cs def:reply-items(item) ?>
		<div  class="feeds_comm_list bg2"> 
			<div  class="feeds_comment_cont"> 
				<?cs if:item.uin.qz_user_type == 0 || string.find(item.nick.url, 'user.qzone.qq.com') > -1 ?>
					<p  class="feeds_comment_text"> 
						<a  href="<?cs var:item.nick.url ?>" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" class="q_namecard q_des comment_nickname c_tx" target="_blank"> 
							<?cs var:item.nick ?>
						</a> 
						<?cs var:item.msg ?>
					</p> 
				<?cs else ?>
					<p  class="feeds_comment_text"> 
						<a  href="<?cs var:item.nick.url ?>" class="q_des comment_nickname c_tx" target="_blank"> 
							<?cs var:item.nick ?>
						</a> 
						<?cs var:item.msg ?>
					</p> \
				<?cs /if ?>
				<p  class="feeds_comment_op"> 
					<span  class="feeds_time c_tx3"> 
						<?cs var:item.datetime ?>
					</span> 
					<?cs if:subcount(item.qz_delete) > 0 ?>
						<span  class="c_tx3"> 
							<?cs call:qz_delete(item) ?>
						</span> 
					<?cs /if ?>
				</p> 
			</div> 
		</div> 
	<?cs /def ?>
	<?cs if:root.reply.0 || subcount(root.reply.0) > 0 ?>
		<?cs each:item = root.reply ?>
			<?cs call:reply-items(item) ?>
		<?cs /each ?>
	<?cs elif:root.reply || subcount(root.reply) > 0 ?>
		<?cs call:reply-items(root.reply) ?>
	<?cs /if ?>
<?cs /def ?>



<?cs def:comment(root) ?>
	<div  class="feeds_comm_list bg2">
		<div  class="feeds_comment_cont">
			<?cs if:string.find(root.cmtnick.url, 'user.qzone.qq.com') > -1 ?>
				<p  class="feeds_comment_text"> 
					<a  href="<?cs var:root.cmtnick.url ?>" link="nameCard_<?cs var:root.cmtuin ?> des_<?cs var:root.cmtuin ?>" class="q_namecard q_des comment_nickname c_tx" target="_blank"> 
						<?cs var:root.cmtnick ?>
					</a> 
					<?cs var:root.comment ?>
				</p> 
			<?cs else ?>
				<p  class="feeds_comment_text"> 
					<a  href="<?cs var:root.cmtnick.url ?>" class="comment_nickname c_tx" target="_blank"> 
						<?cs var:root.cmtnick ?>
					</a> 
					<?cs var:root.comment ?>
				</p> 
			<?cs /if ?>
			<p  class="feeds_comment_op"> 
				<span  class="feeds_time c_tx3"> 
					<?cs var:root.cmtdate ?>
				</span> 
				<?cs if:subcount(root.qz_delete) > 0 ?>
					<span  class="c_tx3"> 
						<?cs call:qz_delete(root) ?>
					</span> 
				<?cs /if ?>
			</p> 
		</div> 
	</div> 
<?cs /def ?>

<?cs def:comment2(root) ?>	
	<div  class="feeds_comment"> 
		<?cs if:subcount(root.reply) > 0 ?>
			<div  class="comment_arrow c_bg2">◆</div> 
		<?cs /if ?>
		<?cs if:root.reply.totalnum > 2 ?>
			<div  class="more_feeds_comment bg2"> 
				<a  href="<?cs var:root.titles.item[subcount(root.titles.item) - 1].url ?>" target="_blank" class="c_tx">查看全部<?cs var:root.reply.totalnum ?>条评论&gt;&gt; 
				</a> 
			</div> 
		<?cs /if ?>
		<?cs def:reply2-items(item) ?>
			<div  class="feeds_comm_list bg2"> 
				<div  class="feeds_comment_cont"> 
					<?cs if:string.find(item.replynick.url, 'user.qzone.qq.com') > -1 ?>
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
						<?cs if:subcount(item.orgin) > 0 ?>
							<span  class="ifeeds_origin c_tx3"> 
								<?cs var:item.orgin ?>
							</span> 
						<?cs /if ?>
					</p> 
				</div> 
			</div> 
		<?cs /def ?>
		<?cs if:root.reply.item.0 || subcount(root.reply.item.0) > 0 ?>
			<?cs each:item = root.reply.item ?>
				<?cs call:reply2-items(item) ?>
			<?cs /each ?>
		<?cs elif:root.reply.item || subcount(root.reply.item) > 0 ?>
			<?cs call:reply2-items(root.reply.item) ?>
		<?cs /if ?>	
		<?cs if:subcount(root.reply.qz_reply) > 0 ?>
			<?cs call:qz_reply2(root) ?>
		<?cs /if ?>
	</div> 
<?cs /def ?>

<div class="feeds_tp_1"> 
	<?cs if:qz_metadata.cmtuin || subcount(qz_metadata.cmtuin) > 0 ?>
		<div  class="feeds_comment">
			<?cs if:qz_metadata.reply || subcount(qz_metadata.reply) > 0 || qz_metadata.comment || subcount(qz_metadata.comment) > 0 ?>
				<div  class="comment_arrow c_bg2">◆</div> 
			<?cs /if ?>
			<?cs if:qz_metadata.reply || subcount(qz_metadata.reply) > 0 ?>
				<?cs call:comment(qz_metadata) ?>
				<?cs if:qz_metadata.replynum > subcount(qz_metadata.reply) ?>
					<div  class="more_feeds_comment bg2"> 
						<a  href="http://rc.qzone.qq.com/myhome/QzoneMovie/#action=topic&amp;topicid=<?cs var:qz_metadata.topicid ?>&amp;owner=<?cs var:qz_metadata.uin ?>" target="_blank" class="c_tx">查看全部<?cs var:qz_metadata.replynum ?>条回复&gt;&gt; 
						</a> 
					</div> 
				<?cs /if ?>
				<?cs call:reply(qz_metadata) ?>
			<?cs elif:qz_metadata.comment || subcount(qz_metadata.comment) > 0 ?>
				<?cs call:comment(qz_metadata) ?>
			<?cs /if ?>
			<?cs if:qz_metadata.qz_reply || subcount(qz_metadata.qz_reply) > 0 ?>
				<?cs call:qz_reply(qz_metadata) ?>
			<?cs /if ?>
		</div> 
	<?cs else ?>
		<?cs if:subcount(qz_metadata.reply) > 0 ?>
			<?cs call:comment2(qz_metadata) ?>
		<?cs /if ?>
	<?cs /if ?>
</div> 
