<?cs ####
	/*分享标题区*/
?>

<?cs def:data_share_title()?>
	<?cs call:i()?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_RELATEPSV ?>
		<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_songlist?>
			<?cs call:data_title_tipTxt("分享歌单")?>
			<?cs call:data_title_richmsg(qz_metadata.relybody.0.msg)?>
		<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_album?>
			<?cs call:data_title_tipTxt("分享专辑")?>
			<?cs call:data_title_richmsg(qz_metadata.relybody.0.msg)?>
		<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_type_newdir?>
			<?cs call:data_title_tipTxt("创建歌单")?>
		<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_follow_dir?>
			<?cs call:data_title_tipTxt("订阅了")?>
			<?cs if:qz_metadata.orgdata.itemdata.0.name?>
				<?cs set:dirurl = "http://imgcache.qq.com/music/qzone/musicbox_jump.html?uin=" + qz_metadata.orgdata.itemdata.0.extendinfo.uHostUin +"&url=music_coll"?>
				<?cs call:data_title_url(qz_metadata.orgdata.itemdata.0.extendinfo.sHostNick, dirurl)?>
				<?cs call:data_title_tipTxt("的歌单")?>
			<?cs /if?>
		<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_update_dir?>
			<?cs call:data_title_tipTxt("在")?>
			<?cs call:data_title_url(qz_metadata.orgdata.itemdata.0.albumname, qz_metadata.orgdata.itemdata.0.action)?>
			<?cs call:data_title_tipTxt("添加了歌曲")?>
			<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, qz_metadata.orgdata.srcurl)?>
		<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_collect?>
			<?cs call:data_title_tipTxt("收藏音乐：")?>
		<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_list?>
			<?cs call:data_title_tipTxt("设置背景音乐：")?>
		<?cs else ?>
			<?cs if:qz_metadata.orgdata.subtype == SHARE_subtype_like?>
				<?cs call:data_title_tipTxt("喜欢：")?>
			<?cs else ?>
				<?cs #### /*call:data_title_tipTxt("转发：")*/?>
			<?cs /if?>
			<?cs call:data_title_richmsg(qz_metadata.relybody.0.msg)?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV?>
		<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_songlist || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_type_newdir 
			|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_follow_dir ||  qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_update_dir?>
			<?cs call:data_title_tipTxt("评论歌单")?>
		<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_album?>
			<?cs call:data_title_tipTxt("评论专辑")?>
		<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_collect || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_list?>
			<?cs call:data_title_tipTxt("评论")?>
		<?cs else ?>
			<?cs if:qz_metadata.orgdata.subtype == SHARE_subtype_like?>
				<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
					<?cs call:data_title_tipTxt("评论")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
					<?cs call:data_title_tipTxt("的喜欢")?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("评论我的喜欢")?>
				<?cs /if?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("评论")?>
			<?cs /if?>
		<?cs /if?>			
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?>
		<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_songlist || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_type_newdir
			|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_follow_dir ||  qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_update_dir?>
			<?cs call:data_title_tipTxt("回复歌单")?>
		<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_album?>
			<?cs call:data_title_tipTxt("回复专辑")?>
		<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_collect || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_list?>
			<?cs call:data_title_tipTxt("回复：")?>
		<?cs else ?>
			<?cs if:qz_metadata.orgdata.subtype == SHARE_subtype_like?>
				<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
					<?cs call:data_title_tipTxt("回复")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
					<?cs call:data_title_tipTxt("的喜欢")?>	
				<?cs else ?>
					<?cs call:data_title_tipTxt("回复我的喜欢")?>
				<?cs /if?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("回复")?>
			<?cs /if?>	
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_songlist || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_type_newdir
				|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_follow_dir ||  qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_update_dir?>
				<?cs call:data_title_tipTxt("在歌单中提到")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_album?>
				<?cs call:data_title_tipTxt("在专辑中提到")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_collect || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_list?>
				<?cs call:data_title_tipTxt("在分享提到")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs else ?>
				<?cs if:qz_metadata.orgdata.subtype == SHARE_subtype_like?>
					<?cs call:data_title_tipTxt("在喜欢中提到")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
					<?cs #call:data_title_richmsg(qz_metadata.relybody.0.msg)?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("在分享提到")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
					<?cs #call:data_title_richmsg(qz_metadata.relybody.0.msg)?>
				<?cs /if?>
			<?cs /if ?>
		<?cs else ?>
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_songlist || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_type_newdir
				|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_follow_dir ||  qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_update_dir?>
				<?cs call:data_title_tipTxt("在歌单中提到我")?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_album?>
				<?cs call:data_title_tipTxt("在专辑中提到我")?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_collect || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_list?>
				<?cs call:data_title_tipTxt("提到了我")?>
			<?cs else ?>
				<?cs if:qz_metadata.orgdata.subtype == SHARE_subtype_like?>
					<?cs call:data_title_tipTxt("在喜欢中提到我")?>
					<?cs #call:data_title_richmsg(qz_metadata.relybody.0.msg)?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("提到我")?>
					<?cs #call:data_title_richmsg(qz_metadata.relybody.0.msg)?>
				<?cs /if?>	
			<?cs /if ?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_songlist || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_type_newdir
				|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_follow_dir ||  qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_update_dir?>
				<?cs call:data_title_tipTxt("在歌单的回复中提到")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_album?>
				<?cs call:data_title_tipTxt("在专辑的回复中提到")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_collect || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_list?>
				<?cs call:data_title_tipTxt("在分享的回复中提到")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs else ?>
				<?cs if:qz_metadata.orgdata.subtype == SHARE_subtype_like?>
					<?cs call:data_title_tipTxt("在喜欢回复中提到")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("在分享提到")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
				<?cs /if?>
			<?cs /if ?>	
		<?cs else ?>
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_songlist || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_type_newdir
				|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_follow_dir ||  qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_update_dir?>
				<?cs call:data_title_tipTxt("在歌单的回复中提到我")?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_album?>
				<?cs call:data_title_tipTxt("在专辑的回复中提到我")?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_collect || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_list?>
				<?cs call:data_title_tipTxt("在回复中提到了我")?>
			<?cs else ?>
				<?cs if:qz_metadata.orgdata.subtype == SHARE_subtype_like?>
					<?cs call:data_title_tipTxt("在喜欢回复中提到我")?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("提到我")?>
				<?cs /if?>	
			<?cs /if ?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_songlist || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_type_newdir
				|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_follow_dir ||  qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_update_dir?>
				<?cs call:data_title_tipTxt("在歌单的评论中提到")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_album?>
				<?cs call:data_title_tipTxt("在专辑的评论中提到")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_collect || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_list?>
				<?cs call:data_title_tipTxt("在分享的评论中提到")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs else ?>
				<?cs if:qz_metadata.orgdata.subtype == SHARE_subtype_like?>
					<?cs call:data_title_tipTxt("在喜欢评论中提到")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("在分享的评论中提到")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
				<?cs /if?>
			<?cs /if ?>	
		<?cs else ?>
			<?cs if:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_songlist || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_type_newdir
				|| qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_follow_dir ||  qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_update_dir?>
				<?cs call:data_title_tipTxt("在歌单的评论中提到我")?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_album?>
				<?cs call:data_title_tipTxt("在专辑的评论中提到我")?>
			<?cs elif:qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_collect || qz_metadata.orgdata.extendinfo.share_subtype == SHARE_srctype_music_list?>
				<?cs call:data_title_tipTxt("在评论中提到了我")?>
			<?cs else ?>
				<?cs if:qz_metadata.orgdata.subtype == SHARE_subtype_like?>
					<?cs call:data_title_tipTxt("在喜欢评论中提到我")?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("提到我")?>
				<?cs /if?>
			<?cs /if ?>	
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT_NOTIFYPSV?>
		<?cs if:qz_metadata.orgdata.subtype == SHARE_subtype_like?>
			<?cs call:data_title_tipTxt("喜欢：")?>
		<?cs elif:qz_metadata.orgdata.subtype == SHARE_subtype_send?>
			<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
				<?cs call:data_title_tipTxt("分享给")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("分享给我：")?>
			<?cs /if ?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("转发：")?>
		<?cs /if?>
		<?cs call:data_title_richmsg(qz_metadata.relybody.0.msg)?>
	<?cs else ?>
		<?cs #### /*call:data_title_tipTxt("转发：")*/?>
	<?cs /if?>
<?cs /def?>
