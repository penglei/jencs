<?cs set:feed = qz_metadata.feed ?>
<?cs set:titletype = qz_metadata.title_sec.titletype ?>
<?cs set:title = qz_metadata.title_sec.title ?>
<?cs set:uin = qz_metadata.title_sec.uin ?>
<?cs set:itemid = qz_metadata.title_sec.itemid ?>
<?cs set:fromtype = qz_metadata.title_sec.fromtype ?>
<?cs set:typename = qz_metadata.title_sec.typename ?>
<?cs set:type = qz_metadata.type ?>
<?cs set:resuin = qz_metadata.resuin ?>
<?cs set:resnick = qz_metadata.resnick ?>
<?cs set:resid = qz_metadata.resid ?>
<?cs set:shareuin = qz_metadata.shareuin ?>
<?cs set:url = qz_metadata.url ?>
<?cs set:shareid = qz_metadata.shareid ?>
<?cs set:picnum = qz_metadata.picnum ?>
<?cs set:feedstype = qz_metadata.feedstype?>
<?cs if:feedstype==1?>
	<?cs set:feedstypename = '喜欢' ?>
<?cs else?>
	<?cs set:feedstypename = '分享' ?>
<?cs /if?>

<?cs if:string.length(qz_metadata.images.0.imageurl) > 0?>
	<?cs set:images_cursor = 0?>
	<?cs each:item = qz_metadata.images?>
		<?cs set:images[images_cursor].imageurl = item.imageurl ?>
		<?cs set:images[images_cursor].imageurl_b = item.imageurl_b ?>
		<?cs set:images_cursor = images_cursor + 1?>
	<?cs /each?>
<?cs elif:string.length(qz_metadata.images.imageurl) > 0?>
	<?cs set:images[0].imageurl = qz_metadata.images.imageurl ?>
	<?cs set:images[0].imageurl_b = qz_metadata.images.imageurl_b ?>
<?cs /if?>

<?cs set:desc = qz_metadata.desc ?>
<?cs set:title = qz_metadata.title ?>
<?cs set:url = qz_metadata.url ?>
<?cs set:imagenum = qz_metadata.imagenum ?>
<?cs set:summary = qz_metadata.summary ?>
<?cs set:sharecount = qz_metadata.sharecount ?>
<?cs set:bbssource = qz_metadata.bbssource ?>
<?cs set:albumname = qz_metadata.albumname ?>
<?cs set:albumurl = qz_metadata.albumurl ?>
<?cs set:replyparam = qz_metadata.replyparam ?>
<?cs set:delcommpart = qz_metadata.delcommpart ?>
<?cs set:commtotal = qz_metadata.commtotal ?>
<?cs set:videourl = qz_metadata.videourl ?>

<?cs if:string.length(qz_metadata.comments.0.commuin) > 0?>
	<?cs set:comments_cursor = 0?>
	<?cs each:item = qz_metadata.images?>
		<?cs set:comments[comments_cursor].from = item.from ?>
		<?cs set:comments[comments_cursor].commnickurl = item.commnickurl ?>
		<?cs set:comments[comments_cursor].commid = item.commid ?>
		<?cs set:comments[comments_cursor].commuin = item.commuin ?>
		<?cs set:comments[comments_cursor].commnick = item.commnick ?>
		<?cs set:comments[comments_cursor].commcon = item.commcon ?>
		<?cs set:comments[comments_cursor].commtime = item.commtime ?>
		<?cs set:comments_cursor = comments_cursor + 1?>
	<?cs /each?>
<?cs elif:string.length(qz_metadata.comments.commuin) > 0?>
	<?cs set:comments[0].from = qz_metadata.comments.from ?>
	<?cs set:comments[0].commnickurl = qz_metadata.comments.commnickurl ?>
	<?cs set:comments[0].commid = qz_metadata.comments.commid ?>
	<?cs set:comments[0].commuin = qz_metadata.comments.commuin ?>
	<?cs set:comments[0].commnick = qz_metadata.comments.commnick ?>
	<?cs set:comments[0].commcon = qz_metadata.comments.commcon ?>
	<?cs set:comments[0].commtime = qz_metadata.comments.commtime ?>
<?cs /if?>

