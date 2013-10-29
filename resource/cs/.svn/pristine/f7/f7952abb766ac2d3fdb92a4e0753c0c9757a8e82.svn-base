<?cs include:"wupcs-v8/data/photo/common.cs"?>
<?cs include:"wupcs-v8/data/photo/photo_title.cs"?>
<?cs include:"wupcs-v8/data/photo/photo_contentbox.cs"?>

<?cs #:生成auto为1的config ?>
<?cs def:make_reply_config(id,nick) ?>
	<?cs set:make_reply_config.ret = "{config:'1|1|0|0|0|0|0',targetUserInfo:{id:" + id + ",nick:'" + json_encode(nick, 1) + "',who:1,auto:1}}"?>
<?cs /def ?>

<?cs call:data_photo_title()?>
<?cs if:!(qz_metadata.feedtype==UC_WUP_FEED_TYPE_ACT_NOTIFYPSV && qz_metadata.orgdata.albumdata.iIsFromMultiFeeds==2)?>

	<?cs call:data_init_cntTitle()?>

	<?cs if:qz_metadata.meta.feedstype != UC_WUP_FEEDSTYPE_ACT ?>
		<?cs call:get_userWho_platform(qz_metadata.orgdata.platformid, qz_metadata.orgdata.platformsubid)?>
		<?cs call:data_cntTitle_nick(qz_metadata.orgdata.uin, get_userWho_platform.ret, qz_metadata.orgdata.nickname)?>
		<?cs call:data_cntTitle_tipTxt(":")?>
	<?cs /if?>

	<?cs call:get_photo_current_picindex() ?>
	<?cs #单图类的描述展示current pic对应的描述，多图类统一使用第一张描述 ?>
	<?cs if:qz_metadata.feedtype==UC_WUP_FEED_TYPE_NEWCOMMENT ||
			qz_metadata.meta.typeid==FEED_TYPE_PHOTO ||
			qz_metadata.meta.typeid==FEED_ABOUT_PHOTO ||
			qz_metadata.meta.typeid==FEED_TYPE_PHOTOCMT ||
			qz_metadata.meta.typeid==FEED_TYPE_AT_PHOTO
	?>
		<?cs call:data_cntTitle_rich(qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].desc)?>
	<?cs else ?>
		<?cs call:data_cntTitle_rich(qz_metadata.orgdata.itemdata[0].desc)?>
	<?cs /if?>
<?cs /if?>

<?cs call:data_photo_contentbox()?>
<?cs call:data_extend_content_lbs(qz_metadata.lbsdata) ?>

<?cs #生成浏览按钮?>
<?cs call:get_photo_current_picindex() ?>
<?cs if:qz_metadata.qz_data.key1.PRD.cnt>0 ?>
	<?cs #:如果批次相册feeds 就填批次的param ?>
	<?cs if:qz_metadata.orgdata.albumdata.sBatchUploadId ?>
			<?cs set:_photo_visitor_param = "4|33;" + qz_metadata.orgdata.albumdata.sAlbumId + "%7C" +
				  qz_metadata.orgdata.albumdata.sBatchUploadId + "|" +
				  qz_metadata.orgdata.uin?>
		<?cs else ?>
		<?cs set:_photo_visitor_param = "4|9;" + qz_metadata.orgdata.albumdata.sAlbumId + "%7C" +
					  qz_metadata.orgdata.itemdata[get_photo_current_picindex.ret].itemid + "|" +
					  qz_metadata.orgdata.uin?>
	<?cs /if ?>
	<?cs call:data_opr_visitor(_photo_visitor_param, qz_metadata.qz_data.key1.PRD.cnt)?>
<?cs /if?>

<?cs #相册使用默认的转发按钮?>
<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ACT_NOTIFYPSV ?>
	<?cs call:data_opr_forward()?>
	<?cs call:data_like()?>
	<?cs call:data_opr_delfeed()?>
