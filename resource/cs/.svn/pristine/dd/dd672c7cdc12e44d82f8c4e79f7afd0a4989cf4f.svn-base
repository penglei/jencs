<?cs def:writeUserName(uin,defaultName) ?>
	<?cs if:string.length(qz_metadata.remarkPool['u'+uin])>0 ?>
		<?cs var:html_encode(qz_metadata.remarkPool['u'+uin], 1) ?>
	<?cs else ?>
		<?cs var:html_encode(defaultName, 1) ?>
	<?cs /if ?>
<?cs /def ?>
<?cs def:userIcon(user) ?>
	<?cs if:user.type == 1 ?>
		<?cs set:uinmod = user.uin % 4 + 1 ?>
		<?cs set:imgsrc = "http://qlogo"+uinmod+".store.qq.com/qzone/"+user.uin+"/"+user.uin+"/30"  ?>
		<a href="http://user.qzone.qq.com/<?cs var:user.uin ?>" target="_blank">
			<img class="q_namecard" link="nameCard_<?cs var:user.uin ?> des_<?cs var:user.uin ?>" alt="<?cs call:writeUserName(user.uin,user.name) ?>" src="<?cs var:imgsrc ?>" />
		</a>
	<?cs elseif:user.type == 2 ?>
		<img alt="<?cs var:html_encode(user.name, 1) ?>" src="http://xy.store.qq.com/campus/<?cs var:user.img ?>/30" />
	<?cs /if ?>
<?cs /def ?>

<?cs def:userLink(user, prefix) ?>
	<?cs if:user.who == 1 || user.type == 1?>
		<a href="http://user.qzone.qq.com/<?cs var:user.uin ?>" class="q_namecard c_tx" link="nameCard_<?cs var:user.uin ?>" target="_blank">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<span><?cs call:writeUserName(user.uin,user.name) ?></span></a>
	<?cs elif:user.who == 2 || user.type == 2 ?>
		<a href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:user.uin ?>" class="c_tx" target="_blank">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<?cs var:html_encode(user.name, 1) ?></a>
	<?cs elif:user.who == 3 || user.type == 3 ?>
		<a href="http://rc.qzone.qq.com/myhome/weibo/profile/<?cs var:user.uin ?>" class="c_tx" target="_blank">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<?cs var:html_encode(user.name, 1) ?></a>
	<?cs else ?>
		<?cs var:html_encode(user.name, 1) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:cmtUserLink(user, prefix) ?>
	<?cs if:user.type == 1 ||user.who == 1?>
		<a href="http://user.qzone.qq.com/<?cs var:user.uin ?>" class="q_namecard nickname c_tx" link="nameCard_<?cs var:user.uin ?>" target="_blank">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<?cs call:writeUserName(user.uin,user.name) ?> </a>
	<?cs elif:user.who == 2 || user.type == 2 ?>
		<a href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:user.uin ?>" class="c_tx" target="_blank">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<?cs var:html_encode(user.name, 1) ?></a>
	<?cs elif:user.who == 3 || user.type == 3 ?>
		<a href="http://rc.qzone.qq.com/myhome/weibo/profile/<?cs var:user.uin ?>" class="c_tx" target="_blank">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<?cs var:html_encode(user.name, 1) ?></a>
	<?cs else ?>
		<?cs var:html_encode(user.name, 1) ?>
	<?cs /if ?>
<?cs /def ?>
<?cs def:grade_star(item) ?>
	<?cs if:string.length(item.grade) > 0 || item.percent?>
	<p class="f_votestar">
		<span class="votestar">
		<?cs with:grade = item.grade ?>
		<?cs if:string.find(grade, ".")  != #-1 ?>
			<?cs set: item.grade = string.slice(grade,0,string.find(grade, "."))?>
		<?cs /if ?>
		<?cs /with?>
		
		<span class="<?cs if:item.grade || item.percent?>votestar_i <?cs /if ?>
		<?cs if:item.grade ?>star_<?cs var:item.grade?><?cs /if?>" style="<?cs if:item.percent?>width:<?cs var:item.percent?>;<?cs /if?>">
		</span>
		
		</span>				
		<span class="votescore"><span class="c_tx3 ui_mr10"><?cs var:item.score ?></span></span>
	</p>
	<?cs /if ?>
