<?cs ####
	/**
	 *生成评论参数
	 */
?>
<?cs def:vote_comment_param()?>
	<?cs set:_param = qz_metadata.feedtype + "|"
				+ qz_metadata.orgdata.mkey + "|" 
				+ qz_metadata.votedata.join_body_uin + "||" ?>
	<?cs set:vote_comment_param.ret = _param?>
<?cs /def?>


<?cs ####
	/**
	 *生成回复参数
	 *也会在回复被动feeds中用到
	 *@param {vt2body} t2body 当前评论的索引
	 */
?>
<?cs def:vote_commentReply_param(t2body)?>
	<?cs set:_param = qz_metadata.feedtype + "|"
				+ qz_metadata.orgdata.mkey + "|" 
				+ qz_metadata.votedata.join_body_uin + "|" 
				+ t2body.seq?>
	<?cs set:vote_commentReply_param.ret = _param?>
<?cs /def?>




