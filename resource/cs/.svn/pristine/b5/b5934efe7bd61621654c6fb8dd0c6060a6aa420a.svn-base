<?cs #:认证标记 ?>
<?cs def:getLogo(item) ?>
	<?cs if:string.slice(item.otherflag, 0, 1) == '1' ?>
		<a target="_blank" href="http://page.qq.com" >
			<img class="ui_mr5" style="vertical-align: -2px;" src="/ac/qzone_v5/client/auth_icon.png" title="腾讯认证" alt="腾讯认证" />
		</a>
	<?cs /if ?>
<?cs /def ?>

<?cs #:emoji 表情 ?>
<?cs def:setEmoji(emoji) ?>
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
<?cs def:genHat(item) ?>
	<?cs if:string.length(item.DK_HAT_ID) > 0 ?>
	<a title="点击装扮头像装饰" 
		target="_blank"
		href='javascript:;' 
		onclick = 'window.open("http://rc.qzone.qq.com/profile/qqhat/");TCISD.hotClick("click_feed","hat.qzone.qq.com");'
		class="info_user_hat f_user_hat" tabindex="-1">
		<img src="http://qzonestyle.gtimg.cn/qzone/space_item/orig/<?cs var:item.DK_HAT_ID % 16 ?>/<?cs var:item.DK_HAT_ID ?>_50<?cs if:item.isIE6==1 ?>_ie6<?cs /if ?>.png">
	</a>
	<?cs /if ?>
<?cs /def ?>

<?cs #:生成feed的id ?>
<?cs def:genFeedId(item) ?>
	<?cs var:item.opuin ?>_<?cs var:item.appid ?>_<?cs var:item.typeid ?>_<?cs var:item.abstime ?>_<?cs var:item.scope ?>_<?cs var:item.ver ?>
<?cs /def ?>

<?cs #:
	/*
	 * qz_vip_icon_{sizeTag}{yearTag} qz_vip_icon{flashTag}_{sizeTag}{greyTag}{yearTag}{yearGrayTag}_{lv}
	 */
	function genVipIcon(item){}
?>
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
		<?cs set:className="qz_vip_icon_l"+yearTag+" qz_vip_icon"+flashTag+"_l"+greyTag+yearTag+yearGrayTag+"_"+lv ?>
		<a class="f_user_vip qz_ichotclick" hotclickpath="isd.qzonemall.year.feeds" hotdomain="mall.qzone.qq.com" href="http://vip.qzone.com/year.html" style="cursor:pointer" target="_blank" title="点击查看黄钻特权详情">
			<i class="<?cs var:className ?>"></i>
		</a>
	<?cs /if ?>
<?cs /def ?>

<?cs ####
	/*feed头像*/
	function feed_avatar(item){}
?>
<?cs def:feed_avatar(item) ?>
<div class="f_aside imgBlock_img" tabindex="-1">
	<div class="qz_feed_avatar f_user_head">
		<a href="<?cs var:item.userHome ?>" target="_blank" class="q_namecard f_user_avatar" link="nameCard_<?cs var:item.opuin ?>">
			<span class="skin_portrait_round"></span>
			<img alt="<?cs var:html_encode(item.nickname, 1) ?>" src="<?cs var:item.logimg ?>">
		</a>
		<?cs call:genHat(item) ?>
		<div class="priviilege_info">
			<?cs call:genVipIcon(item) ?>
		</div>
	</div>
</div>
<?cs /def ?>

<?cs ####
	/**
	 *feed删除、屏蔽等隐藏操作按钮
	 */
?>
<?cs def:feedop(item)?>
	<?cs if:(item.appid == 403 && item.typeid == 10) ||
			(item.oprType == 2 && (item.scope != 1 && item.scope != 100)) ||
			(item.opuin == 20050606)
	?>
		<?cs #这些条件肯定是没有操作的?>
	<?cs else ?>
		<div class="f_op"
			 id="opr_<?cs call:genFeedId(item)?>"
			 data-oprtype="<?cs var:item.oprType ?>"
		>

		<?cs #逻辑太复杂，即使有外框输出，也不一定有操作，这都放在前台脚本里做?>
		<?cs #事件可以考虑用代理方式来做，暂时内联，简单一些?>
		<strong class="op_close bor2 c_tx3">
			<b class="ui_trig ui_trig_b"></b>
		</strong>
		<?cs #var:item.opuin ?>
		<?cs #var:item.appid ?>
		<?cs #var:item.typeid ?>
		<?cs #var:item.abstime ?>
		<?cs #var:item.scope ?>
		<?cs #var:item.ver ?>
		<?cs #var:item.oprType ?>
		<?cs #var:item.key ?>
		<?cs #var:item.otherflag ?>
		<?cs #var:item.rightflag ?>
		</div>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *feeds从title开始解析
	 */
?>
<?cs def:genTitleStart(item)?>
	<?cs include:"transform_metadata.cs"?>

	<div class="f_nick">
		<?cs call:setEmoji(item.emoji) ?>
		<a target="_blank" href="<?cs var:item.userHome ?>" 
			class="nickname q_namecard c_tx ui_mr5" 
			link="nameCard_<?cs var:item.opuin ?>">
			<?cs var:html_encode(item.nickname, 1) ?>
		</a>
		<?cs call:getLogo(item) ?>
		<?cs include:"feed_title_icon.cs" ?>
	</div>
	<?cs #/*有内容才显示*/?>
	<?cs if:isWupfeed != 1 && string.length(item.title)>0 ?>
			<div class="f_info">
		<?cs elif:isWupfeed==1 ?>
			<div class="f_info">
		<?cs /if ?>
		<?cs #/*var:item.title*/ ?>
	<?cs #/*</div>*//*这个结束标签留在后面输出(为wup做准备)*/?>
