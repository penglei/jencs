<?cs def:_fillAttachinfoNode(str) ?>
	<?cs set:_fillAttachinfoNode.ret=str ?>
	<?cs if:string.length(_fillAttachinfoNode.ret) < 7 ?>
		<?cs set:_fillAttachinfoNode.ret = "0"+_fillAttachinfoNode.ret ?>
		<?cs call:_fillAttachinfoNode(_fillAttachinfoNode.ret) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:attachInfo(data) ?>
	<?cs if:data.attachcount > 1 ?>
		<div class="f-reprint">
			<?cs set:_attachInfo_index = 0 ?>
			<?cs set:_attachInfo_arr = 0 ?>
			<?cs call:_fillAttachinfoNode(data.attachtype) ?>
			<?cs set:data.attachtype=_fillAttachinfoNode.ret ?>
			<span class="state ui-mr8">内含
				<?cs if:string.slice(data.attachtype,4,5) == 1 ?>
					<?cs set:_attachInfo_arr[_attachInfo_index] = '视频' ?>
					<?cs set:_attachInfo_index = _attachInfo_index + 1 ?>
				<?cs /if ?>
				<?cs if:string.slice(data.attachtype,6,7) == 1 ?>
					<?cs set:_attachInfo_arr[_attachInfo_index] = '图片' ?>
					<?cs set:_attachInfo_index = _attachInfo_index + 1 ?>
				<?cs /if ?>
				<?cs if:string.slice(data.attachtype,0,1) == 1 ?>
					<?cs set:_attachInfo_arr[_attachInfo_index] = '音乐' ?>
					<?cs set:_attachInfo_index = _attachInfo_index + 1 ?>
				<?cs /if ?>
				<?cs if:string.slice(data.attachtype,1,2) == 1 ?>
					<?cs set:_attachInfo_arr[_attachInfo_index] = '动画' ?>
					<?cs set:_attachInfo_index = _attachInfo_index + 1 ?>
				<?cs /if ?>
				<?cs if:string.slice(data.attachtype,2,3) == 1 ?>
					<?cs set:_attachInfo_arr[_attachInfo_index] = '附件' ?>
					<?cs set:_attachInfo_index = _attachInfo_index + 1 ?>
				<?cs /if ?>
				<?cs loop:i = 0, subcount(_attachInfo_arr) - 1, 1 ?>
					<?cs if:i == 0 ?>
					<?cs elif:i == subcount(_attachInfo_arr) - 1?>
						和
					<?cs else ?>、
					<?cs /if ?>
					<?cs var:_attachInfo_arr[i] ?>
				<?cs /loop ?>
			</span>
			<?cs #:动画 附件 投票 视频 网页 图片 ?>
				<?cs if:data.attachurl ?>
					<a href="<?cs call:ugc_url_check(data.attachurl,1) ?>" target="_blank">
				<?cs else ?>
					<a href="http://user.qzone.qq.com/<?cs var:qfv.meta.hostuin ?>/mood/<?cs var:qfv.meta.hostid ?>.1" target="_blank">
				<?cs /if ?>
				 查看详情
			</a>
		</div>
	<?cs /if ?>
<?cs /def ?>

<?cs def:v8_attachViewer(attach) ?>
	<?cs if:subcount(attach.attachfile) > 0 ?>
		<div class="feeds_attach_wrap">
			<qz:plugin name="Attach" config="path=<?cs escape:'url'?><?cs var:attach.attachfile.path?><?cs /escape?>&uin=<?cs escape:'url'?><?cs var:attach.attachfile.authoruin?><?cs /escape?>">
				<div class="feeds_attach bor bg3 attachProfile" title="点击查看附件">
					<div class="feeds_attach_icon rbor bg2"></div>
					<div class="feeds_attach_content "><a href="javascript:void(0);" onclick="return false;" class="c_tx attach_name attachName"><?cs call:ugc_as_html(attach.attachfile.name,1,1)?></a></div>
				</div>
			</qz:plugin>
		</div>
	<?cs /if ?>
<?cs /def ?>