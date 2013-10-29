<?cs def:macro_param+one(p1, p2, p3, p4)?>
    <?cs def:inline_1(in1, in2,  in3)?>
        <?cs var:in1.zz?>
        in1:<?cs var:in1.ww?>
        in2:<?cs var:in2.ww?>
    <?cs /def?>
    <?cs def:inline_2(in1, in2,  in3)?>
        macro:inline_2
        <?cs var:in1.zz?>
        <?cs var:in1.ww?>
        <?cs set:in1.aaa = "aaa"?>
        <?cs set:xxff = in1.zz?>
        <?cs set:in2.ff = "in-2.ff"?>
        <?cs var:in2.ff ?>
    <?cs /def?>

    <?cs set:tempW = "w"?>
    <?cs set:p1["w" + tempW] = "wwwwwww"?>
    <?cs var:p2?>
    <?cs var:p3?>
    <?cs call:inline_1(p1, p2, p3)?>
    <?cs set:p2.ww = "WWWWWWW"?>
    <?cs call:inline_1(p1, p2, p3)?>
    ----
    <?cs var:p4?>
    <?cs var:p4.error?>
    ----
    <?cs call:inline_2(p4["error"], p2, p3)?>
    <?cs var:p4["error"] + p4?>
    <?cs var:(p4 + "FFF")?>
    <?cs var:p4["error"] + (p4 + "FFF")?>
    <?cs var:1 + p4["error"]?>
<?cs /def?>

<?cs set:x = 'y'?>
<?cs set:literal = "literal's" + x + 2?>
<?cs call:macro_param+one(x[x + 'y'], x, foo.bar.NotExist, literal + "GGG")?>
<?cs call:macro_param+one(x, x, NotExist, literal + "GGG")?>
<?cs call:macro_param+one(x, x, NotExist, "PPP" + "GGG")?>
<?cs call:macro_param+one(x, x, NotExist, "GGG")?>
<?cs call:macro_param+one(x, x, NotExist, "GGG")?>
