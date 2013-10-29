<?cs include:"wupcs/data/couple/const.cs"?>
<?cs include:"wupcs/data/couple/couple_contentbox.cs"?>
<?cs include:"wupcs/data/couple/couple_title.cs"?>

<?cs call:data_couple_title()?>
<?cs call:data_couple_contentbox()?>

<?cs #生成浏览按钮?>
<?cs call:get_tuin_and_tid()?>
<?cs if:qz_metadata.qz_data.key1.PRD.cnt>0 ?>
	<?cs set:_share_visitor_param = qz_metadata.meta.appid + "|" +
					  qz_metadata.orgdata.mkey + "|" +
					  qz_metadata.meta.opuin?>
	<?cs call:data_opr_visitor(_share_visitor_param, qz_metadata.qz_data.key1.PRD.cnt)?>
<?cs /if?>

<?cs call:data_oprtime()?>
<?cs call:data_source()?>
<?cs call:data_like()?>
<?cs call:data_opr_comment()?>
<?cs call:data_opr_delfeed()?>

<?cs if:qz_metadata.orgdata.subtype == COUPLE_srctype_mood ?>
	<?cs call:data_comment_replies("http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_share_qzone_reply_add", "utf-8")?>
<?cs else ?>
	<?cs call:data_comment_replies("http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_photo_qzone_reply_add", "utf-8")?>
<?cs /if?>

<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL ?>

	<?cs if:qz_metadata.orgdata.subtype == COUPLE_srctype_mood ?>
		<?cs call:data_comments("http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_share_qzone_reply_add", "utf-8")?>

		<?cs set:more_param = "uin=" + qz_metadata.orgdata.uin + "^id=" + qz_metadata.orgdata.mkey + "^plat=1^pairkey=" 
			+ qz_metadata.orgdata.extendinfo.pairkey +"^sceneid=" + qz_metadata.feedtype
			+ "^c_id=" + qz_metadata.opinfo.t2body.extendinfo.c_id ?>

		<?cs set:reply_param = "uin=" + qz_metadata.orgdata.uin + "^id=" + qz_metadata.orgdata.mkey + "^plat=1^pairkey=" 
			+ qz_metadata.orgdata.extendinfo.pairkey +"^sceneid=" + qz_metadata.feedtype
			+ "^c_id=" + qz_metadata.opinfo.t2body.extendinfo.c_id ?>		
	<?cs else ?>
		<?cs call:data_comments("http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_photo_qzone_reply_add", "utf-8")?>

		<?cs set:more_param = "uin=" + qz_metadata.orgdata.uin + "^key=" + qz_metadata.orgdata.itemdata[0].itemid 
			+ "^albumid=" + qz_metadata.orgdata.albumdata.sAlbumId + "^plat=1^pairkey=" + qz_metadata.orgdata.extendinfo.pairkey
			+ "^sceneid=" + qz_metadata.feedtype + "^picstring=" + qz_metadata.orgdata.extendinfo.picstring
			+ "^pickey=" + qz_metadata.orgdata.extendinfo.pickey + "^c_id=" + qz_metadata.opinfo.t2body.extendinfo.c_id ?>

		<?cs set:reply_param = "uin=" + qz_metadata.orgdata.uin + "^key=" + qz_metadata.orgdata.itemdata[0].itemid 
			+ "^albumid=" + qz_metadata.orgdata.albumdata.sAlbumId + "^plat=1^pairkey=" + qz_metadata.orgdata.extendinfo.pairkey
			+ "^sceneid=" + qz_metadata.feedtype + "^picstring=" + qz_metadata.orgdata.extendinfo.picstring
			+ "^pickey=" + qz_metadata.orgdata.extendinfo.pickey + "^c_id=" + qz_metadata.opinfo.t2body.extendinfo.c_id ?>
	<?cs /if?>

	<?cs #初始化一条评论?>
	<?cs call:data_opcomment_item("")?>
	<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", reply_param)?>
	<?cs each:j = g_data_comments.replies[0].index?>
		<?cs #/*生成评论的回复节点*/?>
		<?cs call:data_commentReply_loop_item(j)?>
	<?cs /each?>
