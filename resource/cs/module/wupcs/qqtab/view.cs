<?cs include:"wupcs/qqtab/modview/common_marco.cs"?>
<?cs include:"wupcs/qqtab/modview/style.cs"?>

<?cs include:"wupcs/qqtab/modview/user_comp.cs"?>
<?cs include:"wupcs/qqtab/modview/textcon_comp.cs"?>
<?cs include:"wupcs/qqtab/modview/icon_comp.cs"?>
<?cs include:"wupcs/qqtab/modview/popup_comp.cs"?>

<?cs include:"wupcs/qqtab/modview/title.cs"?>
<?cs include:"wupcs/qqtab/modview/titletip.cs"?>
<?cs include:"wupcs/qqtab/modview/quote.cs"?>
<?cs include:"wupcs/qqtab/modview/extendinfo.cs"?>

<?cs include:"wupcs/qqtab/modview/contentbox_media.cs"?>
<?cs include:"wupcs/qqtab/modview/contentbox_txt.cs"?>
<?cs include:"wupcs/qqtab/modview/contentbox.cs"?>

<?cs include:"wupcs/qqtab/modview/time.cs"?>
<?cs include:"wupcs/qqtab/modview/source.cs"?>
<?cs include:"wupcs/qqtab/modview/operate.cs"?>

<?cs include:"wupcs/qqtab/modview/arrow.cs"?>
<?cs include:"wupcs/qqtab/modview/like.cs"?>
<?cs include:"wupcs/qqtab/modview/img_comp.cs"?>
<?cs include:"wupcs/qqtab/modview/comments.cs"?>
<?cs include:"wupcs/qqtab/modview/comments_like.cs"?>

<?cs include:"wupcs/qqtab/modview/summary.cs"?>
<?cs include:"wupcs/qqtab/modview/frame.cs"?>

<?cs if:appid==2?>
	<?cs include:"wupcs/qqtab/view/blog.cs"?>
<?cs elif:appid==202 ?>
	<?cs include:"wupcs/qqtab/view/share.cs"?>
<?cs elif:appid==311 ?>
	<?cs include:"wupcs/qqtab/view/mood.cs"?>
<?cs elif:appid==4?>
	<?cs include:"wupcs/qqtab/view/photo.cs"?>
<?cs /if?>