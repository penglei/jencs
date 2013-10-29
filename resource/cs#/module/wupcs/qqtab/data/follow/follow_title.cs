<?cs ####
	/*赞标题区*/
?>

<?cs def:data_follow_subtitle()?>
	<?cs call:get_userWho_platform(qz_metadata.relybody[0].platformid, qz_metadata.relybody[0].platformsubid)?>
	<?cs call:data_init_cntTitle() ?>
	<?cs call:data_cntTitle_nick(qz_metadata.relybody[0].uin, get_userWho_platform.ret, qz_metadata.relybody[0].nickname) ?>

	<?cs if:subcount(qz_metadata.relybody[0].msg) > 0 && 
		(subcount(qz_metadata.relybody[0].msg) > 1 || qz_metadata.relybody[0].msg[0].type != 0 || string.length(qz_metadata.relybody[0].msg[0].content) > 0 ) ?>
		<?cs call:data_cntTitle_tipTxt("转：") ?>
		<?cs call:data_cntTitle_rich(qz_metadata.relybody[0].msg) ?>
	<?cs else ?>
		<?cs call:data_cntTitle_tipTxt("转了此条信息") ?>
	<?cs /if?>
<?cs /def?>

<?cs def:data_follow_title()?>
	<?cs call:i()?>

	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV ?>
		<?cs call:data_title_tipTxt("评论：")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ?>
		<?cs call:data_title_tipTxt("回复：")?>
	<?cs else ?>
		<?cs call:data_title_tipTxt("关注")?>
		<?cs if:subcount(qz_metadata.orgdata.itemdata) == 1 ?>
			<?cs call:data_title_nick(qz_metadata.orgdata.itemdata[0].itemid, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.itemdata[0].itemid) ?>
		<?cs elif:subcount(qz_metadata.orgdata.itemdata) == 2 ?>
			<?cs call:data_title_nick(qz_metadata.orgdata.itemdata[0].itemid, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.itemdata[0].itemid) ?>
			<?cs call:data_title_tipTxt("和")?>
			<?cs call:data_title_nick(qz_metadata.orgdata.itemdata[1].itemid, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.itemdata[1].itemid) ?>
		<?cs elif:subcount(qz_metadata.orgdata.itemdata) >= 3 ?>
			<?cs call:data_title_nick(qz_metadata.orgdata.itemdata[0].itemid, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.itemdata[0].itemid) ?>
			<?cs call:data_title_tipTxt("和")?>
			<?cs call:data_title_nick(qz_metadata.orgdata.itemdata[1].itemid, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.itemdata[1].itemid) ?>
			<?cs call:data_title_tipTxt("等" + qz_metadata.orgdata.itemcount + "个认证空间")?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>


