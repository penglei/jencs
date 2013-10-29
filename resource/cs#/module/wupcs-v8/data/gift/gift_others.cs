<?cs ####
	/*机器人提醒生日feeds*/
?>
<?cs def:data_gift_robotbirth_entry()?>
	<?cs call:data_content_init(G_LAYOUT_LEFTIMG_V8, G_IMG_SMALL_V8_MODE , "") ?>
	<?cs #/*---title---*/ ?>
	<?cs call:i()?>
	<?cs call:gift_birthday_timeTxt()?>

	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV?>
		<?cs call:data_title_tipTxt("评论")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?>
		<?cs call:data_title_tipTxt("回复")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV?>
		<?cs call:data_title_tipTxt("提到我")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM?>
		<?cs call:data_title_tipTxt("在评论中提到我")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY?>
		<?cs call:data_title_tipTxt("在回复中提到我")?>
	<?cs /if?>

	<?cs if:qz_metadata.orgdata.extendinfo.SupportComment == 1 ?>
		<?cs call:data_textTitle_tipTxt(gift_birthday_timeTxt.ret + "是") ?>
		<?cs call:data_textTitle_nick(qz_metadata.orgdata.uin, UC_PLATFORM_ID_QZONE, qz_metadata.orgdata.nickname) ?>
		<?cs if:qz_metadata.orgdata.extendinfo.Gender == 2 ?>
			<?cs call:data_textTitle_tipTxt("的生日，赶紧送礼物祝她生日快乐吧！")?>
		<?cs else ?>
			<?cs call:data_textTitle_tipTxt("的生日，赶紧送礼物祝他生日快乐吧！")?>
		<?cs /if?>
	<?cs else ?>
		<?cs if:qz_metadata.orgdata.extendinfo.Gender == 2 ?>
			<?cs call:data_textTitle_tipTxt(gift_birthday_timeTxt.ret + "是"+qz_metadata.meta.username+"的生日，赶紧送礼物祝她生日快乐吧！")?>
		<?cs else ?>
			<?cs call:data_textTitle_tipTxt(gift_birthday_timeTxt.ret + "是"+qz_metadata.meta.username+"的生日，赶紧送礼物祝他生日快乐吧！")?>
		<?cs /if?>
	<?cs /if?>

	<?cs #/*生日礼物要在内容区加一个送礼按钮*/#?>
	<?cs #call:data_cntmedia_pic(0, "", "giftbtntxt")?><?cs #url是空的?>
	<?cs call:data_cntmedia_pic(0, "http://qzonestyle.gtimg.cn/qzone_v6/img/feed/gift-default.jpg", "")?><?cs #url是空的?>

	<?cs call:gift_send_birth_param()?>
	<?cs call:data_popup(data_cntmedia_pic.ret + ".action",
						"送礼物",
						"http://imgcache.qq.com/qzone/gift/send_list.html",
						gift_send_birth_param.ret,
						1, 673, 431, "", "gift.content.robotsendgift")?>
	<?cs #call:data_source()?>
	<?cs call:data_oprtime()?>
	<?cs call:data_opr_more()?>
	<?cs call:data_opr_delfeed()?>
	<?cs call:data_like()?>

	<?cs if:qz_metadata.orgdata.extendinfo.SupportComment == 1 ?>
		<?cs call:data_opr_comment()?>
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
					<?cs #call:data_commentReply_replybtn(_mood_comm_config.ret, _mood_comm_param.ret) ?>
			<?cs /each?>
		<?cs /each?>

			<?cs set:comment_param = qz_metadata.meta.appid + "''" + qz_metadata.orgdata.uin + "''" 
				+ qz_metadata.orgdata.mkey + "''-1''0''" + qz_metadata.feedtype
				+ "''0''" + qz_metadata.orgdata.subtype?>
			<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", comment_param, qz_metadata.meta.userid, 
				"http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>	
		<?cs /if?>
	<?cs /if?>
<?cs /def?>


<?cs ####
	/**
	 *黄钻索要feeds(只有被动)
	 */
