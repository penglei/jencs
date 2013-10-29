<?cs #:
	param.data展示数据
	param.key业务的key
	param.max_count展示列表的最多用户数
	param.totalNum 总数
	param.endword结束文案
	param.isNeedMe是否显示“我”
	param.showmode:为1时，isNeedMe为真或者有列表数据为真的时候再展示;为2时，只要totalNum大于1就展示
	param.needShowlist:是否需要详细的列表入口
 ?>
<?cs #:data,key,max_count,totalNum,endword,isNeedMe,showmode,needShowlist ?>
<?cs def:genUserList(data,param) ?>
	<?cs if: subcount(data.0) ?>
		<?cs set:_data_len=subcount(data) ?>
	<?cs elif:subcount(data)>0 ?>
		<?cs set:_data_len=1 ?>
	<?cs else ?>
		<?cs set:_data_len=0 ?>
	<?cs /if ?>
	<?cs if: _data_len>param.max_count ?>
		<?cs set:_data_len=param.max_count ?>
	<?cs /if ?>
	<?cs if: param.isNeedMe ?>
		<span class="_me">		
			我
		<?cs if: param.totalNum>1 ?>
			和
		<?cs /if ?>
		</span>
	<?cs /if ?>
	<?cs loop:i=0,_data_len-1,1 ?>
		<?cs if: subcount(data[i]) ?>
			<?cs call:userLink(data[i],'') ?>
			<?cs if: i<(_data_len-1) ?>、<?cs /if ?>
		<?cs else ?>
			<?cs call:userLink(data,'') ?>
		<?cs /if ?>
	<?cs /loop ?>
	<?cs #:产品说数字先别放出来 ?>
	<?cs if: 0 && _data_len==0 && param.isNeedMe ?>	
		<?cs set:_rander_pre=param.totalNum+"人" ?>
		<?cs set:_rander_tail="" ?>
	<?cs elif:0 && _data_len<param.totalNum  ?>
		<?cs set:_rander_pre="等" ?>	
		<?cs set:_rander_pre=_rander_pre+param.totalNum+"人" ?>
		<?cs set:_rander_tail="都" ?>
	<?cs /if ?>
	<span class="_listcnt <?cs if:_data_len>0 ?>ui_ml5<?cs /if ?>">
		<?cs if: param.needShowlist ?>
			<?cs set:_node_Name="a" ?>
			<?cs set:_nodeParam='href="javascript:;" class="c_tx _showList" data-key="' ?>
			<?cs set:_nodeParam=_nodeParam+param.key+'"' ?>
		<?cs else ?>
			<?cs set:_node_Name="span" ?>
			<?cs set:_nodeParam="" ?>
		<?cs /if ?>
		<<?cs var:_node_Name ?> <?cs var:_nodeParam ?> >
			<?cs var:_rander_pre ?>
		</<?cs var:_node_Name ?>>
		<?cs var:_rander_tail ?>
	</span>
	<?cs var:param.endword ?>
