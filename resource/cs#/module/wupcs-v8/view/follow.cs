<?cs def:v8_follow_contentBox_start()?>
	<div class="f-ct f-ct-b-bg f-ct-webpage-passive">

	<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>

	<div class="f-ct-txtimg">
	<?cs if:subcount(title.con)>0 ?>
		<div class="txt-box">
			<h4 class="txt-box-title">
				<?cs call:v8_conCommon(title.con) ?>
			</h4>
			<div class="f-ang-t"></div>
		</div>
	<?cs /if ?>
<?cs /def?>

<?cs def:v8_follow_contentBox_end()?>
		</div><?cs #必须先闭合 .f_ct_txtimg?>
	</div>
<?cs /def?>

<?cs def:v8_userLike_comp(user)?>
	<?cs if:user.uin?>
	<?cs if:!user.who?><?cs set:user.who = USER_PLATFORM_WHO_QZONE?><?cs /if?>
	<a class="f-name <?cs if:user.who == USER_PLATFORM_WHO_QZONE?> q_namecard<?cs /if?>" 
	<?cs if:user.who == USER_PLATFORM_WHO_QZONE?>
		link="nameCard_<?cs var:user.uin?>" 
	<?cs /if?>

		target="_blank" 
		href="<?cs call:v8_echoUserlink(user)?>"
	>
		<?cs alt:user.prefix?><?cs /alt?>
		<?cs call:v8_echoUsername(user, user.nickname)?> </a>
	<?cs /if?>
<?cs /def?>

<?cs def:v8_follow_title_item(con)?>
	<?cs if:con.action.type == "popup"?>
		<?cs call:v8_con_popup(con)?>
	<?cs else ?>
		<?cs if:con.type == "txt"?>
			<?cs call:v8_con_txt(con)?>
		<?cs elif:con.type == "nick"?>
			<?cs call:v8_userLike_comp(con)?>
		<?cs elif:con.type == "url"?>
			<?cs call:v8_con_url(con)?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs def:v8_follow_like_text_item(con) ?>
	<?cs if:con.type == "txt"?>
		<?cs call:v8_con_txt(con)?>
	<?cs elif:con.type == "url"?>
		<?cs call:v8_con_url(con)?>
	<?cs elif:con.type == "nick"?>
		<?cs call:v8_con_nick(con)?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:赞被动feeds把赞列表放这里 ?>
<?cs def:v8_follow_like_text() ?>
	<?cs if:subcount(qfv.content.like_extendinfo.con)>0 ?>
		<div class="f-like f-like-fv">
		    <div class="f-op-txt">
				<?cs loop:i = 0, subcount(qfv.content.like_extendinfo.con) - 1, 1?>
					<?cs call:v8_follow_like_text_item(qfv.content.like_extendinfo.con[i]) ?>	
				<?cs /loop?>
		    </div>
		</div>
	<?cs /if ?>
<?cs /def ?>

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
						<?cs call:v8_follow_title_item(qfv.title.con[i])?>
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

				<?cs call:v8_quote()?>

				<?cs if:subcount(qfv.content.media.music)?><?cs #说说转发音乐?>
					<div class="f-ct f-ct-b-bg f-ct-passive">
						<div class="f-passive-info">
							<?cs call:v8_conCommon(qfv.content.cntText.title.con)?>
						</div>
						<div class="f-ct-txtimg f-ct-imgtxt f-ct-b-np f-ct-fixed">
							<?cs call:v8_contentMedia()?>
						</div>
					</div>
				<?cs elif:subcount(qfv.content.media.video) ||
						qfv.content.layoutMode == G_LAYOUT_LEFTIMG ||
						qfv.content.layoutMode == G_LAYOUT_LEFTIMG_V8?>
					<div class="f-ct f-ct-b-bg f-ct-passive">
						<?cs if:subcount(qfv.content.title.con)>0 ?>
						<div class="f-passive-info">
							<?cs call:v8_conCommon(qfv.content.title.con) ?>
						</div>
						<?cs /if?>
						<div class="f-ct-txtimg f-ct-imgtxt f-ct-b-np
									<?cs if:subcount(qfv.content.media.video)?> f-ct-video<?cs /if?>"
						>
							<?cs call:v8_contentMedia()?>

							<?cs call:v8_contentTxt_start("")?>
							<?cs call:v8_content_genTitle(qfv.content.cntText.title.con)?>
							<p><?cs call:v8_conCommon(qfv.content.cntText.con)?></p> <?cs #:加个p好通过dom获得 teddy ?>
							<?cs call:v8_contentTxt_end()?>
						</div>
					</div>
				<?cs else ?>
					<div class="f-ct f-ct-b-bg f-ct-passive">
						<div class="f-ct-txtimg  img-box-row-wrap">
							<?cs set:g_view_txtimg = 1?><?cs #上文下图的被动，不用压缩?>
							<?cs call:v8_contentTxt("")?>
							<?cs call:v8_contentMedia()?>
						</div>
					</div>
				<?cs /if?>

				<div class="f-op-wrap">
					<?cs call:v8_follow_like_text() ?>
					<?cs call:v8_operate()?>
					<?cs call:v8_comments-like()?>
				</div>

			</div><?cs #end: .qz_summary?>
		</div><?cs #end: .f-item?>
	</div><?cs #end: .f-wrap?>
</li>

