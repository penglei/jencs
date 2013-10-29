<?cs include:"wupcs/modata/comments_filter.cs" ?>
<?cs #### 
	/**
	 * @class 评论回复数据组件
	 * 变量说明:
	 *  {Bool}		_g_comments_show_stranger		是否展示陌生人的评论

	 *  {Number} 	_g_comment_qfv_index			当前评论生成的数据下标
	 *  {String}	_g_comment_qfv_curpath			当前评论生成的数据路径

	 * 	{Number}	_g_commentReply_qfv_index		当前回复生成的数据下标
	 *  {String} 	_g_commentReply_qfv_curpath		当前回复生成的数据路径
	
	 *  {String}	_g_comment_cur_replies_path		当前评论生成的回复列表数据路径

	 *  {Bool}		g_data_comments_moreflag		是否有更多评论
	 *  {Bool}		g_data_commentReply_moreflag	当前评论是否有更多回复
	 *  {String}	_g_data_comments_more_cgi		查看更多评论的cgi
	 *  {String}	_g_data_comments_moreReply_cgi	查看更多回复的cgi
	 *  {String}	g_data_comments_cgi				评论使用的cg
	 *  {String}	g_data_commentReply_cgi			回复使用的cgi
	 *  {String}	_g_data_comments_more_url		查看更多评论/回复的url
	 *  {Number}	_g_comment_meta_index			遍历一级评论时的当前元数据下标
	 *  {Number}	_g_commentReply_meta_index		遍历评论的回复时当前回复的元数据下标
	 *  {String}	_g_comment_meta_path			当前评论的元数据路径（主要是因为opinfo和vt2body引的差异）
	 *  {String}	_g_commentReply_meta_path		当前回复的元数据路径
	 * @example
	 * 评论回复生成例子
	 * 	<<cs #### 固定步骤1 >>
	 * 	<<cs call:data_comment_replies("http://b.qzone.qq.com/cgi-bin/blognew/blog_reply_comment", "GB")>>
	 * 	<<cs #### 固定步骤2 >>
	 * 	<<cs call:data_comments("http://b.qzone.qq.com/cgi-bin/blognew/blog_add_comment", "GB")>>
	 * 	<<cs #### 如果只是想设置评论的拉more的cgi，或者只是想设置回复的拉more的cgi，只需传对于的参数就行，其它参数可以用空串 >>
	 * 	<<cs #### 固定步骤3 >>
	 * 	<<cs call:data_comments_more("http://b.qzone.qq.com/cgi-bin/blognew/blog_generate_feeds",
							"http://b.qzone.qq.com/cgi-bin/blognew/blog_generate_feeds",
							get_blog_url.ret)>>
		<<cs #### 固定步骤4 >>
	 *	<<cs call:data_comments_more_param(more_param)>>
	 *	<<cs # g_data_comments.index存放了允许展示的评论数据  >>
	 * 	<<cs each:i = g_data_comments.index>>
	 * 		<<cs #### 一定要先调用这个 >>
	 *		<<cs call:data_comment_loop_item(i)>>
	 *		<<cs #### 然后下面可以调用设置各种需要用到的按钮了 >>
	 *		<<cs call:_blog_psv_commentReply_param(qz_metadata.vt2body[i])>>
	 *		<<cs call:data_comment_replybtn("1|1|0|0|0|0|0", _blog_psv_commentReply_param.ret)>>
	 *		<<cs #评论回复的more param都是一样的>>
	 *		<<cs call:data_commentReply_more_param(参数)>>
	 *		<<cs #### 设置完改评论的各种按钮之后，得处理下这条评论的回复了 >>
	 *		<<cs each:j = g_data_comments.replies[i].index>>
	 *			<<cs #一定要先调用这个生成评论的回复节点>>
	 *			<<cs call:data_commentReply_loop_item(j)>>
	 *			<<cs #### 然后下面可以调用设置各种需要用到的按钮了 >>
	 *			<<cs #主动feeds暂时不要删除按钮>>
	 *			<<cs #set:delete_param = get_blog_uin_and_blogid.uin + "," 
						+ get_blog_uin_and_blogid.blogid + ","
						+ qz_metadata.vt2body[i].seq + ",0,"
						+ qz_metadata.vt2body[i].vt3body[j].uin + "," 
						+ qz_metadata.vt2body[i].vt3body[j].ctime + ","
						+ qz_metadata.feedtype >>
	 *			<<cs #call:data_commentReply_deletebtn("http://b.qzone.qq.com/cgi-bin/blognew/blog_del_replycomment", delete_param)>>
	 *		<<cs /each>>
	 *	<<cs /each>>
	 */
	var 评论回复数据组件=function(){};
?>
<?cs ##
	/**
	 * @constant
	 * @description 评论回复的最大展示数
	 */
	G_COMMENTS_MAX=""
?>
<?cs ####
	/*
	 * @namespace 评论回复数据组件
	 * @memberOf 评论回复数据组件
	 * @private
	 * @description 评论组件内部初始化
	 * 每条feeds都只有一个评论列表，在组装评论列表时必须先调用此方法初始化一下
	 */
	_data_comments_init:function(){}
