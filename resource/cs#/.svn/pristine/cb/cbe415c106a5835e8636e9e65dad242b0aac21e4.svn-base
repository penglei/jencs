<?cs #:来自组件,用于内容区内 ?>
<?cs def:iconSource(item) ?>
<div class="f_websiteicon">
	<?cs if:item.who ?>
		<span class='c_tx3'>来自</span><?cs call:userLink(item, '') ?>
	<?cs elif:item.url ?>
		<span class='ui_mr10 c_tx3'>来自<a href="<?cs var:item.url ?>" target="_blank" class="c_tx"><?cs var:item.name ?></a></span>
	<?cs else ?>
		<span class='ui_mr10 c_tx3'>来自<?cs var:item.name ?></span>
	<?cs /if ?>
</div>
<?cs /def ?>

<?cs #: ===================================================================================================== ?>
<?cs #:
	/**
	 * 图片对象的描述
	 * @object media
	 * @param {Object} media 包含图片信息的object
	 * @param {String} media.mydef_onload 自定义的图片onload事件方法体，如果不设置则用默认的
	 * @param {String} media.mydef_widthLimit 图片展示区域的最大宽度
	 * @param {String} media.mydef_heightLimit 图片展示区域的最大高度
	 * @param {String} media.mydef_imgurl 图片真实的url，用在onload事件中的（有些图片是通过回填给img节点）
	 * @param {String} media.mydef_imgsrc 图片url用来填到img节点的src（填src专用，因为如果使用onload来回填节点的话，mydef_imgurl和mydef_imgsrc可能出现不同）
	 * @param {String} media.newWidth 图片的实际宽度
	 * @param {String} media.newHeight 图片的实际高度
	 */
	media = {};
?>

<?cs #:
	/**
	 * 很简单的方法，输出一个img节点，空出各种信息设置，参数中有的就填上，没有的就算了
	 * 这里不给参数设默认值，使用此方法的人不可以偷懒，希望见到这个方法的时候，我们就能知道img的src是什么
	 * @param  {String} src     图片src 
	 * @param  {[type]} width   设置图片的宽
	 * @param  {[type]} height  设置图片的高
	 * @param  {String} sty     给图片节点设置的style值
	 * @param  {String} cls     给图片节点的class值
	 * @param  {String} onload  图片节点的onload事件方法体
	 * @param  {String} onerror 图片节点的onerror事件方法体
	 * @param  {String} extend  可以给图片加上一些你希望有的信息
	 * @return undefined
	 */
	function m_printImageElement(src,width,height,sty,cls,onload,onerror,extend){}
?>
<?cs def:m_printImageElement(src,width,height,sty,cls,onload,onerror,extend) ?>
	<img src="<?cs call:htmlEncodeVar(src,2,0) ?>" 
	<?cs if:width ?>width="<?cs var:width ?>" <?cs /if ?>
	<?cs if:height ?>height="<?cs var:height ?>" <?cs /if ?>
	<?cs if:sty ?>style="<?cs var:sty ?>" <?cs /if ?>
	<?cs if:cls ?>class="<?cs var:cls ?>" <?cs /if ?>
	<?cs if:onload ?>onload="<?cs var:onload ?>" <?cs /if ?>
	<?cs if:onerror ?>onerror="<?cs var:onerror ?>" <?cs /if ?>
	<?cs if:extend ?><?cs var:extend ?><?cs /if ?> />
<?cs /def ?>


<?cs #: =================================没有高宽信息时的逻辑=============================== ?>

<?cs #:
	/**
	 * 为图片展示打上一些标志
	 * @param  {Number|String} phototype 图片类型的标志
	 * @return undefined
	 */
	function imageFlag(phototype){}
?>
<?cs def:imageFlag(phototype) ?>
	<?cs if:phototype == 2?><i class="ui_ico icon_gif_sign"></i><?cs /if ?>
<?cs /def ?>

<?cs #:
	/**
	 * 用js对图片进行短边压缩
	 * @param {Object} media 包含图片信息的object
	 * @param {String} media.mydef_onload 自定义的图片onload事件方法体，如果不设置则用默认的
	 * @param {String} media.mydef_widthLimit 图片展示区域的最大宽度
	 * @param {String} media.mydef_heightLimit 图片展示区域的最大高度
	 * @param {String} media.mydef_imgurl 图片真实的url，用在onload事件中的（有些图片是通过回填给img节点）
	 * @param {String} media.mydef_imgsrc 图片url用来填到img节点的src（填src专用，因为如果使用onload来回填节点的话，mydef_imgurl和mydef_imgsrc可能出现不同）
	 */
	function ReduceImageByShortEdge(media){}
?>
<?cs def:ReduceImageByShortEdge(media) ?>
	<?cs if:string.length(media.mydef_onload) ?>
		<?cs set:ReduceImageByShortEdge_Res.mydef_ShortEdge_onload=media.mydef_onload ?>
	<?cs else ?>
		<?cs call:htmlEncodeVar(media.mydef_imgurl,2,1) ?>
		<?cs set:ReduceImageByShortEdge_Res.mydef_ShortEdge_onload="QZFL.media.reduceImage(1,"+media.mydef_widthLimit+","+media.mydef_heightLimit+
		",{trueSrc:restXHTML('" + htmlEncodeVar_Res + "'),callback:function(img,type,ew,eh,o){var p=img.parentNode,_h = Math.floor(o.oh/o.k),_w = Math.floor(o.ow/o.k);if(_h<=eh){p.style.height=_h+'px';}else{img.style.marginTop=(eh-_h)/2+'px';}img.style.marginLeft=(ew-_w)/2+'px';}})" ?>
	<?cs /if ?>
	<?cs if: string.length(media.mydef_imgsrc)>0 ?>
		<?cs set:ReduceImageByShortEdge_Res.mydef_imgsrc=media.mydef_imgsrc ?>
	<?cs else ?>
		<?cs set:ReduceImageByShortEdge_Res.mydef_imgsrc='/ac/b.gif' ?>
	<?cs /if ?>
	<?cs call:imageFlag(media.phototype) ?>
	<?cs call:m_printImageElement(ReduceImageByShortEdge_Res.mydef_imgsrc,'','',"","",ReduceImageByShortEdge_Res.mydef_ShortEdge_onload,"","") ?>
<?cs /def ?>

<?cs #:
	/**
	 * 用js对图片进行长边压缩
	 * @param {Object} media 包含图片信息的object
	 * @param {String} media.mydef_onload 自定义的图片onload事件方法体，如果不设置则用默认的
	 * @param {String} media.mydef_widthLimit 图片展示区域的最大宽度
	 * @param {String} media.mydef_heightLimit 图片展示区域的最大高度
	 * @param {String} media.mydef_imgurl 图片真实的url，用在onload事件中的（有些图片是通过回填给img节点）
	 * @param {String} media.mydef_imgsrc 图片url用来填到img节点的src（填src专用，因为如果使用onload来回填节点的话，mydef_imgurl和mydef_imgsrc可能出现不同）
	 */
	function ReduceImageByLongEdge(media){}
