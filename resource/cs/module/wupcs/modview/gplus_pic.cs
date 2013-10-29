<?cs def:_contentMedia_GPlus()?>
	<?cs call:contentMedia_start_imgbox()?>

		<?cs #g+模式第一张是大图，在数据层api里控制?>
		<?cs with:pic = qfv.content.media.pic[0]?>
		<?cs set:pic.action.className="img_box_bigshow" ?>
		<?cs if: pic.type==2 ?>
			<?cs set:pic.action.className=pic.action.className+" img_gif" ?>
		<?cs /if ?>

		<?cs if:pic.height >= 800 && pic.height/pic.width>=3 ?>
			<?cs #长图?>
			<?cs if:pic.width>400 ?>
				<?cs set:_width=400 ?>
			<?cs else ?>
				<?cs set:_width=pic.width ?>
			<?cs /if ?>
			<?cs call:_contentMedia_display_pic_start(pic,
					"img-long-pic",
					"width:"+_width+"px;height:400px;overflow:hidden;")?>
			<?cs call:imageFlag(4) ?>
		<?cs else ?>
			<?cs #普通图片?>
			<?cs call:_contentMedia_display_pic_start(pic, "img_box_bigshow", "")?>
			<?cs call:imageFlag(pic.type) ?>
		<?cs /if ?>

		<?cs if:pic.width==0 && pic.height==0 ?>
				<img src="/ac/b.gif"
					<?cs call:echoClass(pic)?>
					<?cs call:echoStyle(pic)?>
					 onload="<?cs escape:'html'?>
						<?cs call:contentBox_ReduceImgByLongEdge_onLoad(pic, 400, 300, "")?>
						<?cs /escape:'html'?>"
				 />
		<?cs else ?>
			<?cs call:ugc_url_check(pic.src,0) ?>
			<?cs if:pic.width > pic.height ?>
				<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" onload="<?cs escape:'html'?>
						<?cs call:contentBox_ReduceImgByLongEdge_onLoad(pic, 400, 300, "")?>
						<?cs /escape:'html'?>"/>
			<?cs else ?>
				<?cs #高大于宽 ?>
				<?cs if:pic.height >= 800 && pic.height/pic.width>=3 ?>
					<?cs #长图 ?>
					<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" style="width:<?cs if:pic.width>400 ?>400<?cs else ?><?cs var:pic.width ?><?cs /if ?>px;">
				<?cs else ?>
					<img src="<?cs call:ugc_as_html(ugc_url_check.ret,1,1) ?>" style="height:<?cs if:pic.height>300 ?>300<?cs else ?><?cs var:pic.height ?><?cs /if ?>px;">
				<?cs /if ?>
			<?cs /if ?>
		<?cs /if ?>
		<?cs call:_contentMedia_display_pic_end()?>
		<?cs /with?>

	<?cs #剩余的小图?>
	<?cs if:_media_pic_count > 1?>
	<?cs if: _media_pic_count > 7 ?>
		<?cs set:_media_pic_count=7 ?>
	<?cs /if ?>
	<?cs set:_loopcount=subcount(qfv.content.media.pic) ?>
	<?cs if: _loopcount>7 ?>
		<?cs set:_loopcount=7 ?>
	<?cs /if ?>
	<?cs set:_count=0 ?><?cs #渲染图片计数?>
	<div class="img_box_preview">
		<?cs #这里_loopcount不要减一因为图片后面有可能还跟一个展示计数的节点 ?>
		<?cs loop:i = 1, _loopcount, 1?>
		<?cs with:pic = qfv.content.media.pic[i]?>
		<?cs if: pic.action.type=="tips" ?>
			<?cs set:_param.text=pic.text ?>
			<?cs set:_param.type="url" ?>
			<?cs set:_param.url=qfv.content.media.pic[1].action.url ?>
			<?cs set:_param.className="img_box_count" ?>
			<?cs call:con_url_start(_param) ?>
				<?cs var:pic.action.tips ?>
			<?cs call:_contentMedia_display_pic_end()?>
		<?cs elif:_count<_media_pic_count ?>
			<?cs set:_count=_count+1 ?>
			<?cs set:pic.action.cssText="width:60px;height:45px;overflow:hidden;" ?>
			<?cs set:pic.action.className="bg2" ?>
			<?cs if: pic.type==2 ?>
				<?cs set:pic.action.className=pic.action.className+" img_gif" ?>
			<?cs /if ?>
			<?cs call:_contentMedia_display_pic_start(pic, "bg2", "width:60px;height:45px;overflow:hidden;")?>
				<?cs call:imageFlag(pic.type) ?>
				<img src="/ac/b.gif"
					<?cs call:echoClass(pic)?>
					<?cs call:echoStyle(pic)?>
					 onload="<?cs escape:'html'?>
						<?cs call:contentBox_ReduceImgByShortEdge_onLoad(pic, 60, 45, "")?>
						<?cs /escape:'html'?>"
				 />
			<?cs call:_contentMedia_display_pic_end()?>
		<?cs /if ?>
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
