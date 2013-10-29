
<?cs #:富文本内容 ?>
<?cs def:richTitle(cons) ?>
	<?cs #:i:当前项编号,length:总长度, 用来计算是否闭合标签?>
	<?cs def:richTitle-items(item, i, length) ?>
		<?cs if:item.type == 'nick' ?>
			<?cs if:qz_richtitle_flag == 1 ?>
				</span>
			<?cs /if ?>
			
			<?cs set:qz_richtitle_flag = 0 ?>
		<?cs elif:item.type == 'name' ?>
			<?cs if:qz_richtitle_flag == 1 ?>
				</span>
			<?cs /if ?>
			
			<?cs set:qz_richtitle_flag = 0 ?>		
		<?cs elif:item.type == 'url' ?>
			<?cs if:qz_richtitle_flag == 1 ?>
				</span><a class="c_tx ui_mr10" href="<?cs var:item.url ?>" target="_blank"><?cs var:item.text ?></a>
				<?cs set:qz_richtitle_flag = 0 ?>
			<?cs elif:qz_richtitle_flag == 0 ?>
				<a class="c_tx ui_mr10" href="<?cs var:item.url ?>" target="_blank"><?cs var:item.text ?></a>
			<?cs /if ?>
		<?cs elif:item.type == 'qz_popup' ?>
			<?cs if:qz_richtitle_flag == 1 ?>
				</span><span class="ui_mr10"><qz:popup 
					title="<?cs var:item.qz_popup.title ?>" 
					height="<?cs var:item.qz_popup.height ?>" 
					width="<?cs var:item.qz_popup.width ?>" 
					version="<?cs var:item.qz_popup.version ?>" 
					src="<?cs var:item.qz_popup.src ?>" 
					param="<?cs var:item.qz_popup.param ?>"><?cs var:item.text ?>
				</qz:popup></span>
				<?cs set:qz_richtitle_flag = 0 ?>
			<?cs elif:qz_richtitle_flag == 0 ?>
				<span class="ui_mr10"><qz:popup 
					title="<?cs var:item.qz_popup.title ?>" 
					height="<?cs var:item.qz_popup.height ?>" 
					width="<?cs var:item.qz_popup.width ?>" 
					version="<?cs var:item.qz_popup.version ?>" 
					src="<?cs var:item.qz_popup.src ?>" 
					param="<?cs var:item.qz_popup.param ?>"><?cs var:item.text ?>
				</qz:popup></span>
			<?cs /if ?>
		<?cs elif:item.type == 'qz_app'?>
			<?cs if:qz_richtitle_flag == 1 ?></span><?cs /if ?>
			<a target="_blank" href="http://rc.qzone.qq.com/myhome/<?cs var:item.aid?>?via=<?cs alt:item.via ?>QZ.TICKER.ISPLAYING<?cs /alt?>"><?cs var:item.text ?></a>
			<?cs if:i < subcount(cons.con) - 1 ?>、<?cs /if?>
		<?cs elif:item.type == 'auth_nick' ?>
			<?cs if:qz_richtitle_flag == 1 ?></span><?cs /if ?>
			
			<img style="vertical-align: -2px;" src="/ac/qzone_v5/client/auth_icon.png" title="腾讯认证" alt="腾讯认证" />
			<?cs set:qz_richtitle_flag = 0 ?>
		<?cs elif:item.type == 'script' ?>
			<?cs if:qz_richtitle_flag == 1 ?></span><?cs /if ?>
			<?cs if:item.biz == "mall"?>
				<a class="c_tx ui_mr10" href="javascript:;" onclick="QZONE.ICFeeds.Interface.checkForDress2(this,'<?cs var:item.param[0]?>', '<?cs var:item.param[1] ?>')">
					<?cs var:item.text ?>
				</a>
			<?cs /if ?>
			<?cs set:qz_richtitle_flag = 0 ?>
		<?cs else ?>
			<?cs if:qz_richtitle_flag == 0 ?>
				<span class="ui_mr10 c_tx3"><?cs var:item ?>
				<?cs set:qz_richtitle_flag = 1 ?>
				<?cs #: 如果是最后一项,标签要闭合哦 ?>
				<?cs if:i==length ?>
					</span>
				<?cs /if ?>
			<?cs elif:qz_richtitle_flag == 1 ?>
				<?cs var:item ?>
			<?cs /if ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 || string.length(cons.con.0) > 0 ?>
		<?cs set:qz_richtitle_flag = 0 ?>
		<?cs loop:i = 0, subcount(cons.con) - 1, 1 ?>
			<?cs call:richTitle-items(cons.con[i], i, subcount(cons.con) - 1) ?>
		<?cs /loop ?>
	<?cs elif:cons.con || subcount(cons.con) > 0 || string.length(cons.con) > 0 ?>
			<?cs set:qz_richtitle_flag = 0 ?>
			<?cs call:richTitle-items(cons.con, 0, 0) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs call:richTitle(qz_metadata.feedtitle.content) ?>