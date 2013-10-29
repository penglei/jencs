<?cs def:imgUserLink(uin, name) ?>
        <?cs set:uinmod = uin % 4 + 1?>
        <?cs set:uinsrc = "http://qlogo"+uinmod+".store.qq.com/qzone/"+uin+"/"+uin+"/30" ?>
        <a class="feeds_comment_portrait c_tx q_namecard" link="nameCard_<?cs var:uin ?>" href="http://user.qzone.qq.com/<?cs var:uin ?>" target="_blank"><img alt="<?cs var:name ?>" src="<?cs var:uinsrc ?>" /></a>
<?cs /def ?>

<?cs #:这里是主入口哦 ?>
<div class="feeds_tp_3"> 
	<?cs #: 展示图片 ?>
	<?cs if qz_metadata.imgs.total > 1 ?> <?cs #: 展示多图 ?>
		<div class="imgbox">
			<?cs set:type = 5 ?>
			<?cs if qz_metadata.optype == 2 ?> <?cs #手机侧报到 ?>
				<?cs set:type = 17 ?>
			<?cs /if ?>
			<?cs set:idx = 0 ?>
			<?cs each:item=qz_metadata.imgs.img?>
				<qz:popup param="<?cs var:qz_metadata.uin ?>" 
					src="/qzone/photo/zone/icenter_popup.html#uin=<?cs var:qz_metadata.uin ?>&index=<?cs var:idx ?>&cid=<?cs var:qz_metadata.cid ?>&type=<?cs var:type ?>&" version="2" title="">
					<img class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(100,100,'<?cs var:item.url1 ?>');" />
				</qz:popup>
				<?cs set:idx = idx + 1 ?>
			<?cs /each ?>
		</div>
		<div class="txtbox">
			<p class="img_amount c_tx3">
				共<?cs var:qz_metadata.imgs.total?>张图片
			</p>
		</div>

	<?cs elif qz_metadata.imgs.total == 1 ?>
		<div class="imgbox img_thumb">
			<a onclick="QZFL.widget.simpleImageViewer.show(this.parentNode, '<?cs var:qz_metadata.imgs.img.url2 ?>', '<?cs var:qz_metadata.imgs.img.url2 ?>', null);return false;" href="javascript:;">
				<img alt="点击查看大图" title="点击放大" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,90,'<?cs var:qz_metadata.imgs.img.url1 ?>');" />
			</a>
		</div>
	<?cs /if ?>

	<?cs #: 展示内容 ?>
	<?cs def:richItem(item) 
		?><?cs if:item.type == 1 
			?><?cs var:item.text
		?><?cs elif:item.type == 2
			?><a class="c_tx q_des q_namecard" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" target="_blank" href="http://user.qzone.qq.com/<?cs var:item.uin ?>"><?cs var:item.nick ?></a
		><?cs /if 
	?><?cs /def ?>
	<?cs if:qz_metadata.optype == 1 || qz_metadata.optype == 2 || qz_metadata.optype == 3 ?>
	<?cs if:qz_metadata.message != '' ?>
	<div class="txtbox quote_txt">
		<p><strong class="quotes_symbols_left c_tx3">“</strong
		><?cs if:qz_metadata.optype == 1 ?><?cs #: 点评 
			?><?cs if:subcount(qz_metadata.message.item.0) == 0 ?><?cs #: only text 
				?><?cs var:qz_metadata.message.item.text 
			?><?cs else 
				?><?cs loop:idx = 0, subcount(qz_metadata.message.item), 1 
					?><?cs call:richItem(qz_metadata.message.item[$idx])
				?><?cs /loop 
			?><?cs /if 
		?><?cs elif:qz_metadata.optype == 2 || qz_metadata.optype == 3 ?><?cs #: 报到/报到提到我
			?><?cs var:qz_metadata.message
		?><?cs /if
		?><strong class="quotes_symbols_right c_tx3">”</strong></p>
	</div>
	<?cs /if ?>
	<?cs /if ?>
 
	
	<?cs #: 点评详情 ?>
	<?cs if:qz_metadata.optype == 1 ?>
		<div class="grade_tp"> 
			<strong class="feeds_grade" title="<?cs var:qz_metadata.grade ?>星"><span class="grade_<?cs var:qz_metadata.grade ?>"></span></strong> 
			<?cs if:qz_metadata.taste > 0 ?>
				<span class="grade_txt">口味：<?cs var:qz_metadata.taste ?></span> 
			<?cs /if ?>
			<?cs if:qz_metadata.env > 0 ?>
				<span class="grade_txt">环境：<?cs var:qz_metadata.env ?></span> 
			<?cs /if ?>
			<?cs if:qz_metadata.svc > 0 ?>
				<span class="grade_txt">服务：<?cs var:qz_metadata.svc ?></span> 
			<?cs /if ?>
			<?cs if:qz_metadata.consume > 0 ?>
				<span class="grade_txt">人均：<?cs var:qz_metadata.consume ?>元</span> 
			<?cs /if ?>
		</div> 
		<?cs if:qz_metadata.cmdcount > 0 ?>
			<div class="grade_tp none"><span class="grade_txt">推荐菜品：<?cs var:qz_metadata.cmd ?></span></div>
		<?cs /if ?>
	<?cs /if ?>
	
	<?cs #: 展示 @ ?>
	<?cs #: 2 手机侧报到详情 ?>
	<?cs #: 3 提到我。被动feeds，但和主动feeds展示同.. ?>
	<?cs if:qz_metadata.optype == 2 || qz_metadata.optype == 3 ?>
		<?cs #: > 1 的时候才能用遍历... ?>
		<?cs if qz_metadata.together.total > 1 ?>
		<?cs set:idx = 0 ?>
		和<?cs each:item = qz_metadata.together.item 
			?><?cs set:idx = idx + 1 ?><a class="c_tx q_des q_namecard" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" target="_blank" href="http://user.qzone.qq.com/<?cs var:item.uin ?>"><?cs var:item.nick ?></a
			><?cs if idx != qz_metadata.together.total && idx != 3 ?>、<?cs /if ?>
			<?cs /each ?><?cs if qz_metadata.together.total > 3 ?>等<?cs var:qz_metadata.together.total ?>个好友<?cs /if ?>在一起
		<?cs /if ?>
		
		<?cs if qz_metadata.together.total == 1 ?>
			和<a class="c_tx q_des q_namecard" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.together.item.uin ?>"><?cs var:qz_metadata.together.item.nick ?></a>在一起
		<?cs /if ?>
	<?cs /if ?>
	
