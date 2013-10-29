<?cs ####
	/**
	 * @fileoverview 设置meta值，以及一些共用的方法
	 * 在view中禁止使用qz_metadata中的数据，如果要用，在这里转换
	 * 
	 */
?>

<?cs #/*空间主人uin(登录态的uin)*/#?>
<?cs call:qfv("meta.loginuin", qz_metadata.meta.loginuin)?>
<?cs #/*uin的其它说明：qz_metadata.orgdata.uin是feeds源信息的uin(考虑转发链的时候)*/?>

<?cs #/*某条feed前面的头像uin(动作的触发者) */?>
<?cs call:qfv("meta.opuin", qz_metadata.meta.opuin)?>
<?cs #/*nickname，这个没有什么用，统一用username*/?>
<?cs #call:qfv("meta.opnickname", qz_metadata.meta.opuin_nick)?>

<?cs #"主贴uin"(动作产生新的“主贴”，这个的来源称作“原贴”)
	对于转发feeds，这个uin要注意。它应该是跟转发链中的uin
	该字段对应于原来的qz_metadata.metadata.uin
?>
<?cs if:subcount(qz_metadata.relybody)?><?cs #replybody是数组，并且最新的转发排在最前面?>
	<?cs call:qfv("meta.hostuin", qz_metadata.relybody.0.uin)?>
	<?cs call:qfv("meta.hostid", qz_metadata.relybody.0.mkey)?>
<?cs else ?>
	<?cs call:qfv("meta.hostuin", qz_metadata.orgdata.uin)?>
	<?cs call:qfv("meta.hostid", qz_metadata.orgdata.mkey)?>
<?cs /if?>
<?cs call:qfv("meta.subtype", qz_metadata.orgdata.subtype)?>

<?cs #/*跟平台无关的uin，它的意义对应着meta.opuin。
		如果是朋友网平台生成的feeds，这个userid就是朋友网的uin加密串，
		但opuin还是空间的uin(QQ号); 如果是空间的feeds，则userid == opuin
	*/ ?>
<?cs call:qfv("meta.userid", qz_metadata.meta.userid)?>
<?cs #/*跟userid具有相同的意思*/?>
<?cs call:qfv("meta.username", qz_metadata.meta.username)?>
<?cs call:qfv("meta.userhome", qz_metadata.meta.userhome)?>
<?cs call:qfv("meta.useravatar", qz_metadata.meta.protrait)?>

<?cs call:qfv("meta.otherflag", qz_metadata.meta.otherflag)?>
<?cs call:qfv("meta.appid", qz_metadata.meta.appid)?>
<?cs call:qfv("meta.typeid", qz_metadata.meta.typeid)?>
<?cs call:qfv("meta.scope", qz_metadata.meta.scope)?>
<?cs call:qfv("meta.abstime", qz_metadata.meta.abstime)?>
<?cs call:qfv("meta.bitmap", qz_metadata.meta.bitmap)?>
<?cs call:qfv("meta.yybitmap", qz_metadata.meta.yybitmap)?>
<?cs if:qz_metadata.orgdata.extendinfo.appid ?>
	<?cs call:qfv("meta.subappid", qz_metadata.orgdata.extendinfo.appid)?>
<?cs /if ?>

<?cs #判断转发?>
<?cs if:qz_metadata.relybody.0.uin?>
	<?cs #判断topicid的值是否有下划线，恶心啊，对说说脏数据兼容，6月底记得下掉?>
	<?cs if:qz_metadata.meta.appid == 311 &&  string.find(qz_metadata.relybody.0.topicid, "_")  == -1 ?>

	<?cs else?>
		<?cs #相册的转载topicId不在转发链，放在orgdata里面的?>
		<?cs if:qz_metadata.meta.appid == 4?>
			<?cs call:qfv("meta.topicid", qz_metadata.orgdata.topicid)?>
		<?cs else?>
			<?cs call:qfv("meta.topicid", qz_metadata.relybody.0.topicid)?>
		<?cs /if?>
	<?cs /if?>
