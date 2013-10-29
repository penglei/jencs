<?cs def:qz_reply(root) ?>
<?cs if:root.thirdappinfo ?>
	<?cs set:AppParam = "," + root.thirdappinfo.id ?>
<?cs else ?>
	<?cs set:AppParam = "" ?>
<?cs /if ?>
<qz:reply action="<?cs var:root.reply.qz_reply.action ?>" param="<?cs var:root.reply.qz_reply.param ?><?cs var:AppParam ?>" type="<?cs var:root.reply.qz_reply.type ?>" charset="<?cs var:root.reply.qz_reply.charset ?>" maxLength="<?cs var:root.reply.qz_reply.maxLength ?>" version="6" btnstyle="<?cs var:root.reply.qz_reply.btnstyle ?>">回复</qz:reply>
<?cs /def ?>

<?cs def:imgs(root) ?>
<div class="imgbox">
<?cs if:root.media.image.0 || subcount(root.media.image.0) ?>
	<?cs set:tmpindex = 0 ?>
	<?cs each:item = root.media.image ?>
		<qz:popup height="<?cs var:qz_metadata.media.qz_popup[tmpindex].height ?>" param="1,<?cs var:qz_metadata.media.qz_popup[tmpindex].param ?>" src="/qzone/photo/zone/icenter_popup.html" title="<?cs var:qz_metadata.media.qz_popup[tmpindex].title ?>" version="2" width="<?cs var:qz_metadata.media.qz_popup[tmpindex].width ?>">
			<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:item ?>');"/>
		</qz:popup>
		<?cs set:tmpindex = tmpindex + 1 ?>
	<?cs /each ?>
<?cs elif:root.media.image ?>
		<qz:popup height="<?cs var:qz_metadata.media.qz_popup.height ?>" param="1,<?cs var:qz_metadata.media.qz_popup.param ?>" src="/qzone/photo/zone/icenter_popup.html" title="<?cs var:qz_metadata.media.qz_popup.title ?>" version="2" width="<?cs var:qz_metadata.media.qz_popup.width ?>">
			<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:qz_metadata.media.image ?>');"/>
		</qz:popup>
<?cs /if ?>
</div>
<?cs /def ?>

<?cs def:content(root) ?>
	<?cs if:qz_metadata.media.image ?>
		<?cs set:imgCount= 1 ?>
	<?cs else ?>
		<?cs set:imgCount= subcount(root.media.image) ?>
	<?cs /if ?>
	<?cs set:attachmentCount = qz_metadata.attachmentnum?>
	<?cs if:imgCount > 0 || attachmentCount > 0 ?>
	<p class="img_amount c_tx3">
		<?cs if:imgCount > 0?>共<?cs var:root.media.media_num ?>张图片&nbsp;&nbsp;<?cs /if?>
		<?cs if:attachmentCount > 0?><a href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/blog/<?cs var:qz_metadata.blogid ?>/attachment" target="_blank" class="c_tx">共<?cs var:attachmentCount ?>个附件</a><?cs /if?>
	</p>
	<?cs /if ?>
	<p><?cs var:root.summary ?></p>
	<p class="same_operate __qzdev_sameuser c_tx3 none"><span class="__qzdev_sameuser_span"><span class="none">.</span></span>转载过此日志</p>
<?cs /def ?>

<?cs def:opts(root) ?>
<p class="feeds_tp_operate">
	<?cs def:opts-item(item) ?>
			<?cs if:item.fold == 1 ?>
				<qz:fc>收起评论</qz:fc>
			<?cs elif:item.extend.qz_reply ?>
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
	<?cs if:root.opts.opt.0 || subcount(root.opts.opt.0) > 0 ?>
	<?cs each:item = root.opts.opt ?>
		<?cs call:opts-item(item) ?>
	<?cs /each ?>
	<?cs elif:root.opts.opt || subcount(root.opts.opt) > 0 ?>
		<?cs call:opts-item(root.opts.opt) ?>
	<?cs /if ?>
	<?cs if:root.thirdappinfo ?>
		<span class="feeds_tp_operate">来自：<a href="<?cs var:root.thirdappinfo.url ?>" class="c_tx" target="_blank"><?cs var:root.thirdappinfo ?></a></span>
	<?cs /if ?>
	<?cs set:noreply = root.noreply ?>
