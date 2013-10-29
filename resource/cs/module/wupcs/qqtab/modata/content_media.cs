<?cs ####
	/**
	 * 修复适配图片大小失败。取小一张的图知道陈功
	 */
?>
<?cs def:_adapt_fail_fix_index(index, pic)?>
	<?cs if:index < 2 ?>
		<?cs set:_adapt_fail_fix_index.ret = 0?>
	<?cs else ?><?cs #即使有4张也按3张逻辑来fix?>
		<?cs if:pic.picinfo[index - 1].url?>
			<?cs set:_adapt_fail_fix_index.ret = index - 1?>
		<?cs elif:pic.picinfo[index - 2].url?>
			<?cs set:_adapt_fail_fix_index.ret = index - 2?>
		<?cs else ?>
			<?cs set:_adapt_fail_fix_index.ret = 0?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *获得一张图片的url。从不同的data源中获取url
	 *同一图片可能有多张规格，后续考虑通过参数控制获取不同规格的图片
	 *@param {String|ItemDataBody}  itemDataBody 包含图片信息的itemDataBody
	 *@param {Unknown}  后续扩展用参数
	 */
?>
<?cs def:_cnt_adapt_pic_url(data, forceIndex)?>
	<?cs if:subcount(data) ?><?cs #传入的是数据结束，而不是url?>
		<?cs set:_pic_index = 0?>
		<?cs if:_g_cnt_media_viewType ==  G_IMG_GPLUS_MODE?>
			<?cs if:!_g_cnt_media_first_pic?><?cs #当前是不是第一张图片(第一张要取大图?>
				<?cs set:_g_cnt_media_first_pic = 1?>
				<?cs set:_pic_index = 2?>
				<?cs if:data.pictype != 2 && g_isV8?><?cs #主页feeds大小调大, gif图片除外?>
					<?cs set:_pic_index = 3?>
				<?cs /if?>
				<?cs if:data.picinfo[_pic_index].url?>
					<?cs set:_pic_url = data.picinfo[_pic_index].url?>
				<?cs else ?>
					<?cs set:_pic_url = data.picinfo.0.url?>
					<?cs set:_pic_index = 0?>
				<?cs /if?>
			<?cs else ?>
				<?cs #优先取第二规格的吧。TODO 需要添加更多的逻辑?>
				<?cs if: data.picinfo.2.url?>
					<?cs set:_pic_url = data.picinfo.2.url?>
					<?cs set:_pic_index = 2?>
				<?cs else ?>
					<?cs set:_pic_url = data.picinfo.0.url?>
					<?cs set:_pic_index = 0?>
				<?cs /if?>
			<?cs /if?>
		<?cs elif:_g_cnt_media_viewType == G_IMG_GRID_MODE ?><?cs #宫格型，选取合适的图片大小?>
			<?cs with:len = g_orgdata_item_count?>
			<?cs if:len == 1?><?cs #width:800?>
				<?cs if:g_isV8?><?cs #首页feeds需要500的大图，所以只能取800的了?>
					<?cs set:_pic_index = 3?>
				<?cs else ?>
					<?cs set:_pic_index = 2?>
				<?cs /if?>
			<?cs elif:len == 2 || len == 4?><?cs #width:400?>
				<?cs if:data.picinfo[1].width == 200 && data.picinfo[1].height >= 200 && !g_isV8 ?>
					<?cs set:_pic_index = 1?>
				<?cs else ?>
					<?cs set:_pic_index = 2?><?cs #width:200只是最大值?>
				<?cs /if?>
			<?cs else ?><?cs #虽然显示区域只有128的大小，但是还是 取200的吧?>
				<?cs set:_pic_index = 1?>
			<?cs /if?>
			<?cs #如果没有指定大小，就取原始大小吧 ?>
			<?cs if:!data.picinfo[_pic_index].url ?>
				<?cs call:_adapt_fail_fix_index(_pic_index, data)?>
				<?cs set:_pic_index = _adapt_fail_fix_index.ret?>
			<?cs /if?>
			<?cs set:_pic_url = data.picinfo[_pic_index].url?>
			<?cs /with?>
		<?cs elif:_g_cnt_media_viewType == G_IMG_SMALL_MODE?>
			<?cs set:_pic_index = 0?><?cs #TODO 小图模式取100px width 合适么..?>
			<?cs set:_pic_url = data.picinfo[_pic_index].url?>
		<?cs elif:_g_cnt_media_viewType == G_IMG_SMALL_V8_MODE?>
			<?cs set:_pic_index = 1?>
			<?cs set:_pic_url = data.picinfo[_pic_index].url?>
			<?cs #如果没有指定大小，就取原始大小吧 ?>
			<?cs if:!data.picinfo[_pic_index].url ?>
				<?cs set:_pic_index = 0?>
			<?cs /if?>
			<?cs set:_pic_url = data.picinfo[_pic_index].url?>
		<?cs elif:_g_cnt_media_viewType == G_IMG_GRID_MODE_SMALL?>
			<?cs if:subcount(data.picinfo)==1 ?><?cs #: 单张图片就显示最小图 ?>
					<?cs set:_pic_index = 0?>
					<?cs set:_pic_url = data.picinfo[_pic_index].url?>
				<?cs else ?>
					<?cs set:_pic_index = 1?>
					<?cs set:_pic_url = data.picinfo[_pic_index].url?>
					<?cs #如果没有指定大小，就取原始大小吧 ?>
					<?cs if:!data.picinfo[_pic_index].url ?>
						<?cs set:_pic_index = 0?>
					<?cs /if?>
					<?cs set:_pic_url = data.picinfo[_pic_index].url?>
			<?cs /if ?>
		<?cs else ?>
			<?cs set:_pic_index = 0?>
			<?cs set:_pic_url = data.picinfo.0.url?><?cs #默认都取第一种规格的图片(100 width)?>
		<?cs /if?>
	<?cs else ?><?cs #data就是图片url?>
		<?cs set:_pic_url = data?>
		<?cs set:_pic_index = -1?>
	<?cs /if?>
	<?cs set:_cnt_adapt_pic_url.ret = _pic_url?>
	<?cs set:_cnt_adapt_pic_url.ret.index = _pic_index?>
