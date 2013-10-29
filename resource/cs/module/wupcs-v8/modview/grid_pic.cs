
<?cs ####
	/**
	 * 计算单图高宽样式
	 */
?>
<?cs def:_cal_grid_one_w_h(width,height)?>
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

<?cs ####
	/**
	 * 单图宫格型
	 */
?>
<?cs def:v8__contentMedia_grid_one()?>
	<div class="img-box">
		<?cs #生成图片?>
		<?cs with:pic=qfv.content.media.pic.0?>
			<?cs if: pic.type == 2 || pic.type == 3?>
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
				<?cs elif:string.find(pic.src,'app/cover')>0 ?><?cs #手机cover的也给个小点的图?>
					<img src="/ac/b.gif" 
					<?cs call:v8_echoClass(pic)?>
					<?cs call:v8_echoStyle(pic)?>
					onload="<?cs escape:'html'?>
						<?cs call:v8_contentBox_ReduceImgByLongEdge_onLoad(pic, 384, 384, "")?><?cs #最大宽高384，384?>
						<?cs /escape:'html'?>"
					/>
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
				<?cs if:string.find(pic.src,'gtimg.cn/qzone/em/')>0 ?><?cs #签到图片就没有必要走脚本压缩了 ?>
					<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" <?cs if:pic.width>512 ?>style="width:512px;"<?cs /if ?>>
				<?cs elif:pic.width > pic.height ?>
					<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" style="<?cs call:_cal_grid_one_w_h(pic.width,pic.height) ?>">
				<?cs else ?>
					<?cs #高大于宽 ?>
					<?cs if:pic.height >= 800 && pic.height/pic.width>=3 ?>
						<?cs #长图 ?>
						<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" style="width:<?cs if:pic.width>512 ?>512<?cs else ?><?cs var:pic.width ?><?cs /if ?>px;">
					<?cs else ?>
						<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" style="height:<?cs if:pic.height>384 ?>384<?cs else ?><?cs var:pic.height ?><?cs /if ?>px;">
					<?cs /if ?>
				<?cs /if ?>
			<?cs /if ?>
			<?cs call:v8__contentMedia_display_pic_end()?>
		<?cs /with?>
	</div>
<?cs /def?>


<?cs ####
	/**
	 * 被动两张小图宫格型
	 */
?>
<?cs def:v8__contentMedia_grid_two_small()?>
	<?cs set:_imgbox_size_width = 100?>
	<?cs set:_imgbox_size_height = 75?>
	<?cs #TODO 图片压缩使用适配的图片大小?>
	<div class="img-box img-box-row">
		<?cs each:pic = qfv.content.media.pic?>
			<?cs if: pic.type == 2 || pic.type == 3 ?>
				<?cs set:pic.action.className=pic.action.className+" with-sign " ?>
			<?cs /if ?>
			<?cs call:v8__contentMedia_display_pic_start(pic, "img-three", "")?>
			<?cs call:v8_imageFlag(pic.type) ?>
			<img src="/ac/b.gif"
				<?cs call:v8_echoClass(pic)?>
				<?cs call:v8_echoStyle(pic)?>
				 onload="<?cs escape:'html'?>
					<?cs call:v8_contentBox_ReduceImgByShortEdge_onLoad(pic, _imgbox_size_width, _imgbox_size_height, "")?>
					<?cs /escape:'html'?>"
			 />
			<?cs call:v8__contentMedia_display_pic_end()?>
		<?cs /each?>
	</div>
<?cs /def?>

<?cs def:v8__contentMedia_grid_four_inline(start, end)?>
	<div class="img-box img-box-row">
		<?cs loop:_k = start, end, 1?>
			<?cs #不用判断，因为肯定有4张?>
			<?cs with:pic=qfv.content.media.pic[_k]?>
				<?cs if: pic.type == 2 || pic.type == 3?>
					<?cs set:pic.action.className = pic.action.className + " with-sign" ?>
				<?cs /if ?>

				<?cs call:v8__contentMedia_display_pic_start(pic, " img-two ", "")?>
				<?cs call:v8_imageFlag(pic.type) ?>
				<?cs if:pic.width == 0 && pic.height == 0 ?>
					<img src="/ac/b.gif"
						<?cs call:v8_echoClass(pic)?>
						<?cs call:v8_echoStyle(pic)?>
						 onload="<?cs escape:'html'?>
							<?cs call:v8_contentBox_ReduceImgByShortEdge_onLoad(pic, _imgbox_size_width, _imgbox_size_height, "")?><?cs #最大宽高都为254?>
							<?cs /escape:'html'?>"
					/>
				<?cs else ?>
					<?cs call:setSmartCenter(pic, _imgbox_size_width, _imgbox_size_height, "")?>
					<?cs call:ugc_url_check(pic.src,0) ?>
					<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" 
						<?cs call:v8_echoClass(pic)?>
						<?cs call:v8_echoStyle(pic)?>
						<?cs #call:v8_echoTwoImageLastSize(pic)?>
					/>
				<?cs /if ?>

				<?cs call:v8__contentMedia_display_pic_end()?>
			<?cs /with?>
		<?cs /loop?>
	</div>
