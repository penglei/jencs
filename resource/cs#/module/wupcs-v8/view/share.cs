<?cs set:_title_after_nickname = 0?>
<?cs if: qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
	<?cs set:_title_after_nickname = 1?>
<?cs /if?>

<li class="f-single f-s-s" id="fct_<?cs call:v8_genFeedId() ?>">
	<div class="f-aside">
		<?cs call:v8_frame_avatar()?>
		<?cs #call:v8_frame_userinfo()?>
		<div class="f-user-info">
			<div class="f-nick">
				<?cs call:v8_feeduser_basicInfo()?>

				<?cs if:_title_after_nickname?>
				<?cs #TODO 有了新评论的被动不是这样展示的?>
					<?cs loop:i = 0, subcount(qfv.title.con) - 1, 1?>
						<?cs call:v8_title_item(qfv.title.con[i])?>
					<?cs /loop?>
				<?cs /if?>

			</div>
			<?cs call:v8_feed_extendinfo()?>
		</div>
		<?cs call:v8_genVipIcon() ?>
	</div>
	<div class="f-wrap">
		<div class="f-item f-s-i<?cs call:echo_item_class()?>" id="feed_<?cs call:v8_genFeedId() ?>" <?cs call:v8_feeds_frame_data()?>>
			<?cs if:!_title_after_nickname && subcount(qfv.title.con)?>
			<div class="f-info">
				<?cs set:_end = subcount(qfv.title.con) - 1?>
				<?cs loop:i = 0, _end, 1?>
					<?cs call:v8_title_item(qfv.title.con[i])?>
				<?cs /loop?>
			</div>
			<?cs /if?>
			<div class="qz_summary wupfeed" id="hex_<?cs call:v8_genFeedId() ?>">
				<?cs call:v8_echo_feed_data()?>

				<?cs if:qfv.content.layoutMode == G_LAYOUT_LEFTIMG_V8 ?><?cs # V8版的左图右文?>
					<?cs ##call:v8_quote()?>
					<?cs ##if:qfv.content.media.imgMode == G_IMG_NOIMG && 0?><?cs #转发全部要框?>
						<?cs ##<div class="f-ct f-ct-b-bg">?>
							<?cs ##<div class="f-ct-txtimg">?>
							<?cs ##call:v8_contentTxt_start("")?>
							<?cs #代替转发链的title如果有的话，应该在这里输出 ?>
							<?cs ##call:v8_contentBox_Title(qfv.content.title) ?>
							<?cs ##call:v8_content_genTitle(qfv.content.cntText.title.con)?>
							<?cs ##call:v8_conCommon(qfv.content.cntText.con)?>
							<?cs ##call:v8_contentTxt_end()?>
					<?cs ##else ?>
						<div class="f-ct f-ct-b-bg">

							<?cs #代替转发链的title如果有的话，应该在这里输出 ?>
							<?cs if:subcount(qfv.content.title.con)>0 ?>
								<div class="f-passive-info">
									<?cs call:v8_conCommon(qfv.content.title.con) ?>
								</div>
							<?cs /if ?>

							<div class="f-ct-txtimg f-ct-imgtxt f-ct-fixed <?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>f-ct-b-np<?cs /if ?>">
								<?cs call:v8_contentMedia()?>

								<?cs call:v8_contentTxt_start("")?>
								<?cs call:v8_content_genTitle(qfv.content.cntText.title.con)?>
								<?cs call:v8_conCommon(qfv.content.cntText.con)?>
								<?cs call:v8_contentTxt_end()?>
					<?cs ##/if ?>
						</div><?cs #必须先闭合 .f_ct_imgtxt?>
					</div><?cs #.f-ct?>

				<?cs else ?>
					<?cs #if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV && (subcount(qfv.content.media.video) || subcount(qfv.content.media.pic))?>
					<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
						<div class="f-ct f-ct-b-bg">
							<?cs if:subcount(qfv.content.title.con)>0 ?>
							<div class="f-passive-info">
								<?cs call:v8_conCommon(qfv.content.title.con) ?>
							</div>
							<?cs /if?>
							<div class="f-ct-txtimg f-ct-imgtxt f-ct-b-np <?cs if:subcount(qfv.content.media.video)?> f-ct-video<?cs /if?>">
								<?cs call:v8_contentMedia()?>
								<?cs #!必须判断三个条件?>
								<?cs if:subcount(qfv.content.cntText.con) ||
										subcount(qfv.content.cntText.title.con) ||
										subcount(qfv.content.title)?>
									<?cs call:v8_contentTxt_start("")?>
									<?cs call:v8_content_genTitle(qfv.content.cntText.title.con)?>
									<p><?cs call:v8_conCommon(qfv.content.cntText.con)?></p>
									<?cs call:v8_contentTxt_end()?>
								<?cs /if?>
							</div>
						</div>
					<?cs else ?><?cs #主动及非视屏其它被动?>
						<?cs if:qfv.content.layoutMode == G_LAYOUT_LEFTIMG ||
								subcount(qfv.content.media.music) ||
								(subcount(qfv.content.media.video) && qfv.content.media.video.0.bigImage != 1)
						?><?cs #左图右文?>
							<?cs set:_ft_class = "f-ct-imgtxt f-ct-fixed"?>

							<?cs if:subcount(qfv.content.media.video)?>
								<?cs set:_ft_class = _ft_class + " f-ct-video f-ct-video-s"?>
							<?cs /if?>

							<?cs call:v8_contentBox_start("borbg", _ft_class)?>
							<?cs call:v8_contentMedia()?>
							<?cs #!必须判断三个条件?>
							<?cs if:subcount(qfv.content.cntText.con) ||
									subcount(qfv.content.cntText.title.con) ||
									subcount(qfv.content.title)?>
								<?cs call:v8_contentTxt_start("")?>
								<?cs call:v8_contentBox_Title(qfv.content.title) ?>
								<?cs call:v8_content_genTitle(qfv.content.cntText.title.con)?>
								<p><?cs call:v8_conCommon(qfv.content.cntText.con)?></p>
								<?cs call:v8_contentTxt_end()?>
							<?cs /if?>

						<?cs else ?><?cs #普通模式，都是上文下图?>
							<?cs set:_ft_class = ""?>
							<?cs if:subcount(qfv.content.media.video)?>
								<?cs set:_ft_class = "f-ct-video"?>
							<?cs /if?>
							<?cs call:v8_contentBox_start("borbg", _ft_class)?>
							<?cs #!必须判断三个条件?>
							<?cs if:subcount(qfv.content.cntText.con) ||
									subcount(qfv.content.cntText.title.con) ||
									subcount(qfv.content.title)?>
								<?cs call:v8_contentTxt_start("")?>
								<?cs call:v8_contentBox_Title(qfv.content.title) ?>
								<?cs call:v8_content_genTitle(qfv.content.cntText.title.con)?>
								<p><?cs call:v8_conCommon(qfv.content.cntText.con)?></p>
								<?cs call:v8_contentTxt_end()?>
							<?cs /if?>
							<?cs call:v8_contentMedia()?>
							<?cs if:qfv.content.extendinfo.picnum > 9?>
								<span class="img-num"><?cs var:qfv.content.extendinfo.picnum?></span>
							<?cs /if?>
						<?cs /if?>
						<?cs call:v8_contentBox_end()?>

					<?cs /if?>
				<?cs /if ?>

				<div class="f-op-wrap">
					<?cs call:v8_operate()?>
					<?cs call:v8_comments-like()?>
					<?cs call:v8_genMergeHtml(qz_metadata.meta) ?>
				</div>
			</div><?cs #end: .qz_summary?>
		</div><?cs #end: .f-item?>
	</div><?cs #end: .f-wrap?>
</li>
