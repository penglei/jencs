<?cs ####
	/**
	 * @description 评论回复的过滤方法统一写到这个文件中
	 * 规则:
	 * 入参:t2body/t3body
	 * 结果(返回值)：
	 * 如果是评论，如果该评论无需被过滤掉的话把filter_comment.ret.disabled设置为0，否则为1
	 * 如果是回复，如果该回复无需被过滤掉的话把filter_reply.ret.disabled设置为0，否则为1
	 * 请注意命名，如果是针对评论的过滤方法的话，请用filter_comment开头
	 * 如果是针对回复的过滤方法的话，请用filter_reply开头
	 * @example
	 *  &lt;?cs def:filter_comment_psv(t2body) ?&gt;
	 *  	if(无需过滤掉)
	 *  		&lt;?cs set:filter_comment.ret.disabled=0 ?&gt;
	 *  	else
	 *  		&lt;?cs set:filter_comment.ret.disabled=1 ?&gt;
	 *  &lt;?cs /def ?&gt;
	 */
?>

<?cs set:filter_comment.ret.disabled=0 ?>
<?cs set:filter_reply.ret.disabled=0 ?>

<?cs #####################评论过滤方法区，评论过滤方法统一写在下方################################### ?>

<?cs ####
	/**
	 *根据相册feeds类型过滤评论(批次和单张)
	 */
?>

<?cs def:filter_comment_photo(t2body)?>
	<?cs if: appid==4 ?>
		<?cs #/*获取评论过滤条件,单图1，批次2，不过滤保持0*/?>
		<?cs if:qz_metadata.feedtype == UC_WUP_FEED_TYPE_NEWCOMMENT || qz_metadata.meta.typeid==FEED_TYPE_AT_PHOTO?>
			<?cs set:showCmtType = 1 ?><?cs #/*单图评论过滤*/?>
		<?cs elif:qz_metadata.feedtype==UC_WUP_FEED_TYPE_COMMPSV || qz_metadata.feedtype==UC_WUP_FEED_TYPE_REPLYPSV || qz_metadata.feedtype == UC_WUP_FEED_TYPE_ATMEPSV ?>
			<?cs if:subtype == PHOTO_subtype_single?>
				<?cs set:showCmtType = 1 ?>
			<?cs elif:subtype==PHOTO_subtype_batch ?>
				<?cs set:showCmtType = 2 ?><?cs #/*批次评论过滤*/?>
			<?cs /if?>
		<?cs elif:qz_metadata.meta.typeid==FEED_TYPE_PHOTO || qz_metadata.meta.typeid==FEED_TYPE_PHOTO_CMTACT || qz_metadata.meta.typeid==FEED_ABOUT_PHOTO?>
			<?cs set:showCmtType = 1 ?><?cs #/*主动按feedtype区分较复杂，直接取typeid，凡是单图评论过滤*/?>
		<?cs else ?>
			<?cs set:showCmtType = 0 ?>
		<?cs /if?>

		<?cs #/*按条件进行过滤,单图1，批次2，不过滤保持0*/?>
		<?cs if:showCmtType == 0?>
			<?cs set:filter_comment.ret.disabled = 0 ?>
		<?cs elif:showCmtType == 1 ?>
			<?cs if:t2body.extendinfo.isPicCmt == 2?>
				<?cs set:filter_comment.ret.disabled = 1 ?>
			<?cs elif:t2body.extendinfo.isPicCmt == 1 ?>
				<?cs if:t2body.extendinfo.PiclargeId != qz_metadata.orgdata.itemdata[qz_metadata.orgdata.albumdata.extendinfo.iCurPicIndex].itemid ?>
					<?cs set:filter_comment.ret.disabled = 1 ?>
				<?cs else ?>
					<?cs set:filter_comment.ret.disabled = 0 ?>
				<?cs /if?>
			<?cs else ?>
				<?cs set:filter_comment.ret.disabled = 0 ?>
			<?cs /if?>
		<?cs elif:showCmtType == 2 ?>
			<?cs if:t2body.extendinfo.isPicCmt == 1?>
				<?cs set:filter_comment.ret.disabled = 1 ?>
			<?cs else ?>
				<?cs set:filter_comment.ret.disabled = 0 ?>
			<?cs /if?>
		<?cs else ?>
			<?cs set:filter_comment.ret.disabled = 0 ?>
		<?cs /if?>
	<?cs /if ?>
<?cs /def?>


