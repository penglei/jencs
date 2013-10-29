<?cs #/*内容区域下面的箭头*/?>
<?cs def:try_arrow()?>
	<?cs #/*有评论、自己赞了、别人赞了都会出现这个箭头*/?>
	<?cs set:_units=string.slice(qfv.meta.loginuin, string.length(qfv.meta.loginuin)-1, string.length(qfv.meta.loginuin))?>
	<?cs if:_units == 0 && qfv.meta.feedstype != UC_WUP_FEEDSTYPE_PSV ?><?cs #/*TODO放量V6用户赞头像，以后干掉*/?>
		<?cs set:_isAlpha=1 ?>
	<?cs else ?>
		<?cs set:_isAlpha=0?>
	<?cs /if?>
	<?cs if:subcount(qfv.comments.comment) ||
			qfv.like.isliked ||
			(qfv.like.count > 0 && !_isAlpha) || qfv.like.likeinfo > 0?>
		<?cs set:_comments_arrow_display=""?>
	<?cs else ?>
		<?cs set:_comments_arrow_display = "none"?>
	<?cs /if?>
	<div class="f_ang_t bor2 qz_comment_arrow <?cs var:_comments_arrow_display?>">
		<div class="ang_i ang_t_d bor2"></div>
		<div class="ang_i ang_t_u bor_bg"></div>
	</div>
<?cs /def?>

