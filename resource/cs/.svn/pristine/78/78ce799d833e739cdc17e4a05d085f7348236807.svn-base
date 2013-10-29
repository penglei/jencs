<?cs include:"wupcs-v8/data/blog/common.cs"?>
<?cs include:"wupcs-v8/data/blog/blog_title.cs"?>
<?cs include:"wupcs-v8/data/blog/blog_contentbox.cs"?>

<?cs call:data_blog_title()?>
<?cs call:data_blog_contentbox()?>

<?cs #生成浏览按钮?>
<?cs if:qz_metadata.qz_data.key1.RZRD.cnt>0 ?>
	<?cs #:转载的日志 ?>
	<?cs if:subcount(qz_metadata.relybody)>0 ?>
		<?cs set:_blog_visitor_param = qz_metadata.meta.appid + "|" +
						  qz_metadata.relybody.0.mkey + "|" +
						  qz_metadata.relybody.0.uin?>
	<?cs else ?>
		<?cs set:_blog_visitor_param = qz_metadata.meta.appid + "|" +
						  qz_metadata.orgdata.mkey + "|" +
						  qz_metadata.meta.opuin?>
	<?cs /if ?>

	<?cs call:data_opr_visitor(_blog_visitor_param, qz_metadata.qz_data.key1.RZRD.cnt)?>
<?cs /if?>

<?cs #日志使用默认的转发按钮?>
<?cs call:data_opr_forward()?>

<?cs call:data_oprtime()?>
<?cs call:data_source()?>
<?cs call:data_like()?>
<?cs call:data_opr_comment()?>
<?cs call:data_privacy_icon()?>
<?cs call:data_opr_prevent()?>
<?cs call:data_opr_collect()?>
<?cs call:data_opr_more()?>
<?cs call:data_opr_delfeed()?>
<?cs call:get_tuin_and_tid()?>


<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>

<?cs ####
	/**
	 *评论日志中的图片，增加小图片
	 */
?>
<?cs def:data_blog_comment_pic_icon(commentItem)?>
	<?cs if:commentItem.extendinfo.comm_pic?>
		<?cs #日志评论缩略图给干掉?>
		<?cs #call:data_comment_withImg_url(
				"",
				commentItem.extendinfo.comm_pic)?>
		<?cs #call:data_comment_withIcon_url(
				"",
				commentItem.extendinfo.comm_pic)?>
	<?cs /if?>
<?cs /def?>

<?cs call:data_comment_replies("http://b.qzone.qq.com/cgi-bin/blognew/blog_reply_comment", "GB")?>

<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT ||
		qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL ?>

	<?cs call:data_comments("http://b.qzone.qq.com/cgi-bin/blognew/blog_add_comment", "GB")?>

	<?cs #只添加更多回复的入口?>
	<?cs #设定more的参数调用必须在data_comment_loop_item或data_opcomment_item前面，因为后者加了一个限制：
		只对有moreUrl或者moreCgi的调用才会生成more入口数据?>
	<?cs call:data_commentReply_more("http://b.qzone.qq.com/cgi-bin/blognew/blog_generate_feeds",
									get_blog_url.ret)?>
	<?cs #初始化一条评论?>
	<?cs call:data_opcomment_item("")?>
	<?cs #更多评论的参数。这必须在data_comment_item或者data_opcomment_item之后调用，因为它依赖前者生成的数据?>
	<?cs set:more_param = get_tuin_and_tid.uin + ","
						+ get_tuin_and_tid.tid + ",1,"
						+ qz_metadata.opinfo.t2body.seq + ","
						+ UC_WUP_FEED_TYPE_SHOW_PSVALL ?>
	<?cs call:data_commentReply_more_param(more_param)?>

	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT ?>
		<?cs call:data_comment_auditbtn("/qzone/newblog/v5/info/audit_blog_pass.html",
			qz_metadata.opinfo.t2body.extendinfo.padding)?>

		<?cs #{/*老的删除按钮逻辑给注释掉，参数前台拼凑*/?>	
		<?cs #if:qz_metadata.orgdata.uin == qz_metadata.meta.loginuin?>
			<?cs #call:data_comment_deletebtn("http://b.qzone.qq.com/cgi-bin/blognew/blog_del_unverify",
											qz_metadata.opinfo.t2body.extendinfo.padding)?>
		<?cs #/if?>
		<?cs #}/**/?>
	<?cs else ?>
		<?cs #主人可以删除别人的评论，但是评论发表者不能删除自己的评论?>
		<?cs call:_blog_psv_commentReply_param(qz_metadata.opinfo.t2body)?>
		<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", _blog_psv_commentReply_param.ret)?>

		<?cs #{/*老的删除按钮逻辑给注释掉，参数前台拼凑*/?>	
		<?cs #if:qz_metadata.orgdata.uin == qz_metadata.meta.loginuin?>
			<?cs #set:delete_param = qz_metadata.opinfo.t2body.uin + ","
									+ get_tuin_and_tid.tid + ","
									+ qz_metadata.opinfo.t2body.seq + ",-1"  ?>
			<?cs #call:data_comment_deletebtn("http://b.qzone.qq.com/cgi-bin/blognew/blog_del_comment_ic", delete_param)?>
		<?cs #/if?>
		<?cs #}/**/?>
	<?cs /if?>

	<?cs #call:data_comment_deletebtn("","")?>

	<?cs call:data_blog_comment_pic_icon(qz_metadata.opinfo.t2body)?>

	<?cs each:j = g_data_comments.replies[0].index?>
	<?cs #loop:j = g_data_commentReplies_start, g_data_commentReplies_end, 1?>
		<?cs #/*生成评论的回复节点*/?>
		<?cs call:data_commentReply_loop_item(j)?>

		<?cs #{/*老的删除按钮逻辑给注释掉，参数前台拼凑*/?>
		<?cs #if:qz_metadata.orgdata.uin == qz_metadata.meta.loginuin?>
			<?cs #set:delete_param = get_tuin_and_tid.uin + ","
									+ get_tuin_and_tid.tid + ","
									+ qz_metadata.opinfo.t2body.seq + ",0,"
									+ qz_metadata.opinfo.t2body.vt3body[j].uin + ","
									+ qz_metadata.opinfo.t2body.vt3body[j].ctime + ","
									+ qz_metadata.feedtype ?>
			<?cs #call:data_commentReply_deletebtn("http://b.qzone.qq.com/cgi-bin/blognew/blog_del_replycomment", delete_param)?>
		<?cs #/if?>
		<?cs #}/**/?>
		<?cs #call:data_commentReply_deletebtn("","")?>
	<?cs #/loop?>
	<?cs /each?>
