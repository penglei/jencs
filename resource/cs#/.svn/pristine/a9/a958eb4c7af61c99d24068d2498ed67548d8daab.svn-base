<?cs #/*feed评论组件*/?>
<?cs #/*评论组建是一个很独立的组件(大家都长得一样)，所以在表现层就不再做拆分*/?>

<?cs ####
	/**
	 * 使用qfv.comments.inputbox生成评论框
	 */
?>
<?cs def:_comments_try_inputbox()?>
	<?cs with:commentBox = qfv.comments.inputbox?>
	<?cs if:subcount(commentBox) > 0?>

		<?cs ##前台只对 .feedClickCmd 和 a[data-cmd] 的节点进行事件代理
			 ##但是同样需要指定data-cmd，否则无法找到处理的模块?>
		<div class="comment-write-holder">
			<div class="comment-write-textarea hotclick"
			 	 data-cmd="qz_reply"
				 data-version="6"
				 data-action="<?cs var:commentBox.action?>"
				 data-param="<?cs var:html_encode(commentBox.param, 1)?>"
				 data-hctag="spcaretab.commentbox.click"
				 data-charset="<?cs var:commentBox.charset?>"
				 data-maxLength="<?cs var:commentBox.maxlength?>"
				 data-tuin="<?cs var:commentBox.tuin?>"
				 data-config="<?cs var:commentBox.config?>" 
				 <?cs if: commentBox.useReply==1 ?>data-typeid="1" <?cs /if ?>
				<?cs if: commentBox.comid>=0 ?>data-tid="<?cs var:commentBox.comid ?>" <?cs /if ?>
				<?cs if: commentBox.comuin ?>data-uin="<?cs var:commentBox.comuin ?>" <?cs /if ?>
			>
                <span class="placeholder">我也说一句</span>
			</div>
		</div>

	<?cs /if?>
	<?cs /with?>
<?cs /def?><?cs #/*end _comments_try_inputbox*/?>


<?cs ####
	/**
	 *特殊应用查看更多评论需要跳到详情页面
	 *TODO 这个需要移到业务层模板去做
	 */
?>
<?cs def:_comments_specialApp_more_info()?>
	<?cs if:qfv.meta.appid == 311 || qfv.meta.appid == 4?>
		<?cs if:qfv.comments.more.url || g_showrefer_more_comments?>
			<div class="comments_list_more">
				<?cs if:g_showrefer_more_comments ?>
					<a href="javascript:;"
						data-cmd="qz_more"
						data-action="close"
						class="c_tx ui_mr10"  
						title="收起评论">收起评论&#8593;</a>
				<?cs /if ?>
				<?cs if:qfv.comments.more.url ?>
					<a href="<?cs var:qfv.comments.more.url?>" class="c_tx" target="_blank">查看剩余全部评论</a>
				<?cs /if ?>
			</div>
		<?cs /if?>
	<?cs else ?>
		<?cs if:qfv.comments.more.url || g_showrefer_more_comments?>
			<div class="comments_list_more">
				<?cs if:g_showrefer_more_comments ?>
					<a href="javascript:;"
						data-cmd="qz_more"
						data-action="close"
						class="c_tx ui_mr10"  
						title="收起评论">收起评论&#8593;</a>
				<?cs /if ?>
				<?cs if:qfv.comments.more.url ?>
					<a href="<?cs var:qfv.comments.more.url?>" class="c_tx" target="_blank">
					查看剩余<?cs var:qfv.comments.more.count?>条评论
					</a>
				<?cs /if ?>
			</div>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *查看更多评论的入口
	 */
?>
<?cs def:_comments_more_info()?>
	<?cs if:qfv.comments.more.url ?>
	<div class="comment-more">
		<a class="hotclick" href="<?cs var:qfv.comments.more.url?>" target="_blank" data-hctag="spcaretab.morecomment.click">查看剩余<?cs var:qfv.comments.more.count?>评论</a>
	</div>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *查看更多回复的入口 被动会有
	 */
?>
<?cs def:_replies_more_info(replies)?>
	<?cs if:replies.more.url?>
	<div class="comment-more" style="padding:0;">
		<a class="hotclick" href="<?cs var:replies.more.url?>" target="_blank" data-hctag="spcaretab.morereply.click">去空间查看更多评论回复</a>
	</div>
	<?cs /if?>
<?cs /def?>

<?cs #{/*---评论区域的私有小组件---*/?>
<?cs def:_comments_cnt(content)?>
	<?cs #/*评论内容是通过richmsg生成的，经过数据层的转换，只需要简单使用conCommn即可*/?>
	<?cs call:conCommon(content.con)?>
<?cs /def?>

<?cs def:_comments_time(time)?>
	<?cs #/*使用文字组件即可*/?>
	<?cs call:con_txt(time)?>
<?cs /def?>
<?cs #}/*---end---*/?>

