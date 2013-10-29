<?cs ####
	/**
	 * 如果是1，说明需要在按钮前面加个点，如果是0则表示不需要
	 */
?>
<?cs set:v8_operate_dot = 0?>

<?cs def:opr_seprate_line()?>
	<?cs #从第二个按钮起，按钮之前都有这个分隔线?>
	<?cs if:v8_operate_dot == 1 ?>
		<span class="item-line"></span>
	<?cs else ?>
		<?cs set:v8_operate_dot = 1?>
	<?cs /if ?>
<?cs /def?>

<?cs #/*
	如果某个按钮只有一个业务在使用，不要放到这里面，放到各自的模板中去做
*/?>

<?cs def:_like_show_count()?>
	<?cs if:qfv.like.count?>
	(
	<?cs if:qfv.like.count > 10000?>
		<?cs var:qfv.like.count / 10000 ?>万
	<?cs else ?>
		<?cs var:qfv.like.count?>
	<?cs /if?>
	)
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * 按钮：收藏feed
	 */
?>
<?cs def:v8_oprCollect()?>
	<?cs if:subcount(qfv.operate.collect) > 0?>
		<?cs call:opr_seprate_line()?>
		<a class="item <?cs var:qfv.operate.collect.class?>" 
			href="javascript:;" 
			data-cmd="qz_opr_collect"
		>
		<i class="ui-icon icon-feed-collect"></i>
		<?cs var:qfv.operate.collect.text?>
		</a>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * 按钮：屏蔽好友
	 */
?>
<?cs def:v8_oprPrevent()?>
	<?cs if:subcount(qfv.operate.prevent) > 0?>
		<?cs call:opr_seprate_line()?>
		<a class="item <?cs var:qfv.operate.prevent.class?>" 
			href="javascript:;" 
			data-cmd="qz_opr_prevent"
		>
		<i class="ui-icon icon-feed-pingbi"></i>
		<?cs var:qfv.operate.prevent.text?>
		</a>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *操作区更多按钮 比如取消关注、屏蔽此人等操作放在这里，但基本通过js来绑定，这里只负责显示入口
	 */
?>
<?cs def:v8_oprMore()?>
	<?cs if:subcount(qfv.operate.more_opr) > 0?>
		<?cs call:opr_seprate_line()?>
		<a class="item item-sp <?cs var:qfv.operate.more_opr.className?>" 
			href="javascript:;" 
			data-cmd="qz_opr_more"
		>
			<i class="ui-icon icon-more"></i>
		</a>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *操作区赞按钮，注意，赞的数据在qfv.like下面，操作区中不添加任何关于赞的数据
	 */
?>
<?cs def:v8_oprLike()?>
	<?cs if:subcount(qfv.like) > 0?>
		<?cs call:opr_seprate_line()?>
		<a class="item qz_like_btn_v3" 
			<?cs #/*是否已经赞了*/?>
			data-islike="<?cs var:qfv.like.isliked?>" 
			<?cs #/*赞的计数*/?>
			data-likecnt="<?cs var:qfv.like.count?>" 
			data-showcount="<?cs var:qfv.like.likeinfo?>" 
			data-unikey="<?cs var:html_encode(qfv.like.unikey,1)?>" 
			data-curkey="<?cs var:html_encode(qfv.like.curkey,1)?>" 
			data-clicklog="like" 
			<?cs if:qfv.like.isbatch?>
				data-isbatch="<?cs var:qfv.like.isbatch?>" 
			<?cs /if?>
			href="javascript:;"
		>
			<i class="ui-icon icon-praise"></i>
		<?cs if:qfv.like.isliked?>
			取消赞<?cs call:_like_show_count()?>
		<?cs else ?>
			赞<?cs call:_like_show_count()?>
		<?cs /if?>
		</a>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *根据评论数生成评论按钮
	 */
?>
<?cs def:v8_oprComment()?>
	<?cs if:subcount(qfv.operate.comment_btn) > 0?>
		<?cs call:opr_seprate_line()?>
		<?cs ###在view里面可以直接修改qfv里的数据,相当于内部变量，不用表现在viewdata里?>
		<?cs ###比如这种data-role完全是为了配合前台的程序，跟数据层是没有关系的，只不过我们需要用统一的输出逻辑来控制?>
		<?cs call:data_attrs("operate.comment_btn", "data-cmd", "qz_reply")?>
		<?cs #表示点进后使用我也说一句来触发评论。自己不带评论数据?>
		<?cs call:data_attrs("operate.comment_btn", "data-link", 1)?>
		<?cs #数据上报的标记节点?>
		<?cs call:data_attrs("operate.comment_btn", "data-clicklog", "comment")?>
		<?cs #qz_btn_reply是用来评论时增加计数的?>
		<?cs call:data_addClassName("operate.comment_btn", "qz_btn_reply item")?>

		<?cs #有更多评论不一定会有moreflag ,moreflag只是告诉可不可以在feed里展开更多评论?>
		<?cs if:qfv.operate.comment_btn.moreflag?>
			<?cs #同时展开更多评论，所以添加一个role为more标志，用以前台进行判断?>
			<?cs call:data_attrs("operate.comment_btn", "data-role", "more")?>
		<?cs /if?>
		<?cs set:qfv.operate.comment_btn.js=1 ?>
		<?cs call:v8_con_url_start(qfv.operate.comment_btn)?>
			<i class="ui-icon icon-comment"></i>
			<?cs var:html_encode(qfv.operate.comment_btn.text, 1)?>
		<?cs call:v8_con_url_end()?>
	<?cs /if ?>
