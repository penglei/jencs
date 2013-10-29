<?cs def:feeds_titles(item) ?>
	<?cs if:item.uin > 1000 ?><a class="c_tx q_namecard q_des" link="nameCard_<?cs var:item.uin ?> des_<?cs var:item.uin ?>" href="http://user.qzone.qq.com/<?cs var:item.uin ?>" target="_blank"><?cs var:item.text ?></a>
	<?cs elif:item.url ?>
		<?cs if:string.length(item.text) > 64 ?>
			<a class="c_tx" href="<?cs var:item.url ?>" target="_blank"><?cs var:string.slice(item.text,0,64) ?>...</a><?cs else ?><a class="c_tx" href="<?cs var:item.url ?>" target="_blank"><?cs var:item.text ?></a>
		<?cs /if ?>
	<?cs else ?>
		<span class="c_tx3"><?cs var:item.text ?></span>
	<?cs /if ?>
<?cs /def ?>
<?cs if:qz_metadata.titles.item.0 || subcount(qz_metadata.titles.item.0) > 0 ?>
	<?cs each:item = qz_metadata.titles.item ?>
		<?cs call:feeds_titles(item) ?>
	<?cs /each ?>
<?cs elif:qz_metadata.titles.item || subcount(qz_metadata.titles.item) > 0 ?>
	<?cs call:feeds_titles(qz_metadata.titles.item) ?>
<?cs /if ?>
<?cs if:qz_metadata.original?><span class="ifeeds_origin c_tx3"><?cs var:qz_metadata.original.text ?></span><?cs /if ?>