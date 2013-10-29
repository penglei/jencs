{
	'count' : <?cs var:specialcare_count ?>,
	'feedstype' : <?cs var:usespecialcare ?>,
	'carefriend': [
		<?cs each:item = suggest_specialcare_list ?>{
			'uin':'<?cs var:item.uin ?>',
			'name':"<?cs var:json_encode(item.nick,1) ?>",
			'img':'<?cs var:item.icon ?>'
		},<?cs /each ?>{}
	]
}