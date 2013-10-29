<?cs ####
	/**
	 *将数据节点的attrs数组输出为html的节点属性
	 */
?>
<?cs def:v8_con_attrs(con)?>
	<?cs if:subcount(con.attrs)?>
		<?cs each:attr = con.attrs?>
			<?cs var:attr.key?>="<?cs var:html_encode(attr.value, 1)?>" 
		<?cs /each?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * 解析文字
	 * @param {Object} 带有text,color,mr的信息的文字数据对象
	 */
	 function con_txt(con)
?>
<?cs def:v8_con_txt(con)?>
	<?cs if:string.length(con.text)?><?cs #/*如果文字都没有，就不要显示出来了*/?>
	<?cs call:v8_get_con_style(con)?>
	<?cs if:v8_get_con_style.ret.margin_class != "" ||
			v8_get_con_style.ret.color_class != "" ||
			con.className?>
		<span <?cs call:v8_echoTextClass(con)?> <?cs call:v8_echoStyle(con) ?>>
		<?cs var:html_encode(con.text, 1)?>
		</span>
	<?cs else ?>
		<?cs var:html_encode(con.text, 1)?>
	<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs def:v8_con_url_start(con)?>
	<?cs call:v8_get_con_style(con)?>
	<?cs if: con.js==1 ?>
		<a href="<?cs var:con.url ?>" 
	<?cs else ?>
		<?cs call:ugc_url_check(con.url,0)?>
		<a href="<?cs var:html_encode(ugc_url_check.ret,1)?>" 
	<?cs /if ?>

		<?cs if:con.hotclickPath ?>
			hotclickPath="<?cs var:con.hotclickPath ?>" 
		<?cs /if ?>
		<?cs if:!con.notarget?>
			<?cs if:con.target?>
			target="<?cs var:con.target?>" 
			<?cs else?>
			target="_blank" 
			<?cs /if?>
		<?cs /if?>

		<?cs call:v8_con_attrs(con)?>

		<?cs call:v8_echoStyle(con)?>
		<?cs call:v8_echoTextClass(con)?>
	>
<?cs /def?>

<?cs def:v8_con_url_end()?>
	</a>
<?cs /def?>

<?cs ####
	/**
	 * 段落开始和结束，必须配对使用
	 */
?>
<?cs def:v8_con_p_start(con)?>
	<p>
<?cs /def?>

<?cs def:v8_con_p_end(con)?>
	</p>
<?cs /def?>

<?cs ####
	/**
	 * 解析文字组建
	 * @param {Object} 带有text,url,color,mr的信息的链接数据对象
	 */
	 function con_url(con)
?>
<?cs def:v8_con_url(con)?>
	<?cs #/*if:con.text*/?><?cs #/*链接没有文字还是要输出，可能有别的用处*/?>
	<?cs call:v8_con_url_start(con)?>
		<?cs var:html_encode(con.text, 1)?>
	<?cs call:v8_con_url_end()?>
	<?cs #/*/if*/?>
<?cs /def?>

<?cs ####
	/**
	 * 解析用户昵称组建
	 * @param {Object} 带有uin,who,nickname,color,mr的信息的链接数据对象
	 */
	 function con_nick(con)
?>
<?cs def:v8_con_nick(con)?>
	<?cs call:v8_get_con_style(con)?>
	<?cs set:con.cls_color = v8_get_con_style.ret.color_class + " " + v8_get_con_style.ret.margin_class?>
	<?cs call:v8_userLink_comp(con)?>
<?cs /def?>

<?cs def:v8_conCommon_item(con)?>
	<?cs if:con.type == "txt"?>
		<?cs call:v8_con_txt(con)?>
	<?cs elif:con.type == "url"?>
		<?cs call:v8_con_url(con)?>
	<?cs elif:con.type == "nick"?>
		<?cs call:v8_con_nick(con)?>
	<?cs elif:con.type == "p_start"?>
		<?cs call:v8_con_p_start(con)?>
	<?cs elif:con.type == "p_end"?>
		<?cs call:v8_con_p_end(con)?>
	<?cs /if?>
<?cs /def?>

<?cs def:v8_conCommon(cons)?>
	<?cs if:subcount(cons.0) > 0 ?>
		<?cs set:_end = subcount(cons) - 1?>
		<?cs loop:i = 0, _end, 1?>
			<?cs call:v8_conCommon_item(cons[i])?>
		<?cs /loop?>
	<?cs else ?>
		<?cs call:v8_conCommon_item(cons)?>
	<?cs /if?>
<?cs /def?>
