<?cs def:level2TitleView() ?>
<?cs if:string.length(qz_metadata.rt_uin) > 0 ?>
	<?cs if:string.length(qz_metadata.last_fwd_cmt) > 0 ?>
	�����ң�<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>" class="c_tx"><?
		cs var:qz_metadata.last_fwd_cmt ?><?
		cs call:richRetweetList() ?>
	</a> 
		<?cs if:string.length(qz_metadata.t1_source_url) == 0 ?>
			<span class="ifeeds_origin c_tx3">ͨ��<?cs var:qz_metadata.t1_source_name ?></span>
		<?cs else?>
			<a class="ifeeds_origin c_tx3" href="<?cs var:qz_metadata.t1_source_url ?>" target="_blank">ͨ��<?cs var:qz_metadata.t1_source_name ?></a>
		<?cs /if?>
	<?cs else?>
	�����ң�
	<?cs /if?>
<?cs else?>
	�����ң�<a target="_blank" href="http://user.qzone.qq.com/<?cs var:qz_metadata.t1_uin ?>/mood/<?cs var:qz_metadata.t1_tid ?>.<?cs var:qz_metadata.t1_source ?>" class="c_tx"><?
		cs call:clearContent(qz_metadata.t1_con) ?><?
		cs call:richRetweetList() ?>
	</a> 
	<?cs if:string.length(qz_metadata.t1_source_url) == 0 ?>
		<span class="ifeeds_origin c_tx3">ͨ��<?cs var:qz_metadata.t1_source_name ?></span>
	<?cs else?>
		<a class="ifeeds_origin c_tx3" href="<?cs var:qz_metadata.t1_source_url ?>" target="_blank">ͨ��<?cs var:qz_metadata.t1_source_name ?></a>
	<?cs /if?>
<?cs /if?>
<?cs /def ?>