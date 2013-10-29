<?cs #星星组件 ?>
<?cs def:votestar(param) ?>
	<p class="f_votestar">
		<span class="votestar">
			<span class="votestar_i star_4" style="width:<?cs var:param.percent ?>%;"></span>
		</span>
		<span class="votescore">
			<span class="c_tx3 ui_mr10"><?cs var:param.score ?></span>
		</span>
	</p>
<?cs /def ?>

<?cs #V8星星组件 ?>
<?cs def:votestar_new(param) ?>
	<span><span class="votestar"><span class="votestar_i star_4" style="width:<?cs var:param.percent ?>%;"></span></span><?cs var:param.score ?></span></p>
<?cs /def ?>

<?cs #	V8添加应用的内容区 左图右文 ?>
<?cs def:app_content(appinfo) ?>
	<div class="f_ct_imgtxt  f_ct_appfeed ">
		<div class="img_box" richinfo="" url1="<?cs var:appinfo.app_img ?>">
			<a href="<?cs var:appinfo.app_url ?>" target="_blank">
				<img src="<?cs var:appinfo.app_img ?>" onload="QZFL.media.adjustImageSize(400,400,restXHTML('<?cs var:appinfo.app_img ?>'));" width="75">
			</a>
		</div>
		<div class="appfeed_info">
			<?cs call:content_genTitle(qfv.content.cntText.title.con)?>
			<p><?cs var:appinfo.desc ?></p>
			<?cs if:subcount(appinfo.star)?>
				<?cs call:votestar(appinfo.star) ?>
			<?cs /if?>
		</div>
	</div>
<?cs /def ?>

<?cs #添加应用的内容区 左图右文 ?>
<?cs def:app_content_new(appinfo) ?>
	<?cs if:subcount(appinfo) ?>
		<?cs set:_pic.src = appinfo.app_img ?>
	        <div class="img_box bor2">
	        	<a href="<?cs var:appinfo.app_url ?>" style="display:block;overflow:hidden;width:120px;height:120px;" target="_blank">
	        		<img src="/ac/b.gif" onload="<?cs escape:'html'?><?cs call:contentBox_ReduceImgByShortEdge_onLoad(_pic, 120, 120, '')?><?cs /escape:'html'?>"/>
	        	</a>
	        </div>
	        <div class="txt_box">
	        	<?cs call:content_genTitle(qfv.content.cntText.title.con)?>
	            <?cs call:votestar_new(appinfo.star) ?>
	            <a href="<?cs var:appinfo.app_url ?>" target="_blank" class=" c_tx2 hover_nd">
	            	<?cs var:appinfo.desc ?>
	            </a>
	        </div>
    <?cs /if?>
<?cs /def ?>

<?cs #:game组件  ?>
<?cs def:game() ?>
<div class="f_ct f_ct_game">
	<div class="f_ct_imgtxt controls_<?cs var:qfv.meta.itemid ?>">
		<div class="img_box img_game" >
			<?cs call:popup_start(qfv.content.tryplay.pic.0) ?>
				<img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,120,restXHTML('<?cs call:htmlEncodeVar(qfv.content.media.pic.0.src,2,0) ?>'));" />
				<i class="ui_icon icon_game_play"></i>
			<?cs call:popup_end() ?>
		</div>
		<?cs if:subcount(qfv.content.cntText.con) ||
			subcount(qfv.content.cntText.title.con)?>
			<?cs call:contentTxt_start("")?>
			<?cs call:content_genTitle(qfv.content.cntText.title.con)?>
			<?cs call:conCommon(qfv.content.cntText.con)?>
			<?cs call:contentTxt_end()?>
		<?cs /if?>
	</div>
	<div class="player_<?cs var:qfv.meta.itemid ?>" style="display:none;">
		<div class="video_unfold bor3" style="width: 400px; height: 350px; ">
			<div class="flash_<?cs var:qfv.meta.itemid ?>"></div>
			<?cs call:popup_start(qfv.content.tryplay.unfold) ?>↑<?cs call:popup_end() ?>
		</div>
		<p class="video_info"><?cs call:conCommon(qfv.content.cntText.con) ?></p>
	</div>
