<?
cs def:level1TitleView() ?><?
	cs if:string.length(qz_metadata.rt_uin) > 0 ?><span class="c_tx3">转发：</span><?
		cs if:subcount(qz_metadata.last_fwd_at_con) > 0 || string.length(qz_metadata.last_fwd_cmt) > 0 || qz_metadata.rtlist.0 || subcount(qz_metadata.rtlist.0) > 0 || qz_metadata.rtlist || subcount(qz_metadata.rtlist) > 0
			?><strong class="quotes_symbols_left c_tx3">“</strong><?
		cs /if ?><?
		cs if:subcount(qz_metadata.last_fwd_at_con) > 0 ?><?
			cs call:richContent(qz_metadata.last_fwd_at_con) ?><?
		cs elif:string.length(qz_metadata.last_fwd_cmt) > 0 ?><?
			cs var:qz_metadata.last_fwd_cmt_con ?><?
		cs /if ?><?
		cs call:richRetweetList() ?><?
		cs if:subcount(qz_metadata.last_fwd_at_con) > 0 || string.length(qz_metadata.last_fwd_cmt) > 0 || qz_metadata.rtlist.0 || subcount(qz_metadata.rtlist.0) > 0 || qz_metadata.rtlist || subcount(qz_metadata.rtlist) > 0
			?><strong class="quotes_symbols_right c_tx3">”</strong><?
		cs /if ?><?
		cs if:string.length(qz_metadata.t1_source_url) == 0 ?>
			<span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span><?
		cs else ?>
			<a class="ifeeds_origin c_tx3" href="<?cs var:qz_metadata.t1_source_url ?>" target="_blank">通过<?cs var:qz_metadata.t1_source_name ?></a><?
		cs /if ?><?
	cs else ?><?
		cs if:qz_metadata.signin == 1 ?><?
			cs if:subcount(qz_metadata.lbs) > 0 ?>
				在&nbsp<qz:popup height="548" width="558" version="" title="<?cs var:qz_metadata.lbs.idname ?>" src="http://qzs.qq.com/qzone/app/lbs/popup.html#poiid=<?cs var:qz_metadata.lbs.id ?>" param="" ><?cs var:qz_metadata.lbs.idname ?></qz:popup>&nbsp签到：<?
			cs else ?>签到<?cs /if ?><?
		cs elif:subcount(qz_metadata.lbs) > 0 ?>在<a href="http://rc.qzone.qq.com/myhome/qqmeishi?shopid=<?cs var:qz_metadata.lbs.id ?>" target="_blank" class="c_tx geoname"><?cs var:qz_metadata.lbs.idname ?></a>：<?cs /if ?>
		<strong class="quotes_symbols_left c_tx3">“</strong><?
			cs call:richContent(qz_metadata.t1_con) ?><?
			cs call:richRetweetList() ?>
			<?cs if:qz_metadata.richtype == 2 ?> 
				<?cs if:string.length(qz_metadata.url2) > 1 ?>
					<a href='http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs escape:"url"?><?cs var:qz_metadata.url1 ?><?cs /escape?>&appid=311&ugcid=<?cs escape:"url"?><?cs var:qz_metadata.t1_tid ?><?cs /escape?>&where=1' title="原链接：<?cs var:qz_metadata.url2 ?>" class="c_tx" target="_blank"><?cs var:qz_metadata.url1 ?></a>
				<?cs else ?>
					<a href='http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs escape:"url"?><?cs var:qz_metadata.url1 ?><?cs /escape?>&appid=311&ugcid=<?cs escape:"url"?><?cs var:qz_metadata.t1_tid ?><?cs /escape?>&where=1' class="c_tx" target="_blank"><?cs var:qz_metadata.url1 ?></a>
				<?cs /if ?>
			<?cs /if ?>
		<strong class="quotes_symbols_right c_tx3">”</strong>
		<div class="ifeeds_meta"><?
			cs if:string.length(qz_metadata.t1_source_url) == 0 ?>
				<span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span><?
			cs else ?>
				<a class="ifeeds_origin c_tx3" href="<?cs var:qz_metadata.t1_source_url ?>" target="_blank">通过<?cs var:qz_metadata.t1_source_name ?></a><?
			cs /if ?><?
			cs #:新加的转发展示逻辑 ?><?
			cs if:qz_metadata.tweet_fwnum > 0 ?>
				<a class="c_tx3" href="http://rc.qzone.qq.com/myhome/weibo/agg/<?cs var:qz_metadata.tweetid ?>/" target="_blank">微博转播(<?cs var:qz_metadata.tweet_fwnum ?>)</a><?
			cs /if ?>
		</div><?
	cs /if ?><?
cs /def ?>