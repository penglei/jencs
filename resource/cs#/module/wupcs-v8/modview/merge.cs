<?cs set:mergeAppWording[202]="也转了此信息" ?>
<?cs set:mergeAppWording[311]="也转了此信息" ?>
<?cs set:mergeAppWording[333]="也转了此信息" ?>
<?cs if:qz_metadata.meta.appid==352 && qz_metadata.meta.typeid==3 ?>
<?cs set:mergeAppWording[352]="也添加了此应用" ?>
<?cs /if?>
<?cs def:v8_genMergeHtml(data) ?>
	<?cs if:subcount(data.mergeData)>0 && mergeAppWording[data.appid]?>
		<div class="f-source-merger update-more">
			<p class="b-line">
			<a href="javascript:void(0);">
			<?cs if:data.mergeData.nick.0 ?>
				<?cs if: data.mergeData.uin.0==qfv.meta.loginuin ?>
					我
				<?cs else ?>
					<?cs var:html_encode(data.mergeData.nick.0, 1)?>
				<?cs /if ?>
				<?cs loop:i=1, subcount(data.mergeData.uin), 1 ?>
					<?cs if: data.mergeData.uin[i]!=data.mergeData.uin.0 ?>
						<?cs if:data.mergeData.nick[i] ?>
							<?cs if: data.mergeData.uin[i]==qfv.meta.loginuin ?>
								、我
							<?cs else ?>
								、<?cs var:html_encode(data.mergeData.nick[i], 1) ?>
							<?cs /if ?>
						<?cs /if ?>
						<?cs set:i=subcount(data.mergeData.uin) ?>
					<?cs /if ?>
				<?cs /loop ?>
				<?cs if: data.mergeData.count>=2 ?>
					等<?cs var:data.mergeData.count ?>人
				<?cs /if ?>
			<?cs elif:data.mergeData.nick ?>
				<?cs if: data.mergeData.uin==qfv.meta.loginuin ?>
					我
				<?cs else ?>
					<?cs var:html_encode(data.mergeData.nick, 1) ?>
				<?cs /if ?>
			<?cs /if ?>
			<?cs var:mergeAppWording[data.appid] ?>
			</a>
			<b class="ui-trigbox ui-trigbox-b"><b class="ui-trig"></b><b class="ui-trig ui-trig-up"></b></b>
			</p>
		</div>
	<?cs /if ?>
<?cs /def ?>