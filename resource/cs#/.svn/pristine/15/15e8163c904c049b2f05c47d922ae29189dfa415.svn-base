<?
cs def:prefixedUserLink(prefix, userName, href, link, className) ?><?
	cs if:string.length(prefix) > 0 ?>
		<a href="<?cs var:href ?>" class="c_tx" target="_blank"><?cs var:prefix ?><span class="q_namecard q_des <?cs var:className?>" link="<?cs var:link ?>"><?cs var:userName ?></span></a><?
	cs else ?>
		<a class="q_namecard q_des <?cs var:className?> c_tx" href="<?cs var:href ?>" link="<?cs var:link ?>" target="_blank"><?cs var:userName ?></a><?
	cs /if ?><?
cs /def ?><?

cs def:textUserLink(uin, name, s_uin, s_name, className, prefix) ?><?
	cs if:uin == qz_metadata.t1_uin ?><?
		cs call:prefixedUserLink(prefix, qz_metadata.t1_name, 'http://user.qzone.qq.com/' + qz_metadata.t1_uin, 'nameCard_' + qz_metadata.t1_uin + ' des_' + qz_metadata.t1_uin, className) ?><?
	cs else?><?
		cs if:string.length(s_uin) > 0 ?><?
			cs call:prefixedUserLink(prefix, s_name, 'http://profile.pengyou.qq.com/index.php?mod=profile&u=' + s_uin, '', className) ?><?
		cs elif:string.length(uin) > 30?><?
			cs call:prefixedUserLink(prefix, name, 'http://profile.pengyou.qq.com/index.php?mod=profile&u=' + uin, '', className) ?><?
		cs else?><?
			cs call:prefixedUserLink(prefix, name, 'http://user.qzone.qq.com/' + uin, 'nameCard_' + uin + ' des_' + uin, className) ?><?
		cs /if?><?
	cs /if?><?
cs /def ?><?

cs #:这里写的有点怪异，没有闭合a元素，只负责生成a元素的前面一半 ?><?
cs def:moodDetailsLink(uin,tid,source) ?><?
	cs if:string.length(source) > 0 ?>
		<a target="_blank" href="http://user.qzone.qq.com/<?cs var:uin ?>/mood/<?cs var:tid ?>.<?cs var:source ?>" class="c_tx"><?
	cs else ?>
		<a target="_blank" href="http://user.qzone.qq.com/<?cs var:uin ?>/mood/<?cs var:tid ?>" class="c_tx"><?
	cs /if ?><?
cs /def ?><?

cs def:imgUserLink(uin, name, s_uin, s_name, s_img) ?><?
	cs set:uinmod = uin % 4 + 1?><?
	cs set:uinsrc = "http://qlogo"+uinmod+".store.qq.com/qzone/"+uin+"/"+uin+"/30" ?><?
	cs if:uin == qz_metadata.t1_uin?>
		<a class="feeds_comment_portrait c_tx q_namecard" link="nameCard_<?cs var:uin ?>" href="http://user.qzone.qq.com/<?cs var:uin ?>" target="_blank"><img alt="<?cs var:name ?>" src="<?cs var:uinsrc ?>" /></a><?
	cs else ?><?
		cs if:string.length(s_uin) > 0?>
			<a class="feeds_comment_portrait c_tx q_namecard" href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:s_uin ?>" target="_blank"><img alt="<?cs var:s_name ?>" src="http://xy.store.qq.com/campus/<?cs var:s_img ?>/30" /></a><?
		cs elif:string.length(uin) > 30?>
			<a class="feeds_comment_portrait c_tx q_namecard"  href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:uin ?>" target="_blank"><img alt="<?cs var:name ?>" src="http://xy.store.qq.com/campus/<?cs var:s_img ?>/30" /></a><?
		cs else?>
			<a class="feeds_comment_portrait c_tx q_namecard" link="nameCard_<?cs var:uin ?>" href="http://user.qzone.qq.com/<?cs var:uin ?>" target="_blank"><img alt="<?cs var:name ?>" src="<?cs var:uinsrc ?>" /></a><?
		cs /if?><?
	cs /if?><?
cs /def ?><?

