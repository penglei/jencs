<?cs #:认证标记 ?>
<?cs def:v8_getLogo(item) ?>
	<?cs if:string.slice(item.otherflag, 0, 1) == '1' ?>
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
<?cs def:v8_genHat(item) ?>
	<?cs if:string.length(item.DK_HAT_ID) > 0 ?>
	<a title="点击装扮头像装饰" 
		target="_blank"
		href='http://rc.qzone.qq.com/profile/qqhat/' 
		onclick = 'TCISD.hotClick("click_feed","hat.qzone.qq.com");'
		class="f-user-hat" tabindex="-1">
		<img src="http://qzonestyle.gtimg.cn/qzone/space_item/orig/<?cs var:item.DK_HAT_ID % 16 ?>/<?cs var:item.DK_HAT_ID ?>_50<?cs if:item.isIE6==1 ?>_ie6<?cs /if ?>.png">
	</a>
	<?cs /if ?>
<?cs /def ?> 

<?cs def:v8_getUin(item) ?>
	<?cs if: string.length(item.opuin) ?>
		<?cs set:feedsuin=item.opuin ?>
	<?cs elif:string.length(item.uin) ?>
		<?cs set:feedsuin=item.uin ?>
	<?cs elif:string.length(item.ouin) ?>
		<?cs set:feedsuin=item.ouin ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:生成feed的id ?>
<?cs def:v8_genFeedId(item) ?><?cs var:feedsuin ?>_<?cs var:item.appid ?>_<?cs var:item.typeid ?>_<?cs var:item.abstime ?>_<?cs var:item.scope ?>_<?cs var:item.ver ?><?cs if:item.specialType ?>_<?cs var:item.specialType ?><?cs /if ?><?cs /def ?>

<?cs #:
	/*
	 * qz_vip_icon_{sizeTag}{v8_yearTag} qz_vip_icon{v8_flashTag}_{sizeTag}{v8_greyTag}{v8_yearTag}{v8_yearGrayTag}_{lv}
	 */
	function v8_genVipIcon(item){}
?>
<?cs def:v8_genVipIcon(item) ?>
	<?cs set:lv=bitmap_value_ex(item.bitmap,18,4) ?>
	<?cs set:isSuper=bitmap_value_ex(item.bitmap,29,1) ?>
	<?cs set:isVip=(isSuper>0||bitmap_value_ex(item.bitmap,27,1)>0) ?>
	<?cs set:isYearVip=(isVip && bitmap_value_ex(item.bitmap,17,1)) ?>
	<?cs set:isYearExpire=(!isYearVip && bitmap_value_ex(item.bitmap,39,1)) ?>

	<?cs #:黄钻是否过期 ?>
	<?cs if: isVip ?>
		<?cs set:v8_greyTag="" ?>
	<?cs else ?>
		<?cs set:v8_greyTag="-gray" ?>
	<?cs /if ?>

	<?cs if: isYearVip||isYearExpire ?>
		<?cs set:v8_yearTag="-year" ?>
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
		<?cs set:className="qz-vip-icon-l"+v8_yearTag+" qz-vip-icon"+v8_flashTag+"-l"+v8_greyTag+v8_yearTag+v8_yearGrayTag+"-"+lv ?>
		<a class="f-user-vip qz-ichotclick" hotclickpath="isd.qzonemall.year.feeds" hotdomain="mall.qzone.qq.com" href="http://vip.qzone.com/year.html" style="cursor:pointer" target="_blank" title="点击查看黄钻特权详情">
			<i class="<?cs var:className ?>"></i>
		</a>
	<?cs /if ?>
<?cs /def ?>

<?cs ####
	/*feed头像*/
	function feed_avatar(item){}
