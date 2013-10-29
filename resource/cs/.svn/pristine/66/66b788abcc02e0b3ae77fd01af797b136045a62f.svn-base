<?cs #-------------------- ?>
<?cs #{writeUserName.cs}?>
<?cs #-------------------- ?>

<?cs def:writeUserName(uin,defaultName) ?>
	<?cs if:string.length(qz_metadata.remarkPool['u'+uin])>0 ?>
		<?cs var:html_decode(qz_metadata.remarkPool['u'+uin],1) ?>
	<?cs else ?>
		<?cs var:html_decode(defaultName, 1) ?>
	<?cs /if ?>
<?cs /def ?>


<?cs #-------------------- ?>
<?cs #{user_link.cs}?>
<?cs #-------------------- ?>

<?cs #:用户链接组件 ?>
<?cs #:prefix 是前缀啊例如@ ?>
<?cs def:userLink(user, prefix) ?>
	<?cs if:user.who == 1 || user.type == 1?>
		<a href="http://user.qzone.qq.com/<?cs var:user.uin+'?active_channel=1&active_type=4&active_mode='+stat_flag_nick ?>" style="color: #577DA8;text-decoration:none;" target="_blank">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<span><?cs call:writeUserName(user.uin,user.name) ?></span></a>
	<?cs elif:user.who == 2 || user.type == 2 ?>
		<a href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:user.uin ?>" style="color: #577DA8;text-decoration:none;" target="_blank">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<?cs var:html_encode(user.name, 1) ?></a>
	<?cs elif:user.who == 3 || user.type == 3 ?>
		<a href="http://rc.qzone.qq.com/myhome/weibo/profile/<?cs var:user.uin ?>" style="color: #577DA8;text-decoration:none;" target="_blank">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<?cs var:html_encode(user.name, 1) ?></a>
	<?cs else ?>
		<?cs var:html_encode(user.name, 1) ?>
	<?cs /if ?>
<?cs /def ?>


<?cs #-------------------- ?>
<?cs #{rich_content_title.cs}?>
<?cs #-------------------- ?>

<?cs #:富文本内容 ?>
<?cs def:richContent-title(cons) ?>
	<?cs def:richContentTitle-items(item) ?>
		<?cs if:item.type == 'nick' ?>
			<?cs call:userLink(item , '@') ?>
		<?cs elif:item.type == 'url' ?>
			<a style="color: #577DA8;text-decoration:none;" href="<?cs var:item.url ?>" target="_blank"><?cs if:item.text ?><?cs var:item.text ?><?cs else ?><?cs var:item.url ?><?cs /if ?></a>
		<?cs elif:item.type == 'qz_app' ?>
			<?cs var:item.text ?>
		<?cs else ?>
			<?cs var:item ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 || string.length(cons.con.0) > 0 ?>
		<?cs loop:i = 0, subcount(cons.con) - 1, 1 ?>
			<?cs call:richContentTitle-items(cons.con[i]) ?>
		<?cs /loop ?>
	<?cs elif:cons.con || subcount(cons.con) > 0 || string.length(cons.con) > 0 ?>
			<?cs call:richContentTitle-items(cons.con) ?>
	<?cs /if ?>
<?cs /def ?>


<?cs #-------------------- ?>
<?cs #{rich_title.cs}?>
<?cs #-------------------- ?>

