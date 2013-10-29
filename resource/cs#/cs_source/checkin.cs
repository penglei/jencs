<?cs #:在 腾讯大厦 签到 ?>
<?cs def:checkin(mod) ?>
	<?cs if:subcount(qz_metadata.lbs) > 0 ?>
		<span class='ui_mr5 c_tx3'>在</span>
		<?cs if:subcount(qz_metadata.lbs.showmap.qz_popup) > 0 ?>
			<qz:popup 
				unrend="true" 
				version="<?cs var:qz_metadata.lbs.showmap.qz_popup.version ?>" 
				width="<?cs var:qz_metadata.lbs.showmap.qz_popup.width ?>" 
				height="<?cs var:qz_metadata.lbs.showmap.qz_popup.height ?>" 
				title="<?cs var:qz_metadata.lbs.showmap.qz_popup.title ?>" 
				src="<?cs var:qz_metadata.lbs.showmap.qz_popup.src ?>" 
				config="<?cs var:qz_metadata.lbs.showmap.qz_popup.config ?>" 
				param="<?cs var:qz_metadata.lbs.showmap.qz_popup.param ?>" 
			><a href="javascript:;" class="c_tx ui_mr5"><?cs var:qz_metadata.lbs.idname ?></a></qz:popup>
		<?cs /if ?>
<?cs /if ?>
<?cs /def ?>