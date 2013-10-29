<?cs ####
	/**
	 * v8标题的系统弱文案需要特殊的样式，因此直接在这个文件里实现
	 */
?>
<?cs def:v8_con_txt_title_psv(con)?>
	<?cs if:string.length(con.text)?><?cs #/*如果文字都没有，就不要显示出来了*/?>
	<?cs call:v8_get_con_style_psvTitle(con)?>
	<?cs if:v8_get_con_style_psvTitle.ret.margin_class != "" ||
			v8_get_con_style_psvTitle.ret.color_class != "" ||
			con.className?>
		<span <?cs call:v8_echoTextClass_psvTitle(con)?> <?cs call:v8_echoStyle(con) ?>>
		<?cs var:html_encode(con.text, 1)?>
		</span>
	<?cs else ?>
		<?cs var:html_encode(con.text, 1)?>
	<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs def:v8_title_item(con)?>
	<?cs if:con.action.type == "popup"?>
		<?cs call:v8_con_popup(con)?>
	<?cs else ?>
		<?cs if:con.type == "txt"?>
			<?cs call:v8_con_txt(con)?>
		<?cs elif:con.type == "url"?>
			<?cs call:v8_con_url(con)?>
		<?cs elif:con.type == "nick"?>
			<?cs call:v8_con_nick(con)?>
		<?cs elif:con.type == "quoteright"?>
			<i class="ui_ico quote_before c_tx3">“</i>
		<?cs elif:con.type == "quoteleft"?>
			<i class="ui_ico quote_after c_tx3">”</i>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs ##由于li标签是从外框中移入的，
	而所有的视图层模板都是用title和summary组成,
	所以把feeds开头放在了title宏里面;
	</li>放在了summary_end宏里面，这样view层的修改最小
?>

<?cs def:v8_title_start()?>
	<li class="f-single f-s-s" id="fct_<?cs call:v8_genFeedId() ?>">
		<div class="f-aside">
			<?cs call:v8_frame_avatar()?>
			<?cs call:v8_frame_userinfo()?>
		</div><?cs #f-aside?>

		<div class="f-wrap">
			<div class="f-item f-s-i<?cs call:echo_item_class()?>" 
					id="feed_<?cs call:v8_genFeedId() ?>" 
					<?cs call:v8_feeds_frame_data()?>
				>
				<div class="f-info">
<?cs /def?>

<?cs def:v8_title_end()?>
	</div><?cs #end: .f-info?>
	<?cs #内容结束部分要放在summary_end中?>
<?cs /def?>

<?cs def:v8_title()?>
	<?cs call:v8_title_start()?>
	<?cs #解析每条feed不同的title?>
	<?cs set:_end = subcount(qfv.title.con) - 1?>
	<?cs loop:i = 0, _end, 1?>
		<?cs call:v8_title_item(qfv.title.con[i])?>
	<?cs /loop?>
	<?cs call:v8_title_end()?>
<?cs /def?>
