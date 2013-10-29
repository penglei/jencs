<?cs ####
	/**
	 *生成视频预览图片
	 */
?>
<?cs def:v8__contentMedia_cover_preview(video)?>
	<img src="/ac/b.gif"
		 width="<?cs var:video.limitWidth?>"<?cs #防斗?>
		 onload="<?cs call:v8_contentBox_adjustIamge_onLoad_encoded(video.screenshot, video.limitWidth, video.limitHeight)?>"
	/>
<?cs /def?>

<?cs ####
	/**
	 *生成跳转类型的图片
	 */?>

<?cs def:v8__contentMedia_video_item(video)?>
	<div class="img-box"
	<?cs #TODO 这几个属性还不知道是做什么的，遇到了再处理
		 url1=""
		 url2=""
		 url3=""
	 ?>
	>
		<?cs call:ugc_url_check(video.action.url,0)?>
		<a class="img-video" target="_blank" href="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>">
			<?cs call:v8__contentMedia_cover_preview(video)?>
			<i class="ui-icon f-play-video"></i><?cs #这个是跳转走的视频，带一个角标表示是视频?>
		</a>
	</div>
<?cs /def?>

<?cs def:v8__contentMedia_video_show(video)?>
	<div class="img-box" url3="<?cs var:html_encode(video.src, 1) ?>" >
		<?cs set:video.action.width = 500 ?>
		<?cs call:v8_popup_start(video) ?>
			<i class="ui-icon f-play-video"></i>
			<img alt="视频缩略图" src="/ac/b.gif" 
				<?cs if:video.bigImage != 1 ?>
					<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
						width="100" height="100"
					<?cs else ?>
						width="120" height="90"
					<?cs /if?>
				<?cs /if ?>

				onload="
				<?cs if:video.bigImage == 1 ?>
					QZFL.media.adjustImageSize(500, 500, restXHTML('<?cs var:html_encode(json_encode(video.screenshot, 1), 1) ?>'));
				<?cs else ?>
					this.onload=null;this.src = '<?cs call:ugc_as_html(video.screenshot,1,1) ?>'
				<?cs /if?>
				" />
		<?cs call:v8_popup_end() ?>
	</div>
<?cs /def?>

<?cs #:
	/**/
	function vedio(video){}
?>
<?cs def:v8_video(video) ?>
	<?cs if: video.action.type == "url" ?>
		<?cs call:v8__contentMedia_video_item(video) ?>
	<?cs elif:video.action.type == "popup" ?>
		<?cs call:v8__contentMedia_video_show(video) ?>
	<?cs /if ?>
<?cs /def ?>