?>
<?cs def:ReduceImageByLongEdge(media) ?>
	<?cs if: string.length(media.mydef_onload) ?>
		<?cs set:ReduceImageByLongEdge_Res.mydef_LongEdge_onload=media.mydef_onload ?>
	<?cs else ?>
		<?cs call:htmlEncodeVar(media.mydef_imgurl,2,1) ?>
		<?cs set:ReduceImageByLongEdge_Res.mydef_LongEdge_onload="QZFL.media.reduceImage(0,"+media.mydef_widthLimit+","+media.mydef_heightLimit+
		",{trueSrc:restXHTML('"+htmlEncodeVar_Res+"'),callback:function(img,type,ew,eh,o){var _h = Math.floor(o.oh/o.k),_w = Math.floor(o.ow/o.k);if(_w<=ew && _h<=eh){var p=img.parentNode;p.style.width=_w+'px';p.style.height=_h+'px';}}})" ?>
	<?cs /if ?>
	<?cs if: string.length(media.mydef_imgsrc)>0 ?>
		<?cs set:ReduceImageByLongEdge_Res.mydef_imgsrc=media.mydef_imgsrc ?>
	<?cs else ?>
		<?cs set:ReduceImageByLongEdge_Res.mydef_imgsrc='/ac/b.gif' ?>
	<?cs /if ?>
	<?cs call:imageFlag(media.phototype) ?>
	<?cs call:m_printImageElement(ReduceImageByLongEdge_Res.mydef_imgsrc,'','',"","",ReduceImageByLongEdge_Res.mydef_LongEdge_onload,"","") ?>
<?cs /def ?>
<?cs #: =================================没有高宽信息时的逻辑 END=============================== ?>


<?cs #: =================================有高宽信息时的逻辑 ==================================== ?>

<?cs #:
	/**
	 * 计算压缩信息的方法,计算结果存放在getReduceInfo_Res中
	 * getReduceInfo_Res.onload
	 * getReduceInfo_Res.long_pro
	 * getReduceInfo_Res.short_pro
	 * @param  {Obkect} media 包含图片信息的object
	 * @param  {String} media.mydef_widthLimit 图片展示区域的最大宽度
	 * @param  {String} media.mydef_heightLimit 图片展示区域的最大高度
	 * @param  {String} media.newWidth 图片的实际宽度
	 * @param  {String} media.newHeight 图片的实际高度
	 * @return {Object} getReduceInfo_Res
	 */
	function getReduceInfo(media){}
?>
<?cs def:getReduceInfo(media) ?>
	<?cs set:media.mydef_widthLimit=media.mydef_widthLimit-0 ?>
	<?cs set:media.mydef_heightLimit=media.mydef_heightLimit-0 ?>
	<?cs set:media.newWidth=media.newWidth-0 ?>
	<?cs set:media.newHeight=media.newHeight-0 ?>

	<?cs if:media.newWidth>=media.newHeight ?>
		<?cs #:宽为长边 ?>
		<?cs if:media.newWidth>=media.mydef_widthLimit ?>
			<?cs set:getReduceInfo_Res.long_pro = 'width="'+media.mydef_widthLimit+'"' ?>
		<?cs else ?>
			<?cs set:getReduceInfo_Res.long_pro = 'width="'+media.newWidth+'"' ?>
		<?cs /if ?>
		<?cs if:media.newHeight>=media.mydef_heightLimit ?>
			<?cs set:getReduceInfo_Res.short_pro = 'height="'+media.mydef_heightLimit+'"' ?>
		<?cs else ?>
			<?cs set:getReduceInfo_Res.short_pro = 'height="'+media.newHeight+'"' ?>
		<?cs /if ?>
	<?cs else ?>
		<?cs #:高为长边 ?>
		<?cs if:media.newHeight>=media.mydef_heightLimit ?>
			<?cs set:getReduceInfo_Res.long_pro = 'height="'+media.mydef_heightLimit+'"' ?>
		<?cs else ?>
			<?cs set:getReduceInfo_Res.long_pro = 'height="'+media.newHeight+'"' ?>
		<?cs /if ?>
		<?cs if:media.newWidth>=media.mydef_widthLimit ?>
			<?cs set:getReduceInfo_Res.short_pro = 'width="'+media.mydef_widthLimit+'"' ?>
		<?cs else ?>
			<?cs set:getReduceInfo_Res.short_pro = 'width="'+media.newWidth+'"' ?>
		<?cs /if ?>
	<?cs /if ?>
	<?cs set:getReduceInfo_Res.onload = "var p=this.parentNode;if(this.width<="+media.mydef_widthLimit+"){p.style.width=this.width;}else{this.style.marginLeft=(("+media.mydef_widthLimit+"-this.width)/2)+'px';}if(this.height<="+media.mydef_widthLimit+"){p.style.height=this.height;}else{this.style.marginTop=(("+media.mydef_heightLimit+"-this.height)/2)+'px';}" ?>
<?cs /def ?>

<?cs #:@info 在模版中按短边压缩图片的逻辑 ?>
<?cs #:@param [object] media 包含图片信息的object ?>
<?cs #:@param [number] media.mydef_imgurl 用来做为img节点的src的值 ?> 
<?cs #:@param [extend] media参数对象的其他属性依赖请看{@link:getReduceInfo} ?>
<?cs #:
	/**
	 * 在模版中按短边压缩图片的逻辑
	 * @param  {Object} media 包含图片信息的object
	 * @param  {String} media.mydef_widthLimit 图片展示区域的最大宽度
	 * @param  {String} media.mydef_heightLimit 图片展示区域的最大高度
	 * @param  {String} media.newWidth 图片的实际宽度
	 * @param  {String} media.newHeight 图片的实际高度
	 * @return undefined
	 */
	function PreReduceImageByShortEdge(media){}
?>
<?cs def:PreReduceImageByShortEdge(media) ?>
	<?cs call:getReduceInfo(media) ?>
	<?cs if: media.mydef_onload ?>
		<?cs set:getReduceInfo_Res.onload=media.mydef_onload ?>
	<?cs /if ?>
	<?cs call:m_printImageElement(media.mydef_imgurl,'','',"","",getReduceInfo_Res.onload,"",getReduceInfo_Res.short_pro) ?>
<?cs /def ?>

<?cs #:@info 在模版中按短边压缩图片的逻辑 ?>
<?cs #:@param [object] media 包含图片信息的object ?>
<?cs #:@param [number] media.mydef_imgurl 用来做为img节点的src的值 ?> 
<?cs #:@param [extend] media参数对象的其他属性依赖请看{link:getReduceInfo} ?>
<?cs #:
	/**
	 * 在模版中按短边压缩图片的逻辑
	 * @param {Object} media media 包含图片信息的object
	 * @param  {String} media.mydef_widthLimit 图片展示区域的最大宽度
	 * @param  {String} media.mydef_heightLimit 图片展示区域的最大高度
	 * @param  {String} media.newWidth 图片的实际宽度
	 * @param  {String} media.newHeight 图片的实际高度
	 * @return undefined
	 */
	function PreReduceImageByLongEdge(media){}
?>
<?cs def:PreReduceImageByLongEdge(media) ?>
	<?cs call:getReduceInfo(media) ?>
	<?cs if: media.mydef_onload ?>
		<?cs set:getReduceInfo_Res.onload=media.mydef_onload ?>
	<?cs /if ?>
	<?cs call:m_printImageElement(media.mydef_imgurl,'','',"","",getReduceInfo_Res.onload,"",getReduceInfo_Res.long_pro) ?>
<?cs /def ?>



<?cs #: =================================有高宽信息时的逻辑 END==================================== ?>


<?cs #:
	/**
	 * 选择mydef_imgurl
	 * @param  {[type]} media [description]
	 * @return {[type]}       [description]
	 */
	function selectImgUrl(media){}
?>
<?cs def:selectImgUrl(media) ?>
	<?cs if:media.picmarkflag==0 || !media.picmarksrc?>
		<?cs set:media.mydef_imgurl=media.src ?>
	<?cs else ?>
		<?cs set:media.mydef_imgurl=media.picmarksrc ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:构造一个reduceImage类型且具有fullscreenDialog弹层功能的img ?>
