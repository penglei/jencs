<?cs #/*生日礼物*/?>

<?cs #/*---title---*/?>
<?cs call:i()?>
<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_SMALL_V8_MODE , "") ?>
<?cs call:gift_birthday_timeTxt()?>
<?cs #/*call:data_title_defaultTxt(gift_birthday_timeTxt.ret + "生日，收到生日礼物：")*/?>
<?cs call:data_textTitle_tipTxt(gift_birthday_timeTxt.ret + "是") ?>
<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, UC_PLATFORM_ID_QZONE, qz_metadata.orgdata.nickname) ?>
<?cs if:qz_metadata.orgdata.itemcount > 0?>
	<?cs if:qz_metadata.orgdata.extendinfo.Gender == 2 ?>
		<?cs call:data_textTitle_tipTxt("的生日，共收到" + qz_metadata.orgdata.itemcount + "份生日礼物，你也赶紧送一份礼物祝她生日快乐吧！")?>
	<?cs else?>
		<?cs call:data_textTitle_tipTxt("的生日，共收到" + qz_metadata.orgdata.itemcount + "份生日礼物，你也赶紧送一份礼物祝他生日快乐吧！")?>
	<?cs /if?>
<?cs else ?>
	<?cs if:qz_metadata.orgdata.extendinfo.Gender == 2 ?>
		<?cs call:data_textTitle_tipTxt("的生日，赶紧送礼物祝她生日快乐吧！")?>
	<?cs else?>
		<?cs call:data_textTitle_tipTxt("的生日，赶紧送礼物祝他生日快乐吧！")?>
	<?cs /if?>
<?cs /if?>
<?cs #/*---quote.con---*/?>
<?cs #call:data_quote_desc(qz_metadata.orgdata.itemdata.0.desc)?>

<?cs #/*生日礼物要在内容区加一个送礼按钮*/#?>
<?cs call:data_cntmedia_pic(0, "http://qzonestyle.gtimg.cn/qzone_v6/img/feed/gift-default.jpg", "")?><?cs #url是空的?>

<?cs call:gift_send_birth_param()?>
<?cs call:data_popup(data_cntmedia_pic.ret + ".action",
						"送礼物",
						"http://imgcache.qq.com/qzone/gift/send_list.html",
						gift_send_birth_param.ret,
						1, 673, 431, "", "gift.content.robotsendgift")?>

<?cs #/*---extend_info.con---*/?>
<?cs set:j = 0?>
<?cs each:avatar = qz_metadata.orgdata.itemdata?>
	<?cs call:data_con_avatar("content.extendinfo.avatar." + j, avatar.extendinfo.uSenderUin, avatar.extendinfo.strSenderUin)?>
	<?cs set:j = j + 1 ?>
<?cs /each?>

<?cs #:只有一个就没有“等”字 ?>
<?cs if:qz_metadata.orgdata.extendinfo.nRevTotal > 0  ?>
	<?cs call:data_extendinfo_txt_style("","等","tip",0) ?>
<?cs /if ?>

<?cs call:data_extendinfo_url_style("", qz_metadata.orgdata.extendinfo.nRevTotal + "人" , "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/gift/list.html?type=0","link",0) ?>
<?cs if:subcount(qz_metadata.orgdata.itemdata) > 1  ?>
	<?cs call:data_extendinfo_txt_style("","都","tip",0) ?>
<?cs /if ?>
<?cs call:data_extendinfo_txt_style("", "已送礼","tip",0) ?>

<?cs #/*---operate.opr---*/?>
<?cs call:data_opr_more()?>
<?cs call:data_opr_delfeed()?>
<?cs call:data_source()?>
<?cs call:data_oprtime()?>
<?cs call:data_like()?>
<?cs if:qz_metadata.orgdata.extendinfo.SupportComment == 1 ?>
	<?cs call:data_opr_comment()?>
<?cs /if?>
<?cs #{/*评论区域*/?>
<?cs call:data_comment_replies("http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>
<?cs call:data_comments("http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>

<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV
		|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV
		|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL?>
		
	<?cs call:data_opcomment_item("")?>
	<?cs set:reply_param = qz_metadata.meta.appid + "''" + qz_metadata.orgdata.uin + "''" 
		+ qz_metadata.orgdata.mkey + "''" + qz_metadata.opinfo.t2body.seq + "''" 
		+ qz_metadata.opinfo.t2body.uin + "''" + qz_metadata.feedtype
		+ "''0''" + qz_metadata.orgdata.subtype ?>
	<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", reply_param)?>
	
	<?cs each:j = g_data_comments.replies[0].index?>
		<?cs #/*生成评论的回复节点*/?>
		<?cs call:data_commentReply_loop_item(j)?>
	<?cs /each?>

	<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", reply_param, qz_metadata.meta.userid, 
		"http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>	

<?cs else ?><?cs #主动?>
	<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV && qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY 
		&& qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ?>
		<?cs call:data_comments_showstranger(0)?><?cs #禁止展示陌生人的评论?>
	<?cs /if?>

	<?cs each:i = g_data_comments.index?>
		<?cs call:data_comment_loop_item(i)?>

		<?cs set:reply_param = qz_metadata.meta.appid + "''" + qz_metadata.orgdata.uin + "''" 
			+ qz_metadata.orgdata.mkey + "''" + qz_metadata.vt2body[i].seq + "''" 
			+ qz_metadata.vt2body[i].uin + "''" + qz_metadata.feedtype
			+ "''0''" + qz_metadata.orgdata.subtype ?>
		<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", reply_param)?>

		<?cs each:j = g_data_comments.replies[i].index?>
			<?cs #/*生成评论的回复节点*/?>
			<?cs call:data_commentReply_loop_item(j)?>
		<?cs /each?>
	<?cs /each?>

	<?cs set:comment_param = qz_metadata.meta.appid + "''" + qz_metadata.orgdata.uin + "''" 
		+ qz_metadata.orgdata.mkey + "''-1''0''" + qz_metadata.feedtype
		+ "''0''" + qz_metadata.orgdata.subtype?>
	<?cs #call:data_comments_inputbox("1|1|0|0|0|0|0", comment_param, qz_metadata.meta.userid, "http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>	
<?cs /if?>
