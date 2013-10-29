<?cs if:qz_metadata.ver == 0?>
<div class="feeds_tp_3">
	<?cs if:subcount(qz_metadata.summary.avatar) > 0 ?>
	<div class="img_txt_tp">
		<div class="img_ex" style="width:50px;height:50px;">
			<a title='点击"我也关注",即可在个人中心收取该空间动态' href="http://user.qzone.qq.com/<?cs var:qz_metadata.uin ?>" target="_blank" style="width:50px;height:50px;font-size:0px;"><img src="<?cs var:qz_metadata.summary.avatar.src ?>" style="width:50px;height:50px;" alt='点击"我也关注",即可在个人中心收取该空间动态' /></a>
		</div>
		<div class="txt_ex" style="margin-left:68px;">
			<p><?cs var:qz_metadata.summary.detail?></p>
			<p class="c_tx3">共有<?cs var:qz_metadata.summary.likecount ?>人关注</p>
		</div>
	</div>
	<?cs else ?>
	<div class="txtbox">
		<p><?cs var:qz_metadata.summary.detail?></p>
		<p class="c_tx3">共有<?cs var:qz_metadata.summary.likecount ?>人关注</p>
	</div>
	<?cs /if ?>
	<div class="feeds_tp_operate"><a title="我关注" href="javascript:;" class="c_tx" onclick="g_Parent.QZONE.FrontPage.addILike(<?cs var:qz_metadata.summary.item.uin ?>,function(o){if(o.ret==-20){g_Parent.QZONE.FrontPage.showMsgbox('您已经关注此空间，请勿重复操作。',0,2000);}});return false;"><span title='点击"我也关注",即可在个人中心收取该空间动态'>我也关注</span></a></div>
</div>
<?cs else ?>
<?cs if:qz_metadata.itemcount == 1?>
<div class="feeds_tp_3">
	<?cs if:subcount(qz_metadata.summary.item.avatar) > 0 ?>
	<div class="img_txt_tp">
		<div class="img_ex" style="width:50px;height:50px;">
			<a href="http://user.qzone.qq.com/<?cs var:qz_metadata.summary.item.uin ?>" target="_blank" style="width:50px;height:50px;font-size:0px;"><img src="<?cs var:qz_metadata.summary.item.avatar.src ?>" style="width:50px;height:50px;" alt='头像' /></a>
		</div>
		<div class="txt_ex" style="margin-left:68px;">
			<p><?cs var:qz_metadata.summary.item.detail?></p>
			<p class="c_tx3">共有<?cs var:qz_metadata.summary.item.likecount ?>人关注</p>
		</div>
	</div>
	<?cs else ?>
	<div class="txtbox">
		<p><?cs var:qz_metadata.summary.item.detail?></p>
		<p class="c_tx3">共有<?cs var:qz_metadata.summary.item.likecount ?>人关注</p>
	</div>
	<?cs /if ?>
	<div class="feeds_tp_operate"><a title="我关注" href="javascript:;" class="c_tx" onclick="g_Parent.QZONE.FrontPage.addILike(<?cs var:qz_metadata.summary.item.uin ?>,function(o){if(o.ret==-20){g_Parent.QZONE.FrontPage.showMsgbox('您已经关注此空间，请勿重复操作。',0,2000);}});return false;"><span title='点击"我也关注",即可在个人中心收取该空间动态'>我也关注</span></a></div>
</div>
<?cs else ?>
	<?cs def:summary-items(item) ?>
		<?cs if:subcount(item.avatar) > 0 ?>
			<a class="q_namecard" link="nameCard_<?cs var:item.uin ?>" href="http://user.qzone.qq.com/<?cs var:item.uin ?>" target="_blank" style="width:50px;height:50px;font-size:0px;"><img src="<?cs var:item.avatar.src ?>" style="width:50px;height:50px;" /></a>
		<?cs /if ?>
	<?cs /def ?>
	<div class="feeds_tp_3">
		<div class="imgbox">
			<?cs if:qz_metadata.summary.item.0 || subcount(qz_metadata.summary.item.0) > 0 ?>
				<?cs each:item = qz_metadata.summary.item ?>
					<?cs call:summary-items(item) ?>
				<?cs /each ?>
			<?cs elif:qz_metadata.summary.item || subcount(qz_metadata.summary.item) > 0 ?>
				<?cs call:summary-items(qz_metadata.summary.item) ?>
			<?cs /if ?>
		</div>
		<div class="feeds_tp_operate"><a class="c_tx" href="http://rc.qzone.qq.com/myhome/217" target="_blank">去认证空间查看更多</a></div>
	</div>
<?cs /if ?>
<?cs /if ?>