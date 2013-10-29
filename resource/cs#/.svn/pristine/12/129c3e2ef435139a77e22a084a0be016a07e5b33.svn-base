<?
cs #:at 的被动 Feed（被评论后） ?><?
cs def:at2TitleView()
	?>转发了提到我的说说<?cs #:r
	?><strong class="quotes_symbols_left c_tx3">“</strong><?
		cs if:subcount(qz_metadata.last_fwd_at_con) > 0 ?><?
			cs call:richContent(qz_metadata.last_fwd_at_con) ?><?
		cs else ?><?
			cs var:qz_metadata.last_fwd_cmt ?><?
		cs /if
	?><strong class="quotes_symbols_right c_tx3">”</strong><?
	cs if:string.length(qz_metadata.t1_source_url) == 0
		?><span class="ifeeds_origin c_tx3">通过<?cs var:qz_metadata.t1_source_name ?></span><?
	cs else
		?><a class="ifeeds_origin c_tx3" href="<?cs var:qz_metadata.t1_source_url ?>" target="_blank">通过<?cs var:qz_metadata.t1_source_name ?></a><?
	cs /if ?><?
cs /def ?>