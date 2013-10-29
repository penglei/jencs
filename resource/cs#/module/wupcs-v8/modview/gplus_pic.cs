<?cs ####
	/**
	 * 计算单图高宽样式
	 */
?>
<?cs def:_cal_gplus_one_w_h(width,height)?>
	<?cs if:width>512 ?>
		<?cs if:height*512/width>384 ?>
			height:384px;
		<?cs else ?>
			width:512px;
		<?cs /if ?>
	<?cs else ?>
		<?cs if:height>384 ?>
			height:384px;
		<?cs else ?>
			width:<?cs var:width ?>px;
		<?cs /if ?>
	<?cs /if ?>
<?cs /def?>

<?cs def:v8__contentMedia_GPlus()?>
	<?cs call:v8_contentMedia_start_imgbox()?>

	<?cs #g+模式第一张是大图，在数据层api里控制?>
	<?cs with:pic = qfv.content.media.pic[0]?>
		<?cs if: pic.type == 2?>
			<?cs set:pic.action.className = " with-sign " ?>
		<?cs /if ?>

		<?cs if:pic.height >= 800 && pic.height/pic.width>=3 ?>
			<?cs #长图?>
			<?cs call:v8__contentMedia_display_pic_start(pic, "img-long-pic", "") ?>
			<?cs call:v8_imageFlag(4) ?>
		<?cs else ?>
			<?cs #普通图片?>
			<?cs call:v8__contentMedia_display_pic_start(pic, "", "")?>
			<?cs call:v8_imageFlag(pic.type) ?>
		<?cs /if ?>


		<?cs if:pic.width==0 && pic.height==0 ?>
			<?cs #后台没有返回图片尺寸数据 ?>
			<?cs call:ugc_url_check(pic.src,0) ?>
			<?cs if:string.find(pic.src,'gtimg.cn/qzone/em/')>0 ?><?cs #签到图片就没有必要走脚本压缩了 ?>
				<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>">
			<?cs else?>
				<img src="/ac/b.gif" 
					<?cs call:v8_echoClass(pic)?>
					<?cs call:v8_echoStyle(pic)?>
					onload="<?cs escape:'html'?>
						<?cs call:v8_contentBox_ReduceImgByLongEdge_onLoad(pic, 512, 512, "")?><?cs #最大宽高512，512?>
					<?cs /escape:'html'?>"
				/>
			<?cs /if ?>
		<?cs else ?>
			<?cs call:ugc_url_check(pic.src,0) ?>
			<?cs if:pic.width > pic.height ?>
				<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" style="<?cs call:_cal_gplus_one_w_h(pic.width,pic.height) ?>">
			<?cs else ?>
				<?cs #高大于宽 ?>
				<?cs if:pic.height >= 800 && pic.height/pic.width>=3 ?>
					<?cs #长图 ?>
					<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" style="width:<?cs if:pic.width>500 ?>512<?cs else ?><?cs var:pic.width ?><?cs /if ?>px;">
				<?cs else ?>
					<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" style="height:<?cs if:pic.height>384 ?>384<?cs else ?><?cs var:pic.height ?><?cs /if ?>px;">
				<?cs /if ?>
			<?cs /if ?>
		<?cs /if ?>
		<?cs call:v8__contentMedia_display_pic_end()?>
	<?cs /with?>
	<?cs #剩余的小图?>
	<?cs if:_media_pic_count > 1?>
	<?cs if: _media_pic_count > 7 ?>
		<?cs set:_media_pic_count = 7 ?>
	<?cs /if ?>
	<?cs set:_count = 0 ?><?cs #渲染图片计数?>
	<div class="img-box-preview">
		<?cs loop:i = 1, _media_pic_count, 1?>
		<?cs with:pic = qfv.content.media.pic[i]?>
		<?cs if: pic.action.type=="tips" ?>
			<?cs set:_param.type="url" ?>
			<?cs set:_param.url=qfv.content.media.pic[1].action.url ?>
			<?cs set:_param.className="img-box-count" ?>
			<?cs call:v8_con_url_start(_param) ?>
				<?cs var:pic.action.tips ?>
			<?cs call:v8_con_url_end()?>

		<?cs elif:_count < _media_pic_count ?>
			<?cs set:_count = _count + 1 ?>
			<?cs if: pic.type == 2 ?>
				<?cs set:pic.action.className=pic.action.className+" with-sign " ?>
			<?cs /if ?>
			<?cs call:v8_imageFlag(pic.type) ?>
			<?cs if:pic.width == 0 && pic.height == 0 ?>
			<?cs call:v8__contentMedia_display_pic_start(pic,"","display:inline-block;overflow:hidden;width:60px;height:45px;")?>
				<img src="/ac/b.gif"
					<?cs call:v8_echoClass(pic)?>
					<?cs call:v8_echoStyle(pic)?>
					 onload="<?cs escape:'html'?>
						<?cs call:v8_contentBox_ReduceImgByShortEdge_onLoad(pic, 60, 45, "")?>
						<?cs /escape:'html'?>"
				 />
			<?cs else ?>
				<?cs call:ugc_url_check(pic.src,0) ?>
				<?cs set:_param.widthfix=20 ?>
				<?cs call:setShortEdgeResizeAlignCenter(pic, 60, 45, _param)?>
				<?cs call:v8__contentMedia_display_pic_start(pic,"","")?>
				<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" 
				<?cs call:v8_echoClass(pic)?>
				<?cs call:v8_echoStyle(pic)?>
				<?cs #call:v8_echoTwoImageLastSize(pic)?>
				/>
			<?cs /if ?>


			<?cs call:v8__contentMedia_display_pic_end()?>
		<?cs /if ?>

		<?cs /with?>
		<?cs /loop?>

		<?cs #TODO,考虑通用性。?>
		<?cs #添加共有多少张图片的文字提示?>
		<?cs if:qfv.content.media.album.totalpic?>
			<?cs call:v8__contentMedia_display_pic_start(
					qfv.content.media.pic.1,
					"img-box-count", "")?>
				共<?cs var:qfv.content.media.album.totalpic?>张
			<?cs call:v8__contentMedia_display_pic_end()?>
		<?cs /if?>
	</div>
	<?cs /if?>
	<?cs call:v8_contentMedia_end_imgbox()?>
<?cs /def?>
