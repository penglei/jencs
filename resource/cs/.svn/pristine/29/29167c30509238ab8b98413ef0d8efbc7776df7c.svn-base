<?cs set:_title_after_nickname = 0?>

<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
	<?cs set:_title_after_nickname = 1?>
<?cs /if?>

<?cs set:_borbgtype = ""?>
<?cs if:qfv.meta.typeid == MOOD_TYPEID_FORWARD ||
		qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ||
		qfv.meta.feedstype == UC_WUP_FEEDSTYPE_ABT ||
		subcount(qfv.content.media.music)?>
	<?cs set:_borbgtype = "f-ct-b-bg"?>
<?cs /if ?>

<?cs set:_txtimg_class=""?><?cs #TODO 这个没有用了，只有左图右文才需要?>
<?cs if: subcount(qfv.content.media.video) ?>
	<?cs set:_txtimg_class = "f-ct-video"?>
<?cs elif:subcount(qfv.content.media.music) ?>
	<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
		<?cs set:_txtimg_class = "f-ct-imgtxt f-ct-fixed f-ct-b-np"?>
	<?cs else ?>
		<?cs set:_txtimg_class = "f-ct-imgtxt f-ct-fixed"?>
	<?cs /if?>
<?cs /if?>

<li class="f-single f-s-s" id="fct_<?cs call:v8_genFeedId() ?>">
	<div class="f-aside">
		<?cs call:v8_frame_avatar()?>

		<div class="f-user-info">
			<div class="f-nick">
				<?cs call:v8_feeduser_basicInfo()?>

				<?cs if:_title_after_nickname?>
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
				<?cs set:_end = subcount(qfv.title.con) - 1?>
				<?cs if:_end >= 0?>
					<div class="f-info<?cs if: qfv.title.con_more > 0 ?> qz_info_cut<?cs /if?>">
						<?cs #被动说说把内空当作标题?>
						<?cs loop:i = 0, _end, 1?>
							<?cs call:v8_title_item(qfv.title.con[i])?>
						<?cs /loop?>

						<?cs if: qfv.title.con_more > 0 ?>
							 <a href="javascript:;" data-cmd="qz_toggle" data-pos="1" class="ui-ft12">展开全文</a>
							<img src="http://qzonestyle.gtimg.cn/qzone_v6/img/feed/loading.gif" class="load_img none">
						<?cs /if?>
					</div><?cs #/*endfor: .f-info*/ ?>
					<?cs if: qfv.title.con_more > 0 ?>
					<div class="f-info qz_info_complete none"></div>
					<?cs /if?>
				<?cs /if?>
			<?cs else ?>
				<?cs #转发别人的转的，并且带转发理由的，需要在这里展示title2，也就是title的位置?>
				<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT_NOTIFYPSV?>
					<?cs set:_end = subcount(qfv.title2.con) - 1?>
					<?cs if:_end >= 0?>
						<div class="f-info">
							<?cs loop:i = 0, _end, 1?>
								<?cs call:v8_title_item(qfv.title2.con[i])?>
							<?cs /loop?>
						</div>
					<?cs /if?>
				<?cs /if?>
			<?cs /if?>

			<div class="qz_summary wupfeed" id="hex_<?cs call:v8_genFeedId() ?>">
				<?cs call:v8_echo_feed_data()?>
				<?cs #call:v8_quote()?>

				<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?><?cs #被动 ?>
					<?cs if:subcount(qfv.content.media.music) ||
							subcount(qfv.content.media.pic) > 1 ||
							qfv.content.layoutMode == G_LAYOUT_LEFTIMG?>
						<?cs #音乐需要f-ct-passive来进行样式调整?>
						<?cs #被动多图需要 f-ct-passive来进行大小控制?>
						<?cs set:_borbgtype = "f-ct-b-bg f-ct-passive"?>
					<?cs /if ?>

					<div class="f-ct <?cs var:_borbgtype?>">

						<?cs #if:qfv.content.layoutMode == G_LAYOUT_LEFTIMG_V8"?>
						<?cs if:_g_forward == 1?>
							<?cs if:subcount(qfv.content.title.con)?>
							<div class="f-passive-info">
								<?cs call:v8_conCommon(qfv.content.title.con)?>
							</div>
							<?cs /if?>
							<?cs #转发?>
							<div class="f-ct-txtimg<?cs if:subcount(qfv.content.media.pic)?> f-ct-imgtxt<?cs /if?> f-ct-b-np">
								<?cs call:v8_contentMedia()?>
								<?cs call:v8_contentTxt_start("")?>
								<?cs #call:v8_contentBox_Title(qfv.content.title) ?>
								<?cs call:v8_content_genTitle(qfv.content.cntText.title.con)?>
								<?cs call:v8_conCommon(qfv.content.cntText.con)?>
								<?cs call:v8_contentTxt_end()?>
							</div>
						<?cs else ?>
							<?cs #音乐被动 ?>
							<?cs if:subcount(qfv.content.media.music) && subcount(qfv.content.cntText.title.con)?>
								<div class="f-passive-info">
									<?cs call:v8_conCommon(qfv.content.cntText.title.con)?>
								</div>
							<?cs /if?>

							<div class="f-ct-txtimg 
									<?cs var:_txtimg_class?>
									<?cs if:qfv.content.media.imgMode == G_IMG_GRID_MODE?> img-box-row-wrap<?cs /if?>"
							>
								<?cs #其他说说被动 ?>
								<?cs if:subcount(qfv.content.cntText.con) || subcount(qfv.content.cntText.title.con) || subcount(qfv.content.title)?>
									<?cs if:!subcount(qfv.content.media.music) ?>

											<?cs if:qfv.content.con_more > 0?>
												<?cs call:v8_contentTxt_start(" qz_info_cut")?>
											<?cs else ?>
												<?cs call:v8_contentTxt_start("")?>
											<?cs /if?>

											<?cs call:v8_contentBox_Title(qfv.content.title) ?>
											<?cs call:v8_conCommon(qfv.content.cntText.title.con)?>

											<?cs if: qfv.content.con_more > 0 ?>
												 <a href="javascript:;" data-cmd="qz_toggle" data-pos="2" class="ui-ft12">展开全文</a>
												<img src="http://qzonestyle.gtimg.cn/qzone_v6/img/feed/loading.gif" class="load_img none">
											<?cs /if?>
											<?cs call:v8_contentTxt_end()?>
										<?cs if: qfv.content.con_more > 0 ?>
											<div class="txt-box qz_info_complete none"></div>
										<?cs /if?>
									<?cs /if?>
								<?cs /if?>

								<?cs set:g_view_txtimg = 1?><?cs #上文下图的被动，不用压缩?>
								<?cs call:v8_contentMedia()?>
							</div><?cs #/*必须先闭合 .f-ct-imgtxt*/?>
							<?cs call:attachInfo(qfv.content.attach) ?>
						<?cs /if?>
					</div>
				<?cs else ?>
					<?cs #主动 ?>
					<?cs if:qfv.content != 0?>
						<div class="f-ct <?cs var:_borbgtype?>">
							<div class="f-ct-txtimg 
									<?cs var:_txtimg_class?>
									<?cs if:qfv.content.media.imgMode == G_IMG_GRID_MODE && subcount(qfv.content.media.pic) > 1?> img-box-row-wrap<?cs /if?>"
							>
								<?cs if:qfv.content.layoutMode == G_LAYOUT_LEFTIMG?><?cs #左图右文?>
									<?cs call:v8_contentMedia()?>
									<?cs call:v8_contentTxt("")?>
								<?cs else ?><?cs #普通模式，都是上文下图?>
									<?cs if:!subcount(qfv.content.media.music) ?>
										<?cs if:qfv.content.con_more > 0?>
											<?cs call:v8_contentTxt_start(" qz_info_cut")?>
										<?cs else ?>
											<?cs call:v8_contentTxt_start("")?>
										<?cs /if?>
										<?cs call:v8_contentBox_Title(qfv.content.title) ?>
										<?cs call:v8_conCommon(qfv.content.cntText.title.con)?>
										<?cs if: qfv.content.con_more > 0 ?>
											 <a href="javascript:;" data-cmd="qz_toggle" data-pos="2" class="ui-ft12">展开全文</a>
											<img src="http://qzonestyle.gtimg.cn/qzone_v6/img/feed/loading.gif" class="load_img none">
										<?cs /if?>
										<?cs call:v8_contentTxt_end()?>
										<?cs if: qfv.content.con_more > 0 ?>
											<div class="txt-box qz_info_complete none"></div>
										<?cs /if?>
									<?cs /if?>
									<?cs call:v8_contentMedia()?>
								<?cs /if?>
							</div><?cs #/*必须先闭合 .f-ct-imgtxt*/?>
							<?cs call:attachInfo(qfv.content.attach) ?>
						</div>
					<?cs /if?>
				<?cs /if?>

				<div class="f-op-wrap">
					<?cs call:v8_operate()?>
					<?cs call:v8_comments-like()?>
					<?cs call:v8_genMergeHtml(qz_metadata.meta)?>
				</div>
			</div><?cs #end: .qz_summary?>
		</div><?cs #end: .f-item?>
	</div><?cs #end: .f-wrap?>
</li>
