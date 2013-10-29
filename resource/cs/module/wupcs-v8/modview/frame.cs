<?cs #:认证标记 ?>
<?cs def:v8_getLogo() ?>
	<?cs if:string.slice(qz_metadata.meta.otherflag, 0, 1) == '1' ?>
		<a target="_blank" href="http://page.qq.com" >
			<img class="ui_mr5" style="vertical-align: -2px;" src="/ac/qzone_v5/client/auth_icon.png" title="腾讯认证" alt="腾讯认证" />
		</a>
	<?cs /if ?>
<?cs /def ?>

<?cs #:emoji 表情 ?>
<?cs def:v8_setEmoji(emoji) ?>
	<?cs if:subcount(emoji)>0 ?>
		<a href="javascript:;" class="qz_emoji" target="_blank" title="体验个性昵称" tabindex="-1">
		<?cs loop:i=0,subcount(emoji),1 ?>
			<?cs if:string.length(emoji[i])>0 ?>
				<img class="ui_emoji emoji_<?cs var:emoji[i] ?>" src="http://qzonestyle.gtimg.cn/qzone_v6/img/emoji/<?cs var:emoji[i] ?>.png" />
			<?cs /if ?>
		<?cs /loop ?>
		</a>
	<?cs elif:string.length(emoji)>0 ?>
		<a href="javascript:;" class="qz_emoji" target="_blank" title="体验个性昵称" tabindex="-1">
			<img class="ui_emoji emoji_<?cs var:emoji ?>" src="http://qzonestyle.gtimg.cn/qzone_v6/img/emoji/<?cs var:emoji ?>.png" />
		</a>
	<?cs /if ?>
<?cs /def ?>

<?cs #:帽子 ?>
<?cs def:v8_genHat() ?>
	<?cs if:string.length(qz_metadata.meta.DK_HAT_ID) > 0 ?>
	<a title="点击装扮头像装饰" 
		target="_blank"
		href='http://rc.qzone.qq.com/profile/qqhat/' 
		onclick = 'TCISD.hotClick("click_feed","hat.qzone.qq.com");'
		class="f-user-hat" tabindex="-1">
		<img src="http://qzonestyle.gtimg.cn/qzone/space_item/orig/<?cs var:qz_metadata.meta.DK_HAT_ID % 16 ?>/<?cs var:qz_metadata.meta.DK_HAT_ID ?>_50<?cs if:qz_metadata.meta.isIE6==1 ?>_ie6<?cs /if ?>.png">
	</a>
	<?cs /if ?>
<?cs /def ?>

<?cs #:
	/*
	 * qz_vip_icon_{sizeTag}{yearTag} qz_vip_icon{flashTag}_{sizeTag}{greyTag}{yearTag}{yearGrayTag}_{lv}
	 */
?>
<?cs def:v8_genVipIcon() ?>
<?cs if:qfv.meta.feedstype != UC_WUP_FEEDSTYPE_PSV && g_refer != SENCE_REQ_HOME?>

	<?cs set:lv=bitmap_value_ex(qfv.meta.bitmap,18,4) ?>
	<?cs set:isSuper=bitmap_value_ex(qfv.meta.bitmap,29,1) ?>
	<?cs set:isVip=(isSuper>0||bitmap_value_ex(qfv.meta.bitmap,27,1)>0) ?>
	<?cs set:isYearVip=(isVip && bitmap_value_ex(qfv.meta.bitmap,17,1)) ?>
	<?cs set:isYearExpire=(!isYearVip && bitmap_value_ex(qfv.meta.bitmap,39,1)) ?>

	<?cs #:黄钻是否过期 ?>
	<?cs if: isVip ?>
		<?cs set:v8_greyTag="" ?>
	<?cs else ?>
		<?cs set:v8_greyTag="-gray" ?>
	<?cs /if ?>

	<?cs if: isYearVip||isYearExpire ?>
		<?cs set:v8_yearTag="-y" ?>
	<?cs else ?>
		<?cs set:v8_yearTag="" ?>
	<?cs /if ?>

	<?cs if: isYearExpire ?>
		<?cs set:v8_yearGrayTag="-gray" ?>
	<?cs else ?>
		<?cs set:v8_yearGrayTag="" ?>
	<?cs /if ?>

	<?cs #:是否豪华黄钻 ?>
	<?cs if: isSuper ?>
		<?cs set:v8_flashTag="-fla" ?>
	<?cs else ?>
		<?cs set:v8_flashTag="" ?>
	<?cs /if ?>
	<?cs if: isVip || isSuper || isYearVip || isYearExpire?>
		<?cs #set:className = "qz-vip-icon-l" + v8_yearTag + " qz-vip-icon"+ v8_flashTag + "-l" + v8_greyTag + v8_yearTag + v8_yearGrayTag + "-" + lv ?>
		<?cs #set:className = "qz-vip-icon-s" + v8_yearTag + " qz-vip-icon"+ v8_flashTag + "-s" + v8_greyTag + v8_yearTag + v8_yearGrayTag + "-" + lv ?>
		<?cs set:className = "qz-f-vip-l" + v8_yearTag + " qz-f-vip"+ v8_flashTag + "-l" + v8_greyTag + v8_yearTag + v8_yearGrayTag + "-" + lv ?>

		<a class="qz-ichotclick <?cs var:className ?>" 
			hotclickpath="isd.qzonemall.year.feeds" 
			hotdomain="mall.qzone.qq.com" 
			href="http://vip.qzone.com/year.html?login=qq" 
			target="_blank" 
			title="点击查看黄钻特权详情"> </a>
	<?cs /if ?>