<div class="feeds_tp_operate">
	<qz:reply type="link">评论</qz:reply>
	
	<?cs if:qz_metadata.opadmin == 1 && qz_metadata.source == 2 ?>
		<a target="blank" href="http://meishi.qq.com/shops/<?cs var:qz_metadata.sid ?>" class="c_tx"><span class="c_tx">查看商家</span></a>
	<?cs else ?>
		<a target="blank" href="http://rc.qzone.qq.com/myhome/qqmeishi?shopid=<?cs var:qz_metadata.sid ?>" class="c_tx"><span class="c_tx">查看商家</span></a>
	<?cs /if ?>
	
	<?cs set:viewLink = "javascript:;" ?>
	<?cs if:qz_metadata.optype == 2 || qz_metadata.optype == 3 ?> 	<?cs # 报到、报到提到我的link ?>
		<?cs set:viewLink = "http://rc.qzone.qq.com/myhome/qqmeishi?checkin=" + qz_metadata.uin + "_" + qz_metadata.cid + "_" + qz_metadata.sid ?>
	<?cs elif:qz_metadata.optype == 1 || qz_metadata.optype == 4 ?>	<?cs # 点评、点评提到我的link ?>
		<?cs if:qz_metadata.opadmin == 1 && qz_metadata.source == 2 ?>
			<?cs set:viewLink = "http://meishi.qq.com/reviews/" + qz_metadata.timestamp + "_" + qz_metadata.sid + "_" + qz_metadata.uin + "_2" ?>
		<?cs else ?>
			<?cs set:viewLink = "http://rc.qzone.qq.com/myhome/qqmeishi?vc=" + qz_metadata.uin + "_" + qz_metadata.sid ?>
		<?cs /if ?>
	<?cs /if ?>
	<a target="blank" href="<?cs var:viewLink ?>" class="c_tx"><span class="c_tx">查看全文</span></a>
