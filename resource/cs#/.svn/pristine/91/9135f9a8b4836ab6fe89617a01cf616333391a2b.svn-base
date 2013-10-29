<?
cs #:转发时发给上一个转发人的被动 Feed ?><?
cs def:forward2TitleView()
	?>转发我的说说：<?cs #:r
	?><a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.pre_fwd.uin ?>/mood/<?cs var:qz_metadata.pre_fwd.tid ?>.<?cs var:qz_metadata.pre_fwd.source ?>" class="c_tx"><?
		cs var:qz_metadata.pre_fwd.con ?><?
		cs call:richRetweetList()
	?></a><?
	cs if:string.length(qz_metadata.t1_source_url) == 0 ?>
		<span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span><?
	cs else?>
		<a class="ifeeds_origin c_tx3" href="<?cs var:qz_metadata.t1_source_url ?>" target="_blank">通过<?cs var:qz_metadata.t1_source_name ?></a><?
	cs /if?><?
cs /def ?>