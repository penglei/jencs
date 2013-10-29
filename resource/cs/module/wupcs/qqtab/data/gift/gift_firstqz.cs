<?cs #/*开通空间特殊礼物*/#?>
<?cs call:i()?>
<?cs call:data_title_defaultTxt("你收到黄钻官方团队寄来的一封信")?>

<?cs call:data_cntmedia_pic_urlaction(0,
					g_siDomain + "/qzone_v6/img/feed/act_img/vip_look_more.png",
					"http://vip.qzone.com/priv_all", "", "")?>

<?cs call:data_opr_url(0, "开通黄钻", "http://pay.qq.com/qzone/index.shtml?aid=feeds.open","gift.opr.getVip")?>
<?cs call:data_oprtime()?>
<?cs call:data_opr_more()?>
<?cs call:data_opr_delfeed()?>