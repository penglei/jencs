<?cs ####
	/**
	 *生成评论参数
	 */
?>
<?cs def:profile_comment_param()?>
	<?cs set:_param = qz_metadata.meta.appid + "''"
				+ qz_metadata.orgdata.uin + "''" 
				+ qz_metadata.orgdata.topicid + "''" 
				+ "-1" + "''" 
				+ "0" + "''"
				+ qz_metadata.feedtype + "''"
				+ "0" + "''"
				+ qz_metadata.orgdata.subtype?>
	<?cs set:profile_comment_param.ret = _param?>
<?cs /def?>
		

<?cs ####
	/**
	 *生成回复参数
	 *也会在回复被动feeds中用到
	 *@param {vt2body} t2body 当前评论的索引
	 */
?>
<?cs def:profile_commentReply_param(t2body)?>
	<?cs set:_param = qz_metadata.meta.appid + "''"
				+ qz_metadata.orgdata.uin + "''" 
				+ qz_metadata.orgdata.topicid + "''" 
				+ t2body.seq + "''" 
				+ t2body.uin + "''"
				+ qz_metadata.feedtype + "''"
				+ "0" + "''"
				+ qz_metadata.orgdata.subtype?>
	<?cs set:profile_commentReply_param.ret = _param?>
<?cs /def?>


<?cs ####
	/**
	 *生成更多评论参数
	 */
?>
<?cs def:profile_comment_more_param()?>
	<?cs set:_param = qz_metadata.meta.appid + "''"
				+ qz_metadata.orgdata.uin + "''" 
				+ qz_metadata.orgdata.topicid + "''" 
				+ "-1" + "''" 
				+ "0" + "''"
				+ qz_metadata.feedtype + "''"
				+ "1" + "''"
				+ qz_metadata.orgdata.subtype?>

	<?cs set:profile_comment_more_param.ret = _param?>
	
<?cs /def?>