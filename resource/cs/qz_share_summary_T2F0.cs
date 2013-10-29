<?cs set:feed = qz_metadata.feed ?>
<?cs set:titletype = qz_metadata.title_sec.titletype ?>
<?cs set:title = qz_metadata.title_sec.title ?>
<?cs set:uin = qz_metadata.title_sec.uin ?>
<?cs set:itemid = qz_metadata.title_sec.itemid ?>
<?cs set:fromtype = qz_metadata.title_sec.fromtype ?>
<?cs set:typename = qz_metadata.title_sec.typename ?>
<?cs set:type = qz_metadata.type ?>
<?cs set:resuin = qz_metadata.resuin ?>
<?cs set:resnick = qz_metadata.resnick ?>
<?cs set:resid = qz_metadata.resid ?>
<?cs set:shareuin = qz_metadata.shareuin ?>
<?cs set:shareid = qz_metadata.shareid ?>
<?cs set:picnum = qz_metadata.picnum ?>
<?cs set:musictime  = qz_metadata.musictime  ?>
<?cs set:feedstype = qz_metadata.feedstype?>
<?cs set:fromsource = qz_metadata.bbssource?>
<?cs if:feedstype==1?>
	<?cs set:feedstypename = '喜欢' ?>
<?cs else?>
	<?cs set:feedstypename = '分享' ?>
<?cs /if?>

<?cs if:string.length(qz_metadata.images.0.imageurl) > 0?>
	<?cs set:images_cursor = 0?>
	<?cs each:item = qz_metadata.images?>
		<?cs set:images[images_cursor].imageurl = item.imageurl ?>
		<?cs set:images[images_cursor].imageurl_b = item.imageurl_b ?>
		<?cs set:images_cursor = images_cursor + 1?>
	<?cs /each?>
<?cs elif:string.length(qz_metadata.images.imageurl) > 0?>
	<?cs set:images[0].imageurl = qz_metadata.images.imageurl ?>
	<?cs set:images[0].imageurl_b = qz_metadata.images.imageurl_b ?>
<?cs /if?>

<?cs set:title = qz_metadata.title ?>
<?cs set:url = qz_metadata.url ?>
<?cs set:imagenum = qz_metadata.imagenum ?>
<?cs set:summary = qz_metadata.summary ?>
<?cs set:sharecount = qz_metadata.sharecount ?>
<?cs set:bbssource = qz_metadata.bbssource ?>
<?cs set:albumname = qz_metadata.albumname ?>
<?cs set:albumurl = qz_metadata.albumurl ?>
<?cs set:replyparam = qz_metadata.replyparam ?>
<?cs set:delcommpart = qz_metadata.delcommpart ?>
<?cs set:commtotal = qz_metadata.commtotal ?>
<?cs set:videourl = qz_metadata.videourl ?>
<?cs set:noreply = qz_metadata.noreply ?>


<?cs #:平台组默认给所有字段做了html编码，这里的自定义方法现在不做处理 ?>
<?cs def:html_escape(source)?><?cs escape:"none"?><?cs var:source?><?cs /escape?><?cs /def?>
<?cs def:url_escape(source)?><?cs escape:"url"?><?cs var:source?><?cs /escape?><?cs /def?>
<?cs def:js_escape(source)?><?cs escape:"js"?><?cs var:source?><?cs /escape?><?cs /def?>

<?cs def:viewerUrl(image, index) ?>http://imgcache.qq.com/qzone/app/snslib/widget/imageViewer/imageViewer.html#scriptPath=customs/<?cs if:type == 1 || type == 2 || type == 12 || type == 13 || type == 14 || type == 15?>share.js&uin=<?cs var:shareuin ?>&shareid=<?cs var:shareid ?><?cs elif:type == 2 ?>album.js&uin=<?cs var:resuin ?>&albumid=<?cs var:resid ?><?cs elif:type == 10 ?>goods.js&uin=<?cs var:shareuin ?>&shareid=<?cs var:shareid ?><?cs else ?>image.js<?cs if:type == 3 ?>&pftype=<?cs var:opftype ?><?cs if:opftype == 200 ?>&key=<?cs var:resuin ?><?cs else ?>&uin=<?cs var:resuin ?><?cs /if ?><?cs /if ?>&url=<?cs call:url_escape(image.imageurl_b) ?><?cs /if ?>&imgindex=<?cs var:index ?><?cs /def ?>

