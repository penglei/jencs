<?cs #{/*
	内空区主要分为media和summary，media展示图片、视频等，而summary展示文本等信息

	summary 的数据路径为qfv.content.cntText 
			支持多段落
			支持插入特殊的内容，如很大的赞按钮
*/}?>

<?cs set:_cnt_summary_path = "content.cntText."?>
<?cs set:data_content_i=0 ?>
<?cs #:
	/**/
	function data_content_i++(){}
?>
<?cs def:data_content_i++() ?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs set:data_content_i=data_content_i+1 ?>
<?cs /def ?>
<?cs ####
	/**
	 *使用richMsg填充文本内容
	 */
?>
<?cs def:data_content_text(richMsgs)?>
	<?cs set:_len = subcount(richMsgs)?>
	<?cs loop:i = 0, _len - 1, 1?>
		<?cs call:data_rich_msg(_cnt_summary_path + "con." + data_content_i, richMsgs[i], "", 0)?>
		<?cs call:data_content_i++() ?>
	<?cs /loop?>
<?cs /def?>

<?cs ####
	/**
	 *在使用richMsg填充文本内容前,设置下作者用户，本方法只能用在data_content开始处
	 */
?>
<?cs def:data_content_bigin_nick(uin,who,name)?>
	<?cs call:data_con_nick(_cnt_summary_path + "con."+data_content_i, uin, who, name, "link", 0)?>
	<?cs call:data_content_i++() ?>
	<?cs call:data_con_txt(_cnt_summary_path + "con."+data_content_i, ":", "normal", 0)?>
	<?cs call:data_content_i++() ?>
<?cs /def?>

<?cs def:data_content_url(url, text) ?>
	<?cs call:data_con_url(_cnt_summary_path + "con."+data_content_i,text, url, "link", 0) ?>
	<?cs call:data_content_i++() ?>
<?cs /def ?>

<?cs def:data_content_txt(text) ?>
	<?cs call:data_con_txt(_cnt_summary_path + "con."+data_content_i, text, "normal", 0) ?>
	<?cs call:data_content_i++() ?>
<?cs /def ?>

<?cs def:data_content_nick(uin, who, name) ?>
	<?cs call:data_con_nick(_cnt_summary_path + "con."+data_content_i, uin, who, name, "link", 0)?>
	<?cs call:data_content_i++() ?>
<?cs /def ?>

<?cs ####
	/**
	 * 段落开始和结束，必须配对使用
	 */
?>
<?cs def:data_content_p_start()?>
	<?cs call:data_con_p_start(_cnt_summary_path + "con." + data_content_i) ?>
	<?cs call:data_content_i++() ?>
<?cs /def?>

<?cs def:data_content_p_end()?>
	<?cs call:data_con_p_end(_cnt_summary_path + "con." + data_content_i) ?>
	<?cs call:data_content_i++() ?>
<?cs /def?>


<?cs set:textTitle_i=0 ?>
<?cs set:_cnt_summary_title_path = "content.cntText.title.con."?>
<?cs def:textTitle_i++() ?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs set:textTitle_i = textTitle_i+1 ?>
<?cs /def ?>

<?cs #:
	/**/
	function data_textTitle_url(text, url, mr){}
?>
<?cs def:data_textTitle_url(text, url) ?>
	<?cs call:data_con_url(_cnt_summary_title_path+textTitle_i,text, url, "link", 0) ?>
	<?cs call:textTitle_i++() ?>
<?cs /def ?>

<?cs #:
	/**/
	function data_textTitle_tipTxt(arguments){}
?>
<?cs def:data_textTitle_tipTxt(text) ?>
	<?cs call:data_con_txt(_cnt_summary_title_path+textTitle_i, text, "tip", 0) ?>
	<?cs call:textTitle_i++() ?>
<?cs /def ?>

<?cs #:
	/**/
	function data_textTitle_defaultTxt(text){}
?>
<?cs def:data_textTitle_defaultTxt(text) ?>
	<?cs call:data_con_txt(_cnt_summary_title_path+textTitle_i, text, "normal", 0) ?>
	<?cs call:textTitle_i++() ?>
<?cs /def ?>

<?cs #:
	/**/
	function data_textTitle_nick(uin, who, name){}
?>
<?cs def:data_textTitle_nick(uin, who, name) ?>
	<?cs call:data_con_nick(_cnt_summary_title_path+textTitle_i, uin, who, name, "link", 0)?>
	<?cs call:textTitle_i++() ?>
<?cs /def ?>

<?cs #:
	/**/
	function data_textTitle_rich(richmsgs){}
?>
<?cs def:data_textTitle_rich(richmsgs) ?>
	<?cs set:data_textTitle_rich_loop_count = subcount(richmsgs) ?>
	<?cs if:data_textTitle_rich_loop_count ?>
	<?cs loop:i=0, data_textTitle_rich_loop_count - 1, 1 ?>
		<?cs call:data_rich_msg(_cnt_summary_title_path+textTitle_i, richmsgs[i], 0, 0) ?>
		<?cs call:textTitle_i++() ?>
	<?cs /loop ?>
	<?cs /if?>
<?cs /def ?>

<?cs #:
	/**/
	function data_textTitle(arguments){}
?>
<?cs def:data_textTitle(title) ?>
	<?cs set:data_textTitle_count = subcount(title) ?>
	<?cs loop:i=0, data_textTitle_count - 1, 1 ?>
		<?cs call:data_rich_msg(_cnt_summary_title_path+textTitle_i, title[i], title[i].color, title[i].mr) ?>
		<?cs call:textTitle_i++() ?>
	<?cs /loop ?>
<?cs /def ?>