?>
<?cs def:_data_comments_init()?>
	<?cs ####/*
		_g_comments_show_stranger		是否展示陌生人的评论

		_g_comment_qfv_index			当前评论生成的数据下标
		_g_comment_qfv_curpath			当前评论生成的数据路径

		_g_commentReply_qfv_index		当前回复生成的数据下标
		_g_commentReply_qfv_curpath		当前回复生成的数据路径

		_g_comment_cur_replies_path		当前评论生成的回复列表数据路径

		g_data_comments_moreflag		是否有更多评论
		g_data_commentReply_moreflag	当前评论是否有更多回复

		_g_data_comments_more_cgi		查看更多评论的cgi
		_g_data_comments_moreReply_cgi	查看更多回复的cgi

		g_data_comments_cgi				评论使用的cgi
		g_data_commentReply_cgi			回复使用的cgi

		_g_data_comments_more_url		查看更多评论/回复的url

		_g_commentReply_meta_index		遍历评论的回复时当前回复的元数据下标

		_g_comment_meta_path			当前评论的元数据路径（主要是因为opinfo和vt2body引的差异）
		_g_commentReply_meta_path		当前回复的元数据路径
		*/
	?>

	<?cs #生成viewdata时评论的下标.注意，是-1，**因为生成评论循环一开始要加1**，才能变成从0开始?>
	<?cs #为什么要从-1开始呢? 是因为生成评论的循环在外层（业务模板），组件内部不容易判断出循环结束。（正常的话应该是循环结束时才对其加1)?>
	<?cs set:_g_comment_qfv_index = -1?>

	<?cs set:_g_comments_show_stranger = 1?><?cs #默认显示陌生人的评论?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @private
	 * @description 本私有方法仅供在接口{ @link data_comments_more }中调用。若评论超过三条，要加查看更多评论的入口。本方法用来设置该入口需调用的cgi或者跳转地址
	 * @param {String} cgi 拉取更多的cgi地址。若在点击跳转的场景下，此项可传空串
	 * @param {String} url 点击跳转的地址。若在点击拉取更多的场景下，此项可传空串
	 */
	_data_comments_more:function(){}
?>
<?cs def:_data_comments_more(cgi, url)?>
	<?cs if:cgi || url?>

		<?cs if:qz_metadata.vt2count > g_data_comments_count_show + 0 + filter_comment.ret.disabled_count?>
			<?cs #/*默认是三条*/?>
			<?cs call:qfv("comments.more.count", qz_metadata.vt2count - g_data_comments_count_show - 0 - filter_comment.ret.disabled_count)?>
			<?cs set:g_data_comments_moreflag = 1?>

			<?cs if:g_showrefer_more_comments?>
				<?cs #已经是展开更多评论的假feeds。还有更多评论时需要跳转到详情页查看?>
				<?cs if:url?>
					<?cs call:qfv("comments.more.url", url)?>
				<?cs /if?>
			<?cs else ?>
				<?cs if:cgi?>
					<?cs call:qfv("comments.more.action", cgi)?>
				<?cs /if?>
				<?cs #展示组件里会优先使用cgi模板展开更多，所以指定url也不影响?>
				<?cs #!!但是这里要考虑一下被动feeds。只有一条评论，因为数据是全的，会不会出现展示更多评论的入口?>
				<?cs if:url?>
					<?cs call:qfv("comments.more.url", url)?>
				<?cs /if?>
			<?cs /if?>

		<?cs else ?>
			<?cs set:g_data_comments_moreflag = 0?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 设置"查看更多评论/回复"的完整数据块接口。设定查看更多评论/回复的参数
	 * @param {String} commentMoreCgi 拉取更多评论的cgi地址。若在点击跳转的场景下，此项可传空串
	 * @param {String} replyMoreCgi 拉取更多回复的cgi地址。若在点击跳转的场景下，此项可传空串
	 * @param {String} moreUrl 点击跳转的地址。若在点击拉取更多的场景下，此项可传空串
	 */
	data_comments_more:function(){}
?>
<?cs def:data_comments_more(commentMoreCgi, replyMoreCgi, moreUrl)?>
	<?cs set:_g_data_comments_more_cgi = commentMoreCgi?>
	<?cs set:_g_data_comments_moreReply_cgi = replyMoreCgi?>

	<?cs set:_g_data_comments_more_url = moreUrl?>

	<?cs #/*根据展示的评论条数是否超过最大值来决定是否生成more信息*/?>
	<?cs call:_data_comments_more(commentMoreCgi, moreUrl)?>
<?cs /def?>

<?cs ####
/**
 *	@memberOf 评论回复数据组件
 *	@deprecated 从2013年3月20号开始，废弃这个函数，目前可能用到的业务有礼物，分享，日志。后续增加的业务如再使用这个方法，请自行修改组件提供方不提供维护。代替方法见@{link data_comments_more}
 */
 data_commentReply_more:function(){}
?>
<?cs def:data_commentReply_more(replyMoreCgi, moreUrl)?>
	<?cs set:_g_data_comments_moreReply_cgi = replyMoreCgi?>
	<?cs set:_g_data_comments_more_url = moreUrl?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 设置查看更多评论使用的参数,每条feed中只有一个。其实后续可以统一收到@{link data_comments_more}这样的接口中来完成即可
	 * @param {String} param 展开更多的参数，各个业务都不同.
	 */
	data_comments_more_param:function(){}
?>
<?cs def:data_comments_more_param(param)?>
	<?cs if:g_data_comments_moreflag?>
		<?cs call:qfv("comments.more.param", param)?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 设置查看更多回复使用的参数
	 * @param {String} param 展开更多的参数，各个业务都不同.
	 */
	data_commentReply_more_param:function(){}
?>
<?cs def:data_commentReply_more_param(param)?>
	<?cs if:g_data_commentReply_moreflag?>
		<?cs call:set(_g_comment_cur_replies_path, "more.param", param)?>
	<?cs /if?>
<?cs /def?>

