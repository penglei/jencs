<?cs ####
	/**
	 * @class 语音播放器
	 * @description 目前用于语音说说
	 */
	语音播放器={};
?>

<?cs ####
	/**
	 * @memberOf 语音播放器
	 * @private
	 * @description 当前设置语音数据块下标
	 */
?>
<?cs set:_data_voice_index=-1 ?>
<?cs #:
	/**
	 * @memberOf 语音播放器
	 * @private
	 * @description 把当前指向语音数据块下标往后加1
	 */
	_data_voice_next:function(){}
?>
<?cs def:_data_voice_next() ?>
	<?cs set:_data_voice_index=_data_voice_index+1 ?>
<?cs /def ?>

<?cs #:
	/**
	 * @memberOf 语音播放器
	 * @description 设置语音播放器在正文中的布局.如果没有调用这个方法，默认block
	 * @param  {String} layout 布局方式："block","inline"
	 */
	data_set_voice_layout:function(layout){}
?>
<?cs def:data_set_content_voice_layout(layout) ?>
	<?cs call:qfv("content.voice.layout",layout) ?>
<?cs /def ?>

<?cs #:
	/**
	 * 语音说说
	 * @param  {Number} id 表情ID
	 * @param  {String} config 业务传过来的配置
	 * @return {[type]}           [description]
	 */
	data_voice:function (id){}
?>
<?cs def:data_voice(id,voice_url,duration,cipher,extendData) ?>
	<?cs if: _data_voice_index<0 ?>
		<?cs call:_data_voice_next() ?>
	<?cs /if ?>
	<?cs set:data_voice_cur_path="content.voice."+_data_voice_index ?>
	<?cs call:qfv(data_voice_cur_path+".id",id) ?>
	<?cs call:qfv(data_voice_cur_path+".url",voice_url) ?>
	<?cs call:qfv(data_voice_cur_path+".duration",duration) ?>
	<?cs call:qfv(data_voice_cur_path+".cipher",cipher) ?>
	<?cs call:qfv(data_voice_cur_path+".expire_time",expire_time) ?>
	<?cs call:_data_voice_next() ?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
<?cs /def ?>
