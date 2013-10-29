<?cs def:textUserLink(uin, name, s_uin, s_name, className) ?>
	<?cs if:string.length(s_uin) > 0 ?>
		<a class="q_namecard q_des <?cs var:className?> c_tx" href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:s_uin ?>" target="_blank"><?cs var:s_name ?></a>
	<?cs elif:string.length(uin) > 30?>
		<a class="q_namecard q_des <?cs var:className?> c_tx" href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:uin ?>" target="_blank"><?cs var:name ?></a>
	<?cs else?>
		<a class="q_namecard q_des <?cs var:className?> c_tx" href="http://user.qzone.qq.com/<?cs var:uin ?>" link="nameCard_<?cs var:uin ?> des_<?cs var:uin ?>" target="_blank"><?cs var:name ?></a>
	<?cs /if?>
<?cs /def ?>

<?cs def:richContent(cons) ?>
	<?cs def:richContent-items(item) ?>
		<?cs if:item.type == 'nick' ?>
			<?cs if:string.length(item.uin) > 30?>
				<?cs call:textUserLink("", "", item.uin, '@'+item.name, "comment_nickname") ?>
				<?cs else?>
				<?cs call:textUserLink(item.uin, '@'+item.name, "", "", "comment_nickname") ?>
			<?cs /if?>
		<?cs elif:item.type == 'url' ?>
			<?cs if:item.ourl ?>
				<a href="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs var:item.url ?>" title="原链接：<?cs var:item.ourl ?>" class="c_tx" target="_blank"><?cs var:item.url ?></a>
			<?cs else ?>
				<a href="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs var:item.url ?>" class="c_tx" target="_blank"><?cs var:item.url ?></a>
			<?cs /if ?>
		<?cs else ?>
			<?cs var:item ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 ?>
		<?cs each:item = cons.con ?>
			<?cs call:richContent-items(item) ?>
		<?cs /each ?>
	<?cs elif:cons.con || subcount(cons.con) > 0 ?>
		<?cs call:richContent-items(cons.con) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:clearContent(cons) ?>
	<?cs def:clearContent-items(item) ?>
		<?cs if:item.type == 'nick' ?>
			@<?cs var:item.name ?>
		<?cs elif:item.type == 'url' ?>
			<?cs var:item.url ?>
		<?cs else ?>
			<?cs var:item ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 ?>
		<?cs each:item = cons.con ?>
			<?cs call:clearContent-items(item) ?>
		<?cs /each ?>
	<?cs elif:cons.con || subcount(cons.con) > 0 ?>
		<?cs call:clearContent-items(cons.con) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:level1TitleView() ?>
<?cs if:string.length(qz_metadata.rt_uin) > 0 ?><span class="c_tx3">转发：</span>
	<?cs if:string.length(qz_metadata.last_fwd_cmt) > 0 ?>
	<strong class="quotes_symbols_left c_tx3">“</strong>
		<?cs var:qz_metadata.last_fwd_cmt ?>
	<strong class="quotes_symbols_right c_tx3">”</strong>
	<span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span>
	<?cs /if ?>
<?cs else ?>
	<strong class="quotes_symbols_left c_tx3">“</strong>
		<?cs call:richContent(qz_metadata.t1_con) ?>
		<?cs if:qz_metadata.richtype == 2 ?> 
			<?cs if:string.length(qz_metadata.url2) > 1 ?>
				<a href="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs var:qz_metadata.url1 ?>" title="原链接：<?cs var:qz_metadata.url2 ?>" class="c_tx" target="_blank"><?cs var:qz_metadata.url1 ?></a>
			<?cs else ?>
				<a href="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs var:qz_metadata.url1 ?>" class="c_tx" target="_blank"><?cs var:qz_metadata.url1 ?></a>
			<?cs /if ?>
		<?cs /if ?>
	<strong class="quotes_symbols_right c_tx3">”</strong><span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span>
<?cs /if ?>
<?cs /def ?>

<?cs def:level2TitleView() ?>
	评论我：<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>" class="c_tx">
		<?cs call:clearContent(qz_metadata.t1_con) ?>
	</a> <span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span>
<?cs /def ?>

