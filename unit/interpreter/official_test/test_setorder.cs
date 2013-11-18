<?cs #证实set是没有顺序的?>
<?cs set:foo.3 = 3333?>
<?cs set:foo.1 = 1111?>
<?cs set:foo.2 = 2222?>
<?cs set:i = 0?>

--------------------

<?cs each:item = foo?>
    <?cs var:item?>
<?cs /each?>
