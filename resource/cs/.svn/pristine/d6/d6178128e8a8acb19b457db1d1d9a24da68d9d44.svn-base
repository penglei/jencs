<?cs def:imageViewer(map) ?>
	<div class="img_thumb">
		<a onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs var:map.bigurl ?>', '<?cs var:map.bigurl ?>',410, 0, 0);return false;" href="javascript:;">
			<img alt="点击查看大图" title="点击放大" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(160,150,'<?cs var:map.smallurl ?>');" />
		</a>
	</div>
<?cs /def ?>



<div class=" feeds_tp_1">
	<?cs if:qz_metadata.lloc ?><?cs if:qz_metadata.uin ?><?cs set:qz_key_uin = qz_metadata.uin ?><?cs else ?><?cs set:qz_key_uin = qz_metadata.zzuin ?><?cs /if ?><qz:data key="http://user.qzone.qq.com/<?cs var:qz_key_uin ?>/photo/<?cs var:qz_metadata.albumid ?>/<?cs var:qz_metadata.lloc ?>" /><?cs /if ?>
	<?cs if:qz_metadata.feedtitle!=1 ?>
		<?cs if:string.length(qz_metadata.photodesc)>1 ?>
			<div class="txtbox_quote_txt">
				<p><strong class="quotes_symbols_left c_tx3">“</strong><?cs var:qz_metadata.photodesc ?><strong class="quotes_symbols_right c_tx3">”</strong></p>
			</div>
		<?cs /if ?>
	<?cs /if ?>
	<?cs call:imageViewer(qz_metadata) ?>
	<?cs if: subcount(qz_metadata.qz_reply)>0 ?>
		<div class="feeds_comment">
			<div class="comment_arrow c_bg2">◆</div>
			<?cs if:subcount(qz_metadata.comment.0)>0 ?>
				<?cs each:item=qz_metadata.comment ?>
					<div class="feeds_comm_list bg2">
						<div class="feeds_comment_cont">
							<p class="feeds_comment_text">
								<a href="http://user.qzone.qq.com/<?cs var:item.uin ?>/" target="_blank" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" class="comment_nickname c_tx q_namecard q_des"><?cs var:item.nick ?></a>
								<?cs var:item.msg ?>
							</p>
							<p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:item.date ?></span></p>
						</div>
					 </div>
				<?cs /each ?>
			<?cs elif:subcount(qz_metadata.comment)>0 ?>
				<div class="feeds_comm_list bg2">
					<div class="feeds_comment_cont">
						<p class="feeds_comment_text">
							<a href="http://user.qzone.qq.com/<?cs var:qz_metadata.comment.uin ?>/" target="_blank" link="nameCard_<?cs var:qz_metadata.comment.uin ?> des_<?cs var:qz_metadata.comment.uin ?>" class="comment_nickname c_tx q_namecard q_des"><?cs var:qz_metadata.comment.nick ?></a>
							<?cs var:qz_metadata.comment.msg ?>
						</p>
						<p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:qz_metadata.comment.date ?></span></p>
					</div>
				 </div>
			<?cs /if ?>
			<?cs if:subcount(qz_metadata.qz_reply)>0 ?>
				<qz:reply action="<?cs var:qz_metadata.qz_reply.action ?>" param="<?cs var:qz_metadata.qz_reply.param ?>" type="text" charset="UTF8" maxLength="140" version="6" btnstyle="6.1">回复</qz:reply>
			<?cs /if ?>
		</div>
	<?cs /if ?>
</div>
