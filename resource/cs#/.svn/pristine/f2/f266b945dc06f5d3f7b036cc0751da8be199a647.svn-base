<?cs #: 根据来源(app/portal)生成商家链接 ?>
<?cs def:geneShopLink() ?>
	<?cs if:qz_metadata.opadmin == 1 && qz_metadata.source == 2 ?>
		<?cs var:qz_metadata.cityname ?> <a class="c_tx" href="http://meishi.qq.com/shops/<?cs var:qz_metadata.sid ?>" target="_blank"><?cs var:qz_metadata.sname ?></a>
	<?cs else ?>
		<?cs var:qz_metadata.cityname ?> <a class="c_tx" href="http://rc.qzone.qq.com/myhome/qqmeishi?shopid=<?cs var:qz_metadata.sid ?>" target="_blank"><?cs var:qz_metadata.sname ?></a>
	<?cs /if ?>
<?cs /def ?>

<?cs #:这里是主入口哦 ?>
<?cs if:qz_metadata.optype == 1 ?>
点评：<?cs call:geneShopLink() ?>
<?cs /if ?>
<?cs if:qz_metadata.optype == 2 ?>
在<?cs var:qz_metadata.cityname ?> <a class="c_tx" 
	href="http://rc.qzone.qq.com/myhome/qqmeishi?shopid=<?cs var:qz_metadata.sid ?>" 
	target="_blank"><?cs var:qz_metadata.sname ?></a
	>报到<?cs if:qz_metadata.is_king == 1 ?>并成为酋长<?cs /if ?><span class="ifeeds_origin c_tx3">通过手机</span>
<?cs /if ?>