<?cs def:debug(node)?>
	<h1><?cs name:node?>:<?cs var:node?></h1>
<?cs /def?>

<?cs set:g_data_pool = ""?>
<?cs set:g_key_pool_i = 0?>
<?cs set:g_key_pool = ""?>

<?cs def:save(key, value)?>
<?cs set:_len = subcount(g_key_pool)?>
<?cs if:_len == 0?>
	<?cs set:g_key_pool[g_key_pool_i] = key?>
	<?cs set:g_key_pool_i = g_key_pool_i + 1?>
<?cs else ?>
	<?cs set:_nokeyflag = 1?>
	<?cs each:_key = g_key_pool?>
		<?cs if:_key == key?>
			<?cs set:_nokeyflag = 0?>
		<?cs /if?>
	<?cs /each?>

	<?cs if:_nokeyflag?>
		<?cs set:g_key_pool[g_key_pool_i] = key?>
		<?cs set:g_key_pool_i = g_key_pool_i + 1?>
		<?cs set:g_data_pool[key] = value?>
	<?cs else ?>
		<?cs #/*如果存在key，覆盖值*/?>
		<?cs set:g_data_pool[key] = value?>
	<?cs /if?>
<?cs /if?>


<?cs /def?>

<?cs def:set(path, name, value)?>
	<?cs if:name == ""?>
		<?cs set:qfv[path] = value ?>
		<?cs set:_key = "qfv." + path?>
		<?cs set:_val = value?>
		<?cs call:save(_key, _val)?>
	<?cs else ?>
		<?cs set:qfv[path][name] = value?>
		<?cs set:_key="qfv." + path + "." + name?>
		<?cs set:_val=value?>
		<?cs call:save(_key, _val)?>
	<?cs /if?>
<?cs /def?>

<?cs def:qfv(path, value)?>
	<?cs set:qfv[path] = value?>

	<?cs set:_key="qfv." + path?>
	<?cs set:_val = value?>
	<?cs call:save(_key, _val)?>
<?cs /def?>

<?cs def:print()?>
	<?cs set:i = 0?>
	<?cs each:key = g_key_pool?>
		<?cs set:g_print_key = key?>
		<?cs set:g_print_value = g_data_pool[key]?>
<?cs include:"wupcs/debug_print.cs"?>
		<?cs set: i = i + 1?>
	<?cs /each?>
	<?cs #/*检查下标，看看输出的数据是否对应上*/?>
//i=<?cs var:i?>,g_key_pool_i=<?cs var:g_key_pool_i?>
<?cs /def?>
<?cs include:"wupcs/data.cs"?>

<?cs call:print()?>