<?cs #{/*---评论公用小组件*/?>
<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @private
	 * @description 在指定路径上生成评论回复正文数据
	 * @param {String} path 数据路径
	 * @param {Object} item wup元数据中的t2body或者t3body
	 * @example
	 * <<cs call:data_comments_content("comments.comment.0",t2body) >>
	 * 生成:
	 * qfv.comments.comment.0.content.con.0.text=XXXXXXXX
	 * qfv.comments.comment.0.content.con.0.type=txt
	 * qfc.comments.comment.0.content.con.1.type=XXXXXX
	 * ......
	 */
	data_comments_content:function(){}
?>

<?cs def:data_comments_content(path, item)?>
	<?cs loop:i = 0, subcount(item.msg) - 1, 1?>
		<?cs call:data_rich_msg(path + ".content.con." + i, item.msg[i], "", 0)?>
		<?cs if:item.msg[i].type == 1?><?cs #是@结构，加一个前缀?>
			<?cs call:data_nick_addprefix(data_rich_msg.ret.path, "@")?>
		<?cs /if?>
	<?cs /loop?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 在指定路径中生成评论回复的作者的数据块
	 * @param {String} path 数据路径
	 * @param {Object} itemUser wup元数据中的t2body或者t3body
	 * @example 
	 * <<cs call:data_comments_user("comments.comment.0",t2body) >>
	 * 生成
	 * qfv.comments.comment.0.user.who=1
	 * qfv.comments.comment.0.user.uin=12345667
	 * qfv.comments.comment.0.user.nickname=nickname
	 */
	data_comments_user:function(){}
?>
<?cs def:data_comments_user(path, itemUser)?>
	<?cs call:get_userWho_platform(itemUser.platformid, itemUser.platformsubid)?>
	<?cs if:get_userWho_platform.ret == USER_PLATFORM_WHO_PY?>
		<?cs call:set(path, "user.img", itemUser.extendinfo.xyimg)?>
	<?cs /if?>
	<?cs call:data_con_nick(path + ".user",
					itemUser.uin,
					get_userWho_platform.ret,
					itemUser.nickname, "", 0
				)?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @private
	 * @description 在指定路径中生成评论回复对象的数据（xxx 回复 yyy，其中的"yyy"）
	 * @param {String} path 数据生成路径
	 * @param {Object} item wup元数据中的t2body或者t3body
	 * @example
	 * <<cs call:data_comments_targetUser("comments.comment.0.replies.reply.0",t3body) >>
	 * 生成：
	 * qfv.comments.comment.0.replies.reply.0.targetUser.uin=XXXXXX
	 * qfv.comments.comment.0.replies.reply.0.targetUser.who=XXXXX
	 * qfv.comments.comment.0.replies.reply.0.targetUser.nickname=XXXXXXXX
	 */
	data_comments_targetUser:function(){}
?>
<?cs def:data_comments_targetUser(path, item)?>
	<?cs if:item.extendinfo.target_uin && item.extendinfo.target_uin != item.uin?>
		<?cs call:data_con_nick(path + ".targetUser",
					item.extendinfo.target_uin,
					item.extendinfo.target_who,
					item.extendinfo.target_nickname,
					"link", 0
					)?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @private
	 * @description 在指定路径中生成评论时间的节点
	 * @param {String} path 数据生成路径
	 * @param {Object} item item wup元数据中的t2body或者t3body
	 * @example
	 * <<cs call:data_comments_time("comments.comment.0.replies.reply.0",t3body) >>
	 * 生成：
	 * qfv.comments.comment.0.time.mr=XXXXXXX
	 * qfv.comments.comment.0.time.type=txt
	 * qfv.comments.comment.0.time.color=tip
	 * qfv.comments.comment.0.time.text=
	 * qfv.comments.comment.0.time.abstime=XXXXXXXX
	 */
	data_comments_time:function(){}
?>
<?cs def:data_comments_time(path, item)?>
	<?cs call:data_con_txt(path + ".time", item.strtime, "tip", "10")?>
	<?cs call:set(path, "time.abstime", item.ctime)?>
<?cs /def?>
<?cs #}/*end: ---评论公用小组件*/?>

<?cs #{/*一级评论的回复按钮参数*/?>
<?cs #####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 扩展接口，除非是特殊情况下需要增加特殊属性，否则无需使用。在_g_comment_qfv_curpath指向的路径中设置回复按钮的属性接口
	 * @param {String} name 属性名城
	 * @param {String} val 属性值
	 * @example
	 * <<cs call:data_comment_replybtn_attr("param","paramString") >>
	 * 生成：
	 * qfv.comments.comment.0.replybtn.param=paramString
	 * <<cs call:data_comment_replybtn_attr("charset","utf8") >>
	 * qfv.comments.comment.0.replybtn.param=paramString
	 */
	data_comment_replybtn_attr:function(){}
?>
<?cs def:data_comment_replybtn_attr(name, val)?>
	<?cs call:set(_g_comment_qfv_curpath + ".replybtn", name, val)?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @requires  g_data_commentReply_cgi、g_data_commentReply_charset
	 * @description 设置当前评论的回复按钮数据.
	 * @param {String} config 评论回复按钮的config属性，貌似是各个业务不同的
	 * @param {String} param 评论回复按钮的param属性。各个业务不同
	 */
	data_comment_replybtn:function(){}
?>
<?cs def:data_comment_replybtn(config, param)?>
	<?cs call:set(_g_comment_qfv_curpath + ".replybtn", "action", g_data_commentReply_cgi)?>
	<?cs call:set(_g_comment_qfv_curpath + ".replybtn", "config", config)?>
	<?cs call:set(_g_comment_qfv_curpath + ".replybtn", "param", param)?>
	<?cs call:set(_g_comment_qfv_curpath + ".replybtn", "charset", g_data_commentReply_charset)?>
	<?cs call:data_comment_replybtn_attr("version", "6.2")?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @requires  g_data_commentReply_cgi、g_data_commentReply_charset
	 * @description 新设置当前评论的回复按钮数据.
	 * @param {Object} param 评论回复按钮属性
	 * @param {String} param.config 评论回复按钮的config属性，貌似是各个业务不同的
	 * @param {String} param.param 评论回复按钮的param属性。各个业务不同
	 * @param {String} param.version 评论回复按钮的version属性，若不传默认6.2
	 */
	data_comment_replybtn:function(){}
