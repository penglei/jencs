<?cs #输出用户昵称?>
<?cs def:writeUserName(uin,defaultName) ?>
	<?cs if:string.length(qz_metadata.remarkPool["u"+uin])>0 ?>
		<?cs var:html_encode(qz_metadata.remarkPool["u"+uin],1) ?>
	<?cs else ?>
		<?cs var:html_encode(defaultName, 1) ?><?cs #存储里的数据做了xmlEscape，读出来的时候没有转，所以这个地方不用做编码了?>
		<?cs #var:defaultName?><?cs #差点又被XSS坑死了?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:writeUserHome(user)?>
	http://user.qzone.qq.com/<?cs var:user.uin ?>
<?cs /def?>

<?cs #输出用户头像地址?>
<?cs def:getLogo_30(user,type) ?>
	<?cs if:user.type ?>
		<?cs set:uicon_type=user.type ?>
	<?cs else ?>
		<?cs set:uicon_type=type ?>
	<?cs /if ?>

	<?cs if:uicon_type==1 ?>
		<?cs if:string.length(qz_metadata.logoPool30['u'+user.uin])>0 ?>
			<?cs var:qz_metadata.logoPool30['u'+user.uin] ?>
		<?cs else ?>
			<?cs set:uinmod = user.uin % 4 + 1 ?>
			http://qlogo<?cs var:uinmod ?>.store.qq.com/qzone/<?cs var:user.uin ?>/<?cs var:user.uin ?>/30
		<?cs /if ?>
	<?cs elif:uicon_type==2 ?>
		<?cs if:string.length(qz_metadata.xylogoPool30['u'+user.uin])>0 ?>
			<?cs var:qz_metadata.xylogoPool30['u'+user.uin] ?>
		<?cs else ?>
			http://xy.store.qq.com/campus/<?cs var:user.img ?>/30
		<?cs /if ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:getLogo_50(uin,type) ?>
	<?cs if:user.type ?>
		<?cs set:uicon_type=user.type ?>
	<?cs else ?>
		<?cs set:uicon_type=type ?>
	<?cs /if ?>
	<?cs if:uicon_type==1 ?>
		<?cs if:string.length(qz_metadata.logoPool50['u'+user.uin])>0 ?>
			<?cs var:qz_metadata.logoPool50['u'+user.uin] ?>
		<?cs else ?>
			<?cs set:uinmod = user.uin % 4 + 1 ?>
			http://qlogo<?cs var:uinmod ?>.store.qq.com/qzone/<?cs var:user.uin ?>/<?cs var:user.uin ?>/50
		<?cs /if ?>
	<?cs elif:uicon_type==2 ?>
		<?cs if:string.length(qz_metadata.xylogoPool50['u'+user.uin])>0 ?>
			<?cs var:qz_metadata.xylogoPool50['u'+user.uin] ?>
		<?cs else ?>
			http://xy.store.qq.com/campus/<?cs var:user.img ?>/50
		<?cs /if ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #输出用户头像 ?>
<?cs def:userIcon(user) ?>
	<?cs if:user.type == 1 ?><?cs #:Qzone用户 ?>
		<a href="<?cs call:writeUserHome(user)?>" target="_blank">
			<img title="<?cs call:writeUserName(user.uin,user.name) ?>" alt="<?cs call:writeUserName(user.uin,user.name)?>" src="<?cs call:getLogo_50(user,1) ?>" />
		</a>
	<?cs elif:user.type == 2 ?><?cs #:朋友用户 ?>
		<img alt="<?cs var:html_encode(user.name, 1) ?>" src="<?cs call:getLogo_50(user,2) ?>" />
	<?cs /if ?>
<?cs /def ?>

<?cs def:userIconStyle(user, style)?>
	<a href="<?cs call:writeUserHome(user)?>" class="<?cs if:style == 'act'?>act_ava<?cs /if?>" target="_blank">
		<img class="q_namecard <?cs if:style == 'act'?>sslist_ava<?cs /if?>" link="nameCard_<?cs var:user.uin ?> des_<?cs var:user.uin ?>" alt="<?cs call:writeUserName(user.uin,user.name) ?>" src="<?cs call:getLogo_50(user,1) ?>" />
	</a>
<?cs /def?>

<?cs #:用户链接组件 ?>
<?cs def:userLink(user, prefix) ?>
	<?cs if:user.who == 1 || user.type == 1?>
		<a href="<?cs call:writeUserHome(user)?>" class="q_namecard c_tx hotclick" link="nameCard_<?cs var:user.uin ?>" target="_blank" data-hctag="spcaretab.goqzone.nickname" data-hcsuffix="feedpage">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<span><?cs call:writeUserName(user.uin,user.name) ?></span></a>
	<?cs elif:user.who == 2 || user.type == 2 ?>
		<a href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:user.uin ?>" class="c_tx hotclick" target="_blank" data-hctag="spcaretab.link.click" data-hcsuffix="feed">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<?cs var:html_encode(user.name, 1) ?></a>
	<?cs elif:user.who == 3 || user.type == 3 ?>
		<a href="http://rc.qzone.qq.com/myhome/weibo/profile/<?cs var:user.uin ?>" class="c_tx hotclick" target="_blank" data-hctag="spcaretab.link.click" data-hcsuffix="feed">
		<?cs if:string.length(prefix) > 0 ?>
			<?cs var:prefix ?>
		<?cs /if ?>
		<?cs var:html_encode(user.name, 1) ?></a>
	<?cs else ?>
		<?cs var:html_encode(user.name, 1) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:richContent-items(item) ?>
	<?cs if:item.newsec ?><p><?cs /if?><?cs #换行 ?>
	<?cs if:item.type == "nick" ?>
		<?cs call:userLink(item , "") ?>
	<?cs elif:item.type == "url" ?>
		<a class="c_tx hotclick" href="<?cs var:html_encode(item.url, 1) ?>" target="_blank" data-hctag="spcaretab.link.click" data-hcsuffix="feed"><?cs if:item.text ?><?cs var:item.text ?><?cs else ?><?cs var:item.url ?><?cs /if ?></a>
	<?cs elif:item.type == "text" ?>
		<?cs var:item.text ?>
	<?cs else ?>
		<?cs var:item ?>
	<?cs /if ?>
	<?cs if:item.newsec ?></p><?cs /if?>
