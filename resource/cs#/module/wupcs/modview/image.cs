<?cs def:imageFlag(flag) ?>
	<?cs if:flag == 2?><i class="ui_ico icon_gif_sign"></i><?cs /if ?>
	<?cs if:flag == "tryplay"?><i class="ui_icon icon_game_play"></i><?cs /if ?>
	<?cs if:flag == 4?><i class="ico-long-pic" title="长图"></i><?cs /if ?>
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
		<?cs call:ugc_url_check(pic.src,0) ?>
		trueSrc:'<?cs var:json_encode(ugc_url_check.ret, 1)?>',
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
		<?cs call:ugc_url_check(pic.src,0) ?>
		trueSrc:'<?cs var:json_encode(ugc_url_check.ret, 1)?>',
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
		<?cs call:ugc_url_check(pic.src,0) ?>
		trueSrc:'<?cs var:json_encode(ugc_url_check.ret, 1)?>',
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

		trueSrc:'<?cs var:json_encode(ugc_url_check.ret, 1)?>',
	<?cs call:ugc_url_check(_img_src_,0) ?>
	<?cs escape:'html'?>
	QZFL.media.adjustImageSize(
		'<?cs var:_width?>',
		'<?cs var:_height?>',
		'<?cs var:json_encode(ugc_url_check.ret, 1)?>')
	<?cs /escape:'html'?>
<?cs /def?>


<?cs #{简单居中短边压缩(展示区域为正方形)?>
<?cs def:v6_setShortEdgeResizeAlignCenter(pic, _width, _height, extend)?>
	<?cs if:pic.width < _width && pic.height<_height ?><?cs #图片小于外框 ?>
		<?cs set:_margin_top = (_height-pic.height)/2 ?>
		<?cs set:_margin_left = (_width-pic.width)/2 ?>
		<?cs set:pic.cssText = pic.cssText + "margin-left:" + _margin_left + "px;margin-top:" + _margin_top + "px;"?>
	<?cs elif:pic.width >= pic.height && pic.width >= _width ?><?cs #宽大于高，图片超出外框 ?>
		<?cs set:_margin_left = (pic.width * _height) / ( 2 * pic.height) - _width / 2?>
		<?cs if:_height > 0?>
		<?cs set:pic.cssText = pic.cssText + "margin-left:-" + _margin_left + "px;height:" + _height + "px;"?>
		<?cs /if?>
	<?cs else ?>
		<?cs if:pic.height >= _height && _width > 0?>
			<?cs set:_margin_top = (pic.height * _width) / ( 2 * pic.width) - _height / 2?>
			<?cs set:pic.cssText = "margin-top:-" + _margin_top + "px;width:" + _width + "px;"?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs #智能裁剪?>
<?cs #:智能裁剪逻辑解释 ?>
<?cs #:没有中心点就自己去中心点走智能裁剪逻辑，不再走 v6_setShortEdgeResizeAlignCenter 因为这个不支持区域为长方形的容器 ?>
<?cs def:v6_setSmartCenter(pic, _width, _height, extend)?>
	<?cs #:有中心点取中心点，没有中心点取图片的宽高二分之一处 ?>
	<?cs if:pic.centerpoint_y <= 0 || pic.centerpoint_x <= 0 ?>
		<?cs  set:pic.centerpoint_y = pic.height / 2 ?>
		<?cs  set:pic.centerpoint_x = pic.width / 2 ?>
	<?cs /if ?>

	<?cs if:pic.width * _height >= pic.height * _width ?>
		<?cs set:_w = pic.width * _height / pic.height  ?>
		<?cs set:_h = _height ?>
		<?cs set:_cx = pic.centerpoint_x * _height / pic.height  ?>
		<?cs set:_cy = pic.centerpoint_y * _height / pic.height  ?>
	<?cs else ?>
		<?cs set:_h = pic.height * _width / pic.width ?>
		<?cs set:_w = _width ?>
		<?cs set:_cx = pic.centerpoint_x * _width / pic.width  ?>
		<?cs set:_cy = pic.centerpoint_y * _width / pic.width  ?>
	<?cs /if ?>

	<?cs #:图片不变糊优化begin 如果图片大小与容器宽高相差无几（10px以内,但必须图片宽高比容器大），就直接取图片的宽高，这样不容易变糊 ?>
	<?cs if: pic.height - _h < 10 && pic.height - _h > 0 &&
			 pic.width  - _w < 10 && pic.width  - _w > 0 ?>
		<?cs set:_h = pic.height ?>
		<?cs set:_w = pic.width ?>
	<?cs /if ?>
	<?cs #:图片不变糊优化end ?>

	<?cs set:_a = _cy - _height / 2 ?>
	<?cs set:_b = _cx - _width / 2 ?>

	<?cs if:_a < 0 ?>
		<?cs set:_a = 0 ?>
	<?cs elif: _a > _h - _height ?>
		<?cs set:_a = _h - _height ?>
	<?cs /if ?>

	<?cs if:_b < 0 ?>
		<?cs set:_b = 0 ?>
	<?cs elif: _b > _w - _width ?>
		<?cs set:_b = _w - _width ?>
	<?cs /if ?>

	<?cs set:_margin_top = 0- #_a ?>
	<?cs set:_margin_left = 0 - #_b ?>


	<?cs set:pic.cssText = pic.cssText + "margin-left:" + _margin_left + "px;margin-top:" + _margin_top + "px;height:" + _h + "px;width:" + _w + "px;"?>
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
		<?cs set:pic.action.attrs=1 ?>
		<?cs set:pic.action.attrs.0.key="title" ?>
		<?cs set:pic.action.attrs.0.value= "点击跳转查看" ?>
		<?cs set:pic.action.attrs=pic.action.attrs+1 ?>
		<?cs if: pic.type==3 ?>
			<?cs set:pic.action.attrs[pic.action.attrs].key="class" ?>
			<?cs set:pic.action.attrs[pic.action.attrs].value= parentClassName+" img_document" ?>
			<?cs set:pic.action.attrs=pic.action.attrs+1 ?>
		<?cs elif:parentClassName ?>
			<?cs set:pic.action.attrs[pic.action.attrs].key="class" ?>
			<?cs set:pic.action.attrs[pic.action.attrs].value= parentClassName ?>
			<?cs set:pic.action.attrs=pic.action.attrs+1 ?>
		<?cs /if ?>
		<?cs if: parentCssText ?>
			<?cs set:pic.action.attrs[pic.action.attrs].key="style" ?>
			<?cs set:pic.action.attrs[pic.action.attrs].value= parentCssText ?>
		<?cs /if ?>
		<?cs call:con_url_start(pic.action)?>
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