?>
<?cs def:data_comment_replybtn_v2(param)?>
	<?cs if: !param.version ?>
		<?cs set:param.version=6.2 ?>
	<?cs /if ?>
	<?cs call:set(_g_comment_qfv_curpath + ".replybtn", "action", g_data_commentReply_cgi)?>
	<?cs call:set(_g_comment_qfv_curpath + ".replybtn", "config", param.config)?>
	<?cs call:set(_g_comment_qfv_curpath + ".replybtn", "param", param.param)?>
	<?cs call:set(_g_comment_qfv_curpath + ".replybtn", "charset", g_data_commentReply_charset)?>
	<?cs call:data_comment_replybtn_attr("version", param.version)?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 在_g_comment_qfv_curpath指向的当前路径中生成生成删除评论的按钮数据。只有在被动里面，并且feed对应的原贴是登录态uin所属，才可以删除评论。并且，主人可以删除其它人的评论
	 * @param {String} action 删除评论要用的cgi-bin
	 * @param {String} param 删除评论时用到的参数
	 * @example
	 * <<cs call:data_comment_deletebtn(acition,param) >>
	 * qfv.comments.comment.0.deletebtn.action=action
	 * qfv.comments.comment.0.deletebtn.param=param
	 */
	data_comment_deletebtn:function(){}
?>
<?cs def:data_comment_deletebtn(action, param)?>
	<?cs with:commentItem = qz_metadata[_g_comment_meta_path]?>
		<?cs if:qfv.meta.hostuin == qfv.meta.loginuin || qfv.meta.loginuin == commentItem.uin?>
			<?cs call:set(_g_comment_qfv_curpath + ".deletebtn", "action", action)?>
			<?cs call:set(_g_comment_qfv_curpath + ".deletebtn", "param", param)?>
			<?cs call:set(_g_comment_qfv_curpath + ".deletebtn", "mr", 10)?>
		<?cs /if?>
	<?cs /with?>

<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 生成评论的审核按钮
	 * @param {URL} src 通过审核弹框的地址
	 * @param {String} param 通过审核的参数
	 * @example
	 * <<cs call:data_comment_auditbtn(src,param) >>
	 * 生成
	 * qfv.comments.comment.0.auditbtn.src=action
	 * qfv.comments.comment.0.auditbtn.param=param
	 */
	data_comment_auditbtn:function(){}
?>

<?cs def:data_comment_auditbtn(src, param)?>
	<?cs call:set(_g_comment_qfv_curpath, "auditbtn.src", src)?>
	<?cs call:set(_g_comment_qfv_curpath, "auditbtn.param", param)?>
<?cs /def?>
<?cs #}/*end: 一级评论的回复按钮参数*/?>


<?cs #{/*comment item */?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 在_g_comment_cur_replies_path指向的回复数据块中生成查看更多回复的数据块
	 * @param {Object} commentItem wup元数据中的t2body
	 * @example
	 * 待补
	 */
	_data_comment_replies_more:function(){}
?>
<?cs def:_data_comment_replies_more(commentItem)?>
	<?cs #这个限制导致api调用必须有一定的顺序?>
	<?cs if:_g_data_comments_moreReply_cgi || _g_data_comments_more_url?>
		<?cs if:(g_data_commentReplies_count_show+0+filter_reply.ret.disabled_count) < commentItem.respnum?>
			<?cs set:g_data_commentReply_moreflag = 1?>
			<?cs call:set(_g_comment_cur_replies_path, "more.count",
							commentItem.respnum - g_data_commentReplies_count_show-0-filter_reply.ret.disabled_count
				)?>

			<?cs if:g_showrefer_more_comments ?>
				<?cs #已经是展开更多评论的假feeds。还有更多评论时需要跳转到详情页查看?>
				<?cs if:_g_data_comments_more_url?>
					<?cs call:set(_g_comment_cur_replies_path, "more.url", _g_data_comments_more_url)?>
				<?cs /if?>
			<?cs else ?>
				<?cs if:_g_data_comments_moreReply_cgi?>
					<?cs call:set(_g_comment_cur_replies_path, "more.action", _g_data_comments_moreReply_cgi)?>
				<?cs /if?>
				<?cs #展示组件里会优先使用cgi模板展开更多，所以指定url也不影响?>
				<?cs if:_g_data_comments_more_url?>
					<?cs call:set(_g_comment_cur_replies_path, "more.url", _g_data_comments_more_url)?>
				<?cs /if?>
			<?cs /if?>

		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 过滤不该展示的回复数据
	 * @param {Vt2body} commentItem 评论节点。不关心元数据路径
	 */
	_data_comment_replies:function(){}
