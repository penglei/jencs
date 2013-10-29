<?cs ####
	/**
	 *style分为三种情况:
	 * 1、f_ct_1 bor3			#bor
	 * 2、f_ct_2 bor3 bg3		#borbg
	 * 3、NULL
	 */
?>
<?cs def:summary_start()?>
<div class="qz_summary wupfeed" id="hex_<?cs call:genFeedId() ?>">
<?cs call:echo_feed_data()?>
<?cs /def?>

<?cs def:summary_end() ?>
</div>
<?cs /def?>


<?cs def:data-meta()?>
	<i class="none feed-metadata"
	 data-uin="<?cs var:qfv.meta.opuin ?>"
	 data-hostuin="<?cs var:qfv.meta.hostuin ?>"
	 data-commentUrl="<?cs var:qfv.comments.inputbox.action?>"
	 data-commentParam="<?cs call:ugc_as_html(qfv.comments.inputbox.param,1,1)?>"
	 data-commentTuin="<?cs var:qfv.comments.inputbox.tuin?>"
	 data-likeUnikey="<?cs var:html_encode(_like_unikey, 1)?>"
	 data-likeCurkey="<?cs var:html_encode(qz_metadata.qz_data.key1, 1)?>"<?cs #奇怪的东西..?>
	 data-isliked="<?cs var:qfv.like.isliked?>"
	 data-likecnt="<?cs var:_like_count?>"
	<?cs if:qz_metadata.metadata.appid != 4?>
	 data-tid="<?cs var:qfv.meta.hostid?>"
	<?cs else ?>
	 data-tid="<?cs var:qz_metadata.content_box.media.albumid ?>"
		<?cs if:subcount(qz_metadata.content_box.media.media.0) == 0 &&
			subcount(qz_metadata.content_box.media.media) > 0 ?>
		<?cs #:只有一张照片，当它就是照片feeds ?>
		 data-subid="<?cs var:qz_metadata.content_box.media.media.largeid ?>" 
		<?cs /if?>
	<?cs /if?>
	 data-origtid="<?cs var:qz_metadata.orgdata.mkey ?>" 
	 data-origuin="<?cs var:qz_metadata.orgdata.uin ?>" 
	 data-issignin="<?cs var:qfv.meta.issignin ?>" 
	 data-retweetcount="<?cs var:qfv.meta.fwdcount?>"
	 data-totweet="<?cs var:qfv.meta.totweet ?>" 
	 data-source="<?cs var:qfv.meta.moodSource ?>" 
	 data-topicid="<?cs var:qfv.meta.topicid ?>" 
	 data-feedstype="<?cs var:qfv.meta.feedoptype ?>" 
	 ></i><?cs #commentTuin并不完全就是feed前面的uin?>
<?cs /def?>


<?cs #:一个标准的summary解析流程?>
<?cs def:summary(style, cls) ?>
		<?cs call:quote()?>
		<?cs if:subcount(qfv.content.media.pic)?><?cs #有图片才展示?>
			<?cs call:contentBox-image(style, cls)?>
		<?cs else?>
			
			<?cs call:contentBox(style, cls)?>
		<?cs /if?>
		<div class="f_op_wrap">
			<?cs call:operate()?>
			<?cs call:likeinfo()?>
			<?cs call:comments()?>
		</div>
		<?cs call:data-meta()?>
<?cs /def?>

<?cs def:summaryOnlyMedia(style, cls)?>
	<?cs call:summary_start()?>
		<?cs call:quote()?>
		<?cs call:contentBox_start(style, cls)?>
			<?cs call:contentMedia()?>
		<?cs call:contentBox_end()?>
		<?cs call:operate()?>
		<?cs call:comments-like()?>
	<?cs call:summary_end()?>
<?cs /def?>

<?cs def:summaryOnlyTxt(style, cls)?>
	<?cs call:summary_start()?>
		<?cs call:quote()?>
		<?cs call:contentBox_start(style, cls)?>
			<?cs call:contentTxt("")?>
		<?cs call:contentBox_end()?>
		<div class="f_op_wrap">
			<?cs call:operate()?>
			<?cs call:comments-like()?>
		</div>
	<?cs call:summary_end()?>
<?cs /def?>
