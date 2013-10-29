<?cs #查看更多(评论或者回复)，放开10条?>
<?cs #:主动feeds放开10*10 被动还是一样 ?>

<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_ACT ?>
	<?cs if:qz_metadata.extendKVinfo.showrefer == 1?>
		<?cs set:g_showrefer_more_comments = 1?>
		<?cs set:G_COMMENTS_MAX = 20 ?>
		<?cs set:G_COMMENTS_REPLIES_MAX = 20?>
	<?cs else ?>
		<?cs set:G_COMMENTS_MAX = 10 ?>
		<?cs set:G_COMMENTS_REPLIES_MAX = 10?>
	<?cs /if?>
	<?cs else ?>
	<?cs if:qz_metadata.extendKVinfo.showrefer == 1?>
		<?cs set:g_showrefer_more_comments = 1?>
		<?cs set:G_COMMENTS_MAX = 10 ?>
		<?cs set:G_COMMENTS_REPLIES_MAX = 10?>
	<?cs else ?>
		<?cs set:G_COMMENTS_MAX = 3 ?>
		<?cs set:G_COMMENTS_REPLIES_MAX = 3?>
	<?cs /if?>
<?cs /if ?>
<?cs set:G_COMMENTS_PARAM_NORMAL_FLAG = 0?><?cs #/*目前是真feeds*/?>

<?cs set:g_data_comments_default_cgi = "http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment"?>
