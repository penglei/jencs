<?cs include:"wupcs-v8/data/app/const.cs"?>
<?cs include:"wupcs-v8/data/app/app_contentbox.cs"?>
<?cs include:"wupcs-v8/data/app/app_title.cs"?>
<?cs include:"wupcs-v8/data/app/app_other.cs"?>

<?cs call:data_oprtime()?>
<?cs call:data_app_title()?>
<?cs call:data_app_contentbox()?>
<?cs call:data_source()?>
<?cs call:data_like()?>
<?cs call:data_opr_prevent()?>
<?cs call:data_opr_more()?>
<?cs call:data_opr_delfeed()?>

<?cs if:qz_metadata.orgdata.subtype == APP_subtype_addapp || qz_metadata.orgdata.subtype == APP_subtype_share 
	|| qz_metadata.orgdata.subtype == APP_subtype_game || qz_metadata.orgdata.subtype == APP_subtype_mobile_cover?>
	<?cs call:data_opr_comment()?>
<?cs /if?>

<?cs call:data_app_operate()?>
<?cs call:data_app_download()?>

<?cs call:data_comment_replies("http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>

<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL ?>

	<?cs call:data_comments("http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>

	<?cs #初始化一条评论?>
	<?cs call:data_opcomment_item("")?>

	<?cs call:_app_psv_commentReply_param(qz_metadata.opinfo.t2body)?>
	<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", _app_psv_commentReply_param.ret)?>

	<?cs each:j = g_data_comments.replies[0].index?>
		<?cs #/*生成评论的回复节点*/?>
		<?cs call:data_commentReply_loop_item(j)?>
	<?cs /each?>
<?cs else ?><?cs #主动?>

	<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV && qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY 
		&& qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ?>
		<?cs call:data_comments_showstranger(0)?><?cs #禁止展示陌生人的评论?>
	<?cs /if?>

	<?cs call:data_comments("http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>

	<?cs each:i = g_data_comments.index?>
		<?cs call:data_comment_loop_item(i)?>
		<?cs call:_app_psv_commentReply_param(qz_metadata.vt2body[i])?>
		<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", _app_psv_commentReply_param.ret)?>

		<?cs each:j = g_data_comments.replies[i].index?>
			<?cs call:data_commentReply_loop_item(j)?>
		<?cs /each?>
	<?cs /each?>
<?cs /if?>

<?cs #{/*评论框*/?>
<?cs if:qz_metadata.orgdata.subtype == APP_subtype_addapp || qz_metadata.orgdata.subtype == APP_subtype_share 
	|| qz_metadata.orgdata.subtype == APP_subtype_game || qz_metadata.orgdata.subtype == APP_subtype_mobile_cover?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV
		|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT  || qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL ?>
		<?cs call:_app_psv_commentReply_param(qz_metadata.opinfo.t2body)?>
		<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", _app_psv_commentReply_param.ret, qz_metadata.meta.userid, 
										"http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>
	<?cs else ?>
		<?cs call:_app_psv_comment_param()?>
		<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", _app_psv_comment_param.ret, qz_metadata.meta.userid, 
			"http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>
	<?cs /if?>
<?cs /if?>
<?cs #}/*end: 评论框*/?>
