<?cs #/*生日礼物*/?>

<?cs #/*---title---*/?>
<?cs call:i()?>
<?cs call:gift_birthday_timeTxt()?>
<?cs #/*call:data_title_defaultTxt(gift_birthday_timeTxt.ret + "生日，收到生日礼物：")*/?>
<?cs call:data_title_txt_style(gift_birthday_timeTxt.ret + "生日，赶紧送礼物祝他生日快乐吧！", "tip", 0)?>

<?cs #/*---quote.con---*/?>
<?cs #call:data_quote_desc(qz_metadata.orgdata.itemdata.0.desc)?>

<?cs #/*生日礼物要在内容区加一个送礼按钮*/#?>
<?cs call:data_cntmedia_pic(0, "", "giftbtntxt")?><?cs #url是空的?>

<?cs call:gift_send_birth_param()?>
<?cs call:data_popup(data_cntmedia_pic.ret + ".action",
					"送礼物",
					"http://imgcache.qq.com/qzone/gift/send_list.html",
					gift_send_birth_param.ret,
					1, 673, 431, "", "gift.content.sendgift")?>
<?cs call:data_popup_add_attr(data_cntmedia_pic.ret + ".action","className", "btn-gift1")?>

<?cs #/*---extend_info.con---*/?>
<?cs set:j = 0?>
<?cs each:avatar = qz_metadata.orgdata.itemdata?>
	<?cs call:data_con_avatar("content.extendinfo.avatar." + j, avatar.extendinfo.uSenderUin, avatar.extendinfo.strSenderUin)?>
	<?cs set j = j + 1 ?>
<?cs /each?>

<?cs #:只有一个就没有“等”字 ?>
<?cs if:qz_metadata.orgdata.extendinfo.nRevTotal > 0  ?>
	<?cs call:data_extendinfo_txt_style("","等","tip",0) ?>
<?cs /if ?>

<?cs call:data_extendinfo_url_style("", qz_metadata.orgdata.extendinfo.nRevTotal + "人" , "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/gift/list.html?type=0","link",0) ?>
<?cs if:subcount(qz_metadata.orgdata.itemdata) > 1  ?>
	<?cs call:data_extendinfo_txt_style("","都","tip",0) ?>
<?cs /if ?>
<?cs call:data_extendinfo_txt_style("", "已送礼","tip",0) ?>

<?cs #/*---operate.opr---*/?>
<?cs call:data_gift_birth_opr_popup(0, "赠送礼物")?>
<?cs call:data_opr_more()?>
<?cs call:data_opr_delfeed()?>
<?cs call:data_source()?>
<?cs call:data_oprtime()?>
<?cs #礼物主动没有评论按钮?>


<?cs #{/*评论区域*/?>

<?cs #}/*end: 评论区域*/?>