?>
<?cs def:_data_comment_replies(commentItem)?>
	<?cs set:g_data_commentReplies_count_show=0 ?>
	<?cs if:commentItem.respnum > 0?>
		<?cs call:set(_g_comment_qfv_curpath, "replies.totalreply", commentItem.respnum)?>
	<?cs /if?>

	<?cs set:_cmtReplies_has_count = subcount(commentItem.vt3body)?>
	<?cs #{{二级评论(回复)的下标?>
	<?cs #逻辑参考评论(data_comments)?>
	<?cs if: _cmtReplies_has_count ?>
		<?cs if:_g_comments_show_stranger?>
			<?cs #/*生成要展示的回复的最大条数*/?>
			<?cs set:_index_tmp = 0?>
			<?cs #每个评论的回复列表是不一样的，因此，要把每一个评论的“回复下标列表”保存在不同的地方，防止冲突(前一条回复下标列表留下的脏数据，如果cs引擎可以直接删掉某个值后重新赋值将没有这么麻烦)?>
			<?cs set:g_data_comments.replies[_g_comment_meta_index] = 1?><?cs #给数组节点赋值必须保证数组节点存在，因此set其为1生成数组节点，这样才能在数组每一项上面添加属性，普通对象没有必要?>
			<?cs with:repliesIndex = g_data_comments.replies[_g_comment_meta_index]?>
				<?cs loop:i = _cmtReplies_has_count-1, 0, -1?>
					<?cs if: g_data_commentReplies_count_show<G_COMMENTS_REPLIES_MAX  ?>
						<?cs call:filter_reply(commentItem.vt3body[i]) ?>
						<?cs if: filter_reply.ret.disabled==0 ?>
							<?cs set:repliesIndex.index[_index_tmp] = i?><?cs #保存要展示的评论数据下标?>
							<?cs set:_index_tmp = _index_tmp + 1?>
							<?cs set:g_data_commentReplies_count_show=g_data_commentReplies_count_show+1 ?>
						<?cs /if ?>
					<?cs /if ?>
				<?cs /loop?>
			<?cs /with?>
		<?cs else ?><?cs #不展示陌生人的评论?>
			<?cs set:g_data_comments.replies[_g_comment_meta_index] = 1?><?cs #参见上面?>
			<?cs set:_index_tmp = 0?>
			<?cs with:repliesIndex = g_data_comments.replies[_g_comment_meta_index]?>
				<?cs loop:i = _cmtReplies_has_count - 1, 0, -1?><?cs #因为要展示最新的，所以要从末尾循环?>
						<?cs if:!commentItem.vt3body[i].extendinfo.isstranger && g_data_commentReplies_count_show<G_COMMENTS_REPLIES_MAX ?>
							<?cs call:filter_reply(commentItem.vt3body[i]) ?>
							<?cs if: filter_reply.ret.disabled==0 ?>
								<?cs set:repliesIndex.index[_index_tmp] = i?><?cs #保存要展示的评论数据下标?>
								<?cs set:_index_tmp = _index_tmp + 1?>
								<?cs set:g_data_commentReplies_count_show=g_data_commentReplies_count_show+1 ?>
							<?cs /if?>
						<?cs /if?>
				<?cs /loop?>
				<?cs #set:_index_len = subcount(g_data_comments.replies.index)?>
			<?cs /with?>
		<?cs /if?>
		<?cs #}}?>
	<?cs /if ?>
	<?cs set:_index_len = subcount(g_data_comments.replies[_g_comment_meta_index].index)?>
	<?cs #g_data_comments.replies[_g_comment_meta_index].index 是倒序的，需要反过来?>
	<?cs with:repliesIndex= g_data_comments.replies[_g_comment_meta_index]?>
		<?cs loop:i = 0, _index_len - 1, 1?>
			<?cs set:_k_ = _index_len - 1 - i?>
			<?cs if:_k_ > i ?>
				<?cs set:_index_tmp = repliesIndex.index[i]?>
				<?cs set:repliesIndex.index[i] = repliesIndex.index[_k_]?>
				<?cs set:repliesIndex.index[_k_] = _index_tmp?>
			<?cs /if?>
		<?cs /loop?>
	<?cs /with?>
	<?cs #/*尝试生成查看*更多回复*的入口*/?>
	<?cs set:_g_comment_cur_replies_path = _g_comment_qfv_curpath + ".replies"?>
	<?cs call:_data_comment_replies_more(commentItem)?>

<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @private
	 * @description 内部方法。生成一个评论节点
	 */
	_data_comment_T2bodyItem:function(){}
?>
<?cs def:_data_comment_T2bodyItem(path, commentItem)?>
	<?cs set:_g_commentReply_qfv_index  = -1?><?cs #初始化为-1，与_g_comment_qfv_index原理一样?>

	<?cs call:set(_g_comment_qfv_curpath, "seq", commentItem.seq)?>

	<?cs call:data_comments_user(_g_comment_qfv_curpath, commentItem)?>

	<?cs call:data_comments_targetUser(_g_comment_qfv_curpath, commentItem)?>

	<?cs call:data_comments_time(_g_comment_qfv_curpath, commentItem)?>
	<?cs call:data_comments_content(_g_comment_qfv_curpath, commentItem)?>
	<?cs call:_data_comment_replies(commentItem)?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 生成评论节点
	 * @param {Integer} i 当前评论的索引
	 */
	data_comment_loop_item:function(){}
?>
<?cs def:data_comment_loop_item(i)?>
		<?cs set:_g_comment_qfv_index = _g_comment_qfv_index + 1?>
		<?cs set:_g_comment_qfv_curpath = "comments.comment." + _g_comment_qfv_index ?>

		<?cs set:_g_comment_meta_index = i?><?cs #使用的数据下表路径?>
		<?cs set:_g_comment_meta_path = "vt2body." + i?>
		<?cs if: string.length(qz_metadata[_g_comment_meta_path].uin) > 4 ?>
			<?cs call:_data_comment_T2bodyItem(_g_comment_qfv_curpath, qz_metadata.vt2body[i])?>
		<?cs /if ?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件（评论前ico）
	 * @description 评论内容前添加一个点击跳转类型的小图标。用于针对单图的评论
	 * @param {Integer} i 当前评论的索引
	 * @param {String} url 跳转地址
	 */
	data_comment_withIcon_url:function(){}
