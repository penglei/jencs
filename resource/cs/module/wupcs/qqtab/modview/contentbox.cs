<?cs ####:
	/*
	 *生成代替转发链的那种titile
	 *relyMsg转发链中的一个节点
	 */
?>
<?cs def:contentBox_Title(title) ?>
	<?cs if:subcount(title.con.0)>0 ?>
		<div class="txt_box">
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
		<?cs if:subcount(qfv.content.media.pic) ?> f_img<?cs /if?>
		<?cs if:style == "bor"?> f_ct_rt bor2
		<?cs elif:style == "borbg"?> f_ct_rt bor2
		<?cs /if?>"
	>
		<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>
		<?cs call:contentBox_Title(qfv.content.title) ?>
		<?cs set:g_f_ct_x_has_closed = 0?>
<?cs /def?>

<?cs def:contentBox_end()?>
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


<?cs #/*图片展示*/?>
<?cs def:contentBox-image(style, cls)?>
<?cs if:qfv.content != 0?>
	<?cs call:contentBox_start(style, cls)?>
		<?cs call:contentTxt("")?>
		<?cs call:_contentMedia_pictures_count()?>
		<div class="img_box img_box_<?cs var:cntbox_img_count?>">
			<?cs call:contentMedia()?>
		</div>
	<?cs call:contentBox_end()?>
	<?cs if: appid==4 && qfv.content.extendinfo.con.0.text ?>
		<div class="f_detail"><span class="c_tx3"><?cs var:qfv.content.extendinfo.con.0.text?></span></div>
	<?cs elif: cntbox_img_count>=4 ?>
		<div class="f_detail"><span class="c_tx3">共有<?cs var:subcount(qfv.content.media.pic)?>张图片</span></div>
	<?cs /if?>
<?cs /if?>
<?cs /def?>
