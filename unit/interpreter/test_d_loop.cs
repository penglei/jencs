<?cs loop:foo = 0, 3, 1?>
    3.--------loop(<?cs var:foo?>)--------
    <?cs set:foo = foo + 2?>
<?cs /loop?>

============negative step==============
<?cs loop:i=-1, 0, -1?>
    error, this can't be rendered
<?cs /loop?>
