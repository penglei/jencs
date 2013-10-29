<li class="f_item" id="<?cs call:genFeedId()?>">
	<?cs call:feed_avatar_new()?>
	<div class="f_main">
		<?cs call:title() ?>
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
	</div>
</li>