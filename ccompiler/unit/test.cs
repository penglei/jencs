<?cs if:one?>
	one condition
<?cs /if?>
<?cs if:foo || kbp ?>
	two condition
<?cs /if?>

<?cs if:foo || bar || gar?>
	three condition
<?cs /if?>

<?cs if:0?>
	false constant
<?cs /if?>

<?cs if:1?>
	true constant
<?cs /if?>

<?cs if:0 || foo_left_false?>
	constant left false and hdf
<?cs /if?>

<?cs if:1 || foo_left_true?>
	constant left true and hdf
<?cs /if?>

<?cs if:foo1 || 0?>
	constant right false and hdf
<?cs /if?>

<?cs if:foo2 || 1?>
	constant right true and hdf
<?cs /if?>


<?cs if:foo || bar && gar ?>
	result is and
<?cs /if?>

<?cs if:foo && bar || gar ?>
	result is or
<?cs /if?>
