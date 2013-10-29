<?cs ####
	/*投票基本信息*/
?>
<?cs def:data_vote_base(votetype, topicid, owner, id, limit, num)?>
	<?cs call:qfv("content.vote_box.votetype", votetype) ?>
	<?cs call:qfv("content.vote_box.topicid", topicid) ?>
	<?cs call:qfv("content.vote_box.owner", owner) ?>
	<?cs call:qfv("content.vote_box.id", id) ?>
	<?cs call:qfv("content.vote_box.limit", limit) ?>
	<?cs call:qfv("content.vote_box.num", num) ?>
<?cs /def?>

<?cs ####
	/*投票图片*/
?>
<?cs def:data_vote_pic(url, jumpurl)?>
	<?cs call:qfv("content.vote_box.mainpic.url", url) ?>
	<?cs call:qfv("content.vote_box.mainpic.jumpurl", jumpurl) ?>
<?cs /def?>

<?cs ####
	/*投票选项*/
?>
<?cs def:data_vote_option(i, text)?>
	<?cs call:qfv("content.vote_box.options." + i, text) ?>
<?cs /def?>


<?cs ####
	/*投票内容区*/
?>

<?cs def:data_vote_contentbox()?>
	<?cs call:data_vote_base(qz_metadata.votedata.extendinfo.vote_type, qz_metadata.votedata.vote_topicid, qz_metadata.votedata.vote_owner, 
		qz_metadata.votedata.vote_id, qz_metadata.votedata.vote_limit, qz_metadata.votedata.vote_num) ?>
	<?cs if:qz_metadata.orgdata.itemdata[0].picinfo[0].url ?>
		<?cs call:data_vote_pic(qz_metadata.orgdata.itemdata[0].picinfo[0].url, qz_metadata.orgdata.itemdata[0].action) ?>
	<?cs /if ?>
	<?cs set:_end = subcount(qz_metadata.votedata.vote_option) - 1 ?>
	<?cs loop:i=0, _end, 1?>
		<?cs call:data_vote_option(i, qz_metadata.votedata.vote_option[i]) ?>
	<?cs /loop?>
<?cs /def?>