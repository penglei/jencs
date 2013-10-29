<?cs ####
	/**
	 *生成contentbox，支持样式
	 *1、简单边框
	 *2、边框加背景
	 */
?>
<?cs def:v8_contentBox_start(style, cls)?>
	<div class="f-ct
		<?cs if:style == "bor_bg_np"?> f-ct-b-bg-np
		<?cs elif:style == "borbg"?> f-ct-b-bg
		<?cs /if?>"
	>
		<?cs #f-ct-imgtxt 左图右文 || 文?>
		<?cs #f-ct-imgtxt f-ct-txtimg 上图下文?>
		<?cs #因此，f-ct-imgtxt是一直存在的?>

		<div class="f-ct-txtimg 
			<?cs var:cls?>
			<?cs if:(qfv.content.media.imgMode == G_IMG_GRID_MODE && subcount(qfv.content.media.pic) > 1) ||
					qfv.content.media.imgMode == G_IMG_GRID_MODE_SMALL?>
					 img-box-row-wrap
			<?cs /if?>"
		>
<?cs /def?>

<?cs def:v8_contentBox_end()?>
		</div><?cs #/*必须先闭合 .f-ct-imgtxt*/?>
	</div>
<?cs /def?>

<?cs #/*一个默认的contentbox，包含文字和图片展示窗口*/?>
<?cs def:v8_contentBox(style, cls)?>
	<?cs if:qfv.content != 0?>
		<?cs call:v8_contentBox_start(style, cls)?>
			<?cs if:qfv.content.layoutMode == G_LAYOUT_LEFTIMG?><?cs #左图右文?>
				<?cs call:v8_contentMedia()?>
				<?cs call:v8_contentTxt("")?>
			<?cs else ?><?cs #普通模式，都是上文下图?>
				<?cs call:v8_contentTxt("")?>
				<?cs call:v8_contentMedia()?>
			<?cs /if?>
		<?cs call:v8_contentBox_end()?>
	<?cs /if?>
<?cs /def?>
