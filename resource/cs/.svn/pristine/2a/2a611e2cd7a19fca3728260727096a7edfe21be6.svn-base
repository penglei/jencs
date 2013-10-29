<?cs #:个人档  ?>
<?cs def:profileViewer(mod) ?>
	<?cs def:showProfileItem(item)?>
	<p>
		<?cs if:item.type == 1 ?>
			<i class="ui_ico icon_profile icon_profile_1"></i>
			<?cs if:string.length(item.firstdata) == 0 ?>
				<?cs var:item.enddata ?>
			<?cs else ?>
				从“<?cs var:item.firstdata ?>”变成“<?cs var:item.enddata ?>”
			<?cs /if ?>
		<?cs elif:item.type == 2?>
			<i class="ui_ico icon_profile icon_profile_2"></i>
			<?cs if:string.length(item.firstdata) == 0 ?>
				搬到“<?cs var:item.enddata ?>”
			<?cs else ?>
				从“<?cs var:item.firstdata ?>”搬到“<?cs var:item.enddata ?>”
			<?cs /if ?>
		<?cs elif:item.type == 3 ?>
			<i class="ui_ico icon_profile icon_profile_3"></i>
			家乡是“<?cs var:item.enddata ?>”
		<?cs /if ?>
	</p>
	<?cs /def ?>

	<?cs if:subcount(qz_metadata.profile) > 0 && qz_metadata.profile.content.type < 4 ?>
	<div class="f_ct_imgtxt f_ct_profile"><div class="txt_box">
		<?cs if: subcount(qz_metadata.profile.psv) > 0 ?>
		<p>
			<a class="c_tx" href="http://user.qzone.qq.com/<?cs var:qz_metadata.profile.psv.host_uin?>/profile/" target="_blank"><?cs var:qz_metadata.profile.psv.host_name?> </a>
			<span class="c_tx3 ">更新了 </span>
			<a class="c_tx" href="http://user.qzone.qq.com/<?cs var:qz_metadata.profile.psv.host_uin?>/profile/" target="_blank">个人资料</a>
		</p>
		<?cs /if ?>

		<?cs if: subcount(qz_metadata.profile.content.0) > 0 ?>
		<?cs each:item=qz_metadata.profile.content ?>
			<?cs call:showProfileItem(item) ?>
		<?cs /each ?>
		<?cs else ?>
			<?cs call:showProfileItem(qz_metadata.profile.content) ?>
		<?cs /if ?>
	</div></div>
	<?cs /if ?>
<?cs /def ?>