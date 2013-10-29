<?cs #:转发参数 ?>
<?cs #:构造转发的param数据，评论回复支持转发的话，无法每条评论回复都冗余出那么大一坨转发信息 ?>
<?cs #:fwdtype 转发类型，0-转发主贴 1-转发评论 2-转发回复  fwdtype ?>
<?cs def:buildForwardParam(fwdtype,comment,reply) ?>
	<?cs #:这里为该死的转发框弄多个方法，转多一次 ?>
	<?cs def:writeUserName_forwad(uin,defaultName) ?>
		<?cs if:string.length(qz_metadata.remarkPool['u'+uin])>0 ?>
			<?cs escape:'js' ?><?cs var:html_encode(html_encode(qz_metadata.remarkPool['u'+uin],1), 1) ?><?cs /escape ?>
		<?cs else ?>
			<?cs escape:'js' ?><?cs var:html_encode(html_encode(defaultName, 1), 1) ?><?cs /escape ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs def:fillNum(num) ?>
		<?cs if:num ?>
			<?cs var:num ?>
		<?cs else ?>
			<?cs var:0 ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs def:setForwardCount(n,m) ?>
		<?cs if:n>0 ?>
			<?cs var:n ?>
		<?cs elif:m>0 ?>
			<?cs var:m ?>
		<?cs else ?>
			0
		<?cs /if ?>
	<?cs /def ?>
		<?cs def:fillContent(content) ?>
		<?cs def:setContent(con) ?>
			<?cs if:con.type == "url"?>
				<?cs escape:'html' ?><?cs var:json_encode(con.text, 1) ?><?cs /escape ?>
			<?cs elif:con.type == "text"?>
				<?cs escape:'html' ?><?cs var:json_encode(con.text, 1) ?><?cs /escape ?>
			<?cs elif:con.type == "nick" ?>
				@{uin:<?cs var:con.uin ?>,nick:<?cs escape:'html' ?><?cs call:writeUserName_forwad(con.uin, con.name)?><?cs /escape ?>}
			<?cs elif:con.type == "qz_app" ?>
				<?cs escape:"html" ?><?cs var:json_encode(con.text, 1) ?><?cs /escape ?>
			<?cs elif:con.type == 'script' ?>
				<?cs if:con.biz == "mall"?>
					<?cs escape:"html" ?><?cs var:json_encode(con.text, 1) ?><?cs /escape ?>
				<?cs /if ?>
			<?cs else ?>
				<?cs escape:"html" ?><?cs var:json_encode(con, 1) ?><?cs /escape ?>
			<?cs /if ?>
		<?cs /def ?>
		restHTML('
		<?cs if:content.con.0||subcount(content.con.0)>0 ?>
			<?cs loop:i =0, subcount(content.con), 1 ?>
				<?cs call:setContent(content.con[i]) ?>
			<?cs /loop ?>
		<?cs elif:content.con||subcount(content.con)>0 ?>
			<?cs call:setContent(content.con) ?>
		<?cs /if ?>
		')
	<?cs /def ?>
	<?cs if:!fwdtype ?>
		<?cs set:fwdtype=0 ?>
	<?cs /if ?>
	{
		id:'<?cs var:qz_metadata.metadata.blogid ?>',
		poster:{
			uin:'<?cs var:qz_metadata.metadata.uin ?>',
			nickname:restHTML('<?cs escape:'html' ?><?cs call:writeUserName_forwad(qz_metadata.metadata.uin,qz_metadata.metadata.mood_uinnickname) ?><?cs /escape ?>')},
			owner:{
				uin:'<?cs var:qz_metadata.metadata.orguin ?>',
				nickname:restHTML('<?cs escape:'html' ?><?cs call:writeUserName_forwad(qz_metadata.metadata.uin,qz_metadata.metadata.orgname) ?><?cs /escape ?>')
			},
			content:<?cs call:fillContent(qz_metadata.actiontitle.content) ?>,
			rt_content:restHTML('<?cs escape:'html' ?><?cs var:json_encode(qz_metadata.metadata.rt_content, 1) ?><?cs /escape ?>'),
			source:'<?cs var:qz_metadata.metadata.mood_source ?>',
			retweetListInfo:{
				tid:'<?cs var:qz_metadata.metadata.blogid ?>',
				uin:'<?cs var:qz_metadata.metadata.uin ?>',
				pfType:<?cs call:fillNum(qz_metadata.metadata.mood_source) ?>,
				rtTid:'<?cs var:qz_metadatika.metadata.orgid ?>',
				rtUin:<?cs call:fillNum(qz_metadata.metadata.orguin) ?>,
				rtPfType:<?cs call:fillNum(qz_metadata.metadata.orgpfid) ?>,
				totalForShow:<?cs call:setForwardCount(qz_metadata.qz_data.key2.ZS.cnt,qz_metadata.qz_data.key1.ZS.cnt) ?>,
				hasToWeibo:<?cs call:fillNum(qz_metadata.metadata.to_tweet) ?><?cs if:fwdtype>0 ?>,
				commentInfo:{
					poster:{
						uin:'<?cs var:comment.user.uin ?>',
						nickname:restHTML('<?cs escape:'html' ?><?cs call:writeUserName_forwad(comment.user.uin,comment.user.name) ?><?cs /escape ?>')
					},
					tid:'<?cs var:comment.commentid ?>',
					uin:'<?cs var:comment.user.uin ?>',
					nickname:restHTML('<?cs escape:'html' ?><?cs call:writeUserName_forwad(comment.user.uin,comment.user.name) ?><?cs /escape ?>'),
					reply_num:<?cs call:fillNum(comment.totalreply) ?>,
					content:<?cs call:fillContent(comment.content) ?><?cs if:fwdtype==2 ?>,
					replyInfo:{
						poster:{
							uin:'<?cs var:reply.user.uin ?>',
							nickname:restHTML('<?cs escape:'html' ?><?cs call:writeUserName_forwad(reply.user.uin,reply.user.name) ?><?cs /escape ?>')
						},
						tid:'<?cs var:reply.replyid ?>',
						uin:'<?cs var:reply.user.uin ?>',
						nickname:restHTML('<?cs escape:'html' ?><?cs call:writeUserName_forwad(reply.user.uin,replu.user.name) ?><?cs /escape ?>'),
						content:<?cs call:fillContent(reply.content) ?>
					}<?cs /if ?>
				}<?cs /if ?>
			},
			isSignIn:<?cs call:fillNum(qz_metadata.metadata.signin) ?>,
			fwdtype:<?cs call:fillNum(fwdtype) ?><?cs if:fwdtype>0 ?>,
			t2_tid:'<?cs var:comment.commentid ?>'<?cs /if ?><?cs if:fwdtype==2 ?>,
			t3_tid:'<?cs var:reply.replyid ?>'<?cs /if ?>
		}
<?cs /def ?>

<?cs #:fwdtype 转发类型，0-分享主贴 1-分享评论 2-分享回复  fwdtype ?>
<?cs def:buildReshareParam(fwdtype,comment,reply) ?>
	<?cs def:fillContent_buildReshareParam(content) ?>
		<?cs def:setContent_buildReshareParam(con) ?>
			<?cs if:con.type == "url"?>
				<?cs escape:"html" ?><?cs var:con.text ?><?cs /escape ?>
			<?cs elif:con.type == "text"?>
				<?cs escape:"html" ?><?cs var:con.text ?><?cs /escape ?>
			<?cs elif:con.type == "nick" ?>
				<?cs escape:"html" ?>@{uin:<?cs var:con.uin ?>,nick:<?cs call:writeUserName(con.uin, con.name)?>,who:<?cs var:con.who ?>}<?cs /escape ?>
			<?cs else ?>
				<?cs escape:"html" ?><?cs var:uri_encode(con) ?><?cs /escape ?>
			<?cs /if ?>
		<?cs /def ?>
		<?cs if:content.con.0||subcount(content.con.0)>0 ?>
			<?cs loop:i =0, subcount(content.con), 1 ?>
				<?cs call:setContent_buildReshareParam(content.con[i]) ?>
			<?cs /loop ?>
		<?cs elif:content.con||subcount(content.con)>0 ?>
			<?cs call:setContent_buildReshareParam(content.con) ?>
		<?cs /if ?>
	<?cs /def ?>

	&plusdescription=
	<?cs if:fwdtype == 2 ?>
		||<?cs escape:"html" ?>@{uin:<?cs var:reply.user.uin ?>,nick:<?cs var:reply.user.name ?>,who:<?cs var:reply.user.type ?>}<?cs /escape ?>:<?cs call:fillContent_buildReshareParam(reply.content) ?>
	<?cs /if ?>
	<?cs if:fwdtype == 1 || fwdtype == 2 ?>
		||<?cs escape:"html" ?>@{uin:<?cs var:comment.user.uin ?>,nick:<?cs var:comment.user.name ?>,who:<?cs var:comment.user.type ?>}<?cs /escape ?>:<?cs call:fillContent_buildReshareParam(comment.content) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:操作行 ?>

<?cs def:opr(mod) ?>
	<?cs def:opr-btn-like(mod) ?>
		<?cs #:key2:源key key1:节点key ?>
		<?cs if:mod.like == 'on' ?>
			<a class="qz_like_btn_v2 c_tx ui_mr10" href="javascript:;"></a>
		<?cs /if ?>
		<?cs #:<a class="qz_like_btn_v2 c_tx ui_mr10 none" href="javascript:;"></a> ?>
	<?cs /def ?>

	<?cs def:time(mod) ?>
		<?cs if:subcount(qz_metadata.time) > 0 || qz_metadata.time.text ?>
			<span class="ui_mr10"><?cs var:qz_metadata.time.qz_text ?></span>
		<?cs /if ?>
	<?cs /def ?>

	<?cs def:btn(mod) ?>
		<?cs def:setCount(n,m) ?>
			<?cs if:n>0 || m>0 ?>(<?cs var:0+n+m ?>)<?cs /if ?>
		<?cs /def ?>
		<?cs if:qz_metadata.qz_data.key2.LIKE.cnt>0 ?>
			<?cs set:qz_data_key='key2' ?>
			<?cs elif:qz_metadata.qz_data.key1.LIKE.cnt>0 ?>
				<?cs set:qz_data_key='key1' ?>
			<?cs else ?>
				<?cs set:qz_data_key='key2' ?>
			<?cs /if ?>
		<?cs def:randerLike(mod) ?>
			<?cs if:qz_metadata.qz_data.version==1 && !qz_metadata.accessright&&mod.like=='on' ?>
				<?cs if:qz_metadata.qz_data.key2 ?>
					<?cs set:unikey=qz_metadata.qz_data.key2 ?>
				<?cs elif:qz_metadata.qz_data.key1 ?>
					<?cs set:unikey=qz_metadata.qz_data.key1 ?>
				<?cs /if ?>
				<?cs if:subcount(qz_metadata.qz_data[qz_data_key].LIKE.user)>0 ?>
					<?cs set:likeinfo=subcount(qz_metadata.qz_data[qz_data_key].LIKE.user) ?>
					<?cs if:likeinfo>6 ?>
						<?cs set:likeinfo=6 ?>
					<?cs /if ?>
				<?cs elif:subcount(qz_metadata.qz_data[qz_data_key].LIKE.user[0]) ?>
					<?cs set:likeinfo = 1 ?>
				<?cs else ?>
					<?cs set:likeinfo = 0 ?>
				<?cs /if ?>
				<?cs if:qz_metadata.qz_data[qz_data_key].LIKE.isliked==1 && string.length(qz_metadata.qz_data.key1)>0 ?>
					<a class="qz_like_btn_v3 c_tx ui_mr10 _likeBtn" 
						data-islike="<?cs var:qz_metadata.qz_data[qz_data_key].LIKE.isliked||'0' ?>" 
						data-likecnt="<?cs if:qz_metadata.qz_data[qz_data_key].LIKE.cnt ?>
									<?cs var:qz_metadata.qz_data[qz_data_key].LIKE.cnt ?>
								<?cs else ?>0<?cs /if ?>" 
						data-showcount="<?cs var:likeinfo ?>" 
						data-unikey="<?cs var:unikey ?>" 
						data-curkey="<?cs var:qz_metadata.qz_data.key1 ?>" 
						href="javascript:;" >
							取消赞<?cs call:setCount(qz_metadata.qz_data.key2.LIKE.cnt,qz_metadata.qz_data.key1.LIKE.cnt) ?>
					</a>
				<?cs elif:string.length(qz_metadata.qz_data.key1)>0 ?>
					<a class="qz_like_btn_v3 c_tx ui_mr10 _likeBtn" 
					data-islike="<?cs var:qz_metadata.qz_data[qz_data_key].LIKE.isliked||'0' ?>" 
					data-likecnt="
						<?cs if:qz_metadata.qz_data[qz_data_key].LIKE.cnt ?>
						<?cs var:qz_metadata.qz_data[qz_data_key].LIKE.cnt ?>
						<?cs else ?>0<?cs /if ?>" 
					data-showcount="<?cs var:likeinfo ?>" 
					data-unikey="<?cs var:unikey ?>" 
					data-curkey="<?cs var:qz_metadata.qz_data.key1 ?>" 
					href="javascript:;">
						赞<?cs call:setCount(qz_metadata.qz_data.key2.LIKE.cnt,qz_metadata.qz_data.key1.LIKE.cnt) ?>
					</a>
				<?cs /if ?>
			<?cs /if ?>
		<?cs /def ?>

		<?cs def:btn-reply(item) ?>
			<?cs #:如果有展开更多,就变成More Btn 这里和comments耦合了.很罪过啊.?>
			<?cs if:qz_metadata.noreply && qz_metadata.comments.qz_more.action ?>
				<span class="ui_mr10 qz_btn_reply"><qz:more 
					action="<?cs var:qz_metadata.comments.qz_more.action ?>" 
					param="<?cs var:qz_metadata.comments.qz_more.param ?>" 
					charset="<?cs var:qz_metadata.comments.qz_more.charset ?>" 
					commentsurl="<?cs var:qz_metadata.metadata.commentsurl ?>" 
					link= 1 
				>评论<?cs call:setCount(qz_metadata.totalcomment,0) ?></qz:more></span>
			<?cs elif:qz_metadata.totalcomment > 3 && qz_metadata.comments.qz_more.action ?>
				<span class="ui_mr10 qz_btn_reply"><qz:more 
					action="<?cs var:qz_metadata.comments.qz_more.action ?>" 
					param="<?cs var:qz_metadata.comments.qz_more.param ?>" 
					charset="<?cs var:qz_metadata.comments.qz_more.charset ?>" 
					link= 1 
				>评论<?cs call:setCount(qz_metadata.totalcomment,0) ?></qz:more></span>
			<?cs else ?>
				<qz:reply 
				unrend="true" 
				<?cs if:item.qz_reply.action > 0 ?> action="<?cs var:item.qz_reply.action ?>" <?cs /if ?> 
				<?cs if:item.qz_reply.charset > 0 ?> charset="<?cs var:item.qz_reply.charset ?>"<?cs /if ?>
				 type="link" version="6.3"  name="reply" 
				<?cs if:item.qz_reply.param > 0 ?> param="<?cs var:item.param ?>"<?cs /if ?> 
				<?cs if:item.qz_reply.maxlength > 0 ?> maxLength="<?cs var:item.qz_reply.maxlength ?>"<?cs /if ?>
				<?cs if:item.qz_reply.tuin > 0 ?> tuin="<?cs var:item.qz_reply.tuin ?>" <?cs /if ?> 
				<?cs if:item.qz_reply.config > 0 ?> tuin="<?cs var:item.qz_reply.config ?>" <?cs /if ?> 
				><a class="c_tx ui_mr10 qz_btn_reply" href="javascript:;">评论<?cs call:setCount(qz_metadata.totalcomment,0) ?></a></qz:reply>
			<?cs /if ?>
		<?cs /def ?>
		<?cs def:setText(t) ?>
			<?cs if:t=='转载'?>
				转载<?cs if:qz_metadata.qz_data.key1.ZZ.cnt>0 ?><?cs call:setCount(qz_metadata.qz_data.key2.ZZ.cnt,qz_metadata.qz_data.key1.ZZ.cnt) ?><?cs /if ?>
			<?cs elif:t=="我也要参与"||t=='参与' ?>
				参与<?cs if:qz_metadata.qz_data.key1.ACT.cnt>0 ?><?cs call:setCount(qz_metadata.qz_data.key2.ACT.cnt,qz_metadata.qz_data.key1.ACT.cnt) ?><?cs /if ?>
			<?cs else ?>
				<?cs var:t ?>
			<?cs /if ?>
		<?cs /def ?>
		<?cs def:btn-items(item) ?>
			<?cs if:item.type == 1 ?><?cs #:评论 ?>
				<?cs call:btn-reply(item) ?>
			<?cs elif:item.type == 2 ?><?cs #:转发 ?>
				<a 
					data-cmd="qz_popup"  
					href="javascript:void(0)" 
					data-version="4" 
					data-isnewtype="1" 
					data-type="RetweetBox" 
					data-needcontainer="1" 
					data-src="/qzone/app/mood/retweetBoxFacade.js" 
					class="c_tx ui_mr10" 
					data-link="/qzonestyle/qzone_app/app_feeds_v1/mood_feeds.css">
					转发<?cs call:setCount(qz_metadata.qz_data.key1.SJDP.cnt,qz_metadata.qz_data.key1.SJDQ.cnt)  ?>
				</a>
			<?cs elif:item.type == 3 ?><?cs #:分享 ?>
				<a 
					data-cmd="qz_popup" 
					href="javascript:void(0)" 
					title="<?cs if:item.qz_popup.title ?><?cs var:item.qz_popup.title ?><?cs else ?>分享<?cs /if ?>" 
					class="c_tx ui_mr10" 
					data-height="<?cs var:item.qz_popup.height ?>" 
					data-width="<?cs var:item.qz_popup.width ?>" 
					data-version="1" 
					data-src="<?cs var:item.qz_popup.src ?>" 
					data-param="<?cs var:item.qz_popup.param ?>"
				>
					分享<?cs call:setCount(qz_metadata.qz_data.key1.ZF.cnt,0) ?>
				</a>
			<?cs elif:item.type == 4 ?><?cs #:动作是js脚本 ?><?cs #目前只有v6升级在用，2012年把它干掉 ?>
				<a class="c_tx ui_mr10" href="javascript:;" onclick="<?cs var:item.javascript ?>"><?cs call:setText(item.text) ?></a>
			<?cs elif:item.type == 5 ?><?cs #:转播 ?>
				<a 
					data-cmd="qz_popup" 
					href="javascript:void(0)" 
					data-version="4" 
					data-type="<?cs var:item.qz_popup.type ?>" 
					data-param="<?cs var:item.qz_popup.param ?>" 
					data-needcontainer="<?cs var:item.qz_popup.needcontainer ?>" 
					data-src="<?cs var:item.qz_popup.src ?>" 
					data-link="<?cs var:item.qz_popup.link ?>" 
					data-charset="<?cs var:item.qz_popup.charset ?>" 
					class="c_tx ui_mr10" 
					title="<?cs var:item.qz_popup.title ?>">
					转播<?cs call:setCount(qz_metadata.qz_data.key2.WBZB.cnt,qz_metadata.qz_data.key1.WBZB.cnt) ?>
				</a>
			<?cs elif:item.type == 6 ?><?cs #:更多 ?>
				<a class="ui_mr10 c_tx" href="<?cs var:item.action ?>'" target="_blank">查看更多微博</a>
			<?cs elif:item.type == '101' ?>
				<a class="ui_mr10 c_tx" href="<?cs var:item.url ?>"  target="_blank" ><?cs call:setText(item.text) ?></a>
			<?cs elif:item.type == 'url' ?>
				<a class="ui_mr10 c_tx" href="<?cs var:item.url ?>"  target="_blank" ><?cs call:setText(item.text) ?></a>
			<?cs elif:item.type == 'qz_popup' ?>
				<a 
					data-cmd="qz_popup" 
					href="javascript:void(0)" 
					title="<?cs var:item.qz_popup.title ?>" 
					data-height="<?cs var:item.qz_popup.height ?>" 
					data-width="<?cs var:item.qz_popup.width ?>" 
					data-version="<?cs var:item.qz_popup.version ?>" 
					data-src="<?cs var:item.qz_popup.src ?>" 
					class="c_tx ui_mr10" 
					data-param="<?cs var:item.qz_popup.param ?>"><?cs call:setText(item.text) ?>
				</a>
			<?cs elif:item.type == "checkin"?>
				<a 
					data-cmd="qz_popup" 
					href="javascript:void(0)" 
					data-src="/qzone/app/mood/info/checkin.html" 
					data-width="318" 
					data-height="400" 
					data-version ="5" 
					class="c_tx ui_mr10" 
					data-config="id:checkin|closeButtonColor:#D4CFC2;font-weight:400;|arrowOffset:10|noflush:true">
					我也签到
				</a>
			<?cs elif:item.type == "qz_app"?>
				<?cs if:item.aid == "appsetup"?>
					<a class="ui_mr10 c_tx" href="http://rc.qzone.qq.com/appsetup"><?cs call:setText(item.text) ?></a>
				<?cs else ?>
					<a class="ui_mr10 c_tx" target="_blank" href="<?cs alt:item.url?>http://rc.qzone.qq.com/myhome/<?cs var:item.aid?><?cs /alt?>">
						<?cs call:setText(item.text) ?>
					</a>
				<?cs /if ?>
			<?cs elif:item.type == "visitor" ?>
				<?cs if:qz_metadata.metadata.appid == 2 ?>
					<?cs if:qz_metadata.qz_data.version!=1 ?>
						<a class="ui_mr10 c_tx" href="javascript:;" config="<?cs var:qz_metadata.metadata.blogid ?>" uin="<?cs var:qz_metadata.metadata.uin ?>">浏览</a>
					<?cs elif:qz_metadata.qz_data.key1.RZRD.cnt>0 ?>
						<qz:plugin name="Visitor" 
							config="<?cs var:qz_metadata.metadata.appid ?>|
							<?cs if:qz_metadata.metadata.appid == 2?>
								<?cs var:qz_metadata.metadata.blogid ?>
							<?cs else ?>
								<?cs var:qz_metadata.qz_data.likedid?>
							<?cs /if?>
							|<?cs var:qz_metadata.metadata.uin ?>" >
							<a class="ui_mr10 c_tx" href="javascript:;">浏览<?cs call:setCount(qz_metadata.qz_data.key1.RZRD.cnt,0) ?></a>
						</qz:plugin>
					<?cs else ?>
						<a class="ui_mr10 c_tx" href="javascript:;">浏览(0)</a>
					<?cs /if ?>
				<?cs /if ?>
				<?cs if:qz_metadata.metadata.appid == 4 ?>
					<?cs if:qz_metadata.qz_data.version!=1 ?>
						<a class="ui_mr10 c_tx" href="javascript:;" 
							config="9;<?cs var:qz_metadata.content_box.media.albumid ?>%7C<?cs if:subcount(qz_metadata.content_box.media.media.0) > 0 ?><?cs var:qz_metadata.content_box.media.media.0.largeid ?><?cs else ?><?cs var:qz_metadata.content_box.media.media.largeid ?><?cs /if ?>" uin="<?cs var:qz_metadata.metadata.uin ?>">浏览</a>
					<?cs elif:qz_metadata.qz_data.key1.PRD.cnt>0 ?>
					<qz:plugin name="Visitor" 
						config="<?cs var:qz_metadata.metadata.appid ?>|9;
						<?cs var:qz_metadata.content_box.media.albumid ?>%7C
						<?cs if:subcount(qz_metadata.content_box.media.media.0) > 0 ?>
							<?cs var:qz_metadata.content_box.media.media.0.largeid ?>
						<?cs else ?>
							<?cs var:qz_metadata.content_box.media.media.largeid ?>
						<?cs /if ?>
						|
						<?cs var:qz_metadata.metadata.uin ?>" >
							<a class="ui_mr10 c_tx" href="javascript:;">浏览<?cs call:setCount(qz_metadata.qz_data.key1.PRD.cnt,0) ?></a>
					</qz:plugin>
					<?cs else ?>
						<a class="ui_mr10 c_tx" href="javascript:;">浏览(0)</a>
					<?cs /if ?>
				<?cs /if ?>
			<?cs elif:item.type == 'script'?>
				<?cs if:item.biz == 'mall' ?>
					<a class="c_tx ui_mr10" href="javascript:;" onclick="QZONE.ICFeeds.Interface.checkForDress2(this,'<?cs var:item.param[0]?>', '<?cs var:item.param[1] ?>')">
					<?cs call:setText(item.text) ?>
					</a>
				<?cs elif:item.biz == 'lookaround' ?>
					<a class="c_tx ui_mr10" href="javascript:;" onclick="QZONE.ICPlugin.LookAround.showDialog();return false;">随便看看</a>
				<?cs /if ?>
			<?cs elif:item.type == 'dress' ?><?cs #已经转移到item.type == 'script' && item.biz == 'mall'.@2012可以删除 ?>
				<a class="ui_mr10 c_tx" href="javascript:;" onclick="return QZONE.ICFeeds.Interface.checkForDress(this,'<?cs var:qz_metadata.metadata.uin ?>','<?cs var:qz_metadata.metadata.blogid ?>',<?cs var:qz_metadata.metadata.srctype ?>)"><?cs call:setText(item.text) ?></a>
			<?cs elif:item.type == 'like'?>
				<a class="ui_mr10 c_tx" href="javascript:;" onclick="g_Parent.QZONE.FrontPage.addILike('<?cs var:item.uin ?>',function(o){if(o.ret==-20){g_Parent.QZONE.FrontPage.showMsgbox('您已经关注此空间，请勿重复操作。',0,2000)}if(!o.ret){g_Parent.QZONE.FrontPage.showMsgbox('关注成功', 4, 2000)}},{scene:1});return false;"  title='点击"我也关注",即可在个人中心收取该空间动态'><?cs call:setText(item.text) ?></a>
			<?cs /if ?>
		<?cs /def ?>
		<?cs if:subcount(qz_metadata.operate.opt.0) > 0 ?>
			<?cs call:randerLike(mod) ?>
			<?cs each:item=qz_metadata.operate.opt ?>
				<?cs call:btn-items(item) ?>
			<?cs /each ?>
		<?cs else  ?>
			<?cs call:randerLike(mod) ?>
			<?cs call:btn-items(qz_metadata.operate.opt) ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs def:oprModCaller(mods) ?>
		<?cs each:mod = mods ?>
			<?cs if:mod.name == "time" ?>
				<?cs call:time(mod) ?>
			<?cs /if ?>
			<?cs if:mod.name == "source" ?>
				<?cs include:"opr_source.cs"?>
			<?cs /if ?>
			<?cs if:mod.name == "btn" ?>
				<span class='feeds_tp_operate_v2 f_detail_op'>
				<?cs call:btn(mod) ?>
				</span>
			<?cs /if ?>
		<?cs /each ?>
	<?cs /def ?>
	<p class="f_detail c_tx3">
	<?cs call:oprModCaller(mod.mod) ?>
	</p>
<?cs /def ?>