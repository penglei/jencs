<?cs include:"wupcs/data/profile/const.cs"?>
<?cs include:"wupcs/data/profile/common.cs"?>
<?cs include:"wupcs/data/profile/profile_title.cs"?>
<?cs include:"wupcs/data/profile/profile_contentbox.cs"?>

<?cs call:data_profile_title()?>
<?cs call:data_profile_contentbox()?>
<?cs call:data_oprtime()?>
<?cs call:data_opr_comment()?>
<?cs call:data_opr_delfeed()?>
<?cs call:data_opr_more()?>

<?cs call:data_comment_replies("http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "UTF-8")?>
<?cs call:data_comments("http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "UTF-8")?>

<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV
		|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL || qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM 
		|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY ?>
	<?cs call:data_opcomment_item("")?>
	<?cs call:profile_commentReply_param(qz_metadata.opinfo.t2body)?>
	<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", profile_commentReply_param.ret)?>
	
	<?cs each:j = g_data_comments.replies[0].index?>
		<?cs #/*生成评论的回复节点*/?>
		<?cs call:data_commentReply_loop_item(j)?>
	<?cs /each?>
<?cs else ?><?cs #主动?>
	<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV && qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY 
		&& qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ?>
		<?cs call:data_comments_showstranger(0)?><?cs #禁止展示陌生人的评论?>
	<?cs /if?>

	<?cs call:data_comments_more("http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment","","")?>
	<?cs call:profile_comment_more_param()?>
	<?cs call:data_comments_more_param(profile_comment_more_param.ret)?>

	<?cs each:i = g_data_comments.index?>
		<?cs call:data_comment_loop_item(i)?>
		<?cs call:profile_commentReply_param(qz_metadata.vt2body[i])?>
		<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", profile_commentReply_param.ret)?>
		
		<?cs each:j = g_data_comments.replies[i].index?>
			<?cs #/*生成评论的回复节点*/?>
			<?cs call:data_commentReply_loop_item(j)?>
		<?cs /each?>
	<?cs /each?>
<?cs /if?>

<?cs #/*评论框*/?>
<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV
	|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY || qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM
	|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL?>
	<?cs call:vote_commentReply_param(qz_metadata.opinfo.t2body)?>
	<?cs set:data_comments_inputbox_v2.param.config="1|1|0|0|0|0|0" ?>
	<?cs set:data_comments_inputbox_v2.param.param=profile_commentReply_param.ret ?>
	<?cs set:data_comments_inputbox_v2.param.charset="UTF-8" ?>
	<?cs set:data_comments_inputbox_v2.param.tuin=qz_metadata.meta.userid ?>
	<?cs set:data_comments_inputbox_v2.param.useReply=1 ?>
	<?cs set:data_comments_inputbox_v2.param.comuin=qz_metadata.opinfo.opuin ?>
	<?cs set:data_comments_inputbox_v2.param.comid=qz_metadata.opinfo.t2body.seq ?>
	<?cs call:data_comments_inputbox_v2(data_comments_inputbox_v2.param) ?>
<?cs else ?>
		<?cs call:profile_comment_param()?>
		<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", profile_comment_param.ret, qz_metadata.meta.userid, 
			"http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "UTF-8")?>
<?cs /if?>

