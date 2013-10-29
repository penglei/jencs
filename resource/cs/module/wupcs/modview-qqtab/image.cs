<?cs def:imageFlag(flag) ?>
	<?cs if:flag == 2?><i class="ui_ico icon_gif_sign"></i><?cs /if ?>
	<?cs if:flag == "img_doc"?><i class="ui_ico icon_doc_sign"></i><?cs /if ?>
	<?cs if:flag == "tryplay"?><i class="ui_icon icon_game_play"></i><?cs /if ?>
<?cs /def ?>
<?cs ####
	/**
	 *按长边压缩图片算法
	 *注意：cs不支持escape:'html'的嵌套使用
	 *@param {Integer} _width 最大宽度
	 *@param {Integer} _height 最大高度
	 *@param {Unknown} extend 预留接口参数
	 *@outputs 图片的onload算法
	 */
?>
<?cs def:contentBox_ReduceImgByLongEdge_onLoad(pic, _width, _height, extend)?>
QZFL.media.reduceImage(0,
	<?cs var:_width?>,
	<?cs var:_height?>,
	{
		trueSrc:'<?cs var:json_encode(pic.src, 1)?>',
		callback:function(img,type,ew,eh,o){
			var _h = Math.floor(o.oh/o.k),
				_w = Math.floor(o.ow/o.k);
			if(_w<=ew && _h<=eh){
				var p=img.parentNode;
				p.style.width=_w+'px';
				p.style.height=_h+'px';
			}
		}
	})
<?cs /def?>

<?cs ####
	/**
	 *按短边压缩图片算法
	 */
?>
<?cs def:contentBox_ReduceImgByShortEdge_onLoad(pic, _width, _height, extend)?>
QZFL.media.reduceImage(1,
	<?cs var:_width?>,
	<?cs var:_height?>,
	{
		trueSrc:'<?cs var:json_encode(pic.src, 1)?>',
		callback:function(img,type,ew,eh,o){
			var p=img.parentNode,
				_h = Math.floor(o.oh/o.k),
				_w = Math.floor(o.ow/o.k);
			if(_h<=eh){
				p.style.height=_h+'px';
			}else{
				img.style.marginTop=(eh-_h)/2+'px';
			}
			img.style.marginLeft=(ew-_w)/2+'px';
		}
	})
<?cs /def?>

<?cs ###
	/**
	 *图片按短边压缩后居中
	 */
?>
<?cs def:contentBox_ReduceImgByShortEdge_AlignCenter_onLoad(pic, _width, _height, extend)?>
QZFL.media.reduceImage(1,
	<?cs var:_width?>,
	<?cs var:_height?>,
	{
		trueSrc:'<?cs var:json_encode(pic.src, 1)?>',
		callback:function(img,type,ew,eh,o){
			var p=img.parentNode,
				_h = Math.floor(o.oh/o.k),
				_w = Math.floor(o.ow/o.k);
			img.style.marginTop=(eh-_h)/2+'px';
			img.style.marginLeft=(ew-_w)/2+'px';
		}
	})
<?cs /def?>

<?cs ####
	/**
	 *图片最大边限制，是一种简单的长边压缩
	 */
?>
<?cs def:contentBox_adjustIamge_onLoad_encoded(pic, _width, _height)?>

	<?cs if:subcount(pic)?><?cs #支持多数类型?>
		<?cs set:_img_src_ = pic.src?>
	<?cs else ?>
		<?cs set:_img_src_ = pic?>
	<?cs /if?>

	<?cs escape:'html'?>
	QZFL.media.adjustImageSize(
		'<?cs var:_width?>',
		'<?cs var:_height?>',
		'<?cs var:json_encode(_img_src_, 1)?>')
	<?cs /escape:'html'?>
<?cs /def?>

<?cs #{////图片外框?>

<?cs ####
	/**
	 *生成基本的contentbox img
	 */
?>
<?cs def:contentMedia_start_imgbox()?>
	<div class="img_box">
<?cs /def?>

<?cs def:contentMedia_custom_imgbox(className, cssText)?>
	<div class="img_box"
		<?cs if:className?> class="<?cs var:className?>"<?cs /if?>
		<?cs if:cssText?> style="<?cs var:cssText?>"<?cs /if?>
	>
<?cs /def?>

<?cs def:contentMedia_end_imgbox()?>
	</div>
