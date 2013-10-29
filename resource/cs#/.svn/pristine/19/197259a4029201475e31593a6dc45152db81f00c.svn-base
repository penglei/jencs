<?
cs #:这里写的有点怪异，没有闭合a元素，只负责生成a元素的前面一半 ?><?
cs def:moodDetailsLink(uin,tid,source) ?><?
	cs if:string.length(source) > 0 ?>
		<a target="_blank" href="http://user.qzone.qq.com/<?cs var:uin ?>/mood/<?cs var:tid ?>.<?cs var:source ?>" class="c_tx"><?
	cs else ?>
		<a target="_blank" href="http://user.qzone.qq.com/<?cs var:uin ?>/mood/<?cs var:tid ?>" class="c_tx"><?
	cs /if ?><?
cs /def ?><?
cs def:prefixedUserName(prefix, uin, name) ?><?
	cs if:string.length(name) > 0 ?><?
		cs var: prefix + name ?><?
	cs else ?><?
		cs var: prefix + uin ?><?
	cs /if ?><?
cs /def ?><?

cs def:textUserLink(uin, name, s_uin, s_name, className, prefix) ?><?
	cs if:string.length(s_uin) > 30
		?><a class="q_namecard q_des <?cs var:className?> c_tx" href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:s_uin ?>" target="_blank"><?cs call:prefixedUserName(prefix, s_uin, s_name) ?></a><?
	cs elif:string.length(uin) > 30
		?><a class="q_namecard q_des <?cs var:className?> c_tx" href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:uin ?>" target="_blank"><?cs call:prefixedUserName(prefix, uin, name) ?></a><?
	cs else
		?><a class="q_namecard q_des <?cs var:className?> c_tx" href="http://user.qzone.qq.com/<?cs var:uin ?>" link="nameCard_<?cs var:uin ?> des_<?cs var:uin ?>" target="_blank"><?cs call:prefixedUserName(prefix, uin, name) ?></a><?
	cs /if ?><?
cs /def ?><?

cs def:textUserLinkNew(uin, name, s_uin, s_name, className, prefix) ?><?
	cs if:string.length(s_uin) > 30
		?><span class="q_namecard q_des <?cs var:className?> c_tx" href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:s_uin ?>" target="_blank"><?cs call:prefixedUserName(prefix, s_uin, s_name) ?></span><?
	cs elif:string.length(uin) > 30
		?><span class="q_namecard q_des <?cs var:className?> c_tx" href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:uin ?>" target="_blank"><?cs call:prefixedUserName(prefix, uin, name) ?></span><?
	cs else
		?><span class="q_namecard q_des <?cs var:className?> c_tx" href="http://user.qzone.qq.com/<?cs var:uin ?>" link="nameCard_<?cs var:uin ?> des_<?cs var:uin ?>" target="_blank"><?cs call:prefixedUserName(prefix, uin, name) ?></span><?
	cs /if ?><?
cs /def ?><?

cs def:clearUserLink(uin, name, s_uin, s_name, prefix) ?><?
	cs if:string.length(s_uin) > 0 ?><?
		cs call:prefixedUserName(prefix, s_uin, s_name) ?><?
	cs else?><?
		cs call:prefixedUserName(prefix, uin, name) ?><?
	cs /if?><?
cs /def ?><?

cs def:richContent(cons) ?><?
	cs def:richContent-items(item) ?><?
		cs if:item.type == 'nick' ?><?
			cs if:string.length(item.uin) > 30?><?
				cs call:textUserLink("", "", item.uin, item.name, "comment_nickname", '@') ?><?
			cs else?><?
				cs call:textUserLink(item.uin, item.name, "", "", "comment_nickname", '@') ?><?
			cs /if?><?
		cs elif:item.type == 'url' ?><?
			cs if:item.ourl
				?><a href='http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs escape:"url"?><?cs var:item.ourl ?><?cs /escape?>&appid=311&ugcid=<?cs escape:"url"?><?cs var:qz_metadata.t1_tid ?><?cs /escape?>&where=1' title="原链接：<?cs var:item.ourl ?>" class="c_tx" target="_blank"><?cs var:item.url ?></a><?
			cs else
				?><a href='http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?url=<?cs escape:"url"?><?cs var:item.url ?><?cs /escape?>&appid=311&ugcid=<?cs escape:"url"?><?cs var:qz_metadata.t1_tid ?><?cs /escape?>&where=1' class="c_tx" target="_blank"><?cs var:item.url ?></a><?
			cs /if ?><?
		cs else ?><?
			cs var:item ?><?
		cs /if ?><?
	cs /def ?><?
	cs if:cons.con.0 || subcount(cons.con.0) > 0 || string.length(cons.con.0) > 0 ?><?
		cs loop:i = 0, subcount(cons.con) - 1, 1 ?><?
			cs call:richContent-items(cons.con[i]) ?><?
		cs /loop ?><?
	cs elif:cons.con || subcount(cons.con) > 0 || string.length(cons.con) > 0 ?><?
		cs call:richContent-items(cons.con) ?><?
	cs /if ?><?