<?cs /def?>

<?cs ####
	/**
	 *通过容器宽高和图片宽高来算出最合适的图片尺寸 ，如果没有宽高信息，那么就走老逻辑
	 *@param {ItemDataBody}  itemDataBody 包含图片信息的itemDataBody
	 *@param {Int} 容器宽度
	 *@param {Int} 容器高度
	 */
?>
<?cs def:get_proper_img_url(data,box_width, box_height) ?>
	<?cs if:#data.picinfo[0].width >0 && #data.picinfo[0].height>0?>
		<?cs set:_end = subcount(data.picinfo) -1 ?>
		<?cs loop:i = 0, _end, 1?>
			<?cs with:picinfo = data.picinfo[i]?>
				<?cs if:picinfo.width > box_width && picinfo.height > box_height ?>
					<?cs set:_ret_url = picinfo.url ?>
					<?cs set:_ret_index = i ?>
					<?cs set:i = _end ?> <?cs #:相当于break ?>
				<?cs /if ?>
			<?cs /with ?>
		<?cs /loop ?>
		<?cs set:get_proper_img_url.ret = _ret_url?>
		<?cs set:get_proper_img_url.ret.index = _ret_index?>
	<?cs else ?>
		<?cs call:_cnt_adapt_pic_url(data, "") ?>
		<?cs set:get_proper_img_url.ret = _cnt_adapt_pic_url.ret?>
		<?cs set:get_proper_img_url.ret.index = _cnt_adapt_pic_url.ret.index?>
	<?cs /if ?>

<?cs /def ?>
<?cs ####
	/**
	 *限制图片的大小。
	 *一般情况是不需要指明图片的大小限制的，只有那些默认展现的图片可能需要大小限制。
	 */
?>
<?cs def:data_cntmedia_limitImgSize(path, width, height)?>
	<?cs call:set(path, "limitWidth", width)?>
	<?cs call:set(path, "limitHeight", width)?>
<?cs /def?>

<?cs #{////图片接口?>
<?cs ####
	/**
	 *生成一个图片节点
	 *@param {Integer} index 图片的序号
	 *@param {String} url 图片地址
	 *@param {String} type 图片的额外数据，用以区分不同的图片
	 *@returns {String} 图片的数据路径
	 */
?>
<?cs def:data_cntmedia_pic(index, url, type)?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs set:data_cntmedia_pic.ret = "content.media.pic." + index?>
	<?cs call:set(data_cntmedia_pic.ret, "src", url)?>
	<?cs if:type?>
		<?cs call:set(data_cntmedia_pic.ret, "type", type)?>
	<?cs /if?>
<?cs /def?>


<?cs #{////图片接口?>
<?cs ####
	/**
	 *生成一个图片节点
	 *@param {Integer} path 指定路径
	 *@param {String} url 图片地址
	 *@param {String} type 图片的额外数据，用以区分不同的图片
	 *@returns {String} 图片的数据路径
	 */