<?cs #:富文本内容 ?>
<?cs def:richTitle(cons) ?>
	<?cs #:i:当前项编号,length:总长度, 用来计算是否闭合标签?>
	<?cs def:richTitle-items(item, i, length) ?>
		<?cs if:item.type == 'nick' ?>
			<?cs if:qz_richtitle_flag == 1 ?>
				</span>
			<?cs /if ?>
			<?cs call:userLink(item , '@') ?>
			<?cs set:qz_richtitle_flag = 0 ?>
		<?cs elif:item.type == 'name' ?>
			<?cs if:qz_richtitle_flag == 1 ?>
				</span>
			<?cs /if ?>
			<span class="ui_mr5"><?cs call:userLink(item , '') ?></span>
			<?cs set:qz_richtitle_flag = 0 ?>		
		<?cs elif:item.type == 'url' ?>
			<?cs if:qz_richtitle_flag == 1 ?>
				</span><a style="color: #577DA8;text-decoration:none;" href="<?cs var:item.url+'?active_channel=1&active_type=4&active_mode='+stat_flag ?>" target="_blank"><?cs var:item.text ?></a>
				<?cs set:qz_richtitle_flag = 0 ?>
			<?cs elif:qz_richtitle_flag == 0 ?>
				<a style="color: #577DA8;text-decoration:none;" href="<?cs var:item.url+'?active_channel=1&active_type=4&active_mode='+stat_flag ?>" target="_blank"><?cs var:item.text ?></a>
			<?cs /if ?>
		<?cs elif:item.type == 'qz_popup' ?>
			<?cs if:qz_richtitle_flag == 1 ?>
				</span><span class="ui_mr5"><qz:popup 
					title="<?cs var:item.qz_popup.title ?>" 
					height="<?cs var:item.qz_popup.height ?>" 
					width="<?cs var:item.qz_popup.width ?>" 
					version="<?cs var:item.qz_popup.version ?>" 
					src="<?cs var:item.qz_popup.src ?>" 
					param="<?cs var:item.qz_popup.param ?>"><?cs var:item.text ?>
				</qz:popup></span>
				<?cs set:qz_richtitle_flag = 0 ?>
			<?cs elif:qz_richtitle_flag == 0 ?>
				<span class="ui_mr5"><qz:popup 
					title="<?cs var:item.qz_popup.title ?>" 
					height="<?cs var:item.qz_popup.height ?>" 
					width="<?cs var:item.qz_popup.width ?>" 
					version="<?cs var:item.qz_popup.version ?>" 
					src="<?cs var:item.qz_popup.src ?>" 
					param="<?cs var:item.qz_popup.param ?>"><?cs var:item.text ?>
				</qz:popup></span>
			<?cs /if ?>
		<?cs elif:item.type == 'qz_app'?><?cs #一段时间后可以去掉了 ?>
			<?cs if:qz_richtitle_flag == 1 ?></span><?cs /if ?>
			<a target="_blank" href="http://rc.qzone.qq.com/myhome/<?cs var:item.aid?>"><?cs var:item.text ?></a>
			<?cs if:i < subcount(cons.con) - 1 ?>、<?cs /if?>
		<?cs elif:item.type == 'auth_nick' ?>
			<?cs if:qz_richtitle_flag == 1 ?></span><?cs /if ?>
			<?cs call:userLink(item, '') ?>
			<img style="vertical-align: -2px;" src="/ac/qzone_v5/client/auth_icon.png" title="腾讯认证" alt="腾讯认证" />
			<?cs set:qz_richtitle_flag = 0 ?>
		<?cs elif:item.type == 'script' ?>
			<?cs if:qz_richtitle_flag == 1 ?></span><?cs /if ?>
			<?cs if:item.biz == "mall"?>
				<a class="c_tx ui_mr5" href="javascript:;" onclick="QZONE.ICFeeds.Interface.checkForDress2(this,'<?cs var:item.param[0]?>', '<?cs var:item.param[1] ?>')">
					<?cs var:item.text ?>
				</a>
			<?cs /if ?>
			<?cs set:qz_richtitle_flag = 0 ?>
		<?cs elif:item.type == 'text' ?>
			<?cs if:qz_richtitle_flag == 1 ?>
				</span>
			<?cs /if ?>
			<a target="_blank" href="<?cs var:qz_metadata.qz_data.key1+'?active_channel=1&active_type=4&active_mode='+stat_flag ?>" style="color:#333333;text-decoration: none">
				<?cs var:item.text ?>
			</a>
			<?cs set:qz_richtitle_flag = 0 ?>	
		<?cs else ?>
			<?cs var:" " ?>
			<?cs if:qz_richtitle_flag == 0 ?>
					<?cs #:顿号和冒号结尾的都不用加mr10 ?>
					<?cs if: item == "、" || string.slice(item, string.length(item)-3, string.length(item)) == "："  ?>
						<span class="c_tx3">
					<?cs else ?>
						<span class="ui_mr5 c_tx3">
					<?cs /if ?>
				<a target="_blank" href="<?cs var:qz_metadata.qz_data.key1+'?active_channel=1&active_type=4&active_mode='+stat_flag ?>" style="color:#333333;text-decoration: none">
					<?cs var:item ?>
				</a>
				<?cs set:qz_richtitle_flag = 1 ?>
				<?cs #: 如果是最后一项,标签要闭合哦 ?>
				<?cs if:i==length ?>
					</span>
				<?cs /if ?>
			<?cs elif:qz_richtitle_flag == 1 ?>
				<a target="_blank" href="<?cs var:qz_metadata.qz_data.key1+'?active_channel=1&active_type=4&active_mode='+stat_flag ?>" style="color:#333333;text-decoration: none">
					<?cs var:item ?>
				</a>
				<?cs #: 如果是最后一项,标签要闭合哦 ?>
				<?cs if:i==length ?>
					</span>
				<?cs /if ?>
			<?cs /if ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 || string.length(cons.con.0) > 0 ?>
		<?cs set:qz_richtitle_flag = 0 ?>
		<?cs loop:i = 0, subcount(cons.con) - 1, 1 ?>
			<?cs call:richTitle-items(cons.con[i], i, subcount(cons.con) - 1) ?>
		<?cs /loop ?>
	<?cs elif:cons.con || subcount(cons.con) > 0 || string.length(cons.con) > 0 ?>
			<?cs set:qz_richtitle_flag = 0 ?>
			<?cs call:richTitle-items(cons.con, 0, 0) ?>
	<?cs /if ?>
