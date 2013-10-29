<?cs #:音乐组件 ?>

<?cs def:v8_music(music) ?>
	<?cs if:subcount(music) > 0 ?>
		<?cs if:music.count>0 ?>
			<?cs set:row2="共"+music.count+"首" ?>
		<?cs else ?>
			<?cs set:row2=music.singer ?>
		<?cs /if ?>
		<div class="controls_<?cs var:music.itemid?>">
			<?cs set:song_img=music.imgSrc?>
			<?cs if:string.length(song_img)<1?>
				<?cs set:song_img="http://music.qq.com/musicbox/img/uccpic_error.jpg"?>
			<?cs /if?>

			<div class="img-box">
				<?cs if:music.url ?>
					<a href="<?cs call:ugc_url_check(music.url,1)?>" class="with-sign" target=_blank>
						<img class="song_img" src="<?cs var:html_encode(song_img, 1)?>" 
						style="
							<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
								height:100px;
							<?cs else ?>
								height:120px;
							<?cs /if?>
						" />
						<i class="ui-ico icon-music-sign"></i>
					</a>
				<?cs else ?>
					<img class="song_img" src="<?cs var:html_encode(song_img, 1) ?>" 
					style="
						<?cs if:qfv.meta.feedstype == UC_WUP_FEEDSTYPE_PSV?>
							height:100px;
						<?cs else ?>
							height:120px;
						<?cs /if?>
					" />
				<?cs /if ?>
			</div>
			<div class="txt-box">
				<?cs if:string.length(music.title)>0 ?>
				<p class="info spacing">
					<span class="ui-b">
						<?cs if:music.url ?>
							<a href="<?cs call:ugc_url_check(music.url,1)?>" target=_blank><?cs var:html_encode(music.title, 1) ?></a>
						<?cs else ?>
							<?cs var:html_encode(music.title, 1) ?>
						<?cs /if ?>
					</span>
				</p>
				<?cs /if ?>
				<p class="fixed-bottom">
					<?cs set:music.puoup.action.className = 'fixed-btn' ?>
					<?cs call:v8_popup_start(music.puoup) ?>
						<b class="trig"></b>播放
					<?cs call:v8_popup_end()?>
				</p>
			</div>
		</div>
		<div class="f_ct_song_player player_<?cs var:music.itemid ?>" style="display:none;">
			<?cs call:v8_popup_start(music.packup) ?>收起<?cs call:v8_popup_end()?>
			<div class="flash_<?cs var:music.itemid ?>"></div>
		</div>
	<?cs /if?>
<?cs /def ?>
