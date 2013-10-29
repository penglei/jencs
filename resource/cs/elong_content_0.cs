<?cs def:textUserLink(p_uin,p_name) ?>
	<a class="q_namecard q_des comment_nickname c_tx" href="http://user.qzone.qq.com/<?cs var:p_uin ?>" link="nameCard_<?cs var:p_uin ?> des_<?cs var:p_uin ?>" target="_blank"><?cs var:p_name ?></a>
<?cs /def ?>

<?cs def:imgUserLink(p_uin,p_name) ?>
	<a class="feeds_comment_portrait c_tx q_namecard" link="nameCard_<?cs var:p_uin ?>" href="http://user.qzone.qq.com/<?cs var:p_uin ?>" target="_blank"><img alt="<?cs var:p_name ?>" src="http://qlogo<?cs var:p_uin%4 + #1 ?>.store.qq.com/qzone/<?cs var:p_uin ?>/<?cs var:p_uin ?>/30" /></a>
<?cs /def ?>

<?cs def:replyList(p_replycount,p_replylist) ?>
	<?cs if:p_replycount>#1 ?>
		<?cs each:item=p_replylist ?>\
			<div class="comment_reply_list">
				<div class="bbor5">
					<?cs call:imgUserLink(item.uin,item.nick) ?>
					<div class="comment_reply_cont">
						<p class="comment_reply_text">
							<?cs call:textUserLink(item.uin,item.nick) ?>
							<?cs var:item.content ?>
						</p>
						<p class="comment_reply_op">
							<span class="feeds_time c_tx3"><?cs var:item.date ?></span>
						</p>
					</div>
				</div>
			</div>
		<?cs /each ?>
	<?cs else ?>
		<div class="comment_reply_list">
			<div class="bbor5">
				<?cs call:imgUserLink(p_replylist.uin,p_replylist.nick) ?>
				<div class="comment_reply_cont">
					<p class="comment_reply_text">
						<?cs call:textUserLink(p_replylist.uin,p_replylist.nick) ?>
						<?cs var:p_replylist.content ?>
					</p>
					<p class="comment_reply_op">
						<span class="feeds_time c_tx3"><?cs var:p_replylist.date ?></span>
					</p>
				</div>
			</div>
		</div>
	<?cs /if ?>
<?cs /def ?>

<?cs def:commentList(p_commentcount,p_commentlist) ?>
	<?cs if:p_commentcount>1 ?>
		<?cs each:item=p_commentlist ?>
			<div class="feeds_comment_list bg2">
			    <?cs call:imgUserLink(item.uin,item.nick) ?>
				<div class="feeds_comment_cont">
					<p class="feeds_comment_text">
					    <?cs call:textUserLink(item.uin,item.nick) ?>
						<?cs var:item.content ?>
					</p>
					<p class="feeds_comment_op">
					    <span class="feeds_time c_tx3"><?cs var:item.date ?></span>
						<qz:reply action="http://elong.qzone.qq.com/cgi-bin/elong_tour_add_comment_reply.cgi"  maxLength="100" charset="UTF-8" param="ouin=<?cs var:qz_metadata.uin ?>&amp;topicid=<?cs var:qz_metadata.id ?>&amp;commentid=<?cs var:item.id ?>&amp;typeid=1" type="text" version="6.2" >回复</qz:reply>
					</p>
				</div>
				<?cs if:item.replys.total>#0 ?>
					<?cs call:replyList(item.replys.total,item.replys.reply) ?>
				<?cs /if ?>
			</div>
		<?cs /each ?>
	<?cs elif:p_commentcount==1 ?>
		<div class="feeds_comment_list bg2">
		    <?cs call:imgUserLink(p_commentlist.uin,p_commentlist.nick) ?>
			<div class="feeds_comment_cont">
				<p class="feeds_comment_text">
				    <?cs call:textUserLink(p_commentlist.uin,p_commentlist.nick) ?>
					<?cs var:p_commentlist.content ?>
				</p>
				<p class="feeds_comment_op">
				    <span class="feeds_time c_tx3"><?cs var:p_commentlist.date ?></span>
					<qz:reply action="http://elong.qzone.qq.com/cgi-bin/elong_tour_add_comment_reply.cgi"  maxLength="100" charset="UTF-8" param="ouin=<?cs var:qz_metadata.uin ?>&amp;topicid=<?cs var:qz_metadata.id ?>&amp;commentid=<?cs var:p_commentlist.id ?>&amp;typeid=1" type="text" version="6.2" >回复</qz:reply>
				</p>
			</div>
			<?cs if:item.total>#0 ?>
				<?cs call:replyList(p_commentlist.replys.total,p_commentlist.replys.reply) ?>
			<?cs /if ?>
		</div>
	<?cs /if ?>
<?cs /def ?>


<?cs #:好了启动程序开始 ?>
<div class="feeds_tp_5">
	<?cs if:qz_metadata.dest.imgs.img||subcount(qz_metadata.dest.imgs.img)>#0 ?>
		<div class="feeds_tp_photo">
			<a href="http://rc.qzone.qq.com/myhome/502?destid=<?cs var:qz_metadata.dest.id ?>" target="_blank">
				<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:qz_metadata.dest.imgs.img ?>');"/>
			</a>
		</div>
    <?cs /if ?>
    <div class="txtbox  quote_txt">
	    <p>
			<strong class="quotes_symbols_left c_tx3">“</strong>
			    <?cs var:qz_metadata.content ?>
			<strong class="quotes_symbols_right c_tx3">”</strong>
		</p>
	</div>
	<div class="feeds_tp_operate">
		<qz:reply type="link">评论</qz:reply>
		<a class="c_tx" href="http://rc.qzone.qq.com/myhome/502?destid=<?cs var:qz_metadata.dest.id ?>" target="blank">
		    <span class="c_tx">查看目的地</span>
		</a>
		<a class="c_tx" href="http://rc.qzone.qq.com/myhome/502?ouin=<?cs var:qz_metadata.uin ?>&amp;tid=<?cs var:qz_metadata.id ?>" target="blank">
		    <span class="c_tx">查看全文</span>
		</a>
	</div>
	<div class="feeds_comment">
	    <div class="comment_arrow c_bg2">◆</div>
			<?cs if:qz_metadata.comments.total>#2 ?>
				<div class="more_feeds_comment bg2">
					<a class="c_tx" href="http://rc.qzone.qq.com/myhome/502?ouin=<?cs var:qz_metadata.uin ?>&amp;tid=<?cs var:qz_metadata.id ?>" target="_blank">查看全部<?cs var:qz_metadata.comments.total ?>条评论&gt;&gt;</a>
				</div>
			<?cs /if ?>
			<?cs call:commentList(qz_metadata.comments.total,qz_metadata.comments.comment) ?>
        <qz:reply action="http://elong.qzone.qq.com/cgi-bin/elong_tour_add_comment.cgi" version="6"  maxLength="100" param="ouin=<?cs var:qz_metadata.uin ?>&amp;topicid=<?cs var:qz_metadata.id ?>&amp;typeid=1" type="text" charset="UTF-8" >确定</qz:reply>
    </div>
</div>
