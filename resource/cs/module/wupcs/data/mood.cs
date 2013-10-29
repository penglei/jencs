<?cs include:"wupcs/data/mood/const.cs"?>
<?cs include:"wupcs/data/mood/common.cs"?>
<?cs include:"wupcs/data/mood/mood_title.cs"?>
<?cs include:"wupcs/data/mood/mood_contentbox.cs"?>

<?cs call:data_mood_title()?>
<?cs call:data_mood_subtitle()?>
<?cs call:data_mood_contentbox()?>
<?cs call:data_listSameAction()?>
<?cs call:data_extend_content_lbs(qz_metadata.lbsdata) ?>

<?cs if:qz_metadata.meta.typeid == MOOD_TYPEID_FORWARD || qz_metadata.feedstype == UC_WUP_FEEDSTYPE_PSV || qz_metadata.feedstype == UC_WUP_FEEDSTYPE_ABT?>
<?cs call:data_extendinfo_source("") ?>
<?cs /if?>

<?cs #默认的转发按钮?>
<?cs if:!(qz_metadata.orgdata.mediatype=="vedio" && qz_metadata.orgdata.extendinfo.video_type==VIDEO_TYPE_LOCAL) ?>
	<?cs call:data_opr_forward()?>
<?cs /if?>

<?cs call:data_oprtime()?>
<?cs call:data_source()?>
<?cs call:data_like()?>
<?cs call:data_opr_more()?>
<?cs call:data_opr_prevent()?>
<?cs call:data_opr_delfeed()?>
<?cs call:data_privacy_icon()?>
<?cs call:data_opr_comment()?>
<?cs call:get_tuin_and_tid()?>
<?cs call:get_mood_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid, qz_metadata.orgdata.extendinfo.t1_source)?>

<?cs #生成浏览按钮?>
<?cs if:qz_metadata.qz_data.key1.PRD.cnt>0 ?>
	<?cs #:这里无法区分转发说说还是原创说说，暂时通过有没有relybody来判断?>
	<?cs if:subcount(qz_metadata.relybody) > 0 ?>
		<?cs set:_mood_visitor_param = qz_metadata.meta.appid + "|" +
					  qz_metadata.relybody.0.mkey + "|" +
					  get_tuin_and_tid.uin ?>
		<?cs else ?>
		<?cs set:_mood_visitor_param = qz_metadata.meta.appid + "|" +
						  qz_metadata.orgdata.mkey + "|" +
						  get_tuin_and_tid.uin ?>
	<?cs /if ?>
		<?cs call:data_opr_visitor(_mood_visitor_param, qz_metadata.qz_data.key1.PRD.cnt)?>
<?cs /if?>

<?cs call:data_comment_replies("http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds", "utf-8")?>

