<?cs def:qzData() ?>
	<?cs #:key1是节点key key2是源节点key  gdt_key 是广点通?>
	<?cs if:subcount(qz_metadata.qz_data) > 0 || qz_metadata.gdt_url ?>
		<qz:data 
			<?cs if:qz_metadata.qz_data.version==1 ?>
				version="<?cs var:qz_metadata.qz_data.version ?>" 
			<?cs /if ?>
			<?cs if:qz_metadata.qz_data.key1 ?>
				key1="<?cs var:qz_metadata.qz_data.key1 ?>" 
			<?cs /if ?>
			<?cs if:qz_metadata.qz_data.key2 ?>
				key2="<?cs var:qz_metadata.qz_data.key2 ?>" 
			<?cs /if ?>
			<?cs if:qz_metadata.metadata.gdt_url ?>
				gdt_key="<?cs var:qz_metadata.metadata.gdt_url ?>" 
			<?cs /if ?>
			/>
	<?cs /if ?>
<?cs /def ?>