<?cs /def ?>
<?cs #:富文本内容 ?>
<?cs def:richContent(cons) ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 || string.length(cons.con.0) > 0 ?>
		<?cs loop:i = 0, subcount(cons.con) - 1, 1 ?>
			<?cs call:richContent-items(cons.con[i]) ?>
		<?cs /loop ?>
	<?cs elif:cons.con || subcount(cons.con) > 0 || string.length(cons.con) > 0 ?>
			<?cs call:richContent-items(cons.con) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #{{文字内容区----?>
<?cs def:contentSubTitleItem(item) ?>
	<?cs if:item.type == "url"?>
		<a href="<?cs var:item.url ?>" class="c_tx" target="_blank"><?cs var:item.text ?></a>
	<?cs elif:item.type == "text"?>
		<?cs var:item.text ?>
	<?cs elif:item.type == "nick" ?>
		<?cs call:userLink(item, "") ?>
	<?cs else ?>
		<?cs var:item ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:contentBoxCommon-txt(mod) ?>
	<?cs if:qz_metadata.content_box.detailurl || 
			subcount(qz_metadata.content_box[qz_content_name]) || 
			subcount(qz_metadata.content_box.title) 
		?>
		<div class="txt_box">
			<?cs if:subcount(qz_metadata.content_box.title) > 0?>
			<p>
				<?cs if:subcount(qz_metadata.content_box.title.con.0) > 0 ?>
					<?cs each:item = qz_metadata.content_box.title.con ?>
						<?cs call:contentSubTitleItem(item) ?>
					<?cs /each ?>
				<?cs elif:subcount(qz_metadata.content_box.title.con) > 0 ?>
					<?cs call:contentSubTitleItem(qz_metadata.content_box.title.con) ?>
				<?cs elif:subcount(qz_metadata.content_box.title) > 0 ?>
					<?cs if:qz_metadata.content_box.title.url ?>
						<a href="<?cs var:qz_metadata.content_box.title.url ?>" target="_blank"><?cs var:qz_metadata.content_box.title.text ?></a>
					<?cs /if ?>
				<?cs /if ?>
			</p>
			<?cs /if ?>

			<?cs #真正的文字内容?>
			<?cs if:subcount(qz_metadata.content_box[qz_orginuser_name]) ?><?cs call:userLink(qz_metadata.content_box[qz_orginuser_name],'') ?>: <?cs /if ?>
			<?cs call:richContent(qz_metadata.content_box[qz_content_name]) ?>

		</div>
	<?cs /if ?>

<?cs /def ?>
<?cs ##}}?>

<?cs ##{{--------内容区图片-------?>
<?cs def:content-image-simpleView(mediaImg)?>
	<a class="img_box_item qz_img_viewer hotclick" href="#" data-hctag="spcaretab.pic.click" data-hcsuffix="feed"
		 data-imgurl="<?cs var:html_encode(mediaImg.mydef_imgurl, 1)?>"
		 data-action="1"<?cs #普通弹框?>
	>
		<img src="<?cs var:html_encode(mediaImg.mydef_imgurl, 1)?>" onload="QzFeedsResizeImage(this, <?cs alt:cntbox_img_count?>0<?cs /alt?>)" />
	</a>
<?cs /def?>

<?cs def:content-image-slideView(mediaImg, param)?>
	<a class="img_box_item qz_img_viewer" href="#"
		 data-action="2"<?cs #大图弹框?>
		 data-param="<?cs var:param?>"<?cs #这里不需要做html_encode，都是系统内id?>
		 data-imgurl="<?cs var:html_encode(mediaImg.mydef_imgurl, 1)?>"
	>
		<?cs #图片压缩算法?>
		<?cs #压缩的算法全部由前台实现，这里值提供图片展示的方式：1张|2宫格|4宫格?>
		<img src="<?cs var:html_encode(mediaImg.mydef_imgurl, 1)?>" onload="QzFeedsResizeImage(this, <?cs alt:cntbox_img_count?>0<?cs /alt?>)" />
	</a>
<?cs /def?>

<?cs def:content-image-backToSource(mediaImg)?>
	<a class="img_box_item" href="<?cs var:mediaImg.action ?>" target="_blank" data-hctag="spcaretab.goqzone.blogpic"><?cs #图片回源?>
		<img src="<?cs var:mediaImg.mydef_imgurl?>" onload="QzFeedsResizeImage(this, <?cs alt:cntbox_img_count?>0<?cs /alt?>)" />
	</a>
<?cs /def?>