<?cs /def ?>


<?cs #-------------------- ?>
<?cs #{feed_title.cs}?>
<?cs #-------------------- ?>

<?cs #:Feed标题 ?>
<?cs def:feedTitle(mod) ?>
	<?cs set:qz_feedtitle_name = 'feedtitle' ?>
	<?cs if:subcount(qz_metadata.feedtitle_2) > 0 ?>
		<?cs set:qz_feedtitle_name = 'feedtitle_2' ?>
	<?cs /if ?>
	<?cs call:richTitle(qz_metadata[qz_feedtitle_name].content) ?>
<?cs /def ?>


<?cs #-------------------- ?>
<?cs #{checkin.cs}?>
<?cs #-------------------- ?>

<?cs #:在 腾讯大厦 签到 ?>
<?cs def:checkin(mod) ?>
	<?cs if:subcount(qz_metadata.lbs) > 0 ?>
		<span class='ui_mr5 c_tx3'>在</span>
		<?cs if:subcount(qz_metadata.lbs.showmap.qz_popup) > 0 ?>
			<qz:popup 
				unrend="true" 
				version="<?cs var:qz_metadata.lbs.showmap.qz_popup.version ?>" 
				width="<?cs var:qz_metadata.lbs.showmap.qz_popup.width ?>" 
				height="<?cs var:qz_metadata.lbs.showmap.qz_popup.height ?>" 
				title="<?cs var:qz_metadata.lbs.showmap.qz_popup.title ?>" 
				src="<?cs var:qz_metadata.lbs.showmap.qz_popup.src ?>" 
				config="<?cs var:qz_metadata.lbs.showmap.qz_popup.config ?>" 
				param="<?cs var:qz_metadata.lbs.showmap.qz_popup.param ?>" 
			><a href="javascript:;" class="c_tx ui_mr5"><?cs var:qz_metadata.lbs.idname ?></a></qz:popup>
		<?cs /if ?>