<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHARETOME ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY ||
		qz_metadata.feedtype == UC_WUP_FEED_BE_AT_IN_COMM ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLY_ALSO?>

	<?cs call:data_comments("http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds", "utf-8")?>

	<?cs #只添加更多回复的入口?>
	<?cs #设定more的
	参数调用必须在data_comment_loop_item或data_opcomment_item前面，因为后者加了一个限制：
		只对有moreUrl或者moreCgi的调用才会生成more入口数据?>
	<?cs call:data_commentReply_more("http://taotao.qq.com/cgi-bin/emotion_cgi_ic_getreplies",
									get_mood_url.ret)?>
	<?cs #初始化一条评论?>
	<?cs call:data_opcomment_item("")?>
	<?cs #更多评论的参数。这必须在data_comment_item或者data_opcomment_item之后调用，因为它依赖前者生成的数据?>
	<?cs set:more_param = "uin=" + get_tuin_and_tid.uin
						+ "&pos=0&num=10&t1_source=" + qz_metadata.extendinfo.t1_source
						+ "&tid=" + get_tuin_and_tid.tid
						+ "&t2_uin=" + qz_metadata.vt2body[i].uin
						+ "&t2_tid=" + qz_metadata.vt2body[i].seq
						+ "&sceneid=" + UC_WUP_FEED_TYPE_SHOW_ACT_COMMALL?>
	<?cs call:data_commentReply_more_param(more_param)?>

	<?cs if:subcount(qz_metadata.orgdata.itemdata) > qz_metadata.opinfo.t2body.extendinfo.pic_index 
		&& subcount(qz_metadata.orgdata.itemdata[qz_metadata.opinfo.t2body.extendinfo.pic_index].picinfo) > 0 && qz_metadata.orgdata.itemcount >1 ?><?cs #/*单图就不展示评论缩略图*/?>
		<?cs set:icon_param = get_tuin_and_tid.tid + "|"
			+ get_tuin_and_tid.uin + "|"
			+ qz_metadata.opinfo.t2body.extendinfo.pic_index ?>
		<?cs call:data_comment_withImg_popup(0, "", "/qzone/photo/zone/icenter_popup.html", icon_param, 2, "", "", qz_metadata.orgdata.itemdata[qz_metadata.opinfo.t2body.extendinfo.pic_index].picinfo[0].url, "") ?>
		<?cs set:topicid = get_tuin_and_tid.uin + "_" + get_tuin_and_tid.tid + "_" + qz_metadata.orgdata.extendinfo.t1_source?>
		<?cs call:data_comment_withImg_popup_v2(topicid, qz_metadata.orgdata.itemdata[qz_metadata.opinfo.t2body.extendinfo.pic_index].picinfo.0.extendinfo.mood_pickey) ?>
		<?cs #call:data_comment_withIcon_popup(0, "", "/qzone/photo/zone/icenter_popup.html", icon_param, 2, "", "", "", "") ?>
	<?cs /if?>

	<?cs #/*评论回复参数*/?>
	<?cs call:_mood_comm_param(qz_metadata.opinfo.t2body) ?>
	<?cs call:_mood_comm_config() ?>
	
	<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_SHARETOME ?>

		<?cs call:data_comment_replybtn(_mood_comm_config.ret, _mood_comm_param.ret)?>
		
		<?cs #call:data_comment_deletebtn("","")?>

		<?cs each:j = g_data_comments.replies[0].index?>
			<?cs #/*生成评论的回复节点*/?>
			<?cs call:data_commentReply_loop_item(j)?>

			<?cs set:_at_uin = qz_metadata.opinfo.t2body.t3body.vt3body[j].uin ?>
			<?cs set:_at_nick = qz_metadata.opinfo.t2body.t3body.vt3body[j].nickname ?>
			<?cs call:get_remark(_at_uin, _at_nick) ?>
			<?cs call:ugc_as_json_in_html(get_remark.ret,1,0) ?>

			<?cs #/*同步微博的设置*/ ?>
			<?cs if:qz_metadata.orgdata.extendinfo.to_tweet != 0 && !qz_metadata.orgdata.multimedia.voice.voice_id?>
				<?cs call:is_famous_uin(qz_metadata.meta.bitmap)?>
				<?cs if:is_famous_uin.ret == 1?>
					<?cs set:com_config = "{config:'1|1|1|b41,1,to_tweet,点评到微博|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick',"
						+ "atuin:" + _at_uin + ",atnick:'" + ugc_as_json_in_html.ret + "'}"?>
				<?cs else?>
					<?cs set:com_config = "{config:'1|1|1|b41,!w_sync,to_tweet,点评到微博|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick',"
						+ "atuin:" + _at_uin + ",atnick:'" + ugc_as_json_in_html.ret + "'}"?>
				<?cs /if?>
			<?cs else ?>
				<?cs set:com_config = "{config:'1|1|1|0|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick',"
						+ "atuin:" + _at_uin + ",atnick:'" + ugc_as_json_in_html.ret + "'}"?>
			<?cs /if?>	

			<?cs #{/*二级评论再回复*/?>
			<?cs call:data_commentReply_replybtn(com_config, _mood_comm_param.ret) ?>
			<?cs #/*二级评论再回复的删除*/?>
			<?cs #call:data_commentReply_deletebtn("","")?>
		<?cs /each?>
	<?cs /if?>