<?cs #:因为平台cs版本过低，缺少部分函数，这里的自定义方法下列方法 ?>
<?cs def:html_escape(source)?><?cs escape:"none"?><?cs var:source?><?cs /escape?><?cs /def?>
<?cs def:url_escape(source)?><?cs escape:"url"?><?cs var:source?><?cs /escape?><?cs /def?>
<?cs def:js_escape(source)?><?cs escape:"js"?><?cs var:source?><?cs /escape?><?cs /def?>


<?cs def:prefixedUserLink(prefix, userName, href, link, className) ?>
	<?cs if:string.length(prefix) > 0 ?>
		<a href="<?cs var:href ?>" class="q_namecard c_tx <?cs var:className?>" target="_blank"  link="<?cs var:link ?>"><?cs var:prefix ?><?cs var:userName ?></a>
	<?cs else ?>
		<a class="q_namecard q_des <?cs var:className?> c_tx" href="<?cs var:href ?>" link="<?cs var:link ?>" target="_blank"><?cs var:userName ?></a>
	<?cs /if ?>
<?cs /def ?>

<?cs def:textUserLink(uin, name, who, className, prefix) ?>
	<?cs if:uin == qz_metadata.t1_uin ?>
		<?cs call:prefixedUserLink(prefix, qz_metadata.t1_name, 'http://user.qzone.qq.com/' + qz_metadata.t1_uin, 'nameCard_' + qz_metadata.t1_uin + ' des_' + qz_metadata.t1_uin, className) ?>
	<?cs else?>		
		<?cs if:who == 1 ?>
			<?cs call:prefixedUserLink(prefix, name, 'http://user.qzone.qq.com/' + uin +'/share', 'nameCard_' + uin + ' des_' + uin, className) ?>
		<?cs elif:who == 2?>
			<?cs call:prefixedUserLink(prefix, name, 'http://share.pengyou.qq.com/index.php?mod=usershare&act=guest&u=' + uin, '', className) ?>
		<?cs elif:who == 3 ?>
			<?cs call:prefixedUserLink(prefix, name, 'http://rc.qzone.qq.com/myhome/weibo/profile1/' + uin, '', className) ?>
		<?cs /if?>
	<?cs /if?>	
<?cs /def ?>

<?cs def:newRichContent(content) ?>
	<?cs def:richContent-items(item) ?>
		<?cs if:item.type=='nick' ?>
				<?cs call:textUserLink(item.uin, item.name, item.who,"comment_nickname", "@") ?>			
		<?cs else ?>
			<?cs var:item ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if: content.con.0 || subcount(content.con.0) > 0 ?>
		<?cs loop:x=0,subcount(content.con)-1,1?>
			<?cs call:richContent-items(content.con[x]) ?>
		<?cs /loop?>
	<?cs elif:content.con ?>
		<?cs call:richContent-items(content.con) ?>
	<?cs else?>
		<?cs var:content?>
	<?cs /if?>
<?cs /def ?>

<?cs def:ownerSource()?>
<?cs if:titletype=="atme_passive"&&type==17?>在<?cs var:feedstypename?>中提到我
<?cs else?>
	<?cs if:titletype=="atme_passive"?>在<?cs var:feedstypename?>
	<?cs else?>
		<?cs if: type>0&&type<4 ?>
			<?cs if: fromtype==200 ?>
				<?cs var:feedstypename?><?cs if: string.length(resnick)>0?><a class="c_tx" target="_blank" href="http://xiaoyou.qq.com/index.php?mod=profile&u=<?cs var: resuin?>"><?cs var: resnick?></a>的<?cs /if?><?cs call:html_escape(typename) ?>：
			<?cs else?>
				<?cs var:feedstypename?><?cs if: string.length(resnick)>0?><a class="c_tx q_namecard comment_nickname" link="nameCard_<?cs var: resuin?> des_<?cs var:resuin?>" target="_blank" href="http://user.qzone.qq.com/<?cs var: resuin?>"><?cs var: resnick?></a>的<?cs /if?><?cs call:html_escape(typename) ?>：
			<?cs /if?>
		<?cs elif:type==17 ?>
			<?cs var:feedstypename?><?cs if: string.length(resnick)>0?><a class="c_tx" target="_blank" href="<?cs var:url ?>"><?cs var: resnick?></a>的<?cs /if?><?cs call:html_escape(typename) ?>
		<?cs else?>
			<?cs var:feedstypename?><?cs call:html_escape(typename) ?>：
		<?cs /if?>
	<?cs /if?>
	<?cs if:type!=17?>
		<a href="<?cs if:type==4 || type==15 ?>http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshare_urlcheck?uin=<?cs var:uin ?>&shareid=<?cs var:itemid ?><?cs else ?>http://user.qzone.qq.com/<?cs var:uin ?>/share/<?cs var:itemid ?><?cs /if ?>" class="c_tx" target="_blank">
			<?cs call:html_escape(title) ?>
		</a>
	<?cs /if?>
	<?cs if:titletype=="atme_passive"?> 中提到我<?cs /if?>
