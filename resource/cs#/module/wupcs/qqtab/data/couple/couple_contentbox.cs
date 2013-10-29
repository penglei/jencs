
<?cs def:data_couple_contentbox()?>
	<?cs call:i()?>

	<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_SMALL_MODE , "") ?>	
	<?cs else ?>
		<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_GRID_MODE , "") ?>
	<?cs /if?>

	<?cs if:qz_metadata.orgdata.subtype == COUPLE_srctype_mood ?>
		<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV
			|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY || qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM
			|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT_NOTIFYPSV ?>

			<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ACT_NOTIFYPSV ?>
				<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, "")?>
			<?cs /if?>
			<?cs call:data_textTitle_rich(qz_metadata.orgdata.content) ?>
			<?cs call:data_textTitle_url(qz_metadata.orgdata.srcurl, qz_metadata.orgdata.srcurl) ?>
		<?cs /if?>
	<?cs else ?>
		<?cs call:data_textTitle_rich(qz_metadata.orgdata.albumdata.sAlbumName) ?>
	<?cs /if?>

	<?cs if:qz_metadata.orgdata.mediatype == UC_MEDIA_TYPE_PIC && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
		<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
			<?cs if:subcount(qz_metadata.orgdata.itemdata) >0 ?>
				<?cs set:popup_param = COUPLE_SPACE_OPENID + "|1|" + qz_metadata.orgdata.albumdata.sAlbumId + "|" 
					+ qz_metadata.orgdata.itemdata[0].itemid + "|" + qz_metadata.orgdata.uin + "|0|"
					+ qz_metadata.orgdata.itemdata[0].itemdata[0].url + "|" + qz_metadata.orgdata.extendinfo.pairkey + "|"
					+ qz_metadata.orgdata.extendinfo.picstring + "|" + qz_metadata.orgdata.extendinfo.pickey ?>

				<?cs call:data_cntmedia_pic_popup(0, qz_metadata.orgdata.itemdata[0], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
			<?cs /if?>
		<?cs else ?>
			<?cs set:_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
			<?cs loop:j=0, _end, 1?>
				<?cs set:popup_param = COUPLE_SPACE_OPENID + "|1|" + qz_metadata.orgdata.albumdata.sAlbumId + "|" 
					+ qz_metadata.orgdata.itemdata[j].itemid + "|" + qz_metadata.orgdata.uin + "|0|"
					+ qz_metadata.orgdata.itemdata[j].itemdata[0].url + "|" + qz_metadata.orgdata.extendinfo.pairkey + "|"
					+ qz_metadata.orgdata.extendinfo.picstring + "|" + qz_metadata.orgdata.extendinfo.pickey ?>

				<?cs call:data_cntmedia_pic_popup(j, qz_metadata.orgdata.itemdata[j], popup_param, "/qzone/photo/zone/icenter_popup.html", "") ?>
			<?cs /loop?>
		<?cs /if?>
	<?cs /if?>

<?cs /def?>
