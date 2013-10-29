<?cs if:qz_metadata.title.items.item.text ?>
	<?cs var:qz_metadata.title.items.item.text ?>
<?cs else ?>
	<?cs if:qz_metadata.title.item.0 ?>
		<?cs each:item = qz_metadata.title.item ?>
			<?cs if:item.url ?>
				<?cs if:qz_metadata.type == 3 ?>
					“<a class="c_tx" href="javascript:;" onclick="return QZONE.ICFeeds.Interface.checkForDress(this,'<?cs var:qz_metadata.try.uin.value ?>','<?cs var:qz_metadata.try.item.value ?>',<?cs var:qz_metadata.try.item.suitflag ?>);"><?cs var:item ?></a>”
				<?cs else ?>
					“<a class="c_tx" href="<?cs var:item.url ?>" target="_blank"><?cs var:item ?></a>”
				<?cs /if ?>
			<?cs else ?>
				<?cs var:item ?>
			<?cs /if ?>
		<?cs /each ?>
		<?cs if:qz_metadata.type == 5 || qz_metadata.type == 6?>
			<?cs var:qz_metadata.activity.text.0 ?>
		<?cs /if ?>
	<?cs else ?>
		<?cs if:qz_metadata.title.item.url ?>
				<?cs if:qz_metadata.type == 3 ?>
					“<a class="c_tx" href="javascript:;" onclick="return QZONE.ICFeeds.Interface.checkForDress(this,'<?cs var:qz_metadata.try.uin.value ?>','<?cs var:qz_metadata.try.item.value ?>',<?cs var:qz_metadata.try.item.suitflag ?>);"><?cs var:item ?></a>”
				<?cs else ?>
					“<a class="c_tx" href="<?cs var:item.url ?>" target="_blank"><?cs var:item ?></a>”
				<?cs /if ?>
		<?cs else ?>
			<?cs var:qz_metadata.title.item ?>
		<?cs /if ?>
	<?cs /if ?>
<?cs /if ?>