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
		<a href="#" class="ui_btn op_btn_like hotclick" 
			data-likecnt="<?cs var:qfv.like.count?>" 
			data-showcount="<?cs var:qfv.like.likeinfo?>" 
			data-hctag="spcaretab.<?cs if:qfv.like.isliked?>unlike<?cs else ?>like<?cs /if ?>.click">
			<i class="ui_icon icon_op_like"></i>
			<?cs if:qfv.like.isliked?>
				已赞
			<?cs else ?>
				赞
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
		<a href="#" class="ui_btn op_btn_comment hotclick" data-role="commentBtn" data-hctag="spcaretab.comment.click"><i class="ui_icon icon_op_comment"></i>评</a>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *生成转发按钮。注意:转发按钮已经不需要param参数了
	 */
?>
<?cs def:oprForward()?>
	<?cs if:subcount(qfv.operate.forward)?>
		<a href="#" class="ui_btn op_btn_rt hotclick" data-hctag="spcaretab.share.click" data-fwddenied=""><i class="ui_icon icon_op_rt"></i>转</a>
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

	<?cs #{/*操作区几个标准按钮*/?>
	<div class="op_btn">
		<?cs call:oprLike()?>
		<?cs call:oprComment()?>
		<?cs call:oprForward()?>
	</div>
	<?cs #}?>

<?cs /def?>

<?cs def:operate_end()?>
<?cs /def?>


<?cs #:操作区组件默认入口 ?>
<?cs def:operate() ?>
	<?cs if:subcount(qfv.operate) > 0?>
		<div class="op_btn">
			<a href="#" class="ui_btn op_btn_like hotclick" 
				data-likecnt="<?cs var:qfv.like.count?>" 
				data-showcount="<?cs var:qfv.like.likeinfo?>" 
				data-hctag="spcaretab.<?cs if:qfv.like.isliked?>unlike<?cs else ?>like<?cs /if ?>.click">
				<i class="ui_icon icon_op_like"></i>
				<?cs if:qfv.like.isliked?>
					已赞
				<?cs else ?>
					赞
				<?cs /if?>
			</a>
			<a href="#" class="ui_btn op_btn_comment hotclick" data-role="commentBtn" data-hctag="spcaretab.comment.click"><i class="ui_icon icon_op_comment"></i>评</a>
			<a href="#" class="ui_btn op_btn_rt hotclick" data-hctag="spcaretab.share.click" data-fwddenied=""><i class="ui_icon icon_op_rt"></i>转</a>
		</div>
	<?cs /if?>
<?cs /def ?>
