<?cs set:data_extendinfo_index=0 ?>
<?cs #:
	/**/
	function extendinfo_index++(){}
?>

<?cs def:extendinfo_index++() ?>
	<?cs set:data_extendinfo_index=data_extendinfo_index+1 ?>
<?cs /def ?>
<?cs ####
	/**
	 * 扩展信息中的文字
	 */
?>
<?cs def:data_extendinfo_txt(index, text)?>
	<?cs call:data_con_txt("content.extendinfo.con." + data_extendinfo_index, text, "tip", 5)?>
	<?cs call:extendinfo_index++() ?>
<?cs /def?>

<?cs ####
	/**
	 * 扩展信息中的链接
	 */
?>
<?cs def:data_extendinfo_url(index, text, url)?>
	<?cs call:data_con_url("content.extendinfo.con." + data_extendinfo_index, text, url, "link", 0)?>
	<?cs call:extendinfo_index++() ?>
<?cs /def?>

<?cs ####
	/**
	 * 扩展信息中的昵称
	 */
?>
<?cs def:data_extendinfo_nick(index, uin, who, name)?>
	<?cs call:data_con_nick("content.extendinfo.con." + data_extendinfo_index, uin, who, name, "link", 0)?>
	<?cs call:extendinfo_index++() ?>
<?cs /def?>

<?cs ####
	/**
	 * 扩展信息中的文字带样式
	 */
?>
<?cs def:data_extendinfo_txt_style(index, text,color, mr)?>
	<?cs call:data_con_txt("content.extendinfo.con." + data_extendinfo_index, text, color, mr)?>
	<?cs call:extendinfo_index++() ?>
<?cs /def?>

<?cs ####
	/**
	 * 扩展信息中的链接带样式
	 */
?>
<?cs def:data_extendinfo_url_style(index, text, url,color, mr)?>
	<?cs call:data_con_url("content.extendinfo.con." + data_extendinfo_index, text, url, color, mr)?>
	<?cs call:extendinfo_index++() ?>
<?cs /def?>

<?cs ####
	/**
	 *来源
	 */
?>
<?cs def:data_extendinfo_source(extend)?>

	<?cs set:_platformid = qz_metadata.orgdata.platformid?>
	<?cs set:_subplatformid = qz_metadata.orgdata.platformsubid?>
	<?cs set:_useragent = qz_metadata.orgdata.useragent?>

	<?cs if:qz_source_type[_platformid][_subplatformid] ?>
		<?cs if:_useragent?>
			<?cs set:text = _useragent?>
			<?cs set:url = qz_source_type[_platformid][_subplatformid].url?>
		<?cs else?>
			<?cs set:text = qz_source_type[_platformid][_subplatformid]?>
			<?cs set:url = qz_source_type[_platformid][_subplatformid].url?>
		<?cs /if?>
	<?cs /if?>

	<?cs if:text?>
		<?cs set:text = "来自" + text ?>
		<?cs if:url ?>
			<?cs call:data_extendinfo_url_style(0, text, url,'tip',0) ?>
		<?cs else?>
			<?cs call:data_extendinfo_txt(0, text) ?>
		<?cs /if ?>
	<?cs /if?>

<?cs /def?>



<?cs #{//更简洁的接口?>

<?cs ####
	/**
	 * 图片数
	 */
?>
<?cs def:data_extendinfo_picnum(count)?>
    <?cs if:count>0 ?>
	    <?cs call:data_extendinfo_txt(0, "共有" + count + "张图片")?>
    <?cs /if ?>
<?cs /def?>

<?cs ####
	/**
	 * 转载
	 */
?>
<?cs def:data_extendinfo_forward(index, count)?>
	<?cs set:text = "转载(" + count + ")" ?>
	<?cs call:data_extendinfo_txt(0,text) ?>
<?cs /def?>

<?cs ####
	/**
	 * 原文被转
	 */
?>
<?cs def:data_extendinfo_share()?>
	<?cs set:data_extendinfo_share_count=qz_metadata.qz_data.key2.ZF.cnt-0+qz_metadata.qz_data.key2.WB.cnt ?>
	<?cs set:text = "原文被转(" + data_extendinfo_share_count + ")" ?>
	<?cs call:data_extendinfo_txt(0,text) ?>
<?cs /def?>

<?cs ####
	/**
	 * 时间
	 */
?>
<?cs def:data_extendinfo_time()?>
	<?cs if: qz_metadata.orgdata.ctime ?>
		<?cs set:text = qz_metadata.orgdata.strtime ?>
		<?cs call:data_extendinfo_txt(0,text) ?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * 原文转发
	 * 用于说说和微博
	 */
?>
<?cs def:data_extendinfo_retweet(index, url, count)?>
	<?cs set:text = "转发"?>
	<?cs if: count > 0 ?>
		<?cs set:text = text +"("+count+")" ?>
	<?cs /if?>
	<?cs set:url = url + "#action=forward" ?>
	<?cs call:data_extendinfo_url(0, text, url)?>
