<?cs def:v8_gift_extendinfo() ?>
	<?cs if:subcount(qfv.content.extendinfo.avatar) >0 ?>
		<div class="f-guest">
			<div class="guest-ava">
			<?cs set:_end = subcount(qfv.content.extendinfo.avatar) - 1?>
			<?cs loop:j=0, _end, 1?>
				<?cs call:v8_userAvatar_comp(qfv.content.extendinfo.avatar[j],30) ?>
			<?cs /loop?>
		</div>
		<span class="guest-text">
			<?cs call:v8_conCommon(qfv.content.extendinfo.con)?>
		</span>
	</div>
	<?cs /if ?>
<?cs /def ?>



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
			<?cs if:!_title_after_nickname?>
			<div class="f-info">
				<?cs set:_end = subcount(qfv.title.con) - 1?>
				<?cs loop:i = 0, _end, 1?>
					<?cs call:v8_title_item(qfv.title.con[i])?>
				<?cs /loop?>
			</div>
			<?cs /if?>

			<div class="qz_summary wupfeed" id="hex_<?cs call:v8_genFeedId() ?>">
				<?cs call:v8_echo_feed_data()?>

			<?cs if:qfv.content.layoutMode == G_LAYOUT_LEFTIMG_V8 ?>
				<div class="f-ct f-ct-b-bg">
					<div class="f-ct-txtimg f-ct-imgtxt f-ct-fixed">
						<?cs call:v8_contentMedia()?>
						<?cs call:v8_contentTxt_start("")?>
						<?cs call:v8_content_genTitle(qfv.content.cntText.title.con)?>
						<?cs call:v8_conCommon(qfv.content.cntText.con)?>
						<?cs if:qfv.meta.subtype == GIFT_TYPE_GIFTROBOT ||
								qfv.meta.giftonly_type == GIFT_subtype_birthday ||
								qfv.meta.giftonly_type == GIFT_subtype_birthday_xy
							?>
							<?cs set:qfv.content.media.pic.0.action.className="fixed-btn" ?>
							<p class="fixed-bottom">
								<?cs call:v8_popup_start(qfv.content.media.pic.0)?>
									<i class="ui-icon icon-birthday"></i>赠送礼物
								<?cs call:v8_popup_end()?>
							</p>
						<?cs /if ?>
						<?cs call:v8_contentTxt_end()?>
					</div><?cs #必须先闭合 .f_ct_imgtxt?>
				</div>

				<div class="f-op-wrap">
					<?cs call:v8_operate()?>
					<?cs #/*comments-like中包含了赞的信息,如果要单独展示赞信息而不展示评论信息，需要在数据转换层控制*/?>
					<?cs call:v8_comments-like()?>
				</div>
			<?cs else ?>
				<?cs #call:v8_quote()?>

				<div class="f-ct f-ct-b-bg f-ct-passive">
					<div class="f-ct-txtimg f-ct-imgtxt<?cs if:qfv.meta.subtype != GIFT_EXPIRE_YELLOW?> f-ct-fixed<?cs /if?>">

						<div class="img-box">
							<?cs #call:v8_contentMediaPic_item(qfv.content.media.pic.0)?><?cs #语音礼物的type为8，放到里面去处理了?>
							<?cs with:picItem = qfv.content.media.pic.0?>
							<?cs call:v8_popup_start(picItem)?>
								<?cs call:v8_imageFlag(picItem.type) ?>
								<?cs #call:v8_media_img(picItem)?>
								<img src="/ac/b.gif" onload="<?cs escape:'html'?>
								<?cs if:picItem.type == 8 || picItem.type == 9?>
									<?cs call:v8_contentBox_ReduceImgByShortEdge_onLoad(picItem, 100, 100, '')?>
								<?cs elif:qfv.meta.subtype == GIFT_EXPIRE_YELLOW?><?cs #黄钻催费的feed,唯一一个被动大图展示 ?>
									<?cs call:v8_contentBox_ReduceImgByShortEdge_onLoad(picItem, 400, 140, '')?>
								<?cs else?>
									<?cs call:v8_contentBox_ReduceImgByLongEdge_AlignCenter_onLoad(picItem, 100, 100, '')?>
								<?cs /if?><?cs /escape:'html'?>
								" />
							<?cs call:v8_popup_end()?>
							<?cs /with?>
						</div>
						<div class="txt-box">
							<p class="info spacing">
								<?cs #确定这里只有一项?>
								<?cs with:con = qfv.content.cntText.title.con.0?>
									<?cs if:con.action.type == "popup"?>
										<?cs call:v8_con_popup(con)?>
									<?cs else ?>
										<?cs call:v8_conCommon_item(con)?>
									<?cs /if?>
								<?cs /with?>
							</p>
							<p class="info state">
								<?cs call:v8_conCommon(qfv.content.cntText.con) ?>
							</p>
							<?cs if:subcount(qfv.content.button.huizen)?>
							<?cs with:btn = qfv.content.button.huizen?>
								<p class="fixed-bottom">
									<?cs set:btn.action.className = "fixed-btn"?>
									<?cs call:v8_popup_start(btn)?>
										<i class="ui-icon icon-gift"></i><?cs var:btn.text?>
									<?cs call:v8_popup_end()?>
								</p>
							<?cs /with?>
							<?cs /if?>
						</div>
					</div><?cs #/*必须先闭合 .f-ct-imgtxt*/?>
				</div>

				<div class="f-op-wrap">
					<?cs call:v8_operate()?>
					<?cs #/*comments-like中包含了赞的信息,如果要单独展示赞信息而不展示评论信息，需要在数据转换层控制*/?>
					<?cs call:v8_comments-like()?>
					<?cs call:v8_genMergeHtml(qz_metadata.meta)?>
				</div>
			<?cs /if ?>
			</div><?cs #end: .qz_summary?>
		</div><?cs #end: .f-item?>
	</div><?cs #end: .f-wrap?>
</li>
