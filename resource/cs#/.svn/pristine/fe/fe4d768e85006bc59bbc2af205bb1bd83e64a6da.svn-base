<?cs def:v8_lbs()?>
	<?cs if:qfv.content.lbs.idName?>
		<?cs with:lbs = qfv.content.lbs?>
		<?cs set:extend_lbs.map.text=lbs.idName ?>
		<?cs set:extend_lbs.map.type="txt" ?>
		<?cs set:extend_lbs.map.action.mr = 8 ?>
		<?cs set:extend_lbs.map.action.color="tip" ?>
		<?cs set:extend_lbs.map.action.version="5" ?>
		<?cs set:extend_lbs.map.action.width="300" ?>
		<?cs set:extend_lbs.map.action.height="200" ?>
		<?cs set:extend_lbs.map.action.title="点击查看地图" ?>
		<?cs set:extend_lbs.map.action.tagtitle="点击查看地图" ?>
		<?cs set:extend_lbs.map.action.param="#posx="+lbs.posx+"&posy="+lbs.posy ?>
		<?cs set:extend_lbs.map.action.config="id:map" ?>
		<?cs set:extend_lbs.map.action.src="/qzone/app/controls/map/tips.html" ?>
		<?cs call:v8_con_popup(extend_lbs.map) ?>
		<?cs /with?>
	<?cs /if?>
<?cs /def?>

<?cs def:v8_extend_lbs(lbsdata) ?>
	<?cs if:!lbsdata ?>
		<?cs with:lbs=qfv.content.lbs ?>
		<?cs if: subcount(lbs) ?>
			<?cs #set:extend_lbs.con.type="txt" ?>
			<?cs #set:extend_lbs.con.text=lbs.idName ?>
			<?cs #set:extend_lbs.con.mr=10 ?>
			<?cs #call:v8_con_txt(extend_lbs.con) ?>
			<?cs #查看地图的链接 ?>
			<?cs set:extend_lbs.map.text=lbs.idName ?>
			<?cs set:extend_lbs.map.type="txt" ?>
			<?cs set:extend_lbs.map.action.mr = 10 ?>
			<?cs set:extend_lbs.map.action.version="5" ?>
			<?cs set:extend_lbs.map.action.width="300" ?>
			<?cs set:extend_lbs.map.action.height="200" ?>
			<?cs set:extend_lbs.map.action.title="点击查看地图" ?>
			<?cs set:extend_lbs.map.action.tagtitle="点击查看地图" ?>
			<?cs set:extend_lbs.map.action.param="#posx="+lbs.posx+"&posy="+lbs.posy ?>
			<?cs set:extend_lbs.map.action.config="id:map" ?>
			<?cs set:extend_lbs.map.action.src="/qzone/app/controls/map/tips.html" ?>
			<?cs call:v8_con_popup(extend_lbs.map) ?>
		<?cs /if ?>
		<?cs /with ?>
	<?cs else ?>
		<?cs if: subcount(lbsdata) ?>
			<?cs #set:extend_lbs.con=1 ?>
			<?cs #set:extend_lbs.con.type="txt" ?>
			<?cs #set:extend_lbs.con.text=lbsdata.idName ?>
			<?cs #set:extend_lbs.con.mr=10 ?>
			<?cs #call:v8_con_txt(extend_lbs.con) ?>
			<?cs #查看地图的链接 ?>
			<?cs set:extend_lbs.map.text=lbsdata.idName ?>
			<?cs set:extend_lbs.map.type="txt" ?>
			<?cs set:extend_lbs.map.action.mr = 10 ?>
			<?cs set:extend_lbs.map.action.version="5" ?>
			<?cs set:extend_lbs.map.action.width="300" ?>
			<?cs set:extend_lbs.map.action.height="200" ?>
			<?cs set:extend_lbs.map.action.title="点击查看地图" ?>
			<?cs set:extend_lbs.map.action.tagtitle="点击查看地图" ?>
			<?cs set:extend_lbs.map.action.param="#posx="+lbsdata.posx+"&posy="+lbsdata.posy ?>
			<?cs set:extend_lbs.map.action.config="id:map" ?>
			<?cs set:extend_lbs.map.action.src="/qzone/app/controls/map/tips.html" ?>
			<?cs call:v8_con_popup(extend_lbs.map) ?>
		<?cs /if ?>
	<?cs /if ?>
<?cs /def ?>
