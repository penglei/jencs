<?cs #{/*需要使用wup元数据
	user必须带有uin,who,name三个信息
	*/
?>
<?cs def:echoUsername(user, defaultName)?>
	<?cs if:string.length(qz_metadata.remarkPool["u" + user.uin]) > 0 ?>
		<?cs var:html_encode(qz_metadata.remarkPool["u" + user.uin], 1)?>
	<?cs elif:string.length(defaultName) ?>
		<?cs var:html_encode(defaultName, 1)?>
	<?cs else ?>
		<?cs var:user.uin?>
	<?cs /if ?>
<?cs /def?>

<?cs #:user必须带有uin,who两个信息?>
<?cs def:echoUserlink(user)?>
	<?cs if:user.who == USER_PLATFORM_WHO_QZONE?>
		http://user.qzone.qq.com/<?cs var:user.uin?>
	<?cs elif:user.who == USER_PLATFORM_WHO_PY?>
		http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:user.uin?>"
	<?cs elif:user.who == USER_PLATFORM_WHO_WEIBO?>
		http://rc.qzone.qq.com/myhome/weibo/profile/<?cs var:user.uin?>
	<?cs else ?>
		http://user.qzone.qq.com/<?cs var:user.uin?>
	<?cs /if?>
<?cs /def?>

<?cs def:userLink_comp(user)?>
	<?cs if:user.uin?>
	<?cs if:!user.who?><?cs set:user.who = USER_PLATFORM_WHO_QZONE?><?cs /if?>
	<a class="<?cs alt:user.cls_color?>c_tx<?cs /alt?><?cs #配置默认的颜色为链接色?>
			<?cs if:user.who == USER_PLATFORM_WHO_QZONE?> q_namecard<?cs /if?>" 

	<?cs call:echoStyle(user)?> 

	<?cs if:user.who == USER_PLATFORM_WHO_QZONE?>
		link="nameCard_<?cs var:user.uin?>" 
	<?cs /if?>

		target="_blank" 
		href="<?cs call:echoUserlink(user)?>"
	>
		<?cs alt:user.prefix?><?cs /alt?>
		<?cs call:echoUsername(user, user.nickname)?>
	</a>
	<?cs /if?>
<?cs /def?>

<?cs def:authSpaceIcon_comp()?>
	<img class="ui_mr5" style="vertical-align: -2px;" 
		src="<?cs var:g_siDomain?>/ac/qzone_v5/client/auth_icon.png" 
		title="腾讯认证" 
		alt="腾讯认证" />
<?cs /def?>

<?cs def:_userAvatar_url(user, size)?>
	<?cs if:!size?><?cs set:size = 30?><?cs /if?>
	<?cs if:!user.who?><?cs set:user.who = USER_PLATFORM_WHO_QZONE?><?cs /if?>
	<?cs if:user.who == USER_PLATFORM_WHO_QZONE || user.who == USER_PLATFORM_WHO_WEIBO?>
		<?cs if:string.length(qz_metadata["logoPool" + size]['u' + user.uin])>0 ?>
			<?cs set:_userAvatar_url.ret = qz_metadata["logoPool" + size]['u' + user.uin] ?>
		<?cs else ?>
			<?cs set:_user_icon_mod = user.uin % 4 + 1?>
			<?cs set:_userAvatar_url.ret = "http://qlogo" + _user_icon_mod +
											".store.qq.com/qzone/" +
											user.uin + "/" + user.uin + "/" + size?>
		<?cs /if?>
	<?cs elif:user.who == USER_PLATFORM_WHO_PY ?>
		<?cs if:string.length(qz_metadata["xylogoPool" + size]['u' + user.uin]) > 0 ?>
			<?cs set:_userAvatar_url.ret = qz_metadata["xylogoPool" + size]['u' + user.uin]?>
		<?cs elif:user.img ?>
			<?cs set:_userAvatar_url.ret = "http://xy.store.qq.com/campus/" + user.img + "/" + size?>
		<?cs else ?>
			<?cs set:_userAvatar_url.ret = "http://py.qlogo.cn/friend/" + user.uin + "/audited/" + size?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs def:_userAvatar_url_by_uin(uin, size)?>
	<?cs if:!size?><?cs set:size = 30?><?cs /if?>
	<?cs if:string.length(qz_metadata["logoPool" + size]['u' + uin])>0 ?>
		<?cs set:_userAvatar_url_by_uin.ret = qz_metadata["logoPool" + size]['u' + uin] ?>
	<?cs else ?>
		<?cs set:_user_icon_mod = uin % 4 + 1?>
		<?cs set:_userAvatar_url_by_uin.ret = "http://qlogo" + _user_icon_mod + ".store.qq.com/qzone/" + uin + "/" + uin + "/" + size?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * 输出用户头像
	 */
?>
<?cs def:userAvatar_comp(user, size)?>
	<?cs if:!user.who?><?cs set:user.who = USER_PLATFORM_WHO_QZONE?><?cs /if?>
	<?cs call:_userAvatar_url(user, size)?>
	<?cs if:user.who == USER_PLATFORM_WHO_QZONE || user.who == USER_PLATFORM_WHO_WEIBO?><?cs #/*QZONE用户，微博用户的头像也用空间的*/?>
		<a href="http://user.qzone.qq.com/<?cs var:user.uin ?>" target="_blank">
			<img class="q_namecard" 
				link="nameCard_<?cs var:user.uin ?> des_<?cs var:user.uin ?>" 
				alt="<?cs call:echoUsername(user, user.nickname) ?>" 
				src="<?cs var:_userAvatar_url.ret ?>" />
		</a>
	<?cs elif:user.who == USER_PLATFORM_WHO_PY ?><?cs #/*朋友用户*/?>
		<img alt="<?cs call:echoUsername(user, user.nickname)?>" 
			src="<?cs var:_userAvatar_url.ret ?>" />
	<?cs /if ?>
<?cs /def?>
