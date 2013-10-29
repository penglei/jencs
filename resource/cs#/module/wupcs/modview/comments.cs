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
		<div class="mod_commnets_poster bg2 feedClickCmd comment_default_inputentry"
			 data-cmd="qz_reply"
			 data-version="6"
			 data-action="<?cs var:commentBox.action?>"
			 data-param="<?cs var:html_encode(commentBox.param, 1)?>"
			 data-charset="<?cs var:commentBox.charset?>"
			 data-maxLength="<?cs var:commentBox.maxlength?>"
			 data-tuin="<?cs var:commentBox.tuin?>"
			 data-config="<?cs var:commentBox.config?>" 
			 <?cs if: commentBox.useReply==1 ?>data-typeid="1" <?cs /if ?>
			<?cs if: commentBox.comid>=0 ?>data-tid="<?cs var:commentBox.comid ?>" <?cs /if ?>
			<?cs if: commentBox.comuin ?>data-uin="<?cs var:commentBox.comuin ?>" <?cs /if ?>
		>
			<div class="comments_poster_bd comments_poster_default">
				<div class="comments_box">
					<div class="textinput textinput_default bor2" alt="replybtn" placeholder="我也说一句">
						<a class="c_tx3" href="javascript:void(0);" alt="replybtn">我也说一句</a>
					</div>
					<div class="mod-quick-comment bor2">
						<a href="javascript:;" data-cmd="qz_quick_reply" class="btn-quick-comment bg">
							<i class="icon-flash"></i>
							<span class="flash-text c_tx3">快评</span>
						</a>
					</div>
				</div>
			</div>
		</div>

	<?cs /if?>
	<?cs /with?>
<?cs /def?><?cs #/*end _comments_try_inputbox*/?>


<?cs ####
	/**
	 *展开剩余x条回复入口
	 */
?>
<?cs def:_replies_more_info(replies)?>
	<?cs if:replies.more.action?>
	<div class="comments_list_more">
		<?cs #<qz:more action="" param="" charset=""> </qz:more>?>
		<a href="javascript:;" class="c_tx"
			 data-cmd="qz_more"
			 data-action="<?cs var:replies.more.action?>"
			 data-param="<?cs var:html_encode(replies.more.param, 1)?>"
			 data-charset="<?cs var:replies.more.charset?>"
		>展开剩余<?cs var:replies.more.count?>条回复&#8595;</a>
	</div>
	<?cs elif:replies.more.url?>
		<div class="comments_list_more">
			<?cs if:qfv.meta.appid == 4?>
				<a href="<?cs var:replies.more.url?>" class="c_tx" target="_blank">查看剩余全部回复</a>
			<?cs else ?>
				<a href="<?cs var:replies.more.url?>" class="c_tx" target="_blank">
					查看剩余<?cs var:replies.more.count?>条回复
				</a>
			<?cs /if?>
		</div>
	<?cs /if?>
<?cs /def?>


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
	<?cs if:qfv.comments.more.action?>
	<div class="comments_list_more">
		<?cs #<qz:more action="" param="" charset=""></qz:more>?>
		<a href="javascript:;" class="c_tx comments_more_entry"
			 data-cmd="qz_more"
			 data-action="<?cs var:qfv.comments.more.action?>"
			 data-param="<?cs var:html_encode(qfv.comments.more.param, 1)?>"
			 data-charset="<?cs var:qfv.comments.more.charset?>"
		>展开剩余<?cs var:qfv.comments.more.count?>条评论&#8595;</a>
	</div>
	<?cs else ?>
		<?cs call:_comments_specialApp_more_info()?>
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
	<?cs if:commentItem.afcnt.width?>
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
	 *评论的回复列表
	 */
