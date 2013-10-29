<?cs def:_likeinfo_userlist()?>
	<?cs set:_likeinfo_user_len = subcount(qfv.like.users.item)?>
	<?cs loop:i = 0, _likeinfo_user_len - 1, 1?>
		<?cs call:userLink_comp(qfv.like.users.item[i])?>
		<?cs if:i < _like_user_len - 1?>、<?cs /if?>
	<?cs /loop?>
<?cs /def?>

<?cs ####
	/**
	 *展示已经赞了的信息
	 */
?>
<?cs def:likeinfo()?>
<?cs #/*自己赞了或者其他人赞了会出现这个提示信息*/?>
<?cs if:qfv.like.count > 0?>
	<div class="op_num bor2">
		<i class="ui_icon icon_op_like"></i>
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
			<?cs var:_like_others_num ?>
			<?cs var:_likeinfo_counts_tip_tail?>
			<?cs /if?>觉得很赞
		</span>
	</div>
<?cs /if?>
<?cs /def?>