<?cs else ?><?cs #主动?>

	<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV && qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY 
		&& qz_metadata.feedtype != UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ?>
		<?cs call:data_comments_showstranger(0)?><?cs #禁止展示陌生人的评论?>
	<?cs /if?>
	<?cs call:data_comments("http://b.qzone.qq.com/cgi-bin/blognew/blog_add_comment", "GB")?>

	<?cs call:data_comments_more("http://b.qzone.qq.com/cgi-bin/blognew/blog_generate_feeds",
								"http://b.qzone.qq.com/cgi-bin/blognew/blog_generate_feeds",
								get_blog_url.ret)?>
	<?cs set:more_param = get_tuin_and_tid.uin + "," + get_tuin_and_tid.tid + ",0,0," + UC_WUP_FEED_TYPE_SHOW_ACT_COMMALL ?>
	<?cs call:data_comments_more_param(more_param)?>

	<?cs each:i = g_data_comments.index?>
	<?cs #loop: i = g_data_comments_start, g_data_comments_end, 1?>
		<?cs call:data_comment_loop_item(i)?>

		<?cs call:data_blog_comment_pic_icon(qz_metadata.vt2body[i])?>

		<?cs call:_blog_psv_commentReply_param(qz_metadata.vt2body[i])?>
		<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", _blog_psv_commentReply_param.ret)?>

		<?cs #主动feeds暂时不要删除按钮?>
		<?cs #set:delete_param = qz_metadata.vt2body[i].uin + ","
			+ get_tuin_and_tid.tid + ","
			+ qz_metadata.vt2body[i].seq + ",-1"  ?>
		<?cs #call:data_comment_deletebtn("http://b.qzone.qq.com/cgi-bin/blognew/blog_del_comment_ic", delete_param)?>

		<?cs call:data_comment_deletebtn("","")?>

		<?cs #评论回复的more param都是一样的?>
		<?cs call:data_commentReply_more_param(more_param)?>

		<?cs each:j = g_data_comments.replies[i].index?>
		<?cs #loop:j = g_data_commentReplies_start, g_data_commentReplies_end, 1?>
			<?cs #/*生成评论的回复节点*/?>
			<?cs call:data_commentReply_loop_item(j)?>

			<?cs #主动feeds暂时不要删除按钮?>
			<?cs #set:delete_param = get_tuin_and_tid.uin + "," 
				+ get_tuin_and_tid.tid + ","
				+ qz_metadata.vt2body[i].seq + ",0,"
				+ qz_metadata.vt2body[i].vt3body[j].uin + "," 
				+ qz_metadata.vt2body[i].vt3body[j].ctime + ","
				+ qz_metadata.feedtype ?>
			<?cs #call:data_commentReply_deletebtn("http://b.qzone.qq.com/cgi-bin/blognew/blog_del_replycomment", delete_param)?>
			<?cs call:data_commentReply_deletebtn("","")?>
		<?cs #/loop?>
		<?cs /each?>
	<?cs #/loop?>
	<?cs /each?>
<?cs /if?>

<?cs #{/*评论框*/?>
<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV
	|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHOW_PSVALL ?>
	<?cs call:_blog_psv_commentReply_param(qz_metadata.opinfo.t2body)?>
	<?cs set:data_comments_inputbox_v2.param.config="1|1|0|0|0|0|0" ?>
	<?cs set:data_comments_inputbox_v2.param.param=_blog_psv_commentReply_param.ret ?>
	<?cs set:data_comments_inputbox_v2.param.charset="GB" ?>
	<?cs set:data_comments_inputbox_v2.param.tuin=qz_metadata.meta.userid ?>
	<?cs set:data_comments_inputbox_v2.param.useReply=1 ?>
	<?cs set:data_comments_inputbox_v2.param.comuin=qz_metadata.opinfo.opuin ?>
	<?cs set:data_comments_inputbox_v2.param.comid=qz_metadata.opinfo.t2body.seq ?>
	<?cs call:data_comments_inputbox_v2(data_comments_inputbox_v2.param) ?>
<?cs else ?>
	<?cs call:_blog_psv_comments_param()?>
	<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", _blog_psv_comments_param.ret, qz_metadata.meta.userid, 
		"http://b.qzone.qq.com/cgi-bin/blognew/blog_add_comment", "GB")?>
<?cs /if?>


<?cs #}/*end: 评论框*/?>
