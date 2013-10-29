<?cs ####
	/**
	 *style分为三种情况:
	 * 1、f_ct_1 bor3			#bor
	 * 2、f_ct_2 bor3 bg3		#borbg
	 * 3、NULL
	 */
?>
<?cs def:v8_summary_start()?>
			<?cs #qz_summary这个节点其实可以不要，这是我们自己加上的?>
			<div class="qz_summary wupfeed" id="hex_<?cs call:v8_genFeedId() ?>">
				<?cs call:v8_echo_feed_data()?>
<?cs /def?>

<?cs def:v8_summary_end() ?>
			</div><?cs #end: .qz_summary?>
		</div><?cs #end: .f-item?>
	</div><?cs #end: .f-wrap?>
</li>
<?cs /def?>

<?cs #:一个标准的summary解析流程?>
<?cs def:v8_summary(style, cls) ?>
	<?cs call:v8_summary_start()?>
		<?cs call:v8_quote()?>
		<?cs call:v8_contentBox(style, cls)?>
		<div class="f-op-wrap">
			<?cs call:v8_operate()?>
			<?cs call:v8_comments-like()?>
		</div>
	<?cs call:v8_summary_end()?>
<?cs /def?>
