<?cs ####
	/**
	 * 在某一路径下生成普通文字组建数据
	 * @param {String} path 数据路径
	 * @param {String} title popup弹框的标题
	 * @param {String} src popup链接地址
	 * @param {String} param popup链接地址的参数
	 * @param {Integer} version popup版本控制，用以前台做不同处理
	 * @param {Integer} width popup初始宽度
	 * @param {Integer} height popup初始高度
	 * @param {String} extend_1 可扩展字段1
	 * @param {String} extend_2 可扩展字段2
	 */
	 funciton data_popup(path, title, src, param, version, width, height, actiontype, statString)
?>
<?cs def:data_popup(path, title, src, param, version, width, height, actiontype, statString)?>
	<?cs if: actiontype ?>
		<?cs set:data_popup_type=actiontype ?>
	<?cs else ?>
		<?cs set:data_popup_type="popup" ?>
	<?cs /if ?>
	<?cs call:set(path, "type", data_popup_type)?>
	<?cs call:set(path, "title", title)?>
	<?cs call:set(path, "src", src)?>
	<?cs call:set(path, "param", param)?>
	<?cs call:set(path, "version", version)?>
	<?cs call:set(path, "width", width)?>
	<?cs call:set(path, "height", height)?>
	<?cs if: statString ?>
		<?cs call:set(path, "hotclickPath", statString)?>
	<?cs /if ?>

<?cs /def?>

<?cs #:
	/**/
	function data_popup_add_attr(path,key,value){}
?>
<?cs def:data_popup_add_attr(path,key,value) ?>
	<?cs call:set(path, key, value)?>
<?cs /def ?>