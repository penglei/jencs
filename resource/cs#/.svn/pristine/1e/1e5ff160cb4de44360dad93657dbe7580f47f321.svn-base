<?cs include:"wupcs/modview-qqtab/common_marco.cs"?>
<?cs include:"wupcs/modview-qqtab/style.cs"?>

<?cs include:"wupcs/modview-qqtab/user_comp.cs"?>
<?cs include:"wupcs/modview-qqtab/textcon_comp.cs"?>
<?cs include:"wupcs/modview-qqtab/icon_comp.cs"?>
<?cs include:"wupcs/modview-qqtab/popup_comp.cs"?>

<?cs include:"wupcs/modview-qqtab/title.cs"?>

<?cs include:"wupcs/modview-qqtab/quote.cs"?>
<?cs include:"wupcs/modview-qqtab/extendinfo.cs"?>

<?cs include:"wupcs/modview-qqtab/contentbox_media.cs"?>
<?cs include:"wupcs/modview-qqtab/contentbox_txt.cs"?>
<?cs include:"wupcs/modview-qqtab/contentbox.cs"?>

<?cs include:"wupcs/modview-qqtab/time.cs"?>
<?cs include:"wupcs/modview-qqtab/source.cs"?>
<?cs include:"wupcs/modview-qqtab/operate.cs"?>

<?cs include:"wupcs/modview-qqtab/arrow.cs"?>
<?cs include:"wupcs/modview-qqtab/like.cs"?>
<?cs include:"wupcs/modview-qqtab/img_comp.cs"?>
<?cs include:"wupcs/modview-qqtab/comments.cs"?>
<?cs include:"wupcs/modview-qqtab/comments_like.cs"?>

<?cs include:"wupcs/modview-qqtab/summary.cs"?>



<?cs #:为填充此feeds初始化一系列变量 ?>
<?cs if:subcount(qfv.content.media.pic.0) ?>
	<?cs #优先取200的图 ?>
	<?cs set:src_big=qz_metadata.orgdata.itemdata.0.picinfo.1.url ?>
	<?cs if:!src_big ?>
		<?cs set:src_big=qfv.content.media.pic.0.src ?>
	<?cs /if ?>
	<?cs set:src_big_pre=qz_metadata.orgdata.itemdata.0.picinfo.2.url ?>
	<?cs if:!src_big_pre ?>
		<?cs set:src_big_pre=qfv.content.media.pic.0.src ?>
	<?cs /if ?>
	<?cs set:withPhoto=1 ?>
<?cs /if ?>

<?cs set:_appid=qfv.meta.appid ?>


<?cs #:设置需要显示作者姓名的小黑条的应用 ?>
<?cs set:author_meta[4]=1 ?>
<?cs set:author_meta[311]=1 ?>

<?cs #:设置需要文字使用的大遮罩的应用 ?>
<?cs call:get_tuin_and_tid()?>
<?cs if:_appid==2 ?>
	<?cs call:get_blog_url(get_tuin_and_tid.uin, get_tuin_and_tid.tid)?>
	<?cs set:_fakeLink[2]=get_blog_url.ret ?>
<?cs /if ?>
<?cs set:_fakeLinkCmdstr[2]='blog' ?>
<?cs set:_fakeLinkTitle[2]='点击查看原文' ?>

<?cs if:withPhoto==1 ?>
	<?cs #:为带图feeds设置图片的属性行为 ?>
	<?cs set:_setThumbnailParam[2] = ' href="javascript:;" cmdstr="showmark"' ?>
	<?cs set:_setThumbnailParam[4] = ' href="javascript:;" key="' + feedkey +
										'" uin="' + qfv.meta.opuin +
										'" timeStamp="' + qfv.meta.abstime +
										'" cmdstr="popupImg" appid="' + _appid +
										'" param="' + qfv.content.media.pic.0.action.param + src_big_pre + '|' + qfv.content.media.pic.0.action.topicid + '" ' ?>
    
    <?cs #:有可能出现pickey 不存在这种坑爹情况，如果有才走新逻辑好了 ?>
	<?cs if:qfv.content.media.pic.0.action.pickey && qfv.meta.topicid ?>
		<?cs set:_setThumbnailParam[311] = ' href="javascript:;" key="' + qfv.meta.feedkey +
										'" uin="' + qfv.meta.opuin +
										'" timeStamp="' + qfv.meta.abstime +
										'" cmdstr="popupImg" appid="' + _appid +
										'" param="'+ qfv.meta.opuin +'||' + qfv.content.media.pic.0.action.pickey + '|' + src_big_pre + '|' +
													 qfv.meta.topicid +'"' ?>
													 <?cs #:之所以这样写，是为了参数和相册的参数保持一致，好在ic_original.js里面调用新浮层时统一调用 ?>

	<?cs else ?>
		<?cs set:_setThumbnailParam[311] = ' href="javascript:;" key="' + qfv.meta.feedkey +
										'" uin="' + qfv.meta.opuin +
										'" timeStamp="' + qfv.meta.abstime +
										'" cmdstr="popupImg" appid="' + _appid +
										'" param="'+ get_tuin_and_tid.tid + '|' +
													qfv.meta.opuin + '|0" ' ?>
	<?cs /if ?>

	<?cs /if ?>