?>
<?cs def:v8_feed_avatar(item) ?>
<div class="f-aside" tabindex="-1">
	<div class="qz-feed-avatar f-user-head">
		<a href="<?cs var:item.userHome ?>" target="_blank" class="q_namecard f-user-avatar" link="nameCard_<?cs var:item.opuin ?>">
			<span class="skin_portrait_round"></span>
			<img alt="<?cs var:html_encode(item.nickname, 1) ?>" src="<?cs alt:item.logimg ?>/ac/b.gif<?cs /alt ?>">
		</a>
		<?cs call:v8_genHat(item) ?>
		<div class="priviilege-info">
			<?cs call:v8_genVipIcon(item) ?>
		</div>
	</div>
</div>
<?cs /def ?>

<?cs set:mergeAppWording[202]="也转了此信息" ?>
<?cs set:mergeAppWording[311]="也转了此信息" ?>
<?cs set:mergeAppWording[333]="也转了此信息" ?>
<?cs set:mergeAppWording[352]="也添加了此应用" ?>
<?cs def:v8_genMergeHtml(data) ?>
	<?cs if:subcount(data.mergeData)>0 && mergeAppWording[data.appid] && !(data.appid==352 && data.typeid!=3) ?>
		<div class="f-source-merger qz_sourcemerge_wrap">
			<a href="javascript:void(0);" class="qz_sourcemerge_btn">
			<?cs if:data.mergeData.nick.0 ?>
				<?cs if: data.mergeData.uin.0==feeds.hostuin ?>
					我
				<?cs else ?>
					<?cs var:html_encode(data.mergeData.nick.0, 1)?>
				<?cs /if ?>
				<?cs loop:i=1, subcount(data.mergeData.uin), 1 ?>
					<?cs if: data.mergeData.uin[i]!=data.mergeData.uin.0 ?>
						<?cs if:data.mergeData.nick[i] ?>
							<?cs if: data.mergeData.uin[i]==feeds.hostuin ?>
								、我
							<?cs else ?>
								、<?cs var:html_encode(data.mergeData.nick[i], 1) ?>
							<?cs /if ?>
						<?cs /if ?>
						<?cs set:i=subcount(data.mergeData.uin) ?>
					<?cs /if ?>
				<?cs /loop ?>
				<?cs if: data.mergeData.count>=2 ?>
					等<?cs var:data.mergeData.count ?>人
				<?cs /if ?>
			<?cs elif:data.mergeData.nick ?>
				<?cs if: data.mergeData.uin==feeds.hostuin ?>
					我
				<?cs else ?>
					<?cs var:html_encode(data.mergeData.nick, 1) ?>
				<?cs /if ?>
			<?cs /if ?>
			<?cs var:mergeAppWording[data.appid] ?>
			</a>
			<b class="ui-trig ui-trig-b"></b>
		</div>
	<?cs /if ?>
<?cs /def ?>


<?cs ####
	/**
	 *feed删除、屏蔽等隐藏操作按钮
	 */
?>
<?cs def:v8_feedop(item)?>
	<?cs #:wup Feeds 而且不在客人首页及个人档才这样处理 ?>
	<?cs if:isWupfeed ==1 && item.refer != SENCE_REQ_HOME ?>
		<?cs #:wupfeeds把操作区域放到下面所以这里没有 ?>
	<?cs else ?>
		<?cs if:(item.appid == 403 && item.typeid == 10) ||
				(item.oprType == 2 && (item.scope != 1 && item.scope != 100)) ||
				(item.opuin == 20050606)
		?>
			<?cs #这些条件肯定是没有操作的?>
		<?cs else ?>
			<div class="f-op"
				 id="opr_<?cs call:v8_genFeedId(item)?>"
				 data-oprtype="<?cs var:item.oprType ?>"
				 data-isv8=1
			>

			<?cs #逻辑太复杂，即使有外框输出，也不一定有操作，这都放在前台脚本里做?>
			<?cs #事件可以考虑用代理方式来做，暂时内联，简单一些?>
			<strong class="op-close">
				<b class="ui-trigbox ui-trigbox-b"><b class="ui-trig"></b><b class="ui-trig ui-trig-up"></b></b>
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
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *feeds从title开始解析
	 */
