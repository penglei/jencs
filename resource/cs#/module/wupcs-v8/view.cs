<?cs include:"wupcs-v8/modview/feed_inner_data.cs"?>

<?cs include:"wupcs-v8/modview/common_marco.cs"?>
<?cs include:"wupcs-v8/modview/style.cs"?>

<?cs include:"wupcs-v8/modview/user_comp.cs"?>
<?cs include:"wupcs-v8/modview/textcon_comp.cs"?>
<?cs include:"wupcs-v8/modview/icon_comp.cs"?>
<?cs include:"wupcs-v8/modview/popup_comp.cs"?>

<?cs include:"wupcs-v8/modview/time.cs"?>
<?cs include:"wupcs-v8/modview/lbs.cs" ?>
<?cs include:"wupcs-v8/modview/source.cs"?>
<?cs include:"wupcs-v8/modview/visitor.cs"?>

<?cs include:"wupcs-v8/modview/frame.cs"?>

<?cs include:"wupcs-v8/modview/title.cs"?>

<?cs include:"wupcs-v8/modview/quote.cs"?>
<?cs include:"wupcs-v8/modview/extendinfo.cs"?>

<?cs include:"wupcs-v8/modview/contentbox_media.cs"?>
<?cs include:"wupcs-v8/modview/contentbox_txt.cs"?>
<?cs include:"wupcs-v8/modview/contentbox.cs"?>

<?cs include:"wupcs-v8/modview/operate.cs"?>
<?cs include:"wupcs-v8/modview/merge.cs"?>

<?cs include:"wupcs-v8/modview/arrow.cs"?>
<?cs include:"wupcs-v8/modview/like.cs"?>
<?cs include:"wupcs-v8/modview/img_comp.cs"?>
<?cs include:"wupcs-v8/modview/comments.cs"?>
<?cs include:"wupcs-v8/modview/comments_like.cs"?>

<?cs include:"wupcs-v8/modview/summary.cs"?>

<?cs if:appid == 333?>
	<?cs include:"wupcs-v8/view/gift.cs"?>
<?cs elif:appid == 2?>
	<?cs include:"wupcs-v8/view/blog.cs"?>
<?cs elif:appid == 202?>
	<?cs include:"wupcs-v8/view/share.cs"?>
<?cs elif:appid == 217?>
	<?cs include:"wupcs-v8/view/follow.cs"?>
<?cs elif:appid == 311 || appid == 6100 ?>
	<?cs include:"wupcs-v8/view/mood.cs"?>
<?cs elif:appid == 4 || appid == 422?>
	<?cs include:"wupcs-v8/view/photo.cs"?>
<?cs elif:appid == 334?>
	<?cs include:"wupcs-v8/view/msgboard.cs"?>
<?cs elif:appid == 352?>
	<?cs include:"wupcs-v8/view/app.cs"?>
<?cs elif:appid == 7005?>
	<?cs include:"wupcs-v8/view/shake.cs"?>
<?cs elif:appid == 340?>
	<?cs include:"wupcs-v8/view/shangcheng.cs"?>
<?cs elif:appid == 201?>
	<?cs include:"wupcs-v8/view/vote.cs"?>
<?cs /if?>
