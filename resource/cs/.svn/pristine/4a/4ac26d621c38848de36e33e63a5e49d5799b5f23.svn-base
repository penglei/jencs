<?cs def:getActionTitleName() ?>
	<?cs set:getActionTitleName.ret = 'actiontitle' ?>
	<?cs if:subcount(qz_metadata.actiontitle_2) > 0 ?>
		<?cs set:getActionTitleName.ret = 'actiontitle_2' ?>
	<?cs /if ?>
<?cs /def ?>
<?cs def:getTitleContentNodeName() ?>
	<?cs set:getContentNodeName.ret="content" ?>
	<?cs if: qz_metadata.qz_data.feedtype!=1 && subcount(qz_metadata.metadata.lastmsg)&&qz_metadata.metadata.lastmsg.visiable==1 ?>
		<?cs set:getContentNodeName.ret="lastmsg" ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:富文本内容 ?>
<?cs def:richContent-title(cons) ?>
	<?cs def:richContentTitle-items(item) ?>
		<?cs if:item.type == 'nick' ?>
			<?cs call:userLink(item , '@') ?>
		<?cs elif:item.type == 'url' ?>
			<a class="c_tx ui_mr5 " href="<?cs var:item.url ?>" target="_blank"><?cs if:item.text ?><?cs var:item.text ?><?cs else ?><?cs var:item.url ?><?cs /if ?></a>
		<?cs elif:item.type == 'qz_app' ?>
			<?cs var:item.text ?>
		<?cs else ?>
			<?cs var:item ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 || string.length(cons.con.0) > 0 ?>
		<?cs set:_actionNo = qz_metadata[getActionTitleName.ret].action.no ?>
		<?cs set:_continueflag = 1 ?>
		<?cs loop:i = 0, subcount(cons.con) - 1, 1 ?>
			<?cs #:转发和分享的主动feeds中，title正文不展示转发链 ?>
			<?cs if: _continueflag && (_actionNo==4||_actionNo==5) && cons.con[i]==" || " ?>
				<?cs set:_continueflag = 0 ?>
			<?cs /if ?>
			<?cs if: _continueflag ?>
				<?cs call:richContentTitle-items(cons.con[i]) ?>
			<?cs /if ?>
		<?cs /loop ?>
	<?cs elif:cons.con || subcount(cons.con) > 0 || string.length(cons.con) > 0 ?>
			<?cs call:richContentTitle-items(cons.con) ?>
	<?cs /if ?>
<?cs /def ?>