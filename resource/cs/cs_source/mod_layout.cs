<?cs #从布局版本开始执行?>
<?cs def:start(module) ?>
	<?cs def:versionCaller(mod,ver) ?>
		<?cs #只有存在多版本布局时，前面的假设成立?>
		<?cs set:layout_v = "layout_v" + ver ?>
		<?cs if:subcount(mod[layout_v]) > 0 ?>
			<?cs call:modCaller(mod[layout_v]) ?>
		<?cs else ?>
			<?cs call:modCaller(mod) ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs def:initConfig(mod, ver) ?>
		<?cs #:初始化配置项目 ?>
		<?cs if:subcount(mod[layout_v]) > 0 ?>
				<?cs set:qz_config = mod[layout_v].config ?>
		<?cs else ?>
				<?cs set:qz_config = mod.config ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs #假设所有feed的存在布局版本?>
	<?cs set:qz_layout_version = 1 ?>
	<?cs if:qz_metadata.layout_version[module] ?>
		<?cs set:qz_layout_version = qz_metadata.layout_version[module] ?>
	<?cs /if?>
	<?cs if:module == "summary" ?>
		<?cs call:versionCaller(qz_metadata.qz_layout_summary, qz_layout_version) ?>
		<?cs call:initConfig(qz_metadata.qz_layout_summary, qz_layout_version) ?>
	<?cs elif:module == "title" ?>
		<?cs call:versionCaller(qz_metadata.qz_layout_title, qz_layout_version) ?>
	<?cs /if ?>
<?cs /def ?>