</p>
<?cs /def ?>

<?cs def:qz_reply_content(root, item) ?>
<div class="comment_reply_cont">
		<?cs if:string.length(item.nick.url) == 0 ?>
			<p class="comment_reply_text"><span style="margin-right:5px;" class="comment_nickname"><?cs var:item.nick ?></span><?cs var:item.content ?></p>
		<?cs elif:item.uin.qz_user_type == 0 ||  string.find(item.nick.url, 'user.qzone.qq.com') ?>
			<p class="comment_reply_text"><a href="<?cs var:item.nick.url ?>" target="_blank" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" class="comment_nickname c_tx q_namecard q_des"><?cs var:item.nick ?></a><?cs var:item.content ?></p>
		<?cs else ?>
			<p class="comment_reply_text"><a href="<?cs var:item.nick.url ?>" target="_blank" class="comment_nickname c_tx"><?cs var:item.nick ?></a><?cs var:item.content ?></p>
		<?cs /if ?>
	<p class="comment_reply_op"><span class="feeds_time c_tx3"><?cs var:item.datetime ?></span></p>
</div>
<?cs /def ?>

<?cs def:qz_replylist(root, item) ?>
	<?cs def:qz_replylist-items(item) ?>
		<div class="comment_reply_list">
			<?cs set:posi = name:item ?>
			<?cs if:itemnum == posi ?><div><?cs call:qz_reply_content(qz_metadata, item) ?></div><?cs /if ?>
			<?cs if:itemnum != posi ?><div class="bbor5"><?cs call:qz_reply_content(qz_metadata, item) ?></div><?cs /if ?>
		</div>
	<?cs /def ?>
	<?cs if:subcount(item.comment.item.0) == 0 ?>
		<?cs set:itemnum= 1 ?>
	<?cs else ?>
		<?cs set:itemnum= subcount(item.comment.item) ?>
	<?cs /if ?>
	<?cs if:itemnum > 1 ?>
		<?cs each:item = item.comment.item ?>
			<?cs call:qz_replylist-items(item) ?>
		<?cs /each ?>
	<?cs elif:item.comment.item || subcount(item.comment.item) > 0 ?>
		<?cs call:qz_replylist-items(item.comment.item) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:comment(root) ?>