<?cs #图片含有newtype的处理?>
<?cs def:content-imageItem(mediaImg, index)?>
	<?cs #只有跳转地址的图片，无法使用弹框。目前日志的feeds基本都是这样?>
	<?cs if:string.length(mediaImg.action)>0 ?>
		<?cs call:content-image-backToSource(mediaImg)?>
	<?cs else ?>
		<?cs if:subcount(mediaImg.qz_popup)?>
			<?cs set:_param = mediaImg.qz_popup.param?>
		<?cs else ?>
			<?cs set:_param = qz_metadata.metadata.uin + "|"
							+ qz_metadata.content_box.media.albumid + "|"
							+ mediaImg.largeid + "|"
							+ mediaImg.src?>
		<?cs /if?>
		<?cs call:content-image-slideView(mediaImg, _param)?>
	<?cs /if?>
<?cs /def?>

<?cs #无newtype的图片数据处理?>
<?cs def:content-imageItem-deprecated(mediaImg, index)?>
	<?cs if:subcount(mediaImg.qz_popup)?>
		<?cs set:_param = mediaImg.qz_popup.param?>
		<?cs call:content-image-slideView(mediaImg, _param)?>
	<?cs else ?>
		<?cs call:content-image-simpleView(mediaImg)?>
	<?cs /if?>
<?cs /def?>

<?cs #video的展示?>
<?cs def:content-mediaBox-video(media)?>
	<?cs #视频直接跳空间详情页?>
	<div class="video_box">
		<a class="video_img" href="<?cs var:qz_metadata.metadata.commentsurl?>" target="_blank">
			<img src="<?cs var:media.src?>" onload="QzFeedsResizeImage(this, 0)" />
		</a>
		<a class="video_bt" href="<?cs var:qz_metadata.metadata.commentsurl?>" target="_blank"><i class="ui_icon icon_vedio_play"></i></a>
		<div class="video_info">点击去空间播放</div>
	</div>
<?cs /def?>

<?cs #music的展示?>
<?cs def:content-mediaBox-music(music)?>
	<div class="f_file bor2">
		<div class="file_icon bg_bor2">
			<i class="ui_icon icon_type_music"></i>
		</div>
		<div class="file_name">
			<a href="<?cs alt:music.url?><?cs var:qz_metadata.metadata.commentsurl?><?cs /alt?>" target="_blank"><?cs var:music.title?></a>
		</div>
	</div>
<?cs /def?>

<?cs #attachment的展示?>
<?cs def:content-attachmentBox(attachment)?>
	<?cs if:subcount(attachment.attachfile)?>
		<div class="f_file bor2">
			<div class="file_icon bg_bor2">
				<i class="ui_icon icon_type_attachment"></i>
			</div>
			<div class="file_name">
				<a href="<?cs var:qz_metadata.metadata.commentsurl?>" target="_blank"><?cs var:attachment.attachfile.name?></a>
			</div>
		</div>
	<?cs /if?>
<?cs /def?>


<?cs #内容区的图片展示框?>
<?cs def:contentBoxCommon-media(mod) ?>

	<?cs def:cntBoxMedia-img-item(media, index)?>
		<?cs #确定img src ?>
		<?cs if:media.picmarkflag==0 || !media.picmarksrc?>
			<?cs set:media.mydef_imgurl  =media.src ?>
		<?cs else ?>
			<?cs set:media.mydef_imgurl = media.picmarksrc ?>
		<?cs /if ?>

		<?cs if:qz_metadata.content_box.media.newtype ?>
			<?cs call:content-imageItem(media, index)?>
		<?cs else ?>
			<?cs #TODO这种老feeds暂时不管吧，遇到了再处理?>
			<?cs call:content-imageItem-deprecated(media, index)?>
		<?cs /if?>
	<?cs /def?>

	<?cs if:subcount(qz_metadata.music_box)?>
		<?cs call:content-mediaBox-music(qz_metadata.music_box)?><?cs #音乐feeds?>
	<?cs elif:subcount(qz_metadata.attach)?>
		<?cs call:content-attachmentBox(qz_metadata.attach)?><?cs #附件?>
	<?cs elif: subcount(qz_metadata.content_box.media.media) && qz_metadata.content_box.media.media.type == "video"?>
		<?cs call:content-mediaBox-video(qz_metadata.content_box.media.media)?><?cs #视频只会有一个?>
	<?cs #图片处理?>
	<?cs elif: qz_metadata.content_box.media|| 
				qz_metadata.content_box.media.media.src || 
				qz_metadata.content_box.media.media.type || 
				qz_metadata.content_box.media.media.0.src
		?>

		<?cs if:subcount(qz_metadata.content_box.media.media.0)?>
			<?cs set:cntbox_img_count = subcount(qz_metadata.content_box.media.media)?>
			<?cs if:cntbox_img_count == 3?>
				<?cs set:cntbox_img_count = 2?>
			<?cs elif:cntbox_img_count > 4?>
				<?cs set:cntbox_img_count = 4?>
			<?cs /if?>
		<?cs else ?>
			<?cs set:cntbox_img_count = 1?>
		<?cs /if?>

		<?cs #set:cntbox_img_count = 1?><?cs #xTODO 多图情况出现很丑的展示，暂时只展示一张?>
		<?cs #图片只有1、2、4张三种展现方式?>
		<div class="img_box img_box_<?cs var:cntbox_img_count?>">
			<?cs if:cntbox_img_count == 1?>
				<?cs if:subcount(qz_metadata.content_box.media.media.0)?>
					<?cs call:cntBoxMedia-img-item(qz_metadata.content_box.media.media.0, 1)?>
				<?cs else ?>
					<?cs call:cntBoxMedia-img-item(qz_metadata.content_box.media.media, 1)?>
				<?cs /if?>
			<?cs else ?>
				<?cs loop: i = 0, cntbox_img_count - 1, 1?>
					<?cs call:cntBoxMedia-img-item(qz_metadata.content_box.media.media[i], i)?>
				<?cs /loop?>
			<?cs /if?>
		</div>
	<?cs /if?>