<?cs def:data_like_title()?>
	<?cs call:i()?>
	<?cs if:qz_metadata.scope != SCOPE_FRIENDSHIP_ME_TO_FRIEND && qz_metadata.scope != SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
		<?cs if:subcount(qz_metadata.relybody) > 0 && qz_metadata.meta.loginuin == qz_metadata.relybody[0].uin ?>
			<?cs set:likedCount = subcount(qz_metadata.relybody[0].extendinfo.uininfo.uin_like) ?>
			<?cs if:likedCount == 2 ?>
				<?cs call:data_title_tipTxt("和")?>
				<?cs call:data_title_nick(qz_metadata.relybody[0].extendinfo.uininfo.uin_like[1], qz_metadata.relybody[0].extendinfo.uininfo.who_like[1], qz_metadata.relybody[0].extendinfo.uininfo.nick_like[1]) ?>
			<?cs elif:likedCount == 3 ?>
				<?cs call:data_title_tipTxt("和")?>
				<?cs call:data_title_nick(qz_metadata.relybody[0].extendinfo.uininfo.uin_like[1], qz_metadata.relybody[0].extendinfo.uininfo.who_like[1], qz_metadata.relybody[0].extendinfo.uininfo.nick_like[1]) ?>
				<?cs call:data_title_txt_style("、","",0)?>
				<?cs call:data_title_nick(qz_metadata.relybody[0].extendinfo.uininfo.uin_like[2], qz_metadata.relybody[0].extendinfo.uininfo.who_like[2], qz_metadata.relybody[0].extendinfo.uininfo.nick_like[2]) ?>
			<?cs elif:likedCount > 3 ?>
				<?cs call:data_title_tipTxt("和")?>
				<?cs call:data_title_nick(qz_metadata.relybody[0].extendinfo.uininfo.uin_like[1], qz_metadata.relybody[0].extendinfo.uininfo.who_like[1], qz_metadata.relybody[0].extendinfo.uininfo.nick_like[1]) ?>

				<?cs set:likedCountForView = likedCount ?>
				<?cs if:likedCount > 40 ?>
					<?cs set:likedCountForView = 40 ?>
				<?cs /if?>

				<?cs loop:_j=2, likedCountForView - 1, 1?>
					<?cs call:data_title_txt_style("、","",0)?>
					<?cs call:data_title_nick(qz_metadata.relybody[0].extendinfo.uininfo.uin_like[_j], qz_metadata.relybody[0].extendinfo.uininfo.who_like[_j], qz_metadata.relybody[0].extendinfo.uininfo.nick_like[_j]) ?>
				<?cs /loop?>

				<?cs call:data_title_tipTxt("等" + likedCount + "人")?>
			<?cs /if?>
		<?cs else ?>
			<?cs set:likedCount = subcount(qz_metadata.orgdata.extendinfo.uininfo.uin_like) ?>
			<?cs if:likedCount == 2 ?>
				<?cs call:data_title_tipTxt("和")?>
				<?cs call:data_title_nick(qz_metadata.orgdata.extendinfo.uininfo.uin_like[1], qz_metadata.orgdata.extendinfo.uininfo.who_like[1], qz_metadata.orgdata.extendinfo.uininfo.nick_like[1]) ?>
			<?cs elif:likedCount == 3 ?>
				<?cs call:data_title_tipTxt("和")?>
				<?cs call:data_title_nick(qz_metadata.orgdata.extendinfo.uininfo.uin_like[1], qz_metadata.orgdata.extendinfo.uininfo.who_like[1], qz_metadata.orgdata.extendinfo.uininfo.nick_like[1]) ?>
				<?cs call:data_title_txt_style("、","",0)?>
				<?cs call:data_title_nick(qz_metadata.orgdata.extendinfo.uininfo.uin_like[2], qz_metadata.orgdata.extendinfo.uininfo.who_like[2], qz_metadata.orgdata.extendinfo.uininfo.nick_like[2]) ?>
			<?cs elif:likedCount > 3 ?>
				<?cs call:data_title_tipTxt("和")?>
				<?cs call:data_title_nick(qz_metadata.orgdata.extendinfo.uininfo.uin_like[1], qz_metadata.orgdata.extendinfo.uininfo.who_like[1], qz_metadata.orgdata.extendinfo.uininfo.nick_like[1]) ?>

				<?cs set:likedCountForView = likedCount ?>
				<?cs if:likedCount > 40 ?>
					<?cs set:likedCountForView = 40 ?>
				<?cs /if?>

				<?cs loop:_j=2, likedCountForView - 1, 1?>
					<?cs call:data_title_txt_style("、","",0)?>
					<?cs call:data_title_nick(qz_metadata.orgdata.extendinfo.uininfo.uin_like[_j], qz_metadata.orgdata.extendinfo.uininfo.who_like[_j], qz_metadata.orgdata.extendinfo.uininfo.nick_like[_j]) ?>
				<?cs /loop?>

				<?cs call:data_title_tipTxt("等" + likedCount + "人")?>
			<?cs /if?>
		<?cs /if?>
	<?cs /if?>
	
	<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
		<?cs if:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_blog?>
			<?cs call:data_title_tipTxt("赞了")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs call:data_title_tipTxt("的日志")?>
			<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl)?>
		<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_photo?>
			<?cs if:qz_metadata.orgdata.extendinfo.subtype == FOLLOW_phototype_album || qz_metadata.orgdata.extendinfo.subtype == FOLLOW_phototype_batch ?>
				<?cs call:data_title_tipTxt("赞了")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>			
				<?cs call:data_title_tipTxt("的相册")?>
				<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl)?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("赞了")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>			
				<?cs call:data_title_tipTxt("的照片")?>
			<?cs /if?>		
		<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_share?>
			<?cs if:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_music_collect?>
				<?cs call:data_title_tipTxt("赞了")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>			
				<?cs call:data_title_tipTxt("收藏的音乐")?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_music_list?>
				<?cs call:data_title_tipTxt("赞了")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
				<?cs call:data_title_tipTxt("设置的背景音乐")?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_space_dress?>
				<?cs call:data_title_tipTxt("赞了")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>			
				<?cs call:data_title_tipTxt("分享的装扮")?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("赞了")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>	
				<?cs call:data_title_tipTxt("的分享")?>
			<?cs /if?>

			<?cs #call:data_title_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl) ?>
		<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_mood?>
			<?cs if:qz_metadata.orgdata.extendinfo.subtype == FOLLOW_moodtype_timeline ?>
				<?cs call:data_title_tipTxt("赞了")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>			
				<?cs call:data_title_tipTxt("的时光轴")?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("赞了")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>			
				<?cs call:data_title_tipTxt("的说说")?> 
			<?cs /if?>
		<?cs /if ?>
	<?cs else ?>
		<?cs if:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_blog?>
			<?cs call:data_title_tipTxt("赞了我的日志")?>
			<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl)?>
		<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_photo?>
			<?cs if:qz_metadata.orgdata.extendinfo.subtype == FOLLOW_phototype_album || qz_metadata.orgdata.extendinfo.subtype == FOLLOW_phototype_batch ?>
				<?cs call:data_title_tipTxt("赞了我的相册")?>
				<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl)?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("赞了我的照片")?>
			<?cs /if?>		
		<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_share?>
			<?cs if:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_music_collect?>
				<?cs call:data_title_tipTxt("赞了我收藏的音乐")?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_music_list?>
				<?cs call:data_title_tipTxt("赞了我设置的背景音乐")?>
			<?cs elif:qz_metadata.orgdata.extendinfo.sharetype == SHARE_srctype_space_dress?>
				<?cs call:data_title_tipTxt("赞了我分享的装扮")?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("赞了分享")?>
			<?cs /if?>

			<?cs #call:data_title_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl) ?>
		<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_mood?>
			<?cs if:qz_metadata.orgdata.extendinfo.subtype == FOLLOW_moodtype_timeline ?>
				<?cs call:data_title_tipTxt("赞了我的时光轴")?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("赞了我的说说")?> 
			<?cs /if?>
		<?cs elif:qz_metadata.orgdata.extendinfo.appid == FOLLOW_srctype_main?>
			<?cs call:data_title_tipTxt("赞了我的主页,")?>
			<?cs call:data_title_url("也去他主页赞一下吧", "http://user.qzone.qq.com/"+qz_metadata.opinfo.opuin)?>
		<?cs /if ?>		
	<?cs /if ?>
<?cs /def?>

<?cs def:data_main_title()?>
	<?cs if:qz_metadata.orgdata.subtype == FOLLOW_mtype_follow ?>
		<?cs call:data_follow_title() ?>
	<?cs else ?>
		<?cs call:data_like_title() ?> 
		<?cs if:subcount(qz_metadata.relybody) > 0 && qz_metadata.meta.loginuin == qz_metadata.relybody[0].uin ?>
			<?cs call:data_follow_subtitle() ?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>