<?cs ####
	/**
	 *生成视频预览图片
	 */
?>
<?cs def:_contentMedia_cover_preview(video)?>
	<img src="/ac/b.gif"
		 width="<?cs var:video.limitWidth?>"<?cs #防斗?>
		 onload="<?cs call:contentBox_adjustIamge_onLoad_encoded(video.screenshot,
								video.limitWidth,
								video.limitHeight)?>"
	/>
<?cs /def?>

<?cs ####
	/**
	 *生成跳转类型的图片
	 */?>

<?cs def:_contentMedia_video_item(video)?>
	<div class="img_box"
	<?cs #TODO 这几个属性还不知道是做什么的，遇到了再处理
		 url1=""
		 url2=""
		 url3=""
	 ?>
	>
		<?cs call:ugc_url_check(video.action.url,0)?>
		<a class="img_video" target="_blank" href="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>">
			<i class="ui_icon icon_video_sign"></i><?cs #这个是跳转走的视频，带一个角标表示是视频?>
			<?cs call:_contentMedia_cover_preview(video)?>
		</a>
	</div>
<?cs /def?>

<?cs ####
	/**
	 *生成跳转类型的图片
	 */?>

<?cs def:_contentMedia_video_show(video)?>
	<div class="img_box"
		 url1=""
		 url2=""
		 url3="<?cs var:html_encode(video.src, 1) ?>"
	>
		<?cs call:popup_start(video) ?>
			<?cs set:media_is_bigimg=0 ?>
			<span class="video_preview<?cs if:video.bigImage==1?>_big<?cs /if?><?cs if:video.pictype==3 ?> img_document<?cs /if?>">
				<?cs if:video.pictype == "img_doc"?>
					<i class="ui_ico icon_doc_sign"></i>
				<?cs else?>
					<span class="video_play_bt"></span>
				<?cs /if?>
				<img alt="视频缩略图" src="/ac/b.gif" 
					onload="QZFL.media.adjustImageSize(
						<?cs if:video.bigImage==1 ?>400,400<?cs else ?>120,120<?cs /if?>,
						restXHTML('<?cs call:htmlEncodeVar(video.screenshot,2,0) ?>'));" />
			</span>
		<?cs call:popup_end() ?>
	</div>
<?cs /def?>

<?cs ####
	/**
	 *左图右文型的新版视频feeds
	 */?>

<?cs def:_contentMedia_video_show_leftimg_v8(video)?>
	<div class="img_box bor2"
		 url1=""
		 url2=""
		 url3="<?cs var:html_encode(video.src, 1) ?>"
	>
	<?cs set:video.leftvideo = 1 ?>
		<?cs call:popup_start(video) ?>
				<img alt="视频缩略图" src="/ac/b.gif" width="120" height="120"
		 			onload="this.onload=null;this.src = '<?cs call:ugc_as_html(video.screenshot,1,1) ?>';" />
				<span class="video_play_bt"></span>
		<?cs call:popup_end() ?>
	</div>
<?cs /def?>
<?cs #:
	/**/
	function vedio(video){}
?>
<?cs def:video(video) ?>
	<?cs if: video.action.type == "url" ?>
		<?cs call:_contentMedia_video_item(video) ?>
	<?cs elif:video.action.type == "popup" ?>
		<?cs if:_g_cnt_layoutMode == G_LAYOUT_LEFTIMG_V8 ?>
			<?cs call:_contentMedia_video_show_leftimg_v8(video) ?>
		<?cs else ?>
			<?cs call:_contentMedia_video_show(video) ?>
		<?cs /if ?>
		
	<?cs /if ?>
<?cs /def ?>