<?cs def:level3TitleView() ?>
	在<?cs if:qz_metadata.beuin != qz_metadata.t1_uin && qz_metadata.operuin != qz_metadata.t1_uin && (qz_metadata.t1_uin != qz_metadata.t2.rep.uin || qz_metadata.t1_uin != qz_metadata.rep[subcount(qz_medata.rep) -1].uin) ?>
		<?cs call:textUserLink(qz_metadata.t1_uin, qz_metadata.t1_name , "", "", "comment_nickname") ?>的
		<?cs /if ?>：<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>" class="c_tx">
		<?cs call:clearContent(qz_metadata.t1_con) ?>
	</a> 回复我<span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span>
<?cs /def ?>

<?cs def:at1TitleView() ?>
	在：<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>" class="c_tx">
		<?cs call:clearContent(qz_metadata.t1_con) ?>
	</a> 中提到我 <span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span>
<?cs /def ?>

<?cs def:at2TitleView() ?>
	在：<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>" class="c_tx">
		<?cs call:clearContent(qz_metadata.t1_con) ?>
	</a> 中提到我 <span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span>
<?cs /def ?>

<?cs def:forward1TitleView(t1_uin,t1_tid,t1_con,t1_source_name) ?>
	转发我的说说：
	<a target="_blank" href="http://user.qzone.qq.com/<?cs var:t1_uin ?>/mood/<?cs var:t1_tid ?>.<?cs var:qz_metadata.t1_source ?>" class="c_tx">
		<?cs call:clearContent(t1_con) ?>
	</a> <span class="ifeeds_origin c_tx3">通过<?cs var:t1_source_name ?></span>
<?cs /def ?>

<?cs def:forward2TitleView(pre_fwd,t1_source_name) ?>
	转发我的说说：
	<a target="_blank" href="http://user.qzone.qq.com/<?cs var:pre_fwd.uin ?>/mood/<?cs var:pre_fwd.tid ?>.<?cs var:pre_fwd.source ?>" class="c_tx">
		<?cs var:pre_fwd.con ?>
	</a> <span class="ifeeds_origin c_tx3">通过<?cs var:t1_source_name ?></span>
<?cs /def ?>

<?cs #:这里写的有点怪异，没有闭合a元素，只负责生成a元素的前面一半 ?>
<?cs def:moodDetailsLink(uin,tid,source) ?>
	<?cs if:string.length(source) > 0?>
	<a target="_blank" href="http://user.qzone.qq.com/<?cs var:uin ?>/mood/<?cs var:tid ?>.<?cs var:source ?>" class="c_tx">
		<?cs else?>
	<a target="_blank" href="http://user.qzone.qq.com/<?cs var:uin ?>/mood/<?cs var:tid ?>" class="c_tx">
	<?cs /if?>
<?cs /def?>

<?cs def:imgUserLink(uin, name, s_uin, s_name, s_img) ?>
	<?cs set:uinmod = uin % 4 + 1?>
	<?cs set:uinsrc = "http://qlogo"+uinmod+".store.qq.com/qzone/"+uin+"/"+uin+"/30" ?>
	<?cs if:uin == qz_metadata.t1_uin?>
		<a class="feeds_comment_portrait c_tx q_namecard" link="nameCard_<?cs var:uin ?>" href="http://user.qzone.qq.com/<?cs var:uin ?>" target="_blank"><img alt="<?cs var:name ?>" src="<?cs var:uinsrc ?>" /></a>
	<?cs else?>
		<?cs if:string.length(s_uin) > 0?>
			<a class="feeds_comment_portrait c_tx q_namecard" href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:s_uin ?>" target="_blank"><img alt="<?cs var:s_name ?>" src="http://xy.store.qq.com/campus/<?cs var:s_img ?>/30" /></a>
		<?cs elif:string.length(uin) > 30?>
			<a class="feeds_comment_portrait c_tx q_namecard"  href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:uin ?>" target="_blank"><img alt="<?cs var:name ?>" src="http://xy.store.qq.com/campus/<?cs var:s_img ?>/30" /></a>
		<?cs else?>
			<a class="feeds_comment_portrait c_tx q_namecard" link="nameCard_<?cs var:uin ?>" href="http://user.qzone.qq.com/<?cs var:uin ?>" target="_blank"><img alt="<?cs var:name ?>" src="<?cs var:uinsrc ?>" /></a>
		<?cs /if?>
	<?cs /if?>