<?cs #{/*---comment区域的一些hook---*/?>
<?cs def:_comment_cnt_prefix_hook(commentItem)?>
	<?cs #/*日志feeds的评论有commnet_icon，可以在这里加上*/?>
	<?cs if:subcount(commentItem.bfcnt)?>
		<?cs if:commentItem.bfcnt.action.type == "url"?>
			<a href="<?cs call:ugc_url_check(commentItem.bfcnt.action.url,1) ?>" target="_blank" title="点击查看图片">
		<?cs elif:commentItem.bfcnt.action.type == "popup"?>
			<?cs call:popup_start(commentItem.bfcnt) ?>
		<?cs /if?>
				<?cs call:con_icon(commentItem.bfcnt)?>
		<?cs if:commentItem.bfcnt.action.type == "url"?>
			</a>
		<?cs elif:commentItem.bfcnt.action.type == "popup"?>
			<?cs call:popup_end() ?>
		<?cs /if?>
	<?cs /if?>
	<?cs #others? 多半不会再有了,PS:你错了，还是会有的！！比如comment_img ?>
<?cs /def?>

<?cs #{/*---comment区域的一些hook---*/?>
<?cs def:_comment_cnt_endfix_hook(commentItem)?>
	<?cs #/*日志feeds的评论有commnet_img，可以在这里加上*/?>
	<?cs if:subcount(commentItem.afcnt)?>
		<div class="comments_thumbnails">
		<?cs if:commentItem.afcnt.action.type == "url"?>
			<a href="<?cs call:ugc_url_check(commentItem.afcnt.action.url,1) ?>" target="_blank" title="点击查看图片">
		<?cs elif:commentItem.afcnt.action.type == "popup"?>
			<?cs call:popup_start(commentItem.afcnt) ?>
		<?cs /if?>
			<?cs call:con_img(commentItem.afcnt)?>
		<?cs if:commentItem.afcnt.action.type == "url"?>
			</a>
		<?cs elif:commentItem.afcnt.action.type == "popup"?>
			<?cs call:popup_end() ?>
		<?cs /if?>
		</div>
	<?cs /if?>
<?cs /def?>

<?cs def:_comment_try_op_fwd(commentItem)?><?cs #/*有些评论可以转发*/?>
<?cs /def?>

<?cs def:_comment_try_op_share(commentItem)?><?cs #/*有些评论可以分享*/?>
<?cs /def?>

<?cs def:_comments_try_op_audit(item)?><?cs #/*有些评论需要审核*/?>
	<?cs if:subcount(item.auditbtn)?>
		<?cs #call:get_con_style(qfv.operate.forward)?>
		<a href="javascript:;" 
			class="ui_mr10 c_tx" 
			onclick="QZONE.ICFeeds.Interface.auditPassExtend(
				{
					dataonly:1,
					src:'<?cs var:item.auditbtn.src ?>',
					param:'<?cs var:item.auditbtn.param ?>'
				})"
		>
			通过审核
		</a>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *在评论的用户后面生成 " 回复 yyy"信息
	 */
?>
<?cs def:_comments_targetUser_info(item)?>
	<?cs if:subcount(item.targetUser)?>
		 回复 <?cs call:userLink_comp(item.targetUser)?>
	<?cs /if?>
<?cs /def?>

<?cs #评论或者回复的删除按钮?>
<?cs def:_comments_try_op_delete(item)?>
	<?cs #存在topicId出现删除按钮，且分享由于seq字段漏传到更新中心，对分享做兼容：存在seq才出现删除按钮?>
	<?cs if:qfv.meta.topicid ?>
		<?cs #使用a标签，判断数据层是否有deletebtn 来加入删除按钮?>
		<?cs if:subcount(item.deletebtn)?>
			<?cs if:qfv.meta.appid==202 && item.seq == 0 ?>
			<?cs else?>
				<a class="ui_mr10 c_tx none" 
					href="javascript:;" 
					data-cmd="qz_delete"
				>删除</a>
			<?cs /if?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs #回复按钮?>
<?cs def:_comments_op_reply(item)?>
	<?cs if:subcount(item.replybtn)?>
		<?cs with:replybtn = item.replybtn?>
			<?cs ##<qz:reply version="" action="" param="" charset="" tuin="" config=""></qz:reply>?>
			<a class="ui_mr10 c_tx" href="javascript:;"
				 data-cmd="qz_reply"
				 data-version="<?cs var:replybtn.version?>"
				 data-action="<?cs var:replybtn.action?>"
				 data-param="<?cs var:html_encode(replybtn.param, 1)?>"
				 data-charset="<?cs var:replybtn.charset?>"
				 data-tuin="<?cs var:replybtn.tuin?>"
				 data-config="<?cs var:html_encode(replybtn.config,1)?>"
			>回复</a>
		<?cs /with?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *评论操作区按钮
	 */
?>
<?cs def:_comment_op_suffix_hook(commentItem)?>
	<?cs #/*一下几个操作都是同时处理，需要各自加入自己的判断条件*/?>
	<?cs call:_comment_try_op_fwd(commentItem)?>
	<?cs call:_comment_try_op_share(commentItem)?>
	<?cs call:_comments_op_reply(commentItem)?><?cs #/*大多数评论都有回复按钮*/?>
	<?cs call:_comments_try_op_audit(commentItem)?>
	<?cs call:_comments_try_op_delete(commentItem)?>