cs def:imageViewer(width) ?>
	<div class="img_thumb"><?
		cs if:string.length(qz_metadata.url3) > 0 ?><?
			cs #:对老数据作的兼容 ?><?
			cs if:string.length(qz_metadata.isurl) > 0 ?>
				<a onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs var:qz_metadata.url2 ?>', '<?cs var:qz_metadata.url3 ?>', <?cs var:width?>, <?cs var:qz_metadata.width?>+0, <?cs var:qz_metadata.height?>+0);return false;" href="javascript:;"><?
			cs else ?>
				<a onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs var:qz_metadata.url2 ?>', 'http://xa3g.photo.store.qq.com/<?cs var:qz_metadata.url3 ?>_374a600', <?cs var:width?>, <?cs var:qz_metadata.height?>+0, <?cs var:qz_metadata.width?>+0);return false;" href="javascript:;"><?
			cs /if ?><?
		cs else ?>
			<a onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs var:qz_metadata.url2 ?>', '<?cs var:qz_metadata.url2 ?>', <?cs var:width?>, <?cs var:qz_metadata.width?>+0, <?cs var:qz_metadata.height?>+0);return false;" href="javascript:;"><?
		cs /if ?>
			<img alt="点击查看大图" title="点击放大" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(160,150,'<?cs var:qz_metadata.url1 ?>');" /><?
			cs if:qz_metadata.pictype == 2 ?><span class="gif_flag"></span><?cs /if ?>
		</a>
	</div><?
cs /def ?><?

cs def:videoViewer(width) ?>
	<div class="imgbox video_img" richinfo="<?cs var:qz_metadata.richtype ?>" url1="<?cs var:qz_metadata.url1 ?>" url2="<?cs var:qz_metadata.url2 ?>" url3="<?cs var:qz_metadata.url3 ?>">
		<qz:popup version="3" param="<?cs var:qz_metadata.richtype ?>" src="/qzone/app/mood/richinfo_view.html" width="<?cs var:width ?>" height="365">
			<span>
				<span class="play_op">　</span>
				<img alt="视频缩略图" width="120" height="90" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,90,'<?cs var:qz_metadata.url1 ?>');" />
			</span>
		</qz:popup>
	</div><?
cs /def ?><?

cs def:linkViewer() ?>
	<a href='http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs escape:"url"?><?cs var:qz_metadata.url1 ?><?cs /escape?>' class="c_tx" target="_blank"><?cs var:qz_metadata.url1 ?></a><?
cs /def ?><?

cs def:richContent(cons) ?><?
	cs def:richContent-items(item) ?><?
		cs if:item.type == 'nick' ?><?
			cs if:string.length(item.uin) > 30 ?><?
				cs call:textUserLink("", "", item.uin, item.name, "comment_nickname", "@") ?><?
			cs else ?><?
				cs call:textUserLink(item.uin, item.name, "", "", "comment_nickname", "@") ?><?
			cs /if?><?
		cs elif:item.type == 'url' ?><?
			cs if:item.ourl ?>
				<a href='http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs escape:"url"?><?cs var:item.url ?><?cs /escape?>' title="原链接：<?cs var:item.ourl ?>" class="c_tx" target="_blank"><?cs var:item.url ?></a><?
			cs else ?>
				<a href='http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs escape:"url"?><?cs var:item.url ?><?cs /escape?>' class="c_tx" target="_blank"><?cs var:item.url ?></a><?
			cs /if ?><?
		cs else ?><?
			cs var:item ?><?
		cs /if ?><?
	cs /def ?><?
	cs if:cons.con.0 || subcount(cons.con.0) > 0 ?><?
		cs each:item = cons.con ?><?
			cs call:richContent-items(item) ?><?
		cs /each ?><?
	cs elif:cons.con || subcount(cons.con) > 0 ?><?
		cs call:richContent-items(cons.con) ?><?
	cs /if ?><?
cs /def ?><?

cs def:clearContent(cons) ?><?
	cs def:clearContent-items(item) ?><?
		cs if:item.type == 'nick' ?>@<?
			cs var:item.name ?><?
		cs elif:item.type == 'url' ?><?
			cs var:item.url ?><?
		cs else ?><?
			cs var:item ?><?
		cs /if ?><?
	cs /def ?><?
	cs if:cons.con.0 || subcount(cons.con.0) > 0 ?><?
		cs each:item = cons.con ?><?
			cs call:clearContent-items(item) ?><?
		cs /each ?><?
	cs elif:cons.con || subcount(cons.con) > 0 ?><?
		cs call:clearContent-items(cons.con) ?><?
	cs /if ?><?
cs /def ?><?

