<?cs #: 根据来源(app/portal)生成商家链接 ?>
<?cs def:geneShopLink() ?>
	<?cs if:qz_metadata.opadmin == 1 && qz_metadata.source == 2 ?>
		<?cs var:qz_metadata.cityname ?> <a class="c_tx" href="http://meishi.qq.com/shops/<?cs var:qz_metadata.sid ?>" target="_blank"><?cs var:qz_metadata.sname ?></a>
	<?cs else ?>
		<?cs var:qz_metadata.cityname ?> <a class="c_tx" href="http://rc.qzone.qq.com/myhome/qqmeishi?shopid=<?cs var:qz_metadata.sid ?>" target="_blank"><?cs var:qz_metadata.sname ?></a>
	<?cs /if ?>
<?cs /def ?>

<?cs #: 根据来源(app/portal)生成点评链接 ?>
<?cs def:geneTopicLink() ?>
	<?cs if:qz_metadata.opadmin == 1 && qz_metadata.source == 2 ?>
		<a class="c_tx" href="http://meishi.qq.com/reviews/<?cs var:qz_metadata.timestamp ?>_<?cs var:qz_metadata.sid ?>_<?cs var:qz_metadata.uin ?>_2" target="_blank"><?cs var:qz_metadata.message ?></a>
	<?cs else ?>
		<a class="c_tx" href="http://rc.qzone.qq.com/myhome/qqmeishi?vc=<?cs var:qz_metadata.uin ?>_<?cs var:qz_metadata.sid ?>" target="_blank"><?cs var:qz_metadata.message ?></a>
	<?cs /if ?>
<?cs /def ?>

<?cs #:这里是主入口哦 ?>
<?cs if:qz_metadata.optype == 10 ?>
	评论我对<?cs call:geneShopLink() ?>的点评：<?cs call:geneTopicLink() ?>
<?cs elif:qz_metadata.optype == 11 ?>
	在<?cs call:geneShopLink() ?>的点评：<?cs call:geneTopicLink() ?>回复我
<?cs elif:qz_metadata.optype == 12 ?>
	<?cs if:qz_metadata.isking == 1 ?>
		评论我成为<?cs call:geneShopLink() ?>的酋长
	<?cs else ?>
		评论我在<?cs call:geneShopLink() ?>的报到
	<?cs /if ?>
<?cs elif:qz_metadata.optype == 13 ?>
	<?cs if:qz_metadata.isking == 1 ?>
		在「成为<?cs call:geneShopLink() ?>的酋长」的评论中回复我
	<?cs else ?>
		在「<?cs call:geneShopLink() ?>报到」的评论中回复我
	<?cs /if ?>
<?cs elif:qz_metadata.optype == 14 ?>
	评论我获得勋章：<a class="c_tx" href="http://rc.qzone.qq.com/myhome/qqmeishi?user=<?cs var:qz_metadata.uin ?>&amp;uc=<?cs var:qz_metadata.ugcshop ?>" target="_blank"><?cs var:qz_metadata.medalname ?></a>
<?cs elif:qz_metadata.optype == 15 ?>
	在「获得勋章：<a class="c_tx" href="http://rc.qzone.qq.com/myhome/qqmeishi?user=<?cs var:qz_metadata.uin ?>&amp;uc=<?cs var:qz_metadata.ugcshop ?>" target="_blank"><?cs var:qz_metadata.medalname ?></a>」的评论中回复我
<?cs elif:qz_metadata.optype == 16 ?>
	评论我成为<?cs call:geneShopLink() ?>的酋长
<?cs elif:qz_metadata.optype == 17 ?>
	在「成为<?cs call:geneShopLink() ?>的酋长」的评论中回复我
<?cs elif:qz_metadata.optype == 3 ?>
	在<?cs call:geneShopLink() ?>报到时提到我
<?cs elif:qz_metadata.optype == 4 ?>
	在<?cs call:geneShopLink() ?>的点评：<?cs call:geneTopicLink() ?>中提到我<span class="ifeeds_origin c_tx3">通过QQ空间</span>
<?cs /if ?>
