<?cs #/*该标志用以和contentbox结合，判断是否需要输出extendinfo(考虑和页面样式的同学沟通换一下标签位置)*/?>
<?cs set:g_extendinfo_exist = 0?>


<?cs set:listSameAction_conf.max_count=3 ?>
<?cs set:listSameAction_conf.endword="转过" ?>

<?cs def:v8_listSameAction(data,param) ?>
	<?cs if:subcount(data) ?>
	<div class="f-reprint-box">
		<p class="f-reprint">
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
				<?cs call:v8_conCommon(data) ?>
				<span class="ui_ml5">
					<?cs var:listSameAction_conf.endword ?>
				</span>
			</span>
		</p>
	</div>
	<?cs /if ?>
<?cs /def ?>

<?cs ####
	/**
	 *扩展信息组件
	 */
?>
<?cs def:v8_extendinfo_start()?>
	<?cs if:subcount(qfv.content.extendinfo.con) > 0 ?>
		<?cs set:g_extendinfo_exist = 1?>
		<p class="f-reprint">
	<?cs /if ?>
<?cs /def?>

<?cs def:v8_extendinfo_end()?>
	<?cs if:g_extendinfo_exist == 1?>
		</p>
	<?cs /if?>
<?cs /def?>

<?cs #:
	/**/
	function extend_lbs(lbs){}
?>

<?cs def:v8_conCommon_split(cons)?>
	<?cs if:subcount(cons.1) > 0 ?>
		<?cs set:_end = subcount(cons) - 1?>
		<?cs loop:i = 1, _end, 1?>
			<?cs call:v8_conCommon_item(cons[i])?>
		<?cs /loop?>
	<?cs /if?>
<?cs /def?>

<?cs def:v8_extendinfo() ?>
	<?cs call:v8_extendinfo_start()?>
	<?cs if:g_extendinfo_exist && subcount(qfv.content.extendinfo.con.0) > 0 ?>
		<?cs call:v8_conCommon_item(qfv.content.extendinfo.con[0])?>
	<?cs /if?>
	<?cs if:g_extendinfo_exist ?>
		<?cs call:v8_conCommon_split(qfv.content.extendinfo.con)?>
	<?cs /if ?>
	<?cs call:v8_extendinfo_end()?>
	<?cs #call:v8_listSameAction(qfv.content.extendinfo.relyPool.con,"") ?>
<?cs /def ?>
