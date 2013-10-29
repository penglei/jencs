<?cs #/*example:评论组件使用*/?>
<?c #/*---变化的部分---*/?>
<?cs #/*所有评论组件必须从这个宏开始使用*/?>
<?cs call:data_comments(s_comments_cgi, "utf-8", "http://w.qzone.qq.com/cgi-bin/feeds/feeds_get_comment", "")?>
<?cs #/*尝试添加查看更多评论\回复的数据*/?>
<?cs call:data_comments_more_param("t1_source=1&t1_uin=1520077302&t1_tid=f6899a5ae65060502ee80200&t2_uin=1943589134&t2_tid=3&subdotype=55802&signin=0&sceneid=0")?>

<?cs #/*开始生成评论数据*/?>
<?cs loop: i = 0, g_data_comments_count - 1, 1?>
	<?cs #/*生成基本的评论信息*/?>
	<?cs call:data_comment_loop_item(i)?>
些快乐的时光。
	<?cs #/*评论的其它一些数据节点*/?>
	<?cs set:_comment_param = "{param}" ?>
	<?cs set:_comment_config = "1|0|1|0"?>
	<?cs call:data_comment_replybtn(_comment_config, _comment_param)?>

	<?cs call:data_comment_replies_more_param("{reply param}")?>
	<?cs loop:j = 0, g_data_commentReplies_count - 1, 1?>
		<?cs #/*生成评论的回复节点*/?>
		<?cs call:data_comment_loop_replyItem(j)?>

		<?cs #/*评论回复的其它一些数据节点*/?>
		<?cs call:data_commentReply_replyBtn(config, param)?>
	<?cs /loop?>
<?cs /loop?>
<?cs call:data_comments_inputbox(config, param)?>