<?cs #:！！！！请注意关注这个方法对你特性的兼容性：qz:popup中写个stype是为了让首屏样式不乱，若有人在其它地方调用了这个方法,请注意关注这个style对你特性的兼容性： ?>
<?cs #:mode==1表示是用一张大图的分支，mode!=1是有多张图的 ?>

<?cs #:
	/**
	 * [popupReduceImageV2 description]
	 * @param  {[type]} media      [description]
	 * @param  {[type]} parentNode [description]
	 * @param  {[type]} mode       [description]
	 * @return {[type]}            [description]
	 */
	function popupReduceImageV2(media,parentNode,mode){}
?>
<?cs def:popupReduceImageV2(media,parentNode,mode) ?>
	<?cs #:这里蛋疼了，之前做相册的G+效果的时候没有顾及到popup由业务控制，后续得干掉else分支，兼容成以前的数据 ?>
	<?cs if:media.qz_popup.classNames ?>
		<?cs set:cls=media.qz_popup.classNames+' '+cls ?>
	<?cs /if ?>
	<?cs if:media.qz_popup.cssTexts ?>
		<?cs set:cst=media.qz_popup.cssTexts+cst ?>
	<?cs /if ?>

	<?cs if:subcount(media.qz_popup) > 0 ?>
		<a 
			data-cmd="qz_popup"  
			href="javascript:void(0)" 
			data-version="<?cs var:media.qz_popup.version ?>" 
			data-width="<?cs var:media.qz_popup.width ?>" 
			data-height="<?cs var:media.qz_popup.height ?>" 
			data-title="<?cs var:media.qz_popup.title ?>" 
			data-class="<?cs var:parentNode.cls ?> img_gif" 
			style="<?cs var:parentNode.sty ?>" 
			data-src="<?cs var:media.qz_popup.src ?>" 
			data-config="<?cs var:media.qz_popup.config ?>" 
			data-env="<?cs var:media.qz_popup.env ?>" 
			data-param="<?cs var:media.qz_popup.param ?>"
			class="<?cs var:parentNode.cls ?>  img_gif" >
	<?cs else ?>
		<a 
			data-cmd="qz_popup" 
			href="javascript:void(0)" 
			data-version="2" 
			data-width="<?cs var:media.width ?>" 
			data-height="<?cs var:media.height ?>" 
			title="" 
			class="<?cs var:parentNode.cls ?> img_gif" 
			style="<?cs var:parentNode.sty ?>" 
			data-src="/qzone/photo/zone/icenter_popup.html" 
			data-config="<?cs var:parentNode.config ?>" 
			data-param="<?cs var:qz_metadata.metadata.uin ?>|<?cs var:qz_metadata.content_box.media.albumid ?>|<?cs var:media.largeid ?>|<?cs var:media.src ?>">
	<?cs /if ?>
		<?cs if:media.newWidth || media.newHeight ?>
			<?cs #:有这两个东西就是有带了宽高信息，在模版里搞定展显逻辑 ?>
			<?cs if:mode==1 ?>
				<?cs call:PreReduceImageByLongEdge(media) ?>
			<?cs else ?>
				<?cs call:PreReduceImageByShortEdge(media) ?>
			<?cs /if ?>
		<?cs else ?>
			<?cs if:mode==1 ?>
				<?cs call:ReduceImageByLongEdge(media) ?>
			<?cs else ?>
				<?cs call:ReduceImageByShortEdge(media) ?>
			<?cs /if ?>
		<?cs /if ?>
	</a>
<?cs /def ?>

<?cs def:actionRedirectImageV2(media, parentNode,mode,tinyPic) ?>
	<a href="<?cs var:media.action ?>" class="<?cs alt:parentNode.cls ?><?cs /alt ?>" style="<?cs alt:parentNode.sty ?><?cs /alt ?>" target="_blank">
		<?cs #:mode==1表示是用一张大图的分支，mode!=1是有多张图的 ?>

		<?cs if:media.newWidth || media.newHeight ?>
			<?cs #:有这两个东西就是有带了宽高信息，在模版里搞定展显逻辑 ?>
			<?cs if:mode==1 ?>
				<?cs call:PreReduceImageByLongEdge(media) ?>
			<?cs else ?>
				<?cs call:PreReduceImageByShortEdge(media) ?>
				<?cs if:tinyPic==1 ?>
				<?cs if:media.vedioid?>
					<i class="ui_icon icon_video_sign_s"></i>
				<?cs /if?>
			<?cs /if?>
			<?cs /if ?>
		<?cs else ?>
			<?cs if:mode==1 ?>
				<?cs call:ReduceImageByLongEdge(media) ?>
			<?cs else ?>
				<?cs call:ReduceImageByShortEdge(media) ?>
				<?cs if:tinyPic==1 ?>
				<?cs if:media.vedioid?>
					<i class="ui_icon icon_video_sign_s"></i>
				<?cs /if?>
			<?cs /if?>
			<?cs /if ?>
		<?cs /if ?>
	</a>
<?cs /def ?>

<?cs def:reduceActionRouterV2(media,parentNode,mode,tinyPic) ?>
	<?cs if:media.picmarkflag==0 || !media.picmarksrc?>
		<?cs set:media.mydef_imgurl=media.src ?>
	<?cs else ?>
		<?cs set:media.mydef_imgurl=media.picmarksrc ?>
	<?cs /if ?>
	<?cs if:string.length(media.action) > 0 ?>
		<?cs call:actionRedirectImageV2(media, parentNode,mode,tinyPic) ?>
	<?cs else ?>
		<?cs call:popupReduceImageV2(media,parentNode,mode) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:G+那种体验的图片查看 ?>