<?cs /if ?>
<?cs /def ?>


<?cs #-------------------- ?>
<?cs #{qz_static.cs}?>
<?cs #-------------------- ?>

<?cs set:qz_static_action[1] = '发表日志' ?>
<?cs set:qz_static_action[2] = '发表信纸日志' ?>
<?cs set:qz_static_action[3] = '转载' ?>
<?cs set:qz_static_action[4] = '分享：' ?>
<?cs set:qz_static_action[5] = '转发：' ?>
<?cs set:qz_static_action[6] = '发表魔方日志' ?>
<?cs set:qz_static_action[7] = '修改' ?>
<?cs set:qz_static_action[8] = '回复您的日志：' ?>
<?cs set:qz_static_action[9] = '评论您的日志：' ?>
<?cs set:qz_static_action[10] = '分享网页' ?>
<?cs set:qz_static_action[11] = '中上传照片' ?>
<?cs set:qz_static_action[12] = '用' ?>
<?cs set:qz_static_action[13] = '回复了您的分享' ?>
<?cs set:qz_static_action[14] = '评论了您的分享' ?>
<?cs set:qz_static_action[15] = '提到了您' ?>
<?cs set:qz_static_action[16] = '分享了您的日志' ?>
<?cs set:qz_static_action[17] = '分享了您的相册' ?>
<?cs set:qz_static_action[18] = '分享了您的照片' ?>
<?cs set:qz_static_action[19] = '分享了您的分享' ?>
<?cs set:qz_static_action[20] = '评论您：' ?>
<?cs set:qz_static_action[21] = '转发您的说说：' ?>
<?cs set:qz_static_action[23] = '中提到您' ?>
<?cs set:qz_static_action[24] = '回复您' ?>
<?cs set:qz_static_action[25] = '转发了提到您的说说：' ?>
<?cs set:qz_static_action[26] = '也转发了' ?>
<?cs set:qz_static_action[27] = '回复了您的微博' ?>
<?cs set:qz_static_action[28] = '评论了您的微博' ?>
<?cs set:qz_static_action[29] = '更新相册' ?>
<?cs set:qz_static_action[30] = '评论了您的相册' ?>
<?cs set:qz_static_action[31] = '评论了您的照片' ?>
<?cs set:qz_static_action[32] = '回复您' ?>
<?cs set:qz_static_action[33] = '转载您的照片' ?>
<?cs set:qz_static_action[34] = '转载照片' ?>
<?cs set:qz_static_action[35] = '上传照片' ?>