?>
<?cs def:data_comment_withIcon_url(i,url) ?>
	<?cs call:set(_g_comment_qfv_curpath, "bfcnt.action.type", "url")?>
	<?cs call:set(_g_comment_qfv_curpath, "bfcnt.action.url", url)?>
	<?cs call:set(_g_comment_qfv_curpath, "bfcnt.src", "/qzone_v6/img/icenter/icon_feedctr_album.png")?>
	<?cs call:set(_g_comment_qfv_curpath, "bfcnt.className", "icon_feed_postlist_img")?>
<?cs /def ?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件（评论前ico）
	 * @description 评论内容前添加一个点击跳转类型的小图标。用于针对单图的评论
	 * @param {Integer} i 当前评论的索引
	 * 其它参数见 @{link data_popup}的参数
	 */
	data_comment_withIcon_popup:function(){}
?>
<?cs def:data_comment_withIcon_popup(i,title, src, param, version, width, height, extend_1, extend_2) ?>
	<?cs call:set(_g_comment_qfv_curpath, "bfcnt.src", "/qzone_v6/img/icenter/icon_feedctr_album.png")?>
	<?cs call:set(_g_comment_qfv_curpath, "bfcnt.className", "icon_feed_postlist_img")?>
	<?cs call:data_popup(_g_comment_qfv_curpath+".bfcnt.action", title, src, param, version, width, height, extend_1, extend_2)?>
<?cs /def ?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件（评论后img）
	 * @description 评论内容后添加一个点击跳转类型的小缩略图。用于针对单图的评论
	 * @param {Integer} i 当前评论的索引
	 * @param {String} url 跳转地址
	 */
	data_comment_withImg_url:function(){}
?>
<?cs def:data_comment_withImg_url(i,url) ?>
	<?cs call:set(_g_comment_qfv_curpath, "afcnt.action.type", "url")?>
	<?cs call:set(_g_comment_qfv_curpath, "afcnt.action.url", url)?>
	<?cs call:set(_g_comment_qfv_curpath, "afcnt.src", "/qzone_v6/img/icenter/icon_feedctr_album.png")?>
<?cs /def ?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件（评论后img）
	 * @description 评论内容后添加一个点击跳转类型的小缩略图。用于针对单图的评论
	 * @param {Integer} i 当前评论的索引
	 * 其它参数见 @{link data_popup}的参数
	 */
	data_comment_withImg_popup:function(){}
?>
<?cs def:data_comment_withImg_popup(i,title, src, param, version, width, height, imageSrc, extend_2) ?>
	<?cs call:set(_g_comment_qfv_curpath, "afcnt.src", imageSrc)?>
	<?cs call:set(_g_comment_qfv_curpath, "afcnt.width", "65")?>
	<?cs call:set(_g_comment_qfv_curpath, "afcnt.height", "45")?>
	<?cs call:data_popup(_g_comment_qfv_curpath+".afcnt.action", title, src, param, version, width, height, "", extend_2)?>
<?cs /def ?>

<?cs ####
	/**
	 * 新版浮层需要的参数
	 */
	data_comment_withImg_popup_v2:function(){}
?>
<?cs def:data_comment_withImg_popup_v2(topicid, pickey) ?>
	<?cs #新版浮层需要的参数 ?>
	<?cs set:_actionpath = _g_comment_qfv_curpath+".afcnt.action"?>
	<?cs call:data_popup_add_attr(_actionpath, "topicid", topicid) ?>
	<?cs call:data_popup_add_attr(_actionpath, "pickey", pickey) ?>
<?cs /def ?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 生成一条评论数据，该方法使用固定的qz_metadata.opinfo.t2body生成评论,给被动feeds使用。后续或有可能干掉这样的方法
	 * @param {void} param 暂时没用上
	 */
	data_opcomment_item:function(){}
?>
<?cs def:data_opcomment_item(param)?>
	<?cs set:_g_comment_meta_path = "opinfo.t2body"?>
	<?cs set:_g_comment_qfv_index = _g_comment_qfv_index + 1?>
	<?cs set:_g_comment_meta_index=0 ?>
	<?cs set:_g_comment_qfv_curpath = "comments.comment." + _g_comment_qfv_index ?>
	<?cs if: string.length(qz_metadata[_g_comment_meta_path].uin) > 4 ?>
		<?cs #过滤一些不正确的qq号?>
		<?cs call:_data_comment_T2bodyItem(_g_comment_qfv_curpath, qz_metadata.opinfo.t2body)?>
	<?cs /if ?>
<?cs /def?>

<?cs #}/*end: comment item*/?>

<?cs #{/*comment reply item*/?>



<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 生成评论的回复节点
	 * @param {Integer} j 当前回复的索引
	 */
	data_commentReply_loop_item:function(){}
