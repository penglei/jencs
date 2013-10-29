<?cs include:"wupcs-v8/modview/image.cs"?><?cs #默认图片展示，基本接口(压缩、显示)?>
<?cs include:"wupcs-v8/modview/grid_pic.cs"?><?cs #宫格模式图片展示方法?>
<?cs include:"wupcs-v8/modview/gplus_pic.cs"?><?cs #g+(6 + 1)模式展示?>
<?cs include:"wupcs-v8/modview/smart_pic.cs"?><?cs #图片智能排版?>
<?cs include:"wupcs-v8/modview/music.cs" ?>
<?cs include:"wupcs-v8/modview/video.cs" ?>
<?cs include:"wupcs-v8/modview/attachmentPresenter.cs" ?>
<?cs include:"wupcs-v8/modview/attachViewer.cs" ?>
<?cs include:"wupcs-v8/modview/voice_player.cs" ?>

<?cs ####
	/**
	 *限定图片的展示大小。
	 *通过图片展示模式，限制图片的最大高宽
	 */
?>
<?cs def:v8__limit_image_size(width, height)?>
	<?cs set:_end = subcount(qfv.content.media.pic) - 1?>
	<?cs loop:i = 0, _end, 1?>
		<?cs call:data_cntmedia_limitImgSize("content.media.pic." + i, width, height)?>
	<?cs /loop?>
<?cs /def?>

<?cs ####{
	/**
	 *默认模式展示图片
	 */
?>
<?cs def:v8_contentMedia_pic_default()?>
	<?cs call:v8_contentMedia_start_imgbox()?>
		<?cs each:picItem = qfv.content.media.pic?>
			<?cs call:v8_contentMediaPic_item(picItem)?>
		<?cs /each?>
	<?cs call:v8_contentMedia_end_imgbox()?>
<?cs /def?>

<?cs ####
	/**
	 *V8 分享左图右文的图片
	 */
?>
<?cs def:v8_contentMedia_pic_LeftSmallPic()?>
	<div class="img-box">
	<?cs #:最多只显示一张 ?>
	<?cs if:subcount(qfv.content.media.pic)?>
	<?cs with:pic = qfv.content.media.pic.0?>
		<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
			<?cs call:setShortEdgeResizeAlignCenter(pic, 100, 100, "")?>
		<?cs else ?>
			<?cs call:setShortEdgeResizeAlignCenter(pic, 120, 120, "")?>
		<?cs /if?>
		<?cs set:className="" ?>
		<?cs if:pic.type == 2 || pic.type == 3 ?>
			<?cs set:className="with-sign" ?>
		<?cs /if ?>
		<?cs call:v8__contentMedia_display_pic_start(pic,className,"")?>
		<?cs call:v8_imageFlag(pic.type) ?>
		<?cs if:pic.width==0 && pic.height==0 ?>
			<img src="/ac/b.gif"
				onload="<?cs escape:'html'?>
					<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
						<?cs call:v8_contentBox_ReduceImgByShortEdge_AlignCenter_onLoad(pic, 100, 100, "")?><?cs #最大宽为120, 高为120?>
					<?cs else ?>
						<?cs call:v8_contentBox_ReduceImgByShortEdge_AlignCenter_onLoad(pic, 120, 120, "")?><?cs #最大宽为100, 高为100?>
					<?cs /if?>
					<?cs /escape:'html'?>"
			 />
		<?cs else ?>
			<img src="<?cs var:pic.src ?>" 
			<?cs call:v8_echoClass(pic)?>
			<?cs call:v8_echoStyle(pic)?>
			<?cs #call:v8_echoOneImageAlignCenter(pic)?>
			/>
		<?cs /if ?>
		<?cs call:v8__contentMedia_display_pic_end()?>
		<?cs call:v8_contentMedia_end_imgbox()?>
	<?cs /with?>
	<?cs /if?>
<?cs /def?>
<?cs #}?>

<?cs ####
	/**
	 *宫格模式展示图片
	 */
?>
<?cs def:v8_contentMedia_pic_Grid(size)?>
	<?cs set:_media_pic_count = subcount(qfv.content.media.pic)?>
	<?cs if:_media_pic_count == 1?>
		<?cs if:size == "small" ?>
			<?cs call:v8__limit_image_size(100, 75)?>
			<?cs call:v8_contentMedia_pic_default()?><?cs #默认的样子?>
		<?cs else ?>
			<?cs call:v8__contentMedia_grid_one()?>
		<?cs /if?>
	<?cs elif:_media_pic_count == 2?><?cs #两张应该叫做4宫格?>
		<?cs if:size == "small" ?>
			<?cs call:v8__contentMedia_grid_two_small()?>
		<?cs else ?>
			<?cs call:v8__contentMedia_grid_four()?>
		<?cs /if ?>
	<?cs elif:_media_pic_count == 4?><?cs #四张是四宫格?>
		<?cs if:size == "small" ?>
			<?cs call:v8__contentMedia_grid_four_small()?>
		<?cs else ?>
			<?cs call:v8__contentMedia_grid_four()?>
		<?cs /if?>
	<?cs else?><?cs #如果图片是3张，或者>=5张，都应该是九宫格?>
		<?cs if:size == "small" ?>
			<?cs call:v8__contentMedia_grid_nine_small()?>
		<?cs else ?>
			<?cs call:v8__contentMedia_grid_nine()?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *g+ 模式展示图片
	 */
