<?cs set:BLOG_NEW = 1?>
<?cs set:BLOG_FWD = 2?>

<?cs ####
	/**
	 * 获取日志类型
	 */
?>

<?cs set:SCOPE_FRIENDSHIP_ME_TO_FRIEND=1?>  <?cs #/*friendship场景标识:我对好友的操作*/#?>
<?cs set:SCOPE_FRIENDSHIP_FRIEND_TO_ME=2?>	<?cs #/*friendship场景标识:好友对我的操作*/#?>

<?cs def:get_blog_type()?>
	<?cs if:bitmap_value_ex(qz_metadata.orgdata.extendinfo.iEffectExt,1,1)?>
		<?cs set:get_blog_type.ret = "模版日志"?>
	<?cs elif:bitmap_value_ex(qz_metadata.orgdata.extendinfo.iEffect1,24,1)?>
		<?cs set:get_blog_type.ret = "信纸日志"?>
	<?cs elif:bitmap_value_ex(qz_metadata.orgdata.extendinfo.iEffect1,27,1)?>
		<?cs set:get_blog_type.ret = "QQ秀泡泡日志"?>
	<?cs elif:bitmap_value_ex(qz_metadata.orgdata.extendinfo.iEffect2,21,1)?>
		<?cs set:get_blog_type.ret = "魔方日志"?>
	<?cs else ?>
		<?cs set:get_blog_type.ret = "日志"?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *根据uin和blogid生成跳转地址
	 */
?>
<?cs def:get_blog_url(uin, blogid)?>
	<?cs set:_url = "http://user.qzone.qq.com/" + uin + "/blog/" + blogid ?>
	<?cs set:get_blog_url.ret = _url?>
<?cs /def?>

<?cs ####
	/**
	 *生成评论参数
	 */
?>
<?cs def:_blog_psv_comments_param()?>

	<?cs call:get_tuin_and_tid()?>
	<?cs set:_param = get_tuin_and_tid.uin + "," 
				+ get_tuin_and_tid.tid + "," 
				+ qz_metadata.feedtype ?>

	<?cs set:_blog_psv_comments_param.ret = _param?>
<?cs /def?>

<?cs ####
	/**
	 *生成回复参数
	 *也会在回复被动feeds中用到
	 *@param {vt2body} t2body 当前评论的索引
	 */
?>
<?cs def:_blog_psv_commentReply_param(t2body)?>

	<?cs call:get_tuin_and_tid()?>
	<?cs set:_param = get_tuin_and_tid.uin + "," 
				+ get_tuin_and_tid.tid + "," 
				+ t2body.seq 
				+ ", -1, " 
				+ qz_metadata.feedtype ?>

	<?cs set:_blog_psv_commentReply_param.ret = _param?>
<?cs /def?>