<?cs /def?>

<?cs ####
	/**
	 * 原文评论
	 * 用于说说和微博
	 */
?>
<?cs def:data_extendinfo_recomment(index, url, count)?>
	<?cs set:text = "评论"?>
	<?cs if: count > 0 ?>
		<?cs set:text = text +"("+count+")" ?>
	<?cs /if?>
	<?cs call:data_extendinfo_url(0, text, url)?>
<?cs /def?>

<?cs ####
	/**
	 * 礼物赠送次数
	 */
?>
<?cs def:data_extendinfo_gift_sendnum(index, count)?>
	<?cs set:text = "该礼物已被赠送" + count + "次" ?>
	<?cs call:data_extendinfo_txt(0, text)?>
<?cs /def?>

<?cs ####
	/**
	 * 收到生日礼物数
	 */
?>
<?cs def:data_extendinfo_gift_birthnum(index, count)?>
	<?cs set:text = "共收到" + count + "份生日礼物" ?>
	<?cs call:data_extendinfo_txt(0, text)?>
<?cs /def?>

<?cs ####
	/**
	 * 关注人数
	 */
?>
<?cs def:data_extendinfo_care(index, count)?>
	<?cs set:text = "共有" + count + "人关注" ?>
	<?cs call:data_extendinfo_txt(0, text)?>
<?cs /def?>

<?cs #}?>

<?cs #:
	/**/
	function showDynamic(){}
?>
<?cs set:DATA_LISTSAMEACTION_MAX = 3 ?>
<?cs def:data_listSameAction() ?>
	<?cs set:data_listSameAction_len=subcount(qz_metadata.relybody) ?>
	<?cs if: data_listSameAction_len>DATA_LISTSAMEACTION_MAX ?>
		<?cs set:data_listSameAction_len=DATA_LISTSAMEACTION_MAX ?>
	<?cs /if ?>
	<?cs set:qfv.content.extendinfo.relyPool.con=1 ?>
	<?cs set:_index=0?>
	<?cs loop:i=1, data_listSameAction_len-1, 1 ?>
		<?cs if: qz_metadata.relybody[i].uin!=qz_metadata.relybody[0].uin ?>
			<?cs call:data_con_nick("content.extendinfo.relyPool.con."+_index,
						qz_metadata.relybody[i].uin,
						qz_metadata.relybody[i].who,
						qz_metadata.relybody[i].nickname, "", "") ?>
			<?cs if: i < data_listSameAction_len - 1 ?>
				<?cs set:_index=_index+1 ?>
				<?cs call:data_con_txt("content.extendinfo.relyPool.con."+_index, "、", "tip", 0) ?>
				<?cs set:_index=_index+1 ?>
			<?cs /if ?>
		<?cs /if ?>
	<?cs /loop ?>
<?cs /def ?>

<?cs #:
	/**/
	function data_extend_time(){}
?>
<?cs def:data_extend_time(strtime) ?>
		<?cs call:data_extendinfo_txt(0,strtime) ?>
<?cs /def ?>

<?cs #:
	/**
	 * 目前lbs信息都是展示在正文extend区中的
	 *
	 * TODO 修改这个方法。这里应该生成展示用的数据，而不是把lbs转到另一个字段!!
	 * extendinfo区域的lbs信息就是一个文字popup，所以应该生成popup数据和相应的样式
	 *
	 * @param  {[type]} lbs [description]
	 * @return {[type]}     [description]
	 */
	function data_extend_content_lbs(lbs){}
?>
<?cs def:data_extend_content_lbs(lbs) ?>
	<?cs set:_path="content.lbs" ?>
	<?cs if: lbs.lbs_name ?>
		<?cs call:set(_path,"name",lbs.lbs_name) ?>
	<?cs /if ?>

	<?cs if: lbs.lbs_id ?>
		<?cs call:set(_path,"id",lbs.lbs_id) ?>
	<?cs /if ?>

	<?cs if: lbs.lbs_idname ?><?cs #这才是关键数据，有这个才显示?>
		<?cs call:qfv("content", 1)?>
		<?cs call:set(_path,"idName",lbs.lbs_idname) ?>
	<?cs /if ?>

	<?cs if: lbs.lbs_pos_x && lbs.lbs_pos_x != 900 ?>
		<?cs call:set(_path,"posx",lbs.lbs_pos_x) ?>
	<?cs /if ?>

	<?cs if: lbs.lbs_pos_y && lbs.lbs_pos_y != 900 ?>
		<?cs call:set(_path,"posy",lbs.lbs_pos_y) ?>
	<?cs /if ?>

	<?cs #call:data_popup(path, title, src, param, version, width, height, actiontype, statString)?>
<?cs /def ?>

