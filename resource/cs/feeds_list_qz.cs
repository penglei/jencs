<?cs def:getLogo(item) ?>
	<?cs if:string.slice(item.otherflag, 0, 1) == '1' ?>
		<a target="_blank" href="http://page.opensns.qq.com" >
			<img style="vertical-align: -2px;" src="/ac/qzone_v5/client/auth_icon.png" title="腾讯认证" alt="腾讯认证" />
		</a>
	<?cs /if ?>
<?cs /def ?>

<?cs def:setEmoji(emoji) ?>
	<?cs if:subcount(emoji)>0 ?>
		<a href="javascript:;" class="qz_emoji" title="体验个性昵称">
		<?cs loop:i=0,subcount(emoji),1 ?>
			<?cs if:string.length(emoji[i])>0 ?>
				<img class="ui_emoji emoji_<?cs var:emoji[i] ?>" src="http://qzonestyle.gtimg.cn/qzone_v6/img/emoji/<?cs var:emoji[i] ?>.png" />
			<?cs /if ?>
		<?cs /loop ?>
		</a>
	<?cs elif:string.length(emoji)>0 ?>
		<a href="javascript:;" class="qz_emoji" title="体验个性昵称">
			<img class="ui_emoji emoji_<?cs var:emoji ?>" src="http://qzonestyle.gtimg.cn/qzone_v6/img/emoji/<?cs var:emoji ?>.png" />
		</a>
	<?cs /if ?>
<?cs /def ?>

<div class="feed">
	<div class="feed_inner">
		<ul id="ifeedsContainer">
		<?cs each:item = feeds.feeds_list ?>
			<?cs set:isWupfeed = bitmap_value_ex(item.feedsflag, 17, 1)?>
			<li class="f_single imgBlock bor2">
				<div class="f_aside imgBlock_img">
					<qz:user param='<?cs var:item.userHome ?>|
									<?cs var:item.logimg ?>|
									<?cs var:item.namecardLink ?>|
									<?cs var:item.info_user_name ?>||
									<?cs var:item.vip ?>|
									<?cs var:item.type ?>|
									<?cs var:item.uin ?>|
									<?cs var:item.ouin ?>|
									<?cs var:item.otherflag ?>' 
								nick='<?cs var:html_encode(item.nickname, 1) ?>'></qz:user>
				</div>
				<div class="f_wrap imgBlock_ct">
					<div link="1" 
						class="f_item" 
						data-isWupfeed="<?cs var:isWupfeed?>" 
						data-feedsflag="<?cs var:item.feedsflag?>" 
						id="feed_<?cs var:item.uin ?>
							_<?cs var:item.appid ?>
							_<?cs var:item.typeid ?>
							_<?cs var:item.abstime ?>
							_<?cs var:item.feedno ?>
							_<?cs var:item.scope ?>
							_<?cs var:item.ver ?>"
					>
						<div class="f_info">
							<?cs call:setEmoji(item.emoji) ?>
							<a target="_blank" href="http://user.qzone.qq.com/<?cs var:item.uin ?>/profile" 
								class="nickname q_namecard c_tx" 
								link="nameCard_<?cs var:item.uin ?>"
							>
								<?cs var:html_encode(item.nickname, 1) ?>
							</a>
							<?cs call:getLogo(item) ?>
				<?cs if:isWupfeed != 1?><?cs #非wupfeed才直接输出?>
							<?cs var:item.title ?>
						</div>
						<div class="qz_summary" 
							id="hex_<?cs var:item.appid ?>
								_<?cs var:item.abstime ?>
								_<?cs var:item.uin ?>
								_<?cs var:item.scope ?>"
						>
							<?cs var:item.summary ?>
						</div>
				<?cs else ?>
					<?cs #/*title 和 summary 已经合在一起生成*/ ?>
					<?cs var:item.summary?><?cs #/*内部闭合div*/?>
				<?cs /if?>
			</div>
		</div>
		</li>
	<?cs /each ?>
		</ul>
	</div>
</div>

<?cs if:string.length(hasMoreFeeds) > 0 ?>
	<script type="text/javascript">
		var G_Param = {
				scope:<?cs var:scope ?>,
				daylist:'<?cs var:daylist ?>',
				uinlist:'<?cs var:uinlist ?>',
				filter:'<?cs var:filter ?>',
				hasMoreFeeds:'<?cs var:hasMoreFeeds ?>',
				offset:<?cs var:offset ?>,
				count:<?cs var:count ?>,
				begintime:<?cs var:begintime ?>,
				myFeeds_new_cnt:<?cs var:myFeeds_new_cnt ?>
			}
	</script>
<?cs /if ?>
