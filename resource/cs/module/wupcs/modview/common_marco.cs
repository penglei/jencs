<?cs ####
    /**
     *生成feeds id
     */
?>
<?cs def:genFeedId()?>
<?cs var:qfv.meta.opuin ?>_<?cs var:qfv.meta.appid ?>_<?cs var:qfv.meta.typeid ?>_<?cs var:qfv.meta.abstime ?>_<?cs var:qfv.meta.scope ?>_<?cs #/*var:qfv.meta.ver*/ ?>1<?cs #/*wup feed 肯定算收归后的feed*/?>
<?cs /def ?>

<?cs def:echo_feed_data()?>
<i class="none" name="feed_data" 
	 data-bitmap="<?cs var:qfv.meta.bitmap?>"
	 data-yybitmap="<?cs var:qfv.meta.yybitmap?>"

	<?cs #!!祥见../modata/common.cs?>
	 data-fkey="<?cs var:qz_metadata.meta.feedkey?>"<?cs #当前feed key?>
	 data-tid="<?cs var:qfv.meta.hostid?>"<?cs #当前贴id(转发情况就是转发的新生成帖子的id)?>
	 data-uin="<?cs var:qfv.meta.hostuin?>"
	 data-origfkey="<?cs var:qz_metadata.meta.origid?>"<?cs #被转发贴(原贴)的feed key?>
	 data-origtid="<?cs var:qz_metadata.orgdata.mkey?>"<?cs #原帖id?>
	 data-origuin="<?cs var:qz_metadata.orgdata.uin?>"<?cs #原贴uin?>
	 <?cs if:qfv.meta.subappid ?>data-subappid="<?cs var:qfv.meta.subappid ?>" <?cs /if ?>
	 data-subid="<?cs var:qfv.meta.subid?>"<?cs #业务生成，比如只有一张照片，它就是照片id(TODO 难道只有相册在用)?>
	 data-totweet="<?cs var:qfv.meta.totweet?>"<?cs #是否转发到微博?>
	 data-issignin="<?cs var:qfv.meta.issignin?>"<?cs #TODO 是否是签到?? ?>
	 data-source="<?cs var:qfv.meta.moodSource?>"<?cs #TODO 不知道是做什么的.. ?>
	 data-retweetcount="<?cs alt:qfv.meta.fwdcount?>0<?cs /alt?>"<?cs #由转发按钮生成?>
	 data-stat="<?cs var:qz_metadata.meta.feedstat ?>"  <?cs #feeds上报统计所用 ?>
	 <?cs if:qfv.meta.topicid ?>data-topicid="<?cs var:qfv.meta.topicid ?>" <?cs /if ?>
	 <?cs if:qfv.meta.feedoptype ?>data-feedstype="<?cs var:qfv.meta.feedoptype ?>" <?cs /if ?>
	 data-abstime="<?cs var:qz_metadata.meta.abstime ?>"  <?cs #站内tips统计feeds频率用到 ?>
	 <?cs if:qfv.meta.appid == 352 ?>data-appname="<?cs var:qz_metadata.orgdata.itemdata[0].name ?>" <?cs /if ?> <?cs #:应用的名称，在隐藏此类应用中用到 ?>
></i>
<?cs /def?>

<?cs #:
	/**
	 * 对指定值做指定次数的htmlencode之后输出
	 * @param  {String} value 需要encode的值
	 * @param  {Number} time  次数
	 * @param  {Number} notprint  不直接打印出来，值为真时不打印，值为假时打印
	 * @return htmlEncodeVar_Res
	 */
	function htmlEncodeVar(value,time,notprint){}
?>
<?cs def:htmlEncodeVar(value,time,notprint) ?>
	<?cs if: time<=0 ?>
		<?cs set:time=1 ?>
	<?cs /if ?>
	<?cs set:htmlEncodeVar_Res=value ?>
	<?cs loop:i=1, time, 1 ?>
		<?cs set:htmlEncodeVar_Res=html_encode(htmlEncodeVar_Res,1) ?>
	<?cs /loop ?>
	<?cs if:notprint==0 ?>
		<?cs var:htmlEncodeVar_Res ?>
	<?cs /if ?>
<?cs /def ?>