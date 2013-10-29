<?cs def:v8_tieban_contentBox_start(tieban)?>
	<qz:plugin name="Tieban" config="url=<?cs escape:'url'?><?cs var:html_decode(tieban.url,1) ?><?cs /escape?>&tb_id=<?cs var:html_decode(tieban.tb_id,1) ?>">
	<div class="f-ct f-ct-b-bg-np">
	<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>
	<div class="f-ct-txtimg">
<?cs /def ?>

<?cs def:v8_tieban_contentBox_end()?>
			</div><?cs #必须先闭合 .f_ct_imgtxt?>
		</div>
	</qz:plugin>
<?cs /def ?>


<li class="f-single f-s-s" id="fct_<?cs call:v8_genFeedId() ?>">
	<div class="f-aside">
		<?cs call:v8_frame_avatar()?>
		<?cs #$call:v8_frame_userinfo()?>

		<div class="f-user-info">
			<div class="f-nick">
				<?cs call:v8_feeduser_basicInfo()?>

				<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
				<?cs #TODO 有了新评论的被动不是这样展示的?>
					<?cs loop:i = 0, subcount(qfv.title.con) - 1, 1?>
						<?cs call:v8_title_item(qfv.title.con[i])?>
					<?cs /loop?>
				<?cs /if?>

			</div>
			<?cs call:v8_feed_extendinfo()?>
		</div>
		<?cs call:v8_genVipIcon() ?>

	</div><?cs #f-aside?>
	<div class="f-wrap">
		<div class="f-item f-s-i<?cs call:echo_item_class()?>" id="feed_<?cs call:v8_genFeedId() ?>" <?cs call:v8_feeds_frame_data()?> >
			<?cs if:qfv.meta.feedstype != UC_WUP_FEEDSTYPE_PSV?>
			<div class="f-info">
				<?cs loop:i = 0, subcount(qfv.title.con) - 1, 1?>
					<?cs call:v8_title_item(qfv.title.con[i])?>
				<?cs /loop?>
			</div><?cs #end: .f-info?>
			<?cs /if?>
			<div class="qz_summary wupfeed" id="hex_<?cs call:v8_genFeedId() ?>" data-clicklog="sc_summary">
				<?cs call:v8_echo_feed_data()?>

				<?cs if:subcount(qfv.content.tieban) == 0 ?>
					<?cs call:v8_contentBox("", "f-ct-imgtxt")?>
				<?cs else ?>
					<qz:plugin name="Tieban" config="url=<?cs escape:'html'?><?cs escape:'url'?><?cs var:qfv.content.tieban.url?><?cs /escape?><?cs /escape?>&tb_id=<?cs var:html_encode(qfv.content.tieban.tb_id,1) ?>">
					<div class="f-ct f-ct-b-bg-np">
					<?cs #:代替转发链的title如果有的话，应该在这里输出 ?>
						<div class="f-ct-txtimg">
							<?cs call:v8_contentMedia()?>
							<?cs call:v8_contentTxt_start("")?>
							<?cs #代替转发链的title如果有的话，应该在这里输出 ?>
							<?cs call:v8_content_genTitle(qfv.content.cntText.title.con)?>
							<?cs call:v8_conCommon(qfv.content.cntText.con)?>
							<?cs call:v8_contentTxt_end()?>
						</div><?cs #必须先闭合 .f_ct_imgtxt?>
					</div>
					</qz:plugin>
				<?cs /if?>


				<div class="f-op-wrap">
					<?cs call:v8_operate()?>
					<?cs call:v8_comments-like()?>
				</div>

			</div><?cs #end: .qz_summary?>
		</div><?cs #end: .f-item?>
	</div><?cs #end: .f-wrap?>
</li>
