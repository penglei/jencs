<?cs #:一级元件召唤 ?>
<?cs def:modCaller(titleMod) ?>
	<?cs def:mods(mod) ?>
		<?cs if:mod.name == "feedTitle" ?>
			<?cs call:feedTitle(mod) ?>
		<?cs elif:mod.name == 'actionTitle' ?>
			<?cs call:actionTitle(mod) ?>
		<?cs elif:mod.name == 'checkin' ?>
			<?cs call:checkin(mod) ?>
		<?cs /if ?>
		<?cs if:mod.contentHide==1 ?>
			<div class="f_fold">
				<a title="查看详情" class="c_tx" href="javascript:;" cmd="toggleContentBox">
					查看详情
				</a>
			</div>
		<?cs /if ?>
	<?cs /def ?>

	<?cs #:这个来判断是否数组的方法,从来就没有爽过 叼~~?>
	<?cs if:subcount(titleMod.mod.0) > 0 ?>
		<?cs each:mod = qz_metadata.qz_layout_title.mod ?>
			<?cs call:mods(mod) ?>
		<?cs /each ?>
	<?cs else ?>
		<?cs call:mods(titleMod.mod) ?>
	<?cs /if ?>
<?cs /def ?>