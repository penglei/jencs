<?cs set:appid = qz_metadata.feedtitle.content.con.1.aid ?>
<?cs set:appname = qz_metadata.feedtitle.content.con.1.text ?>
<?cs set:appimg = qz_metadata.content_box.media.media.src ?>
<?cs set:appdetail = qz_metadata.content_box.media.media.text ?>
<?cs set:appurl = "http://rc.qzone.qq.com/myhome/"+appid+"?via=QZ.TICKER2.ISPLAYING" ?>
<?cs set:percent = qz_metadata.content_box.content.con.percent ?>
<?cs set:cnt = qz_metadata.content_box.content.con.grade ?>

<div class="f_ct">
	<div class="f_ct_imgtxt">
		<div class="img_box">
			<a target="_blank" href="<?cs var:appurl ?>"><img alt="" src="<?cs var:appimg ?>"></a>
			<p><a target="_blank" href="<?cs var:appurl ?>" class="ui_ico icon_addapp">添加应用</a></p>
		</div>
		<div class="txt_box">
			<h4 class="txt_box_title"><a target="_blank" href="<?cs var:appurl ?>"><?cs var:appname ?></a></h4>
			<div class="f_hot_vote">
				<span class="votebar ui_mr10"><span class="votebar_i  star" style="width:<?cs var:percent ?>%"></span></span><span class="c_tx3"><?cs var:percent ?>分，共<?cs var:cnt ?>人评分</span>
				<p class="hot_vote_fri none"><a class="ui_mr10" href="#">猴子与面包</a>等30位好友已添加应用</p>
			</div>
			<p><?cs var:appdetail ?></p>

		</div>
	</div>
</div>
<p class="f_detail c_tx3"><span class="mr8 c_tx3"><qz:time abstime="<?cs var:qz_metadata.time.abstime ?>"><?cs var:qz_metadata.time.text ?></qz:time></span><span class="mr8 c_tx3">通过QQ空间</span><a target="_blank" href="http://rc.qzone.qq.com/appsetup/?via=QZ.ASSISTANT.FEED.INSTALL" class="mr8" title="点击喜欢">去应用中心</a></p>