cs def:jsContent(cons) ?><?
	cs def:jsContent-items(item) ?><?
		cs if:item.type == 'nick' ?>@<?
			cs escape:'js' ?><?
				cs var:item.name ?><?
			cs /escape ?><?
		cs elif:item.type == 'url' ?><?
			cs escape:'js' ?><?
				cs var:item.url ?><?
			cs /escape ?><?
		cs else ?><?
			cs escape:'js' ?><?
				cs var:item ?><?
			cs /escape ?><?
		cs /if ?><?
	cs /def ?><?
	cs if:cons.con.0 || subcount(cons.con.0) > 0 ?><?
		cs each:item = cons.con ?><?
			cs call:jsContent-items(item) ?><?
		cs /each ?><?
	cs elif:cons.con || subcount(cons.con) > 0 ?><?
		cs call:jsContent-items(cons.con) ?><?
	cs /if ?><?
cs /def ?><?

cs def:clearContent-var(cons) ?><?
	cs set:clearCon = "" ?><?
	cs def:clearContent-var-items(item) ?><?
		cs if:item.type == 'nick' ?><?
			cs set:clearCon = clearCon + "@" + item.name ?><?
		cs elif:item.type == 'url' ?><?
			cs set:clearCon = clearCon + item.url ?><?
		cs else ?><?
			cs set:clearCon = clearCon + item ?><?
		cs /if ?><?
	cs /def ?><?
	cs if:cons.con.0 || subcount(cons.con.0) > 0 ?><?
		cs each:item = cons.con ?><?
			cs call:clearContent-var-items(item) ?><?
		cs /each ?><?
	cs elif:cons.con || subcount(cons.con) > 0 ?><?
		cs call:clearContent-var-items(cons.con) ?><?
	cs /if ?><?
cs /def ?><?

cs #:这里要特别注意cs模板里面def的顺序 ?><?

cs #:有头像的单条回复，主人动态的表现 ?><?
cs def:replyItemView1(reply) ?>
	<div class="bbor5"><?
		cs call:imgUserLink(reply.uin, reply.nick, reply.t3_wc_uin, reply.t3_wc_nick, reply.t3_wc_img) ?>
		<div class="comment_reply_cont">
			<p class="comment_reply_text"><?
				cs call:textUserLink(reply.uin, reply.nick, reply.t3_wc_uin, reply.t3_wc_nick, "comment_nickname", "") ?><?
				cs if:subcount(reply.t3_con) > 0 ?><?
					cs call:richContent(reply.t3_con) ?><?
				cs else ?><?
					cs var:reply.msg ?><?
				cs /if ?>
			</p>
			<p class="comment_reply_op">
				<span class="feeds_time c_tx3"><?cs var:reply.t3_time ?></span>
			</p>
		</div>
	</div><?
cs /def?><?

cs #:没头像的单条回复，主动feed的表现，现阶段的表现和评论的表现一样 ?><?
cs def:replyItemView2(reply)?>
<div class="feeds_comm_list bg2">
	<div class="feeds_comment_cont">
		<p class="feeds_comment_text"><?
			cs call:textUserLink(reply.uin, reply.nick, reply.t3_wc_uin, reply.t3_wc_nick, "comment_nickname", "") ?><?
			cs if:subcount(reply.t3_con) > 0 ?><?
				cs call:richContent(reply.t3_con) ?><?
			cs else ?><?
				cs var:reply.msg ?><?
			cs /if ?>
		</p>
		<p class="feeds_comment_op">
			<span class="feeds_time c_tx3"><?cs var:reply.t3_time ?></span>
		</p>
	</div>
</div><?
cs /def?><?