<?cs def:show_GPuls(medias) ?>
	<?cs if: medias.media.src ?>
		<?cs set:mydef_picCount=1 ?>
	<?cs else ?>
		<?cs set:mydef_picCount=subcount(medias.media) ?>
	<?cs /if ?>
	<div class="img_box">
	<?cs if:medias.media.src ?>
		<?cs #:只有一张图片 ?>
		<?cs set:mydef.mediaParent.cls="img_box_bigshow" ?>
		<?cs set:mydef.mediaParent.sty="width:400px;height:300px;overflow:hidden;" ?>
		<?cs set:mydef.mediaParent.width=400 ?>
		<?cs set:mydef.mediaParent.height=300 ?>
		<?cs set:medias.media.mydef_widthLimit=400 ?>
		<?cs set:medias.media.mydef_heightLimit=300 ?>
		<?cs call:selectImgUrl(medias.media) ?>
		<?cs call:reduceActionRouterV2(medias.media,mydef.mediaParent,1,0) ?>
		<?cs if:medias.albumtotalpic>0 ?>
			<p class="f_reprint c_tx3"><span class="ui_mr10">共有<?cs var:medias.albumtotalpic ?>张图片</span></p>
		<?cs /if ?>
	<?cs else ?>
		<?cs set:mydef.mediaParent.cls="img_box_bigshow" ?>
		<?cs set:mydef.mediaParent.sty="width:400px;height:300px;overflow:hidden;" ?>
		<?cs set:mydef.mediaParent.width=400 ?>
		<?cs set:mydef.mediaParent.height=300 ?>
		<?cs set:medias.media.0.mydef_widthLimit=400 ?>
		<?cs set:medias.media.0.mydef_heightLimit=300 ?>
		<?cs call:selectImgUrl(medias.media.0) ?>
		<?cs call:reduceActionRouterV2(medias.media.0,mydef.mediaParent,1,0) ?>
		<div class="img_box_preview">
			<?cs if:subcount(medias.media)-1>6 ?>
				<?cs set:len=6 ?>
			<?cs else ?>
				<?cs set:len=subcount(medias.media)-1 ?>
			<?cs /if ?>
			<?cs loop:i =1, len, 1 ?>
				<?cs set:mydef.mediaParent.cls="bg2" ?>
				<?cs set:mydef.mediaParent.sty="width:60px;height:45px;overflow:hidden;" ?>
				<?cs set:mydef.mediaParent.width=60 ?>
				<?cs set:mydef.mediaParent.height=45 ?>
				<?cs set:medias.media[i].mydef_widthLimit=60 ?>
				<?cs set:medias.media[i].mydef_heightLimit=45 ?>
				<?cs call:selectImgUrl(medias.media[i]) ?>
				<?cs call:reduceActionRouterV2(medias.media[i],mydef.mediaParent,2,1) ?>
			<?cs /loop ?>
			<?cs if:subcount(qz_metadata.content_box.mediatips) > 0?>
				<a class="img_box_count c_tx" href="<?cs alt:medias.media[1].action?>javascript:;<?cs /alt?>" target="_blank"><?cs var:qz_metadata.content_box.mediatips.text ?></a>
			<?cs /if?>
			<?cs if:mydef_picCount>1 && medias.albumtotalpic>1 ?>
				<?cs if:subcount(medias.media[1].qz_popup) > 0 ?>
					<a 
						data-cmd="qz_popup" 
						href="javascript:void(0)" 
						data-version="<?cs var:medias.media[1].qz_popup.version ?>" 
						data-width="<?cs var:medias.media[1].qz_popup.width ?>" 
						data-height="<?cs var:medias.media[1].qz_popup.height ?>" 
						title="<?cs var:medias.media[1].qz_popup.title ?>" 
						class="img_box_count" 
						style="<?cs var:medias.media[1].qz_popup.cssTexts ?>" 
						data-src="<?cs var:medias.media[1].qz_popup.src ?>" 
						data-config="<?cs var:medias.media[1].qz_popup.config ?>" 
						data-env="<?cs var:medias.media[1].qz_popup.env ?>" 
						data-param="<?cs var:medias.media[1].qz_popup.param ?>" 
					>
				<?cs else ?>
					<a 
						data-cmd="qz_popup" 
						href="javascript:void(0)" 
						data-version="2" 
						class="img_box_count" 
						data-src="/qzone/photo/zone/icenter_popup.html" 
						data-config="<?cs var:medias.media[1].config ?>" 
						data-param="<?cs var:qz_metadata.metadata.uin ?>|
								<?cs var:qz_metadata.content_box.media.albumid ?>|
								<?cs var:medias.media[1].largeid ?>|
								<?cs call:htmlEncodeVar(medias.media[1].src, 2, 0) ?>" 
					>
				<?cs /if ?>
						共<?cs var:medias.albumtotalpic ?>张
				</a>
			<?cs /if ?>
		</div>
	<?cs /if ?>
	</div>
<?cs /def ?>

<?cs #:
	/**/
	function nineGridImage(media){}
?>
<?cs def:nineGridImage(medias) ?>
	<?cs set:mydef_inx = 1 ?>
	<?cs each:item=medias.media ?>
		<?cs set:mydef.mediaParent.cls="img_box_120x120 ui_mr8 bg2" ?>
		<?cs set:mydef.mediaParent.sty="width:128px;height:128px;overflow:hidden;display:inline-block;" ?>
		<?cs set:mydef.mediaParent.width=128 ?>
		<?cs set:mydef.mediaParent.height=128 ?>
		<?cs set:item.mydef_widthLimit=128 ?>
		<?cs set:item.mydef_heightLimit=128 ?>
		<?cs call:selectImgUrl(item) ?>
		<?cs call:htmlEncodeVar(item.mydef_imgurl,2,1) ?>
		<?cs set:item.mydef_onload="QZFL.media.reduceImage(1,"+item.mydef_widthLimit+","+item.mydef_heightLimit+
		",{trueSrc:restXHTML('" + htmlEncodeVar_Res + "'),callback:function(img,type,ew,eh,o){var p=img.parentNode,_h = Math.floor(o.oh/o.k),_w = Math.floor(o.ow/o.k);img.style.marginTop=(eh-_h)/2+'px';img.style.marginLeft=(ew-_w)/2+'px';}})" ?>
		<?cs if:mydef_inx == 1 ?>
			<div class="img_box" style="margin-top:4px;margin-bottom:4px;float:none;width:408px;">
		<?cs /if ?>
		<?cs call:reduceActionRouterV2(item,mydef.mediaParent,2,0) ?>
		<?cs if:mydef_inx == 3 ?>
			</div>
		<?cs /if ?>
		<?cs set:mydef_inx = mydef_inx+1 ?>
		<?cs if:mydef_inx ==4 ?>
			<?cs set:mydef_inx = 1 ?>
		<?cs /if ?>
	<?cs /each ?>
	<?cs if:mydef_inx !=1 ?>
		</div>
	<?cs /if ?>
<?cs /def ?>

<?cs #:G+那种体验的图片查看 ?>
<?cs def:photoViewWithSmartImage(medias,mod) ?>
	<?cs if: medias.media.src ?>
		<?cs set:mydef_picCount=1 ?>
	<?cs else ?>
		<?cs set:mydef_picCount=subcount(medias.media) ?>
	<?cs /if ?>
	<?cs if:medias.newtype==2 || mydef_picCount == 1?>
		<?cs call:show_GPuls(medias) ?>
	<?cs elif:mydef_picCount==2 ?>
		<?cs #:只有两张图片 ?>
		<?cs set:mydef.mediaParent.cls="img_box_200x200 ui_mr8" ?>
		<?cs set:mydef.mediaParent.sty="width:196px;height:196px;overflow:hidden;" ?>
		<?cs set:mydef.mediaParent.width=196 ?>
		<?cs set:mydef.mediaParent.height=196 ?>
		<div class="img_box" style="margin-top:4px;margin-bottom:4px;float:none;width:408px;">
			<?cs each:item=medias.media ?>
				<?cs set:item.mydef_widthLimit=196 ?>
				<?cs set:item.mydef_heightLimit=196 ?>
				<?cs call:selectImgUrl(item) ?>
				<?cs call:reduceActionRouterV2(item,mydef.mediaParent,2,0) ?>
			<?cs /each ?>
		</div>
	<?cs elif:mydef_picCount==3 ?>
		<?cs call:nineGridImage(medias) ?>
	<?cs elif:mydef_picCount>3 && medias.newtype == 1 ?>
		<?cs call:show_GPuls(medias) ?>
	<?cs elif:mydef_picCount==4 ?>
		<?cs set:mydef.mediaParent.cls="img_box_200x200 ui_mr8 bg2" ?>
		<?cs set:mydef.mediaParent.sty="width:196px;height:196px;overflow:hidden;" ?>
		<?cs set:mydef.mediaParent.width=196 ?>
		<?cs set:mydef.mediaParent.height=196 ?>
		<?cs set:mydef_inx = 1 ?>
		<?cs each:item=medias.media ?>
			<?cs if:mydef_inx == 1 ?>
				<div class="img_box" style="margin-top:4px;margin-bottom:4px;float:none;width:408px;">
			<?cs /if ?>
			<?cs set:item.mydef_widthLimit=196 ?>
			<?cs set:item.mydef_heightLimit=196 ?>
			<?cs call:selectImgUrl(item) ?>
			<?cs call:htmlEncodeVar(item.mydef_imgurl,2,1) ?>
			<?cs set:item.mydef_onload="QZFL.media.reduceImage(1,"+item.mydef_widthLimit+","+
			item.mydef_heightLimit + ",{trueSrc:restXHTML('" + item.mydef_imgurl + "'),callback:function(img,type,ew,eh,o){var p=img.parentNode,_h = Math.floor(o.oh/o.k),_w = Math.floor(o.ow/o.k);img.style.marginTop=(eh-_h)/2+'px';img.style.marginLeft=(ew-_w)/2+'px';}})" ?>
			<?cs call:reduceActionRouterV2(item,mydef.mediaParent,2,0) ?>
			<?cs if:mydef_inx == 2 ?>
				</div>
			<?cs /if ?>
			<?cs set:mydef_inx = mydef_inx+1 ?>
			<?cs if:mydef_inx ==3 ?>
				<?cs set:mydef_inx = 1 ?>
			<?cs /if ?>
		<?cs /each ?>
		<?cs if:mydef_inx !=1 ?>
			</div>
		<?cs /if ?>
	<?cs elif:mydef_picCount>=5 ?>
		<?cs call:nineGridImage(medias) ?>
	<?cs /if ?>
