<?cs include:"wupcs/data/shake/const.cs"?>
<?cs include:"wupcs/data/shake/common.cs"?>
<?cs include:"wupcs/data/shake/shake_contentbox.cs"?>
<?cs include:"wupcs/data/shake/shake_title.cs"?>


<?cs call:data_shake_title()?>
<?cs call:data_shake_subtitle()?>
<?cs call:data_shake_contentbox()?>
<?cs call:data_oprtime()?>
<?cs call:data_source_custom(qz_metadata.orgdata.source_name,qz_metadata.orgdata.source_url)?>
<?cs call:data_extendinfo_time() ?>	
<?cs call:data_opr_url(0,"查看",qz_metadata.orgdata.srcurl,"")?>
<?cs call:data_opr_more() ?>
<?cs call:data_opr_delfeed()?>