<?cs /def ?>
<?cs #:附加信息区域 ?>
<?cs def:extendInfo(mod) ?>
	<?cs set:qz_extendinfo_name = 'extend_info' ?>
	<?cs if:subcount(qz_metadata.extend_info_2) > 0 ?>
		<?cs set:qz_extendinfo_name = 'extend_info_2' ?>
	<?cs /if ?>
	<?cs def:extendInfo-items(item) ?>
		<?cs if:item.type == 'source' ?>
			<?cs #:来源微博的原文链接 ?>
			<?cs if:item.name=="腾讯微博" ?>
				<?cs set:retweet_url=item.url ?>
			<?cs /if ?>
			<?cs if:source_in_content ?>
			<?cs else ?>
				<?cs if:item.who ?>
					<span class='ui_mr10'>来自<a href="http://user.qzone.qq.com/<?cs var:item.uin ?>" class="q_namecard c_tx3" link="nameCard_<?cs var:item.uin ?>" target="_blank"><?cs call:writeUserName(item.uin, item.name) ?></a></span>
				<?cs elif:item.url ?>
					<span class='ui_mr10'>来自<a href="<?cs var:item.url ?>" target="_blank" class="c_tx3"><?cs var:item.name ?></a></span>
				<?cs else ?>
					<span class='ui_mr10'>来自<?cs var:item.name ?></span>
				<?cs /if ?>
			<?cs /if ?>
		<?cs elif:item.type == 'picnum' ?>
			<?cs if:item.count > 0?>
			<span class='ui_mr10'>共有<?cs var:item.count ?>张图片</span>
			<?cs /if?>
		<?cs #:这里为统一计数迁移到天津做个兼容过渡 ?>
		<?cs if: qz_metadata.qz_data.version!=1 ?>
			<?cs set:mydef_qz_orgin_cnt=" none" ?>
		<?cs /if ?>
		<?cs elif:item.type == 'forward' && (qz_metadata.qz_data.version!=1 || qz_metadata.qz_data.key2.ZZ.cnt>0) ?>
			<?cs #:转载原先使用的是item.count,这里尝试使用统一计数的数据试试 ?>
			<span class='ui_mr10 qz_orgin_cnt<?cs var:mydef_qz_orgin_cnt ?>' cnttype='forward'>转载(<?cs var:qz_metadata.qz_data.key2.ZZ.cnt ?>)</span>
		<?cs elif:item.type == 'share' && (qz_metadata.qz_data.version!=1 || (qz_metadata.qz_data.key2.ZF.cnt-0+qz_metadata.qz_data.key2.WB.cnt)>0) ?>
			<span class='ui_mr10 qz_orgin_cnt<?cs var:mydef_qz_orgin_cnt ?>' cnttype='share'>原文分享(<?cs var:qz_metadata.qz_data.key2.ZF.cnt-0+qz_metadata.qz_data.key2.WB.cnt ?>)次</span>
		<?cs elif:item.type == 'retweet' && (qz_metadata.qz_data.version!=1 || qz_metadata.qz_data.key2.ZS.cnt>0) ?>
			<?cs #:来源空间的原文链接 ?>
			<?cs if:!retweet_url ?>
				<?cs set:retweet_url = "http://user.qzone.qq.com/"+qz_metadata.metadata.orguin+"/mood/"+qz_metadata.metadata.orgid+"."+qz_metadata.metadata.orgpfid ?>
			<?cs /if ?>
			<span class='ui_mr10'>
				<a target=_blank class="c_tx3" href="<?cs var:retweet_url ?>">评论<?cs if:qz_metadata.qz_data.key2.CS.cnt>0 ?>(<?cs var:qz_metadata.qz_data.key2.CS.cnt ?>)<?cs /if ?></a>
			</span>
			<span class='ui_mr10 qz_orgin_cnt<?cs var:mydef_qz_orgin_cnt ?>' cnttype='retweet'>
				<a target=_blank class="c_tx3" href="<?cs var:retweet_url ?>#action=forward">转发(<?cs var:qz_metadata.qz_data.key2.ZS.cnt ?>)</a>
			</span>
		<?cs elif:item.type == 'gift_sendnum' ?>
			<span class='ui_mr10'>该礼物已被赠送<?cs var:item.count ?>次</span>
		<?cs elif:item.type == 'gift_birthnum' ?>
			<span class='ui_mr10'>共收到<?cs var:item.count ?>份生日礼物</span>
		<?cs elif:item.type == 'orgintime' ?>
			<span class='ui_mr10'><?cs var:item.text ?></span>
		<?cs elif:item.type == 'care' ?>
			<span class="ui_mr10">共有<?cs var:item.count ?>人关注</span>
		<?cs elif:item.type == 'txt' ?>
			<span><?cs var:item.text ?></span>
		<?cs elif:item.type == 'url' ?>
			<a href="<?cs var:item.url?>" target="_blank"><?cs var:item.text ?></a>
		<?cs /if ?>

	<?cs /def ?>
	<?cs with:exinfo = qz_metadata[qz_extendinfo_name] ?>
	<?cs if:exinfo.info.0 || subcount(exinfo.info.0) > 0 ?>
		<p class="f_reprint c_tx3 _extendInfo_">
		<?cs loop:i = 0, subcount(exinfo.info) -1, 1 ?>
			<?cs call:extendInfo-items(exinfo.info[i]) ?>
		<?cs /loop ?>
		</p>
	<?cs elif:exinfo.info.type ?>
		<p class="f_reprint c_tx3 _extendInfo_">
		<?cs call:extendInfo-items(exinfo.info) ?>
		</p>
	<?cs elif:subcount(exinfo.cate) > 0 ?>
		<div class="f_votestar">
			<span class="votestar">
				<span class="votestar_i star_<?cs var:exinfo.cate.grade?>"></span>
			</span>
			<span class="votescore">
				<?cs if:exinfo.cate.taste ?><span class="ui_mr10 c_tx3">口味：<?cs var:exinfo.cate.taste ?></span><?cs /if ?>
				<?cs if:exinfo.cate.svc ?><span class="ui_mr10 c_tx3">服务：<?cs var:exinfo.cate.svc ?></span><?cs /if ?>
				<?cs if:exinfo.cate.env ?><span class="ui_mr10 c_tx3">环境：<?cs var:exinfo.cate.env ?></span><?cs /if ?>
				<?cs if:exinfo.cate.consume ?><span class="ui_mr10 c_tx3">人均：<?cs var:exinfo.cate.consume ?>元</span><?cs /if ?>
			</span>
		</div>
	<?cs elif:subcount(exinfo.qz_app) > 0 ?>
		<?cs def:extInfo-app(item) ?>
			<a class="c_tx" target="_blank" href="
			<?cs alt:item.url?>http://rc.qzone.qq.com/myhome/<?cs var:item.id?><?cs /alt?>
			"><?cs var:item.name ?></a>
		<?cs /def ?>

		<?cs with:app = exinfo.qz_app.app ?>
		<?cs if:subcount(app) > 0?>
		<p class="f_reprint c_tx3">
			<?cs with:user = exinfo.qz_app.user?>
			<a href="http://user.qzone.qq.com/<?cs var:user.uin ?>" class="q_namecard c_tx" link="nameCard_<?cs var:user.uin ?>" target="_blank"><?cs call:writeUserName(user.uin,user.nickname) ?></a>
			<?cs /with ?>还添加了
			<?cs if:app.0 || subcount(app.0) >0 ?>
				<?cs set:ex_appshow_count = subcount(app) ?>
				<?cs loop:i = 0, ex_appshow_count - 1, 1?>
					<?cs call:extInfo-app(app[i])?>
					<?cs if:i < ex_appshow_count - 2 ?>、<?cs /if ?>
					<?cs if:i == ex_appshow_count - 2 ?>和<?cs /if?>
				<?cs /loop ?>
				<?cs if: exinfo.qz_app.count >= ex_appshow_count ?>
					<?cs if:ex_appshow_count < exinfo.qz_app.count?>等<?cs /if?><?cs var:exinfo.qz_app.count?>款应用
				<?cs /if ?>
			<?cs else ?>
				<?cs call:extInfo-app(app)?>
			<?cs /if ?>
		</p>
		<?cs /if ?>
		<?cs /with ?>
	<?cs elif:subcount(exinfo.sameusers.user) > 0 && exinfo.sameusers.type ?>
		<?cs if:exinfo.sameusers.type == "app"?>
			<p class="f_reprint c_tx3">
			<?cs if:subcount(exinfo.sameusers.user.0) > 0?>
				<?cs set:ex_show_count = subcount(exinfo.sameusers.user)?>
				<?cs with:user=exinfo.sameusers.user?>
				<?cs loop:i = 0, ex_show_count - 1, 1?>
					<a href="http://user.qzone.qq.com/<?cs var:user[i].uin ?>" class="q_namecard c_tx" link="nameCard_<?cs var:user[i].uin ?>" target="_blank"><?cs call:writeUserName(user[i].uin, user[i].nickname) ?></a>
					<?cs if:i < ex_show_count - 2 ?>、<?cs /if ?>
					<?cs if:i == ex_show_count - 2 ?>和<?cs /if?>
				<?cs /loop ?>
				<?cs if: exinfo.sameusers.count > ex_show_count?>等<?cs var:exinfo.sameusers.count ?>个好友<?cs /if?>
				<?cs /with?>
			<?cs else ?>
				<a href="http://user.qzone.qq.com/<?cs var:exinfo.sameusers.user.uin ?>" class="q_namecard c_tx" link="nameCard_<?cs var:exinfo.sameusers.user.uin ?>" target="_blank"><?cs call:writeUserName(exinfo.sameusers.user.uin, exinfo.sameusers.user.nickname) ?></a>
			<?cs /if?>也向我发了请求
			</p>
		<?cs /if ?>
	<?cs /if ?>
	<?cs /with ?>

	<?cs #动态合并附加信息?>
	<?cs def:showDynamic(mod) ?>
		<?cs if:mod.name == "extendInfo" && mod.dynamic == "on" && subcount(qz_metadata.relyPool)>0 ?>
			<?cs set:_param.max_count=mod.dynamic.max_count ?>
			<?cs set:_param.endword=mod.dynamic.endword ?>
			<?cs if: mod.dynamic.isNeedMe ?>
				<?cs set:_param.endword=qz_metadata.relyPool.hasMe ?>
			<?cs /if ?>
			<?cs set:_param.showmode=mod.dynamic.showmode ?>
			<?cs set:_param.needShowlist=mod.dynamic.needShowlist ?>
			<?cs if: qz_metadata.metadata.appid==202 ?>
				<?cs #:跟原文分享数量一样 ?>
				<?cs set:_param.totalNum=qz_metadata.qz_data.key2.ZF.cnt-0+qz_metadata.qz_data.key2.WB.cnt ?>
			<?cs elif:qz_metadata.metadata.appid==311 ?>
				<?cs #:跟原文转发数量一样 ?>
				<?cs set:_param.totalNum=qz_metadata.qz_data.key2.ZS.cnt ?>
			<?cs /if ?>
			<p class="f_reprint c_tx3">
			<span class="ur_mr10">
					<?cs call:genUserList(qz_metadata.relyPool,_param) ?>
			</span>
		</p>
		<?cs /if ?>	
	<?cs /def ?>
	<?cs #:这里没办法了，粗暴的查找下节点，找时间优化掉 ?>
	<?cs if: subcount(mod.mod.0) ?>
		<?cs loop:i=0,subcount(mod.mod)-1,1 ?>
			<?cs if: mod.mod[i].name == "extendInfo" ?>
				<?cs call:showDynamic(mod.mod[i]) ?>
			<?cs /if ?>
		<?cs /loop ?>
	<?cs elif:(subcount(mod.mod)||string.length(mod.mod.name))&& mod.mod.name == "extendInfo"?>
		<?cs call:showDynamic(mod.mod) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:sameUserList(max_num)?>
	<?cs with:exinfo=qz_metadata.extend_info?>
	<?cs if:subcount(exinfo.sameusers.user) > 0 && !exinfo.sameusers.type ?><?cs #没有type默认是七周年的feeds?>
	<div class="f_ct_act_ava">
		<p><?cs alt:exinfo.sameusers.title.text?>他们也参与了活动<?cs /alt?></p>
		<?cs if:subcount(exinfo.sameusers.user.0) > 0 ?>
			<?cs set:qz_users_len=subcount(exinfo.sameusers.user)?>
			<?cs #默认最多展示11个?>
			<?cs set:qz_info_umax=min(min(max_num, 11), qz_users_len)?>
			<?cs loop:i = 1,qz_info_umax, 1 ?>
				<?cs call:userIconStyle(exinfo.sameusers.user[i-1], 'act')?>
			<?cs /loop ?>
			<?cs if:qz_users_len > qz_info_umax?>
				<span class="act_ava_more bor3 bg3 bg2_hover" title="共<?cs var:qz_users_len?>位好友">...</span>
			<?cs /if?>
		<?cs elif:subcount(exinfo.sameusers.user) > 0 ?>
			<?cs call:userIconStyle(exinfo.sameusers.user, 'act')?>
		<?cs /if ?>
	</div>
	<?cs /if ?>
	<?cs /with ?>
<?cs /def ?>