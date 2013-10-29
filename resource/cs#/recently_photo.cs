{
    "photos" : [
        <?cs each:item = feeds.photo_data ?>
        {
            "albumId" : "<?cs  var:item.albumId ?>",
            "albumName" : "<?cs  var:json_encode(item.albumName,1) ?>",
            "desc" : "<?cs  var:json_encode(item.desc,1) ?>",
            "sloc" : "<?cs  var:item.sloc ?>",
            "lloc" : "<?cs  var:item.lloc ?>",
            "name" : "<?cs  var:json_encode(item.name,1) ?>",
            "photoType" : "<?cs  var:item.photoType ?>",
            "pre" : "<?cs  var:item.pre ?>",
            "uploadTime" : "<?cs  var:item.uploadTime ?>",
            "url" : "<?cs  var:item.url ?>",
            "height":"<?cs  var:item.height ?>",
            "width":"<?cs  var:item.width ?>"
        },
        <?cs /each ?>
        undefined
    ]
}
