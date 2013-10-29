<?cs #:附件详情 ?>
<?cs def:attachInfo(mod) ?>
	<?cs set:qz_attachinfo_count = qz_metadata.content_box.attachnum ?>
	<?cs set:qz_attachinfo_node = qz_metadata.content_box.attachtype ?>
	<?cs set:qz_attachinfo_arr = 0 ?>
	<?cs set:qz_attachinfo_index = 0 ?>
	<?cs if:qz_attachinfo_count > 1 ?>

	<?cs #:qz_attachinfo_node 二进制用作字符串，需要补足位数 ?>
	<?cs def:fillAttachinfoNode() ?>
		<?cs if:string.length(qz_attachinfo_node) < 7 ?>
			<?cs set:qz_attachinfo_node = "0"+qz_attachinfo_node ?>
			<?cs call:fillAttachinfoNode() ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs call:fillAttachinfoNode() ?>

	<?cs #:动画 附件 投票 视频 网页 图片 ?>
		<?cs if:qz_metadata.content_box.attachurl ?>
			<a href="<?cs var:qz_metadata.content_box.attachurl ?>" class="c_tx" target="_blank">
		<?cs else ?>
			<a href="http://user.qzone.qq.com/<?cs var:qz_metadata.metadata.uin ?>/mood/<?cs var:qz_metadata.metadata.blogid ?>.1" class="c_tx" target="_blank">
		<?cs /if ?>
		<span class="ui_mr10">内含
		<?cs if:string.slice(qz_attachinfo_node,4,5) == 1 ?>
			<?cs set:qz_attachinfo_arr[qz_attachinfo_index] = '视频' ?>
			<?cs set:qz_attachinfo_index = qz_attachinfo_index + 1 ?>
		<?cs /if ?>
		<?cs if:string.slice(qz_attachinfo_node,6,7) == 1 ?>
			<?cs set:qz_attachinfo_arr[qz_attachinfo_index] = '图片' ?>
			<?cs set:qz_attachinfo_index = qz_attachinfo_index + 1 ?>
		<?cs /if ?>
		<?cs if:string.slice(qz_attachinfo_node,0,1) == 1 ?>
			<?cs set:qz_attachinfo_arr[qz_attachinfo_index] = '音乐' ?>
			<?cs set:qz_attachinfo_index = qz_attachinfo_index + 1 ?>
		<?cs /if ?>
		<?cs if:string.slice(qz_attachinfo_node,1,2) == 1 ?>
			<?cs set:qz_attachinfo_arr[qz_attachinfo_index] = '动画' ?>
			<?cs set:qz_attachinfo_index = qz_attachinfo_index + 1 ?>
		<?cs /if ?>
		<?cs if:string.slice(qz_attachinfo_node,2,3) == 1 ?>
			<?cs set:qz_attachinfo_arr[qz_attachinfo_index] = '附件' ?>
			<?cs set:qz_attachinfo_index = qz_attachinfo_index + 1 ?>
		<?cs /if ?>
		<?cs loop:i = 0, subcount(qz_attachinfo_arr) - 1, 1 ?>
			<?cs if:i == 0 ?>
			<?cs elif:i == subcount(qz_attachinfo_arr) - 1?>
				和
			<?cs else ?>、
			<?cs /if ?>
			<?cs var:qz_attachinfo_arr[i] ?>
		<?cs /loop ?>
		 查看详情</span>
	</a>
	<?cs /if ?>
<?cs /def ?>