<?cs /def?>

<?cs ####
	/**
	 * 四张宫格型
	 */
?>
<?cs def:v8__contentMedia_grid_four()?>

	<?cs if:g_v8_home ?>
		<?cs set:_imgbox_size_width = 276?>
		<?cs set:_imgbox_size_height = 276?>
	<?cs else ?>
		<?cs set:_imgbox_size_width = 254?>
		<?cs set:_imgbox_size_height = 190?>
	<?cs /if ?>

	<?cs call:v8__contentMedia_grid_four_inline(0,1)?>
	<?cs if:_media_pic_count > 2?>
		<?cs call:v8__contentMedia_grid_four_inline(2,3)?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * 四张宫格型
	 */
?>
<?cs def:v8__contentMedia_grid_four_small()?>


	<?cs set:_imgbox_size_width = 100?>
	<?cs set:_imgbox_size_height = 75?>
	<?cs call:v8__contentMedia_grid_four_inline(0,1)?>
	<?cs if:_media_pic_count > 2?>
		<?cs call:v8__contentMedia_grid_four_inline(2,3)?>
	<?cs /if?>
<?cs /def?>


<?cs def:v8__contentMedia_grid_nine_inline(start, end)?>
	<div class="img-box img-box-row">
		<?cs if:end > _media_pic_count - 1?>
			<?cs set:end = _media_pic_count - 1?>
		<?cs /if?>
		<?cs loop:_k = start, end, 1?>
			<?cs #不用判断，因为肯定有4张?>
			<?cs with:pic=qfv.content.media.pic[_k]?>
				<?cs if: pic.type == 2 || pic.type == 3?>
					<?cs set:pic.action.className = pic.action.className+" with-sign" ?>
				<?cs /if ?>

				<?cs call:v8__contentMedia_display_pic_start(pic, " img-three ", "")?>
				<?cs call:v8_imageFlag(pic.type) ?>
				<?cs if:pic.width == 0 && pic.height == 0 ?>
					<img src="/ac/b.gif"
						<?cs call:v8_echoClass(pic)?>
						<?cs call:v8_echoStyle(pic)?>
						 onload="<?cs escape:'html'?>
							<?cs call:v8_contentBox_ReduceImgByShortEdge_AlignCenter_onLoad(pic, _imgbox_size_width, _imgbox_size_height, "")?>
							<?cs /escape:'html'?>"
					/>
				<?cs else ?>
					<?cs call:setSmartCenter(pic, _imgbox_size_width, _imgbox_size_height, "")?>
					<?cs call:ugc_url_check(pic.src,0) ?>
					<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" 
						<?cs call:v8_echoClass(pic)?>
						<?cs call:v8_echoStyle(pic)?>
						<?cs #call:v8_echoThreeImageLastSize(pic)?>
					/>
				<?cs /if ?>
				<?cs call:v8__contentMedia_display_pic_end()?>
			<?cs /with?>
		<?cs /loop?>
	</div>
<?cs /def?>

<?cs ####
	/**
	 * 3、6、9张宫格型
	 */
?>
<?cs def:v8__contentMedia_grid_nine()?>
	<?cs if:g_v8_home ?>
		<?cs set:_imgbox_size_width = 182?>
		<?cs set:_imgbox_size_height = 182?>
	<?cs else ?>
		<?cs set:_imgbox_size_width = 168?>
		<?cs set:_imgbox_size_height = 125?>
	<?cs /if ?>
	<?cs call:v8__contentMedia_grid_nine_inline(0, 2)?>
	<?cs if:_media_pic_count > 3?>
		<?cs call:v8__contentMedia_grid_nine_inline(3, 5)?>
	<?cs /if?>
	<?cs if:_media_pic_count > 5?>
		<?cs call:v8__contentMedia_grid_nine_inline(6, 8)?>
	<?cs /if?>

	<?cs #TODO 图片数不是这样取的?>
	<?cs #if:subcount(qfv.content.media.pic) > 9 ?>
	<?cs #<span class="img-num">?><?cs #var:subcount(qfv.content.media.pic)?><?cs #</span>?>
	<?cs #/if?>
<?cs /def?>


<?cs ####
	/**
	 * 3、6、9张宫格型(被动feeds)
	 * 固定100大小，并且没有 num 文字标号
	 */
?>
<?cs def:v8__contentMedia_grid_nine_small()?>

	<?cs set:_imgbox_size_width = 100?>
	<?cs set:_imgbox_size_height = 75?>
	<?cs call:v8__contentMedia_grid_nine_inline(0, 2)?>
	<?cs if:_media_pic_count > 3?>
		<?cs call:v8__contentMedia_grid_nine_inline(3, 5)?>
	<?cs /if?>
	<?cs if:_media_pic_count > 5?>
		<?cs call:v8__contentMedia_grid_nine_inline(6, 8)?>
	<?cs /if?>
<?cs /def?>

