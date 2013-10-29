<?cs #{//内容区的布局方式?>
<?cs set:G_LAYOUT_DEFAULT = 0 ?>
<?cs set:G_LAYOUT_LEFTIMG = 1?>
<?cs set:G_LAYOUT_LEFTIMG_V8 = 2?>
<?cs #}//?>

<?cs #{//图片的展示模式?>
<?cs set:G_IMG_DEFAULT = 0?>
<?cs set:G_IMG_GRID_MODE = 1?>
<?cs set:G_IMG_GPLUS_MODE = 2?>
<?cs set:G_IMG_SMALL_MODE = 3?>
<?cs set:G_IMG_SMALL_V8_MODE = 4?><?cs #: 默认模式但用短边压缩居中压缩算法 ?>
<?cs set:G_IMG_NOIMG = 5?><?cs #:无图模式 待干掉 ?>
<?cs set:G_IMG_GRID_MODE_SMALL = 6?><?cs #:小图的网格状展示 ?>
<?cs set:G_IMG_SMART = 7?><?cs #:智能排版 ?>
<?cs #}//?>

<?cs ####
	/**
	 * 表示没有内容。这个全局变量是为了标示内容区有没有“内容”。
	 * 这是为了内容区不会出现空标签，导致空行或者色块。
	 * 内容区在展示的时候，会判断这个标志是否被置1，如果是，表示内容区有“内容”，然后才输出内容区组件
	 * 因此，如果需要在内容区添加组件，必须把该字段置1
	 */
?>
<?cs call:qfv("content", 0)?>

<?cs #内容区标题?>
<?cs include:"wupcs-v8/modata/content_title.cs"?>
<?cs #图片内容?>
<?cs include:"wupcs-v8/modata/content_media.cs"?>
<?cs #文字内容?>
<?cs include:"wupcs-v8/modata/content_summary.cs"?>
<?cs #智能排版图片容器大小定义?>
<?cs include:"wupcs-v8/modata/smartlayout_image_size.cs"?>
<?cs ####
	/**
	 *内容区初始化，
	 *比如
	 *  对图片的总数处理，生成内部变量
	 *  决定是否使用九宫格模式展现
	 *对于只有一张图片，无论是9宫格，还是6+1，展现方式是否一样看视图层自己处理
	 */
?>
<?cs def:data_content_init(layoutMode, imgMode, extend)?>
	<?cs set:_g_cnt_layoutMode = layoutMode?>
	<?cs set:_g_cnt_media_viewType = imgMode?><?cs #判断图片展示的类型，用以具体图片的信息获取?>

	<?cs set:_g_cnt_media_item_count = subcount(qz_metadata.orgdata.itemdata) ?>
	<?cs call:qfv("content.media.imgMode", imgMode)?>
	<?cs call:qfv("content.layoutMode", layoutMode)?>

	<?cs call:data_cntmedia_album()?><?cs #判断是否有相册数据?>
<?cs /def?>