?>
<?cs def:data_cntmedia_pic_with_path(path, url, type)?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs set:data_cntmedia_pic_with_path.ret = path ?>
	<?cs call:set(data_cntmedia_pic_with_path.ret, "src", url)?>
	<?cs if:type?>
		<?cs call:set(data_cntmedia_pic_with_path.ret, "type", type)?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *标准的图片数据生成方法，点击使用浮层查看
	 *popup需要iframe的址，目前没有使用参数传递，那就需要在这里进行配置。
	 *
	 *@param {Integer} index 第几张图片
	 *@param {String|ItemDataBody} data 图片url或者图片数据节点
	 *@param {String} action  点击图片要跳转的地址
	 *@param {popupSrc} extend1 扩展字段1
	 *@param {Unknown} extend2 扩展字段2
	 *@returns {String} 生成的图片数据路径
	 */
?>
<?cs def:data_cntmedia_pic_popup(index, data, param, popupSrc, photoype)?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs call:_cnt_adapt_pic_url(data, "")?>
	<?cs call:data_cntmedia_pic(index, _cnt_adapt_pic_url.ret, data.pictype)?>

	<?cs call:set(data_cntmedia_pic.ret, "width", data.picinfo[_cnt_adapt_pic_url.ret.index].width)?>
	<?cs call:set(data_cntmedia_pic.ret, "height", data.picinfo[_cnt_adapt_pic_url.ret.index].height)?>
	<?cs call:set(data_cntmedia_pic.ret, "centerpoint_x", data.picinfo[_cnt_adapt_pic_url.ret.index].extendinfo.centerpoint_x)?>
	<?cs call:set(data_cntmedia_pic.ret, "centerpoint_y", data.picinfo[_cnt_adapt_pic_url.ret.index].extendinfo.centerpoint_y)?>
	<?cs set:_actionpath = data_cntmedia_pic.ret + ".action"?>

	<?cs call:data_popup(_actionpath, "", popupSrc, param, 2, data.width, data.height, "", "")?>
	<?cs set:data_cntmedia_pic_popup.ret = data_cntmedia_pic.ret?>
<?cs /def?>

<?cs ####
	/**
	 *新版相册浮层，没有iframe了
	 *
	 *@param {Integer} index 第几张图片
	 *@param {String|ItemDataBody} data 图片url或者图片数据节点
	 *@param {String} appid 如果是赞的话，需要回源到本身的appid。可以为空
	 */
?>
<?cs def:data_cntmedia_pic_popup_v2(index, data, topicid, pickey, appid)?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs call:_cnt_adapt_pic_url(data, "")?>
	<?cs call:data_cntmedia_pic(index, _cnt_adapt_pic_url.ret, data.pictype)?>
	<?cs set:_actionpath = data_cntmedia_pic.ret + ".action"?>
	<?cs call:data_popup(_actionpath, "", "", "", 2, data.width, data.height, "", "")?>
	<?cs call:data_popup_add_attr(_actionpath, "topicid", topicid) ?>
	<?cs call:data_popup_add_attr(_actionpath, "pickey", pickey) ?>
	<?cs call:data_popup_add_attr(_actionpath, "appid", appid) ?>
	<?cs set:data_cntmedia_pic_popup.ret = data_cntmedia_pic.ret?>
<?cs /def?>

<?cs ####
	/**
	 * 依赖data_cntmedia_pic_popup，必须在data_cntmedia_pic_popup调用后才能调用
	 * @param  {[type]} key   [description]
	 * @param  {[type]} value [description]
	 * @return {[type]}       [description]
	 */
	function data_cntmedia_pic_add_popup_attr(key,value){}
?>
<?cs def:data_cntmedia_pic_add_popup_attr(key,value) ?>
	<?cs set:_actionpath = data_cntmedia_pic_popup.ret + ".action"?>
	<?cs call:data_popup_add_attr(data_cntmedia_pic_popup.ret + ".action",key,value) ?>
<?cs /def ?>

<?cs ####
	/**
	 *标准的图片数据生成方法，包含点击浮层展示的数据
	 */