<?cs /def ?>

<?cs def:imageViewer(width) ?>
	<div class="imgbox img_thumb">
	<?cs if:string.length(qz_metadata.url3) > 0?>
		<img class="bor3" src="<?cs var:qz_metadata.url2 ?>"/>
	<?cs /if?>
	</div>
<?cs /def ?>

<?cs def:videoViewer(width) ?>
	<div class="imgbox video_img" richinfo="<?cs var:qz_metadata.richtype ?>" url1="<?cs var:qz_metadata.url1 ?>" url2="<?cs var:qz_metadata.url2 ?>" url3="<?cs var:qz_metadata.url3 ?>">
	<qz:popup version="3" param="<?cs var:qz_metadata.richtype ?>" src="/qzone/app/mood/richinfo_view.html" width="<?cs var:width ?>" height="365">
	<span>
		<span class="play_op">　</span>
		<img alt="视频缩略图" width="120" height="90" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,90,'<?cs var:qz_metadata.url1 ?>');" />
	</span>
	</qz:popup>
	</div>
<?cs /def ?>

<?cs def:linkViewer() ?>
	<a href="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs var:qz_metadata.url1 ?>" class="c_tx" target="_blank"><?cs var:qz_metadata.url1 ?></a>
<?cs /def ?>

<?cs def:clearContent-var(cons) ?>
	<?cs set:clearCon = "" ?>
	<?cs def:clearContent-var-items(item) ?>
		<?cs if:item.type == 'nick' ?>
			<?cs set:clearCon = clearCon + "@" + item.name ?>
		<?cs elif:item.type == 'url' ?>
			<?cs set:clearCon = clearCon + item.url ?>
		<?cs else ?>
			<?cs set:clearCon = clearCon + item ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 ?>
		<?cs each:item = cons.con ?>
			<?cs call:clearContent-var-items(item) ?>
		<?cs /each ?>
	<?cs elif:cons.con || subcount(cons.con) > 0 ?>
		<?cs call:clearContent-var-items(cons.con) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:这里要特别注意cs模板里面def的顺序 ?>

<?cs #:有头像的单条回复，主人动态的表现 ?>
<?cs def:replyItemView1(reply) ?>
	<div class="bbor5">
		<?cs call:imgUserLink(reply.uin, reply.nick, reply.t3_wc_uin, reply.t3_wc_nick, reply.t3_wc_img) ?>
		<div class="comment_reply_cont">
			<p class="comment_reply_text">
				<?cs call:textUserLink(reply.uin, reply.nick, reply.t3_wc_uin, reply.t3_wc_nick, "comment_nickname") ?>
				<?cs var:reply.msg ?>
			</p>
			<p class="comment_reply_op">
				<span class="feeds_time c_tx3"><?cs var:reply.t3_time ?></span>
			</p>
		</div>
	</div>
<?cs /def?>

<?cs #:没头像的单条回复，主动feed的表现，现阶段的表现和评论的表现一样 ?>
<?cs def:replyItemView2(reply)?>
<div class="feeds_comm_list bg2">
	<div class="feeds_comment_cont">
		<p class="feeds_comment_text">
			<?cs call:textUserLink(reply.uin, reply.nick, reply.t3_wc_uin, reply.t3_wc_nick, "comment_nickname") ?>
			<?cs var:reply.msg ?>
		</p>
		<p class="feeds_comment_op">
			<span class="feeds_time c_tx3"><?cs var:reply.t3_time ?></span>
		</p>
	</div>
</div>
<?cs /def?>

