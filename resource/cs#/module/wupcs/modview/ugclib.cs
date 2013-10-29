<?cs #:
	/**
	 * ugc用在json中用这个处理下
	 * @param  {[type]} ugc_str [ugc内容]
	 * @param  {number} charset 0是gbk ，1是utf8
	 * @param {number} needPrint 是否要打印出来，0是不打印，1是打印
	 * @return {string} ugc_as_json.ret 处理后的ugc内容
	 */
	function ugc_as_json(ugc_str,charset,needPrint){}
?>
<?cs def:ugc_as_json(ugc_str,charset,needPrint) ?>
	<?cs set:ugc_as_json.ret = json_encode(ugc_str,charset) ?>
	<?cs if: needPrint ?>
		<?cs var:ugc_as_json.ret ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:
	/**
	 * ugc用在html中用这个处理下
	 * @param  {[type]} ugc_str [ugc内容]
	 * @param  {number} charset 0是gbk ，1是utf8
	 * @param {number} needPrint 是否要打印出来，0是不打印，1是打印
	 * @return {string} ugc_as_html.ret 处理后的ugc内容
	 */
	function ugc_as_html(ugc_str,charset,needPrint){}
?>
<?cs def:ugc_as_html(ugc_str,charset,needPrint) ?>
	<?cs set:ugc_as_html.ret = html_encode(ugc_str,charset) ?>
	<?cs if: needPrint ?>
		<?cs var:ugc_as_html.ret ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:
	/**
	 * ugc是作为json数据的一个属性值，且该jsonhtml字符串是会被放在html代码中，比如节点属性等，需要用这个处理下
	 * @param  {[type]} ugc_str [ugc内容]
	 * @param  {number} charset 0是gbk ，1是utf8
	 * @param {number} needPrint 是否要打印出来，0是不打印，1是打印
	 * @return {string} ugc_as_json_in_html.ret 处理后的ugc内容
	 */
	function ugc_as_json_in_html(ugc_str,charset,needPrint){}
?>
<?cs def:ugc_as_json_in_html(ugc_str,charset,needPrint) ?>
	<?cs call:ugc_as_json(ugc_str,charset,0) ?>
	<?cs call:ugc_as_html(ugc_as_json.ret,charset,needPrint) ?>
	<?cs set:ugc_as_json_in_html.ret = ugc_as_html.ret ?>
<?cs /def?>

<?cs #:
	/**
	 * ugc是放在html代码中的，且该html代码是用来作为json数据的一个属性值，需要用这个处理下
	 * @param  {[type]} ugc_str [ugc内容]
	 * @param  {number} charset 0是gbk ，1是utf8
	 * @param {number} needPrint 是否要打印出来，0是不打印，1是打印
	 * @return {string} ugc_as_html_in_json.ret 处理后的ugc内容
	 */
	function ugc_as_html_in_json(ugc_str,charset,needPrint){}
?>
<?cs def:ugc_as_html_in_json(ugc_str,charset,needPrint) ?>
	<?cs call:ugc_as_html(ugc_str,charset,0) ?>
	<?cs call:ugc_as_json(ugc_as_html.ret,charset,needPrint) ?>
	<?cs set:ugc_as_html_in_json.ret = ugc_as_json.ret ?>
<?cs /def?>

<?cs #:
	/**
	 * ugc中过来的url。需要用这个方法处理下，我们不允许ugc中传javascript:伪协议过来
	 * @param  {[type]} url 目标串
	 * @param {number} needPrint 是否要打印出来，0是不打印，1是打印
	 * @return {string} ugc_url_check.ret 处理后的url
	 */
	function ugc_url_check(url,needPrint){}
?>
<?cs def:ugc_url_check(url,needPrint) ?>
	<?cs if: url ?>
		<?cs set:ugc_url_check.ret=string_firstwords_replace(url,"javascript:","http://") ?>
		<?cs set:ugc_url_check.ret=string_firstwords_replace(ugc_url_check.ret,"vbscript:","http://") ?>
	<?cs else ?>
		<?cs set:ugc_url_check.ret="" ?>
	<?cs /if ?>
	
	<?cs if: needPrint ?>
		<?cs var:ugc_url_check.ret ?>
	<?cs /if ?>
<?cs /def?>