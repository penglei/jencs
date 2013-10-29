<?cs #:music组件  ?>
<?cs def:music(mod) ?>
<?cs if:subcount(qz_metadata.music_box) > 0 ?>
	<div class="f_ct_song controls_<?cs var:qz_metadata.music_box.itemid ?>">
		<?cs set:song_img=qz_metadata.music_box.src_org?>
		<?cs if:string.length(song_img)<1?>
			<?cs set:song_img="http://music.qq.com/musicbox/img/uccpic_error.jpg"?>
		<?cs /if?>

		<?cs if:qz_metadata.music_box.url ?>
			<a href="<?cs var:qz_metadata.music_box.url?>" target=_blank>
				<img class="song_img" src="<?cs call:htmlEncodeVar(song_img,2,0) ?>">
			</a>
			<div class="song_info c_tx">
				<p class="song_name">
					<a href="<?cs var:qz_metadata.music_box.url?>" target=_blank><?cs var:qz_metadata.music_box.title ?></a>
				</p>
				<p class="song_songer"></p>
			</div>
		<?cs else ?>
			<img class="song_img" src="<?cs call:htmlEncodeVar(song_img,2,0) ?>" alt="">
			<div class="song_info c_tx">
				<p class="song_name"><?cs var:qz_metadata.music_box.title ?></p>
				<p class="song_songer"></p>
			</div>
		<?cs /if ?>

		<div class="f_ct_music">
			<a 
				data-cmd="qz_popup" 
				href="javascript:void(0)" 
				data-param="<?cs var:qz_metadata.music_box.param ?>" 
				data-version="4" 
				data-src="/music/qzone/music_ic.js" 
				data-needContainer="1" 
				data-charset="utf-8" 
				data-width="375" 
				data-height="169" 
				data-type="Music">
					<span class="music_playbt bor2">
						<span class="music_playbt_i bor2"><b class="trig c_tx"></b></span>
					</span>
			</a>
		</div>
		<i class="ui_icon icon_music_sign"></i>
	</div>
	<div class="player_<?cs var:qz_metadata.music_box.itemid ?>" style="display:none;">
		<a 
			data-cmd="qz_popup"  
			href="javascript:void(0)" 
			data-param="{action:4<?cs var:string.slice(qz_metadata.music_box.param,9, string.length(qz_metadata.music_box.param)) ?>"  
			data-src="/music/qzone/music_ic.js" 
			data-needContainer="1" 
			data-version="4" 
			data-charset="utf-8" 
			data-width="375" 
			data-height="169" 
			data-type="Music">收起</a>
		<div class="flash_<?cs var:qz_metadata.music_box.itemid ?>"></div>
	</div>
<?cs /if?>
<?cs /def ?>