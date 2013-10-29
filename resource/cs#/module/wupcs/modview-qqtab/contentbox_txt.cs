<?cs def:contentTxt_start(cls)?>
	<div class="<?cs alt:cls?>txt_box<?cs /alt?><?cs if: qfv.content.con_more>0 ?> info_cut<?cs /if ?>">
<?cs /def?>

<?cs def:contentTxt_end()?>
	</div>
<?cs /def?>

<?cs #这里可能得加个title ?>
<?cs def:content_genTitle(title) ?>
	<?cs if: subcount(title) ?>
		<p>
		<?cs call:conCommon(title)?>
		</p>
	<?cs /if ?>
<?cs /def?>

<?cs def:contentMoreTxtEntry()?>
	<?cs if: qfv.content.con_more > 0 ?>
		<div class="txt_box info_complete none"></div>
		<div class="txt_box f_toggle">
			<a href="javascript:;" data-cmd="qz_toggle" data-complete="0" data-pos="2">
				展开查看全文
			</a>
			<img src="http://qzonestyle.gtimg.cn/qzone_v6/img/feed/loading.gif" class="load_img none">
		</div>
	<?cs /if ?>
<?cs /def?>

<?cs def:contentTxt(cls)?>
	<?cs #!必须判断两个条件?>
	<?cs if:subcount(qfv.content.cntText.con) ||
			subcount(qfv.content.cntText.title.con)?>
	<?cs #qfv.content.cntText.con是txt_box数据的容器?>
	<?cs #qfv.content.cntText.title.con是内容标题的容器?>
		<?cs call:contentTxt_start(cls)?>
		<?cs call:content_genTitle(qfv.content.cntText.title.con)?>

		<?cs call:conCommon(qfv.content.cntText.con)?>
		<?cs call:contentTxt_end()?>
	<?cs /if?>

	<?cs #TODO 这个逻辑只有说说用到，应该移到说说去?>
	<?cs call:contentMoreTxtEntry()?>
<?cs /def?>
