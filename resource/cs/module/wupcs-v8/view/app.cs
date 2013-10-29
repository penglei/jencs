<?cs #星星组件 ?>
<?cs def:v8_votestar(param) ?>
	<p class="source spacing">
		<span class="votestar">
			<span class="votestar-i star-4" style="width:<?cs var:param.percent ?>%;"></span>
		</span>
		<span class="ui-ml10"><?cs var:param.score ?></span>
	</p>
<?cs /def ?>

<?cs #V8星星组件 ?>
<?cs def:v8_votestar_new(param) ?>
	<span><span class="votestar"><span class="votestar-i star-4" style="width:<?cs var:param.percent ?>%;"></span></span><?cs var:param.score ?></span></p>
<?cs /def ?>

<?cs #添加应用的内容区 左图右文 ?>
<?cs def:v8_app_content(appinfo) ?>
	<div class="img-box" url1="<?cs var:appinfo.app_img ?>">
		<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_ACT ?>
			<?cs set:_size='120' ?>
		<?cs else ?>
			<?cs set:_size='100' ?>
		<?cs /if ?>
		<a href="<?cs var:appinfo.app_url ?>" target="_blank">
			<?cs set:_pic.src = appinfo.app_img ?>
			<img src="/ac/b.gif" onload="<?cs escape:'html'?>
							<?cs call:v8_contentBox_ReduceImgByShortEdge_onLoad(_pic, _size, _size, '')?>
							<?cs /escape:'html'?>"/>
		</a>
	</div>
	<div class="txt-box">
		<p class="info"><span class="ui-b"><?cs call:v8_content_genTitle(qfv.content.cntText.title.con)?></span></p>
		<?cs call:v8_votestar(appinfo.star) ?>
		<p class="info">
			<a href="<?cs var:appinfo.app_url ?>" target="_blank" class=" c-tx2 hover-nd"><?cs var:appinfo.desc ?></a>
		</p>
		<?cs call:app_download()?>
	</div>
<?cs /def ?>

<?cs set:_title_after_nickname = 0?>
<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
	<?cs set:_title_after_nickname = 1?>
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
			<?cs if:_title_after_nickname == 0?>
			<div class="f-info">
				<?cs set:_end = subcount(qfv.title.con) - 1?>
				<?cs loop:i = 0, _end, 1?>
					<?cs call:v8_title_item(qfv.title.con[i])?>
				<?cs /loop?>
			</div>
			<?cs /if?>

			<div class="qz_summary wupfeed" id="hex_<?cs call:v8_genFeedId() ?>">
				<?cs call:v8_echo_feed_data()?>

				<?cs if:qfv.content.media.imgMode == G_IMG_NOIMG ?>
				<div class="f-ct f-ct-b-bg">
					<div class="f-ct-txtimg">
						<?cs call:v8_contentTxt_start("")?>
						<?cs call:v8_content_genTitle(qfv.content.cntText.title.con)?>
						<?cs call:v8_conCommon(qfv.content.cntText.con)?>
						<?cs call:v8_contentTxt_end()?>
				<?cs else ?>
				<div class="f-ct f-ct-b-bg<?cs if:qfv.meta.feedstype != UC_WUP_FEEDSTYPE_ACT ?> f-ct-webpage-passive<?cs /if ?>">
					<div class="f-ct-txtimg f-ct-imgtxt <?cs if:qfv.meta.subtype != APP_subtype_mobile_cover?> f-ct-fixed<?cs /if ?>">
					<?cs if:subcount(qfv.content.appinfo) ?>
						<?cs call:v8_app_content(qfv.content.appinfo)?>
					<?cs elif:qfv.meta.subtype == APP_subtype_mobile_cover?><?cs #手机cover更换，要长的像说说?>
						<?cs call:v8_contentTxt("")?>
						<?cs call:v8__contentMedia_grid_one()?>
					<?cs else ?>
						<?cs call:v8_contentMedia()?>
						<?cs call:v8_contentTxt("")?>
					<?cs /if ?>
				<?cs /if ?>
					</div><?cs #必须先闭合 .f-ct-txtimg?>
				</div><?cs #必须先闭合 .f-ct?>
				<div class="f-op-wrap">
					<?cs call:v8_operate()?>
					<?cs call:v8_comments-like()?>
					<?cs call:v8_genMergeHtml(qz_metadata.meta)?>
				</div>
			</div><?cs #end: .qz_summary?>
		</div><?cs #end: .f-item?>
	</div><?cs #end: .f-wrap?>
</li>
