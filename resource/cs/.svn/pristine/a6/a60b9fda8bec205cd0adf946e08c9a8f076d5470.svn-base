<?cs set:G_LIKE_USER_SHOW_MAXNUM = 24?>
<?cs ####
	/**
	 *生成赞的数据
	 */
?>
<?cs def:data_like()?>
	<?cs #/*if:qz_metadata.qz_data.version == 1 && !qz_metadata.accessright*/ ?>
	<?cs if:!qz_metadata.accessright ?>
		<?cs  if:string.length(qz_metadata.qz_data.key1) > 0 || string.length(qz_metadata.qz_data.key2) > 0?>

			<?cs if:qz_metadata.qz_data.key2.LIKE.cnt > 0?>
				<?cs set:_qz_data_key = "key2"?>
			<?cs elif:qz_metadata.qz_data.key1.LIKE.cnt > 0?>
				<?cs set:_qz_data_key = "key1"?>
			<?cs else ?>
				<?cs set:_qz_data_key = "key2"?>
			<?cs /if?>

			<?cs #/*call:qfv("like.keyname", _qz_data_key)*/?>

			<?cs if:qz_metadata.qz_data.key2?>
				<?cs set:_like_unikey = qz_metadata.qz_data.key2?>
			<?cs elif:qz_metadata.qz_data.key1?>
				<?cs set:_like_unikey = qz_metadata.qz_data.key1?>
			<?cs /if?>

			<?cs if:qz_metadata.qz_data[_qz_data_key].LIKE.isliked ?>
				<?cs call:qfv("like.isliked", 1)?>
			<?cs else ?>
				<?cs call:qfv("like.isliked", 0)?>
			<?cs /if?>

			<?cs #{/*评论区的赞信息*/?>
			<?cs #/*TODO 这里的逻辑很奇怪，需要找后台再核对一下*/?>
			<?cs #if:subcount(qz_metadata.qz_data[_qz_data_key].LIKE.user)?>
				<?cs set:_likeinfo = subcount(qz_metadata.qz_data[_qz_data_key].LIKE.user)?>
			<?cs #elif:subcount(qz_metadata.qz_data[_qz_data_key].LIKE.user.0)?>
				<?cs #set:_likeinfo = subcount(qz_metadata.qz_data[_qz_data_key].LIKE.user.0)?>
			<?cs #else ?>
				<?cs #set:_likeinfo = 0?>
			<?cs #/if?>
			<?cs if: _likeinfo > G_LIKE_USER_SHOW_MAXNUM ?>
				<?cs set:_likeinfo=G_LIKE_USER_SHOW_MAXNUM ?>
			<?cs /if ?>
			<?cs call:qfv("like.likeinfo", _likeinfo)?><?cs #/*赞数据里可用的用户总数(可能会小于赞的总数)*/?>

			<?cs #}/*end: 评论区的赞信息*/?>

			<?cs call:qfv("like.unikey", _like_unikey)?>
			<?cs call:qfv("like.curkey", qz_metadata.qz_data.key1)?>

			<?cs if:qz_metadata.qz_data[_qz_data_key].LIKE.cnt > 0?>
				<?cs set:_like_count = qz_metadata.qz_data[_qz_data_key].LIKE.cnt?>
			<?cs else ?>
				<?cs set:_like_count = 0?>
			<?cs /if?>

			<?cs #/*赞的总数*/?>
			<?cs call:qfv("like.count", _like_count)?>

			<?cs #/*拷贝G_LIKE_USER_SHOW_MAXNUM个赞用户数据*/?>
			<?cs call:i()?>
			<?cs if:subcount(qz_metadata.qz_data[_qz_data_key].LIKE.user.0)?>
				<?cs set:_like_user_len = subcount(qz_metadata.qz_data[_qz_data_key].LIKE.user)?>
				<?cs if:_like_user_len > G_LIKE_USER_SHOW_MAXNUM ?>
					<?cs set:_like_user_len = G_LIKE_USER_SHOW_MAXNUM ?>
				<?cs /if?>
				<?cs loop:j = 0, _like_user_len - 1, 1?>
				<?cs with:user = qz_metadata.qz_data[_qz_data_key].LIKE.user[j]?>
					<?cs call:data_con_nick("like.users.item." + j,
											user.uin,
											user.type,
											user.name,
											"link", 0)?>
				<?cs /with?>
				<?cs /loop?>
			<?cs elif:subcount(qz_metadata.qz_data[_qz_data_key].LIKE.user)?>
				<?cs with:user = qz_metadata.qz_data[_qz_data_key].LIKE.user?>
					<?cs call:data_con_nick("like.users.item.0",
											user.uin,
											user.type,
											user.nickname,
											"link", 0)?>
				<?cs /with?>
			<?cs /if?>

		<?cs /if?>
	<?cs /if?>
<?cs /def?>

