<?cs if:qz_metadata.type > 1 ?>
<div class="feeds_tp_6">
	<div class="imgbox">
		<?cs if:subcount(qz_metadata.summary.items.img.0)>#0 ?>
			<?cs each:item=qz_metadata.summary.items.img ?>
				<a href="<?cs var:item.url ?>" target="_blank">
		            <img alt="<?cs var:item.alt ?>" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,90,'<?cs var:item.src ?>');"/>
		        </a>
		    <?cs /each ?>
		<?cs elif:subcount(qz_metadata.summary.items.img)>#0 ?>
			<a href="<?cs var:qz_metadata.summary.items.img.url ?>" target="_blank">
	            <img alt="<?cs var:qz_metadata.summary.items.img.alt ?>" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,90,'<?cs var:qz_metadata.summary.items.img.src ?>');"/>
	        </a>
		<?cs /if ?>
    </div>
    <?cs if:qz_metadata.type == #1 ?>
		<?cs #活动类 ?>
		<div class="c_tx3">
			<?cs var:qz_metadata.activity.text.0 ?>
		</div>
		<p class="same_operate __qzdev_sameuser c_tx3 none"><span class="__qzdev_sameuser_span"><span class="none">.</span></span>进行了此装扮</p>
		<div class="feeds_tp_operate">
			<a class="c_tx" href="<?cs var:qz_metadata.activity.text.1.url ?>" target="_blank"><?cs var:qz_metadata.activity.text.1 ?></a>
		</div>
	<?cs elif:qz_metadata.type == #2 ?>
	<?cs #时事类 ?>
	     <p class="same_operate __qzdev_sameuser c_tx3 none"><span class="__qzdev_sameuser_span"><span class="none">.</span></span>进行了此装扮</p>
		 <div class="feeds_tp_operate">
			<a class="c_tx" href="http://rc.qzone.qq.com/mall?target=free" target="_blank">我也支持一下</a>
	     </div>
	<?cs elif:qz_metadata.type == #3 ?>
	<?cs #普通装扮类 ?>
	    <p class="same_operate __qzdev_sameuser c_tx3 none"><span class="__qzdev_sameuser_span"><span class="none">.</span></span>进行了此装扮</p>
        <div class="feeds_tp_operate">
            <?cs if:qz_metadata.try.uin.value > #10000 ?>
				<?cs if:qz_metadata.try.item.suitflag == 1 ?>
					<a class="c_tx" href="javascript:;" onclick="return QZONE.ICFeeds.Interface.checkForDress(this,'<?cs var:qz_metadata.try.uin.value ?>','<?cs var:qz_metadata.try.item.value ?>',1);">预览装扮</a>
				<?cs elif:qz_metadata.try.item.suitflag == 0 ?>
					<a class="c_tx" href="javascript:;" onclick="return QZONE.ICFeeds.Interface.checkForDress(this,'<?cs var:qz_metadata.try.uin.value ?>','<?cs var:qz_metadata.try.item.value ?>',0);">预览装扮</a>
				<?cs /if ?>
            <?cs /if ?>
			<a class="c_tx" href="http://rc.qzone.qq.com/mall?target=<?cs var:qz_metadata.sametarget ?>" target="_blank">更多同类物品</a>
        </div>
	<?cs elif:qz_metadata.type == #4 ?>
	<?cs #赠送类 ?>
		<div class="txtbox quote_txt">
			<strong class="quotes_symbols_left c_tx3">“</strong><?cs var:qz_metadata.word ?><strong class="quotes_symbols_right c_tx3">”</strong>
		</div>
		<div class="feeds_tp_operate">
			<a class="c_tx" href="javascript:;" onclick="QZONE.FP.toApp('/mall');return false;" target="_blank">赠送好友装扮</a>
	    </div>
	<?cs elif:qz_metadata.type == #5 ?>
		<?cs #新活动类 ?>
		<div class="c_tx3">
			已有<span class="qz_orgin_cnt person" cnttype="act"></span>人参与此活动&nbsp;&nbsp;100%中奖
		</div>
		<p class="same_operate __qzdev_sameuser c_tx3 none"><span class="__qzdev_sameuser_span"><span class="none">.</span></span>进行了此装扮</p>
		<div class="feeds_tp_operate">
			<a class="c_tx" href="<?cs var:qz_metadata.activity.text.1.url ?>" target="_blank"><?cs var:qz_metadata.activity.text.1 ?></a>
		</div>
		<qz:data key="http://qzs.qq.com/qzone/mall/app/vip_reward/index.html:for_count" />
	<?cs elif:qz_metadata.type == #6 ?>
		<?cs #新活动类 其实就是type5的，改了下文字而已?>
		<div class="c_tx3">
			已有<span class="qz_orgin_cnt person" cnttype="act"></span>人成功参加了活动
		</div>
		<p class="same_operate __qzdev_sameuser c_tx3 none"><span class="__qzdev_sameuser_span"><span class="none">.</span></span>进行了此装扮</p>
		<div class="feeds_tp_operate">
			<a class="c_tx" href="<?cs var:qz_metadata.activity.text.1.url ?>" target="_blank"><?cs var:qz_metadata.activity.text.1 ?></a>
		</div>
		<qz:data key="<?cs var:qz_metadata.participant ?>" />
    <?cs else ?>
		<div class="feeds_tp_operate">
			<?cs if:qz_metadata.try.uin.value > #10000 ?>
				<?cs if:qz_metadata.try.item.suitflag == 1 ?>
					<a class="c_tx" href="javascript:;" onclick="return QZONE.ICFeeds.Interface.checkForDress(this,'<?cs var:qz_metadata.try.uin.value ?>','<?cs var:qz_metadata.try.item.value ?>',1);">我也要试试</a><a href="javascript:;" class="c_tx __qzdev_good_btn">赞一个</a>
				<?cs elif:qz_metadata.try.item.suitflag == 0 ?>
					<a class="c_tx" href="javascript:;" onclick="return QZONE.ICFeeds.Interface.checkForDress(this,'<?cs var:qz_metadata.try.uin.value ?>','<?cs var:qz_metadata.try.item.value ?>',0);">我也要试试</a><a href="javascript:;" class="c_tx __qzdev_good_btn">赞一个</a>
				<?cs else ?>
					<a class="c_tx" href="javascript:;" onclick="return QZONE.ICFeeds.Interface.checkForDress(this,'<?cs var:qz_metadata.try.uin.value ?>','<?cs var:qz_metadata.try.item.value ?>',null);">我也要试试</a>
				<?cs /if ?>
			<?cs /if ?>
		</div>
	<?cs /if ?>
