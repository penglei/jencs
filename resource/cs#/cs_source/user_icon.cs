<?cs def:getLogo_30(user,type) ?>
	<?cs if:user.type ?>
		<?cs set:uicon_type=user.type ?>
	<?cs else ?>
		<?cs set:uicon_type=type ?>
	<?cs /if ?>

	<?cs if:uicon_type==1 ?>
		<?cs if:string.length(qz_metadata.logoPool30['u'+user.uin])>0 ?>
			<?cs var:qz_metadata.logoPool30['u'+user.uin] ?>
		<?cs else ?>
			<?cs set:uinmod = user.uin % 4 + 1 ?>
			http://qlogo<?cs var:uinmod ?>.store.qq.com/qzone/<?cs var:user.uin ?>/<?cs var:user.uin ?>/30
		<?cs /if ?>
	<?cs elif:uicon_type==2 ?>
		<?cs if:string.length(qz_metadata.xylogoPool30['u'+user.uin])>0 ?>
			<?cs var:qz_metadata.xylogoPool30['u'+user.uin] ?>
		<?cs else ?>
			http://xy.store.qq.com/campus/<?cs var:user.img ?>/30
		<?cs /if ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:getLogo_50(uin,type) ?>
	<?cs if:user.type ?>
		<?cs set:uicon_type=user.type ?>
	<?cs else ?>
		<?cs set:uicon_type=type ?>
	<?cs /if ?>
	<?cs if:uicon_type==1 ?>
		<?cs if:string.length(qz_metadata.logoPool50['u'+user.uin])>0 ?>
			<?cs var:qz_metadata.logoPool50['u'+user.uin] ?>
		<?cs else ?>
			<?cs set:uinmod = user.uin % 4 + 1 ?>
			http://qlogo<?cs var:uinmod ?>.store.qq.com/qzone/<?cs var:user.uin ?>/<?cs var:user.uin ?>/50
		<?cs /if ?>
	<?cs elif:uicon_type==2 ?>
		<?cs if:string.length(qz_metadata.xylogoPool50['u'+user.uin])>0 ?>
			<?cs var:qz_metadata.xylogoPool50['u'+user.uin] ?>
		<?cs else ?>
			http://xy.store.qq.com/campus/<?cs var:user.img ?>/50
		<?cs /if ?>
	<?cs /if ?>
<?cs /def ?>


<?cs #:头像组件 ?>
<?cs #:参数user ?>
<?cs def:userIcon(user) ?>
	<?cs if:user.type == 1 ?><?cs #:Qzone用户 ?>
		<a href="http://user.qzone.qq.com/<?cs var:user.uin ?>" target="_blank">
			<img class="q_namecard" link="nameCard_<?cs var:user.uin ?> des_<?cs var:user.uin ?>" alt="<?cs call:writeUserName(user.uin,user.name) ?>" src="<?cs call:getLogo_30(user,1) ?>" />
		</a>
	<?cs elseif:user.type == 2 ?><?cs #:朋友用户 ?>
		<img alt="<?cs var:html_encode(user.name, 1) ?>" src="<?cs call:getLogo_30(user,2) ?>" />
	<?cs /if ?>
<?cs /def ?>
<?cs def:userIconStyle(user, style)?>
	<a href="http://user.qzone.qq.com/<?cs var:user.uin ?>" class="<?cs if:style == 'act'?>act_ava<?cs /if?>" target="_blank">
		<img class="q_namecard <?cs if:style == 'act'?>sslist_ava<?cs /if?>" link="nameCard_<?cs var:user.uin ?> des_<?cs var:user.uin ?>" alt="<?cs call:writeUserName(user.uin,user.name) ?>" src="<?cs call:getLogo_30(user,1) ?>" />
	</a>
<?cs /def?>