<?cs #:有头像的单条评论，好友动态的表现 ?>
<?cs def:commentItemView1(comment)?>
	<div class="feeds_comment_list bg2">
		<?cs call:imgUserLink(comment.t2_uin, comment.t2_name, comment.t2_wc_uin, comment.t2_wc_nick, comment.t2_wc_img) ?>
		<div class="feeds_comment_cont">
			<p class="feeds_comment_text">
				<?cs call:textUserLink(comment.t2_uin, comment.t2_name, comment.t2_wc_uin, comment.t2_wc_nick, "comment_nickname") ?>
				<?cs var:comment.reply ?>
			</p>
			<p class="feeds_comment_op">
				<span class="feeds_time c_tx3"><?cs var:comment.t2_time ?></span>
				<?cs if:string.length(comment.t2_wc_uin) > 0?>
					<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds" charset="UTF-8" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;t2_uin=<?cs var:comment.t2_wc_uin ?>&amp;t2_tid=<?cs var:comment.t2_id ?>" type="ubb" version="6.2">回复</qz:reply>
						<?cs else?>
					<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds" charset="UTF-8" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;t2_uin=<?cs var:comment.t2_uin ?>&amp;t2_tid=<?cs var:comment.t2_id ?>" type="ubb" version="6.2">回复</qz:reply>
				<?cs /if?>
			</p>
		</div>
		<?cs if:subcount(comment.rep) > 0 ?>
		<div class="comment_reply_list">
			<?cs if:comment.t2_total > 3 ?>
				<div class="more_feeds_comment bg2"><?cs call:moodDetailsLink(qz_metadata.t1_uin, qz_metadata.t1_tid, qz_metadata.t1_source) ?>查看全部<?cs var:comment.t2_total ?>条回复&gt;&gt;</a></div>
			<?cs /if ?>
			<?cs if:subcount(comment.rep.0) > 0?>
				<?cs each:reply = comment.rep ?>
					<?cs call:replyItemView1(reply) ?>
				<?cs /each?>
			<?cs else?>
				<?cs call:replyItemView1(comment.rep) ?>
			<?cs /if?>
		</div>
		<?cs /if ?>
	</div>
<?cs /def?>

<?cs #:没头像的单条评论，主动feed的表现，一般来说只会展示一条评论 ?>
<?cs def:commentItemView2(comment)?>
	<div class="feeds_comm_list bg2">
		<div class="feeds_comment_cont">
			<p class="feeds_comment_text">
				<?cs call:textUserLink(comment.t2_uin, comment.t2_name, comment.t2_wc_uin, comment.t2_wc_nick, "comment_nickname") ?>
				<?cs var:comment.reply ?>
			</p>
			<p class="feeds_comment_op">
				<span class="feeds_time c_tx3"><?cs var:comment.t2_time ?></span>
			</p>
		</div>
	</div>
<?cs /def?>

