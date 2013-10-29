<?cs def:_contentMedia_GPlus()?>
	<?cs call:contentMedia_start_imgbox()?>

		<?cs #g+模式第一张是大图，在数据层api里控制?>
		<?cs with:pic = qfv.content.media.pic[0]?>
		<?cs set:pic.action.cssText="width:400px;height:300px;overflow:hidden;" ?>
		<?cs set:pic.action.className="img_box_bigshow" ?>
		<?cs if: pic.type==2 ?>
			<?cs set:pic.action.className=pic.action.className+" img_gif" ?>
		<?cs /if ?>
		<?cs call:_contentMedia_display_pic_start(pic,
				"img_box_bigshow",
				"width:400px;height:300px;overflow:hidden;")?>
			<?cs call:imageFlag(pic.type) ?>
			<img src="/ac/b.gif"
				<?cs call:echoClass(pic)?>
				<?cs call:echoStyle(pic)?>
				 onload="<?cs escape:'html'?>
					<?cs call:contentBox_ReduceImgByLongEdge_onLoad(pic, 400, 300, "")?>
					<?cs /escape:'html'?>"
			 />
		<?cs call:_contentMedia_display_pic_end()?>
		<?cs /with?>

	<?cs #剩余的小图?>
	<?cs if:_media_pic_count > 1?>
	<?cs if: _media_pic_count>7 ?>
		<?cs set:_media_pic_count=7 ?>
	<?cs /if ?>
	<div class="img_box_preview">
		<?cs loop:i = 1, _media_pic_count - 1, 1?>
		<?cs with:pic = qfv.content.media.pic[i]?>
		<?cs set:pic.action.cssText="width:60px;height:45px;overflow:hidden;" ?>
		<?cs set:pic.action.className="bg2" ?>
		<?cs if: pic.type==2 ?>
			<?cs set:pic.action.className=pic.action.className+" img_gif" ?>
		<?cs /if ?>
		<?cs call:_contentMedia_display_pic_start(pic,
				"bg2",
				"width:60px;height:45px;overflow:hidden;")?>
			<?cs call:imageFlag(pic.type) ?>
			<img src="/ac/b.gif"
				<?cs call:echoClass(pic)?>
				<?cs call:echoStyle(pic)?>
				 onload="<?cs escape:'html'?>
					<?cs call:contentBox_ReduceImgByShortEdge_onLoad(pic, 60, 45, "")?>
					<?cs /escape:'html'?>"
			 />
		<?cs call:_contentMedia_display_pic_end()?>
		<?cs /with?>
		<?cs /loop?>

		<?cs #TODO,考虑通用性。?>
		<?cs #添加共有多少张图片的文字提示?>
		<?cs if:qfv.content.media.album.totalpic?>
			<?cs call:_contentMedia_display_pic_start(
					qfv.content.media.pic.1,
					"img_box_count", "")?>
				共<?cs var:qfv.content.media.album.totalpic?>张
			<?cs call:_contentMedia_display_pic_end()?>
		<?cs /if?>
	</div>
	<?cs /if?>
	<?cs call:contentMedia_end_imgbox()?>
<?cs /def?>
