<?cs #/*
	如果某个按钮只有一个业务在使用，不要放到这里面，放到各自的模板中去做
*/?>

<?cs ####
	/**
	 *操作区更多按钮 比如取消关注、屏蔽此人等操作放在这里，但基本通过js来绑定，这里只负责显示入口
	 */
?>
<?cs def:oprMore()?>
	<?cs if:subcount(qfv.operate.more_opr) > 0?>
	<a class="c_tx ui_mr10 <?cs var:qfv.operate.more_opr.class?>" 
		href="javascript:;"
		data-cmd="qz_opr_more"
	>
	<?cs var:qfv.operate.more_opr.text?>
	</a>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *操作区赞按钮，注意，赞的数据在qfv.like下面，操作区中不添加任何关于赞的数据
	 */
?>
<?cs def:oprLike()?>
	<?cs if:subcount(qfv.like) > 0?>
	<a class="qz_like_btn_v3 _likeBtn c_tx ui_mr10" 

		<?cs #/*是否已经赞了*/?>
		data-islike="<?cs var:qfv.like.isliked?>" 

		<?cs #/*赞的计数*/?>
		data-likecnt="<?cs var:qfv.like.count?>" 
		data-showcount="<?cs var:qfv.like.likeinfo?>" 
		data-unikey="<?cs var:html_encode(qfv.like.unikey,1)?>" 
		data-curkey="<?cs var:html_encode(qfv.like.curkey,1)?>" 
		href="javascript:;"
	>
	<?cs if:qfv.like.isliked?>
		取消赞
		<?cs if:qfv.like.count?>
			(<?cs var:qfv.like.count?>)
		<?cs /if?>
	<?cs else ?>
		赞
		<?cs if:qfv.like.count?>
			(<?cs var:qfv.like.count?>)
		<?cs /if?>
	<?cs /if?>
	</a>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *根据评论数生成评论按钮
	 */
?>
<?cs def:oprComment()?>
	<?cs if:subcount(qfv.operate.comment_btn) > 0?>
		<?cs ###在view里面可以直接修改qfv里的数据,相当于内部变量，不用表现在viewdata里?>
		<?cs ###比如这种data-role完全是为了配合前台的程序，跟数据层是没有关系的
				只不过我们需要用统一的输出逻辑来控制?>
		<?cs call:data_attrs("operate.comment_btn", "data-cmd", "qz_reply")?>
		<?cs #表示点进后使用我也说一句来触发评论。自己不带评论数据?>
		<?cs call:data_attrs("operate.comment_btn", "data-link", 1)?>
		<?cs #qz_btn_reply是用来评论时增加计数的?>
		<?cs call:data_addClassName("operate.comment_btn", "qz_btn_reply")?>

		<?cs call:set("operate.comment_btn", "mr", 10)?>
		<?cs call:set("operate.comment_btn", "color", "link")?>
		<?cs #有更多评论不一定会有moreflag ,moreflag只是告诉可不可以在feed里展开更多评论?>
		<?cs if:qfv.operate.comment_btn.moreflag?>
			<?cs #同时展开更多评论，所以添加一个role为more标志，用以前台进行判断?>
			<?cs call:data_attrs("operate.comment_btn", "data-role", "more")?>
		<?cs /if?>
		<?cs set:qfv.operate.comment_btn.js=1 ?>
		<?cs call:con_url(qfv.operate.comment_btn)?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *生成转发按钮。注意:转发按钮已经不需要param参数了
	 */
?>
<?cs def:oprForward()?>
	<?cs if:qfv.operate.forward?>
		<?cs call:get_con_style(qfv.operate.forward)?>
		<a 
			class="qz_retweet_btn <?cs var:get_con_style.ret.margin_class?> <?cs var:get_con_style.ret.cls_color?>" 
			href="javascript:;" 
			data-cmd="qz_popup" 
			data-version="4" 
			data-isnewtype="1" 
			data-type="ForwardingBox" 
			<?cs #目前已知装扮feeds还需要这个参数 ?>
			<?cs if: string.length(qfv.operate.forward.param) ?>
			data-param="<?cs var:qfv.operate.forward.param ?>" 
			<?cs /if ?>
			data-src="/qzone/app/controls/forwardingBox/forwardingBoxFacade.js" 
			data-link="/qzonestyle/qzone_app/app_feeds_v1/mood_feeds.css" 
			<?cs if:subcount(qfv.operate.privacy_icon)?>
			data-fwddenied=1
			<?cs /if?> 
		>
			转发<?cs if:qfv.operate.forward.count?>(<?cs var:qfv.operate.forward.count?>)<?cs /if?>
		</a>
	<?cs /if?>
<?cs /def?>

<?cs #//赞、转、评是三个标准操作按钮?>


<?cs ####
	/**
	 *浏览按钮
	 *up 浏览按钮是否是标准按钮呢?是不是应该考虑index排序的问题，visitor按钮有可能是最后一个
	 */
