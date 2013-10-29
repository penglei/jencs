<?cs #:fkey准备改名为tid ?>
<?cs #:origfkey准备改名为origtid ?>
<?cs def:genFeedsData() ?>
	<i class="none" name="feed_data" 
		data-bitmap="<?cs var:qz_metadata.hdf.bitmap ?>" 
		data-yybitmap="<?cs var:qz_metadata.hdf.yybitmap ?>" 
		data-fkey="<?cs var:qz_metadata.metadata.blogid ?>" 
		data-tid="<?cs var:qz_metadata.metadata.blogid ?>" 
		data-uin="<?cs var:qz_metadata.metadata.uin ?>" 
		data-totweet="<?cs var:qz_metadata.metadata.to_tweet ?>" 
		data-origfkey="<?cs if:qz_metadata.metadata.orgid.0 ?><?cs var:qz_metadata.metadata.orgid.0 ?><?cs elif:qz_metadata.metadata.orgid ?><?cs var:qz_metadata.metadata.orgid ?><?cs /if ?>" 
		data-origtid="<?cs if:qz_metadata.metadata.orgid.0 ?><?cs var:qz_metadata.metadata.orgid.0 ?><?cs elif:qz_metadata.metadata.orgid ?><?cs var:qz_metadata.metadata.orgid ?><?cs /if ?>" 
		data-origuin="<?cs var:qz_metadata.metadata.orguin ?>" 
		data-issignin="<?cs var:qz_metadata.metadata.signin ?>" 
		data-source="<?cs var:qz_metadata.metadata.mood_source ?>" 
		data-retweetcount="<?cs if:qz_metadata.qz_data.key2.ZS.cnt ?><?cs var:qz_metadata.qz_data.key2.ZS.cnt?><?cs elif:qz_metadata.qz_data.key1.ZS.cnt ?><?cs var:qz_metadata.qz_data.key1.ZS.cnt ?><?cs else ?>0<?cs /if ?>"
	></i>
<?cs /def ?>

<?cs #:这里自动call一下这个，则全文都能直接使用isAuthUser_r来判断当前用户是否认证空间 ?>
<?cs call:isAuthUser() ?>
<?cs call:isAlpha() ?>
<?cs call:genFeedsData() ?>
<?cs #主入口?>
<?cs call:start("summary")?>
<span class="none feed_log_data" 
	<?cs if:string.length(qz_metadata.metadata.srctype) > 0 ?>
		<?cs if:string.length(qz_metadata.metadata.feedoz) > 0 ?>
			srctype="<?cs var:qz_metadata.metadata.srctype ?>"
			<?cs set:srctype = 1000 + qz_metadata.metadata.srctype ?>
			data-showlog="<?cs var:qz_metadata.metadata.feedoz ?>|202_<?cs var:srctype ?>" 
		<?cs /if ?>
	<?cs else ?>
		<?cs if:string.length(qz_metadata.metadata.feedoz) > 0 ?>
			data-showlog="<?cs var:qz_metadata.metadata.feedoz ?>" 
		<?cs /if ?>
	<?cs /if ?>
	<?cs if:qz_metadata.metadata.orguin ?>
		data-original="<?cs var:qz_metadata.metadata.orguin ?>" 
	<?cs elif:qz_metadata.metadata.shareorguin ?>
		data-original="<?cs var:qz_metadata.metadata.shareorguin ?>" 
	<?cs /if ?>
>
</span>