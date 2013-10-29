

<?cs def:attachViewer(attach) ?>
	<?cs if:subcount(attach.attachfile) > 0 ?>
		<div class="f_file bor2">
			<div class="file_icon bg_bor2">
				<i class="ui_icon icon_type_attachment"></i>
			</div>
			<div class="file_name">
				<a href="<?cs var:get_mood_url.ret?>" target="_blank"><?cs var:attach.attachfile.name?></a>
			</div>
		</div>
	<?cs /if ?>
<?cs /def ?>