</div>
	<div class="feeds_comment">
	<div class="comment_arrow c_bg2">◆</div>
	<?cs if:qz_metadata.replycount > 0 ?>
		<?cs if:qz_metadata.replycount > subcount(qz_metadata.reply) ?>
		<div class="more_feeds_comment bg2">
			<a target="_blank" href="<?cs var:viewLink ?>" class="c_tx">查看全部<?cs var:qz_metadata.replycount ?>条评论&gt;&gt;</a>
		</div>
		<?cs /if ?>
		<?cs def:feeds_comment-items(item) ?>
			<div class="feeds_comment_list bg2">
				<?cs call:imgUserLink(item.uin, item.nick) ?>
				<div class="feeds_comment_cont">
					<p class="feeds_comment_text">
						<a target="_blank" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" href="http://user.qzone.qq.com/<?cs var:item.uin ?>" class="q_namecard q_des comment_nickname c_tx"><?cs var:item.nick ?><span class="none"></span></a>
						<?cs var:item.msg ?>
					</p>
					<p class="feeds_comment_op">
						<span class="feeds_time c_tx3"><?cs var:item.replydate ?></span>
						<qz:reply action="http://meishi.qzone.qq.com/ajax/add_rep_lv2_meta.php" param="<?cs var:qz_metadata.cid ?>,<?cs var:item.rid ?>,<?cs var:qz_metadata.optype ?>" type="text" charset="UTF-8" maxLength="200" version="6.2">回复</qz:reply>
					</p>
				</div>
				<?cs if:item.rreplycount > 0 ?>
					<div class="comment_reply_list">
						<?cs def:feeds_rrplay-items(rrplay_item) ?>
							<div class="bbor5">
								<?cs call:imgUserLink(rrplay_item.uin, rrplay_item.nick) ?>
								<div class="comment_reply_cont">
									<p class="comment_reply_text">
										<a class="q_namecard q_des comment_nickname c_tx" href="http://user.qzone.qq.com/<?cs var:item.rreply.uin ?>" target="_blank" link="nameCard_<?cs var:item.rreply.uin ?> des_<?cs var:item.rreply.uin ?>"><?cs var:rrplay_item.nick ?><span class="none"></span></a><?cs var:rrplay_item.msg ?><span class="none"></span>
									</p>
									<p class="comment_reply_op">
										<span class="feeds_time c_tx3"><?cs var:rrplay_item.replydate ?></span>
									</p>
								</div>
							</div>
						<?cs /def ?>
						<?cs if:item.rreply.0 || subcount(item.rreply.0) > 0 ?>
							<?cs each:rrplay_item = item.rreply ?>
							<?cs call:feeds_rrplay-items(rrplay_item) ?>
						<?cs /each ?>
						<?cs elif:item.rreply || subcount(item.rreply) > 0 ?>
							<?cs call:feeds_rrplay-items(item.rreply) ?>
						<?cs /if ?>
					</div>
				<?cs /if ?>
			</div>
		<?cs /def ?>
		<?cs if:qz_metadata.reply.0 || subcount(qz_metadata.reply.0) > 0 ?>
			<?cs each:item = qz_metadata.reply ?>
				<?cs call:feeds_comment-items(item) ?>
			<?cs /each ?>
		<?cs elif:qz_metadata.reply || subcount(qz_metadata.reply) > 0 ?>
			<?cs call:feeds_comment-items(qz_metadata.reply) ?>
		<?cs /if ?>
	<?cs /if ?>
	<qz:reply action="http://meishi.qzone.qq.com/ajax/add_rep_lv1_meta.php" param="<?cs var:qz_metadata.cid ?>,<?cs var:qz_metadata.optype ?>" type="text" charset="UTF-8" maxLength="200" version="6" btnstyle="6.1">评论</qz:reply>
	</div>
	<qz:data key="http://rc.qzone.qq.com/myhome/qqmeishi?vc=<?cs var:qz_metadata.uin ?>_<?cs var:qz_metadata.sid ?>"/>
</div> 