<?cs else ?>

	<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV && qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY 
		&& qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_COM && qz_metadata.feedtype != UC_WUP_FEED_BE_AT_IN_COMM 
		&& qz_metadata.feedtype != UC_WUP_FEED_TYPE_REPLY_ALSO?>
		<?cs call:data_comments_showstranger(0)?><?cs #禁止展示陌生人的评论?>
	<?cs /if?>
	<?cs call:data_comments("http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds", "utf-8")?>

	<?cs call:data_comments_more("http://taotao.qq.com/cgi-bin/emotion_cgi_ic_getcomments",
								"http://taotao.qq.com/cgi-bin/emotion_cgi_ic_getreplies",
								get_mood_url.ret)?>

	<?cs set:more_param = "uin=" + get_tuin_and_tid.uin
						+ "&pos=0&num=20&cmtnum=100&t1_source=" + qz_metadata.extendinfo.t1_source
						+ "&tid=" + get_tuin_and_tid.tid
						+ "&who=1"
						+ "&sceneid=" + UC_WUP_FEED_TYPE_SHOW_ACT_COMMALL?>
	<?cs call:data_comments_more_param(more_param)?>

	<?cs each:i = g_data_comments.index?>
		<?cs call:data_comment_loop_item(i)?>

		<?cs if:subcount(qz_metadata.orgdata.itemdata) > qz_metadata.vt2body[i].extendinfo.pic_index
			&& subcount(qz_metadata.orgdata.itemdata[qz_metadata.vt2body[i].extendinfo.pic_index].picinfo) > 0 && qz_metadata.orgdata.itemcount >1?>
			<?cs set:icon_param = get_tuin_and_tid.tid + "|"
				+ get_tuin_and_tid.uin + "|"
				+ qz_metadata.vt2body[i].extendinfo.pic_index ?>
			<?cs call:data_comment_withImg_popup(0, "", "/qzone/photo/zone/icenter_popup.html", icon_param, 2, "", "", qz_metadata.orgdata.itemdata[qz_metadata.vt2body[i].extendinfo.pic_index].picinfo[0].url, "") ?>
			<?cs set:topicid = get_tuin_and_tid.uin + "_" + get_tuin_and_tid.tid + "_" + qz_metadata.orgdata.extendinfo.t1_source?>
			<?cs call:data_comment_withImg_popup_v2(topicid, qz_metadata.orgdata.itemdata[qz_metadata.vt2body[i].extendinfo.pic_index].picinfo.0.extendinfo.mood_pickey) ?>
			<?cs #call:data_comment_withIcon_popup(0, "", "/qzone/photo/zone/icenter_popup.html", icon_param, 2, "", "", "", "") ?>
		<?cs /if?>

		<?cs call:_mood_comm_param(qz_metadata.vt2body[i])?>
		<?cs call:_mood_comm_config() ?>

		<?cs call:data_comment_replybtn(_mood_comm_config.ret, _mood_comm_param.ret)?>


		<?cs #/*主动feeds对主人自己的评论或主贴下的评论加上删除按钮，提到我的在被动feeds内不出现*/?>
		<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV ?>
			<?cs call:data_comment_deletebtn("","")?>
		<?cs /if?>

		<?cs #/*主动的more reply 请求参数*/ ?>
		<?cs set:more_param = "uin=" + get_tuin_and_tid.uin
						+ "&pos=0&num=20&t1_source=" + qz_metadata.extendinfo.t1_source
						+ "&tid=" + get_tuin_and_tid.tid
						+ "&t2_uin=" + qz_metadata.vt2body[i].uin
						+ "&t2_tid=" + qz_metadata.vt2body[i].seq
						+ "&sceneid=" + UC_WUP_FEED_TYPE_SHOW_PSVALL?>
		<?cs call:data_commentReply_more_param(more_param)?>

		<?cs each:j = g_data_comments.replies[i].index?>
			<?cs #/*生成评论的回复节点*/?>
			<?cs call:data_commentReply_loop_item(j)?>

			<?cs #{/*二级评论再回复*/?>
			<?cs call:data_commentReply_replybtn(_mood_comm_config.ret, _mood_comm_param.ret) ?>
			<?cs #/*二级评论再回复的删除*/?>
			<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV ?>
				<?cs call:data_commentReply_deletebtn("","")?>
			<?cs /if?>
		<?cs /each?>

	<?cs /each?>
<?cs /if?>

