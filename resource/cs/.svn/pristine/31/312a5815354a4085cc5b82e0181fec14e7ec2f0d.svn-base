<?cs #:@人被动feeds模板 ?>

<?cs #:广播显示 ?>
<?cs def:richContent(cons,from) ?>
	<?cs def:richContent-item(item) ?>
		<?cs if:item.type == 'wbnick' ?>
			<a href="http://rc.qzone.qq.com/myhome/weibo/profile/<?cs var:item.wbaccount ?>/" target="_blank" class="q_namecard q_des c_tx" title="<?cs var:item.wbnick ?>(@<?cs var:item.wbaccount ?>)"><?cs var:item.wbnick ?></a>
		<?cs elif:item.type == 'topic' ?>
			<a href="http://rc.qzone.qq.com/myhome/weibo/topic/<?cs var:item.topic ?>/" target="_blank" class="c_tx">#<?cs var:item.topic ?>#</a>
		<?cs elif:item.type == 'url' ?>
			<a href="<?cs var:item.url ?>?type=1&from=19&f=2&s=<?cs var:from ?>" target="_blank" class="c_tx"><?cs var:item.url ?></a>
		<?cs else ?>
			<?cs var:item ?>
		<?cs /if ?>
	<?cs /def ?>
	<?cs if:cons.con.0 || subcount(cons.con.0) > 0 ?>
		<?cs each:item = cons.con ?>
			<?cs call:richContent-item(item) ?>
		<?cs /each ?>
	<?cs elif:string.length(cons.con) > 0 || subcount(cons.con) > 0 ?>
		<?cs call:richContent-item(cons.con) ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:主函数 ?>
<?cs def:main() ?>
	<?cs if:subcount(qz_metadata.content) > 0 ?>
		在微博：
		<strong class="quotes_symbols_left c_tx3">“</strong><?cs call:richContent(qz_metadata.content,qz_metadata.wbfrom) ?><strong class="quotes_symbols_right c_tx3">”</strong>
	<?cs /if ?>
	提到我
<?cs /def ?>

<?cs call:main() ?>