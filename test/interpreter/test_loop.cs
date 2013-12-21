================================
<?cs set:foo = 1?>
<?cs set:foo.a = 111111?>
<?cs set:foo.b = 222222?>
<?cs set:foo.c = 333333?>
<?cs var:subcount(foo)?>
<?cs loop:foo = 0, subcount(foo), foo?>
    <?cs set:foo[foo] = foo?>
    --------loop(<?cs var:foo?>)--------
    <?cs #var:subcount(foo)?>
<?cs /loop?>

<?cs var:subcount(foo)?>

==============step times test==============
<?cs loop:foo = 0, 3, 1?>
    1.--------loop(<?cs var:foo?>)--------
<?cs /loop?>

<?cs loop:foo = 0, 3, 1?>
    <?cs set:foo = foo + 2?>
    2.--------loop(<?cs var:foo?>)--------
<?cs /loop?>

<?cs loop:foo = 0, 3, 1?>
    3.--------loop(<?cs var:foo?>)--------
    <?cs #set:foo = foo + 2?><?cs #这是一个歧义语法，不做测试?>
<?cs /loop?>

============negative step==============
<?cs loop:i=-1, 0, -1?>
    error, this can't be rendered
<?cs /loop?>