?>
<?cs def:v8_genTitleStart(item,isprofile)?>
	<?cs include:"transform_metadata.cs"?>

	<?cs if:item.scope != 1 && item.scope != 100?>
		<div class="f-nick">
			<?cs call:v8_setEmoji(item.emoji) ?>
			<a target="_blank" href="<?cs var:item.userHome ?>" 
				class="f-name q_namecard" 
				link="nameCard_<?cs var:item.opuin ?>">
				<?cs var:html_encode(item.nickname, 1) ?>
			</a>
			<?cs call:v8_getLogo(item) ?><?cs #:认证标记 ?>
			<?cs include:"feed_title_icon.cs" ?>
		</div>
		<?cs #/*有内容才显示*/?>
		<?cs if:isWupfeed != 1 && string.length(item.title)>0 ?>
			<div class="f-info">
		<?cs elif:isWupfeed==1 ?>
			<div class="f-info">
		<?cs /if ?>
			<?cs #/*var:item.title*/ ?>
		<?cs #/*</div>*//*这个结束标签留在后面输出(为wup做准备)*/?>
	<?cs else ?>
		<div class="f-info">
			<?cs call:v8_setEmoji(item.emoji) ?>
			<a target="_blank" href="<?cs var:item.userHome ?>" 
				class="f-name q_namecard" 
				link="nameCard_<?cs var:item.opuin ?>">
				<?cs var:html_encode(item.nickname, 1) ?>
			</a>
			<?cs call:v8_getLogo(item) ?>
			<?cs #/*var:item.title*/ ?>
		<?cs #/*</div>*//*同上*/?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *feeds外围数据，生成节点属性放在在外层的li标签里
	 */
?>
<?cs def:v8_feeds_frame_data(item)?>
	data-feedsflag="<?cs var:item.feedsflag?>" 
	data-iswupfeed="<?cs var:isWupfeed?>" 
	data-key="<?cs var:item.key ?>" 
	<?cs #标识是否是时光feed。这种特殊类型(只出现在主动中)，这是一种新的区分维度，与scope不一样?>
	data-specialtype="<?cs var:item.specialType?>" 
	data-extend-info="<?cs var:item.otherflag ?>|<?cs var:item.bitmap ?>|<?cs var:item.yybitmap ?>"
<?cs /def?>

<?cs def:v8_genFeedHTML(item,isprofile) ?>

	<?cs call:v8_getUin(item) ?>

	<?cs set:isWupfeed = bitmap_value_ex(item.feedsflag, 17, 1)?>

	<li class="f-single f-s-s" id="fct_<?cs call:v8_genFeedId(item) ?>">
		<?cs #:头像区域 ?>
		<?cs call:v8_feed_avatar(item) ?>
		<?cs #:内容区域 ?>
		<div class="f-wrap">
			<div link="1" 
				class="f-item f-s-i" 
				id="feed_<?cs call:v8_genFeedId(item) ?>" 
				<?cs call:v8_feeds_frame_data(item)?>
			>
				<?cs call:v8_genTitleStart(item,isprofile)?>

				<?cs if:isWupfeed != 1?>
					<?cs var:item.title ?>
					<?cs #/*有内容才显示*/?>
					<?cs if: item.scope == 1 || item.scope == 100 || isprofile==1?>
						</div>
					<?cs elif:string.length(item.title)>0 ?>
						</div><?cs #/*闭合genTitleStart里的div*/?>
					<?cs /if ?>

					<div class="qz_summary" id="hex_<?cs call:v8_genFeedId(item) ?>">
						<?cs var:item.summary ?>
					</div>
				<?cs else ?>
					<?cs #/*title 和 summary 已经合在一起生成*/ ?>
					<?cs var:item.summary?><?cs #/*内部闭合div*/?>
				<?cs /if?>

				<?cs call:v8_feedop(item)?><?cs #/*---feed操作按钮---*/?>
				<?cs call:v8_genMergeHtml(item) ?>
			</div>
		</div>
	</li>
<?cs /def ?>
