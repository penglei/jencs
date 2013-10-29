<?cs call:i()?>
<?cs call:data_title_tipTxt("回复")?>

<?cs #/*具体的礼物*/?>
<?cs #放在cntText.title里面, content text title?>
<?cs if:qz_metadata.orgdata.extendinfo.gifttype != APP_GIFT?><?cs #/*title里有具体的礼物*/#?>
	<?cs call:data_textTitle_txtPopup(
				qz_metadata.orgdata.itemdata.0.name,
				"礼物卡",
				"http://drift.qzone.qq.com/cgi-bin/getgiftcard",
				"&uin=" + qz_metadata.orgdata.uin + "&type=0&arch=0&answerid=" + qz_metadata.orgdata.extendinfo.sRecordId,
				1, 625, 495 )?>
<?cs else ?>
	<?cs call:data_textTitle_url(qz_metadata.orgdata.itemdata.0.name, qz_metadata.orgdata.srcurl)?>
<?cs /if?>

<?cs #/*---quote---*/?>
<?cs #/*---content text---*/?>
<?cs call:data_content_text(qz_metadata.orgdata.itemdata.0.desc)?>


<?cs #/*---contentbox---*/?>
<?cs if:qz_metadata.orgdata.extendinfo.gifttype == PLATFORM_GIFT ||
		qz_metadata.orgdata.extendinfo.gifttype == APP_GIFT?>
	<?cs set:_imgurl = qz_metadata.orgdata.itemdata.0.picinfo.0.url?>
<?cs else ?>
	<?cs call:gift_media_url(qz_metadata.orgdata.itemdata.0)?>
	<?cs set:_imgurl = gift_media_url.ret?>
<?cs /if?>

<?cs call:i()?>

<?cs if:qz_metadata.orgdata.itemdata.0.extendinfo.nSpecialType==NSPECIALTYPE_FOR_YUYIN ?><?cs #/*语音礼物*/?>
	<?cs if:string.length(qz_metadata.orgdata.itemdata.0.extendinfo.strAudioUrl)>0  ?>
		<?cs call:data_cntmedia_pic(0, _imgurl, 8)?>
	<?cs else ?>
		<?cs call:data_cntmedia_pic(0, _imgurl, 9)?><?cs #:9代表没有语音的语音礼物 ?>
	<?cs /if ?>
	
<?cs else ?>
	<?cs call:data_cntmedia_pic(0, _imgurl, "")?>
<?cs /if?>

<?cs call:data_cntmedia_limitImgSize(data_cntmedia_pic.ret, 100, 100)?>
<?cs call:data_popup(data_cntmedia_pic.ret + ".action",
	"礼物卡",
	"http://drift.qzone.qq.com/cgi-bin/getgiftcard",
	"&uin=" + qz_metadata.orgdata.uin + "&type=0&arch=0&answerid=" + qz_metadata.orgdata.extendinfo.sRecordId,
	1, 625, 495, "", "gift.title.giftname")?><?cs #/*同title*/?>
<?cs call:i++()?>

<?cs #/*生日礼物要在内容区加一个送礼按钮*/#?>
<?cs #call:data_cntmedia_pic(i, "", "giftbtntxt")?><?cs #url是空的?>

<?cs #call:gift_resend_birth_param()?>
<?cs #call:data_popup(data_cntmedia_pic.ret + ".action",
					"回赠礼物",
					"http://imgcache.qq.com/qzone/gift/view_send.html",
					gift_resend_birth_param.ret,
					1, 625, 450, "", "gift.content.returngift")?>
<?cs #call:data_popup_add_attr(data_cntmedia_pic.ret + ".action","className", "btn-gift2")?>

<?cs #/*opr*/?>
<?cs if:qz_metadata.scope != SCOPE_FRIENDSHIP_ME_TO_FRIEND ?>
	<?cs call:data_gift_psv_oprs()?>
<?cs /if ?>

<?cs call:data_opr_comment()?>
<?cs call:data_opr_more()?>
<?cs call:data_opr_delfeed()?>
<?cs call:data_source()?>
<?cs call:data_oprtime()?>

<?cs call:data_comments(g_data_comments_default_cgi, "GB")?>
<?cs call:data_comment_replies(g_data_comments_default_cgi, "GB")?>

<?cs call:data_opcomment_item("")?>

<?cs #/*礼物没有查看更多评论*/?>
<?cs call:_gift_psv_comments_param(qz_metadata.opinfo.t2body.seq,
							qz_metadata.opinfo.t2body.uin,
							G_COMMENTS_PARAM_NORMAL_FLAG)?>

<?cs call:data_comment_replybtn("1|1|0|0|0|0|0", _gift_psv_comments_param.ret)?>

<?cs #loop:j = g_data_commentReplies_start, g_data_commentReplies_end, 1?>
<?cs each:j = g_data_comments.replies[0].index?>
	<?cs #/*生成评论的回复节点*/?>
	<?cs call:data_commentReply_loop_item(j)?>

	<?cs #{/*不支持二级评论再回复*/?>
	<?cs #/*call:_gift_psv_comments_param(qz_metadata.vt2body[i].vt3body[j].seq,
								qz_metadata.vt2body[i].vt3body[j].uin,
								G_COMMENTS_PARAM_NORMAL_FLAG)*/?>
	<?cs #/*call:data_commentReply_replybtn("1|1|0|0|0|0|0", _gift_psv_comments_param.ret)*/?>
	<?cs #}/*end*/?>

<?cs /each?>
<?cs #/loop?>


<?cs #{?>
<?cs #call:_gift_psv_comments_param(qz_metadata.vt2body.0.seq, qz_metadata.meta.opuin, G_COMMENTS_PARAM_NORMAL_FLAG)?>
<?cs #call:data_comments_inputbox("1|1|0|0|0|0|0", _gift_psv_comments_param.ret, qz_metadata.meta.opuin, "http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>
<?cs #}?>

<?cs #{/*评论框*/?>
<?cs #/*只有两个人交互，所以如果有新评论，就是第一条评论的回复*/?>
<?cs if:qz_metadata.vt2count?>
	<?cs set:_cmt_input_tuin = qz_metadata.vt2body.0.uin?>
	<?cs call:_gift_psv_comments_param(qz_metadata.vt2body.0.seq, qz_metadata.vt2body.0.uin, G_COMMENTS_PARAM_NORMAL_FLAG)?>
<?cs else ?>
	<?cs set:_cmt_input_tuin = qz_metadata.orgdata.uin?>
	<?cs call:_gift_psv_comments_param(-1, 0, G_COMMENTS_PARAM_NORMAL_FLAG)?>
<?cs /if?>

<?cs call:data_comments_inputbox("1|1|0|0|0|0|0", _gift_psv_comments_param.ret, _cmt_input_tuin, "http://w.qzone.qq.com/cgi-bin/feeds/feeds_add_comment", "GB")?>
<?cs #}/*end: 评论框*/?>

