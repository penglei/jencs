<?cs ####
	/**
	 *生日礼物后面要加一个送礼按钮
	 */
?>
<?cs def:gift_birth_media_content_btn(picItem)?>
	<?cs call:popup_start(picItem)?>
	<?cs call:con_txt(picItem)?>
	<?cs call:popup_end()?>
<?cs /def?>

<?cs def:gift_robotbirth_entry_start() ?>
	<div class="f_ct f_ct_2 bor3 bg3 f_ct_fixed">
	<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>
	<div class="f_ct_imgtxt">
<?cs /def?>

<?cs def:gift_robotbirth_entry_end()?>
		</div><?cs #必须先闭合 .f_ct_imgtxt?>
	</div>
<?cs /def?>

<?cs def:gift_extendinfo() ?>
	<?cs if:subcount(qfv.content.extendinfo.avatar) >0 ?>
		<div class="f_guest">
			<div class="guest_ava">
			<?cs set:_end = subcount(qfv.content.extendinfo.avatar) - 1?>
			<?cs loop:j=0, _end, 1?>
				<?cs call:userAvatar_comp(qfv.content.extendinfo.avatar[j],30) ?>
			<?cs /loop?>
		</div>
		<span class="guest_text">
			<?cs call:conCommon(qfv.content.extendinfo.con)?>
		</span>
	</div>
	<?cs /if ?>

<?cs /def ?>

<?cs def:gift_contentBox_end()?>
		</div><?cs #/*必须先闭合 .f_ct_imgtxt*/?>
	<?cs if:!g_extendinfo_exist?>
		<?cs call:gift_extendinfo()?>
	<?cs /if?>
	</div>
<?cs /def?>

<?cs call:title()?>
<?cs call:summary_start()?>
	<?cs if:qfv.content.layoutMode==G_LAYOUT_LEFTIMG_V8 ?>
		<?cs call:gift_robotbirth_entry_start()?>
		<?cs call:contentMedia()?>
		<?cs call:contentTxt_start("")?>
		<?cs call:content_genTitle(qfv.content.cntText.title.con)?>
		<?cs call:conCommon(qfv.content.cntText.con)?>
		<?cs if:qfv.meta.subtype==GIFT_TYPE_GIFTROBOT || qfv.meta.giftonly_type == GIFT_subtype_birthday ||
				qfv.meta.giftonly_type == GIFT_subtype_birthday_xy ?>
			<?cs set:qfv.content.media.pic.0.action.className="send_gift bor3" ?>
			<p>
				<?cs call:popup_start(qfv.content.media.pic.0)?>
					<i class="ui_icon icon_cake"></i>赠送礼物
				<?cs call:popup_end()?>
			</p>
		<?cs /if ?>
		<?cs call:contentTxt_end()?>
		<?cs call:gift_robotbirth_entry_end()?>
		<?cs call:operate()?>
		<?cs call:comments-like()?>
	<?cs else ?>
		
		<?cs call:quote()?>
		<?cs call:contentBox_start("", "")?>
			<?cs call:contentMedia_start_imgbox()?>
			<?cs each:picItem = qfv.content.media.pic?>
				<?cs if:picItem.type == "giftbtntxt" ?>
					<?cs call:gift_birth_media_content_btn(picItem)?>
				<?cs else ?>
					<?cs call:contentMediaPic_item(picItem)?>
				<?cs /if?>
			<?cs /each?>
			<?cs call:contentMedia_end_imgbox()?>
		<?cs call:gift_contentBox_end()?>
		<?cs call:operate()?>

		<?cs #/*comments-like中包含了赞的信息,如果要单独展示赞信息而不展示评论信息，需要在数据转换层控制*/?>
		<?cs call:comments-like()?>
	<?cs /if ?>

<?cs call:summary_end()?>

