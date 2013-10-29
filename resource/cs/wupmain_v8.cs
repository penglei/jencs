
<?cs def:debug(node)?>
	<?cs if:subcount(node)?>
		<?cs each:item=node?>
			<h1><?cs name:item?>:<?cs var:item?></h1>
		<?cs /each?>
	<?cs else ?>
		<h1><?cs name:node?>:<?cs var:node?></h1>
	<?cs /if?>
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
<?cs include:"wupcs-v8/data.cs"?>
<?cs include:"wupcs-v8/view.cs"?>
