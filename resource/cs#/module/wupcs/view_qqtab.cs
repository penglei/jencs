<?cs include:"wupcs/modview-qqtab/common_marco.cs"?>
<?cs include:"wupcs/modview-qqtab/style.cs"?>

<?cs include:"wupcs/modview-qqtab/user_comp.cs"?>
<?cs include:"wupcs/modview-qqtab/textcon_comp.cs"?>
<?cs include:"wupcs/modview-qqtab/icon_comp.cs"?>
<?cs include:"wupcs/modview-qqtab/popup_comp.cs"?>

<?cs include:"wupcs/modview-qqtab/title.cs"?>

<?cs include:"wupcs/modview-qqtab/quote.cs"?>
<?cs include:"wupcs/modview-qqtab/extendinfo.cs"?>

<?cs include:"wupcs/modview-qqtab/contentbox_media.cs"?>
<?cs include:"wupcs/modview-qqtab/contentbox_txt.cs"?>
<?cs include:"wupcs/modview-qqtab/contentbox.cs"?>

<?cs include:"wupcs/modview-qqtab/time.cs"?>
<?cs include:"wupcs/modview-qqtab/source.cs"?>
<?cs include:"wupcs/modview-qqtab/operate.cs"?>

<?cs include:"wupcs/modview-qqtab/arrow.cs"?>
<?cs include:"wupcs/modview-qqtab/like.cs"?>
<?cs include:"wupcs/modview-qqtab/img_comp.cs"?>
<?cs include:"wupcs/modview-qqtab/comments.cs"?>
<?cs include:"wupcs/modview-qqtab/comments_like.cs"?>

<?cs include:"wupcs/modview-qqtab/summary.cs"?>

<?cs if:appid == 2?>
	<?cs include:"wupcs/view-qqtab/blog.cs"?>
<?cs elif:appid == 202?>
	<?cs include:"wupcs/view-qqtab/share.cs"?>
<?cs elif:appid == 311?>
	<?cs include:"wupcs/view-qqtab/mood.cs"?>
<?cs elif:appid == 4?>
	<?cs include:"wupcs/view-qqtab/photo.cs"?>
<?cs /if?>