<?cs #:转发相关的被动feeds，只会显示最新一条评论且不显示回复 ?>
<?cs def:forwardSummaryView() ?>
<div class="feeds_tp_5">
	<?cs if:qz_metadata.dotype == 10003 ?>
		<?cs if:qz_metadata.richtype == 1 ?>
			<?cs call:imageViewer(374) ?>
		<?cs /if ?>
		<?cs if:qz_metadata.richtype == 3 ?>
			<?cs call:videoViewer(392) ?>
		<?cs /if ?>
	<?cs /if?>
	<?cs if:qz_metadata.dotype == 10004 ?>
		<div class="bor2 mood_rt_frame bgr3">
			<p class="mood_rt_text">
				<?cs #:可能需要修改的地方，原帖用户昵称的展示 ?>
				<?cs call:textUserLink(qz_metadata.rt_uin, qz_metadata.rt_name ,qz_metadata.rt_wc_uin, qz_metadata.rt_wc_nick, "nickname") ?>
				<span class="c_tx1">
					<?cs call:richContent(qz_metadata.t1_con) ?>
				</span>
				<?cs if:qz_metadata.richtype==2 ?> 
					<?cs call:linkViewer() ?>
				<?cs /if ?>
			</p>
			<?cs if:qz_metadata.richtype == 1 ?>
				<?cs call:imageViewer(374) ?>
			<?cs /if?>
			<?cs if:qz_metadata.richtype == 3 ?>
				<?cs call:videoViewer(374) ?>
			<?cs /if ?>
			<p class="mood_rt_info c_tx3">
				<span><?cs var:qz_metadata.rt_time ?></span>
				<span>通过<?cs var:qz_metadata.rt_source_name ?></span>
				<?cs var:qz_metadata.rt_sum ?>人转发
			</p>
		<?cs if:subcount(qz_metadata.rtlist) > 0 ?>
			<div class="mood_broadcast_list">
				<ul>
				<?cs if:qz_metadata.rtlist.0.uin ?>
					<?cs each:item = qz_metadata.rtlist ?>
						<li class="tbor3">
							<?cs call:textUserLink(item.uin, item.name, item.wc_uin, item.wc_nick, "comment_nickname") ?>
							<?cs var:item.con ?>
						</li>
					<?cs /each ?>
				<?cs else ?>
					<li class="tbor3">
						<?cs call:textUserLink(qz_metadata.rtlist.uin, qz_metadata.rtlist.name, qz_metadata.rtlist.wc_uin, qz_metadata.rtlist.wc_nick, "comment_nickname") ?>
						<?cs var:qz_metadata.rtlist.con ?>
					</li>
				<?cs /if ?>
				</ul>
			</div>
		<?cs /if ?>
		</div>
	<?cs /if ?>
	<?cs if:string.length(qz_metadata.last_fwd_cmt) > 0?>
	<div class="txtbox ifeed_rt_comment">
		并说：<strong class="quotes_symbols_left c_tx3">“</strong>
			<?cs var:qz_metadata.last_fwd_cmt ?>
		<strong class="quotes_symbols_right c_tx3">”</strong>
	</div>
	<?cs /if ?>
	<div class="feeds_tp_operate" oname="<?cs var:qz_metadata.rt_name ?>" ocon="<?cs call:clearContent(qz_metadata.t1_con) ?>" source="<?cs var:qz_metadata.t1_source ?>">
		<qz:reply type="link">评论</qz:reply>
	</div>
	<div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div>
		<?cs if:subcount(qz_metadata.t2) > 0 ?>
			<?cs if:subcount(qz_metadata.t2.0) > 0 ?>
					<?cs each:comment = qz_metadata.t2 ?>
						<?cs call:commentItemView2(comment) ?>
					<?cs /each?>
				<?cs else?>
					<?cs call:commentItemView2(qz_metadata.t2) ?>
			<?cs /if?>
		<?cs /if?>
		<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds" version="6" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;subdotype=<?cs var:qz_metadata.dotype ?>" type="ubb" charset="UTF-8">回复</qz:reply>
	</div>
</div>
<?cs /def ?>

<?cs #:好友动态默认表现 ?>
<?cs def:level1SummaryView() ?>
<?cs if:string.length(qz_metadata.rt_uin) > 0 ?>
	<div class="bor2 mood_rt_frame bgr3">
		<p class="mood_rt_text">
			<?cs #:可能需要修改的地方，原帖用户昵称的展示 ?>
			<?cs call:textUserLink(qz_metadata.rt_uin, qz_metadata.rt_name, qz_metadata.rt_wc_uin, qz_metadata.rt_wc_nick, "nickname") ?>
			<span class="c_tx1">
			<?cs call:richContent(qz_metadata.t1_con) ?>
			</span>
			<?cs if:qz_metadata.richtype == 2 ?> 
				<?cs call:linkViewer() ?>
			<?cs /if ?>
		</p>
		<?cs if:qz_metadata.richtype == 1 ?>
				<?cs call:imageViewer(374) ?>
		<?cs /if ?>
		<?cs if:qz_metadata.richtype == 3 ?>
				<?cs call:videoViewer(374) ?>
		<?cs /if ?>
		<p class="mood_rt_info c_tx3">
			<span><?cs var:qz_metadata.rt_time ?></span>
			<span>通过<?cs var:qz_metadata.rt_source_name ?></span>
<?cs #:新加的转发展示逻辑 ?>
<?cs if:subcount(qz_metadata.rtlist) > 0 ?>
<a href="javascript:;" class="c_tx"
onclick='var pns=this.parentNode.nextSibling;if(pns){pns.style.display="";if(pns.nextSibling){pns.nextSibling.style.display="";}}this.style.display="none";this.nextSibling.style.display="";return false;'
><?cs var:qz_metadata.rt_sum ?>人转发 ↓</a><a href="javascript:;"
style="display:none;" class="c_tx"
onclick='var pns=this.parentNode.nextSibling;if(pns){pns.style.display="none";if(pns.nextSibling){pns.nextSibling.style.display="none";}}this.style.display="none";this.previousSibling.style.display="";return false;'
>收起转发 ↑</a>
<?cs else ?>
	<?cs var:qz_metadata.rt_sum ?>人转发
