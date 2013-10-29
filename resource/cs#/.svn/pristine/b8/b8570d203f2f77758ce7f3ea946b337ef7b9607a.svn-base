<?cs if:qz_metadata.hdf.hotspotv3 ?>
	<?cs set:_isNewOringal = 1 ?>
<?cs else?>
	<?cs set:_isNewOringal = 0 ?>
<?cs /if?>


<?cs def:userlogo(item) ?>
	<?cs #:临时在这里加display:inline 解决ie6下float+margin 的双重margin bug ?>
	<li style="display:inline;"><a href="javascript:;" isLiked="<?cs var:item.isLiked?>" class="_des" link="des_<?cs var:item.uin ?>" cmdstr="<?cs if:_isNewOringal>0 ?>showPopupFeeds<?cs else ?>showFeeds<?cs /if ?>" nick="<?cs var:html_encode(item.nickname, 1) ?>" uin="<?cs var:item.uin ?>" title="点击查看<?cs var:html_encode(item.nickname,1) ?>的原创信息"><img alt="<?cs var:html_encode(item.nickname, 1) ?>" src="http://qlogo<?cs var:item.uin % 4 + 1 ?>.store.qq.com/qzone/<?cs var:item.uin ?>/<?cs var:item.uin ?>/<?cs if:_isNewOringal>0 ?>50<?cs else ?>30<?cs /if ?>"></a><?cs if:item.newsCount ?><i class="num"><?cs var:item.newsCount?></i><?cs /if?></li>
<?cs /def ?>

<?cs def:missRate(feeds) ?>
	<div class="statistical-info bor2 bg bg2_hover none " id="FPOriginalReadedPer" style="cursor:pointer;" data-hasmore="<?cs var:feeds.hasmore ?>" ><span class="text">查看更多</span></div>
<?cs /def ?>

<?cs def:main() ?>
	<?cs if:_isNewOringal>0 ?>
		<h2>可能错过的精彩动态</h2>
	<?cs else ?>
		<h2>好友最新原创</h2>
	<?cs /if ?>

	<a class="c_tx3 ui_x bg6_hover" cmdstr="closeOb" href="javascript:;" title="关闭">×</a>

	<div class="avatar_list clearfix">
		<?cs if:_isNewOringal>0 ?>
			<?cs call:missRate(feeds) ?>
		<?cs /if ?>
		<ul id="FriendListCon"<?cs if:_isNewOringal>0 ?> class="pto-list clearfix"<?cs /if ?>>
			<?cs if:_isNewOringal!=1 ?>
				<li class="current first" index=1 cur=1><a href="javascript:;" style="display:none;" type="text" title="点击查看原创信息" class="bor_bg2" cmdstr="showFeeds" uin="fp"><span>全部</span></a><a href="javascript:;" class="bg6 bor_bg2" title="点击查看原创信息" cmdstr="showFeeds" uin="fp" type="image"><img alt="" src="/qzone_v6/img/icenter/icenter_feed_friends.png"></a><b class="trig bor_bg6"></b></li>
			<?cs /if ?>
			<?cs if:subcount(feeds.users.user.0) > 0 ?>
				<?cs if:subcount(feeds.users.user) >= 7 ?>
					<?cs set:_ori_feed_count= 6 ?>
				<?cs else ?>
					<?cs set:_ori_feed_count= subcount(feeds.users.user) ?>
				<?cs /if?>
				<?cs loop:i=0,_ori_feed_count-1,1 ?>
					<?cs call:userlogo(feeds.users.user[i]) ?>
				<?cs /loop?>
			<?cs elif:subcount(feeds.users.user) > 0 ?>
				<?cs call:userlogo(feeds.users.user) ?>
			<?cs /if ?>
		</ul>
	</div>

	<?cs if:_isNewOringal!=1 ?>
		<?cs #:显示3个最近的feed  ?>
		<?cs if:feeds.friend_data.0 || subcount(feeds.friend_data.0) > 0 ?>
			<div class="feeds bg2" id="FPOriginalBroadcaster">
				<?cs each:item=feeds.friend_data ?>
					<?cs var:item.summary ?>
				<?cs /each ?>
			</div>
		<?cs elif:subcount(feeds.friend_data) > 0 ?>
			<div class="feeds bg2" id="FPOriginalBroadcaster">
				<?cs var:feeds.friend_data.summary ?>
			</div>
		<?cs /if ?>
		<div class="feeds bg2" id="OriginalBroadcaster" style="display:none;"></div>
	<?cs /if ?>
<?cs /def ?>
<?cs call:main() ?>
