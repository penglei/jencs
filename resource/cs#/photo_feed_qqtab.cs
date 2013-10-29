{
    "hasmore" : <?cs  var:feeds.hasMoreFeeds ?>,
    "begintime" : <?cs  var:feeds.begintime ?>,
    "start" : <?cs  var:feeds.start ?>,
    "count" : <?cs  var:feeds.count ?>,
    "feeds" : [
        <?cs each:item = feeds.photo_data ?>
        {
            "type" : "<?cs  var:item.type ?>",
            "s_type" : "<?cs  var:item.s_type ?>",
            "vip" : "<?cs  var:item.vip ?>",
            "time" : "<?cs  var:item.time ?>",
            "uin" : "<?cs  var:item.uin ?>",
            "appid":"<?cs  var:item.appid ?>",
            "typeid":"<?cs  var:item.typeid ?>",
            "source":"<?cs  var:item.source ?>",
            "skey":"<?cs  var:item.skey ?>",
            "nick" : "<?cs  var:json_encode(item.nick,1) ?>",
            "albumname" : "<?cs  var:json_encode(item.albumname,1) ?>",
            "albumid" : "<?cs  var:item.albumid ?>",
            "albumtotalpic" : "<?cs  var:item.albumtotalpic ?>",
            "multiupnumber" : "<?cs  var:item.multiupnumber ?>",
            "batchid" : "<?cs  var:item.batchid ?>",        
            "privacy" : "<?cs  var:item.privacy ?>",
            "ilike" : "<?cs  var:item.ilike ?>",
            "like_users" : "<?cs  var:json_encode(item.like_users,1) ?>",
            "comment_total" : "<?cs  var:item.comment_total ?>",
            "comments" : [
                <?cs each:item_comment = item.commentinfo ?>
                {
                    "id" : "<?cs  var:item_comment.id ?>",
                    "uin" : "<?cs  var:item_comment.uin ?>",
                    "nick" : "<?cs  var:json_encode(item_comment.nick,1) ?>",
                    "time" : "<?cs var:item_comment.time ?>",
                    "content" : "<?cs var:json_encode(item_comment.content,1) ?>",
                    "respnum" : "<?cs var:item_comment.respnum ?>",
                    "responses" : [
                        <?cs each:item_response = item_comment.response ?>
                        {
                            "id" : "<?cs var:item_response.id ?>",
                            "uin" : "<?cs var:item_response.uin ?>",
                            "nick" : "<?cs var:json_encode(item_response.nick,1) ?>",
                            "target_uin" : "<?cs var:item_response.target_uin ?>",
                            "target_nick" : "<?cs var:json_encode(item_response.target_nick,1) ?>",
                            "time" : "<?cs var:item_response.time ?>",
                            "content" : "<?cs var:json_encode(item_response.content,1) ?>"
                        },
                        <?cs /each ?>
                        undefined
                    ]
                },
                <?cs /each ?>
                undefined
            ],
            "praiseNum" : "<?cs  var:item.praiseNum ?>",
            "desc" : "<?cs  var:json_encode(item.desc,1) ?>",
            "photo_total" : "<?cs  var:item.photo_total ?>",
            "photos" : [
                <?cs each:item_photo = item.photoinfo ?>
                {
                    "sloc" : "<?cs  var:item_photo.sloc ?>",
                    "lloc" : "<?cs  var:item_photo.lloc ?>",
                    "picsmallurl" : "<?cs  var:item_photo.picsmallurl ?>",  
                    "picbigurl" : "<?cs  var:item_photo.picbigurl ?>",      
                    "url" : "<?cs  var:item_photo.url ?>",
                    "picname" : "<?cs  var:json_encode(item_photo.picname,1) ?>",
                    "org_width" : "<?cs  var:item_photo.org_width ?>",  
                    "org_height" : "<?cs  var:item_photo.org_height ?>",
                    "desc" : "<?cs  var:json_encode(item_photo.desc,1) ?>"
                },
                <?cs /each ?>
                undefined
            ]
        }, 
        <?cs /each ?>   
        undefined
    ]
}