</div>
<?cs else ?>
<?cs #这里是旧的模版,过一段时间后删除?>
<div class="feeds_tp_6">
	<div class="imgbox">
		<?cs if:subcount(qz_metadata.summary.items.img.0)>#0 ?>
			<?cs each:item=qz_metadata.summary.items.img ?>
				<a href="<?cs var:item.url ?>" target="_blank">
		            <img alt="<?cs var:item.alt ?>" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,90,'<?cs var:item.src ?>');"/>
		        </a>
		    <?cs /each ?>
		<?cs elif:subcount(qz_metadata.summary.items.img)>#0 ?>
			<a href="<?cs var:qz_metadata.summary.items.img.url ?>" target="_blank">
	            <img alt="<?cs var:qz_metadata.summary.items.img.alt ?>" class="bor3" src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,90,'<?cs var:qz_metadata.summary.items.img.src ?>');"/>
	        </a>
		<?cs /if ?>
    </div>
     <?cs if:qz_metadata.type == #1 ?>
	     <div class="c_tx3">
			<?cs var:qz_metadata.activity.text.0 ?>
	     </div>
	     <div class="feeds_tp_operate">
			<a class="c_tx" href="<?cs var:qz_metadata.activity.text.1.url ?>" target="_blank"><?cs var:qz_metadata.activity.text.1 ?></a>
	     </div>
     <?cs else ?>
        <div class="feeds_tp_operate">
            <?cs if:qz_metadata.try.uin.value > #10000 ?>
				<?cs if:qz_metadata.try.item.suitflag == 1 ?>
					<a class="c_tx" href="javascript:;" onclick="return QZONE.ICFeeds.Interface.checkForDress(this,'<?cs var:qz_metadata.try.uin.value ?>','<?cs var:qz_metadata.try.item.value ?>',1);">我也要试试</a><a href="javascript:;" class="c_tx __qzdev_good_btn">赞一个</a>
				<?cs elif:qz_metadata.try.item.suitflag == 0 ?>
					<a class="c_tx" href="javascript:;" onclick="return QZONE.ICFeeds.Interface.checkForDress(this,'<?cs var:qz_metadata.try.uin.value ?>','<?cs var:qz_metadata.try.item.value ?>',0);">我也要试试</a><a href="javascript:;" class="c_tx __qzdev_good_btn">赞一个</a>
				<?cs else ?>
					<a class="c_tx" href="javascript:;" onclick="return QZONE.ICFeeds.Interface.checkForDress(this,'<?cs var:qz_metadata.try.uin.value ?>','<?cs var:qz_metadata.try.item.value ?>',null);">我也要试试</a>
				<?cs /if ?>
            <?cs /if ?>
        </div>
		<div class="feeds_praise __qzdev_good none">
			<div class="feeds_comment_line">
				<span class="none">none</span>
			</div>
			<span class="none">none</span>
		</div>
	<?cs /if ?>
</div>
<?cs /if ?>