<?cs /if?>

<?cs call:data_oprtime()?>	
<?cs call:data_source()?>
<?cs call:data_opr_prevent()?>
<?cs call:data_opr_collect()?>
<?cs call:data_opr_more()?>
<?cs call:data_privacy_icon()?>
<?cs call:get_photo_uin()?>

<?cs #生成回复cgi?>
<?cs call:get_reply_cgi_param(qz_metadata.orgdata.subtype)?>

<?cs #评论区的入口初始化函数?>
<?cs call:get_cmt_cgi_param(qz_metadata.orgdata.subtype)?>
<?cs call:data_comments(get_cmt_cgi_param.ret.addcgi, "GB")?>

<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL ||
		qz_metadata.feedtype == UC_WUP_FEEDS_TYPE_SHARETOME  ?>
	<?cs #被动评论的回复?>
	<?cs call:data_comment_replies(get_reply_cgi_param.ret.addcgi, "GB")?>
	
	<?cs #更多回复?>
	<?cs #设定more的参数调用必须在data_comment_loop_item或data_opcomment_item前面，因为后者加了一个限制：
		只对有moreUrl或者moreCgi的调用才会生成more入口数据?>
	<?cs call:get_morereply_cgi_param(qz_metadata.orgdata.subtype)?>
	<?cs call:data_commentReply_more(get_morereply_cgi_param.ret.morecgi,get_morereply_cgi_param.ret.moreurl)?>
	<?cs #初始化opinfo里的评论?>	
	<?cs call:data_opcomment_item("")?>
	<?cs #更多回复的参数。这必须在data_comment_item或者data_opcomment_item之后调用，因为它依赖前者生成的数据?>
	<?cs call:data_commentReply_more_param(get_morereply_cgi_param.ret.moreparam)?>
	

	<?cs #评论的审核或者评论的回复删除按钮2个按钮?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT ?>
		<?cs call:get_audit_cgi_param(qz_metadata.orgdata.subtype)?>
		<?cs call:data_comment_auditbtn("/qzone/photo/v7/page/photo.html#init=photo.v7/module/audit/feeds/index&",
			get_audit_cgi_param.ret.passparam)?>
		<?cs #{/*删除按钮继续注释老模板*/?>
		<?cs #if:qz_metadata.orgdata.uin == qz_metadata.meta.loginuin?>
			<?cs #call:data_comment_deletebtn(get_audit_cgi_param.ret.delcgi,get_audit_cgi_param.ret.delparam)?>
		<?cs #/if?>
		<?cs #}/**/?>
		<?cs #call:data_comment_deletebtn("","")?>
	<?cs elif:qz_metadata.meta.typeid != FEED_TYPE_FRI_PRIV_PASSIVE  ?>
		<?cs #/*除了指定人被动外，都支持评论的回复节点*/?>
		<?cs #/*生成评论的回复节点*/?>
		<?cs call:make_reply_config(qz_metadata.opinfo.t2body.uin,qz_metadata.opinfo.t2body.nickname) ?>
		<?cs set:data_comment_replybtn_v2.param.config= make_reply_config.ret ?>
		<?cs set:data_comment_replybtn_v2.param.param=get_reply_cgi_param.ret.addparam ?>
		<?cs set:data_comment_replybtn_v2.param.version="6.5" ?>
		<?cs call:data_comment_replybtn_v2(data_comment_replybtn_v2.param)?>

		<?cs #{/*删除按钮继续注释老模板*/?>
		<?cs #主人可以删除别人的评论，但是评论发表者不能删除自己的评论?>
		<?cs #if:qz_metadata.orgdata.uin == qz_metadata.meta.loginuin?>
			<?cs #call:data_comment_deletebtn(get_cmt_cgi_param.ret.delcgi, get_cmt_cgi_param.ret.delparam)?>
		<?cs #/if?>
		<?cs #}/*删除按钮继续注释老模板*/?>
		<?cs #call:data_comment_deletebtn("","")?>
	<?cs /if?>

	<?cs each:j = g_data_comments.replies[0].index?>
		<?cs #/*生成回复的回复节点*/?>
		<?cs call:data_commentReply_loop_item(j)?>
		<?cs #{/*删除按钮继续注释老模板*/?>
		<?cs #if:qz_metadata.orgdata.uin == qz_metadata.meta.loginuin?>
			<?cs #call:data_commentReply_deletebtn(get_reply_cgi_param.ret.delcgi[j] , get_reply_cgi_param.ret.delparam[j])?>
		<?cs #/if?>
		<?cs #}/*删除按钮继续注释老模板*/?>
		<?cs call:make_reply_config(qz_metadata.opinfo.t2body.vt3body[j].uin, qz_metadata.opinfo.t2body.vt3body[j].nickname) ?>
		<?cs call:data_commentReply_replybtn(make_reply_config.ret, get_reply_cgi_param.ret.addparam)?>
		<?cs #call:data_commentReply_deletebtn("","")?>
	<?cs /each ?>
