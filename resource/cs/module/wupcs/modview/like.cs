<?cs def:_likeinfo_userlist()?>
	<?cs set:_likeinfo_user_len = subcount(qfv.like.users.item)?>
	<?cs loop:i = 0, _likeinfo_user_len - 1, 1?>
		<?cs call:userLink_comp(qfv.like.users.item[i])?>
		<?cs if:i < _like_user_len - 1?>、<?cs /if?>
	<?cs /loop?>
<?cs /def?>

<?cs #/*移植过来的*/?>
<?cs def:_userIcon_comp(user, size)?>
	<?cs call:_userAvatar_url(user, size)?>
	<?cs if:!user.who?><?cs set:user.who = USER_PLATFORM_WHO_QZONE?><?cs /if?>
	<?cs if:user.who == USER_PLATFORM_WHO_QZONE ?><?cs #/*QZONE用户*/?>
		<a href="<?cs call:echoUserlink(user)?>" class="item" target="_blank">
			<img class="q_namecard" 
				link="nameCard_<?cs var:user.uin ?> des_<?cs var:user.uin ?>" 
				alt="<?cs call:echoUsername(user, user.nickname) ?>" 
				src="<?cs var:_userAvatar_url.ret ?>" />
		</a>
	<?cs elif:user.who == USER_PLATFORM_WHO_PY ?><?cs #/*朋友用户*/?>
		<a href="<?cs call:echoUserlink(user)?>" class="item" target="_blank">
			<img title="<?cs call:echoUsername(user, user.nickname)?>" alt="<?cs call:echoUsername(user, user.nickname)?>" src="<?cs var:_userAvatar_url.ret ?>" />
		</a>
	<?cs /if ?>
<?cs /def?>

<?cs def:_likeinfo_owner()?>
	<?cs if:qfv.like.isliked == 1 ?>
		<?cs call:_userAvatar_url_by_uin(qz_metadata.meta.loginuin, 30)?>
		<a href="http://user.qzone.qq.com/<?cs var:qz_metadata.meta.loginuin ?>" class="item _ownerlike" target="_blank">
			<img class="q_namecard" link="nameCard_<?cs var:qz_metadata.meta.loginuin ?> des_<?cs var:qz_metadata.meta.loginuin ?>" src="<?cs var:_userAvatar_url_by_uin.ret ?>" />
		</a>
		<?cs set:_olike=1?>
	<?cs else?>
		<?cs set:_olike=0?>
	<?cs /if ?>
<?cs /def?>

<?cs def:_likeinfo_usericonlist()?>
	<?cs call:_likeinfo_owner()?>
	<?cs if:subcount(qfv.like.users.item) < 15?><?cs #/*V6中先只展示2行赞，不然IE6有空行bug*/?>
		<?cs set:_likeinfo_user_len = subcount(qfv.like.users.item)?>
	<?cs else ?>
		<?cs set:_likeinfo_user_len = 15?>
	<?cs /if ?>
	<?cs loop:i = 0, _likeinfo_user_len - _olike, 1?>
		<?cs call:_userIcon_comp(qfv.like.users.item[i], 30)?>
	<?cs /loop?>
<?cs /def?>
<?cs #/*移植过来的*/?>


<?cs ####
	/**
	 *展示已经赞了的信息
	 */
?>
<?cs def:likeinfo()?>
<?cs #/*自己赞了或者其他人赞了会出现这个提示信息*/?>
<?cs #/*目前给尾数0的V6用户放V8的赞*/?>

