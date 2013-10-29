<?cs #:认证标记 ?>
<?cs def:getLogo(friend) ?>
	<?cs if:friend.isauth==1 ?>
		<a class="ui_mr5" target="_blank" href="http://page.qq.com" >
			<img src="/ac/qzone_v5/client/auth_icon.png" title="腾讯认证" alt="腾讯认证" />
		</a>
	<?cs /if ?>
<?cs /def ?>

<li class="topic_item" key="<?cs var:qz_metadata.qz_data.key1 ?>" feedkey="<?cs var:qz_metadata.hot_topic.key ?>" data-url="<?cs var:qz_metadata.metadata.commentsurl ?>"
	<?cs if:qz_metadata.from=='pos' ?> poskey="<?cs var:'13_'+qz_metadata.metadata.adid+'_'+qz_metadata.metadata.traceid ?>"<?cs /if ?>>
	<?cs if:subcount(qz_metadata.hot_topic.friend.0)>0 ?>
		<?cs set:uin=qz_metadata.hot_topic.friend.0.uin ?>
		<?cs set:avatar=qz_metadata.hot_topic.friend.0.avatar ?>
	<?cs else ?>
		<?cs set:uin=qz_metadata.hot_topic.friend.uin ?>
		<?cs set:avatar=qz_metadata.hot_topic.friend.avatar ?>
	<?cs /if ?>
	<a class="topic_avatar" target="_blank" href="http://user.qzone.qq.com/<?cs var:uin ?>"><img src="<?cs var:avatar ?>" alt=""></a>
	<div class="topic_main">
		<div class="topic_title">
			<?cs #第一个好友肯定要显示 ?>
			<?cs if:subcount(qz_metadata.hot_topic.friend.0)>0 ?>
				<?cs set:uin=qz_metadata.hot_topic.friend.0.uin ?>
				<?cs set:nickname=qz_metadata.hot_topic.friend.0.nickname ?>
			<?cs else ?>
				<?cs set:uin=qz_metadata.hot_topic.friend.uin ?>
				<?cs set:nickname=qz_metadata.hot_topic.friend.nickname ?>
			<?cs /if ?>
			<a target="_blank" href="http://user.qzone.qq.com/<?cs var:uin ?>" class="ui_mr5"><?cs var:html_encode(nickname, 1) ?></a>
			<?cs if:subcount(qz_metadata.hot_topic.friend.0)>0 ?>
				<?cs call:getLogo(qz_metadata.hot_topic.friend.0) ?>
			<?cs else ?>
				<?cs call:getLogo(qz_metadata.hot_topic.friend) ?>
			<?cs /if ?>
			<?cs #第二个好友要看下有没有 ?>
			<?cs if:subcount(qz_metadata.hot_topic.friend.1)>0 ?>
				<?cs set:uin=qz_metadata.hot_topic.friend.1.uin ?>
				<?cs set:nickname=qz_metadata.hot_topic.friend.1.nickname ?>
				<span class="c_tx3">、</span>
				<a target="_blank" href="http://user.qzone.qq.com/<?cs var:uin ?>" class="ui_mr5"><?cs var:html_encode(nickname, 1) ?></a>
				<?cs call:getLogo(qz_metadata.hot_topic.friend.1) ?>
			<?cs /if ?>
			<?cs #参与话题的描述要看下是不是pos的 ?>
			<?cs if:qz_metadata.from=="pos" ?>
			<?cs elif:qz_metadata.hot_topic.friend_count==1 || qz_metadata.hot_topic.friend_count==2 ?>
				<span class="c_tx3">参与了话题：</span>
			<?cs elif:qz_metadata.hot_topic.friend_count>2 ?>
				<span class="c_tx3">等<?cs var:qz_metadata.hot_topic.friend_count ?>位好友参与了话题：</span>
			<?cs /if ?>
		</div>
		<div class="topic_text">
			<a class="c_tx2 noline" target="_blank" href="<?cs var:qz_metadata.metadata.commentsurl ?>">
			<?cs var:qz_metadata.content ?>
			<?cs if:qz_metadata.metadata.appid==2 ?>
				<?cs var:qz_metadata.metadata.title ?>
			<?cs elif:qz_metadata.metadata.appid==311 ?>
				<?cs var:qz_metadata.actiontitle.orgcontent ?>
			<?cs /if ?>
			</a>
		</div>
		<?cs if:qz_metadata.content_box.media.media.type=="pic" && qz_metadata.content_box.media.media.src ?>
			<?cs set:pic=qz_metadata.content_box.media.media.src ?>
		<?cs elif:qz_metadata.content_box.media.media.0.type=="pic" && qz_metadata.content_box.media.media.0.src ?>
			<?cs set:pic=qz_metadata.content_box.media.media.0.src ?>
		<?cs /if ?>
		<?cs if:pic ?>
			<div class="topic_img"><a target="_blank" href="<?cs var:qz_metadata.metadata.commentsurl ?>"><img src="<?cs var:pic ?>" alt=""></a></div>
		<?cs /if ?>
		<div class="topic_op">
			<a href="javascript:;" class="topic_op_link noline"><i class="ui_icon icon_hottopic_like"></i><span class="act_text">感兴趣</span></a> 
			<span class="topic_op_num bor2<?cs if:qz_metadata.hot_topic.act_count==0?> none<?cs /if?>">
				<span class="act_count"><?cs var:qz_metadata.hot_topic.act_count ?></span>
				<b class="ui_trigbox ui_trigbox_l">
					<b class="ui_trig bor2"></b>
					<b class="ui_trig ui_trig_up bor_bg2"></b>
				</b>
			</span> 
			<span class="topic_op_jia1 c_tx4 none">+1</span>
		</div>
	</div>
</li>