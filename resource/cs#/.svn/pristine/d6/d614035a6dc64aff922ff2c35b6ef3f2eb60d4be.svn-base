<?cs #:音乐组件 ?>

<?cs def:music(music) ?>
	<?cs if:subcount(music) > 0 ?>
		<?cs if:music.count>0 ?>
			<?cs set:row2="共"+music.count+"首" ?>
		<?cs else ?>
			<?cs set:row2=music.singer ?>
		<?cs /if ?>
		<div class="f_ct_song controls_<?cs var:music.itemid ?>">
			<?cs set:song_img=music.imgSrc?>
			<?cs if:string.length(song_img)<1?>
				<?cs set:song_img="http://music.qq.com/musicbox/img/uccpic_error.jpg"?>
			<?cs /if?>
			<?cs if:music.url ?>
				<a href="<?cs call:ugc_url_check(music.url,1)?>" target=_blank>
					<img class="song_img" src="<?cs call:htmlEncodeVar(song_img, 2, 0) ?>">
				</a>
				<div class="song_info c_tx">
					<p class="song_name">
						<a href="<?cs call:ugc_url_check(music.url,1)?>" target=_blank><?cs var:html_encode(music.title, 1) ?></a>
					</p>
				</div>
			<?cs else ?>
				<img class="song_img" src="<?cs call:htmlEncodeVar(song_img, 2, 0) ?>" alt="">
				<div class="song_info c_tx">
					<p class="song_name"><?cs var:html_encode(music.title, 1) ?></p>
				</div>
			<?cs /if ?>
			<div class="f_ct_music">
				<?cs call:popup_start(music.puoup) ?>
					<span class="music_playbt bor2">
						<span class="music_playbt_i bor2"><b class="trig c_tx"></b><i class="c_tx">播放</i></span>
					</span>
				<?cs call:popup_end()?>
			</div>
			<i class="ui_icon icon_music_sign"></i>
		</div>
		<div class="f_ct_song_player player_<?cs var:music.itemid ?>" style="display:none;">
			<?cs call:popup_start(music.packup) ?>收起<?cs call:popup_end()?>
			<div class="flash_<?cs var:music.itemid ?>"></div>
		</div>
	<?cs /if?>
<?cs /def ?>
