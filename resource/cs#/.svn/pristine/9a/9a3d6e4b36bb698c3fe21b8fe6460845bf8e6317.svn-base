<?cs #:为填充此feeds初始化一系列变量 ?>
<?cs if:qz_metadata.content_box.media.media.0||subcount(qz_metadata.content_box.media.media.0) ?>
	<?cs set:photoid=qz_metadata.content_box.media.media.0.largeid ?>
	<?cs set:src_big=qz_metadata.content_box.media.media.0.src ?>
	<?cs set:withPhoto=1 ?>
<?cs elif:qz_metadata.content_box.media.media||subcount(qz_metadata.content_box.media.media) ?>
	<?cs set:photoid=qz_metadata.content_box.media.media.largeid ?>
	<?cs set:src_big=qz_metadata.content_box.media.media.src ?>
	<?cs set:withPhoto=1 ?>
<?cs /if ?>

<?cs if:qz_metadata.totalcomment ?>
	<?cs set:commentcount=qz_metadata.totalcomment ?>
<?cs else ?>
	<?cs set:commentcount=0 ?>
<?cs /if ?>

<?cs set:_appid=qz_metadata.metadata.appid ?>


<?cs #:设置需要显示作者姓名的小黑条的应用 ?>
<?cs set:author_meta[4]=1 ?>
<?cs set:author_meta[311]=1 ?>

<?cs #:设置需要文字使用的大遮罩的应用 ?>
<?cs set:_fakeLink[2]=qz_metadata.metadata.commentsurl ?>
<?cs set:_fakeLinkCmdstr[2]='blog' ?>
<?cs set:_fakeLinkTitle[2]='点击查看原文' ?>

<?cs if:withPhoto==1 ?>
	<?cs #:为带图feeds设置图片的属性行为 ?>
	<?cs set:_setThumbnailParam[2] = ' href="javascript:;" cmdstr="showmark"' ?>
	<?cs set:_setThumbnailParam[4] = ' href="javascript:;" key="' + feedkey +
										'"uin="' + qz_metadata.metadata.uin +
										'" timeStamp="' + qz_metadata.time.abstime +
										'" cmdstr="popupImg" appid="' + qz_metadata.metadata.appid +
										'" param="' + qz_metadata.metadata.uin + '|' +
														qz_metadata.content_box.media.albumid + '|' + 
														photoid + '|' + 
														src_big + '" ' ?>
	<?cs set:_setThumbnailParam[311] = ' href="javascript:;" key="' + feedkey +
										'"uin="' + qz_metadata.metadata.uin +
										'" timeStamp="' + qz_metadata.time.abstime +
										'" cmdstr="popupImg" appid="' + qz_metadata.metadata.appid +
										'" param="'+ qz_metadata.metadata.blogid + '|' +
													qz_metadata.metadata.uin + '|0" ' ?>
<?cs /if ?>

<?cs #:若带图，文字部分的容器样式 ?>
<?cs set:wpContainerClass[2]='tb_log' ?>

<?cs #:为正文的图片部分指定展示内容 ?>
<?cs set:_summary[2]=1 ?>
<?cs set:_summaryTitle[2]=qz_metadata.metadata.title ?>
<?cs set:_summaryAuthor[2]='来自:'+html_encode(nickname, 1) ?>
<?cs set:_summaryContent[2]=qz_metadata.content_box.content.con ?>

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
			<a href="http://user.qzone.qq.com/<?cs var:qz_metadata.metadata.uin ?>" 
				cmdstr="nickname" 
				target="_blank"
			>
				<span class="txt textoverflow">
					<i class="icon_home"></i>
					作者：<span>
						<?cs call:writeUserName(qz_metadata.metadata.uin,nickname) ?>
						</span>
				</span>
				<?cs if:viplevel ?>
					<i class="qz_vip_icon_s qz_vip_icon_s_<?cs var:viplevel ?>"></i>
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
			uin="<?cs var:qz_metadata.metadata.uin ?>" 
			key="<?cs var:feedkey ?>" 
			target="_blank">
		</a>
	<?cs /if ?>
<?cs /def ?>

<?cs #:展示框中的大图 ?>
<?cs def:setThumbnail() ?>
	<?cs if:string.length(src_big)>0 ?>
		<a class="thumbnail"<?cs var:_setThumbnailParam[_appid] ?>>
			<img src="/ac/b.gif" 
				onload="QZFL.media.reduceImage(
						1,160,128,
						{
							trueSrc:'<?cs var:json_encode(src_big,1) ?>',
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
	<?cs if:viplevel ?>
		<span class="vip_icon">
			<i class="qz_vip_icon_s qz_vip_icon_s_<?cs var:viplevel ?>"></i>
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
<div class="feed _feed_con<?cs if:withPhoto==1 ?> feed_photo<?cs else ?> feed_log<?cs /if ?>">
	<div class="thumbnail bg">
		<?cs call:setThumbnail() ?>
		<?cs if:_summary[_appid]==1 ?>
			<?cs if:withPhoto==1 ?>
				<div style="bottom:-128px;" class="<?cs var:wpContainerClass[_appid] ?> ui_mask _contentText">
			<?cs /if ?>
			<?cs call:fakeLink() ?>
			<?cs if:string.length(_summaryTitle[_appid])>0 ?>
				<h4 class="textoverflow">
					<span  class="c_tx"><?cs var:_summaryTitle[_appid] ?></span>
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
						<?cs call:writeUserName(qz_metadata.metadata.uin,nickname) ?>
						:
						<?cs call:richContent-title(qz_metadata.actiontitle.content) ?>
					<?cs else ?>
						<?cs var:_summaryContent[_appid] ?>
					<?cs /if ?>
				</p>
			<?cs /if ?>
			<?cs if:withPhoto==1 ?>
				</div>
			<?cs /if ?>
		<?cs /if ?>
		<?cs set:total = 0 + qz_metadata.qz_data.key1.LIKE.cnt +
						 qz_metadata.qz_data.key1.ZF.cnt +
						 qz_metadata.totalcomment ?>
		<a class="hot_number_holder<?cs if:total==0?> none<?cs /if?>" href="javascript:;">
			<span class="hot_number bg6 c_tx6">
				<b class="c_bg6"></b>
				<?cs var:total ?>
			</span>
		</a>
	</div>
	<?cs call:setAuthorMeta() ?>
</div>
<?cs /def ?>
<?cs call:main() ?>
