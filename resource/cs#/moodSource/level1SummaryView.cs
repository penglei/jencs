<?
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
		cs elif:qz_metadata.richtype == 3 ?><?
			cs call:videoViewer(392) ?><?
		cs elif:qz_metadata.richtype == 4 ?><?
			cs call:voteViewer(qz_metadata.vote) ?><?
		cs elif:qz_metadata.richtype == 5 ?><?
			cs call:attachViewer(qz_metadata.attach) ?><?
		cs /if ?><?
		cs set:oname = qz_metadata.t1_name ?><?
		cs call:clearContent-var(qz_metadata.t1_con) ?><?
		cs set:ocon = clearCon ?><?
		cs if:subcount(qz_metadata.lbs) > 0 ?><?
			cs if:string.length(qz_metadata.lbs.name) != 0 || (string.length(qz_metadata.lbs.pos_x) != 0 && string.length(qz_metadata.lbs.pos_y) != 0)?>
				<div class="location">
					<?cs if:string.length(qz_metadata.lbs.name) != 0?>
						<span><?cs var:qz_metadata.lbs.name ?></span>
					<?cs /if?>
					<?cs if:string.length(qz_metadata.lbs.pos_x) != 0 && string.length(qz_metadata.lbs.pos_y) != 0?>
						<?cs if:qz_metadata.signin == 1 ?>
							<qz:popup height="558" width="548" version="" title="<?cs var:qz_metadata.lbs.idname ?>" src="http://qzs.qq.com/qzone/app/lbs/popup.html#poiid=<?cs var:qz_metadata.lbs.id ?>" param="" >查看地图</qz:popup>
						<?cs else ?>
							<qz:popup version=5 src="/qzone/app/controls/map/tips.html" width="300" height="200" config="id:map" param="?posx=<?cs var:qz_metadata.lbs.pos_x ?>&posy=<?cs var:qz_metadata.lbs.pos_y ?>" title="点击查看地图">显示地图</qz:popup>
						<?cs /if ?>
					<?cs /if ?>
				</div><?
			cs /if ?><?
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
		cs def:retweetListParmas()
			?>{<?cs #:r
				?>tid:'<?cs var:qz_metadata.t1_tid ?>',<?cs #:r
				?>uin:'<?cs var:qz_metadata.t1_uin ?>',<?cs #:r
				?>pfType:<?
					cs if:qz_metadata.t1_source ?><?
						cs var:qz_metadata.t1_source ?><?
					cs else ?><?cs #:r
						?>1<?
					cs /if ?>,<?cs #:r
				?>rtTid:'<?cs var:qz_metadata.rt_tid ?>',<?cs #:r
				?>rtUin:'<?cs var:qz_metadata.rt_uin ?>',<?cs #:r
				?>rtPfType:<?
					cs if:qz_metadata.rt_source ?><?
						cs var:qz_metadata.rt_source ?><?
					cs else ?><?cs #:r
						?>1<?
					cs /if ?>,<?cs #:r
				?>totalForShow:<?
					cs if:qz_metadata.rt_sum ?><?
						cs var:qz_metadata.rt_sum ?><?
					cs else ?><?cs #:r
						?>0<?
					cs /if
			?>}<?
		cs /def ?><?
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
				?>source:'<?cs escape:'js' ?><?cs var:qz_metadata.t1_source ?><?cs /escape ?>',<?cs #:r
				?>retweetListInfo:<?cs call:retweetListParmas() ?>,<?cs #:r
				?>isSignIn:<?cs if:qz_metadata.signin == 1 ?>1<?cs else ?>0<?cs /if ?><?cs #:r
			?>}<?
		cs /def ?><?
		cs if:qz_metadata.t1_total > 3 && subcount(qz_metadata.t2) > 0 && (subcount(qz_metadata.t2) == qz_metadata.t1_total || subcount(qz_metadata.t2) == 25) ?>
			<qz:fc>收起评论</qz:fc><?
		cs else ?>
			<qz:reply type="link" tuin="<?cs var:qz_metadata.t1_uin ?>" name="reply">评论</qz:reply><?
		cs /if ?>
		<qz:popup version="4" type="RetweetBox" param="<?cs call:retweetInfo() ?>" needcontainer="1" src="/qzone/app/mood/retweetBoxFacade.js" link="/qzonestyle/qzone_app/app_feeds_v1/mood_feeds.css"><span style="margin:0">转发</span></qz:popup><?
		cs call:moodDetailsLink(qz_metadata.t1_uin, qz_metadata.t1_tid, qz_metadata.t1_source) ?>详情</a><?
		cs if:qz_metadata.signin == 1 ?>
			<qz:popup src="/qzone/app/mood/info/checkin.html" width="318" height="400" version ="5" config="id:checkin|closeButtonColor:#D4CFC2;font-weight:400;|arrowOffset:10|noflush:true">我也签到</qz:popup><?
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
				cs if:qz_metadata.t1_total > subcount(qz_metadata.t2) ?>
					<div class="more_feeds_comment bg2"><?
						cs if:subcount(qz_metadata.t2) == 25 ?>
							<?cs call:moodDetailsLink(qz_metadata.t1_uin, qz_metadata.t1_tid, qz_metadata.t1_source) ?>查看全部<?cs var:qz_metadata.t1_total ?>条评论&gt;&gt;</a><?
						cs else ?>
							<qz:more action="http://taotao.qq.com/cgi-bin/emotion_cgi_ic_getcomments" param="uin=<?cs var:qz_metadata.t1_uin ?>&amp;pos=0&amp;num=25&amp;cmtnum=100&amp;t1_source=<?cs var:qz_metadata.t1_source ?>&amp;tid=<?cs var:qz_metadata.t1_tid ?>&amp;who=1" charset="UTF-8">
								展开其余<?cs var:qz_metadata.t1_total - 3 ?>条评论&gt;&gt;
							</qz:more><?
						cs /if ?>
					</div><?
				cs /if ?><?
				cs if:subcount(qz_metadata.t2.0) > 0 ?><?
					cs loop:x = 0, subcount(qz_metadata.t2) - 1, 1 ?><?
						cs call:commentItemView1(qz_metadata.t2[x],qz_metadata.dotype) ?><?
					cs /loop?><?
				cs else ?><?
					cs call:commentItemView1(qz_metadata.t2,qz_metadata.dotype) ?><?
				cs /if ?><?
			cs /if ?><?
		cs /if ?><?
		cs #:subcount(qz_metadata.t2.0) 表明 qz_metadata.t2 是个数组 ?><?
		cs if:subcount(qz_metadata.t2.0) > 0 && subcount(qz_metadata.t2) == 25 ?>
			<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds" version="6" config="1|1|1|1,<?cs if:qz_metadata.auth_flag ?>1<?cs else ?>b52<?cs /if ?>,with_fwd,同时转发;<?cs if:qz_metadata.to_tweet ?>b41,0,to_tweet,点评到微博<?cs else ?>0<?cs /if ?>|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;signin=<?cs if:qz_metadata.signin == 1 ?>1<?cs else ?>0<?cs /if ?>&amp;num=25" type="ubb" charset="UTF-8" tuin="<?cs var:qz_metadata.t1_uin ?>">回复</qz:reply><?
		cs else ?>
			<qz:reply action="http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds" version="6" config="1|1|1|1,<?cs if:qz_metadata.auth_flag ?>1<?cs else ?>b52<?cs /if ?>,with_fwd,同时转发;<?cs if:qz_metadata.to_tweet ?>b41,0,to_tweet,点评到微博<?cs else ?>0<?cs /if ?>|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick" param="t1_source=<?cs var:qz_metadata.t1_source ?>&amp;t1_uin=<?cs var:qz_metadata.t1_uin ?>&amp;t1_tid=<?cs var:qz_metadata.t1_tid ?>&amp;signin=<?cs if:qz_metadata.signin == 1 ?>1<?cs else ?>0<?cs /if ?>" type="ubb" charset="UTF-8" tuin="<?cs var:qz_metadata.t1_uin ?>">回复</qz:reply><?
		cs /if ?>
	</div><?
	cs if:string.length(qz_metadata.rt_uin) > 0 ?>
		<qz:data key1='http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>' key2='http://user.qzone.qq.com/<?cs var:qz_metadata.rt_uin ?>/mood/<?cs var:qz_metadata.rt_tid ?>.<?cs var:qz_metadata.rt_source ?>'/><?
	cs else ?>
		<qz:data key1='http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>'/><?
	cs /if ?><?
cs /def ?>