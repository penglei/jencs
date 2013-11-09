================================
<?cs set:foo = 1?>
<?cs set:foo.a = 111111?>
<?cs set:foo.b = 222222?>
<?cs set:foo.c = 333333?>
<?cs var:subcount(foo)?>
<?cs loop:foo = 0, subcount(foo), foo?>
    <?cs set:foo[foo] = foo?>
    --------loop(<?cs var:foo?>)--------
    <?cs var:subcount(foo)?>
<?cs /loop?>

<?cs var:subcount(foo)?>

<?cs set:bar.2 = 222?>
<?cs set:bar.1 = 111?>
<?cs set:bar.3 = 333?>

<?cs each:item = bar?>
<?cs var:item?>
<?cs /each?>