<?cs #:若带图，文字部分的容器样式 ?>
<?cs set:wpContainerClass[2]='tb_log' ?>

<?cs #:为正文的图片部分指定展示内容 ?>
<?cs set:_summary[2]=1 ?>
<?cs set:_summaryTitle[2]=qfv.title.con.1.text ?>
<?cs set:_summaryAuthor[2]='来自:'+html_encode(qfv.meta.username, 1) ?>
<?cs set:_summaryContent[2]=qfv.content.cntText.con.0.text ?>

<?cs def:writeUserName(uin,defaultName) ?>
	<?cs if:string.length(qz_metadata.remarkPool['u'+uin])>0 ?>
		<?cs var:html_encode(qz_metadata.remarkPool['u'+user.uin], 1) ?>
	<?cs else ?>
		<?cs var:html_encode(defaultName, 1) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:用户链接组件 ?>
<?cs #:prefix 是前缀啊例如@ ?>
<?cs def:userLink(user, prefix) ?>
	<?cs if:user.who == 1 || user.type == 1?>
		<a href="http://user.qzone.qq.com/<?cs var:user.uin ?>" 
			class="q_namecard c_tx" 
			link="nameCard_<?cs var:user.uin ?>" 
			target="_blank">
			<?cs if:string.length(prefix) > 0 ?>
				<?cs var:prefix ?>
			<?cs /if ?>
			<span class="q_des_name _des" link="des_<?cs var:user.uin ?>">
				<?cs call:writeUserName(user.uin,user.name) ?>
			</span>
		</a>
	<?cs elif:user.who == 2 || user.type == 2 ?>
		<a href="http://profile.pengyou.qq.com/index.php?mod=profile&u=<?cs var:user.uin ?>" 
			class="c_tx" 
			target="_blank">
			<?cs if:string.length(prefix) > 0 ?>
				<?cs var:prefix ?>
			<?cs /if ?>
			<span class="_des" ink="des_<?cs var:user.uin ?>">
				<?cs var:html_encode(user.name, 1) ?>
			</span>
		</a>
	<?cs elif:user.who == 3 || user.type == 3 ?>
		<a href="http://rc.qzone.qq.com/myhome/weibo/profile/<?cs var:user.uin ?>" 
			class="c_tx" 
			target="_blank"
		>
			<?cs if:string.length(prefix) > 0 ?>
					<?cs var:prefix ?>
			<?cs /if ?>
			<span class="_des" link="des_<?cs var:user.uin ?>">
				<?cs var:html_encode(user.name, 1) ?>
			</span>
		</a>
	<?cs else ?>
		<<span class="_des" link="des_<?cs var:user.uin ?>"><?cs var:html_encode(user.name, 1) ?></span>
	<?cs /if ?>
<?cs /def ?>

<?cs def:richContent-title(cons) ?>
	<?cs def:richContentTitle-items(item) ?>
		<?cs if:item.type == 'nick' ?>
			<?cs call:userLink(item , '@') ?>
		<?cs elif:item.type == 'url' ?>
			<a class="c_tx ui_mr10 " 
				href="<?cs var:item.url ?>" 
				target="_blank"
			>
				<?cs if:item.text ?>
					<?cs var:item.text ?>
				<?cs else ?>
					<?cs var:item.url ?>
				<?cs /if ?>
			</a>
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

<?cs #:内容 ?>
<?cs def:actionTitle_content() ?>
	<?cs call:richContent-title(qz_metadata.actiontitle.content) ?>
<?cs /def ?>


<?cs #:显示作者姓名的小黑条 ?>
<?cs def:setAuthorMeta() ?>
	<?cs if:author_meta[_appid]==1 ?>
		<div class="author_meta" style="bottom:-22px;">
			<a href="http://user.qzone.qq.com/<?cs var:qfv.meta.opuin ?>" 
				cmdstr="nickname" 
				target="_blank"
			>
				<span class="txt textoverflow">
					<i class="icon_home"></i>
					作者：<span>
						<?cs call:writeUserName(qfv.meta.opuin,qfv.meta.username) ?>
						</span>
				</span>
				<?cs if:qz_metadata.meta.viplevel ?>
					<i class="qz_vip_icon_s qz_vip_icon_s_<?cs var:qz_metadata.meta.viplevel ?>"></i>
				<?cs /if ?>
			</a>
		</div>
	<?cs /if ?>
