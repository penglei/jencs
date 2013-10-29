<?cs ####
	/**
	 * 智能排版 
	 */
?>

<?cs def:_set_smart_pic(pic,index,className, _cssText) ?>
	<?cs set:index = index +1 ?>
	<li class="img-item-<?cs var:index ?>">
		<?cs if: pic.type==2 ?>
			<?cs set:pic.action.className=pic.action.className+" img_gif " ?>
		<?cs /if ?>
		<?cs set:cssText = _cssText?>
		<?cs call:v8__contentMedia_display_pic_start(pic, className, cssText)?>
		<?cs call:v8_imageFlag(pic.type) ?>
		<?cs call:setSmartCenter(pic, pic.box_width, pic.box_height, "")?>
		<img src="<?cs var:pic.src ?>" 
			<?cs call:v8_echoClass(pic)?>
		<?cs call:v8_echoStyle(pic)?>
			 data-centerx = "<?cs var:pic.centerpoint_x  ?>" data-centery = "<?cs var:pic.centerpoint_y?>"
			 data-width = "<?cs var:pic.width  ?>" data-height = "<?cs var:pic.height?>" 
		/>
		<?cs call:v8__contentMedia_display_pic_end()?>
	</li>
<?cs /def ?>

<?cs def:contentMedia_smart() ?>

	<?cs #set:_imgMode = qfv.content.media.smartLayoutType?>
	<?cs #call:_print("$$subcount(qfv.content.media.pic)", subcount(qfv.content.media.pic)) ?>
	<?cs set:count = subcount(qfv.content.media.pic)?>
	<div class="tpl-item <?cs var:qfv.content.media.smartlayouttype ?>">
		<ul>
			<?cs loop:i = 0, count - 1, 1?>
				<?cs with:pic = qfv.content.media.pic[i]?>
					<?cs call:_set_smart_pic(pic, i, "", "") ?>
				<?cs /with?>
			<?cs /loop?>
		</ul>
	</div>
<?cs /def ?>