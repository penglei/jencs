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
    <?cs call:fooa(444)?>
<?cs /def?>
<?cs #include:"test_d_macrodef.cs"?>
-----if-------

<?cs set:a = "<b>sss</b>"?>

<?cs var:a?>


<?cs call:foo_if()?>


<?cs var:b?>


ff
