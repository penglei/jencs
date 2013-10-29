<?cs #:音乐组件 ?>

<?cs set:data_music_path="content.media.music" ?>

<?cs def:data_music_popup(param) ?>
	<?cs call:data_popup(data_music_path + ".puoup.action", "", "/music/qzone/music_ic.js", param, 4, 375, 169, "Music", "") ?>
<?cs /def ?>

<?cs def:data_music_packup_btn(param) ?>
	<?cs call:data_popup(data_music_path + ".packup.action", "", "/music/qzone/music_ic.js", "{action:4"+string.slice(param,9, string.length(param)), 4, 375, 169, "Music", "") ?>
<?cs /def ?>

<?cs def:data_music_count(count) ?>
	<?cs call:set(data_music_path, "count", count) ?>
<?cs /def ?>

<?cs def:data_music_itemid(itemid) ?>
	<?cs call:set(data_music_path, "itemid", itemid) ?>
<?cs /def ?>

<?cs def:data_music_url(url) ?>
	<?cs call:set(data_music_path, "url", url) ?>
<?cs /def ?>

<?cs def:data_music_image(imgSrc) ?>
	<?cs call:set(data_music_path, "imgSrc", imgSrc) ?>
<?cs /def ?>

<?cs def:data_music_time(musictime) ?>
	<?cs call:set(data_music_path, "musictime", musictime) ?>
<?cs /def ?>

<?cs #:
	/**/
	function data_music_title(title){}
?>
<?cs def:data_music_title(title) ?>
	<?cs call:set(data_music_path, "title", title) ?>
<?cs /def ?>

<?cs def:data_music(title, itemid, count, url, imgSrc, musictime, popupParam, packupParam) ?>
	<?cs call:qfv("content", 1)?><?cs #在内容区展现，因此置1?>
	<?cs call:data_music_popup(popupParam) ?>
	<?cs call:data_music_packup_btn(popupParam) ?>
	<?cs call:data_music_count(count) ?>
	<?cs call:data_music_itemid(itemid) ?>
	<?cs call:data_music_url(url) ?>
	<?cs call:data_music_image(imgSrc) ?>
	<?cs call:data_music_title(title) ?>
	<?cs call:data_music_time(musictime) ?>
<?cs /def ?>

<?cs def:data_music_attr(name, value)?>
	<?cs call:set(data_music_path, name, value)?>
<?cs /def?>