<?cs /if?>
<?cs /def?>



<?cs #:微博 ?>
<?cs def:SubType_17() ?>
<?cs if:titletype=="atme_passive"?>在<?cs var:feedstypename?>中提到我：<?cs else?><?cs var:feedstypename?>：<?cs /if?><?cs if:qz_metadata.newdesc.con||qz_metadata.newdesc.con.0 || subcount(qz_metadata.newdesc.con.0) > 0?><strong class="quotes_symbols_left c_tx3">“</strong><?cs call:newRichContent(qz_metadata.newdesc) ?><strong class="quotes_symbols_right c_tx3">”</strong><?cs /if?>
<?cs /def ?>

<?cs #:音乐 ?>
<?cs def:SubType_18() ?>
<?cs if:titletype=="atme_passive"?>在<?cs var:feedstypename?>中提到我：<?cs else?><?cs var:feedstypename?>：<?cs /if?><?cs if:qz_metadata.newdesc.con||qz_metadata.newdesc.con.0 || subcount(qz_metadata.newdesc.con.0) > 0?><strong class="quotes_symbols_left c_tx3">“</strong><?cs call:newRichContent(qz_metadata.newdesc) ?><strong class="quotes_symbols_right c_tx3">”</strong><?cs /if?>
<?cs /def ?>

<?cs #:早期feed的直接改造，根据分享子类型的变化慢慢迁移到新入口 ?>
<?cs def:oldEntry()?>
<?cs if:titletype=="share_active"||titletype=="atme_passive"||titletype=="aboutme_passive"||titletype=="share_video" ?>
	<?cs if:fromtype==1000?>
		通过<a href="http://imgcache.qq.com/qzone/v5/promotion/QQtoolbar.html" class="c_tx" target="_blank">QQ工具栏</a>
	<?cs /if ?>
	<?cs if:titletype=="atme_passive"?>在<?cs var:feedstypename?>中提到我：<?cs else?><?cs var:feedstypename?>：<?cs /if?><?cs if:qz_metadata.newdesc.con||qz_metadata.newdesc.con.0 || subcount(qz_metadata.newdesc.con.0) > 0?><strong class="quotes_symbols_left c_tx3">“</strong><?cs call:newRichContent(qz_metadata.newdesc) ?><strong class="quotes_symbols_right c_tx3">”</strong><?cs /if?>
<?cs elif:titletype=="comm_passive" ?>
	评论我的<?cs var:feedstypename?>：
	<a href="http://user.qzone.qq.com/<?cs var:uin ?>/share/<?cs var:itemid ?>" class="c_tx" target="_blank"><?cs call:html_escape(title) ?></a>
<?cs elif:titletype=="share_passive" ?>
	<?cs var:feedstypename?>我的<?cs call:html_escape(typename) ?>：
	<a href="http://user.qzone.qq.com/<?cs var:uin ?>/share/<?cs var:itemid ?>" class="c_tx" target="_blank"><?cs call:html_escape(title) ?></a>
<?cs elif:titletype=="reply_passive" ?>
	在<?cs var:feedstypename?>：
	<a href="http://user.qzone.qq.com/<?cs var:uin ?>/share/<?cs var:itemid ?>" class="c_tx"  target="_blank"><?cs call:html_escape(title) ?></a> 回复我
<?cs /if ?>
<?cs /def ?>

<?cs #:新入口 ?>
<?cs def:newEntry()?>
<?cs if:type == "17"?><?cs call:SubType_17()?>
<?cs elif:type == "18"?><?cs call:SubType_18()?>
<?cs /if?>
<?cs /def ?>

<?cs #:全局入口?>
<?cs if:(type == "17" || type == "18") && titletype == "share_active" ?>
	<?cs call:newEntry() ?>
<?cs else ?>
	<?cs call:oldEntry() ?>
<?cs /if ?>