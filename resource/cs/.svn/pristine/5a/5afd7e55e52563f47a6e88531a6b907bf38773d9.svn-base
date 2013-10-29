<?cs call:title()?>

<?cs def:follow_contentBox_start()?>
	<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_ACT ?>
		<div class="f_ct f_ct_2 bor3 bg3 f_ct_webpage">
		<?cs else ?>
		<div class="f_ct f_ct_2 bor3 bg3 f_ct_webpage_passive">
	<?cs /if ?>
	<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>
	<?cs call:contentBox_Title(qfv.content.title) ?>
	<div class="f_ct_imgtxt">
	<?cs set:g_f_ct_x_has_closed = 0?>
<?cs /def?>

<?cs def:follow_contentBox_start_forText()?>
	<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_ACT ?>
		<div class="f_ct f_ct_2 bor3 bg3">
		<?cs else ?>
		<div class="f_ct f_ct_2 bor3 bg3">
	<?cs /if ?>
	<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>
	<?cs call:contentBox_Title(qfv.content.title) ?>
	<div class="f_ct_imgtxt">
	<?cs set:g_f_ct_x_has_closed = 0?>
<?cs /def?>

<?cs def:follow_contentBox_end()?>
		</div><?cs #必须先闭合 .f_ct_imgtxt?>
	</div>
<?cs /def?>

<?cs if:qfv.content.layoutMode == G_LAYOUT_LEFTIMG_V8 ?><?cs # V8版的左图右文?>
	<?cs call:summary_start()?>
		<?cs call:quote()?>
		<?cs if:qfv.content.media.imgMode == G_IMG_NOIMG ?>
				<?cs call:follow_contentBox_start_forText()?>
					<?cs call:contentTxt_start("")?>
					<?cs call:content_genTitle(qfv.content.cntText.title.con)?>
					<?cs call:conCommon(qfv.content.cntText.con)?>
					<?cs call:contentTxt_end()?>
			<?cs else ?>
				<?cs call:follow_contentBox_start()?>
					<?cs call:contentMedia()?>
					<?cs call:contentTxt_start("")?>
					<?cs call:content_genTitle(qfv.content.cntText.title.con)?>
					<?cs call:conCommon(qfv.content.cntText.con)?>
					<?cs call:contentTxt_end()?>
		<?cs /if ?>

		<?cs call:follow_contentBox_end()?>
		<?cs call:operate()?>
		<?cs call:comments-like()?>
	<?cs call:summary_end()?>
	
	<?cs else ?>
		<?cs call:summary("borbg","")?>
<?cs /if ?>
