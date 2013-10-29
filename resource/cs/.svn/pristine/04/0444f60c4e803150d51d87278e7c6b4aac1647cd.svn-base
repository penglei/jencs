<?cs #:平台组默认给所有字段做了html编码，这里的自定义方法现在不做处理 ?>
<?cs def:html_escape(source)?><?cs escape:"none"?><?cs var:source?><?cs /escape?><?cs /def?>
<?cs def:url_escape(source)?><?cs escape:"url"?><?cs var:source?><?cs /escape?><?cs /def?>
<?cs def:js_escape(source)?><?cs escape:"js"?><?cs var:source?><?cs /escape?><?cs /def?>

<?cs set:titletype = qz_metadata.title_sec.titletype ?>
<?cs set:title = qz_metadata.title_sec.title ?>
<?cs set:uin = qz_metadata.title_sec.uin ?>
<?cs set:itemid = qz_metadata.title_sec.itemid ?>
<?cs set:fromtype = qz_metadata.title_sec.fromtype ?>
<?cs set:typename = qz_metadata.title_sec.typename ?>


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



<?cs def:replyView(item)?>
<div class="feeds_comm_list bg2">
	<div class="feeds_comment_cont">
		<p class="feeds_comment_text">
			<a href="<?cs var:item.nameurl ?>" class="q_namecard q_des comment_nickname c_tx" <?cs if:200==item.commtype ?><?cs else ?>link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>"<?cs /if ?> target="_blank"><?cs call:html_escape(item.name) ?></a>
			<?cs if:subcount(item.content_at)>0?>
				<?cs call:newRichContent(item.content_at)?>
			<?cs else?>
				<?cs call:html_escape(item.content)?>
			<?cs /if?>
		</p>
		<p class="feeds_comment_op">
			<span class="feeds_time c_tx3"><?cs var:item.time ?></span>
		</p>
	</div>
</div>
<?cs /def?>


<?cs if:subcount(qz_metadata.reply) > 0?>

	<?cs set:feed = qz_metadata.feed ?>
	<?cs set:title = qz_metadata.title ?>
	<?cs set:shareuin = qz_metadata.shareuin ?>
	<?cs set:shareid = qz_metadata.shareid ?>
	<?cs set:commuin = qz_metadata.commuin ?>
	<?cs set:commnick = qz_metadata.name ?>
	<?cs set:commnickurl = qz_metadata.nameurl ?>
	<?cs set:comment = qz_metadata.comment ?>
	<?cs set:commid = qz_metadata.commid ?>
	<?cs set:commtype = qz_metadata.commtype ?>
	<?cs set:commtime = qz_metadata.time ?>
	<?cs set:replyparam = qz_metadata.replyparam ?>
	<?cs set:replyomit = qz_metadata.replyomit ?>

	

	<?cs if:qz_metadata.noreply != 1 ?>
		<div class="feeds_tp_1"><div class="feeds_comment"><div class="comment_arrow c_bg2">◆</div> <div class="feeds_comm_list bg2" ><div class="feeds_comment_cont"><p class="feeds_comment_text"><a href="<?cs var:commnickurl ?>" class="q_namecard q_des comment_nickname c_tx" <?cs if:200==item.commtype ?><?cs else ?>link="nameCard_<?cs var:item.commuin ?> des_<?cs var:item.commuin ?>"<?cs /if ?> target="_blank"><?cs call:html_escape(commnick) ?></a><?cs if:subcount(qz_metadata.comment_at)>0?><?cs call:newRichContent(qz_metadata.comment_at)?><?cs else?><?cs call:html_escape(qz_metadata.comment) ?><?cs /if?></p><p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:commtime ?></span></p></div></div> <?cs if:replyomit>0 ?><div class="more_feeds_comment bg2"><a href="http://user.qzone.qq.com/<?cs var:shareuin ?>/share/<?cs var:shareid ?>" class="c_tx" target="_blank">中间省略<?cs var:replyomit?>条回复>></a></div><?cs /if ?>
			<?cs if:subcount(qz_metadata.reply.item.0)>0?>
				<?cs each:rep = qz_metadata.reply.item?>
					<?cs call:replyView(rep)?>
				<?cs /each?>				
			<?cs elif:subcount(qz_metadata.reply.item)>0?>
				<?cs call:replyView(qz_metadata.reply.item)?>
			<?cs /if?>
			<qz:reply max="2" action="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareaddcomment" param="<?cs var:uin ?>''<?cs var:itemid?>''<?cs var:commid?>''<?cs var:commuin?>''<?cs call:url_escape(title)?>''<?cs var:titletype?>" config="0|1|0|0|0|0|0" type="text" version="6" charset="UTF-8">回复</qz:reply></div></div>
	<?cs /if?>
<?cs else?>

	<?cs set:commnick = qz_metadata.name ?>
	<?cs set:nickurl = qz_metadata.nameurl ?>
	<?cs set:link = qz_metadata.link ?>
	<?cs set:reply = qz_metadata.reply ?>
	<?cs set:feed = qz_metadata.feed ?>
	<?cs set:commuin = qz_metadata.commuin ?>
	<?cs set:shareuin = qz_metadata.shareuin ?>
	<?cs set:shareid = qz_metadata.shareid ?>
	<?cs set:title = qz_metadata.title ?>
	<?cs set:url = qz_metadata.url ?>
	<?cs set:comment = qz_metadata.comment ?>
	<?cs set:commid = qz_metadata.commid ?>
	<?cs set:time = qz_metadata.time ?>
	<?cs set:time_ = qz_metadata.time_ ?>
	<?cs set:replyparam = qz_metadata.replyparam ?>
	<?cs if:qz_metadata.noreply != 1 ?>
		<div class="feeds_comment"><div class="comment_arrow c_bg2">◆</div><div class="feeds_comm_list bg2" ><div class="feeds_comment_cont"><p class="feeds_comment_text"><a href="<?cs var:nickurl ?>" class="q_namecard q_des comment_nickname c_tx" <?cs var:link ?> target="_blank"><?cs call:html_escape(commnick) ?></a><?cs if:subcount(qz_metadata.comment_at)>0?><?cs call:newRichContent(qz_metadata.comment_at)?><?cs else?><?cs call:html_escape(qz_metadata.comment) ?><?cs /if?></p><p class="feeds_comment_op"><span class="feeds_time c_tx3"><?cs var:time ?></span></p></div></div><qz:reply action="http://sns.qzone.qq.com/cgi-bin/qzshare/cgi_qzshareaddcomment" charset="UTF-8" param="<?cs var:uin ?>''<?cs var:itemid?>''<?cs var:commid?>''<?cs var:commuin?>''<?cs call:url_escape(title)?>''<?cs var:titletype?>" type="text" version="6" config="0|1|0|0|0|0|0">回复</qz:reply></div>
	<?cs /if?>
<?cs /if?>