<?cs set:_title_after_nickname = 0?>
<?cs #if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_NEWCOMMENT?>
<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV ?>
	<?cs set:_title_after_nickname = 1?>
<?cs /if?>

<?cs #set:_txtimg_class=""?><?cs #TODO 只有左图才添加这个class?>
<?cs #if:(subcount(qfv.content.cntText.con) || subcount(qfv.content.cntText.title.con) || subcount(qfv.content.title)) &&
		(subcount(qfv.content.media.pic) || subcount(qfv.content.media.video)) ?>
	<?cs #set:_txtimg_class = "f-ct-imgtxt"?>
<?cs #/if?>

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
			<?cs if:!_title_after_nickname && g_blog_type != BLOG_FWD?><?cs #日志转载不需要title?>
				<?cs set:_len = subcount(qfv.title.con)?>
				<?cs if:_len > 0?>
					<div class="f-info">
							<?cs loop:i = 0, _len - 1, 1?>
							<?cs with:item = qfv.title.con[i]?>
								<?cs if:item.prop == "BLOG_TITLE"?>
									<?cs set:item.className = ""?>
									<?cs set:item.color = ""?>
								<?cs /if?>
								<?cs call:v8_title_item(item)?>
							<?cs /with?>
							<?cs /loop?>
					</div><?cs #end: .f-info?>
				<?cs /if?>
			<?cs /if?>

			<div class="qz_summary wupfeed" id="hex_<?cs call:v8_genFeedId() ?>">
				<?cs call:v8_echo_feed_data()?>

				<?cs #call:v8_quote()?><?cs #v8新版feeds已经不需要quote了?>

				<div class="f-ct <?cs if:g_blog_type == BLOG_FWD?>f-ct-b-bg<?cs /if ?>">
					<div class="f-ct-txtimg <?cs if:subcount(qfv.content.media.video) ?> f-ct-video <?cs /if?>
						<?cs if:qfv.content.media.imgMode == G_IMG_GRID_MODE && subcount(qfv.content.media.pic) > 1?> img-box-row-wrap<?cs /if?>"
					>
						<?cs set:g_view_txtimg = 1?><?cs #上文下图的被动，不用压缩?>
						<?cs call:v8_contentTxt("txt-big-size")?>
						<?cs call:v8_contentMedia()?>
						<?cs if:(qfv.content.media.imgMode == G_IMG_GRID_MODE || qfv.content.media.imgMode == G_IMG_GRID_MODE_SMALL) && qz_metadata.orgdata.itemcount >9 ?>
							<a href="http://user.qzone.qq.com/<?cs var:qz_metadata.orgdata.uin ?>/blog/<?cs var:qz_metadata.orgdata.mkey ?>" target="_blank" class="img-num"><?cs var:qz_metadata.orgdata.itemcount ?></a>
						<?cs /if ?>
					</div><?cs #/*必须先闭合 .f-ct-imgtxt*/?>
				</div>

				<div class="f-op-wrap">
					<?cs call:v8_operate()?>
					<?cs call:v8_comments-like()?>
				</div>

			</div><?cs #end: .qz_summary?>
		</div><?cs #end: .f-item?>
	</div><?cs #end: .f-wrap?>
</li>