<?cs def:imageViewer(preview_url, origin_url, width) ?>
	<div class="img_thumb">
		<a onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs var:origin_url ?>', '<?cs var:origin_url ?>', <?cs var:width?>);return false;" href="javascript:;">
			<img alt="点击查看大图" title="点击放大" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(160,150,'<?cs var:origin_url ?>');" />
		</a>
	</div>
<?cs /def ?>

<?cs def:prefixedUserLink(prefix, userName, href, link, className) ?>
	<?cs if:string.length(prefix) > 0 ?>
		<a href="<?cs var:href ?>" class="q_namecard c_tx <?cs var:className?>" target="_blank"  link="<?cs var:link ?>"><?cs var:prefix ?><?cs var:userName ?></a>
	<?cs else ?>
		<a class="q_namecard q_des <?cs var:className?> c_tx" href="<?cs var:href ?>" link="<?cs var:link ?>" target="_blank"><?cs var:userName ?></a>
	<?cs /if ?>
<?cs /def ?>

<?cs def:textUserLink(uin, name, who, className, prefix) ?>
	<?cs if:uin == qz_metadata.t1_uin ?>
		<?cs call:prefixedUserLink(prefix, qz_metadata.t1_name, 'http://user.qzone.qq.com/' + qz_metadata.t1_uin, 'nameCard_' + qz_metadata.t1_uin + ' des_' + qz_metadata.t1_uin, className) ?>
	<?cs else?>		
		<?cs if:who == 1 ?>
			<?cs call:prefixedUserLink(prefix, name, 'http://user.qzone.qq.com/' + uin +'/share', 'nameCard_' + uin + ' des_' + uin, className) ?>
		<?cs elif:who == 2?>
			<?cs call:prefixedUserLink(prefix, name, 'http://share.pengyou.qq.com/index.php?mod=usershare&act=guest&u=' + uin, '', className) ?>
		<?cs elif:who == 3 ?>
			<?cs call:prefixedUserLink(prefix, name, 'http://rc.qzone.qq.com/myhome/weibo/profile1/' + uin, '', className) ?>
		<?cs /if?>
	<?cs /if?>	
<?cs /def ?>

<?cs def:newRichContent(content) ?>
	<?cs def:richContent-items(item) ?>
		<?cs if:item.type=='nick' ?>
				<?cs call:textUserLink(item.uin, item.name, item.who,"comment_nickname", "@") ?>			
		<?cs else ?>
			<?cs var:item ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if: content.con.0 || subcount(content.con.0) > 0 ?>
		<?cs loop:x=0,subcount(content.con)-1,1?>
			<?cs call:richContent-items(content.con[x]) ?>
		<?cs /loop?>
	<?cs elif:content.con ?>
		<?cs call:richContent-items(content.con) ?>
	<?cs else?>
		<?cs var:content?>
	<?cs /if?>
<?cs /def ?>


<?cs def:commandPanel()?>
<?cs if:type==1 || type==2 || type==4 || type==12 || type==13 || type==14|| type==15?>
<?cs elif:type==3 ?>
	<?cs if:string.length(albumname)!=0 ?><p>所属相册：<a href="<?cs var:albumurl ?>" class="c_tx" target="_blank"><?cs call:html_escape(albumname) ?></a></p><?cs /if ?>
<?cs elif:type==10 ?>
	<?cs if:picnum ?><p>价格：<?cs var:picnum ?>元</p><?cs /if ?>