?>
<?cs def:data_cntmedia_pic_urlaction(index, data, action, photoype, attr)?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs call:_cnt_adapt_pic_url(data, "")?>
	<?cs call:data_cntmedia_pic(index, _cnt_adapt_pic_url.ret, data.pictype)?>
	<?cs call:set(data_cntmedia_pic.ret, "width", data.picinfo[_cnt_adapt_pic_url.ret.index].width)?>
	<?cs call:set(data_cntmedia_pic.ret, "height", data.picinfo[_cnt_adapt_pic_url.ret.index].height)?>
	<?cs call:set(data_cntmedia_pic.ret, "centerpoint_x", data.picinfo[_cnt_adapt_pic_url.ret.index].extendinfo.centerpoint_x)?>
	<?cs call:set(data_cntmedia_pic.ret, "centerpoint_y", data.picinfo[_cnt_adapt_pic_url.ret.index].extendinfo.centerpoint_y)?>
	<?cs set:_actionpath = data_cntmedia_pic.ret + ".action"?>
	<?cs call:set(_actionpath, "type", "url")?>
	<?cs call:set(_actionpath, "url", action)?><?cs #注意，跳转是用url字段?>
	<?cs set:data_cntmedia_pic_urlaction.ret = data_cntmedia_pic.ret?>
	<?cs loop:i=0, subcount(attr)-1, 1 ?>
		<?cs call:set(_actionpath, attr[i].key, attr[i].value)?>
	<?cs /loop ?>
<?cs /def?>

<?cs #}// end:图片接口?>



<?cs #{////视频接口?>

<?cs ####
	/**
	 *生成视频节点数据
	 *
	 *视频复用了imgMode，只是用来获取不同大小的图片
	 *
	 *XXX 视频虽然用数组存储，但是一般一条feeds只支持展示一个视频(多个有必要么..)。
	 * 视频一般和图片也是互斥的
	 *
	 */
?>
<?cs def:data_cntmedia_video(index, itemDataBody, pictype)?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs set:_cnt_media_path = "content.media.video." + index?>
	<?cs set:data_cntmedia_video.ret = _cnt_media_path?>

	<?cs call:set(_cnt_media_path, "name", itemDataBody.videourl)?>
	<?cs call:set(_cnt_media_path, "id", itemDataBody.itemid)?>
	<?cs call:set(_cnt_media_path, "src", itemDataBody.vidioswfurl)?>

	<?cs if: qz_metadata.meta.feedstype != UC_WUP_FEEDSTYPE_PSV && itemDataBody.width>=400 ?>
		<?cs call:set(_cnt_media_path,"bigImage",1)?>
	<?cs else ?>
		<?cs call:set(_cnt_media_path,"bigImage",0)?>
	<?cs /if ?>

	<?cs call:_cnt_adapt_pic_url(itemDataBody, "")?><?cs #视频的图片这样取可以么？还是单独写一个函数来取?>
	<?cs call:set(_cnt_media_path, "screenshot", _cnt_adapt_pic_url.ret)?>
	<?cs #视频预览图的类型 ?>
	<?cs if:pictype ?>
		<?cs call:set(_cnt_media_path, "pictype", pictype)?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *生成视频信息，点击跳转
	 */
?>
<?cs def:data_cntmedia_video_urlaction(index, itemDataBody, action, pictype, extend2)?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs call:data_cntmedia_video(index, itemDataBody, pictype)?>
	<?cs call:set(data_cntmedia_video.ret, "action.type", "url")?>
	<?cs call:set(data_cntmedia_video.ret, "action.url", action)?>

	<?cs set:data_cntmedia_video_urlaction.ret = data_cntmedia_video.ret?><?cs #返回数据路径?>
<?cs /def?>

<?cs ####
	/**
	 *生成视频信息，在当前位置播放
	 */
?>
<?cs def:data_cntmedia_video_show(index, itemDataBody, param, playerUrl, pictype)?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs call:data_cntmedia_video(index, itemDataBody, pictype)?>
	<?cs set:popup_path=data_cntmedia_video.ret+".action" ?>
	<?cs call:data_popup(popup_path, "", playerUrl, param, 3, itemDataBody.width, itemDataBody.height, "popup", "") ?>
<?cs /def?>

<?cs #}// end:视频接口?>


<?cs ####
	/**
	 *图片的相册信息
	 *@param {String} id 相册id
	 *@param {String} albumName 相册名字
	 *@param {String} converurl 相册封面url
	 *@param {Integer} totalpic 相册中总共有多少图片
	 */
?>
<?cs def:data_cntmedia_album()?>
	<?cs if:subcount(qz_metadata.orgdata.albumdata)?><?cs #是否需要添加 appid == 4 的判断呢??>

	<?cs # 相册目前在使用６宫格展示图片信息，所以在这里生成图片展示标志?>
	<?cs with:albumdata = qz_metadata.orgdata.albumdata?>

		<?cs call:qfv("content.media.album.name", albumdata.sAlbumName)?>
		<?cs call:qfv("content.media.album.id", albumdata.id)?>
		<?cs call:qfv("content.media.album.coverurl", albumdata.sAlbumCoverUrl)?>
		<?cs call:qfv("content.media.album.totalpic", albumdata.iPicNum)?>

	<?cs /with?>
	<?cs /if?>
<?cs /def?>