<?cs /def ?>
<?cs #}}-----内容区图片-----?>

<?cs def:contentBoxCommon(type, mod) ?>

	<?cs #:元数据兼容 ?>
	<?cs set:qz_content_name = "content" ?>
	<?cs if:subcount(qz_metadata.content_box.content_2) >0 ?>
		<?cs set:qz_content_name = "content_2" ?>
	<?cs /if ?>

	<?cs set:qz_orginuser_name = "orginuser" ?>
	<?cs if:subcount(qz_metadata.content_box.orginuser_2) > 0 ?>
		<?cs set:qz_orginuser_name = "orginuser_2" ?>
	<?cs /if ?>

	<?cs if:subcount(qz_metadata.content_box.title) > 0 || 
			subcount(qz_metadata.content_box.media) || 
			subcount(qz_metadata.content_box[qz_content_name]) ||
			subcount(qz_metadata.attach) ||
			subcount(qz_metadata.music_box)?>
	<div class="<?cs var:type ?>">

		<?cs if:subcount(qz_metadata.magicemotion) || 
				subcount(qz_metadata.content_box.title) > 0 || 
				subcount(qz_metadata.content_box[qz_content_name]) ||
				subcount(qz_metadata.music_box) ||
				subcount(qz_metadata.content_box) ||
				subcount(qz_metadata.attach) ||
				qz_metadata.content_box.detailurl || 
				qz_metadata.content_box.media ||
				qz_metadata.content_box.media.media.src || 
				qz_metadata.content_box.media.media.0.src || 
				qz_metadata.content_box.media.media.0.avatar ?>

			<?cs #:文本区域 ?>
			<?cs call:contentBoxCommon-txt(mod) ?>
			<?cs #:图片或者视频 ?>
			<?cs call:contentBoxCommon-media(mod) ?>

		<?cs /if ?>
	</div>
	<?cs /if ?>
<?cs /def ?>

<?cs #:有原文的表现 ?>
<?cs def:contentBoxLeftBorBg(mod) ?>
	<?cs call:contentBoxCommon("f_ct f_ct_rt bor2", mod) ?>
<?cs /def ?>

<?cs #:普通的表现 ?>
<?cs def:contentBox(mod) ?>
	<?cs call:contentBoxCommon("f_ct", mod) ?>
<?cs /def ?>

<?cs #:一级元件召唤 ?>
<?cs def:modCaller(summaryMod) ?>
	<?cs def:mods(mod) ?>
		<?cs if:mod.name == "contentBox" ?>
			<?cs call:contentBox(mod) ?>
		<?cs elif:mod.name == "contentBoxLeftBorBg" ?>
			<?cs call:contentBoxLeftBorBg(mod) ?>
		<?cs elif:mod.name == "opr" ?><?cs #顺序不一样了，这些都没用了?>
			<?cs #call:opr(mod) ?>
		<?cs elif:mod.name == "comment" ?>
			<?cs #call:comment(mod) ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs if:subcount(summaryMod.mod.0) > 0 ?>
		<?cs each:mod = summaryMod.mod ?>
			<?cs call:mods(mod) ?>
		<?cs /each ?>
	<?cs else ?>
		<?cs call:mods(summaryMod.mod) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #-------------------- ?>
<?cs #{mod_layout.cs}?>
<?cs #-------------------- ?>

<?cs #从布局版本开始执行?>
<?cs def:start(module) ?>
	<?cs def:versionCaller(mod,ver) ?>
		<?cs #只有存在多版本布局时，前面的假设成立?>
		<?cs set:layout_v = "layout_v" + ver ?>
		<?cs if:subcount(mod[layout_v]) > 0 ?>
			<?cs call:modCaller(mod[layout_v]) ?>
		<?cs else ?>
			<?cs call:modCaller(mod) ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs #假设所有feed的存在布局版本?>
	<?cs set:qz_layout_version = 1 ?>
	<?cs if:qz_metadata.layout_version[module] ?>
		<?cs set:qz_layout_version = qz_metadata.layout_version[module] ?>
	<?cs /if?>
	<?cs if:module == "summary" ?>
		<?cs call:versionCaller(qz_metadata.qz_layout_summary, qz_layout_version) ?>
	<?cs elif:module == "title" ?>
		<?cs call:versionCaller(qz_metadata.qz_layout_title, qz_layout_version) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:内容区?>
<?cs call:start("summary")?>

<?cs def:extendInfo() ?>
	<?cs set:qz_extendinfo_name = "extend_info" ?>
	<?cs if:subcount(qz_metadata.extend_info_2) > 0 ?>
		<?cs set:qz_extendinfo_name = "extend_info_2" ?>
	<?cs /if ?>

	<?cs def:extendInfo-items(item) ?>
		<?cs if:item.type == "picnum" ?>
			<?cs if:item.count > 0?>
			<span class="c_tx3<?cs #ui_mr10?>">共有<?cs var:item.count ?>张图片</span>
			<?cs /if?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs def:extendInfo-by-others()?>
		<?cs if:qz_metadata.content_box.media.albumtotalpic?>
			<div class="f_detail">
				<span class="c_tx3">共有<?cs var:qz_metadata.content_box.media.albumtotalpic ?>张图片</span>
			</div>
		<?cs /if?>
	<?cs /def?>

	<?cs with:exinfo = qz_metadata[qz_extendinfo_name] ?>
	<?cs if:exinfo.info.0 || subcount(exinfo.info.0) > 0 ?>
		<div class="f_detail">
		<?cs loop:i = 0, subcount(exinfo.info) -1, 1 ?>
			<?cs call:extendInfo-items(exinfo.info[i]) ?>
		<?cs /loop ?>
		</div>
	<?cs elif:exinfo.info.type ?>
		<div class="f_detail">
		<?cs call:extendInfo-items(exinfo.info) ?>
		</div>
	<?cs /if ?>
	<?cs /with ?>
	<?cs call:extendInfo-by-others()?>
