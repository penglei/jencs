<?cs def:data_more_attach(param) ?>
	<?cs call:qfv("content.attach.attachcount",param.attachnum) ?>
	<?cs call:qfv("content.attach.attachtype",param.attachtype) ?>
	<?cs call:qfv("content.attach.attachurl",param.attachurl) ?>
<?cs /def ?>


<?cs #:
	/**
	 * 魔法表情
	 * @param  {Number} id 表情ID
	 * @param  {String} config 业务传过来的配置
	 * @return {[type]} [description]
	 */
	function data_attach(id){}
?>
<?cs def:data_attach(id,name,path,authorname,authoruin,size) ?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs call:qfv("content.attach.attachfile.id",id) ?>
	<?cs call:qfv("content.attach.attachfile.authorname",authorname) ?>
	<?cs call:qfv("content.attach.attachfile.authoruin",authoruin) ?>
	<?cs call:qfv("content.attach.attachfile.size",size) ?>
	<?cs call:qfv("content.attach.attachfile.name",name) ?>
	<?cs call:qfv("content.attach.attachfile.path",path) ?>
<?cs /def ?>