<?cs else?>
	<?cs if:qz_metadata.meta.appid == 311 &&  string.find(qz_metadata.orgdata.topicid, "_")  == -1  ?>
	<?cs else?>
		<?cs call:qfv("meta.topicid", qz_metadata.orgdata.topicid)?>
	<?cs /if?>
<?cs /if?>
<?cs call:qfv("meta.feedstype", qz_metadata.meta.feedstype)?>
<?cs call:qfv("meta.feedoptype", qz_metadata.feedtype)?>

<?cs #feeds的拉取地方，目前只有feeds_html_module 会传入refer=2?>
<?cs set:SENCE_REQ_HOME = 2?>
<?cs set:g_refer = qz_metadata.meta.refer?>

<?cs #当前应该生成v8feeds的标志?>
<?cs #来自qz的请求走V6逻辑?>
<?cs if:g_refer == "qq_qz"?>
	<?cs set:g_isV8 = 0?>
<?cs else ?>
	<?cs if:g_refer == SENCE_REQ_HOME?><?cs #个人主页要根据feeds所有者来判断是不是v8feeds?>
		<?cs set:g_isV8 = bitmap_value_ex(qz_metadata.meta.unbitmap, 40, 1)?>
		<?cs set:g_visitor_isAlpha = bitmap_value_ex(qz_metadata.meta.hostbitmap, 58, 1)?>

		<?cs set:g_v8_home  = 0?>
		<?cs if:g_isV8 == 1?>
			<?cs if:g_visitor_isAlpha == 1?>
				<?cs set:g_v8_home = 1?>
				<?cs set:g_isV8 = 1?>
			<?cs else ?>
				<?cs set:g_isV8 = 0?>
			<?cs /if?>
		<?cs /if?>
	<?cs else ?>
		<?cs set:g_isV8 = bitmap_value_ex(qz_metadata.meta.hostunbitmap, 40, 1)?>
		<?cs set:g_visitor_isAlpha = bitmap_value_ex(qz_metadata.meta.hostbitmap, 58, 1)?>
	<?cs /if?>
<?cs /if?>
<?cs ####
	/**
	 * @description 添加全局元数据
	 * @param {String} key 设置键值
	 * @param {String} val 值
	 */
?>
<?cs def:meta(key, val)?>
	<?cs call:set("meta", key, val)?>
<?cs /def?>

<?cs ####
	/**
	 * @description 当前feed的appid
	 * @constant
	 */
	appid=""
?>
<?cs set:appid = qz_metadata.meta.appid?>
<?cs ####
	/**
	 * @description 当前feed的typeid
	 * @constant
	 */
	typeid=""
?>
<?cs set:typeid = qz_metadata.meta.typeid?>
<?cs ####
	/**
	 * @description 前端静态域名
	 * @constant
	 */
	g_siDomain=""
?>
<?cs set:g_siDomain="http://qzonestyle.gtimg.cn"?>

