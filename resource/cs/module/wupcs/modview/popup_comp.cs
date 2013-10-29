<?cs #/*popup用来表示需要业务配合的组件*/?>

<?cs def:popup_start(con)?>
	<a 
		data-cmd="qz_popup" 
		href="javascript:void(0)" 

		<?cs #hank业务title的数据，version=2走大图浮层 ?>
		<?cs if:con.action.version==2?>
		title="点击看大图" 
		<?cs else?>
		title="点击查看详情" 
		<?cs /if?>

		<?cs call:echoStyle(con.action)?> 
		<?cs call:echoTextClass(con.action)?> 

		<?cs if:con.role?>
		data-type="<?cs var:con.action.role?>" 
		<?cs /if?>

		<?cs #赞被动需要的env参数，后面换新浮层了就去掉吧 ?>
		<?cs if:con.action.env?>
		data-env="<?cs var:con.action.env?>" 
		<?cs /if?>

		<?cs #新版相册浮层需要加的参数 ?>
		<?cs if:con.action.topicid?>
		data-topicid="<?cs var:con.action.topicid?>" 
		<?cs /if?>
		<?cs if:con.action.pickey?>
		data-pickey="<?cs call:ugc_as_html(con.action.pickey,1,1)?>" 
		<?cs /if?>
		<?cs if:con.action.imagesrc?>
		data-imagesrc="<?cs call:ugc_as_html(con.action.imagesrc,1,1)?>" 
		<?cs /if?>
		<?cs if:con.action.originurl?>
		data-originurl="<?cs call:ugc_as_html(con.action.originurl,1,1)?>" 
		<?cs /if?>
		<?cs if:con.action.appid?>
		data-appid="<?cs var:con.action.appid?>" 
		<?cs /if?>
		
		<?cs if:con.leftvideo ?>
		data-leftvideo= "<?cs var:con.leftvideo ?>" 
		<?cs /if ?>
		hotclickPath="<?cs var:con.action.hotclickPath ?>" 
		hotdomain="<?cs var:con.action.hotdomain ?>" 
		data-version="<?cs var:con.action.version?>" 
		data-param="<?cs var:html_encode(con.action.param, 1)?>" 
		data-src="<?cs var:html_encode(con.action.src, 1)?>" 
		data-width="<?cs var:con.action.width?>" 
		data-height="<?cs var:con.action.height?>" 
		data-type="<?cs var:con.action.type?>" 
		data-title="<?cs var:con.action.title?>" 
		data-config="<?cs var:con.action.config?>"
	>
<?cs /def?>

<?cs def:popup_end()?>
</a>
<?cs /def?>

<?cs def:con_popup_url(con)?>
	<?cs call:popup_start(con.action)?>
	<?cs call:con_url(con)?>
	<?cs call:popup_end()?>
<?cs /def?>

<?cs def:con_popup_txt(con)?>
	<?cs call:popup_start(con)?>
	<?cs call:con_txt(con)?>
	<?cs call:popup_end()?>
<?cs /def?>

<?cs def:con_popup(con)?>
	<?cs if:con.type == "txt"?>
		<?cs call:con_popup_txt(con)?>
	<?cs #/*elif:con.type == "url"*/?>
		<?cs #/*call:con_popup_url(con)*/?>
	<?cs /if?>
<?cs /def?>
