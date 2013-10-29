<?
cs #:at 的被动 Feed（被评论前） ?><?
cs def:at1TitleView() ?>
	在：<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>" class="c_tx"><?
		cs call:clearContent(qz_metadata.t1_con) ?><?
		cs call:richRetweetList() ?>
	</a> 中提到我 <?
	cs if:string.length(qz_metadata.t1_source_url) == 0 ?>
		<span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span><?
	cs else?>
		<a class="ifeeds_origin c_tx3" href="<?cs var:qz_metadata.t1_source_url ?>" target="_blank">通过<?cs var:qz_metadata.t1_source_name ?></a><?
	cs /if?><?
cs /def ?>