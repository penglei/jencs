<?cs def:voice_player(data)?>

<div class="qz_feed_plugin qz_shuoshuo_audio bor2 ui_mr5"<?cs #/*前台改变这个bor2 | bg_bor6*/?>
	 data-role="MoodRecord"
	 data-sourceid="<?cs var:data.id?>"
	 data-sourceurl="<?cs var:html_encode(data.url, 1)?>"
	 data-timelen="<?cs var:data.time_len?>">
	<div class="shuoshuo_audio_inner bg_bor2" style="width:0">
		<div class="audio_icon">
			<b class="icon_play bor_bg6"></b>
			<b class="icon_pause">
				<span class="icon_pause_left bor_bg6"></span>
				<span class="icon_pause_right bor_bg6"></span>
			</b>
		</div>
		<div class="audio_time">
			<span class="c_tx2"><?cs var:data.time_len?>&quot;</span>
		</div>
	</div>
</div>

<?cs /def?>

<?cs def:contentBox-block-voice()?>
	<?cs if:subcount(qz_metadata.content_box.voice) > 0?>
	<?cs if:!qz_metadata.content_box.voice_type?>
		<?cs with:voices=qz_metadata.content_box.voice?>

		<?cs if:subcount(voices.0)?>
			<?cs each:voice=voices?>
			<div class="audio_box">
				<?cs call:voice_player(voice)?>
			</div>
			<?cs /each?>
		<?cs else ?>
			<div class="audio_box">
			<?cs call:voice_player(voices)?>
			</div>
		<?cs /if?>

		<?cs /with?>
	<?cs /if?>
	<?cs /if?>
<?cs /def?>

<?cs def:contentBox-inline-voice()?>
	<?cs if:subcount(qz_metadata.content_box.voice) > 0?>
	<?cs if:qz_metadata.content_box.voice_type == 1?><?cs #在转发原文中(行内语音说说)?>
		<?cs with:voices=qz_metadata.content_box.voice?>
		<?cs if:subcount(voices.0)?>
			<?cs each:voice=voices?>
				<?cs call:voice_player(voice)?>
			<?cs /each?>
		<?cs else ?>
			<?cs call:voice_player(voices)?>
		<?cs /if?>
		<?cs /with?>
	<?cs /if?>
	<?cs /if?>
<?cs /def?>