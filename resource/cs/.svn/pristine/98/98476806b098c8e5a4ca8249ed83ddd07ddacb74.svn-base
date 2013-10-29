<?cs include:"wupcs-v8/data/share/const.cs"?>
<?cs include:"wupcs-v8/data/share/common.cs"?>
<?cs include:"wupcs-v8/data/share/share_contentbox.cs"?>
<?cs include:"wupcs-v8/data/share/share_title.cs"?>
<?cs include:"wupcs-v8/data/share/share_other.cs"?>

<?cs call:data_share_title()?>
<?cs call:data_share_subtitle()?>
<?cs call:data_share_contentbox()?>
<?cs #call:data_share_extendinfo()?>
<?cs call:data_share_operate()?>
<?cs call:data_listSameAction()?>

<?cs if:qz_metadata.feedstype == UC_WUP_FEEDSTYPE_PSV || qz_metadata.feedstype == UC_WUP_FEEDSTYPE_ABT?>
<?cs call:data_extendinfo_source("") ?>
<?cs /if?>

<?cs #生成浏览按钮?>

<?cs call:get_tuin_and_tid()?>
<?cs if:qz_metadata.qz_data.key1.PRD.cnt>0 ?>
	<?cs set:_share_visitor_param = qz_metadata.meta.appid + "|" +
					  get_tuin_and_tid.tid + "|" +
					  qz_metadata.meta.opuin?>
	<?cs call:data_opr_visitor(_share_visitor_param, qz_metadata.qz_data.key1.PRD.cnt)?>
<?cs /if?>

<?cs #日志使用默认的转发按钮?>
<?cs call:data_opr_forward()?>
<?cs call:data_oprtime()?>
<?cs call:data_source()?>
<?cs call:data_like()?>
<?cs call:data_opr_comment()?>
<?cs call:data_opr_prevent()?>
<?cs call:data_opr_collect()?>
<?cs call:data_opr_more() ?>
<?cs call:data_opr_delfeed()?>
<?cs call:data_share_detail_url()?>

<?cs call:data_comment_replies("http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareaddcomment", "utf-8")?>

<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL ?>

	<?cs call:data_comments("http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareaddcomment", "utf-8")?>

	<?cs #只添加更多回复的入口?>
	<?cs #设定more的参数调用必须在data_comment_loop_item或data_opcomment_item前面，因为后者加了一个限制：
		只对有moreUrl或者moreCgi的调用才会生成more入口数据?>
	<?cs call:data_commentReply_more("", data_share_detail_url.ret)?>

	<?cs #初始化一条评论?>
	<?cs call:data_opcomment_item("")?>
	<?cs call:data_commentReply_more_param("")?>

	<?cs if:string.length(qz_metadata.opinfo.t2body.extendinfo.pic_index) > 0 ?>
		<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_blog ?>

			<?cs call:get_userWho_platform(qz_metadata.orgdata.platformid, qz_metadata.orgdata.platformsubid)?>
			<?cs if:get_userWho_platform.ret == USER_PLATFORM_WHO_PY ?>
				<?cs set:blog_url = "http://baseapp.pengyou.com/" + qz_metadata.orgdata.uin 
					+ "/blog/" + qz_metadata.orgdata.mkey ?>
			<?cs else ?>
				<?cs set:blog_url = "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_blogdetail?blogurl=" 
					+ uri_encode(qz_metadata.orgdata.srcurl)
					+ "&shareuin=" + get_tuin_and_tid.uin 
					+ "&itemid=" + get_tuin_and_tid.tid 
					+ "&spaceuin=&cginame=&isfriend=1" ?>
			<?cs /if?>
			<?cs call:data_comment_withImg_url(0, blog_url) ?>
			<?cs #call:data_comment_withIcon_url(0, blog_url) ?>
		<?cs elif:subcount(qz_metadata.orgdata.itemdata) > qz_metadata.opinfo.t2body.extendinfo.pic_index
			&& subcount(qz_metadata.orgdata.itemdata[qz_metadata.opinfo.t2body.extendinfo.pic_index].picinfo) > 0 ?>
				<?cs set:icon_param = qz_metadata.orgdata.extendinfo.share_subtype + "|" 
					+ qz_metadata.opinfo.t2body.extendinfo.pic_index + "|"
					+ get_tuin_and_tid.uin + "|"
					+ get_tuin_and_tid.tid + "|"
					+ qz_metadata.orgdata.mkey + "|"
					+ qz_metadata.orgdata.platformid + "|"
					+ qz_metadata.orgdata.uin + "|"
					+ uri_encode(qz_metadata.orgdata.itemdata[qz_metadata.opinfo.t2body.extendinfo.pic_index].picinfo[0].url) ?>
				<?cs call:data_comment_withImg_popup(0, "", "/qzone/photo/zone/icenter_popup.html", icon_param, 2, "", "", qz_metadata.orgdata.itemdata[qz_metadata.opinfo.t2body.extendinfo.pic_index].picinfo[0].url, "") ?>
				<?cs #call:data_comment_withIcon_popup(0, "", "/qzone/photo/zone/icenter_popup.html", icon_param, 2, "", "", "", "") ?>
		<?cs /if?>
	<?cs /if?>

	<?cs #主人可以删除别人的评论，但是评论发表者不能删除自己的评论?>
	<?cs call:_share_psv_commentReply_param(qz_metadata.opinfo.t2body)?>
	<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", _share_psv_commentReply_param.ret)?>

	<?cs #call:data_comment_deletebtn("","")?>

	<?cs each:j = g_data_comments.replies[0].index?>
		<?cs #/*生成评论的回复节点*/?>
		<?cs call:data_commentReply_loop_item(j)?>

		<?cs #{/*二级评论再回复*/?>
		<?cs call:data_commentReply_replybtn("1|1|0|0|0|0|0", _share_psv_commentReply_param.ret) ?>

		<?cs #/*二级评论再回复的删除按钮*/?>
		<?cs #call:data_commentReply_deletebtn("","") ?>
	<?cs /each?>
