<?cs ####
/*
---------------------------
xxx
<span>xxx<span>
<span class="c_tx3">xxx<span> #灰色文字
<a class="c_tx">xxx</a> #链接色文字
<a class="q_namecard c_tx" link="nameCard_78133089">xxx</a> #昵称
<a class="q_namecard c_tx" link="nameCard_78133089">@xxx</a> #带@格式的
---------------------------
*/
?>

<?cs def:v8_get_con_style(con)?>
	<?cs set:_margin_class = ""?>
	<?cs set:_color_class = ""?>
	<?cs if:con.mr>0?><?cs #/*如果mr为0就表示没有mr*/ ?>
		<?cs set:_margin_class = "ui-mr" + con.mr?>
	<?cs /if?>
	<?cs if:con.mr<0?><?cs #/*如果mr为0就表示没有mr*/ ?>
		<?cs set:con.ml=abs(con.mr) ?>
		<?cs set:_margin_class = "ui-ml" + con.ml?>
	<?cs /if?>
	<?cs if:con.color == "tip" ?>		<?cs #/*灰色的弱内容提示*/?>
		<?cs set:_color_class = "state" ?>
	<?cs elif:con.color == "link"?>		<?cs #/*跟主题的链接颜色*/?>
		<?cs set:_color_class = "c_tx"?>
	<?cs elif:con.color == "normal"?>		<?cs #/*跟普通字体的颜色*/?>
		<?cs set:_color_class = "f-name"?>
	<?cs /if?>
	<?cs set:v8_get_con_style.ret.margin_class = _margin_class?>
	<?cs set:v8_get_con_style.ret.color_class = _color_class?>
<?cs /def?>

<?cs ####
	/**
	 *v8被动标题不同，只是样式的修改
	 */
?>
<?cs def:v8_get_con_style_psvTitle(con)?>
	<?cs set:_margin_class = ""?>
	<?cs set:_color_class = ""?>
	<?cs if:con.mr>0?><?cs #/*如果mr为0就表示没有mr*/ ?>
		<?cs set:_margin_class = "ui-mr" + con.mr?>
	<?cs /if?>
	<?cs if:con.mr<0?><?cs #/*如果mr为0就表示没有mr*/ ?>
		<?cs set:con.ml=abs(con.mr) ?>
		<?cs set:_margin_class = "ui-ml" + con.ml?>
	<?cs /if?>
	<?cs if:con.color == "tip" ?>		<?cs #/*灰色的弱内容提示*/?>
		<?cs #set:_color_class = "c_tx3" ?>
		<?cs set:_color_class = "f-style-block" ?>
	<?cs elif:con.color == "link"?>		<?cs #/*跟主题的链接颜色*/?>
		<?cs set:_color_class = "c_tx"?>
	<?cs /if?>
	<?cs set:v8_get_con_style_psvTitle.ret.margin_class = _margin_class?>
	<?cs set:v8_get_con_style_psvTitle.ret.color_class = _color_class?>
<?cs /def?>

<?cs ####
	/**
	 *输出一个内联样式
	 */
?>
<?cs def:v8_echoStyle(con)?>
	<?cs if:con.cssText?>
		 style="<?cs var:con.cssText?>"
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *输了节点上的class
	 */
?>
<?cs def:v8_echoClass(obj)?>
	<?cs if:obj.className?>
		 class="<?cs var:obj.className?>"
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *输出标准样式class及自定义样式(className字段)
	 */
?>
<?cs def:v8_echoTextClass(con)?>
	<?cs call:v8_get_con_style(con)?>
	<?cs if:v8_get_con_style.ret.margin_class != "" ||
			v8_get_con_style.ret.color_class != "" ||
			con.className?>
		 class="
		 	<?cs if:con.hotclickPath ?>
		 	qz_ichotclick 
		 	<?cs /if?>
			<?cs if:con.className?>
			<?cs var:con.className?> 
			<?cs /if?>

			<?cs if:v8_get_con_style.ret.margin_class?>
			<?cs var:v8_get_con_style.ret.margin_class?> 
			<?cs /if?>

			<?cs if:v8_get_con_style.ret.color_class?>
			<?cs var:v8_get_con_style.ret.color_class?>
			<?cs /if?>
			"
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *v8被动feeds的标题要输出标准样式class及自定义样式(className字段)
	 */
?>
<?cs def:v8_echoTextClass_psvTitle(con)?>
	<?cs call:v8_get_con_style_psvTitle(con)?>
	<?cs if:v8_get_con_style_psvTitle.ret.margin_class != "" ||
			v8_get_con_style_psvTitle.ret.color_class != "" ||
			con.className?>
		 class="
		 	<?cs if:con.hotclickPath ?>
		 	qz_ichotclick 
		 	<?cs /if?>
			<?cs if:con.className?>
			<?cs var:con.className?> 
			<?cs /if?>

			<?cs if:v8_get_con_style_psvTitle.ret.margin_class?>
			<?cs var:v8_get_con_style_psvTitle.ret.margin_class?> 
			<?cs /if?>

			<?cs if:v8_get_con_style_psvTitle.ret.color_class?>
			<?cs var:v8_get_con_style_psvTitle.ret.color_class?>
			<?cs /if?>
			"
	<?cs /if?>
<?cs /def?>