<?cs set:_units=string.slice(qfv.meta.loginuin, string.length(qfv.meta.loginuin)-1, string.length(qfv.meta.loginuin))?>
<?cs if:_units == 0 && qfv.meta.feedstype != UC_WUP_FEEDSTYPE_PSV?>
    <?cs #/*V8的展示*/?>
	<?cs if:qfv.like.isliked==1 || qfv.like.likeinfo > 0?>
    <div class="f-like _likeInfo" likeinfo="<?cs var:qfv.like.likeinfo?>">
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
				<div class="icon-btn">
					<a class="praise bor2 bg2_hover qz_like_btn_v3" href="javascript:;">
						<i class="ui-icon icon-praise <?cs if:qfv.like.isliked == 1 ?>icon-praised<?cs /if?>"></i>
					</a>
				</div>
				<?cs call:_likeinfo_owner()?>
				<?cs if:qfv.like.isliked == 1 ?><?cs #/*只有自己赞了*/?>
					<?cs if:_like_others_num-1 > 0 ?>
						<a class="txt-item _showLikeList" href="javascript:;" data-unikey="<?cs var:html_encode(qfv.like.unikey, 1)?>" title="<?cs var:_likeinfo_counts_tip_pre ?><?cs var:_like_others_num ?><?cs var:_likeinfo_counts_tip_tail?>觉得很赞">
						<?cs var:_likeinfo_counts_tip_pre ?><?cs var:_like_others_num ?><?cs var:_likeinfo_counts_tip_tail?>觉得很赞</a>
					<?cs else ?>
						<span class="txt-item">觉得很赞</span>
					<?cs /if?>
				<?cs /if?>
			<?cs else ?>
				<div class="icon-btn">
					<a class="praise bor2 bg2_hover qz_like_btn_v3" href="javascript:;">
						<i class="ui-icon icon-praise <?cs if:qfv.like.isliked == 1 ?>icon-praised<?cs /if?>"></i>
					</a>
				</div>
				<?cs call:_likeinfo_usericonlist()?>

				<?cs if:_likeinfo_counts_tip && _like_others_num > 0?>
					<a class="txt-item _showLikeList" href="javascript:;" data-unikey="<?cs var:html_encode(qfv.like.unikey, 1)?>" title="<?cs var:_likeinfo_counts_tip_pre ?><?cs var:_like_others_num ?><?cs var:_likeinfo_counts_tip_tail?>觉得很赞">
						<?cs var:_likeinfo_counts_tip_pre ?><?cs var:_like_others_num ?><?cs var:_likeinfo_counts_tip_tail?>觉得很赞</a>
				<?cs else ?>
					<span class="txt-item">觉得很赞</span>
				<?cs /if?>
			<?cs /if?>
		</div>
	</div>
	<?cs /if?>
<?cs elif:qfv.like.count > 0 ?>
    <?cs #/*V6的展示*/?>
	<div class="f_like _likeInfo" likeinfo="<?cs var:qfv.like.likeinfo?>">
		<i class="ui_ico icon_hand ui_ico_f qz_like_btn_v3 hand" style="cursor: pointer;"></i>
		<span class="_ilike">
			<?cs if:qfv.like.isliked?>
				我<?cs if:qfv.like.count > 1?>和<?cs /if?>
			<?cs /if?>
		</span>

		<?cs call:_likeinfo_userlist()?>

		<?cs if:qfv.like.likeinfo > 0?><?cs #/*拉出来赞的好友列表*/?>
			<?cs set:_like_user_not_show_num = qfv.like.count-qfv.like.likeinfo-qfv.like.isliked?>
			<?cs if:_like_user_not_show_num > 0?>
				<?cs set:_likeinfo_counts_tip=1 ?>
				<?cs set:_likeinfo_counts_tip_pre = "等"?>
				<?cs set:_like_others_num = qfv.like.count ?>
				<?cs set:_likeinfo_counts_tip_tail =  "人都"?>
			<?cs /if?>
		<?cs else ?><?cs #没有拉出来赞的人列表，只显示 "我和xx人"?>
				<?cs set:_likeinfo_counts_tip=1 ?>
				<?cs set:_likeinfo_counts_tip_pre = ""?>
				<?cs set:_like_others_num = qfv.like.count - qfv.like.isliked?>
				<?cs set:_likeinfo_counts_tip_tail =  "人"?>
		<?cs /if?>

		<?cs if:_likeinfo_counts_tip && _like_others_num >0?>
		<span class="_likecnt<?cs if:qfv.like.likeinfo?> ui_mr5<?cs /if?>">
			<?cs var:_likeinfo_counts_tip_pre ?>
			<a href="javascript:;" class="c_tx _showLikeList" data-unikey="<?cs var:html_encode(qfv.like.unikey, 1) ?>" >
				<?cs var:_like_others_num ?>
			</a>
			<?cs var:_likeinfo_counts_tip_tail?>
		</span>
		<?cs /if?>觉得很赞
	</div>

<?cs /if?>
<?cs /def?>
