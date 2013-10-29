<?cs def:comments-like()?>

<div class="feeds_comment_v2">
	<?cs if: qfv.like.likeinfo>0 || qfv.comments.comment.0.user.uin ||subcount(qfv.comments.inputbox)>0?>
		<?cs #有赞信息或者至少有一条评论（有评论的时候这个uin必然不为空）或者有评论框出现 ?>
		<?cs call:try_arrow()?>
	<?cs /if ?>
	<?cs call:likeinfo()?>
	<?cs call:comments()?>
</div>
<?cs /def?>
