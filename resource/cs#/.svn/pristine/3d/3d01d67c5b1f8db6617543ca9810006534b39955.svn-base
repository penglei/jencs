<?cs def:showProfile(profile_item) ?>
	<li>
		<?cs if:profile_item.profiletype == 1 ?>
			<i class="ico_info_2"></i>
			<?cs if:string.length(profile_item.firstdata) == 0 ?>
				<?cs var:profile_item.enddata ?>
			<?cs else ?>
				从“<?cs var:profile_item.firstdata ?>”变成“<?cs var:profile_item.enddata ?>”
			<?cs /if ?>
		<?cs elif:profile_item.profiletype == 2 ?>
			<i class="ico_info_3"></i>
			<?cs if:string.length(profile_item.firstdata) == 0 ?>
				搬到“<?cs var:profile_item.enddata ?>”
			<?cs else ?>
				从“<?cs var:profile_item.firstdata ?>”搬到“<?cs var:profile_item.enddata ?>”
			<?cs /if ?>
		<?cs else ?>
		<i class="ico_info_1"></i>
		家乡是“<?cs var:profile_item.enddata ?>”
		<?cs /if ?>
	</li>
<?cs /def ?>

<div class="info_change_box">
	<ul>
		<?cs if:subcount(qz_metadata.profile.0)>0?>
			<?cs each:profile_item = qz_metadata.profile ?>
				<?cs call:showProfile(profile_item) ?>
			<?cs /each ?>
		<?cs elif:subcount(qz_metadata.profile)>0?>
			<?cs call:showProfile(qz_metadata.profile) ?>
		<?cs /if?>
	</ul>
</div>


<?cs set:topicuin = qz_metadata.topicuin?>
<?cs set:topicid = qz_metadata.topicid?>
<?cs #:平台组默认给所有字段做了html编码，这里的自定义方法现在不做处理 ?>
<?cs def:html_escape(source)?><?cs escape:"none"?><?cs var:source?><?cs /escape?><?cs /def?>
<?cs def:url_escape(source)?><?cs escape:"url"?><?cs var:source?><?cs /escape?><?cs /def?>
<?cs def:js_escape(source)?><?cs escape:"js"?><?cs var:source?><?cs /escape?><?cs /def?>

<?cs def:imgUserLink(uin, name) ?>
	<?cs set:uinmod = uin % 4 + 1?>
	<?cs set:uinsrc = "http://qlogo"+uinmod+".store.qq.com/qzone/"+uin+"/"+uin+"/30" ?>
	<a class="feeds_comment_portrait c_tx q_namecard" link="nameCard_<?cs var:uin ?>" href="http://user.qzone.qq.com/<?cs var:uin ?>" target="_blank"><img alt="<?cs var:name ?>" src="<?cs var:uinsrc ?>" /></a>
<?cs /def ?>

<?cs def:replyView(item)?>
<div class="bbor5">
	<?cs call:imgUserLink(item.replyuin,item.replynick) ?>
	<div class="comment_reply_cont">
		<p class="comment_reply_text">
			<a href="<?cs var:item.replynickurl ?>" class="q_namecard q_des comment_nickname c_tx" link="nameCard_<?cs var:item.replyuin ?> des_<?cs var:item.replyuin ?>" target="_blank"><?cs call:html_escape(item.replynick) ?></a>
			<?cs call:html_escape(item.replycon)?>
		</p>
		<p class="comment_reply_op">
			<span class="feeds_time c_tx3"><?cs var:item.replytime ?></span>
		</p>
	</div>
</div>
<?cs /def ?>

<?cs def:commentView(item)?>
<div class="feeds_comment_list bg2" >
	<?cs call:imgUserLink(item.commuin,item.commnick)?>
	<div class="feeds_comment_cont">
		<p class="feeds_comment_text">		
		<a href="<?cs var:item.commnickurl ?>" class="q_namecard q_des comment_nickname c_tx" link="nameCard_<?cs var:item.commuin ?> des_<?cs var:item.commuin ?>" target="_blank"><?cs call:html_escape(item.commnick) ?></a><?cs call:html_escape(item.commcon)?></p>
		<p class="feeds_comment_op">
			<span class="feeds_time c_tx3"><?cs var:item.commtime ?></span>
			<qz:reply action="http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment" param="51''<?cs var:topicuin?>''<?cs var:topicid?>''<?cs var:item.commid ?>''<?cs var:item.commuin ?>''<?cs var:qz_metadata.feedtype ?>''0" type="text" version="6.2" charset="gb2312">回复</qz:reply>
		</p>
	</div>
	
	<?cs if:subcount(item.replys.0)>0?>
		<div class="comment_reply_list">
			<?cs each:rep = item.replys?>
				<?cs call:replyView(rep)?>
			<?cs /each?>
		</div>
	<?cs elif:subcount(item.replys)>0?>
		<div class="comment_reply_list">
			<?cs call:replyView(item.replys)?>
		</div>
	<?cs /if?>
</div>
<?cs /def?>

<?cs def:commentPanel() ?>
	<div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div>
		<?cs if:qz_metadata.showAllFeeds == 0 ?>
			<?cs if:qz_metadata.commtotal >3 ?>
			<div class="more_feeds_comment bg2">
				<qz:more action="http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment" param="51''<?cs var:topicuin?>''<?cs var:topicid?>''-1''0''<?cs var:qz_metadata.feedtype ?>''1" charset="gb2312">
					查看全部<?cs var:qz_metadata.commtotal ?>条评论&gt;&gt;
				</qz:more>
			</div>
			<?cs /if ?>
		<?cs /if ?>
		
		<?cs if:subcount(qz_metadata.comments.0)>0?>
			<?cs each:com = qz_metadata.comments?>
				<?cs call:commentView(com)?>
			<?cs /each?>
		<?cs elif:subcount(qz_metadata.comments)>0?>
			<?cs call:commentView(qz_metadata.comments)?>
		<?cs /if?>
		<qz:reply action="http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment" param="51''<?cs var:topicuin?>''<?cs var:topicid?>''-1''0''<?cs var:qz_metadata.feedtype ?>''0" type="text" version="6" charset="gb2312">回复</qz:reply>
	</div>
<?cs /def ?>


<?cs #:全局入口?>
<?cs call:commentPanel() ?>