<?cs /if ?>
<p class="img_amount c_tx3">
		<?cs if: type>0&&type<4 ?>
			<?cs if: fromtype==200 ?>
				<?cs if: string.length(resnick)>0?>来自<a class="c_tx" target="_blank" href="http://xiaoyou.qq.com/index.php?mod=profile&u=<?cs var: resuin?>"><?cs var: resnick?></a>&nbsp;<?cs /if?>
			<?cs else?>
				<?cs if: string.length(resnick)>0?>来自<a class="c_tx q_namecard comment_nickname" link="nameCard_<?cs var: resuin?> des_<?cs var:resuin?>" target="_blank" href="http://user.qzone.qq.com/<?cs var: resuin?>"><?cs var: resnick?></a>&nbsp;<?cs /if?>
			<?cs /if?>
		<?cs elif:type==4 ?><?cs #外站链接?>
			<?cs if: string.length(fromsource)>0?>来自<a class="c_tx" target="_blank" href="<?cs var:url ?>" title="<?cs var:url ?>"><?cs var: fromsource?></a>&nbsp;<?cs /if?>
		<?cs elif:type==15 ?><?cs #url support interface?>
			<?cs if: string.length(fromsource)>0?>来自<a class="c_tx" target="_blank" href="<?cs var:url ?>" title="<?cs var:url ?>"><?cs var: fromsource?></a>&nbsp;<?cs /if?>
		<?cs elif:type==17 ?>
			<?cs if: string.length(resnick)>0?>来自<a class="c_tx" target="_blank" href="<?cs var:url ?>"><?cs var: resnick?></a>&nbsp;<?cs /if?>
		<?cs else ?>  
			来自<a href="<?cs var:url ?>" target="_blank" class="c_tx3" title="<?cs var:url ?>"><?cs call:html_escape(bbssource) ?></a>&nbsp;
		<?cs /if?>
	<?cs if:type<3 ?><?cs if:picnum>0 ?>  &#60;共<?cs var:picnum ?>张图片&#62; <?cs /if ?><?cs /if ?>
	<span class="qz_orgin_cnt" cnttype="share">空间<?cs var:feedstypename?>(<?cs var:sharecount?>)</span>
</p>
<p class="same_operate __qzdev_sameuser c_tx3 none"><span class="__qzdev_sameuser_span"></span><?cs var:feedstypename?>过</p>
<?cs /def ?>


<?cs def:replyView(item)?>
<div class="feeds_comm_list bg2">
	<div class="feeds_comment_cont">
		<p class="feeds_comment_text">
			<a href="<?cs var:item.replynickurl ?>" class="q_namecard q_des comment_nickname c_tx" <?cs if:200==item.replytype ?><?cs else ?>link="nameCard_<?cs var:item.replyuin ?> des_<?cs var:item.replyuin ?>"<?cs /if ?> target="_blank"><?cs call:html_escape(item.replynick) ?></a>
			<?cs if:subcount(item.replycon_at)>0?>
				<?cs call:newRichContent(item.replycon_at)?>
			<?cs else?>
				<?cs call:html_escape(item.replycon)?>
			<?cs /if?>
		</p>
		<p class="feeds_comment_op">
			<span class="feeds_time c_tx3"><?cs var:item.replytime ?></span>
		</p>
	</div>
</div>
<?cs /def?>



<?cs def:commentView(item)?>
<div class="feeds_comm_list bg2" >
	<div class="feeds_comment_cont">
		<p class="feeds_comment_text">		
		<a href="<?cs var:item.commnickurl ?>" class="q_namecard q_des comment_nickname c_tx" <?cs if:200==item.commtype ?><?cs else ?>link="nameCard_<?cs var:item.commuin ?> des_<?cs var:item.commuin ?>"<?cs /if ?> target="_blank"><?cs call:html_escape(item.commnick) ?></a><?cs if:subcount(item.commcon_at)>0?><?cs call:newRichContent(item.commcon_at) ?><?cs else?><?cs call:html_escape(item.commcon)?><?cs /if?></p>
		<p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:item.commtime ?></span></p>
	</div>
	
	<?cs if:subcount(item.replys.0)>0?>
		<?cs if:item.replycount > 3 ?>
			<div class="more_feeds_comment bg2"><a class="c_tx" href="http://user.qzone.qq.com/<?cs var:shareuin ?>/share/<?cs var:shareid ?>" " target="_blank">查看全部<?cs var:item.replycount ?>条回复&gt;&gt;</a></div>
		<?cs /if ?>
		<div class="comment_reply_list">
		<?cs each:rep = item.replys?>
			<?cs call:replyView(rep)?>
		<?cs /each?>
		</div>
	<?cs elif:subcount(item.replys)>0?>
		<div class="comment_reply_list">
		<?cs call:replyView(item.replys)?>
		</div>
	<?cs /if?>
	
