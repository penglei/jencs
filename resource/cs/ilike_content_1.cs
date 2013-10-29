<?cs if:qz_metadata.subject.rt_comment && qz_metadata.app.name!="说说" ?><?cs # 赞的说说不再跟转发理由 ?>
<div class="feeds_tp_3">
	<div class="txtbox quote_txt">
		<strong class="quotes_symbols_left c_tx3">“</strong><?cs var:qz_metadata.subject.rt_comment ?><strong class="quotes_symbols_right c_tx3">”</strong>
	</div>
</div>
<?cs /if ?>

<?cs if:qz_metadata.padding.desc ?>
<div class="feeds_tp_3">
	<div class="txtbox quote_txt">
		<strong class="quotes_symbols_left c_tx3">“</strong><?cs var:qz_metadata.padding.desc ?><strong class="quotes_symbols_right c_tx3">”</strong>
	</div>
</div>
<?cs /if ?>

<?cs if:subcount(qz_metadata.padding.photolist)> 0 || subcount(qz_metadata.padding.photolist.photo) > 0 ?>
	
	<?cs if:subcount(qz_metadata.padding.photolist.photo.0)>0 ?><?cs # 相册 ?>
	<div class="feeds_tp_1">
		<div class="imgbox">
			<?cs each:item=qz_metadata.padding.photolist.photo ?>
					<a href="<?cs var:item.url ?>" target="_blank">
						<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:item.surl ?>');"/>
					</a>
			<?cs /each ?>
	</div>

	<?cs elif:subcount(qz_metadata.padding.photolist.photo)>0 ?><?cs # 单张相片 ?>
	<div class="feeds_tp_1">
		<?cs if:qz_metadata.padding.photolist.photo.lurl ?>
			<a href="javascript:;" onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs var:qz_metadata.padding.photolist.photo.lurl ?>', '', 374, 0, 0);return false;" >
		<?cs else ?><?cs # 老数据要兼容 ?>
			<a href="<?cs var:qz_metadata.padding.photolist.photo.url ?>" target="_blank">
		<?cs /if ?>

				<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(160,150,'<?cs var:qz_metadata.padding.photolist.photo.surl ?>');"/>
			</a>
	</div>
	<?cs /if ?>
<?cs /if ?>