<?cs /def ?>
<?cs #附加信息区?>
<?cs call:extendInfo()?>

<?cs #//生成赞的信息?>
<?cs def:data_like()?>
<?cs  if:string.length(qz_metadata.qz_data.key1) > 0?>

	<?cs #得到赞的key?>
	<?cs if:qz_metadata.qz_data.key2.LIKE.cnt > 0?>
		<?cs set:_qz_data_key = "key2"?>
	<?cs elif:qz_metadata.qz_data.key1.LIKE.cnt > 0?>
		<?cs set:_qz_data_key = "key1"?>
	<?cs else ?>
		<?cs set:_qz_data_key = "key2"?>
	<?cs /if?>

	<?cs #/*call:qfv("like.keyname", _qz_data_key)*/?>

	<?cs if:qz_metadata.qz_data.key2?>
		<?cs set:_like_unikey = qz_metadata.qz_data.key2?>
	<?cs elif:qz_metadata.qz_data.key1?>
		<?cs set:_like_unikey = qz_metadata.qz_data.key1?>
	<?cs /if?>

	<?cs if:qz_metadata.qz_data[_qz_data_key].LIKE.isliked ?>
		<?cs set:_isliked = 1?>
	<?cs else ?>
		<?cs set:_isliked = 0?>
	<?cs /if?>

	<?cs #/*赞数据里可用的用户总数(可能会小于赞的总数)*/?>
	<?cs if:subcount(qz_metadata.qz_data[_qz_data_key].LIKE.user)?>
		<?cs set:_likeinfo = subcount(qz_metadata.qz_data[_qz_data_key].LIKE.user)?>
	<?cs elif:subcount(qz_metadata.qz_data[_qz_data_key].LIKE.user.0)?>
		<?cs set:_likeinfo = subcount(qz_metadata.qz_data[_qz_data_key].LIKE.user.0)?>
	<?cs else ?>
		<?cs set:_likeinfo = 0?>
	<?cs /if?>

	<?cs #/*赞的总数*/?>
	<?cs if:qz_metadata.qz_data[_qz_data_key].LIKE.cnt > 0?>
		<?cs set:_like_count = qz_metadata.qz_data[_qz_data_key].LIKE.cnt?>
	<?cs else ?>
		<?cs set:_like_count = 0?>
	<?cs /if?>

<?cs /if?>
<?cs /def?>
<?cs call:data_like()?><?cs #先保证赞的数据都已经生成?>

<?cs def:opr()?>
	<?cs #下面三项通过样式控制是否出现。在这里一并生成ps:这里有结构改变，若想看以前，则查logo?>
	<?cs if:qz_metadata.qz_data.key2.LIKE.cnt>0 ?>
		<?cs set:qz_data_key='key2' ?>
	<?cs elif:qz_metadata.qz_data.key1.LIKE.cnt>0 ?>
		<?cs set:qz_data_key='key1' ?>
	<?cs else ?>
		<?cs set:qz_data_key='key2' ?>
	<?cs /if ?>
	<?cs if:subcount(qz_metadata.qz_data[qz_data_key].LIKE.user)>0 ?>
		<?cs set:likeinfo=subcount(qz_metadata.qz_data[qz_data_key].LIKE.user) ?>
	<?cs if:likeinfo>3 ?>
		<?cs set:likeinfo=3 ?>
	<?cs /if ?>
	<?cs elif:subcount(qz_metadata.qz_data[qz_data_key].LIKE.user[0]) ?>
		<?cs set:likeinfo = 1 ?>
	<?cs else ?>
		<?cs set:likeinfo = 0 ?>
	<?cs /if ?>
	<?cs #加入统一权限判断 1公开 4QQ好友 16指定人 64仅自己?>
	<?cs if:qz_metadata.metadata.privacy > 3?>
		<?cs set:paivacy = 1 ?>
	<?cs /if?>

	<div class="op_btn"><?cs #只有个人中心?>
		<a href="#" class="ui_btn op_btn_like hotclick" data-likecnt="<?cs alt:qz_metadata.qz_data[qz_data_key].LIKE.cnt ?>0<?cs /alt ?>" data-showcount="<?cs var:likeinfo ?>" data-hctag="spcaretab.<?cs if:_isliked?>unlike<?cs else ?>like<?cs /if ?>.click">
			<i class="ui_icon icon_op_like"></i>
			<?cs if:_isliked?>已<?cs /if?>赞
		</a>
		<a href="#" class="ui_btn op_btn_comment hotclick" data-role="commentBtn" data-hctag="spcaretab.comment.click"><i class="ui_icon icon_op_comment"></i>评</a>
		<a href="#" class="ui_btn op_btn_rt hotclick" data-hctag="spcaretab.share.click" data-fwddenied="<?cs var:paivacy?>"><i class="ui_icon icon_op_rt"></i>转</a>
	</div>
<?cs /def?>
<?cs #操作按钮?>

