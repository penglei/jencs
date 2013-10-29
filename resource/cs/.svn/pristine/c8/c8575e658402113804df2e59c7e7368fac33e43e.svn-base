<?cs #:game组件  ?>
<?cs def:game() ?>
<div class="f_ct f_ct_game">
	<div class="f_ct_imgtxt controls_<?cs var:qz_metadata.content_box.media.media.itemid ?>">
		<div class="img_box img_game" >
			<a 
				data-cmd="qz_popup" 
				href="javascript:void(0);" 
				data-version="<?cs var:qz_metadata.content_box.media.media.qz_popup.version ?>" 
				data-width="<?cs var:qz_metadata.content_box.media.media.qz_popup.width ?>" 
				data-height="<?cs var:qz_metadata.content_box.media.media.qz_popup.height ?>" 
				title="点击试玩" 
				data-src="<?cs var:qz_metadata.content_box.media.media.qz_popup.src ?>" 
				data-config="<?cs var:qz_metadata.content_box.media.media.qz_popup.config ?>" 
				class="img_gif"  
				data-type="GamingFeed"
				data-env="<?cs var:qz_metadata.content_box.media.media.qz_popup.env ?>" 
				data-param="<?cs var:qz_metadata.content_box.media.media.qz_popup.param ?>" >
					<img src="/ac/b.gif" onload="QZFL.media.adjustImageSize(120,120,restXHTML('<?cs call:htmlEncodeVar(qz_metadata.content_box.media.media.src,2,0) ?>'));" />
					<i class="ui_icon icon_game_play"></i>
			</a>
		</div>
		<div class="txt_box">
			<h4 class="txt_box_title"><?cs var:qz_metadata.content_box.content.con.text ?></h4>
			<p><?cs var:qz_metadata.content_box.content.con.text2 ?></p>
		</div>
	</div>
	<div class="player_<?cs var:qz_metadata.content_box.media.media.itemid ?>" style="display:none;">
		<div class="video_unfold bor3" style="width: 400px; height: 350px; ">
			<div class="flash_<?cs var:qz_metadata.content_box.media.media.itemid ?>"></div>
			<a 
				class="video_retract_bt bor3 bg2 c_tx" 
				data-cmd="qz_popup" 
				style="float:right" 
				title="收起" 
				href="javascript:void(0);" 
				data-param="{action:4<?cs var:string.slice(qz_metadata.content_box.media.media.qz_popup.param,9, string.length(qz_metadata.content_box.media.media.qz_popup.param)) ?>"  
				data-src="<?cs var:qz_metadata.content_box.media.media.qz_popup.src ?>" 
				data-needContainer="1" 
				data-version="4" 
				data-charset="utf-8" 
				data-width="375" 
				data-height="169" 
				data-type="GamingFeed">↑
			</a>
		</div>
		<p class="video_info"><?cs var:qz_metadata.content_box.content.con.text ?></p>
	</div>
</div>
<?cs /def ?>