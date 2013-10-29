<?cs def:data_oprtime()?>
	<?cs if:qz_metadata.orgdata.strtime?>
		<?cs call:data_con_txt("time", qz_metadata.meta.feedtime, "tip", "10")?>
		<?cs call:qfv("time.abstime", qz_metadata.meta.abstime)?>
	<?cs /if?>
<?cs /def?>
