<?cs def:tieban() ?>
	<qz:plugin name="Tieban" config="url=<?cs escape:'url'?><?cs var:html_decode(qfv.content.tieban.url,1) ?><?cs /escape?>&tb_id=<?cs var:html_decode(qfv.content.tieban.tb_id,1) ?>">
		<?cs call:contentBox_start(style, cls)?>
			<?cs if:qfv.content.layoutMode == G_LAYOUT_LEFTIMG?><?cs #左图右文?>
				<?cs call:contentMedia()?>
				<?cs call:contentTxt("")?>
			<?cs else ?><?cs #普通模式，都是上文下图?>
				<?cs call:contentTxt("")?>
				<?cs call:contentMedia()?>
			<?cs /if?>
		<?cs call:contentBox_end()?>
	</qz:plugin>
<?cs /def ?>