<?cs else ?>	<?cs #主动?>
	<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV && qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY 
		&& qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ?>
		<?cs call:data_comments_showstranger(0)?><?cs #禁止展示陌生人的评论?>
	<?cs /if?>
	<?cs #主动评论的回复?>
	<?cs call:data_comment_replies(get_reply_cgi_param.ret.addcgi, "GB")?>
	<?cs #更多评论?>
	<?cs call:get_morecmt_cgi_param(qz_metadata.orgdata.subtype)?>
	
	
	<?cs #生成主动评论数据?>
	<?cs each:i = g_data_comments.index?>
	<?cs #loop: i = g_data_comments_start, g_data_comments_end, 1?>
		<?cs call:get_reply_cgi_param_bycid(qz_metadata.orgdata.subtype,i)?>
		
		<?cs #更多回复，主动里更多回复也用get_morecmt_cgi_param?>
		<?cs call:data_commentReply_more(get_morecmt_cgi_param.ret.morecgi,"")?>
		<?cs call:data_comment_loop_item(i)?>
		
		<?cs #生成单图评论小图标(仅针对多图主动)?>
		<?cs if:qz_metadata.vt2body[i].extendinfo.isPicCmt!=2 && (qz_metadata.feedtype==UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype==UC_WUP_FEED_TYPE_SHOW_ACTALL || qz_metadata.feedtype==UC_WUP_FEED_TYPE_SHOW_ACT_COMMALL) && qz_metadata.orgdata.albumdata.iIsFromMultiFeeds!=0?>
			<?cs call:get_photo_popup_paramByPicInfo(qz_metadata.vt2body[i].extendinfo.PiclargeId,qz_metadata.vt2body[i].extendinfo.LargeUrl)?>
			<?cs #call:get_photo_comment_picIndex(qz_metadata.vt2body[i].extendinfo.PiclargeId)?>
			<?cs call:data_comment_withImg_popup(0, "", "/qzone/photo/zone/icenter_popup.html", get_photo_popup_paramByPicInfo.ret, 2, "", "",qz_metadata.vt2body[i].extendinfo.SmallUrl, "") ?>
			<?cs call:data_comment_withImg_popup_v2(qz_metadata.orgdata.albumdata.sAlbumId, qz_metadata.vt2body[i].extendinfo.PiclargeId) ?>
			<?cs #call:data_comment_withIcon_popup(0, "", "/qzone/photo/zone/icenter_popup.html", get_photo_popup_paramByPicInfo.ret, 2, "", "", "", "") ?>	
			<?cs #新版浮层需要的参数 ?>
			<?cs call:data_popup_add_attr(_g_comment_qfv_curpath+".bfcnt.action", "imagesrc", qz_metadata.vt2body[i].extendinfo.LargeUrl) ?>
		<?cs /if?>
		<?cs call:make_reply_config(qz_metadata.vt2body[i].uin,qz_metadata.vt2body[i].nickname) ?>
		<?cs set:data_comment_replybtn_v2.param.config=make_reply_config.ret ?>
		<?cs set:data_comment_replybtn_v2.param.param=get_reply_cgi_param_bycid.ret.addparam ?>
		<?cs set:data_comment_replybtn_v2.param.version="6.2" ?>
		<?cs call:data_comment_replybtn_v2(data_comment_replybtn_v2.param)?>

		<?cs call:data_comment_deletebtn("","")?>

		<?cs #评论回复的more param都是一样的?>
		<?cs call:data_commentReply_more_param(get_morecmt_cgi_param.ret.moreparam)?>

		<?cs each:j = g_data_comments.replies[i].index?>
			<?cs #/*生成评论的回复节点*/?>
			<?cs call:data_commentReply_loop_item(j)?>
			<?cs call:make_reply_config(qz_metadata.vt2body[i].vt3body[j].uin, qz_metadata.vt2body[i].vt3body[j].nickname) ?>
			<?cs call:data_commentReply_replybtn(make_reply_config.ret, get_reply_cgi_param_bycid.ret.addparam)?>
			<?cs call:data_commentReply_deletebtn("","")?>
		<?cs /each?>
	<?cs /each?>
	<?cs call:data_comments_more(get_morecmt_cgi_param.ret.morecgi,get_morecmt_cgi_param.ret.morecgi,get_morecmt_cgi_param.ret.moreurl)?>
	<?cs call:data_comments_more_param(get_morecmt_cgi_param.ret.moreparam)?>
