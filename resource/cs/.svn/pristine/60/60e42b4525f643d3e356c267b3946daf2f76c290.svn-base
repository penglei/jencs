{
   "hasmore" : <?cs  var:feeds.hasMoreFeeds ?>,
   "begintime" : <?cs  var:feeds.begintime ?>,
   "photos" : [
	<?cs each:item = feeds.photo_data ?>
      {
		"album" : {
            "bitmap" : "<?cs  var:item.bitmap ?>",
            "id" : "<?cs  var:item.albumid ?>",
            "privacy" : "<?cs  var:item.privacy ?>",
            "title" : "<?cs  var:json_encode(item.albumtitle,1) ?>",
            "total" : "<?cs  var:item.albumtotal ?>"
         },
         "owner" : {
            "face" : "<?cs  var:json_encode(item.face,1) ?>",
            "nick" : "<?cs  var:json_encode(item.nick,1) ?>",
            "uin" : "<?cs  var:item.uin ?>"
         },		
		 "number":"<?cs var:item.number ?>",
         "comment_total" : "<?cs  var:item.comment_total ?>",
		 "praiseNum" : "<?cs  var:item.praiseNum ?>",
         "large_image" : {
            "height" : "<?cs  var:item.height ?>",
            "url" : "<?cs  var:json_encode(item.large_url,1) ?>",
            "width" : "<?cs  var:item.width ?>"
         },
         "lloc" : "<?cs  var:item.lloc ?>",
         "sloc" : "<?cs  var:item.sloc ?>",
         "medium_url" : "<?cs  var:json_encode(item.medium_url,1) ?>",
		 "small_url" : "<?cs  var:json_encode(item.small_url,1) ?>",
         "title" : "<?cs  var:json_encode(item.title,1) ?>",
         "uploadtime" : "<?cs  var:item.uploadtime ?>",
		 "modifytime" : "<?cs  var:item.modifytime ?>",
		 "desc" : "<?cs  var:json_encode(item.desc,1) ?>"
      }, 
	<?cs /each ?>	
	undefined
   ]
}
