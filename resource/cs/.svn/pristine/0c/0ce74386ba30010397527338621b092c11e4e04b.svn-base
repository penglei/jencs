<?cs def:qz_reply(qz_reply) ?>
	<qz:reply action="<?cs var:qz_reply.action ?>" param="<?cs var:qz_reply.param ?>" type="<?cs var:qz_reply.type ?>" charset="<?cs var:qz_reply.charset ?>" maxLength="<?cs var:qz_reply.maxLength ?>" version="6" btnstyle="<?cs var:qz_reply.btnstyle ?>">回复</qz:reply>
<?cs /def ?>

<?cs def:qz_delete(qz_delete) ?>
<qz:delete action="<?cs var:qz_delete.action ?>" param="<?cs var:qz_delete.param ?>" title="<?cs var:qz_delete.title ?>">删除</qz:delete>
<?cs /def ?>

<?cs def:reply() ?>
	<?cs def:replys-item(item) ?>
		<div class="feeds_comm_list bg2">
			<div class="feeds_comment_cont">
					<?cs if:item.uin.qz_user_type == 0 || string.find(item.nick.url, 'uesr.qzone.qq.com') ?>
						<p class="feeds_comment_text"><a href="<?cs var:item.nick.url ?>" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" class="q_namecard q_des comment_nickname c_tx" target="_blank"><?cs var:item.nick ?></a><?cs var:item.msg ?></p>
					<?cs else ?>
						<p class="feeds_comment_text"><a href="<?cs var:item.nick.url ?>"  class="q_des comment_nickname c_tx" target="_blank"><?cs var:item.nick ?></a><?cs var:item.msg ?></p>
					<?cs /if ?>
				<p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:item.datetime ?></span>
					<?cs if:item.qz_delete || subcount(item.qz_delete) > 0 || subcount(item.qz_delete.0) ?>
						<span class="c_tx3"><?cs call:qz_delete(item.qz_delete) ?></span>
					<?cs /if ?>
				</p>
			</div>
		</div>
	<?cs /def ?>
	<?cs if:qz_metadata.reply.0 || subcount(qz_metadata.reply.0) > 0 ?>
		<?cs each:item = qz_metadata.reply ?>
			<?cs call:replys-item(item) ?>
		<?cs /each ?>
	<?cs elif:subcount(qz_metadata.reply) > 0 ?>
		<?cs call:replys-item(qz_metadata.reply) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:comment()?>
		<div class="feeds_comm_list bg2">
			<div class="feeds_comment_cont">
					<?cs if:string.find(qz_metadata.cmtnick.url, 'user.qzone.qq.com') && subcount(qz_metadata.gift)>0 ?>
						<?cs set:param = "&uin="+qz_metadata.uin+"&gifttype="+qz_metadata.gift.gifttype+"&answerid="+qz_metadata.gift.giftid+"&giftid="+ qz_metadata.gift.giftitemid+"&onmalllist="+qz_metadata.gift.gifmalllist+"&typeid=32&btndisable=1&format=gift&itemtype=0&type=1" ?>
						<p class="feeds_comment_text"><a href="<?cs var:qz_metadata.cmtnick.url ?>" link="nameCard_<?cs var:qz_metadata.cmtuin ?> des_<?cs var:qz_metadata.cmtuin ?>" class="q_namecard q_des comment_nickname c_tx" target="_blank"><?cs var:qz_metadata.cmtnick ?></a><?cs var:qz_metadata.comment ?><br />并赠送了礼物：
						<qz:popup height="495" title="礼物卡" width="625" param="<?cs var:param ?>" src="http://imgcache.qq.com/qzone/gift/gift_view.html" title="<?cs var:qz_metadata.gift.giftname ?>"><?cs var:qz_metadata.gift.giftname ?></qz:popup>
					</p>
					<?cs elif:string.find(qz_metadata.cmtnick.url, 'user.qzone.qq.com') ?>
						<p class="feeds_comment_text"><a href="<?cs var:qz_metadata.cmtnick.url ?>" link="nameCard_<?cs var:qz_metadata.cmtuin ?> des_<?cs var:qz_metadata.cmtuin ?>" class="q_namecard q_des comment_nickname c_tx" target="_blank"><?cs var:qz_metadata.cmtnick ?></a><?cs var:qz_metadata.comment ?></p>
					<?cs else ?>
						<p class="feeds_comment_text"><a href="<?cs var:qz_metadata.cmtnick.url ?>"  class="comment_nickname c_tx" target="_blank"><?cs var:qz_metadata.cmtnick ?></a><?cs var:qz_metadata.comment ?></p>
					<?cs /if ?>
				<p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:qz_metadata.cmtdate ?></span>
				<?cs if:subcount(qz_metadata.qz_audit) > 0 || qz_metadata.qz_sudit ?>
						<span class="c_tx3"><a class="c_tx" href="javascript:;" onclick="QZONE.ICFeeds.Interface.auditPassExtend({dataonly:1,src:'/qzone/newblog/v5/info/audit_blog_pass.html',param:'<?cs var:qz_metadata.qz_audit.param ?>'})">通过审核</a></span>
				<?cs /if ?>
				<?cs if:subcount(qz_metadata.qz_delete) > 0 || qz_metadata.qz_delete ?>
					<span class="c_tx3"><?cs call:qz_delete(qz_metadata.qz_delete) ?></span>
				<?cs /if ?>
				</p>
			</div>
		 </div>
