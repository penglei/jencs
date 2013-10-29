{
	main:{
		hasMoreFeeds:<?cs  var:feeds.hasMoreFeeds ?>,
		daylist:'<?cs  var:feeds.g_daylist ?>',
		uinlist:'<?cs  var:feeds.g_uinlist ?>',
		error:'<?cs  var:feeds.error ?>',
		hotkey:'<?cs  var:feeds.hotkey ?>',
		friend_level:'<?cs  var:feeds.friend_level ?>',
		begintime:'<?cs  var:feeds.begintime ?>',
		endtime:'<?cs  var:feeds.endtime ?>',
		dayspac:'<?cs  var:feeds.dayspac ?>'
	},
	data:[
		<?cs each:item = feeds.hotspot_data ?>
		{
			ver:'<?cs  var:item.ver ?>',
			appid:'<?cs  var:item.appid ?>',
			typeid:'<?cs  var:item.typeid ?>',
			key:'<?cs  var:item.key ?>',
			hotkey:'<?cs  var:item.hotkey ?>',
			feedno:'<?cs  var:item.feedno ?>',
			cmtnum:'<?cs  var:item.cmtnum ?>',
			picnum:'<?cs  var:item.picnum ?>',
			albumid:'<?cs var:item.albumid ?>',
			urllist:{<?cs var:item.urllist ?>},
			title:'<?cs var:json_encode(item.title, 1) ?>',
			summary:'<?cs var:json_encode(item.summary, 1) ?>',
			appiconid:'<?cs  var:item.appiconid ?>',
			abstime:'<?cs  var:item.abstime ?>',
			feedstime:'<?cs  var:item.feedstime ?>',
			userHome:'<?cs  var:item.userHome ?>',
			uin:'<?cs  var:item.uin ?>',
			nickname:'<?cs var:json_encode(item.nickname, 1) ?>',
			vip:'<?cs  var:item.vip ?>',
			logimg:'<?cs  var:item.logimg ?>',
			upernum:'<?cs  var:item.upernum ?>',
			oprType:'<?cs  var:item.oprType ?>',
			moreflag:'<?cs  var:item.moreflag ?>',
			otherflag:'<?cs  var:item.otherflag ?>',
			rightflag:'<?cs  var:item.rightflag ?>',
			uper_isfriend:[<?cs var:item.uper_isfriend ?>],
			uperlist:[<?cs  var:item.uperlist ?>],
			specialType:[<?cs  var:item.specialType ?>],
			html:'<?cs var:json_encode(item.html, 1) ?>',
                        mergeData:[<?cs var:item.mergeData_json ?>undefined],
                        likecnt:'<?cs  var:item.likecnt ?>',
                        relycnt:'<?cs  var:item.relycnt ?>',
                        commentcnt:'<?cs  var:item.commentcnt ?>'
		},
		<?cs /each ?>	
		undefined
	]
}