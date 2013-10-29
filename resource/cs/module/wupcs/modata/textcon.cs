<?cs ####
	/**
	 * 获得间距大小
	 * @param {Integer} mr,间距大小
	 * @return {Integer} 调整后的间距大小
	 */
	 funciton get_mr(mr)
?>
<?cs def:get_mr(mr)?>
	<?cs if:mr <= 5 && mr > 0?><?cs set:mr = 5?>
	<?cs elif:mr > 10?><?cs set:mr = 10?>
	<?cs elif:mr <0 && mr>=-5 ?><?cs set:mr = -5?>
	<?cs elif:mr <-5 && mr>=-10 ?><?cs set:mr = -10?>
	<?cs /if?>
	<?cs set:get_mr.ret = mr?>
<?cs /def?>

<?cs ####
	/**
	 * 在某一路径下生成文字组件数据
	 * @param {String} path 数据路径
	 * @param {String} text 文字
	 * @param {String} color 文字颜色(支持三种颜色:""|"normal", "tip", "link")
	 * @param {Integer} mr   文字与后面文字的间距: 5 、8 或者 10
	 */
	 funciton data_con_txt(path, text, color, mr)
?>
<?cs def:data_con_txt(path, text, color, mr)?>
	<?cs call:get_mr(mr)?>
	<?cs call:set(path, "mr", get_mr.ret)?>
	<?cs call:set(path, "type", "txt")?>
	<?cs call:set(path, "color", color)?>
	<?cs call:set(path, "text", text)?>
<?cs /def?>

<?cs ####
	/**
	 * 在某一路径下生成普通文字组建数据
	 * @param {String} path 数据路径
	 * @param {String} text 文字
	 * @param {String} color 文字颜色(支持三种颜色:""|"normal", "tip", "link")
	 * @param {Integer} mr   文字与后面文字的间距: 5 、8 或者 10
	 */
	 funciton data_con_url(path, text, url, color, mr)
?>
<?cs def:data_con_url(path, text, url, color, mr)?>
	<?cs call:data_con_txt(path, text, color, mr)?>
	<?cs call:set(path, "type", "url")?>
	<?cs call:set(path, "url", url)?>
<?cs /def?>

<?cs ####
	/**
	 * 在某一路径下生成头像组建数据
	 * @param {String} path 数据路径
	 * @param {String} uin
	 * @param {Integer} nickname  用户昵称
	 */
	 funciton data_con_avatar(path, uin, nickname)
?>
<?cs def:data_con_avatar(path, uin, nickname)?>
	<?cs call:set(path, "uin", uin)?>
	<?cs call:set(path, "nickname", nickname)?>
	<?cs call:set(path, "type", "avatar")?>
<?cs /def?>

<?cs ####
	/**
	 * 在某一路径下生成头像
	 * @param {String} path 数据路径
	 * @param {String} uin
	 * @param {String} who 用户所属平台 1:QZONE, 2:pengyou, 3:weibo
	 * @param {Integer} nickname  用户昵称
	 * @param {String} color 文字颜色(支持三种颜色:""|"normal", "tip", "link")
	 * @param {Integer} mr   文字与后面文字的间距:0, 5 、8 或者 10
	 */
	 funciton data_con_nick(path, uin, who, nickname, color, mr)
?>
<?cs def:data_con_nick(path, uin, who, nickname, color, mr)?>
	<?cs call:get_mr(mr)?>
	<?cs call:set(path, "mr", get_mr.ret)?>
	<?cs call:set(path, "uin", uin)?>
	<?cs call:set(path, "nickname", nickname)?>
	<?cs call:set(path, "color", color)?>
	<?cs call:set(path, "who", who)?>
	<?cs call:set(path, "type", "nick")?>
<?cs /def?>

<?cs ####
	/**
	 *给某个节点加指定名字的属性
	 */
?>
<?cs def:data_attrs(path, key, val)?>
	<?cs set:_index = subcount(qfv[path].attrs)?>
	<?cs set:_path = path + ".attrs." + _index?>
	<?cs set:qfv[_path] = 1?><?cs #数组必须先让这个路径有效，因此随意地设一个值?>
	<?cs call:set(_path, "key", key)?>
	<?cs call:set(_path, "value", val)?>
<?cs /def?>

<?cs ####
	/**
	 *给某个节点展示中额外的class
	 */
?>
<?cs def:data_addClassName(path, val)?>
	<?cs if:qfv[path].className?>
		<?cs set:val = qfv[path].className + " " + val?>
	<?cs /if?>
	<?cs call:set(path, "className", val)?>
<?cs /def?>

<?cs ####
	/**
	 *
	 */
?>
<?cs def:data_addStyle(path, val)?>
	<?cs call:set(path, "cssText", val)?>
<?cs /def?>

<?cs ####
	/**
	 *给某个昵称节点前加一个前缀，如"@"
	 */
?>
<?cs def:data_nick_addprefix(path, prefix)?>
	<?cs if:qfv[path].uin?>
		<?cs call:set(path, "prefix", prefix)?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * 段落开始和结束，必须配对使用
	 */
?>
<?cs def:data_con_p_start(path)?>
	<?cs call:set(path, "type", "p_start")?>
<?cs /def?>

<?cs def:data_con_p_end(path)?>
	<?cs call:set(path, "type", "p_end")?>
<?cs /def?>