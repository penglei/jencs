<?cs ####
	/*分享标题区*/
?>

<?cs def:data_app_title()?>
	<?cs call:i()?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT?>
		<?cs if:qz_metadata.orgdata.subtype == APP_subtype_addapp?>
			<?cs call:data_title_tipTxt("添加了应用")?>
			<?cs #call:data_title_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
		<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_share?>
			<?cs call:data_title_tipTxt("推荐试玩")?>
			<?cs #call:data_title_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
		<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_game?>
			<?cs call:data_title_richmsg(qz_metadata.orgdata.desc)?>
			<?cs #call:data_title_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
		<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_mobile_cover?>
			<?cs if:subcount(qz_metadata.orgdata.itemdata[0].desc) > 0 ?>
				<?cs call:data_title_richmsg(qz_metadata.orgdata.itemdata[0].desc)?>
			<?cs else?>
				<?cs call:data_title_tipTxt("我把手机空间的个人主页背景换成了自己的照片，还不错哦~你也来试试吧~")?>
			<?cs /if?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs if:qz_metadata.orgdata.subtype == APP_subtype_invite?>
				<?cs if:qz_metadata.orgdata.itemdata[0].extendinfo.iAppInviteCount > 1?>
					<?cs call:data_title_tipTxt("等" + qz_metadata.orgdata.itemdata[0].extendinfo.iAppInviteCount + "位好友邀请")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
					<?cs call:data_title_tipTxt("一起玩")?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("邀请")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
					<?cs call:data_title_tipTxt("一起玩")?>
				<?cs /if?>
				<?cs call:data_title_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
			<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_invite_with_mqzone?>
				<?cs if:qz_metadata.orgdata.itemdata[0].extendinfo.iAppInviteCount > 1?>
					<?cs call:data_title_tipTxt("等" + qz_metadata.orgdata.itemdata[0].extendinfo.iAppInviteCount + "位好友邀请")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
					<?cs call:data_title_tipTxt("一起参与")?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("邀请")?>
					<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
					<?cs call:data_title_tipTxt("一起参与")?>
				<?cs /if?>
				<?cs call:data_title_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
			<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_video?>
				<?cs call:data_title_tipTxt("邀请")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
				<?cs call:data_title_tipTxt("一起看")?>
				<?cs call:data_title_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
			<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_activate?>
				<?cs call:data_title_tipTxt("在")?>
				<?cs call:data_title_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
				<?cs call:data_title_tipTxt("中给")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
				<?cs call:data_title_tipTxt("发了请求")?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("评论")?>			
			<?cs /if?>
		<?cs else?>
			<?cs if:qz_metadata.orgdata.subtype == APP_subtype_invite?>
				<?cs if:qz_metadata.orgdata.itemdata[0].extendinfo.iAppInviteCount > 1?>
					<?cs call:data_title_tipTxt("等" + qz_metadata.orgdata.itemdata[0].extendinfo.iAppInviteCount + "位好友邀请我一起玩")?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("邀请我一起玩")?>
				<?cs /if?>
				<?cs call:data_title_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
			<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_invite_with_mqzone?>
				<?cs if:qz_metadata.orgdata.itemdata[0].extendinfo.iAppInviteCount > 1?>
					<?cs call:data_title_tipTxt("等" + qz_metadata.orgdata.itemdata[0].extendinfo.iAppInviteCount + "位好友邀请我一起参与")?>
				<?cs else ?>
					<?cs call:data_title_tipTxt("邀请我一起参与")?>
				<?cs /if?>
				<?cs call:data_title_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
			<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_video?>
				<?cs call:data_title_tipTxt("邀请我一起看")?>
				<?cs call:data_title_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
			<?cs elif:qz_metadata.orgdata.subtype == APP_subtype_activate?>
				<?cs call:data_title_tipTxt("在")?>
				<?cs call:data_title_url(qz_metadata.orgdata.itemdata[0].name, qz_metadata.orgdata.itemdata[0].action)?>
				<?cs call:data_title_tipTxt("中给我发了请求")?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("评论")?>
			<?cs /if?>
		<?cs /if?>	
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?>
		<?cs call:data_title_tipTxt("回复")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs if:qz_metadata.orgdata.subtype == APP_subtype_share?>
				<?cs call:data_title_tipTxt("在分享中提到了")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("提到了")?>
				<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs /if?>		
		<?cs else?>
			<?cs if:qz_metadata.orgdata.subtype == APP_subtype_share?>
				<?cs call:data_title_tipTxt("在分享中提到了我")?>
			<?cs else ?>
				<?cs call:data_title_tipTxt("提到了我")?>
			<?cs /if?>
		<?cs /if?>	
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("在回复中提到")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
		<?cs else?>
			<?cs call:data_title_tipTxt("在回复中提到我：")?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("在评论中提到")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
		<?cs else?>
			<?cs call:data_title_tipTxt("在评论中提到我：")?>
		<?cs /if?>	
	<?cs /if?>
<?cs /def?>
