{
	hasMoreHotFeeds:<?cs alt:feeds.hasMoreHotFeeds ?>0<?cs /alt ?>,
	data:[
		<?cs each:item = feeds.friend_data ?>
			<?cs set:_emoji='' ?>
			<?cs if:subcount(item.emoji)>0 ?>
				<?cs set:_len=subcount(item.emoji) ?>
				<?cs loop:i =0, _len, 1 ?>
					<?cs if:string.length(item.emoji[i])>0 ?>
						<?cs set:_emoji=_emoji+"'"+item.emoji[i]+"'" ?>
						<?cs if:i<(_len-1) ?><?cs set:_emoji=_emoji+"," ?><?cs /if ?>
					<?cs /if ?>
				<?cs /loop ?>
			<?cs elif:string.length(item.emoji)>0 ?>
				<?cs set:_emoji=_emoji+"'"+item.emoji+"'" ?>
			<?cs /if ?>
		{
			ver:'<?cs  var:item.ver ?>',
			appid:'<?cs  var:item.appid ?>',
			typeid:'<?cs  var:item.typeid ?>',
			key:'<?cs  var:item.key ?>',
			flag:'<?cs  var:item.flag ?>',
			dataonly:'0',
			specialType:1,
			titleTemp:'<?cs  var:item.titleTemp ?>',
			summaryTemp:'<?cs  var:item.summaryTemp ?>',
			feedno:'<?cs  var:item.feedno ?>',
			title:'<?cs var:json_encode(item.title, 1) ?>',
			summary:'<?cs var:json_encode(item.summary, 1) ?>',
			appiconid:'<?cs  var:item.appiconid ?>',
			clscFold:'<?cs  var:item.clscFold ?>',
			abstime:'<?cs  var:item.abstime ?>',
			feedstime:'<?cs  var:item.feedstime ?>',
			userHome:'<?cs  var:item.userHome ?>',
			namecardLink:'<?cs  var:item.namecardLink ?>',
			opuin:'<?cs  var:item.opuin ?>',
			uin:'<?cs  var:item.uin ?>',
			ouin:'<?cs  var:item.ouin ?>',
			foldFeed:'<?cs  var:item.foldFeed ?>',
			foldFeedTitle:'<?cs  var:item.foldFeedTitle ?>',
			showEbtn:'<?cs  var:item.showEbtn ?>',
			scope:'<?cs  var:item.scope ?>',
			hideExtend:'<?cs  var:item.hideExtend ?>',
			nickname:'<?cs var:json_encode(item.nickname, 1) ?>',
			emoji:[<?cs var:_emoji ?>],
			remark:'<?cs var:json_encode(item.remark, 1) ?>',
			type:'<?cs  var:item.type ?>',
			vip:'<?cs  var:item.vip ?>',
			bitmap:'<?cs  var:item.bitmap ?>',
			yybitmap:'<?cs  var:item.yybitmap ?>',
			info_user_name:'<?cs  var:item.info_user_name ?>',
			logimg:'<?cs  var:item.logimg ?>',
			bor:'<?cs  var:item.bor ?>',
			lastFeedBor:'<?cs  var:item.lastFeedBor ?>',
			list_bor2:'<?cs  var:item.list_bor2 ?>',
			info_user_display:'<?cs  var:item.info_user_display ?>',
			upernum:'<?cs  var:item.upernum ?>',
			oprType:'<?cs  var:item.oprType ?>',
			moreflag:'<?cs  var:item.moreflag ?>',
			otherflag:'<?cs  var:item.otherflag ?>',
			rightflag:'<?cs  var:item.rightflag ?>',
			sameuser:{<?cs  var:item.sameuser ?>},
			uper_isfriend:[<?cs var:item.uper_isfriend ?>],
			uperlist:[<?cs  var:item.uperlist ?>],
			smallstar:'<?cs  var:item.smallstar ?>',
			html:'<?cs var:json_encode(item.html, 1) ?>',
			mergeData:[<?cs var:item.mergeData_json ?>undefined],
			likecnt:'<?cs  var:item.likecnt ?>',
			relycnt:'<?cs  var:item.relycnt ?>',
			commentcnt:'<?cs  var:item.commentcnt ?>',
			picdata:{
				srcurl:'<?cs var:json_encode(item.picUrl, 1) ?>',
				albumid: '<?cs var:json_encode(item.albumId, 1) ?>',
				topicid:'<?cs var:item.picTopicId ?>',
				jumpurl:'<?cs var:json_encode(item.picJumpUrl, 1) ?>',
				pickey: '<?cs var:json_encode(item.picKey, 1) ?>'
			}
		},
		<?cs /each ?>	
		undefined
	]
}
