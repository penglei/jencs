<?cs ####
	/**
	 * 输出评论后缩略图
	 */
?>
<?cs def:v8_con_img(img)?>
	<img src="/ac/b.gif"
		<?cs if:img.alt?> alt="<?cs var:img.alt?>"<?cs /if?>
		<?cs if:img.name?> name="<?cs var:img.name?>"<?cs /if?>
		<?cs if:img.src?> onload="<?cs call:v8_contentBox_ReduceImgByShortEdge_onLoad(img,img.width,img.height,'') ?>"<?cs /if?>
	 />
<?cs /def?>