<?cs /if?>

<?cs #/*评论框*/?>
<?cs #/*调整t2count数目*/#?>
<?cs call:modify_pav_t2count()?>

<?cs #/*特殊逻辑在最前面实现（如feeds为指定人被动,圈人被动）*/#?>
<?cs if:qz_metadata.meta.typeid == FEED_TYPE_FRI_PRIV_PASSIVE || (qz_metadata.orgdata.subtype==PHOTO_subtype_picmark && qz_metadata.feedtype==UC_WUP_FEED_TYPE_COMMPSV) ?>
	<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", get_cmt_cgi_param.ret.addparam, qz_metadata.meta.userid, 
		 get_cmt_cgi_param.ret.addcgi, "GB")?>
<?cs #相册的评论回复被动，点“评论”或“评论框”走回复逻辑?>
<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ?>
	<?cs set:data_comments_inputbox_v2.param.param=get_cmt_cgi_param.ret.addparam ?>
	<?cs set:data_comments_inputbox_v2.param.charset="gbk" ?>
	<?cs set:data_comments_inputbox_v2.param.tuin=qz_metadata.meta.userid ?>
	<?cs set:data_comments_inputbox_v2.param.useReply=1 ?>
	<?cs set:data_comments_inputbox_v2.param.comuin=qz_metadata.opinfo.opuin ?>
	<?cs set:data_comments_inputbox_v2.param.comid=qz_metadata.opinfo.t2body.seq ?>
	<?cs call:data_comments_inputbox_v2(data_comments_inputbox_v2.param) ?>
<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT ?>
	<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", get_reply_cgi_param.ret.addparam, qz_metadata.meta.userid, 
									get_reply_cgi_param.ret.addcgi, "GB")?>
<?cs elif:qz_metadata.meta.typeid != FEED_TYPE_INDIVALBUM?>
	<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", get_cmt_cgi_param.ret.addparam, qz_metadata.meta.userid, 
		 get_cmt_cgi_param.ret.addcgi, "GB")?>
<?cs /if?>
<?cs #/*end: 评论框*/?>
<?cs #因为有个评论数依赖评论回复的过滤逻辑，所以这个要放到最后 ?>
<?cs call:data_opr_comment()?>
