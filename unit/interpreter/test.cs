================================
<?cs set:foo = 1?>
<?cs set:foo.a = 111111?>
<?cs set:foo.b = 222222?>
<?cs set:foo.c = 333333?>
<?cs var:subcount(foo)?>
<?cs loop:foo = 0, subcount(foo), foo?>
    <?cs set:foo[foo] = foo?>
    <?cs #set:foo = 2?>
    --------loop(<?cs var:foo?>)--------
<?cs /loop?>

<?cs var:subcount(foo)?>


================================
<?cs loop:foo = 0, 3, 1?>
    <?cs set:foo.xx = foo?>
    <?cs #set:foo = 2?>
    --------loop(<?cs var:foo?>)--------
<?cs /loop?>


==============3==================
<?cs loop:foo = 0, 3, 1?>
    1.--------loop(<?cs var:foo?>)--------
<?cs /loop?>

<?cs loop:foo = 0, 3, 1?>
    <?cs set:foo = foo + 2?>
    2.--------loop(<?cs var:foo?>)--------
<?cs /loop?>

<?cs loop:foo = 0, 3, 1?>
    3.--------loop(<?cs var:foo?>)--------
    <?cs set:foo = foo + 2?>
<?cs /loop?>

============negative step==============
<?cs loop:i=-1, 0, -1?>
    negative:<?cs var:i?>
<?cs /loop?>