<?cs /def?>

<?cs ####
	/**
	 *回复操作区按钮
	 */
?>
<?cs def:_replies_op_suffix_hook(replyItem)?>
	<?cs call:_comments_op_reply(replyItem)?><?cs #/*二级平的的回复按钮有version差异，在数据层决定*/?>
	<?cs #call:_comments_try_op_audit(replyItem)?><?cs #回复应该是没有审核按钮吧?>
	<?cs call:_comments_try_op_delete(replyItem)?>
<?cs /def?>

<?cs #}/*---end---*/?>


<?cs ####
	/**
	 *评论的回复列表 展示10条
	 */
?>
<?cs def:_comment_replies_list(replies)?>
	<?cs set:_end_reply = subcount(replies.reply) - 1?>
	<?cs loop:i = 0, _end_reply, 1?>
	<?cs with:reply = replies.reply[i]?>
		<li class="comment-item" data-role="lireplyroot">
			<?cs call:userLink_comp(reply.user)?><?cs #/*昵称*/ ?>
			<?cs call:_comments_targetUser_info(reply)?><?cs #可能生成 " 回复 xxx"?>
			 : <?cs #/*注意前后有一个空格*/?>
			<?cs call:_comments_cnt(reply.content)?><?cs #/*回复内容*/?>
			<a href="javascript:;"
				 class="reply-icon-wrap qz_reply_entry hotclick"
				 data-tuin="<?cs var:reply.user.uin?>"<?cs #一级评论的tuin只有user里有，二级评论的tuin在qz_reply里面也有?>
				 data-tid=<?cs if:qfv.meta.appid==2 || qfv.meta.appid==334 ?>"<?cs var:reply.time.abstime?>"<?cs else?>"<?cs var:reply.seq?>"<?cs /if?>
				 data-nickname="<?cs call:echoUsername(reply.user, reply.user.nickname)?>"
				 data-uintype="<?cs var:reply.user.who?>"<?cs #区分评论是来自哪个平台的?>
				 data-uinimg=""<?cs #朋友网的头像?>
				 data-role="replyroot"
				 data-param="<?cs call:ugc_as_html(reply.replybtn.param,1,1)?>"
				 data-hctag="spcaretab.reply.click"
				><i class="ui-icon icon-reply-gray"></i></a>
		</li>
	<?cs /with?>
	<?cs /loop?>
	<?cs if:qz_metadata.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?><?cs #/*被动出查看更多评论回复按钮*/?>
		<?cs call:_replies_more_info(replies)?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *展示评论列表
	 */
?>
<?cs def:_comments_list()?>
	<?cs set:_end_comment = subcount(qfv.comments.comment) - 1?>
	<?cs loop:i = 0, _end_comment, 1?>
	<?cs with:commentItem = qfv.comments.comment[i]?>
		<?cs if: subcount(commentItem.user) ?>
		<li class="comment-item" data-role="licommentroot">
			<?cs call:userLink_comp(commentItem.user)?><?cs #/*昵称*/ ?>
			<?cs call:_comments_targetUser_info(commentItem)?><?cs #可能生成 " 回复 xxx"?>
			 : <?cs #/*注意前后有一个空格*/?>
			<?cs call:_comments_cnt(commentItem.content)?><?cs #/*评论内容*/?>
			<a href="javascript:;"
				 class="reply-icon-wrap qz_reply_entry hotclick"
				 data-tid="<?cs var:commentItem.seq?>"
				 data-tuin="<?cs var:commentItem.user.uin?>"<?cs #一级评论的tuin只有user里有，二级评论的tuin在qz_reply里面也有?>
				 data-nickname="<?cs call:echoUsername(commentItem.user, commentItem.user.nickname)?>"
				 data-uintype="<?cs var:commentItem.user.who?>"<?cs #区分评论是来自哪个平台的?>
				 data-uinimg=""<?cs #朋友网的头像?>
				 data-role="commentroot"
				 data-param="<?cs call:ugc_as_html(commentItem.replybtn.param,1,1)?>"
				 data-hctag="spcaretab.reply.click"
				><i class="ui-icon icon-reply-gray"></i></a>
		</li>

		<?cs #/*二级评论*/?>
		<?cs if:subcount(commentItem.replies.reply) > 0?>
			<?cs call:_comment_replies_list(commentItem.replies)?>
		<?cs /if?>

		<?cs /if ?>
	<?cs /with?>
	<?cs /loop?>
<?cs /def?>

<?cs ####
	/**
	 *comments总入口
	 *业务不直接使用该入口，而使用comments-like，因为Like和comments中有交叉的html节点
	 */
?>
<?cs def:comments()?>
	<div class="f-comment qz_feed_comment"> 
		<?cs if:subcount(qfv.comments.comment)?>
		<ul class="comment-list">
			<?cs call:_comments_list()?>
		</ul>
		<?cs /if?>
		<?cs call:_comments_more_info()?>
		<?cs #/*输出评论框*/?>
		<?cs call:_comments_try_inputbox()?>
	</div>
<?cs /def?>
