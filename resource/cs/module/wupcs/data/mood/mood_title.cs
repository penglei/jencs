<?cs ####
	/*说说标题区*/
?>


<?cs def:data_mood_title()?>
	<?cs call:i()?>
	<?cs # call:get_tuin_and_tid()?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_NEWCOMMENT ?>
		<?cs # call:get_mood_type()?>

		<?cs if:subcount(qz_metadata.orgdata.content) == 0 
			|| (subcount(qz_metadata.orgdata.content) == 1 && qz_metadata.orgdata.content[0].type == 0 && string.length(qz_metadata.orgdata.content[0].content) == 0)?>
			<?cs call:data_title_tipTxt("照片有了")?>
			<?cs call:get_last_comment_pos()?>
			<?cs call:get_userWho_platform(qz_metadata.vt2body[get_last_comment_pos.ret].platformid, qz_metadata.vt2body[get_last_comment_pos.ret].platformsubid)?>
			<?cs with:t2body = qz_metadata.vt2body[get_last_comment_pos.ret]?>
			<?cs call:data_title_nick(t2body.uin, get_userWho_platform.ret, t2body.nickname)?>
			<?cs /with?>
			<?cs call:data_title_tipTxt("的新评论")?>
		<?cs else?>
			<?cs call:data_title_startQuote()?>
			<?cs call:data_title_richmsg(qz_metadata.orgdata.content)?>
			<?cs call:data_title_endQuote()?>
			<?cs call:data_title_tipTxt("有了")?>
		
			<?cs call:get_last_comment_pos()?>
			<?cs call:get_userWho_platform(qz_metadata.vt2body[get_last_comment_pos.ret].platformid, qz_metadata.vt2body[get_last_comment_pos.ret].platformsubid)?>
	
		
			<?cs with:t2body = qz_metadata.vt2body[get_last_comment_pos.ret]?>
			<?cs call:data_title_nick(t2body.uin, get_userWho_platform.ret, t2body.nickname)?>
			<?cs /with?>
			<?cs call:data_title_tipTxt("的新评论")?>
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT?>
		<?cs if:qz_metadata.orgdata.platformid == UC_PLATFORM_ID_QZONE 
				&& qz_metadata.orgdata.platformsubid == UC_PLATFORM_QZONE_SUBID_TIMELINE
				&& subcount(qz_metadata.relybody) == 0?>
			<?cs call:data_title_tipTxt("添加时光轴记录")?>
			<?cs if:string.length(qz_metadata.orgdata.extendinfo.lbs_abstime) > 0?>
				<?cs set:time_url = "http://user.qzone.qq.com/"
									+ qz_metadata.orgdata.uin 
									+ "/main?mode=gfp_timeline#eid=s"
									+ qz_metadata.orgdata.mkey
									+ "&time="
									+ qz_metadata.orgdata.extendinfo.lbs_abstime?>

				<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.lbs_time, time_url)?>
			<?cs /if?>
			<?cs if:qz_metadata.orgdata.extendinfo.uin_timeline_num > 0?>
				
				<?cs #set:debug = qz_metadata.orgdata.extendinfo.uin_timeline_num?>
				<?cs #var:debug?>

				<?cs call:data_title_tipTxt("和")?>
				<?cs set:_end = qz_metadata.orgdata.extendinfo.uin_timeline_num - 2 ?>
				<?cs loop:j=0, _end, 1?>
					<?cs set:name_url = "http://user.qzone.qq.com/"
										+ qz_metadata.orgdata.extendinfo.uininfo.uin_timeline[j]
										+ "/main?mode=gfp_timeline"?>
					<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.uininfo.nick_timeline[j], name_url)?>
					<?cs call:data_title_tipTxt(",")?>
				<?cs /loop?>

				<?cs set:last_pos = qz_metadata.orgdata.extendinfo.uin_timeline_num - 1?>
				<?cs set:name_url = "http://user.qzone.qq.com/"
									+ qz_metadata.orgdata.extendinfo.uininfo.uin_timeline[last_pos]
									+ "/main?mode=gfp_timeline"?>
				<?cs call:data_title_url(qz_metadata.orgdata.extendinfo.uininfo.nick_timeline[last_pos], name_url)?>
				<?cs call:data_title_tipTxt("一起：")?>
			<?cs else?>
				<?cs call:data_title_tipTxt("：")?>
			<?cs /if?>
			<?cs call:data_title_richmsg(qz_metadata.orgdata.content)?>
		<?cs elif:subcount(qz_metadata.relybody) > 0 ?>
			<?cs #call:data_title_tipTxt("转发：")?>
			<?cs call:data_title_richmsg(qz_metadata.orgdata.title)?>
		<?cs else?>
			<?cs call:qfv("title.con_more",qz_metadata.orgdata.extendinfo.con_more) ?>
			<?cs call:data_title_richmsg(qz_metadata.orgdata.content)?>
		<?cs /if?>	
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV ?><?cs #评论被动?>
		<?cs call:data_title_tipTxt("评论")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?><?cs #回复被动?>
		<?cs call:data_title_tipTxt("回复")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV?><?cs #我参与的?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
			<?cs call:data_title_tipTxt("在说说提到我")?>
		<?cs elif:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("在说说提到")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("提到我：")?>
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
			<?cs call:data_title_tipTxt("在说说提到我")?>
		<?cs elif:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("在说说提到")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("提到我：")?>
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("提到")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("提到我：")?>
		<?cs /if ?>	
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT_NOTIFYPSV?>
		<?cs call:data_title_tipTxt("转发：")?>
		<?cs call:data_title_richmsg(qz_metadata.orgdata.title)?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEINTOPIC?>
		<?cs if:qz_metadata.opinfo.action == UC_API_ACTION_COMMENTS?>
			<?cs call:data_title_tipTxt("评论")?>
		<?cs elif:qz_metadata.opinfo.action == UC_API_ACTION_REPLY?>
			<?cs call:data_title_tipTxt("回复")?>
		<?cs else?>
			<?cs call:data_title_tipTxt("提到我：")?>
		<?cs /if?>
		<?cs call:data_title_richmsg(qz_metadata.orgdata.title)?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_FWDME_GETCOMMREPLY?>
		<?cs call:data_title_tipTxt("转发")?>
		<?cs call:data_title_richmsg(qz_metadata.orgdata.title)?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATME_GETFWDCOMMREPLY?>
		<?cs call:data_title_tipTxt("转了提到我的说说：")?>
		<?cs call:data_title_richmsg(qz_metadata.orgdata.title)?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_FWD_NOTIFY?>
		<?cs call:data_title_tipTxt("转发")?>
		<?cs call:data_title_richmsg(qz_metadata.orgdata.title)?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_VEDIO_AUDIT ?>
		<?cs call:data_title_tipTxt("您的视频已通过审核")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHARETOME ?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("发了一条说说给")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("发了一条说说给我")?>
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATME_FWD_GETFWDCOMMREPLY ?>
		<?cs call:data_title_richmsg(qz_metadata.orgdata.title)?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLY_ALSO?>
		<?cs call:data_title_tipTxt("也回复")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_BE_AT_IN_COMM?>
		<?cs call:data_title_tipTxt("回复")?>
	<?cs else?>
		<?cs call:data_title_richmsg(qz_metadata.orgdata.content)?>
	<?cs /if?>

<?cs /def?>
