<?cs #:一级元件召唤 ?>
<?cs def:modCaller(summaryMod) ?>
	<?cs def:mods(mod) ?>
		<?cs if:mod.name == "quote" ?>
			<?cs call:quote(mod) ?>
		<?cs elif:mod.name == "contentBox" ?>
			<?cs call:contentBox(mod) ?>
		<?cs elif:mod.name == 'contentBoxLeftBor' ?>
			<?cs call:contentBoxLeftBor(mod) ?>
		<?cs elif:mod.name == 'contentBoxLeftBorBg' ?>
			<?cs call:contentBoxLeftBorBg(mod) ?>
		<?cs elif:mod.name == 'contentBoxLeftImage' ?>
			<?cs call:contentBoxLeftImage(mod) ?>
		<?cs elif:mod.name == 'contentBoxLeftBorLeftImage' ?>
			<?cs call:contentBoxLeftBorLeftImage(mod) ?>
		<?cs elif:mod.name == 'contentBoxLeftBorBgLeftImage' ?>
			<?cs call:contentBoxLeftBorBgLeftImage(mod) ?>
		<?cs elif:mod.name == "opr" ?>
			<?cs call:opr(mod) ?>
		<?cs elif:mod.name == "comment" ?>
			<?cs call:comment(mod) ?>
		<?cs elif:mod.name == "voteViewer" ?>
			<?cs call:voteViewer(mod) ?>
		<?cs elif:mod.name == "locationInfo" ?>
			<?cs call:locationInfo(mod) ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:summaryMod.source_in_content ?><?cs set:source_in_content=1?><?cs /if?>
	<?cs #:这个来判断是否数组的方法,从来就没有爽过 叼~~?>
	<?cs if:subcount(summaryMod.mod.0) > 0 ?>
		<?cs each:mod = summaryMod.mod ?>
			<?cs call:mods(mod) ?>
		<?cs /each ?>
	<?cs else ?>
		<?cs call:mods(summaryMod.mod) ?>
	<?cs /if ?>
	<?cs #:异步化数据key ?>
	<?cs call:qzData() ?>
<?cs /def ?>

<?cs #:fkey准备改名为tid ?>
<?cs #:origfkey准备改名为origtid ?>