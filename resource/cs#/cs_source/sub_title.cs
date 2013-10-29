<?cs #:Feed整块内容区的标题 ?>
<?cs def:subTitle() ?>
<?cs if:(qz_metadata.qz_data.feedtype!=1||qz_metadata.metadata.appid==7005) && subcount(qz_metadata.content_box.subtitle) > 0?>
	<?cs def:subTitle-item(con)?>
		<?cs if:con.type == "nick"?>
			<?cs set:con.type = 1?>
			<?cs call:userLink(con, '')?>
		<?cs elif:con.type == "text"?>
			<?cs escape:"html" ?><?cs var:con.text ?><?cs /escape ?>
		<?cs elif:con.type == "url"?>
			<a class="c_tx ui_mr5 " href="<?cs var:con.url ?>" target="_blank"><?cs if:con.text ?><?cs escape:"html" ?><?cs var:con.text ?><?cs /escape ?><?cs else ?><?cs var:con.url ?><?cs /if ?></a>
		<?cs elif:con.type == "p"?>
			<p><?cs escape:"html" ?><?cs var:con ?><?cs /escape ?></p>
		<?cs elif:con.type == "p_url"?>
			<p><a class="c_tx ui_mr5 " href="<?cs var:con.url ?>" target="_blank"><?cs if:con.text ?><?cs escape:"html" ?><?cs var:con.text ?><?cs /escape ?><?cs else ?><?cs var:con.url ?><?cs /if ?></a><p>
		<?cs else ?>
			<span class="ui_mr5"><?cs var:con ?></span>
		<?cs /if?>
	<?cs /def?>
	<?cs with:st = qz_metadata.content_box.subtitle?>
	<p class="f_ct_title">

		<?cs #:这里显示源作者 ?>
		<?cs if:subcount(st.orginuser) > 0 ?>
			<span class="c_tx3 ui_mr5"><?cs call:userLink(st.orginuser, '') ?></span>
		<?cs /if ?>

		<?cs if:st.action.no > 0 ?>
			<?cs set:qz_subtitle_actionWord = qz_static_action[st.action.no] ?>
			<?cs if:string.slice(qz_subtitle_actionWord, string.length(qz_subtitle_actionWord)-3, string.length(qz_subtitle_actionWord)) == "："  ?>
				<span class="c_tx3">
			<?cs else ?>
				<span class="ui_mr5 c_tx3">
			<?cs /if ?>
			<?cs var:qz_subtitle_actionWord ?></span>
		<?cs /if ?>

		<?cs if:subcount(st.con.0) || st.con.0 ?>
			<?cs loop:i = 0, subcount(st.con) - 1, 1 ?>
				<?cs call:subTitle-item(st.con[i])?>
			<?cs /loop ?>
		<?cs else ?>
			<?cs call:subTitle-item(st.con)?>
		<?cs /if?>
	</p>
	<?cs /with?>
<?cs /if?>
<?cs /def ?>