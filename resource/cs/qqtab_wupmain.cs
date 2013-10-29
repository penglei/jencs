
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

<?cs set:_is_in_qqtab=1?>

<?cs include:"wupcs/qqtab/data.cs"?>
<?cs include:"wupcs/qqtab/view.cs"?>
