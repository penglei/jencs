<?cs set:SCOPE_FRIENDSHIP_ME_TO_FRIEND=1?>  <?cs #/*friendship场景标识:我对好友的操作*/#?>
<?cs set:SCOPE_FRIENDSHIP_FRIEND_TO_ME=2?>  <?cs #/*friendship场景标识:好友对我的操作*/#?>

<?cs call:i()?>
<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT ?>
	<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
		<?cs call:data_title_tipTxt("给")?>
		<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
		<?cs call:data_title_tipTxt("留言：")?>
	<?cs else ?>
		<?cs call:data_title_tipTxt("给我留言：")?>
	<?cs /if?>
	<?cs call:data_textTitle_rich(qz_metadata.orgdata.content)?>
<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?>
	<?cs call:data_title_tipTxt("回复")?>
	<?cs call:data_textTitle_rich(qz_metadata.orgdata.content)?>
<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV || qz_metadata.feedtype ==UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ?>
	<?cs call:data_title_tipTxt("在给")?>
	<?cs call:data_title_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname) ?>
	<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
		<?cs call:data_title_tipTxt("的留言中提到")?>
		<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
	<?cs else ?>
		<?cs call:data_title_tipTxt("的留言中提到我")?>
	<?cs /if?>
	<?cs call:data_textTitle_rich(qz_metadata.orgdata.content)?>
<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY?>
	<?cs call:data_title_tipTxt("在")?>
	<?cs call:data_title_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname) ?>
	<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
		<?cs call:data_title_tipTxt("的留言回复中提到")?>
		<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
	<?cs else ?>
		<?cs call:data_title_tipTxt("的留言回复中提到我")?>
	<?cs /if?>
	<?cs call:data_textTitle_rich(qz_metadata.orgdata.content)?>
<?cs /if?>

<?cs if:subcount(qz_metadata.orgdata.itemdata) > 0 &&  subcount(qz_metadata.orgdata.itemdata[0].picinfo) > 0 ?>
	<?cs call:data_cntmedia_pic_urlaction(0, qz_metadata.orgdata.itemdata[0], "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/msgboard", "", "") ?>
<?cs /if?>

<?cs call:data_oprtime()?>
<?cs call:data_source()?>
<?cs call:data_opr_more()?>
<?cs call:data_opr_prevent()?>
<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_AUDIT ?>
	<?cs call:data_opr_comment()?>
	<?cs call:data_like()?>
<?cs /if?>

<?cs call:data_comments("http://m.qzone.qq.com/cgi-bin/new/msgb_comment_answer.cgi", "GB")?>

<?cs each:i = g_data_comments.index?>
	<?cs call:data_comment_loop_item(i)?>
<?cs /each?>


<?cs if:qz_metadata.feedtype != UC_WUP_FEED_TYPE_AUDIT ?>
	<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", qz_metadata.orgdata.uin + "," + qz_metadata.orgdata.mkey + ",0", qz_metadata.meta.userid, 
									"http://m.qzone.qq.com/cgi-bin/new/msgb_comment_answer.cgi", "GB")?>
	<?cs # 这是回复 ?>
	<?cs call:qfv("comments.inputbox.useReply", 1)?>
<?cs /if?>

<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM 
	|| qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY ?>
	<?cs call:data_opr_url(0, "我也留言", "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/msgboard","") ?>
<?cs /if?>


<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT ?>
	<?cs call:data_opr_audit("/qzone/msgboard/info/audit_msg_pass.html", qz_metadata.orgdata.mkey) ?>
	<?cs call:data_opr_del("http://m.qzone.qq.com/cgi-bin/new/msgb_del_unverify.cgi", qz_metadata.orgdata.mkey) ?>
<?cs else ?>
	<?cs #:如果没有删除按钮的情况下就出删除动态的按钮 ?>
	<?cs call:data_opr_delfeed() ?>
<?cs /if?>
