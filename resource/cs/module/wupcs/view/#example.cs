<?cs def:gift_title_con(con)?>
	<?cs if:con.type =="gift_custom"?>
		<?cs #自定义标题类型?>
		<span class="xxx yyy" data-role="xxxx">
		</span>
	<?cs else ?>
		<?cs call:title_item(con)?>
	<?cs /if?>
<?cs /def ?>

<?cs #call:title()?>
<?cs #----------------#?>
<?cs call:title_start()?>
<?cs if:subcount(qfv.title.con.0) > 0?>
<?cs each:item=qfv.title.con?>
	<?cs call:gift_title_con(item)?>
<?cs /each?>
<?cs else ?>
	<?cs call:gift_title_con(qfv.title.con)?>
<?cs /if?>
<?cs call:title_end()?>


<?cs #def:summary() ?>
<?cs #----------------#?>
<?cs call:summary_start()?>
	<?cs call:quote()?>

	<?cs #call:content("leftbor")?>
	<?cs #----------------#?>
	<?cs call:content_start("leftbor")?>
		<?cs call:subtitle()?>
		<?cs call:cnt-image()?>
		<?cs call:cnt-txt()?>
	<?cs call:content_end()?>

	<?cs #call:operate()?>
	<?cs #----------------#?>
	<?cs call:operate_start()?>
	<?cs call:operate_end()?>

	<?cs call:comment()?>

<?cs call:content_end()?>
