<?cs #:passfeed ?>
<?cs set:feed = qz_metadata.feed ?>
<?cs set:titletype = qz_metadata.title_sec.titletype ?>
<?cs set:title = qz_metadata.title_sec.title ?>
<?cs set:uin = qz_metadata.title_sec.uin ?>
<?cs set:itemid = qz_metadata.title_sec.itemid ?>
<?cs set:fromtype = qz_metadata.title_sec.fromtype ?>
<?cs set:typename = qz_metadata.title_sec.typename ?>
<?cs set:type = qz_metadata.type ?>
<?cs set:resuin = qz_metadata.resuin ?>
<?cs set:resid = qz_metadata.resid ?>
<?cs set:shareuin = qz_metadata.shareuin ?>
<?cs set:shareid = qz_metadata.shareid ?>
<?cs set:picnum = qz_metadata.picnum ?>

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
<?cs set:noreply = qz_metadata.noreply ?>

<?cs if:string.length(qz_metadata.comments.0.commuin) > 0?>
	<?cs set:comments_cursor = 0?>
	<?cs each:item = qz_metadata.comments?>
		<?cs set:comments[comments_cursor].from = item.from ?>
		<?cs set:comments[comments_cursor].commnickurl = item.commnickurl ?>
		<?cs set:comments[comments_cursor].commid = item.commid ?>
		<?cs set:comments[comments_cursor].commuin = item.commuin ?>
		<?cs set:comments[comments_cursor].commnick = item.commnick ?>
		<?cs set:comments[comments_cursor].commcon = item.commcon ?>
		<?cs set:comments[comments_cursor].commtime = item.commtime ?>
		<?cs set:comments[comments_cursor].commtype = item.commtype ?>
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
	<?cs set:comments[0].commtype = qz_metadata.comments.commtype ?>
<?cs /if?>

<?cs #:平台组默认给所有字段做了html编码，这里的自定义方法现在不做处理 ?>
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





<?cs def:commentPanel()?>
<div class="feeds_tp_operate">
	<qz:reply type="link">评论</qz:reply>
</div>
<?cs if:1!=delcommpart ?>
	<div class="feeds_comment">
		<div class="comment_arrow c_bg2">◆</div>
		<?cs if:string.length(qz_metadata.comment) > 0?>
		<div class="feeds_comm_list bg2" ><div class="feeds_comment_cont"><p class="feeds_comment_text"><a href="<?cs var:qz_metadata.nameurl ?>" class="q_namecard q_des comment_nickname c_tx" <?cs if:200==qz_metadata.commtype ?><?cs else ?>link="nameCard_<?cs var:qz_metadata.commuin ?> des_<?cs var:qz_metadata.commuin ?>"<?cs /if ?> target="_blank"><?cs call:html_escape(qz_metadata.name) ?></a><?cs if:subcount(qz_metadata.comment_at)>0?><?cs call:newRichContent(qz_metadata.comment_at)?><?cs else?><?cs call:html_escape(qz_metadata.comment) ?><?cs /if?></p><p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:qz_metadata.time ?></span></p></div></div>
		<?cs /if?>
		<qz:reply action="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareaddcomment" param="<?cs var:uin ?>''<?cs var:itemid?>''-1''0''<?cs call:url_escape(title)?>''<?cs var:titletype?>" type="text" version="6" charset="UTF-8" config="0|1|0|0|0|0|0">回复</qz:reply>
	</div>
<?cs /if?>
<?cs /def ?>


<div class="feeds_tp_3 music_player_feed">
	<?cs if:qz_metadata.newdesc.con || qz_metadata.newdesc.con.0 || subcount(qz_metadata.newdesc.con.0) > 0?><div class="txtbox"><div class="txtbox"><strong class="quotes_symbols_left c_tx3">“</strong><?cs call:newRichContent(qz_metadata.newdesc) ?><strong class="quotes_symbols_right c_tx3">”</strong></div></div><?cs /if?>
	<?cs if:noreply != "1" ?><?cs /if?>
</div>