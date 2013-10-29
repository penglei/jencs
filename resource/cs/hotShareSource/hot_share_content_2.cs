<div class="feeds_tp_3">
	<div class="img_txt_tp2 bg3">
		<div class="imgbox">
			<?cs set:idx = 1 ?>
			<?cs each:item=qz_metadata.pictures.picture ?>
			<a href="<?cs var:item.url ?>" target="_blank" onclick="g_Parent.TCISD.pv('hotfeeds.qzone.qq.com', '/pic<?cs var:idx ?>');"><img alt="照片名" class="bor3 bg" src="<?cs var:item.src ?>"/></a>
			<?cs set:idx = idx + 1 ?>
			<?cs /each ?>
		</div>
		<div class="txt_ex">
			<ul>
			<?cs set:index = 1 ?>
			<?cs each:item=qz_metadata.items.item ?>
				<li><strong class="bg6"><span class="tx_bg6"><?cs var:index ?></span></strong><a href="<?cs var:item.url ?>" target="_blank" onclick="g_Parent.TCISD.pv('hotfeeds.qzone.qq.com', '/title<?cs var:index ?>');"  class="txt_ex_list c_tx"><?cs var:item.title ?></a></li>
				<?cs set:index = index + 1 ?>
			<?cs /each ?>
			</ul>
			<p>有<strong>__qz_dev_seeing_count__</strong>个好友也在看</p>
		</div>
	</div>
</div>
