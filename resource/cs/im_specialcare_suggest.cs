<?cs if:usespecialcare==2 || usespecialcare==3 ?>
<div id="no_care_tips" class="f_item fn_hint">
	<?cs if:specialcare_count==0 ?>
	<p>暂时无特别关心的好友</p>
	<?cs elif:specialcare_count>=0 ?>
	<p>您关心的好友无动态更新</p>
	<?cs /if ?>
	<p><a href="http://user.qzone.qq.com/<?cs var:uin ?>/infocenter?qz_referrer=special&source=qqtab" target="_blank" class="hotclick" data-hctag="spcaretab.goqzone.addfriend" data-hcsuffix="goqzone">去空间添加</a></p>
</div>
<?cs /if ?>

<?cs if:show_suggest==1 ?>
<div id="suggest_item" class="f_item">
	<div class="fn_title" id="suggest_title">
		<h3 class="title_text">你可能关心</h3>
		<div class="title_op"><span id="op_care_close" class="c_tx3 none hotclick" data-hctag="spcaretab.recommend" data-hcsuffix="clickx">×</span></div>		
	</div>
	<div id="suggest_list" class="fn_recm_care">
		<ul id="sul_list">
			<?cs each:item = suggest_specialcare_list ?>
			<li id="sc_li_<?cs var:item.uin ?>" uin="<?cs var:item.uin ?>" class="recm_item">
				<a class="recm_ava hotclick" href="http://user.qzone.qq.com/<?cs var:item.uin ?>" target="_blank" data-hctag="spcaretab.goqzone.nickname" data-hcsuffix="recommend"><img src="<?cs var:item.icon ?>" /></a>
				<div class="recm_name"><a href="http://user.qzone.qq.com/<?cs var:item.uin ?>" target="_blank" class="hotclick" data-hctag="spcaretab.goqzone.nickname" data-hcsuffix="recommend"><?cs var:html_encode(item.nick,1) ?></a></div>
				<div class="recm_op">
					<div id="cared_<?cs var:item.uin ?>" uin="<?cs var:item.uin ?>" class="recm_op_cared hotclick" data-hctag="spcaretab.uncare.click" data-hcsuffix="<?cs if:specialcare_count==0 ?>recommfeed<?cs else ?>recommend<?cs /if ?>"><a class="ui_btn" href="javascript:;"><i class="ui_icon icon_op_cared" uin="<?cs var:item.uin ?>" title="已关心"></i><span class="c_tx3">已关心</span></a></div>
					<div id="care_<?cs var:item.uin ?>" uin="<?cs var:item.uin ?>" class="recm_op_care hotclick" data-hctag="spcaretab.care.click" data-hcsuffix="<?cs if:specialcare_count==0 ?>recommfeed<?cs else ?>recommend<?cs /if ?>"><a href="javascript:;" uin="<?cs var:item.uin ?>" id="op_care_<?cs var:item.uin ?>" class="ui_btn"><i class="ui_icon icon_op_care"></i>特别关心</a></div>
				</div>
			</li>
			<?cs /each ?>
		</ul>
		<p><a id="sc_change" class="hotclick" data-hctag="spcaretab.recommend" data-hcsuffix="clicknext" href="javascript:;" title="<?cs if:specialcare_count==0 ?>换一组<?cs else ?>换一个<?cs /if ?>"><?cs if:specialcare_count==0 ?>换一组<?cs else ?>换一个<?cs /if ?></a></p>
	</div>
</div>
<?cs /if ?>

<?cs if:usespecialcare==2 || usespecialcare==3 ?>
<div id="no_friend_tip" class="fn_title none">
	<h3 class="title_text">关心好友，不再错过精彩动态：</h3>
</div>
<?cs /if ?>

