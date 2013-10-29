<?cs ####
	/**
	 *style分为三种情况:
	 * 1、f_ct_1 bor3			#bor
	 * 2、f_ct_2 bor3 bg3		#borbg
	 * 3、NULL
	 */
?>
<?cs def:summary_start()?>
<div class="qz_summary wupfeed" id="hex_<?cs call:genFeedId() ?>">
<?cs call:echo_feed_data()?>
<?cs /def?>

<?cs def:summary_end() ?>
</div>
<?cs /def?>

<?cs #:一个标准的summary解析流程?>
<?cs def:summary(style, cls) ?>
	<?cs call:summary_start()?>
		<?cs call:quote()?>
		<?cs call:contentBox(style, cls)?>
		<?cs call:operate()?>
		<?cs call:comments-like()?>
	<?cs call:summary_end()?>
<?cs /def?>

<?cs def:summaryOnlyMedia(style, cls)?>
	<?cs call:summary_start()?>
		<?cs call:quote()?>
		<?cs call:contentBox_start(style, cls)?>
			<?cs call:contentMedia()?>
		<?cs call:contentBox_end()?>
		<?cs call:operate()?>
		<?cs call:comments-like()?>
	<?cs call:summary_end()?>
<?cs /def?>

<?cs def:summaryOnlyTxt(style, cls)?>
	<?cs call:summary_start()?>
		<?cs call:quote()?>
		<?cs call:contentBox_start(style, cls)?>
			<?cs call:contentTxt("")?>
		<?cs call:contentBox_end()?>
		<?cs call:operate()?>
		<?cs call:comments-like()?>
	<?cs call:summary_end()?>
<?cs /def?>
