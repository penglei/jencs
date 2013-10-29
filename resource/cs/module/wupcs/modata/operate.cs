<?cs def:data_opr_url(index, text, url,statString)?>
	<?cs set:_path = "operate.opr." + index?>
	<?cs if:g_isV8 ?>
		<?cs call:data_con_url(_path, text, url, "link", 0) ?>
		<?cs else ?>
		<?cs call:data_con_url(_path, text, url, "link", 10) ?>
	<?cs /if ?>
	<?cs call:qfv(_path+".hotclickPath",statString) ?>
	<?cs set:data_opr_url.ret.path = _path?>
<?cs /def?>

<?cs def:data_opr_txt(index, text)?>
	<?cs set:_path = "operate.opr." + index?>
	<?cs call:data_con_txt(_path, text, "link", 10)?>
	<?cs set:data_opr_txt.ret.path = _path?>
<?cs /def?>

<?cs ####
	/**
	 *以后考虑把各种按钮通过这个通用接口提供给数据层
	 *这就需要操作区按钮参数的规范。
	 *要考虑的东西很多，如计数、操作、文字的变化、附加数据
	 */
?>
<?cs def:data_opr_item(index, type, param, text)?>
<?cs /def?>

<?cs ####
	/**
	 * 按钮：通过审核
	 */
?>
<?cs def:data_opr_audit(src, param)?>
	<?cs set:_path = "operate" ?>
	<?cs call:set(_path, "auditbtn.src", src)?>
	<?cs call:set(_path, "auditbtn.param", param)?>
<?cs /def?>

<?cs ####
	/**
	 * 按钮：删除留言板
	 */
?>
<?cs def:data_opr_del(action, param)?>
	<?cs set:_path = "operate" ?>
	<?cs call:set(_path, "qz_delete.action", action)?>
	<?cs call:set(_path, "qz_delete.param", param)?>
<?cs /def?>
<?cs ####
	/**
	 * 按钮：删除动态
	 */
?>
<?cs def:data_opr_delfeed()?>
	<?cs if:(qz_metadata.meta.loginuin == qz_metadata.meta.opuin) || 
			(qz_metadata.meta.scope == 1) ||
			(!g_qz_is_auth && qz_metadata.meta.scope == 100) ?>
			<?cs set:_path = "operate" ?>
			<?cs call:set(_path, "delete_feed_opr.text", "删除")?>
		<?cs else ?>
		<?cs #:没有删除 ?>
	<?cs /if ?>

<?cs /def?>
<?cs ####
	/**
	 * 按钮：更多 屏蔽此人-取消关注等等
	 */
?>
<?cs def:data_opr_more()?>
	<?cs if:(qz_metadata.meta.appid == 403 && qz_metadata.meta.typeid == 10) ||
			(qz_metadata.meta.opuin == 20050606) || 
			(qz_metadata.meta.scope == 1) ||
			(!g_qz_is_auth && qz_metadata.meta.scope == 100) ||
			(qz_metadata.meta.appid == 333) ||
			(qz_metadata.meta.appid == 340)
			?>
		<?cs #:没有更多 ?>
	<?cs else ?>
		<?cs set:_path = "operate" ?>
		<?cs call:set(_path, "more_opr.text", "更多")?>
		<?cs call:set(_path, "more_opr.class", "none")?>
	<?cs /if ?>
<?cs /def?>

<?cs ####
	/**
	 * 按钮：屏蔽此人
	 */
?>
<?cs def:data_opr_prevent()?>
	<?cs if:(qz_metadata.meta.loginuin == qz_metadata.meta.opuin) || 
			(qz_metadata.meta.scope == 1) ||
			(!g_qz_is_auth && qz_metadata.meta.scope == 100) ||
			qz_metadata.shield_enable != 1 ||
			g_refer == SENCE_REQ_HOME ?>
		<?cs #:没有更多 ?>
	<?cs else ?>
		<?cs set:_path = "operate" ?>
		<?cs call:set(_path, "prevent.text", "隐藏")?>
	<?cs /if ?>
<?cs /def?>


<?cs ####
	/**
	 * 操作区popup，用于方便地生成popup型按钮
	 */
?>
<?cs def:data_opr_txtPopup(index, text, title, src, param, version, width, height,statString)?>
	<?cs set:_path = "operate.opr." + index?>
	<?cs call:data_con_txt(_path, text, "", 10)?>
	<?cs call:data_popup(
			_path + ".action",
			title, src, param, version, width, height, "", statString)?>
	<?cs call:data_popup_add_attr(_path + ".action", "mr", 10) ?>
	<?cs set:data_opr_txtPopup.ret = _path?>
<?cs /def?>

<?cs ####
	/**
	 *评论按钮应该和评论框一样，只是触发打开评论框
	 *依赖评论组件的常量
	 */
