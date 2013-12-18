<?cs def:fooa(a)?>

    <?cs var:11111111?>
    <?cs #debugger?>
<?cs
/def

?>

<?cs #include:"test_d_if.cs"

?>
<?cs if:aaa?>
    aaa
    yyy
<?cs else ?>
    .....
<?cs /if?>
  
-----each--------
<?cs def:foo_if()?>
    <?cs var:333?>
    <?cs #debugger?>
<?cs /def?>
<?cs #include:"test_d_macrodef.cs"?>
-----if-------

<?cs set:a = 333?>

<?cs var:a?>

<?cs call:fooa(444)?>



<?cs var:b?>


ff
