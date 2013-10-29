<?cs set:isWupfeed = (subcount(qz_metadata.orgdata)>0)?>

<?cs if:isWupfeed == 1 ?>

	<?cs def:debug(node)?>
		<h1><?cs name:node?>:<?cs var:node?></h1>
	<?cs /def?>

	<?cs def:set(path, name, value)?>
		<?cs if:name == ""?>
			<?cs set:qfv[path] = value ?>
		<?cs else ?>
			<?cs set:qfv[path][name] = value?>
		<?cs /if?>
	<?cs /def?>

	<?cs def:qfv(path, value)?>
		<?cs set:qfv[path] = value?>
	<?cs /def?>

	<?cs include:"wupcs/data.cs"?>
	<?cs include:"wupcs/view_hs.cs"?>

<?cs else ?>
	<?cs include:"hs_summary_old.cs" ?>
<?cs /if ?>