<?cs /def?>

<?cs #} end:图片外框?>


<?cs ####
	/**
	 *展示一张图片的外框
	 */
?>
<?cs def:_contentMedia_display_pic_start(pic, parentClassName, parentCssText)?>
	<?cs if:pic.action.type == "popup"?>
		<?cs set:pic.action.className = pic.action.className + " " +parentClassName ?>
		<?cs set:pic.action.cssText = pic.action.cssText + parentCssText ?>
		<?cs call:popup_start(pic) ?>
	<?cs else ?>
		<a href="<?cs var:html_encode(string_firstwords_filter(pic.action.url,"javascript:"),1)?>" 
			target="_blank" 
			title="点击跳转查看"
			<?cs if:parentClassName?> class="<?cs var:parentClassName?> <?cs if:pic.type==3 ?> img_document<?cs /if?>"<?cs /if?>
			<?cs if:parentCssText?> style="<?cs var:parentCssText?>"<?cs /if?>
		>
	<?cs /if?>

	<?cs #TODO 添加图片角标?>
	<?cs #//因为该宏是生成a标签，可能被用来生成文字，因此在这里不能添加，得想个办法..?>
	<?cs #if:pic.isGif?>
		<?cs #<i class="ui_icon icon_gif_sign" />?>
	<?cs #/if?>
	<?cs set:_contentMedia_display_pic_start.ret = "popup" ?>
<?cs /def?>

<?cs def:_contentMedia_display_pic_end()?>
	<?cs if: _contentMedia_display_pic_start.ret=="popup" ?>
		<?cs call:popup_end() ?>
	<?cs else ?>
		</a>
	<?cs /if ?>
<?cs /def?>



<?cs ############?>



<?cs #{////图片展示方式实现?>

<?cs ####
	/**
	 *默认方式展示一张图片
	 */
?>
<?cs def:media_img(picItem)?>
	<?cs if:picItem.limitWidth?>
		<?cs set:_pic_limitWidth = picItem.limitWidth?>
	<?cs elif:qfv.content.media.imgMode==G_IMG_DEFAULT ?>
		<?cs set:_pic_limitWidth = 120?>
	<?cs else ?>
		<?cs set:_pic_limitWidth = 400?><?cs #最大宽度400吧，这个可以考虑改成参数可配置化?>
	<?cs /if?>

	<?cs if:picItem.limitHeight?>
		<?cs set:_pic_limitHeight = picItem.limitHeight?>
	<?cs elif:qfv.content.media.imgMode==G_IMG_DEFAULT ?>
		<?cs set:_pic_limitHeight = 120?>
	<?cs else ?>
		<?cs set:_pic_limitHeight = 300?><?cs #最大高度300吧，这个可以考虑改成参数可配置化?>
	<?cs /if?>
		<?cs call:imageFlag(picItem.type) ?>
		<img src="/ac/b.gif" 
			onload="<?cs call:contentBox_adjustIamge_onLoad_encoded(picItem, _pic_limitWidth, _pic_limitHeight)?>"
			<?cs if:picItem.className?> class="<?cs var:picItem.className?>"<?cs /if?>
			<?cs if:picItem.cssText?> style="<?cs var:picItem.cssText?>"<?cs /if?>
		/>
<?cs /def?>

<?cs ####
	/**
	 *默认图片节点生成方法
	 *包含压缩，动作信息的生成
	 */
?>
<?cs def:contentMediaPic_item(picItem)?>
	<?cs if: subcount(picItem.action)&&picItem.type==2 ?>
		<?cs if: string.length(picItem.action.className) ?>
			<?cs set:picItem.action.className=picItem.action.className+"img_gif" ?>
		<?cs else ?>
			<?cs set:picItem.action.className="img_gif" ?>
		<?cs /if ?>
	<?cs /if ?>
	<?cs if:picItem.action.type == "popup"?>
		<?cs call:popup_start(picItem)?>
		<?cs call:media_img(picItem)?>
		<?cs call:popup_end()?>
	<?cs elif:picItem.action.type == "url"?>
		<?cs call:con_url_start(picItem.action)?>
		<?cs call:media_img(picItem)?>
		<?cs call:con_url_end()?>
	<?cs else ?>
		<?cs call:media_img(picItem)?>
	<?cs /if?>
<?cs /def?>

<?cs #} end:图片默认展示方式?>

