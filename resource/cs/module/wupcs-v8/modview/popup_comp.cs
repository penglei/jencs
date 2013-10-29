<?cs #/*popup用来表示需要业务配合的组件*/?>

<?cs def:v8_popup_start(con)?>
	<a 
		data-cmd="qz_popup" 
		href="javascript:void(0)" 

		<?cs if:con.action.tagtitle ?>
		title="<?cs var:con.action.tagtitle ?>" 
		<?cs /if?>

		<?cs call:v8_echoStyle(con.action)?> 
		<?cs call:v8_echoTextClass(con.action)?> 

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
		data-pickey="<?cs call:ugc_as_html(con.action.pickey,1,1) ?>" 
		data-clicklog="pic" 
		<?cs /if?>
		<?cs if:con.action.imagesrc?>
		data-imagesrc="<?cs call:ugc_as_html(con.action.imagesrc,1,1) ?>" 
		<?cs /if?>
		<?cs if:con.action.originurl?>
		data-originurl="<?cs var:html_encode(con.action.originurl,1) ?>" 
		<?cs /if?>
		<?cs if:con.action.appid?>
		data-appid="<?cs var:con.action.appid?>" 
		<?cs /if?>

		<?cs #:视频feeds是左图右文的标志 ?>
		<?cs if:con.videoType ?>
		data-videotype= "<?cs var:con.videoType ?>" 
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

<?cs def:v8_popup_end()?>
</a>
<?cs /def?>

<?cs def:v8_con_popup_url(con)?>
	<?cs call:v8_popup_start(con.action)?>
	<?cs call:v8_con_url(con)?>
	<?cs call:v8_popup_end()?>
<?cs /def?>

<?cs def:v8_con_popup_txt(con)?>
	<?cs call:v8_popup_start(con)?>
	<?cs var:con.text?>
	<?cs call:v8_popup_end()?>
<?cs /def?>

<?cs def:v8_con_popup(con)?>
	<?cs if:con.type == "txt"?>
		<?cs call:v8_con_popup_txt(con)?>
	<?cs #/*elif:con.type == "url"*/?>
		<?cs #/*call:con_popup_url(con)*/?>
	<?cs /if?>
<?cs /def?>