?>
<?cs set:data_commentReply_loop_lastComment=-1 ?>
<?cs def:data_commentReply_loop_item(j)?>
		<?cs #上次处理的评论数据下标和本次的不同，那肯定是换了一条评论了 ?>
		<?cs if: data_commentReply_loop_lastComment!=_g_comment_qfv_index ?>

			<?cs set:data_commentReply_loop_lastComment=_g_comment_qfv_index ?>
			<?cs set:_g_commentReply_qfv_index=0 ?>
		<?cs else ?>
			<?cs set:_g_commentReply_qfv_index= _g_commentReply_qfv_index+1?>
		<?cs /if ?>
		<?cs set:_g_commentReply_meta_index = j?>

		<?cs #当前回复用的元数据路径?>
		<?cs set:_g_commentReply_meta_path = _g_comment_meta_path + ".vt3body." + j?>

		<?cs set:_g_commentReply_qfv_curpath =  "comments.comment." +
												_g_comment_qfv_index +
												".replies.reply." +
												_g_commentReply_qfv_index?>

		<?cs with:replyItem = qz_metadata[_g_commentReply_meta_path]?>
				<?cs call:set(_g_commentReply_qfv_curpath, "seq", replyItem.seq)?>

				<?cs call:data_comments_user(_g_commentReply_qfv_curpath, replyItem)?>

				<?cs call:data_comments_targetUser(_g_commentReply_qfv_curpath, replyItem)?>

				<?cs call:data_comments_time(_g_commentReply_qfv_curpath, replyItem)?>
				<?cs call:data_comments_content(_g_commentReply_qfv_curpath, replyItem)?>
		<?cs /with?>
	
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 生成回复中的回复按钮
	 * @param {String} config 回复的config参数，各业务不同
	 * @param {String} param 回复的param参数，各业务不同
	 */
	data_commentReply_replybtn:function(){}
?>
<?cs def:data_commentReply_replybtn(config, param)?>
		<?cs call:set(_g_commentReply_qfv_curpath + ".replybtn", "action", g_data_commentReply_cgi)?>
		<?cs call:set(_g_commentReply_qfv_curpath + ".replybtn", "config", config)?>
		<?cs call:set(_g_commentReply_qfv_curpath + ".replybtn", "param", param)?>
		<?cs call:set(_g_commentReply_qfv_curpath + ".replybtn", "charset", g_data_commentReply_charset)?>
		<?cs call:set(_g_commentReply_qfv_curpath + ".replybtn", "version", "6.4")?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 生成回复中的删除按钮
	 * @param {String} action 删除的action参数
	 * @param {String} param 删除的param参数
	 */
	data_commentReply_deletebtn:function(){}
?>
<?cs def:data_commentReply_deletebtn(action, param)?>
		<?cs with:replyItem = qz_metadata[_g_commentReply_meta_path]?>
		<?cs #删除逻辑详见评论删除按钮?>
		<?cs if:qfv.meta.hostuin == qfv.meta.loginuin || replyItem.uin == qfv.meta.loginuin ?>
			<?cs call:set(_g_commentReply_qfv_curpath + ".deletebtn", "action", action)?>
			<?cs call:set(_g_commentReply_qfv_curpath + ".deletebtn", "param", param)?>
			<?cs call:set(_g_commentReply_qfv_curpath + ".deletebtn", "mr", 10)?>
		<?cs /if?>
		<?cs /with?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 生成回复中的审核按钮
	 * @param {String} src 点击审核弹出窗口的地址
	 * @param {String} param 删除的param参数
	 */
	data_commentReply_auditbtn:function(){}
?>
<?cs def:data_commentReply_auditbtn(src, param)?>
	<?cs call:set(_g_commentReply_qfv_curpath, "auditbtn.src", src)?>
	<?cs call:set(_g_commentReply_qfv_curpath, "auditbtn.param", param)?>
<?cs /def?>
<?cs #}/*end: comment reply item*/?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 评论回复框数据生成
	 * @param {String} param 评论回复的参数
	 * @param {num} tuin 评论回复的对象
	 * @param {String} cgi 有时候评论框是回复功能，使用的是回复cgi.
	 * @param {String} charset 指定编码
	 */
	data_comments_inputbox:function(){}
?>
<?cs def:data_comments_inputbox(config, param, tuin, cgi, charset)?>
	<?cs call:qfv("comments.inputbox.config", config)?>
	<?cs if:cgi?>
		<?cs call:qfv("comments.inputbox.action", cgi)?>
	<?cs else ?>
		<?cs call:qfv("comments.inputbox.action", g_data_comments_cgi)?>
	<?cs /if?>
	<?cs call:qfv("comments.inputbox.param", param)?>
	<?cs call:qfv("comments.inputbox.charset", charset)?>
	<?cs if:tuin == ""?><?cs set:tuin = 0?><?cs /if?>
	<?cs call:qfv("comments.inputbox.tuin", tuin)?>
<?cs /def?>

<?cs #:
	/**/
	function data_comments_inputbox_v2(param){}
?>
<?cs def:data_comments_inputbox_v2(param) ?>
	<?cs if: param.config ?>
		<?cs call:qfv("comments.inputbox.config", param.config)?>
	<?cs /if ?>
	<?cs if:param.cgi?>
		<?cs call:qfv("comments.inputbox.action", param.cgi)?>
	<?cs else ?>
		<?cs call:qfv("comments.inputbox.action", g_data_comments_cgi)?>
	<?cs /if?>
	<?cs if: param.param ?>
		<?cs call:qfv("comments.inputbox.param", param.param)?>
	<?cs /if ?>
	<?cs if: param.charset ?>
		<?cs call:qfv("comments.inputbox.charset", param.charset)?>
	<?cs /if ?>
	<?cs if: param.useReply ?>
		<?cs call:qfv("comments.inputbox.useReply", param.useReply)?>
		<?cs call:qfv("comments.inputbox.comuin", param.comuin)?>
		<?cs call:qfv("comments.inputbox.comid", param.comid)?>
	<?cs /if ?>
	<?cs if:param.tuin == ""?><?cs set:param.tuin = 0?><?cs /if?>
	<?cs call:qfv("comments.inputbox.tuin", param.tuin)?>
