<?cs def:imageViewer(map) ?>
	<div class="img_thumb">
	<?cs if:string.length(map.photodynamicurl) > 0 ?>
		<a onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs var:map.photobigurl ?>', '<?cs var:map.photodynamicurl ?>', 410, 0, 0);return false;" href="javascript:;">
	<?cs else ?>
		<a onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs var:map.photobigurl ?>', '<?cs var:map.photobigurl ?>', 410, 0, 0);return false;" href="javascript:;">
	<?cs /if ?>
			<img alt="点击查看大图" title="点击放大" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(160,150,'<?cs var:map.photourl ?>');" />
		</a>
	</div>
<?cs /def ?>
<?cs if:qz_metadata.albumid ?>
	<div  class="feeds_tp_6">
		<div class="feeds_tp_photo">
			<?cs if:subcount(qz_metadata.piclist)>0 ?>
				<div class="imgbox">
					<?cs if:subcount(qz_metadata.piclist.0)>0 ?>
						<?cs each:item=qz_metadata.piclist ?>
							<qz:popup param="<?cs var:qz_metadata.uin ?>$||^<?cs var:qz_metadata.albumid ?>$||^<?cs var:item.photoid ?>$||^<?cs var:item.photourl ?>$||^<?cs var:qz_metadata.feedtime ?>" src="/qzone/photo/zone/icenter_popup.html#timeStamp=<?cs var:qz_metadata.feedtime ?>" version="2" title="<?cs var:qz_metadata.albumname ?>"><img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:item.photourl ?>');"/>
							</qz:popup>
						<?cs /each ?>
					<?cs elif:subcount(qz_metadata.piclist)>0 ?>
						<?cs call:imageViewer(qz_metadata) ?>
					<?cs /if ?>
				</div>
			<?cs /if ?>
		</div>
		<div  class="feeds_comment">
			<div  class="comment_arrow c_bg2">◆</div>
			<div  class="feeds_comm_list bg2">
				<div  class="feeds_comment_cont">
					<p class="feeds_comment_text">
						<?cs if:qz_metadata.comment.uin_type == 0 ?>
						<a href="http://user.qzone.qq.com/<?cs var:qz_metadata.comment.uin ?>/" target="_blank" link="nameCard_<?cs var:qz_metadata.comment.uin ?> des_<?cs var:qz_metadata.comment.uin ?>" class="comment_nickname c_tx q_namecard q_des"><?cs var:qz_metadata.comment.nick ?></a>
						<?cs /if ?>
						<?cs if:qz_metadata.comment.uin_type == 1 || qz_metadata.comment.uin_type == 2 ?>
						<a href="http://pengyou.qq.com/index.php?mod=profile&u=<?cs var:qz_metadata.comment.uin ?>" target="_blank" class="comment_nickname c_tx q_namecard q_des"><?cs var:qz_metadata.comment.nick ?></a>
						<?cs /if ?>
						<?cs var:qz_metadata.comment.msg ?></p>
					<p class="feeds_comment_op">
						<span  class="feeds_time c_tx3"><?cs var:qz_metadata.comment.date ?></span>
						<?cs if:subcount(qz_metadata.comment.qz_audit_pass)>0 ?><a href="javascript:void(0);" onclick="QZONE.ICFeeds.Interface.auditPassExtend({dataonly:1,src:'/qzone/photo/zone/icAudit.html',param:'p&url=<?cs var:qz_metadata.comment.qz_audit_pass.action ?>&<?cs var:qz_metadata.comment.qz_audit_pass.param ?>'});return false;" class="c_tx">通过审核</a><?cs /if ?>
						<?cs if:subcount(qz_metadata.comment.qz_audit_del)>0 ?><qz:delete action="<?cs var:qz_metadata.comment.qz_audit_del.action ?>" param="<?cs var:qz_metadata.comment.qz_audit_del.param ?>">删除</qz:delete><?cs /if ?>
						<?cs if:subcount(qz_metadata.comment.qz_delete)>0 ?><qz:delete action="<?cs var:qz_metadata.comment.qz_delete.action ?>" param="<?cs var:qz_metadata.comment.qz_delete.param ?>">删除</qz:delete><?cs /if ?>
					</p>
				</div>
			</div><?cs if:qz_metadata.omit ?><div class="more_feeds_comment bg2"><a class="c_tx" href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>/photo/<?cs var:qz_metadata.albumid ?>/" target="_blank">查看全部<?cs var:qz_metadata.total + #1 ?>条评论>></a></div><?cs /if ?><?cs if:subcount(qz_metadata.reply.0)>0 ?><?cs each:item=qz_metadata.reply ?><?cs if:item.uin ?><div class="feeds_comm_list bg2"><div class="feeds_comment_cont"><p class="feeds_comment_text">
			<?cs if:item.uin_type == 0 ?>
				<a href="http://user.qzone.qq.com/<?cs var:item.uin ?>/" target="_blank" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" class="comment_nickname c_tx q_namecard q_des"><?cs var:item.nick ?></a>
			<?cs /if ?>
			<?cs if:item.uin_type == 1 || item.uin_type == 2 ?>
				<a href="http://pengyou.qq.com/index.php?mod=profile&u=<?cs var:item.uin ?>" target="_blank"  class="comment_nickname c_tx q_namecard q_des"><?cs var:item.nick ?></a>
			<?cs /if ?>
			<?cs var:item.msg ?></p><p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:item.date ?></span><?cs if:item.reply_del.action ?><qz:delete action="<?cs var:item.reply_del.action ?>" param="<?cs var:item.reply_del.param ?>">删除</qz:delete><?cs /if ?></p></div> </div><?cs /if ?><?cs /each ?><?cs else ?><?cs if:qz_metadata.reply.uin ?><div class="feeds_comm_list bg2"><div class="feeds_comment_cont"><p class="feeds_comment_text">
			<?cs if:qz_metadata.reply.uin_type == 0 ?>
				<a href="http://user.qzone.qq.com/<?cs var:qz_metadata.reply.uin ?>/" target="_blank" link="nameCard_<?cs var:qz_metadata.reply.uin ?> des_<?cs var:qz_metadata.reply.uin ?>" class="comment_nickname c_tx q_namecard q_des"><?cs var:qz_metadata.reply.nick ?></a>
			<?cs /if ?>
			<?cs if:qz_metadata.reply.uin_type == 1 || qz_metadata.reply.uin_type == 2 ?>
				<a href="http://pengyou.qq.com/index.php?mod=profile&u=<?cs var:qz_metadata.reply.uin ?>" target="_blank"  class="comment_nickname c_tx q_namecard q_des"><?cs var:qz_metadata.reply.nick ?></a>
			<?cs /if ?>
			<?cs var:qz_metadata.reply.msg ?></p><p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:qz_metadata.reply.date ?></span><?cs if:qz_metadata.reply.reply_del.action ?><qz:delete action="<?cs var:qz_metadata.reply.reply_del.action ?>" param="<?cs var:qz_metadata.reply.reply_del.param ?>">删除</qz:delete><?cs /if ?></p></div> </div><?cs /if ?><?cs /if ?><?cs if:qz_metadata.qz_reply.action || qz_metadata.qz_reply.0.action?><qz:reply action="<?cs var:qz_metadata.qz_reply.action ?>" param="<?cs var:qz_metadata.qz_reply.param ?>" type="ubb" charset="UTF8" maxLength="150" version="6" btnstyle="6.1">回复</qz:reply><?cs /if ?></div></div><?cs /if ?>