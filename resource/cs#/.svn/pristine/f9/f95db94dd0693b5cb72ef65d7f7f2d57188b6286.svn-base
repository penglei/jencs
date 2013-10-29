<?cs if:qz_metadata.title.item ?>
	<?cs var:qz_metadata.title.item?>
<?cs else ?>
	<?cs if:qz_metadata.title.item.0 ?>
		<?cs each:item = qz_metadata.title.item ?>
			<?cs if:item.url ?>
					<a class="c_tx" href="<?cs var:item.url ?>" target="_blank"><?cs var:item ?></a>
			<?cs else ?>
				<?cs var:item ?>
			<?cs /if ?>
		<?cs /each ?>
	<?cs /if ?>
<?cs /if ?>