<?cs #动作类型配置?>
<?cs #{/*以下常量和后台意义对应*/#?>
<?cs set:UC_API_ACTION_RUBLISHED = 0?>		<?cs #/*发表*/#?>
<?cs set:UC_API_ACTION_COMMENTS=1?>			<?cs #/*评论*/#?>
<?cs set:UC_API_ACTION_REPLY=2?>			<?cs #/*回复*/#?>
<?cs set:UC_API_ACTION_FORWARD=3?>			<?cs #/*转载*/#?>
<?cs set:UC_API_ACTION_SAHRE=4?>			<?cs #/*分享*/#?>
<?cs set:UC_API_ACTION_MODIFY=5?>			<?cs #/*修改*/#?>
<?cs set:UC_API_ACTION_DELCOMMENTS=6?>		<?cs #/*删除评论*/#?>
<?cs set:UC_API_ACTION_DELREPLY=7?>			<?cs #/*删除回复*/#?>
<?cs set:UC_API_ACTION_AUDIT=8?>			<?cs #/*添加审核*/#?>
<?cs set:UC_API_ACTION_DELAUDIT=9?>			<?cs #/*删除审核*/#?>
<?cs set:UC_API_ACTION_MODIFYCOMMENTS=10?>	<?cs #/*修改的评论请求，不发tips不更新时间*/#?>
<?cs set:UC_API_ACTION_MODIFYREPLY=11?>		<?cs #/*修改的回复请求，不发tips不更新时间*/#?>
<?cs set:UC_API_ACTION_LIKE=12?>			<?cs #/*站内喜欢*/#?>
<?cs set:UC_API_ACTION_UNLIKE=13?>			<?cs #/*站内取消喜欢*/#?>
<?cs set:UC_API_ACTION_FORWARDCOMMENTS=14?>	<?cs #/*转发评论*/#?>
<?cs set:UC_API_ACTION_FORWARDREPLYS=15?>	<?cs #/*转发回复*/#?>
<?cs set:UC_API_ACTION_DELTOPIC=16?>		<?cs #/*删除主贴(对应photo侧删除单张照片)*/#?>
<?cs set:UC_API_ACTION_DELBATCH=17?>		<?cs #/*批量删除主贴(对应删除相册)*/#?>
<?cs set:UC_API_ACTION_DELAPPPSV=18?>		<?cs #/*删除app请求被动feeds*/#?>
<?cs #}//end?>

<?cs ####
	/**
	 * @class feed类型配置
	 * @description feed 动作类型，用来区分展示场景，而不是具体的某种业务feeds(一定要坚守这个原则)
	 * 对应qz_metadata.feedtype，保存在wup数据中。该维度是区分不同feed但数据相同的场景
	 */
	{}
