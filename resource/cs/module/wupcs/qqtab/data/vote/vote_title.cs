<?cs ####
	/*投票标题区*/
?>

<?cs def:data_vote_title()?>
	<?cs call:i()?>
	<?cs call:get_tuin_and_tid()?>
	<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ACT || qz_metadata.feedtype == UC_WUP_FEED_TYPE_RELATEPSV?>
		<?cs set:_vote_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/vote/" + qz_metadata.votedata.vote_topicid ?>
		<?cs call:get_userWho_platform(qz_metadata.votedata.vote_source,"")>
		<?cs if:string.length(qz_metadata.votedata.extendinfo.join_uin) > 0 && qz_metadata.votedata.extendinfo.join_uin != VOTE_INVALID_JOIN_UIN?>
			<?cs call:data_title_tipTxt("参与")?>	
			<?cs call:data_title_nick(qz_metadata.votedata.vote_owner, get_userWho_platform.ret, qz_metadata.orgdata.nickname)>
			<?cs call:data_title_tipTxt("发起的投票")?>				
		<?cs else?>
			<?cs call:data_title_tipTxt("发起投票")?>	
		<?cs /if?>
		<?cs if:get_userWho_platform.ret == USER_PLATFORM_WHO_PY>
			<?cs set:_vote_url = "http://baseapp.pengyou.com/" + qz_metadata.orgdata.extendinfo.uin_xy + "/vote/" + qz_metadata.votedata.vote_topicid ?>
		<?cs /if?>
		<?cs call:data_title_nick(qz_metadata.orgdata.uin, get_userWho_platform.ret, qz_metadata.orgdata.nickname)>
		<?cs if:subcount(qz_metadata.votedata.vote_title) > 0?>
			<?cs call:data_title_url(qz_metadata.votedata.vote_title.0.content,_vote_url)?>
		<?cs /if?>
		<?cs if:string.length(qz_metadata.votedata.extendinfo.join_uin) > 0 && qz_metadata.votedata.extendinfo.join_uin != VOTE_INVALID_JOIN_UIN?>
			<?cs call:data_title_tipTxt("选择了：")?>
			<?cs set:_opt_cnt = ""?>
			<?cs set:_end = subcount(qz_metadata.votedata.vote_option) - 1?>
			<?cs loop:j=0, _end, 1?>
				<?cs set:_opt_cnt = _opt_cnt + qz_metadata.votedata.vote_option[j] + "、"?>
			<?cs /loop?>
			<?cs if:qz_metadata.votedata.vote_num > 0?>
				</cs set:_opt_cnt = _opt_cnt + "超过" + qz_metadata.votedata.vote_num + "人参与该投票"?>
			<?cs /if?>
			<?cs call:data_title_tipTxt(_opt_cnt)?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_COMMPSV?>
		<?cs if:string.length(qz_metadata.votedata.extendinfo.join_uin) > 0 && qz_metadata.votedata.extendinfo.join_uin != VOTE_INVALID_JOIN_UIN?>
			<?cs call:data_title_tipTxt("评论了我参与的投票")?>
		<?cs else?>
			<?cs call:data_title_tipTxt("评论")?>
		<?cs /if?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV?>
		<?cs call:data_title_tipTxt("回复")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV?>
		<?cs call:data_title_tipTxt("在投票中提到了我")?>
	<?cs elif:qz_metadata.feedtype == UC_WUP_FEED_TYPE_OTHER?>
		<?cs set:_opt_cnt = ""?>
		<?cs call:data_title_tipTxt("参与投票")?>
		<?cs set:_vote_url = "http://user.qzone.qq.com/" + qz_metadata.orgdata.uin + "/vote/" + qz_metadata.votedata.vote_topicid ?>
		<?cs if:subcount(qz_metadata.votedata.vote_title) > 0?>
			<?cs call:data_title_url(qz_metadata.votedata.vote_title.0.content,_vote_url)?>
		<?cs /if?>
		<?cs call:data_title_tipTxt("选择了：")?>
		<?cs set:_end = subcount(qz_metadata.votedata.vote_option) - 1?>
		<?cs loop:j=0, _end, 1?>
			<?cs set:_opt_cnt = _opt_cnt + qz_metadata.votedata.vote_option[j] + "、"?>
		<?cs /loop?>
		<?cs call:data_title_tipTxt(_opt_cnt)?>
	<?cs /if?>
<?cs /def?>