?>
<?cs def:data_opr_comment()?>
	<?cs set:count=qz_metadata.vt2count-0-filter_comment.ret.disabled_count ?>
	<?cs set:_oprcomment_path = "operate.comment_btn"?>
	<?cs call:set(_oprcomment_path, "comments_count", count)?>
	<?cs if:count > G_COMMENTS_MAX && g_comment_moreflag?>
		<?cs call:set(_oprcomment_path, "moreflag", 1)?>
	<?cs else ?>
		<?cs call:set(_oprcomment_path, "moreflag", 0)?>
		<?cs call:data_attrs(_oprcomment_path, "data-version", "6.3")?>
		<?cs #call:set(_oprcomment_path, "version", 6.3)?>
	<?cs /if?>
	<?cs if:count > 0?><?cs #考虑把这几个文字放在view里面做?>
		<?cs set:_comment_oprbtn_text = "评论(" + count + ")"?>
	<?cs else ?>
		<?cs set:_comment_oprbtn_text = "评论"?>
	<?cs /if?>
	<?cs call:set(_oprcomment_path, "text", _comment_oprbtn_text)?>
	<?cs call:set(_oprcomment_path, "url", "javascript:;")?>
	<?cs call:set(_oprcomment_path, "notarget", 1)?>
<?cs /def?>


<?cs #{转发按钮?>
<?cs ####
	/**
	 *内部使用，生成转发按钮
	 */
?>
<?cs def:_data_opr_create_forward(count)?>
	<?cs set:_op_forward_path = "operate.forward"?>

	<?cs call:qfv("operate.forward", 1)?>

	<?cs #元数据里面需要这货!!!?>
	<?cs call:meta("fwdcount", count)?>

	<?cs call:set(_op_forward_path, "count", count)?>

	<?cs call:set(_op_forward_path, "color", "link")?>
	<?cs call:set(_op_forward_path, "mr", 10)?>
<?cs /def?>

<?cs ####
	/**
	 *操作区默认转发按钮
	 */
?>
<?cs def:data_opr_forward()?>
	<?cs #param已经没有用了?>
	<?cs call:_data_opr_create_forward(qz_metadata.qz_data.key1.SYZF.cnt)?>
<?cs /def?>

<?cs ####
	/**
	 *业务生成计数的转发按钮
	 */
?>
<?cs def:data_opr_forwardCustom(count, extend1)?>
	<?cs call:_data_opr_create_forward(count)?>
<?cs /def?>

<?cs #}?>

<?cs ####
	/**
	 *在内容区添加浏览按钮
	 *浏览按钮实如果用通用按钮来处理，怎么弄呢?有点麻烦
	 */
?>
<?cs def:data_opr_visitor(param, count)?>
	<?cs #v8把这个从操作区移除?>
	<?cs set:_opr_visitor_path = "operate.visitor"?>

	<?cs #wup feeds数据都是新的，是不是qz_data.version可以不用管了呢?>
	<?cs call:set(_opr_visitor_path, "param", param)?>
	<?cs call:set(_opr_visitor_path, "count", count)?>
	<?cs call:set(_opr_visitor_path, "uin", qz_metadata.meta.uin)?>

	<?cs call:set(_opr_visitor_path, "url", "javascript:;")?>
	<?cs if:g_isV8?>
		<?cs call:set(_opr_visitor_path, "color", "tip")?>
	<?cs else ?>
		<?cs call:set(_opr_visitor_path, "color", "link")?>
	<?cs /if?>
	<?cs call:set(_opr_visitor_path, "mr", 10)?>
	<?cs set:data_opr_visitor.ret.path = _opr_visitor_path?>
<?cs /def?>

<?cs #相册权限 ?>
<?cs set:ALBUM_PRIV_PUBLIC = 1 ?>
<?cs set:ALBUM_PRIV_PASSWD = 2 ?>
<?cs set:ALBUM_PRIV_PRIVATE = 3 ?>
<?cs set:ALBUM_PRIV_FRIEND = 4 ?>
<?cs set:ALBUM_PRIV_QA = 5 ?>
<?cs set:ALBUM_PROV_SOME_FRI = 6 ?>

<?cs ####
	/**
	 *权限显示ICON
	 *现在这里支持两套权限，一套是相册的，一套是统一的
	 */
?>
<?cs def:data_privacy_icon()?>
	<?cs if:qz_metadata.orgdata.albumdata.iPrivacy ?><?cs #相册不标准的权限使用方式，先要兼容?>
	<?cs with:privacy = qz_metadata.orgdata.albumdata.iPrivacy ?>
		<?cs if:privacy == ALBUM_PROV_SOME_FRI ?><?cs #指定好友可见?>
			<?cs call:set("operate.privacy_icon", "text", "指定好友可见")?>
		<?cs elif:privacy == ALBUM_PRIV_PASSWD || privacy == ALBUM_PRIV_QA?>
			<?cs call:set("operate.privacy_icon", "text", "已回答过问题可见")?>
		<?cs elif:privacy == ALBUM_PRIV_PRIVATE?>
			<?cs call:set("operate.privacy_icon", "text", "仅自己可见")?>
		<?cs /if?>
	<?cs /with ?>
	<?cs else ?>
	<?cs with:accessright = qz_metadata.orgdata.accessright?>
		<?cs if:accessright == UGCFLAG_QQFRIEND ?>
			<?cs call:set("operate.privacy_icon", "text", "QQ好友可见")?>
		<?cs elif:accessright == UGCFLAG_WHITELIST ?>
			<?cs call:set("operate.privacy_icon", "text", "指定好友可见")?>
		<?cs elif:accessright == UGCFLAG_ONLYSELF?>
			<?cs call:set("operate.privacy_icon", "text", "仅自己可见")?>
		<?cs /if?>
	<?cs /with?>
	<?cs /if?>
<?cs /def?>
