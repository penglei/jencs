<?cs def:userImHome(url)?>
	<?cs var:url?>
<?cs /def?>

<?cs #:生成feed的id ?>
<?cs def:genFeedId(item) ?>
	<?cs var:item.opuin ?>_<?cs var:item.appid ?>_<?cs var:item.typeid ?>_<?cs var:item.abstime ?>_<?cs var:item.scope ?>_<?cs var:item.ver ?>
<?cs /def ?>

<?cs #:生成feed的黄钻信息 ?>
<?cs def:genVipIcon(item) ?>
	<?cs set:lv=bitmap_value_ex(item.bitmap,18,4) ?>
	<?cs set:isSuper=bitmap_value_ex(item.bitmap,29,1) ?>
	<?cs set:isVip=(isSuper>0||bitmap_value_ex(item.bitmap,27,1)>0) ?>
	<?cs set:isYearVip=(isVip && bitmap_value_ex(item.bitmap,17,1)) ?>
	<?cs set:isYearExpire=(!isYearVip && bitmap_value_ex(item.bitmap,39,1)) ?>

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
<?cs def:genPrivacyIcon(item) ?>
	<?cs #相册权限 ?>
	<?cs set:ALBUM_PRIV_PUBLIC = 1 ?>
	<?cs set:ALBUM_PRIV_PASSWD = 2 ?>
	<?cs set:ALBUM_PRIV_PRIVATE = 3 ?>
	<?cs set:ALBUM_PRIV_FRIEND = 4 ?>
	<?cs set:ALBUM_PRIV_QA = 5 ?>
	<?cs set:ALBUM_PROV_SOME_FRI = 6 ?>
	<?cs #统一权限控制位 ?>
	<?cs set:UGCFLAG_ALL_PUBLIC = 1?> <?cs #公开?>
	<?cs set:UGCFLAG_QQFRIEND = 4?>  <?cs #QQ好友?>
	<?cs set:UGCFLAG_WHITELIST = 16?> <?cs #指定人?>
	<?cs set:UGCFLAG_ONLYSELF = 64?> <?cs #仅自己?>
	<?cs if:item.privacy ?>
		<?cs if:item.privacy == UGCFLAG_QQFRIEND ?>
			<i class="ui_icon icon_lock" title="QQ好友可见"></i>
		<?cs elif:item.privacy == UGCFLAG_WHITELIST ?>
			<i class="ui_icon icon_lock" title="指定好友可见"></i>
		<?cs elif:item.privacy == UGCFLAG_ONLYSELF?>
			<i class="ui_icon icon_lock" title="仅自己可见"></i>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs def:feed_avatar(item) ?>
<div class="f_aside">
	<a class="f_avatar hotclick" href="<?cs call:userImHome(item.userHome)?>" tabindex="-1" data-hctag="spcaretab.goqzone.nickname" target="_blank">
		<img alt="<?cs var:html_encode(item.nickname, 1) ?>" src="<?cs var:item.logimg ?>" />
	</a>
	<a class="f_nick hotclick" href="<?cs call:userImHome(item.userHome)?>"  data-hctag="spcaretab.goqzone.nickname" target="_blank"><?cs var:html_encode(item.nickname, 1) ?></a>
	<?cs call:genVipIcon(item) ?>
	<div class="f_info">
		<span class="f_date c_tx3"><?cs var:item.time.text?></span>
		<?cs call:genPrivacyIcon(item) ?>
	</div>
</div>
<?cs /def ?>

<?cs def:genFeedHTML(item) ?>
<li class="f_item<?cs if:isWupfeed?> wupfeed<?cs /if?>" id="<?cs call:genFeedId(item)?>">
	<?cs call:feed_avatar(item)?>
	<div class="f_main">
		<?cs if:string.length(item.title) > 0 ?>
			<div class="f_title">
				<?cs var:item.title?>
			</div>
		<?cs /if ?>
		<?cs var:item.summary?>
	</div>
</li>
<?cs /def?>
<?cs set:isWupfeed = bitmap_value_ex(feeds.feedsflag, 17, 1)?>
<?cs if:isWupfeed != 1?>
	<?cs call:genFeedHTML(feeds)?>
<?cs else ?>
	<?cs var:feeds.summary ?>
<?cs /if?>