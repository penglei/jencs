<?cs include:"wupcs/modview-qqtab/image.cs"?><?cs #默认图片展示，基本接口(压缩、显示)?>
<?cs include:"wupcs/modview-qqtab/grid_pic.cs"?><?cs #宫格模式图片展示方法?>
<?cs include:"wupcs/modview-qqtab/gplus_pic.cs"?><?cs #g+(6 + 1)模式展示?>
<?cs include:"wupcs/modview-qqtab/music.cs" ?>
<?cs include:"wupcs/modview-qqtab/video.cs" ?>
<?cs include:"wupcs/modview-qqtab/attachmentPresenter.cs" ?>
<?cs include:"wupcs/modview-qqtab/attachViewer.cs" ?>
<?cs include:"wupcs/modview-qqtab/voice_player.cs" ?>

<?cs ####
	/**
	 *默认模式展示图片
	 */
?>
<?cs def:contentMedia_pic_default()?>
	<?cs call:contentMedia_start_imgbox()?>
		<?cs each:picItem = qfv.content.media.pic?>
			<?cs call:contentMediaPic_item(picItem)?>
		<?cs /each?>
	<?cs call:contentMedia_end_imgbox()?>
<?cs /def?>

<?cs ####
	/**
	 *V8 分享左图右文的图片压缩逻辑
	 */
?>
<?cs def:contentMedia_pic_LeftSmallPic()?>
	<div class="img_box">
	<?cs #:最多只显示一张 ?>
	<?cs if:subcount(qfv.content.media.pic)?>
	<?cs with:pic = qfv.content.media.pic.0?>
		<?cs set:className="bor3" ?>
		<?cs if: pic.type==2 ?>
			<?cs set:className = className+" img_gif" ?>
		<?cs /if ?>
		<?cs call:_contentMedia_display_pic_start(pic,className,"")?>
		<?cs call:imageFlag(pic.type) ?>
		<img src="/ac/b.gif"
			 onload="<?cs escape:'html'?>
				<?cs call:contentBox_ReduceImgByShortEdge_AlignCenter_onLoad(pic, 120, 120, "")?><?cs #最大宽为120, 高为120?>
				<?cs /escape:'html'?>"
		 />
		<?cs call:_contentMedia_display_pic_end()?>
		<?cs call:contentMedia_end_imgbox()?>
	<?cs /with?>
	<?cs /if ?>
<?cs /def?>

<?cs ####
	/**
	 *宫格模式展示图片
	 */
?>
<?cs def:contentMedia_pic_Grid(size)?>
	<?cs set:_media_pic_count = subcount(qfv.content.media.pic)?>
	<?cs if:_media_pic_count == 1?>
		<?cs call:_contentMedia_grid_one()?>
	<?cs elif:_media_pic_count == 2?><?cs #两张应该叫做3宫格?>
		<?cs if:size == "small" ?>
			<?cs call:_contentMedia_grid_two_small()?>
			<?cs else ?>
			<?cs call:_contentMedia_grid_two()?>
		<?cs /if ?>
	<?cs elif:_media_pic_count == 4?><?cs #四张是四宫格?>
		<?cs call:_contentMedia_grid_four()?>
	<?cs else?><?cs #如果图片是3张，或者>=5张，都应该是九宫格?>
		<?cs call:_contentMedia_grid_nine()?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *g+ 模式展示图片
	 */
?>
<?cs def:contentMedia_pic_GPlus()?>
	<?cs set:_media_pic_count = subcount(qfv.content.media.pic)?>
	<?cs call:_contentMedia_GPlus()?>
<?cs /def?>

<?cs ####
	/**
	 *限定图片的展示大小。
	 *通过图片展示模式，限制图片的最大高宽
	 */
?>
<?cs def:_limit_image_size(width, height)?>
	<?cs set:_end = subcount(qfv.content.media.pic) - 1?>
	<?cs loop:i = 0, _len - 1, 1?>
		<?cs call:data_cntmedia_limitImgSize("content.media.pic." + i, width, height)?>
	<?cs /loop?>
<?cs /def?>


<?cs ####
	/**
	 *图片数
	 */