<?cs /def ?>


<?cs def:contentBoxCommon-img(mod, isShowOnePic) ?>
	<?cs if:qz_metadata.content_box.media || 
			qz_metadata.content_box.media.media.src || 
			qz_metadata.content_box.media.media.type || 
			qz_metadata.content_box.media.media.0.src || 
			qz_metadata.content_box.media.media.0.avatar ?>
		<?cs if:qz_metadata.content_box.media.newtype ?>
			<?cs call:photoViewWithSmartImage(qz_metadata.content_box.media,mod) ?>
		<?cs else ?>
			<div class="img_box"
				 richinfo="<?cs var:qz_metadata.content_box.media.media.qz_popup.param ?>" 
				 url1="<?cs call:htmlEncodeVar(qz_metadata.content_box.media.media.src,2,0) ?>" 
				 url2="<?cs call:htmlEncodeVar(qz_metadata.content_box.media.media.src_big,2,0) ?>" 
				 url3="<?cs call:htmlEncodeVar(qz_metadata.content_box.media.media.src_org,2,0) ?>"
			>
				<?cs #:多张图而且弹出框的情况 ?>
				<?cs if:qz_metadata.content_box.media.media.0.src  && subcount(qz_metadata.content_box.media.media.0.qz_popup) > 0 ?>
					<?cs if:isShowOnePic ?>
						<a 
							data-cmd="qz_popup" 
							href="javascript:void(0)" 
							data-version="<?cs var:qz_metadata.content_box.media.media.0.qz_popup.version ?>" 
							data-width="<?cs var:qz_metadata.content_box.media.media.0.qz_popup.width ?>" 
							data-height="<?cs var:qz_metadata.content_box.media.media.0.qz_popup.height ?>" 
							title="<?cs var:qz_metadata.content_box.media.media.0.qz_popup.title ?>" 
							data-src="<?cs var:qz_metadata.content_box.media.media.0.qz_popup.src ?>" 
							data-config="<?cs var:qz_metadata.content_box.media.media.0.qz_popup.config ?>" 
							class="img_gif" 
							data-env="<?cs var:qz_metadata.content_box.media.media.0.qz_popup.env ?>" 
							data-param="<?cs var:qz_metadata.content_box.media.media.0.qz_popup.param ?>" >
								<?cs call:imageFlag(qz_metadata.content_box.media.media.0.phototype) ?><img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,120,restXHTML('<?cs call:htmlEncodeVar(qz_metadata.content_box.media.media.0.src,2,0) ?>'));" />
						</a>
					<?cs else ?>
					<?cs each:item = qz_metadata.content_box.media.media ?>
						<a 
							data-cmd="qz_popup" 
							href="javascript:void(0)" 
							data-version="<?cs var:item.qz_popup.version ?>" 
							data-width="<?cs var:item.qz_popup.width ?>" 
							data-height="<?cs var:item.qz_popup.height ?>" 
							title="<?cs var:item.qz_popup.title ?>" 
							data-src="<?cs var:item.qz_popup.src ?>" 
							data-config="<?cs var:item.qz_popup.config ?>" 
							class="img_gif" 
							data-env="<?cs var:item.qz_popup.env ?>" 
							data-param="<?cs var:item.qz_popup.param ?>" >
								<?cs call:imageFlag(item.phototype) ?><img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,120,restXHTML('<?cs call:htmlEncodeVar(item.src,2,0) ?>'));" />
						</a>
					<?cs /each ?>
					<?cs /if ?>
				<?cs #:多张且是app的情况 ?>
				<?cs elif: qz_metadata.content_box.media.media.0.src && qz_metadata.content_box.media.media.0.aid ?>
					<?cs each:item = qz_metadata.content_box.media.media ?>
						<a class="appfeed_icon" target="_blank" href="http://rc.qzone.qq.com/myhome/<?cs var:item.aid?>">
							<img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(50,50,restXHTML('<?cs call:htmlEncodeVar(item.src,2,0) ?>'));"/>
						</a>
					<?cs /each ?>
				<?cs #:多张且是链接跳走的情况?>
				<?cs elif:qz_metadata.content_box.media.media.0.action ?>
					<?cs each:item = qz_metadata.content_box.media.media ?>
						<a href="<?cs var:item.action ?>" class="img_gif" target="_blank">
						<?cs if:item.avatar ?>
							<?cs call:imageFlag(item.phototype) ?><img src="/ac/b.gif" class="q_namecard" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" onload="QZFL.media.adjustImageSize(120,120,restXHTML('<?cs call:htmlEncodeVar(item.avatar,2,0) ?>'));" />
						<?cs else ?>
							<?cs call:imageFlag(item.phototype) ?><img src="<?cs call:htmlEncodeVar(item.src,2,0) ?>" />
						<?cs /if ?>
						</a>
					<?cs /each ?>
				<?cs elif:qz_metadata.content_box.media.media.src ||qz_metadata.content_box.media.media.type ?>
					<?cs #:一张的情况 ?>
					<?cs if:qz_metadata.content_box.media.media.type == 'video' ?>
						<?cs with:media = qz_metadata.content_box.media.media?>
						<a 
							data-cmd="qz_popup" 
							href="javascript:void(0)" 
							data-height="<?cs var:media.qz_popup.height ?>" 
							data-src="<?cs var:media.qz_popup.src ?>" 
							data-width="<?cs var:media.qz_popup.width ?>" 
							data-param="<?cs var:media.qz_popup.param ?>" 
							data-env="<?cs var:media.qz_popup.env ?>" 
							data-version="<?cs var:media.qz_popup.version ?>"
							>
							<?cs set: media_is_bigimg = 0?>
							<?cs if:media.preview == 'big' || media.isbimg == 1 ?><?cs set: media_is_bigimg = 1?><?cs /if?>
							<span class="video_preview<?cs if:media_is_bigimg?>_big<?cs /if?>">
								<span class="video_play_bt">　</span>
								<img alt="视频缩略图" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(<?cs if:media_is_bigimg ?>400,400<?cs else ?>120,120<?cs /if?>,restXHTML('<?cs call:htmlEncodeVar(media.src,2,0) ?>'));" />
							</span>
						</a>
						<?cs /with ?>
					<?cs elif:qz_metadata.content_box.media.media.type == 'music' ?>
						<div class="f_ct_music controls_<?cs var:qz_metadata.content_box.media.media.itemid?>">
							<a 
								data-cmd="qz_popup" 
								href="javascript:void(0)" 
								data-param="<?cs var:qz_metadata.content_box.media.media.qz_popup.param ?>" 
								data-version="4" 
								data-src="/music/qzone/music_ic.js" 
								data-needContainer="1" 
								data-charset="utf-8" 
								data-width="375" 
								data-height="169" 
								data-type="Music">
							<span class="music_playbt bor2">
								<span class="music_playbt_i bor2"><b class="trig c_tx"></b></span>
							</span>
							</a>
							<span class="music_playtime c_tx3"><?cs var:qz_metadata.content_box.media.media.musictime ?></span>
						</div>
						<div class="player_<?cs var:qz_metadata.content_box.media.media.itemid?>" style="display:none;">
							<a 
								data-cmd="qz_popup" 
								href="javascript:void(0)" 
								data-param="{action:4<?cs var:string.slice(qz_metadata.content_box.media.media.qz_popup.param,9, string.length(qz_metadata.content_box.media.media.qz_popup.param)) ?>"  
								data-src="/music/qzone/music_ic.js" 
								data-needContainer="1" 
								data-version="4" 
								data-charset="utf-8" 
								data-width="375" 
								data-height="169" 
								data-type="Music">收起</a>
							<div class="flash_<?cs var:qz_metadata.content_box.media.media.itemid?>"></div>
						</div>
					<?cs elif:qz_metadata.content_box.media.media.type == 'script' ?>
						<?cs with: media = qz_metadata.content_box.media.media?>
						<?cs if:media.biz == "mall" ?>
							<a class="c_tx ui_mr10" href="javascript:;" onclick="QZONE.ICFeeds.Interface.checkForDress2(this,'<?cs var:media.param[0]?>', '<?cs var:media.param[1] ?>')">
							<img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(430,430,restXHTML('<?cs call:htmlEncodeVar(media.src,2,0) ?>'));"/>
						</a>
					<?cs /if ?>
					<?cs /with?>
				<?cs else ?>
					<?cs if:subcount(qz_metadata.content_box.media.media.qz_popup) > 0 ?>
						<a 
							data-cmd="qz_popup" 
							href="javascript:void(0)" 
							data-version="<?cs var:qz_metadata.content_box.media.media.qz_popup.version ?>" 
							data-width="<?cs var:qz_metadata.content_box.media.media.qz_popup.width ?>" 
							data-height="<?cs var:qz_metadata.content_box.media.media.qz_popup.height ?>" 
							title="<?cs var:qz_metadata.content_box.media.media.qz_popup.title ?>" 
							data-src="<?cs var:qz_metadata.content_box.media.media.qz_popup.src ?>" 
							data-config="<?cs var:qz_metadata.content_box.media.media.qz_popup.config ?>" 
							class="img_gif" 
							data-env="<?cs var:qz_metadata.content_box.media.media.qz_popup.env ?>" 
							data-param="<?cs var:qz_metadata.content_box.media.media.qz_popup.param ?>" >
									<?cs call:imageFlag(qz_metadata.content_box.media.media.phototype) ?><img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,120,restXHTML('<?cs call:htmlEncodeVar(qz_metadata.content_box.media.media.src,2,0) ?>'));" />
						</a>
					<?cs elif:qz_metadata.content_box.media.media.action ?>
						<?cs #:链接跳走的情况 ?>
						<?cs with:media = qz_metadata.content_box.media.media?>
							<?cs if:media.vedioid?>
								<a class="img_video" target="_blank" href="<?cs var:media.action ?>">
									<i class="ui_icon icon_video_sign"></i>
									<img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,120,restXHTML('<?cs call:htmlEncodeVar(media.src,2,0) ?>'))" />
								</a>
							<?cs #:邀请我一起看 ?>
							<?cs elif:qz_metadata.feedtitle.content.con.0=='邀请我一起看' ?>
								<a class="img_box_width100" href="<?cs var:media.action ?>" target="_blank">
									<img src="<?cs call:htmlEncodeVar(media.src,2,0) ?>"/>
								</a>
							<?cs else ?>
								<a href="<?cs var:media.action ?>" target="_blank">
								<?cs if:media.avatar ?>
										<img src="/ac/b.gif" class="q_namecard" link="nameCard_<?cs var:media.uin ?> des_<?cs var:media.uin ?>" onload="QZFL.media.adjustImageSize(50,50,restXHTML('<?cs call:htmlEncodeVar(media.avatar,2,0) ?>'));" />
									<?cs else ?>
										<img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(<?cs alt:media.width?>400<?cs /alt?>,<?cs alt:media.height?>400<?cs /alt ?>,restXHTML('<?cs call:htmlEncodeVar(media.src,2,0) ?>'));" />
								<?cs /if ?>
								</a>
							<?cs /if?>
						<?cs /with ?>
					<?cs elif:qz_metadata.content_box.media.media.aid ?>
						<?cs #:空间内应用跳转的情况 /*deprecated*/ 2012 中 可删除?>
						<?cs with:appMedia = qz_metadata.content_box.media.media?>
						<a target="_blank" href="http://rc.qzone.qq.com/myhome/<?cs var:appMedia.aid?>">
							<img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(75,75,restXHTML('<?cs call:htmlEncodeVar(appMedia.src,2,0) ?>'));"/>
						</a>
						<?cs /with ?>
					<?cs elif:qz_metadata.content_box.media.media.src_org ?>
							<a href="javascript:;" onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs call:htmlEncodeVar(qz_metadata.content_box.media.media.src_big,2,0) ?>', '<?cs call:htmlEncodeVar(qz_metadata.content_box.media.media.src_ori,2,0) ?>', 374, 0, 0);return false;"  class="<?cs if:qz_metadata.content_box.media.media.phototype == "2" ?> img_gif<?cs /if ?>"><?cs call:imageFlag(qz_metadata.content_box.media.media.phototype) ?><img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(200,300,restXHTML('<?cs call:htmlEncodeVar(qz_metadata.content_box.media.media.src,2,0) ?>'));"/></a>
						<?cs else ?>
							<img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(410,430,restXHTML('<?cs call:htmlEncodeVar(qz_metadata.content_box.media.media.src,2,0) ?>'));" />
						<?cs /if ?>
					<?cs /if ?>

				<?cs /if ?>
				<?cs #cs要是好用一点，在这里加一个hook多好...! 生日礼物要加一个特殊的图片 ?>
				<?cs if:subcount(qz_metadata.content_box.media.extend.birthday) > 0?>
					<?cs with:popup=qz_metadata.content_box.media.extend.birthday.qz_popup?>
					<a 
						data-cmd="qz_popup" 
						href="javascript:void(0)" 
						title="<?cs var:popup.title ?>" 
						data-width="<?cs var:popup.width ?>" 
						data-height="<?cs var:popup.height ?>" 
						data-version="<?cs var:popup.version ?>" 
						data-src="<?cs var:popup.src ?>" 
						data-param="<?cs var:popup.param ?>" >
						<span href="javascript:;" title="<?cs var:popup.title ?>" class="btn_gift"></span>
					</a>
					<?cs /with ?>
				<?cs /if?>
			</div>
		<?cs /if ?>
	<?cs /if ?>
