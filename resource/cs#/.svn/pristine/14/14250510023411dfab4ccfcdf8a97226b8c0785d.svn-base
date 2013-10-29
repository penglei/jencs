<?cs def:locationInfo(mod) ?>
<?cs if:subcount(qz_metadata.lbs) > 0 ?>
	<p class="f_reprint c_tx3">
		<?cs if:qz_metadata.lbs.idname ?>
			<span class='ui_mr10'><?cs var:qz_metadata.lbs.idname ?></span>
		<?cs else if:qz_metadata.lbs.name ?>
			<span class='ui_mr10'><?cs var:qz_metadata.lbs.name ?></span>
		<?cs /if ?>
		<?cs if:subcount(qz_metadata.lbs.showmap.qz_popup) > 0 ?>
			<a 
				data-cmd="qz_popup" 
				href="javascript:void(0)" 
				data-version="<?cs var:qz_metadata.lbs.showmap.qz_popup.version ?>" 
				data-width="<?cs var:qz_metadata.lbs.showmap.qz_popup.width ?>" 
				data-height="<?cs var:qz_metadata.lbs.showmap.qz_popup.height ?>" 
				data-title="<?cs var:qz_metadata.lbs.showmap.qz_popup.title ?>" 
				data-src="<?cs var:qz_metadata.lbs.showmap.qz_popup.src ?>" 
				data-config="<?cs var:qz_metadata.lbs.showmap.qz_popup.config ?>" 
				class="c_tx ui_mr10" 
				data-param="<?cs var:qz_metadata.lbs.showmap.qz_popup.param ?>" 
			>查看地图</a>
		<?cs /if ?>
	</p>
<?cs /if ?>
<?cs /def ?>