<?cs def:_contentMedia_grid_one()?>
	<?cs call:contentMedia_start_imgbox()?>
	<?cs #生成图片?>
	<?cs with:pic=qfv.content.media.pic.0?>
		<?cs set:pic.action.cssText="width:400px;height:300px;overflow:hidden;" ?>
		<?cs if: pic.type==2 ?>
			<?cs set:pic.action.className="img_gif" ?>
		<?cs /if ?>
		<?cs call:_contentMedia_display_pic_start(pic, "", "")?>
			<?cs call:imageFlag(pic.type) ?>
			<img src="/ac/b.gif"
				<?cs call:echoClass(pic)?>
				<?cs call:echoStyle(pic)?>
				 onload="<?cs escape:'html'?>
					<?cs call:contentBox_ReduceImgByLongEdge_onLoad(pic, 400, 300, "")?><?cs #最大宽高400，300?>
					<?cs /escape:'html'?>"
			 />
		<?cs call:_contentMedia_display_pic_end()?>
	<?cs /with?>
	<?cs call:contentMedia_end_imgbox()?>
<?cs /def?>

<?cs def:_contentMedia_grid_two()?>
	<div class="img_box" style="margin-top:4px;margin-bottom:4px;float:none;width:408px;">
		<?cs each:pic = qfv.content.media.pic?>
			<?cs set:pic.action.cssText="width:196px;height:196px;overflow:hidden;display:inline-block;" ?>
			<?cs set:pic.action.className="img_box_200x200 ui_mr8" ?>
			<?cs if: pic.type==2 ?>
				<?cs set:pic.action.className=pic.action.className+" img_gif" ?>
			<?cs /if ?>
			<?cs call:_contentMedia_display_pic_start(pic, 
					"img_box_200x200 ui_mr8", 
					"width:196px;height:196px;overflow:hidden;")?>
			<?cs call:imageFlag(pic.type) ?>
			<img src="/ac/b.gif"
				<?cs call:echoClass(pic)?>
				<?cs call:echoStyle(pic)?>
				 onload="<?cs escape:'html'?>
					<?cs call:contentBox_ReduceImgByShortEdge_onLoad(pic, 196, 196, "")?><?cs #最大宽高都为196?>
					<?cs /escape:'html'?>"
			 />
			<?cs call:_contentMedia_display_pic_end()?>
		<?cs /each?>
	</div>
<?cs /def?>

<?cs def:_contentMedia_grid_two_small()?>
	<div class="img_box" style="margin-top:4px;margin-bottom:4px;float:none;width:408px;">
		<?cs each:pic = qfv.content.media.pic?>
			<?cs set:pic.action.cssText="width:128px;height:128px;overflow:hidden;display:inline-block;" ?>
			<?cs set:pic.action.className="img_box_120x120 ui_mr8" ?>
			<?cs if: pic.type==2 ?>
				<?cs set:pic.action.className=pic.action.className+" img_gif" ?>
			<?cs /if ?>
			<?cs call:_contentMedia_display_pic_start(pic, 
					"img_box_120x120 ui_mr8", 
					"width:128px;height:128px;overflow:hidden;")?>
			<?cs call:imageFlag(pic.type) ?>
			<img src="/ac/b.gif"
				<?cs call:echoClass(pic)?>
				<?cs call:echoStyle(pic)?>
				 onload="<?cs escape:'html'?>
					<?cs call:contentBox_ReduceImgByShortEdge_onLoad(pic, 128,128, "")?><?cs #最大宽高都为128?>
					<?cs /escape:'html'?>"
			 />
			<?cs call:_contentMedia_display_pic_end()?>
		<?cs /each?>
	</div>
<?cs /def?>

<?cs def:_contentMedia_grid_four()?>
	<?cs def:_contentMedia_grid_four_inline(start, end)?>
		<div class="img_box" style="margin-top:4px;margin-bottom:4px;float:none;width:408px;">
			<?cs loop:_k = start, end, 1?>
				<?cs #不用判断，因为肯定有4张?>
				<?cs with:pic=qfv.content.media.pic[_k]?>
					<?cs set:pic.action.cssText="width:196px;height:196px;overflow:hidden;display:inline-block;" ?>
					<?cs set:pic.action.className="img_box_200x200 ui_mr8 bg2" ?>
					<?cs if: pic.type==2 ?>
						<?cs set:pic.action.className=pic.action.className+" img_gif" ?>
					<?cs /if ?>
					<?cs call:_contentMedia_display_pic_start(pic, 
							"img_box_200x200 ui_mr8 bg2", 
							"width:196px;height:196px;overflow:hidden;")?>
					<?cs call:imageFlag(pic.type) ?>
					<img src="/ac/b.gif"
						<?cs call:echoClass(pic)?>
						<?cs call:echoStyle(pic)?>
						 onload="<?cs escape:'html'?>
							<?cs call:contentBox_ReduceImgByShortEdge_AlignCenter_onLoad(pic, 196, 196, "")?>
							<?cs /escape:'html'?>"
					 />
					<?cs call:_contentMedia_display_pic_end()?>
				<?cs /with?>
			<?cs /loop?>
		</div>
	<?cs /def?>
	<?cs call:_contentMedia_grid_four_inline(0,1)?>
	<?cs call:_contentMedia_grid_four_inline(2,3)?>
<?cs /def?>

<?cs def:_contentMedia_grid_nine()?>
	<?cs def:_contentMedia_grid_nine_inline(start, end)?>
		<div class="img_box" style="margin-top:4px;margin-bottom:4px;float:none;width:408px;">
			<?cs if:end > _media_pic_count - 1?>
				<?cs set:end = _media_pic_count - 1?>
			<?cs /if?>
			<?cs loop:_k = start, end, 1?>
				<?cs #不用判断，因为肯定有4张?>
				<?cs with:pic=qfv.content.media.pic[_k]?>
					<?cs set:pic.action.cssText="width:128px;height:128px;overflow:hidden;display:inline-block;" ?>
					<?cs set:pic.action.className="img_box_120x120 ui_mr8 bg2" ?>
					<?cs if: pic.type==2 ?>
						<?cs set:pic.action.className=pic.action.className+" img_gif" ?>
					<?cs /if ?>
					<?cs call:_contentMedia_display_pic_start(pic, 
							"img_box_120x120 ui_mr8 bg2", 
							"width:128px;height:128px;overflow:hidden;")?>
					<?cs call:imageFlag(pic.type) ?>
					<img src="/ac/b.gif"
						<?cs call:echoClass(pic)?>
						<?cs call:echoStyle(pic)?>
						 onload="<?cs escape:'html'?>
							<?cs call:contentBox_ReduceImgByShortEdge_AlignCenter_onLoad(pic, 128, 128, "")?>
							<?cs /escape:'html'?>"
					 />
					<?cs call:_contentMedia_display_pic_end()?>
				<?cs /with?>
			<?cs /loop?>
		</div>
	<?cs /def?>

	<?cs call:_contentMedia_grid_nine_inline(0, 2)?>
	<?cs if:_media_pic_count > 3?>
		<?cs call:_contentMedia_grid_nine_inline(3, 5)?>
	<?cs /if?>
	<?cs if:_media_pic_count > 5?>
		<?cs call:_contentMedia_grid_nine_inline(6, 8)?>
	<?cs /if?>
<?cs /def?>