?>
<?cs def:v8_contentMedia_pic_GPlus()?>
	<?cs set:_media_pic_count = subcount(qfv.content.media.pic)?>
	<?cs call:v8__contentMedia_GPlus()?>
<?cs /def?>

<?cs ####
	/**
	 *展示图片入口
	 */
?>
<?cs def:v8__contentMedia_pictures()?>
	<?cs set:_imgMode = qfv.content.media.imgMode?>
	<?cs if:_imgMode == G_IMG_GPLUS_MODE?><?cs #六+1模式?>
		<?cs call:v8_contentMedia_pic_GPlus()?>
	<?cs elif:_imgMode == G_IMG_GRID_MODE?><?cs #多宫格模式展示?>
		<?cs call:v8_contentMedia_pic_Grid("big")?>
	<?cs elif:_imgMode == G_IMG_SMALL_MODE?><?cs #小图模式?>
		<?cs #这种应该只有被动feeds会有?>
		<?cs call:v8__limit_image_size(100, 75)?>
		<?cs call:v8_contentMedia_pic_default()?><?cs #默认的样子?>
	<?cs elif:_imgMode == G_IMG_SMALL_V8_MODE?><?cs #v8小图模式?>
		<?cs call:v8_contentMedia_pic_LeftSmallPic() ?>
	<?cs elif:_imgMode == G_IMG_GRID_MODE_SMALL?><?cs #小图多宫格模式展示?>
		<?cs call:v8_contentMedia_pic_Grid("small") ?>
	<?cs elif:_imgMode == G_IMG_SMART ?><?cs #智能排版?>
		<?cs call:contentMedia_smart() ?>
	<?cs else ?>
		<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
			<?cs call:v8__limit_image_size(100, 100)?>
		<?cs else ?>
			<?cs call:v8__limit_image_size(512, 512)?>
		<?cs /if?>
		<?cs call:v8_contentMedia_pic_default()?><?cs #默认的样子?>
	<?cs /if?>

<?cs /def?>

<?cs ####
	/**
	 *展示视频入口
	 */
?>
<?cs def:v8__contentMedia_videos()?>
	<?cs #视频就分为小图模式和大图模式，两个都要固定的高宽限制?>
	<?cs #注意:data_cntmedia_limitImgSize是定义的公共宏，在modata/contentbox_media.cs里面?>

	<?cs #XXX 视屏虽然是数组，但目前只有一个?>
	<?cs set:_imgMode = qfv.content.media.imgMode?>
	<?cs if:_imgMode == G_IMG_SMALL_MODE?>
		<?cs call:data_cntmedia_limitImgSize("content.media.video.0", 120, 120)?>
	<?cs else ?>
		<?cs call:data_cntmedia_limitImgSize("content.media.video.0", 400, 300)?>
	<?cs /if?>

	<?cs if:subcount(qfv.content.media.video)?>
		<?cs call:v8_video(qfv.content.media.video.0)?>
	<?cs /if?>
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
<?cs def:v8_contentMedia()?>
	<?cs #XXX 默认视频和图片是互斥的，不会放在一起展示。如果某一天产品有这个需求，再考虑修改展示层，数据层不要变?>
	<?cs if:subcount(qfv.content.voice)?><?cs #语音可以和图片一起展示?>
		<?cs call:v8_voice_player(qfv.content.voice) ?>
	<?cs /if?>

	<?cs if:subcount(qfv.content.media.video)?>
		<?cs call:v8__contentMedia_videos()?>
	<?cs elif:subcount(qfv.content.media.pic)?><?cs #有图片才展示?>
		<?cs call:v8__contentMedia_pictures()?>
	<?cs elif:subcount(qfv.content.media.music)?><?cs #有音乐才展示?>
		<?cs call:v8_music(qfv.content.media.music) ?>
	<?cs elif:subcount(qfv.content.magicemotion)?><?cs #有魔法表情才展示?>
		<?cs call:v8_attachmentPresenter(qfv.content.magicemotion) ?>
	<?cs elif:subcount(qfv.content.attach)?><?cs #有魔法表情才展示?>
		<?cs call:v8_attachViewer(qfv.content.attach) ?>
	<?cs /if?>
<?cs /def?>