<?cs /def ?>

<?cs def:imgs(root) ?>
<div class="imgbox">
<?cs if:root.media.image.0 || subcount(root.media.image.0) ?>
	<?cs each:item = root.media.image ?>
		<qz:popup height="<?cs var:qz_metadata.media.qz_popup[name:item].height ?>" param="1,<?cs var:qz_metadata.media.qz_popup[name:item].param ?>" src="/qzone/photo/zone/icenter_popup.html" title="<?cs var:qz_metadata.media.qz_popup[name:item].title ?>" version="2" width="<?cs var:qz_metadata.media.qz_popup[name:item].width ?>">
			<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:item ?>');"/>
		</qz:popup>
	<?cs /each ?>
<?cs elif:root.media.image ?>
		<qz:popup height="<?cs var:qz_metadata.media.qz_popup.height ?>" param="1,<?cs var:qz_metadata.media.qz_popup.param ?>" src="/qzone/photo/zone/icenter_popup.html" title="<?cs var:qz_metadata.media.qz_popup.title ?>" version="2" width="<?cs var:qz_metadata.media.qz_popup.width ?>">
			<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:qz_metadata.media.image ?>');"/>
		</qz:popup>
<?cs /if ?>
</div>
<?cs /def ?>

<?cs def:opts() ?>
<p class="feeds_tp_operate">
	<?cs def:opts-item(item) ?>
			<?cs if:item.extend.qz_reply.action ?>
				<qz:reply action="<?cs var:item.extend.qz_reply.action ?>" param="<?cs var:item.extend.qz_reply.param ?>" type="<?cs var:item.extend.qz_reply.type ?>" charset="<?cs var:item.extend.qz_reply.charset ?>" maxLength="<?cs var:item.extend.qz_reply.maxLength ?>" version="6.3" btnstyle="<?cs var:item.extend.qz_reply.btnstyle ?>">
					<?cs var:item.text ?>
				</qz:reply>
			<?cs elif:item.extend.qz_popup ?>
				<qz:popup height="<?cs var:item.extend.qz_popup.height ?>" param="<?cs var:item.extend.qz_popup.param ?>" src="<?cs var:item.extend.qz_popup.src ?>" title="<?cs var:item.extend.qz_popup.title ?>" version="<?cs var:item.extend.qz_popup.version ?>" width="<?cs var:item.extend.qz_popup.width ?>">
					<?cs var:item.text ?>
				</qz:popup>
			<?cs else ?>
				<span class="feeds_tp_operate"><a href="<?cs var:item.url ?>" class="c_tx" target="_blank"><?cs var:item.text ?></a></span>
			<?cs /if ?>
	 <?cs /def ?>
	 <?cs if:subcount(qz_metadata.opts.opt.0) > 0 ?>
		 <?cs each:item = qz_metadata.opts.opt ?>
			<?cs call:opts-item(item) ?>
		 <?cs /each ?>
	 <?cs elif:subcount(qz_metadata.opts.opt) > 0 || qz_metadata.opts.opt ?>
		<?cs call:opts-item(qz_metadata.opts.opt) ?>
	 <?cs /if ?>
</p>
<?cs /def ?>