</div>
<?cs /def?>

<?cs def:commentPanel()?>
<?cs if:1!=delcommpart ?>
	<div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div>
		<?cs if:commtotal>3||(noreply==1&&commtotal>0)?><div class="more_feeds_comment bg2"><a href="http://user.qzone.qq.com/<?cs var:shareuin ?>/share/<?cs var:shareid ?>" class="c_tx" target="_blank">查看全部<?cs var:commtotal ?>条评论&#62;&#62;</a></div><?cs /if ?>
		<?cs if:noreply != "1" ?>
			<?cs if:subcount(qz_metadata.comments.0)>0?>
				<?cs each:com = qz_metadata.comments?>
					<?cs call:commentView(com)?>
				<?cs /each?>
			<?cs elif:subcount(qz_metadata.comments)>0?>
				<?cs call:commentView(qz_metadata.comments)?>
			<?cs /if?>
		<?cs /if?>
		<qz:reply action="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareaddcomment" param="<?cs var:uin ?>''<?cs var:itemid?>''-1''0''<?cs call:url_escape(title)?>''<?cs var:titletype?>" tuin="<?cs var:uin?>" type="text" config="0|1|0|0|0|0|0" version="6" charset="UTF-8">回复</qz:reply>
	</div>
<?cs /if?>
<?cs /def ?>


<?cs #:广播显示 ?>
<?cs def:richSummary(cons) ?>
	<?cs def:richContent-item(item) ?>
		<?cs if:item.type == 'wbnick' ?>
			<a href="http://rc.qzone.qq.com/myhome/weibo/profile/<?cs var:item.wbaccount ?>/" target="_blank" class="c_tx" title="<?cs var:item.wbnick ?>(@<?cs var:item.wbaccount ?>)"><?cs var:item.wbnick ?></a>
		<?cs elif:item.type == 'nick' ?>
			<?cs call:textUserLink(item.uin, item.name, item.who,"comment_nickname", "") ?>
		<?cs elif:item.type == 'topic' ?>
			<a href="http://rc.qzone.qq.com/myhome/weibo/topic/<?cs var:item.topic ?>/" target="_blank" class="c_tx">#<?cs var:item.topic ?>#</a>
		<?cs elif:item.type == 'url' ?>
			<a href="<?cs var:item.url ?>?type=1&from=19&f=2&s=" target="_blank" class="c_tx"><?cs var:item.url ?></a>
		<?cs else ?>
			<?cs var:item ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 ?>
		<?cs loop:x=0,subcount(cons.con)-1,1?>
			<?cs call:richContent-item(cons.con[x]) ?>
		<?cs /loop?>
	<?cs elif:string.length(cons.con) > 0 || subcount(cons.con) > 0 ?>
		<?cs call:richContent-item(cons.con) ?>
	<?cs elif:string.length(cons) > 0 ?>
		<?cs var:cons ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:微博 ?>
