<?cs #:
	/**
	 * 魔法表情
	 * @param  {Number} id 表情ID
	 * @param  {String} config 业务传过来的配置
	 * @return {[type]}           [description]
	 */
	function data_attach(id){}
?>
<?cs def:data_tieban(url,tb_id,btn_txt) ?>
    <?cs call:set("content","type","tieban") ?>
    <?cs call:set("content.tieban","url",url) ?>
    <?cs call:set("content.tieban","tb_id",tb_id) ?>
    <?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_SMALL_V8_MODE ,"") ?>
    <?cs call:data_con_p_start(_cnt_summary_path + "con." + data_content_i) ?>
    <?cs call:data_content_i++() ?>
    <?cs call:set(_cnt_summary_path + "con." + data_content_i,"js",1) ?>
    <?cs call:set(_cnt_summary_path + "con." + data_content_i,"notarget",1) ?>
    <?cs call:data_content_url("javascript:;",btn_txt) ?>
    <?cs call:data_con_p_end(_cnt_summary_path + "con." + data_content_i) ?>
<?cs /def ?>
