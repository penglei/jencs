<?cs #:Feed标题 ?>
<?cs def:feedTitle(mod) ?>
	<?cs set:qz_feedtitle_name = 'feedtitle' ?>
	<?cs if:subcount(qz_metadata.feedtitle_2) > 0 ?>
		<?cs set:qz_feedtitle_name = 'feedtitle_2' ?>
	<?cs /if ?>
	<?cs call:richTitle(qz_metadata[qz_feedtitle_name].content) ?>
<?cs /def ?>