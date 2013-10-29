<?cs call:data_gift_title()?>
<?cs call:data_gift_quote()?>

<?cs def:bthgift_media_url(giftItem)?>
	<?cs set:_url = g_siDomain + "/qzone/space_item/pre/"?>
	<?cs set:_tmp = giftItem.itemid % 16 ?><?cs #必须分开写?>
	<?cs set:_url = _url + _tmp + "/"?>
	<?cs set:_url = _url + giftItem.itemid + "." + giftItem.extendinfo.strPreFormat?>
	<?cs set:bthgift_media_url.ret = _url?>
<?cs /def?>

<?cs #{/*content.media.item*/?>
<?cs def:data_bthgift_media_pic(path, giftItem)?>
	<?cs call:set(path, "type", "pic")?>
	<?cs call:bthgift_media_url(giftItem)?>
	<?cs call:set(path, "src", bthgift_media_url.ret)?>
	<?cs call:data_bthgift_popup(path, giftItem)?>
<?cs /def?>

<?cs if:subcount(qz_metadata.orgdata.itemdata.0) > 0 ?>
	<?cs set:_end = subcount(qz_metadata.orgdata.itemdata) - 1?>
	<?cs loop:i=0, _end, 1?>
		<?cs set:_path = "content.media.item." + i?>
		<?cs call:data_bthgift_media_pic(_path, qz_metadata.orgdata.itemdata[i])?>
	<?cs /loop?>
<?cs else ?>
	<?cs set:_path = "content.media.item"?>
	<?cs call:data_bthgift_media_pic(_path, qz_metadata.orgdata.itemdata)?>
<?cs /if?>
<?cs #}/*content.media.item*/?>


<?cs #{/*extend_info.con*/?>
<?cs set:_text = "共收到" + qz_metadata.orgdata.extendinfo.giftnum + "件礼物"?>
<?cs call:data_extendinfo_txt("extend_info.con", _text)?>
<?cs #}/*extend_info.con*/?>

<?cs #{/*operate.opr*/?>
<?cs call:data_con_txt("operate.opr.0", "赠送礼物", "link", 10)?>
<?cs call:data_popup("operate.opr.0.action",
				"送礼物", 
				"http://imgcache.qq.com/qzone/gift/send_list.html",
				"#param#", 1,  625, 595, "", "")?>

<?cs set:_gift_all_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/gift/list.html?type=0"?>
<?cs call:data_opr_url("operate.opr.1", "查看全部礼物", _gift_all_url,"gift.opr.chakanquanbu")?>
<?cs #}/*operate.opr*/?>

<?cs call:data_source_id()?>
<?cs call:data_oprtime()?>

<?cs #--------------comment------------?>