cs /def ?><?

cs def:clearContent(cons) ?><?
	cs def:clearContent-items(item) ?><?
		cs if:item.type == 'nick' ?><?
			cs if:string.length(item.uin) > 30?><?
				cs call:textUserLinkNew("", "", item.uin, item.name, "comment_nickname", '@') ?><?
			cs else?><?
				cs call:textUserLinkNew(item.uin, item.name, "", "", "comment_nickname", '@') ?><?
			cs /if?><?
		cs elif:item.type == 'url' ?><?
			cs var:item.url ?><?
		cs else ?><?
			cs var:item ?><?
		cs /if ?><?
	cs /def ?><?
	cs if:cons.con.0 || subcount(cons.con.0) > 0 || string.length(cons.con.0) > 0 ?><?
		cs loop:i = 0, subcount(cons.con) - 1, 1 ?><?
			cs call:clearContent-items(cons.con[i]) ?><?
		cs /loop ?><?
	cs elif:cons.con || subcount(cons.con) > 0 || string.length(cons.con) > 0 ?><?
		cs call:clearContent-items(cons.con) ?><?
	cs /if ?><?
cs /def ?><?

cs def:rtlist() ?><?
	cs def:rtlist-items(item, isFirst) ?> || <?
		cs if:isFirst || item.uin == qz_metadata.t1_uin ?><?
			cs call:clearUserLink(item.uin, item.name, item.uin, item.name, '@') ?><?
		cs else ?><?
			cs call:clearUserLink(item.uin, item.name, item.wc_uin, item.wc_nick, '@') ?><?
		cs /if ?> <?
		cs if:subcount(item.at_con) > 0 ?><?
			cs call:clearContent(item.at_con) ?><?
		cs else ?><?
			cs var:item.con ?><?
		cs /if ?><?
	cs /def ?><?
	cs if:qz_metadata.rtlist.0 || subcount(qz_metadata.rtlist.0) > 0 ?><?
		cs call:rtlist-items(qz_metadata.rtlist[subcount(qz_metadata.rtlist) - 1], 1) ?><?
		cs loop:i = subcount(qz_metadata.rtlist) - 2, 0, '-1' ?><?
			cs call:rtlist-items(qz_metadata.rtlist[i], 0) ?><?
		cs /loop ?><?
	cs elif:qz_metadata.rtlist || subcount(qz_metadata.rtlist) > 0 ?><?
		cs call:rtlist-items(qz_metadata.rtlist, 1) ?><?
	cs /if ?><?
cs /def ?><?

cs def:richRetweetList() ?><?
	cs def:richRetweetList-items(item, isFirst) ?> || <?
		cs if:isFirst || item.uin == qz_metadata.t1_uin ?><?
			cs call:textUserLink(item.uin, item.name, item.uin, item.name, '', '@') ?><?
		cs else ?><?
			cs call:textUserLink(item.uin, item.name, item.wc_uin, item.wc_nick, '', '@') ?><?
		cs /if ?> <?
		cs if:subcount(item.at_con) > 0 ?><?
			cs call:richContent(item.at_con) ?><?
		cs else ?><?
			cs var:item.con ?><?
		cs /if ?><?
	cs /def ?><?
	cs if:qz_metadata.rtlist.0 || subcount(qz_metadata.rtlist.0) > 0 ?><?
		cs call:richRetweetList-items(qz_metadata.rtlist[subcount(qz_metadata.rtlist) - 1], 1) ?><?
		cs loop:i = subcount(qz_metadata.rtlist) - 2, 0, '-1' ?><?
			cs call:richRetweetList-items(qz_metadata.rtlist[i], 0) ?><?
		cs /loop ?><?
	cs elif:qz_metadata.rtlist || subcount(qz_metadata.rtlist) > 0 ?><?
		cs call:richRetweetList-items(qz_metadata.rtlist, 1) ?><?
	cs /if ?><?
