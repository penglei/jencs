<?cs include:"wupcs/data/gift/const.cs"?>
<?cs include:"wupcs/data/gift/common.cs"?>
<?cs include:"wupcs/data/gift/gift_others.cs"?><?cs #/*一些特殊的feeds*/#?>

<?cs with:giftType = qz_metadata.orgdata.subtype?>
<?cs if:giftType == GIFT_TYPE_GIFTROBOT?>
	<?cs call:data_gift_robotbirth_entry()?>
<?cs elif:giftType == GIFT_ASKFOR_YELLOW?>
	<?cs call:data_gift_askyellow_entry()?>
<?cs elif:giftType == GIFT_EXPIRE_YELLOW?>
	<?cs call:data_gift_expireyellow_entry()?>
<?cs else ?><?cs #/*主要的礼物逻辑*/#?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT?>
		<?cs #/*主动Feeds*/?>
		<?cs with:giftSubType = qz_metadata.orgdata.extendinfo.gift_subtype?>
		<?cs if:giftSubType == GIFT_subtype_birthday ||
				giftSubType == GIFT_subtype_birthday_xy?>
			<?cs set:qfv.meta.giftonly_type=qz_metadata.orgdata.extendinfo.gift_subtype ?>
			<?cs #/*!记住，这里只有主动，下一个分支相同*/?>
			<?cs include:"wupcs/data/gift/gift_birthday.cs"?>
		<?cs elif:giftSubType == GIFT_subtype_action ||
				  giftSubType == GIFT_subtype_action_xy?>
			<?cs include:"wupcs/data/gift/gift_event.cs"?>
		<?cs else ?>
			<?cs include:"wupcs/data/gift/gift_friend.cs"?><?cs #/*其它主动*/?>
		<?cs /if?>
		<?cs /with?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_GIFT_ACTION ?>
		<?cs #/*一种特别奇怪的主动feeds*/?>
		<?cs include:"wupcs/data/gift/gift_event.cs"?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV?>
		<?cs if:qz_metadata.orgdata.extendinfo.GiftSenderUin == qz_metadata.meta.opuin || qz_metadata.orgdata.extendinfo.GiftReceiverUin == qz_metadata.meta.opuin ?>
			<?cs include:"wupcs/data/gift/gift_comm_psv.cs"?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("评论")?>
			<?cs include:"wupcs/data/gift/gift_birthday.cs"?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?>
		<?cs if:qz_metadata.orgdata.extendinfo.GiftSenderUin == qz_metadata.meta.opuin || qz_metadata.orgdata.extendinfo.GiftReceiverUin == qz_metadata.meta.opuin ?>
			<?cs include:"wupcs/data/gift/gift_reply_psv.cs"?>
		<?cs else ?>
			<?cs call:data_title_tipTxt("回复")?>
			<?cs include:"wupcs/data/gift/gift_birthday.cs"?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_QZVIP_INVITE?>
		<?cs include:"wupcs/data/gift/gift_firstqz.cs"?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV?>
		<?cs call:data_title_tipTxt("提到我")?>
		<?cs include:"wupcs/data/gift/gift_birthday.cs"?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_COM?>
		<?cs call:data_title_tipTxt("在评论中提到我")?>
		<?cs include:"wupcs/data/gift/gift_birthday.cs"?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV_BY_REPLY?>
		<?cs call:data_title_tipTxt("在回复中提到我")?>
		<?cs include:"wupcs/data/gift/gift_birthday.cs"?>
	<?cs /if?>
<?cs /if?>
<?cs /with?>