<?cs #{/*评论框*/?>
<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV
	|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL 
	||	qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM || 	qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY
	|| qz_metadata.feedtype == UC_WUP_FEED_BE_AT_IN_COMM || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLY_ALSO?>

	<?cs #/*评论被动,回复被动。评论中提到，回复中提到，点击评论框和操作区将评论当作回复操作*/?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV 
			||	qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM || qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY
			|| qz_metadata.feedtype == UC_WUP_FEED_BE_AT_IN_COMM || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLY_ALSO?>
		<?cs call:_mood_comm_param(qz_metadata.opinfo.t2body) ?>
		<?cs set:data_comments_inputbox_v2.param.param=_mood_comm_param.ret ?>
		<?cs set:data_comments_inputbox_v2.param.charset="utf-8" ?>
		<?cs set:data_comments_inputbox_v2.param.tuin=qz_metadata.meta.userid ?>
		<?cs set:data_comments_inputbox_v2.param.useReply=1 ?>
		<?cs set:data_comments_inputbox_v2.param.comuin=qz_metadata.opinfo.opuin ?>
		<?cs set:data_comments_inputbox_v2.param.comid=qz_metadata.opinfo.t2body.seq ?>
		<?cs call:data_comments_inputbox_v2(data_comments_inputbox_v2.param) ?>
	<?cs else?>
		<?cs call:_mood_comm_param(qz_metadata.opinfo.t2body) ?>
		<?cs call:_mood_comm_config() ?>
		<?cs call:data_comments_inputbox(_mood_comm_config.ret, _mood_comm_param.ret, qz_metadata.meta.userid, 
										"http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds", "utf-8")?>
	<?cs /if?>
<?cs else ?>
	<?cs set:_mood_comm_param = "t1_source=" + qz_metadata.extendinfo.t1_source
							  + "&t1_uin=" + get_tuin_and_tid.uin
							  + "&t1_tid=" + get_tuin_and_tid.tid
							  + "&signin=" + qz_metadata.orgdata.extendinfo.signin
							  + "&sceneid=" + qz_metadata.feedtype?>

	<?cs if:qz_metadata.orgdata.extendinfo.to_tweet != 0 && !qz_metadata.orgdata.multimedia.voice.voice_id?>
		<?cs call:is_famous_uin(qz_metadata.meta.bitmap)?>
		<?cs if:is_famous_uin.ret == 1?>
			<?cs #/*权限说说不能有转发按钮*/ ?>
			<?cs if:qz_metadata.orgdata.accessright == UGCFLAG_QQFRIEND || qz_metadata.orgdata.accessright == UGCFLAG_WHITELIST || qz_metadata.orgdata.accessright == UGCFLAG_ONLYSELF ?>
				<?cs set:com_config = "1|1|1|0,b52,with_fwd,同时转发;b41,1,to_tweet,点评到微博|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick"?>
			<?cs else?>
				<?cs set:com_config = "1|1|1|1,b52,with_fwd,同时转发;b41,1,to_tweet,点评到微博|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick"?>
			<?cs /if?>
		<?cs else?>
			<?cs #/*权限说说不能有转发按钮*/ ?>
			<?cs if:qz_metadata.orgdata.accessright == UGCFLAG_QQFRIEND || qz_metadata.orgdata.accessright == UGCFLAG_WHITELIST || qz_metadata.orgdata.accessright == UGCFLAG_ONLYSELF ?>
				<?cs set:com_config = "1|1|1|0,b52,with_fwd,同时转发;b41,!w_sync,to_tweet,点评到微博|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick"?>
			<?cs else?>
				<?cs set:com_config = "1|1|1|1,b52,with_fwd,同时转发;b41,!w_sync,to_tweet,点评到微博|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick"?>
			<?cs /if?>
		<?cs /if?>
	<?cs else ?>
		<?cs if:qz_metadata.orgdata.multimedia.voice?>
			<?cs set:com_config = "1|1|1|0|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick"?>
		<?cs else?>
			<?cs #/*权限说说不能有转发按钮*/ ?>
			<?cs if:qz_metadata.orgdata.accessright == UGCFLAG_QQFRIEND || qz_metadata.orgdata.accessright == UGCFLAG_WHITELIST || qz_metadata.orgdata.accessright == UGCFLAG_ONLYSELF ?>			
				<?cs set:com_config = "1|1|1|0,b52,with_fwd,同时转发;0|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick"?>
			<?cs else?>
				<?cs set:com_config = "1|1|1|1,b52,with_fwd,同时转发;0|1,taotaoact.qzone.qq.com,@InputReply|1,taotaoact.qzone.qq.com,@ClickReply|1,taotaoact.qzone.qq.com,commentPresentClick"?>
			<?cs /if?>
		<?cs /if?>
	<?cs /if?>

	<?cs call:data_comments_inputbox(com_config, _mood_comm_param, qz_metadata.meta.userid, 
		"http://taotao.qq.com/cgi-bin/emotion_cgi_re_feeds", "utf-8")?>
<?cs /if?>

<?cs #}/*end: 评论框*/?>
