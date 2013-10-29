<?cs ####
	/**
	 *获取分享的uin和
	 */
?>
<?cs def:get_share_uin_and_shareid()?>
	<?cs if:subcount(qz_metadata.relybody) > 0?>
		<?cs set:get_share_uin_and_shareid.uin = qz_metadata.relybody[0].uin ?>
		<?cs set:get_share_uin_and_shareid.shareid = qz_metadata.relybody[0].mkey ?>
	<?cs else ?>
		<?cs set:get_share_uin_and_shareid.uin = qz_metadata.orgdata.uin ?>
		<?cs set:get_share_uin_and_shareid.shareid = qz_metadata.orgdata.mkey ?>
	<?cs /if?>
<?cs /def?>

<?cs ####
	/**
	 *生成回复参数
	 *也会在回复被动feeds中用到
	 *@param {vt2body} t2body 当前评论的索引
	 */
?>
<?cs def:_share_psv_commentReply_param(t2body)?>

	<?cs call:get_tuin_and_tid()?>
	<?cs set:_param = get_tuin_and_tid.uin + "''" 
				+ get_tuin_and_tid.tid + "''" 
				+ t2body.seq  + "''" 
				+ t2body.uin  + "''" 
				+ "''" 
				+ "''"
				+ qz_metadata.feedtype ?>

	<?cs set:_share_psv_commentReply_param.ret = _param?>
<?cs /def?>

<?cs ####
	/**
	 *生成评论参数
	 *也会在评论被动feeds中用到
	 */
?>
<?cs def:_share_psv_comment_param()?>

	<?cs call:get_tuin_and_tid()?>
	<?cs set:_param = get_tuin_and_tid.uin + "''" 
				+ get_tuin_and_tid.tid + "''" 
				+ "-1"  + "''" 
				+ "0"  + "''" 
				+ "''" 
				+ "''"
				+ qz_metadata.feedtype ?>

	<?cs set:_share_psv_comment_param.ret = _param?>
<?cs /def?>

<?cs ####
	/**
	 *分享生成更多评论入口
	 */
?>
<?cs def:data_share_detail_url()?>
	<?cs call:get_tuin_and_tid()?>
	<?cs set:data_share_detail_url.ret = "http://user.qzone.qq.com/" + get_tuin_and_tid.uin + "/share/" + get_tuin_and_tid.tid ?>
<?cs /def?>