<?cs else ?><?cs #主动?>

	<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV && qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY 
		&& qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ?>
		<?cs call:data_comments_showstranger(0)?><?cs #禁止展示陌生人的评论?>
	<?cs /if?>

	<?cs if:qz_metadata.orgdata.subtype == COUPLE_srctype_mood ?>
		<?cs call:data_comments("http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_share_qzone_reply_add", "utf-8")?>

		<?cs set:more_param = "uin=" + qz_metadata.orgdata.uin + "^id=" + qz_metadata.orgdata.mkey + "^plat=1^pairkey=" 
			+ qz_metadata.orgdata.extendinfo.pairkey +"^sceneid=" + qz_metadata.feedtype ?>

		<?cs set:reply_param = "uin=" + qz_metadata.orgdata.uin + "^id=" + qz_metadata.orgdata.mkey + "^plat=1^pairkey=" 
			+ qz_metadata.orgdata.extendinfo.pairkey +"^sceneid=" + qz_metadata.feedtype ?>
	<?cs else ?>
		<?cs call:data_comments("http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_photo_qzone_reply_add", "utf-8")?>

		<?cs set:more_param = "uin=" + qz_metadata.orgdata.uin + "^key=" + qz_metadata.orgdata.itemdata[0].itemid 
			+ "^albumid=" + qz_metadata.orgdata.albumdata.sAlbumId + "^plat=1^pairkey=" + qz_metadata.orgdata.extendinfo.pairkey
			+ "^sceneid=" + qz_metadata.feedtype + "^picstring=" + qz_metadata.orgdata.extendinfo.picstring
			+ "^pickey=" + qz_metadata.orgdata.extendinfo.pickey ?>

		<?cs set:reply_param = "uin=" + qz_metadata.orgdata.uin + "^key=" + qz_metadata.orgdata.itemdata[0].itemid 
			+ "^albumid=" + qz_metadata.orgdata.albumdata.sAlbumId + "^plat=1^pairkey=" + qz_metadata.orgdata.extendinfo.pairkey
			+ "^sceneid=" + qz_metadata.feedtype + "^picstring=" + qz_metadata.orgdata.extendinfo.picstring
			+ "^pickey=" + qz_metadata.orgdata.extendinfo.pickey ?>
	<?cs /if?>

	<?cs each:i = g_data_comments.index?>
		<?cs call:data_comment_loop_item(i)?>

		<?cs set:tmp_more_param = more_param ?>
		<?cs set:tmp_reply_param = reply_param ?>
		<?cs set:tmp_more_param = tmp_more_param + "^c_id=" + qz_metadata.vt2body[i].extendinfo.c_id ?>
		<?cs set:tmp_reply_param = tmp_reply_param + "^c_id=" + qz_metadata.vt2body[i].extendinfo.c_id ?>

		<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", tmp_reply_param)?>
		<?cs each:j = g_data_comments.replies[i].index?>
			<?cs #/*生成评论的回复节点*/?>
			<?cs call:data_commentReply_loop_item(j)?>

			<?cs #{/*二级评论再回复*/?>
			<?cs call:data_commentReply_replybtn("1|1|0|0|0|0|0", tmp_reply_param) ?>
		<?cs /each?>
	<?cs /each?>
<?cs /if?>

<?cs #{/*评论框*/?>
<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV
	|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT  || qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL ?>

	<?cs if:qz_metadata.orgdata.subtype == COUPLE_srctype_mood ?>
		<?cs set:reply_action = "http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_share_qzone_reply_add" ?>
		<?cs set:reply_param = "uin=" + qz_metadata.orgdata.uin + "^id=" + qz_metadata.orgdata.mkey + "^plat=1^pairkey=" 
			+ qz_metadata.orgdata.extendinfo.pairkey +"^sceneid=" + qz_metadata.feedtype
			+ "^c_id=" + qz_metadata.opinfo.t2body.extendinfo.c_id ?>
	<?cs else ?>
		<?cs set:reply_action = "http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_photo_qzone_reply_add" ?>
		<?cs set:reply_param = "uin=" + qz_metadata.orgdata.uin + "^key=" + qz_metadata.orgdata.itemdata[0].itemid 
			+ "^albumid=" + qz_metadata.orgdata.albumdata.sAlbumId + "^plat=1^pairkey=" + qz_metadata.orgdata.extendinfo.pairkey
			+ "^sceneid=" + qz_metadata.feedtype + "^picstring=" + qz_metadata.orgdata.extendinfo.picstring
			+ "^pickey=" + qz_metadata.orgdata.extendinfo.pickey + "^c_id=" + qz_metadata.opinfo.t2body.extendinfo.c_id ?>
	<?cs /if?>

	<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", reply_param, qz_metadata.meta.userid, reply_action, "utf-8")?>
<?cs else ?>
	<?cs if:qz_metadata.orgdata.subtype == COUPLE_srctype_mood ?>
		<?cs set:comment_action = "http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_share_qzone_comment_add" ?>
		<?cs set:comment_param = "uin=" + qz_metadata.orgdata.uin + "^id=" + qz_metadata.orgdata.mkey + "^plat=1^pairkey=" 
			+ qz_metadata.orgdata.extendinfo.pairkey +"^sceneid=" + qz_metadata.feedtype ?>
	<?cs else ?>
		<?cs set:comment_action = "http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_photo_qzone_comment_add" ?>
		<?cs set:comment_param = "uin=" + qz_metadata.orgdata.uin + "^key=" + qz_metadata.orgdata.itemdata[0].itemid 
			+ "^albumid=" + qz_metadata.orgdata.albumdata.sAlbumId + "^plat=1^pairkey=" 
			+ qz_metadata.orgdata.extendinfo.pairkey +"^sceneid=" + qz_metadata.feedtype 
			+ "^picstring=" + qz_metadata.orgdata.extendinfo.picstring + "^pickey=" + qz_metadata.orgdata.extendinfo.pickey ?>
	<?cs /if?>
	<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", comment_param, qz_metadata.meta.userid, comment_action, "utf-8")?>
<?cs /if?>
<?cs #}/*end: 评论框*/?>