<?cs set:qz_static_action[37] = '制作动感影集：' ?>
<?cs set:qz_static_action[38] = '评论您的动感影集：' ?>
<?cs set:qz_static_action[39] = '装扮相册：' ?>
<?cs set:qz_static_action[40] = '圈圈评论' ?>
<?cs set:qz_static_action[41] = '收到对照片' ?>
<?cs set:qz_static_action[42] = '发表圈圈评论' ?>
<?cs set:qz_static_action[43] = '圈圈评论您' ?>
<?cs set:qz_static_action[44] = '回复您的喜欢' ?>
<?cs set:qz_static_action[45] = '评论您的喜欢' ?>
<?cs set:qz_static_action[46] = '喜欢了您的日志' ?>
<?cs set:qz_static_action[47] = '喜欢了您的相册' ?>
<?cs set:qz_static_action[48] = '喜欢了您的相片' ?>
<?cs set:qz_static_action[49] = '喜欢了您的' ?>
<?cs set:qz_static_action[50] = '签到：' ?>
<?cs set:qz_static_action[51] = '喜欢' ?>
<?cs set:qz_static_action[52] = '转播：' ?>
<?cs set:qz_static_action[53] = '喜欢了您的微博' ?>
<?cs set:qz_static_action[54] = '分享了您的微博' ?>
<?cs set:qz_static_action[55] = '发起投票' ?>
<?cs set:qz_static_action[56] = '参与投票' ?>
<?cs set:qz_static_action[57] = '向你推荐' ?>
<?cs set:qz_static_action[58] = '回复说说：' ?>
<?cs set:qz_static_action[59] = '参与' ?>
<?cs set:qz_static_action[60] = '中回复了您' ?>
<?cs set:qz_static_action[62] = '在分享中回复了您' ?>
<?cs set:qz_static_action[63] = '有了新评论' ?>
<?cs set:qz_static_action[64] = '收藏音乐：' ?>
<?cs set:qz_static_action[65] = '设置背景音乐：' ?>
<?cs set:qz_static_action[66] = '评论说说：' ?>
<?cs set:qz_static_action[67] = '在说说中回复：' ?>
<?cs set:qz_static_action[68] = '在说说中提到您：' ?>
<?cs set:qz_static_action[69] = '在说说的评论中提到您：' ?>
<?cs set:qz_static_action[70] = '在说说的回复中提到您：' ?>
<?cs set:qz_static_action[71] = '评论了提到您的说说：' ?>
<?cs set:qz_static_action[72] = '有了' ?>
<?cs set:qz_static_action[73] = '转发您的时光轴：' ?>
<?cs set:qz_static_action[74] = '评论时光轴：' ?>
<?cs set:qz_static_action[75] = '在时光轴中回复：' ?>
<?cs set:qz_static_action[76] = '在时光轴中提到您：' ?>
<?cs set:qz_static_action[77] = '在时光轴的评论中提到您：' ?>
<?cs set:qz_static_action[78] = '在时光轴的回复中提到您：' ?>
<?cs set:qz_static_action[79] = '评论分享：' ?>
<?cs set:qz_static_action[80] = '回复分享：' ?>
<?cs set:qz_static_action[81] = '在分享回复中提到您：' ?>
<?cs set:qz_static_action[82] = '在分享评论中提到您：' ?>
<?cs set:qz_static_action[83] = '在分享中提到您：' ?>
<?cs set:qz_static_action[84] = '上传照片到相册' ?>
<?cs set:qz_static_action[85] = '分享歌单' ?>
<?cs set:qz_static_action[86] = '分享专辑' ?>
<?cs set:qz_static_action[87] = '订阅了' ?>
<?cs set:qz_static_action[88] = '创建歌单' ?>
<?cs set:qz_static_action[89] = '添加了歌曲' ?>
<?cs set:qz_static_action[90] = '评论歌单' ?>
<?cs set:qz_static_action[91] = '回复歌单' ?>
<?cs set:qz_static_action[92] = '在歌单中提到了您' ?>
<?cs set:qz_static_action[93] = '评论专辑' ?>
<?cs set:qz_static_action[94] = '回复专辑' ?>
<?cs set:qz_static_action[95] = '在专辑中提到了您' ?>
<?cs set:qz_static_action[96] = '分享给您：' ?>

<?cs set:qz_static_object[1]='的相册' ?>
<?cs set:qz_static_object[2]='的日志' ?>
<?cs set:qz_static_object[3]='的信纸日志' ?>
<?cs set:qz_static_object[4]='的微博' ?>
<?cs set:qz_static_object[5]='在相册' ?>
<?cs set:qz_static_object[6]='装扮相册' ?>
<?cs set:qz_static_object[7]='在分享' ?>
<?cs set:qz_static_object[8]='说说' ?>
<?cs set:qz_static_object[25]='签到' ?>
<?cs set:qz_static_action[36] = '' ?>
<?cs set:qz_static_object[9]='在' ?>
<?cs set:qz_static_object[10]='在微博' ?>
<?cs set:qz_static_object[11]='在照片' ?>
<?cs set:qz_static_object[12]='在照片活动：' ?>
<?cs set:qz_static_object[13]='上传的相册' ?>
<?cs set:qz_static_object[14]='在动感影集' ?>
<?cs set:qz_static_object[15]='的圈圈评论' ?>
<?cs set:qz_static_object[16]='在您的照片' ?>
<?cs set:qz_static_object[17]='的照片' ?>
<?cs set:qz_static_object[18]='在喜欢' ?>
<?cs set:qz_static_object[19]='上传的照片' ?>
<?cs set:qz_static_object[20]='的评论中提到您' ?>
<?cs set:qz_static_object[21]='的回复中提到您' ?>
<?cs set:qz_static_object[22]='说说' ?>
<?cs set:qz_static_object[23]='发起的投票' ?>
<?cs set:qz_static_object[24]='并说' ?>

