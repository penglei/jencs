<?cs def:v8_comments-like()?>
	<?cs ####
		/**
		 * 操作区和评论、赞之间的虚线分隔
		 * 如果没有赞和评论，就不展示这虚线
		 */
	?>
	<?cs #def:v8_operate_separater()?>
		<?cs if:subcount(qfv.comments.comment) || qfv.like.isliked || qfv.like.likeinfo > 0?>
			<div class="f-ang-t"></div>
		<?cs /if?>
	<?cs #/def?>

	<?cs #<div class="feeds_comment_v2">?>
		<?cs call:v8_likeinfo()?>
		<?cs call:v8_comments()?>
	<?cs #</div>?>
<?cs /def?>