<?cs /if?>
<?cs /def ?>

<?cs ####
	/**
	 *feeds外围数据，生成节点属性放在在外层的li标签里 
	 */
?>
<?cs def:v8_feeds_frame_data()?>
	data-feedsflag="<?cs var:qz_metadata.meta.feedsflag?>" 
	data-iswupfeed="1" <?cs #已经在wup模板里就肯定是wupfeed了?>
	data-key="<?cs var:qz_metadata.meta.feedkey ?>" 
	<?cs #标识是否是时光feed。这种特殊类型(只出现在主动中)，这是一种新的区分维度，与scope不一样?>
	data-specialtype="<?cs var:qz_metadata.meta.specialType?>" 
	data-extend-info="<?cs var:qfv.meta.otherflag ?>|<?cs var:qfv.meta.bitmap ?>|<?cs var:qfv.meta.yybitmap ?>"
<?cs /def?>

<?cs ####
	/**
	 * 权限ICON
	 */
?>
<?cs def:v8_oprPrivacyIcon()?>
	<?cs if:subcount(qfv.operate.privacy_icon)?>
		<span class="state ui-mr8"><i class="ui-icon icon-competence" title="<?cs var:qfv.operate.privacy_icon.text?>"></i></span>
	<?cs /if?>
<?cs /def?>

<?cs ##{新版feeds?>

<?cs def:v8_feeduser_basicInfo()?>
	<?cs call:v8_setEmoji(qz_metadata.meta.emoji) ?>
	<a target="_blank" href="<?cs var:qfv.meta.userhome ?>"  data-clicklog="nick" 
		class="f-name <?cs if:!qfv.meta.qun_feed?>q_namecard <?cs /if?>" link="nameCard_<?cs var:qfv.meta.opuin ?>">
		<?cs var:html_encode(qfv.meta.username, 1) ?>
	</a> <?cs #注意这里需要一个空格?>
	<?cs call:v8_getLogo() ?><?cs #:认证标记 ?>
	<?cs #因为feed_title_icon.cs 里面直接使用item这个名字，而这个文件又被非wup的使用，所以要这样改?>
	<?cs with:item = qz_metadata.meta?>
		<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
			<?cs #被动不要等级信息?>
			<?cs set:g_wup_psv_no_grade = 1?>
		<?cs /if?>
		<?cs include:"feed_title_icon.cs" ?>
	<?cs /with?>
<?cs /def?>

<?cs def:v8_feed_extendinfo()?>
	<div class="info-detail">
		<?cs call:v8_mainTime()?>
		<?cs call:v8_lbs()?><?cs #lbs?>
		<?cs if:qfv.meta.feedstype != UC_WUP_FEEDSTYPE_PSV?>
			<?cs #被动不要来源?>
			<?cs call:v8_source()?>
		<?cs /if?>
		<?cs call:v8_visitor()?>
		<?cs call:v8_oprPrivacyIcon()?>
	</div>
<?cs /def?>

<?cs def:v8_frame_avatar()?>
	<div class="f-user-pto">
		<a href="<?cs var:qfv.meta.userhome ?>" target="_blank" class="f-user-avatar <?cs if:!qfv.meta.qun_feed?>q_namecard <?cs /if?>f-s-a" link="nameCard_<?cs var:qfv.meta.opuin ?>" data-clicklog="avatar">
			<img alt="" src="<?cs alt:qfv.meta.useravatar?>/ac/b.gif<?cs /alt ?>">
		</a>
		<?cs call:v8_genHat() ?>
	</div>
<?cs /def?>

<?cs def:v8_frame_userinfo()?>
	<div class="f-user-info">
		<div class="f-nick">
			<?cs call:v8_feeduser_basicInfo()?>
		</div>
		<?cs call:v8_feed_extendinfo()?>
	</div>
	<?cs call:v8_genVipIcon() ?>
<?cs /def?>

<?cs ##}?>
