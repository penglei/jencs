<?cs #:闹钟提醒feeds组件  ?>
<?cs def:clockBoxViewer() ?>


<?cs def:clockbox_UserList(users, maxcount)?>
	<?cs if:subcount(users.0)?>
		<?cs loop:i=0, maxcount, 1?>
			<?cs var:users[i].uin ?>|
		<?cs /loop?>
	<?cs else ?>
		<?cs var:users.uin ?>
	<?cs /if?>
<?cs /def ?>

<?cs if:subcount(qz_metadata.clock_box) > 0 ?>
<?cs with:clockBox = qz_metadata.clock_box ?>
<div class="f_ct_imgtxt f_ct_act">
	<div class="txt_box">
		<?cs if:clockBox.time ?>
		<p>
			<i class="ui_ico icon_act icon_act_time"></i>
			<?cs var:clockBox.time?>
		</p>
		<?cs /if?>

		<?cs if:subcount(clockBox.lbs) > 0 && clockBox.lbs.name?>
		<p>
			<i class="ui_ico icon_act icon_act_location"></i>
			<?cs var:clockBox.lbs.name ?>
		</p>
		<?cs /if ?>

		<?cs if:clockBox.uincount > 0 ?>
		<?cs set:_clockbox_uincout_max = 24?>
		<?cs if:clockBox.uincount < _clockbox_uincout_max?>
			<?cs set:_clockbox_uincout_max = clockBox.uincount?>
		<?cs /if?>
		<p data-uinlist="<?cs call:clockbox_UserList(clockBox.uinlist.friends, _clockbox_uincout_max) ?>" data-display="hide">
			<a href="javascript:;" class="qz-clock-uinsBtn">
			<i class="ui_ico icon_act icon_act_member"></i>
			<?cs var:clockBox.uincount ?>人参加
			</a>
		</p>
		<?cs /if?>

	</div>
</div>
<?cs /with ?>
<?cs /if ?>
<?cs /def ?>