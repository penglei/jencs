<?cs include:"wupcs-v8/modview/ugclib.cs"?>
<?cs include:"wupcs-v8/modata/common.cs"?>

<?cs include:"wupcs-v8/modata/textcon.cs"?>
<?cs include:"wupcs-v8/modata/richcontent.cs"?>
<?cs include:"wupcs-v8/modata/popup.cs"?>

<?cs include:"wupcs-v8/modata/global.cs"?>

<?cs include:"wupcs-v8/modata/title.cs"?>
<?cs include:"wupcs-v8/modata/quote.cs"?>
<?cs include:"wupcs-v8/modata/content.cs"?>
<?cs include:"wupcs-v8/modata/tieban.cs" ?>
<?cs include:"wupcs-v8/modata/time.cs"?>

<?cs include:"wupcs-v8/modata/extendinfo.cs"?>
<?cs include:"wupcs-v8/modata/source.cs"?>
<?cs include:"wupcs-v8/modata/comments_const.cs"?>
<?cs #/*有些常量在operate和comment都会用到*/?>
<?cs include:"wupcs-v8/modata/operate.cs"?>
<?cs include:"wupcs-v8/modata/comments.cs"?>
<?cs include:"wupcs-v8/modata/like.cs"?>
<?cs include:"wupcs-v8/modata/music.cs" ?>
<?cs include:"wupcs-v8/modata/attachmentPresenter.cs" ?>
<?cs include:"wupcs-v8/modata/attachViewer.cs" ?>
<?cs include:"wupcs-v8/modata/voice.cs" ?>

<?cs #--------route----------#?>


<?cs if:appid == 333?>
	<?cs include:"wupcs-v8/data/gift.cs"?>
<?cs elif:appid == 2?>
	<?cs include:"wupcs-v8/data/blog.cs"?>
<?cs elif:appid == 202?>
	<?cs include:"wupcs-v8/data/share.cs"?>
<?cs elif:appid == 217 ?>
	<?cs include:"wupcs-v8/data/follow.cs"?>
<?cs elif:appid == 311 || appid == 6100?>
	<?cs include:"wupcs-v8/data/mood.cs"?>
<?cs elif:appid == 4 ?>
	<?cs include:"wupcs-v8/data/photo.cs"?>
<?cs elif:appid == 334 ?>
	<?cs include:"wupcs-v8/data/msgboard.cs"?>
<?cs elif:appid == 352 ?>
	<?cs include:"wupcs-v8/data/app.cs"?>
<?cs elif:appid == 7005 ?>
	<?cs include:"wupcs-v8/data/shake.cs"?>
<?cs elif:appid == 340 ?>
	<?cs include:"wupcs-v8/data/shangcheng.cs"?>
<?cs elif:appid == 201 ?>
	<?cs include:"wupcs-v8/data/vote.cs"?>
<?cs elif:appid == 422 ?>
	<?cs include:"wupcs-v8/data/upp.cs"?>
<?cs /if?>
