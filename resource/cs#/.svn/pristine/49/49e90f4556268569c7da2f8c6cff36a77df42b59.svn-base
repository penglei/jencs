<?
cs #:转发时发给原帖人的被动 Feed ?><?
cs def:forward1TitleView()
	?>转发我的说说：<?
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
	cs if:string.length(qz_metadata.t1_source_url) == 0
		?><span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span><?
	cs else
		?><a class="ifeeds_origin c_tx3" href="<?cs var:qz_metadata.t1_source_url ?>" target="_blank">通过<?cs var:qz_metadata.t1_source_name ?></a><?
	cs /if ?><?
cs /def ?>