<?cs /def ?>
<?cs #:无图文字使用的大遮罩 ?>
<?cs def:fakeLink() ?>
	<?cs if:string.length(_fakeLink[_appid])>0 ?>
		<a title="<?cs var:_fakeLinkTitle[_appid] ?>" 
			class="fake_link" 
			cmdstr="<?cs var:_fakeLinkCmdstr[_appid] ?>" 
			href="<?cs var:_fakeLink[_appid] ?>" 
			uin="<?cs var:qfv.meta.opuin ?>" 
			key="<?cs var:qz_metadata.meta.feedkey ?>" 
			target="_blank">
		</a>
	<?cs /if ?>
<?cs /def ?>

<?cs #:展示框中的大图 ?>
<?cs def:setThumbnail() ?>
	<?cs if:string.length(src_big)>0 ?>
		<a class="thumbnail"<?cs var:_setThumbnailParam[_appid] ?> onclick="return false;">
			<img src="/ac/b.gif" 
				onload="QZFL.media.reduceImage(
						1,160,128,
						{
							trueSrc:'<?cs var:html_encode(json_encode(src_big,1), 1) ?>',
							callback:function(img,type,ew,eh,o){
								var _h = Math.floor(o.oh/o.k);
								var _w = Math.floor(o.ow/o.k);
								img.style.marginLeft=(ew-_w)/2+'px';
								img.style.marginTop=(eh-_h)/2+'px';
							}
						})"/>
		</a>
	<?cs /if ?>
<?cs /def ?>
<?cs def:showVipIcon() ?>
	<?cs if:qz_metadata.meta.viplevel ?>
		<span class="vip_icon">
			<i class="qz_vip_icon_s qz_vip_icon_s_<?cs var:qz_metadata.meta.viplevel ?>"></i>
		</span>
		<?cs /if ?>
<?cs /def ?>

<?cs def:cutString(s,len) ?>
	<?cs if:string.length(s)>len ?>
		<?cs var:string.slice(s,0,len) ?>...
	<?cs else ?>
		<?cs var:s ?>
	<?cs /if ?>
<?cs /def ?>


<?cs def:main() ?>
<div class="feed _feed_con<?cs if:withPhoto==1 ?> feed_photo<?cs else ?> feed_log<?cs /if ?> wupfeed">
	<div class="thumbnail bg">
		<?cs call:setThumbnail() ?>
		<?cs if:_summary[_appid]==1 ?>
			<?cs if:withPhoto==1 ?>
				<div style="bottom:-128px;" class="<?cs var:wpContainerClass[_appid] ?> ui_mask _contentText">
			<?cs /if ?>
			<?cs call:fakeLink() ?>
			<?cs if:string.length(_summaryTitle[_appid])>0 ?>
				<h4 class="textoverflow">
					<span  class="c_tx"><?cs var:html_encode(_summaryTitle[_appid], 1) ?></span>
				</h4>
			<?cs /if ?>
			<?cs if:string.length(_summaryAuthor[_appid])>0 ?>
				<p class="author c_tx">
					<span class="txt textoverflow"><?cs var:_summaryAuthor[_appid] ?></span>
					<?cs call:showVipIcon() ?>
				</p>
			<?cs /if ?>
			<?cs if:string.length(_summaryContent[_appid])>0 ?>
				<p class="c_tx3 article">
					<?cs if:_appid==311 ?>
						<?cs call:writeUserName(qfv.meta.opuin,nickname) ?>
						:
						<?cs call:richContent-title(qz_metadata.actiontitle.content) ?>
					<?cs else ?>
						<?cs var:html_encode(_summaryContent[_appid], 1) ?>
					<?cs /if ?>
				</p>
			<?cs /if ?>
			<?cs if:withPhoto==1 ?>
				</div>
			<?cs /if ?>
		<?cs /if ?>
		<a class="hot_number_holder<?cs if:qfv.comments.totalcomment==0?> none<?cs /if?>" href="javascript:;">
			<span class="hot_number bg6 c_tx6">
				<b class="c_bg6"></b>
				<?cs var:qfv.comments.totalcomment ?>
			</span>
		</a>
	</div>
	<?cs call:setAuthorMeta() ?>
</div>
<?cs /def ?>
<?cs call:main() ?>
