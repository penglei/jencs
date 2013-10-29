<?cs set:NotExist = "not exist value..."?>
<?cs var:NotExist?>
<?cs set:NotExist = aaa.bd?>
<?cs var:NotExist?>
<?cs set:a = aaa + bbb + ccc?>
<?cs var:a?>

<?cs #var:x.yy.zz?>
<?cs #set:a = "attr"?>
<?cs #set:aaa[a] = 32?>
<?cs #var:aaa[a]?>
<?cs #set: z = "zz"?>
<?cs #var:x.yy.zz.pp?>
<?cs set:x["yy." + "zz"][p] = "ff111"?>
<?cs var:x.yy.zz.pp?>

<?cs #var:x.yy?>
<?cs #set:x.yy = "哈哈222"?>
<?cs #var:x.yy?>
