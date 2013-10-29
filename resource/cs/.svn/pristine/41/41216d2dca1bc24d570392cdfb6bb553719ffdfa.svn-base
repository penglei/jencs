<?cs #:投票组件  ?>
<?cs def:v8_vote_viewer() ?>
<?cs if:subcount(qfv.content.vote_box) > 0 ?>
<?cs with:voteBox = qfv.content.vote_box ?>
<?cs set:qz_votebox_mainpic = 0?>
<?cs if:subcount(voteBox.mainpic) > 0?>
	<?cs set:qz_votebox_mainpic = 1 ?>
<?cs /if ?>
<?cs set:qz_votebox_isvideo = 0?>
<?cs if:voteBox.votetype == 1?>
	<?cs set:qz_votebox_isvideo = 1?>
<?cs /if?>
	<div class="qz-vote vote-feed-container" 
		data-owner="<?cs var:voteBox.owner?>" 
		data-haspic="<?cs var:qz_votebox_mainpic?>" 
		data-cmd="qz_plugin" 
		data-name="Vote" 
		data-config="topicid=<?cs var:voteBox.topicid?>&owneruin=<?cs var:voteBox.owner?>&voteid=<?cs var:voteBox.id?>" 
		data-isvideo="<?cs var:qz_votebox_isvideo?>">
		<div class="qz-vote-inner<?cs if:!qz_votebox_mainpic?> vote-img-large<?cs /if?><?cs if:qz_votebox_isvideo?> icon_video_thumb<?cs /if?>">
			<?cs if:qz_votebox_mainpic?>
			<div class="vote-img">
				<?cs call:ugc_url_check(voteBox.mainpic.jumpurl,0) ?>
				<a href="<?cs alt:ugc_url_check.ret?>javascript:;<?cs /alt?>" <?cs if:ugc_url_check.ret ?>target="_blank" onclick="QZFL.event.cancelBubble(event);"<?cs /if?>>
					<img src="<?cs var:html_encode(voteBox.mainpic.url, 1) ?>"
						 alt="vote image"
						 <?cs #data-role 命名不好，其实应该叫qz-vote-leftbtn?>
						 data-role="qz-vote-videobtn" />
					<?cs if:qz_votebox_isvideo?>
						<i class="ui_icon icon_qz_vote_video" data-role="qz-vote-videobtn"></i>
					<?cs /if?>
				</a>
			</div>
			<?cs /if?><?cs #vote_img?>
			<div class="vote-main">
				<ul class="vote-list">
					<?cs set:vote_show_count = 6 ?>
					<?cs set:vote_item_count = subcount(voteBox.options)?>
					<?cs if:vote_item_count < vote_show_count ?>
					<?cs set:vote_show_count = vote_item_count?>
					<?cs /if?>
					<?cs loop:i = 0, vote_show_count - 1, 1 ?>
						<li class="vote-item">
							<label class="vote-label">
							<i class="ui-icon qz-<?cs if:voteBox.limit == 1?>radio<?cs else ?>checkbox<?cs /if?>"></i>
							<input type="<?cs if:voteBox.limit == 1?>radio<?cs else ?>checkbox<?cs /if?>" class="ui-icon none"/>
							<?cs var:voteBox.options[i] ?>
							<i class="ui-icon icon-qz-vote-checked"></i>
							</label>
						</li>
					<?cs /loop ?>
				</ul>
				<?cs if:voteBox.num > vote_show_count?>
				<div class="vote-btnbar"><a href="javascript:;" class="btn-unfold">展开选项</a></div>
				<?cs /if?>
			</div>
		</div>
	</div>
<?cs /with ?>
<?cs /if ?>
<?cs /def?>


<li class="f-single f-s-s" id="fct_<?cs call:v8_genFeedId() ?>">
	<div class="f-aside">
		<?cs call:v8_frame_avatar()?>
		<?cs #$call:v8_frame_userinfo()?>

		<div class="f-user-info">
			<div class="f-nick">
				<?cs call:v8_feeduser_basicInfo()?>

				<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
				<?cs #TODO 有了新评论的被动不是这样展示的?>
					<?cs loop:i = 0, subcount(qfv.title.con) - 1, 1?>
						<?cs call:v8_title_item(qfv.title.con[i])?>
					<?cs /loop?>
				<?cs /if?>

			</div>
			<?cs call:v8_feed_extendinfo()?>
		</div>
		<?cs call:v8_genVipIcon() ?>

	</div><?cs #f-aside?>
	<div class="f-wrap">
		<div class="f-item f-s-i<?cs call:echo_item_class()?>" id="feed_<?cs call:v8_genFeedId() ?>" <?cs call:v8_feeds_frame_data()?> >
			<?cs if:qfv.meta.feedstype != UC_WUP_FEEDSTYPE_PSV?>
			<div class="f-info">
				<?cs loop:i = 0, subcount(qfv.title.con) - 1, 1?>
					<?cs call:v8_title_item(qfv.title.con[i])?>
				<?cs /loop?>
			</div><?cs #end: .f-info?>
			<?cs /if?>
			<div class="qz_summary wupfeed" id="hex_<?cs call:v8_genFeedId() ?>">
				<?cs call:v8_echo_feed_data()?>

				<?cs if:subcount(qfv.content.vote_box) == 0 ?>
					<?cs call:v8_contentBox("", "f-ct-imgtxt")?>
				<?cs else ?>
					<div class="f-ct">
						<div class="f-ct-txtimg">
							<?cs call:v8_vote_viewer()?>
						</div><?cs #必须先闭合 .f_ct_imgtxt?>
					</div>
				<?cs /if?>

				<div class="f-op-wrap">
					<?cs call:v8_operate()?>
					<?cs call:v8_comments-like()?>
				</div>

			</div><?cs #end: .qz_summary?>
		</div><?cs #end: .f-item?>
	</div><?cs #end: .f-wrap?>
</li>