<?cs /def ?>


	<?cs def:contentSubTitleItem(item) ?>
		<?cs if:item.type == "url"?>
			<a href="<?cs var:item.url ?>" class="c_tx" target="_blank"><?cs var:item.text ?></a>
		<?cs elif:item.type == "text"?>
			<?cs var:item.text ?>
		<?cs elif:item.type == "nick" ?>
			<?cs call:userLink(item,"") ?>
		<?cs elif:item.type == "qz_app" ?>
			<a class="c_tx" target="_blank" href="http://rc.qzone.qq.com/myhome/<?cs var:item.aid?>">
				<?cs var:item.text ?>
			</a>
		<?cs elif:item.type == 'script' ?>
			<?cs if:item.biz == "mall"?>
				<a class="c_tx" href="javascript:;" onclick="QZONE.ICFeeds.Interface.checkForDress2(this,'<?cs var:item.param[0]?>', '<?cs var:item.param[1] ?>')">
					<?cs var:item.text ?>
				</a>
			<?cs /if ?>
	<?cs else ?>
		<?cs var:item ?>
		<?cs /if ?>
	<?cs /def ?>

<?cs def:contentBoxCommon-txt(mod, isVideo) ?>
<?cs #isVideo:是否视频类 isMargin:是否加边距 ?>
	<?cs if:qz_metadata.content_box.detailurl || 
			subcount(qz_metadata.content_box[qz_content_name]) || 
			( subcount(qz_metadata.content_box.voice)
				&& qz_metadata.content_box.voice_type == 1) ||
			subcount(qz_metadata.content_box.title)  ?>

			<?cs if:qz_metadata.content_box[qz_content_name].con.type =='qz_app' ?>

				<?cs set:qz_cont_box_class = 'appfeed_info' ?>
			<?cs elif:(qz_metadata.content_box.voice_type == 1 &&
						subcount(qz_metadata.content_box.voice)) ?>
				<?cs set:qz_cont_box_class = 'audio_box' ?>
			<?cs else ?>
				<?cs #:默认为.txt_box ?>
				<?cs set:qz_cont_box_class = 'txt_box' ?>
			<?cs /if ?>

			<div class="<?cs var:qz_cont_box_class ?>">
			<?cs if:subcount(qz_metadata.content_box.title) > 0 && mod.hidetitle!='true' ?>
		<h4 class="txt_box_title">
			<?cs if:subcount(qz_metadata.content_box.title.con.0) > 0 ?>
				<?cs each:item = qz_metadata.content_box.title.con ?>
					<?cs call:contentSubTitleItem(item) ?>
				<?cs /each ?>
			<?cs elif:subcount(qz_metadata.content_box.title.con) > 0 ?>
				<?cs call:contentSubTitleItem(qz_metadata.content_box.title.con) ?>
			<?cs elif:subcount(qz_metadata.content_box.title) > 0 ?>
				<?cs if:qz_metadata.content_box.title.url ?>
					<a href="<?cs var:qz_metadata.content_box.title.url ?>" target="_blank"><?cs var:qz_metadata.content_box.title.text ?></a>
				<?cs /if ?>
			<?cs /if ?>
		</h4>
			<?cs /if ?>
			<?cs #:来自组件 ?>
			<?cs if:source_in_content?>
				<?cs if:qz_metadata.extend_info.info.type == 'source' ?>
					<?cs call:iconSource(qz_metadata.extend_info.info) ?>
			<?cs /if ?>
			<?cs if:qz_metadata.extend_info.info.0.type == 'source' ?>
				<?cs call:iconSource(qz_metadata.extend_info.info.0) ?>
			<?cs /if ?>
		<?cs /if ?>

		<?cs if:qz_metadata.content_box[qz_orginuser_name].name ?><?cs call:userLink(qz_metadata.content_box[qz_orginuser_name],'') ?>: <?cs /if ?>

		<?cs if:qz_metadata.content_box.bgid && mod.blogTemplate==1?><?cs #模板日志 ?>
		<?cs with:bgid =qz_metadata.content_box.bgid ?>
			<div style="background:url('http://qzs.qq.com/qzone/space_item/orig/<?cs var:bgid % 16?>/<?cs var:bgid?>/feed.png')" class="txt_blog_template">
				<div class="blog_template_mask"></div>
				<i class="ui_icon blog_template_icon icon_blog_template"></i>
				<div class="blog_template_text"><?cs var:qz_metadata.content_box[qz_content_name].con?></div>
				<a class="blog_template_link" href="<?cs var:qz_metadata.content_box.detailurl?>" target="_blank"></a>
			</div>
		<?cs /with ?>
		<?cs else ?>
			<?cs call:contentBox-inline-voice()?><?cs #说说语音都放在前面?>
			<?cs call:richContent(qz_metadata.content_box[qz_content_name]) ?>
		<?cs /if ?>
		</div>
	<?cs /if ?>
