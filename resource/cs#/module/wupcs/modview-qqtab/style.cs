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

<?cs def:get_con_style(con)?>
	<?cs set:_margin_class = ""?>
	<?cs set:_color_class = ""?>
	<?cs if:con.mr>0?><?cs #/*如果mr为0就表示没有mr*/ ?>
		<?cs set:_margin_class = "ui_mr" + con.mr?>
	<?cs /if?>
	<?cs if:con.mr<0?><?cs #/*如果mr为0就表示没有mr*/ ?>
		<?cs set:con.ml=abs(con.mr) ?>
		<?cs set:_margin_class = "ui_ml" + con.ml?>
	<?cs /if?>
	<?cs if:con.color == "tip" ?>		<?cs #/*灰色的弱内容提示*/?>
		<?cs set:_color_class = "c_tx3" ?>
	<?cs elif:con.color == "link"?>		<?cs #/*跟主题的链接颜色*/?>
		<?cs set:_color_class = "c_tx"?>
	<?cs elif:con.color == "normal"?>		<?cs #/*跟普通字体的颜色*/?>
		<?cs set:_color_class = "c_tx2 hover_nd"?><?cs #/*hover_nd 代表没有下划线*/?>
	<?cs /if?>
	<?cs set:get_con_style.ret.margin_class = _margin_class?>
	<?cs set:get_con_style.ret.color_class = _color_class?>
<?cs /def?>

<?cs ####
	/**
	 *输出一个内联样式
	 */
?>
<?cs def:echoStyle(con)?>
	<?cs if:con.cssText?>
		 style="<?cs var:con.cssText?>"
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *输了节点上的class
	 */
?>
<?cs def:echoClass(obj)?>
	<?cs if:obj.className?>
		 class="<?cs var:obj.className?>"
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *输出标准样式class及自定义样式(className字段)
	 */
?>
<?cs def:echoTextClass(con)?>
	<?cs call:get_con_style(con)?>
	<?cs if:get_con_style.ret.margin_class != "" ||
			get_con_style.ret.color_class != "" ||
			con.className?>
		 class="
		 	<?cs if:con.hotclickPath ?>
		 	qz_ichotclick 
		 	<?cs /if?>
			<?cs if:con.className?>
			<?cs var:con.className?> 
			<?cs /if?>

			<?cs if:get_con_style.ret.margin_class?>
			<?cs var:get_con_style.ret.margin_class?> 
			<?cs /if?>

			<?cs if:get_con_style.ret.color_class?>
			<?cs var:get_con_style.ret.color_class?>
			<?cs /if?>
			"
	<?cs /if?>
<?cs /def?>
