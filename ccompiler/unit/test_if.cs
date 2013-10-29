<?cs if:bar == 1?>
bar111111
<?cs elif:bar == foo?>
bar222222
<?cs elif:bar == 3?>
bar333333
<?cs else ?>
bar444444
<?cs /if?>

<?cs if: gar ?>
    <?cs var:gar?>
<?cs /if?>

<?cs def:macro_param+one(p1, p2, p3, p4)?>
<?cs /def?>

<?cs set:foo = "aaa"?>
<?cs set:bar = "baa"?>
<?cs if:foo != bar?>
    <?cs var: foo + bar?>
    <?cs if:foo != bar?>
        <?cs #var: foo + bar?>
    <?cs /if?>
<?cs /if?>

<?cs #var: foo + 2?>
<?cs #var: foo + "come here"?>
