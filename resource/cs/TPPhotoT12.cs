<?cs if:qz_metadata.activityid ?>
	<div class="feeds_tp_1">
		<?cs if:subcount(qz_metadata.piclist)>0 ?>
			<div class="imgbox">
				<?cs if:subcount(qz_metadata.piclist.0)>0 ?>
					<?cs each:item=qz_metadata.piclist ?>
						<qz:popup param="<?cs var:qz_metadata.uin ?>$||^<?cs var:qz_metadata.activityid ?>$||^<?cs var:item.piclargeid ?>$||^<?cs var:item.picsmallurl ?>$||^<?cs var:qz_metadata.feedtime ?>" src="/qzone/photo/zone/icenter_popup.html#type=act&timeStamp=<?cs var:qz_metadata.feedtime ?>" version="2" title="<?cs var:qz_metadata.activityname ?>">
							<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:item.picsmallurl ?>');"/>
						</qz:popup>
					<?cs /each ?>
				<?cs elif:subcount(qz_metadata.piclist)>0 ?>
					<qz:popup param="<?cs var:qz_metadata.uin ?>$||^<?cs var:qz_metadata.activityid ?>$||^<?cs var:qz_metadata.piclist.piclargeid ?>$||^<?cs var:qz_metadata.piclist.picsmallurl ?>$||^<?cs var:qz_metadata.feedtime ?>" src="/qzone/photo/zone/icenter_popup.html#type=act&timeStamp=<?cs var:qz_metadata.feedtime ?>" version="2" title="<?cs var:qz_metadata.activityname ?>">
						<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(160,150,'<?cs var:qz_metadata.piclist.picsmallurl ?>');"/>
					</qz:popup>
				<?cs /if ?>
			</div>
		<?cs /if ?>
		<div class="feeds_tp_operate">
			<a href="http://rc.qzone.qq.com/photo/activity/<?cs var:qz_metadata.activityid ?>/" target="_blank;" class="c_tx">参加活动</a>
		</div>
	</div>
<?cs /if ?>