?>
<?cs def:data_gift_askyellow_entry()?>
	<?cs call:i()?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?>
		<?cs #/*黄钻索要回复*/?>
		<?cs call:data_title_tipTxt("在 “黄钻索要” 回复了我")?>
	<?cs else ?>
		<?cs #/*标题只是一句话"xxx向我索要a个月黄钻*/?>
		<?cs call:data_title_tipTxt("向我索要“" +  qz_metadata.orgdata.extendinfo.iMonCnt + "黄钻服务”")?>
	<?cs /if?>

	<?cs call:data_quote_desc(qz_metadata.orgdata.desc)?>

	<?cs #/*内容区是一张图片*/ ?>
	<?cs call:data_cntmedia_pic_urlaction(
			0,
			g_siDomain + "/qzone_v6/img/feed/act_img/charge_me.png",
			qz_metadata.orgdata.srcurl,
			"", "")?>

	<?cs #/*srcurl是动作相关的一个url，是一个很广义的字段*/?>
	<?cs call:data_opr_url(0, "为TA付费", qz_metadata.orgdata.srcurl,"gift.opr.payforTA")?>

	<?cs call:data_opr_comment()?>
	<?cs call:data_opr_more()?>
	<?cs call:data_opr_delfeed()?>
	<?cs call:data_source()?>
	<?cs call:data_oprtime()?>

	<?cs call:data_comments(g_data_comments_default_cgi, "GB")?>
	<?cs #/*礼物没有查看更多评论*/?>
	<?cs loop: i = g_data_comments_start, g_data_comments_end, 1?>
		<?cs call:data_comment_loop_item(i)?>
		<?cs call:_gift_psv_comments_param(qz_metadata.vt2body[i].seq,
									qz_metadata.vt2body[i].uin,
									G_COMMENTS_PARAM_NORMAL_FLAG)?>

		<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", _gift_psv_comments_param.ret)?>

		<?cs loop:j = g_data_commentReplies_start, g_data_commentReplies_end, 1?>
			<?cs #/*生成评论的回复节点*/?>
			<?cs call:data_commentReply_loop_item(j)?>
		<?cs /loop?>

	<?cs /loop?>

	<?cs #{/*评论框*/?>
	<?cs #/*只有两个人交互，所以如果有新评论，就是第一条评论的回复*/?>
	<?cs if:qz_metadata.vt2count?>
		<?cs set:_cmt_input_tuin = qz_metadata.vt2body.0.uin?>
		<?cs call:_gift_psv_comments_param(qz_metadata.vt2body.0.seq, qz_metadata.vt2body.0.uin, G_COMMENTS_PARAM_NORMAL_FLAG)?>
	<?cs else ?>
		<?cs set:_cmt_input_tuin = qz_metadata.orgdata.uin?>
		<?cs call:_gift_psv_comments_param(-1, 0, G_COMMENTS_PARAM_NORMAL_FLAG)?>
	<?cs /if?>

	<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", _gift_psv_comments_param.ret, _cmt_input_tuin, "http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>
	<?cs #}/*end: 评论框*/?>

<?cs /def?>


	<?cs ####
		/**
		 *黄钻过期提醒feeds(被动)
		 */
	?>
	<?cs def:data_gift_expireyellow_entry()?>
		<?cs call:i()?>
		<?cs set:_srcurl="http://pay.qq.com/qzone/index.shtml?aid=feeds.close"?>
		<?cs if:qz_metadata.orgdata.srcurl?>
			<?cs set:_srcurl=qz_metadata.orgdata.srcurl?>
		<?cs /if?>
		<?cs if:qz_metadata.orgdata.title.0.content?>
			<?cs call:data_title_defaultTxt(qz_metadata.orgdata.title.0.content)?>
			<?cs call:data_cntmedia_pic_urlaction(
					0,
					qz_metadata.orgdata.itemdata.0.picinfo.0.url,
					_srcurl,
					"", "")?>
		<?cs else?>
			<?cs set:_text = "提醒我：您的黄钻服务还有" +
					qz_metadata.orgdata.extendinfo.iDayCnt +
					"天就到期了，赶紧续费延长期限，免得影响特权的使用哦！"?>
			<?cs call:data_title_defaultTxt(_text)?>
			<?cs call:data_cntmedia_pic_urlaction(
					0,
					g_siDomain + "/qzone_v6/img/feed/act_img/remind_charge_vip.png",
					_srcurl,
					"", "")?>
		<?cs /if?>

		<?cs call:data_opr_url(0, "续费", _srcurl,"gift.opr.xufei")?>

		<?cs call:data_source()?>
		<?cs call:data_oprtime()?>
		<?cs call:data_opr_more()?>
		<?cs call:data_opr_delfeed()?>
<?cs /def?>
