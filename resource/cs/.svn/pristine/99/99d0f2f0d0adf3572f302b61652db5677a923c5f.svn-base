<?cs ####
    /**
     *生成feeds id
     */
?>
<?cs def:v8_genFeedId()?><?cs var:qfv.meta.opuin ?>_<?cs var:qfv.meta.appid ?>_<?cs var:qfv.meta.typeid ?>_<?cs var:qfv.meta.abstime ?>_<?cs var:qfv.meta.scope ?>_<?cs #/*var:qfv.meta.ver*/ ?>1<?cs #/*wup feed 肯定算收归后的feed*/?><?cs /def ?>

<?cs def:v8_echo_feed_data()?>
<i class="none" name="feed_data" 
	 data-bitmap="<?cs var:qfv.meta.bitmap?>"
	 data-yybitmap="<?cs var:qfv.meta.yybitmap?>"

	<?cs #!!祥见../modata/common.cs?>
	 data-fkey="<?cs var:qz_metadata.meta.feedkey?>"<?cs #当前feed key?>
	 data-tid="<?cs var:qfv.meta.hostid?>"<?cs #当前贴id(转发情况就是转发的新生成帖子的id)?>
	 data-uin="<?cs var:qfv.meta.hostuin?>"<?cs #主贴uin...?>
	 data-origfkey="<?cs var:qz_metadata.meta.origid?>"<?cs #被转发贴(原贴)的feed key?>
	 data-origtid="<?cs var:qz_metadata.orgdata.mkey?>"<?cs #原帖id?>
	 data-origuin="<?cs var:qz_metadata.orgdata.uin?>"<?cs #原贴uin?>

	 data-subid="<?cs var:qfv.meta.subid?>"<?cs #业务生成，比如只有一张照片，它就是照片id(TODO 难道只有相册在用)?>
	 data-totweet="<?cs var:qfv.meta.totweet?>"<?cs #是否转发到微博?>
	 data-issignin="<?cs var:qfv.meta.issignin?>"<?cs #TODO 是否是签到?? ?>
	 data-source="<?cs var:qfv.meta.moodSource?>"<?cs #TODO 不知道是做什么的.. ?>
	 data-retweetcount="<?cs alt:qfv.meta.fwdcount?>0<?cs /alt?>"<?cs #由转发按钮生成?>
	 data-stat="<?cs var:qz_metadata.meta.feedstat ?>"<?cs #feeds上报统计所用 ?>
	 <?cs if:qfv.meta.topicid ?>data-topicid="<?cs var:qfv.meta.topicid ?>" <?cs /if ?>
	 <?cs if:qfv.meta.subappid ?>data-subappid="<?cs var:qfv.meta.subappid ?>" <?cs /if ?><?cs #用于赞被动获取原appid ?>
	 <?cs if:qfv.meta.feedoptype ?>data-feedstype="<?cs var:qfv.meta.feedoptype ?>" <?cs /if ?>
	 data-abstime="<?cs var:qz_metadata.meta.abstime ?>"  <?cs #站内tips统计feeds频率用到 ?>
	 data-iswupfeed=1
	 <?cs if:qfv.meta.appid == 352 ?>data-appname="<?cs var:qz_metadata.orgdata.itemdata[0].name ?>" <?cs /if ?> <?cs #:应用的名称，在隐藏此类应用中用到 ?>
></i>

<?cs #:如果没打开就不搞出去 ?>
<?cs if:qfv.debug_on != 0 ?>
<i class="none" name="feed_debug_log">
	<?cs set:_end = subcount(qfv.debug) - 1?>
	<?cs loop:j=0, _end, 1?>
		<?cs var:qfv.debug[j] ?>
	<?cs /loop?>
</i>
<?cs /if ?>
<?cs /def?>

<?cs def:echo_item_class()?>
	<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
	 f-item-passive
	<?cs /if?>
<?cs /def?>
