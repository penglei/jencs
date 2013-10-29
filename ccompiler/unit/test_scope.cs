<?cs #set:foo = 1?>
<?cs if:foo.aa[bb].cc || bar + gon || kbp?>
	<?cs #var: foo + bar?>
    if is true  1111
<?cs /if?>

<?cs if:foo || kbp?>
	<?cs var: foo?>
    if is true
<?cs /if?>

<?cs def:target()?>
    <?cs var:str?>
<?cs /def?>

<?cs def:a(a1, a2)?>
    <?cs call:target()?><?cs #会在这里找str?>
<?cs /def?>

<?cs def:b(str)?>
    <?cs call:a(1, 2)?><?cs #a的子宏在找str，当前域是b，所以，在b中找str?>
<?cs /def?>

<?cs call:b(22)?>

