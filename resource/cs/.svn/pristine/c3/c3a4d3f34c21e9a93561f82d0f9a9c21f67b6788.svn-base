<?cs #:附件 ?>
<?cs def:attachViewer(mod) ?>
	<?cs if:subcount(qz_metadata.attach.attachfile) > 0 ?>
		<div class="feeds_attach_wrap">
			<qz:plugin name="Attach" config="path=<?cs escape:'url'?><?cs var:qz_metadata.attach.attachfile.path?><?cs /escape?>&uin=<?cs escape:'url'?><?cs var:qz_metadata.attach.attachfile.owner?><?cs /escape?>">
				<div class="feeds_attach bor bg3 attachProfile" title="点击查看附件">
					<div class="feeds_attach_icon rbor bg2"></div>
					<div class="feeds_attach_content "><a href="javascript:void(0);" onclick="return false;" class="c_tx attach_name attachName"><?cs var:qz_metadata.attach.attachfile.name?></a></div>
				</div>
			</qz:plugin>
		</div>
	<?cs /if ?>
<?cs /def ?>