<?cs ####
	/*日志内容区*/
?>

<?cs def:data_blog_contentbox()?>
	<?cs call:get_tuin_and_tid()?>

	<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
		<?cs if:qz_metadata.orgdata.mediatype == UC_MEDIA_TYPE_VEDIO ?>
			<?cs call:data_content_init(G_LAYOUT_LEFTIMG , G_IMG_SMALL_MODE , "") ?>
		<?cs else ?>
			<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND || qz_metadata.scope == SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT , G_IMG_GRID_MODE , "") ?>
			<?cs else ?>
				<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_SMALL_MODE , "") ?>
			<?cs /if ?>
		<?cs /if?>
	<?cs else ?>
		<?cs if:qz_metadata.orgdata.mediatype == UC_MEDIA_TYPE_VEDIO ?>
			<?cs call:data_content_init(G_LAYOUT_LEFTIMG , G_IMG_SMALL_MODE , "") ?>
		<?cs else ?>
			<?cs call:data_content_init(G_LAYOUT_DEFAULT, G_IMG_GRID_MODE , "") ?>
		<?cs /if?>
	<?cs /if?>

	<?cs set:_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
	<?cs call:data_extendinfo_picnum(qz_metadata.orgdata.itemcount)?>

	<?cs call:data_content_text(qz_metadata.orgdata.content) ?>
	<?cs if:qz_metadata.orgdata.mediatype == UC_MEDIA_TYPE_VEDIO && subcount(qz_metadata.orgdata.itemdata) > 0 ?>

		<?cs call:get_blog_url(qz_metadata.orgdata.uin, qz_metadata.orgdata.mkey)?>
		<?cs call:data_cntmedia_video_urlaction(0, qz_metadata.orgdata.itemdata[0], get_blog_url.ret, "", "")?>
	<?cs elif:qz_metadata.orgdata.mediatype == UC_MEDIA_TYPE_PIC && subcount(qz_metadata.orgdata.itemdata) > 0 ?>
		<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
			<?cs set:pic_idx = 0 ?>
			<?cs if:(qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV)
					&& string.length(qz_metadata.opinfo.t2body.extendinfo.pic_index) > 0 ?>
				<?cs set:pic_idx = qz_metadata.opinfo.t2body.extendinfo.pic_index ?>
				<?cs if:pic_idx > _end ?>
					<?cs set:pic_idx = 0 ?>
				<?cs /if?>
				<?cs if:pic_idx < 0 ?>
					<?cs set:pic_idx = 0 ?>
				<?cs /if ?>
			<?cs /if?>
			<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
			<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[pic_idx], get_blog_url.ret, "", "") ?>
		<?cs else ?>
			<?cs call:get_blog_url(qz_metadata.orgdata.uin, qz_metadata.orgdata.mkey)?>
			<?cs loop:j=0, _end, 1?>
				<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
				<?cs call:data_cntmedia_pic_urlaction(j, qz_metadata.orgdata.itemdata[j], get_blog_url.ret, "", "") ?>
			<?cs /loop?>
		<?cs /if?>
	<?cs /if?>

	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_RELATEPSV ?>
		<?cs if:qz_metadata.orgdata.action != UC_API_ACTION_MODIFY ?>
			<?cs if:subcount(qz_metadata.relybody) > 0 ?><?cs #转载日志?>
				<?cs if:g_isV8?>
					<?cs call:data_init_cntTitle() ?>
					<?cs call:data_cntTitle_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
					<?cs call:data_cntTitle_tipTxt("发表" + get_blog_type.ret)?>
					<?cs call:data_cntTitle_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret)?>
				<?cs /if?>
				<?cs set:g_blog_type = BLOG_FWD?>
			<?cs else ?><?cs #发表新日志?>
				<?cs set:g_blog_type = BLOG_NEW?>
			<?cs /if?>
		<?cs /if?>
	<?cs /if?>
	<?cs if:qz_metadata.orgdata.extendinfo.con_more > 0?>
		<?cs set:qfv.extendinfo.con_more=1 ?>
	<?cs /if?>
<?cs /def?>
