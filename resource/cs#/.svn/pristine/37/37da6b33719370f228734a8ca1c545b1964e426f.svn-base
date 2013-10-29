<?cs ####
	/**
	 *浏览按钮
	 *up 浏览按钮是否是标准按钮呢?是不是应该考虑index排序的问题，visitor按钮有可能是最后一个
	 */
?>
<?cs def:v8_visitor()?>
	<?cs if:subcount(qfv.operate.visitor)?>
		<?cs call:qfv("visitor.notarget", 1)?><?cs #有target会跳到新页面，这是不行的?>
		<?cs set:qfv.operate.visitor.js = 1 ?>
		<span class="state ui-mr10">
		
		<a href="javascript:;" 
			class="state qz_feed_plugin" 
			data-role="Visitor" 
			data-config="<?cs var:qfv.operate.visitor.param?>" 
		>
			<i class="ui-icon icon-browse"></i>浏览<?cs if:qfv.operate.visitor.count?>(<?cs var:qfv.operate.visitor.count?>)<?cs /if?>
		</a>
		</span>
	<?cs /if?>
<?cs /def?>