<?cs /if ?>
		</p><?cs if:subcount(qz_metadata.rtlist) > 0 ?><div class="mood_broadcast_list" style="display:none;">
				<ul>
					<?cs if:subcount(qz_metadata.rtlist.0) > 0 ?>
						<?cs each:item = qz_metadata.rtlist ?>
							<li class="tbor3">
								<?cs call:textUserLink(item.uin, item.name, item.wc_uin, item.wc_nick, "comment_nickname") ?>
								<?cs var:item.con ?>
								<p class="c_tx3"><?cs var:item.time ?></p>
							</li>
						<?cs /each ?>
						<?cs else ?>
							<li class="tbor3">
								<?cs call:textUserLink(qz_metadata.rtlist.uin, qz_metadata.rtlist.name, qz_metadata.rtlist.wc_uin, qz_metadata.rtlist.wc_nick, "comment_nickname") ?>
								<span class="c_tx3">转：</span><?cs var:qz_metadata.rtlist.con ?>
								<p class="c_tx3"><?cs var:qz_metadata.rtlist.time ?></p>
							</li>
					<?cs /if ?>
				</ul>
			</div>
			<?cs if:subcount(qz_metadata.rtlist.0) > 0 ?>
				<?cs if:qz_metadata.rt_sum > 5 ?>
					<div class="mood_broadcast_ft tbor3" style="display:none;"><span class="c_tx3">以上仅显示部分转发理由</span></div>
				<?cs /if ?>
			<?cs /if?>
		<?cs /if ?>
	</div>
	<?cs set:oname = qz_metadata.rt_name ?>
	<?cs call:clearContent-var(qz_metadata.t1_con) ?>
	<?cs set:ocon = clearCon ?>
<?cs else ?><?cs #:上面是转发的，下面是正常的 ?>
	<?cs if:qz_metadata.richtype == 1 ?>
		<?cs call:imageViewer(374) ?>
	<?cs /if ?>
	<?cs if:qz_metadata.richtype == 3 ?>
		<?cs call:videoViewer(392) ?>
	<?cs /if ?>
	<?cs set:oname = qz_metadata.t1_name ?>
	<?cs call:clearContent-var(qz_metadata.t1_con) ?>
	<?cs set:ocon = clearCon ?>
<?cs /if ?>

<?cs /def ?>

<?cs #:评论的主人动态 ?>
<?cs def:level2SummaryView() ?>
	<div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div>
		<?cs call:commentItemView2(qz_metadata.t2) ?>
		<?cs if:qz_metadata.t2.t2_total > 3 ?>
			<div class="more_feeds_comment bg2"><?cs call:moodDetailsLink(qz_metadata.t1_uin, qz_metadata.t1_tid, qz_metadata.t1_source) ?>查看全部<?cs var:qz_metadata.t2.t2_total ?>条回复&gt;&gt;</a></div>
		<?cs /if ?>
		<?cs if:subcount(qz_metadata.t2.rep) > 0 ?>
			<?cs if:subcount(qz_metadata.t2.rep.0) > 0 ?>
				<?cs each:reply = qz_metadata.t2.rep ?>
					<?cs call:replyItemView2(reply) ?>
				<?cs /each ?>
			<?cs else ?>
				<?cs call:replyItemView2(qz_metadata.t2.rep) ?>
			<?cs /if ?>
		<?cs /if ?>
		<?cs if:string.length(qz_metadata.t2.t2_wc_uin) > 0 ?>
			<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_host_refeeds" version="6" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;t2_uin=<?cs var:qz_metadata.t2.t2_wc_uin ?>&amp;t2_tid=<?cs var:qz_metadata.t2.id ?>" type="ubb" charset="UTF-8">回复</qz:reply>
				<?cs else?>
			<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_host_refeeds" version="6" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;t2_uin=<?cs var:qz_metadata.t2.t2_uin ?>&amp;t2_tid=<?cs var:qz_metadata.t2.id ?>" type="ubb" charset="UTF-8">回复</qz:reply>
		<?cs /if?>
	</div>