<?cs #赞详情 ?>
<?cs def:randerLikeInfo() ?>
	<?cs if:qz_metadata.qz_data.key2.LIKE.cnt>0 ?>
		<?cs set:qz_data_key='key2' ?>
	<?cs elif:qz_metadata.qz_data.key1.LIKE.cnt>0 ?>
		<?cs set:qz_data_key='key1' ?>
	<?cs else ?>
		<?cs set:qz_data_key='key2' ?>
	<?cs /if ?>
	<?cs def:setFriend() ?>
		<?cs #:限制最多展示三个好友 ?>
		<?cs set:len=subcount(qz_metadata.qz_data[qz_data_key].LIKE.user) ?>
		<?cs if:len>3 ?>
			<?cs set:len=2 ?>
		<?cs elif:len==0&&qz_metadata.qz_data[qz_data_key].LIKE.user ?>
			<?cs call:userLink(qz_metadata.qz_data[qz_data_key].LIKE.user,'') ?>
		<?cs else ?>
			<?cs set:len=len-1 ?>
		<?cs /if ?>
		<?cs if:subcount(qz_metadata.qz_data[qz_data_key].LIKE.user[0]) ?>
			<?cs loop:i = 0, len, 1 ?>
				<?cs call:userLink(qz_metadata.qz_data[qz_data_key].LIKE.user[i],'') ?><?cs if:i<len ?>、<?cs /if ?>
			<?cs /loop ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs #:likeinfo表示好友赞的人数 ?>
	<?cs if:subcount(qz_metadata.qz_data[qz_data_key].LIKE.user)>0 ?>
		<?cs set:likeinfo=subcount(qz_metadata.qz_data[qz_data_key].LIKE.user) ?>
	<?cs elif:subcount(qz_metadata.qz_data[qz_data_key].LIKE.user[0]) ?>
		<?cs set:likeinfo = 1 ?>
	<?cs else ?>
		<?cs set:likeinfo = 0 ?>
	<?cs /if ?>
	<?cs if:likeinfo>3 ?>
		<?cs set:likeinfo=3 ?>
	<?cs /if ?>
	<div class="op_num bor2<?cs if: qz_metadata.qz_data[qz_data_key].LIKE.cnt<=0 ?> none<?cs /if?>">
		<i class="ui_icon icon_op_like"></i>
		<span class="_ilike"><?cs if:qz_metadata.qz_data[qz_data_key].LIKE.isliked ?>我<?cs if:qz_metadata.qz_data[qz_data_key].LIKE.cnt > 1?>和<?cs /if ?><?cs /if ?></span>
		<?cs call:setFriend() ?>
		<?cs set:_randerLikeInfo_num="" ?>
		<?cs if:likeinfo>0 && (qz_metadata.qz_data[qz_data_key].LIKE.cnt-likeinfo-qz_metadata.qz_data[qz_data_key].LIKE.isliked)>0 ?>
			<?cs set:_randerLikeInfo_tmp=qz_metadata.qz_data[qz_data_key].LIKE.cnt ?>
			<?cs set:_randerLikeInfo_pre="等" ?>
			<?cs set:_randerLikeInfo_num=(_randerLikeInfo_tmp+"人") ?>
			<?cs set:_randerLikeInfo_tail="都" ?>
		<?cs elif:likeinfo<=0 ?>
				<?cs set:_randerLikeInfo_pre="" ?>
				<?cs set:_randerLikeInfo_tail="" ?>
				<?cs if:qz_metadata.qz_data[qz_data_key].LIKE.isliked>0 ?>
					<?cs set:_randerLikeInfo_tmp=(qz_metadata.qz_data[qz_data_key].LIKE.cnt-1) ?>
					<?cs if:_randerLikeInfo_tmp>=1 ?>
						<?cs set:_randerLikeInfo_num=(_randerLikeInfo_tmp+"人") ?>
					<?cs /if ?>
				<?cs elif:qz_metadata.qz_data[qz_data_key].LIKE.cnt>0 ?>
					<?cs set:_randerLikeInfo_tmp=qz_metadata.qz_data[qz_data_key].LIKE.cnt ?>
					<?cs set:_randerLikeInfo_num=(_randerLikeInfo_tmp+"人") ?>
				<?cs /if ?>
		<?cs /if ?>
		<span class="_likecnt ui_ml5">
			<?cs var:_randerLikeInfo_pre ?>
			<?cs var:_randerLikeInfo_num ?>
			<?cs var:_randerLikeInfo_tail ?>
			觉得很赞
		</span>
	</div>
<?cs /def ?>

<div class="f_op">
<?cs call:opr()?>
<?cs call:randerLikeInfo() ?>
</div>


<?cs def:comments-ava(item)?>
	<div class="comment_ava hotclick" data-hctag="spcaretab.commenter.click">
		<?cs call:userIcon(item.user)?>
	</div>
<?cs /def?>
<?cs def:comments-username(user)?>
	<?cs call:userLink(user, "")?>
<?cs /def?>

<?cs def:comments-content-con(item)?>
	<?cs if: item.disabled!=1 ?>
		<?cs if:item.type == "nick"?>
			<?cs call:userLink(item, "@")?>
		<?cs else ?>
			<?cs call:richContent-items(item)?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs #:没有targetUser，尝试解析第一个@的人作为回复对象 ?>