<?cs /def ?>
<?cs #:
	function with_hr_title(param){}
?>
<?cs def:with_hr_title(mod,user,item) ?>
	<?cs if: subcount(user) ?>
		<div class="f_ct_title">
			<?cs call:userLink(user,"") ?>
			<span class="c_tx3 ui_ml5"><?cs var:mod.actionText ?>
			<?cs if:!subcount(item.0)&&!string.length(item) ?>
				了此条信息
			<?cs else ?>
				<?cs var:mod.actionPrating ?>
			<?cs /if ?>
			</span>
			<?cs if:subcount(item.0) ?>
				<?cs set:_hr=1 ?>
				<?cs loop:i=0, subcount(item)-1, 1 ?>
					<?cs call:contentSubTitleItem(item[i]) ?>
				<?cs /loop ?>
			<?cs elif:subcount(item)||string.length(item) ?>
				<?cs set:_hr=1 ?>
				<?cs call:contentSubTitleItem(item) ?>
			<?cs /if ?>
		</div>
		<div class="f_ang_t bor2"></div>
	<?cs /if ?>
<?cs /def ?>

<?cs #:元数据兼容 ?>
<?cs set:qz_content_name = 'content' ?>
<?cs if:subcount(qz_metadata.content_box.content_2) >0 ?>
	<?cs set:qz_content_name = 'content_2' ?>
<?cs /if ?>

<?cs set:qz_orginuser_name = 'orginuser' ?>
<?cs if:subcount(qz_metadata.content_box.orginuser_2) > 0 ?>
	<?cs set:qz_orginuser_name = 'orginuser_2' ?>
<?cs /if ?>