cs #:有头像的单条评论，好友动态的表现 ?><?
cs def:commentItemView1(comment,dotype) ?>
	<div class="feeds_comment_list bg2"><?
		cs call:imgUserLink(comment.t2_uin, comment.t2_name, comment.t2_wc_uin, comment.t2_wc_nick, comment.t2_wc_img) ?>
		<div class="feeds_comment_cont">
			<p class="feeds_comment_text"><?
				cs call:textUserLink(comment.t2_uin, comment.t2_name, comment.t2_wc_uin, comment.t2_wc_nick, "comment_nickname", "") ?><?
				cs if:subcount(comment.t2_con) > 0 ?><?
					cs call:richContent(comment.t2_con) ?><?
				cs else ?><?
					cs var:comment.reply ?><?
				cs /if ?>
			</p>
			<p class="feeds_comment_op">
				<span class="feeds_time c_tx3"><?cs var:comment.t2_time ?></span><?
				cs if:string.length(comment.t2_wc_uin) > 0?>
					<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds" charset="UTF-8" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;t2_uin=<?cs var:comment.t2_wc_uin ?>&amp;t2_tid=<?cs var:comment.t2_id ?>&amp;subdotype=<?cs var:dotype ?>" type="ubb" version="6.2" tuin="<?cs var:comment.t2_wc_uin ?>">回复</qz:reply><?
				cs else ?>
					<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds" charset="UTF-8" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;t2_uin=<?cs var:comment.t2_uin ?>&amp;t2_tid=<?cs var:comment.t2_id ?>&amp;subdotype=<?cs var:dotype ?>" type="ubb" version="6.2" tuin="<?cs var:comment.t2_uin ?>">回复</qz:reply><?
				cs /if ?>
			</p>
		</div><?
		cs if:subcount(comment.rep) > 0 ?>
			<div class="comment_reply_list"><?
				cs if:comment.t2_total > 3 ?>
					<div class="more_feeds_comment bg2"><?cs call:moodDetailsLink(qz_metadata.t1_uin, qz_metadata.t1_tid, qz_metadata.t1_source) ?>查看全部<?cs var:comment.t2_total ?>条回复&gt;&gt;</a></div><?
				cs /if ?><?
				cs if:subcount(comment.rep.0) > 0?><?
					cs each:reply = comment.rep ?><?
						cs call:replyItemView1(reply) ?><?
					cs /each?><?
				cs else?><?
					cs call:replyItemView1(comment.rep) ?><?
				cs /if?>
			</div><?
		cs /if ?>
	</div><?
cs /def ?><?

cs #:没头像的单条评论，主动feed的表现，一般来说只会展示一条评论 ?><?
cs def:commentItemView2(comment) ?>
	<div class="feeds_comm_list bg2">
		<div class="feeds_comment_cont">
			<p class="feeds_comment_text"><?
				cs call:textUserLink(comment.t2_uin, comment.t2_name, comment.t2_wc_uin, comment.t2_wc_nick, "comment_nickname", "") ?><?
				cs if:subcount(comment.t2_con) > 0 ?><?
					cs call:richContent(comment.t2_con) ?><?
				cs else ?><?
					cs var:comment.reply ?><?
				cs /if ?>
			</p>
			<p class="feeds_comment_op">
				<span class="feeds_time c_tx3"><?cs var:comment.t2_time ?></span>
			</p>
		</div>
	</div><?
cs /def ?><?

