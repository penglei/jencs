<?cs include:"wupcs/data/upp/common.cs"?>
<?cs include:"wupcs/data/upp/upp_title.cs"?>
<?cs include:"wupcs/data/upp/upp_contentbox.cs"?>

<?cs call:data_upp_title()?>
<?cs call:data_upp_contentbox()?>
<?cs call:data_oprtime()?>
<?cs call:data_source()?>
<?cs call:data_like()?>
<?cs call:data_opr_comment()?>
<?cs call:data_opr_delfeed()?>
<?cs call:get_tuin_and_tid()?>

<?cs #/*赞批次*/?>
<?cs if:qz_metadata.orgdata.albumdata.extendinfo["isbatch"] == UPP_ACTION_BATCH ?>
	<?cs call:qfv("like.isbatch", 1) ?>
<?cs /if?>

<?cs call:data_comment_replies("http://u.photo.qq.com/cgi-bin/upp/qun_add_uc_cmt", "UTF-8")?>
<?cs call:data_comments("http://u.photo.qq.com/cgi-bin/upp/qun_add_uc_cmt", "UTF-8")?>

<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV
		|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL || qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM 
		|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY ?>
	<?cs call:data_opcomment_item("")?>
	<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", "")?>
	
	<?cs each:j = g_data_comments.replies[0].index?>
		<?cs #/*生成评论的回复节点*/?>
		<?cs call:data_commentReply_loop_item(j)?>
		<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", "")?>
	<?cs /each?>
<?cs else ?><?cs #主动?>
	<?cs #展示群头像、昵称 ?>
	<?cs call:qfv("meta.qun_feed", 1)?>
	<?cs call:qfv("meta.username", qz_metadata.meta.qun_name)?>
	<?cs call:qfv("meta.userhome", qz_metadata.meta.qun_home)?>
	<?cs call:qfv("meta.useravatar", qz_metadata.meta.qun_avatar)?>
	<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV && qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY 
		&& qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ?>
		<?cs call:data_comments_showstranger(0)?><?cs #禁止展示陌生人的评论?>
	<?cs /if?>

	<?cs each:i = g_data_comments.index?>
		<?cs call:data_comment_loop_item(i)?>
		<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", "")?>
		<?cs #生成单图评论小图标(仅针对多图主动)?>
		<?cs if:qz_metadata.orgdata.albumdata.iMultiUpNumber > 1 && qz_metadata.vt2body[i].extendinfo.lloc 
			&& (qz_metadata.feedtype==UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype==UC_WUP_FEED_TYPE_SHOW_ACTALL || qz_metadata.feedtype==UC_WUP_FEED_TYPE_SHOW_ACT_COMMALL) ?>
			<?cs call:get_topicid()?>
			<?cs call:data_comment_withImg_popup(0, "", "/qzone/photo/zone/icenter_popup.html", "", 2, "", "",qz_metadata.vt2body[i].extendinfo.surl, "") ?>
			<?cs call:data_comment_withImg_popup_v2(get_topicid.ret, qz_metadata.vt2body[i].extendinfo.lloc) ?>
			<?cs #新版浮层需要的参数 ?>
			<?cs call:data_popup_add_attr(_g_comment_qfv_curpath+".bfcnt.action", "imagesrc", qz_metadata.vt2body[i].extendinfo.lurl) ?>
		<?cs /if?>
		
		<?cs each:j = g_data_comments.replies[i].index?>
			<?cs #/*生成评论的回复节点*/?>
			<?cs call:data_commentReply_loop_item(j)?>
		<?cs /each?>
	<?cs /each?>
<?cs /if?>

<?cs #/*评论框*/?>
<?cs #if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ?>
	<?cs set:data_comments_inputbox_v2.param.param="" ?>
	<?cs set:data_comments_inputbox_v2.param.charset="utf-8" ?>
	<?cs set:data_comments_inputbox_v2.param.tuin=qz_metadata.meta.userid ?>
	<?cs set:data_comments_inputbox_v2.param.useReply=1 ?>
	<?cs set:data_comments_inputbox_v2.param.comuin=qz_metadata.opinfo.opuin ?>
	<?cs set:data_comments_inputbox_v2.param.comid=qz_metadata.opinfo.t2body.seq ?>
	<?cs call:data_comments_inputbox_v2(data_comments_inputbox_v2.param) ?>
<?cs #/if?>

<?cs call:data_opr_comment()?>
