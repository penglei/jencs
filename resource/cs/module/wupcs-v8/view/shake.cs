<?cs call:v8_title()?>

<?cs def:v8_shake_contentBox_start()?>
	<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_ACT ?>
		<div class="f-ct f-ct-b-bg">
			<?cs else ?>
		<div class="f-ct f-ct-b-bg">
	<?cs /if ?>
	<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>
	<?cs call:v8_contentBox_Title(qfv.content.title) ?>
	<div class="f-ct-imgtxt">
<?cs /def?>

<?cs def:v8_shake_contentBox_end()?>
	</div>
	<?cs #必须先闭合 .f_ct_imgtxt?>
	</div>
<?cs /def?>

<?cs if:qfv.content.layoutMode == G_LAYOUT_LEFTIMG_V8 ?><?cs # V8版的左图右文?>
	<?cs call:v8_summary_start()?>
		<?cs call:v8_quote()?>
		<?cs call:v8_shake_contentBox_start()?>
		<?cs call:v8_contentMedia()?>
		<?cs call:v8_contentTxt_start("")?>
		<?cs call:v8_content_genTitle(qfv.content.cntText.title.con)?>
		<?cs call:v8_conCommon(qfv.content.cntText.con)?>
		<?cs call:v8_contentTxt_end()?>
		<?cs call:v8_shake_contentBox_end()?>
		<div class="f-op-wrap">
			<?cs call:v8_operate()?>
		</div>
	<?cs call:v8_summary_end()?>

<?cs else ?>
	<?cs call:v8_summary("borbg","")?>
<?cs /if ?>