cs #:展示原帖 ?><?
cs def:originContentView() ?>
	<div class="bor2 mood_rt_frame bgr3">
		<p class="mood_rt_text"><?
			cs #:可能需要修改的地方，原帖用户昵称的展示 ?><?
			cs call:textUserLink(qz_metadata.rt_uin, qz_metadata.rt_name, qz_metadata.rt_wc_uin, qz_metadata.rt_wc_nick, "nickname", "") ?><?
			cs if:subcount(qz_metadata.lbs) > 0 ?>在<a href="http://rc.qzone.qq.com/myhome/qqmeishi?shopid=<?cs var:qz_metadata.lbs.id ?>" target="_blank" class="c_tx geoname"><?cs var:qz_metadata.lbs.idname ?></a>：<?cs /if ?>
			<span class="c_tx1"><?
				cs call:richContent(qz_metadata.t1_con)
			?></span><?
			cs if:qz_metadata.richtype == 2 ?><?
				cs call:linkViewer() ?><?
			cs /if ?>
		</p><?
		cs if:qz_metadata.richtype == 1 ?>
			<?cs call:imageViewer(374) ?><?
		cs /if ?><?
		cs if:qz_metadata.richtype == 3 ?><?
			cs call:videoViewer(374) ?><?
		cs /if ?><?
		cs if:subcount(qz_metadata.lbs) > 0 ?>
			<div class="location">
				<span><?cs var:qz_metadata.lbs.name ?></span><qz:popup version=5 src="/qzone/app/controls/map/tips.html" width="300" height="200" param="?posx=<?cs var:qz_metadata.lbs.pos_x ?>&posy=<?cs var:qz_metadata.lbs.pos_y ?>" title="点击查看地图">显示地图</qz:popup>
			</div><?
		cs /if ?>
		<p class="mood_rt_info c_tx3">
			<span><?cs var:qz_metadata.rt_time ?></span><?
			cs if:string.length(qz_metadata.rt_source_url) == 0 ?>
				<span>通过<?cs var:qz_metadata.rt_source_name ?></span><?
			cs else ?>
				<a class="c_tx3" href="<?cs var:qz_metadata.rt_source_url ?>" target="_blank">通过<?cs var:qz_metadata.rt_source_name ?></a><?
			cs /if ?><?
			cs #:新加的转发展示逻辑 ?><?
			cs var:qz_metadata.rt_sum ?>人转发
		</p><?
		cs if:subcount(qz_metadata.rtlist) > 0 ?>
			<div class="mood_broadcast_list" style="display:none;">
				<ul><?
					cs if:subcount(qz_metadata.rtlist.0) > 0 ?><?
						cs each:item = qz_metadata.rtlist ?>
							<li class="tbor3"><?
								cs call:textUserLink(item.uin, item.name, item.wc_uin, item.wc_nick, "comment_nickname", "") ?><?
								cs if:subcount(item.at_con) > 0 || string.length(item.con) > 0?>
									<span class="c_tx3">转：</span><?
									cs if:subcount(item.at_con) > 0 ?><?
										cs call:richContent(item.at_con) ?><?
									cs else ?><?
										cs var:item.con ?><?
									cs /if ?><?
								cs else?>
									<span class="c_tx3">转发了这条说说</span><?
								cs /if?>
								<p class="c_tx3"><?cs var:item.time ?></p>
							</li><?
						cs /each ?><?
					cs else ?>
						<li class="tbor3"><?
							cs call:textUserLink(qz_metadata.rtlist.uin, qz_metadata.rtlist.name, qz_metadata.rtlist.wc_uin, qz_metadata.rtlist.wc_nick, "comment_nickname", "") ?><?
							cs if:subcount(qz_metadata.rtlist.at_con) > 0 || string.length(qz_metadata.rtlist.con) > 0?>
								<span class="c_tx3">转：</span><?
								cs if:subcount(qz_metadata.rtlist.at_con) > 0 ?><?
									cs call:richContent(qz_metadata.rtlist.at_con) ?><?
								cs else ?><?
									cs var:qz_metadata.rtlist.con ?><?
								cs /if ?><?
							cs else?>
								<span class="c_tx3">转发了这条说说</span><?
							cs /if?>
							<p class="c_tx3"><?cs var:qz_metadata.rtlist.time ?></p>
						</li><?
					cs /if ?>
				</ul>
			</div><?
			cs if:subcount(qz_metadata.rtlist.0) > 0 ?><?
				cs if:qz_metadata.rt_sum > 5 ?>
					<div class="mood_broadcast_ft tbor3" style="display:none;"><span class="c_tx3">以上仅显示部分转发理由</span></div><?
				cs /if ?><?
			cs /if?><?
		cs /if ?>
	</div><?