<?cs else ?><?cs #主动?>

	<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV && qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY 
		&& qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ?>
		<?cs call:data_comments_showstranger(0)?><?cs #禁止展示陌生人的评论?>
	<?cs /if?>

	<?cs call:data_comments("http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareaddcomment", "utf-8")?>

	<?cs call:data_comments_more("", "", data_share_detail_url.ret)?>
	<?cs call:data_comments_more_param("")?>

	<?cs each:i = g_data_comments.index?>
		<?cs call:data_comment_loop_item(i)?>

		<?cs if:string.length(qz_metadata.vt2body[i].extendinfo.pic_index) > 0 ?>
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_blog ?>

				<?cs call:get_userWho_platform(qz_metadata.orgdata.platformid, qz_metadata.orgdata.platformsubid)?>
				<?cs if:get_userWho_platform.ret == USER_PLATFORM_WHO_PY ?>
					<?cs set:blog_url = "http://baseapp.pengyou.com/" + qz_metadata.orgdata.uin 
						+ "/blog/" + qz_metadata.orgdata.mkey ?>
				<?cs else ?>
					<?cs set:blog_url = "http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_blogdetail?blogurl=" 
						+ uri_encode(qz_metadata.orgdata.srcurl)
						+ "&shareuin=" + get_tuin_and_tid.uin 
						+ "&itemid=" + get_tuin_and_tid.tid 
						+ "&spaceuin=&cginame=&isfriend=1" ?>
				<?cs /if?>
				<?cs call:data_comment_withImg_url(i, blog_url) ?>
				<?cs #call:data_comment_withIcon_url(i, blog_url) ?>
			<?cs elif:subcount(qz_metadata.orgdata.itemdata) > qz_metadata.vt2body[i].extendinfo.pic_index
				&& subcount(qz_metadata.orgdata.itemdata[qz_metadata.vt2body[i].extendinfo.pic_index].picinfo) > 0 ?>
					<?cs set:icon_param = qz_metadata.orgdata.extendinfo.share_subtype + "|" 
						+ qz_metadata.vt2body[i].extendinfo.pic_index + "|"
						+ get_tuin_and_tid.uin + "|"
						+ get_tuin_and_tid.tid + "|"
						+ qz_metadata.orgdata.mkey + "|"
						+ qz_metadata.orgdata.platformid + "|"
						+ qz_metadata.orgdata.uin + "|"
						+ uri_encode(qz_metadata.orgdata.itemdata[qz_metadata.vt2body[i].extendinfo.pic_index].picinfo[0].url) ?>
					<?cs call:data_comment_withImg_popup(0, "", "/qzone/photo/zone/icenter_popup.html", icon_param, 2, "", "", qz_metadata.orgdata.itemdata[qz_metadata.vt2body[i].extendinfo.pic_index].picinfo[0].url, "") ?>
					<?cs call:data_comment_withIcon_popup(i, "", "/qzone/photo/zone/icenter_popup.html", icon_param, 2, "", "", "", "") ?>
			<?cs /if?>
		<?cs /if?>

		<?cs call:_share_psv_commentReply_param(qz_metadata.vt2body[i])?>
		<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", _share_psv_commentReply_param.ret)?>

		<?cs call:data_comment_deletebtn("","")?>

		<?cs #评论回复的more param都是一样的?>
		<?cs call:data_commentReply_more_param("")?>

		<?cs each:j = g_data_comments.replies[i].index?>
			<?cs #/*生成评论的回复节点*/?>
			<?cs call:data_commentReply_loop_item(j)?>

			<?cs #{/*二级评论再回复*/?>
			<?cs call:data_commentReply_replybtn("1|1|0|0|0|0|0", _share_psv_commentReply_param.ret) ?>

			<?cs #/*二级评论再回复的删除按钮*/?>
			<?cs call:data_commentReply_deletebtn("","") ?>
		<?cs /each?>
	<?cs /each?>
<?cs /if?>

<?cs #{/*评论框*/?>
<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV
	|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT  || qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL ?>
	<?cs call:_share_psv_commentReply_param(qz_metadata.opinfo.t2body)?>
	<?cs set:data_comments_inputbox_v2.param.config="1|1|0|0|0|0|0" ?>
	<?cs set:data_comments_inputbox_v2.param.param=_share_psv_commentReply_param.ret ?>
	<?cs set:data_comments_inputbox_v2.param.charset="utf-8" ?>
	<?cs set:data_comments_inputbox_v2.param.tuin=qz_metadata.meta.userid ?>
	<?cs set:data_comments_inputbox_v2.param.useReply=1 ?>
	<?cs set:data_comments_inputbox_v2.param.comuin=qz_metadata.opinfo.opuin ?>
	<?cs set:data_comments_inputbox_v2.param.comid=qz_metadata.opinfo.t2body.seq ?>
	<?cs call:data_comments_inputbox_v2(data_comments_inputbox_v2.param) ?>
<?cs else ?>
	<?cs call:_share_psv_comment_param()?>
	<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", _share_psv_comment_param.ret, qz_metadata.meta.userid, 
		"http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareaddcomment", "utf-8")?>
<?cs /if?>
<?cs #}/*end: 评论框*/?>
