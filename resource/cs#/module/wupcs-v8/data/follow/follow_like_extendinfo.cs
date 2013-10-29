<?cs #:赞被动feeds的赞信息放在正文区的下面，操作栏的上面 ?>

<?cs set:data_like_extendinfo_index=0 ?>
<?cs #:
	/**/
	function like_extendinfo_index++(){}
?>

<?cs def:like_extendinfo_index++() ?>
	<?cs set:data_like_extendinfo_index=data_like_extendinfo_index+1 ?>
<?cs /def ?>
<?cs ####
	/**
	 * 赞扩展信息中的文字
	 */
?>
<?cs def:data_like_extendinfo_txt(index, text)?>
	<?cs call:data_con_txt("content.like_extendinfo.con." + data_like_extendinfo_index, text, "tip", 0)?>
	<?cs call:like_extendinfo_index++() ?>
<?cs /def?>

<?cs ####
	/**
	 * 赞扩展信息中的链接
	 */
?>
<?cs def:data_like_extendinfo_url(index, text, url)?>
	<?cs call:data_con_url("content.like_extendinfo.con." + data_like_extendinfo_index, text, url, "link", 0)?>
	<?cs call:like_extendinfo_index++() ?>
<?cs /def?>

<?cs ####
	/**
	 * 赞扩展信息中的昵称
	 */
?>
<?cs def:data_like_extendinfo_nick(index, uin, who, name)?>
	<?cs call:data_con_nick("content.like_extendinfo.con." + data_like_extendinfo_index, uin, who, name, "link", 0)?>
	<?cs call:like_extendinfo_index++() ?>
<?cs /def?>


<?cs ####
	/**
	 *生成赞的数据
	 */
?>
<?cs def:data_like_extendinfo_data()?>
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
			
			<?cs #:我知否赞过 ?>
			<?cs if:qz_metadata.qz_data[_qz_data_key].LIKE.isliked ?>
				<?cs set:data_like_extendinfo_data.isliked = 1 ?>
			<?cs else ?>
				<?cs set:data_like_extendinfo_data.isliked = 0 ?>
			<?cs /if?>

			<?cs if:qz_metadata.qz_data[_qz_data_key].LIKE.cnt > 0?>
				<?cs set:_like_count = qz_metadata.qz_data[_qz_data_key].LIKE.cnt?>
			<?cs else ?>
				<?cs set:_like_count = 0?>
			<?cs /if?>

			<?cs #/*赞的总数，包括我*/?>
			<?cs set:data_like_extendinfo_data.count = _like_count ?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs def:data_like_extendinfo_get_txt(uininfo) ?>
	<?cs set:likeinfo = subcount(uininfo.uin_like) ?> <?cs #/*赞数据里可用的用户总数(可能会小于赞的总数)*/?>
	<?cs #set:likeuinnum = qz_metadata.orgdata.extendinfo.likeuinnum ?>  <?cs #:被动feeds ?>
	<?cs set:isLiked = data_like_extendinfo_data.isliked ?>
	<?cs set:count = data_like_extendinfo_data.count ?> <?cs #:总赞数，包括自己 ?>

	<?cs if:isLiked ?>
		<?cs call:data_like_extendinfo_txt(0,"我")?>
		<?cs if:likeinfo >= 2?>
			<?cs call:data_like_extendinfo_txt(0,"和")?>
		<?cs /if ?>
	<?cs /if ?>
	<?cs set:likeinfoForView = likeinfo ?>
	<?cs if:likeinfo > 11 ?>
		<?cs set:likeinfoForView = 11 ?>
	<?cs /if?>

	<?cs loop:i = 1, likeinfoForView - 1, 1?>
		<?cs call:data_like_extendinfo_nick(0,uininfo.uin_like[i], uininfo.who_like[1], uininfo.nick_like[1]) ?>
		<?cs if:i < likeinfoForView - 1?>
			<?cs call:data_like_extendinfo_txt(0,"、")?>
		<?cs else ?>
			<?cs call:data_like_extendinfo_txt(0," ")?>
		<?cs /if?>
	<?cs /loop?>

	<?cs if:likeinfo <=1 ?>
		<?cs if:isLiked ?>
			<?cs set:_like_user_not_show_num = #count-#likeinfo-#isLiked  ?>
			<?cs if: _like_user_not_show_num > 0 ?>
				<?cs call:data_like_extendinfo_txt(0,"等")?>
				<?cs call:data_like_extendinfo_txt(0,count -1)?>
				<?cs call:data_like_extendinfo_txt(0,"人")?>
			<?cs /if ?>
			<?cs call:data_like_extendinfo_txt(0,"也觉得赞")?>
		<?cs /if ?>
	<?cs elif:likeinfo <= 11 ?>
		<?cs set:_like_user_not_show_num = #count-#likeinfo-#isLiked?>
		<?cs if:_like_user_not_show_num > 0?>
			<?cs call:data_like_extendinfo_txt(0,"等")?>
			<?cs call:data_like_extendinfo_txt(0,count-1)?>
			<?cs call:data_like_extendinfo_txt(0,"人")?>
		<?cs /if?>
		<?cs call:data_like_extendinfo_txt(0,"也觉得赞")?>
	<?cs else ?>
		<?cs if:count > 1 ?>
			<?cs call:data_like_extendinfo_txt(0,"等")?>
			<?cs call:data_like_extendinfo_txt(0,count-1)?>
			<?cs call:data_like_extendinfo_txt(0,"人")?>
		<?cs /if ?>
		<?cs call:data_like_extendinfo_txt(0,"也觉得赞")?>
	<?cs /if ?>

<?cs /def ?>
<?cs #:赞被动feeds 的赞信息往下挪，放到 ?>
<?cs def:data_like_extendinfo() ?>
	<?cs if:qz_metadata.scope != SCOPE_FRIENDSHIP_ME_TO_FRIEND && qz_metadata.scope != SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
		<?cs call:data_like_extendinfo_data() ?>
		<?cs if:subcount(qz_metadata.relybody) > 0 && qz_metadata.meta.loginuin == qz_metadata.relybody[0].uin ?>
			<?cs call:data_like_extendinfo_get_txt(qz_metadata.relybody[0].extendinfo.uininfo) ?>
		<?cs else ?>
			<?cs call:data_like_extendinfo_get_txt(qz_metadata.orgdata.extendinfo.uininfo) ?>
		<?cs /if?>
	<?cs /if?>
<?cs /def ?>

<?cs call:data_like_extendinfo() ?>
