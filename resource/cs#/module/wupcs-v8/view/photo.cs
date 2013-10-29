<?cs set:_title_after_nickname = 0?>
<?cs #if:(qfv.meta.feedoptype == UC_WUP_FEED_TYPE_NEWCOMMENT && subcount(qz_metadata.orgdata.itemdata) > 0 ) ||?>
<?cs if: qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
	<?cs set:_title_after_nickname = 1?>
<?cs /if?>

<li class="f-single f-s-s" id="fct_<?cs call:v8_genFeedId() ?>">
	<div class="f-aside">
		<?cs call:v8_frame_avatar()?>
		<?cs #$call:v8_frame_userinfo()?>
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

	</div><?cs #f-aside?>

	<div class="f-wrap">
		<div class="f-item f-s-i<?cs call:echo_item_class()?>" id="feed_<?cs call:v8_genFeedId() ?>" <?cs call:v8_feeds_frame_data()?> >
			<?cs if:!_title_after_nickname?>
			<div class="f-info">
				<?cs loop:i = 0, subcount(qfv.title.con) - 1, 1?>
					<?cs call:v8_title_item(qfv.title.con[i])?>
				<?cs /loop?>
			</div><?cs #end: .f-info?>
			<?cs /if?>

			<div class="qz_summary wupfeed" id="hex_<?cs call:v8_genFeedId() ?>">
				<?cs call:v8_echo_feed_data()?>

				<div class="f-ct
						<?cs if:qfv.meta.feedstype != UC_WUP_FEEDSTYPE_ACT ?> f-ct-b-bg<?cs /if ?>
						<?cs #passive，被动宫格小图。注意数据层的逻辑，用这个判断多图宫格型被动就行了?>
						<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV && qfv.content.media.imgMode == G_IMG_GRID_MODE_SMALL?> f-ct-passive<?cs /if?>
						"
				>
					<div class="f-ct-txtimg
						<?cs if:qfv.content.media.imgMode == G_IMG_GRID_MODE && subcount(qfv.content.media.pic) > 1?> img-box-row-wrap<?cs /if?>"
					>
						<?cs call:v8_contentTxt("txt-big-size")?>
						<?cs set:g_view_txtimg = 1?><?cs #上文下图的被动，不用压缩?>
						<?cs call:v8_contentMedia()?>

					<?cs if:qz_metadata.meta.feedstype != UC_WUP_FEEDSTYPE_PSV && (qfv.content.media.imgMode == G_IMG_GRID_MODE || qfv.content.media.imgMode == G_IMG_GRID_MODE_SMALL) && subcount(qfv.content.media.pic) > 1 && qz_metadata.orgdata.albumdata.iPicNum>9 ?>
						<a href="http://user.qzone.qq.com/<?cs var:qz_metadata.orgdata.uin ?>/photo/<?cs var:qz_metadata.orgdata.albumdata.sAlbumId ?>" target="_blank" class="img-num"><?cs var:qz_metadata.orgdata.albumdata.iPicNum?></a>
					<?cs /if ?>
					</div><?cs #/*必须先闭合 .f-ct-imgtxt*/?>
					<?cs if:subcount(qfv.extend) ?>
						<?cs if:qfv.extend.type == ALBUM_BABY ?>
							<div class="f-sp-act">
								<div class="img-box">
									<a href="<?cs call:ugc_as_html(qfv.extend.url,1,1) ?>" target="_blank" class=""><i class="icon-feed-kids-b"></i></a>
								</div>
								<div class="btn-box">
									<a href="<?cs call:ugc_as_html(qfv.extend.url,1,1) ?>" target="_blank" class="btn-gray"><span class="btn-inner">查看详情</span></a>
								</div>
								<div class="txt-box">
									<div>
										<span class="state">亲子相册</span> <a href="<?cs call:ugc_as_html(qfv.extend.url,1,1) ?>" class="f-name"  target="_blank"><?cs call:ugc_as_html(qfv.extend.albumName,1,1) ?></a>
									</div>
									<?cs if:string.length(qfv.extend.extendinfo) && string.length(qfv.extend.num) ?>
										<div>
											<span class="state">去看看我们家宝宝&nbsp;出生&nbsp;-&nbsp;<?cs var:qfv.extend.extendinfo ?>的<?cs var:qfv.extend.num ?>个瞬间吧</span>
										</div>
									<?cs /if ?>
								</div>
							</div>
						<?cs elif:qfv.extend.type == ALBUM_TRAVEL ?>
							<div class="f-sp-act">
								<div class="img-box">
									<a href="<?cs call:ugc_as_html(qfv.extend.url,1,1) ?>" target="_blank"><i class="icon-feed-travel-b"></i></a>
								</div>
								<div class="btn-box">
									<a href="<?cs call:ugc_as_html(qfv.extend.url,1,1) ?>" class="btn-gray" target="_blank"><span class="btn-inner">查看详情</span></a>
								</div>
								<div class="txt-box">
									<div>
										<span class="state">旅游相册</span> 
									</div>
									<div>
										<span class="state">去看看我在&nbsp; 
											<a href="<?cs call:ugc_as_html(qfv.extend.url,1,1) ?>" class="f-name" target="_blank"><?cs call:ugc_as_html(qfv.extend.albumName,1,1) ?></a>
										&nbsp;的更多精彩瞬间吧</span>
									</div>
								</div>
							</div>
					<?cs /if ?>
		
					<?cs /if ?>
				</div>

				<div class="f-op-wrap">
					<?cs call:v8_operate()?>
					<?cs call:v8_comments-like()?>
				</div>

			</div><?cs #end: .qz_summary?>
		</div><?cs #end: .f-item?>
	</div><?cs #end: .f-wrap?>
</li>

