<?cs ####:
	/*
	 *生成代替转发链的那种titile
	 *relyMsg转发链中的一个节点
	 */
?>
<?cs def:contentBox_Title(title) ?>
	<?cs if:subcount(title.con.0)>0 ?>
		<div class="f_ct_title">
			<?cs call:conCommon(title.con) ?>
		</div>
		<div class="f_ang_t bor2"></div>
	<?cs /if ?>
<?cs /def ?>

<?cs ####
	/**
	 *生成contentbox，支持样式
	 *1、简单边框
	 *2、边框加背景
	 */
?>
<?cs def:contentBox_start(style, cls)?>
	<div class="f_ct
		<?cs if:style == "bor"?> f_ct_1 bor3
		<?cs elif:style == "borbg"?> f_ct_2 bor3 bg3
		<?cs /if?>"
	>
		<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>
		<?cs call:contentBox_Title(qfv.content.title) ?>
		<div class="f_ct_imgtxt <?cs var:cls?>">
		<?cs set:g_f_ct_x_has_closed = 0?>
<?cs /def?>

<?cs def:contentBox_end()?>
		</div><?cs #/*必须先闭合 .f_ct_imgtxt*/?>
	<?cs call:attachInfo(qfv.content.attach) ?>
	<?cs if:!g_extendinfo_exist?>
		<?cs call:extendinfo()?>
	<?cs /if?>
	</div>
<?cs /def?>

<?cs #/*一个默认的contentbox，包含文字和图片展示窗口*/?>
<?cs def:contentBox(style, cls)?>
<?cs if:qfv.content != 0?>
	<?cs call:contentBox_start(style, cls)?>
		<?cs if:qfv.content.layoutMode == G_LAYOUT_LEFTIMG?><?cs #左图右文?>
			<?cs call:contentMedia()?>
			<?cs call:contentTxt("")?>
		<?cs else ?><?cs #普通模式，都是上文下图?>
			<?cs call:contentTxt("")?>
			<?cs call:contentMedia()?>
		<?cs /if?>
	<?cs call:contentBox_end()?>
	<?cs /if?>
<?cs /def?>