<div class="feeds_comment">
	<div class="comment_arrow c_bg2">◆</div>
	<?cs if:root.reply.item.replyuin ?>
		<?cs set:itemnum= 1 ?>
	<?cs else ?>
		<?cs set:itemnum= subcount(root.reply.item) ?>
	<?cs /if ?>
	<?cs if:root.uin ?>
		<?cs set:uinexist = 1 ?>
	<?cs else ?>
		<?cs set:uinexist= subcount(root.uin) ?>
	<?cs /if ?>
	<?cs if:root.hotspot ?>
		<?cs set:hotspot= 1 ?>
	<?cs else ?>
		<?cs set:hotspot= subcount(root.hotspot) ?>
	<?cs /if ?>
	<?cs set:shownum= 3 ?>
	<?cs set:totalnum = root.reply.totalnum ?>
	<?cs set:noreply = root.noreply ?>
	
	<?cs if:noreply == 1 && totalnum > 0 ?>
		<div class="more_feeds_comment bg2"><a href="<?cs var:root.titles.item[subcount(root.titles.item) - 1].url ?>" target="_blank" class="c_tx">目前已有<?cs var:totalnum ?>条评论&gt;&gt;</a></div>
	<?cs else ?>
		<?cs if:totalnum > shownum ?>
				<?cs if:(totalnum < 11) && (itemnum != totalnum) && (uinexist > 0 ) && (hotspot == 0) ?>
					<div class="more_feeds_comment bg2"><qz:more action="http://b.qzone.qq.com/cgi-bin/blognew/blog_generate_feeds" param="<?cs var:root.uin ?>,<?cs var:root.blogid ?>,0,0,0" charset="GB">展开更多<?cs var:totalnum - shownum ?>条评论↓</qz:more></div>
				<?cs elif:(totalnum < 11) && (itemnum != totalnum) && (uinexist > 0 ) && (hotspot > 0) ?>
					<div class="more_feeds_comment bg2"><qz:more action="http://b.qzone.qq.com/cgi-bin/blognew/blog_generate_feeds" param="<?cs var:root.uin ?>,<?cs var:root.blogid ?>,0,0,1" charset="GB">展开更多<?cs var:totalnum - shownum ?>条评论↓</qz:more></div>
				<?cs elif:(totalnum < 11) && (itemnum == totalnum) ?>
					<span class="none">.</span>
				<?cs elif:(totalnum > 10)  &&  (itemnum != 10) && (uinexist > 0) && (hotspot == 0) ?>
					<div class="more_feeds_comment bg2"><qz:more action="http://b.qzone.qq.com/cgi-bin/blognew/blog_generate_feeds" param="<?cs var:root.uin ?>,<?cs var:root.blogid ?>,0,0,0" charset="GB">展开更多<?cs var:10 - shownum ?>条评论↓</qz:more></div>
				<?cs elif:(totalnum > 10)  &&  (itemnum != 10) && (uinexist > 0) && (hotspot > 0) ?>
					<div class="more_feeds_comment bg2"><qz:more action="http://b.qzone.qq.com/cgi-bin/blognew/blog_generate_feeds" param="<?cs var:root.uin ?>,<?cs var:root.blogid ?>,0,0,1" charset="GB">展开更多<?cs var:10 - shownum ?>条评论↓</qz:more></div>
				<?cs else ?>
					<div class="more_feeds_comment bg2"><a href="<?cs var:root.titles.item[subcount(root.titles.item) - 1].url ?>" target="_blank" class="c_tx">查看全部<?cs var:totalnum ?>条评论&gt;&gt;</a></div>
				<?cs /if ?>
		<?cs /if ?>
		<?cs if:subcount(root.reply.item.0) > 0 ?>
			<?cs each:item = root.reply.item ?>
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
							<?cs if:item.replyuin.qz_user_type == 0 && item.opt.extend.qz_reply && qz_metadata.noreply == 0 ?>
								<qz:reply action="<?cs var:item.opt.extend.qz_reply.action ?>" param="<?cs var:item.opt.extend.qz_reply.param ?>" type="<?cs var:item.opt.extend.qz_reply.type ?>" charset="<?cs var:item.opt.extend.qz_reply.charset ?>" maxLength="<?cs var:item.opt.extend.qz_reply.maxLength ?>" version="6.2">回复</qz:reply>
							<?cs /if ?>
						</p>
					</div>
					<?cs call:qz_replylist(qz_metadata, item) ?>
				 </div>
			<?cs /each ?>
		<?cs elif:subcount(root.reply.item) > 0 ?>
			<?cs if:root.thirdappinfo ?>
				<?cs set:ReplyAppParam = "," + root.thirdappinfo.id ?>
			<?cs else ?>
				<?cs set:ReplyAppParam = "" ?>
			<?cs /if ?>
			<div class="feeds_comm_list bg2">
				<div class="feeds_comment_cont">
					<?cs if:root.reply.item.replyuin.qz_user_type == 0 ||  string.find(root.reply.item.replynick.url, 'user.qzone.qq.com') > -1 ?>
						<p class="feeds_comment_text"><a href="<?cs var:root.reply.item.replynick.url ?>" target="_blank" link="nameCard_<?cs var:root.reply.item.replyuin ?> des_<?cs var:root.reply.item.replyuin ?>" class="comment_nickname c_tx q_namecard q_des"><?cs var:root.reply.item.replynick ?></a><?cs var:root.reply.item.replycontent ?></p>
					<?cs else ?>
						<p class="feeds_comment_text"><a href="<?cs var:root.reply.item.replynick.url ?>" target="_blank" class="comment_nickname c_tx"><?cs var:root.reply.item.replynick ?></a><?cs var:root.reply.item.replycontent ?></p>
					<?cs /if ?>
					<p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:root.reply.item.datetime ?></span>
						<?cs if:root.reply.item.origin ?>
							<span class="ifeeds_origin c_tx3"><?cs var:root.reply.item.origin ?></span>
						<?cs /if ?>
						<?cs if:root.reply.item.replyuin.qz_user_type == 0 &&  root.reply.item.opt.extend.qz_reply && qz_metadata.noreply == 0 ?>
							<qz:reply action="<?cs var:root.reply.item.opt.extend.qz_reply.action ?>" param="<?cs var:root.reply.item.opt.extend.qz_reply.param ?><?cs var:ReplyAppParam ?>" type="<?cs var:root.reply.item.opt.extend.qz_reply.type ?>" charset="<?cs var:root.reply.item.opt.extend.qz_reply.charset ?>" maxLength="<?cs var:root.reply.item.opt.extend.qz_reply.maxLength ?>" version="6.2">回复</qz:reply>
						<?cs /if ?>
					</p>
				</div>
				<?cs call:qz_replylist(qz_metadata, root.reply.item) ?>
			</div>
		<?cs /if ?>
	<?cs /if ?>
	<?cs if:root.reply.qz_reply ?>
		<?cs call:qz_reply(qz_metadata) ?>
	<?cs /if ?>