<?cs def:SubType_17() ?>
<div class="bor2 mood_rt_frame bgr3">
	<p class="mood_rt_text">
		<span class="c_tx1">
			<?cs if:subcount(qz_metadata.summary_wb)>0?><?cs call:richSummary(qz_metadata.summary_wb) ?><?cs else?><?cs var:qz_metadata.summary ?><?cs /if?>
		</span>
	</p>
	<?cs if:string.find(videourl,".swf")!=-1?>
		<?cs if:imagenum > 1?><?cs call:imageViewer(images[0].imageurl, images[0].imageurl_b, 374)?><?cs /if ?>
		<div class="imgbox video_img"><qz:popup params="" title="<?cs call:html_escape(title) ?>" src="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareget_onedetail?uin=<?cs var:shareuin ?>&itemid=<?cs var:shareid ?>&cginame=popup&spaceuin=0" width="375" height="334" version="3"><span><span class="play_op"></span><img src="<?cs if:2==imagenum ?><?cs var:images.1.imageurl ?><?cs elif:1==imagenum ?><?cs var:images.0.imageurl ?><?cs else ?>http://imgcache.qq.com/ac/qzone_v5/app/qzshare/video.jpg<?cs /if ?>" class="bor3" style="width:120px;height:90px;" /></span></qz:popup></div>
	<?cs elif:imagenum > 0?>
		<?cs call:imageViewer(images[0].imageurl, images[0].imageurl_b, 374)?>
	<?cs /if?>
	<?cs call:commandPanel() ?>
</div>
<?cs /def ?>

<?cs #:音乐 ?>
<?cs def:SubType_18() ?>
<div class="bor2 mood_rt_frame bgr3">
	<strong>
		<p class="mood_rt_text">
			<span class="c_tx1">
				<qz:popup param="{shareuin:'<?cs var:shareuin?>',action:3,songid:<?cs var:resid?>,flashid:'flash_<?cs var:itemid?>',playbtn:'controls_<?cs var:itemid?>',redarea:'player_<?cs var:itemid?>' <?cs if:typename=='高品质音乐'?>,quality:1<?cs /if?>}"
		version="4" src="/music/qzone/music_ic.js" charset="utf-8" type="Music" title="<?cs call:html_escape(title) ?>"
	><?cs call:html_escape(title) ?></qz:popup>
			</span>
		</p>
	</strong>
	<div class="tool_song" id="controls_<?cs var:itemid?>">
		<qz:popup param="{shareuin:'<?cs var:shareuin?>',action:3,songid:<?cs var:resid?>,flashid:'flash_<?cs var:itemid?>',playbtn:'controls_<?cs var:itemid?>',redarea:'player_<?cs var:itemid?>' <?cs if:typename=='高品质音乐'?>,quality:1<?cs /if?>}"
		version="4" src="/music/qzone/music_ic.js" charset="utf-8" width="375" height="169" type="Music"
		class="bt_play bor2 bg" title="试听歌曲">
			<b class="bt_con bor2 bg"><b class="bt_txt c_tx">◆</b></b>
		</qz:popup>
		<span class="song_time c_tx3">0:00/<?cs var:musictime ?></span>
	</div>
	<div id="player_<?cs var:itemid?>" style="display:none;">
		<qz:popup param="{shareuin:'<?cs var:shareuin?>',action:4,songid:<?cs var:resid?>,flashid:'flash_<?cs var:itemid?>',playbtn:'controls_<?cs var:itemid?>',redarea:'player_<?cs var:itemid?>' <?cs if:typename=='高品质音乐'?>,quality:1<?cs /if?>}" 
		src="/music/qzone/music_ic.js" version="4" charset="utf-8" width="375" height="169" type="Music">收起</qz:popup>
		<div id="flash_<?cs var:itemid?>"></div>
	</div>
	<?cs call:commandPanel() ?>
</div>
<?cs /def ?>