</div>
<?cs /def ?>

<?cs def:app_share_contentBox_start()?>
	<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_ACT ?>
		<div class="f_ct f_ct_2 bor3 bg3 f_ct_fixed">
		<?cs else ?>
		<div class="f_ct f_ct_2 bor3 bg3 f_ct_fixed_passive">
	<?cs /if ?>
	<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>
	<?cs call:contentBox_Title(qfv.content.title) ?>
	<div class="f_ct_imgtxt">
	<?cs set:g_f_ct_x_has_closed = 0?>
<?cs /def?>

<?cs def:app_share_contentBox_start_forText()?>
	<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_ACT ?>
		<div class="f_ct f_ct_2 bor3 bg3">
		<?cs else ?>
		<div class="f_ct f_ct_2 bor3 bg3">
	<?cs /if ?>
	<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>
	<?cs call:contentBox_Title(qfv.content.title) ?>
	<div class="f_ct_imgtxt">
	<?cs set:g_f_ct_x_has_closed = 0?>
<?cs /def?>

<?cs def:app_share_contentBox_end()?>
		</div><?cs #必须先闭合 .f_ct_imgtxt?>
	</div>
<?cs /def?>


<?cs call:title()?>

<?cs if:qfv.content.layoutMode == G_LAYOUT_LEFTIMG_V8 ?><?cs # V8版的左图右文?>

	<?cs call:summary_start()?>
		<?cs call:quote()?>
		<?cs if:qfv.content.media.imgMode == G_IMG_NOIMG ?>
			<?cs call:app_share_contentBox_start_forText()?>
				<?cs call:contentTxt_start("")?>
				<?cs call:content_genTitle(qfv.content.cntText.title.con)?>
				<?cs call:conCommon(qfv.content.cntText.con)?>
				<?cs call:contentTxt_end()?>
			<?cs call:app_share_contentBox_end()?>
		<?cs else ?>
			<?cs call:app_share_contentBox_start()?>
			<?cs if:subcount(qfv.content.appinfo) ?>
				<?cs call:app_content_new(qfv.content.appinfo) ?>
			<?cs else ?>
				<?cs call:contentMedia()?>
				<?cs call:contentTxt_start("")?>
				<?cs call:content_genTitle(qfv.content.cntText.title.con)?>
				<?cs call:conCommon(qfv.content.cntText.con)?>
				<?cs call:contentTxt_end()?>
			<?cs /if ?>
		<?cs /if ?>
		<?cs call:app_share_contentBox_end()?>
		<?cs call:operate()?>
		<?cs call:comments-like()?>
	<?cs call:summary_end()?>
	
<?cs else ?>

	<?cs call:summary_start()?>
		<?cs if:qfv.meta.feedstype==UC_WUP_FEEDSTYPE_PSV && qfv.meta.subtype==APP_subtype_mobile_cover?>
			<?cs call:contentBox_start("borbg", "")?>
		<?cs else?>
			<?cs call:contentBox_start("", "")?>
		<?cs /if?>
		<?cs if:subcount(qfv.content.appinfo) ?>
			<?cs call:app_content(qfv.content.appinfo)?>
		<?cs elif:subcount(qfv.content.tryplay) ?>
			<?cs call:game() ?>
		<?cs else ?>
			<?cs if:qfv.content.layoutMode == G_LAYOUT_LEFTIMG?><?cs #左图右文?>
				<?cs call:contentMedia()?>
				<?cs call:contentTxt("")?>
			<?cs else ?><?cs #普通模式，都是上文下图?>
				<?cs call:contentTxt("")?>
				<?cs call:contentMedia()?>
			<?cs /if?>
		<?cs /if ?>
		<?cs call:contentBox_end()?>
		<?cs call:operate()?>
		<?cs call:comments-like()?>
	<?cs call:summary_end()?>

<?cs /if ?>