<?cs /def?>

<?cs ####
	/**
	 *feeds外围数据，生成节点属性放在在外层的li标签里
	 */
?>
<?cs def:feeds_frame_data(item)?>
	 data-feedsflag="<?cs var:item.feedsflag?>"
	 data-iswupfeed="<?cs var:isWupfeed?>"
	 data-key="<?cs var:item.key ?>"
	 data-specialtype="<?cs var:item.specialType?>"
	 data-extend-info="<?cs var:item.otherflag ?>|<?cs var:item.bitmap ?>|<?cs var:item.yybitmap ?>"
<?cs /def?>

<?cs def:genFeedHTML(grpitem) ?>
	<?cs set:alpha = bitmap_value_ex(grpitem.owner_bitmap,58,1) ?>

	<li class="f_single imgBlock bor2">
	<?cs #:头像区域 ?>
	<?cs call:feed_avatar(grpitem)?>
	<?cs #:内容区域 ?>
	<div class="f_wrap imgBlock_ct">
	<?cs each:item = grpitem.feeds_list?>
		<?cs set:isWupfeed = bitmap_value_ex(item.feedsflag, 17, 1)?>
		<div link="1" 
			class="f_item" 
			id="feed_<?cs call:genFeedId(item) ?>" 
			<?cs call:feeds_frame_data(item)?>
		>
			<?cs call:genTitleStart(item)?>

				<?cs if:isWupfeed != 1?>
					<?cs var:item.title ?>
					<?cs #/*有内容才显示*/?>
					<?cs if: item.scope == 1 || item.scope == 100 ?>
						</div>
					<?cs elif:string.length(item.title)>0 ?>
						</div><?cs #/*闭合genTitleStart里的div*/?>
					<?cs /if ?>
					<div class="qz_summary" id="hex_<?cs call:genFeedId(item) ?>">
						<?cs var:item.summary ?> 
					</div>
				<?cs else ?>
					<?cs #/*title 和 summary 已经合在一起生成*/ ?>
					<?cs var:item.summary?><?cs #/*内部闭合div*/?>
				<?cs /if?>

			<?cs call:feedop(item)?><?cs #/*---feed操作按钮---*/?>
		</div>
	<?cs /each ?>
	</div>
</li>
<?cs /def ?>

<?cs ####
	/**/
	function genUserLogo(item){}
?>
<?cs def:genUserLogo(item) ?>
	<div class="qz_feed_avatar f_user_head">
		<a href="<?cs var:item.userHome ?>" target="_blank" class="q_namecard f_user_avatar" link="nameCard_<?cs var:item.ouin ?>">
			<span class="skin_portrait_round"></span>
			<img alt="<?cs var:html_encode(item.nickname, 1) ?>" src="<?cs var:item.logimg ?>">
		</a>
		<?cs call:genHat(item) ?>
		<div class="priviilege_info">
			<?cs call:genVipIcon(item) ?>
		</div>
	</div>
<?cs /def ?>

<?cs if:feeds.oldfeed!=1 ?>
	<?cs each:grpitem = feeds.feeds_group ?>
		<?cs call:genFeedHTML(grpitem) ?>
	<?cs /each ?>
<?cs else ?>
	<?cs each:grpitem = feeds.feeds_group ?>

		<li class="f_single imgBlock bor2">
			<div class="f_aside imgBlock_img">
				<?cs call:genUserLogo(grpitem) ?>
			</div>
			<div class="f_wrap imgBlock_ct">
			<?cs each:item = grpitem.feeds_list?>
				<?cs set:isWupfeed = bitmap_value_ex(item.feedsflag, 17, 1)?>

				<div link="1" class="f_item" id="feed_<?cs var:item.uin ?>_<?cs var:item.appid ?>_<?cs var:item.typeid ?>_<?cs var:item.abstime ?>_<?cs var:item.feedno ?>_<?cs var:item.scope ?>_<?cs var:item.ver ?>" >
					<?cs call:genTitleStart(item)?>

					<?cs if:isWupfeed != 1?>
							<?cs var:item.title ?>
						<?cs #/*有内容才显示*/?>
						<?cs if:string.length(item.title)>0 ?>
							</div><?cs #/*闭合genTitleStart里的div*/?>
						<?cs /if ?>
						<div class="qz_summary" id="hex_<?cs var:item.uin ?>_<?cs var:item.appid ?>_<?cs var:item.typeid ?>_<?cs var:item.abstime ?>_<?cs var:item.feedno ?>_<?cs var:item.scope ?>_<?cs var:item.ver ?>">
							<?cs var:item.summary ?>
						</div>
					<?cs else ?>
						<?cs #/*title 和 summary 已经合在一起生成*/ ?>
						<?cs var:item.summary?><?cs #/*内部闭合div*/?>
					<?cs /if?>

					<?cs call:feedop(item)?>

				</div>
				<?cs /each ?>
			</div>
		</li>
	<?cs /each ?>
<?cs /if ?>