<?cs #####################评论过滤方法区结束######################################################### ?>
<?cs #####################回复过滤方法区，回复过滤方法统一写在下方################################### ?>
<?cs #:
	/**
	 * @memberOf 评论回复数据组件
	 * @description 判断回复是否需要展示。主要用在拆分的被动feeds
	 * @private
	 * @param {Object} reply wup元数据中的t3body
	 * @return {Bool} _data_comment_replies_filter_psv_ret
	 */
	function _data_comment_replies_filter_psv(reply){}
?>
<?cs def:filter_reply_psv(reply) ?>
	<?cs if: (qz_metadata.appid==2||qz_metadata.appid==311||qz_metadata.appid==4) &&qz_metadata.feedtype == UC_WUP_FEED_TYPE_REPLYPSV ?>
		<?cs if: qz_metadata.opinfo.opuin== reply.uin ?>
			<?cs set:filter_reply.ret.disabled = 0?>
		<?cs elif:g_CurHostUin==reply.uin ?>
			<?cs if: !reply.extendinfo.target_uin ?>
				<?cs if:qz_metadata.opinfo.opuin == qz_metadata.opinfo.t2body.uin ?>
					<?cs set:filter_reply.ret.disabled = 0?>
				<?cs else ?>
					<?cs set:filter_reply.ret.disabled = 1?>
				<?cs /if ?>
			<?cs elif:reply.extendinfo.target_uin==qz_metadata.opinfo.opuin ?>
				<?cs set:filter_reply.ret.disabled = 0?>
			<?cs /if ?>
		<?cs elif:qz_metadata.opinfo.t2body.uin== reply.uin?>
			<?cs if: !reply.extendinfo.target_uin ?>
				<?cs if:qz_metadata.opinfo.opuin == qz_metadata.opinfo.t2body.uin ?>
					<?cs set:filter_reply.ret.disabled = 0?>
				<?cs else ?>
					<?cs set:filter_reply.ret.disabled = 1?>
				<?cs /if ?>
			<?cs elif:reply.extendinfo.target_uin==qz_metadata.opinfo.opuin ?>
				<?cs set:filter_reply.ret.disabled = 0?>
			<?cs /if ?>
		<?cs else ?>
			<?cs set:filter_reply.ret.disabled = 1?>
		<?cs /if ?>
	<?cs else ?>
		<?cs set:filter_reply.ret.disabled = 0?>
	<?cs /if ?>
<?cs /def ?>

<?cs #####################回复过滤方法区结束######################################################### ?>

<?cs set:filter_comment.ret.disabled_count=0 ?>

<?cs #:
	/**/
	function filter_comment(reply){}
?>
<?cs def:filter_comment(comment) ?>
	<?cs #开始时要把这个结果值先给置回默认值 ?>
	<?cs set:filter_comment.ret.disabled=0 ?>


	<?cs if: filter_comment.ret.disabled==0 ?>
		<?cs #把过滤方法“注册”进来 ?>
		<?cs call:filter_comment_photo(comment) ?>
	<?cs /if ?>

	<?cs #以下方法体结束区，必须用这个结尾，不能更改 ?>
	<?cs if: filter_comment.ret.disabled==1 ?>
		<?cs set: filter_comment.ret.disabled_count=filter_comment.ret.disabled_count+1 ?>
	<?cs /if ?>
<?cs /def ?>

<?cs set: filter_reply.ret.disabled_count=0 ?>

<?cs #:
	/**/
	function filter_reply(reply){}
?>
<?cs def:filter_reply(reply) ?>
	<?cs #开始时要把这个结果值先给置回默认值 ?>
	<?cs set:filter_reply.ret.disabled=0 ?>

	<?cs #把过滤方法“注册”进来 ?>
	<?cs if: filter_reply.ret.disabled==0 ?>
		<?cs call:filter_reply_psv(reply) ?>
	<?cs /if ?>


	<?cs #以下方法体结束区，必须用这个结尾，不能更改 ?>
	<?cs if: filter_reply.ret.disabled==1 ?>
		<?cs set: filter_reply.ret.disabled_count=filter_reply.ret.disabled_count+1 ?>
	<?cs /if ?>
<?cs /def ?>

<?cs #:
	/**/
	function filter_reply_reset(){}
?>
<?cs def:filter_reply_reset() ?>
	<?cs set: filter_reply.ret.disabled_count=0 ?>
<?cs /def ?>
