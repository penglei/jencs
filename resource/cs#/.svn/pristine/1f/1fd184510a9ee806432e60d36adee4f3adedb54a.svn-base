<?cs #:富文本内容 ?>
<?cs def:richContent(cons) ?>

	<?cs def:richContent-items(item) ?>
		<?cs if:item.newsec ?><p><?cs /if?><?cs #换行 ?>
		<?cs if:item.type == 'nick' ?>
			<?cs call:userLink(item , '@') ?>
		<?cs elif:item.type == 'url' ?>
			<a class="c_tx" href="<?cs var:item.url ?>" target="_blank"><?cs if:item.text ?><?cs var:item.text ?><?cs else ?><?cs var:item.url ?><?cs /if ?></a>
		<?cs elif:item.type == 'qz_app' ?><?cs #过时，等后台一起把它干掉 ?>
			<?cs if:string.length(item.title) > 0 ?>
			<p>
				<a class="c_tx" target="_blank" href="<?cs alt:item.url?>http://rc.qzone.qq.com/myhome/<?cs var:item.id?><?cs /alt?>"><?cs var:item.title?></a>
			</p>
			<?cs /if ?>
			<?cs if:string.length(item.text)?>
			<p><?cs var:item.text?></p>
			<?cs /if?>
			<?cs call:grade_star(item) ?>
		<?cs elif:item.type == 'btn' ?>
				<a class="f_act_bt" href="<?cs var:item.url?>" target="_blank"><?cs var:item.text?></a>
		<?cs elif:item.type == 'text' ?>
			<?cs var:item.text ?>
		<?cs else ?>
			<?cs var:item ?>
		<?cs /if ?>
		<?cs if:item.newsec ?></p><?cs /if?>
	<?cs /def ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 || string.length(cons.con.0) > 0 ?>
		<?cs loop:i = 0, subcount(cons.con) - 1, 1 ?>
			<?cs call:richContent-items(cons.con[i]) ?>
		<?cs /loop ?>
	<?cs elif:cons.con || subcount(cons.con) > 0 || string.length(cons.con) > 0 ?>
			<?cs call:richContent-items(cons.con) ?>
	<?cs /if ?>
<?cs /def ?>