<div class="feeds_tp_3">
	<div class="img_txt_tp bg3" style="padding:8px;">
		<div class="img_ex bor3">
			<a href="<?cs var:qz_metadata.picture.url ?>" target="_blank" onclick="g_Parent.TCISD.pv('hotfeeds.qzone.qq.com', '/pic');"><img src="<?cs var:qz_metadata.picture.src ?>" style="width:100px;height:100px;" alt="图片" /></a>
		</div>
		<div class="txt_ex">
			<h6><a href="<?cs var:qz_metadata.summary.item[0].url ?>" target="_blank" onclick="g_Parent.TCISD.pv('hotfeeds.qzone.qq.com', '/title');" class="c_tx"><?cs var:qz_metadata.summary.item[0].title ?></a></h6>
			<ul>
			<?cs def:summary-items(item) ?>
				<li><strong class="feeds_txt_sort bor2"><a href="<?cs var:item.url ?>" target="_blank"  onclick="g_Parent.TCISD.pv('hotfeeds.qzone.qq.com', '/category<?cs var:tmpindex ?>');" class="c_tx"><?cs var:item.category ?></a></strong><a href="<?cs var:item.url ?>" target="_blank"  onclick="g_Parent.TCISD.pv('hotfeeds.qzone.qq.com', '/title<?cs var:tmpindex ?>');" class="txt_ex_list c_tx"><?cs var:item.title ?></a></li>
			<?cs /def ?>
			<?cs if:qz_metadata.summary.item.0 || subcount(qz_metadata.summary.item.0) > 0 ?>
				<?cs set:tmpindex = 0 ?>
				<?cs each:item = qz_metadata.summary.item ?>
					<?cs if:tmpindex > 0 ?>
						<?cs call:summary-items(item) ?>
					<?cs /if ?>
					<?cs set:tmpindex = tmpindex + 1 ?>
				<?cs /each ?>
			<?cs elif:qz_metadata.summary.item || subcount(qz_metadata.summary.item) > 0 ?>
				<?cs set:tmpindex = 1 ?>
				<?cs call:summary-items(qz_metadata.summary.item) ?>
			<?cs /if ?>
			</ul>
		</div>
		<div class="c_tx3" style="float:left;clear:both;margin-top:6px;">有<strong>__qz_dev_seeing_count__</strong>个好友也在看</div>
	</div>
	<div class="feeds_tp_operate"><a class="c_tx" href="http://rc.qzone.qq.com/myhome/share/#action=recs&amp;from=feed" target="_blank" onclick="g_Parent.TCISD.pv('hotfeeds.qzone.qq.com', '/share');">更多热门分享</a></div>
</div>