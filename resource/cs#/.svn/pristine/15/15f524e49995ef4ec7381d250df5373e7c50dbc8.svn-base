<?cs include:"source_platform_config.cs"?>

<?cs def:get_source_info()?>
	<?cs set:platformid = qz_metadata.source.platformid?>
	<?cs set:subplatformid = qz_metadata.source.subplatformid?>

	<?cs #/* 只显示指定来源 */?>
	<?cs if:(platformid == UC_PLATFORM_ID_QZONE && subplatformid == 11)
			|| (platformid == UC_PLATFORM_ID_MOBILE && (subplatformid==2 || subplatformid==3 || subplatformid==4 || subplatformid==12))
			?>
		<?cs if:platformid == UC_PLATFORM_ID_OTHER?><?cs #/*业务只传url或者useragent*/?>
			<?cs if:qz_metadata.source.useragent?>
				<?cs set:get_source_info.ret.name = qz_metadata.source.useragent?>
				<?cs set:get_source_info.ret.url = qz_metadata.source.url?>
			<?cs /if?>
		<?cs elif:platformid ?>
			<?cs if:qz_metadata.source.useragent?>
				<?cs set:get_source_info.ret.name = qz_metadata.source.useragent?>
			<?cs elif:qz_source_platform[platformid][subplatformid] ?>
				<?cs set:get_source_info.ret.name = qz_source_platform[platformid][subplatformid]?>
			<?cs elif:qz_source_platform[platformid]?>
				<?cs set:get_source_info.ret.name = qz_source_platform[platformid]?>
			<?cs /if?>

			<?cs if:qz_metadata.source.url?>
				<?cs set:get_source_info.ret.url = qz_metadata.source.url?>
			<?cs elif:qz_source_platform[platformid][subplatformid].url?>
				<?cs set:get_source_info.ret.url = qz_source_platform[platformid][subplatformid].url?>
			<?cs /if?>
		<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs def:opr_source_platform()?>
	<?cs call:get_source_info()?>
	<?cs if:get_source_info.ret.name?>
		<span class="ui_mr10 c_tx3">来自
		<?cs if:get_source_info.ret.url?>
			<a href="<?cs call:ugc_url_check(get_source_info.ret.url,needPrint) ?>" target="_blank" class="c_tx3">
				<?cs var:get_source_info.ret.name?>
			</a>
		<?cs else ?>
			<?cs var:get_source_info.ret.name?>
		<?cs /if?>
		</span>
	<?cs /if?>
<?cs /def?>