<?cs #:日志的入口?>
<?cs def:SubType_1()?>
<div class="bor2 mood_rt_frame bgr3">
	<strong>
		<p class="mood_rt_text">
			<span class="c_tx1">
					<a href="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_blogdetail?blogurl=<?cs call:url_escape(url)?>&shareuin=<?cs var:shareuin?>&itemid=<?cs var:shareid?>&spaceuin=<?cs var:uin?>&cginame=&isfriend=1" class="c_tx" target="_blank"><?cs call:html_escape(title) ?></a>
			</span>
		</p>
	</strong>
	<?cs if:string.length(summary)!=0 ?>
		<p class="mood_rt_text">
			<span class="c_tx1">
				<?cs call:html_escape(summary) ?>
			</span>
		</p>
	<?cs /if ?>
		<?cs if:imagenum>0 ?><div class="imgbox"><?cs set::imageIndex=0 ?><?cs each:item=images ?><qz:popup param="<?cs var:type ?>|<?cs var:imageIndex?>|<?cs var:shareuin ?>|<?cs var:shareid ?>|<?cs var:resid ?>|<?cs var:opftype ?>|<?cs var:resuin ?>|<?cs call:url_escape(item.imageurl_b) ?>" src="/qzone/photo/zone/icenter_popup.html" version="2"><img src="/ac/b.gif" class="bor3" onload="QZFL.media.adjustImageSize(100,100,&quot;<?cs var:item.imageurl ?>&quot;);"/></qz:popup><?cs set:imageIndex=imageIndex+1 ?><?cs /each ?></div><?cs /if ?>
<?cs call:commandPanel() ?>
</div>
<?cs /def ?>

<?cs #:相册入口 type==2?>
<?cs def:SubType_2()?>
<div class="bor2 mood_rt_frame bgr3">
		<strong>
		<p class="mood_rt_text">
			<span class="c_tx1">
					<a href="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_blogdetail?blogurl=<?cs call:url_escape(url)?>&shareuin=<?cs var:shareuin?>&itemid=<?cs var:shareid?>&spaceuin=<?cs var:uin?>&cginame=&isfriend=1" class="c_tx" target="_blank"><?cs call:html_escape(title) ?></a>
			</span>
		</p>
	</strong>
	<?cs if:string.length(summary)!=0 ?>
		<p class="mood_rt_text">
			<span class="c_tx1">
				<?cs call:html_escape(summary) ?>
			</span>
		</p>
	<?cs /if ?>
		<?cs if:imagenum>0 ?><div class="imgbox"><?cs set::imageIndex=0 ?><?cs each:item=images ?><qz:popup param="<?cs var:type ?>|<?cs var:imageIndex?>|<?cs var:shareuin ?>|<?cs var:shareid ?>|<?cs var:resid ?>|<?cs var:opftype ?>|<?cs var:resuin ?>|<?cs call:url_escape(item.imageurl_b) ?>" src="/qzone/photo/zone/icenter_popup.html" version="2"><img src="/ac/b.gif" class="bor3" onload="QZFL.media.adjustImageSize(100,100,&quot;<?cs var:item.imageurl ?>&quot;);"/></qz:popup><?cs set:imageIndex=imageIndex+1 ?><?cs /each ?></div><?cs /if ?>
	<?cs call:commandPanel() ?>
</div>
<?cs /def ?>

