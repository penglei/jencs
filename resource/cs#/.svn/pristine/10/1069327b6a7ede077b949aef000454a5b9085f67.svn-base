<?cs #:生成feed的黄钻信息 ?>
<?cs def:genVipIcon_new() ?>
	<?cs set:lv=bitmap_value_ex(qfv.meta.bitmap,18,4) ?>
	<?cs set:isSuper=bitmap_value_ex(qfv.meta.bitmap,29,1) ?>
	<?cs set:isVip=(isSuper>0||bitmap_value_ex(qfv.meta.bitmap,27,1)>0) ?>
	<?cs set:isYearVip=(isVip && bitmap_value_ex(qfv.meta.bitmap,17,1)) ?>
	<?cs set:isYearExpire=(!isYearVip && bitmap_value_ex(qfv.meta.bitmap,39,1)) ?>

	<?cs #:黄钻是否过期 ?>
	<?cs if: isVip ?>
		<?cs set:greyTag="" ?>
	<?cs else ?>
		<?cs set:greyTag="_gray" ?>
	<?cs /if ?>

	<?cs if: isYearVip||isYearExpire ?>
		<?cs set:yearTag="_year" ?>
	<?cs else ?>
		<?cs set:yearTag="" ?>
	<?cs /if ?>

	<?cs if: isYearExpire ?>
		<?cs set:yearGrayTag="_gray" ?>
	<?cs else ?>
		<?cs set:yearGrayTag="" ?>
	<?cs /if ?>

	<?cs #:是否豪华黄钻 ?>
	<?cs if: isSuper ?>
		<?cs set:flashTag="_fla" ?>
	<?cs else ?>
		<?cs set:flashTag="" ?>
	<?cs /if ?>
	<?cs if: isVip || isSuper || isYearVip || isYearExpire?>
		<?cs set:className="qz_vip_icon_s qz_vip_icon"+flashTag+"_s"+greyTag+yearTag+yearGrayTag+"_"+lv ?>
		<a class="<?cs var:className ?>" data-hctag="spcaretab.friendpage.vipicon" href="http://vip.qzone.com/year.html" target="_blank" title="点击查看黄钻特权详情">
		</a>
	<?cs /if ?>
<?cs /def ?>

<?cs ####
	/**
	 *权限显示ICON
	 *现在这里支持两套权限，一套是相册的，一套是统一的，相册先不接吧
	 */
?>
<?cs def:genPrivacyIcon_new() ?>
	<?cs if:subcount(qfv.operate.privacy_icon)?>
		<i class="ui_icon icon_lock" title="<?cs var:qfv.operate.privacy_icon.text?>"></i>
	<?cs /if?>
<?cs /def?>

<?cs def:feed_avatar_new() ?>
	<div class="f_aside">
		<a class="f_avatar hotclick" href="<?cs var:qfv.meta.userhome?>" tabindex="-1" data-hctag="spcaretab.goqzone.nickname" target="_blank">
			<img alt="<?cs var:html_encode(qfv.meta.username, 1) ?>" src="<?cs var:qfv.meta.useravatar ?>" />
		</a>
		<a class="f_nick hotclick" href="<?cs var:qfv.meta.userhome?>"  data-hctag="spcaretab.goqzone.nickname" target="_blank"><?cs var:html_encode(qfv.meta.username, 1) ?></a>
		<?cs call:genVipIcon_new() ?>
		<?cs call:titletip() ?>
		<div class="f_info">
			<span class="f_date c_tx3"><?cs var:qfv.time.text?></span>
			<?cs call:genPrivacyIcon_new() ?>
		</div>
	</div>
<?cs /def ?>
