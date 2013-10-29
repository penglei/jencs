<?cs ####
	/**
	 *根据uin和moodid和source生成跳转地址
	 */
?>
<?cs def:get_mood_url(uin, moodid, source)?>
	<?cs set:_url = "http://user.qzone.qq.com/" + uin + "/mood/" + moodid + "." + source?>
	<?cs set:get_mood_url.ret = _url?>
<?cs /def?>

<?cs ####
	/**
	 *判断是否名博商博认证空间用户
	 */
?>
<?cs def:is_famous_uin(bitmap)?>
	<?cs set:isFU = bitmap_value_ex(bitmap,7,1) ?>	<?cs #/*famous uin*/?>
	<?cs set:isBU = bitmap_value_ex(bitmap,5,1) ?>	<?cs #/*business uin*/?>
	<?cs set:isBraU = bitmap_value_ex(bitmap,52,1) ?>	<?cs #/*brand uin*/?>
	<?cs if: isFU ?>
		<?cs set:is_famous_uin.ret = 1?>
	<?cs elif: isBU ?>
		<?cs set:is_famous_uin.ret = 1?>
	<?cs elif: isBraU?>
		<?cs set:is_famous_uin.ret = 1?>
	<?cs else ?>
		<?cs set:is_famous_uin.ret = 0?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *生成回复参数
	 *也会在回复被动feeds中用到
	 *@param {vt2body} t2body 当前评论的索引
	 */
?>
<?cs def:_mood_comm_param(t2body)?>
	<?cs call:get_tuin_and_tid()?>
	<?cs set:_param =    "t1_source=" + qz_metadata.orgdata.extendinfo.t1_source + "&"
					   + "t1_uin=" + get_tuin_and_tid.uin + "&"
					   + "t1_tid=" + get_tuin_and_tid.tid + "&"
					   + "t2_uin=" + t2body.uin + "&"
					   + "t2_tid=" + t2body.seq + "&"
					   + "subdotype=" + qz_metadata.orgdata.extendinfo.dotype + "&"
					   + "signin=" + qz_metadata.orgdata.extendinfo.signin + "&"
					   + "sceneid=" + qz_metadata.feedtype?>

	<?cs set:_mood_comm_param.ret = _param?>
<?cs /def?>

<?cs ####
	/**
	 *生成回复config
	 */
?>
<?cs def:_mood_comm_config()?>

	<?cs #/*同步微博的设置*/ ?>
	<?cs if:qz_metadata.orgdata.extendinfo.to_tweet != 0 && !qz_metadata.orgdata.multimedia.voice.voice_id?>
		<?cs call:is_famous_uin(qz_metadata.meta.bitmap)?>
		<?cs if:is_famous_uin.ret == 1?>
			<?cs set:com_config = "1|1|1|b41,1,to_tweet,点评到微博|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick"?>
		<?cs else?>
			<?cs set:com_config = "1|1|1|b41,!w_sync,to_tweet,点评到微博|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick"?>
		<?cs /if?>
	<?cs else ?>
		<?cs set:com_config = "1|1|1|0|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick"?>
	<?cs /if?>
	<?cs set:_mood_comm_config.ret = com_config?>

<?cs /def?>


<?cs ####
	/**
	 *是否有content_box
	 */
?>
<?cs def:_mood_is_have_contentbox()?>

	<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV 
		|| qfv.meta.typeid==MOOD_TYPEID_FORWARD
		|| (qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_ABT && qz_metadata.feedtype != UC_WUP_FEED_TYPE_COMM_ALSO)?>
		<?cs set:_mood_is_have_contentbox.ret = 1?>
	<?cs else?>
		<?cs set:_mood_is_have_contentbox.ret = 0?>
	<?cs /if?>

<?cs /def?>
