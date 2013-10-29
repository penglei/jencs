<?cs ####
	/**
	 *生成评论参数
	 */
?>
<?cs def:shangcheng_comment_param()?>
	<?cs set:_param = qz_metadata.meta.appid + "''"
				+ qz_metadata.orgdata.uin + "''" 
				+ qz_metadata.orgdata.topicid + "''-1''0''" 
				+ qz_metadata.feedtype
				+ "''0''"
				+ qz_metadata.orgdata.subtype?>

	<?cs set:shangcheng_comment_param.ret = _param?>
<?cs /def?>


<?cs ####
	/**
	 *生成回复参数
	 *也会在回复被动feeds中用到
	 *@param {vt2body} t2body 当前评论的索引
	 */
?>
<?cs def:shangcheng_commentReply_param(t2body)?>
	<?cs set:_param = qz_metadata.meta.appid + "''"
				+ qz_metadata.orgdata.uin + "''" 
				+ qz_metadata.orgdata.topicid + "''" 
				+ t2body.seq + "''" 
				+ t2body.uin + "''"
				+ qz_metadata.feedtype
				+ "''0''"
				+ qz_metadata.orgdata.subtype?>

	<?cs set:shangcheng_commentReply_param.ret = _param?>
<?cs /def?>

<?cs ####
	/**
	 *生成更多评论参数
	 */
?>
<?cs def:shangcheng_comment_more_param()?>
	<?cs set:_param = qz_metadata.meta.appid + "''"
				+ qz_metadata.orgdata.uin + "''" 
				+ qz_metadata.orgdata.topicid + "''-1''0''" 
				+ qz_metadata.feedtype + "''1''"
				+ qz_metadata.orgdata.subtype?>

	<?cs set:shangcheng_comment_more_param.ret = _param?>
	
<?cs /def?>


<?cs ####
	/**
	 *生成分享按钮参数
	 */
?>

<?cs def:shangcheng_make_share_param()?>
	<?cs set:_dress_url = ""?>
	<?cs if:subcount(qz_metadata.orgdata.itemdata) > 0?>
		<?cs set:_dress_url = qz_metadata.orgdata.itemdata[0].action?>
	<?cs /if?>
	<?cs set:_param = "{cgi:'http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_save',"
					+ "type: 'spacedress',"
					+ "spaceuin:" + qz_metadata.orgdata.uin + ","
					+ "fields:{"
					+ "type:'spacedress',"
					+ "dressname:'" + qz_metadata.orgdata.extendinfo.item_name +"',"
					+ "dresstheme:'" + "装扮商城" +"',"
					+ "dressurl:'" + _dress_url +"',"
					+ "dressthemeurl:'" + qz_metadata.orgdata.extendinfo.topic_url +"',"
					+ "dressimgpre:'" + qz_metadata.orgdata.extendinfo.sPicURLPre +"',"
					+ "dressimgs:'" + qz_metadata.orgdata.extendinfo.sBatPicURL +"',"
					+ "dressimgjumppre:'" + qz_metadata.orgdata.extendinfo.sPicJumpURLPre +"',"
					+ "dressimgjumps:'" + qz_metadata.orgdata.extendinfo.sBatPicJumpURL +"',"
					+ "dressapprovekey:'" + qz_metadata.orgdata.likekey 
					+ "'},cgiType: 'FormSender'}"?>
	<?cs set:shangcheng_make_share_param.ret = uri_encode(_param)?>

<?cs /def?>


<?cs ####
	/**
	 *生成转发按钮参数
	 */
?>
<?cs def:shangcheng_forward_btn(param)?>
	<?cs call:data_opr_forward()?>
	<?cs call:set(_op_forward_path, "param", param)?>
<?cs /def?>
