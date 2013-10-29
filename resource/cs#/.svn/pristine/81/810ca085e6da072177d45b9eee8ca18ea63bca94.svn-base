<?cs if:qz_metadata.ver == 1 ?>
赞了我的<?cs var:qz_metadata.app.name ?>：<a class="c_tx" href="<?cs var:qz_metadata.subject.url ?>" target="_blank"><?cs var:qz_metadata.subject.title ?></a>
<?cs /if ?>

<?# 转载日志去掉源作者的信息[ticket:3813011] ?>
<?cs if:qz_metadata.ver == 2 ?>
	<?cs set:actiontext = '转发' ?>	

	<?cs if:qz_metadata.app.id == 2 ?>
		<?cs set:actiontext = '' ?><?#转载?>		
	<?cs /if ?>
	<?cs if:qz_metadata.app.id == 202 ?>
		<?cs set:actiontext = '分享' ?>		
	<?cs /if ?>

	<?cs if:actiontext ?>
		<?cs set:actionDesc = 
			actiontext + 
			'的' + 
			'<a href="http://user.qzone.qq.com/' + qz_metadata.subject.rt_uin + '" target="_blank" class="c_tx q_des q_namecard" link="nameCard_' + qz_metadata.subject.rt_uin + ' des_' + qz_metadata.subject.rt_uin +'">' + 
				qz_metadata.subject.rt_name + 
			'</a>'
		?>
	<?cs /if ?>

赞了我<?cs var:actionDesc ?>的<?cs var:qz_metadata.app.name ?>：<a class="c_tx" href="<?cs var:qz_metadata.subject.url ?>" target="_blank"><?cs var:qz_metadata.subject.title ?></a>
<?cs /if ?>
