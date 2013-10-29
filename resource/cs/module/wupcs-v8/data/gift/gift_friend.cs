<?cs call:i()?>
<?cs call:gift_birthday_timeTxt()?>
<?cs call:data_title_defaultTxt(gift_birthday_timeTxt.ret + "生日，快送上祝福吧")?>

<?cs #/*---operate.opr---*/?>
<?cs call:i()?>
<?cs call:data_opr_more()?>
<?cs call:data_opr_delfeed()?>
<?cs call:data_gift_birth_opr_popup(0, "赠送礼物")?>
<?cs call:data_opr_url(1, "查看全部礼物", "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/gift/list.html?type=0","gift.opr.chakanquanbu")?>
