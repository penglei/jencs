<?cs #:投票组件  ?>
<?cs def:voteViewer(mod) ?>
<?cs if:subcount(qz_metadata.vote_box) > 0 ?>
<?cs with:voteBox = qz_metadata.vote_box ?>
<?cs set:qz_votebox_mainpic = 0?>
<?cs if:subcount(voteBox.mainpic) > 0?>
	<?cs set:qz_votebox_mainpic = 1 ?>
<?cs /if ?>
<?cs set:qz_votebox_isvideo = 0?>
<?cs if:voteBox.votetype == 1?>
	<?cs set:qz_votebox_isvideo = 1?>
<?cs /if?>
<qz:plugin name="Vote" config="topicid=<?cs var:qz_metadata.vote_box.topicid?>&owneruin=<?cs var:qz_metadata.vote_box.owner?>&voteid=<?cs var:qz_metadata.vote_box.id?>">
	<div class="qz_vote vote-feed-container" 
		data-owner="<?cs var:qz_metadata.vote_box.owner?>" 
		data-haspic="<?cs var:qz_votebox_mainpic?>" 
		data-isvideo="<?cs var:qz_votebox_isvideo?>">
		<div class="qz_vote_inner<?cs if:!qz_votebox_mainpic?> qz_vote_inner_wide<?cs /if?><?cs if:qz_votebox_isvideo?> icon_video_thumb<?cs /if?>">
			<?cs if:qz_votebox_mainpic?>
			<div class="vote_img">
				<a href="<?cs alt:voteBox.mainpic.jumpurl?>javascript:;<?cs /alt?>" <?cs if:voteBox.mainpic.jumpurl ?>target="_blank" onclick="QZFL.event.cancelBubble(event);"<?cs /if?>>
					<img src="<?cs call:htmlEncodeVar(voteBox.mainpic.url,2,0) ?>"
						 alt="vote image"
						 <?cs #data-role 命名不好，其实应该叫qz-vote-leftbtn?>
						 data-role="qz-vote-videobtn" />
					<?cs if:qz_votebox_isvideo?>
						<i class="ui_icon icon_qz_vote_video" data-role="qz-vote-videobtn"></i>
					<?cs /if?>
				</a>
			</div>
			<?cs /if?><?cs #vote_img?>
			<div class="vote_main vote_main_fold">
				<ul class="vote_list">
					<?cs set:vote_show_count = 6 ?>
					<?cs set:vote_item_count = subcount(qz_metadata.vote_box.options)?>
					<?cs if:vote_item_count < vote_show_count ?>
					<?cs set:vote_show_count = vote_item_count?>
					<?cs /if?>
					<?cs loop:i = 0, vote_show_count - 1, 1 ?>
						<li class="vote_item bg2 bg3_hover">
							<label class="vote_label"><i class="ui_icon icon_qz_vote_checked"></i><input type="<?cs if:voteBox.limit == 1?>radio<?cs else ?>checkbox<?cs /if?>" />
							<?cs var:qz_metadata.vote_box.options[i] ?>
							</label>
						</li>
					<?cs /loop ?>
				</ul>
				<?cs if:qz_metadata.vote_box.num > vote_show_count?>
				<div class="vote_btnbar bg2"><a href="javascript:;" class="ui_mr10">展开更多<?cs var:qz_metadata.vote_box.num - vote_show_count?>项↓</a></div>
				<?cs /if?>
			</div>
		</div>
	</div>
</qz:plugin>
<?cs /with ?>
<?cs /if ?>
<?cs /def ?>