<?cs /def ?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description  回复使用的cgi。业务使用的评论cgi和回复cgi还不一样，靠.
	 * @param {String} cgi 评论回复使用的cgi
	 * @param {String} charset 回复返回的数据格式 //deprected
	 */
	data_comment_replies:function(){}
?>
<?cs def:data_comment_replies(replycgi, charset)?>
	<?cs set:g_data_commentReply_cgi = replycgi?>
	<?cs if:charset?>
		<?cs set:g_data_commentReply_charset = charset?>
	<?cs else ?>
		<?cs set:g_data_commentReply_charset = "utf-8"?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 所有评论数据的入口，如果某种feeds要增加评论功能，必须从该方法开始调用
	 * @param {String} cgi 评论发表使用的cgi
	 * @param {String} charset 评论返回的数据格式 //deprected
	 */
	
	data_comments:function(){}
?>
<?cs def:data_comments(cgi, charset)?>
	<?cs set:g_data_comments_count_show = 0 ?>
	<?cs call:_data_comments_init()?>
	<?cs set:g_data_comments_cgi = cgi?>

	<?cs if:!g_data_commentReply_cgi ?>
		<?cs #默认把回复的cgi设成评论的cgi?>
		<?cs set:g_data_commentReply_cgi = cgi?>
		<?cs set:g_data_commentReply_charset  = charset?>
	<?cs /if?>

	<?cs if:charset ?>
		<?cs set:g_data_comments_charset = charset?>
	<?cs else ?>
		<?cs set:g_data_comments_charset = "utf-8"?>
	<?cs /if?>

	<?cs set:_comments_has_count = subcount(qz_metadata.vt2body)?>
	<?cs #{找出要展示的"评论下标列表"，给业务调用?>
	<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV || !g_qz_is_auth?>
		<?cs #认证空间就不要生成评论下标列表， 这样业务模板的循环便不会进行?>
		<?cs #TODO 如果feeds是认证空间，对于“说说”feeds，如果是认证空间的feeds ，
		那评论的发表者如果是登录态的好友，就展示这条评论?>
		<?cs if:_g_comments_show_stranger?>
			<?cs #feeds元数据中携带的评论条数?>
			<?cs set:_index_tmp = 0?>
			<?cs loop:i = _comments_has_count-1, 0, -1?>
				<?cs #调用一下过滤逻辑 ?>
				<?cs call:filter_comment(qz_metadata.vt2body[i]) ?>
				<?cs if: g_data_comments_count_show < G_COMMENTS_MAX ?>
					<?cs if: !filter_comment.ret.disabled ?>
						<?cs set:g_data_comments.index[_index_tmp] = i?><?cs #保存要展示的评论数据下标?>
						<?cs set:_index_tmp = _index_tmp + 1?>
						<?cs set:g_data_comments_count_show = g_data_comments_count_show+1 ?>
						
					<?cs /if ?>
				<?cs /if ?>
			<?cs /loop?>
		<?cs else ?><?cs #不展示陌生人的评论?>
			<?cs set:_index_tmp = 0 ?>
			<?cs #因为要展示最新的，所以要从末尾循环来获取要展示的评论?>
			<?cs loop:i = _comments_has_count-1, 0, -1?>
				<?cs #调用一下过滤逻辑 ?>
				<?cs call:filter_comment(qz_metadata.vt2body[i]) ?>
				<?cs if:!qz_metadata.vt2body[i].extendinfo.isstranger && g_data_comments_count_show < G_COMMENTS_MAX ?>
					<?cs if: !filter_comment.ret.disabled ?>
						<?cs set:g_data_comments.index[_index_tmp] = i?>
						<?cs set:_index_tmp = _index_tmp + 1?>
						<?cs set:g_data_comments_count_show = g_data_comments_count_show+1 ?>
					<?cs /if ?>
				<?cs /if?>
			<?cs /loop?>
		<?cs /if?>
	<?cs /if?>

	<?cs #UGC对应的评论总数?>
	<?cs call:qfv("comments.totalcomment", qz_metadata.vt2count + 0 - filter_comment.ret.disabled_count)?>

	<?cs #g_data_comments.index 是倒序的，需要反过来?>
	<?cs set:_index_len = subcount(g_data_comments.index)?>
	<?cs loop:i = 0, _index_len - 1, 1?>
		<?cs set:_k_ = _index_len - 1 - i?>
		<?cs if:_k_ > i ?>
			<?cs set:_index_tmp = g_data_comments.index[i]?>
			<?cs set:g_data_comments.index[i] = g_data_comments.index[_k_]?>
			<?cs set:g_data_comments.index[_k_] = _index_tmp?>
		<?cs /if?>
	<?cs /loop?>

	<?cs #}
		/*
		 *总体来说，上面的逻辑是为了初始化以下数据：
		 g_data_comments.index[]
		 g_data_comments_start
		 g_data_comments_end
		 g_data_comments_count_show
		 */
	?>

	<?cs #业务评论的遍历接口使用以下方式：?>
	<?cs #each:i = g_data_comments.index?>
		<?cs #call:data_comment_loop_item(i)?>
		<?cs #...?>
		<?cs #do_other_macro_call?>
		<?cs #...?>
	<?cs #/each?>
<?cs /def?>

<?cs ####
	/**
	 * @memberOf 评论回复数据组件
	 * @description 设置是否允许展示陌生人评论回复
	 * @param {Bool} f
	 */
	data_comments_showstranger:function(){}
?>
<?cs def:data_comments_showstranger(f)?>
	<?cs set:_g_comments_show_stranger = f?>
	<?cs if:g_showrefer_more_comments?><?cs #如果是展开更多评论，允许显示陌生人的评论?>
		<?cs set:_g_comments_show_stranger = 1?>
	<?cs /if?>
<?cs /def?>
