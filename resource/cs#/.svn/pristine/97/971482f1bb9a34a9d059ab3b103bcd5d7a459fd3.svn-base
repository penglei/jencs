<?
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
cs /def ?>