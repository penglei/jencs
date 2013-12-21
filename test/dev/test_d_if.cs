<?cs if:aaa?>
    aaa
<?cs elif:bbb?>
    bbb
<?cs elif:xxx ?>
    xxx
<?cs elif:yyy ?>
    yyy
<?cs else ?>
    .....
<?cs /if?>
  
-----each--------
<?cs def:foo_if()?>
    <?cs var:333?>
    <?cs #debugger?>
<?cs /def?>
