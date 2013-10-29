<?cs def:v8_userIcon_comp(user, size)?>
	<?cs call:v8__userAvatar_url(user, size)?>
	<?cs if:!user.who?><?cs set:user.who = USER_PLATFORM_WHO_QZONE?><?cs /if?>
	<?cs if:user.who == USER_PLATFORM_WHO_QZONE ?><?cs #/*QZONE用户*/?>
		<a href="<?cs call:v8_echoUserlink(user)?>" class="item" target="_blank">
			<img class="q_namecard" 
				link="nameCard_<?cs var:user.uin ?> des_<?cs var:user.uin ?>" 
				alt="<?cs call:v8_echoUsername(user, user.nickname) ?>" 
				src="<?cs var:v8__userAvatar_url.ret ?>" />
		</a>
	<?cs elif:user.who == USER_PLATFORM_WHO_PY ?><?cs #/*朋友用户*/?>
		<a href="<?cs call:v8_echoUserlink(user)?>" class="item" target="_blank">
			<img title="<?cs call:v8_echoUsername(user, user.nickname)?>" alt="<?cs call:v8_echoUsername(user, user.nickname)?>" src="<?cs var:v8__userAvatar_url.ret ?>" />
		</a>
	<?cs /if ?>
<?cs /def?>

<?cs def:v8__likeinfo_owner()?>
	<?cs if:qfv.like.isliked == 1 ?>
		<?cs call:v8__userAvatar_url_by_uin(qz_metadata.meta.loginuin, 30)?>
		<a href="http://user.qzone.qq.com/<?cs var:qz_metadata.meta.loginuin ?>" class="item _ownerlike" target="_blank">
			<img class="q_namecard" link="nameCard_<?cs var:qz_metadata.meta.loginuin ?> des_<?cs var:qz_metadata.meta.loginuin ?>" src="<?cs var:v8__userAvatar_url_by_uin.ret ?>" />
		</a>
	<?cs /if ?>
<?cs /def?>

<?cs def:v8__likeinfo_usericonlist()?>
	<?cs call:v8__likeinfo_owner()?>
	<?cs set:_likeinfo_user_len = subcount(qfv.like.users.item)?>
	<?cs loop:i = 0, _likeinfo_user_len - 1, 1?>
		<?cs call:v8_userIcon_comp(qfv.like.users.item[i], 30)?>
	<?cs /loop?>
<?cs /def?>

<?cs ####
	/**
	 *展示已经赞了的信息
	 */
?>
<?cs def:v8_likeinfo()?>
<?cs #/*自己赞了或者其他人赞了会出现这个提示信息*/?>
<?cs if:(qfv.like.isliked == 1 || qfv.like.likeinfo > 0 ) && qfv.meta.feedstype != UC_WUP_FEEDSTYPE_PSV?><?cs #被动feeds不展示赞列表信息?>
	<div class="f-like _likeInfo" likeinfo="<?cs var:qfv.like.likeinfo?>">
		<div class="icon-btn">
			<a href="javascript:;" 
				data-islike="<?cs var:qfv.like.isliked?>" 
				data-likecnt="<?cs var:qfv.like.count?>" 
				data-showcount="<?cs var:qfv.like.likeinfo?>" 
				data-unikey="<?cs var:html_encode(qfv.like.unikey, 1)?>" 
				data-curkey="<?cs var:html_encode(qfv.like.curkey, 1)?>" 
				data-clicklog="like" 
				class="praise qz_like_prase"
			>
				<i class="ui-icon icon-praise<?cs if:qfv.like.isliked==1?>-hover<?cs /if ?>"></i>
			</a>
			<div class="bubble" style="display:none;">
				<div class="bd">+1</div>
				<b class="arrow arrow-down"></b>
			</div>
		</div>
		<?cs #/*拉出来赞的好友列表*/?>
		<?cs if:qfv.like.likeinfo > 0?>
			<?cs set:_like_user_not_show_num = qfv.like.count - qfv.like.likeinfo - qfv.like.isliked?>
			<?cs if:_like_user_not_show_num > 0?>
				<?cs set:_likeinfo_counts_tip=1 ?>
				<?cs set:_likeinfo_counts_tip_pre = "等"?>
				<?cs set:_like_others_num = qfv.like.count ?>
				<?cs set:_likeinfo_counts_tip_tail =  "人都"?>
			<?cs /if?>
		<?cs else ?><?cs #没有拉出来赞的人列表，只显示 "我和xx人"?>
				<?cs set:_likeinfo_counts_tip=1 ?>
				<?cs set:_likeinfo_counts_tip_pre = "等"?>
				<?cs set:_like_others_num = qfv.like.count?>
				<?cs set:_likeinfo_counts_tip_tail =  "人都"?>
		<?cs /if?>

		<div class="user-list">
			<?cs if:qfv.like.likeinfo == 0 ?>
				<?cs call:v8__likeinfo_owner()?>
				<?cs if:qfv.like.isliked == 1 ?>
					<?cs if:_like_others_num-1 > 0 ?>
						<a href="javascript:;" 
							class="more _showLikeList" 
							data-unikey="<?cs var:html_encode(qfv.like.unikey, 1)?>" 
							title="<?cs var:_likeinfo_counts_tip_pre ?><?cs var:_like_others_num ?><?cs var:_likeinfo_counts_tip_tail?>觉得很赞"
						>
							<i class="ui-icon icon-more"></i>
						</a>
					<?cs else ?>

					<?cs /if?>
				<?cs /if?>
			<?cs else ?>
				<?cs call:v8__likeinfo_usericonlist()?>
				<?cs if:_likeinfo_counts_tip && _like_others_num > 0?>
					<a href="javascript:;" class="more _showLikeList" data-unikey="<?cs var:html_encode(qfv.like.unikey, 1)?>" title="<?cs var:_likeinfo_counts_tip_pre ?><?cs var:_like_others_num ?><?cs var:_likeinfo_counts_tip_tail?>觉得很赞"><i class="ui-icon icon-more"></i></a>
				<?cs else ?>

				<?cs /if?>
			<?cs /if?>
		</div>
	</div>
<?cs /if?>
<?cs /def?>