<?cs set:qz_static_object[26]='的新评论' ?>
<?cs set:qz_static_object[27]='的歌单' ?>
<?cs set:qz_static_object[28]='的时光轴' ?>
<?cs set:qz_static_object[29]='说说' ?>
<?cs set:qz_static_object[30]='时光轴' ?>

<?cs #-------------------- ?>
<?cs #{action_title.cs}?>
<?cs #-------------------- ?>

<?cs def:actionTitle(mod) ?>
	
	<?cs set:qz_actiontitle_name = 'actiontitle' ?>
	<?cs if:subcount(qz_metadata.actiontitle_2) > 0 ?>
		<?cs set:qz_actiontitle_name = 'actiontitle_2' ?>
	<?cs /if ?>
	<?cs #:动作 ?>
		<?cs def:actionTitle_action(mod) ?>
		<?cs set:actionNo = qz_metadata[qz_actiontitle_name].action.no ?>
		<?cs set:actionWord = qz_static_action[actionNo] ?>
		<?cs if: string.length(actionWord)>0 ?>
			<?cs #:空转发、空分享则不显示 ?>
			<?cs if:(actionNo == 4 || actionNo == 5 ) && subcount(qz_metadata[qz_actiontitle_name].content)==0  ?>
				<?cs #:啥都不做 ?>
			<?cs else ?>
				<?cs var:" "+actionWord+" " ?>
			<?cs /if ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs #:昵称 ?>
	<?cs def:actionTitle_nick(mod) ?>
		<?cs if:string.length(qz_metadata[qz_actiontitle_name].nick.name)!= 0 ?>
			<span class="ui_mr5"><?cs call:userLink(qz_metadata[qz_actiontitle_name].nick, "")?></span>
		<?cs /if ?>
	<?cs /def ?>

	<?cs #:物件 ?>
	<?cs def:actionTitle_object(mod) ?>
		<?cs if:mod.pos > -1 ?>
			<?cs if:mod.pos == 0 ?>
				<?cs if:qz_static_object[qz_metadata[qz_actiontitle_name].object[mod.pos].no] ?>
					<span class="ui_mr5  c_tx3"><?cs var:qz_static_object[qz_metadata[qz_actiontitle_name].object[mod.pos].no] ?></span>
				<?cs elif:qz_static_object[qz_metadata[qz_actiontitle_name].object.no] ?>
					<span class="ui_mr5  c_tx3"><?cs var:qz_static_object[qz_metadata[qz_actiontitle_name].object.no] ?></span>
				<?cs /if ?>
			<?cs else ?>
				<?cs if:qz_static_object[qz_metadata[qz_actiontitle_name].object[mod.pos].no] ?>
				<span class="ui_mr5  c_tx3"><?cs var:qz_static_object[qz_metadata[qz_actiontitle_name].object[mod.pos].no] ?></span>
				<?cs /if ?>
			<?cs /if ?>
		<?cs else ?>
			<?cs if:qz_static_object[qz_metadata[qz_actiontitle_name].object[mod.pos].no] ?>
			<span class="ui_mr5  c_tx3"><?cs var:qz_static_object[qz_metadata[qz_actiontitle_name].object.no] ?></span>
			<?cs /if ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs #:内容 ?>
	<?cs def:actionTitle_content(mod) ?>
		<?cs call:richContent-title(qz_metadata[qz_actiontitle_name].content) ?>
	<?cs /def ?>

	<?cs #:链接 ?>
	<?cs def:actionTitle_url(mod) ?>
		<?cs if:subcount(qz_metadata[qz_actiontitle_name].url.url)>1 ?>
			<?cs set:url_url = qz_metadata[qz_actiontitle_name].url.url[mod.pos] ?>
			<?cs set:url_text = qz_metadata[qz_actiontitle_name].url.text[mod.pos] ?>
		<?cs else ?>
			<?cs set:url_url = qz_metadata[qz_actiontitle_name].url.url ?>
			<?cs set:url_text = qz_metadata[qz_actiontitle_name].url.text ?>
		<?cs /if ?>
		<?cs if:url_url ?>
			<a style="color: #577DA8;text-decoration:none;" href="<?cs var:url_url ?>" target="_blank"><?cs var:url_text ?></a>
		<?cs /if ?>
	<?cs /def ?>	



	<?cs def:actionTitleModCaller(mods) ?>
		<?cs #:引用 ?>
		<?cs def:actionTitle_quote(mods) ?>
			<i class="ui_ico quote_before c_tx3">“</i>
				<?cs call:actionTitleModCaller(mods) ?>
			<i class="ui_ico quote_after c_tx3">”</i>
		<?cs /def ?>

		<?cs def:actionTitleModCaller-items(mod) ?>
			<?cs # sx>> 邮件摘要，如果是转发的话，特殊处理 ?>
			<?cs if: string.find(actionWord, "转发")>=0 ?>
				
			<?cs else ?>
				<?cs if:mod.name == "quote" ?>
					<?cs call:actionTitle_quote(mod.mod) ?>
				<?cs /if ?>
				<?cs if:mod.name == "action" ?>
					<?cs call:actionTitle_action(mod) ?>
				<?cs /if ?>
				<?cs if:mod.name == "object" ?>
					<?cs call:actionTitle_object(mod) ?>
				<?cs /if ?>
				<?cs if:mod.name == "nick" ?>
					<?cs call:actionTitle_nick(mod) ?>
				<?cs /if ?>
				<?cs if:mod.name == "url" ?>
					<?cs call:actionTitle_url(mod) ?>
				<?cs /if ?>
				<?cs if:mod.name == "content" ?>
					<?cs call:actionTitle_content(mod) ?>
				<?cs /if ?>
			<?cs /if ?>
		<?cs /def ?>
		<?cs #:这个来判断是否数组的方法,从来就没有爽过 叼~~?>
		<?cs if:subcount(mods.0) > 0 ?>
			<?cs each:mod = mods ?>
				<?cs call:actionTitleModCaller-items(mod) ?>
			<?cs /each ?>
		<?cs else ?>
			<?cs call:actionTitleModCaller-items(mods) ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs call:actionTitleModCaller(mod.mod) ?>