<?cs /def?>
<?cs def:richContent(cons) ?>
	<?cs def:richContent-items(item) ?>
		<?cs if:item.type == 'nick' ?>
			<?cs call:userLink(item , '@') ?>
		<?cs elif:item.type == 'url' ?>
			<a class="c_tx" href="<?cs var:item.url ?>" target="_blank"><?cs if:item.text ?><?cs var:item.text ?><?cs else ?><?cs var:item.url ?><?cs /if ?></a>
		<?cs elif:item.type == 'qz_app' ?>	
			<?cs if:string.length(item.title) > 0 ?>
			<p>
				<a class="c_tx" target="_blank" href="<?cs alt:item.url?>http://rc.qzone.qq.com/myhome/<?cs var:item.id?><?cs /alt?>"><?cs var:item.title?></a>
			</p>
			<?cs /if ?>
			<?cs if:string.length(item.text) > 0 ?><p><?cs var:item.text?></p><?cs /if?>
			<?cs call:grade_star(item) ?>
		<?cs else ?>
			<?cs var:item ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 || string.length(cons.con.0) > 0 ?>
		<?cs loop:i = 0, subcount(cons.con) - 1, 1 ?>
			<?cs call:richContent-items(cons.con[i]) ?>
		<?cs /loop ?>
	<?cs elif:cons.con || subcount(cons.con) > 0 || string.length(cons.con) > 0 ?>
		<?cs call:richContent-items(cons.con) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:comment() ?>
	<?cs #:如果没有评论内容的话,这个箭头就不显示了,设计师,我们伤不起啊. ?>
	<?cs if:qz_metadata.comments.comment.0 || subcount(qz_metadata.comments.comment.0) > 0 || subcount(qz_metadata.comments.comment) > 0 || (mod.like=='on' && (qz_metadata.qz_data[qz_data_key].LIKE.isliked||subcount(qz_metadata.qz_data[qz_data_key].LIKE.user)>0|| qz_metadata.qz_data[qz_data_key].LIKE.user))?>
		<?cs set:qz_arrow_display = '' ?>
	<?cs else ?>
		<?cs set:qz_arrow_display = 'none' ?>
	<?cs /if ?>
	<div class="f_ang_t bor2 qz_comment_arrow <?cs var:qz_arrow_display ?>"><div class="ang_i ang_t_d bor2"></div><div class="ang_i ang_t_u bor_bg"></div></div>
	<?cs if:subcount(qz_metadata.comments) > 0 ?>
				<?cs def:comment-items(item) ?>
					<li class="comments_item bor3">
						<div class="comments_item_bd">
							<div class="ui_avatar"><?cs call:userIcon(item.user) ?></div>
							<div class="comments_content"><?cs call:cmtUserLink(item.user, '') ?> : <?cs call:richContent(item.content) ?>
								<div class="comments_op"><span class="c_tx3 ui_mr10"><?cs var:item.time.text ?></span>
								</div>
							</div>
							<?cs def:reply-items(comment,item) ?>
									<li class="comments_item bor3">
										<div class="comments_item_bd">
											<div class="ui_avatar"><?cs call:userIcon(item.user) ?></div>
											<div class="comments_content"><?cs call:cmtUserLink(item.user, '') ?> : <?cs call:richContent(item.content) ?>
												<div class="comments_op"><span class="c_tx3 ui_mr10"><?cs var:item.time.text ?></span>
												</div>
											</div>
										</div>
									</li>
							<?cs /def ?>
							<?cs if:item.replies.reply.0 || subcount(item.replies.reply.0) > 0 || subcount(item.replies.reply) > 0 ?>
							<div class="comments_list mod_comments_sub">
								<?cs if:item.replies.qz_more.action ?>
									<div class="comments_list_more"><qz:more 
										action="<?cs var:item.replies.qz_more.action ?>" 
										param="<?cs var:item.replies.qz_more.param ?>" 
										charset="<?cs var:item.replies.qz_more.charset ?>" 
									>展开剩余<?cs var:item.replies.qz_more.count ?>条回复</qz:more></div>
								<?cs /if ?>
								<?cs if:item.replies.qz_more.url ?>
								<div class="comments_list_more"><a href="<?cs var:item.replies.qz_more.url ?>" class="c_tx" target="_blank">查看剩余<?cs var:item.replies.qz_more.count ?>条回复</a></div>
								<?cs /if ?>
								<ul>
								<?cs if:item.replies.reply.0 || subcount(item.replies.reply.0) > 0 ?>
									<?cs loop:i =0, subcount(item.replies.reply)-1, 1 ?>
										<?cs call:reply-items(item,item.replies.reply[i]) ?>
									<?cs /loop ?>
								<?cs elif:subcount(item.replies.reply) > 0 ?>
									<?cs call:reply-items(item,item.replies.reply) ?>
								<?cs /if ?>
								</ul>
							</div>
							<?cs /if ?>
						</div>
					</li>
				<?cs /def ?>
				<?cs if:qz_metadata.comments.comment.0 || subcount(qz_metadata.comments.comment.0) > 0 || subcount(qz_metadata.comments.comment) > 0 ?>
				<div class="mod_comments">
					<?cs if:!qz_metadata.noreply ?>
					<div class="comments_list">
								<?cs if:qz_metadata.comments.qz_more.url ?>
									<div class="comments_list_more <?cs if:qz_metadata.comments.qz_more.display!=1 ?>none<?cs /if ?>"><a href="<?cs var:qz_metadata.comments.qz_more.url ?>" class="c_tx" target="_blank">查看剩余全部评论</a></div>
								<?cs /if ?>
								<?cs if:qz_metadata.comments.comment.0 || subcount(qz_metadata.comments.comment.0) > 0 ?>
									<ul>
									<?cs loop:i = 0, subcount(qz_metadata.comments.comment)-1, 1 ?>
										<?cs call:comment-items(qz_metadata.comments.comment[i]) ?>
									<?cs /loop ?>
									</ul>
								<?cs elif:subcount(qz_metadata.comments.comment) > 0 ?>
									<ul>
									<?cs call:comment-items(qz_metadata.comments.comment) ?>
									</ul>
								<?cs /if ?>
					</div>
					<?cs /if ?>
				</div>
				<?cs /if ?>
	<?cs /if ?>
<?cs /def ?>
<?cs call:comment() ?>