<?cs #:摘要区域 ?>
<?cs def:contentBoxCommon(type, mod) ?>

	<?cs #:默认的摘要 无框无背景色 				f_ct ?>
	<?cs #:摘要表现1 带左边框 					f_ct f_ct_1 bor3 ?>
	<?cs #:摘要表现2 边框 + 背景色				f_ct f_ct_2 bor3 bg3 ?>

	<?cs #:元数据兼容 ?>
	<?cs set:qz_content_name = 'content' ?>
	<?cs if:subcount(qz_metadata.content_box.content_2) >0 ?>
		<?cs set:qz_content_name = 'content_2' ?>
	<?cs /if ?>

	<?cs set:qz_orginuser_name = 'orginuser' ?>
	<?cs if:subcount(qz_metadata.content_box.orginuser_2) > 0 ?>
		<?cs set:qz_orginuser_name = 'orginuser_2' ?>
	<?cs /if ?>

	<?cs if:mod.contentHide==1 ?>
		<?cs set:className=type+" none _contentBox_" ?>
	<?cs else ?>
		<?cs set:className=type ?>
	<?cs /if ?>
	<?cs if:subcount(qz_metadata.magicemotion) || 
			subcount(qz_metadata.content_box.media) || 
			subcount(qz_metadata.vote_box) || 
			subcount(qz_metadata.lbs) || 
			subcount(qz_metadata.attach) || 
			subcount(qz_metadata.profile) || 
			subcount(qz_metadata.content_box[qz_content_name]) || 
			subcount(qz_metadata.music_box) || 
			subcount(qz_metadata.clock_box) || 
			subcount(qz_metadata.content_box.voice)?>
	<div class="<?cs var:className ?>">

		<?cs if:mod.with_hr==1 && subcount(qz_metadata.metadata.lastmsg) && qz_metadata.metadata.lastmsg.visiable==1 ?>
			<?cs call:with_hr_title(mod,qz_metadata.metadata.lastmsg.user,qz_metadata.metadata.lastmsg.con) ?>
			<?cs if: qz_metadata.metadata.appid==311 ?>
				<?cs set:qz_content_name="content" ?>
				<?cs set:qz_orginuser_name = 'orginuser' ?>
			<?cs /if ?>
		<?cs /if ?>
		<?cs call:subTitle()?>
		<?cs if:subcount(qz_metadata.magicemotion) || 
				subcount(qz_metadata.content_box[qz_content_name]) ||
				subcount(qz_metadata.content_box) ||
				qz_metadata.content_box.detailurl || 
				qz_metadata.content_box.media ||
				qz_metadata.content_box.media.media.src || 
				qz_metadata.content_box.media.media.0.src || 
				qz_metadata.content_box.media.media.0.avatar ?>
			<?cs set:qz_is_txtimg_class = '' ?>
			<?cs if:(	subcount(qz_metadata.content_box[qz_content_name]) > 0 || 
						subcount(qz_metadata.content_box.title) > 0
					) && (
						qz_metadata.content_box.media || 
						qz_metadata.content_box.media.media.src || 
						qz_metadata.content_box.media.media.0.src
					) ?>
				<?cs set:qz_is_txtimg_class = 'f_ct_txtimg' ?>
			<?cs /if ?>

			<?cs set:qz_custom_ct_class = '' ?>
			<?cs if:qz_metadata.content_box.media.media.0.aid ?>
				<?cs #:如果是appstore feeds ?>
				<?cs set:qz_custom_ct_class = ' f_ct_appfeed' ?>
			<?cs else ?>
				<?cs #:默认为.f_c_imgtxt?>
				<?cs set:qz_custom_ct_class = ' f_ct_imgtxt' ?>
			<?cs /if ?>

			<div class="<?cs var:qz_custom_ct_class ?> <?cs var:qz_is_txtimg_class ?>">
				<?cs call:contentBox-block-voice()?>
				<?cs #:文本区域 ?>
				<?cs call:contentBoxCommon-txt(mod, 0) ?>
				<?cs #:判断下有木有图片或者视频 ?>
				<?cs call:contentBoxCommon-img(mod, 0) ?>
				<?cs call:attachmentMagicemotionPresenter() ?>
				<?cs call:sameUserList(10) ?>
			</div>
		<?cs /if ?>
		<?cs call:music(mod) ?>
		<?cs call:profileViewer(mod) ?>
		<?cs call:clockBoxViewer()?>
		<?cs call:attachViewer(mod) ?>
		<?cs call:voteViewer(mod) ?>
		<?cs call:coupon(mod) ?>
		<?cs call:attachInfo(mod) ?>
		<div class="f_reprint_box">
			<?cs call:locationInfo(mod) ?>
			<?cs call:extendInfo(mod) ?>
		</div>
	</div>
	<?cs /if ?>
<?cs /def ?>

<?cs #:摘要区域左图右文 ?>
<?cs def:contentBoxCommonLeftImage(type, mod) ?>
	<?cs #:默认的摘要 无框无背景色 				f_ct ?>
	<?cs #:摘要表现1 带左边框 					f_ct f_ct_1 bor3 ?>
	<?cs #:摘要表现2 边框 + 背景色				f_ct f_ct_2 bor3 bg3 ?>
	<?cs if:subcount(qz_metadata.content_box) > 0  || subcount(qz_metadata.vote_box) > 0 || subcount(qz_metadata.extend_info) > 0 || subcount(qz_metadata.lbs) > 0 ?>
	<?cs set:qz_contentBox_type = qz_metadata.content_box.type ?>
	<?cs set:qz_is_txtimg = 0 ?>
	<?cs set:qz_is_txtimg_class = '' ?>
	<?cs set:qz_custom_ct_class='' ?>
	<?cs if:qz_contentBox_type == 'blog' || qz_contentBox_type == 'video' || qz_contentBox_type == 'pic' || qz_contentBox_type == 'music'?>
		<?cs set:qz_is_txtimg = 1 ?>
		<?cs set:qz_is_txtimg_class = ' f_ct_txtimg' ?>
	<?cs /if ?>

	<?cs if:qz_metadata.content_box[qz_content_name].con.type == 'qz_app' ?>
		<?cs #:如果是appstore feeds ?>
		<?cs set:qz_custom_ct_class=' f_ct_appfeed' ?>
	<?cs /if ?>

	<?cs if:mod.contentHide==1 ?>
		<?cs set:className=type+" none _contentBox_" ?>
	<?cs else ?>
		<?cs set:className=type ?>
	<?cs /if ?>
	<div class="<?cs var:className ?>">
		<div class="f_ct_imgtxt <?cs var:qz_custom_ct_class ?> <?cs if:subcount(qz_metadata.content_box[qz_content_name]) > 0  && qz_is_txtimg ?><?cs var:qz_is_txtimg_class ?><?cs /if ?>">
		<?cs call:subTitle()?>
		<?cs if:qz_is_txtimg?>
			<?cs call:contentBoxCommon-txt(mod, 1) ?>
			<?cs call:contentBoxCommon-img(mod, 0) ?>
		<?cs else ?>
		<?cs #:判断下有木有图片或者视频 ?>
			<?cs call:contentBoxCommon-img(mod, 1) ?>
			<?cs #:文本区域 ?>
			<?cs call:contentBoxCommon-txt(mod, 0) ?>
		<?cs /if ?>
		<?cs call:markPhotoInfo(mod) ?>
		<?cs call:sameUserList(10) ?>
		</div>
		<?cs call:voteViewer(mod) ?>
		<?cs call:coupon(mod) ?>
		<div class="f_reprint_box">
			<?cs call:locationInfo(mod) ?>
			<?cs call:extendInfo(mod) ?>
		</div>
	</div>
	<?cs /if ?>
<?cs /def ?>

<?cs #:默认的摘要 无框无背景色 				f_ct ?>
<?cs def:contentBox(mod)?>
	<?cs #:qz_metadata.content_box.media.newtype是为兼容被动feeds的老数据设置的 ?>
	<?cs #游戏试玩feed直达通道 ?>
	<?cs if:qz_metadata.content_box.media.media.game == 1 ?>
		<?cs call:game() ?>
	<?cs elif:qz_metadata.content_box[qz_content_name].con.type == 'qz_app' ||  (qz_metadata.content_box.media.media.avatar && qz_metadata.metadata.appid == 217) ?>
		<?cs #:如果是应用的话，需要跟据数据来判断是不是左图右文 ?>
		<?cs call:contentBoxCommonLeftImage('f_ct', mod) ?>
	<?cs else ?>
		<?cs call:contentBoxCommon('f_ct', mod) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:摘要表现1 带左边框 					f_ct f_ct_1 bor3 ?>
<?cs def:contentBoxLeftBor(mod) ?>
	<?cs call:contentBoxCommon('f_ct f_ct_1 bor3', mod) ?>
<?cs /def ?>

<?cs #:摘要表现2 边框 + 背景色				f_ct f_ct_2 bor3 bg3 ?>
<?cs def:contentBoxLeftBorBg(mod) ?>
	<?cs call:contentBoxCommon('f_ct f_ct_2 bor3 bg3', mod) ?>
<?cs /def ?>


<?cs #:默认的摘要 无框无背景色 				f_ct ?>
<?cs def:contentBoxLeftImage(mod)?>
	<?cs call:contentBoxCommonLeftImage('f_ct', mod) ?>
<?cs /def ?>

<?cs #:摘要表现1 带左边框 					f_ct f_ct_1 bor3 ?>
<?cs def:contentBoxLeftBorLeftImage(mod) ?>
	<?cs call:contentBoxCommonLeftImage('f_ct f_ct_1 bor3', mod) ?>
<?cs /def ?>

<?cs #:摘要表现2 边框 + 背景色				f_ct f_ct_2 bor3 bg3 ?>
<?cs def:contentBoxLeftBorBgLeftImage(mod) ?>
	<?cs call:contentBoxCommonLeftImage('f_ct f_ct_2 bor3 bg3', mod) ?>
<?cs /def ?>