<?cs def:resolveTargerUser(commentUser,replyUser,targetUser,con,isReply) ?>
	<?cs if:subcount(targetUser) ?>
		<?cs #:自己回复自己的情况不做这逻辑 ?>
		<?cs if: (isReply && subcount(replyUser)>0 &&  targetUser.uin!=replyUser.uin) || (!isReply && targetUser.uin!=commentUser.uin)?>
			回复 <?cs call:comments-username(targetUser) ?>
			<?cs set:resolveTargerUser.hasTargetUser=1 ?>
		<?cs /if ?>
		<?cs if: subcount(con[0]) && con[0].type=="nick" && targetUser.uin==con[0].uin?>
			<?cs set:con[0].disabled=1 ?>
		<?cs /if ?>
	<?cs elif: isReply ?>
		<?cs #:如果是在回复的话，没有targetUser要先尝试解析第一个@的人作为回复对象 ?>
		<?cs #:否则默认是回复当前评论作者 ?>
		<?cs #:如果不是回复的话，就是在评论中了，无需以下逻辑 ?>
		<?cs if: subcount(con[0]) && con[0].type=="nick" ?>
			<?cs if: subcount(replyUser)>0 && con[0].uin != replyUser.uin ?>
				回复 <?cs call:comments-username(con[0]) ?>
				<?cs set:con[0].disabled=1 ?>
			<?cs /if ?>
			<?cs if:subcount(replyUser)>0 && con[0].uin==replyUser.uin ?>
				<?cs set:con[0].disabled=1 ?>
			<?cs /if ?>
			<?cs set:resolveTargerUser.hasTargetUser=1 ?>
		<?cs else ?>
			<?cs set:resolveTargerUser.hasTargetUser=0 ?>
		<?cs /if ?>
		<?cs if: !resolveTargerUser.hasTargetUser && isReply && replyUser!= 0 && commentUser.uin != replyUser.uin ?>
			回复 <?cs call:comments-username(commentUser) ?>
		<?cs /if ?>
	<?cs /if ?>
	<?cs #:用完清理一下这个标志 ?>
	<?cs set:resolveTargerUser.hasTargetUser=0 ?>
<?cs /def ?>

<?cs #flag表示一级，二级评论 ?>
<?cs def:comments-txt(item, flag)?>
	<div class="comment_txt">
		<?cs call:comments-username(item.user)?> <?cs if:flag==2?><?cs call:resolveTargerUser(item.user,item.user,item.targetUser,item.content.con,1)?><?cs /if?>：
		<?cs with:cons = item.content?>
		<?cs if:cons.con.0 || subcount(cons.con.0) > 0 || string.length(cons.con.0) > 0 ?>
			<?cs loop:i = 0, subcount(cons.con) - 1, 1 ?>
				<?cs call:comments-content-con(cons.con[i]) ?>
			<?cs /loop ?>
		<?cs elif:cons.con || subcount(cons.con) > 0 || string.length(cons.con) > 0 ?>
			<?cs call:comments-content-con(cons.con)?>
		<?cs /if ?>
		<?cs /with?>
		<?cs #call:richContent(item.content) ?>
		<a href="#"
			 class="qz_reply_entry hotclick"
			 data-tuin="<?cs var:item.user.uin?>"<?cs #一级评论的tuin只有user里有，二级评论的tuin在qz_reply里面也有?>
			 data-nickname="<?cs call:writeUserName(item.user.uin, item.user.name)?>"
			 data-uintype="<?cs var:item.user.type?>"<?cs #区分评论是来自哪个平台的?>
			 data-uinimg="<?cs var:item.user.img?>"<?cs #朋友网的头像?>
			 data-role="commentReply"
			 data-param="<?cs var:item.qz_reply.param?>"
			 data-hctag="spcaretab.reply.click"
			><i class="ui_icon icon_reply_gray"></i></a>
	</div>
<?cs /def?>

<?cs #flag表示一级，二级评论 ?>
<?cs def:comments-detail(item, flag)?>
	<div class="comment_detail none">
		<div class="op_time c_tx3"><?cs var:item.time.text ?></div>
		<div class="op_op">
			<a href="#"
			 class="qz_reply_entry hotclick"
			 data-tuin="<?cs var:item.user.uin?>"<?cs #一级评论的tuin只有user里有，二级评论的tuin在qz_reply里面也有?>
			 data-nickname="<?cs call:writeUserName(item.user.uin, item.user.name)?>"
			 data-uintype="<?cs var:item.user.type?>"<?cs #区分评论是来自哪个平台的?>
			 data-uinimg="<?cs var:item.user.img?>"<?cs #朋友网的头像?>
			 data-role="commentReply"
			 data-param="<?cs var:item.qz_reply.param?>"
			 data-hctag="spcaretab.reply.click"
			>回复</a>
		</div>
	</div>
<?cs /def?>

