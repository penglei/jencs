<?cs def:actionTitle(mod) ?>
	<?cs #:动作 ?>
	<?cs def:actionTitle_action(mod) ?>
		<?cs set:actionNo = qz_metadata[getActionTitleName.ret].action.no ?>
		<?cs set:actionWord = qz_static_action[actionNo] ?>
		<?cs if: string.length(actionWord)>0 ?>
			<?cs #:空转发、空分享则不显示 ?>
			<?cs if:qz_metadata.qz_data.feedtype!=1 && (actionNo == 4 || actionNo == 5 ) && ((getContentNodeName.ret=="lastmsg"&&subcount(qz_metadata.metadata[getContentNodeName.ret].con)==0)||
				subcount(qz_metadata[getActionTitleName.ret].content)==0 ) ?>
				<?cs #:啥都不做 ?>
			<?cs else ?>
				<?cs if: string.slice(actionWord, string.length(actionWord)-3, string.length(actionWord)) == "："  ?>
					<span class="c_tx3">
				<?cs else ?>
					<span class="ui_mr5 c_tx3">
				<?cs /if ?>
				<?cs var:actionWord ?></span>
			<?cs /if ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs #:昵称 ?>
	<?cs def:actionTitle_nick(mod) ?>
		<?cs if:string.length(qz_metadata[getActionTitleName.ret].nick.name)!= 0 ?>
			<span class="ui_mr5"><?cs call:userLink(qz_metadata[getActionTitleName.ret].nick, "")?></span>
		<?cs /if ?>
	<?cs /def ?>

	<?cs #:物件 ?>
	<?cs def:actionTitle_object(mod) ?>
		<?cs if:mod.pos > -1 ?>
			<?cs if:mod.pos == 0 ?>
				<?cs if:qz_static_object[qz_metadata[getActionTitleName.ret].object[mod.pos].no] ?>
					<span class="ui_mr5  c_tx3"><?cs var:qz_static_object[qz_metadata[getActionTitleName.ret].object[mod.pos].no] ?></span>
				<?cs elif:qz_static_object[qz_metadata[getActionTitleName.ret].object.no] ?>
					<span class="ui_mr5  c_tx3"><?cs var:qz_static_object[qz_metadata[getActionTitleName.ret].object.no] ?></span>
				<?cs /if ?>
			<?cs else ?>
				<?cs if:qz_static_object[qz_metadata[getActionTitleName.ret].object[mod.pos].no] ?>
				<span class="ui_mr5  c_tx3"><?cs var:qz_static_object[qz_metadata[getActionTitleName.ret].object[mod.pos].no] ?></span>
				<?cs /if ?>
			<?cs /if ?>
		<?cs else ?>
			<?cs if:qz_static_object[qz_metadata[getActionTitleName.ret].object[mod.pos].no] ?>
			<span class="ui_mr5  c_tx3"><?cs var:qz_static_object[qz_metadata[getActionTitleName.ret].object.no] ?></span>
			<?cs /if ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs #:内容 ?>
	<?cs def:actionTitle_content(mod) ?>
		<?cs if: getContentNodeName.ret=="lastmsg" ?>
			<?cs call:richContent-title(qz_metadata.metadata[getContentNodeName.ret]) ?>
		<?cs else ?>
			<?cs call:richContent-title(qz_metadata[getActionTitleName.ret][getContentNodeName.ret]) ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs #:链接 ?>
	<?cs def:actionTitle_url(mod) ?>
		<?cs if:subcount(qz_metadata[getActionTitleName.ret].url.url)>1 ?>
			<?cs set:url_url = qz_metadata[getActionTitleName.ret].url.url[mod.pos] ?>
			<?cs set:url_text = qz_metadata[getActionTitleName.ret].url.text[mod.pos] ?>
		<?cs else ?>
			<?cs set:url_url = qz_metadata[getActionTitleName.ret].url.url ?>
			<?cs set:url_text = qz_metadata[getActionTitleName.ret].url.text ?>
		<?cs /if ?>
		<?cs if:url_url ?>
			<a class="c_tx ui_mr5" href="<?cs var:url_url ?>" target="_blank"><?cs var:url_text ?></a>
		<?cs /if ?>
	<?cs /def ?>	



	<?cs def:actionTitleModCaller(mods) ?>
		<?cs #:引用 ?>
		<?cs def:actionTitle_quote(mods) ?>
			<i class="ui_ico quote_before c_tx3">“</i>
				<?cs call:actionTitleModCaller(mods) ?>
			<i class="ui_ico quote_after c_tx3">”</i>
		<?cs /def ?>

		<?cs def:actionTitleModCaller-items(mod) ?>
			<?cs if:mod.name == "quote" ?>
				<?cs call:actionTitle_quote(mod.mod) ?>
			<?cs /if ?>
			<?cs if:mod.name == "action" ?>
				<?cs call:actionTitle_action(mod) ?>
			<?cs /if ?>
			<?cs if:mod.name == "object" ?>
				<?cs call:actionTitle_object(mod) ?>
			<?cs /if ?>
			<?cs if:mod.name == "nick" ?>
				<?cs call:actionTitle_nick(mod) ?>
			<?cs /if ?>
			<?cs if:mod.name == "url" ?>
				<?cs call:actionTitle_url(mod) ?>
			<?cs /if ?>
			<?cs if:mod.name == "content" ?>
				<?cs call:actionTitle_content(mod) ?>
			<?cs /if ?>
		<?cs /def ?>
		<?cs #:这个来判断是否数组的方法,从来就没有爽过 叼~~?>
		<?cs if:subcount(mods.0) > 0 ?>
			<?cs each:mod = mods ?>
				<?cs call:actionTitleModCaller-items(mod) ?>
			<?cs /each ?>
		<?cs else ?>
			<?cs call:actionTitleModCaller-items(mods) ?>
		<?cs /if ?>
	<?cs /def ?>

	<?cs call:actionTitleModCaller(mod.mod) ?>
<?cs /def ?>