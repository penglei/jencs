<div class="feeds_tp_4">
	<div class="txtbox">
		<p>
		<?cs def:comment-items(item) ?>
			<?cs if:item.uin || subcount(item.uin) > 0 ?>
				<a href="http://user.qzone.qq.com/<?cs var:item.uin ?>" target="_blank" class="c_tx"><?cs var:item.unick ?></a>的回答：<br/><?cs var:item.content ?><br/>
			<?cs else ?>
				匿名的回答：<br/><?cs var:item.content ?><br/>
			<?cs /if ?>
	        <?cs /def ?>
		<?cs if:qz_metadata.best_answers.answer.0 || subcount(qz_metadata.best_answers.answer.0) > 0 ?>
			<?cs each:item = qz_metadata.best_answers.answer ?>
				<?cs call:comment-items(item) ?>
			<?cs /each ?>
		<?cs elif:qz_metadata.best_answers.answer || subcount(qz_metadata.best_answers.answer) > 0 ?>
			<?cs call:comment-items(qz_metadata.best_answers.answer) ?>
		<?cs /if ?>
	        
		</p>
	</div>
	<div class="feeds_tp_operate">
		<a target="_blank" href="http://rc.qzone.qq.com/myhome/<?cs var:qz_metadata.qzone_app_ww ?>/<?cs var:qz_metadata.qzone_app_ww_question ?>/<?cs var:qz_metadata.qid ?>/" class="c_tx">查看详情</a>
	</div>
</div>
