<?cs ####
	/*日志标题区*/
?>


<?cs def:data_blog_title()?>
	<?cs call:i()?>
	<?cs call:get_tuin_and_tid()?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_NEWCOMMENT ?>
		<?cs call:get_blog_type()?>
		<?cs call:data_title_tipTxt(get_blog_type.ret)?>
		<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
		<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret)?>
		<?cs call:data_title_tipTxt("有了")?>

		<?cs call:get_last_comment_pos()?>
		<?cs call:get_userWho_platform(qz_metadata.vt2body[get_last_comment_pos.ret].platformid, qz_metadata.vt2body[get_last_comment_pos.ret].platformsubid)?>
		<?cs with:t2body = qz_metadata.vt2body[get_last_comment_pos.ret]?>
		<?cs call:data_title_nick(t2body.uin, get_userWho_platform.ret, t2body.nickname)?>
		<?cs /with?>
		<?cs call:data_title_tipTxt("的新评论")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_RELATEPSV ?>
		<?cs call:get_blog_type() ?>
		<?cs if:qz_metadata.orgdata.action == UC_API_ACTION_MODIFY ?>
			<?cs call:data_title_tipTxt("修改" + get_blog_type.ret)?>
		<?cs else ?>
			<?cs if:subcount(qz_metadata.relybody) > 0 ?>
				<?cs if:!g_isV8?>
					<?cs #转载类?>
					<?cs call:data_title_tipTxt("转载")?>
					<?cs call:data_title_nick(qz_metadata.orgdata.uin, USER_PLATFORM_WHO_QZONE, qz_metadata.orgdata.nickname)?>
					<?cs call:data_title_tipTxt("的" + get_blog_type.ret)?>
				<?cs /if?>
			<?cs else ?>
				<?cs if:!g_isV8?><?cs #v8不需要这几个字了?>
					<?cs call:data_title_tipTxt("发表" + get_blog_type.ret)?>
				<?cs /if?>
			<?cs /if?>
		<?cs /if?>
		<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
		<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret)?><?cs #TODO 对于v8，在转发的情况下这个也不需要的?>
		<?cs call:set(data_title_url.ret.path, "prop", "BLOG_TITLE")?>

	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_AUDIT?>
		<?cs call:data_titletip_tipTxt("评论")?>
		<?cs if:subcount(qz_metadata.relybody) > 0 ?>
			<?cs call:get_blog_url(qz_metadata.relybody[0].uin, qz_metadata.relybody[0].mkey)?>
		<?cs else ?>
			<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
		<?cs /if?>
		<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret)?>		
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?>
		<?cs call:data_titletip_tipTxt("回复")?>
		<?cs if:subcount(qz_metadata.relybody) > 0 ?>
			<?cs call:get_blog_url(qz_metadata.relybody[0].uin, qz_metadata.relybody[0].mkey)?>
		<?cs else ?>
			<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
		<?cs /if?>
		<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret)?>			
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
			<?cs call:data_title_tipTxt("在日志")?>
			<?cs if:subcount(qz_metadata.relybody) > 0 ?>
				<?cs call:get_blog_url(qz_metadata.relybody[0].uin, qz_metadata.relybody[0].mkey)?>
			<?cs else ?>
				<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
			<?cs /if?>
			<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret) ?>
			<?cs call:data_title_tipTxt("提到我")?>
		<?cs elif:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("在日志")?>
			<?cs if:subcount(qz_metadata.relybody) > 0 ?>
				<?cs call:get_blog_url(qz_metadata.relybody[0].uin, qz_metadata.relybody[0].mkey)?>
			<?cs else ?>
				<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
			<?cs /if?>
			<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret) ?>
			<?cs call:data_title_tipTxt("提到")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
		<?cs else ?>
			<?cs call:data_titletip_tipTxt("提到我")?>
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY ?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
			<?cs call:data_title_tipTxt("在日志")?>
			<?cs if:subcount(qz_metadata.relybody) > 0 ?>
				<?cs call:get_blog_url(qz_metadata.relybody[0].uin, qz_metadata.relybody[0].mkey)?>
			<?cs else ?>
				<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
			<?cs /if?>
			<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret) ?>
			<?cs call:data_title_tipTxt("提到我")?>
		<?cs elif:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("在日志")?>
			<?cs if:subcount(qz_metadata.relybody) > 0 ?>
				<?cs call:get_blog_url(qz_metadata.relybody[0].uin, qz_metadata.relybody[0].mkey)?>
			<?cs else ?>
				<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
			<?cs /if?>
			<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret) ?>
			<?cs call:data_title_tipTxt("提到")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
		<?cs else ?>
			<?cs call:data_titletip_tipTxt("提到我")?>
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM ?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_FRIEND_TO_ME ?>
			<?cs call:data_title_tipTxt("在日志")?>
			<?cs if:subcount(qz_metadata.relybody) > 0 ?>
				<?cs call:get_blog_url(qz_metadata.relybody[0].uin, qz_metadata.relybody[0].mkey)?>
			<?cs else ?>
				<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
			<?cs /if?>
			<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret) ?>
			<?cs call:data_title_tipTxt("提到我")?>
		<?cs elif:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("在日志")?>
			<?cs if:subcount(qz_metadata.relybody) > 0 ?>
				<?cs call:get_blog_url(qz_metadata.relybody[0].uin, qz_metadata.relybody[0].mkey)?>
			<?cs else ?>
				<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
			<?cs /if?>
			<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret) ?>
			<?cs call:data_title_tipTxt("提到")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
		<?cs else ?>
			<?cs call:data_titletip_tipTxt("提到我")?>
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT_NOTIFYPSV?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("转载了")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs call:data_title_tipTxt("的日志：")?>
			<?cs call:get_blog_url(qz_metadata.relybody[0].uin, qz_metadata.relybody[0].mkey)?>
			<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret)?>
		<?cs else ?>
			<?cs call:data_titletip_tipTxt("转载了我的日志：")?>
			<?cs call:get_blog_url(qz_metadata.relybody[0].uin, qz_metadata.relybody[0].mkey)?>
			<?cs call:data_titletip_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret)?>
		<?cs /if ?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_SHARETOME?>
		<?cs if:qz_metadata.scope == SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
			<?cs call:data_title_tipTxt("分享给")?>
			<?cs call:data_title_nick(qz_metadata.friendshipuin, USER_PLATFORM_WHO_QZONE, qz_metadata.friendshipnick)?>
			<?cs call:data_title_tipTxt("日志")?>
			<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
			<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret)?>
		<?cs else ?>
			<?cs call:data_titletip_tipTxt("分享给我：")?>
			<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
			<?cs call:data_title_url(qz_metadata.orgdata.title.0.content, get_blog_url.ret)?>
		<?cs /if ?>
	<?cs else ?>
		<?cs call:data_title_tipTxt("发表")?>
	<?cs /if?>
<?cs /def?>
