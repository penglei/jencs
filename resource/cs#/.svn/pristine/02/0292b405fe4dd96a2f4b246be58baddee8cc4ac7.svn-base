<?cs #/*该标志用以和contentbox结合，判断是否需要输出extendinfo(考虑和页面样式的同学沟通换一下标签位置)*/?>
<?cs set:g_extendinfo_exist = 0?>


<?cs set:listSameAction_conf.max_count=3 ?>
<?cs set:listSameAction_conf.endword="转过" ?>

<?cs def:listSameAction(data,param) ?>
	<?cs if:subcount(data) ?>
		<p class="f_reprint c_tx3">
			<span class="ur_mr10">
				<?cs if: subcount(data.0) ?>
					<?cs set:_data_len=subcount(data) ?>
				<?cs elif:subcount(data)>0 ?>
					<?cs set:_data_len=1 ?>
				<?cs else ?>
					<?cs set:_data_len=0 ?>
				<?cs /if ?>
				<?cs if: _data_len > listSameAction_conf.max_count ?>
					<?cs set:_data_len = listSameAction_conf.max_count ?>
				<?cs /if ?>
				<?cs call:conCommon(data) ?>
				<span class="ui_ml5">
					<?cs var:listSameAction_conf.endword ?>
				</span>
			</span>
		</p>
	<?cs /if ?>
<?cs /def ?>

<?cs ####
	/**
	 *扩展信息组件
	 */
?>
<?cs def:extendinfo_start()?>
	<?cs if:subcount(qfv.content.extendinfo.con) > 0 ?>
		<?cs set:g_extendinfo_exist = 1?>
		<p class="f_reprint c_tx3">
	<?cs /if ?>
<?cs /def?>

<?cs def:extendinfo_end()?>
	<?cs if:g_extendinfo_exist == 1?>
		</p>
	<?cs /if?>
<?cs /def?>

<?cs #:
	/**/
	function extend_lbs(lbs){}
?>
<?cs def:extend_lbs(lbsdata) ?>
	<?cs if:!lbsdata ?>
		<?cs with:lbs=qfv.content.lbs ?>
		<?cs if: subcount(lbs) ?>
			<?cs set:extend_lbs.con.type="txt" ?>
			<?cs set:extend_lbs.con.text=lbs.idName ?>
			<?cs set:extend_lbs.con.mr=10 ?>
			<?cs call:con_txt(extend_lbs.con) ?>
			<?cs #查看地图的链接 ?>
			<?cs set:extend_lbs.map.text="查看地图"?>
			<?cs set:extend_lbs.map.type="txt" ?>
			<?cs set:extend_lbs.map.mr = 10 ?>
			<?cs set:extend_lbs.map.action.version="5" ?>
			<?cs set:extend_lbs.map.action.width="300" ?>
			<?cs set:extend_lbs.map.action.height="200" ?>
			<?cs set:extend_lbs.map.action.title="点击查看地图" ?>
			<?cs set:extend_lbs.map.action.param="#posx="+lbs.posx+"&posy="+lbs.posy ?>
			<?cs set:extend_lbs.map.action.config="id:map" ?>
			<?cs set:extend_lbs.map.action.src="/qzone/app/controls/map/tips.html" ?>
			<?cs call:con_popup(extend_lbs.map) ?>
		<?cs /if ?>
		<?cs /with ?>
	<?cs else ?>
		<?cs if: subcount(lbsdata) ?>
			<?cs set:extend_lbs.con=1 ?>
			<?cs set:extend_lbs.con.type="txt" ?>
			<?cs set:extend_lbs.con.text=lbsdata.idName ?>
			<?cs set:extend_lbs.con.mr=10 ?>
			<?cs call:con_txt(extend_lbs.con) ?>
			<?cs #查看地图的链接 ?>
			<?cs set:extend_lbs.map.text="查看地图"?>
			<?cs set:extend_lbs.map.type="txt" ?>
			<?cs set:extend_lbs.map.action.version="5" ?>
			<?cs set:extend_lbs.map.action.width="300" ?>
			<?cs set:extend_lbs.map.action.height="200" ?>
			<?cs set:extend_lbs.map.action.title="点击查看地图" ?>
			<?cs set:extend_lbs.map.action.param="#posx="+lbsdata.posx+"&posy="+lbsdata.posy ?>
			<?cs set:extend_lbs.map.action.config="id:map" ?>
			<?cs set:extend_lbs.map.action.src="/qzone/app/controls/map/tips.html" ?>
			<?cs call:con_popup(extend_lbs.map) ?>
		<?cs /if ?>
	<?cs /if ?>
<?cs /def ?>

<?cs def:extendinfo() ?>
	<?cs call:extendinfo_start()?>
	<?cs call:extend_lbs("") ?>
	<?cs if:g_extendinfo_exist ?>
		<?cs call:conCommon(qfv.content.extendinfo.con)?>
	<?cs /if ?>
	<?cs call:extendinfo_end()?>
	<?cs call:listSameAction(qfv.content.extendinfo.relyPool.con,"") ?>
<?cs /def ?>
