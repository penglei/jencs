<?cs if: qfv.title.con_more>0 ?>
	<div class="txt_box info_cut">
	<?cs call:title_start()?>
	<?cs #解析每条feed不同的title?>
	<?cs set:_end = subcount(qfv.title.con) - 1?>
	<?cs loop:i = 0, _end, 1?>
		<?cs call:title_item(qfv.title.con[i])?>
	<?cs /loop?>
	</div>
	<div class="txt_box info_complete none">
	</div>
	<div class="txt_box f_toggle">
		<a href="javascript:;" data-cmd="qz_toggle" data-pos="1">
			展开查看全文
		</a>
		<img src="http://qzonestyle.gtimg.cn/qzone_v6/img/feed/loading.gif" class="load_img none">
	</div>
</div><?cs #/*endfor: .f_info*/ ?>
<?cs else ?>
	<?cs call:title() ?>
<?cs /if ?>

<?cs if:qfv.meta.typeid==MOOD_TYPEID_FORWARD ||
		qfv.meta.feedstype==UC_WUP_FEEDSTYPE_PSV ||
		(qfv.meta.feedstype==UC_WUP_FEEDSTYPE_ABT && qz_metadata.feedtype != UC_WUP_FEED_TYPE_COMM_ALSO ) ?>
	<?cs call:summary("borbg","f_ct_txtimg")?>
<?cs elif:qz_metadata.orgdata.mediatype == UC_MEDIA_TYPE_AUDIO && subcount(qz_metadata.orgdata.itemdata) > 0?>
	<?cs #音乐feeds?>
	<?cs call:summary("borbg","")?>
<?cs else ?>
	<?cs call:summary("","")?>
<?cs /if ?>