<?cs /def ?>

<?cs #:回复的主人动态 ?>
<?cs def:level3SummaryView() ?>
	<div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div>
		<?cs call:commentItemView2(qz_metadata.t2) ?>
		<?cs if:qz_metadata.t2.t2_total > 3 ?>
			<div class="more_feeds_comment bg2"><?cs call:moodDetailsLink(qz_metadata.t1_uin, qz_metadata.t1_tid, qz_metadata.t1_source) ?>查看全部<?cs var:qz_metadata.t2.t2_total ?>条回复&gt;&gt;</a></div>
		<?cs /if ?>
		<?cs if:subcount(qz_metadata.t2.rep) > 0 ?>
			<?cs if:subcount(qz_metadata.t2.rep.0) > 0 ?>
				<?cs each:reply = qz_metadata.t2.rep ?>
					<?cs call:replyItemView2(reply) ?>
				<?cs /each ?>
			<?cs else ?>
				<?cs call:replyItemView2(qz_metadata.t2.rep) ?>
			<?cs /if ?>
		<?cs /if ?>
		<?cs if:string.length(qz_metadata.t2.t2_wc_uin) > 0 ?>
			<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_host_refeeds" version="6" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;t2_uin=<?cs var:qz_metadata.t2.t2_wc_uin ?>&amp;t2_tid=<?cs var:qz_metadata.t2.id ?>" type="ubb" charset="UTF-8">回复</qz:reply>
				<?cs else?>
			<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_host_refeeds" version="6" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;t2_uin=<?cs var:qz_metadata.t2.t2_uin ?>&amp;t2_tid=<?cs var:qz_metadata.t2.id ?>" type="ubb" charset="UTF-8">回复</qz:reply>
		<?cs /if?>
	</div>
<?cs /def ?>

<?cs #:被提到的主人动态 ?>
<?cs def:at1SummaryView() ?>
	<div class="feeds_comment">
		<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds" version="6.3" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;subdotype=1" type="ubb" charset="UTF-8">评论</qz:reply>
	</div>
<?cs /def ?>

<?cs #:被提到的主人动态 ?>
<?cs def:at2SummaryView() ?>
	<div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div>
		<?cs call:commentItemView2(qz_metadata.t2) ?>
		<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds" version="6" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;subdotype=1" type="ubb" charset="UTF-8">回复</qz:reply>
	</div>
<?cs /def ?>

<?cs #:好了启动程序开始 ?>
<?cs if:string.length(qz_metadata.dotype) > 0 ?>
	<?cs if:qz_metadata.dotype == 55702 ?>
		<?cs call:level2TitleView() ?>
		<?cs call:level2SummaryView() ?>
	<?cs elif:qz_metadata.dotype == 55802 ?>
		<?cs call:level3TitleView() ?>
		<?cs call:level3SummaryView() ?>
	<?cs elif:qz_metadata.dotype == 10001 ?>
		<?cs call:at1TitleView() ?>
		<?cs call:at1SummaryView() ?>
	<?cs elif:qz_metadata.dotype == 10002 ?>
		<?cs call:at2TitleView() ?>
		<?cs call:at2SummaryView() ?>
	<?cs elif:qz_metadata.dotype == 10003 ?>
		<?cs call:forward1TitleView(qz_metadata.t1_uin, qz_metadata.t1_tid, qz_metadata.t1_con, qz_metadata.t1_source_name) ?>
		<?cs call:forwardSummaryView() ?>
	<?cs elif:qz_metadata.dotype == 10004 ?>
		<?cs call:forward2TitleView(qz_metadata.pre_fwd, qz_metadata.t1_source_name) ?>
		<?cs call:forwardSummaryView() ?>
	<?cs /if ?>
<?cs else ?>
	<?cs call:level1TitleView() ?>
	<div class="feeds_tp_5"><?cs call:level1SummaryView() ?></div>
<?cs /if ?>