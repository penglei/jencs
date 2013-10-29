<?cs def:getNicknameHTML(uin, nick) ?>
	<a class="q_namecard c_tx ui_mr5" link="nameCard_<?cs var:uin ?>" href="http://user.qzone.qq.com/<?cs var:uin ?>" target="_blank" 
		onclick="QZONE.ICFeeds.VistorFeeds.sendClick(&quot;visitor_nick_feeds&quot;);"
	>
		<?cs var:html_encode(nick, 1) ?>
	</a>
<?cs /def ?>

<?cs if:count < 2 ?>
	<span class="ui_mr5 c_tx3">最近访问了我</span>
<?cs elif:count == 2 ?>
	<span class="ui_mr5 c_tx3">和</span><?cs call:getNicknameHTML(list.0.uin, list.0.nick) ?><span class="ui_mr5 c_tx3">最近访问了我</span>
<?cs else ?>
	<span class="c_tx3">、</span><?cs call:getNicknameHTML(list.0.uin, list.0.nick) ?><span class="ui_mr5 c_tx3">和</span><?cs call:getNicknameHTML(list.1.uin, list.1.nick) ?>
	<?cs if:count > 3 ?>
		<span class="c_tx3">等<?cs var:count ?>人</span>
	<?cs /if ?>
	<span class="ui_mr5 c_tx3">最近访问了我</span>
<?cs /if ?>
