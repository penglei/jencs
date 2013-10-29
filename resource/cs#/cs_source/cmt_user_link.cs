<?cs #:用户链接组件 ?>
<?cs #:prefix 是前缀啊例如@ ?>
<?cs def:cmtUserLink(user, prefix) ?>
	<?cs if:user.type == 1 ||user.who == 1?>
		<a href="http://user.qzone.qq.com/<?cs var:user.uin ?>" class="q_namecard nickname c_tx" link="nameCard_<?cs var:user.uin ?>" target="_blank">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<?cs call:writeUserName(user.uin,user.name) ?> </a>
	<?cs elif:user.who == 2 || user.type == 2 ?>
		<a href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:user.uin ?>" class="c_tx" target="_blank">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<?cs var:html_encode(user.name, 1) ?></a>
	<?cs elif:user.who == 3 || user.type == 3 ?>
		<a href="http://rc.qzone.qq.com/myhome/weibo/profile/<?cs var:user.uin ?>" class="c_tx" target="_blank">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<?cs var:html_encode(user.name, 1) ?></a>
	<?cs else ?>
		<?cs var:html_encode(user.name, 1) ?>
	<?cs /if ?>
<?cs /def ?>