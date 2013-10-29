<?
cs def:level2SummaryView() ?><?
	cs if:string.length(qz_metadata.rt_uin) > 0 ?><?
		cs call:originContentView() ?><?
	cs /if ?>
	<div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div><?
		cs call:commentItemView2(qz_metadata.t2) ?><?
		cs if:qz_metadata.t2.t2_total > subcount(qz_metadata.t2.rep) ?>
			<div class="more_feeds_comment bg2"><?
				cs if:subcount(qz_metadata.t2.rep) == 25 ?>
					<?cs call:moodDetailsLink(qz_metadata.t1_uin, qz_metadata.t1_tid, qz_metadata.t1_source) ?>查看全部<?cs var:qz_metadata.t2.t2_total ?>条回复&gt;&gt;</a><?
				cs else ?>
					<qz:more action="http://taotao.qq.com/cgi-bin/emotion_cgi_ic_getreplies" param="uin=<?cs var:qz_metadata.t1_uin ?>&amp;pos=0&amp;num=25&amp;t1_source=<?cs var:qz_metadata.t1_source ?>&amp;tid=<?cs var:qz_metadata.t1_tid ?>&amp;t2_uin=<?cs var:qz_metadata.t2.t2_uin ?>&amp;t2_tid=<?cs var:qz_metadata.t2.id ?>" charset="UTF-8">
						展开其余<?cs var:qz_metadata.t2.t2_total - 3 ?>条回复&gt;&gt;
					</qz:more><?
				cs /if ?>
			</div><?
		cs /if ?><?
		cs if:subcount(qz_metadata.t2.rep) > 0 ?><?
			cs if:subcount(qz_metadata.t2.rep.0) > 0 ?><?
				cs loop:x = 0, subcount( qz_metadata.t2.rep) - 1, 1 ?><?
					cs call:replyItemView2( qz_metadata.t2.rep[x]) ?><?
				cs /loop?><?
			cs else ?><?
				cs call:replyItemView2(qz_metadata.t2.rep) ?><?
			cs /if ?><?
		cs /if ?><?
		cs if:string.length(qz_metadata.t2.t2_wc_uin) > 0 ?>
			<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_host_refeeds" version="6" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;t2_uin=<?cs var:qz_metadata.t2.t2_wc_uin ?>&amp;t2_tid=<?cs var:qz_metadata.t2.id ?>&amp;signin=<?cs if:qz_metadata.signin == 1 ?>1<?cs else ?>0<?cs /if ?>" config="1|1|1|1,<?cs if:qz_metadata.auth_flag ?>1<?cs else ?>b52<?cs /if ?>,with_fwd,同时转发;<?cs if:qz_metadata.to_tweet ?>b41,0,to_tweet,点评到微博<?cs else ?>0<?cs /if ?>|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick" type="ubb" charset="UTF-8" tuin="<?cs var:qz_metadata.t2.t2_wc_uin ?>">回复</qz:reply><?
		cs else?>
			<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_host_refeeds" version="6" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;t2_uin=<?cs var:qz_metadata.t2.t2_uin ?>&amp;t2_tid=<?cs var:qz_metadata.t2.id ?>&amp;signin=<?cs if:qz_metadata.signin == 1 ?>1<?cs else ?>0<?cs /if ?>" config="1|1|1|1,<?cs if:qz_metadata.auth_flag ?>1<?cs else ?>b52<?cs /if ?>,with_fwd,同时转发;<?cs if:qz_metadata.to_tweet ?>b41,0,to_tweet,点评到微博<?cs else ?>0<?cs /if ?>|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick" type="ubb" charset="UTF-8" tuin="<?cs var:qz_metadata.t2.t2_uin ?>">回复</qz:reply><?
		cs /if?>
	</div><?
cs /def ?>