?>
<?cs #{feed类型配置?>
<?cs set:UC_WUP_FEED_TYPE_SHOW_ACTALL = 6?>			<?cs #/*主动回复查看更多*/#?>
<?cs set:UC_WUP_FEED_TYPE_SHOW_PSVALL = 7?>			<?cs #/*被动查看更多*/#?>
<?cs set:UC_WUP_FEED_TYPE_SHOW_ACT_COMMALL = 8?>	<?cs #/*主动评论查看更多*/#?>
<?cs set:UC_WUP_FEED_TYPE_ACT = 100?>				<?cs #/*主动*/#?>
<?cs set:UC_WUP_FEED_TYPE_COMMPSV =101?>			<?cs #/*评论被动*/#?>
<?cs set:UC_WUP_FEED_TYPE_REPLYPSV =102?>			<?cs #/*回复被动*/#?>
<?cs set:UC_WUP_FEED_TYPE_ATMEPSV =103?>			<?cs #/*提到我的*/#?>
<?cs set:UC_WUP_FEED_TYPE_ACT_NOTIFYPSV =104?>	<?cs #/*通知(比如xx分享了我的日志)，后台的枚举变量是UC_WUP_FEED_TYPE_NOTIFYPSV*/#?>
<?cs set:UC_WUP_FEED_TYPE_RELATEPSV =105?>		<?cs #/*我参与的(长得和主动一模一样)*/#?>
<?cs set:UC_WUP_FEED_TYPE_AUDIT =106?>			<?cs #/*需要审核*/#?>
<?cs set:UC_WUP_FEED_TYPE_ATMEPSV_BY_COM =107?>	<?cs #/*评论提到我*/#?>
<?cs set:UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY =108?>	<?cs #/*回复提到我*/#?>
<?cs set:UC_WUP_FEED_TYPE_QZVIP_INVITE =109?>		<?cs #/*!我x!禁止!! 礼物特有的一个东西(开通空间系统送的礼物)，后台枚举变量是UC_WUP_FEED_TYPE_GIFT_QZVIPINVITE*/#?>
<?cs set:UC_WUP_FEED_TYPE_NEWCOMMENT =110?>		<?cs #/*有了新评论*/?>
<?cs set:UC_WUP_FEED_TYPE_RELY_ALSO = 111?>			<?cs #/*XXX也转发了*/?>
<?cs set:UC_WUP_FEED_TYPE_RELY_ATME = 112?>			<?cs #/*XXX转发了提到我的xx*/?>
<?cs set:UC_WUP_FEED_TYPE_REPLY_ALSO = 113?>		<?cs #/*XXX回复，A回复了，B再回复同一条评论，A收到的被动*/?>
<?cs set:UC_WUP_FEED_TYPE_GIFT_ACTION = 114?>		<?cs #/*!!礼物的生日礼物承载了活动礼物功能(因此没有mtypeid区分)，所以用这个区分*/?>
<?cs set:UC_WUP_FEED_TYPE_ACT_NOTIFYPSV_CUR =115?>	<?cs #/*通知(比如xx分享了我的日志)，通知给上一个转发人*/#?>
<?cs set:UC_WUP_FEED_TYPE_SHARETOME =116?>			<?cs #/*指定人可见被动*/#?>
<?cs set:UC_WUP_FEED_TYPE_ATMEINTOPIC =117?>			<?cs #/*提到我的主贴被评论回复*/#?>
<?cs set:UC_WUP_FEED_TYPE_COMM_ALSO =118?>			<?cs #/*评论的同级通知。我评论后，别人再次评论，我收到的我参与feed 对应说说typeid=7*/#?>
<?cs set:UC_WUP_FEED_TYPE_FWDME_GETCOMMREPLY =119?>			<?cs #/*我的原帖被人转发，这条转发贴被人评论回复后，我收到的我参与feed 对应说说typeid=8*/#?>
<?cs set:UC_WUP_FEED_TYPE_ATME_GETFWDCOMMREPLY =120?>			<?cs #/*@我的原帖被转发评论or回复*/#?>
<?cs set:UC_WUP_FEED_TYPE_ATME_FWD_GETFWDCOMMREPLY =121?>			<?cs #/*@我的转发帖被转发评论or回复*/#?>
<?cs set:UC_WUP_FEED_TYPE_FWD_NOTIFY =122?>			<?cs #/*转发节点内部的评论回复通知 对应说说的 typeid=12*/#?>
<?cs set:UC_WUP_FEED_VEDIO_AUDIT =123?>			<?cs #/*视频通过审核  对应说说的 typeid=24*/#?>
<?cs set:UC_WUP_FEED_BE_AT_IN_COMM =126?>		<?cs #/*在评论中被@，该评论被回复后收到的被动*/#?>
<?cs set:UC_WUP_FEED_TYPE_OTHER = 199?>			<?cs #/*其它*/#?>
<?cs #}//end?>


<?cs #{/*feeds 拉取类型，这个维度跟qz_metadata.meta是不同的维度，它是cgi拉取的时候设置的数据*/#?>
<?cs #对应qz_metadata.meta.feedstype。该维度是区分同一数据的拉取场景?>
<?cs set:UC_WUP_FEEDSTYPE_ACT=0?>			<?cs #/*主动*/#?>
<?cs set:UC_WUP_FEEDSTYPE_PSV=1?>			<?cs #/*被动*/#?>
<?cs set:UC_WUP_FEEDSTYPE_ABT=2?>			<?cs #/*我参与的*/#?>

<?cs #}//end?>

<?cs set:UC_MEDIA_TYPE_TXT = "txt" ?>		<?cs #/*文本*/#?>
<?cs set:UC_MEDIA_TYPE_PIC = "pic" ?>			<?cs #/*图片*/#?>
<?cs set:UC_MEDIA_TYPE_VEDIO = "vedio" ?>			<?cs #/*视频*/#?>
<?cs set:UC_MEDIA_TYPE_AUDIO = "audio" ?>			<?cs #/*音频*/#?>