cs /def ?><?cs def:level2TitleView() ?>
<?cs if:string.length(qz_metadata.rt_uin) > 0 ?>
	<?cs if:string.length(qz_metadata.last_fwd_cmt) > 0 ?>
	评论我：<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>" class="c_tx"><?
		cs var:qz_metadata.last_fwd_cmt ?><?
		cs call:richRetweetList() ?>
	</a> 
		<?cs if:string.length(qz_metadata.t1_source_url) == 0 ?>
			<span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span>
		<?cs else?>
			<a class="ifeeds_origin c_tx3" href="<?cs var:qz_metadata.t1_source_url ?>" target="_blank">通过<?cs var:qz_metadata.t1_source_name ?></a>
		<?cs /if?>
	<?cs else?>
	评论我：
	<?cs /if?>
<?cs else?>
	评论我：<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>" class="c_tx"><?
		cs call:clearContent(qz_metadata.t1_con) ?><?
		cs call:richRetweetList() ?>
	</a> 
	<?cs if:string.length(qz_metadata.t1_source_url) == 0 ?>
		<span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span>
	<?cs else?>
		<a class="ifeeds_origin c_tx3" href="<?cs var:qz_metadata.t1_source_url ?>" target="_blank">通过<?cs var:qz_metadata.t1_source_name ?></a>
	<?cs /if?>
<?cs /if?>
<?cs /def ?><?
cs def:level3TitleView() ?><?
	cs if:string.length(qz_metadata.rt_uin) > 0 ?><?
		cs if:string.length(qz_metadata.last_fwd_cmt) > 0 ?>
			在<?
			cs if:qz_metadata.beuin != qz_metadata.t1_uin && qz_metadata.operuin != qz_metadata.t1_uin && (qz_metadata.t1_uin != qz_metadata.t2.rep.uin || qz_metadata.t1_uin != qz_metadata.rep[subcount(qz_medata.rep) -1].uin) ?><?
				cs call:textUserLink(qz_metadata.t1_uin, qz_metadata.t1_name , "", "", "comment_nickname", '') ?>的<?
			cs /if ?>：<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>" class="c_tx"><?
			cs var:qz_metadata.last_fwd_cmt ?><?cs
			cs call:richRetweetList() ?></a>
			回复我<?
			cs if:string.length(qz_metadata.t1_source_url) == 0 ?>
				<span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span><?
			cs else ?>
				<a class="ifeeds_origin c_tx3" href="<?cs var:qz_metadata.t1_source_url ?>" target="_blank">通过<?cs var:qz_metadata.t1_source_name ?></a><?
			cs /if ?><?
		cs else ?><?
			cs if:qz_metadata.beuin != qz_metadata.t1_uin && qz_metadata.operuin != qz_metadata.t1_uin && (qz_metadata.t1_uin != qz_metadata.t2.rep.uin || qz_metadata.t1_uin != qz_metadata.rep[subcount(qz_medata.rep) -1].uin) ?>
				在<?cs call:textUserLink(qz_metadata.t1_uin, qz_metadata.t1_name , "", "", "comment_nickname", '') ?>的说说<?
			cs /if?>回复我：<?
		cs /if ?><?
	cs else ?>
		在<?
		cs if:qz_metadata.beuin != qz_metadata.t1_uin && qz_metadata.operuin != qz_metadata.t1_uin && (qz_metadata.t1_uin != qz_metadata.t2.rep.uin || qz_metadata.t1_uin != qz_metadata.rep[subcount(qz_medata.rep) -1].uin) ?><?
			cs call:textUserLink(qz_metadata.t1_uin, qz_metadata.t1_name , "", "", "comment_nickname", '') ?>的<?
		cs /if ?>：<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>" class="c_tx"><?
		cs call:clearContent(qz_metadata.t1_con) ?><?cs
		cs call:richRetweetList() ?></a>
		回复我<?
		cs if:string.length(qz_metadata.t1_source_url) == 0 ?>
			<span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span><?
		cs else ?>
			<a class="ifeeds_origin c_tx3" href="<?cs var:qz_metadata.t1_source_url ?>" target="_blank">通过<?cs var:qz_metadata.t1_source_name ?></a><?
		cs /if?><?
	cs /if ?><?
cs /def ?><?
cs #:被评论回复过的主帖被动 Feed ?><?
cs if:string.length(qz_metadata.dotype) > 0 ?><?
	cs if:qz_metadata.dotype == 55702 ?><?
		cs call:level2TitleView() ?><?
	cs elif:qz_metadata.dotype == 55802 ?><?
		cs call:level3TitleView() ?><?
	cs /if ?><?
cs /if ?>