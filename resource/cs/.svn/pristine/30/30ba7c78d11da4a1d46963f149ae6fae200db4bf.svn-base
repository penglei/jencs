<?cs ####:
	/*
	 *生成代替转发链的那种titile
	 *relyMsg转发链中的一个节点
	 */
?>
<?cs def:v8_contentBox_Title(title) ?>
	<?cs if:subcount(title.con)>0 ?>
		<h4 class="txt-box-title">
			<?cs call:v8_conCommon(title.con) ?>
		</h4>
		<?cs #<div class="f-ang-t"></div>?>
	<?cs /if ?>
<?cs /def ?>

<?cs ####
	 /**
	  * 内容区文字标题title
	  */
?>
<?cs def:v8_content_genTitle(title) ?>
	<?cs if: subcount(title) ?>
		<h4 class="txt-box-title">
			<?cs call:v8_conCommon(title)?>
		</h4>
	<?cs /if ?>
<?cs /def?>

<?cs ####
	/**
	 * 内容区总标题
	 */
?>
<?cs def:v8_contentTxt_start(cls)?>
	<div class="txt-box <?cs alt:cls?><?cs /alt?>">
<?cs /def?>

<?cs def:v8_contentTxt_end()?>
	</div>
<?cs /def?>

<?cs def:v8_contentMoreTxtEntry()?>
	<?cs if: qfv.content.con_more > 0 ?>
		<div class="txt-box qz_info_complete none"></div>
		<div class="txt-box f-toggle">
			<a href="javascript:;" data-cmd="qz_toggle" data-complete="0" data-pos="2">
				展开查看全文
			</a>
			<img src="http://qzonestyle.gtimg.cn/qzone_v6/img/feed/loading.gif" class="load_img none">
		</div>
	<?cs /if ?>
<?cs /def?>

<?cs def:v8_contentBlogMoreTxt()?>
	<?cs if:qfv.extendinfo.con_more == 1?>
		<a href="<?cs var:qfv.extendinfo.blogurl ?>" target="_blank">查看全文</a>
	<?cs /if?>
<?cs /def?>

<?cs def:app_download()?>
	<?cs if:qfv.extendinfo.sourceplatform ?><?cs #指引下载安卓/苹果客户端?>
		<p class="fixed-bottom">
		<?cs if:qfv.extendinfo.sourceplatform == "Android" ?>
			<a data-cmd="download" data-src="<?cs var:qfv.extendinfo.sourceplatform?>" data-appid="<?cs var:qfv.meta.hostid?>" class="fixed-btn" href="javascript:;"><i class="ui-icon icon-android"></i>下载 Android版</a>
		<?cs else?>
			<a data-cmd="download" data-src="<?cs var:qfv.extendinfo.sourceplatform?>" data-appid="<?cs var:qfv.meta.hostid?>" class="fixed-btn" href="javascript:;"><i class="ui-icon icon-iphone"></i>下载 iPhone版</a>
		<?cs /if?>
		</p>
	<?cs /if?>
<?cs /def?>

<?cs def:v8_contentAppDetailBtn()?>
	<?cs if:appid==352 && qfv.meta.subtype==APP_subtype_game && string.length(qfv.extendinfo.achieveText)>0 &&  string.length(qfv.extendinfo.achieveTitle)>0 ?>
		<p class="fixed-bottom">
			<?cs call:ugc_url_check(qfv.extendinfo.url,0) ?>
			<a href="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" class="fixed-btn game-btn" target="_blank">
				<span class="game-option"><?cs call:ugc_as_html(qfv.extendinfo.achieveTitle,1,1) ?></span>
				<span class="game-result"><?cs call:ugc_as_html(qfv.extendinfo.achieveText,1,1) ?></span>
			</a>
		</p>
	<?cs /if?>
<?cs /def?>

<?cs def:v8_contentTxt(cls)?>
	<?cs #!必须判断三个条件?>
	<?cs if:subcount(qfv.content.cntText.con) ||
			subcount(qfv.content.cntText.title.con) ||
			subcount(qfv.content.title)?>
	<?cs #qfv.content.cntText.con是txt_box数据的容器?>
	<?cs #qfv.content.cntText.title.con是内容标题的容器?>
		<?cs call:v8_contentTxt_start(cls)?>
		<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>
		<?cs call:v8_contentBox_Title(qfv.content.title) ?>
		<?cs call:v8_content_genTitle(qfv.content.cntText.title.con)?>

		<?cs call:v8_conCommon(qfv.content.cntText.con)?>
		<?cs call:v8_contentBlogMoreTxt()?>
		<?cs call:app_download()?>
		<?cs call:v8_contentAppDetailBtn() ?>
		<?cs call:v8_contentTxt_end()?>
	<?cs /if?>

	<?cs # 这个逻辑只有说说用到，移到说说中去了?>
	<?cs #call:v8_contentMoreTxtEntry()?>
<?cs /def?>