<?cs /def ?>


<?cs #-------------------- ?>
<?cs #{main_title.cs}?>
<?cs #-------------------- ?>

<?cs #:一级元件召唤 ?>
<?cs def:modCaller(titleMod) ?>
	<?cs def:mods(mod) ?>
		<?cs if:mod.name == "feedTitle" ?>
			<?cs call:feedTitle(mod) ?>
		<?cs elif:mod.name == 'actionTitle' ?>
			<?cs call:actionTitle(mod) ?>
		<?cs elif:mod.name == 'checkin' ?>
			<?cs call:checkin(mod) ?>
		<?cs /if ?>
		<?cs if:mod.contentHide==1 ?>
			<div class="f_fold">
				<a title="查看详情" class="c_tx" href="javascript:;" cmd="toggleContentBox">
					查看详情
				</a>
			</div>
		<?cs /if ?>
	<?cs /def ?>

	<?cs #:这个来判断是否数组的方法,从来就没有爽过 叼~~?>
	<?cs if:subcount(titleMod.mod.0) > 0 ?>
		<?cs each:mod = qz_metadata.qz_layout_title.mod ?>
			<?cs call:mods(mod) ?>
		<?cs /each ?>
	<?cs else ?>
		<?cs call:mods(titleMod.mod) ?>
	<?cs /if ?>
