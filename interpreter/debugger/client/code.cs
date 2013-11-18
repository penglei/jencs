<?cs def:foo()?>
    <?cs #debugger?>
    <?cs var:title?>
    <?cs set:a = 1 + 3?>
    <?cs var:a?>
    ------ end foo -------
<?cs /def?>
-----if-------
<?cs include:"test_d_if.cs"?>
<?cs call:foo()?>

