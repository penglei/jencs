<?cs include:"wupcs/data/shangcheng/const.cs"?>
<?cs include:"wupcs/data/shangcheng/common.cs"?>
<?cs include:"wupcs/data/shangcheng/shangcheng_title.cs"?>
<?cs include:"wupcs/data/shangcheng/shangcheng_contentbox.cs"?>

<?cs call:data_shangcheng_title()?>
<?cs call:data_shangcheng_contentbox()?>
<?cs call:data_oprtime()?>
<?cs call:data_source()?>
<?cs call:data_like()?>
<?cs call:shangcheng_opr_custom()?>
<?cs call:data_opr_more()?>
<?cs call:data_opr_prevent()?>
<?cs call:data_opr_delfeed()?>
<?cs call:get_tuin_and_tid()?>

<?cs if: qz_metadata.orgdata.subtype != SHANGCHENG_TYPE_SHARE_ACTIVE && qz_metadata.orgdata.subtype != SHANGCHENG_TYPE_ACTIVITY_INVITE 
		&& qz_metadata.orgdata.subtype != SHANGCHENG_TYPE_NOTE_INVITE?>
	<?cs call:data_opr_comment()?>
<?cs /if?>

<?cs call:data_comment_replies("http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "utf-8")?>
<?cs call:data_comments("http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "utf-8")?>

<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV
		|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV
		|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL?>
		
	<?cs call:data_opcomment_item("")?>
	<?cs call:shangcheng_commentReply_param(qz_metadata.opinfo.t2body)?>
	<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", shangcheng_commentReply_param.ret)?>
	
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
	<?cs call:shangcheng_comment_more_param()?>
	<?cs call:data_comments_more_param(shangcheng_comment_more_param.ret)?>

	<?cs each:i = g_data_comments.index?>
		<?cs call:data_comment_loop_item(i)?>
		<?cs call:shangcheng_commentReply_param(qz_metadata.vt2body[i])?>
		<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", shangcheng_commentReply_param.ret)?>

		<?cs #评论回复的more param都是一样的?>
		<?cs call:data_commentReply_more_param(shangcheng_comment_more_param.ret)?>

		<?cs each:j = g_data_comments.replies[i].index?>
			<?cs #/*生成评论的回复节点*/?>
			<?cs call:data_commentReply_loop_item(j)?>
		<?cs /each?>
	<?cs /each?>
<?cs /if?>

<?cs #/*评论框*/?>
<?cs if: qz_metadata.orgdata.subtype != SHANGCHENG_TYPE_SHARE_ACTIVE && qz_metadata.orgdata.subtype != SHANGCHENG_TYPE_ACTIVITY_INVITE 
		&& qz_metadata.orgdata.subtype != SHANGCHENG_TYPE_NOTE_INVITE?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV
		|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL 
		|| qz_metadata.feedtype == UC_FEEDS_TYPE_RELATEPSV?>
		<?cs call:shangcheng_commentReply_param(qz_metadata.opinfo.t2body)?>
		<?cs set:data_comments_inputbox_v2.param.config="1|1|0|0|0|0|0" ?>
		<?cs set:data_comments_inputbox_v2.param.param=shangcheng_commentReply_param.ret ?>
		<?cs set:data_comments_inputbox_v2.param.charset="utf-8" ?>
		<?cs set:data_comments_inputbox_v2.param.tuin=qz_metadata.meta.userid ?>
		<?cs set:data_comments_inputbox_v2.param.useReply=1 ?>
		<?cs set:data_comments_inputbox_v2.param.comuin=qz_metadata.opinfo.opuin ?>
		<?cs set:data_comments_inputbox_v2.param.comid=qz_metadata.opinfo.t2body.seq ?>
		<?cs call:data_comments_inputbox_v2(data_comments_inputbox_v2.param) ?>
	<?cs else ?>
		<?cs call:shangcheng_comment_param()?>
		<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", shangcheng_comment_param.ret, qz_metadata.meta.userid, 
			"http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "utf-8")?>
	<?cs /if?>
<?cs /if?>
