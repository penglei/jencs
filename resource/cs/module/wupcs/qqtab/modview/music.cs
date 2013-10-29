<?cs #:音乐组件 ?>

<?cs def:music(music) ?>
	<?cs if:subcount(music) > 0 ?>
		<div class="f_file bor2">
			<div class="file_icon bg_bor2">
				<i class="ui_icon icon_type_music"></i>
			</div>
			<div class="file_name">
				<a href="<?cs alt:music.url?><?cs var:get_mood_url.ret?><?cs /alt?>" target="_blank"><?cs var:html_encode(music.title, 1)?></a>
			</div>
		</div>
	<?cs /if?>
<?cs /def ?>
