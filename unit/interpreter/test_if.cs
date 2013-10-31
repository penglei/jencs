================================
<?cs set:a = "s"?>

<?cs if:one?>
	one condition
<?cs elif:a ?>
    one elif condition
<?cs else ?>
    one else body
<?cs /if?>


<?cs if:a?>
    basic if statement
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
<?cs if:one?>
	one condition
<?cs else ?>
    one else condition
<?cs /if?>


====================================

<?cs set:appid = 4 ?><?cs set:appid_1 = appid?><?cs set:appid_3 = appid?><?cs set:appid_4 = appid?><?cs set:appid_5 = appid?>

<?cs var:appid?>:<?cs var:appid_1?>,<?cs var:appid_3?>,<?cs var:appid_4?>,<?cs var:appid_5?>

<?cs if:appid_1 == 1?>
111111111
<?cs elif:appid_3 == 3?>
333333333
<?cs elif:appid_4 == 4?>
ok
<?cs elif:appid_5 == 5 ?>
555555555
<?cs /if?>

<?cs set:appid = 0 ?><?cs set:appid_1 = appid?><?cs set:appid_3 = appid?><?cs set:appid_4 = appid?><?cs set:appid_5 = appid?>
<?cs var:appid?>:<?cs var:appid_1?>,<?cs var:appid_3?>,<?cs var:appid_4?>,<?cs var:appid_5?>

<?cs if:appid_1 == 1?>
111111111
<?cs else ?>
000000000
<?cs /if?>