?>
<?cs def:_comment_replies_list(replies)?>
	<div class="comments_list mod_comments_sub">
		<?cs call:_replies_more_info(replies)?>
		<?cs set:_end_reply = subcount(replies.reply) - 1?>
		<ul>
		<?cs loop:i = 0, _end_reply, 1?>
		<?cs with:reply = replies.reply[i]?>
			<li class="comments_item bor3" 
				data-type="replyroot" 
				<?cs #/*tid 日志、留言板业务的回复按钮tid传的为生成时间ctime 其他业务为seq*/?>
				data-tid=<?cs if:qfv.meta.appid==2 || qfv.meta.appid==334 ?>"<?cs var:reply.time.abstime?>"<?cs else?>"<?cs var:reply.seq?>"<?cs /if?> 
				data-uin="<?cs var:reply.user.uin?>" 
				data-nick="<?cs call:echoUsername(reply.user, reply.user.nickname)?>" 
				data-who="<?cs var:reply.user.who?>"
			>
				<div class="comments_item_bd">
					<div class="ui_avatar">
						<?cs call:userAvatar_comp(reply.user, 30)?>
					</div><?cs #/*头像*/?>
					<div class="comments_content">
						<?cs call:userLink_comp(reply.user)?><?cs #/*昵称*/ ?>
						<?cs call:_comments_targetUser_info(reply)?><?cs #可能生成 " 回复 xxx"?>
						 : <?cs #/*注意前后有一个空格*/?>
						<?cs call:_comments_cnt(reply.content)?><?cs #/*回复内容*/?>
						<div class="comments_op">
							<?cs call:_comments_time(reply.time)?>
							<?cs call:_replies_op_suffix_hook(reply)?>
						</div>
					</div>
				</div>
			</li>
		<?cs /with?>
		<?cs /loop?>
		</ul>
	</div>
<?cs /def?>

<?cs ####
	/**
	 *展示评论列表
	 */
?>
<?cs def:_comments_list()?>
	<?cs set:_end_comment = subcount(qfv.comments.comment) - 1?>
	<ul>
	<?cs loop:i = 0, _end_comment, 1?>
	<?cs with:commentItem = qfv.comments.comment[i]?>
		<?cs if: subcount(commentItem.user) ?>
		<li class="comments_item bor3" 
			data-type="commentroot" 
			data-tid="<?cs var:commentItem.seq?>" 
			data-uin="<?cs var:commentItem.user.uin?>" 
			data-nick="<?cs call:echoUsername(commentItem.user, commentItem.user.nickname)?>" 
			data-who="<?cs var:commentItem.user.who?>"
		>
			<div class="comments_item_bd">
				<div class="ui_avatar"><?cs call:userAvatar_comp(commentItem.user, 30)?></div><?cs #/*头像*/?>
				<div class="comments_content">
					<?cs call:userLink_comp(commentItem.user)?><?cs #/*昵称*/ ?>
					<?cs call:_comments_targetUser_info(commentItem)?><?cs #可能生成 " 回复 xxx"?>
					 : <?cs #/*注意前后有一个空格*/?>
					<?cs #call:_comment_cnt_prefix_hook(commentItem)?><?cs #/*内容前加一个Hook，用以加入特殊处理*/?>
					<?cs call:_comments_cnt(commentItem.content)?><?cs #/*评论内容*/?>
					<?cs call:_comment_cnt_endfix_hook(commentItem)?><?cs #/*内容后加一个Hook，用以加入特殊处理*/?>
					<div class="comments_op">
						<?cs call:_comments_time(commentItem.time)?>
						<?cs call:_comment_op_suffix_hook(commentItem)?><?cs #/*可以加转发按钮*/?>
					</div>
				</div>

				<?cs #/*二级评论*/?>
				<?cs if:subcount(commentItem.replies.reply) > 0?>
					<?cs call:_comment_replies_list(commentItem.replies)?>
				<?cs /if?>
			</div>
		</li>
		<?cs /if ?>
	<?cs /with?>
	<?cs /loop?>
	</ul>
<?cs /def?>

<?cs ####
	/**
	 *comments总入口
	 *业务不直接使用该入口，而使用comments-like，因为Like和comments中有交叉的html节点
	 */
?>
<?cs def:comments()?>
	<?cs if:subcount(qfv.comments.comment) > 0?>
	<div class="mod_comments">
		<div class="comments_list<?cs if:qfv.comments.authSpaceNoReply?> none<?cs /if?>">
			<?cs call:_comments_more_info()?>
			<?cs call:_comments_list()?>
		</div>
	</div>
	<?cs /if?>

	<?cs #/*尝试输出评论框*/?>
	<?cs call:_comments_try_inputbox()?>
<?cs /def?>