<?cs #{权限控制位 ?>
<?cs set:UGCFLAG_ALL_PUBLIC = 1?>	<?cs #公开?>
<?cs set:UGCFLAG_QQFRIEND = 4?>		<?cs #QQ好友?>
<?cs set:UGCFLAG_WHITELIST = 16?>	<?cs #指定人?>
<?cs set:UGCFLAG_ONLYSELF = 64?>	<?cs #仅自己?>
<?cs #?>

<?cs #{why??为什么会有下面的代码? (les) ?>
<?cs if: subcount(qz_metadata.relybody) > 0 ?>
	<?cs set:g_CurHostUin=qz_metadata.relybody.0.uin ?>
<?cs else ?>
	<?cs set:g_CurHostUin=qz_metadata.orgdata.uin ?>
<?cs /if ?>

<?cs def:get_tuin_and_tid()?>
	<?cs if:subcount(qz_metadata.relybody) > 0?>
		<?cs set:get_tuin_and_tid.uin = qz_metadata.relybody[0].uin ?>
		<?cs set:get_tuin_and_tid.tid = qz_metadata.relybody[0].mkey ?>
	<?cs else ?>
		<?cs set:get_tuin_and_tid.uin = qz_metadata.orgdata.uin ?>
		<?cs set:get_tuin_and_tid.tid = qz_metadata.orgdata.mkey ?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 * @description 获取最后一个非自己的评论人
	 * @return get_last_comment_pos.ret
	 */
?>
<?cs def:get_last_comment_pos()?>
	<?cs set:_get_last_comment_pos.ret.found=0 ?>
	<?cs set:comment_count = subcount(qz_metadata.vt2body) ?>
	<?cs call:get_tuin_and_tid()?>
	<?cs loop: i = comment_count - 1, 0, -1?>
		<?cs if:get_tuin_and_tid.uin != qz_metadata.vt2body[i].uin && _get_last_comment_pos.ret.found==0 ?>
			<?cs set:_get_last_comment_pos.ret.found=1 ?>
			<?cs set:get_last_comment_pos.ret = i ?>
		<?cs /if?>
	<?cs /loop?>
<?cs /def?>

<?cs ####
	/**
	 * 获取备注
	 */
?>
<?cs def:get_remark(uin, defaultName)?>
	<?cs if:string.length(qz_metadata.remarkPool["u" + uin]) > 0 ?>
		<?cs set:get_remark.ret = qz_metadata.remarkPool["u" + uin]?>
	<?cs elif:string.length(defaultName) ?>
		<?cs set:get_remark.ret = defaultName?>
	<?cs else ?>
		<?cs set:get_remark.ret = uin?>
	<?cs /if ?>
<?cs /def?>
<?cs #}这是 ?>


<?cs #相册需要特殊处理 ?>
<?cs if: qz_metadata.meta.appid==4 ?>
	<?cs #相册没有mkey，common_widget.cs里面是直接用相册ID来做这个值的?>
	<?cs call:qfv("meta.hostid", qz_metadata.orgdata.albumdata.sAlbumId) ?>
	<?cs #单图需要加上图片id。判断单图只能这么蛋疼么 ?>
	<?cs if:(qz_metadata.feedtype==UC_WUP_FEED_TYPE_NEWCOMMENT || qz_metadata.meta.typeid==11 || qz_metadata.meta.typeid==15) 
	|| ((qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV) && qz_metadata.orgdata.subtype == 2) ?>
		<?cs call:qfv("meta.subid", qz_metadata.orgdata.itemdata[0].itemid) ?>
	<?cs /if ?>
	<?cs #相册较为特殊，当前的放在orgdata里面了?>
	<?cs call:qfv("meta.hostuin", qz_metadata.orgdata.uin)?>
<?cs /if ?>