<?cs def:comment2() ?>
<div class="feeds_comment">
	<?cs if:subcount(qz_metadata.reply) > 0 || subcount(qz_metadata.reply.0) > 0 ?>
		<div class="comment_arrow c_bg2">◆</div>
	<?cs /if ?>
	<?cs if:qz_metadata.reply.totalnum > 2 ?>
		 <div class="more_feeds_comment bg2"><a href="<?cs var:qz_metadata.titles[subcount(qz_metadata.titles.item) -1].url ?>" target="_blank" class="c_tx">查看全部<?cs var:qz_metadata.reply.totalnum ?>条评论&gt;&gt;</a></div>
	<?cs /if ?>
	<?cs def:comment2-item(item) ?>
		<div class="feeds_comm_list bg2">
			<div class="feeds_comment_cont">
				<?cs if:item.replyuin.qz_user_type == 0 ||  string.find(item.replynick.url, 'user.qzone.qq.com') > -1 ?>
					<p class="feeds_comment_text"><a href="<?cs var:item.replynick.url ?>" target="_blank" link="nameCard_<?cs var:item.replyuin ?> des_<?cs var:item.replyuin ?>" class="comment_nickname c_tx q_namecard q_des"><?cs var:item.replynick ?></a><?cs var:item.replycontent ?></p>
				<?cs else ?>
					<p class="feeds_comment_text"><a href="<?cs var:item.replynick.url ?>" target="_blank" class="comment_nickname c_tx"><?cs var:item.replynick ?></a><?cs var:item.replycontent ?></p>
				<?cs /if ?>
				<p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:item.datetime ?></span>
					<?cs if:item.origin ?>
							<span class="ifeeds_origin c_tx3"><?cs var:item.origin ?></span>
					<?cs /if ?>
				</p>
			</div>
		</div>
	<?cs /def ?>
	<?cs if:subcount(qz_metadata.reply.item.0) > 0 ?>
		 <?cs each:item = qz_metadata.reply.item ?>
			<?cs call:comment2-item(item) ?>
		 <?cs /each ?>
	 <?cs elif:subcount(qz_metadata.reply.item) > 0 || qz_metadata.reply.item ?>
		<?cs call:comment2-item(qz_metadata.reply.item) ?>
	 <?cs /if ?>
	<?cs if:subcount(qz_metadata.reply.qz_reply) > 0 ?>
		<?cs call:qz_reply(qz_metadata.reply.qz_reply) ?>
	<?cs /if ?>
</div>
<?cs /def ?>

<div class="feeds_tp_1">
	<?cs if:qz_metadata.cmtuin ?>
		<div class="feeds_comment">
		<?cs if:subcount(qz_metadata.reply) > 0 || subcount(qz_metadata.comment) > 0 || qz_metadata.comment ?>
			<div class="comment_arrow c_bg2">◆</div>
		<?cs /if ?>
		<?cs if:subcount(qz_metadata.reply) > 0 || qz_metadata.reply.0 ?>
			<?cs call:comment() ?>
			<?cs if:qz_metadata.replynum > 4 ?>
				<?cs set:itemnum = subcount(qz_metadata.reply) ?>
				<?cs set:replynum = qz_metadata.replynum ?>
					<?cs if:replynum < 11 && itemnum != replynum ?>
						<div class="more_feeds_comment bg2"><qz:more action="http://b.qzone.qq.com/cgi-bin/blognew/blog_generate_feeds" param="<?cs var:qz_metadata.uin ?>,<?cs var:qz_metadata.blogid ?>,1,<?cs var:qz_metadata.cmtid ?>,0" charset="GB">展开更多<?cs var:replynum - 4 ?>条回复↓</qz:more></div>
					<?cs elif:replynum < 11 && itemnum == replynum ?>
						<span class="none">.</span>
					<?cs elif:replynum > 10 && itemnum != 10 ?>
						<div class="more_feeds_comment bg2"><qz:more action="http://b.qzone.qq.com/cgi-bin/blognew/blog_generate_feeds" param="<?cs var:qz_metadata.uin ?>,<?cs var:qz_metadata.blogid ?>,1,<?cs var:qz_metadata.cmtid ?>,0" charset="GB">展开更多6条回复↓</qz:more></div>
					<?cs else ?>
						<div class="more_feeds_comment bg2"><a href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/blog/<?cs var:qz_metadata.blogid ?>" target="_blank" class="c_tx">查看全部<?cs var:replynum ?>条回复&gt;&gt;</a></div>
					<?cs /if ?>
			<?cs /if ?>
			<?cs call:reply() ?>
		 <?cs elif:subcount(qz_metadata.comment) > 0 || qz_metadata.comment.0 || qz_metadata.comment ?>
			<?cs call:comment() ?>
		 <?cs /if ?>
		 <?cs if:subcount(qz_metadata.qz_reply) > 0 || qz_metadata.qz_reply.0 || qz_metadata.qz_reply ?>
			<?cs call:qz_reply(qz_metadata.qz_reply) ?>
		<?cs /if ?>
		</div>
		<?cs if:subcount(qz_metadata.opts) > 0  || qz_metadata.opts.opt?>
			<?cs call:opts() ?>
		<?cs /if ?>
	<?cs else ?>
		<?cs if:subcount(qz_metadata.reply) > 0 || qz_metadata.reply.0 ?>
			<?cs call:comment2() ?>
		<?cs /if ?>
		<?cs if:subcount(qz_metadata.opts) > 0 || qz_metadata.opts.opt?>
			<?cs call:opts() ?>
		<?cs /if ?>
	<?cs /if ?>
</div>
<?cs if:qz_metadata.original.uin ?>
	<qz:data key="<?cs var:'http://user.qzone.qq.com/'+qz_metadata.original.uin+'/blog/'+qz_metadata.original.blogid ?>" />
<?cs else ?>
	<qz:data key="<?cs var:'http://user.qzone.qq.com/'+qz_metadata.uin+'/blog/'+qz_metadata.blogid ?>" />
<?cs /if ?>