<?cs /def ?>


<?cs #-------------------- ?>
<?cs #{mod_layout.cs}?>
<?cs #-------------------- ?>

<?cs #从布局版本开始执行?>
<?cs def:start(module) ?>	
	<?cs #昵称统计标志，被动是2，主动是6 ?>
	<?cs set:stat_flag_nick = 2 ?>
	<?cs if:qz_metadata.isactfeed==1 ?>
		<?cs set:stat_flag_nick = 6 ?>
	<?cs /if ?>	
	<?cs #内容统计标志，被动是3，主动是5 ?>
	<?cs set:stat_flag = 3 ?>
	<?cs if:qz_metadata.isactfeed==1 ?>
		<?cs set:stat_flag = 5 ?>
	<?cs /if ?>	

	<?cs #昵称 ?>
	<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.metadata.opuin+'?active_channel=1&active_type=4&active_mode='+stat_flag_nick ?>" style="color: #577DA8;text-decoration:none;"><?cs var:html_encode(qz_metadata.metadata.nickname, 1) ?></a>

	<?cs def:versionCaller(mod,ver) ?>
		<?cs #只有存在多版本布局时，前面的假设成立?>
		<?cs set:layout_v = "layout_v" + ver ?>
		<?cs if:subcount(mod[layout_v]) > 0 ?>
			<?cs call:modCaller(mod[layout_v]) ?>
		<?cs else ?>
			<?cs call:modCaller(mod) ?>
		<?cs /if ?>
	<?cs /def ?>	

	<?cs def:initConfig(mod, ver) ?>
		<?cs #:初始化配置项目 ?>
		<?cs if:subcount(mod[layout_v]) > 0 ?>
				<?cs set:qz_config = mod[layout_v].config ?>
		<?cs else ?>
				<?cs set:qz_config = mod.config ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs if:qz_metadata.metadata.appid==311 && qz_metadata.isactfeed==1 ?>
		<?cs #说说 ?>
		<?cs var:" 发表说说" ?>
	<?cs elif:qz_metadata.metadata.appid==333 ?>
		<?cs #礼物 ?>
		<?cs var:" 送我礼物 " ?>
		<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.metadata.uin+'#!app=333?active_channel=1&active_type=4&active_mode='+stat_flag ?>" style="color: #577DA8;text-decoration:none;"><?cs var:qz_metadata.feedtitle.content.con.1.text ?></a>
	<?cs else ?>
		<?cs #假设所有feed的存在布局版本?>
		<?cs set:qz_layout_version = 1 ?>		
		<?cs if:qz_metadata.layout_version[module] ?>
			<?cs set:qz_layout_version = qz_metadata.layout_version[module] ?>
		<?cs /if?>
		<?cs if:module == "summary" ?>
			<?cs call:versionCaller(qz_metadata.qz_layout_summary, qz_layout_version) ?>
			<?cs call:initConfig(qz_metadata.qz_layout_summary, qz_layout_version) ?>
		<?cs elif:module == "title" ?>
			<?cs call:versionCaller(qz_metadata.qz_layout_title, qz_layout_version) ?>
		<?cs /if ?>
	<?cs /if ?>
<?cs /def ?>


<?cs #-------------------- ?>
<?cs #{title_start.cs}?>
<?cs #-------------------- ?>

<?cs #主入口?>
<?cs call:start("title")?>