cs /def ?><?
cs #:主帖主动 Feed 正文 ?><?
cs def:level1SummaryView() ?><?
	cs if:string.length(qz_metadata.rt_uin) > 0 ?><?
		cs call:originContentView() ?><?
		cs set:oname = qz_metadata.rt_name ?><?
		cs call:clearContent-var(qz_metadata.t1_con) ?><?
		cs set:ocon = clearCon ?><?
	cs else ?><?
		cs #:上面是转发的，下面是正常的 ?><?
		cs if:qz_metadata.richtype == 1 ?><?
			cs call:imageViewer(374) ?><?
		cs /if ?><?
		cs if:qz_metadata.richtype == 3 ?><?
			cs call:videoViewer(392) ?><?
		cs /if ?><?
		cs set:oname = qz_metadata.t1_name ?><?
		cs call:clearContent-var(qz_metadata.t1_con) ?><?
		cs set:ocon = clearCon ?><?
		cs if:subcount(qz_metadata.lbs) > 0 ?>
			<div class="location">
				<span><?cs var:qz_metadata.lbs.name ?></span><qz:popup version=5 src="/qzone/app/controls/map/tips.html" width="300" height="200" param="?posx=<?cs var:qz_metadata.lbs.pos_x ?>&posy=<?cs var:qz_metadata.lbs.pos_y ?>" title="点击查看地图">显示地图</qz:popup>
			</div><?
		cs /if ?><?
	cs /if ?>
	<div class="feeds_tp_operate"
		oname="<?cs var:oname ?>"
		ocon="<?cs var:ocon ?>"
		source="<?cs var:qz_metadata.t1_source ?>"
		source2="<?cs var:qz_metadata.rt_source ?>"
		uin1="<?cs var:qz_metadata.t1_uin ?>"
		tid1="<?cs var:qz_metadata.t1_tid ?>"
		uin2="<?cs var:qz_metadata.rt_uin ?>"
		tid2="<?cs var:qz_metadata.rt_tid ?>"><?
		cs def:retweetInfo() ?><?
			cs def:user(uin,name)
				?>{<?cs #:r
					?>uin:'<?cs var:uin ?>',<?cs #:r
					?>nickname:restHTML('<?cs escape:'js' ?><?cs var:name ?><?cs /escape ?>')<?cs #:r
				?>}<?
			cs /def
			?>{<?cs #:r
				?>id:'<?cs var:qz_metadata.t1_tid ?>',<?cs #:r
				?>poster:<?cs call:user(qz_metadata.t1_uin, qz_metadata.t1_name) ?>,<?
				cs if:qz_metadata.rt_uin
					?>owner:<?cs call:user(qz_metadata.rt_uin, qz_metadata.rt_name) ?>,<?
				cs else
					?>owner:<?cs call:user(qz_metadata.t1_uin, qz_metadata.t1_name) ?>,<?
				cs /if
				?>content:restHTML('<?cs call:jsContent(qz_metadata.t1_con) ?>'),<?cs #:r
				?>source:'<?cs escape:'js' ?><?cs var:qz_metadata.t1_source ?><?cs /escape ?>'<?cs #:r
			?>}<?
		cs /def ?>
		<qz:reply type="link" tuin="<?cs var:qz_metadata.t1_uin ?>" name="reply">评论</qz:reply> <qz:popup version="4" type="RetweetBox" param="<?cs call:retweetInfo() ?>" needcontainer="1" src="/qzone/app/mood/retweetBoxFacade.js" link="/qzonestyle/qzone_app/app_feeds_v1/mood_feeds.css">转发</qz:popup> <?cs call:moodDetailsLink(qz_metadata.t1_uin, qz_metadata.t1_tid, qz_metadata.t1_source) ?>详情</a> <?
		cs if:qz_metadata.signin > 0 ?>
			<qz:popup src="/qzone/app/mood/info/checkin.html" width="318" height="400" version ="5" config="id:checkin|closeButtonColor:transparent|arrowOffset:10|noflush:true">我也签到</qz:popup><?
		cs /if ?>
	</div>
	<div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div><?
		cs if:qz_metadata.noreply == 1 ?><?
			cs if:qz_metadata.t1_total && qz_metadata.t1_total > 0 ?>
				<div class="more_feeds_comment bg2"><?cs call:moodDetailsLink(qz_metadata.t1_uin, qz_metadata.t1_tid, qz_metadata.t1_source) ?>目前已有<?cs var:qz_metadata.t1_total ?>条评论&gt;&gt;</a></div><?
			cs /if ?><?
		cs else ?><?
			cs if:subcount(qz_metadata.t2) > 0 ?><?
				cs if:qz_metadata.t1_total && qz_metadata.t1_total > 3 ?>
					<div class="more_feeds_comment bg2">
						<qz:more action="http://taotao.qq.com/cgi-bin/emotion_cgi_getcomments?uin=<?cs var:qz_metadata.t1_uin ?>&amp;pos=0&amp;num=25&amp;cmtnum=100&amp;t1_source=<?cs var:qz_metadata.t1_source ?>&amp;tid=<?cs var:qz_metadata.t1_tid ?>&amp;who=1" param="" charset="UTF-8">
							展开其余<?cs var:qz_metadata.t1_total - 3 ?>条评论&gt;&gt;
						</qz:more>
					</div><?
				cs /if ?><?
				cs if:subcount(qz_metadata.t2.0) > 0 ?><?
					cs each:comment = qz_metadata.t2 ?><?
						cs call:commentItemView1(comment,qz_metadata.dotype) ?><?
					cs /each?><?
				cs else ?><?
					cs call:commentItemView1(qz_metadata.t2,qz_metadata.dotype) ?><?
				cs /if ?><?
			cs /if ?><?
		cs /if ?>
		<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds" version="6" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>" type="ubb" charset="UTF-8" tuin="<?cs var:qz_metadata.t1_uin ?>">回复</qz:reply>
	</div><?
cs /def ?><?
cs #:@我的主贴被转发或评论或回复 ?>
<div class="feeds_tp_5"><?cs call:level1SummaryView() ?></div>