?>
<?cs def:oprVisitor()?>
	<?cs if:subcount(qfv.operate.visitor)?>
		<?cs #找时间把自定义节点干掉?>
		<qz:plugin
			 name="Visitor"
			 config="<?cs var:qfv.operate.visitor.param?>"
			 uin="<?cs var:qfv.operate.visitor.uin?>"
		>
			<?cs call:qfv("operate.visitor.notarget", 1)?><?cs #有target会跳到新页面，这是不行的?>
			<?cs set:qfv.operate.visitor.js=1 ?>
			<?cs call:con_url_start(qfv.operate.visitor)?>
				浏览<?cs if:qfv.operate.visitor.count?>(<?cs var:qfv.operate.visitor.count?>)<?cs /if?>
			<?cs call:con_url_end()?>
		</qz:plugin>
	<?cs /if?>
<?cs /def?>


<?cs ####
	/**
	 * 按钮：通过审核
	 */
?>
<?cs def:oprAudit()?>
	<?cs with: item=qfv.operate ?>
	<?cs if:subcount(item.auditbtn)?>
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
	<?cs /with ?>
<?cs /def?>


<?cs ####
	/**
	 * 按钮：删除
	 */
?>
<?cs def:oprDel()?>
	<?cs with: item=qfv.operate ?>
	<?cs if:subcount(item.qz_delete)?>
		<span class="ui_mr10">
			<qz:delete 
				action="<?cs var:item.qz_delete.action ?>" 
				param="<?cs var:item.qz_delete.param ?>"
			>删除</qz:delete>
		</span>
	<?cs /if?>
	<?cs /with ?>
<?cs /def?>
<?cs ####
	/**
	 *操作区按钮 删除动态
	 */
?>
<?cs def:oprDelFeed()?>
	<?cs if:subcount(qfv.operate.delete_feed_opr) > 0?>
	<a class="c_tx ui_mr10" 
		href="javascript:;"
		data-cmd="qz_opr_delete"
	>
	<?cs var:qfv.operate.delete_feed_opr.text?>
	</a>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * 按钮：屏蔽好友
	 */
?>
<?cs def:oprPrevent()?>
	<?cs if:subcount(qfv.operate.prevent) > 0?>
	<a class="c_tx ui_mr10" 
		href="javascript:;"
		data-cmd="qz_opr_prevent"
	>
	<?cs var:qfv.operate.prevent.text?>
	</a>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * 相册权限ICON
	 */
?>
<?cs def:oprPrivacyIcon()?>
	<?cs if:subcount(qfv.operate.privacy_icon)?>
		<i class="ui_icon icon_gray_lock" title="<?cs var:qfv.operate.privacy_icon.text?>"></i>
	<?cs /if?>
<?cs /def?>


<?cs ####
	/**
	 *操作区自定义按钮
	 */
?>
<?cs def:operate_item(oprItem)?>
	<?cs if:oprItem.action.type == "popup"?>
		<?cs call:con_popup(oprItem)?>
	<?cs else ?>
		<?cs if:oprItem.type == "txt"?>
			<?cs call:con_txt(oprItem)?>
		<?cs elif:oprItem.type == "url"?>
			<?cs call:con_url(oprItem)?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs def:operate_start()?>
	<p class="f_detail c_tx3">

	<?cs #/*时间信息在操作区*/?>
	<?cs call:oprTime()?>

	<?cs #{/*操作区几个标准按钮*/?>
	<?cs call:oprSource()?>

	<span class="feeds_tp_operate_v2 f_detail_op">
	<?cs if:string.slice(qfv.meta.loginuin, string.length(qfv.meta.loginuin)-1, string.length(qfv.meta.loginuin)) == 0 ?>
		<?cs #/*尾号0的把赞放到第三个*/?>
		<?cs call:oprComment()?>
		<?cs call:oprForward()?>
		<?cs call:oprLike()?>
	<?cs else ?>
		<?cs call:oprLike()?>
		<?cs call:oprComment()?>
		<?cs call:oprForward()?>
	<?cs /if ?>
		<?cs call:oprVisitor()?>
		<?cs call:oprAudit()?>
		<?cs call:oprDel()?>
		<?cs call:oprDelFeed()?>
		<?cs call:oprPrevent()?>
		<?cs call:oprMore()?>
	</span>
	<?cs #}?>

	<?cs call:oprPrivacyIcon()?>

<?cs /def?>

<?cs def:operate_end()?>
	</p>
<?cs /def?>


<?cs #:操作区组件默认入口 ?>
<?cs def:operate() ?>
	<?cs call:operate_start()?><?cs #先生成默认操作?>
	<?cs if:subcount(qfv.operate.opr) > 0?>
		<?cs set:_end = subcount(qfv.operate.opr) - 1?>
		<?cs loop:i = 0, _end, 1?>
			<?cs call:operate_item(qfv.operate.opr[i])?>
		<?cs /loop?>
	<?cs /if?>
	<?cs call:operate_end()?>
<?cs /def ?>