?>
<?cs def:_contentMedia_pictures_count()?>
	<?cs set:cntbox_img_count = subcount(qfv.content.media.pic)?>
	<?cs if:cntbox_img_count == 3?>
		<?cs set:cntbox_img_count = 2?>
	<?cs elif:cntbox_img_count > 4?>
		<?cs set:cntbox_img_count = 4?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *展示图片入口
	 */
?>
<?cs def:_contentMedia_pictures()?>
	<?cs loop: i = 0, cntbox_img_count - 1, 1?>
	<?cs if:qfv.content.media.pic[i].action.type=="url"?>
		<a class="img_box_item" href="<?cs var:qfv.content.media.pic[i].action.url ?>" target="_blank"><?cs #图片回源?>
			<img src="<?cs var:html_encode(qfv.content.media.pic[i].src, 1)?>" onload="QzFeedsResizeImage(this, <?cs alt:cntbox_img_count?>0<?cs /alt?>)" />
		</a>
	<?cs else?>
		<a class="img_box_item qz_img_viewer" href="#"
			 data-action="2"<?cs #大图弹框?>
			 data-param="<?cs var:qfv.content.media.pic[i].action.param?>"<?cs #这里不需要做html_encode，都是系统内id?>
			 data-imgurl="<?cs var:html_encode(qfv.content.media.pic[i].src, 1)?>"
		>
			<?cs #图片压缩算法?>
			<?cs #压缩的算法全部由前台实现，这里值提供图片展示的方式：1张|2宫格|4宫格?>
			<img src="<?cs var:html_encode(qfv.content.media.pic[i].src, 1)?>" onload="QzFeedsResizeImage(this, <?cs alt:cntbox_img_count?>0<?cs /alt?>)" />
		</a>
	<?cs /if?>
	<?cs /loop?>
<?cs /def?>

<?cs ####
	/**
	 *展示视频入口
	 */
?>
<?cs def:_contentMedia_videos()?>
	<?cs set: _detail_url = "#" ?>
	<?cs if:qfv.meta.appid==311 ?>
		<?cs set: _detail_url = get_mood_url.ret ?>
	<?cs elif:qfv.meta.appid==202 ?>
		<?cs set: _detail_url = data_share_detail_url.ret ?>
	<?cs /if ?>
	<div class="video_box">
	<a class="video_img" href="<?cs var:_detail_url?>" target="_blank">
	<img src="<?cs var:html_encode(qfv.content.media.video.0.screenshot, 1)?>" onload="QzFeedsResizeImage(this, 0)">
	</a>
	<a class="video_bt" href="<?cs var:_detail_url?>" target="_blank">
	<i class="ui_icon icon_vedio_play"></i>
	</a><div class="video_info">点击去空间播放</div></div>
<?cs /def?>


<?cs ####
	/**
	 *所有媒体信息的默认展示方式
	 *默认方式中图片和视频是不互斥的
	 *
	 *大多数feeds的media展示都使用该方法，但是如果某种feeds的media展示有很大的不同，
	 *或者放在这些统一逻辑里不好处理，就要单独在业务视图模板实现media的展示
	 *可以参考礼物feeds的做法。礼物feeds就没有使用contentMedia宏
	 */
?>
<?cs def:contentMedia()?>
	<?cs #XXX 默认视频和图片是互斥的，不会放在一起展示。如果某一天产品有这个需求，再考虑修改展示层，数据层不要变?>

	<?cs if:subcount(qfv.content.media.video)?><?cs #有视频才展示?>
		<?cs call:_contentMedia_videos()?>
	<?cs elif:subcount(qfv.content.media.pic)?><?cs #有图片才展示?>
		<?cs call:_contentMedia_pictures()?>
	<?cs elif:subcount(qfv.content.media.music)?><?cs #有音乐才展示?>
		<?cs call:music(qfv.content.media.music) ?>
	
	<?cs elif:subcount(qfv.content.magicemotion)?><?cs #有魔法表情才展示?>
		<?cs call:attachmentPresenter(qfv.content.magicemotion) ?>
	<?cs elif:subcount(qfv.content.attach)?><?cs #有魔法表情才展示?>
		<?cs call:attachViewer(qfv.content.attach) ?>
	<?cs /if?>
<?cs /def?>