<?cs #:早期feed的直接改造，根据分享子类型的变化慢慢迁移到新入口 ?>
<?cs def:oldEntry()?>
<div class="bor2 mood_rt_frame bgr3">
	<strong>
		<p class="mood_rt_text">
			<span class="c_tx1">
				<?cs if: type==2 || type==3 || type==10 || type==12 ?>
					<a href="<?cs var:url?>" class="c_tx" target="_blank"><?cs call:html_escape(title) ?></a>
				<?cs elif:type==4 || type==13 || type==15?>
					<a href="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?uin=<?cs var:uin ?>&shareid=<?cs var:itemid ?>" class="c_tx" target="_blank"><?cs call:html_escape(title) ?></a>
				<?cs elif:type==5 ?>
					<a href="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs call:url_escape(url)?>&shareid=<?cs var:itemid ?>" class="c_tx" target="_blank"><?cs call:html_escape(title) ?></a>
				<?cs else?>
					<a href="http://user.qzone.qq.com/<?cs var:uin ?>/share/<?cs var:itemid ?>" class="c_tx" target="_blank"><?cs call:html_escape(title) ?></a>
				<?cs /if?>
			</span>
		</p>
	</strong>
	<?cs if:string.length(summary)!=0 ?>
		<p class="mood_rt_text">
			<span class="c_tx1">
				<?cs call:html_escape(summary) ?>
			</span>
		</p>
	<?cs /if ?>
	<?cs if:5==type ?>
		<div class="imgbox video_img"><qz:popup params="" title="<?cs call:html_escape(title) ?>" src="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareget_onedetail?uin=<?cs var:shareuin ?>&itemid=<?cs var:shareid ?>&cginame=popup&spaceuin=0" width="375" height="334" version="3"><span><span class="play_op"></span><img src="<?cs if:1==imagenum ?><?cs var:images.0.imageurl ?><?cs else ?>http://imgcache.qq.com/ac/qzone_v5/app/qzshare/video.jpg<?cs /if ?>" class="bor3" style="width:120px;height:90px;" /></span></qz:popup></div>
	<?cs elif:11==type&&1==imagenum ?>
		<?cs each:item=images ?><div class="imgbox"><a href="http://user.qzone.qq.com/<?cs var:shareuin ?>/share/<?cs var:shareid ?>" class="c_tx" target="_blank"><img src="/ac/b.gif" class="bor3" onload="QZFL.media.adjustImageSize(100,100,&quot;<?cs var:item.imageurl ?>&quot;);"/></a></div><?cs /each ?>
	<?cs else ?>
		<?cs if:imagenum>0 ?><div class="imgbox"><?cs set::imageIndex=0 ?><?cs each:item=images ?><qz:popup param="<?cs var:type ?>|<?cs var:imageIndex?>|<?cs var:shareuin ?>|<?cs var:shareid ?>|<?cs var:resid ?>|<?cs var:opftype ?>|<?cs var:resuin ?>|<?cs call:url_escape(item.imageurl_b) ?>" src="/qzone/photo/zone/icenter_popup.html" version="2"><img src="/ac/b.gif" class="bor3" onload="QZFL.media.adjustImageSize(100,100,&quot;<?cs var:item.imageurl ?>&quot;);"/></qz:popup><?cs set:imageIndex=imageIndex+1 ?><?cs /each ?></div><?cs /if ?>
	<?cs /if ?>
<?cs call:commandPanel() ?>
</div>
<?cs /def ?>

<?cs #:全局入口?>
<div class="feeds_tp_3 music_player_feed">
	<?cs if:type==1?>
		<?cs call:SubType_1() ?>
	<?cs elif:type==2?>
		<?cs call:SubType_2() ?>
	<?cs elif:type == "17" ?>
		<?cs call:SubType_17() ?>
	<?cs elif:type == "18" ?>
		<?cs call:SubType_18() ?>
	<?cs else ?>
		<?cs call:oldEntry() ?>
	<?cs /if ?>
	<div class="feeds_tp_operate"><qz:popup params="" title="分享" src="http://imgcache.qq.com/qzone/app/qzshare/popup.html#uin=<?cs var:shareuin ?>&itemid=<?cs var:shareid ?>" width="432" height="210">分享</qz:popup> <a href="http://rc.qzone.qq.com/myhome/share/recs/feed" target="_blank" class="c_tx">今日热门推荐</a><?cs if:type == 5 ?> <a href="javascript:;" onclick="QZONE.FP.toApp('/myhome/367/?ADTAG=INNER.QZONE.CENTER');return false;" class="c_tx">更多精彩视频</a><?cs /if ?></div>
	<?cs call:commentPanel() ?>
</div>

<?cs def:getKey(key) ?><?cs set:qz_count = 12 - string.length(key) ?><?cs loop:i=0,qz_count-1,1 ?>0<?cs /loop ?><?cs var:key ?><?cs /def ?>
<qz:data key1="<?cs call:getKey(shareuin) ?><?cs call:getKey(shareid) ?>" key2="<?cs var:qz_metadata.url ?>" />