<?cs /def?>

<?cs ####
	/**
	 *生成转发按钮。注意:转发按钮已经不需要param参数了
	 */
?>
<?cs def:v8_oprForward()?>
	<?cs if:qfv.operate.forward?>
		<?cs call:opr_seprate_line()?>
		<a 
			class="item qz_retweet_btn " 
			href="javascript:;" 
			data-cmd="qz_popup" 
			data-version="4" 
			<?cs if: string.length(qfv.operate.forward.param) ?>
			data-param="<?cs var:qfv.operate.forward.param ?>" 
			<?cs /if ?>
			data-isnewtype="1" 
			data-type="ForwardingBox" 
			data-src="/qzone/app/controls/forwardingBox/forwardingBoxFacade.js" 
			data-clicklog="retweet" 
			<?cs #data-link="/qzonestyle/qzone_app/app_feeds_v1/mood_feeds.css"#这个没用了?>
		>
			<i class="ui-icon icon-forward"></i>
			转发<?cs if:qfv.operate.forward.count?>(<?cs var:qfv.operate.forward.count?>)<?cs /if?>
		</a>
	<?cs /if?>
<?cs /def?>

<?cs #//赞、转、评是三个标准操作按钮?>


<?cs ####
	/**
	 * 按钮：通过审核
	 */
?>
<?cs def:v8_oprAudit()?>
	<?cs with: item=qfv.operate ?>
	<?cs if:subcount(item.auditbtn)?>
		<?cs call:opr_seprate_line()?>
		<a href="javascript:;" 
			class="item" 
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
	<?cs /with ?>
<?cs /def?>


<?cs ####
	/**
	 * 按钮：删除
	 */
?>
<?cs def:v8_oprDel()?>
	<?cs if:subcount(qfv.operate.qz_delete)?>
		<?cs #留言板feed的有一个“删除”留言的按钮，已经带有删除feed的功能，不需要再单独加一个“删除”feed的按钮?>
		<?cs call:opr_seprate_line()?>
		<span class="item">
			<qz:delete 
				action="<?cs var:qfv.operate.qz_delete.action ?>" 
				param="<?cs var:qfv.operate.qz_delete.param ?>"
			>删除</qz:delete>
		</span>
	<?cs elif:qfv.operate.delete_feed_opr == 1?>
		<?cs call:opr_seprate_line()?>
		<a class="item" href="javascript:;" data-cmd="qz_opr_delete"><i class="ui-icon icon-feed-delete"></i>删除</a>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *操作区自定义按钮
	 */
?>
<?cs def:v8_operate_item(oprItem)?>
	<?cs if:oprItem.action.type == "popup"?>
		<?cs set:oprItem.action.className = oprItem.action.className + " item"?>
		<?cs call:opr_seprate_line()?>
		<?cs call:v8_con_popup(oprItem)?>
	<?cs else ?>
		<?cs set:oprItem.className = oprItem.className + " item"?>
		<?cs if:oprItem.type == "txt"?>
			<?cs call:opr_seprate_line()?>
			<?cs call:v8_con_txt(oprItem)?>
		<?cs elif:oprItem.type == "url"?>
			<?cs call:opr_seprate_line()?>
			<?cs call:v8_con_url(oprItem)?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs def:v8_operate_start()?>
	<p class="f-detail">

	<?cs #/*时间信息在操作区 TODO:class=time*/?>
	<?cs set:qfv.time.className=qfv.time.className + " time"?>
	<?cs #call:v8_oprTime()?>
	<?cs if:subcount(qfv.operate) > 0?>
		<?cs #call:v8_oprVisitor()?>
		<?cs #call:v8_oprSource()?>

		<?cs #{/*操作区几个标准按钮*/?>

		<?cs #先生成默认操作?>
		<?cs call:v8_oprComment()?>
		<?cs call:v8_oprForward()?>
		<?cs call:v8_oprLike()?>

		<?cs call:v8_oprAudit()?>
		<?cs call:v8_oprDel()?>
		<?cs if:subcount(qfv.operate) > 0?>
			<?cs set:_end = subcount(qfv.operate.opr) - 1?>
			<?cs loop:i = 0, _end, 1?>
				<?cs call:v8_operate_item(qfv.operate.opr[i])?>
			<?cs /loop?>
		<?cs /if?>
		<?cs #call:v8_oprPrevent() ?>
		<?cs call:v8_oprCollect() ?>
		<?cs call:v8_oprMore() ?>
		<?cs #call:v8_oprPrivacyIcon()?><?cs #这个移到title部分?>
	<?cs /if ?>
<?cs /def?>

<?cs def:v8_operate_end()?>
	</p>
<?cs /def?>


<?cs #:操作区组件默认入口 ?>
<?cs def:v8_operate() ?>
	<?cs call:v8_operate_start()?>
	<?cs call:v8_operate_end()?>
<?cs /def ?>
