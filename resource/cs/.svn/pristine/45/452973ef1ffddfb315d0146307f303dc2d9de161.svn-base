<?cs set:qz_mobile[1] = 'QQ空间手机版'?>
<?cs set:qz_mobile[1].stat = 'noniphone'?>
<?cs set:qz_mobile[1].img = 'icon_mobile.png'?>
<?cs set:qz_mobile[3] = 'iPad'?>
<?cs set:qz_mobile[3].stat = 'ipad'?>
<?cs set:qz_mobile[3].img = 'icon_ipad.png'?>
<?cs set:qz_mobile[4] = 'Android'?>
<?cs set:qz_mobile[4].stat = 'android'?>
<?cs set:qz_mobile[4].img = 'icon_android.png'?>
<?cs set:qz_mobile[12] = 'iPhone'?>
<?cs set:qz_mobile[12].stat = 'iphone'?>
<?cs set:qz_mobile[12].img = 'icon_iphone.png'?>

<?cs set:qz_mobile_type = -1 ?>

<?cs #/*不定义宏是因为该文件肯能被包含两次(不同分支条件中)*/?>

<?cs #支持WUP ?>
<?cs set:_platformid = qz_metadata.orgdata.platformid ?>
<?cs set:_subplatformid = qz_metadata.orgdata.subplatformid ?>
<?cs if:!_platformid ?>
	<?cs set:_platformid = qz_metadata.source.platformid ?>
	<?cs set:_subplatformid = qz_metadata.source.subplatformid ?>
<?cs /if?>
<?cs if:_platformid || _subplatformid ?>
	<?cs include:"source_platform_config.cs"?>
	<?cs if:_platformid == UC_PLATFORM_ID_MOBILE?>
		<?cs with:subplatformid = _subplatformid?>
			<?cs #只显示有配置的几个类型 ?>
			<?cs if:qz_mobile[subplatformid] ?>
				<?cs set:qz_mobile_type = subplatformid ?>
				<?cs set:qz_mobile[qz_mobile_type].url = qz_source_platform[UC_PLATFORM_ID_MOBILE][subplatformid].url?>
				<?cs if:qz_metadata.source.url?>
					<?cs set:qz_mobile[qz_mobile_type].url = qz_metadata.source.url?>
				<?cs /if?>
			<?cs /if?>
		<?cs /with?>
	<?cs /if?>
<?cs /if?>


<?cs #------空间等级icon-------?>
<?cs if:g_wup_psv_no_grade != 1?>
	<?cs with:grade = item.DK_GARDENER?>
	<?cs if:grade >= 30?>

	<?cs set: len = string.length(grade)?>
	<a target="_blank" onclick="window.open('http://rc.qzone.qq.com/qzscore');
								window.TCISD && window.TCISD.hotClick('level_icon', 'ic3act.qzone.qq.com');
								return false;" 
		href="javascript:;"
	>
		<span class="ui_mr5" title="当前空间等级：<?cs var:grade?>级；积分：<?cs var:item.DK_SCORE?>分">
		<?cs if:grade >= 64?>
			<span class="qz_qzone_lv qz_qzone_lv_4 qz_qzone_no_<?cs var:len?>">
		<?cs elif:grade >= 30?>
			<span class="qz_qzone_lv qz_qzone_lv_3 qz_qzone_no_<?cs var:len?>">
		<?cs /if?>
				<span class="no">
					<?cs loop: i = 0, len - 1, 1?>
						<b class="d<?cs var:string.slice(grade, i, i+1)?>"></b>
					<?cs /loop?>
				</span>
			</span>
		</span>
	</a>
	<?cs /if?>
	<?cs /with?>
<?cs /if?>
<?cs #------------------------?>

<?cs if:qz_mobile_type>=0 ?>
	<a target="_blank" 
		href="<?cs var:qz_mobile[qz_mobile_type].url ?>" 
		title="来自<?cs var:qz_mobile[qz_mobile_type] ?>" 
		hotclickPath="feed.<?cs var:qz_mobile[qz_mobile_type].stat ?>" 
		hotDomain="ic3act.qzone.qq.com"
	>
		<img src="http://qzonestyle.gtimg.cn/qzone_v6/img/feed/<?cs var:qz_mobile[qz_mobile_type].img ?>">
	</a>
<?cs /if ?>


<?cs #------活动icon-------?>
<?cs set:hero_medal_level =bitmap_value_ex(qz_metadata.hdf.yybitmap,4,3) ?>
<?cs set:qixi_icon =bitmap_value_ex(qz_metadata.hdf.yybitmap,8,1) ?>

<?cs if:qixi_icon ==1 ?>
	<a class="valentine-day" target="_blank" href="http://qzs.qq.com/qzone/mall/v8/module/act/index.html?act=midautumn.2013" title="参加七夕活动，点亮爱神图标" onclick="window.TCISD && window.TCISD.hotClick('meko.qixi', 'vip.qzone.com');">
		<i class="ui-ico ico-valentine-day"></i>
	</a>
<?cs elif:hero_medal_level == 1 ?><?cs #英雄勋章-绿巨人?>
        <span class="hero-medal" data-hlevel="<?cs var:hero_medal_level?>">
            <i class="ui-ico ico-hero-green-giant"></i>
            <a class="txt" target="_blank" href="/qzone/mall/v8/module/act/index.html?act=iamhero" title="领取英雄勋章" 
            	onclick="window.TCISD && window.TCISD.hotClick('meko.iamhero', 'vip.qzone.com');"></a>
        </span>
<?cs elif:hero_medal_level == 2 ?><?cs #英雄勋章-蝙蝠侠?>
    <span class="hero-medal" data-hlevel="<?cs var:hero_medal_level?>">
        <i class="ui-ico ico-hero-batman"></i>
        <a class="txt" target="_blank" href="/qzone/mall/v8/module/act/index.html?act=iamhero" title="领取英雄勋章"
         onclick="window.TCISD && window.TCISD.hotClick('meko.iamhero', 'vip.qzone.com');"></a>
    </span>
<?cs elif:hero_medal_level == 3 ?><?cs #英雄勋章-黑寡妇?>
    <span class="hero-medal" data-hlevel="<?cs var:hero_medal_level?>">
        <i class="ui-ico ico-hero-black-widow"></i>
        <a class="txt" target="_blank" href="/qzone/mall/v8/module/act/index.html?act=iamhero" title="领取英雄勋章" 
         onclick="window.TCISD && window.TCISD.hotClick('meko.iamhero', 'vip.qzone.com');"></a>
    </span>
<?cs elif:hero_medal_level == 4 ?><?cs #英雄勋章-忍者神龟?>
    <span class="hero-medal" data-hlevel="<?cs var:hero_medal_level?>">
        <i class="ui-ico ico-hero-turtle"></i>
        <a class="txt" target="_blank" href="/qzone/mall/v8/module/act/index.html?act=iamhero" title="领取英雄勋章" onclick="window.TCISD && window.TCISD.hotClick('meko.iamhero', 'vip.qzone.com');"></a>
    </span>
<?cs elif:hero_medal_level == 5 ?><?cs #英雄勋章-美国队长?>
    <span class="hero-medal" data-hlevel="<?cs var:hero_medal_level?>">
        <i class="ui-ico ico-hero-american-captain"></i>
        <a class="txt" target="_blank" href="/qzone/mall/v8/module/act/index.html?act=iamhero" title="领取英雄勋章" onclick="window.TCISD && window.TCISD.hotClick('meko.iamhero', 'vip.qzone.com');"></a>
    </span>
<?cs else ?>
<?cs /if ?>