</div>
<?cs /def ?>

<?cs if:qz_metadata.uin ?>
<div class="feeds_tp_1">
	<div class="blog_feeds_tp bor2">
		<?cs if:qz_metadata.media.image || subcount(qz_metadata.media.image) ?>
			<?cs call:imgs(qz_metadata) ?>
		<?cs /if ?>
		<?cs if:!qz_metadata.accessright ?>
			<div class="txtbox"><?cs call:content(qz_metadata) ?></div>
		<?cs /if ?>
		<?cs if:qz_metadata.accessright ?>
			<div><?cs call:content(qz_metadata) ?></div>
		<?cs /if ?>
		<?cs if:qz_metadata.share || qz_metadata.quote ?>
			<div class="c_tx3"><?cs if:qz_metadata.quote ?>转载(<?cs var:qz_metadata.quote ?>)<?cs /if ?></div>
		<?cs /if ?>
	</div>
	<?cs if:qz_metadata.accessright ?>
		<div class="feeds_tp_7">
			<div class="feeds_tp_operate"><span class="encrypt_feeds c_tx3">该日志仅QQ好友可见</span><a class=c_tx href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/blog/<?cs var:qz_metadata.blogid ?>/" target="_blank">点击查看</a></div>
		</div>
	<?cs /if ?>
	<?cs if:qz_metadata.opts.opt || subcount(qz_metadata.opts.opt) ?>
		<?cs call:opts(qz_metadata) ?>
	<?cs /if ?>
	<?cs if:!qz_metadata.accessright ?>
		<?cs call:comment(qz_metadata) ?>
	<?cs /if ?>
</div>
	<?cs if:qz_metadata.original.uin ?>
		<qz:data key="<?cs var:'http://user.qzone.qq.com/'+qz_metadata.original.uin+'/blog/'+qz_metadata.original.blogid ?>" />
	<?cs else ?>
		<qz:data key="<?cs var:'http://user.qzone.qq.com/'+qz_metadata.uin+'/blog/'+qz_metadata.blogid ?>" />
	<?cs /if ?>
<?cs /if ?>