<?cs def:comments()?>
	<?cs def:commentSub-item(replies, reply)?>
		<li class="comment_item">
			<div class="comment_main">
				<?cs call:comments-txt(reply, 2)?>
				<?cs call:comments-detail(reply, 2)?>
			</div>
		</li>
	<?cs /def?>

	<?cs #生成该条评论的回复?>
	<?cs def:comment-reply-item(commentItem)?>
		<?cs set:reply_loop_flag = 0?>
        <?cs set:reply_num = 0?>
        <?cs set:reply_limit = 3?>
		<?cs if:subcount(commentItem.replies.reply.0)?>
			<?cs set:reply_num = subcount(commentItem.replies.reply)?>
			<?cs set:reply_loop_flag = 1?>
			<? #:一条评论展示三条回复?>
			<?cs if:reply_num<=3 ?>
				<?cs set:reply_limit = reply_num?>
			<?cs /if ?>
		<?cs elif:subcount(commentItem.replies.reply)?>
			<?cs set:reply_num = 1?>
		<?cs /if?>

		<?cs if:reply_num?>
			<?cs if:reply_loop_flag?>
			<?cs loop:i = reply_num - reply_limit, reply_num - 1, 1?>
				<?cs call:commentSub-item(commentItem.replies, commentItem.replies.reply[i])?>
			<?cs /loop?>
			<?cs else ?>
				<?cs call:commentSub-item(commentItem.replies, commentItem.replies.reply)?>
			<?cs /if?>
		<?cs /if?>
	<?cs /def?>

	<?cs #生成评论?>
	<?cs def:comment-item(commentItem)?>
		<li class="comment_item">
			<div class="comment_main">
				<?cs call:comments-txt(commentItem, 1)?>
				<?cs call:comments-detail(commentItem, 1)?>
			</div>
		</li>
		<?cs call:comment-reply-item(commentItem)?>		
	<?cs /def?>

	<?cs #评论开始?>
	<?cs if:subcount(qz_metadata.comments)?>
	<div class="f_comment qz_feed_comment">
		<?cs set:comment_loop_flag = 0?>
        <?cs set:comment_num = 0?>
        <?cs set:comment_limit = 3?>
		<?cs if:subcount(qz_metadata.comments.comment.0)?>
			<?cs set:comment_num = subcount(qz_metadata.comments.comment)?>
			<?cs set:comment_loop_flag = 1?>
			<?cs #:展示三条评论?>
			<?cs if:comment_num<=3 ?>
				<?cs set:comment_limit = comment_num ?>
			<?cs /if ?>
		<?cs elif:subcount(qz_metadata.comments.comment)?>
			<?cs set:comment_num = 1?>
		<?cs /if?>

		<?cs if:comment_num?><ul class="comment_list">
			<?cs if:comment_loop_flag?>
			<?cs loop:i = comment_num-comment_limit, comment_num - 1, 1?>
				<?cs call:comment-item(qz_metadata.comments.comment[i]) ?>
			<?cs /loop ?>
			<?cs else ?>
				<?cs call:comment-item(qz_metadata.comments.comment) ?>
			<?cs /if?>
		</ul><?cs /if ?>

		<?cs #if:qz_metadata.metadata.appid == 2 ||
				qz_metadata.metadata.appid == 4 ||
				qz_metadata.metadata.appid == 311?>
		<?cs if:qz_metadata.totalcomment > 3?>
		<p class="comment_more">
			<a class="hotclick" href="<?cs if:qz_metadata.metadata.commentsurl?><?cs var:qz_metadata.metadata.commentsurl?><?cs elif:qz_metadata.content_box.media.albumid ?>http://user.qzone.qq.com/<?cs var:qz_metadata.metadata.uin?>/photo/<?cs var:qz_metadata.content_box.media.albumid ?><?cs else ?>http://user.qzone.qq.com/<?cs var:qz_metadata.metadata.uin ?><?cs /if?>" target="_blank" data-hctag="spcaretab.morecomment.click">查看剩余<?cs var:qz_metadata.totalcomment - 3 ?>评论</a>
		</p>
		<?cs /if?>
		<?cs #/if?>
		
	</div>
	<?cs /if?>
<?cs /def?>
<?cs #默认展开3条评论,每条评论3条回复?>
<?cs call:comments()?>

<?cs def:data-meta()?>
	<i class="none feed-metadata"
	 data-uin="<?cs var:qz_metadata.metadata.uin ?>" 
	 data-commentUrl="<?cs var:qz_metadata.comments.qz_reply.action?>"
	 data-commentParam="<?cs var:qz_metadata.comments.qz_reply.param?>"
	 data-commentTuin="<?cs var:qz_metadata.comments.qz_reply.tuin?>"
	 data-likeUnikey="<?cs var:_like_unikey?>"
	 data-likeCurkey="<?cs var:qz_metadata.qz_data.key1?>"<?cs #奇怪的东西..?>
	 data-isliked="<?cs var:_isliked?>"
	 data-likecnt="<?cs var:_like_count?>"
	<?cs if:qz_metadata.metadata.appid != 4?>
	 data-tid="<?cs var:qz_metadata.metadata.blogid?>"
	<?cs else ?>
	 data-tid="<?cs var:qz_metadata.content_box.media.albumid ?>"
		<?cs if:subcount(qz_metadata.content_box.media.media.0) == 0 &&
			subcount(qz_metadata.content_box.media.media) > 0 ?>
		<?cs #:只有一张照片，当它就是照片feeds ?>
		 data-subid="<?cs var:qz_metadata.content_box.media.media.largeid ?>" 
		<?cs /if?>
	<?cs /if?>
	 data-origtid="<?cs alt:qz_metadata.metadata.orgid.0 ?><?cs var:qz_metadata.metadata.orgid ?><?cs /alt ?>" 
	 data-origuin="<?cs var:qz_metadata.metadata.orguin ?>" 
	 data-issignin="<?cs var:qz_metadata.metadata.signin ?>" 
	 data-retweetcount="<?cs if:qz_metadata.qz_data.key2.ZS.cnt ?><?cs var:qz_metadata.qz_data.key2.ZS.cnt?><?cs elif:qz_metadata.qz_data.key1.ZS.cnt ?><?cs var:qz_metadata.qz_data.key1.ZS.cnt ?><?cs else ?>0<?cs /if ?>"
	 data-totweet="<?cs var:qz_metadata.metadata.to_tweet ?>" 
	 data-source="<?cs var:qz_metadata.metadata.mood_source ?>" 
	 data-topicid="<?cs if:qz_metadata.relybody.0.uin?><?cs var:qz_metadata.relybody.0.topicid?><?cs else?><?cs var:qz_metadata.orgdata.topicid?><?cs /if?>"
	 data-feedstype="<?cs var:qz_metadata.metadata.feedstype?>"
	 ></i><?cs #commentTuin并不完全就是feed前面的uin?>
<?cs /def?>
<?cs call:data-meta()?>

